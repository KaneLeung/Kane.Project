#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：Mapper
* 类 描 述 ：表达式树实现高性能对象映射
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/5/16 22:52:30
* 更新时间 ：2020/5/16 22:52:30
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
#if !NET40
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using static System.Linq.Expressions.Expression;

namespace Kane.Extension
{
    /// <summary>
    /// 表达式树实现高性能对象映射
    /// <para>参考来自https://www.cnblogs.com/castyuan/p/9324088.html</para>
    /// </summary>
    /// <typeparam name="Source">原对象</typeparam>
    /// <typeparam name="Target">目标对象</typeparam>
    public class Mapper<Source, Target> where Source : class where Target : class, new()
    {
        private readonly static Func<Source, Target> MapFunc = GetMapFunc();
        private readonly static Action<Source, Target> MapAction = GetMapAction();
        private static MapperConfig<Source, Target> Config { get; set; }

        #region 设置映射规则 + SetConfig(Action<MapperConfig<Source, Target>> action)
        /// <summary>
        /// 设置映射规则
        /// </summary>
        /// <param name="action"></param>
        public static void SetConfig(Action<MapperConfig<Source, Target>> action)
        {
            Config ??= new MapperConfig<Source, Target>();
            action.Invoke(Config);
        }
        #endregion

        #region 将对象Source映射为Target + Map(Source source, Action<Source, Target> action = null)
        /// <summary>
        /// 将对象Source映射为Target
        /// </summary>
        /// <param name="source">原对象</param>
        /// <param name="action">映射后执行的Action</param>
        /// <returns></returns>
        public static Target Map(Source source, Action<Source, Target> action = null)
        {
            if (source == null) return null;
            var target = MapFunc(source);
            Config?.BindAction?.Invoke(source, target);
            action?.Invoke(source, target);
            return target;
        }
        #endregion

        #region 将对象Source映射为Target + Map(Source source, Target target, Action<Source, Target> action = null)
        /// <summary>
        /// 将非集合对象Source映射为Target
        /// </summary>
        /// <param name="source">原对象</param>
        /// <param name="target">映射的对象</param>
        /// <param name="action">映射后执行的Action</param>
        public static void Map(Source source, Target target, Action<Source, Target> action = null)
        {
            if (source == null || target == null) return;
            MapAction(source, target);
            Config?.BindAction?.Invoke(source, target);
            action?.Invoke(source, target);
        }
        #endregion

        #region 将集合对象Source映射为Target + MapList(IEnumerable<Source> sources, Action<Source, Target> action = null)
        /// <summary>
        /// 将集合对象Source映射为Target
        /// </summary>
        /// <param name="sources">原集合对象</param>
        /// <param name="action">映射后执行的Action</param>
        /// <returns></returns>
        public static List<Target> MapList(IEnumerable<Source> sources, Action<Source, Target> action = null)
        {
            if (sources == null) return null;
            var list = new List<Target>();
            foreach (var item in sources) list.Add(Map(item, action));
            return list;
        }
        #endregion


        private static Func<Source, Target> GetMapFunc()
        {
            return source =>
            {
                var target = new Target();
                MapAction(source, target);
                return target;
            };
        }

        private static Action<Source, Target> GetMapAction()
        {
            Config ??= new MapperConfig<Source, Target>();
            var sourceType = typeof(Source);
            var targetType = typeof(Target);
            if (sourceType.IsCollection() || targetType.IsCollection()) throw new NotSupportedException("Enumerable types are not supported,please use MapList method.");
            //Func委托传入变量
            var sourceParameter = Parameter(sourceType, "p");
            var targetParameter = Parameter(targetType, "t");
            //创建一个表达式集合
            var expressions = new List<Expression>();
            // x.GetIndexParameters().Length ==0  过滤 this[index]  索引项
            var targetTypes = targetType.GetProperties().Where(k => k.GetIndexParameters().Length == 0 && (k.PropertyType.IsPublic || k.PropertyType.IsNestedPublic) && k.CanWrite);
            //过滤忽略项
            if (Config.IgnoreColoums != null && Config.IgnoreColoums.Count > 0)
            {
                targetTypes = targetTypes.Where(x => !Config.IgnoreColoums.Contains(x.Name));
            }
            var sourceTypes = sourceType.GetProperties().Where(k => k.GetIndexParameters().Length == 0 && (k.PropertyType.IsPublic || k.PropertyType.IsNestedPublic) && k.CanRead);
            foreach (var targetItem in targetTypes)
            {
                var sourceItem = sourceTypes.FirstOrDefault(x => string.Compare(x.Name, targetItem.Name, Config.IgnoreCase) == 0);
                //判断实体的读写权限
                if (sourceItem == null || !sourceItem.CanRead || sourceItem.PropertyType.IsNotPublic) continue;
                //标注NotMapped特性的属性忽略转换
                if (sourceItem.GetCustomAttribute<NotMappedAttribute>() != null) continue;
                var sourceProperty = Property(sourceParameter, sourceItem);
                var targetProperty = Property(targetParameter, targetItem);
                //当非值类型且类型不相同时
                if (!sourceItem.PropertyType.IsValueType && sourceItem.PropertyType != targetItem.PropertyType
                    && sourceItem.PropertyType != typeof(string) && targetItem.PropertyType != typeof(string)
                    )
                {
                    //判断都是(非泛型、非数组)class
                    if (sourceItem.PropertyType.IsClass && targetItem.PropertyType.IsClass
                        && !sourceItem.PropertyType.IsArray && !targetItem.PropertyType.IsArray
                        && !sourceItem.PropertyType.IsGenericType && !targetItem.PropertyType.IsGenericType)
                    {
                        var expression = GetClassExpression(sourceProperty, sourceItem.PropertyType, targetItem.PropertyType);
                        expressions.Add(Assign(targetProperty, expression));
                    }
                    //集合数组类型的转换
                    if (typeof(IEnumerable).IsAssignableFrom(sourceItem.PropertyType) && typeof(IEnumerable).IsAssignableFrom(targetItem.PropertyType))
                    {
                        var expression = GetListExpression(sourceProperty, sourceItem.PropertyType, targetItem.PropertyType);
                        expressions.Add(Assign(targetProperty, expression));
                    }
                    continue;
                }
                //可空类型转换到非可空类型，当可空类型值为null时，用默认值赋给目标属性；不为null就直接转换
                if (sourceItem.PropertyType.IsNullable() && !targetItem.PropertyType.IsNullable())
                {
                    var hasValueExpression = Equal(Property(sourceProperty, "HasValue"), Constant(true));
                    var conditionItem = Condition(hasValueExpression, Convert(sourceProperty, targetItem.PropertyType), Default(targetItem.PropertyType));
                    expressions.Add(Assign(targetProperty, conditionItem));
                    continue;
                }

                //非可空类型转换到可空类型，直接转换
                if (!sourceItem.PropertyType.IsNullable() && targetItem.PropertyType.IsNullable())
                {
                    var memberExpression = Convert(sourceProperty, targetItem.PropertyType);
                    expressions.Add(Assign(targetProperty, memberExpression));
                    continue;
                }
                if (targetItem.PropertyType != sourceItem.PropertyType) continue;
                expressions.Add(Assign(targetProperty, sourceProperty));
            }
            //当Target!=null判断source是否为空
            var testSource = NotEqual(sourceParameter, Constant(null, sourceType));
            var ifTrueSource = Block(expressions);
            var conditionSource = IfThen(testSource, ifTrueSource);
            //判断target是否为空
            var testTarget = NotEqual(targetParameter, Constant(null, targetType));
            var conditionTarget = IfThen(testTarget, conditionSource);
            var lambda = Lambda<Action<Source, Target>>(conditionTarget, sourceParameter, targetParameter);
            return lambda.Compile();
        }

        private static Expression GetClassExpression(Expression sourceProperty, Type sourceType, Type targetType)
        {
            //条件p.Item!=null
            var testItem = NotEqual(sourceProperty, Constant(null, sourceType));
            //构造回调 Mapper<Source, Target>.Map()
            var mapperType = typeof(Mapper<,>).MakeGenericType(sourceType, targetType);
            var actionType = typeof(Action<,>).MakeGenericType(sourceType, targetType);
            var iftrue = Call(mapperType.GetMethod(nameof(Map), new Type[] { sourceType, actionType }), sourceProperty, Constant(null, actionType));
            var conditionItem = Condition(testItem, iftrue, Constant(null, targetType));
            return conditionItem;
        }

        private static Expression GetListExpression(Expression sourceProperty, Type sourceType, Type targetType)
        {
            //条件p.Item!=null
            var testItem = NotEqual(sourceProperty, Constant(null, sourceType));
            //构造回调 Mapper<Source, Target>.MapList()
            var sourceArg = sourceType.IsArray ? sourceType.GetElementType() : sourceType.GetGenericArguments()[0];
            var targetArg = targetType.IsArray ? targetType.GetElementType() : targetType.GetGenericArguments()[0];
            var mapperType = typeof(Mapper<,>).MakeGenericType(sourceArg, targetArg);
            var actionType = typeof(Action<,>).MakeGenericType(sourceArg, targetArg);
            var mapperExecMap = Call(mapperType.GetMethod(nameof(MapList), new Type[] { sourceType, actionType }), sourceProperty, Constant(null, actionType));
            Expression iftrue;
            if (targetType == mapperExecMap.Type) iftrue = mapperExecMap;
            else if (targetType.IsArray)//数组类型调用ToArray()方法
            {
                iftrue = Call(typeof(Enumerable), nameof(Enumerable.ToArray), new[] { mapperExecMap.Type.GenericTypeArguments[0] }, mapperExecMap);
            }
            else if (typeof(IDictionary).IsAssignableFrom(targetType)) iftrue = Constant(null, targetType);//字典类型不转换
            else iftrue = Convert(mapperExecMap, targetType);
            var conditionItem = Condition(testItem, iftrue, Constant(null, targetType));
            return conditionItem;
        }
    }
}
#endif
#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：ClassEx
* 类 描 述 ：反射类扩展
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/16 23:08:22
* 更新时间 ：2020/05/28 10:42:22
* 版 本 号 ：v1.0.2.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Kane.Extension
{
    /// <summary>
    /// 反射类扩展
    /// </summary>
    public static class ClassEx
    {
        internal const BindingFlags BINDING_FLAGS = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        #region 获取类所有的属性信息 + GetProps<T>(this T target)
        /// <summary>
        /// 获取类所有的属性信息
        /// </summary>
        /// <param name="target">反射对象</param>
        /// <returns>属性信息</returns>
        public static PropertyInfo[] GetProps<T>(this T target) => target.GetType().GetProperties(BINDING_FLAGS);
        #endregion

        #region 检测对象是否包含指定【属性】 + HasProp<T>(this T target, string name)
        /// <summary>
        /// 检测对象是否包含指定【属性】
        /// </summary>
        /// <typeparam name="T">要检测对象类型</typeparam>
        /// <param name="target">要检测对象</param>
        /// <param name="name">属性名</param>
        /// <returns></returns>
        public static bool HasProp<T>(this T target, string name) => target.GetType().GetProperty(name, BINDING_FLAGS) != null;
        #endregion

        #region 获取属性的值 + GetPropValue<T>(this object target, string name)
        /// <summary>
        /// 获取属性的值
        /// </summary>
        /// <param name="target">反射对象</param>
        /// <param name="name">属性名</param>
        /// <returns>属性值</returns>
        public static T GetPropValue<T>(this object target, string name)
        {
            PropertyInfo fieldInfo = target.GetType().GetProperty(name, BINDING_FLAGS);
            return (T)fieldInfo.GetValue(target, null);
        }
        #endregion

        #region 设置属性的值 + SetPropValue<T, K>(this T target, string name, K value)
        /// <summary>
        /// 设置属性的值
        /// </summary>
        /// <param name="target">反射对象</param>
        /// <param name="name">属性名</param>
        /// <param name="value">值</param>
        public static void SetPropValue<T, K>(this T target, string name, K value)
        {
            PropertyInfo fieldInfo = target.GetType().GetProperty(name, BINDING_FLAGS);
            fieldInfo.SetValue(target, Convert.ChangeType(value, fieldInfo.PropertyType), null);
        }
        #endregion

        #region 获取所有的字段信息 + GetFields<T>(this T target)
        /// <summary>
        /// 获取所有的字段信息
        /// </summary>
        /// <param name="target">反射对象</param>
        /// <returns>字段信息</returns>
        public static FieldInfo[] GetFields<T>(this T target) => target.GetType().GetFields(BINDING_FLAGS);
        #endregion

        #region 获取单个字段的值 + GetFieldValue<T>(this object target, string name) where T : class
        /// <summary>
        /// 获取单个字段的值
        /// </summary>
        /// <param name="target">反射对象</param>
        /// <param name="name">字段名</param>
        /// <typeparam name="T">约束返回的T</typeparam>
        /// <returns>T类型</returns>
        public static T GetFieldValue<T>(this object target, string name)
        {
            FieldInfo fieldInfo = target.GetType().GetField(name, BINDING_FLAGS);
            return (T)fieldInfo.GetValue(target);
        }
        #endregion

        #region 设置单个字段的值 + SetFieldValue<T, K>(this T target, string name, K value)
        /// <summary>
        /// 设置单个字段的值
        /// </summary>
        /// <param name="target">反射对象</param>
        /// <param name="name">字段名</param>
        /// <param name="value">值</param>
        public static void SetFieldValue<T, K>(this T target, string name, K value)
        {
            FieldInfo fieldInfo = target.GetType().GetField(name, BINDING_FLAGS);
            fieldInfo.SetValue(target, value);
        }
        #endregion

        #region 根据类的类型型创建类实例 + CreateInstance(this Type target)
        /// <summary>  
        /// 根据类的类型型创建类实例。  
        /// </summary>  
        /// <param name="target">将要创建的类型。</param>  
        /// <returns>返回创建的类实例。</returns>  
        public static object CreateInstance(this Type target) => Activator.CreateInstance(target);
        #endregion

#if !NET40
        #region 根据类的名称,属性列表创建型实例。 + CreateInstance(string className, List<PropInfo> propInfos)
        /// <summary>  
        /// 根据类的名称,属性列表创建型实例。  
        /// </summary>  
        /// <param name="className">将要创建的类的名称。</param>  
        /// <param name="propInfos">将要创建的类的属性列表。</param>  
        /// <returns>返回创建的类实例</returns>  
        public static object CreateInstance(string className, List<PropInfo> propInfos) => Activator.CreateInstance(AddProp(BuildType(className), propInfos));
        #endregion

        #region 根据属性列表创建类的实例,默认类名为DefaultClass + CreateInstance(List<PropInfo> propInfos)
        /// <summary>  
        /// 根据属性列表创建类的实例,默认类名为DefaultClass,由于生成的类不是强类型,所以类名可以忽略。  
        /// </summary>  
        /// <param name="propInfos">将要创建的类的属性列表</param>  
        /// <returns>返回创建的类的实例。</returns>  
        public static object CreateInstance(List<PropInfo> propInfos) => CreateInstance("DefaultClass", propInfos);
        #endregion

        #region 创建一个没有成员的类型的实例,类名为"DefaultClass" + BuildType()
        /// <summary>  
        /// 创建一个没有成员的类型的实例,类名为"DefaultClass"。  
        /// </summary>  
        /// <returns>返回创建的类型的实例。</returns>  
        public static Type BuildType() => BuildType("DefaultClass");
        #endregion

        #region 根据类名创建一个没有成员的类型的实例 + BuildType(string className)
        /// <summary>  
        /// 根据类名创建一个没有成员的类型的实例。  
        /// </summary>  
        /// <param name="className">将要创建的类型的实例的类名。</param>  
        /// <returns>返回创建的类型的实例。</returns>  
        public static Type BuildType(string className)
        {
            //什么是Assembly(程序集) ?
            //Assembly是一个包含来程序的名称，版本号，自我描述，文件关联关系和文件位置等信息的一个集合。在.net框架中通过Assembly类来支持，该类位于System.Reflection下，物理位置位于：mscorlib.dll。
            AssemblyName name = new AssemblyName
            {
                Name = "DynamicAssembly"
            };
            //创建一个程序集,设置为AssemblyBuilderAccess.RunAndCollect。  
            AssemblyBuilder builder = AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndCollect);
            //创建一个单模程序块。  
            ModuleBuilder moduleBuilder = builder.DefineDynamicModule(name.Name);
            //创建TypeBuilder。  
            TypeBuilder typeBuilder = moduleBuilder.DefineType(className, TypeAttributes.Public);
            //创建类型。
            Type result = typeBuilder.CreateTypeInfo();
            //保存程序集,以便可以被Ildasm.exe解析,或被测试程序引用。  
            //builder.Save(DynamicAssembly.Name + ".dll");  
            return result;
        }
        #endregion


        #region 添加属性到类型的实例 + AddProp(this Type classType, List<PropInfo> propInfos)
        /// <summary>  
        /// 添加属性到类型的实例,注意:该操作会将其它成员清除掉,其功能有待完善。  
        /// </summary>  
        /// <param name="classType">指定类型的实例。</param>  
        /// <param name="propInfos">表示属性的一个列表。</param>  
        /// <returns>返回处理过的类型的实例。</returns>  
        public static Type AddProp(this Type classType, List<PropInfo> propInfos)
        {
            //合并先前的属性,以便一起在下一步进行处理。  
            MergeProp(classType, propInfos);
            //把属性加入到Type。  
            return AddPropToType(classType, propInfos);
        }
        #endregion

        #region 添加属性到类型的实例 + AddProp(this Type classType, PropInfo propInfo)
        /// <summary>  
        /// 添加属性到类型的实例,注意:该操作会将其它成员清除掉,其功能有待完善。  
        /// </summary>  
        /// <param name="classType">指定类型的实例。</param>  
        /// <param name="propInfo">表示一个属性。</param>  
        /// <returns>返回处理过的类型的实例。</returns>  
        public static Type AddProp(this Type classType, PropInfo propInfo)
        {
            List<PropInfo> propInfos = new List<PropInfo> { propInfo };
            //合并先前的属性,以便一起在下一步进行处理。  
            MergeProp(classType, propInfos);
            //把属性加入到Type。  
            return AddPropToType(classType, propInfos);
        }
        #endregion

        #region 给对象实例添加新属性并返回新对象实例 + AddProp(this object target, PropInfo propInfo)
        /// <summary>
        /// 给对象实例添加新属性并返回新对象实例
        /// </summary>
        /// <param name="target"></param>
        /// <param name="propInfo">自定义属性对象</param>
        /// <returns></returns>
        public static object AddProp(this object target, PropInfo propInfo) => AddProp(target, new List<PropInfo>() { propInfo });
        #endregion

        #region 给对象实例添加新属性并返回新对象实例 + AddProp(this object target, List<PropInfo> propInfos)
        /// <summary>
        /// 给对象实例添加新属性并返回新对象实例
        /// </summary>
        /// <param name="target"></param>
        /// <param name="propInfos">自定义属性对象集合</param>
        /// <returns></returns>
        public static object AddProp(this object target, List<PropInfo> propInfos)
        {
            Type originType = target.GetType();
            var newProps = propInfos.ToDictionary(i => i.PropName, i => i.PropValue);
            var result = AddProp(originType, propInfos).CreateInstance();
            foreach (var originProperty in originType.GetProperties())
            {
                result.SetPropValue(originProperty.Name, originProperty.GetValue(target));
            }
            foreach (var prop in newProps)
            {
                result.SetPropValue(prop.Key, prop.Value);
            }
            return result;
        }
        #endregion

        #region 给对象实例添加新属性并返回新对象实例 + AddProp(this object target, string propName, object propValue)
        /// <summary>
        /// 给对象实例添加新属性并返回新对象实例
        /// </summary>
        /// <param name="target"></param>
        /// <param name="propName">属性名</param>
        /// <param name="propValue">属性值</param>
        /// <returns></returns>
        public static object AddProp(this object target, string propName, object propValue)
        {
            return AddProp(target, new List<PropInfo>() { new PropInfo(propValue.GetType(), propName, propValue) });
        }
        #endregion

        #region 从类型的实例中移除属性 + DeleteProp(this Type classType, string propName)
        /// <summary>  
        /// 从类型的实例中移除属性,注意:该操作会将其它成员清除掉,其功能有待完善。  
        /// </summary>  
        /// <param name="classType">指定类型的实例。</param>  
        /// <param name="propName">要移除的属性。</param>  
        /// <returns>返回处理过的类型的实例。</returns>  
        public static Type DeleteProp(this Type classType, string propName)
        {
            //合并先前的属性,以便一起在下一步进行处理。  
            List<PropInfo> propInfos = SeparateProp(classType, new List<string> { propName });
            //把属性加入到Type。  
            return AddPropToType(classType, propInfos);
        }
        #endregion

        #region 从类型的实例中移除属性 + DeleteProp(this Type classType, List<string> propName)
        /// <summary>  
        /// 从类型的实例中移除属性,注意:该操作会将其它成员清除掉,其功能有待完善。  
        /// </summary>  
        /// <param name="classType">指定类型的实例。</param>  
        /// <param name="propName">要移除的属性列表。</param>  
        /// <returns>返回处理过的类型的实例。</returns>  
        public static Type DeleteProp(this Type classType, List<string> propName)
        {
            //合并先前的属性,以便一起在下一步进行处理。  
            List<PropInfo> propInfos = SeparateProp(classType, propName);
            //把属性加入到Type。  
            return AddPropToType(classType, propInfos);
        }
        #endregion

        #region 删除对象的属性并返回新对象实例 + DeleteProp(this object target, string property)
        /// <summary>
        /// 删除对象的属性并返回新对象实例
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="property">属性名</param>
        /// <returns></returns>
        public static object DeleteProp(this object target, string property) => DeleteProp(target, new List<string>() { property });
        #endregion

        #region 删除对象的属性并返回新对象实例 + DeleteProp(this object target, List<string> propNames)
        /// <summary>
        /// 删除对象的属性并返回新对象实例
        /// </summary>
        /// <param name="target"></param>
        /// <param name="propNames">属性名集合</param>
        /// <returns></returns>
        public static object DeleteProp(this object target, List<string> propNames)
        {
            PropertyInfo[] originProps = target.GetType().GetProperties();
            Type type = target.GetType();
            foreach (string name in propNames)
            {
                type = type.DeleteProp(name);
            }
            var newInstance = type.CreateInstance();
            foreach (var prop in newInstance.GetProps())
            {
                newInstance.SetPropValue(prop.Name, originProps.FirstOrDefault(k => k.Name.Equals(prop.Name)).GetValue(target));
            }
            return newInstance;
        }
        #endregion

        #region 把类型的实例type和propInfos参数里的属性进行合并。+ MergeProp(Type type, List<PropInfo> propInfos)
        /// <summary>  
        /// 把类型的实例type和propInfos参数里的属性进行合并。
        /// </summary>  
        /// <param name="type">实例type</param>  
        /// <param name="propInfos">里面包含属性列表的信息。</param>  
        private static void MergeProp(Type type, List<PropInfo> propInfos)
        {
            foreach (PropertyInfo pi in type.GetProperties())
            {
                propInfos.Add(new PropInfo(pi.PropertyType, pi.Name));
            }
        }
        #endregion

        #region 从类型的实例type的属性移除属性列表removeList,返回的新属性列表。 + SeparateProp(Type type, List<string> removeList)
        /// <summary>  
        /// 从类型的实例type的属性移除属性列表removeList,返回的新属性列表。
        /// </summary>  
        /// <param name="type">类型的实例t。</param>  
        /// <param name="removeList">要移除的属性列表。</param>  
        private static List<PropInfo> SeparateProp(Type type, List<string> removeList)
        {
            List<PropInfo> propInfos = new List<PropInfo>();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (removeList.Any(k => k != pi.Name)) propInfos.Add(new PropInfo(pi.PropertyType, pi.Name));
            }
            return propInfos;
        }
        #endregion

        #region 把propInfos参数里的属性加入到typeBuilder中 + AddPropToTypeBuilder(TypeBuilder typeBuilder, List<PropInfo> propInfos)
        /// <summary>  
        /// 把propInfos参数里的属性加入到typeBuilder中。注意:该操作会将其它成员清除掉,其功能有待完善。  
        /// </summary>  
        /// <param name="typeBuilder">类型构造器的实例。</param>  
        /// <param name="propInfos">里面包含属性列表的信息。</param>  
        private static void AddPropToTypeBuilder(TypeBuilder typeBuilder, List<PropInfo> propInfos)
        {
            // 属性Set和Get方法要一个专门的属性。这里设置为Public。  
            var attr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
            // 添加属性到myTypeBuilder。  
            foreach (PropInfo info in propInfos)
            {
                //定义字段。  
                FieldBuilder customerNameBldr = typeBuilder.DefineField(info.FieldName, info.Type, FieldAttributes.Private);
                //定义属性。  
                //最后一个参数为null,因为属性没有参数。  
                var custNamePropBldr = typeBuilder.DefineProperty(info.PropName, PropertyAttributes.HasDefault, info.Type, null);
                //定义Get方法。  
                var custNameGetPropMthdBldr = typeBuilder.DefineMethod(info.GetPropertyMethodName, attr, info.Type, Type.EmptyTypes);
                var custNameGetIL = custNameGetPropMthdBldr.GetILGenerator();
                try
                {
                    custNameGetIL.Emit(OpCodes.Ldarg_0);
                    custNameGetIL.Emit(OpCodes.Ldfld, customerNameBldr);
                    custNameGetIL.Emit(OpCodes.Ret);
                }
                catch (Exception)
                {
                    // ignored
                }
                //定义Set方法。  
                var custNameSetPropMthdBldr = typeBuilder.DefineMethod(info.SetPropertyMethodName, attr, null, new[] { info.Type });
                var custNameSetIL = custNameSetPropMthdBldr.GetILGenerator();

                custNameSetIL.Emit(OpCodes.Ldarg_0);
                custNameSetIL.Emit(OpCodes.Ldarg_1);
                custNameSetIL.Emit(OpCodes.Stfld, customerNameBldr);
                custNameSetIL.Emit(OpCodes.Ret);
                //把创建的两个方法(Get,Set)加入到PropertyBuilder中。  
                custNamePropBldr.SetGetMethod(custNameGetPropMthdBldr);
                custNamePropBldr.SetSetMethod(custNameSetPropMthdBldr);
            }
        }
        #endregion

        #region 把属性加入到类型的实例 + AddPropToType(this Type classType, List<PropInfo> propInfos)
        /// <summary>  
        /// 把属性加入到类型的实例。  
        /// </summary>  
        /// <param name="classType">类型的实例。</param>  
        /// <param name="propInfos">要加入的属性列表。</param>  
        /// <returns>返回处理过的类型的实例。</returns>  
        public static Type AddPropToType(this Type classType, List<PropInfo> propInfos)
        {
            AssemblyName name = new AssemblyName
            {
                Name = "DynamicAssembly"
            };
            //创建一个程序集,设置为AssemblyBuilderAccess.RunAndCollect。  
            AssemblyBuilder builder = AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndCollect);
            //创建一个单模程序块。  
            ModuleBuilder moduleBuilder = builder.DefineDynamicModule(name.Name);
            //创建TypeBuilder。  
            // ReSharper disable once AssignNullToNotNullAttribute
            TypeBuilder typeBuilder = moduleBuilder.DefineType(classType.FullName, TypeAttributes.Public);
            //把lcpi中定义的属性加入到TypeBuilder。将清空其它的成员。其功能有待扩展,使其不影响其它成员。  
            AddPropToTypeBuilder(typeBuilder, propInfos);
            //创建类型。
            Type result = typeBuilder.CreateTypeInfo();
            //保存程序集,以便可以被Ildasm.exe解析,或被测试程序引用。  
            //builder.Save(DynamicAssembly.Name + ".dll");  
            return result;
        }
        #endregion
#endif

        #region 自定义的属性信息类型 + PropInfo
        /// <summary>  
        /// 自定义的属性信息类型。  
        /// </summary>  
        public class PropInfo
        {
            /// <summary>  
            /// 根据属性类型名称,属性名称构造实例。  
            /// </summary>  
            /// <param name="type">属性类型名称。</param>  
            /// <param name="propName">属性名称。</param>  
            public PropInfo(Type type, string propName)
            {
                Type = type;
                PropName = propName;
            }
            /// <summary>
            /// 根据属性类型名称,属性名称构造实例，并设置属性值。
            /// </summary>
            /// <param name="type"></param>
            /// <param name="propName"></param>
            /// <param name="propValue"></param>
            public PropInfo(Type type, string propName, object propValue) : this(type, propName)
            {
                PropValue = propValue;
            }
            /// <summary>  
            /// 获取或设置属性类型名称。  
            /// </summary>  
            public Type Type { get; set; }
            /// <summary>  
            /// 获取或设置属性名称。  
            /// </summary>  
            public string PropName { get; set; }
            /// <summary>
            /// 属性值
            /// </summary>
            public object PropValue { get; set; }
            /// <summary>  
            /// 获取属性字段名称。  
            /// </summary>  
            public string FieldName
            {
                get
                {
                    if (PropName.Length < 1) return string.Empty;
                    return PropName.Substring(0, 1).ToLower() + PropName.Substring(1);
                }
            }
            /// <summary>  
            /// 获取属性在IL中的Set方法名。  
            /// </summary>  
            public string SetPropertyMethodName => "set_" + PropName;
            /// <summary>  
            ///  获取属性在IL中的Get方法名。  
            /// </summary>  
            public string GetPropertyMethodName => "get_" + PropName;
        }
        #endregion

        #region 类对象的深克隆，利用反射 + DeepClone<T>(this T source) where T : class, new()
        /// <summary>
        /// 类对象的深克隆，利用反射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">源对象</param>
        /// <returns></returns>
        public static T DeepClone<T>(this T source) where T : class, new()
        {
            Type type = source.GetType();
            object newObject = Activator.CreateInstance(type);
            PropertyInfo[] infos = type.GetProperties();
            for (int i = 0; i < infos.Length; i++)
            {
                PropertyInfo info = infos[i];
#if NET40
                info.SetValue(newObject, info.GetValue(source, null), null);
#else
                info.SetValue(newObject, info.GetValue(source));
#endif
            }
            return (T)newObject;
        }
        #endregion

        #region 判断类型是否为可空类型 + IsNullable(this Type type)
        /// <summary>
        /// 判断类型是否为可空类型
        /// </summary>
        /// <param name="type">要判断的类型</param>
        /// <returns></returns>
        public static bool IsNullable(this Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>); 
        #endregion

        #region 判断类型是否为集合类型 + IsCollection(this Type type)
        /// <summary>
        /// 判断类型是否为集合类型
        /// </summary>
        /// <param name="type">要判断的类型</param>
        /// <returns></returns>
        public static bool IsCollection(this Type type) => type.IsArray || type.GetInterfaces().Any(x => x == typeof(ICollection) || x == typeof(IEnumerable)); 
        #endregion
    }
}
#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：MapperConfig
* 类 描 述 ：表达式树实现高性能对象映射，映射设置
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/5/16 22:41:09
* 更新时间 ：2020/5/16 22:41:09
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
#if !NET40
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Kane.Extension
{
    /// <summary>
    /// 表达式树实现高性能对象映射，映射设置
    /// <para>参考来自https://www.cnblogs.com/castyuan/p/9324088.html</para>
    /// </summary>
    /// <typeparam name="Source"></typeparam>
    /// <typeparam name="Target"></typeparam>
    public class MapperConfig<Source, Target> where Source : class where Target : class, new()
    {
        /// <summary>
        /// 指定列转换
        /// </summary>
        public Action<Source, Target> BindAction { get; set; }
        /// <summary>
        /// 忽略需要转换的列
        /// </summary>
        public List<string> IgnoreColoums { get; private set; }
        /// <summary>
        /// 忽略大小写
        /// </summary>
        public bool IgnoreCase { get; set; }
        /// <summary>
        /// 设置绑定的关系
        /// </summary>
        /// <param name="action"></param>
        public void Bind(Action<Source, Target> action) => BindAction = action;

        #region 设置忽略的映射 + Ignore(Expression<Func<Target, object>> expression)
        /// <summary>
        /// 设置忽略的映射
        /// </summary>
        /// <param name="expression"></param>
        public void Ignore(Expression<Func<Target, object>> expression)
        {
            if (!(expression is LambdaExpression lambda)) throw new ArgumentNullException("无效的Lamda表达式");
            MemberExpression memberExpr = null;
            if (lambda.Body.NodeType == ExpressionType.Convert) memberExpr = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess) memberExpr = lambda.Body as MemberExpression;
            if (memberExpr == null) throw new ArgumentException("无效的成员");
            if (IgnoreColoums == null) IgnoreColoums = new List<string>();
            IgnoreColoums.Add(memberExpr.Member.Name);
        }
        #endregion
    }
}
#endif
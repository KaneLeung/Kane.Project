#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：AttributeEx
* 类 描 述 ：特性扩展类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/5/5 11:24:06
* 更新时间 ：2020/5/5 11:24:06
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
#if !NET40
using System;
using System.Reflection;

namespace Kane.Extension
{
    /// <summary>
    /// 特性扩展类
    /// </summary>
    public static class AttributeEx
    {
        #region 是否有指定特性 + HasAttribute<T>(this Type type, bool inherit = false)
        /// <summary>
        /// 是否有指定特性
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="type">类型</param>
        /// <param name="inherit">是否允许继承链搜索</param>
        public static bool HasAttribute<T>(this Type type, bool inherit = false) where T : Attribute => type.GetTypeInfo().IsDefined(typeof(T), inherit);
        #endregion

        #region 是否有指定特性 + HasAttribute<T>(this Type type, bool inherit = false)
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAttribute">特性类型</typeparam>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="_">【弃元】对象</param>
        /// <param name="inherit">是否允许继承链搜索</param>
        /// <returns></returns>
        public static bool HasAttribute<TAttribute, T>(this T _, bool inherit = false) where TAttribute : Attribute where T : class
            => typeof(T).GetTypeInfo().IsDefined(typeof(TAttribute), inherit);
        #endregion
    }
}
#endif
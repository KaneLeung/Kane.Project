#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.AspNetCore
* 项目描述 ：AspNetCore通用扩展工具
* 类 名 称 ：DIEx
* 类 描 述 ：AspNetCore原生依赖注入扩展类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.AspNetCore
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/06/04 16:38:55
* 更新时间 ：2020/06/04 16:38:55
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kane.AspNetCore
{
    /// <summary>
    /// AspNetCore原生依赖注入扩展类
    /// </summary>
    public static class DIEx
    {
        #region 基于【HttpContext】获取服务，如获取失败返回Null + GetService<T>(this HttpContext context)
        /// <summary>
        /// 基于<see cref="HttpContext"/>获取服务，如获取失败返回Null
        /// </summary>
        /// <typeparam name="T">要获取的服务类型</typeparam>
        /// <param name="context">当前上下文</param>
        /// <returns></returns>
        public static T GetService<T>(this HttpContext context) => context.RequestServices.GetService<T>();
        #endregion

        #region 基于【HttpContext】获取服务，如获取失败抛异常 + GetRequiredService<T>(this HttpContext context)
        /// <summary>
        /// 基于<see cref="HttpContext"/>获取服务，如获取失败抛异常
        /// </summary>
        /// <typeparam name="T">要获取的服务类型</typeparam>
        /// <param name="context">当前上下文</param>
        /// <returns></returns>
        public static T GetRequiredService<T>(this HttpContext context) => context.RequestServices.GetRequiredService<T>();
        #endregion

        #region 基于【Controller】获取服务，如获取失败返回Null + GetService<T>(this Controller controller)
        /// <summary>
        /// 基于<see cref="Controller"/>获取服务，如获取失败返回Null
        /// </summary>
        /// <typeparam name="T">要获取的服务类型</typeparam>
        /// <param name="controller">当前的Controller，注意在【Controller】构造函数时，此值为Null</param>
        /// <returns></returns>
        public static T GetService<T>(this Controller controller) => controller.HttpContext.RequestServices.GetService<T>();
        #endregion

        #region 基于【Controller】获取服务，如获取失败抛异常 + GetRequiredService<T>(this Controller controller)
        /// <summary>
        /// 基于<see cref="Controller"/>获取服务，如获取失败抛异常
        /// </summary>
        /// <typeparam name="T">要获取的服务类型</typeparam>
        /// <param name="controller">当前的Controller，注意在【Controller】构造函数时，此值为Null</param>
        /// <returns></returns>
        public static T GetRequiredService<T>(this Controller controller) => controller.HttpContext.RequestServices.GetRequiredService<T>();
        #endregion

        #region 基于【HttpContext】获取配置文件的指定值 + GetConfigValue<T>(this HttpContext context, string key)
        /// <summary>
        /// 基于<see cref="HttpContext"/>获取配置文件的指定值，配置文件默认是【appsettings.json】
        /// </summary>
        /// <typeparam name="T">返回值的类型</typeparam>
        /// <param name="context">当前上下文</param>
        /// <param name="key">关键词</param>
        /// <returns></returns>
        public static T GetConfigValue<T>(this HttpContext context, string key) => context.GetService<IConfiguration>().GetValue<T>(key);
        #endregion

        #region 基于【HttpContext】获取配置文件的指定值，可设置失败返回值 + GetConfigValue<T>(this HttpContext context, string key, T defaultValue)
        /// <summary>
        /// 基于<see cref="HttpContext"/>获取配置文件的指定值，可设置失败返回值，配置文件默认是【appsettings.json】
        /// </summary>
        /// <typeparam name="T">返回值的类型</typeparam>
        /// <param name="context">当前上下文</param>
        /// <param name="key">关键词</param>
        /// <param name="defaultValue">失败返回值</param>
        /// <returns></returns>
        public static T GetConfigValue<T>(this HttpContext context, string key, T defaultValue)
            => context.GetService<IConfiguration>().GetValue<T>(key, defaultValue) ?? defaultValue;
        #endregion

        #region 基于【Controller】获取配置文件的指定值 + GetConfigValue<T>(this Controller controller, string key)
        /// <summary>
        /// 基于<see cref="Controller"/>获取配置文件的指定值，配置文件默认是【appsettings.json】
        /// </summary>
        /// <typeparam name="T">返回值的类型</typeparam>
        /// <param name="controller">当前的Controller，注意在【Controller】构造函数时，此值为Null</param>
        /// <param name="key">关键词</param>
        /// <returns></returns>
        public static T GetConfigValue<T>(this Controller controller, string key) => controller.GetService<IConfiguration>().GetValue<T>(key);
        #endregion

        #region 基于【Controller】获取配置文件的指定值，可设置失败返回值 + GetConfigValue<T>(this Controller controller, string key, T defaultValue)
        /// <summary>
        /// 基于<see cref="Controller"/>获取配置文件的指定值，可设置失败返回值，配置文件默认是【appsettings.json】
        /// </summary>
        /// <typeparam name="T">返回值的类型</typeparam>
        /// <param name="controller">当前的Controller，注意在【Controller】构造函数时，此值为Null</param>
        /// <param name="key">关键词</param>
        /// <param name="defaultValue">失败返回值</param>
        /// <returns></returns>
        public static T GetConfigValue<T>(this Controller controller, string key, T defaultValue)
            => controller.GetService<IConfiguration>().GetValue<T>(key, defaultValue) ?? defaultValue;
        #endregion

        #region 基于【IServiceProvider】获取配置文件的指定值 + GetConfigValue<T>(this IServiceProvider provider, string key)
        /// <summary>
        /// 基于<see cref="IServiceProvider"/>获取配置文件的指定值，配置文件默认是【appsettings.json】
        /// </summary>
        /// <typeparam name="T">返回值的类型</typeparam>
        /// <param name="provider">已注入的IServiceProvider</param>
        /// <param name="key">关键词</param>
        /// <returns></returns>
        public static T GetConfigValue<T>(this IServiceProvider provider, string key) => provider.GetService<IConfiguration>().GetValue<T>(key);
        #endregion

        #region 基于【IServiceProvider】获取配置文件的指定值，可设置失败返回值 + GetConfigValue<T>(this IServiceProvider provider, string key, T defaultValue)
        /// <summary>
        /// 基于<see cref="IServiceProvider"/>获取配置文件的指定值，可设置失败返回值，配置文件默认是【appsettings.json】
        /// </summary>
        /// <typeparam name="T">返回值的类型</typeparam>
        /// <param name="provider">已注入的IServiceProvider</param>
        /// <param name="key">关键词</param>
        /// <param name="defaultValue">失败返回值</param>
        /// <returns></returns>
        public static T GetConfigValue<T>(this IServiceProvider provider, string key, T defaultValue)
            => provider.GetService<IConfiguration>().GetValue<T>(key, defaultValue) ?? defaultValue;
        #endregion
    }
}
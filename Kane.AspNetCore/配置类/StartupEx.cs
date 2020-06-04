using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;

using Microsoft.Extensions.DependencyInjection;

namespace Kane.AspNetCore
{
    /// <summary>
    /// AspNetCore启动项扩展类
    /// </summary>
    public static class StartupEx
    {
        #region 解决MVC视图中的中文被Html编码的问题 + IServiceCollection AddHtmlEncoder(this IServiceCollection services)
        /// <summary>
        /// 解决MVC视图中的中文被Html编码的问题
        /// </summary>
        /// <param name="services">当前服务容器</param>
        /// <returns></returns>
        public static IServiceCollection AddHtmlEncoder(this IServiceCollection services)
            => services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All)); 
        #endregion
    }
}
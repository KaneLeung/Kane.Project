#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：WebHelper
* 类 描 述 ：常用的网络帮助类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/1/15 16:38:55
* 更新时间 ：2020/3/15 16:38:55
* 版 本 号 ：v1.0.1.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
#if !NET40
using System.Net.Http.Headers;
#endif

namespace Kane.Extension
{
    /// <summary>
    /// 常用的网络帮助类
    /// 【HttpWebRequest】 已经不推荐直接使用了，这已经作为底层机制，不适合业务代码使用
    /// -->这是.NET创建者最初开发用于使用HTTP请求的标准类。使用HttpWebRequest可以让开发者控制请求/响应流程的各个方面，如 timeouts, cookies, headers, protocols。
    /// 另一个好处是HttpWebRequest类不会阻塞UI线程。例如，当您从响应很慢的API服务器下载大文件时，您的应用程序的UI不会停止响应。
    /// 
    /// 【WebClient】 不想为http细节处理而头疼的coder而生，由于内部已经处理了通用设置，某些情况可能导致性能不是很理想
    /// -->WebClient是一种更高级别的抽象，是HttpWebRequest为了简化最常见任务而创建的，使用过程中你会发现他缺少基本的header，timeoust的设置，不过这些可以通过继承httpwebrequest来实现。
    /// 使用WebClient可能比HttpWebRequest直接使用更慢（大约几毫秒）。但这种“低效率”带来了巨大的好处：它需要更少的代码和隐藏了细节处理，更容易使用，并且在使用它时你不太可能犯错误。
    /// 同样的请求示例现在很简单只需要两行而且内部周到的处理完了细节
    /// 
    /// 【HttpClient】 更加适用于异步编程模型中
    /// -->HttpClient提供强大的功能，提供了异步支持，可以轻松配合async await 实现异步请求，具体使用可参考：https://www.cnblogs.com/xiaoliangge/p/9476568.html
    /// </summary>
    public static class WebHelper
    {
        #region 根据字符串获取网络图片，默认超时时间为100秒 + GetUriImage(string uri, int timeout = 100000)
        /// <summary>
        /// 根据字符串获取网络图片，默认超时时间为100秒
        /// HttpWebRequest.Timeout,默认值为100000毫秒（100秒）
        /// </summary>
        /// <param name="uri">字符串Uri地址</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public static Image GetUriImage(string uri, int timeout = 100000)
        {
            try
            {
                var temp = WebRequest.Create(uri);
                temp.Timeout = timeout;
                return Image.FromStream(temp.GetResponse().GetResponseStream());
            }
            catch { return null; }
        }
        #endregion

        #region 根据Uri获取网络图片，默认超时时间为100秒 + GetUriImage(Uri uri, int timeout = 100000)
        /// <summary>
        /// 根据Uri获取网络图片，默认超时时间为100秒
        /// HttpWebRequest.Timeout,默认值为100000毫秒（100秒）
        /// </summary>
        /// <param name="uri">Uri地址</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public static Image GetUriImage(Uri uri, int timeout = 100000)
        {
            try
            {
                var temp = WebRequest.Create(uri);
                temp.Timeout = timeout;
                return Image.FromStream(temp.GetResponse().GetResponseStream());
            }
            catch { return null; }
        }
        #endregion

        #region 移除HTML标签 + ReplaceHtml(this string content)
        /// <summary>
        /// 移除HTML标签
        /// </summary>
        /// <param name="content">待移除Html的内容</param>
        /// <returns></returns>
        public static string ReplaceHtml(this string content)
        {
            var result = Regex.Replace(content, "<[^>]+>", "");
            return Regex.Replace(result, "&[^;]+;", "");
        }
        #endregion

        #region 将[string,string]集合转成查询字符串(QueryString) + ToQueryString(this ICollection<KeyValuePair<string, string>> parms)
        /// <summary>
        /// 将[string,string]集合转成查询字符串(QueryString)
        /// </summary>
        /// <param name="parms">字典值</param>
        /// <returns></returns>
        public static string ToQueryString(this ICollection<KeyValuePair<string, string>> parms)
        {
            var sb = new StringBuilder();
            foreach (var keyValuePair in parms)
            {
                sb.Append(keyValuePair.Key);
                sb.Append('=');
                sb.Append(Uri.EscapeDataString(keyValuePair.Value));
                sb.Append('&');
            }
            return sb.ToString().TrimEnd('&');
        }
        #endregion

        #region 将Uri里的QueryString转成集合，并按照字典序排序 + GetQuerys(this Uri uri)
        /// <summary>
        /// 将Uri里的QueryString转成集合，并按照字典序排序
        /// </summary>
        /// <param name="uri">要转的Uri</param>
        /// <param name="toLower">是否转成小写</param>
        /// <returns></returns>
        public static IOrderedEnumerable<KeyValuePair<string, string>> GetQuerys(this Uri uri, bool toLower = true)
        {
            var collection = HttpUtility.ParseQueryString(uri.Query);
            return collection.Cast<string>().Select(x => new KeyValuePair<string, string>(true ? x.ToLower() : x, toLower ? collection[x].ToLower() : collection[x])).OrderBy(x => x.Key);
        }
        #endregion

#if !NET40
        /// <summary>
        /// 
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static IOrderedEnumerable<KeyValuePair<string, string>> GetHeaders(this HttpRequestHeaders headers, string separator = ";")
            => headers.Select(x => new KeyValuePair<string, string>(x.Key.ToLower(), Uri.EscapeDataString(string.Join(separator, x.Value)))).OrderBy(x => x.Key);
#endif

        #region 对URL字符串进行编码，默认使用【UTF8】编码 + UrlEncode(this string value)
        /// <summary>
        /// 对URL字符串进行编码，默认使用【UTF8】编码
        /// </summary>
        /// <param name="value">要编码的值</param>
        /// <returns></returns>
        public static string UrlEncode(this string value) => HttpUtility.UrlEncode(value, Encoding.UTF8);
        #endregion

        #region 对URL字符串进行编码，可设置编码 + UrlEncode(this string value, Encoding encoding)
        /// <summary>
        /// 对URL字符串进行编码，可设置编码
        /// </summary>
        /// <param name="value">要编码的值</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string UrlEncode(this string value, Encoding encoding) => HttpUtility.UrlEncode(value, encoding);
        #endregion

        #region URL编码的字符串转换为解码的字符串，默认使用【UTF8】编码 + UrlDecode(this string value)
        /// <summary>
        /// URL编码的字符串转换为解码的字符串，默认使用【UTF8】编码
        /// </summary>
        /// <param name="value">要解码的URL编码</param>
        /// <returns></returns>
        public static string UrlDecode(this string value) => HttpUtility.UrlDecode(value, Encoding.UTF8);
        #endregion

        #region URL编码的字符串转换为解码的字符串，可设置编码 + UrlDecode(this string value, Encoding encoding)
        /// <summary>
        /// URL编码的字符串转换为解码的字符串，可设置编码
        /// </summary>
        /// <param name="value">要解码的URL编码</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string UrlDecode(this string value, Encoding encoding) => HttpUtility.UrlDecode(value, encoding);
        #endregion

        #region Uri累加路径 + Append(this Uri uri, params string[] paths)
        /// <summary>
        /// Uri累加路径
        /// </summary>
        /// <param name="uri">原来的Uri</param>
        /// <param name="paths">要加入的路径</param>
        /// <returns></returns>
        public static Uri Append(this Uri uri, params string[] paths)
            => new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => $"{current.TrimEnd('/')}/{path.TrimStart('/')}")); 
        #endregion
    }
}
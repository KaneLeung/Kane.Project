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
* 更新时间 ：2020/6/10 09:38:55
* 版 本 号 ：v1.0.4.0
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

        #region 将Uri里的QueryString转成集合，并按照字典序排序 + GetQuerys(this Uri uri, bool toLower = true)
        /// <summary>
        /// 将Uri里的QueryString转成集合，并按照字典序排序
        /// </summary>
        /// <param name="uri">要转的Uri</param>
        /// <param name="toLower">是否转成小写</param>
        /// <returns></returns>
        public static IOrderedEnumerable<KeyValuePair<string, string>> GetQuerys(this Uri uri, bool toLower = true)
        {
            var collection = HttpUtility.ParseQueryString(uri.Query);
            return collection.Cast<string>().Where(k => k.IsValuable()).Select(k => new KeyValuePair<string, string>(true ? k.ToLower() : k, toLower ? collection[k].ToLower() : collection[k])).OrderBy(k => k.Key);
        }
        #endregion

#if !NET40
        #region 将HttpRequestHeaders里的Header转成集合，并按照字典序排序 + GetHeaders(this HttpRequestHeaders headers, string separator = ";")
        /// <summary>
        /// 将HttpRequestHeaders里的Header转成集合，并按照字典序排序
        /// </summary>
        /// <param name="headers">要转的<see cref="HttpRequestHeaders"/></param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static IOrderedEnumerable<KeyValuePair<string, string>> GetHeaders(this HttpRequestHeaders headers, string separator = ";")
            => headers.Select(k => new KeyValuePair<string, string>(k.Key.ToLower(), Uri.EscapeDataString(string.Join(separator, k.Value)))).OrderBy(k => k.Key);
        #endregion
#endif

        #region 对URL字符串进行编码，默认使用【UTF8】编码 + UrlEncode(this string value)
        /// <summary>
        /// 对URL字符串进行编码，默认使用【UTF8】编码
        /// <para>这个使用时，会对空格转成+号，注意！！！并对编码后的结果小写，并对一些特殊符号不进行编码</para>
        /// <para>必须搭配<see cref="UrlEncode(string)"/>进行解码</para>
        /// </summary>
        /// <param name="value">要编码的值</param>
        /// <returns></returns>
        public static string UrlEncode(this string value) => HttpUtility.UrlEncode(value, Encoding.UTF8);
        #endregion

        #region 对URL字符串进行编码，可设置编码 + UrlEncode(this string value, Encoding encoding)
        /// <summary>
        /// 对URL字符串进行编码，可设置编码
        /// <para>这个使用时，会对空格转成+号，注意！！！并对编码后的结果小写，并对一些特殊符号不进行编码</para>
        /// <para>必须搭配<see cref="UrlEncode(string, Encoding)"/>进行解码</para>
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

        #region 将字符串转换为它的转义表示形式，可以转一些特殊符号 + ToEscape(this string value)
        /// <summary>
        /// 将字符串转换为它的转义表示形式，可以转一些特殊符号
        /// <para>并对转义后的结果大写，可使用<see cref="ToUnescape(string)"/>进行还原</para>
        /// </summary>
        /// <param name="value">要转义的字符串</param>
        /// <returns></returns>
        public static string ToEscape(this string value) => Uri.EscapeDataString(value);
        #endregion

        #region 将字符串转换为它的非转义表示形式 + ToUnescape(this string value)
        /// <summary>
        /// 将字符串转换为它的非转义表示形式
        /// </summary>
        /// <param name="value">要还原的字符串</param>
        /// <returns></returns>
        public static string ToUnescape(this string value) => Uri.UnescapeDataString(value);
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

        #region 根据UserAgent判断用户的操作系统 + GetOSName(string userAgent)
        /// <summary>
        /// 根据UserAgent判断用户的操作系统
        /// </summary>
        /// <param name="userAgent">UserAgent字符串</param>
        /// <returns></returns>
        public static string GetOSName(string userAgent)
        {
            string temp = "未知";
            if (userAgent.Contains("NT 10.0")) temp = "Windows 10";
            else if (userAgent.Contains("NT 6.3")) temp = "Windows 8.1";
            else if (userAgent.Contains("NT 6.2")) temp = "Windows 8";
            else if (userAgent.Contains("NT 6.1")) temp = "Windows 7";
            else if (userAgent.Contains("NT 6.0")) temp = "Windows Vista/Server 2008";
            else if (userAgent.Contains("NT 5.2")) temp = userAgent.Contains("64") ? "Windows XP" : "Windows Server 2003";
            else if (userAgent.Contains("NT 5.1")) temp = "Windows XP";
            else if (userAgent.Contains("NT 5")) temp = "Windows 2000";
            //else if (userAgent.Contains("NT 4")) temp = "Windows NT4";
            //else if (userAgent.Contains("Me")) temp = "Windows Me";
            //else if (userAgent.Contains("98")) temp = "Windows 98";
            //else if (userAgent.Contains("95")) temp = "Windows 95";
            else if (userAgent.Contains("Android")) temp = "Android";
            else if (userAgent.Contains("iPhone") || userAgent.Contains("iPad")) temp = "IOS";
            else if (userAgent.Contains("Mac")) temp = "Mac";
            else if (userAgent.Contains("Unix")) temp = "UNIX";
            else if (userAgent.Contains("Linux")) temp = "Linux";
            else if (userAgent.Contains("SunOS")) temp = "SunOS";
            return temp;
        }
        #endregion
    }
}
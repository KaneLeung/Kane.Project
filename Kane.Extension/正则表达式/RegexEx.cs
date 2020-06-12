#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：RegexEx
* 类 描 述 ：正则表达式扩展类
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/16 23:24:14
* 更新时间 ：2020/06/10 13:24:14
* 版 本 号 ：v1.0.4.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Kane.Extension
{
    /// <summary>
    /// 正则表达式扩展类
    /// </summary>
    public static class RegexEx
    {
        #region 利用正则表达式替换字符串里的文字，区分大小写 + RegexReplace(this string value, string regex, string newValue)
        /// <summary>
        /// 利用正则表达式替换字符串里的文字，区分大小写
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="newValue">替换后的字符串</param>
        /// <returns></returns>
        public static string RegexReplace(this string value, string regex, string newValue) => Regex.Replace(value, regex, newValue);
        #endregion

        #region 利用正则表达式替换多个关键词的文字，区分大小写 + RegexReplace(this string value, string newValue, params string[] keys)
        /// <summary>
        /// 利用正则表达式替换多个关键词的文字，区分大小写
        /// <para>经测试，短的内容效率比<see cref="StringEx.Replace(string, string, string[])"/>略低</para>
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="newValue">替换后的字符串</param>
        /// <param name="keys">要替换的关键词</param>
        /// <returns></returns>
        public static string RegexReplace(this string value, string newValue, params string[] keys)
        {
            if (value.IsNullOrEmpty() || keys.Length < 1) return value;
            return Regex.Replace(value, $"({string.Join("|", keys)})", newValue);
        }
        #endregion

        #region 利用正则表达式查找某字符串出现的次数 + RegexCount(this string value, string pattern)
        /// <summary>
        /// 利用正则表达式查找某字符串出现的次数,如果单个字符，不建议使用这个
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="pattern">要统计的字符串</param>
        /// <returns></returns>
        public static int RegexCount(this string value, string pattern) => Regex.Matches(value, pattern, RegexOptions.Compiled).Count;
        #endregion

        #region 检测字符串中是否找到了匹配项 + IsMatch(this string value, string pattern)
        /// <summary>
        /// 检测字符串中是否找到了匹配项
        /// </summary>
        /// <param name="value">要检测的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>如果正则表达式找到匹配项，则为【True】；否则，为【False】</returns>
        public static bool IsMatch(this string value, string pattern)
        {
            if (value.IsNullOrEmpty()) return false;
            return Regex.IsMatch(value, pattern);
        }
        #endregion

        #region 检测字符串中是否找到了匹配项，要设置匹配选项 + IsMatch(this string value, string pattern, RegexOptions options)
        /// <summary>
        /// 检测字符串中是否找到了匹配项，要设置匹配选项
        /// </summary>
        /// <param name="value">要检测的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="options">枚举值提供匹配选项</param>
        /// <returns>如果正则表达式找到匹配项，则为【True】；否则，为【False】</returns>
        public static bool IsMatch(this string value, string pattern, RegexOptions options)
        {
            if (value.IsNullOrEmpty()) return false;
            return Regex.IsMatch(value, pattern, options);
        }
        #endregion

        #region 利用正则表达式，返回第一个匹配项 + Match(this string value, string pattern)
        /// <summary>
        /// 利用正则表达式，返回第一个匹配项
        /// </summary>
        /// <param name="value">要搜索匹配项的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns></returns>
        public static string Match(this string value, string pattern)
        {
            if (value.IsNullOrEmpty()) return string.Empty;
            return Regex.Match(value, pattern).Value;
        }
        #endregion

        #region 利用正则表达式，返回匹配的所有项 + Matches(this string value, string pattern)
        /// <summary>
        /// 利用正则表达式，返回匹配的所有项
        /// </summary>
        /// <param name="value">要搜索匹配项的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns></returns>
        public static IEnumerable<string> Matches(this string value, string pattern)
        {
            if (value.IsNullOrEmpty()) return new string[] { };
            MatchCollection matches = Regex.Matches(value, pattern);
            return from Match match in matches select match.Value;
        }
        #endregion
    }
}
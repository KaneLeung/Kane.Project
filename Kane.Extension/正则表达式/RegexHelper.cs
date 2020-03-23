#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：RegexHelper
* 类 描 述 ：正则表达式扩展
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/16 23:24:14
* 更新时间 ：2020/03/23 13:24:14
* 版 本 号 ：v1.0.1.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System.Text.RegularExpressions;

namespace Kane.Extension
{
    /// <summary>
    /// 正则表达式扩展
    /// </summary>
    public static class RegexHelper
    {
        #region 利用正则表达式替换字符串里的文字 + RegexReplce(this string value, string regex, string word)
        /// <summary>
        /// 利用正则表达式替换字符串里的文字
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="word">要替换的字符串</param>
        /// <returns></returns>
        public static string RegexReplce(this string value, string regex, string word) => Regex.Replace(value, regex, word);
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
    }
}

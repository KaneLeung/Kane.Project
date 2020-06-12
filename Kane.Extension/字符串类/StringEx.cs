#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：StringEx
* 类 描 述 ：字符串类扩展类
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/16 23:26:06
* 更新时间 ：2020/06/11 09:26:06
* 版 本 号 ：v1.0.7.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Kane.Extension
{
    /// <summary>
    /// 字符串类扩展类
    /// </summary>
    public static class StringEx
    {
        #region 字符串扩展方法，判断字符串是否为NullOrEmpty + IsNullOrEmpty(this string value)
        /// <summary>
        /// 字符串扩展方法，判断字符串是否为NullOrEmpty
        /// <para>判断是否为【Null】【""】【String.Empty】</para>
        /// </summary>
        /// <param name="value">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);
        #endregion

        #region 字符串扩展方法，判断字符串是否为NullOrWhiteSpace + IsNullOrWhiteSpace(this string value)
        /// <summary>
        /// 字符串扩展方法，判断字符串是否为NullOrWhiteSpace
        /// <para>null,String.Empty,new String(' ', 20),"  \t   ",new String('\u2000', 10)都会返回True</para>
        /// </summary>
        /// <param name="value">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);
        #endregion

        #region 字符串扩展方法，判断字符串是否不为NullOrEmpty + IsValuable(this string value)
        /// <summary>
        /// 字符串扩展方法，判断字符串是否不为NullOrEmpty
        /// </summary>
        /// <param name="value">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsValuable(this string value) => !string.IsNullOrEmpty(value);
        #endregion

        #region 泛型扩展方法，判断对象是否为Null + IsNull<T>(this T value)
        /// <summary>
        /// 泛型扩展方法，判断对象是否为Null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要判断的对象</param>
        /// <returns></returns>
        public static bool IsNull<T>(this T value) => value == null;
        #endregion

        #region 泛型扩展方法，判断对象是否不为Null + IsNotNull<T>(this T value)
        /// <summary>
        /// 泛型扩展方法，判断对象是否不为Null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要判断的对象</param>
        /// <returns></returns>
        public static bool IsNotNull<T>(this T value) => value != null;
        #endregion

        #region 字节数组转十六进制字符串【全小写】 + BytesToHex(this byte[] value)
        /// <summary>
        /// 字节数组转十六进制字符串【全小写】，如果要大写，请使用<see cref="BytesToHEX"/>
        /// </summary>
        /// <param name="value">要转的字节数组</param>
        /// <returns>全小写</returns>
        public static string BytesToHex(this byte[] value) => string.Concat(value.Select(k => k.ToString("x2")));
        #endregion

        #region 字节数组转十六进制字符串【全大写】 + BytesToHEX(this byte[] value)
        /// <summary>
        /// 字节数组转十六进制字符串【全大写】，如果要小写，请使用<see cref="BytesToHex"/>
        /// </summary>
        /// <param name="value">要转的字节数组</param>
        /// <returns>全大写</returns>
        public static string BytesToHEX(this byte[] value) => string.Concat(value.Select(k => k.ToString("X2")));
        #endregion

        #region 十六进制字符串转字节数组 + HexToBytes(this string value)
        /// <summary>
        /// 十六进制字符串转字节数组
        /// </summary>
        /// <param name="value">要转的十六进制字符串</param>
        /// <returns></returns>
        public static byte[] HexToBytes(this string value)
        {
            value = value.Replace(" ", "");
            byte[] buffer = new byte[value.Length / 2];
            for (int i = 0; i < value.Length; i += 2)
                buffer[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
            return buffer;
        }
        #endregion

        #region 可根据字符串来Split字符串，忽略大小写 + Split(this string value, string key)
        /// <summary>
        /// 可根据字符串来Split字符串，忽略大小写
        /// </summary>
        /// <param name="value">原来的字符串</param>
        /// <param name="key">拆分关键词</param>
        /// <returns></returns>
        public static string[] Split(this string value, string key) => Regex.Split(value, key, RegexOptions.IgnoreCase);
        #endregion

        #region 统计某字符在字符串中出现的次数 + CharCount(this string value, char key)
        /// <summary>
        /// 统计某字符在字符串中出现的次数
        /// </summary>
        /// <param name="value">要查找的字符串</param>
        /// <param name="key">要统计的字符</param>
        /// <returns></returns>
        public static int CharCount(this string value, char key)
        {
            int count = 0;
            for (int i = 0; i < value.Length; i++)
                if (value[i] == key) count++;
            return count;
        }
        #endregion

        #region 字符串转成字节数组，默认使用UTF8编码 + ToBytes(this string value)
        /// <summary>
        /// 字符串转成字节数组，默认使用UTF8编码
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <returns></returns>
        public static byte[] ToBytes(this string value) => value.ToBytes(Encoding.UTF8);
        #endregion

        #region 字符串转成字节数组，编码方式可选 + ToBytes(this string value, Encoding encoding)
        /// <summary>
        /// 字符串转成字节数组，编码方式可选
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static byte[] ToBytes(this string value, Encoding encoding) => encoding.GetBytes(value);
        #endregion

        #region 字节数组转成字符串，默认使用UTF8编码 + BytesToString(this byte[] value
        /// <summary>
        /// 字节数组转成字符串，默认使用UTF8编码
        /// </summary>
        /// <param name="value">要转的字节数组</param>
        /// <returns></returns>
        public static string BytesToString(this byte[] value) => Encoding.UTF8.GetString(value);
        #endregion

        #region 字节数组转成字符串，编码方式可选 + BytesToString(this byte[] value, Encoding encoding)
        /// <summary>
        /// 字节数组转成字符串，编码方式可选
        /// </summary>
        /// <param name="value">要转的字节数组</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string BytesToString(this byte[] value, Encoding encoding) => encoding.GetString(value);
        #endregion

        #region 字符串转成Base64字符串 + ToBase64(this string value)
        /// <summary>
        /// 字符串转成Base64字符串
        /// </summary>
        /// <param name="value">要转换的字符串</param>
        /// <returns></returns>
        public static string ToBase64(this string value) => Convert.ToBase64String(value.ToBytes());
        #endregion

        #region 字符串转成Base64字符串,可自定义编码 + ToBase64(this string value)
        /// <summary>
        /// 字符串转成Base64字符串，可自定义编码
        /// </summary>
        /// <param name="value">要转换的字符串</param>
        /// <param name="encoding">自定义编码</param>
        /// <returns></returns>
        public static string ToBase64(this string value, Encoding encoding) => Convert.ToBase64String(value.ToBytes(encoding));
        #endregion

        #region Base64字符串转成字符串，默认使用UTF8编码 + FormBase64(this string value)
        /// <summary>
        /// Base64字符串转成字符串，默认使用UTF8编码
        /// </summary>
        /// <param name="value">要转换的Base64字符串</param>
        /// <returns></returns>
        public static string FormBase64(this string value) => FormBase64(value, Encoding.UTF8);
        #endregion

        #region Base64字符串转成字符串，可自定义编码 + FormBase64(this string value, Encoding encoding)
        /// <summary>
        /// Base64字符串转成字符串，可自定义编码
        /// </summary>
        /// <param name="value">要转换的Base64字符串</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string FormBase64(this string value, Encoding encoding) => encoding.GetString(Convert.FromBase64String(value));
        #endregion

        #region 字节数组转成Base64字符串 + ToBase64(this byte[] value)
        /// <summary>
        /// 字节数组转成Base64字符串
        /// </summary>
        /// <param name="value">要转换的字节数组</param>
        /// <returns></returns>
        public static string ToBase64(this byte[] value) => Convert.ToBase64String(value);
        #endregion

        #region Base64字符串转成字节数组 + Base64ToBytes(this string value)
        /// <summary>
        /// Base64字符串转成字节数组
        /// <para>常见错误【Base-64 字符数组或字符串的长度无效，输入的不是有效的Base-64字符串，</para>
        /// <para>因为它包含非Base-64 字符、两个以上的填充字符，或者填充字符间包含非法字符。】</para>
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <returns></returns>
        public static byte[] Base64ToBytes(this string value) => Convert.FromBase64String(value);
        #endregion

        #region 全局字符串转换Bool格式 + BoolFormats
        /// <summary>
        /// 全局字符串转换Bool格式
        /// </summary>
        public static IEnumerable<string> BoolFormats = new string[] { "true", "是", "1", "ok", "yes", "enable" };
        #endregion

        #region 将字符串转换为Bool类型，默认包含【true】【是】【1】【ok】【yes】【enable】 + ToBool(this string value)
        /// <summary>
        /// 将字符串转换为Bool类型
        /// <para>默认包含【true】【是】【1】【ok】【yes】【enable】</para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBool(this string value) => BoolFormats.Any(k => k == value.ToLower());
        #endregion

        #region 校验字符串是否为Url地址，返回校验结果和转为Uri类型的值联 + CheckUrl(this string value)
        /// <summary>
        /// 校验字符串是否为Url地址，返回校验结果和转为Uri类型的值
        /// </summary>
        /// <param name="value">要校验的字符串</param>
        /// <returns></returns>
        public static (bool, Uri) CheckUrl(this string value)
        {
            if (Uri.TryCreate(value, UriKind.Absolute, out Uri result) && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps)) return (true, result);
            else return (false, null);
        }
        #endregion

        #region 将字符串转成Uri，失败返回Null + ToUrl(this string value)
        /// <summary>
        /// 将字符串转成Uri，失败返回Null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Uri ToUrl(this string value)
        {
            if (Uri.TryCreate(value, UriKind.Absolute, out Uri result) && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps)) return result;
            else return null;
        }
        #endregion

        #region 两个字符串合并成一个 + Add(this string str0, string str1)
        /// <summary>
        /// 两个字符串合并成一个
        /// <para>代替+号，一般四个字符串以下用，比+号效率高</para>
        /// </summary>
        /// <param name="str0">第一个字符串</param>
        /// <param name="str1">第二个字符串</param>
        /// <returns></returns>
        public static string Add(this string str0, string str1) => string.Concat(str0, str1);
        #endregion

        #region 三个字符串合并成一个 + Add(this string str0, string str1, string str2)
        /// <summary>
        /// 三个字符串合并成一个
        /// <para>代替+号，一般四个字符串以下用，比+号效率高</para>
        /// </summary>
        /// <param name="str0">第一个字符串</param>
        /// <param name="str1">第二个字符串</param>
        /// <param name="str2">第三个字符串</param>
        /// <returns></returns>
        public static string Add(this string str0, string str1, string str2) => string.Concat(str0, str1, str2);
        #endregion

        #region 四个字符串合并成一个 + Add(this string str0, string str1, string str2, string str3)
        /// <summary>
        /// 四个字符串合并成一个
        /// <para>代替+号，一般四个字符串以下用，比+号效率高</para>
        /// </summary>
        /// <param name="str0">第一个字符串</param>
        /// <param name="str1">第二个字符串</param>
        /// <param name="str2">第三个字符串</param>
        /// <param name="str3">第四个字符串</param>
        /// <returns></returns>
        public static string Add(this string str0, string str1, string str2, string str3) => string.Concat(str0, str1, str2, str3);
        #endregion

        #region 判断两个字符串是否相同，忽略大小写 + EqualsIgnoreCase(this string str0, string str1, bool strict = false)
        /// <summary>
        /// 判断两个字符串是否相同，忽略大小写
        /// <para> 默认为【非严紧模式】，即【Null】和【string.Empty】或【""】比较时为True</para>
        /// <para>【严紧模式】时，【Null】与【string.Empty】或【""】比较时为 False</para>
        /// </summary>
        /// <param name="str0">要比较的字符串1</param>
        /// <param name="str1">要比较的字符串2</param>
        /// <param name="strict">是否为严紧模式</param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string str0, string str1, bool strict = false)
        {
            if (strict == false && str0.IsNullOrEmpty() && str1.IsNullOrEmpty()) return true;
            if (str0 is null || str1 is null) return str0 is null && str1 is null ? true : false;
            return str0.Equals(str1, StringComparison.OrdinalIgnoreCase);
        }
        #endregion

        #region 字符转成全角(SBC Case)的字符 + ToSBC(this char value)
        /// <summary>
        /// 字符转成全角(SBC Case)的字符
        /// <para>全角空格为12288，半角空格为32</para>
        /// <para>其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248</para>
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <returns></returns>
        public static char ToSBC(this char value)
        {
            if (value == 32) value = (char)12288;
            if (value < 127) value = (char)(value + 65248);
            return value;
        }
        #endregion

        #region 字符串转成全角(SBC Case)的字符串 + ToSBC(this string value)
        /// <summary>
        /// 字符串转成全角(SBC Case)的字符串
        /// <para>全角空格为12288，半角空格为32</para>
        /// <para>其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248</para>
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <returns></returns>
        public static string ToSBC(this string value)
        {
            char[] array = value.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 32)
                {
                    array[i] = (char)12288;
                    continue;
                }
                if (array[i] < 127) array[i] = (char)(array[i] + 65248);
            }
            return new string(array);
        }
        #endregion

        #region 字符转成半角(DBC Case)的字符 + ToDBC(this char value)
        /// <summary>
        /// 字符转成半角(DBC Case)的字符
        /// <para>全角空格为12288，半角空格为32</para>
        /// <para>其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248</para>
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <returns></returns>
        public static char ToDBC(this char value)
        {
            if (value == 12288) value = (char)32;
            if (value > 65280 && value < 65375) value = (char)(value - 65248);
            return value;
        }
        #endregion

        #region 字符串转成半角(DBC Case)的字符串 + ToDBC(this string value)
        /// <summary>
        /// 字符串转成半角(DBC Case)的字符串
        /// <para>全角空格为12288，半角空格为32</para>
        /// <para>其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248</para>
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <returns></returns>
        public static string ToDBC(this string value)
        {
            char[] array = value.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 12288)
                {
                    array[i] = (char)32;
                    continue;
                }
                if (array[i] > 65280 && array[i] < 65375) array[i] = (char)(array[i] - 65248);
            }
            return new string(array);
        }
        #endregion

        #region 检测字符串是否匹配任意字符串的开头【区分大小写】 + StartsWith(this string value, params string[] keys)
        /// <summary>
        /// 检测字符串是否匹配任意字符串的开头【区分大小写】
        /// </summary>
        /// <param name="value">要检测字符串</param>
        /// <param name="keys">多个字符串</param>
        /// <returns></returns>
        public static bool StartsWith(this string value, params string[] keys) => keys.Any(key => value.StartsWith(key));
        #endregion

        #region 检测字符串是否匹配任意字符串的开头【忽略大小写】 + StartsWithIgnoreCase(this string value, params string[] keys)
        /// <summary>
        /// 检测字符串是否匹配任意字符串的开头【忽略大小写】
        /// </summary>
        /// <param name="value">要检测字符串</param>
        /// <param name="keys">任意字符串</param>
        /// <returns></returns>
        public static bool StartsWithIgnoreCase(this string value, params string[] keys) => keys.Any(key => value.StartsWith(key, StringComparison.OrdinalIgnoreCase));
        #endregion

        #region 检测字符串是否匹配任意字符串的结尾【区分大小写】 + EndsWith(this string value, params string[] keys)
        /// <summary>
        /// 检测字符串是否匹配任意字符串的结尾【区分大小写】
        /// </summary>
        /// <param name="value">要检测字符串</param>
        /// <param name="keys">任意字符串</param>
        /// <returns></returns>
        public static bool EndsWith(this string value, params string[] keys) => keys.Any(key => value.EndsWith(key));
        #endregion

        #region 检测字符串是否匹配任意字符串的结尾【忽略大小写】 + EndsWithIgnoreCase(this string value, params string[] keys)
        /// <summary>
        /// 检测字符串是否匹配任意字符串的结尾【忽略大小写】
        /// </summary>
        /// <param name="value">要检测字符串</param>
        /// <param name="keys">任意字符串</param>
        /// <returns></returns>
        public static bool EndsWithIgnoreCase(this string value, params string[] keys) => keys.Any(key => value.EndsWith(key, StringComparison.OrdinalIgnoreCase));
        #endregion

        #region 用新的字符替换原字符串中指定位置和长度的字符 + Replace(this string value, int start, int length, char replaceChar = '*')
        /// <summary>
        /// 用新的字符替换原字符串中指定位置和长度的字符
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="start">开始的位置，从【0】开始</param>
        /// <param name="length">替换的长度</param>
        /// <param name="replaceChar">替换的字符</param>
        /// <returns></returns>
        public static string Replace(this string value, int start, int length, char replaceChar = '*')
        {
            if (value.IsNullOrEmpty() || value?.Length < start) return value;
            if (value.Length < start + length) length = value.Length - start;
            if (length < 1) return value;
            return value.Remove(start, length).Insert(start, new string(replaceChar, length));
        }
        #endregion

        #region 查找并替换字符串，可查找多个目标【区分大小写】 + Replace(this string value, string newValue, params string[] keys)
        /// <summary>
        /// 查找并替换字符串，可查找多个目标【区分大小写】
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="newValue">要替换的字符串</param>
        /// <param name="keys">查找的关键词</param>
        /// <returns></returns>
        public static string Replace(this string value, string newValue, params string[] keys)
        {
            if (value.IsNullOrEmpty() || keys.Length == 0) return value;
            foreach (var item in keys)
                value = value.Replace(item, newValue);
            return value;
        }
        #endregion

#if NETCOREAPP
        #region 查找并替换字符串，可查找多个目标【忽略大小写】 + ReplaceIgnoreCase(this string value, string newValue, params string[] keys)
        /// <summary>
        /// 查找并替换字符串，可查找多个目标【忽略大小写】
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="newValue">要替换的字符串</param>
        /// <param name="keys">查找的目标</param>
        /// <returns></returns>
        public static string ReplaceIgnoreCase(this string value, string newValue, params string[] keys)
        {
            if (value.IsNullOrEmpty() || keys.Length == 0) return value;
            foreach (var item in keys)
                value = value.Replace(item, newValue, StringComparison.OrdinalIgnoreCase);
            return value;
        }
        #endregion
#endif

        #region 判断多个字符串是否出现在原字符串中【区分大小写】 + Contains(this string value, params string[] keys)
        /// <summary>
        /// 判断多个字符串是否出现在原字符串中【区分大小写】
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="keys">要查找的多个字符串</param>
        /// <returns></returns>
        public static bool Contains(this string value, params string[] keys) => keys.Any(key => value.IndexOf(key) >= 0);
        #endregion

        #region 判断多个字符串是否出现在原字符串中【忽略大小写】 + ContainsIgnoreCase(this string value, params string[] keys)
        /// <summary>
        /// 判断多个字符串是否出现在原字符串中【忽略大小写】
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="keys">要查找的多个字符串</param>
        /// <returns></returns>
        public static bool ContainsIgnoreCase(this string value, params string[] keys) => keys.Any(key => value.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0);
        #endregion

        #region 查找并移除字符串，可查找多个目标【区分大小写】 + Remove(this string value, params string[] keys)
        /// <summary>
        /// 查找并移除字符串，可查找多个目标【区分大小写】
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="keys">查找的关键词</param>
        /// <returns></returns>
        public static string Remove(this string value, params string[] keys)
        {
            if (value.IsNullOrEmpty() || keys.Length == 0) return value;
            foreach (var item in keys)
                value = value.Replace(item, string.Empty);
            return value;
        }
        #endregion

#if NETCOREAPP
        #region 查找并移除字符串，可查找多个目标【忽略大小写】 + RemoveIgnoreCase(this string value, params string[] keys)
        /// <summary>
        /// 查找并移除字符串，可查找多个目标【忽略大小写】
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="keys">查找的目标</param>
        /// <returns></returns>
        public static string RemoveIgnoreCase(this string value, params string[] keys)
        {
            if (value.IsNullOrEmpty() || keys.Length == 0) return value;
            foreach (var item in keys)
                value = value.Replace(item, string.Empty, StringComparison.OrdinalIgnoreCase);
            return value;
        }
        #endregion
#endif

        #region 利用相似度计算公式，比较两个字符串相似度 + Similarity(this string source, string target)
        /// <summary>
        /// 利用相似度计算公式，比较两个字符串相似度
        /// <para>相似度计算公式是 相似度=Kq*q/(Kq*q+Kr*r+Ks*s)其中(Kq>0,Kr>=0,Ka>=0)</para>
        /// <para>其中，q是源字符串和目标字符串中都存在的单词的总数，s是源字符串中存在，目标字符串中不存在的单词总数，r是目标字符串中存在，源字符串中不存在的单词总数.</para>
        /// <para>Kq，Kr和Ks分别是q，r，s的权重，根据实际的计算情况，我们设Kq=2，Kr=Ks=1</para>
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="target">目标字符串</param>
        /// <returns></returns>
        public static double Similarity(this string source, string target)
        {
            if (source.IsNullOrEmpty() || target.IsNullOrEmpty()) return default;
            const double kq = 2, kr = 1, ks = 1;
            char[] sourceChars = source.ToCharArray();
            char[] targetChars = target.ToCharArray();
            int q = sourceChars.Intersect(targetChars).Count();//获取交集数量
            int s = sourceChars.Length - q, r = targetChars.Length - q;
            return kq * q / (kq * q + kr * r + ks * s);
        }
        #endregion

        #region 编辑距离算法Levenshtein Distance + LevenshteinDistance(string source, string target)
        /// <summary>
        /// 编辑距离算法Levenshtein Distance
        /// <para>编辑距离，又称Levenshtein距离（也叫做Edit Distance），是指两个字串之间，由一个转成另一个所需的最少编辑操作次数，如果它们的距离越大，说明它们越是不同。</para>
        /// <para>许可的编辑操作包括将一个字符替换成另一个字符，插入一个字符，删除一个字符。</para>
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="target">目标字符串</param>
        /// <returns></returns>
        private static int LevenshteinDistance(string source, string target)
        {
            int cell = source.Length;
            int row = target.Length;
            if (cell == 0) return row;
            if (row == 0) return cell;
            int[,] matrix = new int[row + 1, cell + 1];
            for (var i = 0; i <= cell; i++)
            {
                matrix[0, i] = i;
            }
            for (var j = 1; j <= row; j++)
            {
                matrix[j, 0] = j;
            }
            for (var i = 0; i < row; i++)
            {
                for (var j = 0; j < cell; j++)
                {
                    int tmp = source[j].Equals(target[i]) ? 0 : 1;
                    matrix[i + 1, j + 1] = Math.Min(Math.Min(matrix[i, j] + tmp, matrix[i + 1, j] + 1), matrix[i, j + 1] + 1);
                }
            }
            return matrix[row, cell];
        }
        #endregion

        #region 最长公共子序列算法【LongestCommonSubsequence】 + LongestCommonSubsequence(string source, string target)
        /// <summary>
        /// 最长公共子序列算法【LongestCommonSubsequence】
        /// <para>其定义是，一个序列S，如果分别是两个或多个已知序列的子序列，且是所有符合此条件序列中最长的，则S称为已知序列的最长公共子序列。而最长公共子串(要求连续)和最长公共子序列是不同的.</para>
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="target">目标字符串</param>
        /// <returns></returns>
        private static int LongestCommonSubsequence(string source, string target)
        {
            if (source.IsNullOrEmpty() || target.IsNullOrEmpty()) return 0;
            int maxLength = Math.Max(target.Length, source.Length);
            int[,] matrix = new int[maxLength + 1, maxLength + 1];
            for (int i = 0; i < source.Length; i++)
            {
                for (int j = 0; j < target.Length; j++)
                {
                    if (source[i].Equals(target[j])) matrix[i + 1, j + 1] = matrix[i, j] + 1;
                    else matrix[i + 1, j + 1] = 0;
                }
            }
            return (from temp in matrix.Cast<int>() select temp).Max<int>();
        }
        #endregion

        #region 利用编辑距离【Levenshtein Distance】算法，比较两个字符串相似度 + SimilarityLD(this string source, string target)
        /// <summary>
        /// 利用编辑距离【Levenshtein Distance】算法，比较两个字符串相似度
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="target">目标字符串</param>
        /// <returns></returns>
        public static double SimilarityLD(this string source, string target)
        {
            int ld = LevenshteinDistance(source, target);
            return 1 - (double)ld / Math.Max(source.Length, target.Length);
        }
        #endregion

        #region 利用编辑距离【Levenshtein Distance】算法 和 最长公共子序列算法【LongestCommonSubsequence】，比较两个字符串相似度 + SimilarityLCS(this string source, string target)
        /// <summary>
        /// 利用编辑距离【Levenshtein Distance】算法 和 最长公共子序列算法【LongestCommonSubsequence】，比较两个字符串相似度
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="target">目标字符串</param>
        /// <returns></returns>
        public static double SimilarityLCS(this string source, string target)
        {
            var ld = LevenshteinDistance(source, target);
            var lcs = LongestCommonSubsequence(source, target);
            return ((double)lcs) / (ld + lcs);
        }
        #endregion
    }
}
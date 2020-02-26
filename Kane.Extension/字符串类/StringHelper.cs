#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：StringHelper
* 类 描 述 ：字符串类扩展
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/16 23:26:06
* 更新时间 ：2020/01/16 17:26:06
* 版 本 号 ：v1.0.2.0
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
    /// 字符串类扩展
    /// </summary>
    public static class StringHelper
    {
        #region 字符串扩展方法，判断字符串是否为NullOrEmpty + IsNullOrEmpty(this string value)
        /// <summary>
        /// 字符串扩展方法，判断字符串是否为NullOrEmpty
        /// 判断是否为【Null】【""】【String.Empty】
        /// </summary>
        /// <param name="value">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);
        #endregion

        #region 字符串扩展方法，判断字符串是否为NullOrWhiteSpace + IsNullOrWhiteSpace(this string value)
        /// <summary>
        /// 字符串扩展方法，判断字符串是否为NullOrWhiteSpace
        /// null,String.Empty,new String(' ', 20),"  \t   ",new String('\u2000', 10)都会返回True
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

        #region 泛型扩展方法，判断类型是否为Null + IsNull<T>(this T value)
        /// <summary>
        /// 泛型扩展方法，判断类型是否为Null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this T value) => value == null;
        #endregion

        #region 泛型扩展方法，判断类型是否不为Null + IsNotNull<T>(this T value)
        /// <summary>
        /// 泛型扩展方法，判断类型是否不为Null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotNull<T>(this T value) => value != null;
        #endregion

        #region 字节数组转十六进制字符串 + ByteToHex(this byte[] value)
        /// <summary>
        /// 字节数组转十六进制字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ByteToHex(this byte[] value)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
                sb.Append(value[i].ToString("X2"));
            return sb.ToString();
        }
        #endregion

        #region 十六进制字符串转字节数组 + HexToByte(string value)
        /// <summary>
        /// 十六进制字符串转字节数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] HexToByte(this string value)
        {
            value = value.Replace(" ", "");
            byte[] buffer = new byte[value.Length / 2];
            for (int i = 0; i < value.Length; i += 2)
                buffer[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
            return buffer;
        }
        #endregion

        #region 可根据字符串来Split字符串 + Split(this string value, string key)
        /// <summary>
        /// 可根据字符串来Split字符串
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

        #region 字节数组转成字符串，默认使用UTF8编码 + ByteToString(this byte[] value
        /// <summary>
        /// 字节数组转成字符串，默认使用UTF8编码
        /// </summary>
        /// <param name="value">要转的字节数组</param>
        /// <returns></returns>
        public static string ByteToString(this byte[] value) => value.ByteToString(Encoding.UTF8);
        #endregion

        #region 字节数组转成字符串，编码方式可选 + ByteToString(this byte[] value, Encoding encoding)
        /// <summary>
        /// 字节数组转成字符串，编码方式可选
        /// </summary>
        /// <param name="value">要转的字节数组</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string ByteToString(this byte[] value, Encoding encoding) => encoding.GetString(value);
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
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormBase64(this string value) => FormBase64(value, Encoding.UTF8);
        #endregion

        #region Base64字符串转成字符串，可自定义编码 + FormBase64(this string value, Encoding encoding)
        /// <summary>
        /// Base64字符串转成字符串，可自定义编码
        /// </summary>
        /// <param name="value"></param>
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
        /// 常见错误【Base-64 字符数组或字符串的长度无效，输入的不是有效的Base-64字符串，因为它包含非Base-64 字符、两个以上的填充字符，或者填充字符间包含非法字符。】
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <returns></returns>
        public static byte[] Base64ToBytes(this string value) => Convert.FromBase64String(value);
        #endregion

        #region 将字符串【true】【是】【1】【ok】【yes】转换为Bool类型 + ToBool(this string value)
        /// <summary>
        /// 将字符串【true】【是】【1】【ok】【yes】转换为Bool类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBool(this string value) => new string[] { "true", "是", "1", "ok", "yes" }.Any(k => k == value.ToLower());
        #endregion

        #region 校验字符串是否为Url地址，返回校验结果和转为Uri类型的值联 + CheckUrl(this string value)
        /// <summary>
        /// 校验字符串是否为Url地址，返回校验结果和转为Uri类型的值
        /// </summary>
        /// <param name="value">要校验的字符串</param>
        /// <returns></returns>
        public static (bool, Uri) CheckUrl(this string value)
        {
            if (Uri.TryCreate(value, UriKind.Absolute, out Uri result) && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
                return (true, result);
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
        /// 代替+号，一般四个字符串以下用，比+号效率高
        /// </summary>
        /// <param name="str0">第一个字符串</param>
        /// <param name="str1">第二个字符串</param>
        /// <returns></returns>
        public static string Add(this string str0, string str1) => string.Concat(str0, str1);
        #endregion

        #region 三个字符串合并成一个 + Add(this string str0, string str1, string str2)
        /// <summary>
        /// 三个字符串合并成一个
        /// 代替+号，一般四个字符串以下用，比+号效率高
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
        /// 代替+号，一般四个字符串以下用，比+号效率高
        /// </summary>
        /// <param name="str0">第一个字符串</param>
        /// <param name="str1">第二个字符串</param>
        /// <param name="str2">第三个字符串</param>
        /// <param name="str3">第四个字符串</param>
        /// <returns></returns>
        public static string Add(this string str0, string str1, string str2, string str3) => string.Concat(str0, str1, str2, str3);
        #endregion

        #region 检测字符串是否匹配任意字符串的开头【区分大小写和区分区域性】 + StartsWith(this string value, params string[] keys)
        /// <summary>
        /// 检测字符串是否匹配任意字符串的开头【区分大小写和区分区域性】
        /// </summary>
        /// <param name="value">要检测字符串</param>
        /// <param name="keys">任意字符串</param>
        /// <returns></returns>
        public static bool StartsWith(this string value, params string[] keys) => keys.Any(key => value.StartsWith(key));
        #endregion

        #region 检测字符串是否匹配任意字符串的开头，默认为【区分大小写】 + StartsWith(this string value, params string[] keys)
        /// <summary>
        /// 检测字符串是否匹配任意字符串的开头，默认为【区分大小写】
        /// </summary>
        /// <param name="value">要检测字符串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <param name="keys">任意字符串</param>
        /// <returns></returns>
        public static bool StartsWith(this string value, bool ignoreCase = false, params string[] keys)
            => ignoreCase ? keys.Any(key => value.StartsWith(key, ignoreCase, null)) : keys.Any(key => value.StartsWith(key));
        #endregion

        #region 检测字符串是否匹配任意字符串的结尾【区分大小写和区分区域性】 + StartsWith(this string value, params string[] keys)
        /// <summary>
        /// 检测字符串是否匹配任意字符串的结尾【区分大小写和区分区域性】
        /// </summary>
        /// <param name="value">要检测字符串</param>
        /// <param name="keys">任意字符串</param>
        /// <returns></returns>
        public static bool EndsWith(this string value, params string[] keys) => keys.Any(key => value.EndsWith(key));
        #endregion

        #region 检测字符串是否匹配任意字符串的结尾，默认为【区分大小写】 + StartsWith(this string value, params string[] keys)
        /// <summary>
        /// 检测字符串是否匹配任意字符串的结尾，默认为【区分大小写】
        /// </summary>
        /// <param name="value">要检测字符串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <param name="keys">任意字符串</param>
        /// <returns></returns>
        public static bool EndsWith(this string value, bool ignoreCase = false, params string[] keys)
            => ignoreCase ? keys.Any(key => value.EndsWith(key, ignoreCase, null)) : keys.Any(key => value.EndsWith(key));
        #endregion
    }
}
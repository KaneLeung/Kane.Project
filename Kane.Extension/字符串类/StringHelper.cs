﻿#region << 版 本 注 释 >>
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
* 更新时间 ：2019/10/16 23:26:06
* 版 本 号 ：v1.0.0.0
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
    public static class StringHelper
    {
        #region 字符串扩展方法，判断字符串是否为NullOrEmpty + IsNullOrEmpty(this string value)
        /// <summary>
        /// 字符串扩展方法，判断字符串是否为NullOrEmpty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);
        #endregion

        #region 字符串扩展方法，判断字符串是否不为NullOrEmpty + IsValuable(this string value)
        /// <summary>
        /// 字符串扩展方法，判断字符串是否不为NullOrEmpty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValuable(this string value) => !value.IsNullOrEmpty();
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
            StringBuilder sbuilder = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                sbuilder.Append(value[i].ToString("X2"));
            }
            return sbuilder.ToString();
        }
        #endregion

        #region 十六进制字符串转字节数组 + HexToByte(string value)
        /// <summary>
        /// 十六进制字符串转字节数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] HexToByte(string value)
        {
            value = value.Replace(" ", "");
            byte[] buffer = new byte[value.Length / 2];
            for (int i = 0; i < value.Length; i += 2)
            {
                buffer[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
            }
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
            {
                if (value[i] == key)
                    count++;
            }
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

        #region 字节数组转成Base64字符串 + ToBase64String(this byte[] value)
        /// <summary>
        /// 字节数组转成Base64字符串
        /// </summary>
        /// <param name="value">要转换的字节数组</param>
        /// <returns></returns>
        public static string ToBase64String(this byte[] value) => Convert.ToBase64String(value);
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

        #region 将字符串【true】【是】【1】【ok】【yes】转换为Bool类型 + ToBool(this string val
        /// <summary>
        /// 将字符串【true】【是】【1】【ok】【yes】转换为Bool类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBool(this string value) => new string[] { "true", "是", "1", "ok", "yes" }.Any(k => k == value.ToLower());
        #endregion
    }
}

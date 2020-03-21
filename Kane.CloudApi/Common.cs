#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi
* 项目描述 ：常用云服务Api
* 类 名 称 ：Common
* 类 描 述 ：内部共同类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/2/23 16:20:36
* 更新时间 ：2020/3/21 16:20:36
* 版 本 号 ：v1.0.1.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using Kane.Extension;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Kane.CloudApi
{
    /// <summary>
    /// 内部共同类
    /// </summary>
    internal static class Common
    {
        #region HmacSHA256哈希化 + HmacSHA256(byte[] data, byte[] key)
        /// <summary>
        /// HmacSHA256哈希化
        /// </summary>
        /// <param name="data">要哈希化的数据字节数组</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static byte[] HmacSHA256(byte[] data, byte[] key)
        {
            using HMACSHA256 mac = new HMACSHA256(key);
            return mac.ComputeHash(data);
        }
        #endregion

        #region HmacSHA256哈希化 + HmacSHA256哈希化
        /// <summary>
        /// HmacSHA256哈希化
        /// </summary>
        /// <param name="data">要哈希化的数据字符串</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static byte[] HmacSHA256(string data, byte[] key)
        {
            using HMACSHA256 mac = new HMACSHA256(key);
            return mac.ComputeHash(data.ToBytes());
        }
        #endregion

        #region 判断是否为空，如果为空则抛【ArgumentNullException】 + ThrowIfNull<T>(this T value, string parameter)
        /// <summary>
        /// 判断是否为空，如果为空则抛【ArgumentNullException】
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要判断的参数</param>
        /// <param name="parameter">参数名称</param>
        public static void ThrowIfNull<T>(this T value, string parameter)
        {
            if (value is string && value.ToString().IsNullOrWhiteSpace()) throw new ArgumentNullException(parameter, $"参数【{parameter}】不能为空");
            else if (value.IsNull()) throw new ArgumentNullException(parameter, $"参数【{parameter}】不能为空");
        }
        #endregion

        #region 判断该路径的文件是否存在，不存在则抛【FileNotFoundException】 + ThrowIfNotExist(this string file)
        /// <summary>
        /// 判断该路径的文件是否存在，不存在则抛【FileNotFoundException】
        /// </summary>
        /// <param name="file">文件完整路径</param>
        public static void ThrowIfNotExist(this string file)
        {
            if (!File.Exists(file)) throw new FileNotFoundException($"【{file}】该路径文件不存在");
        }
        #endregion
    }
}
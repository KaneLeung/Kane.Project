#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi
* 项目描述 ：
* 类 名 称 ：Common
* 类 描 述 ：
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/2/23 16:20:36
* 更新时间 ：2020/2/23 16:20:36
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using Kane.Extension;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Kane.CloudApi
{
    internal static class Common
    {
        /// <summary>
        /// HmacSHA256哈希化
        /// </summary>
        /// <param name="data">要哈希化的数据字节数组</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] HmacSHA256(byte[] data, byte[] key)
        {
            using HMACSHA256 mac = new HMACSHA256(key);
            return mac.ComputeHash(data);
        }

        /// <summary>
        /// HmacSHA256哈希化
        /// </summary>
        /// <param name="data">要哈希化的数据字符串</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] HmacSHA256(string data, byte[] key)
        {
            using HMACSHA256 mac = new HMACSHA256(key);
            return mac.ComputeHash(data.ToBytes()) ;
        }

        public static void CheckParameter<T>(this T value,string parameter)
        {
            if(value.IsNull()) throw new ArgumentNullException(parameter, $"参数【{parameter}】不能为空");
        }
    }
}
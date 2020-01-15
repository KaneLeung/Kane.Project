#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：JsonEx
* 类 描 述 ：Json扩展类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/1/15 23:29:15
* 更新时间 ：2020/1/15 23:29:15
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;
#if (NETCOREAPP3_0 || NETCOREAPP3_1)
using System.Text.Json;
using System.Threading.Tasks;
#else
using Newtonsoft.Json;
#endif

namespace Kane.Extension.Json类
{
    /// <summary>
    /// Json扩展类
    /// </summary>
    public static class JsonEx
    {
        #region 把对象序列化，转成Json字符串 + ToJson<T>(this T value) where T : class, new()
        /// <summary>
        /// 把对象序列化，转成Json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要序列化的对象</param>
        /// <returns></returns>
        public static string ToJson<T>(this T value) where T : class, new()
        {
#if (NETCOREAPP3_0 || NETCOREAPP3_1)
            return JsonSerializer.Serialize(value);
#else
            return JsonConvert.SerializeObject(value);
#endif
        }
        #endregion

        #region Json字符串反序列化，转成对象 + ToObject<T>(this string value) where T : class, new()
        /// <summary>
        /// Json字符串反序列化，转成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">Json字符串</param>
        /// <returns></returns>
        public static T ToObject<T>(this string value) where T : class, new()
        {
#if (NETCOREAPP3_0 || NETCOREAPP3_1)
            return JsonSerializer.Deserialize<T>(value);
#else
            return JsonConvert.DeserializeObject<T>(value);
#endif
        }
        #endregion
    }
}
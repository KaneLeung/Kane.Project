#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：XmlHelper
* 类 描 述 ：Xml帮助类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/3/16 23:10:46
* 更新时间 ：2020/3/16 23:10:46
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System.IO;
using System.Xml.Serialization;

namespace Kane.Extension
{
    /// <summary>
    /// Xml帮助类
    /// </summary>
    public static class XmlHelper
    {
        #region 将Stream反序列化成对象 + ToObject<T>(this Stream steam) where T : class, new()
        /// <summary>
        /// 将Stream反序列化成对象
        /// </summary>
        /// <typeparam name="T">要反序列化成对象类型</typeparam>
        /// <param name="steam">要反序列化的Stream</param>
        /// <returns></returns>
        public static T ToObject<T>(this Stream steam) where T : class, new()
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(steam);
        }
        #endregion

        #region 将TextReader反序列化成对象 + ToObject<T>(this TextReader reader) where T : class, new()
        /// <summary>
        /// 将TextReader反序列化成对象
        /// </summary>
        /// <typeparam name="T">要反序列化成对象类型</typeparam>
        /// <param name="reader">要反序列化的TextReader</param>
        /// <returns></returns>
        public static T ToObject<T>(this TextReader reader) where T : class, new()
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(reader);
        }
        #endregion
    }
}
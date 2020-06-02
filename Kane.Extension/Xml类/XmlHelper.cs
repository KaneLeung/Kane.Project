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
* 更新时间 ：2020/3/19 13:10:46
* 版 本 号 ：v1.0.1.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Kane.Extension
{
    /// <summary>
    /// Xml帮助类
    /// </summary>
    public static class XmlHelper
    {
        #region 将Stream反序列化成对象 + ToObject<T>(this Stream stream) where T : class, new()
        /// <summary>
        /// 将Stream反序列化成对象
        /// </summary>
        /// <typeparam name="T">要反序列化成对象类型</typeparam>
        /// <param name="stream">要反序列化的Stream</param>
        /// <returns></returns>
        public static T ToObject<T>(this Stream stream) where T : class, new()
        {
            stream.Seek(0, SeekOrigin.Begin);
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stream);
        }
        #endregion

        #region 将TextReader反序列化成对象 + ToObject<T>(this TextReader reader) where T : class, new()
        /// <summary>
        /// 将TextReader反序列化成对象
        /// </summary>
        /// <typeparam name="T">要反序列化成对象类型</typeparam>
        /// <param name="reader">要反序列化的TextReader</param>
        /// <returns></returns>
        public static T ToObject<T>(this TextReader reader) where T : class, new() => (T)new XmlSerializer(typeof(T)).Deserialize(reader);
        #endregion

        #region 将对象Xml序列化 + ToXml<T>(this T value, bool removeNamespace = false, bool removeVersion = false) where T : class, new()
        /// <summary>
        /// 将对象Xml序列化
        /// </summary>
        /// <typeparam name="T">要序列化的对象类型</typeparam>
        /// <param name="value">要序列化的对象</param>
        /// <param name="removeNamespace">是否去掉命名空间</param>
        /// <param name="removeVersion">是否去掉版本信息</param>
        /// <returns></returns>
        public static string ToXml<T>(this T value, bool removeNamespace = false, bool removeVersion = false) where T : class, new()
            => ToXmlBytes(value, removeNamespace, removeVersion).BytesToString();
        #endregion

        #region 将对象Xml序列化成字节数组【Btye[]】 + ToXmlBytes<T>(this T value, bool removeNamespace = false, bool removeVersion = false) where T : class, new()
        /// <summary>
        /// 将对象Xml序列化成字节数组【Btye[]】
        /// </summary>
        /// <typeparam name="T">要序列化的对象类型</typeparam>
        /// <param name="value">要序列化的对象</param>
        /// <param name="removeNamespace">是否去掉命名空间</param>
        /// <param name="removeVersion">是否去掉版本信息</param>
        /// <returns></returns>
        public static byte[] ToXmlBytes<T>(this T value, bool removeNamespace = false, bool removeVersion = false) where T : class, new()
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = removeVersion,//【True】去除xml声明<?xml version="1.0" encoding="utf-8"?>
                Indent = true,//为True时，换行，缩进
                Encoding = Encoding.UTF8//默认为UTF8编码
            };
            using MemoryStream stream = new MemoryStream();
            using (XmlWriter xmlWriter = XmlWriter.Create(stream, settings))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                if (removeNamespace) ns.Add(string.Empty, string.Empty);//去除默认命名空间xmlns:xsd和xmlns:xsi
                new XmlSerializer(typeof(T)).Serialize(xmlWriter, value, ns);//序列化对象
            }
            return stream.ToArray();
        }
        #endregion
    }
}
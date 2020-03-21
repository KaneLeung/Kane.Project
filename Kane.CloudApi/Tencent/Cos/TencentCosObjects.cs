#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent
* 项目描述 ：常用云服务Api
* 类 名 称 ：TencentCosBuckets
* 类 描 述 ：腾讯云Cos存储桶文件列表相关实体
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi.Tencent
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/3/21 11:36:55
* 更新时间 ：2020/3/21 16:36:55
* 版 本 号 ：v1.0.1.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Kane.CloudApi.Tencent
{
    #region 腾讯云Cos存储桶文件列表相关实体 + TencentCosObjects
    /// <summary>
    /// 腾讯云Cos存储桶文件列表相关实体
    /// </summary>
    [XmlRoot("ListBucketResult")]
    public class TencentCosObjects
    {
        /// <summary>
        /// 存储桶的名称，格式为[BucketName-APPID]，例如examplebucket-1250000000
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 对象键匹配前缀，对应请求中的 prefix 参数
        /// </summary>
        public string Prefix { get; set; }
        /// <summary>
        /// 起始对象键标记，从该标记之后（不含）按照 UTF-8 字典序返回对象键条目，对应请求中的 marker 参数
        /// </summary>
        public string Marker { get; set; }
        /// <summary>
        /// 单次响应返回结果的最大条目数量，对应请求中的 max-keys 参数
        /// </summary>
        public int MaxKeys { get; set; }
        /// <summary>
        /// 分隔符，对应请求中的 delimiter 参数，且仅当请求中指定了 delimiter 参数才会返回该节点
        /// </summary>
        public string Delimiter { get; set; }
        /// <summary>
        /// 响应条目是否被截断，布尔值，例如 true 或 false
        /// </summary>
        public bool IsTruncated { get; set; }
        /// <summary>
        /// 从 prefix 或从头（如未指定 prefix）到首个 delimiter 之间相同的部分，定义为 Common Prefix。
        /// </summary>
        [XmlElement]
        public CommonPrefixes[] CommonPrefixes { get; set; }
        /// <summary>
        /// 对象条目
        /// </summary>
        [XmlElement]
        public Contents[] Contents { get; set; }
    }
    #endregion

    #region CommonPrefixes
    /// <summary>
    /// 从 prefix 或从头（如未指定 prefix）到首个 delimiter 之间相同的部分，定义为 Common Prefix。仅当请求中指定了 delimiter 参数才有可能返回该节点
    /// </summary>
    public class CommonPrefixes
    {
        /// <summary>
        /// 单条 Common Prefix 的前缀
        /// </summary>
        public string Prefix { get; set; }
    }
    #endregion

    #region 对象条目 + Contents
    /// <summary>
    /// 对象条目
    /// </summary>
    public class Contents
    {
        /// <summary>
        /// 对象键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 对象最后修改时间，为 ISO8601 格式，如2019-05-24T10:56:40Z
        /// </summary>
        public string LastModified { get; set; }
        /// <summary>
        /// 对象的实体标签（Entity Tag），是对象被创建时标识对象内容的信息标签，可用于检查对象的内容是否发生变化，
        /// </summary>
        public string ETag { get; set; }
        /// <summary>
        /// 对象大小，单位为 Byte
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 对象持有者信息
        /// </summary>
        public Owner Owner { get; set; }
        /// <summary>
        /// 对象存储类型。
        /// </summary>
        public StorageClass StorageClass { get; set; }
        /// <summary>
        /// 对象最后修改时间，转为当前时区的DateTime
        /// </summary>
        [XmlIgnore]
        public DateTime CreationDateLocal { get => DateTime.Parse(LastModified); }
    }
    #endregion

    #region 存储类型枚举 + StorageClass
    /// <summary>
    /// 存储类型枚举
    /// <para>https://cloud.tencent.com/document/product/436/33417</para>
    /// </summary>
    public enum StorageClass
    {
        /// <summary>
        /// 标准存储
        /// </summary>
        [Description("标准存储")]
        STANDARD = 0,
        /// <summary>
        /// 低频存储
        /// </summary>
        [Description("低频存储")]
        STANDARD_IA = 1,
        /// <summary>
        /// 归档存储
        /// </summary>
        [Description("归档存储")]
        ARCHIVE = 2,
    }
    #endregion
}
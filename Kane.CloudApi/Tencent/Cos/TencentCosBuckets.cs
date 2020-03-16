#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent
* 项目描述 ：
* 类 名 称 ：TencentCosBuckets
* 类 描 述 ：
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi.Tencent
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/3/16 22:36:55
* 更新时间 ：2020/3/16 22:36:55
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using Kane.Extension;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Kane.CloudApi.Tencent
{
    /// <summary>
    /// 存储桶列表实体类
    /// <para>https://cloud.tencent.com/document/product/436/8291</para>
    /// </summary>
    [XmlType("ListAllMyBucketsResult")]
    public class TencentCosBuckets
    {
        /// <summary>
        /// 存储桶持有者信息
        /// </summary>
        public Owner Owner { get; set; }
        /// <summary>
        /// 存储桶列表
        /// </summary>
        public List<Bucket> Buckets { get; set; }
    }

    /// <summary>
    /// 存储桶持有者信息
    /// </summary>
    public class Owner
    {
        /// <summary>
        /// 存储桶持有者的完整 ID，格式为qcs::cam::uin/[OwnerUin]:uin/[OwnerUin]
        /// <para>例如qcs::cam::uin/100000000001:uin/100000000001</para>
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 存储桶持有者的名字
        /// </summary>
        public string DisplayName { get; set; }
    }

    /// <summary>
    /// 存储桶
    /// </summary>
    public class Bucket
    {
        /// <summary>
        /// 存储桶的名称，格式为<BucketName-APPID>
        /// <para>例如 examplebucket-1250000000</para>
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 存储桶所在地域，枚举值请参见 地域和访问域名 文档 例如 ap-beijing，ap-hongkong，eu-frankfurt 等
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 存储桶的创建时间，为 ISO8601 格式，例如2019-05-24T10:56:40Z
        /// </summary>
        public string CreationDate { get; set; }
        /// <summary>
        /// 将创建时间，转为当前时区的DateTime
        /// </summary>
        [XmlIgnore]
        public DateTime CreationDateLocal { get => DateTime.Parse(CreationDate); }
    }
}
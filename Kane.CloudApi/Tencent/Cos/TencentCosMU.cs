#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent
* 项目描述 ：
* 类 名 称 ：TencentCosPart
* 类 描 述 ：
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi.Tencent
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/3/17 22:45:54
* 更新时间 ：2020/3/17 22:45:54
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Kane.CloudApi.Tencent
{
    /// <summary>
    /// 分块上传【MultipartUpload】简写成MU
    /// </summary>
    public class TencentCosMU
    {
        
    }

    #region 分块上传初始化【MultipartUploadInit】
    /// <summary>
    /// 接口请求实现初始化分片上传，成功执行此请求后将返回 UploadID，用于后续的 Upload Part 请求
    /// </summary>
    [XmlRoot("InitiateMultipartUploadResult")]
    public class TencentCosMUInit
    {
        /// <summary>
        /// 分片上传的目标 Bucket，由用户自定义字符串和系统生成 APPID 数字串由中划线连接而成，例如 examplebucket-1250000000
        /// </summary>
        public string Bucket { get; set; }
        /// <summary>
        /// Object 的名称
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 在后续上传中使用的 ID
        /// </summary>
        [XmlElement("UploadId")]
        public string UploadID { get; set; }
    }
    #endregion

    #region 分块上传完成请求实体【MultipartUploadComplete】
    /// <summary>
    /// 分块上传完成请求实体
    /// </summary>
    [XmlRoot("CompleteMultipartUpload")]
    public class TencentCosMUComplete
    {
        /// <summary>
        /// 用来说明本次分块上传中每个块的信息
        /// </summary>
        [XmlElement]
        public List<Part> Part { get; set; } = new List<Part>();
    }

    /// <summary>
    /// 用来说明本次分块上传中每个块的信息
    /// </summary>
    public class Part
    {
        /// <summary>
        /// 块编号
        /// </summary>
        public int PartNumber { get; set; }
        /// <summary>
        /// 每个块文件的 MD5 算法校验值
        /// </summary>
        public string ETag { get; set; }
    }
    #endregion

    #region 分块上传完成请求返回信息【TencentCosMUCompleteResult】
    /// <summary>
    /// 分块上传完成请求返回信息
    /// </summary>
    [XmlRoot("CompleteMultipartUploadResult", Namespace = "http://www.qcloud.com/document/product/436/7751")]
    public class TencentCosMUCompleteResult
    {
        /// <summary>
        /// 新创建的对象的外网访问域名
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 分块上传的目标存储桶，格式为 BucketName-APPID，例如：examplebucket-1250000000
        /// </summary>
        public string Bucket { get; set; }
        /// <summary>
        /// 对象名称
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 合并后对象的唯一标签值，该值不是对象内容的 MD5 校验值，仅能用于检查对象唯一性
        /// </summary>
        public string ETag { get; set; }
    }
    #endregion
}
#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent
* 项目描述 ：
* 类 名 称 ：TencentCosResult
* 类 描 述 ：
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi.Tencent
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/3/15 22:41:58
* 更新时间 ：2020/3/15 22:41:58
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
    /// 错误信息返回内容
    /// <para>https://cloud.tencent.com/document/product/436/7730</para>
    /// </summary>
    [XmlRoot("Error")]
    public class TencentCosResult
    {
        /// <summary>
        /// 错误码，用来定位唯一的错误条件和确定错误场景
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 具体的错误信息。
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 请求的资源，存储桶【Bucket】地址或对象【Object】地址。
        /// </summary>
        public string Resource { get; set; }
        /// <summary>
        /// 每次请求发送时，服务端将会自动为请求生成一个 ID，遇到问题时，该 ID 能更快地协助 COS 定位问题
        /// </summary>
        [XmlAttribute("RequestID")]
        public string RequestID { get; set; }
        /// <summary>
        /// 每次请求出错时，服务端将会自动为这个错误生成一个ID，遇到问题时，该 ID 能更快地协助 COS 定位问题
        /// </summary>
        [XmlAttribute("TraceID")]
        public string TraceID { get; set; }
    }
}
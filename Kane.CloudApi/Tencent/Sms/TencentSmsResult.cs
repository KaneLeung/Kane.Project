#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent
* 项目描述 ：常用云服务Api
* 类 名 称 ：TencentSmsResult
* 类 描 述 ：腾讯云发送短信返回结果模型实体
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi.Tencent
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/2/23 0:45:44
* 更新时间 ：2020/2/23 0:45:44
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System.Collections.Generic;
#if NETCOREAPP3_1
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace Kane.CloudApi.Tencent
{
    /// <summary>
    /// 腾讯云发送短信返回结果模型实体
    /// https://cloud.tencent.com/document/product/382/38778
    /// </summary>
    public class TencentSmsResult : TencentResultBase<TencentSmsResponse>
    {
        /// <summary>
        /// 发送成功数
        /// </summary>
        [JsonIgnore]
        public int SuccessCount { get; set; } = 0;
        /// <summary>
        /// 发送成功的号码数组
        /// </summary>
        [JsonIgnore]
        public List<string> SuccessNo { get; set; } = new List<string>();
        /// <summary>
        /// 发送失败数
        /// </summary>
        [JsonIgnore]
        public int FailCount { get; set; } = 0;
        /// <summary>
        /// 发送失败的号码数组
        /// </summary>
        [JsonIgnore]
        public List<string> FailNo { get; set; } = new List<string>();
        /// <summary>
        /// 发送失败的原因数组
        /// </summary>
        [JsonIgnore]
        public List<string> FailMessage { get; set; } = new List<string>();
        /// <summary>
        /// 是否全部成功
        /// </summary>
        [JsonIgnore]
        public bool AllSuccess { get; set; } = true;
        /// <summary>
        /// 发送失败的原因
        /// </summary>
        [JsonIgnore]
        public string Message { get; set; }
    }

    /// <summary>
    /// 发送短信的响应类
    /// </summary>
    public class TencentSmsResponse : TencentResponseBase
    {
        /// <summary>
        /// 短信发送状态数组
        /// </summary>
        public Sendstatusset[] SendStatusSet { get; set; }
    }

    /// <summary>
    /// 短信发送状态
    /// </summary>
    public class Sendstatusset
    {
        /// <summary>
        /// 发送流水号
        /// </summary>
        public string SerialNo { get; set; }
        /// <summary>
        /// 手机号码,e.164标准，+[国家或地区码][手机号] ，示例如：+8613711112222， 其中前面有一个+号 ，86为国家码，13711112222为手机号。
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 计费条数，计费规则请查询
        /// https://cloud.tencent.com/document/product/382/36135
        /// </summary>
        public int Fee { get; set; }
        /// <summary>
        /// 用户Session内容。
        /// </summary>
        public string SessionContext { get; set; }
        /// <summary>
        /// 短信请求错误码，具体含义请参考错误码。
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 短信请求错误码描述。
        /// </summary>
        public string Message { get; set; }
    }
}
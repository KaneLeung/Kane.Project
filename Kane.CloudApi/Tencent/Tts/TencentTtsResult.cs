#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent
* 项目描述 ：常用云服务Api
* 类 名 称 ：TencentTextDetectResult
* 类 描 述 ：腾讯云Tts【语音合成】返回结果模型实体
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi.Tencent
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/3/23 11:00:11
* 更新时间 ：2020/3/23 11:00:11
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
#if NETCOREAPP3_1
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace Kane.CloudApi.Tencent
{
    /// <summary>
    /// 腾讯云Tts【语音合成】返回结果模型实体
    /// <para>https://cloud.tencent.com/document/api/1073/37995</para>
    /// </summary>
    public class TencentTtsResult : TencentResultBase<TencentTtsResponse>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonIgnore]
        public bool Success { get; set; } = true;
        /// <summary>
        /// 发送失败的原因
        /// </summary>
        [JsonIgnore]
        public string Message { get; set; }
    }

    /// <summary>
    /// 语音合成返回的Response
    /// </summary>
    public class TencentTtsResponse : TencentResponseBase
    {
        /// <summary>
        /// Base64编码的wav/mp3音频数据。
        /// </summary>
        public string Audio { get; set; }
    }
}
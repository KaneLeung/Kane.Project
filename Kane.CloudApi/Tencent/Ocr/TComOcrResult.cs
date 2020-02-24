#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent.Ocr
* 项目描述 ：常用云服务Api
* 类 名 称 ：TEnglishOcrResult
* 类 描 述 ：腾讯云通用文字识别返回结果模型实体
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi.Tencent.Ocr
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/2/23 21:00:11
* 更新时间 ：2020/2/23 21:00:11
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
#if (NETCOREAPP3_0 || NETCOREAPP3_1)
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace Kane.CloudApi.Tencent
{
    /// <summary>
    /// 腾讯云通用文字识别返回结果模型实体
    /// https://cloud.tencent.com/document/product/866/34938
    /// </summary>
    public class TComOcrResult : TencentResultBase<TComOcrResponse>
    {
        /// <summary>
        /// 平均置信度 0 ~ 100
        /// </summary>
        [JsonIgnore]
        public int AvgConfidence { get; set; } = 0;
        /// <summary>
        /// 所有文本加起来
        /// </summary>
        [JsonIgnore]
        public string Content { get; set; }
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
    /// EnglishOcr返回的Response
    /// </summary>
    public class TComOcrResponse : TencentResponseBase
    {
        /// <summary>
        /// 检测到的文本信息数组
        /// </summary>
        public Textdetection[] TextDetections { get; set; }
    }

    /// <summary>
    /// 文字识别结果
    /// </summary>
    public class Textdetection
    {
        /// <summary>
        /// 识别出的文本行内容
        /// </summary>
        public string DetectedText { get; set; }
        /// <summary>
        /// 置信度 0 ~ 100
        /// </summary>
        public int Confidence { get; set; }
        /// <summary>
        /// 文本行坐标，以四个顶点坐标表示
        /// 注意：此字段可能返回 null，表示取不到有效值。
        /// </summary>
        public Coord[] Polygon { get; set; }
        /// <summary>
        /// 此字段为扩展字段。GeneralBasicOcr接口返回段落信息Parag，包含ParagNo。
        /// </summary>
        public string AdvancedInfo { get; set; }
    }
}
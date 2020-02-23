#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent.Ocr
* 项目描述 ：
* 类 名 称 ：TEnglishOcrResult
* 类 描 述 ：
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
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Kane.CloudApi.Tencent
{
    /// <summary>
    /// 腾讯云发送短信返回结果模型实体
    /// https://cloud.tencent.com/document/product/382/38778
    /// </summary>
    public class TEnglishOcrResult : TencentResultBase<TEnglishOcr>
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
    public class TEnglishOcr : TencentResponseBase
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
        public Polygon[] Polygon { get; set; }
        /// <summary>
        /// 此字段为扩展字段。GeneralBasicOcr接口返回段落信息Parag，包含ParagNo。
        /// </summary>
        public string AdvancedInfo { get; set; }
    }

    /// <summary>
    /// 坐标
    /// </summary>
    public class Polygon
    {
        /// <summary>
        /// 横坐标
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// 纵坐标
        /// </summary>
        public int Y { get; set; }
    }

}
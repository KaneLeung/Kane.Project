#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent
* 项目描述 ：常用云服务Api
* 类 名 称 ：TencentOcrService
* 类 描 述 ：腾讯云Tts【语音合成】枚举类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi.Tencent
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/3/23 09:48:40
* 更新时间 ：2020/3/23 09:48:40
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System.ComponentModel;

namespace Kane.CloudApi.Tencent
{
    /// <summary>
    /// 音色枚举类
    /// <para>https://cloud.tencent.com/document/api/1073/37995</para>
    /// </summary>
    public enum Voice
    {
        /// <summary>
        /// 云小宁，亲和女声(默认)
        /// </summary>
        [Description("云小宁")]
        Yunxiaoning = 0,
        /// <summary>
        /// 云小奇，亲和男声
        /// </summary>
        [Description("云小奇")]
        Yunxiaoqi = 1,
        /// <summary>
        /// 云小晚，成熟男声
        /// </summary>
        [Description("云小晚")]
        Yunxiaowan = 2,
        /// <summary>
        /// 云小叶，温暖女声
        /// </summary>
        [Description("云小叶")]
        Yunxiaoye = 4,
        /// <summary>
        /// 云小欣，情感女声
        /// </summary>
        [Description("云小欣")]
        Yunxiaoxin = 5,
        /// <summary>
        /// 云小龙，情感男声
        /// </summary>
        [Description("云小龙")]
        Yunxiaolong = 6,
        /// <summary>
        /// 智侠、情感男声（新）
        /// </summary>
        [Description("智侠")]
        Zhixia = 1000,
        /// <summary>
        /// 智瑜，情感女声（新）
        /// </summary>
        [Description("智瑜")]
        Zhiyu = 1001,
        /// <summary>
        /// 智聆，通用女声（新）
        /// </summary>
        [Description("智聆")]
        Zhiling = 1002,
        /// <summary>
        /// 智美，客服女声（新）
        /// </summary>
        [Description("智美")]
        Zhimei = 1003,
        /// <summary>
        /// WeJack，英文男声（新）
        /// </summary>
        [Description("WeJack")]
        WeJack = 1050,
        /// <summary>
        /// WeRose，英文女声（新）
        /// </summary>
        [Description("WeRose")]
        WeRose = 1051,
    }

    /// <summary>
    /// 主语言类型
    /// </summary>
    public enum Language
    {
        /// <summary>
        /// 中文（默认）
        /// </summary>
        [Description("中文")]
        Chinese = 1,
        /// <summary>
        /// 英文
        /// </summary>
        [Description("英文")]
        English = 2,
    }

    /// <summary>
    /// 音频采样率
    /// </summary>
    public enum Rate
    {
        /// <summary>
        /// 16k（默认）
        /// </summary>
        [Description("中文")]
        K16 = 16000,
        /// <summary>
        /// 8k
        /// </summary>
        [Description("英文")]
        K8 = 8000,
    }

    /// <summary>
    /// 音频格式
    /// </summary>
    public enum Codec
    {
        /// <summary>
        /// Wav格式
        /// </summary>
        [Description("Wav格式")]
        Wav,
        /// <summary>
        /// Mp3格式
        /// </summary>
        [Description("Mp3格式")]
        Mp3,
    }
}
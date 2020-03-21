#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent
* 项目描述 ：常用云服务Api
* 类 名 称 ：TQrcodrOcrResult
* 类 描 述 ：腾讯云二维码和条形码识别返回结果模型实体
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi.Tencent
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/2/23 0:45:44
* 更新时间 ：2020/3/21 17:45:44
* 版 本 号 ：v1.0.1.0
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
    /// 腾讯云二维码和条形码识别返回结果模型实体
    /// https://cloud.tencent.com/document/api/866/38292
    /// </summary>
    public class TencentQrcodeResult : TencentResultBase<TencentQrcodeResponse>
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
    /// QrcodeOCR返回的Response
    /// </summary>
    public class TencentQrcodeResponse : TencentResponseBase
    {
        /// <summary>
        /// 二维码/条形码识别结果信息
        /// </summary>
        public Coderesult[] CodeResults { get; set; }
        /// <summary>
        /// 图片大小
        /// </summary>
        public Imgsize ImgSize { get; set; }
    }

    /// <summary>
    /// 图片大小
    /// </summary>
    public class Imgsize
    {
        /// <summary>
        /// 宽
        /// </summary>
        public int Wide { get; set; }
        /// <summary>
        /// 高
        /// </summary>
        public int High { get; set; }
    }

    /// <summary>
    /// 二维码/条形码识别结果信息
    /// </summary>
    public class Coderesult
    {
        /// <summary>
        /// 类型（二维码、条形码）
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 二维码/条形码包含的地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 二维码/条形码坐标（二维码会返回位置坐标，条形码暂不返回位置坐标，因此默认X和Y返回值均为-1）
        /// </summary>
        public Position Position { get; set; }
    }

    /// <summary>
    /// 二维码/条形码坐标（二维码会返回位置坐标，条形码暂不返回位置坐标，因此默认X和Y返回值均为-1）
    /// </summary>
    public class Position
    {
        /// <summary>
        /// 左上顶点坐标（如果是条形码，X和Y都为-1）
        /// </summary>
        public Coord LeftTop { get; set; }
        /// <summary>
        /// 右上顶点坐标（如果是条形码，X和Y都为-1）
        /// </summary>
        public Coord RightTop { get; set; }
        /// <summary>
        /// 右下顶点坐标（如果是条形码，X和Y都为-1）
        /// </summary>
        public Coord RightBottom { get; set; }
        /// <summary>
        /// 左下顶点坐标（如果是条形码，X和Y都为-1）
        /// </summary>
        public Coord LeftBottom { get; set; }
    }
}
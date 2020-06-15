#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent
* 项目描述 ：常用云服务Api
* 类 名 称 ：TencentOcrService
* 类 描 述 ：腾讯云OcrApi服务
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi.Tencent
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/2/23 20:48:40
* 更新时间 ：2020/3/21 18:48:40
* 版 本 号 ：v1.0.1.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using Kane.Extension;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
#if NETCOREAPP3_1
using Kane.Extension.Json;
#else
using Kane.Extension.JsonNet;
#endif

namespace Kane.CloudApi.Tencent
{
    /// <summary>
    /// 腾讯云OcrApi服务
    /// </summary>
    public class TencentOcrService : TencentService
    {
        #region 无参构造函数 + TencentOcrService()
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public TencentOcrService()
        {
            ServiceHost = "ocr.tencentcloudapi.com";
            XtcRegion = "ap-guangzhou";
            XtcVersion = "2018-11-19";
        }
        #endregion

        #region 构造函数 + TencentOcrService(string secretID, string secretKey) : this()
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="secretID">在云API密钥上申请的标识身份的 SecretID，一个 SecretID 对应唯一的 SecretKey</param>
        /// <param name="secretKey">SecretID 对应唯一的 SecretKey</param>
        public TencentOcrService(string secretID, string secretKey) : this()
        {
            SecretID = secretID;
            SecretKey = secretKey;
        }
        #endregion

        #region 通用文字识别 + CommonOCR(string actionName, string imageBase64 = null, string imageUrl = null)
        /// <summary>
        /// 通用文字识别
        /// <para>格式：PNG、JPG、JPEG，暂不支持 GIF 格式。</para>
        /// <para>图片大小：所下载图片经 Base64 编码后不超过 3M。图片下载时间不超过 3 秒。</para>
        /// <para>图片的 ImageUrl、ImageBase64 必须提供一个，如果都提供，只使用 ImageUrl。</para>
        /// </summary>
        /// <param name="actionName">操作的接口名称</param>
        /// <param name="imageBase64">图片的Base64值</param>
        /// <param name="imageUrl">图片的Url地址</param>
        /// <returns></returns>
        internal async Task<TencentOcrResult> CommonOCR(string actionName, string imageBase64 = null, string imageUrl = null)
        {
            actionName.ThrowIfNull(nameof(actionName));
            var imagedata = new
            {
                ImageBase64 = imageBase64,
                ImageUrl = imageUrl,
            };
            var result = await base.RequestService(imagedata.ToJson(), actionName);
            if (result.success)
            {
                var data = result.message.ToObject<TencentOcrResult>();
                if (data.Response.Error.IsNull() && data.Response.TextDetections.Length > 0)
                {
                    int confidence = 0;
                    var sb = new StringBuilder();
                    foreach (var item in data.Response.TextDetections)
                    {
                        sb.Append(item.DetectedText);
                        confidence += item.Confidence;
                    }
                    data.AvgConfidence = confidence / data.Response.TextDetections.Length;
                    data.Content = sb.ToString();
                    data.Message = "Success";
                    return data;
                }
                else
                {
                    data.Success = false;
                    data.Message = data.Response.Error.Message;
                    return data;
                }
            }
            else return new TencentOcrResult() { Success = false, Message = result.message };
        }
        #endregion

        #region 英文识别 + EnglishOcr(string imageBase64 = null, string imageUrl = null)
        /// <summary>
        /// 英文识别
        /// <para>格式：PNG、JPG、JPEG，暂不支持 GIF 格式。</para>
        /// <para>图片大小：所下载图片经 Base64 编码后不超过 3M。图片下载时间不超过 3 秒。</para>
        /// <para>图片的 ImageUrl、ImageBase64 必须提供一个，如果都提供，只使用 ImageUrl。</para>
        /// </summary>
        /// <param name="imageBase64">图片的Base64值</param>
        /// <param name="imageUrl">图片的Url地址</param>
        /// <returns></returns>
        public async Task<TencentOcrResult> EnglishOcr(string imageBase64 = null, string imageUrl = null)
            => await this.CommonOCR("EnglishOCR", imageBase64, imageUrl);
        #endregion

        #region 英文识别 + EnglishOcr(Image image)
        /// <summary>
        /// 英文识别，使用Image格式
        /// <para>格式：PNG、JPG、JPEG，暂不支持 GIF 格式。</para>
        /// <para>图片大小：所下载图片经 Base64 编码后不超过 3M。图片下载时间不超过 3 秒。</para>
        /// <para>图片的 ImageUrl、ImageBase64 必须提供一个，如果都提供，只使用 ImageUrl。</para>
        /// </summary>
        /// <param name="image">Image格式图片</param>
        /// <returns></returns>
        public async Task<TencentOcrResult> EnglishOcr(Image image)
        {
            var base64 = image.ToBase64();
            if (base64.IsNullOrEmpty()) return new TencentOcrResult() { Success = false, Message = "图像转成Base64失败" };
            return await this.EnglishOcr(base64);
        }
        #endregion

        #region 通用印刷体识别（高精度版） + GeneralAccurateOcr(string imageBase64 = null, string imageUrl = null)
        /// <summary>
        /// 通用印刷体识别（高精度版）
        /// <para>格式：PNG、JPG、JPEG，暂不支持 GIF 格式。</para>
        /// <para>图片大小：所下载图片经 Base64 编码后不超过 3M。图片下载时间不超过 3 秒。</para>
        /// <para>图片的 ImageUrl、ImageBase64 必须提供一个，如果都提供，只使用 ImageUrl。</para>
        /// </summary>
        /// <param name="imageBase64">图片的Base64值</param>
        /// <param name="imageUrl">图片的Url地址</param>
        /// <returns></returns>
        public async Task<TencentOcrResult> GeneralAccurateOcr(string imageBase64 = null, string imageUrl = null)
            => await CommonOCR("GeneralAccurateOCR", imageBase64, imageUrl);
        #endregion

        #region 通用印刷体识别 + GeneralBasicOcr(string imageBase64 = null, string imageUrl = null)
        /// <summary>
        /// 通用印刷体识别
        /// <para>格式：PNG、JPG、JPEG，暂不支持 GIF 格式。</para>
        /// <para>图片大小：所下载图片经 Base64 编码后不超过 3M。图片下载时间不超过 3 秒。</para>
        /// <para>图片的 ImageUrl、ImageBase64 必须提供一个，如果都提供，只使用 ImageUrl。</para>
        /// </summary>
        /// <param name="imageBase64">图片的Base64值</param>
        /// <param name="imageUrl">图片的Url地址</param>
        /// <returns></returns>
        public async Task<TencentOcrResult> GeneralBasicOcr(string imageBase64 = null, string imageUrl = null)
            => await CommonOCR("GeneralBasicOCR", imageBase64, imageUrl);
        #endregion

        #region 通用印刷体识别（精简版） + GeneralEfficientOcr(string imageBase64 = null, string imageUrl = null) 
        /// <summary>
        /// 通用印刷体识别（精简版）
        /// <para>格式：PNG、JPG、JPEG，暂不支持 GIF 格式。</para>
        /// <para>图片大小：所下载图片经 Base64 编码后不超过 3M。图片下载时间不超过 3 秒。</para>
        /// <para>图片的 ImageUrl、ImageBase64 必须提供一个，如果都提供，只使用 ImageUrl。</para>
        /// </summary>
        /// <param name="imageBase64">图片的Base64值</param>
        /// <param name="imageUrl">图片的Url地址</param>
        /// <returns></returns>
        public async Task<TencentOcrResult> GeneralEfficientOcr(string imageBase64 = null, string imageUrl = null)
            => await CommonOCR("GeneralEfficientOCR", imageBase64, imageUrl);
        #endregion

        #region 通用印刷体识别（高速版） + GeneralFastOcr(string imageBase64 = null, string imageUrl = null)
        /// <summary>
        /// 通用印刷体识别（高速版）
        /// <para>格式：PNG、JPG、JPEG，暂不支持 GIF 格式。</para>
        /// <para>图片大小：所下载图片经 Base64 编码后不超过 3M。图片下载时间不超过 3 秒。</para>
        /// <para>图片的 ImageUrl、ImageBase64 必须提供一个，如果都提供，只使用 ImageUrl。</para>
        /// </summary>
        /// <param name="imageBase64">图片的Base64值</param>
        /// <param name="imageUrl">图片的Url地址</param>
        /// <returns></returns>
        public async Task<TencentOcrResult> GeneralFastOcr(string imageBase64 = null, string imageUrl = null)
            => await CommonOCR("GeneralFastOCR", imageBase64, imageUrl);
        #endregion

        #region 通用手写体识别 + GeneralHandwritingOcr(string imageBase64 = null, string imageUrl = null)
        /// <summary>
        /// 通用手写体识别
        /// <para>格式：PNG、JPG、JPEG，暂不支持 GIF 格式。</para>
        /// <para>图片大小：所下载图片经 Base64 编码后不超过 3M。图片下载时间不超过 3 秒。</para>
        /// <para>图片的 ImageUrl、ImageBase64 必须提供一个，如果都提供，只使用 ImageUrl。</para>
        /// </summary>
        /// <param name="imageBase64">图片的Base64值</param>
        /// <param name="imageUrl">图片的Url地址</param>
        /// <returns></returns>
        public async Task<TencentOcrResult> GeneralHandwritingOcr(string imageBase64 = null, string imageUrl = null)
            => await CommonOCR("GeneralHandwritingOCR", imageBase64, imageUrl);
        #endregion

        #region 二维码和条形码识别 + QrcodeOcr(string imageBase64 = null, string imageUrl = null)
        /// <summary>
        /// 二维码和条形码识别
        /// <para>本接口支持条形码和二维码的识别（包括 DataMatrix 和 PDF417）。</para>
        /// <para>格式：PNG、JPG、JPEG，暂不支持 GIF 格式。</para>
        /// <para>图片大小：所下载图片经 Base64 编码后不超过 3M。图片下载时间不超过 3 秒。</para>
        /// <para>图片的 ImageUrl、ImageBase64 必须提供一个，如果都提供，只使用 ImageUrl。</para>
        /// </summary>
        /// <param name="imageBase64">图片的Base64值</param>
        /// <param name="imageUrl">图片的Url地址</param>
        /// <returns></returns>
        public async Task<TencentQrcodeResult> QrcodeOcr(string imageBase64 = null, string imageUrl = null)
        {
            var imagedata = new
            {
                ImageBase64 = imageBase64,
                ImageUrl = imageUrl,
            };
            var result = await base.RequestService(imagedata.ToJson(), "QrcodeOCR");
            if (result.success)
            {
                var data = result.message.ToObject<TencentQrcodeResult>();
                if (data.Response.Error.IsNull() && data.Response.CodeResults.Length > 0)
                {
                    data.Message = "Success";
                    return data;
                }
                else
                {
                    data.Success = false;
                    data.Message = data.Response.Error.Message;
                    return data;
                }
            }
            else return new TencentQrcodeResult() { Success = false, Message = result.message };
        }
        #endregion

        #region 快速文本检测 + TextDetect(string imageBase64 = null, string imageUrl = null)
        /// <summary>
        /// 快速文本检测
        /// <para>本接口通过检测图片中的文字信息特征，快速判断图片中有无文字并返回判断结果，帮助用户过滤无文字的图片。默认接口请求频率限制：5次/秒。</para>
        /// <para>本接口支持条形码和二维码的识别（包括 DataMatrix 和 PDF417）。</para>
        /// <para>格式：PNG、JPG、JPEG，暂不支持 GIF 格式。</para>
        /// <para>图片大小：所下载图片经 Base64 编码后不超过 3M。图片下载时间不超过 3 秒。</para>
        /// <para>图片的 ImageUrl、ImageBase64 必须提供一个，如果都提供，只使用 ImageUrl。</para>
        /// </summary>
        /// <param name="imageBase64">图片的Base64值</param>
        /// <param name="imageUrl">图片的Url地址</param>
        /// <returns></returns>
        public async Task<TencentTextDetectResult> TextDetect(string imageBase64 = null, string imageUrl = null)
        {
            var imagedata = new
            {
                ImageBase64 = imageBase64,
                ImageUrl = imageUrl,
            };
            var result = await RequestService(imagedata.ToJson(), "TextDetect");
            if (result.success)
            {
                var data = result.message.ToObject<TencentTextDetectResult>();
                if (data.Response.Error.IsNull())
                {
                    data.Message = "Success";
                    return data;
                }
                else
                {
                    data.Success = false;
                    data.Message = data.Response.Error.Message;
                    return data;
                }
            }
            else return new TencentTextDetectResult() { Success = false, Message = result.message };
        }
        #endregion
    }
}
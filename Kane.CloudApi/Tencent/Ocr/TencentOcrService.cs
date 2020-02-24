#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent
* 项目描述 ：
* 类 名 称 ：TencentOcrService
* 类 描 述 ：
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi.Tencent
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/2/23 20:48:40
* 更新时间 ：2020/2/23 20:48:40
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using Kane.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Kane.CloudApi.Tencent
{
    public class TencentOcrService: TencentService
    {
        public TencentOcrService()
        {
            ServiceHost = "ocr.tencentcloudapi.com";
            XtcRegion = "ap-guangzhou";
            XtcVersion = "2018-11-19";
        }

        public TencentOcrService(string secretID, string secretKey) : this()
        {
            SecretID = secretID;
            SecretKey = secretKey;
        }

        #region 英文识别 + EnglishOCR(string imageBase64 = null, string imageUrl = null)
        /// <summary>
        /// 英文识别
        /// 格式：PNG、JPG、JPEG，暂不支持 GIF 格式。
        /// 图片大小：所下载图片经 Base64 编码后不超过 3M。图片下载时间不超过 3 秒。
        /// 图片的 ImageUrl、ImageBase64 必须提供一个，如果都提供，只使用 ImageUrl。
        /// </summary>
        /// <param name="imageBase64">图片的Base64值</param>
        /// <param name="imageUrl">图片的Url地址</param>
        /// <returns></returns>
        public async Task<TEnglishOcrResult> EnglishOCR(string imageBase64 = null, string imageUrl = null)
        {
            var imagedata = new
            {
                ImageBase64 = imageBase64,
                ImageUrl = imageUrl,
            };
            var result = await base.RequestService(imagedata.ToJson());
            if (result.success)
            {
                var data = result.message.ToObject<TEnglishOcrResult>();
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
            else return new TEnglishOcrResult() { Success = false, Message = result.message };
        }
        #endregion

        #region 英文识别 + EnglishOCR(Image image)
        /// <summary>
        /// 英文识别，使用Image格式
        /// 格式：PNG、JPG、JPEG，暂不支持 GIF 格式。
        /// 图片大小：所下载图片经 Base64 编码后不超过 3M。图片下载时间不超过 3 秒。
        /// 图片的 ImageUrl、ImageBase64 必须提供一个，如果都提供，只使用 ImageUrl。
        /// </summary>
        /// <param name="imageBase64">图片的Base64值</param>
        /// <param name="imageUrl">图片的Url地址</param>
        /// <returns></returns>
        public async Task<TEnglishOcrResult> EnglishOCR(Image image)
        {
            var base64 = image.ToBase64();
            if (base64.IsNullOrEmpty()) return new TEnglishOcrResult() { Success = false, Message = "图像转成Base64失败" };
            return await this.EnglishOCR(base64);
        }
        #endregion
    }
}
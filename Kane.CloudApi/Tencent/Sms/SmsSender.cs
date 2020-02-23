#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent.Sms
* 项目描述 ：
* 类 名 称 ：SmsSender
* 类 描 述 ：
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi.Tencent.Sms
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/2/21 10:52:50
* 更新时间 ：2020/2/21 10:52:50
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using Kane.CloudApi.Tencent.Sms;
using Kane.Extension;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace Kane.CloudApi.Tencent
{
    /// <summary>
    /// 基于【2019-07-11】
    /// https://cloud.tencent.com/document/api/382/38778
    /// </summary>
    public class SmsSender
    {
        /// <summary>
        /// 操作的 API 的版本
        /// </summary>
        private const string Version = "2019-07-11";
        /// <summary>
        /// 服务地址
        /// API 支持就近地域接入，本产品就近地域接入域名为 sms.tencentcloudapi.com ，
        /// 也支持指定地域域名访问，例如广州地域的域名为 sms.ap-guangzhou.tencentcloudapi.com 。
        /// https://cloud.tencent.com/document/api/382/38766
        /// </summary>
        private string Host = "sms.tencentcloudapi.com";
        /// <summary>
        /// 用于标识 API 调用者身份，可以简单类比为用户名
        /// </summary>
        private string SecretID = string.Empty;
        /// <summary>
        /// 用于验证 API 调用者的身份，可以简单类比为密码
        /// </summary>
        private string SecretKey = string.Empty;


        public SmsSender(string secretID, string secretKey)
        {
            SecretID = secretID;
            SecretKey = secretKey;
        }

        public SmsSender(string secretID, string secretKey, string host) : this(secretID, secretKey) => Host = host;


        public async void Send(string smsSdkAppid, string templateID, IEnumerable<string> phoneNumbers, params string[] templateParam)
        {
            try
            {
                var date = DateTime.UtcNow.ToString("yyyy-MM-dd");
                var timestamp = DateTimeHelper.GetTimeStamp();
                var smsdata = new SmsData()
                {
                    TemplateID = templateID,
                    SmsSdkAppid = smsSdkAppid,
                    PhoneNumberSet = phoneNumbers.Select(k => k.ToISPhoneNo()),
                    TemplateParamSet = templateParam,
                };
                string canonicalRequest = $"POST\n/\n\ncontent-type:application/json; charset=utf-8\nhost:{Host}\n\ncontent-type;host\n{CryptoHelper.SHA256Encrypt(smsdata.ToJson()).ToLower()}";
                string stringToSign = $"TC3-HMAC-SHA256\n{timestamp}\n{date}/sms/tc3_request\n{CryptoHelper.SHA256Encrypt(canonicalRequest).ToLower()}";

                byte[] secretDate = HmacSHA256(date.ToBytes(), "TC3".Add(SecretKey).ToBytes());
                byte[] secretService = HmacSHA256("sms".ToBytes(), secretDate);
                byte[] secretSigning = HmacSHA256("tc3_request".ToBytes(), secretService);
                byte[] signature = HmacSHA256(stringToSign.ToBytes(), secretSigning);
                string authorization = $"TC3-HMAC-SHA256 Credential={SecretID}/{date}/sms/tc3_request, SignedHeaders=content-type;host, Signature={signature.ByteToHex().ToLower()}";

                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorization);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Host", Host);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-TC-Timestamp", timestamp);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-TC-Version", Version);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-TC-Region", "ap-guangzhou");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-TC-Action", "SendSms");
                using var content = new StringContent(smsdata.ToJson(), Encoding.UTF8, "application/json");
                using var request = new HttpRequestMessage(HttpMethod.Post, $"https://{Host}".ToUrl());
                request.Content = content;
                var response = await httpClient.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();
                var rep = responseBody.ToObject<ResponseData>();
                //if (response.IsSuccessStatusCode)
                //{
                //    var result = responseBody.ToObject<OneDriveAccessToken>();
                //    return result;
                //}
                //throw new Exception(responseBody);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static byte[] HmacSHA256(byte[] data, byte[] key)
        {
            using HMACSHA256 mac = new HMACSHA256(key);
            return mac.ComputeHash(data);
        }

        public class SmsData
        {
            /// <summary>
            /// 短信SdkAppid在 [短信控制台](https://console.cloud.tencent.com/sms/smslist)  添加应用后生成的实际SdkAppid，示例如1400006666。
            /// </summary>
            public string SmsSdkAppid { get; set; }
            /// <summary>
            /// 模板 ID，必须填写已审核通过的模板 ID。模板ID可登录 [短信控制台](https://console.cloud.tencent.com/sms/smslist) 查看。
            /// </summary>
            public string TemplateID { get; set; }
            /// <summary>
            /// 下发手机号码，采用 e.164 标准，+[国家或地区码][手机号] ，示例如：+8613711112222， 其中前面有一个+号 ，86为国家码，13711112222为手机号，最多不要超过200个手机号。
            /// </summary>
            public IEnumerable<string> PhoneNumberSet { get; set; }
            /// <summary>
            /// 模板参数，若无模板参数，则设置为空。
            /// </summary>
            public IEnumerable<string> TemplateParamSet { get; set; }
        }


        public class ResponseData
        {
            public Response Response { get; set; }
        }

        public class Response
        {
            public Sendstatusset[] SendStatusSet { get; set; }
            public Error Error { get; set; }
            public string RequestId { get; set; }
        }

        public class Error
        {
            public string Code { get; set; }
            public string Message { get; set; }
        }

        public class Sendstatusset
        {
            public string SerialNo { get; set; }
            public string PhoneNumber { get; set; }
            public int Fee { get; set; }
            public string SessionContext { get; set; }
            public string Code { get; set; }
            public string Message { get; set; }
        }

    }
}
#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent
* 项目描述 ：常用云服务Api
* 类 名 称 ：TencentService
* 类 描 述 ：腾讯云服务Api基类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi.Tencent
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/2/23 0:00:15
* 更新时间 ：2020/2/23 0:00:15
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using Kane.Extension;
using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kane.CloudApi.Tencent
{
    /// <summary>
    /// 腾讯云服务Api基类
    /// </summary>
    public class TencentService
    {
        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServiceHost { get; set; }
        /// <summary>
        /// 在云API密钥上申请的标识身份的 SecretID，一个 SecretID 对应唯一的 SecretKey
        /// </summary>
        public string SecretID { private get; set; }
        /// <summary>
        /// SecretID 对应唯一的 SecretKey
        /// </summary>
        public string SecretKey { private get; set; }
        /// <summary>
        /// 地域参数，用来标识希望操作哪个地域的数据。接口接受的地域取值参考接口文档中输入参数公共参数 Region 的说明。
        /// 注意：某些接口不需要传递该参数，接口文档中会对此特别说明，此时即使传递该参数也不会生效。
        /// </summary>
        public string XtcRegion { get; set; }
        /// <summary>
        /// 操作的 API 的版本。取值参考接口文档中入参公共参数 Version 的说明。
        /// </summary>
        public string XtcVersion { get; set; }

        /// <summary>
        /// 腾讯去ApiPost请求服务
        /// </summary>
        /// <param name="paramJson">参数Json字符串</param>
        /// <param name="actionName">操作的接口名称。取值参考接口文档中输入参数公共参数 Action 的说明。</param>
        /// <returns></returns>
        public async Task<(bool success, string message)> RequestService(string paramJson, [CallerMemberName] string actionName = null)
        {
            Common.CheckParameter(SecretID, nameof(SecretID));
            Common.CheckParameter(SecretKey, nameof(SecretKey));
            Common.CheckParameter(ServiceHost, nameof(ServiceHost));
            Common.CheckParameter(XtcVersion, nameof(XtcVersion));
            try
            {
                var date = DateTime.UtcNow.ToString("yyyy-MM-dd");
                string service = ServiceHost.Split('.')[0]; //service 为产品名，通常为域名前缀
                var timestamp = DateTimeHelper.GetTimeStamp();
                string canonicalRequest = $"POST\n/\n\ncontent-type:application/json; charset=utf-8\nhost:{ServiceHost}\n\ncontent-type;host\n{CryptoHelper.SHA256Encrypt(paramJson).ToLower()}";
                string stringToSign = $"TC3-HMAC-SHA256\n{timestamp}\n{date}/{service}/tc3_request\n{CryptoHelper.SHA256Encrypt(canonicalRequest).ToLower()}";

                byte[] secretDate = Common.HmacSHA256(date, "TC3".Add(SecretKey).ToBytes());
                byte[] secretService = Common.HmacSHA256(service, secretDate);
                byte[] secretSigning = Common.HmacSHA256("tc3_request", secretService);
                byte[] signature = Common.HmacSHA256(stringToSign, secretSigning);
                string authorization = $"TC3-HMAC-SHA256 Credential={SecretID}/{date}/{service}/tc3_request, SignedHeaders=content-type;host, Signature={signature.ByteToHex().ToLower()}";

                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorization);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Host", ServiceHost);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-TC-Timestamp", timestamp);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-TC-Version", XtcVersion);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-TC-Region", XtcRegion);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-TC-Action", actionName);
                using var content = new StringContent(paramJson, Encoding.UTF8, "application/json");
                using var request = new HttpRequestMessage(HttpMethod.Post, $"https://{ServiceHost}".ToUrl());
                request.Content = content;
                var response = await httpClient.SendAsync(request);
                var te = response.StatusCode;
                var responseBody = await response.Content.ReadAsStringAsync();
                return (response.IsSuccessStatusCode ? true : false, responseBody);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
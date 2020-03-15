#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent
* 项目描述 ：常用云服务Api
* 类 名 称 ：TencentCosService
* 类 描 述 ：腾讯云CosApi服务
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi.Tencent
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/3/1 23:46:44
* 更新时间 ：2020/3/1 23:46:44
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using Kane.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Kane.CloudApi.Tencent
{
    /// <summary>
    /// 腾讯云CosApi服务
    /// </summary>
    public class TencentCosService
    {
        /// <summary>
        /// 在云API密钥上申请的标识身份的 SecretID，一个 SecretID 对应唯一的 SecretKey
        /// </summary>
        public string SecretID { private get; set; }
        /// <summary>
        /// SecretID 对应唯一的 SecretKey
        /// </summary>
        public string SecretKey { private get; set; }
        /// <summary>
        /// 期望的签名有效时长,单位【秒】,默认【180】秒
        /// </summary>
        public int Expires { get; set; } = 180;
        /// <summary>
        /// 哈希方法，固定字符串
        /// </summary>
        private const string SignAlgorithm = "sha1";
        private readonly static HttpClient client;
        static TencentCosService() => client = new HttpClient();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="secretID"></param>
        /// <param name="secretKey"></param>
        public TencentCosService(string secretID, string secretKey)
        {
            SecretID = secretID;
            SecretKey = secretKey;
        }

        /// <summary>
        /// 上传文件到指定位置
        /// </summary>
        /// <param name="uri">存放位置，包含文件名</param>
        /// <param name="content">文件内容</param>
        /// <param name="headers">请求头部信息</param>
        /// <returns></returns>
        public async Task<Uri> PutObjectAsync(string uri, Stream content, Dictionary<string, string> headers = null)
        {
            Common.ThrowIfNull(uri, nameof(uri));
            Common.ThrowIfNull(content, nameof(content));
            var req = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = new StreamContent(content)
            };
            if (headers?.Count > 0)
            {
                foreach (var header in headers)
                    req.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
            req.Headers.TryAddWithoutValidation("Authorization", BuildAuthorization(req));
            using var resp = await client.SendAsync(req);
            if (resp.StatusCode != HttpStatusCode.OK) RequestFailure(HttpMethod.Put, resp.StatusCode, await resp.Content.ReadAsStringAsync());
            return new Uri(uri);
        }

        /// <summary>
        /// 上传文件到指定位置
        /// </summary>
        /// <param name="baseUri">存储桶位置</param>
        /// <param name="directory">文件夹</param>
        /// <param name="name">文件名</param>
        /// <param name="content">文件内容</param>
        /// <param name="headers">请求头部信息</param>
        /// <returns></returns>
        public async Task<Uri> PutObjectAsync(string baseUri, string directory, string name, Stream content, Dictionary<string, string> headers = null)
        {
            Common.ThrowIfNull(baseUri, nameof(baseUri));
            Common.ThrowIfNull(name, nameof(name));
            var uri = new Uri(baseUri).Append(directory, name);
            return await PutObjectAsync(uri.ToString(), content, headers);
        }


        /// <summary>
        /// https://cloud.tencent.com/document/product/436/7778
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string BuildAuthorization(HttpRequestMessage req)
        {
#if !NET45
            var startTime = DateTimeOffset.UtcNow.AddSeconds(-5);
            var timestamp = $"{startTime.ToUnixTimeSeconds()};{startTime.AddSeconds(Expires).ToUnixTimeSeconds()}";
#else
            var startTime = DateTime.Now.AddSeconds(-5); ;
            var timestamp = $"{startTime.ToStamp()};{startTime.AddSeconds(Expires).ToStamp()}";
#endif
            var crypto = new CryptoHelper();
            var querys = req.RequestUri.GetQuerys();
            var headers = req.Headers.GetHeaders();
            var signKey = crypto.HmacSha1(timestamp, SecretKey).ToLower();
            var headerList = string.Join(";", headers.Select(k => k.Key));
            var httpHeaders = string.Join("&", headers.Select(x => $"{x.Key}={x.Value}"));
            var urlParamList = string.Join(";", querys.Select(k => k.Key));
            var httpParameters = string.Join("&", querys.Select(x => $"{x.Key}={x.Value}"));
            var httpString = $"{req.Method.ToString().ToLower()}\n{req.RequestUri.LocalPath}\n{httpParameters}\n{httpHeaders}\n";
            var hashedHttpString = crypto.Sha1(httpString).ToLower();
            var stringToSign = $"{SignAlgorithm}\n{timestamp}\n{hashedHttpString}\n";
            var signature = crypto.HmacSha1(stringToSign, signKey).ToLower();
            var keys = new Dictionary<string, string>()
            {
                { "q-sign-algorithm",   SignAlgorithm},
                { "q-ak",               SecretID },
                { "q-sign-time",        timestamp },
                { "q-key-time",         timestamp },
                { "q-header-list",      headerList },
                { "q-url-param-list",   urlParamList },
                { "q-signature",        signature }
            };
            return string.Join("&", keys.Select(k => $"{k.Key}={k.Value}")); ;
        }

        private void RequestFailure(HttpMethod method, HttpStatusCode statusCode, string content)
        {
            using var sr = new StringReader(content);
            var serializer = new XmlSerializer(typeof(TencentCosResult));
            var result = (TencentCosResult)serializer.Deserialize(sr);
            throw new Exception($"【{method.ToString()}】【{result?.Resource}】=> 响应码【{statusCode}】错误码【{result?.Code}】错误信息【{result?.Message}】");
        }
    }
}
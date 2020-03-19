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
* 更新时间 ：2020/3/16 23:46:44
* 版 本 号 ：v1.0.1.0
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
        /// 分块上传分块大小，默认10M，
        /// </summary>
        public int BlockSize { get; set; } = 10;
        /// <summary>
        /// 存储桶
        /// <remarks>See:存储桶名称由两部分组成：用户自定义字符串和系统生成数字串（APPID），两者以中划线“-”相连。例如examplebucket-1250000000，其中 examplebucket 为用户自定义字符串，1250000000 为系统生成数字串（APPID）</remarks>
        /// </summary>
        public string Bucket { get; set; }
        /// <summary>
        /// 地域信息，枚举值可参见 可用地域 文档，例如：ap-beijing、ap-hongkong、eu-frankfurt 等
        /// </summary>
        public string Region { get; set; } = "ap-guangzhou";
        /// <summary>
        /// 哈希方法，固定字符串
        /// </summary>
        private const string SignAlgorithm = "sha1";
        /// <summary>
        /// Host: 查询全部存储桶列表指定为service.cos.myqcloud.com，查询特定地域下的存储桶列表指定为cos.[Region].myqcloud.com。
        /// </summary>
        public const string Host = "https://service.cos.myqcloud.com/";
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
        /// 
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="region"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public Uri BaseUri(string bucket, string region, string path = "") => new Uri($"https://{bucket}.cos.{region}.myqcloud.com").Append(path);

        public Uri BaseUri(string path = "") => new Uri($"https://{Bucket}.cos.{Region}.myqcloud.com").Append(path);

        #region 获取所有所有存储空间列表 + GetBucketsAsync()
        /// <summary>
        /// 获取所有所有存储空间列表
        /// </summary>
        /// <remarks>See: https://cloud.tencent.com/document/product/436/8291 </remarks>
        /// <returns></returns>
        public async Task<TencentCosBuckets> GetBucketsAsync()
        {
            var req = new HttpRequestMessage(HttpMethod.Get, Host);
            using var resp = await SendAsync(req);
            if (!resp.IsSuccessStatusCode) ThrowFailure(HttpMethod.Get, resp.StatusCode, await resp.Content.ReadAsStringAsync());
            using Stream sr = await resp.Content.ReadAsStreamAsync();
            return sr.ToObject<TencentCosBuckets>();
        }
        #endregion

        #region 上传文件到指定位置 + PutObjectAsync(string uri, Stream content, Dictionary<string, string> headers = null)
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
            using var resp = await SendAsync(req, headers);
            if (resp.StatusCode != HttpStatusCode.OK) ThrowFailure(HttpMethod.Put, resp.StatusCode, await resp.Content.ReadAsStringAsync());
            return new Uri(uri);
        }
        #endregion

        #region 上传文件到指定位置 + PutObjectAsync(string baseUri, string directory, string name, Stream content, Dictionary<string, string> headers = null)
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
        #endregion

        #region 请求实现初始化分片上传 + MultipartUploadInit(string baseUri, string objectName)
        /// <summary>
        /// 请求实现初始化分片上传
        /// </summary>
        /// <param name="baseUri">存储桶位置</param>
        /// <param name="objectName">对象名称</param>
        /// <returns></returns>
        public async Task<TencentCosMUInit> MultipartUploadInit(string baseUri, string objectName)
        {
            var uri = new Uri(baseUri).Append($"{Uri.EscapeDataString(objectName)}?uploads");
            var req = new HttpRequestMessage(HttpMethod.Post, uri);
            using var resp = await SendAsync(req);
            if (!resp.IsSuccessStatusCode) ThrowFailure(HttpMethod.Post, resp.StatusCode, await resp.Content.ReadAsStringAsync());
            using Stream sr = await resp.Content.ReadAsStreamAsync();
            return sr.ToObject<TencentCosMUInit>();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri">由【key】【partNumber】【uploadId】组成的Uri</param>
        /// <param name="content"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<(bool Success,string Etag)> MultipartUpload(string uri, byte[] content, Dictionary<string, string> headers = null)
        {
            var req = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = new ByteArrayContent(content)
            };
            using var resp = await SendAsync(req, headers);
            if (resp.StatusCode == HttpStatusCode.OK) return (true, resp.Headers.ETag.Tag);
            else return (false, string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="key"></param>
        /// <param name="uploadID"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<TencentCosMUCompleteResult> MultipartUploadComplete(string baseUri, string key, string uploadID, TencentCosMUComplete data)
        {
            var uri = new Uri(baseUri).Append($"{Uri.EscapeDataString(key)}?uploadId={uploadID}");
            var req = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(data.ToXml(true,true))
            };
            using var resp = await SendAsync(req);
            if (!resp.IsSuccessStatusCode) ThrowFailure(HttpMethod.Post, resp.StatusCode, await resp.Content.ReadAsStringAsync());
            using Stream sr = await resp.Content.ReadAsStreamAsync();
            return sr.ToObject<TencentCosMUCompleteResult>();
        }

        public async Task<(bool,string)> MultipartUploadAsnyc(Stream stream, string objectName, string bucket, string region, string path)
        {
            objectName.ThrowIfNull(nameof(objectName));
            bucket.ThrowIfNull(nameof(bucket));
            region.ThrowIfNull(nameof(region));

            var baseUri = BaseUri(bucket, region, path);
            var init = await MultipartUploadInit(baseUri.ToString(), objectName);
            if (init.Key.IsValuable() && init.UploadID.IsValuable())//分块上传初始化成功
            {
                int blockSize = BlockSize * 1024 * 1024;
                var blockCount = stream.Length / blockSize;
                if ((blockSize * blockCount) < stream.Length) blockCount += 1;
                stream.Seek(0, SeekOrigin.Begin);
                var complete = new TencentCosMUComplete();
                for (int i = 1; i <= blockCount; i++)
                {
                    long bufferSize = i == blockCount ? (stream.Length - blockSize * (i - 1)) : blockSize;
                    byte[] buffer = new byte[bufferSize];
                    stream.Read(buffer, 0, (int)bufferSize);
                    var result = await MultipartUpload(BaseUri(bucket,region).Append($"{init.Key.UrlEncode()}?partNumber={i}&uploadId={init.UploadID}").ToString(), buffer);
                    complete.Part.Add(new Part { PartNumber = i, ETag = result.Etag });
                }
                if (complete.Part.Count == blockCount)
                {
                    var result = await MultipartUploadComplete(BaseUri(bucket, region).ToString(), init.Key, init.UploadID, complete);
                    return (true, result.Location);
                }
            }
            return (false, string.Empty);


            //var te = MultipartUploadInit("https://mp-1256147466.cos.ap-guangzhou.myqcloud.com/mall", "Git.exe").Result;
            //if (te.Key.IsValuable() && te.UploadID.IsValuable())
            //{
            //    int CHUNK_SIZE = 10 * 1024 * 1024;//10M
            //    using var steam = new FileStream("D:\\Git.exe", FileMode.Open, FileAccess.Read);
            //    long FileLength = steam.Length;
            //    List<long> PkgList = new List<long>();
            //    for (long iIdx = 0; iIdx < FileLength / Convert.ToInt64(CHUNK_SIZE); iIdx++)
            //    {
            //        PkgList.Add(Convert.ToInt64(CHUNK_SIZE));
            //    }
            //    long s = FileLength % CHUNK_SIZE;
            //    if (s != 0)
            //    {
            //        PkgList.Add(s);
            //    }
            //    var resultdata = new TencentCosMUComplete();
            //    for (int iPkgIdx = 0; iPkgIdx < PkgList.Count; iPkgIdx++)
            //    {
            //        long bufferSize = PkgList[iPkgIdx];
            //        byte[] buffer = new byte[bufferSize];
            //        int bytesRead = steam.Read(buffer, 0, (int)bufferSize);
            //        var baseUri = $"https://mp-1256147466.cos.ap-guangzhou.myqcloud.com/{te.Key.UrlEncode()}?partNumber={iPkgIdx + 1}&uploadId={te.UploadID}";
            //        var result = MultipartUpload(baseUri, buffer).Result;
            //        resultdata.Part.Add(new Part { PartNumber = iPkgIdx+1,ETag = result.Etag });
            //    }

            //   var tt = MultipartUploadComplete("https://mp-1256147466.cos.ap-guangzhou.myqcloud.com/", te.Key, te.UploadID, resultdata).Result;
            //}

            

        }


        


        #region 发送请求共有方法 + SendAsync(HttpRequestMessage req, Dictionary<string, string> headers = null)
        /// <summary>
        /// 发送请求共有方法
        /// </summary>
        /// <param name="req">Http请求消息</param>
        /// <param name="headers">HttpHeader集合</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage req, Dictionary<string, string> headers = null)
        {
            if (headers?.Count > 0)
            {
                foreach (var header in headers) req.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
            req.Headers.TryAddWithoutValidation("Authorization", BuildAuthorization(req));
            return await client.SendAsync(req);
        }
        #endregion

        #region Cos请求签名 + BuildAuthorization(HttpRequestMessage req)
        /// <summary>
        /// Cos请求签名
        /// https://cloud.tencent.com/document/product/436/7778
        /// </summary>
        /// <param name="req">Http请求消息</param>
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
        #endregion

        #region 请求失败时抛出异常 + ThrowFailure(HttpMethod method, HttpStatusCode statusCode, string content)
        /// <summary>
        /// 请求失败时抛出异常
        /// </summary>
        /// <param name="method">请求的方法</param>
        /// <param name="statusCode">返回的状态码</param>
        /// <param name="content">返回的内容</param>
        private void ThrowFailure(HttpMethod method, HttpStatusCode statusCode, string content)
        {
            using var sr = new StringReader(content);
            var result = sr.ToObject<TencentCosError>();
            throw new Exception($"【{method}】【{result?.Resource}】=> 响应码【{statusCode}】错误码【{result?.Code}】错误信息【{result?.Message}】");
        }
        #endregion
    }
}
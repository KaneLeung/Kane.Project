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
* 更新时间 ：2020/3/21 15:14:44
* 版 本 号 ：v1.0.4.0
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
        #region 公有属性
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
        #endregion

        #region 私有成员
        /// <summary>
        /// 哈希方法，固定字符串
        /// </summary>
        private const string SignAlgorithm = "sha1";
        /// <summary>
        /// Host: 查询全部存储桶列表指定为service.cos.myqcloud.com，查询特定地域下的存储桶列表指定为cos.[Region].myqcloud.com。
        /// </summary>
        public const string Host = "https://service.cos.myqcloud.com/";
        private readonly static HttpClient client;
        #endregion

        #region 静态构造函数，保证只运行一次
        /// <summary>
        /// 静态构造函数，保证只运行一次
        /// </summary>
        static TencentCosService() => client = new HttpClient();
        #endregion

        #region 无参构造函数 + TencentCosService()
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public TencentCosService()
        {
        }
        #endregion

        #region 带参数的构造函数 + TencentCosService(string secretID, string secretKey)
        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="secretID">在云API密钥上申请的标识身份的 SecretID，一个 SecretID 对应唯一的 SecretKey</param>
        /// <param name="secretKey">SecretID 对应唯一的 SecretKey</param>
        public TencentCosService(string secretID, string secretKey)
        {
            SecretID = secretID;
            SecretKey = secretKey;
        }
        #endregion

        #region 带参数的构造函数 + TencentCosService(string secretID, string secretKey, string bucket, string region)
        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="secretID">在云API密钥上申请的标识身份的 SecretID，一个 SecretID 对应唯一的 SecretKey</param>
        /// <param name="secretKey">SecretID 对应唯一的 SecretKey</param>
        /// <param name="bucket">存储桶</param>
        /// <param name="region">区域</param>
        public TencentCosService(string secretID, string secretKey, string bucket, string region) : this(secretID, secretKey)
        {
            Bucket = bucket;
            Region = region;
        }
        #endregion

        #region 获取存储桶服务器地址，可设置路径 + BaseUri(string bucket, string region, string path = "")
        /// <summary>
        /// 获取存储桶服务器地址，可设置路径
        /// </summary>
        /// <param name="bucket">存储桶</param>
        /// <param name="region">区域</param>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public Uri BaseUri(string bucket, string region, string path = "")
        {
            bucket.ThrowIfNull(nameof(bucket));
            region.ThrowIfNull(nameof(region));
            return new Uri($"https://{bucket}.cos.{region}.myqcloud.com").Append(path);
        }
        #endregion

        #region 获取存储桶服务器地址，可设置路径 + BaseUri(string path = "")
        /// <summary>
        /// 获取存储桶服务器地址，可设置路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Uri BaseUri(string path = "")
        {
            Bucket.ThrowIfNull(nameof(Bucket));
            Region.ThrowIfNull(nameof(Region));
            return new Uri($"https://{Bucket}.cos.{Region}.myqcloud.com").Append(path);
        }
        #endregion

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

        #region 上传文件到Cos + PutObjectAsync(Stream content, string objectName, string path = "", Dictionary<string, string> headers = null)
        /// <summary>
        /// 上传文件到Cos
        /// </summary>
        /// <param name="content">上传数据流</param>
        /// <param name="objectName">Object 的名称，可包含路径，也可不包含路径</param>
        /// <param name="path">路径</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<(bool success, Uri uri)> PutObjectAsync(Stream content, string objectName, string path = "", Dictionary<string, string> headers = null)
        {
            var uri = BaseUri(path).Append(objectName.ToEscape());
            return await PutObjectAsync(content, uri, headers);
        }
        #endregion

        #region 上传文件到Cos + PutObjectAsync(Stream content, string objectName, string bucket, string region, string path = "", Dictionary<string, string> headers = null)
        /// <summary>
        /// 上传文件到Cos
        /// </summary>
        /// <param name="content">上传数据流</param>
        /// <param name="objectName">Object 的名称，可包含路径，也可不包含路径</param>
        /// <param name="bucket">存储桶</param>
        /// <param name="region">区域</param>
        /// <param name="path">路径</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<(bool success, Uri uri)> PutObjectAsync(Stream content, string objectName, string bucket, string region, string path = "", Dictionary<string, string> headers = null)
        {
            var uri = BaseUri(bucket, region, path).Append(objectName.ToEscape());
            return await PutObjectAsync(content, uri, headers);
        }
        #endregion

        #region 上传文件到Cos + PutObjectAsync(byte[] content, string objectName, string bucket, string region, string path = "", Dictionary<string, string> headers = null)
        /// <summary>
        /// 上传文件到Cos
        /// </summary>
        /// <param name="content">上传数据字节流</param>
        /// <param name="objectName">Object 的名称，可包含路径，也可不包含路径</param>
        /// <param name="bucket">存储桶</param>
        /// <param name="region">区域</param>
        /// <param name="path">路径</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<(bool success, Uri uri)> PutObjectAsync(byte[] content, string objectName, string bucket, string region, string path = "", Dictionary<string, string> headers = null)
        {
            var uri = BaseUri(bucket, region, path).Append(objectName.ToEscape());
            return await PutObjectAsync(content, uri, headers);
        }
        #endregion

        #region 上传文件到Cos + PutObjectAsync(byte[] content, string objectName, string path = "", Dictionary<string, string> headers = null)
        /// <summary>
        /// 上传文件到Cos
        /// </summary>
        /// <param name="content">上传数据字节流</param>
        /// <param name="objectName">Object 的名称，可包含路径，也可不包含路径</param>
        /// <param name="path">路径</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<(bool success, Uri uri)> PutObjectAsync(byte[] content, string objectName, string path = "", Dictionary<string, string> headers = null)
        {
            var uri = BaseUri(path).Append(objectName.ToEscape());
            return await PutObjectAsync(content, uri, headers);
        }
        #endregion

        #region 上传文件到Cos + PutObjectAsync(string file, string bucket, string region, string objectName = "", string path = "", Dictionary<string, string> headers = null)
        /// <summary>
        /// 上传文件到Cos
        /// </summary>
        /// <param name="file">文件完整路径</param>
        /// <param name="bucket">存储桶</param>
        /// <param name="region">区域</param>
        /// <param name="objectName">Object 的名称，可包含路径，也可不包含路径，为空时，从文件路径获取</param>
        /// <param name="path">路径</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<(bool success, Uri uri)> PutObjectAsync(string file, string bucket, string region, string objectName = "", string path = "", Dictionary<string, string> headers = null)
        {
            file.ThrowIfNotExist();
            if (objectName.IsNullOrEmpty()) objectName = Path.GetFileName(file);
            return await PutObjectAsync(await file.FileToBytesAsync(), objectName, bucket, region, path, headers);
        }
        #endregion

        #region 上传文件到Cos + PutObjectAsync(string file, string objectName = "", string path = "", Dictionary<string, string> headers = null)
        /// <summary>
        /// 上传文件到Cos
        /// </summary>
        /// <param name="file">文件完整路径</param>
        /// <param name="objectName">Object 的名称，可包含路径，也可不包含路径，为空时，从文件路径获取</param>
        /// <param name="path">路径</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<(bool success, Uri uri)> PutObjectAsync(string file, string objectName = "", string path = "", Dictionary<string, string> headers = null)
        {
            file.ThrowIfNotExist();
            if (objectName.IsNullOrEmpty()) objectName = Path.GetFileName(file);
            return await PutObjectAsync(await file.FileToBytesAsync(), objectName, path, headers);
        }
        #endregion

        #region 上传文件到Cos + PutObjectAsync(Stream content,Uri uri, Dictionary<string, string> headers = null)
        /// <summary>
        /// 上传文件到Cos
        /// </summary>
        /// <param name="content">上传数据流</param>
        /// <param name="uri">上传的路径</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<(bool success, Uri uri)> PutObjectAsync(Stream content, Uri uri, Dictionary<string, string> headers = null)
        {
            content.ThrowIfNull(nameof(content));
            uri.ThrowIfNull(nameof(uri));
            var req = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = new StreamContent(content)
            };
            using var resp = await SendAsync(req, headers);
            if (resp.StatusCode != HttpStatusCode.OK) ThrowFailure(HttpMethod.Put, resp.StatusCode, await resp.Content.ReadAsStringAsync());
            return (true, uri);
        }
        #endregion

        #region 上传文件到Cos + PutObjectAsync(byte[] content, Uri uri, Dictionary<string, string> headers = null)
        /// <summary>
        /// 上传文件到Cos
        /// </summary>
        /// <param name="content">上传数据字节流</param>
        /// <param name="uri">上传的路径</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<(bool success, Uri uri)> PutObjectAsync(byte[] content, Uri uri, Dictionary<string, string> headers = null)
        {
            content.ThrowIfNull(nameof(content));
            uri.ThrowIfNull(nameof(uri));
            var req = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = new ByteArrayContent(content)
            };
            using var resp = await SendAsync(req, headers);
            if (resp.StatusCode != HttpStatusCode.OK) ThrowFailure(HttpMethod.Put, resp.StatusCode, await resp.Content.ReadAsStringAsync());
            return (true, uri);
        }
        #endregion

        #region 请求实现初始化分片上传 + MultipartUploadInit(string objectName, string bucket, string region, string path = "", Dictionary<string, string> headers = null)
        /// <summary>
        /// 请求实现初始化分片上传
        /// </summary>
        /// <param name="objectName">Object 的名称，可包含路径，也可不包含路径</param>
        /// <param name="bucket">存储桶</param>
        /// <param name="region">区域</param>
        /// <param name="path">路径</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<TencentCosMUInit> MultipartUploadInit(string objectName, string bucket, string region, string path = "", Dictionary<string, string> headers = null)
        {
            var uri = BaseUri(bucket, region, path).Append($"{objectName.ToEscape()}?uploads");
            var req = new HttpRequestMessage(HttpMethod.Post, uri);
            using var resp = await SendAsync(req, headers);
            if (!resp.IsSuccessStatusCode) ThrowFailure(HttpMethod.Post, resp.StatusCode, await resp.Content.ReadAsStringAsync());
            using Stream sr = await resp.Content.ReadAsStreamAsync();
            return sr.ToObject<TencentCosMUInit>();
        }
        #endregion

        #region 请求实现初始化分片上传 + MultipartUploadInit(string objectName, string path = "", Dictionary<string, string> headers = null)
        /// <summary>
        /// 请求实现初始化分片上传
        /// </summary>
        /// <param name="objectName">Object 的名称，可包含路径，也可不包含路径</param>
        /// <param name="path">路径</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<TencentCosMUInit> MultipartUploadInit(string objectName, string path = "", Dictionary<string, string> headers = null)
            => await MultipartUploadInit(objectName, Bucket, Region, path, headers);
        #endregion

        #region 分块上传数据到Cos + MultipartUpload(Uri uri, byte[] content, Dictionary<string, string> headers = null)
        /// <summary>
        /// 分块上传数据到Cos
        /// </summary>
        /// <param name="uri">由【key】【partNumber】【uploadId】组成的Uri</param>
        /// <param name="content">上传的数据【Byte[]】</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<(bool success, string etag)> MultipartUpload(Uri uri, byte[] content, Dictionary<string, string> headers = null)
        {
            var req = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = new ByteArrayContent(content)
            };
            using var resp = await SendAsync(req, headers);
            if (resp.StatusCode == HttpStatusCode.OK) return (true, resp.Headers.ETag.Tag);
            else return (false, string.Empty);
        }
        #endregion

        #region 分块上传已完成请求 + MultipartUploadComplete(string key, string uploadID, string bucket, string region, TencentCosMUComplete data, Dictionary<string, string> headers = null)
        /// <summary>
        /// 分块上传已完成请求
        /// </summary>
        /// <param name="key">Object 的名称，包含路径</param>
        /// <param name="uploadID">上传中使用的ID</param>
        /// <param name="bucket">存储桶</param>
        /// <param name="region">区域</param>
        /// <param name="data">提交的分块信息数据</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<TencentCosMUCompleteResult> MultipartUploadComplete(string key, string uploadID, string bucket, string region, TencentCosMUComplete data, Dictionary<string, string> headers = null)
        {
            var uri = BaseUri(bucket, region).Append($"{key.ToEscape()}?uploadId={uploadID}");
            var req = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(data.ToXml(true, true))
            };
            using var resp = await SendAsync(req, headers);
            if (!resp.IsSuccessStatusCode) ThrowFailure(HttpMethod.Post, resp.StatusCode, await resp.Content.ReadAsStringAsync());
            using Stream sr = await resp.Content.ReadAsStreamAsync();
            return sr.ToObject<TencentCosMUCompleteResult>();
        }
        #endregion

        #region 分块上传已完成请求 + MultipartUploadComplete(string key, string uploadID, TencentCosMUComplete data, Dictionary<string, string> headers = null)
        /// <summary>
        /// 分块上传已完成请求
        /// </summary>
        /// <param name="key">Object 的名称，包含路径</param>
        /// <param name="uploadID">上传中使用的ID</param>
        /// <param name="data">提交的分块信息数据</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<TencentCosMUCompleteResult> MultipartUploadComplete(string key, string uploadID, TencentCosMUComplete data, Dictionary<string, string> headers = null)
            => await MultipartUploadComplete(key, uploadID, Bucket, Region, data, headers);
        #endregion

        #region 分块的方式上传到COS + MultipartUploadAsnyc(Stream content, string objectName, string bucket, string region, string path = "", Dictionary<string, string> headers = null)
        /// <summary>
        /// 分块的方式上传到COS,默认大小10MB
        /// <para>每个分块大小为1MB - 5GB</para>
        /// </summary>
        /// <param name="content">要上传的数据流</param>
        /// <param name="objectName">Object 的名称，可包含路径，也可不包含路径</param>
        /// <param name="bucket">存储桶</param>
        /// <param name="region">区域</param>
        /// <param name="path">路径</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<(bool success, string uri)> MultipartUploadAsnyc(Stream content, string objectName, string bucket, string region, string path = "", Dictionary<string, string> headers = null)
        {
            objectName.ThrowIfNull(nameof(objectName));
            bucket.ThrowIfNull(nameof(bucket));
            region.ThrowIfNull(nameof(region));

            var init = await MultipartUploadInit(objectName, bucket, region, path, headers);
            if (init.Key.IsValuable() && init.UploadID.IsValuable())//分块上传初始化成功
            {
                int blockSize = BlockSize * 1024 * 1024;
                content.Seek(0, SeekOrigin.Begin);
                var complete = new TencentCosMUComplete();
                int index = 0;
                bool flag = true;//上传正常标志
                while (content.Length > content.Position && flag)
                {
                    long bufferSize = (content.Length - content.Position) > blockSize ? blockSize : content.Length - content.Position;
                    byte[] buffer = new byte[bufferSize];
                    content.Read(buffer, 0, (int)bufferSize);
                    var result = await MultipartUpload(BaseUri(bucket, region).Append($"{init.Key.ToEscape()}?partNumber={++index}&uploadId={init.UploadID}"), buffer, headers);
                    if (result.success) complete.Part.Add(new Part { PartNumber = index, ETag = result.etag });
                    else
                    {
                        flag = false;
                        await MultipartUploadAbort(init.Key, init.UploadID, bucket, region, headers);
                        return (flag, string.Empty);
                    }
                }
                if (complete.Part.Count == index)
                {
                    var result = await MultipartUploadComplete(init.Key, init.UploadID, bucket, region, complete, headers);
                    return (true, result.Location);
                }
            }
            return (false, string.Empty);
        }
        #endregion

        #region 分块的方式上传到COS + MultipartUploadAsnyc(Stream content, string objectName, string path = "", Dictionary<string, string> headers = null)
        /// <summary>
        /// 分块的方式上传到COS,默认大小10MB
        /// <para>每个分块大小为1MB - 5GB</para>
        /// </summary>
        /// <param name="content">要上传的数据流</param>
        /// <param name="objectName">Object 的名称，可包含路径，也可不包含路径</param>
        /// <param name="path">路径</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<(bool success, string uri)> MultipartUploadAsnyc(Stream content, string objectName, string path = "", Dictionary<string, string> headers = null)
            => await MultipartUploadAsnyc(content, objectName, Bucket, Region, path, headers);
        #endregion

        #region 分块的方式上传到COS + MultipartUploadAsnyc(byte[] content, string objectName, string bucket, string region, string path = "", Dictionary<string, string> headers = null)
        /// <summary>
        /// 分块的方式上传到COS,默认大小10MB
        /// <para>每个分块大小为1MB - 5GB</para>
        /// </summary>
        /// <param name="content">要上传的字节数据</param>
        /// <param name="objectName">Object 的名称，可包含路径，也可不包含路径</param>
        /// <param name="bucket">存储桶</param>
        /// <param name="region">区域</param>
        /// <param name="path">路径</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<(bool success, string uri)> MultipartUploadAsnyc(byte[] content, string objectName, string bucket, string region, string path = "", Dictionary<string, string> headers = null)
        {
            objectName.ThrowIfNull(nameof(objectName));
            bucket.ThrowIfNull(nameof(bucket));
            region.ThrowIfNull(nameof(region));

            var init = await MultipartUploadInit(objectName, bucket, region, path, headers);
            if (init.Key.IsValuable() && init.UploadID.IsValuable())//分块上传初始化成功
            {
                int blockSize = BlockSize * 1024 * 1024;
                var complete = new TencentCosMUComplete();
                long position = 0;
                int index = 0;
                bool flag = true;//上传正常标志
                while (content.Length > position && flag)
                {
                    long bufferSize = (content.Length - position) > blockSize ? blockSize : content.Length - position;
                    byte[] buffer = new byte[bufferSize];
                    Array.Copy(content, position, buffer, 0, bufferSize);
                    position += bufferSize;
                    var result = await MultipartUpload(BaseUri(bucket, region).Append($"{init.Key.ToEscape()}?partNumber={++index}&uploadId={init.UploadID}"), buffer, headers);
                    if (result.success) complete.Part.Add(new Part { PartNumber = index, ETag = result.etag });
                    else
                    {
                        flag = false;
                        await MultipartUploadAbort(init.Key, init.UploadID, bucket, region, headers);
                        return (flag, string.Empty);
                    }
                }
                if (complete.Part.Count == index)
                {
                    var result = await MultipartUploadComplete(init.Key, init.UploadID, bucket, region, complete, headers);
                    return (true, result.Location);
                }
            }
            return (false, string.Empty);
        }
        #endregion

        #region 分块的方式上传到COS + MultipartUploadAsnyc(byte[] content, string objectName, string path = "", Dictionary<string, string> headers = null)
        /// <summary>
        /// 分块的方式上传到COS,默认大小10MB
        /// <para>每个分块大小为1MB - 5GB</para>
        /// </summary>
        /// <param name="content">要上传的字节数据</param>
        /// <param name="objectName">Object 的名称，可包含路径，也可不包含路径</param>
        /// <param name="path">路径</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<(bool success, string uri)> MultipartUploadAsnyc(byte[] content, string objectName, string path = "", Dictionary<string, string> headers = null)
            => await MultipartUploadAsnyc(content, objectName, Bucket, Region, path, headers);
        #endregion

        #region 中止一个分块上传并删除已上传的块 + MultipartUploadAbort(string key, string uploadID, string bucket, string region, Dictionary<string, string> headers = null)
        /// <summary>
        /// 中止一个分块上传并删除已上传的块
        /// </summary>
        /// <param name="key">Object 的名称，包含路径</param>
        /// <param name="uploadID">上传中使用的ID</param>
        /// <param name="bucket">存储桶</param>
        /// <param name="region">区域</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<bool> MultipartUploadAbort(string key, string uploadID, string bucket, string region, Dictionary<string, string> headers = null)
        {
            var uri = BaseUri(bucket, region).Append($"{key.ToEscape()}?uploadId={uploadID}");
            var req = new HttpRequestMessage(HttpMethod.Delete, uri);
            using var resp = await SendAsync(req, headers);
            if (resp.StatusCode == HttpStatusCode.NoContent) return true;
            else return false;
        }
        #endregion

        #region 中止一个分块上传并删除已上传的块 + MultipartUploadAbort(string key, string uploadID, Dictionary<string, string> headers = null)
        /// <summary>
        /// 中止一个分块上传并删除已上传的块
        /// </summary>
        /// <param name="key">Object 的名称，包含路径</param>
        /// <param name="uploadID">上传中使用的ID</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<bool> MultipartUploadAbort(string key, string uploadID, Dictionary<string, string> headers = null)
            => await MultipartUploadAbort(key, uploadID, Bucket, Region, headers);
        #endregion

        #region 查询指定 UploadID 所属的所有已上传成功的分块 + MultipartUploadPartsList(string key, string uploadID, string bucket, string region, Dictionary<string, string> headers = null)
        /// <summary>
        /// 查询指定 UploadID 所属的所有已上传成功的分块
        /// </summary>
        /// <param name="key">Object 的名称，包含路径</param>
        /// <param name="uploadID">上传中使用的ID</param>
        /// <param name="bucket">存储桶</param>
        /// <param name="region">区域</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<TencentCosMUPartsResult> MultipartUploadPartsList(string key, string uploadID, string bucket, string region, Dictionary<string, string> headers = null)
        {
            var uri = BaseUri(bucket, region).Append($"{key.ToEscape()}?uploadId={uploadID}");
            var req = new HttpRequestMessage(HttpMethod.Get, uri);
            using var resp = await SendAsync(req, headers);
            if (!resp.IsSuccessStatusCode) ThrowFailure(HttpMethod.Get, resp.StatusCode, await resp.Content.ReadAsStringAsync());
            using Stream sr = await resp.Content.ReadAsStreamAsync();
            return sr.ToObject<TencentCosMUPartsResult>();
        }
        #endregion

        #region 查询指定 UploadID 所属的所有已上传成功的分块 + MultipartUploadPartsList(string key, string uploadID, Dictionary<string, string> headers = null)
        /// <summary>
        /// 查询指定 UploadID 所属的所有已上传成功的分块
        /// </summary>
        /// <param name="key">Object 的名称，包含路径</param>
        /// <param name="uploadID">上传中使用的ID</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<TencentCosMUPartsResult> MultipartUploadPartsList(string key, string uploadID, Dictionary<string, string> headers = null)
            => await MultipartUploadPartsList(key, uploadID, Bucket, Region, headers);
        #endregion

        #region 根据上传数据大小，自动使用单次上传或分块上传，可设置单文件上传最大值，默认【10Mb】 + AutoUploadAsync(...)
        /// <summary>
        /// 根据上传数据大小，自动使用单次上传或分块上传，可设置单文件上传最大值，默认【10Mb】
        /// </summary>
        /// <param name="content">要上传的字节数据</param>
        /// <param name="objectName">Object 的名称，可包含路径，也可不包含路径</param>
        /// <param name="bucket">存储桶</param>
        /// <param name="region">区域</param>
        /// <param name="path">路径</param>
        /// <param name="maxSize">单文件上传最大值，默认【10Mb】</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<(bool, Uri)> AutoUploadAsync(byte[] content, string objectName, string bucket, string region, string path = "", int maxSize = 10, Dictionary<string, string> headers = null)
        {
            var uri = BaseUri(bucket, region, path).Append(objectName.ToEscape());
            if (content.Length <= maxSize * 1024 * 1024) return await PutObjectAsync(content, uri, headers);
            else
            {
                var result = await MultipartUploadAsnyc(content, objectName, bucket, region, path, headers);
                return (result.success, new Uri($"https://{result.uri}"));
            }
        }
        #endregion

        #region 根据上传数据大小，自动使用单次上传或分块上传，可设置单文件上传最大值，默认【10Mb】 + AutoUploadAsync(...)
        /// <summary>
        /// 根据上传数据大小，自动使用单次上传或分块上传，可设置单文件上传最大值，默认【10Mb】
        /// </summary>
        /// <param name="content">要上传的字节数据</param>
        /// <param name="objectName">Object 的名称，可包含路径，也可不包含路径</param>
        /// <param name="path">路径</param>
        /// <param name="maxSize">单文件上传最大值，默认【10Mb】</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<(bool, Uri)> AutoUploadAsync(byte[] content, string objectName, string path = "", int maxSize = 10, Dictionary<string, string> headers = null)
            => await AutoUploadAsync(content, objectName, Bucket, Region, path, maxSize, headers);
        #endregion

        #region 根据上传数据大小，自动使用单次上传或分块上传，可设置单文件上传最大值，默认【10Mb】 + AutoUploadAsync(...)
        /// <summary>
        /// 根据上传数据大小，自动使用单次上传或分块上传，可设置单文件上传最大值，默认【10Mb】
        /// </summary>
        /// <param name="content">要上传的数据流</param>
        /// <param name="objectName">Object 的名称，可包含路径，也可不包含路径</param>
        /// <param name="bucket">存储桶</param>
        /// <param name="region">区域</param>
        /// <param name="path">路径</param>
        /// <param name="maxSize">单文件上传最大值，默认【10Mb】</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<(bool, Uri)> AutoUploadAsync(Stream content, string objectName, string bucket, string region, string path = "", int maxSize = 10, Dictionary<string, string> headers = null)
        {
            var uri = BaseUri(bucket, region, path).Append(objectName.ToEscape());
            if (content.Length <= maxSize * 1024 * 1024) return await PutObjectAsync(content, uri, headers);
            else
            {
                var result = await MultipartUploadAsnyc(content, objectName, bucket, region, path, headers);
                return (result.success, new Uri($"https://{result.uri}"));
            }
        }
        #endregion

        #region 根据上传数据大小，自动使用单次上传或分块上传，可设置单文件上传最大值，默认【10Mb】 + AutoUploadAsync(...)
        /// <summary>
        /// 根据上传数据大小，自动使用单次上传或分块上传，可设置单文件上传最大值，默认【10Mb】
        /// </summary>
        /// <param name="content">要上传的数据流</param>
        /// <param name="objectName">Object 的名称，可包含路径，也可不包含路径</param>
        /// <param name="path">路径</param>
        /// <param name="maxSize">单文件上传最大值，默认【10Mb】</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<(bool, Uri)> AutoUploadAsync(Stream content, string objectName, string path = "", int maxSize = 10, Dictionary<string, string> headers = null)
            => await AutoUploadAsync(content, objectName, Bucket, Region, path, maxSize, headers);
        #endregion

        #region 获取存储桶中的对象（Object）下载至本地 + GetObjectAsync(string objectName, string bucket, string region, string path = "", Dictionary<string, string> headers = null)
        /// <summary>
        /// 获取存储桶中的对象（Object）下载至本地，返回Byte[]
        /// </summary>
        /// <param name="objectName">Object 的名称，可包含路径，也可不包含路径</param>
        /// <param name="bucket">存储桶</param>
        /// <param name="region">区域</param>
        /// <param name="path">路径</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<byte[]> GetObjectAsync(string objectName, string bucket, string region, string path = "", Dictionary<string, string> headers = null)
        {
            var uri = BaseUri(bucket, region, path).Append(objectName.ToEscape());
            var req = new HttpRequestMessage(HttpMethod.Get, uri);
            using var resp = await SendAsync(req, headers);
            if (!resp.IsSuccessStatusCode) ThrowFailure(HttpMethod.Get, resp.StatusCode, await resp.Content.ReadAsStringAsync());
            return await resp.Content.ReadAsByteArrayAsync();
        }
        #endregion

        #region 获取存储桶中的对象（Object）下载至本地 + GetObjectAsync(string objectName, string path = "", Dictionary<string, string> headers = null)
        /// <summary>
        /// 获取存储桶中的对象（Object）下载至本地，返回Byte[]
        /// </summary>
        /// <param name="objectName">Object 的名称，可包含路径，也可不包含路径</param>
        /// <param name="path">路径</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<byte[]> GetObjectAsync(string objectName, string path = "", Dictionary<string, string> headers = null)
            => await GetObjectAsync(objectName, Bucket, Region, path, headers);
        #endregion

        #region 根据【prefix】和【delimiter】获取存储桶的目录内容 + GetObjectListAsync(string bucket, string region, string prefix = "", string delimiter = "", Dictionary<string, string> headers = null)
        /// <summary>
        /// 根据【prefix】和【delimiter】获取存储桶的目录内容
        /// </summary>
        /// <param name="bucket">存储桶</param>
        /// <param name="region">区域</param>
        /// <param name="prefix">对象键匹配前缀，限定响应中只包含指定前缀的对象键</param>
        /// <param name="delimiter">一个字符的分隔符，用于对对象键进行分组。</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<TencentCosObjects> GetObjectListAsync(string bucket, string region, string prefix = "", string delimiter = "", Dictionary<string, string> headers = null)
        {
            var queries = prefix.IsValuable() ? $"prefix={prefix.ToEscape()}" : string.Empty;
            if (delimiter.IsValuable()) queries += queries.IsValuable() ? $"&delimiter={delimiter}" : $"delimiter={delimiter.ToEscape()}";
            var uri = queries.IsValuable() ? BaseUri(bucket, region).Append($"?{queries}") : BaseUri(bucket, region);
            var req = new HttpRequestMessage(HttpMethod.Get, uri);
            using var resp = await SendAsync(req, headers);
            if (!resp.IsSuccessStatusCode) ThrowFailure(HttpMethod.Get, resp.StatusCode, await resp.Content.ReadAsStringAsync());
            using Stream sr = await resp.Content.ReadAsStreamAsync();
            return sr.ToObject<TencentCosObjects>();
        }
        #endregion

        #region 根据【prefix】和【delimiter】获取存储桶的目录内容 + GetObjectListAsync(string prefix = "", string delimiter = "", Dictionary<string, string> headers = null)
        /// <summary>
        /// 根据【prefix】和【delimiter】获取存储桶的目录内容
        /// </summary>
        /// <param name="prefix">对象键匹配前缀，限定响应中只包含指定前缀的对象键</param>
        /// <param name="delimiter">一个字符的分隔符，用于对对象键进行分组。</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<TencentCosObjects> GetObjectListAsync(string prefix = "", string delimiter = "", Dictionary<string, string> headers = null)
            => await GetObjectListAsync(Bucket, Region, prefix, delimiter, headers);
        #endregion

        #region 存储桶根的目录内容 + GetRootListAsync(string bucket, string region, Dictionary<string, string> headers = null)
        /// <summary>
        /// 存储桶根的目录内容
        /// </summary>
        /// <param name="bucket">存储桶</param>
        /// <param name="region">区域</param>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<TencentCosObjects> GetRootListAsync(string bucket, string region, Dictionary<string, string> headers = null)
            => await GetObjectListAsync(bucket, region, string.Empty, "/", headers);
        #endregion

        #region 存储桶根的目录内容 + GetRootListAsync(Dictionary<string, string> headers = null)
        /// <summary>
        /// 存储桶根的目录内容
        /// </summary>
        /// <param name="headers">其他请求头部参数</param>
        /// <returns></returns>
        public async Task<TencentCosObjects> GetRootListAsync(Dictionary<string, string> headers = null)
            => await GetObjectListAsync(Bucket, Region, string.Empty, "/", headers);
        #endregion

        #region 异步发送请求共有方法 + SendAsync(HttpRequestMessage req, Dictionary<string, string> headers = null)
        /// <summary>
        /// 异步发送请求共有方法
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
            var httpHeaders = string.Join("&", headers.Select(x => $"{x.Key}={x.Value.ToEscape()}"));
            var urlParamList = string.Join(";", querys.Select(k => k.Key));
            var httpParameters = string.Join("&", querys.Select(x => $"{x.Key}={x.Value.ToEscape()}"));
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
            return string.Join("&", keys.Select(k => $"{k.Key}={k.Value}"));
        }
        #endregion

        #region 请求失败时抛出异常 + ThrowFailure(HttpMethod method, HttpStatusCode statusCode, string content)
        /// <summary>
        /// 请求失败时抛出异常
        /// <para>https://cloud.tencent.com/document/product/436/7730</para>
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
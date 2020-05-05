#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：CompressHelper
* 类 描 述 ：压缩与解压帮助类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/12/4 21:25:22
* 更新时间 ：2020/05/05 11:25:22
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Kane.Extension
{
    /// <summary>
    /// 压缩与解压帮助类
    /// </summary>
    public class CompressHelper
    {
        #region 字节数组压缩成字节数组 + Compress(byte[] data, CompressMethod method)
        /// <summary>
        /// 字节数组压缩成字节数组
        /// </summary>
        /// <param name="data">压缩前字节数组</param>
        /// <param name="method">压缩方法<see cref="CompressMethod"/></param>
        /// <returns></returns>
        public byte[] Compress(byte[] data, CompressMethod method)
        {
            try
            {
                if (method == CompressMethod.None) return data;
                using MemoryStream inputStream = new MemoryStream(data);
                using var outputStream = new MemoryStream();
                if (method == CompressMethod.Deflate)
                {
                    using var compressor = new DeflateStream(outputStream, CompressionMode.Compress, true);
                    inputStream.CopyTo(compressor);
                }
#if NETCOREAPP
                else if (method == CompressMethod.Brotli)
                {
                    using var compressor = new BrotliStream(outputStream, CompressionMode.Compress, true);
                    inputStream.CopyTo(compressor);
                }
#endif
                else
                {
                    using var compressor = new GZipStream(outputStream, CompressionMode.Compress, true);
                    inputStream.CopyTo(compressor);

                }
                return outputStream.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 字符串压缩成字节数组，默认使用UTF8编码 + Compress(string data, CompressMethod method)
        /// <summary>
        /// 字符串压缩成字节数组，默认使用UTF8编码
        /// </summary>
        /// <param name="data">要压缩的字符串</param>
        /// <param name="method">压缩方法<see cref="CompressMethod"/></param>
        /// <returns></returns>
        public byte[] Compress(string data, CompressMethod method) => Compress(data.ToBytes(Encoding.UTF8), method);
        #endregion

        #region 字符串压缩成字节数组，可设置编码 + Compress(string data, CompressMethod method, Encoding encoding)
        /// <summary>
        /// 字符串压缩成字节数组，可设置编码
        /// </summary>
        /// <param name="data">要压缩的字符串</param>
        /// <param name="method">压缩方法<see cref="CompressMethod"/></param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public byte[] Compress(string data, CompressMethod method, Encoding encoding) => Compress(data.ToBytes(encoding), method);
        #endregion

        #region 字节数组压缩成Base64字符串 + CompressToBase64(byte[] data, CompressMethod method)
        /// <summary>
        /// 字节数组压缩成Base64字符串
        /// </summary>
        /// <param name="data">要压缩的字节数组</param>
        /// <param name="method">压缩方法<see cref="CompressMethod"/></param>
        /// <returns></returns>
        public string CompressToBase64(byte[] data, CompressMethod method) => Compress(data, method).ToBase64();
        #endregion

        #region 字符串压缩成Base64字符串，默认使用UTF8编码 + CompressToBase64(string data, CompressMethod method)
        /// <summary>
        /// 字符串压缩成Base64字符串，默认使用UTF8编码
        /// </summary>
        /// <param name="data">要压缩的字符串</param>
        /// <param name="method">压缩方法<see cref="CompressMethod"/></param>
        /// <returns></returns>
        public string CompressToBase64(string data, CompressMethod method) => Compress(data.ToBytes(Encoding.UTF8), method).ToBase64();
        #endregion

        #region 字符串压缩成Base64字符串，可设置编码 + CompressToBase64(string data, CompressMethod method, Encoding encoding)
        /// <summary>
        /// 字符串压缩成Base64字符串，可设置编码
        /// </summary>
        /// <param name="data">要压缩的字符串</param>
        /// <param name="method">压缩方法<see cref="CompressMethod"/></param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public string CompressToBase64(string data, CompressMethod method, Encoding encoding) => Compress(data.ToBytes(encoding), method).ToBase64();
        #endregion

        #region 字节数组解压成字节数组 + DeCompress(byte[] data, CompressMethod method)
        /// <summary>
        /// 字节数组解压成字节数组
        /// </summary>
        /// <param name="data">压缩后的字节数组</param>
        /// <param name="method">压缩方法<see cref="CompressMethod"/></param>
        /// <returns></returns>
        public byte[] DeCompress(byte[] data, CompressMethod method)
        {
            try
            {
                if (method == CompressMethod.None) return data;
                using MemoryStream inputStream = new MemoryStream(data);
                using var outputStream = new MemoryStream();
                if (method == CompressMethod.Deflate)
                {
                    using var decompressor = new DeflateStream(inputStream, CompressionMode.Decompress, true);
                    decompressor.CopyTo(outputStream);
                }
#if NETCOREAPP
                else if (method == CompressMethod.Brotli)
                {
                    using var decompressor = new BrotliStream(inputStream, CompressionMode.Decompress, true);
                    decompressor.CopyTo(outputStream);
                }
#endif
                else
                {
                    using var decompressor = new GZipStream(inputStream, CompressionMode.Decompress, true);
                    decompressor.CopyTo(outputStream);
                }
                return outputStream.GetBuffer();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 字节数组解压成字符串，默认使用UTF8编码 + DeCompressFromByte(byte[] data, CompressMethod method)
        /// <summary>
        /// 字节数组解压成字符串，默认使用UTF8编码
        /// </summary>
        /// <param name="data">要解压的字节数组</param>
        /// <param name="method">压缩方法<see cref="CompressMethod"/></param>
        /// <returns></returns>
        public string DeCompressFromByte(byte[] data, CompressMethod method) => DeCompress(data, method).BytesToString(Encoding.UTF8);
        #endregion

        #region 字节数组解压成字符串，可设置编码 + DeCompressFromByte(byte[] data, CompressMethod method, Encoding encoding)
        /// <summary>
        /// 字节数组解压成字符串，可设置编码
        /// </summary>
        /// <param name="data">要解压的字节数组</param>
        /// <param name="method">压缩方法<see cref="CompressMethod"/></param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public string DeCompressFromByte(byte[] data, CompressMethod method, Encoding encoding) => DeCompress(data, method).BytesToString(encoding);
        #endregion

        #region Base64字符串解压成字符串，默认使用UTF8编码 + DeCompressFromBase64(string data, CompressMethod method)
        /// <summary>
        /// Base64字符串解压成字符串，默认使用UTF8编码
        /// </summary>
        /// <param name="data">要解压的Base64字符串</param>
        /// <param name="method">压缩方法<see cref="CompressMethod"/></param>
        /// <returns></returns>
        public string DeCompressFromBase64(string data, CompressMethod method) => DeCompressFromByte(data.Base64ToBytes(), method);
        #endregion

        #region Base64字符串解压成字符串，可设置编码 + DeCompressFromBase64(string data, CompressMethod method, Encoding encoding)
        /// <summary>
        /// Base64字符串解压成字符串，可设置编码
        /// </summary>
        /// <param name="data">要压缩的Base64字符串</param>
        /// <param name="method">压缩方法CompressMethod</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public string DeCompressFromBase64(string data, CompressMethod method, Encoding encoding) => DeCompressFromByte(data.Base64ToBytes(), method, encoding);
        #endregion
    }
}
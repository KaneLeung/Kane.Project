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
* 更新时间 ：2020/05/16 11:25:22
* 版 本 号 ：v1.0.1.0
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
        #region 字节数组压缩成字节数组 + Compress(byte[] data, CompressMode mode)
        /// <summary>
        /// 字节数组压缩成字节数组
        /// </summary>
        /// <param name="data">压缩前字节数组</param>
        /// <param name="mode">压缩方法<see cref="CompressMode"/></param>
        /// <returns></returns>
        public byte[] Compress(byte[] data, CompressMode mode)
        {
            try
            {
                if (mode == CompressMode.None) return data;
                using MemoryStream inputStream = new MemoryStream(data);
                using var outputStream = new MemoryStream();
                if (mode == CompressMode.Deflate)
                {
                    using var compressor = new DeflateStream(outputStream, CompressionMode.Compress, true);
                    inputStream.CopyTo(compressor);
                }
#if NETCOREAPP
                else if (mode == CompressMode.Brotli)
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

        #region 将流压缩成流 + Compress(Stream data, CompressMode mode)
        /// <summary>
        /// 将流压缩成流
        /// </summary>
        /// <param name="data">压缩前的流</param>
        /// <param name="mode">压缩方法<see cref="CompressMode"/></param>
        /// <returns></returns>
        public Stream Compress(Stream data, CompressMode mode)
        {
            try
            {
                if (mode == CompressMode.None) return data;
                data.Seek(0, SeekOrigin.Begin);
                MemoryStream outputStream = new MemoryStream();
                if (mode == CompressMode.Deflate)
                {
                    using var compressor = new DeflateStream(outputStream, CompressionMode.Compress, true);
                    data.CopyTo(compressor);
                }
#if NETCOREAPP
                else if (mode == CompressMode.Brotli)
                {
                    using var compressor = new BrotliStream(outputStream, CompressionMode.Compress, true);
                    data.CopyTo(compressor);
                }
#endif
                else
                {
                    using var compressor = new GZipStream(outputStream, CompressionMode.Compress, true);
                    data.CopyTo(compressor);

                }
                return outputStream;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 字符串压缩成字节数组，默认使用UTF8编码 + Compress(string data, CompressMode mode)
        /// <summary>
        /// 字符串压缩成字节数组，默认使用UTF8编码
        /// </summary>
        /// <param name="data">要压缩的字符串</param>
        /// <param name="mode">压缩方法<see cref="CompressMode"/></param>
        /// <returns></returns>
        public byte[] Compress(string data, CompressMode mode) => Compress(data.ToBytes(Encoding.UTF8), mode);
        #endregion

        #region 字符串压缩成字节数组，可设置编码 + Compress(string data, CompressMode mode, Encoding encoding)
        /// <summary>
        /// 字符串压缩成字节数组，可设置编码
        /// </summary>
        /// <param name="data">要压缩的字符串</param>
        /// <param name="mode">压缩方法<see cref="CompressMode"/></param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public byte[] Compress(string data, CompressMode mode, Encoding encoding) => Compress(data.ToBytes(encoding), mode);
        #endregion

        #region 字节数组压缩成Base64字符串 + CompressToBase64(byte[] data, CompressMode mode)
        /// <summary>
        /// 字节数组压缩成Base64字符串
        /// </summary>
        /// <param name="data">要压缩的字节数组</param>
        /// <param name="mode">压缩方法<see cref="CompressMode"/></param>
        /// <returns></returns>
        public string CompressToBase64(byte[] data, CompressMode mode) => Compress(data, mode).ToBase64();
        #endregion

        #region 字符串压缩成Base64字符串，默认使用UTF8编码 + CompressToBase64(string data, CompressMode mode)
        /// <summary>
        /// 字符串压缩成Base64字符串，默认使用UTF8编码
        /// </summary>
        /// <param name="data">要压缩的字符串</param>
        /// <param name="mode">压缩方法<see cref="CompressMode"/></param>
        /// <returns></returns>
        public string CompressToBase64(string data, CompressMode mode) => Compress(data.ToBytes(Encoding.UTF8), mode).ToBase64();
        #endregion

        #region 字符串压缩成Base64字符串，可设置编码 + CompressToBase64(string data, CompressMode mode, Encoding encoding)
        /// <summary>
        /// 字符串压缩成Base64字符串，可设置编码
        /// </summary>
        /// <param name="data">要压缩的字符串</param>
        /// <param name="mode">压缩方法<see cref="CompressMode"/></param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public string CompressToBase64(string data, CompressMode mode, Encoding encoding) => Compress(data.ToBytes(encoding), mode).ToBase64();
        #endregion

        #region 字节数组解压成字节数组 + DeCompress(byte[] data, CompressMode mode)
        /// <summary>
        /// 字节数组解压成字节数组
        /// </summary>
        /// <param name="data">压缩后的字节数组</param>
        /// <param name="mode">压缩方法<see cref="CompressMode"/></param>
        /// <returns></returns>
        public byte[] DeCompress(byte[] data, CompressMode mode)
        {
            try
            {
                if (mode == CompressMode.None) return data;
                using MemoryStream inputStream = new MemoryStream(data);
                using var outputStream = new MemoryStream();
                if (mode == CompressMode.Deflate)
                {
                    using var decompressor = new DeflateStream(inputStream, CompressionMode.Decompress, true);
                    decompressor.CopyTo(outputStream);
                }
#if NETCOREAPP
                else if (mode == CompressMode.Brotli)
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

        #region 将流解压成流 + DeCompress(Stream data, CompressMode mode)
        /// <summary>
        /// 将流解压成流
        /// </summary>
        /// <param name="data">压缩后的流</param>
        /// <param name="mode">压缩方法<see cref="CompressMode"/></param>
        /// <returns></returns>
        public Stream DeCompress(Stream data, CompressMode mode)
        {
            try
            {
                if (mode == CompressMode.None) return data;
                data.Seek(0, SeekOrigin.Begin);
                var outputStream = new MemoryStream();
                if (mode == CompressMode.Deflate)
                {
                    using var decompressor = new DeflateStream(data, CompressionMode.Decompress, true);
                    decompressor.CopyTo(outputStream);
                }
#if NETCOREAPP
                else if (mode == CompressMode.Brotli)
                {
                    using var decompressor = new BrotliStream(data, CompressionMode.Decompress, true);
                    decompressor.CopyTo(outputStream);
                }
#endif
                else
                {
                    using var decompressor = new GZipStream(data, CompressionMode.Decompress, true);
                    decompressor.CopyTo(outputStream);
                }
                return outputStream;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 字节数组解压成字符串，默认使用UTF8编码 + DeCompressFromByte(byte[] data, CompressMode mode)
        /// <summary>
        /// 字节数组解压成字符串，默认使用UTF8编码
        /// </summary>
        /// <param name="data">要解压的字节数组</param>
        /// <param name="mode">压缩方法<see cref="CompressMode"/></param>
        /// <returns></returns>
        public string DeCompressFromByte(byte[] data, CompressMode mode) => DeCompress(data, mode).BytesToString(Encoding.UTF8);
        #endregion

        #region 字节数组解压成字符串，可设置编码 + DeCompressFromByte(byte[] data, CompressMode mode, Encoding encoding)
        /// <summary>
        /// 字节数组解压成字符串，可设置编码
        /// </summary>
        /// <param name="data">要解压的字节数组</param>
        /// <param name="mode">压缩方法<see cref="CompressMode"/></param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public string DeCompressFromByte(byte[] data, CompressMode mode, Encoding encoding) => DeCompress(data, mode).BytesToString(encoding);
        #endregion

        #region Base64字符串解压成字符串，默认使用UTF8编码 + DeCompressFromBase64(string data, CompressMode mode)
        /// <summary>
        /// Base64字符串解压成字符串，默认使用UTF8编码
        /// </summary>
        /// <param name="data">要解压的Base64字符串</param>
        /// <param name="mode">压缩方法<see cref="CompressMode"/></param>
        /// <returns></returns>
        public string DeCompressFromBase64(string data, CompressMode mode) => DeCompressFromByte(data.Base64ToBytes(), mode);
        #endregion

        #region Base64字符串解压成字符串，可设置编码 + DeCompressFromBase64(string data, CompressMode mode, Encoding encoding)
        /// <summary>
        /// Base64字符串解压成字符串，可设置编码
        /// </summary>
        /// <param name="data">要压缩的Base64字符串</param>
        /// <param name="mode">压缩方法CompressMode</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public string DeCompressFromBase64(string data, CompressMode mode, Encoding encoding) => DeCompressFromByte(data.Base64ToBytes(), mode, encoding);
        #endregion
    }
}
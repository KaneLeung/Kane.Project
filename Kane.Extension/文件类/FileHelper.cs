﻿#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：FileHelper
* 类 描 述 ：文件类扩展
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/30 0:01:37
* 更新时间 ：2020/05/02 12:31:37
* 版 本 号 ：v1.0.4.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kane.Extension
{
    /// <summary>
    /// 文件类扩展
    /// </summary>
    public static class FileHelper
    {
        #region 通过文件头两字节判断文件类型 + GetFileExt(string path)
        /// <summary>
        /// 通过文件头两字节判断文件类型,会有类型不同，但值相同的情况
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static FileExt GetFileExt(string path)
        {
            using FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            using BinaryReader binaryReader = new BinaryReader(fileStream);
            try
            {
                var buffer = binaryReader.ReadByte();
                var result = buffer.ToString() + binaryReader.ReadByte().ToString();
                return (FileExt)result.ToInt();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return FileExt.None;
            }
        }
        #endregion

        #region 根据后缀判断文件类型 + GetExtension(this string path)
        /// <summary>
        /// 根据后缀判断文件类型
        /// </summary>
        /// <param name="path">文件全路径</param>
        /// <returns></returns>
        public static string GetExtension(this string path) => Path.GetExtension(path);
        #endregion

        #region 递归获取目录下所有文件 + GetPathAllFiles(string path, List<string> files = null)
        /// <summary>
        /// 递归获取目录下所有文件
        /// </summary>
        /// <param name="path">要获取文件的目录</param>
        /// <param name="files">这个不用传值，或传Null</param>
        /// <param name="pattern">文字和通配符的组合, 但不支持正则表达式。</param>
        /// <returns></returns>
        private static List<string> GetPathAllFiles(string path, List<string> files = null, string pattern = "")
        {
            if (files == null) files = new List<string>();
            string[] subPaths = pattern.IsNullOrEmpty() ? Directory.GetDirectories(path) : Directory.GetDirectories(path, pattern);
            foreach (var item in subPaths)
            {
                GetPathAllFiles(item, files, pattern);
            }
            files.AddRange(pattern.IsNullOrEmpty() ? Directory.GetFiles(path).ToList() : Directory.GetFiles(path, pattern).ToList());
            return files;
        }
        #endregion

        #region 递归获取目录下所有文件，可根据通配符来进行过滤。 + GetPathAllFiles(string path, string pattern = "")
        /// <summary>
        /// 递归获取目录下所有文件，可根据通配符来进行过滤。 
        /// 例如, searchPattern字符串 "*t" path搜索以字母 "t" 结尾的所有名称。 
        /// 字符串 "s* "path搜索以字母 "s" 开头的所有名称。 searchPattern
        /// *红星   此位置中的零个或多个字符。
        /// ? (问号)	此位置中的零个或一个字符。
        /// https://docs.microsoft.com/zh-cn/dotnet/api/system.io.directory.getdirectories?view=netcore-3.0#System_IO_Directory_GetDirectories_System_String_System_String_System_IO_SearchOption_
        /// </summary>
        /// <param name="path">要获取文件的目录</param>
        /// <param name="pattern">与目录的名称匹配的搜索字符串。 此参数可以包含有效文本和通配符的组合，但不支持正则表达式。</param>
        /// <returns></returns>
        public static List<string> GetPathAllFiles(string path, string pattern = "") => GetPathAllFiles(path, null, pattern);
        #endregion

        #region 校验文件名是否符合Window文件命名规范 + CheckFilename(string filename)
        /// <summary>
        /// 校验文件名是否符合Window文件命名规范
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool CheckFilename(string filename)
        {
            char[] forbidChars = { '\\', '/', ':', '*', '?', '"', '<', '>', '|' };
            return filename.IndexOfAny(forbidChars) == -1;
        }
        #endregion

        #region 字节数组保存文件 + ToFile(this byte[] bytes, string path)
#if !NET40
        /// <summary>
        /// 字节数组保存文件，建议小文件才使用，大文件请使用<see cref="ToBigFile(byte[], string, int)"/>或【Net.40以上】<see cref="ToBigFileAsync(byte[], string, int)"/>
        /// </summary>
        /// <param name="bytes">字节数组数据</param>
        /// <param name="path">保存至指定路径，包含文件名</param>
#else
        /// <summary>
        /// 字节数组保存文件，建议小文件才使用，大文件请使用<see cref="ToBigFile(byte[], string, int)"/>
        /// </summary>
        /// <param name="bytes">字节数组数据</param>
        /// <param name="path">保存至指定路径，包含文件名</param>
#endif
        public static void ToFile(this byte[] bytes, string path)
        {
            using FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            fileStream.Write(bytes, 0, bytes.Length);
        }
        #endregion

        #region 流数据保存文件，建议小文件才使用 + ToFile(this Stream bytes, string path)
#if !NET40
        /// <summary>
        /// 流数据保存文件，建议小文件才使用，大文件请使用<see cref="ToBigFile(Stream, string, int)"/>或【Net.40以上】<see cref="ToBigFileAsync(Stream, string, int)"/>
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <param name="path">保存至指定路径，包含文件名</param>
#else
        /// <summary>
        /// 流数据保存文件，建议小文件才使用，大文件请使用<see cref="ToBigFile(Stream, string, int)"/>
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <param name="path">保存至指定路径，包含文件名</param>
#endif
        public static void ToFile(this Stream stream, string path)
        {
            stream.Seek(0, SeekOrigin.Begin);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            buffer.ToFile(path);
        }
        #endregion

        #region 流数据保存大文件，可设置缓存大小，默认为【1M】 + ToBigFile(this Stream stream, string path, int bufferSize = 1)
        /// <summary>
        /// 流数据保存大文件，可设置缓存大小，默认为【1M】
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <param name="path">保存至指定路径，包含文件名</param>
        /// <param name="bufferSize">缓存大小，默认为【1M】</param>
        public static void ToBigFile(this Stream stream, string path, int bufferSize = 1)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using var fileStream = new FileStream(path, FileMode.Create);
            var buffer = new byte[bufferSize * 1024 * 1024];
            var size = stream.Read(buffer, 0, buffer.Length);
            while (size > 0)
            {
                fileStream.Write(buffer, 0, size);
                size = stream.Read(buffer, 0, buffer.Length);
            }
        }
        #endregion

        #region 字节数组保存大文件，可设置缓存大小，默认为【1M】 + ToBigFile(this byte[] bytes, string path, int bufferSize = 1)
        /// <summary>
        /// 字节数组保存大文件，可设置缓存大小，默认为【1M】
        /// </summary>
        /// <param name="bytes">字节数组数据</param>
        /// <param name="path">保存至指定路径，包含文件名</param>
        /// <param name="bufferSize">缓存大小，默认为【1M】</param>
        /// <returns></returns>
        public static void ToBigFile(this byte[] bytes, string path, int bufferSize = 1)
        {
            using var ms = new MemoryStream(bytes);
            ToBigFile(ms, path, bufferSize);
        }
        #endregion

#if !NET40
        #region 字节数组异步保存大文件，可设置缓存大小，默认为【1M】 + ToBigFileAsync(this byte[] bytes, string path, int bufferSize = 1)
        /// <summary>
        /// 字节数组异步保存大文件，可设置缓存大小，默认为【1M】
        /// </summary>
        /// <param name="bytes">字节数组数据</param>
        /// <param name="path">保存至指定路径，包含文件名</param>
        /// <param name="bufferSize">缓存大小，默认为【1M】</param>
        /// <returns></returns>
        public static async Task<bool> ToBigFileAsync(this byte[] bytes, string path, int bufferSize = 1)
        {
            using var stream = new MemoryStream(bytes);
            return await ToBigFileAsync(stream, path, bufferSize);
        }
        #endregion

        #region 流数据异步保存大文件，可设置缓存大小，默认为【1M】 + ToBigFileAsync(this Stream stream, string path, int bufferSize = 1)
        /// <summary>
        /// 流数据异步保存大文件，可设置缓存大小，默认为【1M】
        /// </summary>
        /// <param name="stream">流数据</param>
        /// <param name="path">保存至指定路径，包含文件名</param>
        /// <param name="bufferSize">缓存大小，默认为【1M】</param>
        /// <returns></returns>
        public static async Task<bool> ToBigFileAsync(this Stream stream, string path, int bufferSize = 1)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using var fileStream = new FileStream(path, FileMode.Create);
            var buffer = new byte[bufferSize * 1024 * 1024];
            var size = await stream.ReadAsync(buffer, 0, buffer.Length);
            while (size > 0)
            {
                await fileStream.WriteAsync(buffer, 0, size);
                size = await stream.ReadAsync(buffer, 0, buffer.Length);
            }
            return true;
        }
        #endregion

        #region 从文件读取成字节数组 + FileToBytes(this string path)
        /// <summary>
        /// 从文件读取成字节数组
        /// </summary>
        /// <param name="path">文件完整路径</param>
        /// <returns></returns>
        public static async Task<byte[]> FileToBytesAsync(this string path)
        {
            if (!File.Exists(path)) return default;
            using FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            var bytes = new byte[fileStream.Length];
            fileStream.Seek(0, SeekOrigin.Begin);
            await fileStream.ReadAsync(bytes, 0, bytes.Length);
            return bytes;
        }
        #endregion
#endif
        #region 从文件异步读取成字节数组 + FileToBytes(this string path)
        /// <summary>
        /// 从文件异步读取成字节数组
        /// </summary>
        /// <param name="path">文件完整路径</param>
        /// <returns></returns>
        public static byte[] FileToBytes(this string path)
        {
            if (!File.Exists(path)) return default;
            using FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            var bytes = new byte[fileStream.Length];
            fileStream.Seek(0, SeekOrigin.Begin);
            fileStream.Read(bytes, 0, bytes.Length);
            return bytes;
        }
        #endregion
    }
}
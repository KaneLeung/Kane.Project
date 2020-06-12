#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：ExceptionEx
* 类 描 述 ：异常处理扩展类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/5/30 14:11:40
* 更新时间 ：2020/5/30 14:11:40
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.IO;

namespace Kane.Extension
{
    /// <summary>
    /// 异常处理扩展类
    /// </summary>
    public static class ExceptionEx
    {
        #region 判断对象是否为空，如果为空则抛【ArgumentNullException】 + ThrowIfNull<T>(this T value, string paramName, string message = "")
        /// <summary>
        /// 判断对象是否为空，如果为空则抛【ArgumentNullException】
        /// </summary>
        /// <typeparam name="T">要判断的对象的类型</typeparam>
        /// <param name="value">要判断的对象</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="message">异常消息</param>
        public static void ThrowIfNull<T>(this T value, string paramName, string message = "")
        {
            if ((value is string str && str.IsNullOrEmpty()) || value.IsNull()) throw new ArgumentNullException(paramName, message);
        }
        #endregion

        #region 判断该路径的文件是否存在，不存在则抛【FileNotFoundException】 + ThrowFileNotExist(this string file, string message = "")
        /// <summary>
        /// 判断该路径的文件是否存在，不存在则抛【FileNotFoundException】
        /// </summary>
        /// <param name="file">文件完整路径</param>
        /// <param name="message">异常消息,默认为【文件名】该文件不存在</param>
        public static void ThrowFileNotExist(this string file, string message = "")
        {
            if (file.IsNullOrEmpty() || !File.Exists(file)) throw new FileNotFoundException(message.IsNullOrEmpty() ? $"【{file}】该文件不存在" : message);
        }
        #endregion

        #region 判断该路径的文件夹是否存在，不存在则抛【DirectoryNotFoundException】 + ThrowDirNotExist(this string directory, string message = "")
        /// <summary>
        /// 判断该路径的文件夹是否存在，不存在则抛【DirectoryNotFoundException】
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="message"></param>
        public static void ThrowDirNotExist(this string directory, string message = "")
        {
            if (directory.IsNullOrEmpty() || !Directory.Exists(directory)) throw new DirectoryNotFoundException(message.IsNullOrEmpty() ? $"【{directory}】该文件夹不存在" : message);
        }
        #endregion
    }
}
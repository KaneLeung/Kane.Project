#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：FileExt
* 类 描 述 ：文件后缀枚举
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/30 0:02:31
* 更新时间 ：2019/10/30 0:02:31
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace Kane.Extension
{
    /// <summary>
    /// 文件后缀枚举【全大写】
    /// </summary>
    public enum FileExt
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        /// <summary>
        /// 为空
        /// </summary>
        None = 0,
        /// <summary>
        /// Checked
        /// </summary>
        JPG = 255216,
        /// <summary>
        /// Checked
        /// </summary>
        GIF = 7173,
        /// <summary>
        /// Checked
        /// </summary>
        BMP = 6677,
        /// <summary>
        /// Checked
        /// </summary>
        PNG = 13780,
        /// <summary>
        /// Checked
        /// </summary>
        TIF = 7373,
        COM = 7790,
        /// <summary>
        /// Checked
        /// </summary>
        EXE = 7790,
        /// <summary>
        /// Checked
        /// </summary>
        DLL = 7790,
        /// <summary>
        /// Checked
        /// </summary>
        RAR = 8297,
        /// <summary>
        /// Checked
        /// </summary>
        ZIP = 8075,
        XML = 6063,
        /// <summary>
        /// Checked
        /// </summary>
        HTML = 6033,
        ASPX = 239187,
        /// <summary>
        /// Checked
        /// </summary>
        CS = 239187,
        JS = 119105,
        /// <summary>
        /// Checked
        /// </summary>
        TXT = 77105,
        SQL = 255254,
        BAT = 64101,
        BTSEED = 10056,
        RDP = 255254,
        PSD = 5666,
        /// <summary>
        /// Checked
        /// </summary>
        PDF = 3780,
        CHM = 7384,
        LOG = 70105,
        REG = 8269,
        HLP = 6395,
        /// <summary>
        /// Checked208207
        /// </summary>
        DOC = 208207,
        /// <summary>
        /// Checked
        /// </summary>
        XLS = 208207,
        /// <summary>
        /// Checked
        /// </summary>
        DOCX = 8075,
        /// <summary>
        /// Checked
        /// </summary>
        XLSX = 8075,
        /// <summary>
        /// Checked
        /// </summary>
        MKV = 2669,
        //7Z = 55122,
        /// <summary>
        /// Checked
        /// </summary>
        MSI = 208207,
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}

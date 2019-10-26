#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：MouseHookStruct
* 类 描 述 ：鼠标钩子消息结构体
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.WinForm.GlobalHook
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/25 0:38:30
* 更新时间 ：2019/10/25 0:38:30
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Kane.WinForm.GlobalHook
{
    /// <summary>
    /// LayoutKind.Sequential 用于强制将成 员按其出现的顺序进行顺序布局。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class MouseHookStruct
    {
        public Point Point { get; set; }
        public int MouseData { get; set; }
        public uint Flags { get; set; }
        public uint Time { get; set; }
        public IntPtr DwExtraInfo { get; set; }
    }
}

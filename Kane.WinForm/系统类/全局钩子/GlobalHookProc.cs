#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：GlobalHookProc
* 类 描 述 ：定义了指向此类回调函数的指针
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.WinForm.GlobalHook
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/25 22:12:47
* 更新时间 ：2019/10/25 22:12:47
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace Kane.WinForm.GlobalHook
{
    public delegate IntPtr GlobalHookProc(int nCode, IntPtr wParam, IntPtr lParam);
}

#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：MouseHookEnums
* 类 描 述 ：鼠标钩子枚举类
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.WinForm.GlobalHook
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/25 0:40:57
* 更新时间 ：2019/10/25 0:40:57
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
    /// <summary>
    /// 鼠标钩子消息类型枚举
    /// https://docs.microsoft.com/en-us/windows/win32/inputdev/mouse-input-notifications
    /// </summary>
    public enum MouseMessages
    {
        MouseMove = 0x200,
        LeftButtonDown = 0x201,
        LeftButtonUp = 0x202,
        LeftButtonDBCLK = 0x203,
        RightButtonDown = 0x204,
        RightButtonUp = 0x205,
        RightButtonDBCLK = 0x206,
        MiddleButtonDown = 0x207,
        MiddleButtonUp = 0x208,
        MiddleButtonDBCLK = 0x209,
        MouseWheel = 0x20A,
        /// <summary>
        /// Horizontal水平滚轮
        /// </summary>
        MouseHWheel = 0x20E,
    }

    public enum MouseButtons
    {
        None,
        Left,
        Right,
        Middle,
        Wheel,
        HWheel,
    }

    /// <summary>
    /// 鼠标滚轮方向枚举
    /// </summary>
    public enum MouseScrollDirection
    {
        None = -1,
        Up = 0,
        Down = 1
    }
}
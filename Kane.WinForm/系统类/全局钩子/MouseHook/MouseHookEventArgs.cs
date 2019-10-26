#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：MouseHookEventArgs
* 类 描 述 ：鼠标钩子的EventArgs
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.WinForm
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/25 22:16:11
* 更新时间 ：2019/10/25 22:16:11
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Kane.WinForm.GlobalHook
{
    public class MouseHookEventArgs : EventArgs
    {
        private readonly MouseHookStruct HookStruct;
        public MouseHookEventArgs(MouseHookStruct hookStruct)
        {
            HookStruct = hookStruct;
        }

        public Point Position
        {
            get => HookStruct.Point;
        }

        public MouseScrollDirection ScrollDirection
        {
            get
            {
                if (MouseMessage != MouseMessages.MouseWheel)
                    return MouseScrollDirection.None;
                return (HookStruct.MouseData >> 16) > 0 ? MouseScrollDirection.Up : MouseScrollDirection.Down;
            }
        }

        public MouseMessages MouseMessage { get; internal set; }
        public MouseButtons MouseButton { get; internal set; } = MouseButtons.None;
        public bool DoubleClick { get; internal set; } = false;
    }
}
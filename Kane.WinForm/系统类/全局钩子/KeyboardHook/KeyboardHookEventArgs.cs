#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：KeyboardHookEventArgs
* 类 描 述 ：键盘钩子的EventArgs
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.WinForm.GlobalHook
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/26 1:03:44
* 更新时间 ：2019/10/26 1:03:44
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Kane.WinForm.GlobalHook
{
    public class KeyboardHookEventArgs : EventArgs
    {
        private readonly KeyboardHookStruct HookStruct;
        public KeyboardHookEventArgs(KeyboardHookStruct hookStruct)
        {
            HookStruct = hookStruct;
            var nonVirtual = GlobalHookWin32Api.MapVirtualKey((uint)VirtualKeyCode, 2);
            Char = Convert.ToChar(nonVirtual);
        }

        public int VirtualKeyCode { get=> HookStruct.VkCode; }

        public Keys Key { get => (Keys)VirtualKeyCode; }

        public char Char { get; internal set; }

        public string KeyString
        {
            get
            {
                if (Char == '\0')
                {
                    return Key == Keys.Return ? "[Enter]" : $"[{Key}]";
                }
                if (Char == '\r')
                {
                    Char = '\0';
                    return "[Enter]";
                }
                if (Char == '\b')
                {
                    Char = '\0';
                    return "[Backspace]";
                }
                return Char.ToString(CultureInfo.InvariantCulture);
            }
        }

        public KeyboardMessages KeyboardMessage { get; internal set; }
    }
}
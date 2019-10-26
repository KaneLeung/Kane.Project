#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：KeyboardHook
* 类 描 述 ：键盘钩子
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.WinForm.GlobalHook
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/26 1:11:01
* 更新时间 ：2019/10/26 1:11:01
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Kane.WinForm.GlobalHook
{
    public class KeyboardHook : IDisposable
    {
        #region Custom Events

        public event EventHandler<KeyboardHookEventArgs> KeyDown;

        private void OnKeyDown(KeyboardHookEventArgs e)
        {
            KeyDown?.Invoke(this, e);
            OnKeyEvent(e);
        }

        public event EventHandler<KeyboardHookEventArgs> KeyUp;

        private void OnKeyUp(KeyboardHookEventArgs e)
        {
            KeyUp?.Invoke(this, e);
            OnKeyEvent(e);
        }

        public event EventHandler<KeyboardHookEventArgs> KeyEvent;

        private void OnKeyEvent(KeyboardHookEventArgs e)
        {
            KeyEvent?.Invoke(this, e);
        }

        #endregion

        /// <summary>
        /// 私有变量
        /// </summary>
        private IntPtr hookID;
        private readonly GlobalHookProc callback;
        private bool hooked;

        public KeyboardHook()
        {
            callback = KeyboardHookCallback;
        }

        #region 挂载钩子，成功后返回HookID + Hook()
        /// <summary>
        /// 挂载钩子，成功后返回HookID
        /// </summary>
        /// <returns></returns>
        public IntPtr Hook()
        {
            hookID = GlobalHookWin32Api.GetHookID((int)HooksMethod.WH_KEYBOARD_LL, callback);
            hooked = true;
            return hookID;
        }
        #endregion

        public bool Unhook()
        {
            if (!hooked) return true;
            GlobalHookWin32Api.UnhookWindowsHookEx(hookID);
            hooked = false;
            return true;
        }

        #region 触发键盘钩子事件时调用的回调方法 + KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        /// <summary>
        /// 触发键盘钩子事件时调用的回调方法
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var lParamStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                var e = new KeyboardHookEventArgs(lParamStruct);
                switch ((KeyboardMessages)wParam)
                {
                    case KeyboardMessages.KeyDown:
                        e.KeyboardMessage = KeyboardMessages.KeyDown;
                        OnKeyDown(e);
                        break;
                    case KeyboardMessages.KeyUp:
                        e.KeyboardMessage = KeyboardMessages.KeyUp;
                        OnKeyUp(e);
                        break;
                    case KeyboardMessages.SysKeyDown:
                        e.KeyboardMessage = KeyboardMessages.SysKeyDown;
                        OnKeyDown(e);
                        break;
                    case KeyboardMessages.SysKeyUp:
                        e.KeyboardMessage = KeyboardMessages.SysKeyUp;
                        OnKeyUp(e);
                        break;
                }
            }
            return GlobalHookWin32Api.CallNextHookEx(hookID, nCode, wParam, lParam);
        } 
        #endregion

        public void Dispose()
        {
            Unhook();
            GC.SuppressFinalize(this);
        }

        ~KeyboardHook()
        {
            Unhook();
        }
    }
}

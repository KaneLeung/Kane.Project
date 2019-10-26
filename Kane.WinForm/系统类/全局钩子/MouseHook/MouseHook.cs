#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：MouseHook
* 类 描 述 ：鼠标钩子
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.WinForm.GlobalHook
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/25 22:30:15
* 更新时间 ：2019/10/25 22:30:15
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
    public class MouseHook : IDisposable
    {
        #region 自定义事件
        public event EventHandler<MouseHookEventArgs> Move;

        private void OnMove(MouseHookEventArgs e)
        {
            Move?.Invoke(this, e);
            OnMouseEvent(e);
        }

        public event EventHandler<MouseHookEventArgs> LeftButtonDown;

        private void OnLeftButtonDown(MouseHookEventArgs e)
        {
            LeftButtonDown?.Invoke(this, e);
            OnMouseEvent(e);
        }

        public event EventHandler<MouseHookEventArgs> LeftButtonUp;

        private void OnLeftButtonUp(MouseHookEventArgs e)
        {
            LeftButtonUp?.Invoke(this, e);
            OnMouseEvent(e);
        }

        public event EventHandler<MouseHookEventArgs> LeftButtonDBCLK;

        private void OnLeftButtonDBCLK(MouseHookEventArgs e)
        {
            LeftButtonDBCLK?.Invoke(this, e);
            OnMouseEvent(e);
        }

        public event EventHandler<MouseHookEventArgs> RightButtonDown;

        private void OnRightButtonDown(MouseHookEventArgs e)
        {
            RightButtonDown?.Invoke(this, e);
            OnMouseEvent(e);
        }

        public event EventHandler<MouseHookEventArgs> RightButtonUp;

        private void OnRightButtonUp(MouseHookEventArgs e)
        {
            RightButtonUp?.Invoke(this, e);
            OnMouseEvent(e);
        }

        public event EventHandler<MouseHookEventArgs> RightButtonDBCLK;

        private void OnRightButtonDBCLK(MouseHookEventArgs e)
        {
            RightButtonDBCLK?.Invoke(this, e);
            OnMouseEvent(e);
        }

        public event EventHandler<MouseHookEventArgs> MiddleButtonDown;

        private void OnMiddleButtonDown(MouseHookEventArgs e)
        {
            MiddleButtonDown?.Invoke(this, e);
            OnMouseEvent(e);
        }

        public event EventHandler<MouseHookEventArgs> MiddleButtonUp;

        private void OnMiddleButtonUp(MouseHookEventArgs e)
        {
            MiddleButtonUp?.Invoke(this, e);
            OnMouseEvent(e);
        }

        public event EventHandler<MouseHookEventArgs> MiddleButtonDBCLK;

        private void OnMiddleButtonDBCLK(MouseHookEventArgs e)
        {
            MiddleButtonDBCLK?.Invoke(this, e);
            OnMouseEvent(e);
        }

        public event EventHandler<MouseHookEventArgs> MouseWheel;

        private void OnMouseWheel(MouseHookEventArgs e)
        {
            MouseWheel?.Invoke(this, e);
            OnMouseEvent(e);
        }

        public event EventHandler<MouseHookEventArgs> MouseHWheel;

        private void OnMouseHWheel(MouseHookEventArgs e)
        {
            MouseHWheel?.Invoke(this, e);
            OnMouseEvent(e);
        }

        public event EventHandler<MouseHookEventArgs> MouseEvent;

        private void OnMouseEvent(MouseHookEventArgs e)
        {
            MouseEvent?.Invoke(this, e);
        }
        #endregion

        /// <summary>
        /// 私有变量
        /// </summary>
        private IntPtr hookID;
        private readonly GlobalHookProc callback;
        private bool hooked;

        public MouseHook()
        {
            callback = MouseHookCallback;
        }

        #region 挂载钩子，成功后返回HookID + Hook()
        /// <summary>
        /// 挂载钩子，成功后返回HookID
        /// </summary>
        /// <returns></returns>
        public IntPtr Hook()
        {
            hookID = GlobalHookWin32Api.GetHookID((int)HooksMethod.WH_MOUSE_LL, callback);
            hooked = true;
            return hookID;
        }
        #endregion

        #region 卸载钩子，返回是否成功 + Unhook()
        /// <summary>
        /// 卸载钩子，返回是否成功
        /// </summary>
        /// <returns></returns>
        public bool Unhook()
        {
            if (!hooked) return true;
            GlobalHookWin32Api.UnhookWindowsHookEx(hookID);
            hooked = false;
            return true;
        }
        #endregion

        #region 触发鼠标钩子事件时调用的回调方法 + MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        /// <summary>
        /// 触发鼠标钩子事件时调用的回调方法
        /// TODO:注意！！！！双击暂时无法触发，原因不明
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var lParamStruct = (MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));
                var e = new MouseHookEventArgs(lParamStruct);
                switch ((MouseMessages)wParam)
                {
                    case MouseMessages.MouseMove:
                        e.MouseMessage = MouseMessages.MouseMove;
                        OnMove(e);
                        break;
                    case MouseMessages.LeftButtonDown:
                        e.MouseMessage = MouseMessages.LeftButtonDown;
                        e.MouseButton = MouseButtons.Left;
                        OnLeftButtonDown(e);
                        break;
                    case MouseMessages.LeftButtonUp:
                        e.MouseMessage = MouseMessages.LeftButtonUp;
                        e.MouseButton = MouseButtons.Left;
                        OnLeftButtonUp(e);
                        break;
                    case MouseMessages.LeftButtonDBCLK:
                        e.MouseMessage = MouseMessages.LeftButtonDBCLK;
                        e.MouseButton = MouseButtons.Left;
                        e.DoubleClick = true;
                        OnLeftButtonDBCLK(e);
                        break;
                    case MouseMessages.RightButtonDown:
                        e.MouseMessage = MouseMessages.RightButtonDown;
                        e.MouseButton = MouseButtons.Right;
                        OnRightButtonDown(e);
                        break;
                    case MouseMessages.RightButtonUp:
                        e.MouseMessage = MouseMessages.RightButtonUp;
                        e.MouseButton = MouseButtons.Right;
                        OnRightButtonUp(e);
                        break;
                    case MouseMessages.RightButtonDBCLK:
                        e.MouseMessage = MouseMessages.RightButtonDBCLK;
                        e.MouseButton = MouseButtons.Right;
                        e.DoubleClick = true;
                        OnRightButtonDBCLK(e);
                        break;
                    case MouseMessages.MiddleButtonDown:
                        e.MouseMessage = MouseMessages.MiddleButtonDown;
                        e.MouseButton = MouseButtons.Middle;
                        OnMiddleButtonDown(e);
                        break;
                    case MouseMessages.MiddleButtonUp:
                        e.MouseMessage = MouseMessages.MiddleButtonUp;
                        e.MouseButton = MouseButtons.Middle;
                        OnMiddleButtonUp(e);
                        break;
                    case MouseMessages.MiddleButtonDBCLK:
                        e.MouseMessage = MouseMessages.MiddleButtonDBCLK;
                        e.MouseButton = MouseButtons.Middle;
                        e.DoubleClick = true;
                        OnMiddleButtonDBCLK(e);
                        break;
                    case MouseMessages.MouseWheel:
                        e.MouseMessage = MouseMessages.MouseWheel;
                        e.MouseButton = MouseButtons.Wheel;
                        OnMouseWheel(e);
                        break;
                    case MouseMessages.MouseHWheel:
                        e.MouseMessage = MouseMessages.MouseHWheel;
                        e.MouseButton = MouseButtons.HWheel;
                        OnMouseHWheel(e);
                        break;
                }
            }
            return GlobalHookWin32Api.CallNextHookEx(hookID, nCode, wParam, lParam);
        }
        #endregion

        #region 释放资源
        public void Dispose()
        {
            Unhook();
            GC.SuppressFinalize(this);
        }

        ~MouseHook()
        {
            Unhook();
        }
        #endregion
    }
}
#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：GlobalHookWin32Api
* 类 描 述 ：全局钩子用到的Win32Api
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.WinForm.GlobalHook
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/25 22:09:16
* 更新时间 ：2019/10/25 22:09:16
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Kane.WinForm.GlobalHook
{
    public static class GlobalHookWin32Api
    {
        //键盘线程钩子
        //SetWindowsHookEx( 2,KeyboardHookProcedure, IntPtr.Zero, GetCurrentThreadId());//指定要监听的线程idGetCurrentThreadId(),
        //键盘全局钩子,需要引用空间(using System.Reflection;)
        //SetWindowsHookEx( 13,MouseHookProcedure,Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]),0);
        /// <summary>
        /// 使用此功能，安装了一个钩子
        /// </summary>
        /// <param name="hooksMethod">钩子类型，即确定钩子监听何种消息，上面的代码中设为2，即监听键盘消息并且是线程钩子，如果是全局钩子监听键盘消息应设为13,线程钩子监听鼠标消息设为7，全局钩子监听鼠标消息设为14</param>
        /// <param name="lpfn">钩子子程的地址指针。如果threadID参数为0 或是一个由别的进程创建的线程的标识，lpfn必须指向DLL中的钩子子程。 除此以外，lpfn可以指向当前进程的一段钩子子程代码。钩子函数的入口地址，当钩子钩到任何消息后便调用这个函数。</param>
        /// <param name="hMod">应用程序实例的句柄。标识包含lpfn所指的子程的DLL。如果threadID 标识当前进程创建的一个线程，而且子程代码位于当前进程，hMod必须为NULL。可以很简单的设定其为本应用程序的实例句柄</param>
        /// <param name="threadID">与安装的钩子子程相关联的线程的标识符如果为0，钩子子程与所有的线程关联，即为全局钩子</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SetWindowsHookEx(int hooksMethod, GlobalHookProc lpfn, IntPtr hMod, uint threadID);

        /// <summary>
        /// 使用WINDOWS API函数代替获取当前实例的函数,防止钩子失效
        /// </summary>
        /// <param name="lpModuleName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>
        /// 调用此函数卸载钩子
        /// </summary>
        /// <param name="hookID">要删除的钩子的句柄。这个参数是上一个函数SetWindowsHookEx的返回值.</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hookID);

        /// <summary>
        /// 使用此功能，通过信息钩子继续下一个钩子
        /// </summary>
        /// <param name="hhk"></param>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 函数功能:该函数将一虚拟键码翻译(映射)成一扫描码或一字符值，或者将一扫描码翻译成一虚拟键码
        /// </summary>
        /// <param name="uCode"></param>
        /// <param name="uMapType"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern uint MapVirtualKey(uint uCode, uint uMapType);

        /// <summary>
        /// 获取全局钩子HookID
        /// </summary>
        /// <param name="hooksMethod">钩子方法类型</param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static IntPtr GetHookID(int hooksMethod, GlobalHookProc callback)
        {
            IntPtr hookID;
            using (var currentProcess = Process.GetCurrentProcess())
            using (var currentModule = currentProcess.MainModule)
            {
                var handle = GetModuleHandle(currentModule.ModuleName);
                hookID = SetWindowsHookEx(hooksMethod, callback, handle, 0);
            }
            return hookID;
        }
    }
}

#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：HooksMethod
* 类 描 述 ：钩子类型
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.WinForm.GlobalHook
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/25 0:24:09
* 更新时间 ：2019/10/25 0:24:09
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
    /// 钩子类型
    /// </summary>
    public enum HooksMethod
    {
        ///// <summary>
        ///// 【不常用】安装一个挂钩处理过程, 以监视由对话框、消息框、菜单条、或滚动条中的输入事件引发的消息.详情参见MessageProc挂钩处理过程.
        ///// </summary>
        //WH_MSGFILTER = -1,
        ///// <summary>
        ///// 【不常用】安装一个挂钩处理过程, 对寄送至系统消息队列的输入消息进行纪录.详情参见JournalRecordProc挂钩处理过程.
        ///// </summary>
        //WH_JOURNALRECORD = 0,
        ///// <summary>
        ///// 【不常用】安装一个挂钩处理过程, 对此前由WH_JOURNALRECORD 挂钩处理过程纪录的消息进行寄送.详情参见 JournalPlaybackProc挂钩处理过程.
        ///// </summary>
        //WH_JOURNALPLAYBACK = 1,
        ///// <summary>
        ///// 安装一个挂钩处理过程对击键消息进行监视. 详情参见KeyboardProc挂钩处理过程.
        ///// </summary>
        //WH_KEYBOARD = 2,
        ///// <summary>
        ///// 【不常用】安装一个挂钩处理过程对寄送至消息队列的消息进行监视, 详情参见 GetMsgProc 挂钩处理过程.
        ///// </summary>
        //WH_GETMESSAGE = 3,
        ///// <summary>
        ///// 【不常用】安装一个挂钩处理过程, 在系统将消息发送至目标窗口处理过程之前, 对该消息进行监视, 详情参见CallWndProc挂钩处理过程.
        ///// </summary>
        //WH_CALLWNDPROC = 4,
        ///// <summary>
        ///// 【不常用】安装一个挂钩处理过程, 接受对CBT应用程序有用的消息, 详情参见 CBTProc 挂钩处理过程.
        ///// </summary>
        //WH_CBT = 5,
        ///// <summary>
        ///// 【不常用】安装一个挂钩处理过程, 以监视由对话框、消息框、菜单条、或滚动条中的输入事件引发的消息.
        ///// 这个挂钩处理过程对系统中所有应用程序的这类消息都进行监视.详情参见 SysMsgProc挂钩处理过程.
        ///// </summary>
        //WH_SYSMSGFILTER = 6,
        ///// <summary>
        ///// 安装一个挂钩处理过程, 对鼠标消息进行监视. 详情参见 MouseProc挂钩处理过程.
        ///// </summary>
        //WH_MOUSE = 7,
        ///// <summary>
        ///// 【不常用】安装一个挂钩处理过程以便对其他挂钩处理过程进行调试, 详情参见DebugProc挂钩处理过程.
        ///// </summary>
        //WH_DEBUG = 9,
        ///// <summary>
        ///// 【不常用】安装一个挂钩处理过程以接受对外壳应用程序有用的通知, 详情参见 ShellProc挂钩处理过程.
        ///// </summary>
        //WH_SHELL = 10,
        ///// <summary>
        ///// 【不常用】安装一个挂钩处理过程, 该挂钩处理过程当应用程序的前台线程即将进入空闲状态时被调用, 它有助于在空闲时间内执行低优先级的任务.
        ///// </summary>
        //WH_FOREGROUNDIDLE = 11,
        ///// <summary>
        ///// 【不常用】安装一个挂钩处理过程, 它对已被目标窗口处理过程处理过了的消息进行监视, 详情参见 CallWndRetProc 挂钩处理过程.
        ///// </summary>
        //WH_CALLWNDPROCRET = 12,
        /// <summary>
        /// 键盘钩子 此挂钩只能在Windows NT中被安装, 用来对底层的键盘输入事件进行监视.详情参见LowLevelKeyboardProc挂钩处理过程.
        /// </summary>
        WH_KEYBOARD_LL = 13,
        /// <summary>
        /// 鼠标钩子 此挂钩只能在Windows NT中被安装, 用来对底层的鼠标输入事件进行监视.详情参见LowLevelMouseProc挂钩处理过程.
        /// </summary>
        WH_MOUSE_LL = 14,
    }
}

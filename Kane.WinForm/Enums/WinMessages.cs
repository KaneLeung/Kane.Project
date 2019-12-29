#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：WinMessages
* 类 描 述 ：Window消息枚举类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.WinForm
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/12/6 22:09:23
* 更新时间 ：2019/12/6 22:09:23
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Kane.WinForm
{
    /// <summary>
    /// Window消息枚举类
    /// </summary>
    public enum WinMessages : int
    {

        WM_NULL = 0x0000,
        [Description("应用程序创建一个窗口")]
        WM_CREATE = 0x0001,
        [Description("一个窗口被销毁")]
        WM_DESTROY = 0x0002,
        [Description("移动一个窗口")]
        WM_MOVE = 0x0003,
        [Description("改变一个窗口的大小")]
        WM_SIZE = 0x0005,
        [Description("一个窗口被激活或失去激活状态")]
        WM_ACTIVATE = 0x0006,
        [Description("获得焦点后")]
        WM_SETFOCUS = 0x0007,
        [Description("失去焦点")]
        WM_KILLFOCUS = 0x0008,
        [Description("改变Enable状态")]
        WM_ENABLE = 0x000A,
        [Description("设置窗口是否能重画")]
        WM_SETREDRAW = 0x000B,
        [Description("应用程序发送此消息来设置一个窗口的文本")]
        WM_SETTEXT = 0x000C,
        [Description("应用程序发送此消息来复制对应窗口的文本到缓冲区")]
        WM_GETTEXT = 0x000D,
        [Description("得到与一个窗口有关的文本的长度（不包含空字符）")]
        WM_GETTEXTLENGTH = 0x000E,
        [Description("要求一个窗口重画自己")]
        WM_PAINT = 0x000F,
        [Description("当一个窗口或应用程序要关闭时发送一个信号")]
        WM_CLOSE = 0x0010,
        [Description("当用户选择结束对话框或程序自己调用ExitWindows函数")]
        WM_QUERYENDSESSION = 0x0011,
        [Description("用来结束程序运行或当程序调用Postquitmessage函数")]
        WM_QUIT = 0x0012,
        [Description("当用户窗口恢复以前的大小位置时，把此消息发送给某个图标")]
        WM_QUERYOPEN = 0x0013,
        [Description("当窗口背景必须被擦除时（例在窗口改变大小时）")]
        WM_ERASEBKGND = 0x0014,
        [Description("当系统颜色改变时，发送此消息给所有顶级窗口")]
        WM_SYSCOLORCHANGE = 0x0015,
        [Description("当系统进程发出WM_QUERYENDSESSION消息后,此消息发送给应用程序,通知它对话是否结束")]
        WM_ENDSESSION = 0x0016,
        [Description("当隐藏或显示窗口是发送此消息给这个窗口")]
        WM_SHOWWINDOW = 0x0018,
        [Description("***(WM_WININICHANGE)***")]
        WM_WININICHANGE = 0x001A,
        [Description("***(WM_SETTINGCHANGE)***")]
        WM_SETTINGCHANGE = 0x001A,
        [Description("***(WM_DEVMODECHANGE)***")]
        WM_DEVMODECHANGE = 0x001B,
        [Description("发此消息给应用程序哪个窗口是激活的，哪个是非激活的")]
        WM_ACTIVATEAPP = 0x001C,
        [Description("当系统的字体资源库变化时发送此消息给所有顶级窗口")]
        WM_FONTCHANGE = 0x001D,
        [Description("当系统的时间变化时发送此消息给所有顶级窗口")]
        WM_TIMECHANGE = 0x001E,
        [Description("发送此消息来取消某种正在进行的摸态（操作）")]
        WM_CANCELMODE = 0x001F,
        [Description(" 如果鼠标引起光标在某个窗口中移动且鼠标输入没有被捕获时，就发消息给某个窗口")]
        WM_SETCURSOR = 0x0020,
        [Description("当光标在某个非激活的窗口中而用户正按着鼠标的某个键发送此消息给当前窗口")]
        WM_MOUSEACTIVATE = 0x0021,
        [Description("发送此消息给MDI子窗口当用户点击此窗口的标题栏， 或当窗口被激活，移动，改变大小")]
        WM_CHILDACTIVATE = 0x0022,
        [Description("此消息由基于计算机的训练程序发送，通过WH_JOURNALPALYBACK的hook程序分离出用户输入消息")]
        WM_QUEUESYNC = 0x0023,
        [Description("此消息发送给窗口当它将要改变大小或位置")]
        WM_GETMINMAXINFO = 0x0024,
        [Description("发送给最小化窗口当它图标将要被重画")]
        WM_PAINTICON = 0x0026,
        [Description("此消息发送给某个最小化窗口，仅当它在画图标前它的背景必须被重画")]
        WM_ICONERASEBKGND = 0x0027,
        [Description("发送此消息给一个对话框程序去更改焦点位置")]
        WM_NEXTDLGCTL = 0x0028,
        [Description("每当打印管理列队增加或减少一条作业时发出此消息")]
        WM_SPOOLERSTATUS = 0x002A,
        [Description("当Button，Combobox，Listbox，Menu的可视外观改变时发送此消息给这些空件的所有者")]
        WM_DRAWITEM = 0x002B,
        [Description("当Button, Combobox, Listbox, Listviewcontrol, Ormenuitem被创建时发送此消息 给控件的所有者")]
        WM_MEASUREITEM = 0x002C,
        [Description("当Listbox或Combobox被销毁或当某些项被删除通过LB_DELETESTRING, LB_RESETCONTENT, CB_DELETESTRING, or CB_RESETCONTENT消息")]
        WM_DELETEITEM = 0x002D,
        [Description("此消息有一个LBS_WANTKEYBOARDINPUT风格的发出给它的所有者来响应WM_KEYDOWN消息")]
        WM_VKEYTOITEM = 0x002E,
        [Description("此消息由一个LBS_WANTKEYBOARDINPUT风格的列表框发送给他的所有者来响应WM_CHAR消息")]
        WM_CHARTOITEM = 0x002F,
        [Description("当绘制文本时程序发送此消息得到控件要用的颜色")]
        WM_SETFONT = 0x0030,
        [Description("应用程序发送此消息得到当前控件绘制文本的字体")]
        WM_GETFONT = 0x0031,
        [Description("应用程序发送此消息让一个窗口与一个热键相关连")]
        WM_SETHOTKEY = 0x0032,
        [Description("应用程序发送此消息来判断热键与某个窗口是否有关联")]
        WM_GETHOTKEY = 0x0033,
        [Description("此消息发送给最小化窗口，当此窗口将要被拖放而它的类中没有定义图标应用程序能返回一个图标或光标的句柄，当用户拖放图标时系统显示这个图标或光标")]
        WM_QUERYDRAGICON = 0x0037,
        [Description("发送此消息来判定combobox或listbox新增加的项的相对位置")]
        WM_COMPAREITEM = 0x0039,
        [Description("***(WM_GETOBJECT)***")]
        WM_GETOBJECT = 0x003D,
        [Description("***(WM_COMPACTING)***")]
        WM_COMPACTING = 0x0041,
        [Description("***(WM_COMMNOTIFY)***")]
        WM_COMMNOTIFY = 0x0044,
        [Description("发送此消息给那个窗口的大小和位置将要被改变时，来调用setwindowpos函数或其它窗口管理函数")]
        WM_WINDOWPOSCHANGING = 0x0046,
        [Description("发送此消息给那个窗口的大小和位置已经被改变时，来调用setwindowpos函数或其它窗口管理函数")]
        WM_WINDOWPOSCHANGED = 0x0047,
        [Description(" (适用于16位的windows） 当系统将要进入暂停状态时发送此消息")]
        WM_POWER = 0x0048,
        [Description("当一个应用程序传递数据给另一个应用程序时发送此消息")]
        WM_COPYDATA = 0x004A,
        [Description("当某个用户取消程序日志激活状态，提交此消息给程序")]
        WM_CANCELJOURNAL = 0x004B,
        [Description("当某个控件的某个事件已经发生或这个控件需要得到一些信息时，发送此消息给它的父窗口")]
        WM_NOTIFY = 0x004E,
        [Description("当用户选择某种输入语言，或输入语言的热键改变")]
        WM_INPUTLANGCHANGEREQUEST = 0x0050,
        [Description("当平台现场已经被改变后发送此消息给受影响的最顶级窗口")]
        WM_INPUTLANGCHANGE = 0x0051,
        [Description("当程序已经初始化windows帮助例程时发送此消息给应用程序")]
        WM_TCARD = 0x0052,
        [Description("此消息显示用户按下了F1，如果某个菜单是激活的，就发送此消息个此窗口关联的菜单,否则就发送给有焦点的窗口，如果当前都没有焦点，就把此消息发送给当前激活的窗口")]
        WM_HELP = 0x0053,
        [Description("当用户已经登入或退出后发送此消息给所有的窗口，当用户登入或退出时系统更新用户的具体设置信息，在用户更新设置时系统马上发送此消息")]
        WM_USERCHANGED = 0x0054,
        [Description("公用控件，自定义控件和他们的父窗口通过此消息来判断控件是使用ANSI还是UNICODE结构在WM_NOTIFY消息，使用此控件能使某个控件与它的父控件之间进行相互通信")]
        WM_NOTIFYFORMAT = 0x0055,
        [Description("当用户某个窗口中点击了一下右键就发送此消息给这个窗口")]
        WM_CONTEXTMENU = 0x007B,
        [Description("当调用SETWINDOWLONG函数将要改变一个或多个窗口的风格时发送此消息给那个窗口")]
        WM_STYLECHANGING = 0x007C,
        [Description("当调用SETWINDOWLONG函数一个或多个窗口的风格后发送此消息给那个窗口")]
        WM_STYLECHANGED = 0x007D,
        [Description("当显示器的分辨率改变后发送此消息给所有的窗口")]
        WM_DISPLAYCHANGE = 0x007E,
        [Description("此消息发送给某个窗口来返回与某个窗口有关连的大图标或小图标的句柄")]
        WM_GETICON = 0x007F,
        [Description("程序发送此消息让一个新的大图标或小图与某个窗口关联")]
        WM_SETICON = 0x0080,
        [Description("当某个窗口第一次被创建时，此消息在WM_CREATE消息发送前发送")]
        WM_NCCREATE = 0x0081,
        [Description("此消息通知某个窗口，非客户区正在销毁")]
        WM_NCDESTROY = 0x0082,
        [Description("当某个窗口的客户区域必须被核算时发送此消息")]
        WM_NCCALCSIZE = 0x0083,
        [Description("移动鼠标，按住或释放鼠标时发生")]
        WM_NCHITTEST = 0x0084,
        [Description("程序发送此消息给某个窗口当它（窗口）的框架必须被绘制时")]
        WM_NCPAINT = 0x0085,
        [Description("此消息发送给某个窗口仅当它的非客户区需要被改变来显示是激活还是非激活状态")]
        WM_NCACTIVATE = 0x0086,
        [Description("发送此消息给某个与对话框程序关联的控件， windows控制方位键和TAB键使输入进入 此控件通过响应WM_GETDLGCODE消息，应用程序可以把他当成一个特殊的输入控件并能处理它")]
        WM_GETDLGCODE = 0x0087,
        [Description("***(WM_SYNCPAINT)***")]
        WM_SYNCPAINT = 0x0088,
        [Description("当光标在一个窗口的非客户区内移动时发送此消息给这个窗口,非客户区为：窗体的标题栏及窗的边框体")]
        WM_NCMOUSEMOVE = 0x00A0,
        [Description("当光标在一个窗口的非客户区同时按下鼠标左键时提交此消息")]
        WM_NCLBUTTONDOWN = 0x00A1,
        [Description("当用户释放鼠标左键同时光标某个窗口在非客户区时发送此消息")]
        WM_NCLBUTTONUP = 0x00A2,
        [Description("当用户双击鼠标左键同时光标某个窗口在非客户区时发送此消息")]
        WM_NCLBUTTONDBLCLK = 0x00A3,
        [Description("当用户按下鼠标右键同时光标又在窗口的非客户区时发送此消息")]
        WM_NCRBUTTONDOWN = 0x00A4,
        [Description("当用户释放鼠标右键同时光标又在窗口的非客户区时发送此消息")]
        WM_NCRBUTTONUP = 0x00A5,
        [Description("当用户双击鼠标右键同时光标某个窗口在非客户区时发送此消息")]
        WM_NCRBUTTONDBLCLK = 0x00A6,
        [Description("当用户按下鼠标中键同时光标又在窗口的非客户区时发送此消息")]
        WM_NCMBUTTONDOWN = 0x00A7,
        [Description("当用户释放鼠标中键同时光标又在窗口的非客户区时发送此消息")]
        WM_NCMBUTTONUP = 0x00A8,
        [Description("当用户双击鼠标中键同时光标又在窗口的非客户区时发送此消息")]
        WM_NCMBUTTONDBLCLK = 0x00A9,
        [Description("***(WM_NCXBUTTONDOWN)***")]
        WM_NCXBUTTONDOWN = 0x00AB,
        [Description("***(WM_NCXBUTTONUP)***")]
        WM_NCXBUTTONUP = 0x00AC,
        [Description("***(WM_NCXBUTTONDBLCLK)***")]
        WM_NCXBUTTONDBLCLK = 0x00AD,
        [Description("***(WM_INPUT)***")]
        WM_INPUT = 0x00FF,
        [Description("***(WM_KEYFIRST)***")]
        WM_KEYFIRST = 0x0100,
        [Description("按下一个键")]
        WM_KEYDOWN = 0x0100,
        [Description("释放一个键")]
        WM_KEYUP = 0x0101,
        [Description("按下某键，并已发出WM_KEYDOWN，WM_KEYUP消息")]
        WM_CHAR = 0x0102,
        [Description("当用translatemessage函数翻译WM_KEYUP消息时发送此消息给拥有焦点的窗口")]
        WM_DEADCHAR = 0x0103,
        [Description("当用户按住ALT键同时按下其它键时提交此消息给拥有焦点的窗口")]
        WM_SYSKEYDOWN = 0x0104,
        [Description("当用户释放一个键同时ALT键还按着时提交此消息给拥有焦点的窗口")]
        WM_SYSKEYUP = 0x0105,
        [Description("当WM_SYSKEYDOWN消息被TRANSLATEMESSAGE函数翻译后提交此消息给拥有焦点的窗口")]
        WM_SYSCHAR = 0x0106,
        [Description("当WM_SYSKEYDOWN消息被TRANSLATEMESSAGE函数翻译后发送此消息给拥有焦点的窗口")]
        WM_SYSDEADCHAR = 0x0107,
        [Description("***(WM_UNICHAR)***")]
        WM_UNICHAR = 0x0109,
        [Description("***(WM_KEYLAST_NT501)***")]
        WM_KEYLAST_NT501 = 0x0109,
        [Description("***(UNICODE_NOCHAR)***")]
        UNICODE_NOCHAR = 0xFFFF,
        [Description("***(WM_KEYLAST_PRE501)***")]
        WM_KEYLAST_PRE501 = 0x0108,
        [Description("***(WM_IME_STARTCOMPOSITION)***")]
        WM_IME_STARTCOMPOSITION = 0x010D,
        [Description("***(WM_IME_ENDCOMPOSITION)***")]
        WM_IME_ENDCOMPOSITION = 0x010E,
        [Description("***(WM_IME_COMPOSITION)***")]
        WM_IME_COMPOSITION = 0x010F,
        [Description("***(WM_IME_KEYLAST)***")]
        WM_IME_KEYLAST = 0x010F,
        [Description("在一个对话框程序被显示前发送此消息给它,常用此消息初始化控件和执行其它任务")]
        WM_INITDIALOG = 0x0110,
        [Description("当用户选择一条菜单命令项或当某个控件发送一条消息给它的父窗口，一个快捷键被翻译")]
        WM_COMMAND = 0x0111,
        [Description("当用户选择窗口菜单的一条命令或当用户选择最大化或最小化时那个窗口会收到此消息")]
        WM_SYSCOMMAND = 0x0112,
        [Description("发生了定时器事件")]
        WM_TIMER = 0x0113,
        [Description("当一个窗口标准水平滚动条产生一个滚动事件时发送此消息给那个窗口，也发送给拥有它的控件")]
        WM_HSCROLL = 0x0114,
        [Description("当一个窗口标准垂直滚动条产生一个滚动事件时发送此消息给那个窗口也，发送给拥有它的控件")]
        WM_VSCROLL = 0x0115,
        [Description("当一个菜单将要被激活时发送此消息，它发生在用户菜单条中的某项或按下某个菜单键，它允许程序在显示前更改菜单")]
        WM_INITMENU = 0x0116,
        [Description("当一个下拉菜单或子菜单将要被激活时发送此消息，它允许程序在它显示前更改菜单，而不要改变全部")]
        WM_INITMENUPOPUP = 0x0117,
        [Description("当用户选择一条菜单项时发送此消息给菜单的所有者（一般是窗口）")]
        WM_MENUSELECT = 0x011F,
        [Description("当菜单已被激活用户按下了某个键（不同于加速键），发送此消息给菜单的所有者")]
        WM_MENUCHAR = 0x0120,
        [Description("当一个模态对话框或菜单进入空载状态时发送此消息给它的所有者，一个模态对话框或菜单进入空载状态就是在处理一条或几条先前的消息后没有消息它的列队中等待")]
        WM_ENTERIDLE = 0x0121,
        [Description("***(WM_MENURBUTTONUP)***")]
        WM_MENURBUTTONUP = 0x0122,
        [Description("***(WM_MENUDRAG)***")]
        WM_MENUDRAG = 0x0123,
        [Description("***(WM_MENUGETOBJECT)***")]
        WM_MENUGETOBJECT = 0x0124,
        [Description("***(WM_UNINITMENUPOPUP)***")]
        WM_UNINITMENUPOPUP = 0x0125,
        [Description("***(WM_MENUCOMMAND)***")]
        WM_MENUCOMMAND = 0x0126,
        [Description("***(WM_CHANGEUISTATE)***")]
        WM_CHANGEUISTATE = 0x0127,
        [Description("***(WM_UPDATEUISTATE)***")]
        WM_UPDATEUISTATE = 0x0128,
        [Description("***(WM_QUERYUISTATE)***")]
        WM_QUERYUISTATE = 0x0129,
        [Description("在windows绘制消息框前发送此消息给消息框的所有者窗口，通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置消息框的文本和背景颜色")]
        WM_CTLCOLORMSGBOX = 0x0132,
        [Description("当一个编辑型控件将要被绘制时发送此消息给它的父窗口:通过响应这条消息,所有者窗口可以通过使用给定的相关显示设备的句柄来设置编辑框的文本和背景颜色")]
        WM_CTLCOLOREDIT = 0x0133,
        [Description("当一个列表框控件将要被绘制前发送此消息给它的父窗口；通过响应这条息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置列表框的文本和背景颜色")]
        WM_CTLCOLORLISTBOX = 0x0134,
        [Description("当一个按钮控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置按纽的文本和背景颜色")]
        WM_CTLCOLORBTN = 0x0135,
        [Description("当一个对话框控件将要被绘制前发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置对话框的文本背景颜色")]
        WM_CTLCOLORDLG = 0x0136,
        [Description("当一个滚动条控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置滚动条的背景颜色")]
        WM_CTLCOLORSCROLLBAR = 0x0137,
        [Description("当一个静态控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置静态控件的文本和背景颜色")]
        WM_CTLCOLORSTATIC = 0x0138,
        [Description("***(WM_MOUSEFIRST)***")]
        WM_MOUSEFIRST = 0x0200,
        [Description("移动鼠标")]
        WM_MOUSEMOVE = 0x0200,
        [Description("按下鼠标左键")]
        WM_LBUTTONDOWN = 0x0201,
        [Description("释放鼠标左键")]
        WM_LBUTTONUP = 0x0202,
        [Description("双击鼠标左键")]
        WM_LBUTTONDBLCLK = 0x0203,
        [Description("按下鼠标右键")]
        WM_RBUTTONDOWN = 0x0204,
        [Description("释放鼠标右键")]
        WM_RBUTTONUP = 0x0205,
        [Description("双击鼠标右键")]
        WM_RBUTTONDBLCLK = 0x0206,
        [Description("按下鼠标中键")]
        WM_MBUTTONDOWN = 0x0207,
        [Description("释放鼠标中键")]
        WM_MBUTTONUP = 0x0208,
        [Description("双击鼠标中键")]
        WM_MBUTTONDBLCLK = 0x0209,
        [Description("当鼠标轮子转动时发送此消息个当前有焦点的控件")]
        WM_MOUSEWHEEL = 0x020A,
        [Description("***(WM_XBUTTONDOWN)***")]
        WM_XBUTTONDOWN = 0x020B,
        [Description("***(WM_XBUTTONUP)***")]
        WM_XBUTTONUP = 0x020C,
        [Description("***(WM_XBUTTONDBLCLK)***")]
        WM_XBUTTONDBLCLK = 0x020D,
        [Description("***(WM_MOUSELAST_5)***")]
        WM_MOUSELAST_5 = 0x020D,
        [Description("***(WM_MOUSELAST_4)***")]
        WM_MOUSELAST_4 = 0x020A,
        [Description("***(WM_MOUSELAST_PRE_4)***")]
        WM_MOUSELAST_PRE_4 = 0x0209,
        [Description("当MDI子窗口被创建或被销毁，或用户按了一下鼠标键而光标在子窗口上时发送此消息给它的父窗口")]
        WM_PARENTNOTIFY = 0x0210,
        [Description("发送此消息通知应用程序的主窗口已经进入了菜单循环模式")]
        WM_ENTERMENULOOP = 0x0211,
        [Description("发送此消息通知应用程序的主窗口已退出了菜单循环模式")]
        WM_EXITMENULOOP = 0x0212,
        [Description("***(WM_NEXTMENU)***")]
        WM_NEXTMENU = 0x0213,
        [Description("当用户正在调整窗口大小时发送此消息给窗口；通过此消息应用程序可以监视窗口大小和位置也可以修改他们")]
        WM_SIZING = 0x0214,
        [Description("发送此消息给窗口当它失去捕获的鼠标时")]
        WM_CAPTURECHANGED = 0x0215,
        [Description("当用户在移动窗口时发送此消息，通过此消息应用程序可以监视窗口大小和位置也可以修改他们")]
        WM_MOVING = 0x0216,
        [Description("此消息发送给应用程序来通知它有关电源管理事件")]
        WM_POWERBROADCAST = 0x0218,
        [Description("当设备的硬件配置改变时发送此消息给应用程序或设备驱动程序")]
        WM_DEVICECHANGE = 0x0219,
        [Description("应用程序发送此消息给多文档的客户窗口来创建一个MDI子窗口")]
        WM_MDICREATE = 0x0220,
        [Description("应用程序发送此消息给多文档的客户窗口来关闭一个MDI子窗口")]
        WM_MDIDESTROY = 0x0221,
        [Description("应用程序发送此消息给多文档的客户窗口通知客户窗口激活另一个MDI子窗口，当客户窗口收到此消息后，它发出WM_MDIACTIVE消息给MDI子窗口（未激活）激活它")]
        WM_MDIACTIVATE = 0x0222,
        [Description("程序发送此消息给MDI客户窗口让子窗口从最大最小化恢复293. 到原来大小")]
        WM_MDIRESTORE = 0x0223,
        [Description("程序发送此消息给MDI客户窗口激活下一个或前一个窗口")]
        WM_MDINEXT = 0x0224,
        [Description("程序发送此消息给MDI客户窗口来最大化一个MDI子窗口")]
        WM_MDIMAXIMIZE = 0x0225,
        [Description("程序发送此消息给MDI客户窗口以平铺方式重新排列所有MDI子窗口")]
        WM_MDITILE = 0x0226,
        [Description("程序发送此消息给MDI客户窗口以层叠方式重新排列所有MDI子窗口")]
        WM_MDICASCADE = 0x0227,
        [Description("程序发送此消息给MDI客户窗口重新排列所有最小化的MDI子窗口")]
        WM_MDIICONARRANGE = 0x0228,
        [Description("程序发送此消息给MDI客户窗口来找到激活的子窗口的句柄")]
        WM_MDIGETACTIVE = 0x0229,
        [Description("程序发送此消息给MDI客户窗口用MDI菜单代替子窗口的菜单")]
        WM_MDISETMENU = 0x0230,
        [Description("***(WM_ENTERSIZEMOVE)***")]
        WM_ENTERSIZEMOVE = 0x0231,
        [Description("***(WM_EXITSIZEMOVE)***")]
        WM_EXITSIZEMOVE = 0x0232,
        [Description("***(WM_DROPFILES)***")]
        WM_DROPFILES = 0x0233,
        [Description("***(WM_MDIREFRESHMENU)***")]
        WM_MDIREFRESHMENU = 0x0234,
        [Description("***(WM_IME_SETCONTEXT)***")]
        WM_IME_SETCONTEXT = 0x0281,
        [Description("***(WM_IME_NOTIFY)***")]
        WM_IME_NOTIFY = 0x0282,
        [Description("***(WM_IME_CONTROL)***")]
        WM_IME_CONTROL = 0x0283,
        [Description("***(WM_IME_COMPOSITIONFULL)***")]
        WM_IME_COMPOSITIONFULL = 0x0284,
        [Description("***(WM_IME_SELECT)***")]
        WM_IME_SELECT = 0x0285,
        [Description("***(WM_IME_CHAR)***")]
        WM_IME_CHAR = 0x0286,
        [Description("***(WM_IME_REQUEST)***")]
        WM_IME_REQUEST = 0x0288,
        [Description("***(WM_IME_KEYDOWN)***")]
        WM_IME_KEYDOWN = 0x0290,
        [Description("***(WM_IME_KEYUP)***")]
        WM_IME_KEYUP = 0x0291,
        [Description("***(WM_MOUSEHOVER)***")]
        WM_MOUSEHOVER = 0x02A1,
        [Description("***(WM_MOUSELEAVE)***")]
        WM_MOUSELEAVE = 0x02A3,
        [Description("***(WM_NCMOUSEHOVER)***")]
        WM_NCMOUSEHOVER = 0x02A0,
        [Description("***(WM_NCMOUSELEAVE)***")]
        WM_NCMOUSELEAVE = 0x02A2,
        [Description("***(WM_WTSSESSION_CHANGE)***")]
        WM_WTSSESSION_CHANGE = 0x02B1,
        [Description("***(WM_TABLET_FIRST)***")]
        WM_TABLET_FIRST = 0x02c0,
        [Description("***(WM_TABLET_LAST)***")]
        WM_TABLET_LAST = 0x02df,
        [Description("程序发送此消息给一个编辑框或Combobox来删除当前选择的文本")]
        WM_CUT = 0x0300,
        [Description("程序发送此消息给一个编辑框或Combobox来复制当前选择的文本到剪贴板")]
        WM_COPY = 0x0301,
        [Description("程序发送此消息给Editcontrol或Combobox从剪贴板中得到数据")]
        WM_PASTE = 0x0302,
        [Description("程序发送此消息给Editcontrol或Combobox清除当前选择的内容")]
        WM_CLEAR = 0x0303,
        [Description("程序发送此消息给Editcontrol或Combobox撤消最后一次操作")]
        WM_UNDO = 0x0304,
        [Description("***(WM_RENDERFORMAT)***")]
        WM_RENDERFORMAT = 0x0305,
        [Description("***(WM_RENDERALLFORMATS)***")]
        WM_RENDERALLFORMATS = 0x0306,
        [Description("当调用ENPTYCLIPBOARD函数时发送此消息给剪贴板的所有者")]
        WM_DESTROYCLIPBOARD = 0x0307,
        [Description("当剪贴板的内容变化时发送此消息给剪贴板观察链的第一个窗口；它允许用剪贴板观察窗口来显示剪贴板的新内容")]
        WM_DRAWCLIPBOARD = 0x0308,
        [Description("当剪贴板包含CF_OWNERDIPLAY格式的数据并且剪贴板观察窗口的客户区需要重画")]
        WM_PAINTCLIPBOARD = 0x0309,
        [Description("***(WM_VSCROLLCLIPBOARD)***")]
        WM_VSCROLLCLIPBOARD = 0x030A,
        [Description("当剪贴板包含CF_OWNERDIPLAY格式的数据并且剪贴板观察窗口的客户区域的大小已经改变是此消息通过剪贴板观察窗口发送给剪贴板的所有者")]
        WM_SIZECLIPBOARD = 0x030B,
        [Description("通过剪贴板观察窗口发送此消息给剪贴板的所有者来请求一个CF_OWNERDISPLAY格式的剪贴板的名字")]
        WM_ASKCBFORMATNAME = 0x030C,
        [Description("当一个窗口从剪贴板观察链中移去时发送此消息给剪贴板观察链的第一个窗口")]
        WM_CHANGECBCHAIN = 0x030D,
        [Description("此消息通过一个剪贴板观察窗口发送给剪贴板的所有者；它发生在当剪贴板包 CFOWNERDISPALY格式的数据并且有个事件在剪贴板观察窗的水平滚动条上；所有者应滚动剪贴板图象并更新滚动条的值")]
        WM_HSCROLLCLIPBOARD = 0x030E,
        [Description("此消息发送给将要收到焦点的窗口，此消息能使窗口在收到焦点时同时有机会实 现他的逻辑调色板")]
        WM_QUERYNEWPALETTE = 0x030F,
        [Description("当一个应用程序正要实现它的逻辑调色板时发此消息通知所有的应用程序")]
        WM_PALETTEISCHANGING = 0x0310,
        [Description("此消息在一个拥有焦点的窗口实现它的逻辑调色板后发送此消息给所有顶级并重叠的窗口，以此来改变系统调色板")]
        WM_PALETTECHANGED = 0x0311,
        [Description("当用户按下由REGISTERHOTKEY函数注册的热键时提交此消息")]
        WM_HOTKEY = 0x0312,
        [Description("应用程序发送此消息仅当WINDOWS或其它应用程序发出一个请求要求绘制一个应用程序的一部分")]
        WM_PRINT = 0x0317,
        [Description("***(WM_PRINTCLIENT)***")]
        WM_PRINTCLIENT = 0x0318,
        [Description("***(WM_APPCOMMAND)***")]
        WM_APPCOMMAND = 0x0319,
        [Description("***(WM_THEMECHANGED)***")]
        WM_THEMECHANGED = 0x031A,
        [Description("***(WM_HANDHELDFIRST)***")]
        WM_HANDHELDFIRST = 0x0358,
        [Description("***(WM_HANDHELDLAST)***")]
        WM_HANDHELDLAST = 0x035F,
        [Description("***(WM_AFXFIRST)***")]
        WM_AFXFIRST = 0x0360,
        [Description("***(WM_AFXLAST)***")]
        WM_AFXLAST = 0x037F,
        [Description("***(WM_PENWINFIRST)***")]
        WM_PENWINFIRST = 0x0380,
        [Description("***(WM_PENWINLAST)***")]
        WM_PENWINLAST = 0x038F,
        [Description("***(WM_APP)***")]
        WM_APP = 0x8000,
        [Description("***(WM_USER)***")]
        WM_USER = 0x0400,
        [Description("***(EM_GETSEL)***")]
        EM_GETSEL = 0x00B0,
        EM_SETSEL = 0x00B1,
        EM_GETRECT = 0x00B2,
        EM_SETRECT = 0x00B3,
        EM_SETRECTNP = 0x00B4,
        EM_SCROLL = 0x00B5,
        EM_LINESCROLL = 0x00B6,
        EM_SCROLLCARET = 0x00B7,
        EM_GETMODIFY = 0x00B8,
        EM_SETMODIFY = 0x00B9,
        EM_GETLINECOUNT = 0x00BA,
        EM_LINEINDEX = 0x00BB,
        EM_SETHANDLE = 0x00BC,
        EM_GETHANDLE = 0x00BD,
        EM_GETTHUMB = 0x00BE,
        EM_LINELENGTH = 0x00C1,
        EM_REPLACESEL = 0x00C2,
        EM_GETLINE = 0x00C4,
        EM_LIMITTEXT = 0x00C5,
        EM_CANUNDO = 0x00C6,
        EM_UNDO = 0x00C7,
        EM_FMTLINES = 0x00C8,
        EM_LINEFROMCHAR = 0x00C9,
        EM_SETTABSTOPS = 0x00CB,
        EM_SETPASSWORDCHAR = 0x00CC,
        EM_EMPTYUNDOBUFFER = 0x00CD,
        EM_GETFIRSTVISIBLELINE = 0x00CE,
        EM_SETREADONLY = 0x00CF,
        EM_SETWORDBREAKPROC = 0x00D0,
        EM_GETWORDBREAKPROC = 0x00D1,
        EM_GETPASSWORDCHAR = 0x00D2,
        EM_SETMARGINS = 0x00D3,
        EM_GETMARGINS = 0x00D4,
        EM_SETLIMITTEXT = EM_LIMITTEXT,
        EM_GETLIMITTEXT = 0x00D5,
        EM_POSFROMCHAR = 0x00D6,
        EM_CHARFROMPOS = 0x00D7,
        EM_SETIMESTATUS = 0x00D8,
        EM_GETIMESTATUS = 0x00D9,
        BM_GETCHECK = 0x00F0,
        BM_SETCHECK = 0x00F1,
        BM_GETSTATE = 0x00F2,
        BM_SETSTATE = 0x00F3,
        BM_SETSTYLE = 0x00F4,
        BM_CLICK = 0x00F5,
        BM_GETIMAGE = 0x00F6,
        BM_SETIMAGE = 0x00F7,
        STM_SETICON = 0x0170,
        STM_GETICON = 0x0171,
        STM_SETIMAGE = 0x0172,
        STM_GETIMAGE = 0x0173,
        STM_MSGMAX = 0x0174,
        DM_GETDEFID = (WM_USER + 0),
        DM_SETDEFID = (WM_USER + 1),
        DM_REPOSITION = (WM_USER + 2),
        LB_ADDSTRING = 0x0180,
        LB_INSERTSTRING = 0x0181,
        LB_DELETESTRING = 0x0182,
        LB_SELITEMRANGEEX = 0x0183,
        LB_RESETCONTENT = 0x0184,
        LB_SETSEL = 0x0185,
        LB_SETCURSEL = 0x0186,
        LB_GETSEL = 0x0187,
        LB_GETCURSEL = 0x0188,
        LB_GETTEXT = 0x0189,
        LB_GETTEXTLEN = 0x018A,
        LB_GETCOUNT = 0x018B,
        LB_SELECTSTRING = 0x018C,
        LB_DIR = 0x018D,
        LB_GETTOPINDEX = 0x018E,
        LB_FINDSTRING = 0x018F,
        LB_GETSELCOUNT = 0x0190,
        LB_GETSELITEMS = 0x0191,
        LB_SETTABSTOPS = 0x0192,
        LB_GETHORIZONTALEXTENT = 0x0193,
        LB_SETHORIZONTALEXTENT = 0x0194,
        LB_SETCOLUMNWIDTH = 0x0195,
        LB_ADDFILE = 0x0196,
        LB_SETTOPINDEX = 0x0197,
        LB_GETITEMRECT = 0x0198,
        LB_GETITEMDATA = 0x0199,
        LB_SETITEMDATA = 0x019A,
        LB_SELITEMRANGE = 0x019B,
        LB_SETANCHORINDEX = 0x019C,
        LB_GETANCHORINDEX = 0x019D,
        LB_SETCARETINDEX = 0x019E,
        LB_GETCARETINDEX = 0x019F,
        LB_SETITEMHEIGHT = 0x01A0,
        LB_GETITEMHEIGHT = 0x01A1,
        LB_FINDSTRINGEXACT = 0x01A2,
        LB_SETLOCALE = 0x01A5,
        LB_GETLOCALE = 0x01A6,
        LB_SETCOUNT = 0x01A7,
        LB_INITSTORAGE = 0x01A8,
        LB_ITEMFROMPOINT = 0x01A9,
        LB_MULTIPLEADDSTRING = 0x01B1,
        LB_GETLISTBOXINFO = 0x01B2,
        LB_MSGMAX_501 = 0x01B3,
        LB_MSGMAX_WCE4 = 0x01B1,
        LB_MSGMAX_4 = 0x01B0,
        LB_MSGMAX_PRE4 = 0x01A8,
        CB_GETEDITSEL = 0x0140,
        CB_LIMITTEXT = 0x0141,
        CB_SETEDITSEL = 0x0142,
        CB_ADDSTRING = 0x0143,
        CB_DELETESTRING = 0x0144,
        CB_DIR = 0x0145,
        CB_GETCOUNT = 0x0146,
        CB_GETCURSEL = 0x0147,
        CB_GETLBTEXT = 0x0148,
        CB_GETLBTEXTLEN = 0x0149,
        CB_INSERTSTRING = 0x014A,
        CB_RESETCONTENT = 0x014B,
        CB_FINDSTRING = 0x014C,
        CB_SELECTSTRING = 0x014D,
        CB_SETCURSEL = 0x014E,
        CB_SHOWDROPDOWN = 0x014F,
        CB_GETITEMDATA = 0x0150,
        CB_SETITEMDATA = 0x0151,
        CB_GETDROPPEDCONTROLRECT = 0x0152,
        CB_SETITEMHEIGHT = 0x0153,
        CB_GETITEMHEIGHT = 0x0154,
        CB_SETEXTENDEDUI = 0x0155,
        CB_GETEXTENDEDUI = 0x0156,
        CB_GETDROPPEDSTATE = 0x0157,
        CB_FINDSTRINGEXACT = 0x0158,
        CB_SETLOCALE = 0x0159,
        CB_GETLOCALE = 0x015A,
        CB_GETTOPINDEX = 0x015B,
        CB_SETTOPINDEX = 0x015C,
        CB_GETHORIZONTALEXTENT = 0x015d,
        CB_SETHORIZONTALEXTENT = 0x015e,
        CB_GETDROPPEDWIDTH = 0x015f,
        CB_SETDROPPEDWIDTH = 0x0160,
        CB_INITSTORAGE = 0x0161,
        CB_MULTIPLEADDSTRING = 0x0163,
        CB_GETCOMBOBOXINFO = 0x0164,
        CB_MSGMAX_501 = 0x0165,
        CB_MSGMAX_WCE400 = 0x0163,
        CB_MSGMAX_400 = 0x0162,
        CB_MSGMAX_PRE400 = 0x015B,
        SBM_SETPOS = 0x00E0,
        SBM_GETPOS = 0x00E1,
        SBM_SETRANGE = 0x00E2,
        SBM_SETRANGEREDRAW = 0x00E6,
        SBM_GETRANGE = 0x00E3,
        SBM_ENABLE_ARROWS = 0x00E4,
        SBM_SETSCROLLINFO = 0x00E9,
        SBM_GETSCROLLINFO = 0x00EA,
        SBM_GETSCROLLBARINFO = 0x00EB,
        LVM_FIRST = 0x1000,// ListView messages
        TV_FIRST = 0x1100,// TreeView messages
        HDM_FIRST = 0x1200,// Header messages
        TCM_FIRST = 0x1300,// Tab control messages
        PGM_FIRST = 0x1400,// Pager control messages
        ECM_FIRST = 0x1500,// Edit control messages
        BCM_FIRST = 0x1600,// Button control messages
        CBM_FIRST = 0x1700,// Combobox control messages
        CCM_FIRST = 0x2000,// Common control shared messages
        CCM_LAST = (CCM_FIRST + 0x200),
        CCM_SETBKCOLOR = (CCM_FIRST + 1),
        CCM_SETCOLORSCHEME = (CCM_FIRST + 2),
        CCM_GETCOLORSCHEME = (CCM_FIRST + 3),
        CCM_GETDROPTARGET = (CCM_FIRST + 4),
        CCM_SETUNICODEFORMAT = (CCM_FIRST + 5),
        CCM_GETUNICODEFORMAT = (CCM_FIRST + 6),
        CCM_SETVERSION = (CCM_FIRST + 0x7),
        CCM_GETVERSION = (CCM_FIRST + 0x8),
        CCM_SETNOTIFYWINDOW = (CCM_FIRST + 0x9),
        CCM_SETWINDOWTHEME = (CCM_FIRST + 0xb),
        CCM_DPISCALE = (CCM_FIRST + 0xc),
        HDM_GETITEMCOUNT = (HDM_FIRST + 0),
        HDM_INSERTITEMA = (HDM_FIRST + 1),
        HDM_INSERTITEMW = (HDM_FIRST + 10),
        HDM_DELETEITEM = (HDM_FIRST + 2),
        HDM_GETITEMA = (HDM_FIRST + 3),
        HDM_GETITEMW = (HDM_FIRST + 11),
        HDM_SETITEMA = (HDM_FIRST + 4),
        HDM_SETITEMW = (HDM_FIRST + 12),
        HDM_LAYOUT = (HDM_FIRST + 5),
        HDM_HITTEST = (HDM_FIRST + 6),
        HDM_GETITEMRECT = (HDM_FIRST + 7),
        HDM_SETIMAGELIST = (HDM_FIRST + 8),
        HDM_GETIMAGELIST = (HDM_FIRST + 9),
        HDM_ORDERTOINDEX = (HDM_FIRST + 15),
        HDM_CREATEDRAGIMAGE = (HDM_FIRST + 16),
        HDM_GETORDERARRAY = (HDM_FIRST + 17),
        HDM_SETORDERARRAY = (HDM_FIRST + 18),
        HDM_SETHOTDIVIDER = (HDM_FIRST + 19),
        HDM_SETBITMAPMARGIN = (HDM_FIRST + 20),
        HDM_GETBITMAPMARGIN = (HDM_FIRST + 21),
        HDM_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT,
        HDM_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT,
        HDM_SETFILTERCHANGETIMEOUT = (HDM_FIRST + 22),
        HDM_EDITFILTER = (HDM_FIRST + 23),
        HDM_CLEARFILTER = (HDM_FIRST + 24),
        TB_ENABLEBUTTON = (WM_USER + 1),
        TB_CHECKBUTTON = (WM_USER + 2),
        TB_PRESSBUTTON = (WM_USER + 3),
        TB_HIDEBUTTON = (WM_USER + 4),
        TB_INDETERMINATE = (WM_USER + 5),
        TB_MARKBUTTON = (WM_USER + 6),
        TB_ISBUTTONENABLED = (WM_USER + 9),
        TB_ISBUTTONCHECKED = (WM_USER + 10),
        TB_ISBUTTONPRESSED = (WM_USER + 11),
        TB_ISBUTTONHIDDEN = (WM_USER + 12),
        TB_ISBUTTONINDETERMINATE = (WM_USER + 13),
        TB_ISBUTTONHIGHLIGHTED = (WM_USER + 14),
        TB_SETSTATE = (WM_USER + 17),
        TB_GETSTATE = (WM_USER + 18),
        TB_ADDBITMAP = (WM_USER + 19),
        TB_ADDBUTTONSA = (WM_USER + 20),
        TB_INSERTBUTTONA = (WM_USER + 21),
        TB_ADDBUTTONS = (WM_USER + 20),
        TB_INSERTBUTTON = (WM_USER + 21),
        TB_DELETEBUTTON = (WM_USER + 22),
        TB_GETBUTTON = (WM_USER + 23),
        TB_BUTTONCOUNT = (WM_USER + 24),
        TB_COMMANDTOINDEX = (WM_USER + 25),
        TB_SAVERESTOREA = (WM_USER + 26),
        TB_SAVERESTOREW = (WM_USER + 76),
        TB_CUSTOMIZE = (WM_USER + 27),
        TB_ADDSTRINGA = (WM_USER + 28),
        TB_ADDSTRINGW = (WM_USER + 77),
        TB_GETITEMRECT = (WM_USER + 29),
        TB_BUTTONSTRUCTSIZE = (WM_USER + 30),
        TB_SETBUTTONSIZE = (WM_USER + 31),
        TB_SETBITMAPSIZE = (WM_USER + 32),
        TB_AUTOSIZE = (WM_USER + 33),
        TB_GETTOOLTIPS = (WM_USER + 35),
        TB_SETTOOLTIPS = (WM_USER + 36),
        TB_SETPARENT = (WM_USER + 37),
        TB_SETROWS = (WM_USER + 39),
        TB_GETROWS = (WM_USER + 40),
        TB_SETCMDID = (WM_USER + 42),
        TB_CHANGEBITMAP = (WM_USER + 43),
        TB_GETBITMAP = (WM_USER + 44),
        TB_GETBUTTONTEXTA = (WM_USER + 45),
        TB_GETBUTTONTEXTW = (WM_USER + 75),
        TB_REPLACEBITMAP = (WM_USER + 46),
        TB_SETINDENT = (WM_USER + 47),
        TB_SETIMAGELIST = (WM_USER + 48),
        TB_GETIMAGELIST = (WM_USER + 49),
        TB_LOADIMAGES = (WM_USER + 50),
        TB_GETRECT = (WM_USER + 51),
        TB_SETHOTIMAGELIST = (WM_USER + 52),
        TB_GETHOTIMAGELIST = (WM_USER + 53),
        TB_SETDISABLEDIMAGELIST = (WM_USER + 54),
        TB_GETDISABLEDIMAGELIST = (WM_USER + 55),
        TB_SETSTYLE = (WM_USER + 56),
        TB_GETSTYLE = (WM_USER + 57),
        TB_GETBUTTONSIZE = (WM_USER + 58),
        TB_SETBUTTONWIDTH = (WM_USER + 59),
        TB_SETMAXTEXTROWS = (WM_USER + 60),
        TB_GETTEXTROWS = (WM_USER + 61),
        TB_GETOBJECT = (WM_USER + 62),
        TB_GETHOTITEM = (WM_USER + 71),
        TB_SETHOTITEM = (WM_USER + 72),
        TB_SETANCHORHIGHLIGHT = (WM_USER + 73),
        TB_GETANCHORHIGHLIGHT = (WM_USER + 74),
        TB_MAPACCELERATORA = (WM_USER + 78),
        TB_GETINSERTMARK = (WM_USER + 79),
        TB_SETINSERTMARK = (WM_USER + 80),
        TB_INSERTMARKHITTEST = (WM_USER + 81),
        TB_MOVEBUTTON = (WM_USER + 82),
        TB_GETMAXSIZE = (WM_USER + 83),
        TB_SETEXTENDEDSTYLE = (WM_USER + 84),
        TB_GETEXTENDEDSTYLE = (WM_USER + 85),
        TB_GETPADDING = (WM_USER + 86),
        TB_SETPADDING = (WM_USER + 87),
        TB_SETINSERTMARKCOLOR = (WM_USER + 88),
        TB_GETINSERTMARKCOLOR = (WM_USER + 89),
        TB_SETCOLORSCHEME = CCM_SETCOLORSCHEME,
        TB_GETCOLORSCHEME = CCM_GETCOLORSCHEME,
        TB_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT,
        TB_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT,
        TB_MAPACCELERATORW = (WM_USER + 90),
        TB_GETBITMAPFLAGS = (WM_USER + 41),
        TB_GETBUTTONINFOW = (WM_USER + 63),
        TB_SETBUTTONINFOW = (WM_USER + 64),
        TB_GETBUTTONINFOA = (WM_USER + 65),
        TB_SETBUTTONINFOA = (WM_USER + 66),
        TB_INSERTBUTTONW = (WM_USER + 67),
        TB_ADDBUTTONSW = (WM_USER + 68),
        TB_HITTEST = (WM_USER + 69),
        TB_SETDRAWTEXTFLAGS = (WM_USER + 70),
        TB_GETSTRINGW = (WM_USER + 91),
        TB_GETSTRINGA = (WM_USER + 92),
        TB_GETMETRICS = (WM_USER + 101),
        TB_SETMETRICS = (WM_USER + 102),
        TB_SETWINDOWTHEME = CCM_SETWINDOWTHEME,
        RB_INSERTBANDA = (WM_USER + 1),
        RB_DELETEBAND = (WM_USER + 2),
        RB_GETBARINFO = (WM_USER + 3),
        RB_SETBARINFO = (WM_USER + 4),
        RB_GETBANDINFO = (WM_USER + 5),
        RB_SETBANDINFOA = (WM_USER + 6),
        RB_SETPARENT = (WM_USER + 7),
        RB_HITTEST = (WM_USER + 8),
        RB_GETRECT = (WM_USER + 9),
        RB_INSERTBANDW = (WM_USER + 10),
        RB_SETBANDINFOW = (WM_USER + 11),
        RB_GETBANDCOUNT = (WM_USER + 12),
        RB_GETROWCOUNT = (WM_USER + 13),
        RB_GETROWHEIGHT = (WM_USER + 14),
        RB_IDTOINDEX = (WM_USER + 16),
        RB_GETTOOLTIPS = (WM_USER + 17),
        RB_SETTOOLTIPS = (WM_USER + 18),
        RB_SETBKCOLOR = (WM_USER + 19),
        RB_GETBKCOLOR = (WM_USER + 20),
        RB_SETTEXTCOLOR = (WM_USER + 21),
        RB_GETTEXTCOLOR = (WM_USER + 22),
        RB_SIZETORECT = (WM_USER + 23),
        RB_SETCOLORSCHEME = CCM_SETCOLORSCHEME,
        RB_GETCOLORSCHEME = CCM_GETCOLORSCHEME,
        RB_BEGINDRAG = (WM_USER + 24),
        RB_ENDDRAG = (WM_USER + 25),
        RB_DRAGMOVE = (WM_USER + 26),
        RB_GETBARHEIGHT = (WM_USER + 27),
        RB_GETBANDINFOW = (WM_USER + 28),
        RB_GETBANDINFOA = (WM_USER + 29),
        RB_MINIMIZEBAND = (WM_USER + 30),
        RB_MAXIMIZEBAND = (WM_USER + 31),
        RB_GETDROPTARGET = (CCM_GETDROPTARGET),
        RB_GETBANDBORDERS = (WM_USER + 34),
        RB_SHOWBAND = (WM_USER + 35),
        RB_SETPALETTE = (WM_USER + 37),
        RB_GETPALETTE = (WM_USER + 38),
        RB_MOVEBAND = (WM_USER + 39),
        RB_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT,
        RB_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT,
        RB_GETBANDMARGINS = (WM_USER + 40),
        RB_SETWINDOWTHEME = CCM_SETWINDOWTHEME,
        RB_PUSHCHEVRON = (WM_USER + 43),
        TTM_ACTIVATE = (WM_USER + 1),
        TTM_SETDELAYTIME = (WM_USER + 3),
        TTM_ADDTOOLA = (WM_USER + 4),
        TTM_ADDTOOLW = (WM_USER + 50),
        TTM_DELTOOLA = (WM_USER + 5),
        TTM_DELTOOLW = (WM_USER + 51),
        TTM_NEWTOOLRECTA = (WM_USER + 6),
        TTM_NEWTOOLRECTW = (WM_USER + 52),
        TTM_RELAYEVENT = (WM_USER + 7),
        TTM_GETTOOLINFOA = (WM_USER + 8),
        TTM_GETTOOLINFOW = (WM_USER + 53),
        TTM_SETTOOLINFOA = (WM_USER + 9),
        TTM_SETTOOLINFOW = (WM_USER + 54),
        TTM_HITTESTA = (WM_USER + 10),
        TTM_HITTESTW = (WM_USER + 55),
        TTM_GETTEXTA = (WM_USER + 11),
        TTM_GETTEXTW = (WM_USER + 56),
        TTM_UPDATETIPTEXTA = (WM_USER + 12),
        TTM_UPDATETIPTEXTW = (WM_USER + 57),
        TTM_GETTOOLCOUNT = (WM_USER + 13),
        TTM_ENUMTOOLSA = (WM_USER + 14),
        TTM_ENUMTOOLSW = (WM_USER + 58),
        TTM_GETCURRENTTOOLA = (WM_USER + 15),
        TTM_GETCURRENTTOOLW = (WM_USER + 59),
        TTM_WINDOWFROMPOINT = (WM_USER + 16),
        TTM_TRACKACTIVATE = (WM_USER + 17),
        TTM_TRACKPOSITION = (WM_USER + 18),
        TTM_SETTIPBKCOLOR = (WM_USER + 19),
        TTM_SETTIPTEXTCOLOR = (WM_USER + 20),
        TTM_GETDELAYTIME = (WM_USER + 21),
        TTM_GETTIPBKCOLOR = (WM_USER + 22),
        TTM_GETTIPTEXTCOLOR = (WM_USER + 23),
        TTM_SETMAXTIPWIDTH = (WM_USER + 24),
        TTM_GETMAXTIPWIDTH = (WM_USER + 25),
        TTM_SETMARGIN = (WM_USER + 26),
        TTM_GETMARGIN = (WM_USER + 27),
        TTM_POP = (WM_USER + 28),
        TTM_UPDATE = (WM_USER + 29),
        TTM_GETBUBBLESIZE = (WM_USER + 30),
        TTM_ADJUSTRECT = (WM_USER + 31),
        TTM_SETTITLEA = (WM_USER + 32),
        TTM_SETTITLEW = (WM_USER + 33),
        TTM_POPUP = (WM_USER + 34),
        TTM_GETTITLE = (WM_USER + 35),
        TTM_SETWINDOWTHEME = CCM_SETWINDOWTHEME,
        SB_SETTEXTA = (WM_USER + 1),
        SB_SETTEXTW = (WM_USER + 11),
        SB_GETTEXTA = (WM_USER + 2),
        SB_GETTEXTW = (WM_USER + 13),
        SB_GETTEXTLENGTHA = (WM_USER + 3),
        SB_GETTEXTLENGTHW = (WM_USER + 12),
        SB_SETPARTS = (WM_USER + 4),
        SB_GETPARTS = (WM_USER + 6),
        SB_GETBORDERS = (WM_USER + 7),
        SB_SETMINHEIGHT = (WM_USER + 8),
        SB_SIMPLE = (WM_USER + 9),
        SB_GETRECT = (WM_USER + 10),
        SB_ISSIMPLE = (WM_USER + 14),
        SB_SETICON = (WM_USER + 15),
        SB_SETTIPTEXTA = (WM_USER + 16),
        SB_SETTIPTEXTW = (WM_USER + 17),
        SB_GETTIPTEXTA = (WM_USER + 18),
        SB_GETTIPTEXTW = (WM_USER + 19),
        SB_GETICON = (WM_USER + 20),
        SB_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT,
        SB_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT,
        SB_SETBKCOLOR = CCM_SETBKCOLOR,
        SB_SIMPLEID = 0x00ff,
        TBM_GETPOS = (WM_USER),
        TBM_GETRANGEMIN = (WM_USER + 1),
        TBM_GETRANGEMAX = (WM_USER + 2),
        TBM_GETTIC = (WM_USER + 3),
        TBM_SETTIC = (WM_USER + 4),
        TBM_SETPOS = (WM_USER + 5),
        TBM_SETRANGE = (WM_USER + 6),
        TBM_SETRANGEMIN = (WM_USER + 7),
        TBM_SETRANGEMAX = (WM_USER + 8),
        TBM_CLEARTICS = (WM_USER + 9),
        TBM_SETSEL = (WM_USER + 10),
        TBM_SETSELSTART = (WM_USER + 11),
        TBM_SETSELEND = (WM_USER + 12),
        TBM_GETPTICS = (WM_USER + 14),
        TBM_GETTICPOS = (WM_USER + 15),
        TBM_GETNUMTICS = (WM_USER + 16),
        TBM_GETSELSTART = (WM_USER + 17),
        TBM_GETSELEND = (WM_USER + 18),
        TBM_CLEARSEL = (WM_USER + 19),
        TBM_SETTICFREQ = (WM_USER + 20),
        TBM_SETPAGESIZE = (WM_USER + 21),
        TBM_GETPAGESIZE = (WM_USER + 22),
        TBM_SETLINESIZE = (WM_USER + 23),
        TBM_GETLINESIZE = (WM_USER + 24),
        TBM_GETTHUMBRECT = (WM_USER + 25),
        TBM_GETCHANNELRECT = (WM_USER + 26),
        TBM_SETTHUMBLENGTH = (WM_USER + 27),
        TBM_GETTHUMBLENGTH = (WM_USER + 28),
        TBM_SETTOOLTIPS = (WM_USER + 29),
        TBM_GETTOOLTIPS = (WM_USER + 30),
        TBM_SETTIPSIDE = (WM_USER + 31),
        TBM_SETBUDDY = (WM_USER + 32),
        TBM_GETBUDDY = (WM_USER + 33),
        TBM_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT,
        TBM_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT,
        DL_BEGINDRAG = (WM_USER + 133),
        DL_DRAGGING = (WM_USER + 134),
        DL_DROPPED = (WM_USER + 135),
        DL_CANCELDRAG = (WM_USER + 136),
        UDM_SETRANGE = (WM_USER + 101),
        UDM_GETRANGE = (WM_USER + 102),
        UDM_SETPOS = (WM_USER + 103),
        UDM_GETPOS = (WM_USER + 104),
        UDM_SETBUDDY = (WM_USER + 105),
        UDM_GETBUDDY = (WM_USER + 106),
        UDM_SETACCEL = (WM_USER + 107),
        UDM_GETACCEL = (WM_USER + 108),
        UDM_SETBASE = (WM_USER + 109),
        UDM_GETBASE = (WM_USER + 110),
        UDM_SETRANGE32 = (WM_USER + 111),
        UDM_GETRANGE32 = (WM_USER + 112),
        UDM_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT,
        UDM_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT,
        UDM_SETPOS32 = (WM_USER + 113),
        UDM_GETPOS32 = (WM_USER + 114),
        PBM_SETRANGE = (WM_USER + 1),
        PBM_SETPOS = (WM_USER + 2),
        PBM_DELTAPOS = (WM_USER + 3),
        PBM_SETSTEP = (WM_USER + 4),
        PBM_STEPIT = (WM_USER + 5),
        PBM_SETRANGE32 = (WM_USER + 6),
        PBM_GETRANGE = (WM_USER + 7),
        PBM_GETPOS = (WM_USER + 8),
        PBM_SETBARCOLOR = (WM_USER + 9),
        PBM_SETBKCOLOR = CCM_SETBKCOLOR,
        HKM_SETHOTKEY = (WM_USER + 1),
        HKM_GETHOTKEY = (WM_USER + 2),
        HKM_SETRULES = (WM_USER + 3),
        LVM_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT,
        LVM_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT,
        LVM_GETBKCOLOR = (LVM_FIRST + 0),
        LVM_SETBKCOLOR = (LVM_FIRST + 1),
        LVM_GETIMAGELIST = (LVM_FIRST + 2),
        LVM_SETIMAGELIST = (LVM_FIRST + 3),
        LVM_GETITEMCOUNT = (LVM_FIRST + 4),
        LVM_GETITEMA = (LVM_FIRST + 5),
        LVM_GETITEMW = (LVM_FIRST + 75),
        LVM_SETITEMA = (LVM_FIRST + 6),
        LVM_SETITEMW = (LVM_FIRST + 76),
        LVM_INSERTITEMA = (LVM_FIRST + 7),
        LVM_INSERTITEMW = (LVM_FIRST + 77),
        LVM_DELETEITEM = (LVM_FIRST + 8),
        LVM_DELETEALLITEMS = (LVM_FIRST + 9),
        LVM_GETCALLBACKMASK = (LVM_FIRST + 10),
        LVM_SETCALLBACKMASK = (LVM_FIRST + 11),
        LVM_FINDITEMA = (LVM_FIRST + 13),
        LVM_FINDITEMW = (LVM_FIRST + 83),
        LVM_GETITEMRECT = (LVM_FIRST + 14),
        LVM_SETITEMPOSITION = (LVM_FIRST + 15),
        LVM_GETITEMPOSITION = (LVM_FIRST + 16),
        LVM_GETSTRINGWIDTHA = (LVM_FIRST + 17),
        LVM_GETSTRINGWIDTHW = (LVM_FIRST + 87),
        LVM_HITTEST = (LVM_FIRST + 18),
        LVM_ENSUREVISIBLE = (LVM_FIRST + 19),
        LVM_SCROLL = (LVM_FIRST + 20),
        LVM_REDRAWITEMS = (LVM_FIRST + 21),
        LVM_ARRANGE = (LVM_FIRST + 22),
        LVM_EDITLABELA = (LVM_FIRST + 23),
        LVM_EDITLABELW = (LVM_FIRST + 118),
        LVM_GETEDITCONTROL = (LVM_FIRST + 24),
        LVM_GETCOLUMNA = (LVM_FIRST + 25),
        LVM_GETCOLUMNW = (LVM_FIRST + 95),
        LVM_SETCOLUMNA = (LVM_FIRST + 26),
        LVM_SETCOLUMNW = (LVM_FIRST + 96),
        LVM_INSERTCOLUMNA = (LVM_FIRST + 27),
        LVM_INSERTCOLUMNW = (LVM_FIRST + 97),
        LVM_DELETECOLUMN = (LVM_FIRST + 28),
        LVM_GETCOLUMNWIDTH = (LVM_FIRST + 29),
        LVM_SETCOLUMNWIDTH = (LVM_FIRST + 30),
        LVM_CREATEDRAGIMAGE = (LVM_FIRST + 33),
        LVM_GETVIEWRECT = (LVM_FIRST + 34),
        LVM_GETTEXTCOLOR = (LVM_FIRST + 35),
        LVM_SETTEXTCOLOR = (LVM_FIRST + 36),
        LVM_GETTEXTBKCOLOR = (LVM_FIRST + 37),
        LVM_SETTEXTBKCOLOR = (LVM_FIRST + 38),
        LVM_GETTOPINDEX = (LVM_FIRST + 39),
        LVM_GETCOUNTPERPAGE = (LVM_FIRST + 40),
        LVM_GETORIGIN = (LVM_FIRST + 41),
        LVM_UPDATE = (LVM_FIRST + 42),
        LVM_SETITEMSTATE = (LVM_FIRST + 43),
        LVM_GETITEMSTATE = (LVM_FIRST + 44),
        LVM_GETITEMTEXTA = (LVM_FIRST + 45),
        LVM_GETITEMTEXTW = (LVM_FIRST + 115),
        LVM_SETITEMTEXTA = (LVM_FIRST + 46),
        LVM_SETITEMTEXTW = (LVM_FIRST + 116),
        LVM_SETITEMCOUNT = (LVM_FIRST + 47),
        LVM_SORTITEMS = (LVM_FIRST + 48),
        LVM_SETITEMPOSITION32 = (LVM_FIRST + 49),
        LVM_GETSELECTEDCOUNT = (LVM_FIRST + 50),
        LVM_GETITEMSPACING = (LVM_FIRST + 51),
        LVM_GETISEARCHSTRINGA = (LVM_FIRST + 52),
        LVM_GETISEARCHSTRINGW = (LVM_FIRST + 117),
        LVM_SETICONSPACING = (LVM_FIRST + 53),
        LVM_SETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 54),
        LVM_GETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 55),
        LVM_GETSUBITEMRECT = (LVM_FIRST + 56),
        LVM_SUBITEMHITTEST = (LVM_FIRST + 57),
        LVM_SETCOLUMNORDERARRAY = (LVM_FIRST + 58),
        LVM_GETCOLUMNORDERARRAY = (LVM_FIRST + 59),
        LVM_SETHOTITEM = (LVM_FIRST + 60),
        LVM_GETHOTITEM = (LVM_FIRST + 61),
        LVM_SETHOTCURSOR = (LVM_FIRST + 62),
        LVM_GETHOTCURSOR = (LVM_FIRST + 63),
        LVM_APPROXIMATEVIEWRECT = (LVM_FIRST + 64),
        LVM_SETWORKAREAS = (LVM_FIRST + 65),
        LVM_GETWORKAREAS = (LVM_FIRST + 70),
        LVM_GETNUMBEROFWORKAREAS = (LVM_FIRST + 73),
        LVM_GETSELECTIONMARK = (LVM_FIRST + 66),
        LVM_SETSELECTIONMARK = (LVM_FIRST + 67),
        LVM_SETHOVERTIME = (LVM_FIRST + 71),
        LVM_GETHOVERTIME = (LVM_FIRST + 72),
        LVM_SETTOOLTIPS = (LVM_FIRST + 74),
        LVM_GETTOOLTIPS = (LVM_FIRST + 78),
        LVM_SORTITEMSEX = (LVM_FIRST + 81),
        LVM_SETBKIMAGEA = (LVM_FIRST + 68),
        LVM_SETBKIMAGEW = (LVM_FIRST + 138),
        LVM_GETBKIMAGEA = (LVM_FIRST + 69),
        LVM_GETBKIMAGEW = (LVM_FIRST + 139),
        LVM_SETSELECTEDCOLUMN = (LVM_FIRST + 140),
        LVM_SETTILEWIDTH = (LVM_FIRST + 141),
        LVM_SETVIEW = (LVM_FIRST + 142),
        LVM_GETVIEW = (LVM_FIRST + 143),
        LVM_INSERTGROUP = (LVM_FIRST + 145),
        LVM_SETGROUPINFO = (LVM_FIRST + 147),
        LVM_GETGROUPINFO = (LVM_FIRST + 149),
        LVM_REMOVEGROUP = (LVM_FIRST + 150),
        LVM_MOVEGROUP = (LVM_FIRST + 151),
        LVM_MOVEITEMTOGROUP = (LVM_FIRST + 154),
        LVM_SETGROUPMETRICS = (LVM_FIRST + 155),
        LVM_GETGROUPMETRICS = (LVM_FIRST + 156),
        LVM_ENABLEGROUPVIEW = (LVM_FIRST + 157),
        LVM_SORTGROUPS = (LVM_FIRST + 158),
        LVM_INSERTGROUPSORTED = (LVM_FIRST + 159),
        LVM_REMOVEALLGROUPS = (LVM_FIRST + 160),
        LVM_HASGROUP = (LVM_FIRST + 161),
        LVM_SETTILEVIEWINFO = (LVM_FIRST + 162),
        LVM_GETTILEVIEWINFO = (LVM_FIRST + 163),
        LVM_SETTILEINFO = (LVM_FIRST + 164),
        LVM_GETTILEINFO = (LVM_FIRST + 165),
        LVM_SETINSERTMARK = (LVM_FIRST + 166),
        LVM_GETINSERTMARK = (LVM_FIRST + 167),
        LVM_INSERTMARKHITTEST = (LVM_FIRST + 168),
        LVM_GETINSERTMARKRECT = (LVM_FIRST + 169),
        LVM_SETINSERTMARKCOLOR = (LVM_FIRST + 170),
        LVM_GETINSERTMARKCOLOR = (LVM_FIRST + 171),
        LVM_SETINFOTIP = (LVM_FIRST + 173),
        LVM_GETSELECTEDCOLUMN = (LVM_FIRST + 174),
        LVM_ISGROUPVIEWENABLED = (LVM_FIRST + 175),
        LVM_GETOUTLINECOLOR = (LVM_FIRST + 176),
        LVM_SETOUTLINECOLOR = (LVM_FIRST + 177),
        LVM_CANCELEDITLABEL = (LVM_FIRST + 179),
        LVM_MAPINDEXTOID = (LVM_FIRST + 180),
        LVM_MAPIDTOINDEX = (LVM_FIRST + 181),
        TVM_INSERTITEMA = (TV_FIRST + 0),
        TVM_INSERTITEMW = (TV_FIRST + 50),
        TVM_DELETEITEM = (TV_FIRST + 1),
        TVM_EXPAND = (TV_FIRST + 2),
        TVM_GETITEMRECT = (TV_FIRST + 4),
        TVM_GETCOUNT = (TV_FIRST + 5),
        TVM_GETINDENT = (TV_FIRST + 6),
        TVM_SETINDENT = (TV_FIRST + 7),
        TVM_GETIMAGELIST = (TV_FIRST + 8),
        TVM_SETIMAGELIST = (TV_FIRST + 9),
        TVM_GETNEXTITEM = (TV_FIRST + 10),
        TVM_SELECTITEM = (TV_FIRST + 11),
        TVM_GETITEMA = (TV_FIRST + 12),
        TVM_GETITEMW = (TV_FIRST + 62),
        TVM_SETITEMA = (TV_FIRST + 13),
        TVM_SETITEMW = (TV_FIRST + 63),
        TVM_EDITLABELA = (TV_FIRST + 14),
        TVM_EDITLABELW = (TV_FIRST + 65),
        TVM_GETEDITCONTROL = (TV_FIRST + 15),
        TVM_GETVISIBLECOUNT = (TV_FIRST + 16),
        TVM_HITTEST = (TV_FIRST + 17),
        TVM_CREATEDRAGIMAGE = (TV_FIRST + 18),
        TVM_SORTCHILDREN = (TV_FIRST + 19),
        TVM_ENSUREVISIBLE = (TV_FIRST + 20),
        TVM_SORTCHILDRENCB = (TV_FIRST + 21),
        TVM_ENDEDITLABELNOW = (TV_FIRST + 22),
        TVM_GETISEARCHSTRINGA = (TV_FIRST + 23),
        TVM_GETISEARCHSTRINGW = (TV_FIRST + 64),
        TVM_SETTOOLTIPS = (TV_FIRST + 24),
        TVM_GETTOOLTIPS = (TV_FIRST + 25),
        TVM_SETINSERTMARK = (TV_FIRST + 26),
        TVM_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT,
        TVM_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT,
        TVM_SETITEMHEIGHT = (TV_FIRST + 27),
        TVM_GETITEMHEIGHT = (TV_FIRST + 28),
        TVM_SETBKCOLOR = (TV_FIRST + 29),
        TVM_SETTEXTCOLOR = (TV_FIRST + 30),
        TVM_GETBKCOLOR = (TV_FIRST + 31),
        TVM_GETTEXTCOLOR = (TV_FIRST + 32),
        TVM_SETSCROLLTIME = (TV_FIRST + 33),
        TVM_GETSCROLLTIME = (TV_FIRST + 34),
        TVM_SETINSERTMARKCOLOR = (TV_FIRST + 37),
        TVM_GETINSERTMARKCOLOR = (TV_FIRST + 38),
        TVM_GETITEMSTATE = (TV_FIRST + 39),
        TVM_SETLINECOLOR = (TV_FIRST + 40),
        TVM_GETLINECOLOR = (TV_FIRST + 41),
        TVM_MAPACCIDTOHTREEITEM = (TV_FIRST + 42),
        TVM_MAPHTREEITEMTOACCID = (TV_FIRST + 43),
        CBEM_INSERTITEMA = (WM_USER + 1),
        CBEM_SETIMAGELIST = (WM_USER + 2),
        CBEM_GETIMAGELIST = (WM_USER + 3),
        CBEM_GETITEMA = (WM_USER + 4),
        CBEM_SETITEMA = (WM_USER + 5),
        CBEM_DELETEITEM = CB_DELETESTRING,
        CBEM_GETCOMBOCONTROL = (WM_USER + 6),
        CBEM_GETEDITCONTROL = (WM_USER + 7),
        CBEM_SETEXTENDEDSTYLE = (WM_USER + 14),
        CBEM_GETEXTENDEDSTYLE = (WM_USER + 9),
        CBEM_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT,
        CBEM_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT,
        CBEM_SETEXSTYLE = (WM_USER + 8),
        CBEM_GETEXSTYLE = (WM_USER + 9),
        CBEM_HASEDITCHANGED = (WM_USER + 10),
        CBEM_INSERTITEMW = (WM_USER + 11),
        CBEM_SETITEMW = (WM_USER + 12),
        CBEM_GETITEMW = (WM_USER + 13),
        TCM_GETIMAGELIST = (TCM_FIRST + 2),
        TCM_SETIMAGELIST = (TCM_FIRST + 3),
        TCM_GETITEMCOUNT = (TCM_FIRST + 4),
        TCM_GETITEMA = (TCM_FIRST + 5),
        TCM_GETITEMW = (TCM_FIRST + 60),
        TCM_SETITEMA = (TCM_FIRST + 6),
        TCM_SETITEMW = (TCM_FIRST + 61),
        TCM_INSERTITEMA = (TCM_FIRST + 7),
        TCM_INSERTITEMW = (TCM_FIRST + 62),
        TCM_DELETEITEM = (TCM_FIRST + 8),
        TCM_DELETEALLITEMS = (TCM_FIRST + 9),
        TCM_GETITEMRECT = (TCM_FIRST + 10),
        TCM_GETCURSEL = (TCM_FIRST + 11),
        TCM_SETCURSEL = (TCM_FIRST + 12),
        TCM_HITTEST = (TCM_FIRST + 13),
        TCM_SETITEMEXTRA = (TCM_FIRST + 14),
        TCM_ADJUSTRECT = (TCM_FIRST + 40),
        TCM_SETITEMSIZE = (TCM_FIRST + 41),
        TCM_REMOVEIMAGE = (TCM_FIRST + 42),
        TCM_SETPADDING = (TCM_FIRST + 43),
        TCM_GETROWCOUNT = (TCM_FIRST + 44),
        TCM_GETTOOLTIPS = (TCM_FIRST + 45),
        TCM_SETTOOLTIPS = (TCM_FIRST + 46),
        TCM_GETCURFOCUS = (TCM_FIRST + 47),
        TCM_SETCURFOCUS = (TCM_FIRST + 48),
        TCM_SETMINTABWIDTH = (TCM_FIRST + 49),
        TCM_DESELECTALL = (TCM_FIRST + 50),
        TCM_HIGHLIGHTITEM = (TCM_FIRST + 51),
        TCM_SETEXTENDEDSTYLE = (TCM_FIRST + 52),
        TCM_GETEXTENDEDSTYLE = (TCM_FIRST + 53),
        TCM_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT,
        TCM_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT,
        ACM_OPENA = (WM_USER + 100),
        ACM_OPENW = (WM_USER + 103),
        ACM_PLAY = (WM_USER + 101),
        ACM_STOP = (WM_USER + 102),
        MCM_FIRST = 0x1000,
        MCM_GETCURSEL = (MCM_FIRST + 1),
        MCM_SETCURSEL = (MCM_FIRST + 2),
        MCM_GETMAXSELCOUNT = (MCM_FIRST + 3),
        MCM_SETMAXSELCOUNT = (MCM_FIRST + 4),
        MCM_GETSELRANGE = (MCM_FIRST + 5),
        MCM_SETSELRANGE = (MCM_FIRST + 6),
        MCM_GETMONTHRANGE = (MCM_FIRST + 7),
        MCM_SETDAYSTATE = (MCM_FIRST + 8),
        MCM_GETMINREQRECT = (MCM_FIRST + 9),
        MCM_SETCOLOR = (MCM_FIRST + 10),
        MCM_GETCOLOR = (MCM_FIRST + 11),
        MCM_SETTODAY = (MCM_FIRST + 12),
        MCM_GETTODAY = (MCM_FIRST + 13),
        MCM_HITTEST = (MCM_FIRST + 14),
        MCM_SETFIRSTDAYOFWEEK = (MCM_FIRST + 15),
        MCM_GETFIRSTDAYOFWEEK = (MCM_FIRST + 16),
        MCM_GETRANGE = (MCM_FIRST + 17),
        MCM_SETRANGE = (MCM_FIRST + 18),
        MCM_GETMONTHDELTA = (MCM_FIRST + 19),
        MCM_SETMONTHDELTA = (MCM_FIRST + 20),
        MCM_GETMAXTODAYWIDTH = (MCM_FIRST + 21),
        MCM_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT,
        MCM_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT,
        DTM_FIRST = 0x1000,
        DTM_GETSYSTEMTIME = (DTM_FIRST + 1),
        DTM_SETSYSTEMTIME = (DTM_FIRST + 2),
        DTM_GETRANGE = (DTM_FIRST + 3),
        DTM_SETRANGE = (DTM_FIRST + 4),
        DTM_SETFORMATA = (DTM_FIRST + 5),
        DTM_SETFORMATW = (DTM_FIRST + 50),
        DTM_SETMCCOLOR = (DTM_FIRST + 6),
        DTM_GETMCCOLOR = (DTM_FIRST + 7),
        DTM_GETMONTHCAL = (DTM_FIRST + 8),
        DTM_SETMCFONT = (DTM_FIRST + 9),
        DTM_GETMCFONT = (DTM_FIRST + 10),
        PGM_SETCHILD = (PGM_FIRST + 1),
        PGM_RECALCSIZE = (PGM_FIRST + 2),
        PGM_FORWARDMOUSE = (PGM_FIRST + 3),
        PGM_SETBKCOLOR = (PGM_FIRST + 4),
        PGM_GETBKCOLOR = (PGM_FIRST + 5),
        PGM_SETBORDER = (PGM_FIRST + 6),
        PGM_GETBORDER = (PGM_FIRST + 7),
        PGM_SETPOS = (PGM_FIRST + 8),
        PGM_GETPOS = (PGM_FIRST + 9),
        PGM_SETBUTTONSIZE = (PGM_FIRST + 10),
        PGM_GETBUTTONSIZE = (PGM_FIRST + 11),
        PGM_GETBUTTONSTATE = (PGM_FIRST + 12),
        PGM_GETDROPTARGET = CCM_GETDROPTARGET,
        BCM_GETIDEALSIZE = (BCM_FIRST + 0x0001),
        BCM_SETIMAGELIST = (BCM_FIRST + 0x0002),
        BCM_GETIMAGELIST = (BCM_FIRST + 0x0003),
        BCM_SETTEXTMARGIN = (BCM_FIRST + 0x0004),
        BCM_GETTEXTMARGIN = (BCM_FIRST + 0x0005),
        EM_SETCUEBANNER = (ECM_FIRST + 1),
        EM_GETCUEBANNER = (ECM_FIRST + 2),
        EM_SHOWBALLOONTIP = (ECM_FIRST + 3),
        EM_HIDEBALLOONTIP = (ECM_FIRST + 4),
        CB_SETMINVISIBLE = (CBM_FIRST + 1),
        CB_GETMINVISIBLE = (CBM_FIRST + 2),
        LM_HITTEST = (WM_USER + 0x300),
        LM_GETIDEALHEIGHT = (WM_USER + 0x301),
        LM_SETITEM = (WM_USER + 0x302),
        LM_GETITEM = (WM_USER + 0x303)
    }
}
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：WinHotKey
* 类 描 述 ：Win系统热键类扩展
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.WinForm
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/18 21:22:16
* 更新时间 ：2019/10/18 21:22:16
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using Kane.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Kane.WinForm
{
    /// <summary>
    /// Win系统热键类扩展
    /// </summary>
    public class WinHotKey : IMessageFilter
    {
        #region 使用方法
        //public void HowToUse()
        //{
        //    WinHotKey hotkey = new WinHotKey(this.Handle);
        //    hotkey.RegisterHotkey(KeyFlags.Ctrl | KeyFlags.Shift, Keys.D6, "测试一");
        //    hotkey.RegisterHotkey(KeyFlags.Ctrl | KeyFlags.Shift, Keys.D7, "测试二");
        //    hotkey.HotKeyAction += (hotKeyID) =>
        //    {
        //        if (hotKeyID == hotkey.GetHotKeyID("测试一"))
        //        {
        //            //Do something
        //        }
        //        if (hotKeyID == hotkey.GetHotKeyID("测试二"))
        //        {
        //            //Do something
        //        }
        //    };
        //} 
        #endregion

        #region 公开的字段
        /// <summary>
        /// 已注册的热键字典
        /// </summary>
        public Dictionary<int, string> RegisterdHotKeys = new Dictionary<int, string>();
        /// <summary>
        /// 注册热键对应事件
        /// </summary>
        public event Action<int> HotKeyAction;
        #endregion

        #region 私有字段
        /// <summary>
        /// 窗体句柄
        /// </summary>
        private readonly IntPtr hWnd;
        /// <summary>
        /// WindowMessage对应的热键常量
        /// </summary>
        private const int WM_HOTKEY = 0x0312;
        #endregion

        #region 定义了辅助键的枚举，将数字转变为字符
        /// <summary>
        /// 定义了辅助键的枚举，将数字转变为字符
        /// </summary>
        [Flags]
        public enum KeyFlags
        {
            /// <summary>
            /// None
            /// </summary>
            None = 0,
            /// <summary>
            /// Alt
            /// </summary>
            Alt = 1,
            /// <summary>
            /// Ctrl
            /// </summary>
            Ctrl = 2,
            /// <summary>
            /// Shift
            /// </summary>
            Shift = 4,
            /// <summary>
            /// WindowsKey
            /// </summary>
            WindowsKey = 8
        }
        #endregion

        #region 注册全局热键 + RegisterHotKey(IntPtr hWnd, int atom, KeyFlags flags, Keys key);
        /// <summary>
        /// 注册全局热键
        /// </summary>
        /// <param name="hWnd">要定义热键的窗口的句柄</param>
        /// <param name="atom">定义热键ID（不能与其它ID重复），可由GlobalAddAtom创建出来，也可以自定义</param>
        /// <param name="flags">标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效</param>
        /// <param name="key">定义热键的内容</param>
        /// <returns>是否注册成功</returns>
        [DllImport("user32", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int atom, KeyFlags flags, Keys key);
        #endregion

        #region 取消全局热键 + UnregisterHotKey(IntPtr hWnd, int atom); 
        /// <summary>
        /// 取消全局热键
        /// </summary>
        /// <param name="hWnd">要取消热键的窗口的句柄</param>
        /// <param name="atom">要取消热键的ID</param>
        /// <returns>是否取消成功</returns>
        [DllImport("user32", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int atom);
        #endregion

        #region 将字符串添加到全局原子表并返回标识字符串的唯一值 + GlobalAddAtom(string value); 
        /// <summary>
        /// 将字符串添加到全局原子表并返回标识字符串的唯一值
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>原子值</returns>
        [DllImport("kernel32.dll")]
        public static extern int GlobalAddAtom(string value);
        #endregion

        #region 递减全局字符串原子的引用计数。如果原子的引用计数达到零，GlobalDeleteAtom将从全局原子表中删除与原子关联的字符串 + GlobalDeleteAtom(int atom); 
        /// <summary>
        /// 递减全局字符串原子的引用计数。如果原子的引用计数达到零，GlobalDeleteAtom将从全局原子表中删除与原子关联的字符串
        /// </summary>
        /// <param name="atom">原子值</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int GlobalDeleteAtom(int atom);
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="handle">窗体句柄</param>
        public WinHotKey(IntPtr handle)
        {
            hWnd = handle;
            Application.AddMessageFilter(this);
        }
        #endregion

        #region 注册全局热键方法，并把相关信息加入到RegisterdHotKeys + RegisterHotkey(KeyFlags keyflags, Keys Key, string name = "")
        /// <summary>
        /// 注册全局热键方法，并把相关信息加入到RegisterdHotKeys
        /// </summary>
        /// <param name="keyflags">标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效</param>
        /// <param name="Key">定义热键的内容</param>
        /// <param name="name">可定义对应的名称</param>
        /// <returns>是否注册成功</returns>
        public bool RegisterHotkey(KeyFlags keyflags, Keys Key, string name = "")
        {
            name = name.IsValuable() ? name : Guid.NewGuid().ToString("N");
            var hotkeyID = GlobalAddAtom(name);
            if (RegisterHotKey(hWnd, hotkeyID, keyflags, Key))
            {
                RegisterdHotKeys.Add(hotkeyID, name);
                return true;
            }
            else return false;
        }
        #endregion

        #region 注销全部全局热键 + UnregisterHotkeys()
        /// <summary>
        /// 注销全部全局热键
        /// </summary>
        public void UnregisterHotkeys()
        {
            List<int> removeList = new List<int>();
            foreach (var dic in RegisterdHotKeys)
            {
                if (UnregisterHotKey(hWnd, dic.Key))
                {
                    GlobalDeleteAtom(dic.Key);
                    removeList.Add(dic.Key);
                }
            }
            if (removeList.Count > 0)
            {
                foreach (var item in removeList)
                {
                    RegisterdHotKeys.Remove(item);
                }
            }
            if (RegisterdHotKeys.Count == 0)
                Application.RemoveMessageFilter(this);
        }
        #endregion

        #region 根据名称注销单个全局热键 + UnregisterHotkeys(string name)
        /// <summary>
        /// 根据名称注销全局热键
        /// </summary>
        public void UnregisterHotkeys(string name)
        {
            if (RegisterdHotKeys.ContainsValue(name))
            {
                var hotKeyID = RegisterdHotKeys.Where(k => k.Value == name).FirstOrDefault().Key;
                if (UnregisterHotKey(hWnd, hotKeyID))
                {
                    GlobalDeleteAtom(hotKeyID);
                    RegisterdHotKeys.Remove(hotKeyID);
                }
            }
        }
        #endregion

        #region 前置Message过滤器 + PreFilterMessage(ref Message m)
        /// <summary>
        /// 前置Message过滤器
        /// </summary>
        /// <param name="m">消息句柄</param>
        /// <returns>是否过滤</returns>
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                if (HotKeyAction.IsNotNull() && RegisterdHotKeys.ContainsKey((int)m.WParam))
                {
                    HotKeyAction.Invoke((int)m.WParam);
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 根据名称查找对应的HotKeyID,找不到时返回-1 + GetHotKeyID(string name)
        /// <summary>
        /// 根据名称查找对应的HotKeyID,找不到时返回-1
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetHotKeyID(string name)
        {
            if (RegisterdHotKeys.ContainsValue(name))
                return RegisterdHotKeys.Where(k => k.Value == name).First().Key;
            else return -1;
        }
        #endregion
    }
}
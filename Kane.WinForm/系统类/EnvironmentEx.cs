#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：EnvironmentEx
* 类 描 述 ：系统环境扩展类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.WinForm
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/12/20 22:40:56
* 更新时间 ：2019/12/20 22:40:56
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Kane.WinForm
{
    /// <summary>
    /// 系统环境扩展类
    /// </summary>
    public class EnvironmentEx
    {
        #region 获取操作系统版本 + GetOSVersion()
        /// <summary>
        /// 获取操作系统版本
        /// https://docs.microsoft.com/zh-cn/windows/win32/sysinfo/operating-system-version?redirectedfrom=MSDN
        /// </summary>
        /// <returns></returns>
        public static List<string> GetOSVersion()
        {
            List<string> result = new List<string>();
            float version = Environment.OSVersion.Version.Major + ((float)Environment.OSVersion.Version.Minor) / 10;
            switch (version)
            {
                case 5:
                    result.Add("Windows 2000");
                    break;
                case 5.1F:
                    result.Add("Windows XP");
                    break;
                case 5.2F:
                    result.Add("Windows XP 64-Bit Edition");
                    result.Add("Windows Server 2003");
                    result.Add("Windows Server 2003 R2");
                    break;
                case 6:
                    result.Add("Windows Vista");
                    result.Add("Windows Server 2008");
                    break;
                case 6.1F:
                    result.Add("Windows Server 2008 R2");
                    result.Add("Windows 7");
                    break;
                case 6.2F:
                    result.Add("Windows Server 2012");
                    result.Add("Windows 8");
                    break;
                case 6.3F:
                    result.Add("Windows Server 2012 R2");
                    result.Add("Windows 8.1");
                    break;
                case 10.0F:
                    result.Add("Windows Server 2016 Technical Preview");
                    result.Add("Windows 10");
                    break;
                default:
                    break;
            }
            return result;
        }
        #endregion

        #region 获取系统主题颜色，对应【深色】和【浅色】 + GetWindows10ThemeMode()
        /// <summary>
        /// 主题颜色枚举类
        /// </summary>
        public enum ThemeMode { Dark, Light }
        /// <summary>
        /// 获取系统主题颜色，对应【深色】和【浅色】
        /// 只支持 Windows 10 1903+ 以上
        /// </summary>
        /// <returns></returns>
        public static ThemeMode GetWindows10ThemeMode()
        {
            ThemeMode themeMode = ThemeMode.Light;
            try
            {
                RegistryKey temp = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", false);
                if (temp.GetValue("SystemUsesLightTheme") != null)
                {
                    themeMode = (int)(temp.GetValue("SystemUsesLightTheme")) == 0 ? ThemeMode.Dark : ThemeMode.Light;// 0:【深色】, 1:【浅色】
                }

            }
            catch { }
            return themeMode;
        }
        #endregion

        #region 获取屏幕DPI值 + GetScreenDpi()
        /// <summary>
        /// 获取屏幕DPI值
        /// </summary>
        /// <returns></returns>
        public static int GetScreenDpi()
        {
            using Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
            return (int)graphics.DpiX;
        }
        #endregion
    }
}
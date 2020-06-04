#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：RegistryHelper
* 类 描 述 ：注册表常用类
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.WinForm
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/11/3 22:29:46
* 更新时间 ：2020/06/01 12:29:46
* 版 本 号 ：v1.0.1.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using Microsoft.Win32;
using System;
using System.IO;

namespace Kane.WinForm
{
    /// <summary>
    /// 注册表常用类
    /// </summary>
    public static class RegistryHelper
    {
        #region 设置应用程序开机自动运行 SetAutoRun(string path, bool autoRun)
        /// <summary>
        /// 设置应用程序开机自动运行
        /// </summary>
        /// <param name="path">完整的路径 + 应用程序的文件名</param>
        /// <param name="autoRun">是否自动运行，为false时，取消自动运行</param>
        /// <param name="isAdmin">是否为本机所有用户，还是当前用户</param>
        /// <returns></returns>
        public static bool SetAutoRun(string path, bool autoRun,bool isAdmin = true)
        {
            RegistryKey reg = null;
            var state = false;
            try
            {
                if (!File.Exists(path)) throw new Exception("该文件不存在!");
                var fileName = Path.GetFileName(path);
                if (isAdmin)
                {
                    reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    if (reg == null) reg = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                }
                else
                {
                    reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    if (reg == null) reg = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                }
                if (autoRun) reg.SetValue(fileName, $"{path} -auto");
                else reg.SetValue(fileName, false);
                state = true;
            }
            catch
            {
                state = false;
            }
            finally
            {
                if (reg != null) reg.Close();
            }
            return state;
        }
        #endregion
    }
}
﻿#region << 版 本 注 释 >>
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
* 更新时间 ：2019/11/3 22:29:46
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Win32;

namespace Kane.WinForm
{
    public static class RegistryHelper
    {
        #region 设置应用程序开机自动运行 SetAutoRun(string path, bool autoRun)
        /// 设置应用程序开机自动运行
        /// </summary>
        /// <param name="path">完成的路径 + 应用程序的文件名</param>
        /// <param name="isAutoRun">是否自动运行，为false时，取消自动运行</param>
        /// <exception cref="System.Exception">路径有误时抛出异常</exception>
        public static bool SetAutoRun(string path, bool autoRun)
        {
            RegistryKey reg = null;
            var state = false;
            try
            {
                if (!File.Exists(path)) throw new Exception("该文件不存在!");
                var fileName = Path.GetFileName(path);
                reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (reg == null) reg = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                if (autoRun) reg.SetValue(fileName, $"{path} -auto");
                else reg.SetValue(fileName, false);
                state = true;
            }
            catch (Exception)
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

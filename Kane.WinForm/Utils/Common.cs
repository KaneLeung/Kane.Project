#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：Common
* 类 描 述 ：本程序集内部常用方法
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.WinForm
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/11/21 22:46:51
* 更新时间 ：2019/11/21 22:46:51
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Kane.WinForm
{
    internal static class Common
    {
        #region 根据名称获取图像资源文件
        internal static Image GetResourceImage(string name) => (Image)Properties.Resources.ResourceManager.GetObject(name);
        #endregion
    }
}

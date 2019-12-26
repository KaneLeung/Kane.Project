#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：ControlEx
* 类 描 述 ：控件方法扩展类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.WinForm
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/12/20 22:44:17
* 更新时间 ：2019/12/20 22:44:17
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kane.WinForm
{
    /// <summary>
    /// 控件方法扩展类
    /// </summary>
    public static class ControlEx
    {
        #region 递归查控件内所有子控件 + GetChildControls<T>(this Control control) where T : Control
        /// <summary>
        /// 递归查控件内所有子控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetChildControls<T>(this Control control) where T : Control
        {
            if (control.Controls.Count == 0) return Enumerable.Empty<T>();
            var children = control.Controls.OfType<T>().ToList();
            return children.SelectMany(GetChildControls<T>).Concat(children);
        } 
        #endregion
    }
}
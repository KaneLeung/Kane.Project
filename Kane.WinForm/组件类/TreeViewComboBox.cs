﻿#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：TreeViewComboBox
* 类 描 述 ：下拉树控件
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.WinForm
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/12/6 0:23:08
* 更新时间 ：2019/12/6 0:23:08
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
using System.Windows.Forms;

namespace Kane.WinForm
{
    /// <summary>
    /// 下拉树控件
    /// https://docs.microsoft.com/zh-cn/dotnet/api/system.windows.forms.toolstripdropdown?view=netcore-3.0
    /// </summary>
    public class TreeViewComboBox : ComboBox
    {
        ToolStripControlHost TreeViewHost;
        ToolStripDropDown ThisDropDown;
        public TreeViewComboBox()
        {
            TreeView treeView = new TreeView();
            treeView.AfterSelect += new TreeViewEventHandler(TreeView_AfterSelect);
            treeView.BorderStyle = BorderStyle.None;

            TreeViewHost = new ToolStripControlHost(treeView);
            ThisDropDown = new ToolStripDropDown();
            ThisDropDown.Width = this.Width;
            ThisDropDown.Items.Add(TreeViewHost);
        }
        public void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Text = TreeView.SelectedNode.Text;
            ThisDropDown.Close();
        }

        public TreeView TreeView
        {
            get { return TreeViewHost.Control as TreeView; }
        }

        private void ShowDropDown()
        {
            if (ThisDropDown != null)
            {
                TreeViewHost.Size = new Size(DropDownWidth - 2, DropDownHeight);
                ThisDropDown.Show(this, 0, this.Height + 1);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)WinMessages.WM_LBUTTONDBLCLK || m.Msg == (int)WinMessages.WM_LBUTTONDOWN)
            {
                ShowDropDown();
                return;
            }
            base.WndProc(ref m);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && ThisDropDown != null)
            {
                ThisDropDown.Dispose();
                ThisDropDown = null;
            }
            base.Dispose(disposing);
        }
    }
}

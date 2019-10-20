#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：KaneTextBox
* 类 描 述 ：带水印文本输入框
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.WinForm
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/20 23:10:54
* 更新时间 ：2019/10/20 23:10:54
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kane.WinForm
{
    public class KaneTextBox : TextBox
    {
        private string _Watermark;
        [DefaultValue("")]
        public string Watermark
        {
            get { return _Watermark; }
            set { _Watermark = value; this.Invalidate(); }
        }

        Color _WatermarkColor = SystemColors.GrayText;
        public Color WatermarkColor
        {
            get { return _WatermarkColor; }
            set { _WatermarkColor = value; Invalidate(); }
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0xf)
            {
                if (!this.Focused && string.IsNullOrEmpty(this.Text) && !string.IsNullOrEmpty(this.Watermark))
                {
                    using var g = this.CreateGraphics();
                    TextRenderer.DrawText(g, this.Watermark, this.Font, this.ClientRectangle, this.WatermarkColor, this.BackColor, TextFormatFlags.Top + 5 | TextFormatFlags.Left);
                }
            }
        }
        private bool ShouldSerializeHintColor()
        {
            return WatermarkColor != SystemColors.GrayText;
        }
        private void ResetHintColor()
        {
            WatermarkColor = SystemColors.GrayText;
        }
    }
}

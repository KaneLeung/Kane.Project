#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：WatermarkTextBox
* 类 描 述 ：带水印文本输入框
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.WinForm
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/20 23:10:54
* 更新时间 ：2019/12/26 23:10:54
* 版 本 号 ：v1.0.1.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Kane.WinForm
{
    /// <summary>
    /// 带水印文本输入框
    /// </summary>
    public class WatermarkTextBox : TextBox
    {
        #region 私有字段和公共属性
        private string _Watermark;
        /// <summary>
        /// 水印文本
        /// </summary>
        [Browsable(true), DefaultValue(""), Description("水印文本"), EditorBrowsable(EditorBrowsableState.Always)]
        public string Watermark
        {
            get => _Watermark;
            set { _Watermark = value; this.Invalidate(); }
        }

        private Color _WatermarkColor = SystemColors.GrayText;
        /// <summary>
        /// 水印文字颜色
        /// </summary>
        [Browsable(true), Description("水印文字颜色"), EditorBrowsable(EditorBrowsableState.Always)]
        public Color WatermarkColor
        {
            get => _WatermarkColor;
            set { _WatermarkColor = value; Invalidate(); }
        }


        private HorizontalAlignment _WaterTextAlign = HorizontalAlignment.Center;
        /// <summary>
        /// 水印水平对齐方向,默认居中
        /// </summary>
        [Browsable(true), Description("水印水平对齐方向"), EditorBrowsable(EditorBrowsableState.Always)]
        public HorizontalAlignment WaterTextAlign
        {
            get => _WaterTextAlign;
            set { _WaterTextAlign = value; Invalidate(); }
        }
        #endregion

        #region 触发事件 + WndProc(ref Message m)
        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0xf)
            {
                if (!this.Focused && string.IsNullOrEmpty(this.Text) && !string.IsNullOrEmpty(this.Watermark))
                {
                    using var g = this.CreateGraphics();
                    var align = _WaterTextAlign switch
                    {
                        HorizontalAlignment.Left => TextFormatFlags.VerticalCenter | TextFormatFlags.Left,
                        HorizontalAlignment.Center => TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter,
                        HorizontalAlignment.Right => TextFormatFlags.VerticalCenter | TextFormatFlags.Right,
                        _ => TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter
                    };
                    TextRenderer.DrawText(g, this.Watermark, this.Font, this.ClientRectangle, this.WatermarkColor, this.BackColor, align);
                }
            }
        }
        #endregion
    }
}
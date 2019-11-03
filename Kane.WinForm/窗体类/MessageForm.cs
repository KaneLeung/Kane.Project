#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：MessageForm
* 类 描 述 ：消息窗体
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.WinForm
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/11/3 22:57:05
* 更新时间 ：2019/11/3 22:57:05
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Kane.WinForm
{
    public class MessageForm : KaneForm
    {
        #region 私有成员
        private Label LB_Title;
        private Label LB_Content;
        private Button BTN_OK;
        private Button BTN_Cancel;
        #endregion

        #region 构造函数
        /// <summary>
        /// 系统默认颜色的对话框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        public MessageForm(string title, string content)
        {
            InitializeComponent();
            LB_Title.Text = title;
            LB_Content.Text = content;
            this.Paint += (object sender, PaintEventArgs e) =>
            {
                e.Graphics.DrawLines(new Pen(Color.Gray), new Point[] { new Point(0, 0), new Point(0, this.Height - 1), new Point(this.Width - 1, this.Height - 1), new Point(this.Width - 1, 0), new Point(0, 0) });
                //e.Graphics.Clear(Color.Gray);//以前用这个，需要加入Panel去除底色
            };
        }
        /// <summary>
        /// 可自定义颜色的对话框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="borderColor">边框颜色</param>
        /// <param name="backgroundColor">背景颜色，不能用透明</param>
        /// <param name="fontColor">字体颜色</param>
        public MessageForm(string title, string content, Color borderColor, Color backgroundColor, Color fontColor)
        {
            InitializeComponent();
            LB_Title.Text = title;
            LB_Content.Text = content;
            LB_Title.ForeColor = LB_Content.ForeColor = BTN_OK.ForeColor = BTN_Cancel.ForeColor = fontColor;
            this.BackColor = BTN_OK.BackColor = BTN_Cancel.BackColor = backgroundColor;
            this.Paint += (object sender, PaintEventArgs e) =>
            {
                e.Graphics.DrawLines(new Pen(borderColor), new Point[] {
                    new Point(0, 0), new Point(0, this.Height - 1),
                    new Point(this.Width - 1, this.Height - 1),
                    new Point(this.Width - 1, 0), new Point(0, 0) });
                //e.Graphics.Clear(Color.Gray);//以前用这个，需求加入Panel去除底色
            };
        }
        #endregion

        #region 布局 + InitializeComponent()
        private void InitializeComponent()
        {
            this.LB_Title = new Label();
            this.LB_Content = new Label();
            this.BTN_OK = new Button();
            this.BTN_Cancel = new Button();
            this.SuspendLayout();
            // 
            // LB_Title
            // 
            this.LB_Title.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.LB_Title.Font = new Font(this.Font.FontFamily, 13F, Font.Style, Font.Unit, 134);
            this.LB_Title.Location = new Point(30, 30);
            this.LB_Title.Margin = new Padding(0);
            this.LB_Title.Name = "LB_Title";
            this.LB_Title.Size = new Size(507, 24);
            this.LB_Title.Text = "这个是标题 ，可以是很长很长";
            this.LB_Title.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LB_Content
            // 
            this.LB_Content.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.LB_Content.AutoEllipsis = true;
            this.LB_Content.Font = new Font(Font.FontFamily, 10.5F, Font.Style, Font.Unit, 134);
            this.LB_Content.Location = new Point(30, 60);
            this.LB_Content.Margin = new Padding(0);
            this.LB_Content.Name = "LB_Content";
            this.LB_Content.Size = new Size(507, 100);
            this.LB_Content.Text = "这个是内容 ，可以是很长很长";
            // 
            // BTN_OK
            // 
            this.BTN_OK.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.BTN_OK.Location = new Point(351, 170);
            this.BTN_OK.Name = "BTN_OK";
            this.BTN_OK.Size = new Size(88, 30);
            this.BTN_OK.Text = "确　认";
            this.BTN_OK.Font = new Font(Font.FontFamily, 10F, Font.Style, Font.Unit, 134);
            this.BTN_Cancel.TextAlign = ContentAlignment.MiddleCenter;
            this.BTN_OK.UseVisualStyleBackColor = true;
            this.BTN_OK.Click += new EventHandler(BTN_OK_Click);
            // 
            // BTN_Cancel
            // 
            this.BTN_Cancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.BTN_Cancel.Location = new Point(459, 170);
            this.BTN_Cancel.Name = "BTN_Cancel";
            this.BTN_Cancel.Size = new Size(88, 30);
            this.BTN_Cancel.Text = "取　消";
            this.BTN_Cancel.Font = new Font(Font.FontFamily, 10F, Font.Style, Font.Unit, 134);
            this.BTN_Cancel.TextAlign = ContentAlignment.MiddleCenter;
            this.BTN_Cancel.UseVisualStyleBackColor = true;
            this.BTN_Cancel.Click += new EventHandler(BTN_Cancel_Click);
            // 
            // MessageForm
            // 
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new Size(567, 220);
            this.Controls.Add(this.BTN_Cancel);
            this.Controls.Add(this.BTN_OK);
            this.Controls.Add(this.LB_Content);
            this.Controls.Add(this.LB_Title);
            this.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "MessageForm";
            this.LockSize = true;
            this.ShowImageIcon = false;
            this.ShowSetting = false;
            this.ShowMinimized = false;
            this.ShowMaximized = false;
            this.ShowClose = false;
            this.KeyDown += new KeyEventHandler(Form_KeyDown);
            this.StartPosition = FormStartPosition.CenterParent;
            this.ResumeLayout(false);
        }
        #endregion

        #region 点击确认按钮事件 + BTN_OK_Click(object sender, EventArgs e)
        private void BTN_OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region 点击取消按钮事件 + BTN_Cancel_Click(object sender, EventArgs e)
        private void BTN_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region 侦听窗体按钮事件 + Form_KeyDown(object sender, KeyEventArgs e)
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                BTN_OK_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                BTN_Cancel_Click(sender, e);
            }
        }
        #endregion

        #region 统一设计窗体字体 + SetFont(Font font)
        /// <summary>
        /// 统一设计窗体字体
        /// </summary>
        /// <param name="font"></param>
        public void SetFont(Font font)
        {
            this.LB_Title.Font = new Font(font.FontFamily, 13F, font.Style, font.Unit, 134);
            this.LB_Content.Font = new Font(font.FontFamily, 10.5F, font.Style, font.Unit, 134);
            this.BTN_OK.Font = new Font(font.FontFamily, 10F, font.Style, font.Unit, 134);
            this.BTN_Cancel.Font = new Font(font.FontFamily, 10F, font.Style, font.Unit, 134);
        }
        #endregion

        #region 设置标题文本 + SetTitle(string value)
        public void SetTitle(string value) => this.LB_Title.Text = value;
        #endregion

        #region 设置内容文本 + SetContent(string value)
        public void SetContent(string value) => this.LB_Content.Text = value; 
        #endregion

        #region 释放资源 + Dispose(bool disposing)
        protected override void Dispose(bool disposing) => base.Dispose(disposing);
        #endregion
    }
}
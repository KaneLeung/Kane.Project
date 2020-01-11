#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：NumberForm
* 类 描 述 ：自定义数字窗体
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.WinForm
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/12/02 20:04:28
* 更新时间 ：2019/12/02 20:04:28
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kane.WinForm
{
    /// <summary>
    /// 自定义数字窗体
    /// </summary>
    public class NumberForm : KaneForm
    {
        #region 私有成员
        private Label LB_Title;
        private Label LB_Content;
        private NumberTextBox TB_Content;
        private Label BTN_ContentClear;
        private Label LB_Error;
        private Button BTN_OK;
        private Button BTN_Cancel;
        private readonly bool MULTI_LINE;
        private readonly bool REQUIRED;
        #endregion

        #region 公有成员
        /// <summary>
        /// 校验Func
        /// </summary>
        public event Func<string, (bool state, string message)> FuncCheckValues;
        private readonly bool NoDecimal = false;
        private readonly bool NoNegative = false;
        private readonly uint? Precision;
        #endregion

        #region 构造函数
        /// <summary>
        /// NumberForm构造函数
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="precisionm">小数位数</param>
        /// <param name="multiLine">是否多行</param>
        /// <param name="required">是否必填</param>
        /// <param name="noDecimal">是否整数</param>
        /// <param name="noNegative">是否非负数</param>
        public NumberForm(string title, string content, uint? precisionm, bool multiLine = false, bool required = true, bool noDecimal = false, bool noNegative = false)
        {
            MULTI_LINE = multiLine;
            REQUIRED = required;
            Precision = precisionm;
            NoDecimal = noDecimal;
            NoNegative = noNegative;
            InitializeComponent();
            this.Paint += (object sender, PaintEventArgs e) =>
            {
                e.Graphics.DrawLines(new Pen(Color.Gray), new Point[] { new Point(0, 0), new Point(0, this.Height - 1), new Point(this.Width - 1, this.Height - 1), new Point(this.Width - 1, 0), new Point(0, 0) });
            };
            LB_Title.Text = title;
            LB_Content.Text = content;
            TB_Content.Focus();
        }
        #endregion

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.LB_Title = new Label();
            this.LB_Content = new Label();
            this.TB_Content = new NumberTextBox();
            this.BTN_ContentClear = new Label();
            this.LB_Error = new Label();
            this.BTN_OK = new Button();
            this.BTN_Cancel = new Button();
            this.SuspendLayout();
            // 
            // LB_Title
            // 
            this.LB_Title.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.LB_Title.Font = new Font(Font.FontFamily, 13F, Font.Style, Font.Unit, 134);
            this.LB_Title.Location = new Point(30, 30);
            this.LB_Title.Margin = new Padding(0);
            this.LB_Title.Name = "LB_Title";
            this.LB_Title.Size = new Size(507, 26);
            this.LB_Title.AutoSize = true;
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
            this.LB_Content.Size = new Size(507, 21);
            this.LB_Content.Text = "请输入内容";
            this.LB_Content.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TB_Content
            // 
            this.TB_Content.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.TB_Content.Font = new Font(Font.FontFamily, 12F, Font.Style, Font.Unit, 134);
            this.TB_Content.Location = new Point(35, 82);
            this.TB_Content.Name = "TB_Content";
            this.TB_Content.Size = new Size(269, MULTI_LINE ? 80 : 29);//29
            this.TB_Content.Multiline = MULTI_LINE;
            this.TB_Content.NoDecimal = NoDecimal;
            this.TB_Content.NoNegative = NoNegative;
            this.TB_Content.Precision = Precision;
            this.TB_Content.TextChanged += new EventHandler(this.TB_Content_TextChanged);
            this.TB_Content.KeyDown += new KeyEventHandler(this.TB_Content_KeyDown);
            // 
            // BTN_ContentClear
            // 
            this.BTN_ContentClear.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.BTN_ContentClear.BackColor = Color.Transparent;
            this.BTN_ContentClear.Font = new Font(Font.FontFamily, 12F, Font.Style, Font.Unit, 134);
            this.BTN_ContentClear.ForeColor = SystemColors.AppWorkspace;
            this.BTN_ContentClear.Location = new Point(304, 83);
            this.BTN_ContentClear.Margin = new Padding(0);
            this.BTN_ContentClear.Name = "BTN_ContentClear";
            this.BTN_ContentClear.Size = new Size(29, 27);
            this.BTN_ContentClear.Click += new EventHandler(this.BTN_ContentClear_Click);
            // 
            // LB_Error
            // 
            this.LB_Error.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.LB_Error.BackColor = Color.Transparent;
            this.LB_Error.Font = new Font(Font.FontFamily, 12F, Font.Style, Font.Unit, 134);
            this.LB_Error.ForeColor = Color.Firebrick;
            this.LB_Error.Location = new Point(332, 86);
            this.LB_Error.Name = "LB_Error";
            this.LB_Error.Size = new Size(205, 25);
            // 
            // BTN_OK
            // 
            this.BTN_OK.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.BTN_OK.Location = new Point(351, 182);
            this.BTN_OK.Name = "BTN_OK";
            this.BTN_OK.Size = new Size(88, 30);
            this.BTN_OK.Text = "确　认";
            this.BTN_OK.Font = new Font(Font.FontFamily, 10F, Font.Style, Font.Unit, 134);
            this.BTN_OK.TextAlign = ContentAlignment.MiddleCenter;
            this.BTN_OK.UseVisualStyleBackColor = true;
            this.BTN_OK.Click += new EventHandler(this.BTN_OK_Click);
            // 
            // BTN_Cancel
            // 
            this.BTN_Cancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.BTN_Cancel.Location = new Point(459, 182);
            this.BTN_Cancel.Name = "BTN_Cancel";
            this.BTN_Cancel.Size = new Size(88, 30);
            this.BTN_Cancel.Text = "取　消";
            this.BTN_Cancel.Font = new Font(Font.FontFamily, 10F, Font.Style, Font.Unit, 134);
            this.BTN_Cancel.TextAlign = ContentAlignment.MiddleCenter;
            this.BTN_Cancel.UseVisualStyleBackColor = true;
            this.BTN_Cancel.Click += new EventHandler(this.BTN_Cancel_Click);
            // 
            // NumberForm
            // 
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new Size(567, 232);
            this.Controls.Add(this.LB_Title);
            this.Controls.Add(this.LB_Content);
            this.Controls.Add(this.TB_Content);
            this.Controls.Add(this.BTN_ContentClear);
            this.Controls.Add(this.LB_Error);
            this.Controls.Add(this.BTN_OK);
            this.Controls.Add(this.BTN_Cancel);
            this.Controls.SetChildIndex(this.LB_Title, 0);
            this.Controls.SetChildIndex(this.LB_Content, 0);
            this.Controls.SetChildIndex(this.TB_Content, 0);
            this.Controls.SetChildIndex(this.BTN_ContentClear, 0);
            this.Controls.SetChildIndex(this.LB_Error, 0);
            this.DoubleBuffered = true;
            this.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "NumberForm";
            this.LockSize = true;
            this.ShowImageIcon = false;
            this.ShowSetting = false;
            this.ShowMinimized = false;
            this.ShowMaximized = false;
            this.ShowClose = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.KeyDown += new KeyEventHandler(this.Form_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        #region 点击确认按钮事件 + BTN_OK_Click(object sender, EventArgs e)
        private void BTN_OK_Click(object sender, EventArgs e)
        {
            if (REQUIRED && TB_Content.Text.Trim().Length < 1)
            {
                LB_Error.Text = "必填信息为空，请输入。";
                TB_Content.Focus();
                return;
            }
            var (state, message) = FuncCheckValues?.Invoke(TB_Content.Text) ?? (false, "");
            if (state)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                LB_Error.Text = message;
                TB_Content.SelectAll();
                TB_Content.Focus();
            }
        }
        #endregion

        #region 点击取消按钮事件 + BTN_Cancel_Click(object sender, EventArgs e)
        private void BTN_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region 内容输入文本框文本改变事件 + TB_Content_TextChanged(object sender, EventArgs e)
        private void TB_Content_TextChanged(object sender, EventArgs e)
        {
            BTN_ContentClear.Text = TB_Content.Text.Length > 0 ? "×" : " ";
            if (TB_Content.Text.Length > 0)
                LB_Error.Text = "";
        }
        #endregion

        #region 点击内容清除小叉事件 + BTN_ContentClear_Click(object sender, EventArgs e)
        private void BTN_ContentClear_Click(object sender, EventArgs e)
        {
            TB_Content.Text = "";
        }
        #endregion

        #region 侦听键盘事件 + Form_KeyDown(object sender, KeyEventArgs e)
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;//将Handled设置为true，指示已经处理过KeyPress事件
                BTN_OK_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;//将Handled设置为true，指示已经处理过KeyPress事件
                BTN_OK_Click(sender, e);
            }
        }
        #endregion

        #region 内容输入框侦听按键事件 + TB_Content_KeyDown(object sender, KeyEventArgs e)
        private void TB_Content_KeyDown(object sender, KeyEventArgs e)
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
            this.TB_Content.Font = new Font(font.FontFamily, 12F, font.Style, font.Unit, 134);
            this.LB_Error.Font = new Font(font.FontFamily, 12F, font.Style, font.Unit, 134);
            this.BTN_OK.Font = new Font(font.FontFamily, 10F, font.Style, font.Unit, 134);
            this.BTN_Cancel.Font = new Font(font.FontFamily, 10F, font.Style, font.Unit, 134);
        }
        #endregion

        #region 释放资源 + Dispose(bool disposing)
        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) => base.Dispose(disposing);
        #endregion
    }
}
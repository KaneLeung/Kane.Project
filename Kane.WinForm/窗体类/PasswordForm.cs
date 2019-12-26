#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：PasswordForm
* 类 描 述 ：自定义密码输入窗体
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.WinForm
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/11/4 0:44:33
* 更新时间 ：2019/11/4 0:44:33
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
using Kane.Extension;

namespace Kane.WinForm
{
    /// <summary>
    /// 自定义密码输入窗体
    /// </summary>
    public class PasswordForm : KaneForm
    {
        #region 私有成员
        private Label LB_Title;
        private Label LB_Password;
        private TextBox TB_Password;
        private Label BTN_PasswordClear;
        private Label LB_Error;
        private Label LB_Confirm;
        private TextBox TB_Confirm;
        private Label BTN_ConfirmClear;
        private Button BTN_OK;
        private Button BTN_Cancel;
        private bool CONFIRM_MODE = false;
#if NETCOREAPP
        private static readonly int NETCORE_OFFSET = 8;//Netcore使用SetHighDpiMode(HighDpiMode.PerMonitorV2)时的偏移值
#else
        private static readonly int NETCORE_OFFSET = 0;
#endif
        #endregion

        #region 公有成员
        public event Func<string, (bool state, string message)> FuncCheckValues;
        #endregion

        #region 密码输入窗体的构造函数 + PasswordForm(string title, string subTitle, bool ConfirmMode = false, string secondTitle = "")
        /// <summary>
        /// 密码输入窗体的构造函数
        /// </summary>
        /// <param name="title">主标题</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="ConfirmMode">是否开启确认密码模式</param>
        /// <param name="secondTitle">第三标题</param>
        public PasswordForm(string title, string subTitle, bool ConfirmMode = false, string secondTitle = "")
        {
            CONFIRM_MODE = ConfirmMode;
            InitializeComponent();
            this.Paint += (object sender, PaintEventArgs e) =>
            {
                e.Graphics.DrawLines(new Pen(Color.Gray), new Point[] { new Point(0, 0), new Point(0, this.Height - 1), new Point(this.Width - 1, this.Height - 1), new Point(this.Width - 1, 0), new Point(0, 0) });
            };
            LB_Title.Text = title;
            LB_Password.Text = subTitle;
            LB_Confirm.Text = secondTitle;
            TB_Password.Focus();
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
            this.LB_Password = new Label();
            this.TB_Password = new TextBox();
            this.BTN_PasswordClear = new Label();
            this.LB_Error = new Label();
            this.LB_Confirm = new Label();
            this.TB_Confirm = new TextBox();
            this.BTN_ConfirmClear = new Label();
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
            this.LB_Title.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LB_Password
            // 
            this.LB_Password.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.LB_Password.AutoEllipsis = true;
            this.LB_Password.Font = new Font(Font.FontFamily, 10.5F, Font.Style, Font.Unit, 134);
            this.LB_Password.Location = new Point(30, 60);
            this.LB_Password.Margin = new Padding(0);
            this.LB_Password.Name = "LB_Password";
            this.LB_Password.Size = new Size(507, 21);
            this.LB_Password.Text = "请输入密码";
            this.LB_Password.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TB_Password
            // 
            this.TB_Password.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.TB_Password.Font = new Font(Font.FontFamily, 12F, Font.Style, Font.Unit, 134);
            this.TB_Password.Location = new Point(35, 82);
            this.TB_Password.Name = "TB_Password";
            this.TB_Password.PasswordChar = '●';
            this.TB_Password.Size = new Size(269, 29);
            this.TB_Password.BringToFront();
            this.TB_Password.TextChanged += new EventHandler(this.TB_Password_TextChanged);
            this.TB_Password.KeyDown += new KeyEventHandler(this.TB_Password_KeyDown);
            // 
            // BTN_PasswordClear
            // 
            this.BTN_PasswordClear.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.BTN_PasswordClear.BackColor = Color.Transparent;
            this.BTN_PasswordClear.Font = new Font(Font.FontFamily, 12F, Font.Style, Font.Unit, 134);
            this.BTN_PasswordClear.ForeColor = SystemColors.AppWorkspace;
            this.BTN_PasswordClear.Location = new Point(304, 84);
            this.BTN_PasswordClear.Margin = new Padding(0);
            this.BTN_PasswordClear.Name = "BTN_PasswordClear";
            this.BTN_PasswordClear.Size = new Size(29, 27);
            this.BTN_PasswordClear.TextAlign = ContentAlignment.MiddleLeft;
            this.BTN_PasswordClear.Click += new EventHandler(this.BTN_PasswordClear_Click);
            // 
            // LB_Error
            // 
            this.LB_Error.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.LB_Error.BackColor = Color.Transparent;
            this.LB_Error.Font = new Font(Font.FontFamily, 10F, Font.Style, Font.Unit, 134);
            this.LB_Error.ForeColor = Color.Firebrick;
            this.LB_Error.Location = new Point(328, 86);
            this.LB_Error.Name = "LB_Error";
            this.LB_Error.Size = new Size(205, 25);
            if (CONFIRM_MODE)
            {
                // 
                // LB_Confirm
                // 
                this.LB_Confirm.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                this.LB_Confirm.AutoEllipsis = true;
                this.LB_Confirm.Font = new Font(Font.FontFamily, 10.5F, Font.Style, Font.Unit, 134);
                this.LB_Confirm.Location = new Point(30, 111 + NETCORE_OFFSET);
                this.LB_Confirm.Margin = new Padding(0);
                this.LB_Confirm.Name = "LB_Confirm";
                this.LB_Confirm.Size = new Size(507, 21);
                this.LB_Confirm.Text = "请确认密码";
                this.LB_Confirm.TextAlign = ContentAlignment.MiddleLeft;
                // 
                // TB_Confirm
                // 
                this.TB_Confirm.Font = new Font(Font.FontFamily, 12F, Font.Style, Font.Unit, 134);
                this.TB_Confirm.Location = new Point(35, 133 + NETCORE_OFFSET);
                this.TB_Confirm.Name = "TB_Confirm";
                this.TB_Confirm.PasswordChar = '●';
                this.TB_Confirm.Size = new Size(269, 29);
                this.TB_Confirm.TextChanged += new EventHandler(this.TB_Confirm_TextChanged);
                this.TB_Confirm.KeyDown += new KeyEventHandler(this.TB_Confirm_KeyDown);
                // 
                // BTN_ConfirmClear
                // 
                this.BTN_ConfirmClear.BackColor = Color.Transparent;
                this.BTN_ConfirmClear.Font = new Font(Font.FontFamily, 12F, Font.Style, Font.Unit, 134);
                this.BTN_ConfirmClear.ForeColor = SystemColors.AppWorkspace;
                this.BTN_ConfirmClear.Location = new Point(304, 136 + NETCORE_OFFSET);
                this.BTN_ConfirmClear.Margin = new Padding(0);
                this.BTN_ConfirmClear.Name = "BTN_ConfirmClear";
                this.BTN_ConfirmClear.Size = new Size(29, 27);
                this.BTN_ConfirmClear.Click += new EventHandler(this.BTN_ConfirmClearClear_Click);
            }
            // 
            // BTN_OK
            // 
            this.BTN_OK.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.BTN_OK.Location = new Point(351, 182 + NETCORE_OFFSET);
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
            this.BTN_Cancel.Location = new Point(459, 182 + NETCORE_OFFSET);
            this.BTN_Cancel.Name = "BTN_Cancel";
            this.BTN_Cancel.Size = new Size(88, 30);
            this.BTN_Cancel.Text = "取　消";
            this.BTN_Cancel.Font = new Font(Font.FontFamily, 10F, Font.Style, Font.Unit, 134);
            this.BTN_Cancel.TextAlign = ContentAlignment.MiddleCenter;
            this.BTN_Cancel.UseVisualStyleBackColor = true;
            this.BTN_Cancel.Click += new EventHandler(this.BTN_Cancel_Click);
            // 
            // PasswordForm
            // 
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new Size(567, 232 + NETCORE_OFFSET);
            this.Controls.Add(this.LB_Title);
            this.Controls.Add(this.LB_Password);
            this.Controls.Add(this.TB_Password);
            this.Controls.Add(this.BTN_PasswordClear);
            this.Controls.Add(this.LB_Error);
            if (CONFIRM_MODE)
            {
                this.Controls.Add(this.LB_Confirm);
                this.Controls.Add(this.TB_Confirm);
                this.Controls.Add(this.BTN_ConfirmClear);
            }
            this.Controls.Add(this.BTN_OK);
            this.Controls.Add(this.BTN_Cancel);
            this.Controls.SetChildIndex(this.LB_Title, 0);
            this.Controls.SetChildIndex(this.LB_Password, 0);
            this.Controls.SetChildIndex(this.TB_Password, 0);
            this.Controls.SetChildIndex(this.BTN_PasswordClear, 0);
            this.Controls.SetChildIndex(this.LB_Error, 0);
            if (CONFIRM_MODE)
            {
                this.Controls.SetChildIndex(this.LB_Confirm, 0);
                this.Controls.SetChildIndex(this.TB_Confirm, 0);
                this.Controls.SetChildIndex(this.BTN_ConfirmClear, 0);
            }
            this.LockSize = true;
            this.ShowImageIcon = false;
            this.ShowSetting = false;
            this.ShowMinimized = false;
            this.ShowMaximized = false;
            this.ShowClose = false;
            this.DoubleBuffered = true;
            this.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "PasswordForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.KeyDown += new KeyEventHandler(this.Form_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        #region 点击确认按钮事件 + BTN_OK_Click(object sender, EventArgs e)
        private void BTN_OK_Click(object sender, EventArgs e)
        {
            if (TB_Password.Text.Length < 1)
            {
                LB_Error.Text = "密码不能为空";
                TB_Password.Focus();
                return;
            }
            if (CONFIRM_MODE && TB_Confirm.Text.Length < 1)
            {
                LB_Error.Text = "确认密码不能为空";
                TB_Confirm.Focus();
                return;
            }
            if (CONFIRM_MODE && TB_Password.Text != TB_Confirm.Text)
            {
                LB_Error.Text = "两次输入密码不一致";
                TB_Password.SelectAll();
                TB_Password.Focus();
                return;
            }
            var (state, message) = FuncCheckValues?.Invoke(TB_Password.Text) ?? (false, "");
            if (state)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                LB_Error.Text = message.IsValuable() ? message : "密码有误，请重新输入";
                TB_Password.SelectAll();
                TB_Password.Focus();
            }
        }
        #endregion

        #region 点击取消按钮事件 + BTN_Cancel_Click(object sender, EventArgs e)
        private void BTN_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region 密码输入框文本改变事件 + TB_Password_TextChanged(object sender, EventArgs e)
        private void TB_Password_TextChanged(object sender, EventArgs e)
        {
            BTN_PasswordClear.Text = TB_Password.Text.Length > 0 ? "×" : " ";
            if (TB_Password.Text.Length > 0) LB_Error.Text = "";
        }
        #endregion

        #region 点击密码清除小叉事件 + BTN_PasswordClear_Click(object sender, EventArgs e)
        private void BTN_PasswordClear_Click(object sender, EventArgs e) => TB_Password.Text = "";
        #endregion

        #region 确认密码框文本改变事件 + TB_Confirm_TextChanged(object sender, EventArgs e)
        private void TB_Confirm_TextChanged(object sender, EventArgs e)
        {
            BTN_ConfirmClear.Text = TB_Confirm.Text.Length > 0 ? "×" : " ";
            if (TB_Confirm.Text.Length > 0) LB_Error.Text = "";
        }
        #endregion

        #region 点击确认密码清除小叉事件 +BTN_ConfirmClearClear_Click(object sender, EventArgs e)
        private void BTN_ConfirmClearClear_Click(object sender, EventArgs e) => TB_Confirm.Text = "";
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
                BTN_Cancel_Click(sender, e);
            }
        }
        #endregion

        #region 密码输入框侦听按键事件 + TB_Password_KeyDown(object sender, KeyEventArgs e)
        private void TB_Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                if (CONFIRM_MODE)
                {
                    TB_Confirm.SelectAll();
                    TB_Confirm.Focus();
                }
                else BTN_OK_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                BTN_Cancel_Click(sender, e);
            }
        }
        #endregion

        #region 确认密码输入框侦听按键事件 + TB_Confirm_KeyDown(object sender, KeyEventArgs e)
        private void TB_Confirm_KeyDown(object sender, KeyEventArgs e)
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
            this.LB_Password.Font = new Font(font.FontFamily, 10.5F, font.Style, font.Unit, 134);
            this.TB_Password.Font = new Font(font.FontFamily, 12F, font.Style, font.Unit, 134);
            this.LB_Error.Font = new Font(font.FontFamily, 12F, font.Style, font.Unit, 134);
            if (CONFIRM_MODE)
            {
                this.LB_Confirm.Font = new Font(font.FontFamily, 10.5F, font.Style, font.Unit, 134);
                this.TB_Confirm.Font = new Font(font.FontFamily, 12F, font.Style, font.Unit, 134);
            }
            this.BTN_OK.Font = new Font(font.FontFamily, 10F, font.Style, font.Unit, 134);
            this.BTN_Cancel.Font = new Font(font.FontFamily, 10F, font.Style, font.Unit, 134);
        }
        #endregion

        #region 释放资源 + Dispose(bool disposing)
        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing">如果释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) => base.Dispose(disposing);
        #endregion
    }
}
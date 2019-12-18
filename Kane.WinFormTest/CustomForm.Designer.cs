using Kane.WinForm;

namespace Kane.WinFormTest
{
    partial class CustomForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BTN_Normal = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.TB_Console = new System.Windows.Forms.TextBox();
            this.LB_Title = new System.Windows.Forms.Label();
            this.TB_Input = new WatermarkTextBox();
            this.BTN_Message = new System.Windows.Forms.Button();
            this.BTN_Password = new System.Windows.Forms.Button();
            this.BTN_Prompt = new System.Windows.Forms.Button();
            // 
            // BTN_Normal
            // 
            this.BTN_Normal.Location = new System.Drawing.Point(3, 33);
            this.BTN_Normal.Name = "BTN_Normal";
            this.BTN_Normal.Size = new System.Drawing.Size(141, 45);
            this.BTN_Normal.TabIndex = 0;
            this.BTN_Normal.Text = "正常窗体";
            this.BTN_Normal.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 82);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(141, 45);
            this.button2.TabIndex = 0;
            this.button2.Text = "无图标窗体";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 132);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(141, 45);
            this.button3.TabIndex = 0;
            this.button3.Text = "带边框窗体";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(3, 182);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(141, 45);
            this.button4.TabIndex = 0;
            this.button4.Text = "无边框窗体";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // BTN_Message
            // 
            this.BTN_Message.Location = new System.Drawing.Point(3, 233);
            this.BTN_Message.Name = "BTN_Message";
            this.BTN_Message.Size = new System.Drawing.Size(141, 45);
            this.BTN_Message.TabIndex = 0;
            this.BTN_Message.Text = "消息窗体";
            this.BTN_Message.UseVisualStyleBackColor = true;
            this.BTN_Message.Click += new System.EventHandler(this.BTN_Message_Click);
            // 
            // BTN_Password
            // 
            this.BTN_Password.Location = new System.Drawing.Point(3, 283);
            this.BTN_Password.Name = "BTN_Password";
            this.BTN_Password.Size = new System.Drawing.Size(141, 45);
            this.BTN_Password.TabIndex = 0;
            this.BTN_Password.Text = "密码窗体";
            this.BTN_Password.UseVisualStyleBackColor = true;
            this.BTN_Password.Click += new System.EventHandler(this.BTN_Password_Click);
            // 
            // BTN_Prompt
            // 
            this.BTN_Prompt.Location = new System.Drawing.Point(3, 333);
            this.BTN_Prompt.Name = "BTN_Prompt";
            this.BTN_Prompt.Size = new System.Drawing.Size(141, 45);
            this.BTN_Prompt.TabIndex = 0;
            this.BTN_Prompt.Text = "提示窗体";
            this.BTN_Prompt.UseVisualStyleBackColor = true;
            this.BTN_Prompt.Click += new System.EventHandler(this.BTN_Prompt_Click);
            // 
            // TB_Console
            // 
            this.TB_Console.Location = new System.Drawing.Point(155, 111);
            this.TB_Console.Multiline = true;
            this.TB_Console.Name = "TB_Console";
            this.TB_Console.Size = new System.Drawing.Size(634, 331);
            this.TB_Console.TabIndex = 1;
            this.TB_Console.ReadOnly = true;
            // 
            // LB_Title
            // 
            this.LB_Title.AutoSize = true;
            this.LB_Title.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LB_Title.Location = new System.Drawing.Point(155, 40);
            this.LB_Title.Name = "LB_Title";
            this.LB_Title.Size = new System.Drawing.Size(172, 27);
            this.LB_Title.TabIndex = 2;
            this.LB_Title.Text = "全局钩子输出信息";
            // 
            // TB_Input
            // 
            this.TB_Input.Location = new System.Drawing.Point(155, 78);
            this.TB_Input.Name = "TB_Input";
            this.TB_Input.Size = new System.Drawing.Size(634, 27);
            this.TB_Input.TabIndex = 3;
            this.TB_Input.Watermark = "键盘输入测试";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TB_Input);
            this.Controls.Add(this.LB_Title);
            this.Controls.Add(this.TB_Console);
            this.Controls.Add(this.BTN_Normal);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.BTN_Message);
            this.Controls.Add(this.BTN_Password);
            this.Controls.Add(this.BTN_Prompt);
            this.LockSize = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Title = "主窗体（自定义标题）";
            this.ShowSetting = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
        }

        #endregion

        private System.Windows.Forms.Button BTN_Normal;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox TB_Console;
        private System.Windows.Forms.Label LB_Title;
        private WatermarkTextBox TB_Input;
        private System.Windows.Forms.Button BTN_Message;
        private System.Windows.Forms.Button BTN_Password;
        private System.Windows.Forms.Button BTN_Prompt;
    }
}
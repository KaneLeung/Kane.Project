namespace Kane.WinFormTest
{
    partial class MainForm
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
            this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
            this.BTN_NumberForm = new System.Windows.Forms.Button();
            this.BTN_CustomForm = new System.Windows.Forms.Button();
            this.BTN_CustomTabControl = new System.Windows.Forms.Button();
            this.BTN_Decimal = new System.Windows.Forms.Button();
            this.BTN_TreeviewComboBox = new System.Windows.Forms.Button();
            this.TLP_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // TLP_Main
            // 
            this.TLP_Main.ColumnCount = 5;
            this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.TLP_Main.Controls.Add(this.BTN_NumberForm, 3, 0);
            this.TLP_Main.Controls.Add(this.BTN_CustomForm, 0, 0);
            this.TLP_Main.Controls.Add(this.BTN_CustomTabControl, 1, 0);
            this.TLP_Main.Controls.Add(this.BTN_Decimal, 2, 0);
            this.TLP_Main.Controls.Add(this.BTN_TreeviewComboBox, 4, 0);
            this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_Main.Location = new System.Drawing.Point(0, 0);
            this.TLP_Main.Margin = new System.Windows.Forms.Padding(2);
            this.TLP_Main.Name = "TLP_Main";
            this.TLP_Main.RowCount = 7;
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.TLP_Main.Size = new System.Drawing.Size(600, 360);
            this.TLP_Main.TabIndex = 0;
            // 
            // BTN_NumberForm
            // 
            this.BTN_NumberForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BTN_NumberForm.Location = new System.Drawing.Point(363, 3);
            this.BTN_NumberForm.Name = "BTN_NumberForm";
            this.BTN_NumberForm.Size = new System.Drawing.Size(114, 45);
            this.BTN_NumberForm.TabIndex = 3;
            this.BTN_NumberForm.Text = "数字输入窗体";
            this.BTN_NumberForm.UseVisualStyleBackColor = true;
            this.BTN_NumberForm.Click += new System.EventHandler(this.BTN_NumberForm_Click);
            // 
            // BTN_CustomForm
            // 
            this.BTN_CustomForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BTN_CustomForm.Location = new System.Drawing.Point(2, 2);
            this.BTN_CustomForm.Margin = new System.Windows.Forms.Padding(2);
            this.BTN_CustomForm.Name = "BTN_CustomForm";
            this.BTN_CustomForm.Size = new System.Drawing.Size(116, 47);
            this.BTN_CustomForm.TabIndex = 0;
            this.BTN_CustomForm.Text = "自定义窗体";
            this.BTN_CustomForm.UseVisualStyleBackColor = true;
            this.BTN_CustomForm.Click += new System.EventHandler(this.BTN_CustomForm_Click);
            // 
            // BTN_CustomTabControl
            // 
            this.BTN_CustomTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BTN_CustomTabControl.Location = new System.Drawing.Point(122, 2);
            this.BTN_CustomTabControl.Margin = new System.Windows.Forms.Padding(2);
            this.BTN_CustomTabControl.Name = "BTN_CustomTabControl";
            this.BTN_CustomTabControl.Size = new System.Drawing.Size(116, 47);
            this.BTN_CustomTabControl.TabIndex = 1;
            this.BTN_CustomTabControl.Text = "自定义选项卡";
            this.BTN_CustomTabControl.UseVisualStyleBackColor = true;
            this.BTN_CustomTabControl.Click += new System.EventHandler(this.BTN_CustomTabControl_Click);
            // 
            // BTN_Decimal
            // 
            this.BTN_Decimal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BTN_Decimal.Location = new System.Drawing.Point(243, 3);
            this.BTN_Decimal.Name = "BTN_Decimal";
            this.BTN_Decimal.Size = new System.Drawing.Size(114, 45);
            this.BTN_Decimal.TabIndex = 2;
            this.BTN_Decimal.Text = "数字输入框";
            this.BTN_Decimal.UseVisualStyleBackColor = true;
            this.BTN_Decimal.Click += new System.EventHandler(this.BTN_Decimal_Click);
            // 
            // BTN_TreeviewComboBox
            // 
            this.BTN_TreeviewComboBox.Location = new System.Drawing.Point(483, 3);
            this.BTN_TreeviewComboBox.Name = "BTN_TreeviewComboBox";
            this.BTN_TreeviewComboBox.Size = new System.Drawing.Size(114, 45);
            this.BTN_TreeviewComboBox.TabIndex = 4;
            this.BTN_TreeviewComboBox.Text = "下拉树";
            this.BTN_TreeviewComboBox.UseVisualStyleBackColor = true;
            this.BTN_TreeviewComboBox.Click += new System.EventHandler(this.BTN_TreeviewComboBox_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.Controls.Add(this.TLP_Main);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "功能演示";
            this.TLP_Main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TLP_Main;
        private System.Windows.Forms.Button BTN_CustomForm;
        private System.Windows.Forms.Button BTN_CustomTabControl;
        private System.Windows.Forms.Button BTN_Decimal;
        private System.Windows.Forms.Button BTN_NumberForm;
        private System.Windows.Forms.Button BTN_TreeviewComboBox;
    }
}
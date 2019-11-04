using Kane.WinForm;
using Kane.WinForm.GlobalHook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kane.WinFormTest
{
    public partial class MainForm : KaneForm
    {
        private static KeyboardHook KEYBOARD_HOOK;
        private static MouseHook MOUSE_HOOK;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            KEYBOARD_HOOK = new KeyboardHook();
            KEYBOARD_HOOK.KeyEvent += KeyboaardHookEvent;
            var keyboardHookID = KEYBOARD_HOOK.Hook();
            MOUSE_HOOK = new MouseHook();
            MOUSE_HOOK.MouseEvent += MouseHookEvent;
            var mouseHookID = MOUSE_HOOK.Hook();
        }

        #region 键盘钩子事件
        private void KeyboaardHookEvent(object sender, KeyboardHookEventArgs e)
        {
            TB_Console.AppendText($"当前【{((e.KeyboardMessage==KeyboardMessages.KeyDown||e.KeyboardMessage==KeyboardMessages.SysKeyDown)?"按下":"弹起")}】【{e.KeyString}】键{Environment.NewLine}");
        }
        #endregion

        private void MouseHookEvent(object sender, MouseHookEventArgs e)
        {
            if (e.MouseMessage == MouseMessages.MouseMove)
            { 
            
            }
            else TB_Console.AppendText($"当前【{e.MouseMessage}】{Environment.NewLine}");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            KEYBOARD_HOOK.Unhook();
            MOUSE_HOOK.Unhook();
        }

        private void BTN_Message_Click(object sender, EventArgs e)
        {
            var message = new MessageForm("Hello World", "This is message windows form.");
            //message.SetFont(this.Font);
            message.ShowDialog();
        }

        private void BTN_Password_Click(object sender, EventArgs e)
        {
            var password = new PasswordForm("Hello World", "This is password windows form.");
            //password.SetFont(this.Font);
            password.ShowDialog();
        }

        private void BTN_Prompt_Click(object sender, EventArgs e)
        {
            var prompt = new PromptForm("Hello World", "This is prompt windows form.");
            //prompt.SetFont(this.Font);
            prompt.ShowDialog();
        }
    }
}

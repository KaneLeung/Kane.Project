using Kane.WinForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kane.WinFormTest
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void BTN_CustomForm_Click(object sender, EventArgs e)
        {
            new CustomForm().ShowDialog();
        }

        private void BTN_CustomTabControl_Click(object sender, EventArgs e)
        {
            new TabControlForm().ShowDialog();
        }

        private void BTN_Decimal_Click(object sender, EventArgs e)
        {
            new InputForm().ShowDialog();
        }

        private void BTN_NumberForm_Click(object sender, EventArgs e)
        {
            var numberForm = new NumberForm("请输入一个数字", "",3, false, true,false,true);
            numberForm.ShowDialog();
        }

        private void BTN_TreeviewComboBox_Click(object sender, EventArgs e)
        {
            var testForm = new TestForm();
            testForm.ShowDialog();
        }
    }
}

﻿using System;
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
    }
}

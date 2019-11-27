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
    public partial class TabControlForm : Form
    {
        public TabControlForm()
        {
            InitializeComponent();
        }

        public void Form_Load(object sender, EventArgs e)
        {
            this.propertyGrid1.SelectedObject = this.tabControl1;
        }

        public void PropertyValue_Changed(object s, PropertyValueChangedEventArgs e)
        {
            this.tabControl1.Refresh();
        }
    }
}

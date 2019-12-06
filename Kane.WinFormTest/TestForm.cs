using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kane.WinForm;

namespace Kane.WinFormTest
{
    public partial class TestForm : Form
    {
        private TreeViewComboBox comboBox1;
        public TestForm()
        {
            InitializeComponent();

            this.comboBox1 = new TreeViewComboBox();
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(0, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 0;
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("节点1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("节点2");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("节点3");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("节点0", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("节点5");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("节点6");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("节点7");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("节点8");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("节点4", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            treeNode1.Name = "节点1";
            treeNode1.Text = "节点1";
            treeNode2.Name = "节点2";
            treeNode2.Text = "节点2";
            treeNode3.Name = "节点3";
            treeNode3.Text = "节点3";
            treeNode4.Name = "节点0";
            treeNode4.Text = "节点0";
            treeNode5.Name = "节点5";
            treeNode5.Text = "节点5";
            treeNode6.Name = "节点6";
            treeNode6.Text = "节点6";
            treeNode7.Name = "节点7";
            treeNode7.Text = "节点7";
            treeNode8.Name = "节点8";
            treeNode8.Text = "节点8";
            treeNode9.Name = "节点4";
            treeNode9.Text = "节点4";
            comboBox1.TreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode9});
            this.Controls.Add(comboBox1);
        }


    }
}

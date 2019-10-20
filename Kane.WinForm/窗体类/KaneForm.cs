#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：KaneForm
* 类 描 述 ：自定义窗体
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.WinForm
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/20 22:43:29
* 更新时间 ：2019/10/20 22:43:29
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kane.WinForm
{
    public class KaneForm : Form
    {
        #region 可配置选项
        [Browsable(true), Description("是否显示自定义图片图标"), EditorBrowsable(EditorBrowsableState.Always)]
        public bool ShowImageIcon { get; set; } = true;
        [Description("ICON图标")]
        public Image IconImage { get; set; } = GetResourceImage("ICON_BLACK");
        [Description("非活动时ICON图标")]
        public Image DeactivateIconImage { get; set; } = GetResourceImage("ICON_GREY");
        [Description("窗体标题背景颜色")]
        public Color TitleBackColor { get; set; } = Color.Transparent;
        [Description("窗体标题")]
        public string Title { get; set; }
        [Description("窗体标题对齐方向")]
        public ContentAlignment TitleAlign { get; set; } = ContentAlignment.MiddleCenter;
        [Description("窗体标题字体")]
        public Font TitleFont { get; set; } = new Font("微软雅黑", 10.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
        [Description("是否显示设置按钮")]
        public bool ShowSetting { get; set; } = false;
        [Description("是否显示最小化按钮")]
        public bool ShowMinimized { get; set; } = true;
        [Description("是否显示最大化按钮")]
        public bool ShowMaximized { get; set; } = true;
        [Description("是否显示关闭按钮")]
        public bool ShowClose { get; set; } = true;
        [Description("是否锁定大小")]
        public bool LockSize { get; set; } = true;

        //自定义事件
        //增加 一个事件，项目里面用到这个控件的时候就可以使用这个事件了。  
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public event EventHandler SettingButtonClickEvent;
        #endregion

        #region 私有变量
        private readonly Panel HeaderPanel = new Panel();
        private readonly IContainer COMPONENTS = null;
        private Point MOVE_POINT;
        private static readonly Size ICON_SIZE = new Size(30, 30);
        private static readonly Padding ZERO_PADDING = new Padding(0);
        private bool ACTIVATED_STATE = true;
        private readonly PictureBox MAX_ICON = new PictureBox()
        {
            Image = GetResourceImage("MAXIMIZE_BLACK"),//生成类库时，记得加图标入资源文件
            Size = ICON_SIZE,
            Tag = "MAXIMIZE_",
            Margin = ZERO_PADDING,
            SizeMode = PictureBoxSizeMode.StretchImage,
            Padding = ZERO_PADDING,
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
        };
        #endregion

        #region 根据名称获取资源文件
        private static Image GetResourceImage(string name) => (Image)Properties.Resources.ResourceManager.GetObject(name);
        #endregion

        #region 重写Dispose + Dispose(bool disposing)
        protected override void Dispose(bool disposing)
        {
            if (disposing && (COMPONENTS != null)) COMPONENTS.Dispose();
            base.Dispose(disposing);
        }
        #endregion

        #region 构造函数
        public KaneForm()
        {
            this.SuspendLayout();
            HeaderPanel.Dock = DockStyle.Top; //先创建头部Panel
            HeaderPanel.Padding = ZERO_PADDING;
            HeaderPanel.Size = new Size(386, 30);
            HeaderPanel.MouseDown += Title_MouseDown;

            this.AutoScaleDimensions = new SizeF(7F, 17F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(388, 388);
            this.Controls.Add(HeaderPanel);
            this.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = FormBorderStyle.None;
            //this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Padding = new Padding(1);
            this.ShowIcon = false;
            this.ResumeLayout();
        }
        #endregion

        #region 控件首次创建时被调用
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            InitForm();
        }
        #endregion

        #region 重写窗体为当前活动窗体时，改变图标
        protected override void OnActivated(EventArgs e)
        {
            ACTIVATED_STATE = true;
            base.OnActivated(e);
            foreach (Control item in HeaderPanel.Controls)
            {
                if (!string.IsNullOrEmpty(item.Tag?.ToString()) && item.GetType().Name == "PictureBox")
                {
                    if (item.Tag?.ToString() == "ICON_")//ICON图标做特殊处理
                    {
                        ((PictureBox)item).Image = IconImage;
                    }
                    else ((PictureBox)item).Image = GetResourceImage($"{item.Tag?.ToString()}BLACK");
                }
            }
        }
        #endregion

        #region 重写窗体为非活动窗体时，改变图标
        protected override void OnDeactivate(EventArgs e)
        {
            ACTIVATED_STATE = false;
            base.OnDeactivate(e);
            foreach (Control item in HeaderPanel.Controls)
            {
                if (!string.IsNullOrEmpty(item.Tag?.ToString()) && item.GetType().Name == "PictureBox")
                {
                    if (item.Tag?.ToString() == "ICON_")//ICON图标做特殊处理
                    {
                        ((PictureBox)item).Image = DeactivateIconImage;
                    }
                    else ((PictureBox)item).Image = GetResourceImage($"{item.Tag?.ToString()}GREY");
                }
            }
        }
        #endregion

        #region 初始化自定义属性
        private void InitForm()
        {
            int count = 0;
            HeaderPanel.Size = new Size(this.Width - 2, 30);
            HeaderPanel.SuspendLayout();
            this.SuspendLayout();

            if (ShowImageIcon) //加入图标
            {
                count++;
                PictureBox icon = new PictureBox()
                {
                    Image = IconImage,//生成类库时，记得加图标入资源文件
                    Tag = "ICON_",
                    Size = ICON_SIZE,
                    Margin = ZERO_PADDING,
                    Padding = new Padding(0),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Location = new Point(0, 0),
                };
                HeaderPanel.Controls.Add(icon);
            }
            if (ShowClose)//加入关闭按钮
            {
                PictureBox icon = new PictureBox()
                {
                    Image = GetResourceImage("CLOSE_BLACK"),//生成类库时，记得加图标入资源文件
                    Size = ICON_SIZE,
                    Tag = "CLOSE_",
                    Margin = ZERO_PADDING,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Padding = ZERO_PADDING,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    Location = new Point(HeaderPanel.Width - (30 * count++), 0)
                };
                icon.Click += BTN_Close_Click;
                icon.MouseMove += BTN_Close_Move;
                icon.MouseLeave += BTN_Close_Leave;
                HeaderPanel.Controls.Add(icon);
            }
            if (ShowMaximized)//加入最大化按钮
            {
                MAX_ICON.Location = new Point(HeaderPanel.Width - (30 * count++), 0);
                MAX_ICON.Click += BTN_Max_Click;
                MAX_ICON.MouseMove += BTN_Max_Move;
                MAX_ICON.MouseLeave += BTN_Max_Leave;
                HeaderPanel.Controls.Add(MAX_ICON);
            }
            if (ShowMinimized)//加入最小化按钮
            {
                PictureBox icon = new PictureBox()
                {
                    Image = GetResourceImage("MINUS_BLACK"),//生成类库时，记得加图标入资源文件
                    Size = ICON_SIZE,
                    Tag = "MINUS_",
                    Margin = ZERO_PADDING,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Padding = ZERO_PADDING,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    Location = new Point(HeaderPanel.Width - (30 * count++), 0)
                };
                icon.Click += BTN_Min_Click;
                icon.MouseMove += BTN_Min_Move;
                icon.MouseLeave += BTN_Min_Leave;
                HeaderPanel.Controls.Add(icon);
            }
            if (ShowSetting) //加入设置按钮
            {
                PictureBox icon = new PictureBox()
                {
                    Image = GetResourceImage("SETTING_BLACK"),//生成类库时，记得加图标入资源文件
                    Size = ICON_SIZE,
                    Tag = "SETTING_",
                    Margin = ZERO_PADDING,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Padding = ZERO_PADDING,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    Location = new Point(HeaderPanel.Width - (30 * count++), 0),
                };
                icon.MouseMove += BTN_Setting_Move;
                icon.MouseLeave += BTN_Setting_Leave;
                icon.Click += SettingButton_Click;
                HeaderPanel.Controls.Add(icon);
            }
            var title = new Label()//加入标题栏
            {
                Font = TitleFont,
                Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right))),
                Location = new Point(ShowImageIcon ? 30 : 0, 0),
                Margin = ZERO_PADDING,
                Padding = new Padding(3, 1, 3, 1),
                Text = Title,
                TextAlign = TitleAlign,
                AutoEllipsis = true,
                Size = new Size(this.Width - 2 - (30 * count), 30)
            };
            title.MouseDown += Title_MouseDown;
            title.MouseMove += Title_MouseMove;
            title.DoubleClick += Title_DoubleClick;
            HeaderPanel.Controls.Add(title);

            this.Controls.Add(HeaderPanel);
            HeaderPanel.ResumeLayout();
            DoubleBuffered = true;//设置本窗体
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            this.ResumeLayout(false);
            //this.Activated += new EventHandler(this.Form_Activated);
        }
        #endregion

        #region 点击标题栏时，移动窗体
        /// <summary>
        /// 鼠标按下标题栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Title_MouseDown(object sender, MouseEventArgs e) => MOVE_POINT = new Point(e.X, e.Y);

        /// <summary>
        /// 鼠标在移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Title_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) this.Location = new Point(this.Location.X + e.X - MOVE_POINT.X, this.Location.Y + e.Y - MOVE_POINT.Y);
        }
        #endregion

        #region 鼠标进动到设置按钮上的效果
        private void BTN_Setting_Move(object sender, EventArgs e)
        {
            var temp = sender as PictureBox;
            temp.BackColor = Color.FromArgb(255, 18, 150, 219);
            temp.Image = GetResourceImage("SETTING_BLACK");
        }

        private void BTN_Setting_Leave(object sender, EventArgs e)
        {
            var temp = sender as PictureBox;
            temp.BackColor = Color.Transparent;
            temp.Image = ACTIVATED_STATE ? GetResourceImage("SETTING_BLACK") : GetResourceImage("SETTING_GREY");
        }
        #endregion

        #region 最小化按钮事件
        /// <summary>
        /// 最小化按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_Min_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;
        #endregion

        #region 鼠标进动到最小化按钮上的效果
        private void BTN_Min_Move(object sender, EventArgs e)
        {
            var temp = sender as PictureBox;
            temp.BackColor = Color.FromArgb(255, 232, 17, 35);
            temp.Image = GetResourceImage("MINUS_BLACK");
        }

        private void BTN_Min_Leave(object sender, EventArgs e)
        {
            var temp = sender as PictureBox;
            temp.BackColor = Color.Transparent;
            temp.Image = ACTIVATED_STATE ? GetResourceImage("MINUS_BLACK") : GetResourceImage("MINUS_GREY");
        }
        #endregion

        #region 最大化按钮事件
        /// <summary>
        /// 最大化按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_Max_Click(object sender, EventArgs e) => this.MaxNormalSwitch();
        #endregion

        #region 鼠标进动到最大化按钮上的效果
        private void BTN_Max_Move(object sender, EventArgs e)
        {
            var temp = sender as PictureBox;
            temp.BackColor = Color.FromArgb(255, 18, 150, 219);
            if (this.WindowState == FormWindowState.Maximized)
                temp.Image = GetResourceImage("MINIMIZE_BLACK");
            else temp.Image = GetResourceImage("MAXIMIZE_BLACK");
        }

        private void BTN_Max_Leave(object sender, EventArgs e)
        {
            var temp = sender as PictureBox;
            temp.BackColor = Color.Transparent;
            if (this.WindowState == FormWindowState.Maximized)
                temp.Image = ACTIVATED_STATE ? GetResourceImage("MINIMIZE_BLACK") : GetResourceImage("MINIMIZE_GREY");
            else temp.Image = ACTIVATED_STATE ? GetResourceImage("MAXIMIZE_BLACK") : GetResourceImage("MAXIMIZE_GREY");
        }
        #endregion

        #region 关闭按钮事件
        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_Close_Click(object sender, EventArgs e) => this.Close();
        #endregion

        #region 鼠标进动到关闭按钮上的效果
        private void BTN_Close_Move(object sender, EventArgs e)
        {
            var temp = sender as PictureBox;
            temp.BackColor = Color.FromArgb(255, 232, 17, 35);
            temp.Image = GetResourceImage("CLOSE_WHITE");
        }

        private void BTN_Close_Leave(object sender, EventArgs e)
        {
            var temp = sender as PictureBox;
            temp.BackColor = Color.Transparent;
            temp.Image = ACTIVATED_STATE ? GetResourceImage("CLOSE_BLACK") : GetResourceImage("CLOSE_GREY");
        }
        #endregion

        #region 标题双击最大化事件
        /// <summary>
        /// 标题双击最大化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Title_DoubleClick(object sender, EventArgs e)
        {
            if (!LockSize)  this.MaxNormalSwitch();
        }
        #endregion

        #region 最大化和正常状态切换
        /// <summary>
        /// 最大化和正常状态切换
        /// </summary>
        private void MaxNormalSwitch()
        {
            if (this.WindowState == FormWindowState.Maximized)//如果当前状态是最大化状态 则窗体需要恢复默认大小
            {
                this.SuspendLayout();
                this.WindowState = FormWindowState.Normal;
                this.Padding = new Padding(1);
                MAX_ICON.Image = GetResourceImage("MAXIMIZE_BLACK");//切换图标
                MAX_ICON.Tag = "MAXIMIZE_";
                this.ResumeLayout();
            }
            else
            {
                //防止遮挡任务栏
                this.SuspendLayout();
                this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.WindowState = FormWindowState.Maximized;
                this.Padding = ZERO_PADDING;
                MAX_ICON.Image = GetResourceImage("MINIMIZE_BLACK");//切换图标
                MAX_ICON.Tag = "MINIMIZE_";
                this.ResumeLayout();
            }
            //this.Invalidate();//使重绘
        }
        #endregion

        #region 调整大小
        private const int WM_NCHITTEST = 0x0084;
        private const int HTLEFT = 10;      //左边界
        private const int HTRIGHT = 11;     //右边界
        private const int HTTOP = 12;       //上边界
        private const int HTTOPLEFT = 13;   //左上角
        private const int HTTOPRIGHT = 14;  //右上角
        private const int HTBOTTOM = 15;    //下边界
        private const int HTBOTTOMLEFT = 0x10;    //左下角
        private const int HTBOTTOMRIGHT = 17;     //右下角
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (!LockSize && m.Msg == WM_NCHITTEST)
            {
                Point vPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                vPoint = PointToClient(vPoint);
                if (vPoint.X <= 5)
                    if (vPoint.Y <= 5) m.Result = (IntPtr)HTTOPLEFT;
                    else if (vPoint.Y >= ClientSize.Height - 5) m.Result = (IntPtr)HTBOTTOMLEFT;
                    else m.Result = (IntPtr)HTLEFT;
                else if (vPoint.X >= ClientSize.Width - 5)
                    if (vPoint.Y <= 5) m.Result = (IntPtr)HTTOPRIGHT;
                    else if (vPoint.Y >= ClientSize.Height - 5) m.Result = (IntPtr)HTBOTTOMRIGHT;
                    else m.Result = (IntPtr)HTRIGHT;
                else if (vPoint.Y <= 5) m.Result = (IntPtr)HTTOP;
                else if (vPoint.Y >= ClientSize.Height - 5) m.Result = (IntPtr)HTBOTTOM;
            }
        }
        #endregion

        #region 设置按钮点击事件
        private void SettingButton_Click(object sender, EventArgs e) => SettingButtonClickEvent?.Invoke(this, e);
        #endregion
    }
}

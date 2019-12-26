#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：KaneTabControl
* 类 描 述 ：自定义TabControl
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.WinForm
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/11/26 21:27:59
* 更新时间 ：2019/11/30 23:27:59
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using Kane.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace Kane.WinForm
{
    public class KaneTabControl : TabControl
    {
        #region 使用到的枚举
        #region 图标方向，目前只开发到横向模式，纵向模式待开发
        /// <summary>
        /// 图标方向，目前只开发到横向模式，纵向模式待开发
        /// </summary>
        public enum IconDirection
        {
            [Description("纵向模式")]
            Vertical = 0,
            [Description("横向模式")]
            Horizontal = 1,
        }
        #endregion

        #region 选项卡标签标题对齐方向
        /// <summary>
        /// 选项卡标签标题对齐方向
        /// </summary>
        public enum TextAlign
        {
            /// <summary>
            /// 指定文本对齐靠近布局。 在从左到右布局中，保留近的位置。 在从右到左布局中，近的位置是右。
            /// </summary>
            Near = 0,
            /// <summary>
            /// 指定文本在布局矩形的中心对齐。
            /// </summary>
            Center = 1,
            /// <summary>
            /// 指定文本对齐与相差甚远的布局矩形的来源位置。 在从左到右布局中，远的位置是右。 在从右到左布局中，保留远的位置。
            /// </summary>
            Far = 2
        }
        #endregion

        #region 选项卡标签是否显示右上角关闭按钮
        /// <summary>
        /// 选项卡标签是否显示右上角关闭按钮
        /// </summary>
        public enum CloseState
        {
            /// <summary>
            /// 不显示关闭图标，和关闭触发事件
            /// </summary>
            [Description("不显示")]
            None = 0,
            /// <summary>
            /// 显示关闭图标，并点击时触发事件
            /// </summary>
            [Description("总是显示")]
            Always = 1,
        }
        #endregion

        #region 预设颜色方案和自定义方案
        /// <summary>
        /// 预设颜色方案和自定义方案
        /// </summary>
        public enum Style
        {
            /// <summary>
            /// 自定义
            /// </summary>
            [Description("自定义")]
            Custom = -1,
            /// <summary>
            /// 预设赤色
            /// </summary>
            [Description("预设赤色")]
            DefaultRed = 0,
            /// <summary>
            /// 预设蓝色
            /// </summary>
            [Description("预设蓝色")]
            DefaultBlue = 1,
            /// <summary>
            /// 预设橙色
            /// </summary>
            [Description("预设橙色")]
            DefaultOrange = 2,
            /// <summary>
            /// 预设藏青
            /// </summary>
            [Description("预设藏青")]
            DefaultCyan = 3,
            /// <summary>
            /// 预设墨绿
            /// </summary>
            [Description("预设墨绿")]
            DefaultGreen = 4,
            /// <summary>
            /// 预设雅黑
            /// </summary>
            [Description("预设雅黑")]
            DefaultBlack = 5,
            /// <summary>
            /// 预设东哥红
            /// </summary>
            [Description("预设东哥红")]
            DefaultDRed = 6,
        }
        #endregion
        #endregion

        #region 可配置选项
        [Browsable(true), Description("图标显示方向"), EditorBrowsable(EditorBrowsableState.Always)]
        public IconDirection Mode { get; set; } = IconDirection.Horizontal;
        [Browsable(true), Description("颜色方案，选中自定义颜色自定义才生效"), EditorBrowsable(EditorBrowsableState.Always)]
        public Style ColorStyle { get; set; } = Style.DefaultRed;
        [Browsable(true), Description("Tab标题文本对齐方向"), EditorBrowsable(EditorBrowsableState.Always)]
        public TextAlign TitleAlign { get; set; } = TextAlign.Center;


        //////////////////////////////////选中顶部//////////////////////////////////
        [Browsable(true), Description("是否显示选中的标签顶部墨水条"), EditorBrowsable(EditorBrowsableState.Always)]
        public bool ShowSelectedTopInkBar { get; set; } = true;
        [Browsable(true), Description("选中的标签顶部墨水条宽度"), EditorBrowsable(EditorBrowsableState.Always)]
        public uint SelectedTopInkBarWidth { get; set; } = 3;
        [Browsable(true), Description("选中的标签顶部墨水条颜色，用户选择自定义时才生效"), EditorBrowsable(EditorBrowsableState.Always)]
        public Color SelectedTopInkBarColor { get; set; } = SystemColors.GrayText;
        //////////////////////////////////选中顶部//////////////////////////////////
        //////////////////////////////////选中底部//////////////////////////////////
        [Browsable(true), Description("是否显示选中的标签底部墨水条"), EditorBrowsable(EditorBrowsableState.Always)]
        public bool ShowSelectedBottomInkBar { get; set; } = true;
        [Browsable(true), Description("选中的标签底部墨水条宽度"), EditorBrowsable(EditorBrowsableState.Always)]
        public uint SelectedBottomInkBarWidth { get; set; } = 3;
        [Browsable(true), Description("选中的标签底部墨水条颜色，用户选择自定义时才生效"), EditorBrowsable(EditorBrowsableState.Always)]
        public Color SelectedBottomInkBarColor { get; set; } = SystemColors.GrayText;
        //////////////////////////////////选中底部//////////////////////////////////
        //////////////////////////////////选左右部//////////////////////////////////
        [Browsable(true), Description("是否显示选中标签左右边框"), EditorBrowsable(EditorBrowsableState.Always)]
        public bool ShowSlectedTabBorder { get; set; } = true;
        [Browsable(true), Description("选中标签左右边框颜色"), EditorBrowsable(EditorBrowsableState.Always)]
        public Color SelectedTabBorderColor { get; set; } = SystemColors.GrayText;
        //////////////////////////////////选左右部//////////////////////////////////


        //////////////////////////////////非选中顶部//////////////////////////////////
        [Browsable(true), Description("是否显示非选中标签顶部墨水条"), EditorBrowsable(EditorBrowsableState.Always)]
        public bool ShowTopInkBar { get; set; } = true;

        [Browsable(true), Description("非选中标签顶部墨水条宽度"), EditorBrowsable(EditorBrowsableState.Always)]
        public uint TopInkBarWidth { get; set; } = 1;
        [Browsable(true), Description("非选中标签顶部墨水条颜色，用户选择自定义时才生效"), EditorBrowsable(EditorBrowsableState.Always)]
        public Color TopInkBarColor { get; set; } = SystemColors.GrayText;
        //////////////////////////////////非选中顶部//////////////////////////////////
        //////////////////////////////////非选中底部//////////////////////////////////
        [Browsable(true), Description("是否显示非选中标签底部墨水条"), EditorBrowsable(EditorBrowsableState.Always)]
        public bool ShowBottomInkBar { get; set; } = true;
        [Browsable(true), Description("非选中标签底部墨水条宽度"), EditorBrowsable(EditorBrowsableState.Always)]
        public uint BottomInkBarWidth { get; set; } = 3;
        [Browsable(true), Description("非选中标签底部墨水条颜色，用户选择自定义时才生效"), EditorBrowsable(EditorBrowsableState.Always)]
        public Color BottomInkBarColor { get; set; } = SystemColors.GrayText;
        //////////////////////////////////非选中底部//////////////////////////////////
        //////////////////////////////////非选左右部//////////////////////////////////
        [Browsable(true), Description("是否显示非选中标签左右边框"), EditorBrowsable(EditorBrowsableState.Always)]
        public bool ShowTabBorder { get; set; } = true;
        [Browsable(true), Description("非选中标签左右边框颜色"), EditorBrowsable(EditorBrowsableState.Always)]
        public Color TabBorderColor { get; set; } = SystemColors.GrayText;
        //////////////////////////////////非选左右部//////////////////////////////////

        [Browsable(true), Description("是否显示关闭按钮"), EditorBrowsable(EditorBrowsableState.Always)]
        public CloseState ShowCloseButton { get; set; } = CloseState.Always;
        [Browsable(true), Description("自定义关闭图标，12像素"), EditorBrowsable(EditorBrowsableState.Always)]
        public Image CloseImage { get; set; } = Common.GetResourceImage("CLOSE_BLACK_12");
        [Browsable(true), Description("自定义鼠标经过时关闭图标，12像素"), EditorBrowsable(EditorBrowsableState.Always)]
        public Image CloseHoverImage { get; set; } = Common.GetResourceImage("CLOSE_RED_12");
        [Browsable(true), Description("隐藏关闭按钮的标签索引"), EditorBrowsable(EditorBrowsableState.Always)]
        public IEnumerable<int> HideCloseButtonIndex { get; set; } = new List<int>();

        [Browsable(true), Description("点击关闭按钮事件"), EditorBrowsable(EditorBrowsableState.Always)]
        public event EventHandler CloseButtonClickEvent;
        #endregion

        #region 私有变量
        /// <summary>
        /// 预设方案的颜色
        /// </summary>
        private readonly Color[] DEFAULT_COLOR = new Color[] {
            Color.FromArgb(255, 87, 34),//赤色
            Color.FromArgb(1,170,237),//蓝色
            Color.FromArgb(255,184,0),//橙色
            Color.FromArgb(47,64,86),//藏青
            Color.FromArgb(0,150,136),//墨绿
            Color.FromArgb(57,61,73),//雅黑
            Color.FromArgb(225,37,27),//东哥红
        };
        private readonly int CLOSE_IMAGE_WIDTH = 12;//关闭图标边长
        private bool HOVERED = false;//是否鼠标经过关闭按钮标志
        private int HOVER_INDEX = -1;//当经过关闭按钮时，所有的选项卡标签Index 
        #endregion

        #region 构造函数 + KaneTabControl()
        public KaneTabControl()
        {
            base.SetStyle(
                ControlStyles.UserPaint |                      // 控件将自行绘制，而不是通过操作系统来绘制  
                ControlStyles.OptimizedDoubleBuffer |          // 该控件首先在缓冲区中绘制，而不是直接绘制到屏幕上，这样可以减少闪烁  
                ControlStyles.AllPaintingInWmPaint |           // 控件将忽略 WM_ERASEBKGND 窗口消息以减少闪烁  
                ControlStyles.ResizeRedraw |                   // 在调整控件大小时重绘控件  
                ControlStyles.SupportsTransparentBackColor,    // 控件接受 alpha 组件小于 255 的 BackColor 以模拟透明  
                true);                                         // 设置以上值为 true  
            base.UpdateStyles();
            this.SizeMode = TabSizeMode.Fixed;  // 大小模式为固定   
            this.Margin = new Padding(0);
            this.MouseDown += TabMouseDown;
            this.MouseMove += TabMouseMove;
            this.MouseLeave += TabMouseLeave;
        }
        #endregion

        #region 自定义OnPaint + OnPaint(PaintEventArgs e)
        protected override void OnPaint(PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Transparent);
            #region 绘制底部墨水条
            if (ShowBottomInkBar && SelectedIndex >= 0)//显示底部墨水条
            {
                pen = new Pen(ColorStyle == Style.Custom ? BottomInkBarColor : DEFAULT_COLOR[(int)ColorStyle], BottomInkBarWidth);
                //e.Graphics.DrawRectangle(Pens.Blue, ClientRectangle.X + 1, ClientRectangle.Y + 1, this.Width - 3, this.ItemSize.Height - 1);//【调试】TabItem除去边框后范围
                var selectRect = this.GetTabRect(this.SelectedIndex);
                e.Graphics.DrawLine(pen, ClientRectangle.X + ((SelectedIndex == 0 && !ShowSlectedTabBorder) ? 2 : 1), ClientRectangle.Y + 1 + ItemSize.Height,
                    selectRect.X, ClientRectangle.Y + 1 + ItemSize.Height);//不显示选中标签时边框向右偏移+1
                e.Graphics.DrawLine(pen, selectRect.Right - 1, ClientRectangle.Y + 1 + ItemSize.Height,
                    this.Width - 1, ClientRectangle.Y + 1 + ItemSize.Height);
            }
            #endregion
            for (int i = 0; i < this.TabCount; i++)
            {
                var offset = i == SelectedIndex ? 0 : 2;//非选中向下偏移值
                var tabRect = this.GetTabRect(i);//2,2 当前Index的选项卡标签
                //e.Graphics.DrawRectangle(Pens.Red, tabRect.X - 1, tabRect.Y - 1, tabItem.Width, tabItem.Height - 1);//【调试】背景
                #region 开始绘制图标
                //e.Graphics.DrawRectangle(Pens.Black, tabRect.X + 4, tabRect.Y + 2, tabItem.Height - 7, tabItem.Height - 7);//【调试】Icon
                bool hasIcon = false;
                if (this.ImageList != null)//绑定的ImageList不为空，并且开启显示图标
                {
                    int imageIndex = this.TabPages[i].ImageIndex;
                    string imageKey = this.TabPages[i].ImageKey;
                    Image icon = new Bitmap(1, 1);
                    if (imageIndex > -1 && imageIndex < this.ImageList.Images.Count)
                    {
                        icon = this.ImageList.Images[imageIndex];
                        hasIcon = true;
                    }
                    if (!hasIcon && imageKey.IsValuable())
                    {
                        icon = this.ImageList.Images[imageKey];
                        hasIcon = true;
                    }
                    if (hasIcon) e.Graphics.DrawImage(icon, tabRect.X + 4, tabRect.Y + 2 + offset, ItemSize.Height - 7, ItemSize.Height - 7);//找到对应的图标
                }
                //else //【调试】用的
                //{
                //    //var icon = Utils.GetResourceImage("black_32");
                //    var icon = Utils.GetResourceImage("chip_32");
                //    e.Graphics.DrawImage(icon, tabRect.X + 4, tabRect.Y + 2 + offset, tabItem.Height - 7, tabItem.Height - 7);
                //    hasIcon = true;
                //}
                #endregion
                #region 绘制关闭图标结束
                PaintCloseButton(e.Graphics, i);
                #endregion
                #region 绘制标题开始
                var fontX = tabRect.X + (hasIcon ? ItemSize.Height - 1 : 6);
                //SizeF textSize = TextRenderer.MeasureText(this.TabPages[i].Text, this.Font);//获取文本长宽
                //e.Graphics.DrawRectangle(Pens.Yellow, fontX, tabRect.Y + 2 +2 , tabItem.Width - (2 + 4 + tabItem.Height - 7 + 2) - 11, tabItem.Height - 7);//【调试】标题Rec
                var format = new StringFormat
                {
                    FormatFlags = StringFormatFlags.NoWrap,
                    LineAlignment = StringAlignment.Center,
                    Alignment = (StringAlignment)(int)TitleAlign
                };
                //e.Graphics.DrawString(TabPages[i].Text, Font, SystemBrushes.ControlText, new RectangleF(fontX, tabRect.Y + 2 + 2 + offset, tabItem.Width - (2 + 4 + (hasIcon ? (tabItem.Height - 7) : 0) + 2) - 11, tabItem.Height - 7), format);
                e.Graphics.DrawString(TabPages[i].Text, Font, SystemBrushes.ControlText,
                    new RectangleF(fontX, tabRect.Y + 4 + offset, ItemSize.Width - (hasIcon ? (ItemSize.Height + 12) : 19), ItemSize.Height - 7), format);
                #endregion
                #region 绘制标签页左右边框开始
                if (i == SelectedIndex)
                {
                    if (ShowSlectedTabBorder)
                    {
                        pen = new Pen(ColorStyle == Style.Custom ? SelectedTabBorderColor : DEFAULT_COLOR[(int)ColorStyle], 1);//绘制水墨条
                        e.Graphics.DrawLine(pen, tabRect.X - 1, tabRect.Y - 1, tabRect.X - 1, tabRect.Y - 1 + tabRect.Height);
                        e.Graphics.DrawLine(pen, tabRect.X - 1 + tabRect.Width, tabRect.Y - 1, tabRect.X - 1 + tabRect.Width, tabRect.Y - 1 + tabRect.Height);
                    }
                }
                else if (i == SelectedIndex - 1)//选中标签的左边标签,只绘左边的边框
                {
                    if (ShowTabBorder)
                    {
                        pen = new Pen(ColorStyle == Style.Custom ? TabBorderColor : SystemColors.GrayText, 1);//绘制水墨条
                        e.Graphics.DrawLine(pen, tabRect.X - 1, tabRect.Y + 2, tabRect.X - 1, tabRect.Y - 1 + tabRect.Height - 2);
                    }
                }
                else if (i == SelectedIndex + 1)//选中标签的右边标签,只绘右边的边框
                {
                    if (ShowTabBorder)
                    {
                        pen = new Pen(ColorStyle == Style.Custom ? TabBorderColor : SystemColors.GrayText, 1);//绘制水墨条
                        e.Graphics.DrawLine(pen, tabRect.X - 1 + tabRect.Width, tabRect.Y + 2, tabRect.X - 1 + tabRect.Width, tabRect.Y - 1 + tabRect.Height - 2);
                    }
                }
                else  //绘制两边的边框
                {
                    if (ShowTabBorder)
                    {
                        pen = new Pen(ColorStyle == Style.Custom ? TabBorderColor : SystemColors.GrayText, 1);//绘制水墨条
                        e.Graphics.DrawLine(pen, tabRect.X - 1, tabRect.Y + 2, tabRect.X - 1, tabRect.Y - 1 + tabRect.Height - 2);
                        e.Graphics.DrawLine(pen, tabRect.X - 1 + tabRect.Width, tabRect.Y + 2, tabRect.X - 1 + tabRect.Width, tabRect.Y - 1 + tabRect.Height - 2);
                    }
                }
                #endregion
                #region 绘制标签墨水条
                if (i == SelectedIndex)//选中的标签绘制墨水条
                {
                    if (ShowSelectedTopInkBar)//是否显示选中标签顶部墨水条
                    {
                        pen = new Pen(ColorStyle == Style.Custom ? SelectedTopInkBarColor : DEFAULT_COLOR[(int)ColorStyle], SelectedTopInkBarWidth);//绘制项部水墨条
                        e.Graphics.DrawLine(pen, tabRect.X - 1, tabRect.Y - 1, tabRect.X + ItemSize.Width, tabRect.Y - 1);
                    }
                    if (ShowSelectedBottomInkBar)//是否显示选中标签底部墨水条
                    {
                        pen = new Pen(ColorStyle == Style.Custom ? SelectedBottomInkBarColor : SystemColors.Control, SelectedBottomInkBarWidth);
                        e.Graphics.DrawLine(pen, tabRect.X, tabRect.Y - 1 + tabRect.Height, tabRect.X + ItemSize.Width - 1, tabRect.Y - 1 + tabRect.Height);
                    }
                }
                else
                {
                    if (ShowTopInkBar)//绘制非选中的灰色水墨条
                    {
                        pen = new Pen(ColorStyle == Style.Custom ? TopInkBarColor : SystemColors.GrayText, TopInkBarWidth);
                        e.Graphics.DrawLine(pen, tabRect.X, tabRect.Y + 2, tabRect.X + ItemSize.Width - 1, tabRect.Y + 2);
                    }
                }
                #endregion
            }
            pen.Dispose();
        }
        #endregion

        #region 鼠标点击选项卡标签关闭按钮时事件 + TabMouseDown(object sender, MouseEventArgs e)
        private void TabMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ShowCloseButton != CloseState.None && !HideCloseButtonIndex.Any(k => k == SelectedIndex))//是鼠标右击，并且是显示关闭按钮
            {
                //计算关闭区域   
                Rectangle tabRect = this.GetTabRect(this.SelectedIndex);
                //如果鼠标在区域内就关闭选项卡   
                if (e.X >= (tabRect.Right - CLOSE_IMAGE_WIDTH - 1) && e.X < tabRect.Right && e.Y >= tabRect.Y && e.Y <= tabRect.Y + CLOSE_IMAGE_WIDTH)
                {
                    CloseButtonClickEvent?.Invoke(this, e);
                    //Console.WriteLine($"点击关闭按钮事件，当前坐标为{e.X},{e.Y}，点击关闭的选项卡Index为【{this.SelectedIndex}】");
                }
            }
        }
        #endregion

        #region 鼠标经过选项卡标签关闭按钮时事件 + TabMouseMove(object sender, MouseEventArgs e)
        private void TabMouseMove(object sender, MouseEventArgs e)
        {
            if (ShowCloseButton != CloseState.None)
            {
                bool flag = false;
                for (int i = 0; i < this.TabCount; i++)
                {
                    if (!HideCloseButtonIndex.Any(k => k == i))
                    {
                        var tabRect = this.GetTabRect(i);
                        if (e.X >= (tabRect.Right - CLOSE_IMAGE_WIDTH - 1) && e.X < tabRect.Right && e.Y >= tabRect.Y && e.Y <= tabRect.Y + CLOSE_IMAGE_WIDTH)
                        {
                            HOVER_INDEX = i;
                            flag = true;
                            break;
                        }
                    }
                }
                if (flag != HOVERED)//发生改变时，触发OnPaint
                {
                    HOVERED = flag;
                    //Console.WriteLine($"状态发生改变，当前坐标为{e.X},{e.Y}");
                    RepaintCloseButton(this.CreateGraphics());
                }
            }
        }
        #endregion

        #region 鼠标移出控件和关闭按钮时事件 + TabMouseLeave(object sender, EventArgs e)
        private void TabMouseLeave(object sender, EventArgs e)
        {
            if (HOVERED && ShowCloseButton != CloseState.None)
            {
                HOVERED = false;
                RepaintCloseButton(this.CreateGraphics());
            }
        }
        #endregion

        #region 重绘关闭按钮方法 + RepaintCloseButton(Graphics graphics)
        private void RepaintCloseButton(Graphics graphics)
        {
            //Console.WriteLine("RepaintCloseButton");
            for (int i = 0; i < this.TabCount; i++)
            {
                PaintCloseButton(graphics, i);
            }
        }
        #endregion

        #region 绘制关闭按钮 + PaintCloseButton(Graphics graphics, int tabIndex)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics">绘图对象</param>
        /// <param name="tabIndex">标签Index</param>
        private void PaintCloseButton(Graphics graphics, int tabIndex)
        {
            if (ShowCloseButton != CloseState.None && !HideCloseButtonIndex.Any(k => k == tabIndex))
            {
                var tabRect = this.GetTabRect(tabIndex);//2,2 当前Index的选项卡标签
                var offset = tabIndex == SelectedIndex ? 0 : 2;//非选中向下偏移值
                uint topInkBarOffset = ShowSelectedTopInkBar ? (SelectedTopInkBarWidth - 2) < 0 ? 0 : SelectedTopInkBarWidth - 2 : 0;
                //e.Graphics.DrawRectangle(Pens.BurlyWood, tabRect.Right - 11, tabRect.Y, 10, 10);//【调试】Close图标
                if (HOVERED && HOVER_INDEX == tabIndex)
                {
                    graphics.DrawImage(CloseHoverImage, tabRect.Right - CLOSE_IMAGE_WIDTH - 1, tabRect.Y + topInkBarOffset + offset, CLOSE_IMAGE_WIDTH, CLOSE_IMAGE_WIDTH);
                }
                else
                {
                    graphics.FillRectangle(new SolidBrush(SystemColors.Control), tabRect.Right - CLOSE_IMAGE_WIDTH - 1, tabRect.Y + topInkBarOffset + offset, CLOSE_IMAGE_WIDTH, CLOSE_IMAGE_WIDTH);
                    graphics.DrawImage(CloseImage, tabRect.Right - CLOSE_IMAGE_WIDTH - 1, tabRect.Y + topInkBarOffset + offset, CLOSE_IMAGE_WIDTH, CLOSE_IMAGE_WIDTH);
                }
            }
        }
        #endregion
    }
}
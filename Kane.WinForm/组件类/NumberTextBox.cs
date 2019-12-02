#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.WinForm
* 项目描述 ：通用扩展工具
* 类 名 称 ：NumberTextBox
* 类 描 述 ：数字输入文本框
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.WinForm
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/12/2 19:54:37
* 更新时间 ：2019/12/2 19:54:37
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

namespace Kane.WinForm
{
    public class NumberTextBox : WatermarkTextBox
    {
        #region 私有成员
        private string TEMP_DATA = string.Empty; 
        #endregion

        #region 可配置项
        [Browsable(true), Description("是否没有小数"), EditorBrowsable(EditorBrowsableState.Always)]
        public bool NoDecimal { get; set; } = false;
        [Browsable(true), Description("是否不能为负"), EditorBrowsable(EditorBrowsableState.Always)]
        public bool NoNegative { get; set; } = false;
        [Browsable(true), Description("小数位精度"), EditorBrowsable(EditorBrowsableState.Always)]
        public uint? Precision { get; set; } 
        #endregion

        #region 构造函数 + NumberTextBox()
        /// <summary>
        /// 已知问题，鼠标右键-->粘贴时数据未作处理
        /// </summary>
        public NumberTextBox()
        {
            //this.ShortcutsEnabled = false;//暂时对右键菜单限制
            this.KeyPress += NumberTextBox_KeyPress;
            this.KeyUp += NumberTextBox_KeyUp;
        } 
        #endregion

        #region 按钮弹起前事件拦截 + NumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        /// <summary>
        /// 按钮弹起前事件拦截
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            var textbox = sender as TextBox;// 8为退格，13为回车，45为负号 ，46为小数点
            if (e.KeyChar == '\u0001' || e.KeyChar == '\u0003') e.Handled = false; //全局过滤，忽略   '\u0001'为Ctrl+A全选，'\u0003'为Ctrl+C复制
            else if (e.KeyChar == '\u0016')//'\u0016'为粘贴快捷键 Ctrl+V
            {
                Console.WriteLine($"粘贴前备份");
                TEMP_DATA = textbox.Text;//先把原来的数据暂存起来
                e.Handled = false;
            }
            else
            {
                if (NoDecimal)//只能输入整数
                {
                    if (NoNegative)//只能为正数
                    {
                        //设置只允许输入【 0~9 】 【回车】【退格】 否则无效 
                        if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13) e.Handled = true;
                        if (e.KeyChar == 48 && textbox.SelectionStart == 0) e.Handled = true;//如果输入为0，不能输入在第一位
                    }
                    else //可以为负数
                    {
                        //设置只允许输入【-】【 0~9 】【回车】【退格】 否则无效
                        if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 45 && e.KeyChar != 13) e.Handled = true;
                        if (e.KeyChar == 48 && (textbox.SelectionStart == 0 || textbox.Text == "-")) e.Handled = true;//如果输入为0，不能输入在第一位,如果第一位为负数，第二位也不能是0
                        if (e.KeyChar == 45 && textbox.SelectionStart != 0) e.Handled = true;//如果输入是负号，只能输入在第一位
                    }
                }
                else //可以输入小数
                {
                    if (NoNegative) //只能输入正小数
                    {
                        //设置只允许输入【.】【 0~9 】【回车】【退格】 否则无效
                        if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 46) e.Handled = true;
                        //判断 46为小数点时，只能输入一次，如果选中时，判断是否选中字符串中包含小数点
                        if (e.KeyChar == 46 && textbox.Text.CharCount('.') > 0 && textbox.SelectedText.CharCount('.') < 1) e.Handled = true;
                        //限定精度时，只能输入精度位小数
                        if (Precision.HasValue && e.KeyChar != '\b' && (textbox.SelectionStart) > (textbox.Text.LastIndexOf('.')) + Precision && textbox.Text.IndexOf(".") >= 0) e.Handled = true;
                        //第一位是0，第二位必须为小数点
                        if (e.KeyChar != 46 && e.KeyChar != 8 && textbox.Text == "0") e.Handled = true;
                    }
                    else //可以为负小数
                    {
                        //设置只允许输入【.】【-】【 0~9 】 【回车】【退格】否则无效
                        if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46) e.Handled = true;
                        //判断 46为小数点时，只能输入一次，如果选中时，判断是否选中字符串中包含小数点
                        if (e.KeyChar == 46 && textbox.Text.CharCount('.') > 0 && textbox.SelectedText.CharCount('.') < 1) e.Handled = true;
                        //限定只能输入两位小数 
                        if (Precision.HasValue && e.KeyChar != '\b' && (textbox.SelectionStart) > (textbox.Text.LastIndexOf('.')) + Precision && textbox.Text.IndexOf(".") >= 0) e.Handled = true;
                        //第一位是0，第二位必须为小数点
                        if (e.KeyChar != 46 && e.KeyChar != 8 && textbox.Text == "0") e.Handled = true;
                        //如果输入是负号，只能输入在第一位
                        if (e.KeyChar == 45 && textbox.SelectionStart != 0) e.Handled = true;
                        if (e.KeyChar != 46 && e.KeyChar != 8 && textbox.Text == "-0") e.Handled = true;
                    }
                }
            }
        }
        #endregion

        #region Ctrl+V粘贴过来事件处理 + NumberTextBox_KeyUp(object sender, KeyEventArgs e)
        /// <summary>
        /// Ctrl+V粘贴过来事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            //Console.WriteLine($"KeyUpValue[{e.KeyValue}],KeyCode[{e.KeyCode}],KeyData[{e.KeyData}]");
            if (e.KeyData == (Keys.V | Keys.Control))//如果是Ctrl+V粘贴过来
            {
                //如果是整数
                if (NoDecimal && int.TryParse(this.Text, out int intResult)) this.Text = NoNegative ? intResult.ToString().TrimStart('-') : intResult.ToString();
                else if (NoDecimal && decimal.TryParse(this.Text, out decimal result)) //如果是整数，但能转为小位，则取整数部分
                {
                    this.Text = NoNegative ? decimal.ToInt32(result).ToString().TrimStart('-') : decimal.ToInt32(result).ToString();
                }
                else if (!NoDecimal && decimal.TryParse(this.Text, out result))//如果是小数，则尝试转换为Decimal
                {
                    if (Precision.HasValue) this.Text = NoNegative ? Math.Round(result, (int)Precision).ToString().TrimStart('-') : Math.Round(result, (int)Precision).ToString();
                    else this.Text = NoNegative ? result.ToString().TrimStart('-') : result.ToString();
                }
                else this.Text = TEMP_DATA;
                this.Select(this.Text.Length, 0);//光标移到最后
            }
        } 
        #endregion
    }
}
# WinForm常用汇总
*
> 在Textbox 的KeyPress事件添加下列方法，起到拦截作用

### 只能输入整数
`
//只能输入整数
private void TB_Duration_KeyPress(object sender, KeyPressEventArgs e)
{
    char result = e.KeyChar;
    //if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar)) //8 即为 '\b' 回退
    if (char.IsDigit(result) || result == 8) e.Handled = false;
    else e.Handled = true;
}
`

### 两位小数的正数字
`
//两位小数的正数字
private void TwoDigitsPositive(object sender, KeyPressEventArgs e)
{
    var textbox = sender as TextBox;
    //设置只允许输入【.】【 0~9 】 否则无效 13为回车，45为负号 ，46为 小数点
    if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 46)
        e.Handled = true;
    //判断 46为小数点时，只能输入一次，如果选中时，判断是否选中字符串中包含小数点
    if (e.KeyChar == 46 && textbox.Text.CharCount('.')>0 && textbox.SelectedText.CharCount('.')<1)
        e.Handled = true;
    //限定只能输入两位小数 
    if (e.KeyChar != '\b' && (textbox.SelectionStart) > (textbox.Text.LastIndexOf('.')) + 2 && textbox.Text.IndexOf(".") >= 0)
        e.Handled = true;
    //第一位是0，第二位必须为小数点
    if (e.KeyChar != 46 && e.KeyChar != 8 && ((TextBox)sender).Text == "0")
        e.Handled = true;
}
`

### 两位小数的数字
`
//两位小数的数字
private void TwoDigits(object sender, KeyPressEventArgs e)
{
    var textbox = sender as TextBox;
    //设置只允许输入【.】【-】【 0~9 】 否则无效 13为回车，45为负号 ，46为 小数点
    if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar !=46)
        e.Handled = true;
    //判断 46为小数点时，只能输入一次，如果选中时，判断是否选中字符串中包含小数点
    if (e.KeyChar == 46 && textbox.Text.CharCount('.') > 0 && textbox.SelectedText.CharCount('.') < 1)
        e.Handled = true;
    //限定只能输入两位小数 
    if (e.KeyChar != '\b' && (textbox.SelectionStart) > (textbox.Text.LastIndexOf('.')) + 2 && textbox.Text.IndexOf(".") >= 0)
        e.Handled = true;
    //第一位是0，第二位必须为小数点
    if (e.KeyChar != 46 && e.KeyChar != 8 && ((TextBox)sender).Text == "0")
        e.Handled = true;
    //如果输入是负号，只能输入在第一位
    if (e.KeyChar == 45 && textbox.SelectionStart != 0)
        e.Handled = true;
}
`

#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：ConvertHelper
* 类 描 述 ：转换类扩展
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/16 23:25:16
* 更新时间 ：2019/10/16 23:25:16
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Kane.Extension
{
    public static class ConvertHelper
    {
        #region 泛型转换为Int,失败时返回默认值0 + ToInt<T>(this T value, int returnValue = 0)
        /// <summary>
        /// 泛型转换为Int,失败时返回默认值0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要转换的对象</param>
        /// <param name="returnValue">可设置失败后的返回值，默认为0</param>
        /// <returns></returns>
        public static int ToInt<T>(this T value, int returnValue = 0)
        {
            if (value.IsNull() || value.ToString().IsNullOrEmpty()) return returnValue;
            int.TryParse(value.ToString(), out returnValue);
            return returnValue;
        }
        #endregion

        #region 字符串转Decimal，失败时返回默认值0 + ToDec<T>(this T value, decimal returnValue = 0)
        /// <summary>
        /// 字符串转Decimal，失败时返回默认值0
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <param name="returnValue">失败时返回默认值0</param>
        /// <returns></returns>
        public static decimal ToDec<T>(this T value, decimal returnValue = 0)
        {
            if (value.IsNull() || value.ToString().IsNullOrEmpty()) return returnValue;
            decimal.TryParse(value.ToString(), out returnValue);
            return returnValue;
        }
        #endregion

        #region 转换成大写人民币
        /// <summary>
        /// 转换成大写人民币
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static String ConvertToChinese(Decimal value)
        {
            var s = value.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            var d = Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            var r = Regex.Replace(d, ".", m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰"[m.Value[0] - '-'].ToString());
            return r;
        }
        #endregion

        #region 泛型转Decimal,默认保留两位小数(采用4舍6入5取偶) + ToRoundDec<T>(this T value, int digits = 2, int returnValue = 0)
        /// <summary>
        /// 泛型转Decimal,默认保留两位小数(采用4舍6入5取偶)
        /// </summary>
        /// <param name="value">要转的值</param>
        /// <param name="digits">保留的小数位数</param>
        /// <param name="returnValue">失败时返回的值</param>
        /// <returns></returns>
        public static decimal ToRoundDec<T>(this T value, int digits = 2, int returnValue = 0) => Math.Round(value.ToDec(returnValue), digits);
        #endregion
    }
}
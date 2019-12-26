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
* 更新时间 ：2019/12/18 18:00:16
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Text.RegularExpressions;

namespace Kane.Extension
{
    /// <summary>
    /// 转换类扩展
    /// </summary>
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

        #region 泛型转换为Int?(注意是可空类型) + ToNInt<T>(this T value)
        /// <summary>
        /// 泛型转换为Int?(注意是可空类型)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要转换的对象</param>
        /// <returns></returns>
        public static int? ToNInt<T>(this T value)
        {
            if (value.IsNull() || value.ToString().IsNullOrEmpty()) return null;
            if (int.TryParse(value.ToString(), out int returnValue)) return returnValue;
            else return null;
        }
        #endregion

        #region 泛型转换为Float,失败时返回默认值0 + ToFloat<T>(this T value, float returnValue = 0)
        /// <summary>
        /// 泛型转换为Float,失败时返回默认值0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要转换的对象</param>
        /// <param name="returnValue">可设置失败后的返回值，默认为0</param>
        /// <returns></returns>
        public static float ToFloat<T>(this T value, float returnValue = 0)
        {
            if (value.IsNull() || value.ToString().IsNullOrEmpty()) return returnValue;
            float.TryParse(value.ToString(), out returnValue);
            return returnValue;
        }
        #endregion

        #region 泛型转换为Float?(注意是可空类型) + ToNFloat<T>(this T value)
        /// <summary>
        /// 泛型转换为Float?(注意是可空类型)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要转换的对象</param>
        /// <returns></returns>
        public static float? ToNFloat<T>(this T value)
        {
            if (value.IsNull() || value.ToString().IsNullOrEmpty()) return null;
            if (float.TryParse(value.ToString(), out float returnValue)) return returnValue;
            else return null;
        }
        #endregion

        #region 泛型转换为Double,失败时返回默认值0 + ToDouble<T>(this T value, double returnValue = 0)
        /// <summary>
        /// 泛型转换为Double,失败时返回默认值0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要转换的对象</param>
        /// <param name="returnValue">可设置失败后的返回值，默认为0</param>
        /// <returns></returns>
        public static double ToDouble<T>(this T value, double returnValue = 0)
        {
            if (value.IsNull() || value.ToString().IsNullOrEmpty()) return returnValue;
            double.TryParse(value.ToString(), out returnValue);
            return returnValue;
        }
        #endregion

        #region 泛型转换为Double?(注意是可空类型) + ToNDouble<T>(this T value)
        /// <summary>
        /// 泛型转换为Double?(注意是可空类型)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要转换的对象</param>
        /// <returns></returns>
        public static double? ToNDouble<T>(this T value)
        {
            if (value.IsNull() || value.ToString().IsNullOrEmpty()) return null;
            if (double.TryParse(value.ToString(), out double returnValue)) return returnValue;
            else return null;
        }
        #endregion

        #region 泛型转换为Decimal,失败时返回默认值0 + ToDec<T>(this T value, decimal returnValue = 0)
        /// <summary>
        /// 泛型转换为Decimal,失败时返回默认值0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要转换的对象</param>
        /// <param name="returnValue">可设置失败后的返回值，默认为0</param>
        /// <returns></returns>
        public static decimal ToDec<T>(this T value, decimal returnValue = 0)
        {
            if (value.IsNull() || value.ToString().IsNullOrEmpty()) return returnValue;
            decimal.TryParse(value.ToString(), out returnValue);
            return returnValue;
        } 
        #endregion

        #region 泛型转换为Decimal?(注意是可空类型) + ToNDec<T>(this T value)
        /// <summary>
        /// 泛型转换为Decimal?(注意是可空类型)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要转换的对象</param>
        /// <returns></returns>
        public static decimal? ToNDec<T>(this T value)
        {
            if (value.IsNull() || value.ToString().IsNullOrEmpty()) return null;
            if (decimal.TryParse(value.ToString(), out decimal returnValue)) return returnValue;
            else return null;
        }
        #endregion

        #region 转换成大写人民币 + ConvertToChinese(decimal value)
        /// <summary>
        /// 转换成大写人民币
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static string ConvertToChinese(decimal value)
        {
            var valueString = value.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            var temp = Regex.Replace(valueString, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            var result = Regex.Replace(temp, ".", m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰"[m.Value[0] - '-'].ToString());
            return result;
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

        #region 常规字符串转换DateTime，可设置失败后的返回值 + ToDT(this string value, DateTime returnValue)
        /// <summary>
        /// 常规字符串转换DateTime，可设置失败后的返回值
        /// 对象中的格式设置信息分析字符串 s，该对象由当前线程区域性隐式提供。
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <param name="returnValue">失败后的返回值</param>
        /// <returns></returns>
        public static DateTime ToDT(this string value, DateTime returnValue)
        {
            if (value.IsNullOrEmpty()) return returnValue;
            DateTime.TryParse(value, out returnValue);
            return returnValue;
        }
        #endregion

        #region 常规字符串转换DateTime，失败后的返回值默认为DateTime.Now + ToDT(this string value)
        /// <summary>
        /// 常规字符串转换DateTime，失败后的返回值默认为DateTime.Now
        /// 对象中的格式设置信息分析字符串 s，该对象由当前线程区域性隐式提供。
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <returns></returns>
        public static DateTime ToDT(this string value) => ToDT(value, DateTime.Now);
        #endregion

        #region 字符串转换DateTime，可根据自定义格式转换，可设置失败后的返回值 + ToDT(this string value, string format, DateTime returnValue)
        /// <summary>
        /// 字符串转换DateTime，可根据自定义格式转换，可设置失败后的返回值
        /// d 月中的某一天。一位数的日期没有前导零。 
        /// dd 月中的某一天。一位数的日期有一个前导零。 
        /// ddd 周中某天的缩写名称，在 AbbreviatedDayNames 中定义。 
        /// dddd 周中某天的完整名称，在 DayNames 中定义。 
        /// M 月份数字。一位数的月份没有前导零。 
        /// MM 月份数字。一位数的月份有一个前导零。 
        /// MMM 月份的缩写名称，在 AbbreviatedMonthNames 中定义。 
        /// MMMM 月份的完整名称，在 MonthNames 中定义。 
        /// y 不包含纪元的年份。如果不包含纪元的年份小于 10，则显示不具有前导零的年份。 
        /// yy 不包含纪元的年份。如果不包含纪元的年份小于 10，则显示具有前导零的年份。 
        /// yyyy 包括纪元的四位数的年份。 
        /// gg 时期或纪元。如果要设置格式的日期不具有关联的时期或纪元字符串，则忽略该模式。 
        /// h 12 小时制的小时。一位数的小时数没有前导零。 
        /// hh 12 小时制的小时。一位数的小时数有前导零。 
        /// H 24 小时制的小时。一位数的小时数没有前导零。 
        /// HH 24 小时制的小时。一位数的小时数有前导零。 
        /// m 分钟。一位数的分钟数没有前导零。 
        /// mm 分钟。一位数的分钟数有一个前导零。 
        /// s 秒。一位数的秒数没有前导零。 
        /// ss 秒。一位数的秒数有一个前导零。 
        /// f 秒的小数精度为一位。其余数字被截断。 
        /// ff 秒的小数精度为两位。其余数字被截断。 
        /// fff 秒的小数精度为三位。其余数字被截断。 
        /// ffff 秒的小数精度为四位。其余数字被截断。 
        /// fffff 秒的小数精度为五位。其余数字被截断。 
        /// ffffff 秒的小数精度为六位。其余数字被截断。 
        /// fffffff 秒的小数精度为七位。其余数字被截断。 
        /// t 在 AMDesignator 或 PMDesignator 中定义的 AM/PM 指示项的第一个字符（如果存在）。 
        /// tt 在 AMDesignator 或 PMDesignator 中定义的 AM/PM 指示项（如果存在）。 
        /// z 时区偏移量（“+”或“-”后面仅跟小时）。一位数的小时数没有前导零。例如，太平洋标准时间是“-8”。 
        /// zz 时区偏移量（“+”或“-”后面仅跟小时）。一位数的小时数有前导零。例如，太平洋标准时间是“-08”。 
        /// zzz 完整时区偏移量（“+”或“-”后面跟有小时和分钟）。一位数的小时数和分钟数有前导零。例如，太平洋标准时间是“-08:00”。 
        /// : 在 TimeSeparator 中定义的默认时间分隔符。 
        /// / 在 DateSeparator 中定义的默认日期分隔符。
        /// https://docs.microsoft.com/zh-cn/dotnet/api/system.globalization.datetimeformatinfo?view=netcore-3.1
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <param name="format">自定义转换格式</param>
        /// <param name="returnValue">失败后的返回值</param>
        /// <returns></returns>
        public static DateTime ToDT(this string value, string format, DateTime returnValue)
        {
            if (value.IsNullOrEmpty()) return returnValue;
            DateTime.TryParseExact(value, format, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out returnValue);
            return returnValue;
        }
        #endregion

        #region 字符串转换DateTime，可根据自定义格式转换，失败后的返回值默认为DateTime.Now + ToDT(this string value, string format)
        /// <summary>
        /// 字符串转换DateTime，可根据自定义格式转换，失败后的返回值默认为DateTime.Now
        /// d 月中的某一天。一位数的日期没有前导零。 
        /// dd 月中的某一天。一位数的日期有一个前导零。 
        /// ddd 周中某天的缩写名称，在 AbbreviatedDayNames 中定义。 
        /// dddd 周中某天的完整名称，在 DayNames 中定义。 
        /// M 月份数字。一位数的月份没有前导零。 
        /// MM 月份数字。一位数的月份有一个前导零。 
        /// MMM 月份的缩写名称，在 AbbreviatedMonthNames 中定义。 
        /// MMMM 月份的完整名称，在 MonthNames 中定义。 
        /// y 不包含纪元的年份。如果不包含纪元的年份小于 10，则显示不具有前导零的年份。 
        /// yy 不包含纪元的年份。如果不包含纪元的年份小于 10，则显示具有前导零的年份。 
        /// yyyy 包括纪元的四位数的年份。 
        /// gg 时期或纪元。如果要设置格式的日期不具有关联的时期或纪元字符串，则忽略该模式。 
        /// h 12 小时制的小时。一位数的小时数没有前导零。 
        /// hh 12 小时制的小时。一位数的小时数有前导零。 
        /// H 24 小时制的小时。一位数的小时数没有前导零。 
        /// HH 24 小时制的小时。一位数的小时数有前导零。 
        /// m 分钟。一位数的分钟数没有前导零。 
        /// mm 分钟。一位数的分钟数有一个前导零。 
        /// s 秒。一位数的秒数没有前导零。 
        /// ss 秒。一位数的秒数有一个前导零。 
        /// f 秒的小数精度为一位。其余数字被截断。 
        /// ff 秒的小数精度为两位。其余数字被截断。 
        /// fff 秒的小数精度为三位。其余数字被截断。 
        /// ffff 秒的小数精度为四位。其余数字被截断。 
        /// fffff 秒的小数精度为五位。其余数字被截断。 
        /// ffffff 秒的小数精度为六位。其余数字被截断。 
        /// fffffff 秒的小数精度为七位。其余数字被截断。 
        /// t 在 AMDesignator 或 PMDesignator 中定义的 AM/PM 指示项的第一个字符（如果存在）。 
        /// tt 在 AMDesignator 或 PMDesignator 中定义的 AM/PM 指示项（如果存在）。 
        /// z 时区偏移量（“+”或“-”后面仅跟小时）。一位数的小时数没有前导零。例如，太平洋标准时间是“-8”。 
        /// zz 时区偏移量（“+”或“-”后面仅跟小时）。一位数的小时数有前导零。例如，太平洋标准时间是“-08”。 
        /// zzz 完整时区偏移量（“+”或“-”后面跟有小时和分钟）。一位数的小时数和分钟数有前导零。例如，太平洋标准时间是“-08:00”。 
        /// : 在 TimeSeparator 中定义的默认时间分隔符。 
        /// / 在 DateSeparator 中定义的默认日期分隔符。
        /// https://docs.microsoft.com/zh-cn/dotnet/api/system.globalization.datetimeformatinfo?view=netcore-3.1
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <param name="format">自定义转换格式</param>
        /// <returns></returns>
        public static DateTime ToDT(this string value, string format) => ToDT(value, format, DateTime.Now);
        #endregion
    }
}
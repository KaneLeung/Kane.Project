#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：ConvertEx
* 类 描 述 ：转换类扩展类
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/16 23:25:16
* 更新时间 ：2020/05/16 13:00:16
* 版 本 号 ：v1.0.5.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Kane.Extension
{
    /// <summary>
    /// 转换类扩展类
    /// </summary>
    public static class ConvertEx
    {
        #region 泛型转换为Int,失败时返回默认值0 + ToInt<T>(this T value, int returnValue = 0)
        /// <summary>
        /// 泛型转换为Int,失败时返回默认值0
        /// <para>可转【100.001】【-100.001】【  -100.001  】</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要转换的对象</param>
        /// <param name="returnValue">可设置失败后的返回值，默认为0</param>
        /// <returns></returns>
        public static int ToInt<T>(this T value, int returnValue = 0)
        {
            if (value.IsNull() || value.ToString().IsNullOrEmpty()) return returnValue;
            var temp = value.ToString();
            if (temp.IndexOf('.') >= 0) temp = temp.Split('.')[0];
            int.TryParse(temp, out returnValue);
            return returnValue;
        }
        #endregion

        #region 泛型转换为Int?(注意是可空类型) + ToNInt<T>(this T value)
        /// <summary>
        /// 泛型转换为Int?(注意是可空类型)
        /// <para>可转【100.001】【-100.001】【  -100.001  】</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要转换的对象</param>
        /// <returns></returns>
        public static int? ToNInt<T>(this T value)
        {
            if (value.IsNull() || value.ToString().IsNullOrEmpty()) return null;
            var temp = value.ToString();
            if (temp.IndexOf('.') >= 0) temp = temp.Split('.')[0];
            if (int.TryParse(temp, out int returnValue)) return returnValue;
            else return null;
        }
        #endregion

        #region 泛型转换为Long,失败时返回默认值0 + ToLong<T>(this T value, long returnValue = 0)
        /// <summary>
        /// 泛型转换为Long,失败时返回默认值0
        /// <para>可转【100.001】【-100.001】【  -100.001  】</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要转换的对象</param>
        /// <param name="returnValue">可设置失败后的返回值，默认为0</param>
        /// <returns></returns>
        public static long ToLong<T>(this T value, long returnValue = 0)
        {
            if (value.IsNull() || value.ToString().IsNullOrEmpty()) return returnValue;
            var temp = value.ToString();
            if (temp.IndexOf('.') >= 0) temp = temp.Split('.')[0];
            long.TryParse(temp, out returnValue);
            return returnValue;
        }
        #endregion

        #region 泛型转换为Long?(注意是可空类型) + ToNLong<T>(this T value)
        /// <summary>
        /// 泛型转换为Long?(注意是可空类型)
        /// <para>可转【100.001】【-100.001】【  -100.001  】</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要转换的对象</param>
        /// <returns></returns>
        public static long? ToNLong<T>(this T value)
        {
            if (value.IsNull() || value.ToString().IsNullOrEmpty()) return null;
            var temp = value.ToString();
            if (temp.IndexOf('.') >= 0) temp = temp.Split('.')[0];
            if (long.TryParse(temp, out long returnValue)) return returnValue;
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

        #region 转换大写金额 + ToAmoutInWords(float value)
        /// <summary>
        /// 转换大写金额
        /// <para>【资料来源】https://baike.baidu.com/item/大写金额</para>
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static string ToAmoutInWords(this float value)
        {
            var valueString = value.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            var temp = Regex.Replace(valueString, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            var result = Regex.Replace(temp, ".", m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰"[m.Value[0] - '-'].ToString());
            return result;
        }
        #endregion

        #region 转换大写金额 + ToAmoutInWords(double value)
        /// <summary>
        /// 转换大写金额
        /// <para>【资料来源】https://baike.baidu.com/item/大写金额</para>
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static string ToAmoutInWords(this double value)
        {
            var valueString = value.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            var temp = Regex.Replace(valueString, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            var result = Regex.Replace(temp, ".", m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰"[m.Value[0] - '-'].ToString());
            return result;
        }
        #endregion

        #region 转换大写金额 + ToAmoutInWords(decimal value)
        /// <summary>
        /// 转换大写金额
        /// <para>【资料来源】https://baike.baidu.com/item/大写金额</para>
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static string ToAmoutInWords(this decimal value)
        {
            var valueString = value.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            var temp = Regex.Replace(valueString, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            var result = Regex.Replace(temp, ".", m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰"[m.Value[0] - '-'].ToString());
            return result;
        }
        #endregion

        #region 转换大写金额 + ToAmoutInWords(this string value)
        /// <summary>
        /// 字符串转换大写金额，失败返回【string.Empty】
        /// <para>【资料来源】https://baike.baidu.com/item/大写金额</para>
        /// </summary>
        /// <param name="value">要转换的字符串</param>
        /// <returns></returns>
        public static string ToAmoutInWords(this string value)
        {
            if (decimal.TryParse(value, out decimal dec))
            {
                value = dec.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
                var temp = Regex.Replace(value, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
                var result = Regex.Replace(temp, ".", m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰"[m.Value[0] - '-'].ToString());
                return result;
            }
            else return string.Empty;
        }
        #endregion

        #region 泛型转Decimal,默认保留两位小数，默认【采用4舍6入5取偶】 + ToRoundDec<T>(this T value, int digits = 2, int returnValue = 0)
        /// <summary>
        /// 泛型转Decimal,默认保留两位小数，默认【采用4舍6入5取偶】
        /// <para>采用Banker's rounding（银行家算法），即：四舍六入五取偶。事实上这也是IEEE的规范。</para>
        /// <para>备注：<see cref="MidpointRounding.AwayFromZero"/>可以用来实现传统意义上的"四舍五入"。</para>
        /// </summary>
        /// <param name="value">要转的值</param>
        /// <param name="digits">保留的小数位数</param>
        /// <param name="returnValue">失败时返回的值</param>
        /// <param name="mode">可选择模式</param>
        /// <returns></returns>
        public static decimal ToRoundDec<T>(this T value, int digits = 2, int returnValue = 0, MidpointRounding mode = MidpointRounding.ToEven)
            => Math.Round(value.ToDec(returnValue), digits, mode);
        #endregion

        #region 全局日期时间转换格式
        /// <summary>
        /// 全局日期转换格式
        /// </summary>
        public static IEnumerable<string> GlobalFormats = new string[]
        {
            "yyyy-MM-dd HH:mm:ss",
            "yyyyMMdd HH:mm:ss",
            "yyyy/MM/dd HH:mm:ss",
            "yyyy-M-d HH:mm:ss",
            "yyyyMd HH:mm:ss",
            "yyyy/M/d HH:mm:ss",
            "yy-MM-dd HH:mm:ss",
            "yyMMdd HH:mm:ss",
            "yy/MM/dd HH:mm:ss",
            "yy-M-d HH:mm:ss",
            "yyMd HH:mm:ss",
            "yy/M/d HH:mm:ss",
            "yyyyMMddHHmmss",
            "yyyy-MM-dd H:m:s",
            "yyyyMMdd H:m:s",
            "yyyy/MM/dd H:m:s",
            "yyyy-M-d H:m:s",
            "yyyyMd H:m:s",
            "yyyy/M/d H:m:s",
            "yy-MM-dd H:m:s",
            "yyMMdd H:m:s",
            "yy/MM/dd H:m:s",
            "yy-M-d H:m:s",
            "yyMd H:m:s",
            "yy/M/d H:m:s",
            "yyyy-MM-dd",
            "yyyyMMdd",
            "yyyy/MM/dd",
            "yyyy-M-d",
            "yyyyMd",
            "yyyy/M/d",
            "yy-MM-dd",
            "yyMMdd",
            "yy/MM/dd",
            "yy-M-d",
            "yyMd",
            "yy/M/d"
        };
        #endregion

        #region 将字符串转换为可空的日期对象 + ToNDT(this string value, string format = "")
        /// <summary>
        /// 将字符串转换为可空的日期对象
        /// </summary>
        /// <param name="value">日期字符串</param>
        /// <param name="format">日期格式化字符串</param>
        /// <returns>日期对象</returns>
        public static DateTime? ToNDT(this string value, string format = "")
        {
            DateTime? result = null;
            if (value.IsValuable())
            {
                if (format.IsValuable() && DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime temp)) result = temp;
                if (result == null && DateTime.TryParse(value, out temp)) result = temp;
                if (result == null)
                {
                    foreach (string item in GlobalFormats)
                    {
                        if (DateTime.TryParseExact(value, item, CultureInfo.InvariantCulture, DateTimeStyles.None, out temp))
                        {
                            result = temp;
                            break;
                        }
                    }
                }
            }
            return result;
        }
        #endregion

        #region 常规字符串转换DateTime，可设置失败后的返回值 + ToDT(this string value, DateTime returnValue)
        /// <summary>
        /// 常规字符串转换DateTime，可设置失败后的返回值
        /// <para>对象中的格式设置信息分析字符串value，该对象由当前线程区域性隐式提供。</para>
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <param name="returnValue">失败后的返回值</param>
        /// <returns></returns>
        public static DateTime ToDT(this string value, DateTime returnValue)
        {
            if (value.IsNullOrEmpty()) return returnValue;
            return value.ToNDT() ?? returnValue;
        }
        #endregion

        #region 常规字符串转换DateTime，失败后的返回默认值 + ToDT(this string value)
        /// <summary>
        /// 常规字符串转换DateTime，失败后的返回默认值
        /// <para>对象中的格式设置信息分析字符串value，该对象由当前线程区域性隐式提供。</para>
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <returns></returns>
        public static DateTime ToDT(this string value) => ToDT(value, default(DateTime));
        #endregion

        #region 字符串转换DateTime，可根据自定义格式转换，可设置失败后的返回值 + ToDT(this string value, string format, DateTime returnValue)
        /// <summary>
        /// 字符串转换DateTime，可根据自定义格式转换，可设置失败后的返回值
        /// <para>例如【2019-02-05 18:20:30】</para>
        /// g 常规日期/短时间; 标准格式字符串 =>【2019/2/5 18:20】
        /// gg 时期或纪元。如果要设置格式的日期不具有关联的时期或纪元字符串，则忽略该模式。=>【公元】
        /// y 不包含纪元的年份。如果不包含纪元的年份小于 10，则显示不具有前导零的年份。=>【2019年2月】
        /// yy 不包含纪元的年份。如果不包含纪元的年份小于 10，则显示具有前导零的年份。=>【19】
        /// yyyy 包括纪元的四位数的年份。=>【2019】
        /// M 月份数字。一位数的月份没有前导零。=>【2】
        /// MM 月份数字。一位数的月份有一个前导零。=>【02】
        /// MMM 月份的缩写名称，在 AbbreviatedMonthNames 中定义。=>【2月】或【Feb】
        /// MMMM 月份的完整名称，在 MonthNames 中定义。=>【2月】或【February】
        /// d 月中的某一天。一位数的日期没有前导零。=>【5】
        /// dd 月中的某一天。一位数的日期有一个前导零。=>【05】
        /// ddd 周中某天的缩写名称，在 AbbreviatedDayNames 中定义。=>【周二】
        /// dddd 周中某天的完整名称，在 DayNames 中定义。=>【星期二】
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
        /// t 在 AMDesignator 或 PMDesignator 中定义的 AM/PM 指示项的第一个字符（如果存在）。=>【18:20】
        /// tt 在 AMDesignator 或 PMDesignator 中定义的 AM/PM 指示项（如果存在）。=>【下午】
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
            return value.ToNDT(format) ?? returnValue;
        }
        #endregion

        #region 字符串转换DateTime，可根据自定义格式转换，失败后的返回默认值 + ToDT(this string value, string format)
        /// <summary>
        /// 字符串转换DateTime，可根据自定义格式转换，失败后的返回默认值
        /// <para>例如【2019-02-05 18:20:30】</para>
        /// g 常规日期/短时间; 标准格式字符串 =>【2019/2/5 18:20】
        /// gg 时期或纪元。如果要设置格式的日期不具有关联的时期或纪元字符串，则忽略该模式。=>【公元】
        /// y 不包含纪元的年份。如果不包含纪元的年份小于 10，则显示不具有前导零的年份。=>【2019年2月】
        /// yy 不包含纪元的年份。如果不包含纪元的年份小于 10，则显示具有前导零的年份。=>【19】
        /// yyyy 包括纪元的四位数的年份。=>【2019】
        /// M 月份数字。一位数的月份没有前导零。=>【2】
        /// MM 月份数字。一位数的月份有一个前导零。=>【02】
        /// MMM 月份的缩写名称，在 AbbreviatedMonthNames 中定义。=>【2月】或【Feb】
        /// MMMM 月份的完整名称，在 MonthNames 中定义。=>【2月】或【February】
        /// d 月中的某一天。一位数的日期没有前导零。=>【5】
        /// dd 月中的某一天。一位数的日期有一个前导零。=>【05】
        /// ddd 周中某天的缩写名称，在 AbbreviatedDayNames 中定义。=>【周二】
        /// dddd 周中某天的完整名称，在 DayNames 中定义。=>【星期二】
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
        /// t 在 AMDesignator 或 PMDesignator 中定义的 AM/PM 指示项的第一个字符（如果存在）。=>【18:20】
        /// tt 在 AMDesignator 或 PMDesignator 中定义的 AM/PM 指示项（如果存在）。=>【下午】
        /// : 在 TimeSeparator 中定义的默认时间分隔符。 
        /// / 在 DateSeparator 中定义的默认日期分隔符。
        /// https://docs.microsoft.com/zh-cn/dotnet/api/system.globalization.datetimeformatinfo?view=netcore-3.1
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <param name="format">自定义转换格式</param>
        /// <returns></returns>
        public static DateTime ToDT(this string value, string format) => ToDT(value, format, default);
        #endregion

        #region 将电话号码转换为国际标准【International Standard】的电话号码，可自定义国家代码 + ToISPhoneNo(this string value, string code)
        /// <summary>
        /// 将电话号码转换为国际标准【International Standard】的电话号码，可自定义国家代码
        /// <para>【资料来源】https://zh.wikipedia.org/wiki/国际电话区号列表</para>
        /// </summary>
        /// <param name="value">要转的电话号码</param>
        /// <param name="code">国家代码</param>
        /// <returns></returns>
        public static string ToISPhoneNo(this string value, string code) => code.StartsWith("+") ? code.Add(value) : "+".Add(code, value);
        #endregion

        #region 将国内号码转换为国际标准【International Standard】的电话号码 + ToISPhoneNo(this string value)
        /// <summary>
        /// 将国内号码转换为国际标准【International Standard】的电话号码
        /// <para>【资料来源】https://zh.wikipedia.org/wiki/国际电话区号列表</para>
        /// </summary>
        /// <param name="value">要转的电话号码</param>
        /// <returns></returns>
        public static string ToISPhoneNo(this string value) => value.StartsWith("+86") ? value : "+86".Add(value);
        #endregion

        #region 将Stream转成byte[] + ToBytes(this Stream stream)
        /// <summary>
        /// 将Stream转成byte[]
        /// </summary>
        /// <param name="stream">要转的Stream</param>
        /// <returns></returns>
        public static byte[] ToBytes(this Stream stream)
        {
            byte[] result = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);//设置当前流的位置为流的开始
            stream.Read(result, 0, result.Length);
            return result;
        }
        #endregion

        #region 将Stream转成String，默认使用UTF8编码 + StreamToString(this Stream stream)
        /// <summary>
        /// 将Stream转成String，默认使用UTF8编码
        /// </summary>
        /// <param name="stream">要转的Stream</param>
        /// <returns></returns>
        public static string StreamToString(this Stream stream) => stream.StreamToString(Encoding.UTF8);
        #endregion

        #region 将Stream转成String，可自定义编码 + StreamToString(this Stream stream, Encoding encoding)
        /// <summary>
        /// 将Stream转成String，可自定义编码
        /// </summary>
        /// <param name="stream">要转的Stream</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string StreamToString(this Stream stream, Encoding encoding) => stream.ToBytes().BytesToString(encoding);
        #endregion
    }
}
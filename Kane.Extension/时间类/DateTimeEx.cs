#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：DateTimeEx
* 类 描 述 ：时间类扩展类
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/16 23:17:28
* 更新时间 ：2020/05/05 13:17:28
* 版 本 号 ：v1.0.6.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;

namespace Kane.Extension
{
    /// <summary>
    /// 时间类扩展类
    /// </summary>
    public static class DateTimeEx
    {
        #region 将DateTime转成当天起始时间 + DayStart(this DateTime value)
        /// <summary>
        /// 将DateTime转成当天起始时间
        /// </summary>
        /// <param name="value">要转的日期</param>
        /// <returns></returns>
        public static DateTime DayStart(this DateTime value) => new DateTime(value.Year, value.Month, value.Day);
        #endregion

        #region 将DateTime转成下一天的开始时间 + NextDayStart(this DateTime value)
        /// <summary>
        /// 将DateTime转成下一天的开始时间
        /// </summary>
        /// <param name="value">要转的日期</param>
        /// <returns></returns>
        public static DateTime NextDayStart(this DateTime value) => value.DayStart().AddDays(1);
        #endregion

        #region 将DateTime转成上一天的开始时间 + LastDayStart(this DateTime value)
        /// <summary>
        /// 将DateTime转成上一天的开始时间
        /// </summary>
        /// <param name="value">要转的日期</param>
        /// <returns></returns>
        public static DateTime LastDayStart(this DateTime value) => value.DayStart().AddDays(-1);
        #endregion

        #region 将DateTime转成当月初时间 + MonthStart(this DateTime value)
        /// <summary>
        /// 将DateTime转成当月初时间
        /// </summary>
        /// <param name="value">要转的时间点</param>
        /// <returns></returns>
        public static DateTime MonthStart(this DateTime value) => new DateTime(value.Year, value.Month, 1);
        #endregion

        #region 将DateTime转成下个月初的开始时间 + NextMonthStart(this DateTime value)
        /// <summary>
        /// 将DateTime转成下个月初的开始时间
        /// </summary>
        /// <param name="value">要转的时间点</param>
        /// <returns></returns>
        public static DateTime NextMonthStart(this DateTime value) => value.MonthStart().AddMonths(1);
        #endregion

        #region 将DateTime转成上个月初的开始时间 + LastMonthStart(this DateTime value)
        /// <summary>
        /// 将DateTime转成上个月初的开始时间
        /// </summary>
        /// <param name="value">要转的时间点</param>
        /// <returns></returns>
        public static DateTime LastMonthStart(this DateTime value) => value.MonthStart().AddMonths(-1);
        #endregion

        #region 获取今天时间段，通常常用 Start ≥ X ＜ End + GetToday()
        /// <summary>
        /// 获取今天时间段，通常常用 Start ≥ X ＜ End
        /// </summary>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) GetToday()
        {
            var start = DateTime.Today;
            return (start, start.AddDays(1));
        }
        #endregion

        #region 获取昨天时间段，通常用法 Start ≥ X ＜ End + GetYesterday()
        /// <summary>
        /// 获取昨天时间段，通常用法 Start ≥ X ＜ End
        /// </summary>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) GetYesterday()
        {
            var end = DateTime.Today;
            return (end.AddDays(-1), end);
        }
        #endregion

        #region 获取明天时间段，通常用法 Start ≥ X ＜ End + GetTomorrow()
        /// <summary>
        /// 获取明天时间段，通常用法 Start ≥ X ＜ End
        /// </summary>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) GetTomorrow()
        {
            var today = DateTime.Today;
            return (today.AddDays(1), today.AddDays(2));
        }
        #endregion

        #region 获取本周时间段，通常用法 Start ≥ X < End + GetThisWeek()
        /// <summary>
        /// 获取本周时间段，通常用法 Start ≥ X ＜ End
        /// <para>中国人习惯星期一为星期开始，因为星期日为0，所以要减七</para>
        /// </summary>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) GetThisWeek()
        {
            var dayOfWeek = (int)DateTime.Now.DayOfWeek;
            var start = DateTime.Today.AddDays(1 - (dayOfWeek == 0 ? 7 : dayOfWeek));
            return (start, start.AddDays(7));
        }
        #endregion

        #region 获取某一周时间段，通常用法 Start ≥ X ＜ End + GetOneWeek(this DateTime dateTime)
        /// <summary>
        /// 获取本周时间段，通常用法 Start ≥ X ＜ End
        /// <para>中国人习惯星期一为星期开始，因为星期日为0，所以要减七</para>
        /// </summary>
        /// <param name="dateTime">要获取的那一周其中一个时间</param>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) GetOneWeek(this DateTime dateTime)
        {
            var dayOfWeek = (int)dateTime.DayOfWeek;
            var start = dateTime.AddDays(1 - (dayOfWeek == 0 ? 7 : dayOfWeek)).DayStart();
            return (start, start.AddDays(7));
        }
        #endregion

        #region 获取本月时间段，通常用法 Start ≥ X ＜ End + GetThisMonth()
        /// <summary>
        /// 获取本月时间段，通常用法 Start ≥ X ＜ End
        /// </summary>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) GetThisMonth()
        {
            var start = DateTime.Now.MonthStart();
            return (start, start.AddMonths(1));
        }
        #endregion

        #region 获取某一月时间段，通常用法 Start ≥ X ＜ End + GetOneMonth(DateTime dateTime)
        /// <summary>
        /// 获取某一月时间段，通常用法 Start ≥ X ＜ End
        /// </summary>
        /// <param name="dateTime">要获取的那一月的其中一个时间</param>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) GetOneMonth(this DateTime dateTime)
        {
            var start = dateTime.MonthStart();
            return (start, start.AddMonths(1));
        }
        #endregion

        #region 将秒转换成时长字符串，包含天、时、分、秒 + TimeString(int seconds)
        /// <summary>
        /// 将秒转换成时长字符串
        /// </summary>
        /// <param name="seconds">秒</param>
        /// <returns></returns>
        public static string TimeString(int seconds)
        {
            int min = seconds / 60;
            seconds %= 60;
            int hour = min / 60;
            min %= 60;
            int day = hour / 24;
            hour %= 24;
            return string.Format("{0}天{1:00}小时{2:00}分{3:00}秒", day, hour, min, seconds);
        }
        #endregion

        #region 将秒转换成时长字符串,没有天数，只有时、分、秒 + TimeStringNoDays(int seconds)
        /// <summary>
        /// 将秒转换成时长字符串,没有天数，只有时分秒
        /// </summary>
        /// <param name="seconds">秒</param>
        /// <returns></returns>
        public static string TimeStringNoDays(int seconds)
        {
            int min = seconds / 60;
            seconds %= 60;
            int hour = min / 60;
            min %= 60;
            return string.Format("{0:00}小时{1:00}分{2:00}秒", hour, min, seconds);
        }
        #endregion

        #region 获取时间戳，可增加或减少【秒】 + TimeStamp(int seconds = 0)
        /// <summary>  
        /// 获取时间戳，可增加或减少【秒】
        /// <para>时间戳, 又叫Unix Stamp. 从1970年1月1日（UTC/GMT的午夜）开始所经过的秒数，不考虑闰秒。</para>
        /// </summary>  
        /// <param name="seconds">增加或减少【秒】</param>
        /// <returns></returns>  
        public static string TimeStamp(int seconds = 0)
        {
#if NET40 || NET45
            TimeSpan ts = DateTime.UtcNow.AddSeconds(seconds) - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
#else
            return DateTimeOffset.UtcNow.AddSeconds(seconds).ToUnixTimeSeconds().ToString();//和【DateTimeOffset.Now.ToUnixTimeSeconds()】结果一样
#endif
        }
        #endregion

        #region 时间戳转为【当地时区】的DateTime + StampToLocal(long timeStamp)
        /// <summary>
        /// 时间戳转为【当地时区】的DateTime
        /// <para>要到 2286/11/21 01:46:40 才会变成11位（10000000000）</para>
        /// <para>int范围 -2,147,483,648 到 2,147,483,647</para>
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <returns></returns>
        public static DateTime StampToLocal(long timeStamp)
        {
            timeStamp *= 10000000;//new DateTime(621355968000000000 + long.Parse(timestamp) * 10000000);//更简单的方法
            DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);//621355968000000000
            return startTime.Add(new TimeSpan(timeStamp));//以nanosecond为单位，nanosecond：十亿分之一秒   new TimeSpan(10,000,000)为一秒
        }
        #endregion

        #region 时间戳转为【当地时区】的DateTime + StampToLocal(string timeStamp)
        /// <summary>
        /// 时间戳转为【当地时区】的DateTime
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <returns></returns>
        public static DateTime StampToLocal(string timeStamp)
        {
            long stamp = long.Parse(string.Concat(timeStamp, "0000000"));
            DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);//621355968000000000
            return startTime.Add(new TimeSpan(stamp));//以nanosecond为单位，nanosecond：十亿分之一秒   new TimeSpan(10,000,000)为一秒
        }
        #endregion

        #region 时间戳转为【Utc时区】的DateTime + StampToUtc(long timeStamp)
        /// <summary>
        /// 时间戳转为【Utc时区】的DateTime
        /// <para>要到 2286/11/21 01:46:40 才会变成11位（10000000000）</para>
        /// <para>int范围 -2,147,483,648 到 2,147,483,647</para>
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <returns></returns>
        public static DateTime StampToUtc(long timeStamp)
        {
            timeStamp *= 10000000;//new DateTime(621355968000000000 + long.Parse(timestamp) * 10000000);//更简单的方法
            DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);//621355968000000000
            return startTime.Add(new TimeSpan(timeStamp));//以nanosecond为单位，nanosecond：十亿分之一秒   new TimeSpan(10,000,000)为一秒
        }
        #endregion

        #region 时间戳转为【Utc时区】的DateTime + StampToUtc(string timeStamp)
        /// <summary>
        /// 时间戳转为【Utc时区】的DateTime
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <returns></returns>
        public static DateTime StampToUtc(string timeStamp)
        {
            long stamp = long.Parse(string.Concat(timeStamp, "0000000"));
            DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);//621355968000000000
            return startTime.Add(new TimeSpan(stamp));//以nanosecond为单位，nanosecond：十亿分之一秒   new TimeSpan(10,000,000)为一秒
        }
        #endregion

        #region DateTime时间格式转换为Unix时间戳格式 + ToStamp(this DateTime value)
        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// <para>用Int最大值是2038年01月19日03时14分07秒，超过可用Long</para>
        /// </summary>
        /// <param name="value">要转换的时间</param>
        /// <returns></returns>
        public static int ToStamp(this DateTime value)
        {
            DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            return (int)(value - startTime).TotalSeconds;
        }
        #endregion

        #region 以当前时间为基准，计算时间差 + TimeDiff(this DateTime datetime)
        /// <summary>
        /// 以当前时间为基准，计算时间差
        /// </summary>
        /// <param name="datetime">要计算的时间</param>
        /// <returns></returns>
        public static string TimeDiff(this DateTime datetime) => datetime.TimeDiff(DateTime.Now);
        #endregion

        #region 计算两个时间差 + TimeDiff(this DateTime datetime, DateTime point)
        /// <summary>
        /// 计算两个时间差
        /// </summary>
        /// <param name="datetime">要计算的时间</param>
        /// <param name="point">时间基准点</param>
        /// <returns></returns>
        public static string TimeDiff(this DateTime datetime, DateTime point)
        {
            TimeSpan timeSpan = datetime - point;
            var tag = timeSpan > TimeSpan.Parse("0") ? "后" : "前";
            var days = timeSpan.Days;
            if (days != 0)
            {
                days = days < 0 ? days * -1 : days;
                if (days >= 365) return $"{days / 365}年{tag}";
                else if (days >= 30) return $"{days / 30}个月{tag}";
                else if (days >= 7) return $"{days / 7}周{tag}";
                else return $"{days}天{tag}";
            }
            if (timeSpan.Hours != 0)
                return $"{(timeSpan.Hours < 0 ? timeSpan.Hours * -1 : timeSpan.Hours)}小时{tag}";
            if (timeSpan.Minutes != 0)
                return $"{(timeSpan.Minutes < 0 ? timeSpan.Minutes * -1 : timeSpan.Minutes)}分钟{tag}";
            return $"{(timeSpan.Seconds < 0 ? timeSpan.Seconds * -1 : timeSpan.Seconds)}秒{tag}";
        }
        #endregion

        #region 计算周岁年龄 + GetAge(this DateTime dateOfBirth)
        /// <summary>
        /// 计算周岁年龄
        /// </summary>
        /// <param name="dateOfBirth">出生日期</param>
        public static int GetAge(this DateTime dateOfBirth) => GetAge(dateOfBirth, DateTime.Now.Date);
        #endregion

        #region 计算周岁年龄，指定参考日期 + GetAge(this DateTime dateOfBirth, DateTime point)
        /// <summary>
        /// 计算周岁年龄，指定参考日期
        /// </summary>
        /// <param name="dateOfBirth">出生日期</param>
        /// <param name="point">时间基准点</param>
        public static int GetAge(this DateTime dateOfBirth, DateTime point)
        {
            var age = point.Year - dateOfBirth.Year;
            if (point.Month < dateOfBirth.Month || (point.Month == dateOfBirth.Month && point.Day < dateOfBirth.Day)) --age;
            return age;
        }
        #endregion

        #region 根据当前时间获取当前是第几周 + WeekIndex(this DateTime datetime, bool crossover = false)
        /// <summary>
        /// 根据当前时间获取当前是第几周
        /// </summary>
        /// <param name="datetime">当前时间</param>
        /// <param name="crossover">开启【交叉年】：像2016年12月31号与2017年1月1号刚好在同一星期，【开启】交叉年后，则12月31号为第【1】周，否则为【53】周</param>
        /// <returns></returns>
        public static int WeekIndex(this DateTime datetime, bool crossover = false)
        {
            int dayOfYear = datetime.DayOfYear;//求出此时间在一年中的位置
            int dayOfWeek = (int)new DateTime(datetime.Year, 1, 1).DayOfWeek;//当年第一天的星期几
            dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;//当年第一天是星期几，中国人习惯星期一为星期开始，因为星期日为0，所以为7
            var (Start, End) = datetime.GetOneWeek();
            int index = (int)Math.Ceiling(((double)dayOfYear + dayOfWeek - 1) / 7);//确定当前是第几周
            if (crossover && Start.Year < End.Year) index = 1;//判断是否为交叉年
            return index;
        }
        #endregion
    }
}
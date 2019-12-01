#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：DateTimeHelper
* 类 描 述 ：时间类扩展
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/16 23:17:28
* 更新时间 ：2019/10/16 23:17:28
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace Kane.Extension
{
    public static class DateTimeHelper
    {
        #region 将DateTime转成当天起始时间 + DayStart(this DateTime value)
        /// <summary>
        /// 将DateTime转成当天起始时间
        /// </summary>
        /// <param name="value">要转的日期</param>
        /// <returns></returns>
        public static DateTime DayStart(this DateTime value) => new DateTime(value.Year, value.Month, value.Day);
        #endregion

        #region 将DateTime转成明天的开始时间 + NextDayStart(this DateTime value)
        /// <summary>
        /// 将DateTime转成明天的开始时间
        /// </summary>
        /// <param name="value">要转的日期</param>
        /// <returns></returns>
        public static DateTime NextDayStart(this DateTime value) => value.DayStart().AddDays(1);
        #endregion

        #region 将DateTime转成当月初时间 + MonthStart(this DateTime value)
        /// <summary>
        /// 将DateTime转成当月初时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime MonthStart(this DateTime value) => new DateTime(value.Year, value.Month, 1);
        #endregion

        #region 将DateTime转成下个月初的开始时间 + DateTime NextMonthStart(this DateTime value)
        /// <summary>
        /// 将DateTime转成下个月初的开始时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime NextMonthStart(this DateTime value) => value.MonthStart().AddMonths(1);
        #endregion

        #region 获取今天时间段，通常常用 Start >= x < End + GetToday()
        /// <summary>
        /// 获取今天时间段，通常常用 Start >= x < End
        /// </summary>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) GetToday()
        {
            var start = DateTime.Now.DayStart();
            return (start, start.AddDays(1));
        }
        #endregion

        #region 获取昨天时间段，通常用法 Start >= x < End + GetYesterday()
        /// <summary>
        /// 获取昨天时间段，通常用法 Start >= x < End
        /// </summary>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) GetYesterday()
        {
            var end = DateTime.Now.DayStart();
            return (end.AddDays(-1), end);
        }
        #endregion

        #region 获取本周时间段，通常用法 Start >= x < End + GetThisWeek()
        /// <summary>
        /// 获取本周时间段，通常用法 Start >= x < End
        /// 中国人习惯星期一为星期开始，因为星期日为0，所以要减七
        /// </summary>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) GetThisWeek()
        {
            var dayOfWeek = (int)DateTime.Now.DayOfWeek;
            var start = DateTime.Now.AddDays(1 - (dayOfWeek == 0 ? 7 : dayOfWeek)).DayStart();
            return (start, start.AddDays(7));
        }
        #endregion

        #region 获取某一周时间段，通常用法 Start >= x < End + GetOneWeek(DateTime dateTime)
        /// <summary>
        /// 获取本周时间段，通常用法 Start >= x < End
        /// 中国人习惯星期一为星期开始，因为星期日为0，所以要减七
        /// </summary>
        /// <param name="dateTime">要获取的那一周其中一个时间</param>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) GetOneWeek(DateTime dateTime)
        {
            var dayOfWeek = (int)dateTime.DayOfWeek;
            var start = dateTime.AddDays(1 - (dayOfWeek == 0 ? 7 : dayOfWeek)).DayStart();
            return (start, start.AddDays(7));
        }
        #endregion

        #region 获取本月时间段，通常用法 Start >= x < End + GetThisMonth()
        /// <summary>
        /// 获取本月时间段，通常用法 Start >= x < End
        /// </summary>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) GetThisMonth()
        {
            var start = DateTime.Now.MonthStart();
            return (start, start.AddMonths(1));
        }
        #endregion

        #region 获取某一月时间段，通常用法 Start >= x < End + GetOneMonth(DateTime dateTime)
        /// <summary>
        /// 获取某一月时间段，通常用法 Start >= x < End
        /// </summary>
        /// <param name="dateTime">要获取的那一月的其中一个时间</param>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) GetOneMonth(DateTime dateTime)
        {
            var start = dateTime.MonthStart();
            return (start, start.AddMonths(1));
        }
        #endregion

        #region 将秒转换成时长字符串，包含天、时、分、秒 + GetRunTime(int second)
        /// <summary>
        /// 将秒转换成运行时间
        /// </summary>
        /// <param name="second">秒</param>
        /// <returns></returns>
        public static string TimeString(int second)
        {
            int min = second / 60;
            second %= 60;
            int hour = min / 60;
            min %= 60;
            int day = hour / 24;
            hour %= 24;
            return string.Format("{0}天{1:00}小时{2:00}分{3:00}秒", day, hour, min, second);
        }
        #endregion

        #region 将秒转换成时长字符串,没有天数，只有时、分、秒 + TimeStringNoDays(int second)
        /// <summary>
        /// 将秒转换成时长字符串,没有天数，只有时分秒
        /// </summary>
        /// <param name="second">秒</param>
        /// <returns></returns>
        public static string TimeStringNoDays(int second)
        {
            int min = second / 60;
            second %= 60;
            int hour = min / 60;
            min %= 60;
            return string.Format("{0:00}小时{1:00}分{2:00}秒", hour, min, second);
        }
        #endregion

        #region 获取时间戳 + GetTimeStamp()
        /// <summary>  
        /// 获取时间戳
        /// 时间戳, 又叫Unix Stamp. 从1970年1月1日（UTC/GMT的午夜）开始所经过的秒数，不考虑闰秒。
        /// </summary>  
        /// <returns></returns>  
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        #endregion

        #region 时间戳转为DateTime + StampToDateTime(string timeStamp)
        /// <summary>
        /// 时间戳转为DateTime
        /// 要到 2286/11/21 01:46:40 才会变成11位（10000000000）
        /// int范围 -2,147,483,648 到 2,147,483,647
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime StampToDateTime(long timeStamp)
        {
            DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            timeStamp *= 10000000;
            TimeSpan toNow = new TimeSpan(timeStamp);//以nanosecond为单位，nanosecond：十亿分之一秒   new TimeSpan(10,000,000)为一秒
            return startTime.Add(toNow);
        }
        #endregion

        #region 时间戳转为DateTime + StampToDateTime(string timeStamp)
        /// <summary>
        /// 时间戳转为DateTime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime StampToDateTime(string timeStamp)
        {
            DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            long stamp = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(stamp);//以nanosecond为单位，nanosecond：十亿分之一秒   new TimeSpan(10,000,000)为一秒
            return startTime.Add(toNow);
        }
        #endregion

        #region DateTime时间格式转换为Unix时间戳格式 + ToStamp(this DateTime value)
        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// 用Int最大值是2038年01月19日03时14分07秒，超过可用Long
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int ToStamp(this DateTime value)
        {
            DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            return (int)(value - startTime).TotalSeconds;
        }
        #endregion

        #region 以当前时间为基准，计算时间差 + TimeDiff(this DateTime datetime)
        /// <summary>
        /// 以当前时间为基准，计算时间差
        /// </summary>
        /// <param name="datetime"></param>
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
    }
}

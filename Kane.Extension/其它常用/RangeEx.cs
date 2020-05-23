#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：RangeEx
* 类 描 述 ：判断值的范围扩展类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/5/5 11:00:35
* 更新时间 ：2020/5/23 11:00:35
* 版 本 号 ：v1.0.1.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion

namespace Kane.Extension
{
    /// <summary>
    /// 判断值的范围扩展类
    /// </summary>
    public static class RangeEx
    {
        #region 判断当前【Int】值是否在指定范围内 + InRange(this int value, int min, int max)
        /// <summary>
        /// 判断当前【Int】值是否在指定范围内
        /// </summary>
        /// <param name="value">要判断的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>bool</returns>
        public static bool InRange(this int value, int min, int max) => value >= min && value <= max;
        #endregion

        #region 判断当前【Int】值是否在指定范围内，否则返回默认值 + InRange(this int value, int min, int max, int returnValue)
        /// <summary>
        /// 判断当前【Int】值是否在指定范围内，否则返回默认值
        /// </summary>
        /// <param name="value">要判断的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="returnValue">默认值</param>
        /// <returns>int</returns>
        public static int InRange(this int value, int min, int max, int returnValue) => value.InRange(min, max) ? value : returnValue;
        #endregion

        #region 判断当前【Long】值是否在指定范围内 + InRange(this long value, long min, long max)
        /// <summary>
        /// 判断当前【Long】值是否在指定范围内
        /// </summary>
        /// <param name="value">要判断的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>bool</returns>
        public static bool InRange(this long value, long min, long max) => value >= min && value <= max;
        #endregion

        #region 判断当前【Long】值是否在指定范围内，否则返回默认值 + InRange(this long value, long min, long max, long returnValue)
        /// <summary>
        /// 判断当前【Long】值是否在指定范围内，否则返回默认值
        /// </summary>
        /// <param name="value">要判断的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="returnValue">默认值</param>
        /// <returns>long</returns>
        public static long InRange(this long value, long min, long max, long returnValue) => value.InRange(min, max) ? value : returnValue;
        #endregion

        #region 判断当前【Short】值是否在指定范围内 + InRange(this short value, short min, short max)
        /// <summary>
        /// 判断当前【Short】值是否在指定范围内
        /// </summary>
        /// <param name="value">要判断的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>bool</returns>
        public static bool InRange(this short value, short min, short max) => value >= min && value <= max;
        #endregion

        #region 判断当前【Short】值是否在指定范围内，否则返回默认值 + InRange(this short value, short min, short max, short returnValue)
        /// <summary>
        /// 判断当前【Short】值是否在指定范围内，否则返回默认值
        /// </summary>
        /// <param name="value">要判断的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="returnValue">默认值</param>
        /// <returns>short</returns>
        public static short InRange(this short value, short min, short max, short returnValue) => value.InRange(min, max) ? value : returnValue;
        #endregion

        #region 判断当前【float】值是否在指定范围内 + InRange(this float value, short min, short max)
        /// <summary>
        /// 判断当前【float】值是否在指定范围内
        /// </summary>
        /// <param name="value">要判断的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>bool</returns>
        public static bool InRange(this float value, short min, short max) => value >= min && value <= max;
        #endregion

        #region 判断当前【float】值是否在指定范围内，否则返回默认值 + InRange(this float value, short min, short max, float returnValue)
        /// <summary>
        /// 判断当前【float】值是否在指定范围内，否则返回默认值
        /// </summary>
        /// <param name="value">要判断的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="returnValue">默认值</param>
        /// <returns>short</returns>
        public static float InRange(this float value, short min, short max, float returnValue) => value.InRange(min, max) ? value : returnValue;
        #endregion

        #region 判断当前【Double】值是否在指定范围内 + InRange(this double value, double min, double max)
        /// <summary>
        /// 判断当前【Double】值是否在指定范围内
        /// </summary>
        /// <param name="value">要判断的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>bool</returns>
        public static bool InRange(this double value, double min, double max) => value >= min && value <= max;
        #endregion

        #region 判断当前【Double】值是否在指定范围内，否则返回默认值 + InRange(this double value, double min, double max, double returnValue)
        /// <summary>
        /// 判断当前【Double】值是否在指定范围内，否则返回默认值
        /// </summary>
        /// <param name="value">要判断的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="returnValue">默认值</param>
        /// <returns>double</returns>
        public static double InRange(this double value, double min, double max, double returnValue) => value.InRange(min, max) ? value : returnValue;
        #endregion

        #region 判断当前【Decimal】值是否在指定范围内 + InRange(this decimal value, decimal min, decimal max)
        /// <summary>
        /// 判断当前【Decimal】值是否在指定范围内
        /// </summary>
        /// <param name="value">要判断的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>bool</returns>
        public static bool InRange(this decimal value, decimal min, decimal max) => value >= min && value <= max;
        #endregion

        #region 判断当前【Decimal】值是否在指定范围内，否则返回默认值 + InRange(this decimal value, decimal min, decimal max, decimal returnValue)
        /// <summary>
        /// 判断当前【Decimal】值是否在指定范围内，否则返回默认值
        /// </summary>
        /// <param name="value">要判断的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="returnValue">默认值</param>
        /// <returns>decimal</returns>
        public static decimal InRange(this decimal value, decimal min, decimal max, decimal returnValue) => value.InRange(min, max) ? value : returnValue;
        #endregion

        #region 判断字符串长度是否在指定范围内 + InLength(this string value, int min, int max)
        /// <summary>
        /// 判断字符串长度是否在指定范围内
        /// </summary>
        /// <param name="value">要判断的字符串</param>
        /// <param name="min">最小长度</param>
        /// <param name="max">最大长度</param>
        /// <returns></returns>
        public static bool InLength(this string value, int min, int max) => value.Length >= min && value.Length <= max;
        #endregion
    }
}
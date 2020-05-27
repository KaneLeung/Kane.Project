#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：DateTimeFormat
* 类 描 述 ：常用的时间格式枚举类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/5/15 23:16:03
* 更新时间 ：2020/5/15 23:16:03
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion

namespace Kane.Extension
{
    /// <summary>
    /// 常用的时间格式枚举类
    /// </summary>
    public enum DateTimeFormat
    {
        /// <summary>
        /// 长日期时间格式【yyyy-MM-dd HH:mm:ss】=>【2020-02-02 20:20:02】
        /// </summary>
        Long = 0,
        /// <summary>
        /// 短日期时间格式【yyyy-M-d H:m:s】=>【2020-2-2 20:20:2】
        /// </summary>
        Short = 1,
        /// <summary>
        /// 长日期时间格式，包含星期(长)【yyyy年MM月dd日 HH:mm:ss dddd】=>【2020年02月02日 20:20:02 星期日】
        /// </summary>
        LongDTWeek = 2,
        /// <summary>
        /// 短日期时间格式，包含星期(短)【yyyy年M月d日 H:m:s ddd】=>【2020年2月2日 20:20:2 周日】
        /// </summary>
        ShortDTWeek = 3,
        /// <summary>
        /// 长日期时间格式，包含星期(短)【yyyy年MM月dd日 HH:mm:ss ddd】=>【2020年02月02日 20:20:02 周日】
        /// </summary>
        LongDTShortWeek = 4,
        /// <summary>
        /// 短日期时间格式，包含星期(长)【yyyy年M月d日 H:m:s dddd】=>【2020年2月2日 20:20:2 星期日】
        /// </summary>
        ShortDTLongWeek = 5,
        /// <summary>
        /// 长日期格式，包含星期(长)【yyyy年MM月dd日 dddd】=>【2020年02月02日 星期日】
        /// </summary>
        LongDateWeek = 6,
        /// <summary>
        /// 短日期格式，包含星期(短)【yyyy年M月d日 ddd】=>【2020年2月2日 周日】
        /// </summary>
        ShortDateWeek = 7,
        /// <summary>
        /// 长日期格式，包含星期(短)【yyyy年MM月dd日 ddd】=>【2020年02月02日 周日】
        /// </summary>
        LongDateShortWeek = 8,
        /// <summary>
        /// 短日期时间格式，包含星期(长)【yyyy年M月d日 dddd】=>【2020年2月2日 星期日】
        /// </summary>
        ShortDateLongWeek = 9,
        /// <summary>
        /// 长日期时间格式【yyyy年MM月dd日 HH:mm:ss】=>【2020年02月02日 20:20:02】
        /// </summary>
        LongDT = 10,
        /// <summary>
        /// 短日期时间格式【yyyy年M月d日 H:m:s】=>【2020年2月2日 20:20:2】
        /// </summary>
        ShortDT = 11,
        /// <summary>
        /// 长日期格式【yyyy年MM月dd日】=>【2020年02月02日】
        /// </summary>
        LongDate = 12,
        /// <summary>
        /// 短日期格式【yyyy年M月d日】=>【2020年2月2日】
        /// </summary>
        ShortDate = 13,
        /// <summary>
        /// 长时间格式【HH:mm:ss】=>【20:20:02】
        /// </summary>
        LongTime = 14,
        /// <summary>
        /// 短时间格式【H:m:s】=>【20:20:2】
        /// </summary>
        ShortTime = 15,
        /// <summary>
        /// 长星期格式【dddd】=>【星期日】
        /// </summary>
        LongWeek = 16,
        /// <summary>
        /// 短星期格式【ddd】=>【周日】
        /// </summary>
        ShortWeek = 17,
    }
}
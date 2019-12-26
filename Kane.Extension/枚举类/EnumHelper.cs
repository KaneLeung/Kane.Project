#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：EnumHelper
* 类 描 述 ：枚举类扩展
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/16 23:16:16
* 更新时间 ：2019/10/16 23:16:16
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Kane.Extension
{
    /// <summary>
    /// 枚举类扩展
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举值的描述特性
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string Description(this Enum item)
        {
            string itemName = item.ToString();
            Type t = item.GetType();
            FieldInfo fieldInfo = t.GetField(itemName);
            DescriptionAttribute[] arrDesc = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return arrDesc[0].Description;
        }
    }
}

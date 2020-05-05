#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：EnumEx
* 类 描 述 ：枚举类扩展类
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/16 23:16:16
* 更新时间 ：2020/05/05 13:16:16
* 版 本 号 ：v1.0.2.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Kane.Extension
{
    /// <summary>
    /// 枚举类扩展类
    /// </summary>
    public static class EnumEx
    {
        #region 获取枚举值的描述特性 + Description(this Enum item, bool inherit = false)
        /// <summary>
        /// 获取枚举值的描述特性，默认【不继承】
        /// </summary>
        /// <param name="item">该枚举的其中一个成员即可</param>
        /// <param name="inherit">是否继承</param>
        /// <returns></returns>
        public static string Description(this Enum item, bool inherit = false)
        {
            FieldInfo fieldInfo = item.GetType().GetField(item.ToString());
            DescriptionAttribute[] arrDesc = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), inherit);
            return arrDesc[0]?.Description ?? string.Empty;
        }
        #endregion

#if !NET40
        #region 通过描述获取Enum值 + FormDescription<T>(string desc)
        /// <summary>
        /// 通过描述获取Enum值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="desc">描述</param>
        public static T FormDescription<T>(string desc)
        {
            if (desc.IsNullOrEmpty()) throw new ArgumentNullException($"【{nameof(desc)}】不能为空。");
            var type = typeof(T);
            var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.Default);
            var fieldInfo = fieldInfos.FirstOrDefault(p => p.GetCustomAttribute<DescriptionAttribute>(false)?.Description == desc);
            if (fieldInfo == null) throw new ArgumentNullException($"在枚举【{type.FullName}】中，未发现描述特性为【{desc}】的枚举项。");
            return (T)Enum.Parse(type, fieldInfo.Name);
        }
        #endregion
#endif

        #region 将【Enum】转换为【EnumItem】List，默认【不继承】 + EnumToList<T>(this T _, bool inherit = false) where T : Enum
        /// <summary>
        /// 将【Enum】转换为【EnumItem】List，默认【不继承】
        /// </summary>
        /// <param name="_">【弃元】该枚举的其中一个成员即可</param>
        /// <param name="inherit">是否继承</param>
        /// <returns></returns>
        public static IList<EnumItem> EnumToList<T>(this T _, bool inherit = false) where T : Enum
        {
            var result = new List<EnumItem>();
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                var temp = new EnumItem
                {
                    Key = item.ToString(),
                    Value = Convert.ToInt32(item)
                };
                var arrDesc = item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), inherit);
                if (arrDesc.Any()) temp.Description = (arrDesc.First() as DescriptionAttribute).Description;
                result.Add(temp);
            }
            return result;
        }
        #endregion
    }
}
#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：NotMappedAttribute
* 类 描 述 ：忽略对象映射特性
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/06/12 17:18:20
* 更新时间 ：2020/06/12 17:18:20
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;

namespace Kane.Extension
{
    /// <summary>
    /// 忽略对象映射特性
    /// <para>https://github.com/dotnet/runtime/blob/master/src/libraries/System.ComponentModel.Annotations/src/System/ComponentModel/DataAnnotations/Schema/NotMappedAttribute.cs</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false)]
    public class NotMappedAttribute : Attribute
    {
    }
}
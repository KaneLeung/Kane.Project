#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：DateTimeJsonConverter
* 类 描 述 ：自定义DateTimeJsonConverter
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/1/23 23:11:50
* 更新时间 ：2020/1/23 23:11:50
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
#if (NETCOREAPP3_0 || NETCOREAPP3_1)
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kane.Extension
{
    /// <summary>
    /// 自定义DateTimeJsonConverter
    /// https://github.com/dotnet/runtime/blob/master/src/libraries/System.Text.Json/src/System/Text/Json/Serialization/Converters/JsonValueConverterDateTime.cs
    /// </summary>
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        #region 转换格式的私有变量
        /// <summary>
        /// 转换格式的私有变量
        /// </summary>
        private readonly string _format;
        #endregion

        #region 【构造函数】转换器默认格式【yyyy-MM-dd HH:mm:ss】 + DateTimeJsonConverter()
        /// <summary>
        /// 【构造函数】转换器默认格式【yyyy-MM-dd HH:mm:ss】
        /// </summary>
        public DateTimeJsonConverter() => _format = "yyyy-MM-dd HH:mm:ss";
        #endregion

        #region 【构造函数】转换器自定义格式 + DateTimeJsonConverter(string format)
        /// <summary>
        /// 【构造函数】转换器自定义格式
        /// </summary>
        /// <param name="format">用户自定义格式</param>
        public DateTimeJsonConverter(string format) => _format = format;
        #endregion

        #region 重写转换器Read方法 + Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        /// <summary>
        /// 重写转换器Read方法
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => DateTime.Parse(reader.GetString());
        #endregion

        #region 重写转换器Write方法 + Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        /// <summary>
        /// 重写转换器Write方法
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToUniversalTime().ToString(_format));
        #endregion
    }
}
#endif
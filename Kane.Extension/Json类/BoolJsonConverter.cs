#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：BoolJsonConverter
* 类 描 述 ：自定义BoolJsonConverter
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/1/23 23:23:07
* 更新时间 ：2020/1/23 23:23:07
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
    /// System.Text.Json中接收的json中不能将 "true"/"false"识别为boolean的True/False，这也需要自定义Converter实现bool转换
    /// https://github.com/dotnet/runtime/blob/master/src/libraries/System.Text.Json/src/System/Text/Json/Serialization/Converters/JsonValueConverterBoolean.cs
    /// </summary>
    public class BoolJsonConverter : JsonConverter<bool>
    {
        #region 重写转换器Read方法 + Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        /// <summary>
        /// 重写转换器Read方法
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.True || reader.TokenType == JsonTokenType.False) return reader.GetBoolean();
            return bool.Parse(reader.GetString());
        }
        #endregion

        #region 重写转换器Write方法 + Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) => writer.WriteBooleanValue(value);
        /// <summary>
        /// 重写转换器Write方法
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) => writer.WriteBooleanValue(value);
        #endregion
    }
}
#endif
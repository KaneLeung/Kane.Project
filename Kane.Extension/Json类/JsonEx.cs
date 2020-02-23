#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：JsonEx
* 类 描 述 ：Json扩展类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/1/15 23:29:15
* 更新时间 ：2020/1/15 23:29:15
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
#if (NETCOREAPP3_0 || NETCOREAPP3_1)
using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;
#else
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
#endif


namespace Kane.Extension
{
    /// <summary>
    /// Json扩展类
    /// </summary>
    public static class JsonEx
    {
#if (NETCOREAPP3_0 || NETCOREAPP3_1)
        /// <summary>
        /// 全局序列化和反序列化的配置选项
        /// </summary>
        public static JsonSerializerOptions GlobalOption = DefaultOption();

        #region 自用的【System.Text.Json】序列化和反序列化的配置选项 + DefaultOption()
        /// <summary>
        /// 自用的【System.Text.Json】序列化和反序列化的配置选项
        /// </summary>
        /// <returns></returns>
        private static JsonSerializerOptions DefaultOption()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,//保持属性名称不变
                AllowTrailingCommas = true,//忽略多余的逗号
                IgnoreNullValues = true,//忽略Null值
                //PropertyNameCaseInsensitive = true;//反序列化是否不区分大小写
            };
            options.Converters.Add(new JsonConverterEx.DateTimeConverter());//使用【2020-02-21 17:06:15】时间格式
            options.Converters.Add(new JsonConverterEx.BoolConverter());//"true"/"false"识别为boolean的True/False
            options.Converters.Add(new JsonConverterEx.IntConverter());//"88"转为Int
            options.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);//中文不会被编码
            options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            return options;
        }
        #endregion

        #region 把对象序列化，转成Json字符串，使用默认配置选项 + ToJson<T>(this T value, JsonSerializerOptions options = null)
        /// <summary>
        /// 把对象序列化，转成Json字符串，使用默认配置选项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要序列化的对象</param>
        /// <param name="options">序列化参数</param>
        /// <returns></returns>
        public static string ToJson<T>(this T value, JsonSerializerOptions options = null) => JsonSerializer.Serialize(value, options ?? GlobalOption);
        #endregion

        #region 把对象序列化，转成Json字符串 + ToJson<T>(this T value, bool ignore)
        /// <summary>
        /// 把对象序列化，转成Json字符串，可忽略默认配置选项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要序列化的对象</param>
        /// <param name="ignore">是否忽略默认配置选项</param>
        /// <returns></returns>
        public static string ToJson<T>(this T value, bool ignore) => ignore ? JsonSerializer.Serialize(value) : JsonSerializer.Serialize(value, GlobalOption);
        #endregion

        #region Json字符串反序列化，转成对象,使用默认配置选项 + ToObject<T>(this string value, JsonSerializerOptions options = null)
        /// <summary>
        /// Json字符串反序列化，转成对象，使用默认配置选项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">Json字符串</param>
        /// <param name="options">序列化参数</param>
        /// <returns></returns>
        public static T ToObject<T>(this string value, JsonSerializerOptions options = null) => JsonSerializer.Deserialize<T>(value, options ?? GlobalOption);
        #endregion

        #region Json字符串反序列化，转成对象 + ToObject<T>(this string value, bool ignore)
        /// <summary>
        /// Json字符串反序列化，转成对象，可忽略默认配置选项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">Json字符串</param>
        /// <param name="ignore">是否忽略默认配置选项</param>
        /// <returns></returns>
        public static T ToObject<T>(this string value, bool ignore)
            => ignore ? JsonSerializer.Deserialize<T>(value) : JsonSerializer.Deserialize<T>(value, GlobalOption);
        #endregion
#else
        #region 自用的【Newtonsoft.Json】序列化和反序列化的配置选项 + DefaultSetting()
        /// <summary>
        /// 全局序列化和反序列化的配置选项
        /// </summary>
        public static JsonSerializerSettings GlobalSetting = DefaultSetting();

        /// <summary>
        /// 自用的【Newtonsoft.Json】序列化和反序列化的配置选项
        /// </summary>
        /// <returns></returns>
        private static JsonSerializerSettings DefaultSetting()
        {
            return new JsonSerializerSettings
            {
                //日期类型默认格式化处理
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                //空值处理
                NullValueHandling = NullValueHandling.Ignore,
                //设置不处理循环引用
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                //使用默认方式，保持属性名称不变
                ContractResolver = new DefaultContractResolver()
            };
        }
        #endregion

        #region 把对象序列化，转成Json字符串，使用默认配置选项 + ToJson<T>(this T value, JsonSerializerSettings settings = null)
        /// <summary>
        /// 把对象序列化，转成Json字符串，使用默认配置选项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要序列化的对象</param>
        /// <param name="settings">序列化参数</param>
        /// <returns></returns>
        public static string ToJson<T>(this T value, JsonSerializerSettings settings = null) => JsonConvert.SerializeObject(value, settings ?? GlobalSetting);
        #endregion

        #region 把对象序列化，转成Json字符串，可忽略默认配置选项 + ToJson<T>(this T value, bool ignore)
        /// <summary>
        /// 把对象序列化，转成Json字符串，可忽略默认配置选项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要序列化的对象</param>
        /// <param name="ignore">是否忽略默认配置选项</param>
        /// <returns></returns>
        public static string ToJson<T>(this T value, bool ignore) => ignore ? JsonConvert.SerializeObject(value) : JsonConvert.SerializeObject(value, GlobalSetting);
        #endregion

        #region Json字符串反序列化，转成对象 + ToObject<T>(this string value, JsonSerializerSettings settings = null)
        /// <summary>
        /// Json字符串反序列化，转成对象，使用默认配置选项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">Json字符串</param>
        /// <param name="settings">序列化参数</param>
        /// <returns></returns>
        public static T ToObject<T>(this string value, JsonSerializerSettings settings = null)
            => JsonConvert.DeserializeObject<T>(value, settings ?? GlobalSetting);
        #endregion

        #region Json字符串反序列化，转成对象，可忽略默认配置选项 + ToObject<T>(this string value)
        /// <summary>
        /// Json字符串反序列化，转成对象，可忽略默认配置选项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">Json字符串</param>
        /// <param name="ignore">是否忽略默认配置选项</param>
        /// <returns></returns>
        public static T ToObject<T>(this string value, bool ignore)
            => ignore ? JsonConvert.DeserializeObject<T>(value) : JsonConvert.DeserializeObject<T>(value, GlobalSetting);
        #endregion
#endif
    }
}
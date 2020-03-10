#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：JsonNet
* 类 描 述 ：Json扩展类，使用【Newtonsoft.Json】
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension.JsonNet
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/3/10 21:35:10
* 更新时间 ：2020/3/10 21:35:10
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kane.Extension.JsonNet
{
    /// <summary>
    /// Json扩展类，使用【Newtonsoft.Json】
    /// </summary>
    public static class JsonNet
    {
        #region 自用的【Newtonsoft.Json】全局序列化和反序列化的配置选项
        /// <summary>
        /// 全局序列化和反序列化的配置选项
        /// </summary>
        public static JsonSerializerSettings GlobalSetting;
        #endregion

        #region 静态构造函数，保证只运行一次
        /// <summary>
        /// 静态构造函数，保证只运行一次
        /// </summary>
        static JsonNet()
        {
            #region 自用的【Newtonsoft.Json】全局序列化和反序列化的配置选项
            GlobalSetting = new JsonSerializerSettings
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
            #endregion
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
        public static T ToObject<T>(this string value, JsonSerializerSettings settings = null) => JsonConvert.DeserializeObject<T>(value, settings ?? GlobalSetting);
        #endregion

        #region Json字符串反序列化，转成对象，可忽略默认配置选项 + ToObject<T>(this string value, bool ignore)
        /// <summary>
        /// Json字符串反序列化，转成对象，可忽略默认配置选项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">Json字符串</param>
        /// <param name="ignore">是否忽略默认配置选项</param>
        /// <returns></returns>
        public static T ToObject<T>(this string value, bool ignore) => ignore ? JsonConvert.DeserializeObject<T>(value) : JsonConvert.DeserializeObject<T>(value, GlobalSetting);
        #endregion
    }
}
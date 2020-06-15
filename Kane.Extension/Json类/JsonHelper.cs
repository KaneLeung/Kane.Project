#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：JsonHelper
* 类 描 述 ：Json读取和写入帮助类
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/11/15 23:56:17
* 更新时间 ：2020/05/05 11:16:17
* 版 本 号 ：v1.0.2.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
#if NETCOREAPP3_1
using System.Text.Json;
#else
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
#endif

namespace Kane.Extension
{
    /// <summary>
    /// Json读取和写入扩展类
    /// </summary>
    public class JsonHelper
    {
        private string JSON_DATA;
        private bool IS_FILE = false;
        private string FILE_PATH = string.Empty;
        /// <summary>
        /// 默认分割的字符，默认为【:】半角冒号
        /// </summary>
        public static char SplitChar = ':';
#if NETCOREAPP3_1
        /// <summary>
        /// 字符串转JsonDocument时设置的参数，可设置深度
        /// <para>【MaxDepth】深度默认为64</para>
        /// </summary>
        public static JsonDocumentOptions Options = default;
#else
        /// <summary>
        /// 字符串转JObject时设置的参数
        /// </summary>
        public static JsonLoadSettings Settings = null;
#endif


        #region 无参构造函数 + JsonHelper()
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public JsonHelper()
        {
        }
        #endregion

        #region 构造函数，可以是文件路径，也可以是JSON字符串，使用默认编码UTF8 + JsonHelper(string source)
        /// <summary>
        /// 构造函数，可以是文件路径，也可以是JSON字符串，使用默认编码UTF8
        /// </summary>
        /// <param name="source"></param>
        public JsonHelper(string source) : this(source, Encoding.UTF8)
        {
        }
        #endregion

        #region 构造函数，可以是文件路径，也可以是JSON字符串,可设置编码 + JsonHelper(string source, Encoding encoding)
        /// <summary>
        /// 构造函数，可以是文件路径，也可以是JSON字符串,可设置编码
        /// </summary>
        /// <param name="source">可以是文件路径，也可以是JSON字符串</param>
        /// <param name="encoding">编码</param>
        public JsonHelper(string source, Encoding encoding)
        {
            if (File.Exists(source))
            {
                JSON_DATA = File.ReadAllText(source, encoding);
                FILE_PATH = source;
                IS_FILE = true;
            }
            else JSON_DATA = source;
        }
        #endregion

        #region 加载Json内容 + LoadData(string source)
        /// <summary>
        /// 加载Json内容
        /// </summary>
        /// <param name="source">可以是文件路径，也可以是JSON字符串</param>
        /// <returns>是否加载成功</returns>
        public bool LoadData(string source)
        {
            try
            {
                if (File.Exists(source))
                {
                    JSON_DATA = File.ReadAllText(source, Encoding.UTF8);
                    FILE_PATH = source;
                    IS_FILE = true;
                }
                else JSON_DATA = source;
                return JSON_DATA.IsValuable();
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

#if NETCOREAPP3_1
        #region 根据关键词，获取对应的值 + GetValue<T>(string keys, T returnValue = default)
        /// <summary>
        /// 根据关键词，获取对应的值，关键词可用 Key1:Key2:Key3进行遍历，
        /// <para>如果最后一级是Int值，可以取数组的索引对应的值</para>
        /// <para>默认最大【深度】为64</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">关键词，多级时可用【:】半角冒号分割</param>
        /// <param name="returnValue">失败返回值</param>
        /// <returns></returns>
        public T GetValue<T>(string keys, T returnValue = default)
        {
            try
            {
                var keyValues = keys.Trim(SplitChar).Split(SplitChar);
                using var doc = JsonDocument.Parse(JSON_DATA, Options);
                List<JsonElement> result = new List<JsonElement>();
                if (keyValues.Length > 1 && int.TryParse(keyValues.LastOrDefault(), out int index))//大于两个元素，并且最后一位能转为Int类型
                {
                    var _keyValues = keyValues[0..^1];//复制数组，并弃掉最后一个元素
                    GetElements(doc.RootElement.EnumerateObject(), _keyValues, result);
                    if (result.Count > 0 && result.FirstOrDefault().ValueKind == JsonValueKind.Array)
                    {
                        var _result = GetValueByElement<List<T>>(result.FirstOrDefault(), null);
                        if (_result.Count > index) return (T)Convert.ChangeType(_result[index], typeof(T));
                        else return returnValue;
                    }
                }
                else if (keyValues.Length > 1)
                {
                    GetElements(doc.RootElement.EnumerateObject(), keyValues, result);
                    if (result.Count > 0) return GetValueByElement(result.FirstOrDefault(), returnValue);
                }
                else GetElemnets(doc.RootElement.EnumerateObject(), keys, result);
                if (result.Count > 0) return GetValueByElement(result.FirstOrDefault(), returnValue);
                else return returnValue;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
                throw ex;
                //return returnValue;
            }
        }
        #endregion

        #region 根据多个关键词遍历JsonPropertys所有子类 + GetElements(IEnumerable<JsonProperty> properties, string[] keys, List<JsonElement> result, int index = 0)
        /// <summary>
        /// 根据JsonPropertys遍历所有子类
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="keys"></param>
        /// <param name="result"></param>
        /// <param name="index"></param>
        private static void GetElements(IEnumerable<JsonProperty> properties, string[] keys, List<JsonElement> result, int index = 0)
        {
            bool isLast = keys.Length == index + 1;
            foreach (var item in properties)
            {
                if (isLast && item.Name == keys[index] && (item.Value.ValueKind != JsonValueKind.Undefined || item.Value.ValueKind != JsonValueKind.Null))
                {
                    result.Add(item.Value);
                }
                else if (item.Name == keys[index] && item.Value.ValueKind == JsonValueKind.Object)
                {
                    GetElements(item.Value.EnumerateObject(), keys, result, index + 1);
                }
            }
        }
        #endregion

        #region 根据单个关键词遍历JsonPropertys所有子类 + GetElemnets(IEnumerable<JsonProperty> properties, string key, List<JsonElement> result)
        /// <summary>
        /// 根据单个关键词遍历JsonPropertys所有子类
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="key"></param>
        /// <param name="result"></param>
        private static void GetElemnets(IEnumerable<JsonProperty> properties, string key, List<JsonElement> result)
        {
            foreach (var item in properties)
            {
                if (item.Name == key) result.Add(item.Value);
                if (item.Value.ValueKind == JsonValueKind.Object) GetElemnets(item.Value.EnumerateObject(), key, result);
            }
        }
        #endregion

        #region 根据JsonElement获取值 + GetValueByElement<T>(JsonElement element, T returnValue)
        /// <summary>
        /// 根据JsonElement获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        private static T GetValueByElement<T>(JsonElement element, T returnValue)
        {
            try
            {
                var type = typeof(T);
                if (type.Equals(typeof(string))) return (T)Convert.ChangeType(element.GetString(), type);
                else if (type.Equals(typeof(DateTime))) return element.TryGetDateTime(out DateTime result) ? (T)Convert.ChangeType(result, type) : returnValue;
                else if (type.Equals(typeof(short))) return element.TryGetInt16(out short result) ? (T)Convert.ChangeType(result, type) : returnValue;
                else if (type.Equals(typeof(int))) return element.TryGetInt32(out int result) ? (T)Convert.ChangeType(result, type) : returnValue;
                else if (type.Equals(typeof(long))) return element.TryGetInt64(out long result) ? (T)Convert.ChangeType(result, type) : returnValue;
                else if (type.Equals(typeof(float))) return element.TryGetSingle(out float result) ? (T)Convert.ChangeType(result, type) : returnValue;
                else if (type.Equals(typeof(double))) return element.TryGetDouble(out double result) ? (T)Convert.ChangeType(result, type) : returnValue;
                else if (type.Equals(typeof(decimal))) return element.TryGetDecimal(out decimal result) ? (T)Convert.ChangeType(result, type) : returnValue;
                else if (type.Equals(typeof(bool))) return (T)Convert.ChangeType(element.ToString().ToBool(), type);
                else if (type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))//判断是否为可空类型
                {
                    var actType = type.GetGenericArguments().FirstOrDefault();//获取实际的Type
                    if (actType.Equals(typeof(string))) return (T)Convert.ChangeType(element.GetString(), actType);
                    else if (actType.Equals(typeof(DateTime))) return element.TryGetDateTime(out DateTime result) ? (T)Convert.ChangeType(result, actType) : returnValue;
                    else if (actType.Equals(typeof(short))) return element.TryGetInt16(out short result) ? (T)Convert.ChangeType(result, actType) : returnValue;
                    else if (actType.Equals(typeof(int))) return element.TryGetInt32(out int result) ? (T)Convert.ChangeType(result, actType) : returnValue;
                    else if (actType.Equals(typeof(long))) return element.TryGetInt64(out long result) ? (T)Convert.ChangeType(result, actType) : returnValue;
                    else if (actType.Equals(typeof(float))) return element.TryGetSingle(out float result) ? (T)Convert.ChangeType(result, actType) : returnValue;
                    else if (actType.Equals(typeof(double))) return element.TryGetDouble(out double result) ? (T)Convert.ChangeType(result, actType) : returnValue;
                    else if (actType.Equals(typeof(decimal))) return element.TryGetDecimal(out decimal result) ? (T)Convert.ChangeType(result, actType) : returnValue;
                    else if (actType.Equals(typeof(bool))) return (T)Convert.ChangeType(element.ToString().ToBool(), actType);
                    else return default;
                }
                else return JsonSerializer.Deserialize<T>(element.ToString());//一些如Array、List 的集合
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
                throw ex;
            }
        }
        #endregion

        #region 在原有的JSON字符串中修改或添加新的Json元素 + SetValue<T>(string keys, T value)
        /// <summary>
        /// 在原有的JSON字符串中修改或添加新的Json元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">关键词，多级时默认用【:】半角冒号分割</param>
        /// <param name="value">修改或添加的内容</param>
        /// <returns>修改后或添加后的Json字符串</returns>
        public string SetValue<T>(string keys, T value)
        {
            try
            {
                var keyValues = keys.Trim(SplitChar).Split(SplitChar);
                var buffer = new System.Buffers.ArrayBufferWriter<byte>();
                using var writer = new Utf8JsonWriter(buffer, new JsonWriterOptions { Indented = true });
                writer.WriteStartObject();
                if (JSON_DATA.IsValuable())
                {
                    using var document = JsonDocument.Parse(JSON_DATA, Options);
                    WriteJsonObject(writer, keyValues, document.RootElement.EnumerateObject(), value);
                }
                else WriteJsonObject(writer, keyValues, new List<JsonProperty>(), value);
                writer.WriteEndObject();
                writer.Flush();
                JSON_DATA = Encoding.UTF8.GetString(buffer.WrittenSpan.ToArray());
                return JSON_DATA;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 在JSON字符串中修改或添加新的Json元素 + SetValue<T>(string source, Encoding encoding, string keys, T value)
        /// <summary>
        /// 在JSON字符串中修改或添加新的Json元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">源数据，可以是Json字符串或文件路径</param>
        /// <param name="encoding">编码</param>
        /// <param name="keys">关键词，多级时默认用【:】半角冒号分割</param>
        /// <param name="value">修改或添加的内容</param>
        /// <returns>修改后或添加后的Json字符串</returns>
        public string SetValue<T>(string source, Encoding encoding, string keys, T value)
        {
            try
            {
                if (File.Exists(source)) source = File.ReadAllText(source, encoding);
                var keyValues = keys.Trim(SplitChar).Split(SplitChar);
                var buffer = new System.Buffers.ArrayBufferWriter<byte>();
                using var writer = new Utf8JsonWriter(buffer, new JsonWriterOptions { Indented = true });
                writer.WriteStartObject();
                if (source.IsValuable())
                {
                    using var document = JsonDocument.Parse(source, Options);
                    WriteJsonObject(writer, keyValues, document.RootElement.EnumerateObject(), value);
                }
                else WriteJsonObject(writer, keyValues, new List<JsonProperty>(), value);
                writer.WriteEndObject();
                writer.Flush();
                return Encoding.UTF8.GetString(buffer.WrittenSpan.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Utf8JsonWriter写对象方法 + WriteJsonObject<T>(Utf8JsonWriter write, string[] keys, IEnumerable<JsonProperty> properties, T value, bool flag = false, int index = 0)
        /// <summary>
        /// Utf8JsonWriter写对象方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="write">Utf8JsonWriter写入器</param>
        /// <param name="keys">关键词，可以是多个，默认用【:】半角冒号分割</param>
        /// <param name="properties">Json属性</param>
        /// <param name="value">要写入的值</param>
        /// <param name="flag">查找到目标标志</param>
        /// <param name="index">当前关键词索引</param>
        private static void WriteJsonObject<T>(Utf8JsonWriter write, string[] keys, IEnumerable<JsonProperty> properties, T value, bool flag = false, int index = 0)
        {
            foreach (var item in properties)
            {
                if (keys.Length <= index || (keys.Length > index && item.Name != keys[index]))//如果Index大于Keys的范围，或在Key[index]不等于当前
                {
                    switch (item.Value.ValueKind)
                    {
                        case JsonValueKind.Undefined:
                            break;
                        case JsonValueKind.Object:
                            write.WriteStartObject(item.Name);
                            WriteJsonObject(write, keys, item.Value.EnumerateObject(), value, flag, index + 1);
                            write.WriteEndObject();
                            break;
                        case JsonValueKind.Array:
                            write.WriteStartArray(item.Name);
                            WriteJsonArray(write, keys, item.Value.EnumerateArray(), value, flag);
                            write.WriteEndArray();
                            break;
                        case JsonValueKind.String:
                            write.WriteString(item.Name, item.Value.GetString());
                            break;
                        case JsonValueKind.Number:
                            write.WriteNumber(item.Name, item.Value.GetDecimal());
                            break;
                        case JsonValueKind.True:
                        case JsonValueKind.False:
                            write.WriteBoolean(item.Name, item.Value.GetBoolean());
                            break;
                        case JsonValueKind.Null:
                            write.WriteNull(item.Name);
                            break;
                        default:
                            break;
                    }
                }
                else if (keys.Length > index && item.Name == keys[index]) //在Keys的范围内存在
                {
                    var type = typeof(T);
                    if (type.Equals(typeof(string))) write.WriteString(item.Name, value.ToString());
                    else if (type.Equals(typeof(DateTime))) write.WriteString(item.Name, value.ToString());
                    else if (type.Equals(typeof(short))) write.WriteNumber(item.Name, (short)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(int))) write.WriteNumber(item.Name, (int)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(long))) write.WriteNumber(item.Name, (long)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(float))) write.WriteNumber(item.Name, (float)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(double))) write.WriteNumber(item.Name, (double)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(decimal))) write.WriteNumber(item.Name, (decimal)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(bool))) write.WriteBoolean(item.Name, (bool)Convert.ChangeType(value, type));
                    else //如果是集合类型，则继续递归
                    {
                        var temp = JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = true });
                        write.WriteStartObject(item.Name);
                        WriteJsonObject(write, keys, JsonDocument.Parse(temp, Options).RootElement.EnumerateObject(), value, true, index + 1);
                        write.WriteEndObject();
                    }
                }
            }
            if (keys.Length > index && !properties.Any(k => k.Name == keys[index]) && (flag || index == 0))//在范围内，并且不存在，则创建
            {
                if (keys.Length == index + 1)//如果是最后一个Key
                {
                    var type = typeof(T);
                    if (type.Equals(typeof(string))) write.WriteString(keys[index], value.ToString());
                    else if (type.Equals(typeof(DateTime))) write.WriteString(keys[index], value.ToString());
                    else if (type.Equals(typeof(short))) write.WriteNumber(keys[index], (short)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(int))) write.WriteNumber(keys[index], (int)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(long))) write.WriteNumber(keys[index], (long)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(float))) write.WriteNumber(keys[index], (float)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(double))) write.WriteNumber(keys[index], (double)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(decimal))) write.WriteNumber(keys[index], (decimal)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(bool))) write.WriteBoolean(keys[index], (bool)Convert.ChangeType(value, type));
                    else //如果是集合类型，则继续递归
                    {
                        var temp = JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = true });
                        write.WriteStartObject(keys[index]);
                        WriteJsonObject(write, keys, JsonDocument.Parse(temp, Options).RootElement.EnumerateObject(), value, true, index + 1);
                        write.WriteEndObject();
                    }
                }
                else //如果不是最后一个Key,则创建对象，并继续递归下一级
                {
                    write.WriteStartObject(keys[index]);
                    WriteJsonObject(write, keys, new List<JsonProperty>(), value, true, index + 1);
                    write.WriteEndObject();
                }
            }
        }
        #endregion

        #region Utf8JsonWriter写数组方法 + WriteJsonArray<T>(Utf8JsonWriter write, string[] keys, IEnumerable<JsonElement> elements, T value, bool flag = false, int index = 0)
        /// <summary>
        /// Utf8JsonWriter写数组方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="write">Utf8JsonWriter写入器</param>
        /// <param name="keys">关键词，可以是多个，默认用【:】半角冒号分割</param>
        /// <param name="elements">Json元素</param>
        /// <param name="value">要写入的值</param>
        /// <param name="flag">查找到目标标志</param>
        /// <param name="index">当前关键词索引</param>
        private static void WriteJsonArray<T>(Utf8JsonWriter write, string[] keys, IEnumerable<JsonElement> elements, T value, bool flag = false, int index = 0)
        {
            foreach (var item in elements)
            {
                switch (item.ValueKind)
                {
                    case JsonValueKind.Undefined:
                        break;
                    case JsonValueKind.Object:
                        write.WriteStartObject();
                        WriteJsonObject(write, keys, item.EnumerateObject(), value, flag, index);
                        write.WriteEndObject();
                        break;
                    case JsonValueKind.Array:
                        write.WriteStartArray();
                        WriteJsonArray(write, keys, item.EnumerateArray(), value, flag, index);
                        write.WriteEndArray();
                        break;
                    case JsonValueKind.String:
                        write.WriteStringValue(item.GetString());
                        break;
                    case JsonValueKind.Number:
                        write.WriteNumberValue(item.GetDecimal());
                        break;
                    case JsonValueKind.True:
                    case JsonValueKind.False:
                        write.WriteBooleanValue(item.GetBoolean());
                        break;
                    case JsonValueKind.Null:
                        write.WriteNullValue();
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
#else
        #region 根据关键词，获取对应的值 + GetValue<T>(string keys, T returnValue = default)
        /// <summary>
        /// 根据关键词，获取对应的值，关键词可用 Key1:Key2:Key3进行遍历，
        /// 如果最后一级是Int值，可以取数组的索引对应的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">关键词，多级时默认用【:】半角冒号分割</param>
        /// <param name="returnValue">失败返回值</param>
        /// <returns></returns>
        public T GetValue<T>(string keys, T returnValue = default)
        {
            try
            {
                var keyValues = keys.Trim(SplitChar).Split(SplitChar);
                var rootData = JObject.Parse(JSON_DATA, Settings);
                if (keyValues.Length > 1)
                {
                    if (int.TryParse(keyValues.LastOrDefault(), out int index))//最后一位是数字
                    {
                        string[] tempKeys = new string[keyValues.Length - 1];
                        Array.Copy(keyValues, tempKeys, keyValues.Length - 1);
                        var temp = rootData.SelectToken(string.Join(".", tempKeys));
                        if (temp.Type == JTokenType.Array)
                        {
                            var itemList = temp.ToObject<List<T>>();
                            if (itemList.Count >= index + 1) return itemList[index];
                        }
                    }
                    return rootData.SelectToken(string.Join(".", keyValues)).ToObject<T>() ?? returnValue;
                }
                else
                {
                    List<JToken> result = new List<JToken>();
                    GetJTokens(rootData.Properties(), keys, result);
                    if (result.Count > 0) return result.FirstOrDefault().ToObject<T>();
                    else return default;
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
                throw ex;
                //return returnValue;
            }
        }
        #endregion

        #region 根据JProperty遍历所有子类 + GetJTokens(IEnumerable<JProperty> properties, string key, List<JToken> result)
        /// <summary>
        /// 根据JProperty遍历所有子类
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="key"></param>
        /// <param name="result"></param>
        private static void GetJTokens(IEnumerable<JProperty> properties, string key, List<JToken> result)
        {
            foreach (JProperty item in properties)
            {
                if (item.Name == key) result.Add(item.Value);
                if (item.Value.Type == JTokenType.Object) GetJTokens(((JObject)item.Value).Properties(), key, result);
            }
        }
        #endregion

        #region 在原有的JSON字符串中修改或添加新的Json元素 + SetValue<T>(string keys, T value)
        /// <summary>
        /// 在原有的JSON字符串中修改或添加新的Json元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">关键词，多级时默认用【:】半角冒号分割</param>
        /// <param name="value">修改或添加的内容</param>
        /// <returns>修改后或添加后的Json字符串</returns>
        public string SetValue<T>(string keys, T value)
        {
            try
            {
                var keyValues = keys.Trim(SplitChar).Split(SplitChar);
                StringWriter stringWriter = new StringWriter();
                using JsonWriter writer = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
                writer.WriteStartObject();
                if (JSON_DATA.IsValuable())
                {
                    var rootData = JObject.Parse(JSON_DATA, Settings);
                    WriteJsonObject(writer, keyValues, rootData.Properties(), value);
                }
                else WriteJsonObject(writer, keyValues, new List<JProperty>(), value);
                writer.WriteEndObject();
                writer.Flush();
                JSON_DATA = stringWriter.GetStringBuilder().ToString();
                return JSON_DATA;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 在JSON字符串中修改或添加新的Json元素 + SetValue<T>(string source, Encoding encoding, string keys, T value)
        /// <summary>
        /// 在JSON字符串中修改或添加新的Json元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">源数据，可以是Json字符串或文件路径</param>
        /// <param name="encoding">编码</param>
        /// <param name="keys">关键词，多级时默认用【:】半角冒号分割</param>
        /// <param name="value">修改或添加的内容</param>
        /// <returns>修改后或添加后的Json字符串</returns>
        public string SetValue<T>(string source, Encoding encoding, string keys, T value)
        {
            try
            {
                if (File.Exists(source)) source = File.ReadAllText(source, encoding);
                var keyValues = keys.Trim(SplitChar).Split(SplitChar);
                StringWriter stringWriter = new StringWriter();
                using JsonWriter writer = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
                writer.WriteStartObject();
                if (source.IsValuable())
                {
                    var rootData = JObject.Parse(source, Settings);
                    WriteJsonObject(writer, keyValues, rootData.Properties(), value);
                }
                else WriteJsonObject(writer, keyValues, new List<JProperty>(), value);
                writer.WriteEndObject();
                writer.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region JsonWriter写对象方法 + WriteJsonObject<T>(JsonWriter write, string[] keys, IEnumerable<JProperty> properties, T value, bool flag = false, int index = 0)
        /// <summary>
        /// JsonWriter写对象方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="write">JsonWriter写入器</param>
        /// <param name="keys">关键词，可以是多个，默认用【:】半角冒号分割</param>
        /// <param name="properties">Json属性</param>
        /// <param name="value">要写入的值</param>
        /// <param name="flag">查找到目标标志</param>
        /// <param name="index">当前关键词索引</param>
        private static void WriteJsonObject<T>(JsonWriter write, string[] keys, IEnumerable<JProperty> properties, T value, bool flag = false, int index = 0)
        {
            foreach (var item in properties)
            {
                if (keys.Length <= index || (keys.Length > index && item.Name != keys[index]))//如果Index大于Keys的范围，或在Key[index]不等于当前
                {
                    switch (item.Value.Type)
                    {
                        case JTokenType.Object:
                            write.WritePropertyName(item.Name);
                            write.WriteStartObject();
                            WriteJsonObject(write, keys, ((JObject)item.Value).Properties(), value, flag, index + 1);
                            write.WriteEndObject();
                            break;
                        case JTokenType.Array:
                            write.WritePropertyName(item.Name);
                            write.WriteStartArray();
                            WriteJsonArray(write, keys, item.Value.Children(), value, flag);
                            write.WriteEndArray();
                            break;
                        case JTokenType.Property:
                            write.WritePropertyName(item.Value.ToString());
                            break;
                        case JTokenType.Comment:
                            write.WritePropertyName(item.Name);
                            write.WriteComment(item.Value.ToString());
                            break;
                        case JTokenType.Integer:
                            write.WritePropertyName(item.Name);
                            write.WriteValue(item.Value.ToObject<int>());
                            break;
                        case JTokenType.Float:
                            write.WritePropertyName(item.Name);
                            write.WriteValue(item.Value.ToObject<float>());
                            break;
                        case JTokenType.String:
                            write.WritePropertyName(item.Name);
                            write.WriteValue(item.Value.ToObject<string>());
                            break;
                        case JTokenType.Boolean:
                            write.WritePropertyName(item.Name);
                            write.WriteValue(item.Value.ToObject<bool>());
                            break;
                        case JTokenType.Null:
                            write.WritePropertyName(item.Name);
                            write.WriteNull();
                            break;
                        case JTokenType.Date:
                            write.WritePropertyName(item.Name);
                            write.WriteValue(item.Value.ToObject<DateTime>());
                            break;
                        case JTokenType.Raw:
                            write.WritePropertyName(item.Name);
                            write.WriteRaw(item.Value.ToString());
                            break;
                        case JTokenType.Bytes:
                            write.WritePropertyName(item.Name);
                            write.WriteValue(item.Value.ToObject<byte>());
                            break;
                        case JTokenType.Guid:
                            write.WritePropertyName(item.Name);
                            write.WriteValue(item.Value.ToObject<Guid>());
                            break;
                        case JTokenType.Uri:
                            write.WritePropertyName(item.Name);
                            write.WriteValue(item.Value.ToObject<Uri>());
                            break;
                        case JTokenType.TimeSpan:
                            write.WritePropertyName(item.Name);
                            write.WriteValue(item.Value.ToObject<TimeSpan>());
                            break;
                        case JTokenType.None:
                        case JTokenType.Constructor:
                        case JTokenType.Undefined:
                        default:
                            break;
                    }
                }
                else if (keys.Length > index && item.Name == keys[index]) //在Keys的范围内存在
                {
                    var type = typeof(T);
                    write.WritePropertyName(item.Name);
                    if (type.Equals(typeof(string))) write.WriteValue(value.ToString());
                    else if (type.Equals(typeof(DateTime))) write.WriteValue(value.ToString());
                    else if (type.Equals(typeof(short))) write.WriteValue((short)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(int))) write.WriteValue((int)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(long))) write.WriteValue((long)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(float))) write.WriteValue((float)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(double))) write.WriteValue((double)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(decimal))) write.WriteValue((decimal)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(bool))) write.WriteValue((bool)Convert.ChangeType(value, type));
                    else //如果是集合类型，则继续递归
                    {
                        var temp = JsonConvert.SerializeObject(value);
                        write.WriteStartObject();
                        WriteJsonObject(write, keys, JObject.Parse(temp, Settings).Properties(), value, true, index + 1);
                        write.WriteEndObject();
                    }
                }
            }
            if (keys.Length > index && !properties.Any(k => k.Name == keys[index]) && (flag || index == 0))//在范围内，并且不存在，则创建
            {
                if (keys.Length == index + 1)//如果是最后一个Key
                {
                    var type = typeof(T);
                    write.WritePropertyName(keys[index]);
                    if (type.Equals(typeof(string))) write.WriteValue(value.ToString());
                    else if (type.Equals(typeof(DateTime))) write.WriteValue(value.ToString());
                    else if (type.Equals(typeof(short))) write.WriteValue((short)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(int))) write.WriteValue((int)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(long))) write.WriteValue((long)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(float))) write.WriteValue((float)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(double))) write.WriteValue((double)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(decimal))) write.WriteValue((decimal)Convert.ChangeType(value, type));
                    else if (type.Equals(typeof(bool))) write.WriteValue((bool)Convert.ChangeType(value, type));
                    else //如果是集合类型，则继续递归
                    {
                        var temp = JsonConvert.SerializeObject(value);
                        write.WriteStartObject();
                        WriteJsonObject(write, keys, JObject.Parse(temp, Settings).Properties(), value, true, index + 1);
                        write.WriteEndObject();
                    }
                }
                else //如果不是最后一个Key,则创建对象，并继续递归下一级
                {
                    write.WritePropertyName(keys[index]);
                    write.WriteStartObject();
                    WriteJsonObject(write, keys, new List<JProperty>(), value, true, index + 1);
                    write.WriteEndObject();
                }
            }
        }
        #endregion

        #region JsonWriter写数组方法 + WriteJsonArray<T>(JsonWriter write, string[] keys, IEnumerable<JToken> tokens, T value, bool flag = false, int index = 0)
        /// <summary>
        /// JsonWriter写数组方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="write">JsonWriter写入器</param>
        /// <param name="keys">关键词，可以是多个，默认用【:】半角冒号分割</param>
        /// <param name="tokens">Json元素</param>
        /// <param name="value">要写入的值</param>
        /// <param name="flag">查找到目标标志</param>
        /// <param name="index">当前关键词索引</param>
        private static void WriteJsonArray<T>(JsonWriter write, string[] keys, IEnumerable<JToken> tokens, T value, bool flag = false, int index = 0)
        {
            foreach (var item in tokens)
            {
                switch (item.Type)
                {
                    case JTokenType.Object:
                        write.WriteStartObject();
                        WriteJsonObject(write, keys, ((JObject)item).Properties(), value, flag, index);
                        write.WriteEndObject();
                        break;
                    case JTokenType.Array:
                        write.WriteStartArray();
                        WriteJsonArray(write, keys, ((JArray)item).Children(), value, flag, index);
                        write.WriteEndArray();
                        break;
                    case JTokenType.Property:
                        write.WritePropertyName(item.ToString());
                        break;
                    case JTokenType.Comment:
                        write.WriteComment(item.ToString());
                        break;
                    case JTokenType.Integer:
                        write.WriteValue(item.ToObject<int>());
                        break;
                    case JTokenType.Float:
                        write.WriteValue(item.ToObject<float>());
                        break;
                    case JTokenType.String:
                        write.WriteValue(item.ToObject<string>());
                        break;
                    case JTokenType.Boolean:
                        write.WriteValue(item.ToObject<bool>());
                        break;
                    case JTokenType.Null:
                        write.WriteNull();
                        break;
                    case JTokenType.Date:
                        write.WriteValue(item.ToObject<DateTime>());
                        break;
                    case JTokenType.Raw:
                        write.WriteRaw(item.ToString());
                        break;
                    case JTokenType.Bytes:
                        write.WriteValue(item.ToObject<byte>());
                        break;
                    case JTokenType.Guid:
                        write.WriteValue(item.ToObject<Guid>());
                        break;
                    case JTokenType.Uri:
                        write.WriteValue(item.ToObject<Uri>());
                        break;
                    case JTokenType.TimeSpan:
                        write.WriteValue(item.ToObject<TimeSpan>());
                        break;
                    case JTokenType.None:
                    case JTokenType.Constructor:
                    case JTokenType.Undefined:
                    default:
                        break;
                }
            }
        }
        #endregion
#endif
        #region 在JSON字符串中修改或添加新的Json元素,默认使用UTF8编码 + SetValue<T>(string source, string keys, T value)
        /// <summary>
        /// 在JSON字符串中修改或添加新的Json元素,默认使用UTF8编码
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">源数据，可以是Json字符串或文件路径</param>
        /// <param name="keys">关键词，多级时默认用【:】半角冒号分割</param>
        /// <param name="value">修改或添加的内容</param>
        /// <returns>修改后或添加后的Json字符串</returns>
        public string SetValue<T>(string source, string keys, T value) => SetValue(source, Encoding.UTF8, keys, value);
        #endregion

        #region 在原有的JSON字符串中修改或添加新的Json元素，并保存到原来加载的文件上 + SetValueSaveFile<T>(string keys, T value)
        /// <summary>
        /// 在原有的JSON字符串中修改或添加新的Json元素，并保存到原来加载的文件上
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">关键词，多级时可用【:】冒号分割</param>
        /// <param name="value">修改或添加的内容</param>
        /// <returns>修改后或添加后的Json字符串</returns>
        public string SetValueSaveFile<T>(string keys, T value)
        {
            SetValue(keys, value);
            SaveFile();
            return JSON_DATA;
        }
        #endregion

        #region 加载文件或Json字符串，并修改或添加新的Json元素，并保存到指定文件，默认UTF8编码 + SetValueSaveFile<T>(string source, string keys, T value, string path)
        /// <summary>
        /// 加载文件或Json字符串，并修改或添加新的Json元素，并保存到指定文件，默认UTF8编码
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">源数据，可以是Json字符串或文件路径</param>
        /// <param name="keys">关键词，多级时默认用【:】半角冒号分割</param>
        /// <param name="value">修改或添加的内容</param>
        /// <param name="path">保存的文件路径+文件名</param>
        /// <returns>修改后的Json字符串</returns>
        public string SetValueSaveFile<T>(string source, string keys, T value, string path) => SetValueSaveFile(source, Encoding.UTF8, keys, value, path);
        #endregion

        #region 加载文件或Json字符串，并修改或添加新的Json元素，并保存到指定文件，可指定编码 + SetValueSaveFile<T>(string source, Encoding encoding, string keys, T value, string path)
        /// <summary>
        /// 加载文件或Json字符串，并修改或添加新的Json元素，并保存到指定文件，可指定编码
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">源数据，可以是Json字符串或文件路径</param>
        /// <param name="encoding">编码</param>
        /// <param name="keys">关键词，多级时默认用【:】半角冒号分割</param>
        /// <param name="value">修改或添加的内容</param>
        /// <param name="path">保存的文件路径+文件名</param>
        /// <returns>修改后的Json字符串</returns>
        public string SetValueSaveFile<T>(string source, Encoding encoding, string keys, T value, string path)
        {
            var data = SetValue(source, encoding, keys, value);
            File.WriteAllText(path, data, encoding);
            return data;
        }
        #endregion

        #region 按加载文件的路径保存Json数据 + SaveFile()
        /// <summary>
        /// 按加载文件的路径保存Json数据
        /// </summary>
        /// <returns>是否成功</returns>
        public bool SaveFile()
        {
            if (!IS_FILE || FILE_PATH.IsNullOrEmpty()) throw new Exception("没有加载到Json文件，请用【SetValueSaveFile<T>(string source, string keys, T value, string path)】");
            try
            {
                File.WriteAllText(FILE_PATH, JSON_DATA);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
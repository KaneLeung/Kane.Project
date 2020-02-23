### 自定义JsonConverter的使用方法
```
    services.AddControllers().AddJsonOptions(options =>
    {
        //使用自定义时间格式转换器设置时间格式
        options.JsonSerializerOptions.Converters.Add(new JsonConverterEx.DateTimeConverter("yyyy-MM-dd HH:mm:ss"));
        //使用自定义Bool转换器设置bool获取格式
        options.JsonSerializerOptions.Converters.Add(new BoolJsonConverter());
        //若为null，则保持属性名称不变。
        //若为JsonNamingPolicy.CamelCase;则数据格式首字母小写
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        //获取或设置用于将 IDictionary 密钥名称转换为其他格式（如 camel 大小写）的策略。
        options.JsonSerializerOptions.DictionaryKeyPolicy = null;
        //获取或设置要在转义字符串时使用的编码器
        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        //取消Unicode编码，如果【中文被编码】，使用这个
        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
        //忽略空值
        options.JsonSerializerOptions.IgnoreNullValues = true;
        //允许额外符号
        options.JsonSerializerOptions.AllowTrailingCommas = true;
        //反序列化过程中属性名称是否忽略大小写
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        //“非严格JSON”模式,也可解决【中文被编码】的问题
        options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        //将枚举序列化为名称字符串而不是数值
        options.Converters.Add(new JsonStringEnumConverter());
    });
```

https://docs.microsoft.com/zh-cn/dotnet/standard/serialization/system-text-json-converters-how-to
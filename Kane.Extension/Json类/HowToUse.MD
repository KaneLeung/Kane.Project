### �Զ���JsonConverter��ʹ�÷���
```
    services.AddControllers().AddJsonOptions(options =>
    {
        //ʹ���Զ���ʱ���ʽת��������ʱ���ʽ
        options.JsonSerializerOptions.Converters.Add(new JsonConverterEx.DateTimeConverter("yyyy-MM-dd HH:mm:ss"));
        //ʹ���Զ���Boolת��������bool��ȡ��ʽ
        options.JsonSerializerOptions.Converters.Add(new BoolJsonConverter());
        //��Ϊnull���򱣳��������Ʋ��䡣
        //��ΪJsonNamingPolicy.CamelCase;�����ݸ�ʽ����ĸСд
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        //��ȡ���������ڽ� IDictionary ��Կ����ת��Ϊ������ʽ���� camel ��Сд���Ĳ��ԡ�
        options.JsonSerializerOptions.DictionaryKeyPolicy = null;
        //��ȡ������Ҫ��ת���ַ���ʱʹ�õı�����
        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        //ȡ��Unicode���룬��������ı����롿��ʹ�����
        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
        //���Կ�ֵ
        options.JsonSerializerOptions.IgnoreNullValues = true;
        //����������
        options.JsonSerializerOptions.AllowTrailingCommas = true;
        //�����л����������������Ƿ���Դ�Сд
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        //�����ϸ�JSON��ģʽ,Ҳ�ɽ�������ı����롿������
        options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        //��ö�����л�Ϊ�����ַ�����������ֵ
        options.Converters.Add(new JsonStringEnumConverter());
    });
```

https://docs.microsoft.com/zh-cn/dotnet/standard/serialization/system-text-json-converters-how-to
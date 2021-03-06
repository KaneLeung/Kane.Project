## 加密技术通常分为两大类:"对称式"和"非对称式"。

-   对称性加密算法：对称式加密就是加密和解密使用同一个密钥。信息接收双方都需事先知道密匙和加解密算法且其密匙是相同的，之后便是对数据进行加解密了。对称加密算法用来对敏感数据等信息进行加密。

-   非对称算法：非对称式加密就是加密和解密所使用的不是同一个密钥，通常有两个密钥，称为"公钥"和"私钥"，它们两个必需配对使用，否则不能打开加密文件。发送双方 A,B 事先均生成一堆密匙，然后 A 将自己的公有密匙发送给 B，B 将自己的公有密匙发送给 A，如果 A 要给 B 发送消 息，则先需要用 B 的公有密匙进行消息加密，然后发送给 B 端，此时 B 端再用自己的私有密匙进行消息解密，B 向 A 发送消息时为同样的道理。

-   散列算法：散列算法，又称哈希函数，是一种单向加密算法。在信息安全技术中，经常需要验证消息的完整性，散列(Hash)函数提供了这一服务，它对不同长度的输入消息，产生固定长度的输出。这个固定长度的输出称为原输入消息的"散列"或"消息摘要"(Message digest)。散列算法不算加密算法，因为其结果是不可逆的，既然是不可逆的，那么当然不是用来加密的，而是签名。

### 对称性加密算法有：AES、DES、3DES

#### 用途：对称加密算法用来对敏感数据等信息进行加密

-   DES（Data Encryption Standard）：数据加密标准，速度较快，适用于加密大量数据的场合。

-   3DES（Triple DES）：是基于 DES，对一块数据用三个不同的密钥进行三次加密，强度更高。

-   AES（Advanced Encryption Standard）：高级加密标准，是下一代的加密算法标准，速度快，安全级别高；AES 是一个使用 128 为分组块的分组加密算法，分组块和 128、192 或 256 位的密钥一起作为输入，对 4×4 的字节数组上进行操作。众所周之 AES 是种十分高效的算法，尤其在 8 位架构中，这源于它面向字节的设计。AES 适用于 8 位的小型单片机或者普通的 32 位微处理器, 并且适合用专门的硬件实现，硬件实现能够使其吞吐量（每秒可以到达的加密/解密 bit 数）达到十亿量级。同样，其也适用于 RFID 系统。

### 非对称性算法有：RSA、DSA、ECC

-   RSA：由 RSA 公司发明，是一个支持变长密钥的公共密钥算法，需要加密的文件块的长度也是可变的。RSA 在国外早已进入实用阶段，已研制出多种高速的 RSA 的专用芯片。

-   DSA（Digital Signature Algorithm）：数字签名算法，是一种标准的 DSS（数字签名标准），严格来说不算加密算法。

-   ECC（Elliptic Curves Cryptography）：椭圆曲线密码编码学。ECC 和 RSA 相比，具有多方面的绝对优势，主要有：抗攻击性强。相同的密钥长度，其抗攻击性要强很多倍。计算量小，处理速度快。ECC 总的速度比 RSA、DSA 要快得多。存储空间占用小。ECC 的密钥尺寸和系统参数与 RSA、DSA 相比要小得多，意味着它所占的存贮空间要小得多。这对于加密算法在 IC 卡上的应用具有特别重要的意义。带宽要求低。当对长消息进行加解密时，三类密码系统有相同的带宽要求，但应用于短消息时 ECC 带宽要求却低得多。带宽要求低使 ECC 在无线网络领域具有广泛的应用前景。

### 散列算法（签名算法）有：MD5、SHA1、HMAC

#### 用途：主要用于验证，防止信息被修。具体用途如：文件校验、数字签名、鉴权协议

-   MD5：MD5 是一种不可逆的加密算法，目前是最牢靠的加密算法之一，尚没有能够逆运算的程序被开发出来，它对应任何字符串都可以加密成一段唯一的固定长度的代码。

-   SHA1：是由 NISTNSA 设计为同 DSA 一起使用的，它对长度小于 264 的输入，产生长度为 160bit 的散列值，因此抗穷举(brute-force)性更好。SHA-1 设计时基于和 MD4 相同原理,并且模仿了该算法。SHA-1 是由美国标准技术局（NIST）颁布的国家标准，是一种应用最为广泛的 Hash 函数算法，也是目前最先进的加密技术，被政府部门和私营业主用来处理敏感的信息。而 SHA-1 基于 MD5，MD5 又基于 MD4。

-   HMAC：是密钥相关的哈希运算消息认证码（Hash-based Message Authentication Code）,HMAC 运算利用哈希算法，以一个密钥和一个消息为输入，生成一个消息摘要作为输出。也就是说 HMAC 是需要一个密钥的。所以，HMAC_SHA1 也是需要一个密钥的，而 SHA1 不需要。

### 其他常用算法：

-   Base64：其实不是安全领域下的加密解密算法，只能算是一个编码算法，通常用于把二进制数据编码为可写的字符形式的数据，对数据内容进行编码来适合传输(可以对 img 图像编码用于传输)。这是一种可逆的编码方式。编码后的数据是一个字符串，其中包含的字符为：A-Z、a-z、0-9、+、/，共 64 个字符(26 + 26 + 10 + 1 + 1 = 64，其实是 65 个字符，“=”是填充字符。Base64 要求把每三个 8Bit 的字节转换为四个 6Bit 的字节(3*8 = 4*6 = 24)，然后把 6Bit 再添两位高位 0，组成四个 8Bit 的字节，也就是说，转换后的字符串理论上将要比原来的长 1/3。原文的字节最后不够 3 个的地方用 0 来补足，转换时 Base64 编码用=号来代替。这就是为什么有些 Base64 编码会以一个或两个等号结束的原因，中间是不可能出现等号的，但等号最多只有两个。其实不用"="也不耽误解码，之所以用"="，可能是考虑到多段编码后的 Base64 字符串拼起来也不会引起混淆。)

    -   Base64 编码是从二进制到字符的过程，像一些中文字符用不同的编码转为二进制时，产生的二进制是不一样的，所以最终产生的 Base64 字符也不一样。例如"上网"对应 utf-8 格式的 Base64 编码是"5LiK572R"， 对应 GB2312 格式的 Base64 编码是"yc/N+A=="。

    -   标准的 Base64 并不适合直接放在 URL 里传输，因为 URL 编码器会把标准 Base64 中的“/”和“+”字符变为形如“%XX”的形式，而这些“%”号在存入数据库时还需要再进行转换，因为 ANSI SQL 中已将“%”号用作通配符。

    -   为解决此问题，可采用一种用于 URL 的改进 Base64 编码，它不在末尾填充'='号，并将标准 Base64 中的“+”和“/”分别改成了“-”和“\_”，这样就免去了在 URL 编解码和数据库存储时所要作的转换，避免了编码信息长度在此过程中的增加，并统一了数据库、表单等处对象标识符的格式。

    -   另有一种用于正则表达式的改进 Base64 变种，它将“+”和“/”改成了“!”和“-”，因为“+”，“\*”以及前面在 IRCu 中用到的“[”和“]”在正则表达式中都可能具有特殊含义。

    -   此外还有一些变种，它们将“+/”改为“_-”或“._”（用作编程语言中的标识符名称）或“.-”（用于 XML 中的 Nmtoken）甚至“\_:”（用于 XML 中的 Name）。

-   ​HTTPS（全称：Hypertext Transfer Protocol over Secure Socket Layer），是以安全为目标的 HTTP 通道，简单讲是 HTTP 的安全版。即 HTTP 下加入 SSL 层，HTTPS 的安全基础是 SSL(SSL 使用 40 位关键字作为 RC4 流加密算法，这对于商业信息的加密是合适的。)，因此加密的详细内容就需要 SSL。https:URL 表明它使用了 HTTP，但 HTTPS 存在不同于 HTTP 的默认端口及一个加密/身份验证层（在 HTTP 与 TCP 之间），提供了身份验证与加密通讯方法，现在它被广泛用于万维网上安全敏感的通讯，例如交易支付方面。它的主要作用可以分为两种：一种是建立一个信息安全通道，来保证数据传输的安全；另一种就是确认网站的真实性。

    -   项目应用总结：

    1. 加密算法是可逆的，用来对敏感数据进行保护。散列算法(签名算法、哈希算法)是不可逆的，主要用于身份验证。
    2. 对称加密算法使用同一个密匙加密和解密，速度快，适合给大量数据加密。对称加密客户端和服务端使用同一个密匙，存在被抓包破解的风险。
    3. 非对称加密算法使用公钥加密，私钥解密，私钥签名，公钥验签。安全性比对称加密高，但速度较慢。非对称加密使用两个密匙，服务端和客户端密匙不一样，私钥放在服务端，黑客一般是拿不到的，安全性高。
    4. Base64 不是安全领域下的加解密算法，只是一个编码算法，通常用于把二进制数据编码为可写的字符形式的数据，特别适合在 http，mime 协议下的网络快速传输数据。UTF-8 和 GBK 中文的 Base64 编码结果是不同的。采用 Base64 编码不仅比较简短，同时也具有不可读性，即所编码的数据不会被人用肉眼所直接看到，但这种方式很初级，很简单。Base64 可以对图片文件进行编码传输。
    5. https 协议广泛用于万维网上安全敏感的通讯，例如交易支付方面。它的主要作用可以分为两种：一种是建立一个信息安全通道，来保证数据传输的安全；另一种就是确认网站的真实性。
    6. 大量数据加密建议采用对称加密算法，提高加解密速度；小量的机密数据，可以采用非对称加密算法。在实际的操作过程中，我们通常采用的方式是：采用非对称加密算法管理对称算法的密钥，然后用对称加密算法加密数据，这样我们就集成了两类加密算法的优点，既实现了加密速度快的优点，又实现了安全方便管理密钥的优点。
    7. MD5 标准密钥长度 128 位（128 位是指二进制位。二进制太长，所以一般都改写成 16 进制，每一位 16 进制数可以代替 4 位二进制数，所以 128 位二进制数写成 16 进制就变成了 128/4=32 位。16 位加密就是从 32 位 MD5 散列中把中间 16 位提取出来）；sha1 标准密钥长度 160 位(比 MD5 摘要长 32 位)，Base64 转换后的字符串理论上将要比原来的长 1/3。

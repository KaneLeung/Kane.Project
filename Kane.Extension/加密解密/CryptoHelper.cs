#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：CryptoHelper
* 类 描 述 ：常用加解密类扩展
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/16 23:12:20
* 更新时间 ：2020/02/26 18:12:20
* 版 本 号 ：v1.0.1.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Kane.Extension
{
    #region 加密算法(DES, AES, RSA, MD5, SHA1, Base64)比较和项目应用
    //加密技术通常分为两大类:"对称式"和"非对称式"。

    //对称性加密算法：对称式加密就是加密和解密使用同一个密钥。信息接收双方都需事先知道密匙和加解密算法且其密匙是相同的，之后便是对数据进行加解密了。对称加密算法用来对敏感数据等信息进行加密。

    //非对称算法：非对称式加密就是加密和解密所使用的不是同一个密钥，通常有两个密钥，称为"公钥"和"私钥"，它们两个必需配对使用，否则不能打开加密文件。发送双方A,B事先均生成一堆密匙，然后A将自己的公有密匙发送给B，B将自己的公有密匙发送给A，如果A要给B发送消 息，则先需要用B的公有密匙进行消息加密，然后发送给B端，此时B端再用自己的私有密匙进行消息解密，B向A发送消息时为同样的道理。

    //散列算法：散列算法，又称哈希函数，是一种单向加密算法。在信息安全技术中，经常需要验证消息的完整性，散列(Hash)函数提供了这一服务，它对不同长度的输入消息，产生固定长度的输出。这个固定长度的输出称为原输入消息的"散列"或"消息摘要"(Message digest)。散列算法不算加密算法，因为其结果是不可逆的，既然是不可逆的，那么当然不是用来加密的，而是签名。

    //对称性加密算法有：AES、DES、3DES
    //用途：对称加密算法用来对敏感数据等信息进行加密

    //DES（Data Encryption Standard）：数据加密标准，速度较快，适用于加密大量数据的场合。

    //3DES（Triple DES）：是基于DES，对一块数据用三个不同的密钥进行三次加密，强度更高。

    //AES（Advanced Encryption Standard）：高级加密标准，是下一代的加密算法标准，速度快，安全级别高；AES是一个使用128为分组块的分组加密算法，分组块和128、192或256位的密钥一起作为输入，对4×4的字节数组上进行操作。众所周之AES是种十分高效的算法，尤其在8位架构中，这源于它面向字节的设计。AES 适用于8位的小型单片机或者普通的32位微处理器, 并且适合用专门的硬件实现，硬件实现能够使其吞吐量（每秒可以到达的加密/解密bit数）达到十亿量级。同样，其也适用于RFID系统。

    //非对称性算法有：RSA、DSA、ECC

    //RSA：由 RSA 公司发明，是一个支持变长密钥的公共密钥算法，需要加密的文件块的长度也是可变的。RSA在国外早已进入实用阶段，已研制出多种高速的RSA的专用芯片。

    //DSA（Digital Signature Algorithm）：数字签名算法，是一种标准的 DSS（数字签名标准），严格来说不算加密算法。

    //ECC（Elliptic Curves Cryptography）：椭圆曲线密码编码学。ECC和RSA相比，具有多方面的绝对优势，主要有：抗攻击性强。相同的密钥长度，其抗攻击性要强很多倍。计算量小，处理速度快。ECC总的速度比RSA、DSA要快得多。存储空间占用小。ECC的密钥尺寸和系统参数与RSA、DSA相比要小得多，意味着它所占的存贮空间要小得多。这对于加密算法在IC卡上的应用具有特别重要的意义。带宽要求低。当对长消息进行加解密时，三类密码系统有相同的带宽要求，但应用于短消息时ECC带宽要求却低得多。带宽要求低使ECC在无线网络领域具有广泛的应用前景。

    //散列算法（签名算法）有：MD5、SHA1、HMAC
    //用途：主要用于验证，防止信息被修。具体用途如：文件校验、数字签名、鉴权协议

    //MD5：MD5是一种不可逆的加密算法，目前是最牢靠的加密算法之一，尚没有能够逆运算的程序被开发出来，它对应任何字符串都可以加密成一段唯一的固定长度的代码。

    //SHA1：是由NISTNSA设计为同DSA一起使用的，它对长度小于264的输入，产生长度为160bit的散列值，因此抗穷举(brute-force)性更好。SHA-1设计时基于和MD4相同原理,并且模仿了该算法。SHA-1是由美国标准技术局（NIST）颁布的国家标准，是一种应用最为广泛的Hash函数算法，也是目前最先进的加密技术，被政府部门和私营业主用来处理敏感的信息。而SHA-1基于MD5，MD5又基于MD4。

    //HMAC：是密钥相关的哈希运算消息认证码（Hash-based Message Authentication Code）,HMAC运算利用哈希算法，以一个密钥和一个消息为输入，生成一个消息摘要作为输出。也就是说HMAC是需要一个密钥的。所以，HMAC_SHA1也是需要一个密钥的，而SHA1不需要。

    //其他常用算法：

    //Base64：其实不是安全领域下的加密解密算法，只能算是一个编码算法，通常用于把二进制数据编码为可写的字符形式的数据，对数据内容进行编码来适合传输(可以对img图像编码用于传输)。这是一种可逆的编码方式。编码后的数据是一个字符串，其中包含的字符为：A-Z、a-z、0-9、+、/，共64个字符(26 + 26 + 10 + 1 + 1 = 64，其实是65个字符，“=”是填充字符。Base64要求把每三个8Bit的字节转换为四个6Bit的字节(3*8 = 4*6 = 24)，然后把6Bit再添两位高位0，组成四个8Bit的字节，也就是说，转换后的字符串理论上将要比原来的长1/3。原文的字节最后不够3个的地方用0来补足，转换时Base64编码用=号来代替。这就是为什么有些Base64编码会以一个或两个等号结束的原因，中间是不可能出现等号的，但等号最多只有两个。其实不用"="也不耽误解码，之所以用"="，可能是考虑到多段编码后的Base64字符串拼起来也不会引起混淆。)
    //Base64编码是从二进制到字符的过程，像一些中文字符用不同的编码转为二进制时，产生的二进制是不一样的，所以最终产生的Base64字符也不一样。例如"上网"对应utf-8格式的Base64编码是"5LiK572R"， 对应GB2312格式的Base64编码是"yc/N+A=="。
    //标准的Base64并不适合直接放在URL里传输，因为URL编码器会把标准Base64中的“/”和“+”字符变为形如“%XX”的形式，而这些“%”号在存入数据库时还需要再进行转换，因为ANSI SQL中已将“%”号用作通配符。
    //为解决此问题，可采用一种用于URL的改进Base64编码，它不在末尾填充'='号，并将标准Base64中的“+”和“/”分别改成了“-”和“_”，这样就免去了在URL编解码和数据库存储时所要作的转换，避免了编码信息长度在此过程中的增加，并统一了数据库、表单等处对象标识符的格式。
    //另有一种用于正则表达式的改进Base64变种，它将“+”和“/”改成了“!”和“-”，因为“+”，“*”以及前面在IRCu中用到的“[”和“]”在正则表达式中都可能具有特殊含义。
    //此外还有一些变种，它们将“+/”改为“_-”或“._”（用作编程语言中的标识符名称）或“.-”（用于XML中的Nmtoken）甚至“_:”（用于XML中的Name）。

    //​HTTPS（全称：Hypertext Transfer Protocol over Secure Socket Layer），是以安全为目标的HTTP通道，简单讲是HTTP的安全版。即HTTP下加入SSL层，HTTPS的安全基础是SSL(SSL使用40 位关键字作为RC4流加密算法，这对于商业信息的加密是合适的。)，因此加密的详细内容就需要SSL。https:URL表明它使用了HTTP，但HTTPS存在不同于HTTP的默认端口及一个加密/身份验证层（在HTTP与TCP之间），提供了身份验证与加密通讯方法，现在它被广泛用于万维网上安全敏感的通讯，例如交易支付方面。它的主要作用可以分为两种：一种是建立一个信息安全通道，来保证数据传输的安全；另一种就是确认网站的真实性。

    //项目应用总结：
    //1. 加密算法是可逆的，用来对敏感数据进行保护。散列算法(签名算法、哈希算法)是不可逆的，主要用于身份验证。
    //2. 对称加密算法使用同一个密匙加密和解密，速度快，适合给大量数据加密。对称加密客户端和服务端使用同一个密匙，存在被抓包破解的风险。
    //3. 非对称加密算法使用公钥加密，私钥解密，私钥签名，公钥验签。安全性比对称加密高，但速度较慢。非对称加密使用两个密匙，服务端和客户端密匙不一样，私钥放在服务端，黑客一般是拿不到的，安全性高。
    //4. Base64不是安全领域下的加解密算法，只是一个编码算法，通常用于把二进制数据编码为可写的字符形式的数据，特别适合在http，mime协议下的网络快速传输数据。UTF-8和GBK中文的Base64编码结果是不同的。采用Base64编码不仅比较简短，同时也具有不可读性，即所编码的数据不会被人用肉眼所直接看到，但这种方式很初级，很简单。Base64可以对图片文件进行编码传输。
    //5. https协议广泛用于万维网上安全敏感的通讯，例如交易支付方面。它的主要作用可以分为两种：一种是建立一个信息安全通道，来保证数据传输的安全；另一种就是确认网站的真实性。
    //6. 大量数据加密建议采用对称加密算法，提高加解密速度；小量的机密数据，可以采用非对称加密算法。在实际的操作过程中，我们通常采用的方式是：采用非对称加密算法管理对称算法的密钥，然后用对称加密算法加密数据，这样我们就集成了两类加密算法的优点，既实现了加密速度快的优点，又实现了安全方便管理密钥的优点。
    //7. MD5标准密钥长度128位（128位是指二进制位。二进制太长，所以一般都改写成16进制，每一位16进制数可以代替4位二进制数，所以128位二进制数写成16进制就变成了128/4=32位。16位加密就是从32位MD5散列中把中间16位提取出来）；sha1标准密钥长度160位(比MD5摘要长32位)，Base64转换后的字符串理论上将要比原来的长1/3。 
    #endregion

    /// <summary>
    /// 常用加解密类扩展
    /// </summary>
    public class CryptoHelper
    {
        #region 32位MD5加密,【全小写】+ Md5(string value)
        /// <summary>
        /// 32位MD5加密,【全小写】
        /// </summary>
        /// <param name="value">要哈希化的数据</param>
        /// <returns></returns>
        public string Md5(string value)
        {
            using MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = md5.ComputeHash(value.ToBytes());
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sb.Append(Convert.ToString(data[i], 16).PadLeft(2, '0'));
            return sb.ToString().PadLeft(32, '0');
        }
        #endregion

        #region HMAC-MD5加密，【全大写】 + HmacMd5(string value, string key)
        /// <summary>
        /// HMAC-MD5加密，【全大写】
        /// HMAC-MD5就可以用一把发送方和接收方都有的key进行计算，而没有这把key的第三方是无法计算出正确的散列值的，这样就可以防止数据被篡改
        /// </summary>
        /// <param name="value">要哈希化的数据</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public string HmacMd5(string value, string key)
        {
            using HMACMD5 md5 = new HMACMD5(key.ToBytes());
            byte[] byteOut = md5.ComputeHash(value.ToBytes());
            string result = BitConverter.ToString(byteOut);
            return result.Replace("-", "");
        }
        #endregion

        #region SHA1加密，【全大写】 + Sha1(string value)
        /// <summary>
        /// SHA1加密，【全大写】
        /// </summary>
        /// <param name="value">要哈希化的数据</param>
        /// <returns></returns>
        public string Sha1(string value)
        {
            using SHA1 sha1 = SHA1.Create();
            byte[] byteOut = sha1.ComputeHash(value.ToBytes());
            string result = BitConverter.ToString(byteOut);
            return result.Replace("-", "");
        }
        #endregion

        #region SHA256加密，【全大写】 + Sha256(string value)
        /// <summary>
        /// SHA256加密，【全大写】
        /// </summary>
        /// <param name="value">要哈希化的数据</param>
        /// <returns></returns>
        public string Sha256(string value)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] byteOut = sha256.ComputeHash(value.ToBytes());
            string result = BitConverter.ToString(byteOut);
            return result.Replace("-", "");
        }
        #endregion

        #region SHA384加密，【全大写】 + Sha384(string value)
        /// <summary>
        /// SHA384加密，【全大写】
        /// </summary>
        /// <param name="value">要哈希化的数据</param>
        /// <returns></returns>
        public string Sha384(string value)
        {
            using SHA384 sha384 = SHA384.Create();
            byte[] byteOut = sha384.ComputeHash(value.ToBytes());
            string result = BitConverter.ToString(byteOut);
            return result.Replace("-", "");
        }
        #endregion

        #region SHA512加密，【全大写】 + Sha512(string value)
        /// <summary>
        /// SHA512加密，【全大写】
        /// </summary>
        /// <param name="value">要哈希化的数据</param>
        /// <returns></returns>
        public string Sha512(string value)
        {
            using SHA512 sha512 = SHA512.Create();
            byte[] byteOut = sha512.ComputeHash(value.ToBytes());
            string result = BitConverter.ToString(byteOut);
            return result.Replace("-", "");
        }
        #endregion

        #region HMACSHA1加密，【全大写】 + HMACSHA1(string value, string key)
        /// <summary>
        /// HMACSHA1加密，【全大写】
        /// </summary>
        /// <param name="value">要哈希化的数据</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public string HmacSha1(string value, string key)
        {
            using HMACSHA1 hmac = new HMACSHA1(key.ToBytes());
            byte[] byteOut = hmac.ComputeHash(value.ToBytes());
            string result = BitConverter.ToString(byteOut);
            return result.Replace("-", "");
        }
        #endregion

        #region HMACSHA256加密，【全大写】 + HMACSHA256(string value, string key)
        /// <summary>
        /// HMACSHA256加密，【全大写】
        /// </summary>
        /// <param name="value">要哈希化的数据</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public string HmacSha256(string value, string key)
        {
            using HMACSHA256 hmac = new HMACSHA256(key.ToBytes());
            byte[] byteOut = hmac.ComputeHash(value.ToBytes());
            string result = BitConverter.ToString(byteOut);
            return result.Replace("-", "");
        }
        #endregion

        #region HMACSHA384加密，【全大写】 + HMACSHA384(string value, string key)
        /// <summary>
        /// HMACSHA384加密，【全大写】
        /// ###和在线加密的结果不一致###
        /// </summary>
        /// <param name="value">要哈希化的数据</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public string HmacSha384(string value, string key)
        {
            using HMACSHA384 hmac = new HMACSHA384(key.ToBytes());
            byte[] byteOut = hmac.ComputeHash(value.ToBytes());
            string result = BitConverter.ToString(byteOut);
            return result.Replace("-", "");
        }
        #endregion

        #region HMACSHA512加密，【全大写】 + HMACSHA512(string value, string key)
        /// <summary>
        /// HMACSHA512加密，【全大写】
        /// </summary>
        /// <param name="value">要哈希化的数据</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public string HmacSha512(string value, string key)
        {
            using HMACSHA512 hmac = new HMACSHA512(key.ToBytes());
            byte[] byteOut = hmac.ComputeHash(value.ToBytes());
            string result = BitConverter.ToString(byteOut);
            return result.Replace("-", "");
        }
        #endregion

        #region AES对称性加密算法 加密与解密
        // AES加密
        // AES（Advanced Encryption Standard）：对称性加密算法，高级加密标准，是下一代的加密算法标准，速度快，安全级别高；
        // AES是一个使用128为分组块的分组加密算法，分组块和128、192或256位的密钥一起作为输入，对4×4的字节数组上进行操作。
        // 众所周之AES是种十分高效的算法，尤其在8位架构中，这源于它面向字节的设计。AES 适用于8位的小型单片机或者普通的32位微处理器,
        // 并且适合用专门的硬件实现，硬件实现能够使其吞吐量（每秒可以到达的加密/解密bit数）达到十亿量级。同样，其也适用于RFID系统。

        #region AES加密，32位密钥，16位VI向量 + AesEncrypt(string value, string key, string vi)
        /// <summary>
        /// AES加密，32位密钥，16位VI向量
        /// </summary>
        /// <param name="value">要加密的数据</param>
        /// <param name="key">密钥，最大32Bit,可由RandCode(32)生成</param>
        /// <param name="vi">向量，最大16Bit,可由RandCode(16)生成</param>
        /// <returns></returns>
        public string AesEncrypt(string value, string key, string vi)
        {
            byte[] byteValue = value.ToBytes();
            byte[] byteKey = new byte[32];
            Array.Copy(key.PadRight(byteKey.Length).ToBytes(), byteKey, byteKey.Length);
            byte[] byteVI = new byte[16];
            Array.Copy(vi.PadRight(byteVI.Length).ToBytes(), byteVI, byteVI.Length);
            using Aes Aes = Aes.Create();
            try
            {
                using MemoryStream Memory = new MemoryStream();
                using CryptoStream Encryptor = new CryptoStream(Memory, Aes.CreateEncryptor(byteKey, byteVI), CryptoStreamMode.Write);
                Encryptor.Write(byteValue, 0, byteValue.Length);
                Encryptor.FlushFinalBlock();
                return Memory.ToArray().ToBase64();
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

        #region AES解密，32位密钥，16位VI向量 + AesDecrypt(string value, string key, string vi)
        /// <summary>
        /// AES解密，32位密钥，16位VI向量
        /// </summary>
        /// <param name="value">要解密的数据</param>
        /// <param name="key">密钥，最大32Bit,可由RandCode(32)生成</param>
        /// <param name="vi">向量，最大16Bit,可由RandCode(16)生成</param>
        /// <returns></returns>
        public string AesDecrypt(string value, string key, string vi)
        {
            byte[] byteKey = new byte[32];
            Array.Copy(key.PadRight(byteKey.Length).ToBytes(), byteKey, byteKey.Length);
            byte[] byteVI = new byte[16];
            Array.Copy(vi.PadRight(byteVI.Length).ToBytes(), byteVI, byteVI.Length);
            using Aes Aes = Aes.Create();
            try
            {
                using MemoryStream Memory = new MemoryStream(value.Base64ToBytes());
                using CryptoStream Decryptor = new CryptoStream(Memory, Aes.CreateDecryptor(byteKey, byteVI), CryptoStreamMode.Read);
                using MemoryStream tempMemory = new MemoryStream();
                byte[] Buffer = new byte[1024];
                int readBytes = 0;
                while ((readBytes = Decryptor.Read(Buffer, 0, Buffer.Length)) > 0)
                {
                    tempMemory.Write(Buffer, 0, readBytes);
                }
                return tempMemory.ToArray().ByteToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

        #region AES加密，32位密钥，无VI向量 + AesEncrypt(string value, string key)
        /// <summary>
        /// AES加密，32位密钥，无VI向量
        /// </summary>
        /// <param name="value">要加密的数据</param>
        /// <param name="key">密钥，最大32Bit,可由RandCode(32)生成</param>
        /// <returns></returns>
        public string AesEncrypt(string value, string key)
        {
            byte[] byteValue = value.ToBytes();
            byte[] byteKey = new byte[32];
            Array.Copy(key.PadRight(byteKey.Length).ToBytes(), byteKey, byteKey.Length);
            using MemoryStream Memory = new MemoryStream();
            using Aes aes = Aes.Create();
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            aes.Key = byteKey;
            using CryptoStream cryptoStream = new CryptoStream(Memory, aes.CreateEncryptor(), CryptoStreamMode.Write);
            try
            {
                cryptoStream.Write(byteValue, 0, byteValue.Length);
                cryptoStream.FlushFinalBlock();
                return Memory.ToArray().ToBase64();
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

        #region AES解密，32位密钥，无VI向量 + AesDecrypt(string value, string key)
        /// <summary>
        /// AES解密，32位密钥，无VI向量
        /// </summary>
        /// <param name="value">要解密的数据</param>
        /// <param name="key">密钥，最大32Bit,可由RandCode(32)生成</param>
        /// <returns></returns>
        public string AesDecrypt(string value, string key)
        {
            byte[] byteValue = Convert.FromBase64String(value);
            byte[] byteKey = new byte[32];
            Array.Copy(key.PadRight(byteKey.Length).ToBytes(), byteKey, byteKey.Length);
            using MemoryStream Memory = new MemoryStream(byteValue);
            using Aes aes = Aes.Create();
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            aes.Key = byteKey;
            using CryptoStream cryptoStream = new CryptoStream(Memory, aes.CreateDecryptor(), CryptoStreamMode.Read);
            try
            {
                byte[] tmp = new byte[byteValue.Length];
                int len = cryptoStream.Read(tmp, 0, byteValue.Length);
                var byteResult = new byte[len];
                Array.Copy(tmp, 0, byteResult, 0, len);
                return byteResult.ByteToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion
        #endregion

        #region DES 对称性加密算法 加密 + DesEncrypt(string value, string key)
        /// <summary>
        /// DES 对称性加密算法 加密
        /// DES（Data Encryption Standard）：数据加密标准，速度较快，适用于加密大量数据的场合。
        /// </summary>
        /// <param name="value">要加密的数据</param>
        /// <param name="key">密钥，最大24Bit,可由RandCode(24)生成</param>
        /// <returns></returns>
        public string DesEncrypt(string value, string key)
        {
            byte[] byteValue = value.ToBytes();
            byte[] byteKey = new byte[24];
            using MemoryStream Memory = new MemoryStream();
            using TripleDES des = TripleDES.Create();
            Array.Copy(key.PadRight(byteKey.Length).ToBytes(), byteKey, byteKey.Length);
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.PKCS7;
            des.Key = byteKey;
            using CryptoStream cryptoStream = new CryptoStream(Memory, des.CreateEncryptor(), CryptoStreamMode.Write);
            try
            {
                cryptoStream.Write(byteValue, 0, byteValue.Length);
                cryptoStream.FlushFinalBlock();
                return Memory.ToArray().ToBase64();
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

        #region DES 对称性加密算法 解密 + DesDecrypt(string value, string key)
        /// <summary>
        /// DES 对称性加密算法 解密
        /// </summary>
        /// <param name="value">要解密的数据</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public string DesDecrypt(string value, string key)
        {
            byte[] byteValue = Convert.FromBase64String(value);
            byte[] byteKey = new byte[24];
            Array.Copy(key.PadRight(byteKey.Length).ToBytes(), byteKey, byteKey.Length);
            using MemoryStream Memory = new MemoryStream(byteValue);
            using TripleDES des = TripleDES.Create();
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.PKCS7;
            des.Key = byteKey;
            using CryptoStream cryptoStream = new CryptoStream(Memory, des.CreateDecryptor(), CryptoStreamMode.Read);
            try
            {
                byte[] tmp = new byte[byteValue.Length];
                int len = cryptoStream.Read(tmp, 0, byteValue.Length);
                var byteResult = new byte[len];
                Array.Copy(tmp, 0, byteResult, 0, len);
                return byteResult.ByteToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
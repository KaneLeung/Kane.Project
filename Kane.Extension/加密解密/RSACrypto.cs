#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：RSACrypto
* 类 描 述 ：RSA加解密类扩展
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/16 23:14:58
* 更新时间 ：2019/10/16 23:14:58
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
#if (NETCOREAPP3_0 || NETCOREAPP3_1)
using System.Text.Json;
#else
using Newtonsoft.Json;
#endif

namespace Kane.Extension
{
    /// <summary>
    /// RSA加解密类扩展
    /// </summary>
    public static class RSACrypto
    {
        #region RSA 用到的实体类和枚举类
        #region Rsa Key Size 枚举
        /// <summary>
        /// Rsa Key Size 枚举
        /// </summary>
        public enum RsaSize
        {
            R2048 = 2048,
            R3072 = 3072,
            R4096 = 4096
        }
        #endregion

        #region RSA Key 实体
        /// <summary>
        /// RSA Key 实体
        /// </summary>
        public class RSAKey
        {
            /// <summary>
            /// Rsa public key
            /// </summary>
            public string PublicKey { get; set; }
            /// <summary>
            /// Rsa private key
            /// </summary>
            public string PrivateKey { get; set; }
            /// <summary>
            /// Rsa public key Exponent
            /// </summary>
            public string Exponent { get; set; }
            /// <summary>
            /// Rsa public key Modulus
            /// </summary>
            public string Modulus { get; set; }
        }
        #endregion

        #region RSAParameters的字符串实体
        /// <summary>
        /// RSAParameters的字符串实体
        /// </summary>
        internal class RSAParametersJson
        {
            //Public key Modulus
            public string Modulus { get; set; }
            //Public key Exponent
            public string Exponent { get; set; }
            public string P { get; set; }
            public string Q { get; set; }
            public string DP { get; set; }
            public string DQ { get; set; }
            public string InverseQ { get; set; }
            public string D { get; set; }
        }
        #endregion
        #endregion

        #region 创建RSA密钥对，包含公钥和私钥，默认键的大小为2048 + CreateRsaKey(RsaSize rsaSize = RsaSize.R2048)
        /// <summary>
        /// 创建RSA密钥对，包含公钥和私钥，默认键的大小为2048
        /// </summary>
        /// <param name="rsaSize"></param>
        /// <returns></returns>
        public static RSAKey CreateRsaKey(RsaSize rsaSize = RsaSize.R2048)
        {
            using RSA rsa = RSA.Create();
            rsa.KeySize = (int)rsaSize;
            string publicKey = rsa.ToJsonString(false);
            string privateKey = rsa.ToJsonString(true);
            return new RSAKey()
            {
                PublicKey = publicKey,
                PrivateKey = privateKey,
                Exponent = rsa.ExportParameters(false).Exponent.ByteToHex(),
                Modulus = rsa.ExportParameters(false).Modulus.ByteToHex()
            };
        }
        #endregion

#if !NET40 && !NET45
        #region RSA非对称加密，默认使用SHA512哈希算法加密填充
        /// <summary>
        /// RSA非对称加密，默认使用SHA512哈希算法加密填充
        /// </summary>
        /// <param name="value">要加密的数据</param>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public static string RSAEncrypt(string value, string publicKey) => RSAEncrypt(value, publicKey, RSAEncryptionPadding.OaepSHA512);
        #endregion

        #region RSA非对称加密
        /// <summary>
        /// RSA非对称加密
        /// </summary>
        /// <param name="value">要加密的数据</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="padding">加密填充</param>
        /// <returns></returns>
        public static string RSAEncrypt(string value, string publicKey, RSAEncryptionPadding padding)
        {
            using RSA rsa = RSA.Create();
            rsa.FromJsonString(publicKey);
            var maxLength = GetMaxRsaEncryptLength(rsa, padding);
            var rawBytes = Encoding.UTF8.GetBytes(value);
            if (rawBytes.Length > maxLength)
                throw new Exception($"'{value}' is out of max length({maxLength}).");
            byte[] encryptBytes = rsa.Encrypt(rawBytes, padding);
            return encryptBytes.ByteToHex();
        }
        #endregion
#else
        #region RSA非对称加密，Net45或以下没有使用填充
        /// <summary>
        /// RSA非对称加密，Net45或以下没有使用填充
        /// </summary>
        /// <param name="value">要加密的数据</param>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public static string RSAEncrypt(string value, string publicKey)
        {
            using RSA rsa = RSA.Create();
            rsa.FromJsonString(publicKey);
            var rawBytes = Encoding.UTF8.GetBytes(value);
            byte[] encryptBytes = rsa.EncryptValue(rawBytes);
            return encryptBytes.ByteToHex();
        }
        #endregion
#endif

#if !NET40 && !NET45
        #region RSA解密，默认使用SHA512哈希算法加密填充
        /// <summary>
        /// RSA解密，默认使用SHA512哈希算法加密填充
        /// </summary>
        /// <param name="value">要解密的数据，十六进制字符串</param>
        /// <param name="privateKey">私钥</param>
        /// <returns></returns>
        public static string RSADecrypt(string value, string privateKey) => RSADecrypt(value, privateKey, RSAEncryptionPadding.OaepSHA512);
        #endregion

        #region RSA非对称解密
        /// <summary>
        /// RSA非对称解密
        /// </summary>
        /// <param name="value">要解密的数据，十六进制字符串</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="padding">加密填充</param>
        /// <returns></returns>
        public static string RSADecrypt(string value, string privateKey, RSAEncryptionPadding padding)
        {
            using RSA rsa = RSA.Create();
            rsa.FromJsonString(privateKey);
            byte[] valueBytes = StringHelper.HexToByte(value);
            byte[] outBytes = rsa.Decrypt(valueBytes, padding);
            return Encoding.UTF8.GetString(outBytes);
        }
        #endregion
#else
        #region RSA解密，Net45或以下没有使用填充
        /// <summary>
        /// RSA解密，Net45或以下没有使用填充
        /// </summary>
        /// <param name="value">要解密的数据，十六进制字符串</param>
        /// <param name="privateKey">私钥</param>
        /// <returns></returns>
        public static string RSADecrypt(string value, string privateKey)
        {
            using RSA rsa = RSA.Create();
            rsa.FromJsonString(privateKey);
            byte[] valueBytes = StringHelper.HexToByte(value);
            byte[] outBytes = rsa.DecryptValue(valueBytes);
            return Encoding.UTF8.GetString(outBytes);
        }
        #endregion
#endif

#if !NET40 && !NET45
        #region 计算加密码最大长度
        private static int GetMaxRsaEncryptLength(RSA rsa, RSAEncryptionPadding padding)
        {
            var offset = 0;
            if (padding.Mode == RSAEncryptionPaddingMode.Pkcs1) offset = 11;
            else
            {
                if (padding.Equals(RSAEncryptionPadding.OaepSHA1)) offset = 42;
                if (padding.Equals(RSAEncryptionPadding.OaepSHA256)) offset = 66;
                if (padding.Equals(RSAEncryptionPadding.OaepSHA384)) offset = 98;
                if (padding.Equals(RSAEncryptionPadding.OaepSHA512)) offset = 130;
            }
            var keySize = rsa.KeySize;
            var maxLength = keySize / 8 - offset;
            return maxLength;
        }
        #endregion
#endif

        #region RSA扩展方法，将Json字符串转成RSA参数
        /// <summary>
        /// RSA扩展方法，将Json字符串转成RSA参数
        /// </summary>
        /// <param name="rsa"></param>
        /// <param name="jsonString"></param>
        internal static void FromJsonString(this RSA rsa, string jsonString)
        {
            RSAParameters parameters = new RSAParameters();
            try
            {
#if (NETCOREAPP3_0 || NETCOREAPP3_1)
                var paramsJson = JsonSerializer.Deserialize<RSAParametersJson>(jsonString);
#else
                var paramsJson = JsonConvert.DeserializeObject<RSAParametersJson>(jsonString);
#endif
                parameters.Modulus = paramsJson.Modulus.IsNotNull() ? Convert.FromBase64String(paramsJson.Modulus) : null;
                parameters.Exponent = paramsJson.Exponent.IsNotNull() ? Convert.FromBase64String(paramsJson.Exponent) : null;
                parameters.P = paramsJson.P.IsNotNull() ? Convert.FromBase64String(paramsJson.P) : null;
                parameters.Q = paramsJson.Q.IsNotNull() ? Convert.FromBase64String(paramsJson.Q) : null;
                parameters.DP = paramsJson.DP.IsNotNull() ? Convert.FromBase64String(paramsJson.DP) : null;
                parameters.DQ = paramsJson.DQ.IsNotNull() ? Convert.FromBase64String(paramsJson.DQ) : null;
                parameters.InverseQ = paramsJson.InverseQ.IsNotNull() ? Convert.FromBase64String(paramsJson.InverseQ) : null;
                parameters.D = paramsJson.D.IsNotNull() ? Convert.FromBase64String(paramsJson.D) : null;
            }
            catch
            {
                throw new Exception("Invalid Json RSA key.");
            }
            rsa.ImportParameters(parameters);
        }
        #endregion

        #region RSA扩展方法 Key序列化Json + ToJsonString(this RSA rsa, bool includePrivateParameters)
        /// <summary>
        /// RSA Key序列化Json
        /// </summary>
        /// <param name="rsa">RSA实例<see cref="RSA"/></param>
        /// <param name="includePrivateParameters">是否包含私钥</param>
        /// <returns></returns>
        internal static string ToJsonString(this RSA rsa, bool includePrivateParameters)
        {
            RSAParameters parameters = rsa.ExportParameters(includePrivateParameters);
            var parasJson = new RSAParametersJson()
            {
                Modulus = parameters.Modulus.IsNotNull() ? Convert.ToBase64String(parameters.Modulus) : null,
                Exponent = parameters.Exponent.IsNotNull() ? Convert.ToBase64String(parameters.Exponent) : null,
                P = parameters.P.IsNotNull() ? Convert.ToBase64String(parameters.P) : null,
                Q = parameters.Q.IsNotNull() ? Convert.ToBase64String(parameters.Q) : null,
                DP = parameters.DP.IsNotNull() ? Convert.ToBase64String(parameters.DP) : null,
                DQ = parameters.DQ.IsNotNull() ? Convert.ToBase64String(parameters.DQ) : null,
                InverseQ = parameters.InverseQ.IsNotNull() ? Convert.ToBase64String(parameters.InverseQ) : null,
                D = parameters.D.IsNotNull() ? Convert.ToBase64String(parameters.D) : null
            };
#if (NETCOREAPP3_0 || NETCOREAPP3_1)
            return JsonSerializer.Serialize(parasJson);
#else
            return JsonConvert.SerializeObject(parasJson);
#endif

        }
        #endregion

        #region RSA扩展方法 Key序列化XML字符串 + ToXmlString(this RSA rsa, bool includePrivateParameters)
        /// <summary>
        /// RSA扩展方法 Key序列化XML字符串
        /// </summary>
        /// <param name="rsa">RSA实例<see cref="RSA"/></param>
        /// <param name="includePrivateParameters">是否包含私钥</param>
        /// <returns></returns>
        public static string ToXmlString(this RSA rsa, bool includePrivateParameters)
        {
            RSAParameters parameters = rsa.ExportParameters(includePrivateParameters);
            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
                  parameters.Modulus.IsNotNull() ? Convert.ToBase64String(parameters.Modulus) : null,
                  parameters.Exponent.IsNotNull() ? Convert.ToBase64String(parameters.Exponent) : null,
                  parameters.P.IsNotNull() ? Convert.ToBase64String(parameters.P) : null,
                  parameters.Q.IsNotNull() ? Convert.ToBase64String(parameters.Q) : null,
                  parameters.DP.IsNotNull() ? Convert.ToBase64String(parameters.DP) : null,
                  parameters.DQ.IsNotNull() ? Convert.ToBase64String(parameters.DQ) : null,
                  parameters.InverseQ.IsNotNull() ? Convert.ToBase64String(parameters.InverseQ) : null,
                  parameters.D.IsNotNull() ? Convert.ToBase64String(parameters.D) : null);
        }
        #endregion
    }
}
#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：RSACrypto
* 类 描 述 ：RSA非对称加解密类扩展
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/16 23:14:58
* 更新时间 ：2020/05/31 22:14:58
* 版 本 号 ：v1.0.2.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Security.Cryptography;
using System.Text;
#if (NETCOREAPP3_0 || NETCOREAPP3_1)
using Kane.Extension.Json;
#else
using Kane.Extension.JsonNet;
#endif

namespace Kane.Extension
{
    /// <summary>
    /// RSA非对称加解密类扩展
    /// </summary>
    public class RSACrypto
    {
        #region RSA 用到的实体类和枚举类
        #region RSA Key Size 枚举
        /// <summary>
        /// RSA所用密钥模块的大小（以位为单位） 枚举
        /// </summary>
        public enum RSASize
        {
            /// <summary>
            /// RSA密钥使用2048位
            /// </summary>
            R2048 = 2048,
            /// <summary>
            /// RSA密钥使用3072位
            /// </summary>
            R3072 = 3072,
            /// <summary>
            /// RSA密钥使用R4096位
            /// </summary>
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
            /// RSA公钥
            /// </summary>
            public string PublicKey { get; set; }
            /// <summary>
            /// RSA私钥
            /// </summary>
            public string PrivateKey { get; set; }
            /// <summary>
            /// RSA公钥指数
            /// </summary>
            public string Exponent { get; set; }
            /// <summary>
            /// RSA公钥模数
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
            public string Modulus { get; set; }
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

        #region 创建RSA密钥对，包含公钥和私钥，默认键的大小为2048 + CreateRSAKey(RSASize rsaSize = RSASize.R2048)
        /// <summary>
        /// 创建RSA密钥对，包含公钥和私钥，默认键的大小为2048
        /// </summary>
        /// <param name="rsaSize"></param>
        /// <returns></returns>
        public RSAKey CreateRSAKey(RSASize rsaSize = RSASize.R2048)
        {
            using RSA rsa = RSA.Create();
            rsa.KeySize = (int)rsaSize;
            string publicKey = RSAToJson(rsa, false);
            string privateKey = RSAToJson(rsa, true);
            return new RSAKey()
            {
                PublicKey = publicKey,
                PrivateKey = privateKey,
                Exponent = rsa.ExportParameters(false).Exponent.BytesToHEX(),
                Modulus = rsa.ExportParameters(false).Modulus.BytesToHEX()
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
        public string RSAEncrypt(string value, string publicKey) => RSAEncrypt(value, publicKey, RSAEncryptionPadding.OaepSHA512);
        #endregion

        #region RSA非对称加密
        /// <summary>
        /// RSA非对称加密
        /// </summary>
        /// <param name="value">要加密的数据</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="padding">加密填充</param>
        /// <returns></returns>
        public string RSAEncrypt(string value, string publicKey, RSAEncryptionPadding padding)
        {
            using RSA rsa = RSA.Create();
            RSAFromJson(rsa, publicKey);
            var maxLength = GetMaxRSAEncryptLength(rsa, padding);
            var rawBytes = Encoding.UTF8.GetBytes(value);
            if (rawBytes.Length > maxLength) throw new Exception($"'{value}' is out of max length({maxLength}).");
            byte[] encryptBytes = rsa.Encrypt(rawBytes, padding);
            return encryptBytes.BytesToHEX();
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
        public string RSAEncrypt(string value, string publicKey)
        {
            using RSA rsa = RSA.Create();
            RSAFromJson(rsa, publicKey);
            var rawBytes = Encoding.UTF8.GetBytes(value);
            byte[] encryptBytes = rsa.EncryptValue(rawBytes);
            return encryptBytes.BytesToHEX();
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
        public string RSADecrypt(string value, string privateKey) => RSADecrypt(value, privateKey, RSAEncryptionPadding.OaepSHA512);
        #endregion

        #region RSA非对称解密
        /// <summary>
        /// RSA非对称解密
        /// </summary>
        /// <param name="value">要解密的数据，十六进制字符串</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="padding">加密填充</param>
        /// <returns></returns>
        public string RSADecrypt(string value, string privateKey, RSAEncryptionPadding padding)
        {
            using RSA rsa = RSA.Create();
            RSAFromJson(rsa, privateKey);
            byte[] valueBytes = StringEx.HexToBytes(value);
            byte[] outBytes = rsa.Decrypt(valueBytes, padding);
            return Encoding.UTF8.GetString(outBytes);
        }
        #endregion

        #region 计算加密码最大长度
        private int GetMaxRSAEncryptLength(RSA rsa, RSAEncryptionPadding padding)
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
#else
        #region RSA解密，Net45或以下没有使用填充
        /// <summary>
        /// RSA解密，Net45或以下没有使用填充
        /// </summary>
        /// <param name="value">要解密的数据，十六进制字符串</param>
        /// <param name="privateKey">私钥</param>
        /// <returns></returns>
        public string RSADecrypt(string value, string privateKey)
        {
            using RSA rsa = RSA.Create();
            RSAFromJson(rsa, privateKey);
            byte[] valueBytes = StringEx.HexToBytes(value);
            byte[] outBytes = rsa.DecryptValue(valueBytes);
            return Encoding.UTF8.GetString(outBytes);
        }
        #endregion
#endif

        #region 将Json字符串转成RSA参数 + RSAFromJson(RSA rsa, string jsonString)
        /// <summary>
        /// 将Json字符串转成RSA参数
        /// </summary>
        /// <param name="rsa">RSA类</param>
        /// <param name="jsonString">RSA参数Json字符串</param>
        internal void RSAFromJson(RSA rsa, string jsonString)
        {
            RSAParameters parameters = new RSAParameters();
            try
            {
                var paramsJson = jsonString.ToObject<RSAParametersJson>();
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

        #region RSA Key序列化Json + RSAToJson(RSA rsa, bool includePrivateParameters)
        /// <summary>
        /// RSA Key序列化Json
        /// </summary>
        /// <param name="rsa">RSA实例<see cref="RSA"/></param>
        /// <param name="includePrivateParameters">是否包含私钥</param>
        /// <returns></returns>
        internal string RSAToJson(RSA rsa, bool includePrivateParameters)
        {
            RSAParameters parameters = rsa.ExportParameters(includePrivateParameters);
            var paras = new RSAParametersJson()
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
            return paras.ToJson();
        }
        #endregion

        #region 将RSA Key序列化XML字符串 + RSAToXmlString(RSA rsa, bool includePrivateParameters)
        /// <summary>
        /// 将RSA Key序列化XML字符串
        /// </summary>
        /// <param name="rsa">RSA实例<see cref="RSA"/></param>
        /// <param name="includePrivateParameters">是否包含私钥</param>
        /// <returns></returns>
        public string RSAToXmlString(RSA rsa, bool includePrivateParameters)
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
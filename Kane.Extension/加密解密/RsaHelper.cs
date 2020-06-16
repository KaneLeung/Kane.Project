#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：RsaHelper
* 类 描 述 ：RSA非对称加密解密帮助类
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/06/15 15:18:20
* 更新时间 ：2020/06/15 15:18:20
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
#if !NET40 && !NET45
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Kane.Extension
{
    /// <summary>
    /// RSA非对称加密解密帮助类
    /// <para>查阅资料得知，注意要使用System.Security.Cryptography.RSA.Create() 工厂方法</para>
    /// <para>使用它之后，在 Windows 上创建的是 System.Security.Cryptography.RSACng 的实例</para>
    /// <para>在 Mac 与 Linux 上创建的是 System.Security.Cryptography.RSAOpenSsl 的实例，它们都继承自 System.Security.Cryptography.RSA 抽象类。</para>
    /// <para>Java 常用的 key 格式是 PKCS#8，JavaScript 一般使用 PKCS#1</para>
    /// <para>【.NET Core 3.0】和【.NET Standard 2.1】以后增加了对 PKCS#1 和 PKCS#8 Key 导入和导出支持</para>
    /// </summary>
    public class RsaHelper
    {
        static readonly byte[] SeqOID = new byte[] { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
        static readonly byte[] Version = new byte[] { 0x02, 0x01, 0x00 };

        #region Rsa密钥长度（以位为单位） 枚举
        /// <summary>
        /// Rsa密钥长度（以位为单位） 枚举
        /// </summary>
        public enum RsaSize
        {
            /// <summary>
            /// Rsa密钥使用1024位，不推荐使用
            /// </summary>
            R1024 = 1024,
            /// <summary>
            /// Rsa密钥使用2048位
            /// </summary>
            R2048 = 2048,
            /// <summary>
            /// Rsa密钥使用3072位
            /// </summary>
            R3072 = 3072,
            /// <summary>
            /// Rsa密钥使用R4096位
            /// </summary>
            R4096 = 4096
        }
        #endregion

        #region 创建Xml格式的RSA密钥对，默认【密钥长度】为2048 + CreateRSAKey(RSASize rsaSize = RSASize.R2048)
        /// <summary>
        /// 创建Xml格式的RSA密钥对，默认【密钥长度】为2048
        /// </summary>
        /// <param name="size">密钥长度</param>
        /// <returns></returns>
        public (string publicKey, string privateKey) CreateXmlKey(RsaSize size = RsaSize.R2048)
        {
            using RSA rsa = RSA.Create();
            rsa.KeySize = (int)size;
            var te = rsa.ToXmlString(false);
            return (rsa.ToXmlString(false), rsa.ToXmlString(true));
        }
        #endregion

        #region 创建PEM格式的RSA密钥对，默认填充为【PKCS#8】，默认【密钥长度】为2048 + CreatePemKey(bool isPKCS8 = true, RsaSize size = RsaSize.R2048)
        /// <summary>
        /// 创建PEM格式的RSA密钥对，默认填充为【PKCS#8】，默认【密钥长度】为2048
        /// <para>【isPKCS8】为真时返回PKCS#8格式，否则返回PKCS#1格式，默认为【PKCS#8】</para>
        /// </summary>
        /// <param name="isPKCS8">是否为【PKCS#8】填充方式</param>
        /// <param name="size">密钥长度</param>
        /// <returns></returns>
        public (string publicKey, string privateKey) CreatePemKey(bool isPKCS8 = true, RsaSize size = RsaSize.R2048)
        {
            using RSA rsa = RSA.Create();
            rsa.KeySize = (int)size;
            return (RsaToPEM(rsa, false, isPKCS8), RsaToPEM(rsa, true, isPKCS8));
        }
        #endregion

        #region Xml格式的密钥转PEM格式的密钥，默认填充为【PKCS#8】 + XmlToPem(string xml, bool isPKCS8 = true)
        /// <summary>
        /// Xml格式的密钥转PEM格式的密钥，默认填充为【PKCS#8】
        /// </summary>
        /// <param name="xml">原Xml格式密钥</param>
        /// <param name="isPKCS8">是否为【PKCS#8】填充方式</param>
        /// <returns></returns>
        public string XmlToPem(string xml, bool isPKCS8 = true)
        {
            using RSA rsa = RSA.Create();
            rsa.FromXmlString(xml);
            bool isPrivate = false;
            try //先尝试转成私钥
            {
                var para = rsa.ExportParameters(true);
                isPrivate = true;
            }
            catch { }
            return RsaToPEM(rsa, isPrivate, isPKCS8);
        }
        #endregion

        #region PEM格式的密钥转Xml格式的密钥，支持PKCS#1、PKCS#8格式的PEM + PemToXml(string pem)
        /// <summary>
        /// PEM格式的密钥转Xml格式的密钥，支持PKCS#1、PKCS#8格式的PEM
        /// </summary>
        /// <param name="pem">原Pem格式密钥</param>
        /// <returns></returns>
        public string PemToXml(string pem)
        {
            var (param, isPrivate) = GetParmFromPEM(pem);
            using RSA rsa = RSA.Create();
            rsa.ImportParameters(param);
            return rsa.ToXmlString(isPrivate);
        }
        #endregion

        #region 由RSA私钥生成公钥，默认为【Xml格式】的私钥 + ExportPublicKeyFromPrivateKey(string privateKey, bool isXml = true, bool isPKCS8 = true)
        /// <summary>
        /// 由RSA私钥生成公钥，默认为【Xml格式】的私钥
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="isXml">是否为【Xml格式】私钥，否则为【Pem】格式</param>
        /// <param name="isPKCS8">是否为【PKCS#8】填充方式，当【isXml】为false时才生效</param>
        /// <returns></returns>
        public string ExportPublicKeyFromPrivateKey(string privateKey, bool isXml = true, bool isPKCS8 = true)
        {
            if (isXml)//如果是Xml格式
            {
                try
                {
                    using RSA rsa = RSA.Create();
                    rsa.FromXmlString(privateKey);
                    return rsa.ToXmlString(false);
                }
                catch
                {
                    throw new Exception($"无效的RSA私钥");
                }
            }
            else
            {
                var (param, isPrivate) = GetParmFromPEM(privateKey);
                if (isPrivate == false) throw new Exception($"无效的RSA私钥");
                using RSA rsa = RSA.Create();
                rsa.ImportParameters(param);
                return RsaToPEM(rsa, false, isPKCS8);
            }
        }
        #endregion

        #region 将字节数组进行RSA加密，返回加密后的字节数组，默认为【Xml格式】的密钥，默认【不分块】 + EncryptBytes(...)
        /// <summary>
        /// 将字节数组进行RSA加密，返回加密后的字节数组，默认为【Xml格式】的密钥，默认【不分块】
        /// </summary>
        /// <param name="data">要加密的字节数组</param>
        /// <param name="key">公钥或私钥字符串</param>
        /// <param name="padding">填充方式枚举值</param>
        /// <param name="isXmlKey">是否为【Xml格式】私钥</param>
        /// <param name="autoBlock">是否自动分块</param>
        /// <returns></returns>
        public byte[] EncryptBytes(byte[] data, string key, RSAEncryptionPadding padding, bool isXmlKey = true, bool autoBlock = false)
        {
            using var rsa = RSA.Create();
            if (isXmlKey) rsa.FromXmlString(key);
            else
            {
                var (param, _) = GetParmFromPEM(key);
                rsa.ImportParameters(param);
            }
            var maxBlockSize = MaxBlockSize(rsa, padding);
            if (autoBlock == false && data.Length > maxBlockSize)
                throw new ArgumentOutOfRangeException(nameof(data), $"数据超过最大长度[{maxBlockSize}]，请设置【autoBlock】为true，或使用更大的密钥长度");
            if (data.Length <= maxBlockSize) return rsa.Encrypt(data, padding);
            using var memoryStream = new MemoryStream(data);
            using var result = new MemoryStream();
            byte[] buffer = new byte[maxBlockSize];
            int blockSize = memoryStream.Read(buffer, 0, maxBlockSize);
            while (blockSize > 0)
            {
                var blockBytes = new byte[blockSize];
                Array.Copy(buffer, 0, blockBytes, 0, blockSize);
                var encrypts = rsa.Encrypt(blockBytes, padding);
                result.Write(encrypts, 0, encrypts.Length);
                blockSize = memoryStream.Read(buffer, 0, maxBlockSize);
            }
            return result.ToArray();
        }
        #endregion

        #region 将字符串进行RSA加密，返回加密后的字节数组，默认为【Xml格式】的密钥，默认【不分块】 + EncryptBytes(...)
        /// <summary>
        /// 将字符串进行RSA加密，返回加密后的字节数组，默认为【Xml格式】的密钥，默认【不分块】
        /// </summary>
        /// <param name="data">要加密的字符串</param>
        /// <param name="key">公钥或私钥字符串</param>
        /// <param name="padding">填充方式枚举值</param>
        /// <param name="isXmlKey">是否为【Xml格式】私钥</param>
        /// <param name="autoBlock">是否自动分块</param>
        /// <returns></returns>
        public byte[] EncryptBytes(string data, string key, RSAEncryptionPadding padding, bool isXmlKey = true, bool autoBlock = false)
            => EncryptBytes(data.ToBytes(), key, padding, isXmlKey, autoBlock);
        #endregion

        #region 将字节数组进行RSA加密，返回加密后的字节数组，使用【Pkcs1】填充，默认为【Xml格式】的密钥，默认【不分块】 + EncryptBytes(byte[] data, string key, bool isXmlKey = true, bool autoBlock = false)
        /// <summary>
        /// 将字节数组进行RSA加密，返回加密后的字节数组，使用【Pkcs1】填充，默认为【Xml格式】的密钥，默认【不分块】
        /// </summary>
        /// <param name="data">要加密的字节数组</param>
        /// <param name="key">公钥或私钥字符串</param>
        /// <param name="isXmlKey">是否为【Xml格式】私钥</param>
        /// <param name="autoBlock">是否自动分块</param>
        /// <returns></returns>
        public byte[] EncryptBytes(byte[] data, string key, bool isXmlKey = true, bool autoBlock = false) =>
            EncryptBytes(data, key, RSAEncryptionPadding.Pkcs1, isXmlKey, autoBlock);
        #endregion

        #region 将字符串进行RSA加密，返回加密后的字节数组，使用【Pkcs1】填充，默认为【Xml格式】的密钥，默认【不分块】 + EncryptBytes(string data, string key, bool isXmlKey = true, bool autoBlock = false)
        /// <summary>
        /// 将字符串进行RSA加密，返回加密后的字节数组，使用【Pkcs1】填充，默认为【Xml格式】的密钥，默认【不分块】
        /// </summary>
        /// <param name="data">要加密的字符串</param>
        /// <param name="key">公钥或私钥字符串</param>
        /// <param name="isXmlKey">是否为【Xml格式】私钥</param>
        /// <param name="autoBlock">是否自动分块</param>
        /// <returns></returns>
        public byte[] EncryptBytes(string data, string key, bool isXmlKey = true, bool autoBlock = false)
            => EncryptBytes(data.ToBytes(), key, isXmlKey, autoBlock);
        #endregion

        #region 将字节数组进行RSA加密，返回加密后的Base64字符串，默认为【Xml格式】的密钥，默认【不分块】 + Encrypt(...)
        /// <summary>
        /// 将字节数组进行RSA加密，返回加密后的Base64字符串，默认为【Xml格式】的密钥，默认【不分块】
        /// </summary>
        /// <param name="data">要加密的字节数组</param>
        /// <param name="key">公钥或私钥字符串</param>
        /// <param name="padding">填充方式枚举值</param>
        /// <param name="isXmlKey">是否为【Xml格式】私钥</param>
        /// <param name="autoBlock">是否自动分块</param>
        /// <returns>【Base64字符串】</returns>
        public string Encrypt(byte[] data, string key, RSAEncryptionPadding padding, bool isXmlKey = true, bool autoBlock = false)
            => EncryptBytes(data, key, padding, isXmlKey, autoBlock)?.ToBase64();
        #endregion

        #region 将字符串进行RSA加密，返回加密后的Base64字符串，默认为【Xml格式】的密钥，默认【不分块】 + Encrypt(...)
        /// <summary>
        /// 将字符串进行RSA加密，返回加密后的Base64字符串，默认为【Xml格式】的密钥，默认【不分块】
        /// </summary>
        /// <param name="data">要加密的字符串</param>
        /// <param name="key">公钥或私钥字符串</param>
        /// <param name="padding">填充方式枚举值</param>
        /// <param name="isXmlKey">是否为【Xml格式】私钥</param>
        /// <param name="autoBlock">是否自动分块</param>
        /// <returns>【Base64字符串】</returns>
        public string Encrypt(string data, string key, RSAEncryptionPadding padding, bool isXmlKey = true, bool autoBlock = false)
            => Encrypt(data.ToBytes(), key, padding, isXmlKey, autoBlock);
        #endregion

        #region 将字节数组进行RSA加密，返回加密后的Base64字符串，使用【Pkcs1】填充，默认为【Xml格式】的密钥，默认【不分块】 + Encrypt(byte[] data, string key, bool isXmlKey = true, bool autoBlock = false)
        /// <summary>
        /// 将字节数组进行RSA加密，返回加密后的Base64字符串，使用【Pkcs1】填充，默认为【Xml格式】的密钥，默认【不分块】
        /// </summary>
        /// <param name="data">要加密的字节数组</param>
        /// <param name="key">公钥或私钥字符串</param>
        /// <param name="isXmlKey">是否为【Xml格式】私钥</param>
        /// <param name="autoBlock">是否自动分块</param>
        /// <returns>【Base64字符串】</returns>
        public string Encrypt(byte[] data, string key, bool isXmlKey = true, bool autoBlock = false)
            => Encrypt(data, key, RSAEncryptionPadding.Pkcs1, isXmlKey, autoBlock);
        #endregion

        #region 将字符串进行RSA加密，返回加密后的Base64字符串，使用【Pkcs1】填充，默认为【Xml格式】的密钥，默认【不分块】 + Encrypt(string data, string key, bool isXmlKey = true, bool autoBlock = false)
        /// <summary>
        /// 将字符串进行RSA加密，返回加密后的Base64字符串，使用【Pkcs1】填充，默认为【Xml格式】的密钥，默认【不分块】
        /// </summary>
        /// <param name="data">要加密的字符串</param>
        /// <param name="key">公钥或私钥字符串</param>
        /// <param name="isXmlKey">是否为【Xml格式】私钥</param>
        /// <param name="autoBlock">是否自动分块</param>
        /// <returns>【Base64字符串】</returns>
        public string Encrypt(string data, string key, bool isXmlKey = true, bool autoBlock = false)
            => Encrypt(data.ToBytes(), key, isXmlKey, autoBlock);
        #endregion

        #region 将字节数组进行RSA解密，返回解密后的字节数组，默认为【Xml格式】的私钥，默认【不分块】 + DecryptBytes(...)
        /// <summary>
        /// 将字节数组进行RSA解密，返回解密后的字节数组，默认为【Xml格式】的私钥，默认【不分块】
        /// </summary>
        /// <param name="data">要解密的字节数组</param>
        /// <param name="privateKey">私钥字符串</param>
        /// <param name="padding">填充方式枚举值</param>
        /// <param name="isXmlKey">是否为【Xml格式】私钥</param>
        /// <param name="autoBlock">是否自动分块</param>
        /// <returns></returns>
        public byte[] DecryptBytes(byte[] data, string privateKey, RSAEncryptionPadding padding, bool isXmlKey = true, bool autoBlock = false)
        {
            using var rsa = RSA.Create();
            if (isXmlKey) rsa.FromXmlString(privateKey);
            else
            {
                var (param, isPrivate) = GetParmFromPEM(privateKey);
                if (isPrivate) rsa.ImportParameters(param);
                else throw new Exception("需要正确的PEM私钥才能解密数据");
            }
            var maxBlockSize = rsa.KeySize / 8;
            if (autoBlock == false && data.Length > maxBlockSize)
                throw new ArgumentOutOfRangeException(nameof(data), $"数据超过最大长度[{maxBlockSize}]，请设置【autoBlock】为true，或使用更大的密钥长度");
            if (data.Length <= maxBlockSize) return rsa.Decrypt(data, padding);
            using var memoryStream = new MemoryStream(data);
            using var result = new MemoryStream();
            var buffer = new byte[maxBlockSize];
            var blockSize = memoryStream.Read(buffer, 0, maxBlockSize);
            while (blockSize > 0)
            {
                var blockBytes = new byte[blockSize];
                Array.Copy(buffer, 0, blockBytes, 0, blockSize);
                var decrypts = rsa.Decrypt(blockBytes, padding);
                result.Write(decrypts, 0, decrypts.Length);
                blockSize = memoryStream.Read(buffer, 0, maxBlockSize);
            }
            return result.ToArray();
        }
        #endregion

        #region 将Base64字符串进行RSA解密，返回解密后的字节数组，默认为【Xml格式】的私钥，默认【不分块】 + DecryptBytes(...)
        /// <summary>
        /// 将Base64字符串进行RSA解密，返回解密后的字节数组，默认为【Xml格式】的私钥，默认【不分块】
        /// </summary>
        /// <param name="data">要解密的【Base64字符串】</param>
        /// <param name="privateKey">私钥字符串</param>
        /// <param name="padding">填充方式枚举值</param>
        /// <param name="isXmlKey">是否为【Xml格式】私钥</param>
        /// <param name="autoBlock">是否自动分块</param>
        /// <returns></returns>
        public byte[] DecryptBytes(string data, string privateKey, RSAEncryptionPadding padding, bool isXmlKey = true, bool autoBlock = false)
            => DecryptBytes(data.Base64ToBytes(), privateKey, padding, isXmlKey, autoBlock);
        #endregion

        #region 将字节数组进行RSA解密，返回解密后的字节数组，使用【Pkcs1】填充，默认为【Xml格式】的私钥，默认【不分块】 + DecryptBytes(byte[] data, string privateKey, bool isXmlKey = true, bool autoBlock = false)
        /// <summary>
        /// 将字节数组进行RSA解密，返回解密后的字节数组，使用【Pkcs1】填充，默认为【Xml格式】的私钥，默认【不分块】
        /// </summary>
        /// <param name="data">要解密的字节数组</param>
        /// <param name="privateKey">私钥字符串</param>
        /// <param name="isXmlKey">是否为【Xml格式】私钥</param>
        /// <param name="autoBlock">是否自动分块</param>
        /// <returns></returns>
        public byte[] DecryptBytes(byte[] data, string privateKey, bool isXmlKey = true, bool autoBlock = false)
            => DecryptBytes(data, privateKey, RSAEncryptionPadding.Pkcs1, isXmlKey, autoBlock);
        #endregion

        #region 将Base64字符串进行RSA解密，返回解密后的字节数组，使用【Pkcs1】填充，默认为【Xml格式】的私钥，默认【不分块】 + DecryptBytes(string data, string privateKey, bool isXmlKey = true, bool autoBlock = false)
        /// <summary>
        /// 将Base64字符串进行RSA解密，返回解密后的字节数组，使用【Pkcs1】填充，默认为【Xml格式】的私钥，默认【不分块】
        /// </summary>
        /// <param name="data">要解密的【Base64字符串】</param>
        /// <param name="privateKey">私钥字符串</param>
        /// <param name="isXmlKey">是否为【Xml格式】私钥</param>
        /// <param name="autoBlock">是否自动分块</param>
        /// <returns></returns>
        public byte[] DecryptBytes(string data, string privateKey, bool isXmlKey = true, bool autoBlock = false)
            => DecryptBytes(data.Base64ToBytes(), privateKey, isXmlKey, autoBlock);
        #endregion

        #region 将字节数组进行RSA解密，返回解密后的字符串，默认为【Xml格式】的私钥，默认【不分块】 + Decrypt(...)
        /// <summary>
        /// 将字节数组进行RSA解密，返回解密后的字符串，默认为【Xml格式】的私钥，默认【不分块】
        /// </summary>
        /// <param name="data">要解密的字节数组</param>
        /// <param name="privateKey">私钥字符串</param>
        /// <param name="padding">填充方式枚举值</param>
        /// <param name="isXmlKey">是否为【Xml格式】私钥</param>
        /// <param name="autoBlock">是否自动分块</param>
        /// <returns></returns>
        public string Decrypt(byte[] data, string privateKey, RSAEncryptionPadding padding, bool isXmlKey = true, bool autoBlock = false)
            => DecryptBytes(data, privateKey, padding, isXmlKey, autoBlock)?.BytesToString();
        #endregion

        #region 将Base64字符串进行RSA解密，返回解密后的字符串，默认为【Xml格式】的私钥，默认【不分块】 + Decrypt(...)
        /// <summary>
        /// 将Base64字符串进行RSA解密，返回解密后的字符串，默认为【Xml格式】的私钥，默认【不分块】
        /// </summary>
        /// <param name="data">要解密的【Base64字符串】</param>
        /// <param name="privateKey">私钥字符串</param>
        /// <param name="padding">填充方式枚举值</param>
        /// <param name="isXmlKey">是否为【Xml格式】私钥</param>
        /// <param name="autoBlock">是否自动分块</param>
        /// <returns></returns>
        public string Decrypt(string data, string privateKey, RSAEncryptionPadding padding, bool isXmlKey = true, bool autoBlock = false)
            => Decrypt(data.Base64ToBytes(), privateKey, padding, isXmlKey, autoBlock);
        #endregion

        #region 将字节数组进行RSA解密，返回解密后的字符串，使用【Pkcs1】填充，默认为【Xml格式】的私钥，默认【不分块】 + Decrypt(byte[] data, string privateKey, bool isXmlKey = true, bool autoBlock = false)
        /// <summary>
        /// 将字节数组进行RSA解密，返回解密后的字符串，使用【Pkcs1】填充，默认为【Xml格式】的私钥，默认【不分块】
        /// </summary>
        /// <param name="data">要解密的字节数组</param>
        /// <param name="privateKey">私钥字符串</param>
        /// <param name="isXmlKey">是否为【Xml格式】私钥</param>
        /// <param name="autoBlock">是否自动分块</param>
        /// <returns></returns>
        public string Decrypt(byte[] data, string privateKey, bool isXmlKey = true, bool autoBlock = false)
            => Decrypt(data, privateKey, RSAEncryptionPadding.Pkcs1, isXmlKey, autoBlock);
        #endregion

        #region 将Base64字符串进行RSA解密，返回解密后的字符串，使用【Pkcs1】填充，默认为【Xml格式】的私钥，默认【不分块】 + Decrypt(string data, string privateKey, bool isXmlKey = true, bool autoBlock = false)
        /// <summary>
        /// 将Base64字符串进行RSA解密，返回解密后的字符串，使用【Pkcs1】填充，默认为【Xml格式】的私钥，默认【不分块】
        /// </summary>
        /// <param name="data">要解密的【Base64字符串】</param>
        /// <param name="privateKey">私钥字符串</param>
        /// <param name="isXmlKey">是否为【Xml格式】私钥</param>
        /// <param name="autoBlock">是否自动分块</param>
        /// <returns></returns>
        public string Decrypt(string data, string privateKey, bool isXmlKey = true, bool autoBlock = false)
            => Decrypt(data.Base64ToBytes(), privateKey, isXmlKey, autoBlock);
        #endregion

        #region 最大分块大小 + MaxBlockSize(RSA rsa, RSAEncryptionPadding padding)
        /// <summary>
        /// 最大分块大小
        /// </summary>
        /// <param name="rsa">RSA实例</param>
        /// <param name="padding">填充方式</param>
        /// <returns></returns>
        private int MaxBlockSize(RSA rsa, RSAEncryptionPadding padding)
        {
            int offset = 0;
            if (padding.Equals(RSAEncryptionPadding.Pkcs1)) offset = 11;
            else if (padding.Equals(RSAEncryptionPadding.OaepSHA1)) offset = 42;
            else if (padding.Equals(RSAEncryptionPadding.OaepSHA256)) offset = 66;
            else if (padding.Equals(RSAEncryptionPadding.OaepSHA384)) offset = 98;
            else if (padding.Equals(RSAEncryptionPadding.OaepSHA512)) offset = 130;
            return rsa.KeySize / 8 - offset;
        }
        #endregion

        #region 将RSA中的密钥对转换成PEM格式，【isPKCS8】为真时返回PKCS#8格式，否则返回PKCS#1格式 + ParaToPEM(RSAParameters para, bool isPrivate, bool isPKCS8)
        /// <summary>
        /// 将RSA中的密钥对转换成PEM格式，【isPKCS8】为真时返回PKCS#8格式，否则返回PKCS#1格式，默认为【PKCS#8】
        /// </summary>
        /// <param name="rsa">RSA实例</param>
        /// <param name="isPrivate">是否生成私钥</param>
        /// <param name="isPKCS8">是否为【PKCS#8】填充方式</param>
        /// <returns></returns>
        private string RsaToPEM(RSA rsa, bool isPrivate, bool isPKCS8 = true)
        {
            var stream = new MemoryStream();
            var para = rsa.ExportParameters(isPrivate);
            if (isPrivate)//生成PEM私钥
            {
                para.D.ThrowIfNull(nameof(para), $"RSA生成私钥参数不完整，生成失败");
                stream.WriteByte(0x30);//写入总字节数，后续写入
                int postion = (int)stream.Length;
                stream.Write(Version, 0, Version.Length);//写入版本号
                int postion1 = -1, postion2 = -1;
                if (isPKCS8)//PKCS8 多一段数据
                {
                    stream.Write(SeqOID, 0, SeqOID.Length);//固定内容
                    stream.WriteByte(0x04);
                    postion1 = (int)stream.Length;//后续内容长度
                    stream.WriteByte(0x30);
                    postion2 = (int)stream.Length;//后续内容长度
                    stream.Write(Version, 0, Version.Length);//写入版本号
                }
                WriteBlock(stream, para.Modulus);//写入【Modulus】数据
                WriteBlock(stream, para.Exponent);//写入【Exponent】数据
                WriteBlock(stream, para.D);//写入【D】数据
                WriteBlock(stream, para.P);//写入【P】数据
                WriteBlock(stream, para.Q);//写入【Q】数据
                WriteBlock(stream, para.DP);//写入【DP】数据
                WriteBlock(stream, para.DQ);//写入【DQ】数据
                WriteBlock(stream, para.InverseQ);//写入【InverseQ】数据
                var bytes = stream.ToArray();//计算空缺的长度
                if (postion1 != -1)
                {
                    bytes = WriteLen(stream, postion2, bytes);
                    bytes = WriteLen(stream, postion1, bytes);
                }
                bytes = WriteLen(stream, postion, bytes);
                var flag = isPKCS8 ? " PRIVATE KEY" : " RSA PRIVATE KEY";
                return $"-----BEGIN{flag}-----{Environment.NewLine}{WriteLineBreak(bytes.ToBase64(), 64)}{Environment.NewLine}-----END{flag}-----";
            }
            else //生成PEM公钥
            {
                stream.WriteByte(0x30);//写入总字节数，不含本段长度，额外需要24字节的头，后续计算好填入
                var postion = (int)stream.Length;
                stream.Write(SeqOID, 0, SeqOID.Length);//固定[OID]内容
                stream.WriteByte(0x03);//从0x00开始的后续长度
                var postion2 = (int)stream.Length;
                stream.WriteByte(0x00);
                stream.WriteByte(0x30);//后续内容长度
                var postion3 = (int)stream.Length;
                WriteBlock(stream, para.Modulus);//写入Modulus
                WriteBlock(stream, para.Exponent);//写入Exponent
                var bytes = stream.ToArray();//计算空缺的长度
                bytes = WriteLen(stream, postion3, bytes);
                bytes = WriteLen(stream, postion2, bytes);
                bytes = WriteLen(stream, postion, bytes);
                return $"-----BEGIN PUBLIC KEY-----{Environment.NewLine}{WriteLineBreak(bytes.ToBase64(), 64)}{Environment.NewLine}-----END PUBLIC KEY-----";
            }
        }
        #endregion

        #region 写入一个长度字节码 + WriteLenByte(MemoryStream stream, int length)
        /// <summary>
        /// 写入一个长度字节码
        /// </summary>
        /// <param name="stream">原MemoryStream</param>
        /// <param name="length">长度</param>
        private void WriteLenByte(MemoryStream stream, int length)
        {
            if (length < 0x80) stream.WriteByte((byte)length);
            else if (length <= 0xff)
            {
                stream.WriteByte(0x81);
                stream.WriteByte((byte)length);
            }
            else
            {
                stream.WriteByte(0x82);
                stream.WriteByte((byte)(length >> 8 & 0xff));
                stream.WriteByte((byte)(length & 0xff));
            }
        }
        #endregion

        #region 写入一块数据 + WriteBlock(MemoryStream stream, byte[] data)
        /// <summary>
        /// 写入一块数据
        /// </summary>
        /// <param name="stream">原MemoryStream</param>
        /// <param name="data">写入的数据</param>
        private void WriteBlock(MemoryStream stream, byte[] data)
        {
            var zeroFlag = (data[0] >> 4) >= 0x8;
            stream.WriteByte(0x02);
            var length = data.Length + (zeroFlag ? 1 : 0);
            WriteLenByte(stream, length);
            if (zeroFlag) stream.WriteByte(0x00);
            stream.Write(data, 0, data.Length);
        }
        #endregion

        #region 根据后续内容长度写入指定长度数据 + WriteLen(MemoryStream stream, int index, byte[] data)
        /// <summary>
        /// 根据后续内容长度写入指定长度数据
        /// </summary>
        /// <param name="stream">原<see cref="MemoryStream"/></param>
        /// <param name="index">写入的长度</param>
        /// <param name="data">写入的数据</param>
        /// <returns></returns>
        private byte[] WriteLen(MemoryStream stream, int index, byte[] data)
        {
            var length = data.Length - index;
            stream.SetLength(0);
            stream.Write(data, 0, index);
            WriteLenByte(stream, length);
            stream.Write(data, index, length);
            return stream.ToArray();
        }
        #endregion

        #region 根据长度进行换行 + WriteLineBreak(string data, int line)
        /// <summary>
        /// 根据长度进行换行
        /// </summary>
        /// <param name="data">原数据</param>
        /// <param name="line">换行长度</param>
        /// <returns></returns>
        private string WriteLineBreak(string data, int line)
        {
            var index = 0;
            var builder = new StringBuilder();
            while (index < data.Length)
            {
                if (index > 0) builder.Append(Environment.NewLine);
                if (index + line >= data.Length) builder.Append(data.Substring(index));
                else builder.Append(data.Substring(index, line));
                index += line;
            }
            return builder.ToString();
        }
        #endregion

        #region 从PEM格式获取密钥信息，支持PKCS#1、PKCS#8格式的PEM + GetParmFromPEM(string pemKey)
        /// <summary>
        /// 从PEM格式获取密钥信息，支持PKCS#1、PKCS#8格式的PEM
        /// </summary>
        /// <param name="pemKey">PEM字符串</param>
        /// <returns></returns>
        private (RSAParameters param, bool isPrivate) GetParmFromPEM(string pemKey)
        {
            RSAParameters param = new RSAParameters();
            var content = pemKey.RegexReplace(@"--+[\w+\s]+--+|\s+", string.Empty);
            byte[] data = null;
            try { data = content.Base64ToBytes(); } catch { }
            if (data == null) throw new Exception("PEM数据无效");
            var index = 0;
            ReadLen(data, 0x30, ref index);//读取数据总长度
            if (pemKey.Contains("PUBLIC KEY"))//是否为公钥
            {
                var tempIndex = index;
                if (EqualsBytes(data, SeqOID, ref index))//看看有没有OID标记
                {
                    ReadLen(data, 0x03, ref index);//读取1长度
                    index++;//跳过0x00 //读取2长度
                    ReadLen(data, 0x30, ref index);
                }
                else index = tempIndex;
                param.Modulus = ReadBlock(data, ref index);//读取【Modulus】值
                param.Exponent = ReadBlock(data, ref index);//读取【Exponent】值
                return (param, false);
            }
            else if (pemKey.Contains("PRIVATE KEY"))//是否为私钥
            {
                if (!EqualsBytes(data, Version, ref index)) throw new Exception("PEM未知版本");//读取版本号
                var tempIndex = index;
                if (EqualsBytes(data, SeqOID, ref index))//检测PKCS8
                {
                    ReadLen(data, 0x04, ref index);//读取1长度
                    ReadLen(data, 0x30, ref index);//读取2长度
                    if (!EqualsBytes(data, Version, ref index)) throw new Exception("PEM版本无效");//读取版本号
                }
                else index = tempIndex;
                param.Modulus = ReadBlock(data, ref index);//读取【Modulus】值
                param.Exponent = ReadBlock(data, ref index);//读取【Exponent】值
                param.D = ReadBlock(data, ref index);//读取【D】值
                param.P = ReadBlock(data, ref index);//读取【P】值
                param.Q = ReadBlock(data, ref index);//读取【Q】值
                param.DP = ReadBlock(data, ref index);//读取【DP】值
                param.DQ = ReadBlock(data, ref index);//读取【DQ】值
                param.InverseQ = ReadBlock(data, ref index);//读取【InverseQ】值
                return (param, true);
            }
            else throw new Exception("无效的PEM BEGIN END标头");
        }
        #endregion

        #region 读取长度 + ReadLen(byte[] data, byte first, ref int index)
        /// <summary>
        /// 读取长度
        /// </summary>
        /// <param name="data">原数据</param>
        /// <param name="first">起始字节</param>
        /// <param name="index">位置</param>
        /// <returns></returns>
        private int ReadLen(byte[] data, byte first, ref int index)
        {
            if (data[index] == first)
            {
                index++;
                if (data[index] == 0x81)
                {
                    index++;
                    return data[index++];
                }
                else if (data[index] == 0x82)
                {
                    index++;
                    return (data[index++] << 8) + data[index++];
                }
                else if (data[index] < 0x80) return data[index++];
            }
            throw new Exception("PEM未能提取到数据");
        }
        #endregion

        #region 读取块数据 + ReadBlock(byte[] data, ref int index)
        /// <summary>
        /// 读取块数据
        /// </summary>
        /// <param name="data">原数据</param>
        /// <param name="index">位置</param>
        /// <returns></returns>
        private byte[] ReadBlock(byte[] data, ref int index)
        {
            var len = ReadLen(data, 0x02, ref index);
            if (data[index] == 0x00)
            {
                index++;
                len--;
            }
            var bytes = new byte[len];
            for (var i = 0; i < len; i++)
                bytes[i] = data[index + i];
            index += len;
            return bytes;
        }
        #endregion

        #region 比较【data】从【index】位置开始是否是【value】内容 + EqualsBytes(byte[] data, byte[] value, ref int index)
        /// <summary>
        /// 比较【data】从【index】位置开始是否是【value】内容
        /// </summary>
        /// <param name="data">原数据</param>
        /// <param name="value">判断的内容</param>
        /// <param name="index">数组位置</param>
        /// <returns></returns>
        private bool EqualsBytes(byte[] data, byte[] value, ref int index)
        {
            for (var i = 0; i < value.Length; i++, index++)
            {
                if (index >= data.Length) return false;
                if (value[i] != data[index]) return false;
            }
            return true;
        }
        #endregion
    }
}
#endif
#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：AesHelper
* 类 描 述 ：Aes加解密帮助类
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/06/12 13:12:20
* 更新时间 ：2020/06/12 13:12:20
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.IO;
using System.Security.Cryptography;

namespace Kane.Extension
{
    /// <summary>
    /// AES加密解密帮助类
    /// <para>AES（Advanced Encryption Standard）：对称性加密算法，高级加密标准，是下一代的加密算法标准，速度快，安全级别高；</para>
    /// <para>AES是一个使用128为分组块的分组加密算法，分组块和128、192或256位的密钥一起作为输入，对4×4的字节数组上进行操作。</para>
    /// <para>众所周之AES是种十分高效的算法，尤其在8位架构中，这源于它面向字节的设计。AES 适用于8位的小型单片机或者普通的32位微处理器,</para>
    /// <para>并且适合用专门的硬件实现，硬件实现能够使其吞吐量（每秒可以到达的加密/解密bit数）达到十亿量级。同样，其也适用于RFID系统。</para>
    /// </summary>
    public class AesHelper
    {
        #region 将字节数组数据进行AES加密，返回Base64字符串 + Encrypt(byte[] data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字节数组数据进行AES加密，返回Base64字符串，32位密钥，16位IV向量，默认使用【ECB】模式和【PKCS7】填充
        /// </summary>
        /// <param name="data">要加密的数据【字节数组】</param>
        /// <param name="key">密钥，最大32Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(32)生成</param>
        /// <param name="iv">向量，最大16Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(16)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns>返回Base64字符串</returns>
        public string Encrypt(byte[] data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => EncryptBytes(data, key, iv, mode, padding)?.ToBase64();
        #endregion

        #region 将字符串进行AES加密，返回Base64字符串 + Encrypt(string data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字符串进行AES加密，返回Base64字符串，32位密钥，16位IV向量，默认使用【ECB】模式和【PKCS7】填充
        /// <para>密钥和向量不够位数时，自动填充，超出时自动截取</para>
        /// </summary>
        /// <param name="data">要加密的数据【字符串】</param>
        /// <param name="key">密钥，最大32Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(32)生成</param>
        /// <param name="iv">向量，最大16Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(16)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns>返回Base64字符串</returns>
        public string Encrypt(string data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => Encrypt(data.ToBytes(), key, iv, mode, padding);
        #endregion

        #region 将字节数组数据进行AES加密，返回加密后的字节数组 + EncryptBytes(byte[] data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字节数组数据进行AES加密，返回加密后的字节数组，32位密钥，16位IV向量，默认使用【ECB】模式和【PKCS7】填充
        /// </summary>
        /// <param name="data">要加密的数据【字节数组】</param>
        /// <param name="key">密钥，最大32Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(32)生成</param>
        /// <param name="iv">向量，最大16Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(16)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public byte[] EncryptBytes(byte[] data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        {
            byte[] keyBytes = new byte[32], ivBytes = new byte[16];
            Array.Copy(key.PadRight(keyBytes.Length).ToBytes(), keyBytes, keyBytes.Length);
            Array.Copy(iv.PadRight(ivBytes.Length).ToBytes(), ivBytes, ivBytes.Length);
            using Aes aes = Aes.Create();
            try
            {
                aes.Mode = mode;
                aes.Padding = padding;
                using MemoryStream memory = new MemoryStream();
                using CryptoStream Encryptor = new CryptoStream(memory, aes.CreateEncryptor(keyBytes, ivBytes), CryptoStreamMode.Write);
                Encryptor.Write(data, 0, data.Length);
                Encryptor.FlushFinalBlock();
                return memory.ToArray();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 将字符串数据进行AES加密，返回加密后的字节数组 + EncryptBytes(string data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字符串数据进行AES加密，返回加密后的字节数组，32位密钥，16位IV向量，默认使用【ECB】模式和【PKCS7】填充
        /// </summary>
        /// <param name="data">要加密的数据【字节数组】</param>
        /// <param name="key">密钥，最大32Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(32)生成</param>
        /// <param name="iv">向量，最大16Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(16)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public byte[] EncryptBytes(string data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => EncryptBytes(data.ToBytes(), key, iv, mode, padding);
        #endregion

        #region 将字节数组进行AES解密，返回解密后的字符串 + Decrypt(byte[] data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字节数组进行AES解密，返回解密后的字符串，32位密钥，16位VI向量，默认使用【ECB】模式和【PKCS7】填充
        /// </summary>
        /// <param name="data">要解密的数据【字节数组】</param>
        /// <param name="key">密钥，最大32Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(32)生成</param>
        /// <param name="iv">向量，最大16Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(16)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public string Decrypt(byte[] data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => DecryptBytes(data, key, iv, mode, padding)?.BytesToString();
        #endregion

        #region 将Base64字符串进行AES解密，返回解密后的字符串 + Decrypt(string data, string key, string iv,CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将Base64字符串进行AES解密，返回解密后的字符串，32位密钥，16位VI向量，默认使用【ECB】模式和【PKCS7】填充
        /// </summary>
        /// <param name="data">要解密的数据【Base64字符串】</param>
        /// <param name="key">密钥，最大32Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(32)生成</param>
        /// <param name="iv">向量，最大16Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(16)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public string Decrypt(string data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => Decrypt(data.Base64ToBytes(), key, iv, mode, padding);
        #endregion

        #region 将字节数组进行AES解密，返回解密后的字节数组 + DecryptBytes(byte[] data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字节数组进行AES解密，返回解密后的字节数组，32位密钥，16位VI向量，默认使用【ECB】模式和【PKCS7】填充
        /// </summary>
        /// <param name="data">要解密的数据【字节数组】</param>
        /// <param name="key">密钥，最大32Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(32)生成</param>
        /// <param name="iv">向量，最大16Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(16)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public byte[] DecryptBytes(byte[] data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        {
            byte[] keyBytes = new byte[32], ivBytes = new byte[16];
            Array.Copy(key.PadRight(keyBytes.Length).ToBytes(), keyBytes, keyBytes.Length);
            Array.Copy(iv.PadRight(ivBytes.Length).ToBytes(), ivBytes, ivBytes.Length);
            using Aes aes = Aes.Create();
            try
            {
                aes.Mode = mode;
                aes.Padding = padding;
                using MemoryStream memory = new MemoryStream(data);
                using CryptoStream decryptor = new CryptoStream(memory, aes.CreateDecryptor(keyBytes, ivBytes), CryptoStreamMode.Read);
                using MemoryStream result = new MemoryStream();
                byte[] buffer = new byte[1024];
                int readBytes = 0;
                while ((readBytes = decryptor.Read(buffer, 0, buffer.Length)) > 0)
                {
                    result.Write(buffer, 0, readBytes);
                }
                return result.ToArray();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 将Base64字符串进行AES解密，返回解密后的字节数组 + DecryptBytes(string data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将Base64字符串进行AES解密，返回解密后的字节数组，32位密钥，16位VI向量，默认使用【ECB】模式和【PKCS7】填充
        /// </summary>
        /// <param name="data">要解密的数据【Base64字符串】</param>
        /// <param name="key">密钥，最大32Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(32)生成</param>
        /// <param name="iv">向量，最大16Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(16)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public byte[] DecryptBytes(string data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => DecryptBytes(data.Base64ToBytes(), key, iv, mode, padding);
        #endregion

        #region 将字节数据进行AES加密，返回Base64字符串 + Encrypt(byte[] data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字节数据进行AES加密，返回Base64字符串，32位密钥，默认使用【ECB】模式和【PKCS7】填充
        /// </summary>
        /// <param name="data">要加密的数据【字节数组】</param>
        /// <param name="key">密钥，最大32Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(32)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns>返回Base64字符串</returns>
        public string Encrypt(byte[] data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => EncryptBytes(data, key, mode, padding)?.ToBase64();
        #endregion

        #region 将字符串进行AES加密，返回Base64字符串 + Encrypt(string data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字符串进行AES加密，返回Base64字符串，32位密钥，默认使用【ECB】模式和【PKCS7】填充
        /// <para>密钥和向量不够位数时，自动填充，超出时自动截取</para>
        /// </summary>
        /// <param name="data">要加密的数据【字符串】</param>
        /// <param name="key">密钥，最大32Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(32)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns>返回Base64字符串</returns>
        public string Encrypt(string data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => Encrypt(data.ToBytes(), key, mode, padding);
        #endregion

        #region 将字节数组数据进行AES加密，返回加密后的字节数组 + EncryptBytes(byte[] data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字节数组数据进行AES加密，返回加密后的字节数组，32位密钥，默认使用【ECB】模式和【PKCS7】填充
        /// </summary>
        /// <param name="data">要加密的数据【字节数组】</param>
        /// <param name="key">密钥，最大32Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(32)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public byte[] EncryptBytes(byte[] data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        {
            byte[] keyBytes = new byte[32];
            Array.Copy(key.PadRight(keyBytes.Length).ToBytes(), keyBytes, keyBytes.Length);
            using Aes aes = Aes.Create();
            try
            {
                aes.Mode = mode;
                aes.Padding = padding;
                aes.KeySize = 256;
                aes.Key = keyBytes;
                using MemoryStream memory = new MemoryStream();
                using CryptoStream Encryptor = new CryptoStream(memory, aes.CreateEncryptor(), CryptoStreamMode.Write);
                Encryptor.Write(data, 0, data.Length);
                Encryptor.FlushFinalBlock();
                return memory.ToArray();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 将字符串数据进行AES加密，返回加密后的字节数组 + EncryptBytes(string data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字符串数据进行AES加密，返回加密后的字节数组，32位密钥，默认使用【ECB】模式和【PKCS7】填充
        /// </summary>
        /// <param name="data">要加密的数据【字符串】</param>
        /// <param name="key">密钥，最大32Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(32)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public byte[] EncryptBytes(string data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => EncryptBytes(data.ToBytes(), key, mode, padding);
        #endregion

        #region 将字节数组进行AES解密，返回解密后的字符串 + Decrypt(byte[] data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字节数组进行AES解密，返回解密后的字符串，32位密钥，默认使用【ECB】模式和【PKCS7】填充
        /// </summary>
        /// <param name="data">要解密的数据【字节数组】</param>
        /// <param name="key">密钥，最大32Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(32)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public string Decrypt(byte[] data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => DecryptBytes(data, key, mode, padding)?.BytesToString();
        #endregion

        #region 将Base64字符串进行AES解密，返回解密后的字符串 + Decrypt(string data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将Base64字符串进行AES解密，返回解密后的字符串，32位密钥，默认使用【ECB】模式和【PKCS7】填充
        /// </summary>
        /// <param name="data">要解密的数据【Base64字符串】</param>
        /// <param name="key">密钥，最大32Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(32)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public string Decrypt(string data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => Decrypt(data.Base64ToBytes(), key, mode, padding);
        #endregion

        #region 将字节数组进行AES解密，返回解密后的字节数组 + DecryptBytes(byte[] data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字节数组进行AES解密，返回解密后的字节数组，32位密钥，默认使用【ECB】模式和【PKCS7】填充
        /// </summary>
        /// <param name="data">要解密的数据【字节数组】</param>
        /// <param name="key">密钥，最大32Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(32)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public byte[] DecryptBytes(byte[] data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        {
            byte[] keyBytes = new byte[32];
            Array.Copy(key.PadRight(keyBytes.Length).ToBytes(), keyBytes, keyBytes.Length);
            using Aes aes = Aes.Create();
            try
            {
                aes.Mode = mode;
                aes.Padding = padding;
                aes.KeySize = 256;
                aes.Key = keyBytes;
                using MemoryStream memory = new MemoryStream(data);
                using CryptoStream decryptor = new CryptoStream(memory, aes.CreateDecryptor(), CryptoStreamMode.Read);
                using MemoryStream result = new MemoryStream();
                byte[] buffer = new byte[1024];
                int readBytes = 0;
                while ((readBytes = decryptor.Read(buffer, 0, buffer.Length)) > 0)
                {
                    result.Write(buffer, 0, readBytes);
                }
                return result.ToArray();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 将Base64字符串进行AES解密，返回解密后的字节数组 + DecryptBytes(string data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将Base64字符串进行AES解密，返回解密后的字节数组，32位密钥，默认使用【ECB】模式和【PKCS7】填充
        /// </summary>
        /// <param name="data">要解密的数据【Base64字符串】</param>
        /// <param name="key">密钥，最大32Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(32)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public byte[] DecryptBytes(string data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => DecryptBytes(data.Base64ToBytes(), key, mode, padding);
        #endregion
    }
}
#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：DesHelper
* 类 描 述 ：Des加解密帮助类
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/06/12 15:18:20
* 更新时间 ：2020/06/12 15:18:20
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
    /// Des加密解密帮助类【对称性加密算法】
    /// <para>DES（Data Encryption Standard）：数据加密标准，速度较快，适用于加密大量数据的场合。</para>
    /// </summary>
    public class DesHelper
    {
        #region 将字节数组数据进行TirpleDES加密，返回加密后的Base64字符串 + Encrypt(byte[] data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字节数组数据进行TirpleDES加密，返回加密后的Base64字符串，24位密钥，8位IV向量，默认使用【ECB】模式和【PKCS7】填充
        /// <para>经测试当<see cref="CipherMode"/>为【CBC】或【CFB】IV向量才生效</para>
        /// </summary>
        /// <param name="data">要加密的数据【字节数组】</param>
        /// <param name="key">密钥，通常为24Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(24)生成</param>
        /// <param name="iv">向量，通常为8Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(8)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns>返回Base64字符串</returns>
        public string Encrypt(byte[] data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => EncryptBytes(data, key, iv, mode, padding)?.ToBase64();
        #endregion

        #region 将字符串数据进行TirpleDES加密，返回Base64字符串 + Encrypt(string data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字符串数据进行TirpleDES加密，返回Base64字符串，24位密钥，8位IV向量，默认使用【ECB】模式和【PKCS7】填充
        /// <para>经测试当<see cref="CipherMode"/>为【CBC】或【CFB】IV向量才生效</para>
        /// </summary>
        /// <param name="data">要加密的数据【字符串】</param>
        /// <param name="key">密钥，通常为24Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(24)生成</param>
        /// <param name="iv">向量，通常为8Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(8)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns>返回Base64字符串</returns>
        public string Encrypt(string data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => Encrypt(data.ToBytes(), key, iv, mode, padding);
        #endregion

        #region 将字节数组数据进行TirpleDES加密，返回加密后的字节数组 + EncryptBytes(byte[] data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字节数组数据进行TirpleDES加密，返回加密后的字节数组，24位密钥，8位IV向量，默认使用【ECB】模式和【PKCS7】填充
        /// <para>经测试当<see cref="CipherMode"/>为【CBC】或【CFB】IV向量才生效</para>
        /// </summary>
        /// <param name="data">要加密的数据【字节数组】</param>
        /// <param name="key">密钥，通常为24Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(24)生成</param>
        /// <param name="iv">向量，通常为8Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(8)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public byte[] EncryptBytes(byte[] data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        {
            byte[] keyBytes = new byte[24];
            Array.Copy(key.PadRight(keyBytes.Length).ToBytes(), keyBytes, keyBytes.Length);
            using var des = TripleDES.Create();
            try
            {
                des.Mode = mode;
                des.Padding = padding;
                des.Key = keyBytes;
                if (mode == CipherMode.CBC || mode == CipherMode.CFB)
                {
                    byte[] ivBytes = new byte[8];
                    Array.Copy(iv.PadRight(ivBytes.Length).ToBytes(), ivBytes, ivBytes.Length);
                    des.IV = ivBytes;
                }
                using MemoryStream memory = new MemoryStream();
                using CryptoStream encryptor = new CryptoStream(memory, des.CreateEncryptor(), CryptoStreamMode.Write);
                encryptor.Write(data, 0, data.Length);
                encryptor.FlushFinalBlock();
                return memory.ToArray();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 将字符串数据进行TirpleDES加密，返回加密后的字节数组 + EncryptBytes(string data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字符串数据进行TirpleDES加密，返回加密后的字节数组，24位密钥，8位IV向量，默认使用【ECB】模式和【PKCS7】填充
        /// <para>经测试当<see cref="CipherMode"/>为【CBC】或【CFB】IV向量才生效</para>
        /// </summary>
        /// <param name="data">要加密的数据【字符串】</param>
        /// <param name="key">密钥，通常为24Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(24)生成</param>
        /// <param name="iv">向量，通常为8Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(8)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public byte[] EncryptBytes(string data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => EncryptBytes(data.ToBytes(), key, iv, mode, padding);
        #endregion

        #region 将字节数组进行TirpleDES解密，返回解密后的字符串 + Decrypt(byte[] data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字节数组进行TirpleDES解密，返回解密后的字符串，24位密钥，8位VI向量，默认使用【ECB】模式和【PKCS7】填充
        /// <para>经测试当<see cref="CipherMode"/>为【CBC】或【CFB】IV向量才生效</para>
        /// </summary>
        /// <param name="data">要解密的数据【字节数组】</param>
        /// <param name="key">密钥，通常为24Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(24)生成</param>
        /// <param name="iv">向量，通常为8Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(8)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public string Decrypt(byte[] data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => DecryptBytes(data, key, iv, mode, padding)?.BytesToString();
        #endregion

        #region 将Base64字符串进行TirpleDES解密，返回解密后的字符串 + Decrypt(string data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将Base64字符串进行TirpleDES解密，返回解密后的字符串，24位密钥，8位VI向量，默认使用【ECB】模式和【PKCS7】填充
        /// <para>经测试当<see cref="CipherMode"/>为【CBC】或【CFB】IV向量才生效</para>
        /// </summary>
        /// <param name="data">要解密的数据【Base64字符串】</param>
        /// <param name="key">密钥，通常为24Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(24)生成</param>
        /// <param name="iv">向量，通常为8Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(8)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public string Decrypt(string data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => Decrypt(data.Base64ToBytes(), key, iv, mode, padding);
        #endregion

        #region 将字节数组进行TirpleDES解密，返回解密后的字节数组 + DecryptBytes(byte[] data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字节数组进行TirpleDES解密，返回解密后的字节数组，24位密钥，8位VI向量，默认使用【ECB】模式和【PKCS7】填充
        /// <para>经测试当<see cref="CipherMode"/>为【CBC】或【CFB】IV向量才生效</para>
        /// </summary>
        /// <param name="data">要解密的数据【字节数组】</param>
        /// <param name="key">密钥，通常为24Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(24)生成</param>
        /// <param name="iv">向量，通常为8Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(8)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public byte[] DecryptBytes(byte[] data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        {
            byte[] keyBytes = new byte[24];
            Array.Copy(key.PadRight(keyBytes.Length).ToBytes(), keyBytes, keyBytes.Length);
            using var des = TripleDES.Create();
            try
            {
                des.Mode = mode;
                des.Padding = padding;
                des.Key = keyBytes;
                if (mode == CipherMode.CBC || mode == CipherMode.CFB)
                {
                    byte[] ivBytes = new byte[8];
                    Array.Copy(iv.PadRight(ivBytes.Length).ToBytes(), ivBytes, ivBytes.Length);
                    des.IV = ivBytes;
                }
                using MemoryStream memory = new MemoryStream(data);
                using CryptoStream decryptor = new CryptoStream(memory, des.CreateDecryptor(), CryptoStreamMode.Read);
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

        #region 将Base64字符串进行TirpleDES解密，返回解密后的字节数组 + DecryptBytes(string data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将Base64字符串进行TirpleDES解密，返回解密后的字节数组，24位密钥，8位VI向量，默认使用【ECB】模式和【PKCS7】填充
        /// <para>经测试当<see cref="CipherMode"/>为【CBC】或【CFB】IV向量才生效</para>
        /// </summary>
        /// <param name="data">要解密的数据【Base64字符串】</param>
        /// <param name="key">密钥，通常为24Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(24)生成</param>
        /// <param name="iv">向量，通常为8Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(8)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public byte[] DecryptBytes(string data, string key, string iv, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => DecryptBytes(data.Base64ToBytes(), key, iv, mode, padding);
        #endregion

        #region 将字节数组数据进行TirpleDES加密，返回加密后的Base64字符串 + Encrypt(byte[] data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字节数组数据进行TirpleDES加密，返回加密后的Base64字符串，24位密钥，默认使用【ECB】模式和【PKCS7】填充
        /// <para>经测试当<see cref="CipherMode"/>为【CBC】或【CFB】IV向量才生效</para>
        /// </summary>
        /// <param name="data">要加密的数据【字节数组】</param>
        /// <param name="key">密钥，通常为24Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(24)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns>返回Base64字符串</returns>
        public string Encrypt(byte[] data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => EncryptBytes(data, key, mode, padding)?.ToBase64();
        #endregion

        #region 将字符串数据进行TirpleDES加密，返回加密后的Base64字符串 + Encrypt(string data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字符串数据进行TirpleDES加密，返回加密后的Base64字符串，24位密钥，默认使用【ECB】模式和【PKCS7】填充
        /// <para>经测试当<see cref="CipherMode"/>为【CBC】或【CFB】IV向量才生效</para>
        /// </summary>
        /// <param name="data">要加密的数据【字符串】</param>
        /// <param name="key">密钥，通常为24Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(24)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns>返回Base64字符串</returns>
        public string Encrypt(string data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => Encrypt(data.ToBytes(), key, mode, padding);
        #endregion

        #region 将字节数组数据进行TirpleDES加密，返回加密后的字节数组 + EncryptBytes(byte[] data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字节数组数据进行TirpleDES加密，返回加密后的字节数组，24位密钥，默认使用【ECB】模式和【PKCS7】填充
        /// <para>经测试当<see cref="CipherMode"/>为【CBC】或【CFB】IV向量才生效</para>
        /// </summary>
        /// <param name="data">要加密的数据【字节数组】</param>
        /// <param name="key">密钥，通常为24Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(24)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public byte[] EncryptBytes(byte[] data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        {
            byte[] keyBytes = new byte[24];
            Array.Copy(key.PadRight(keyBytes.Length).ToBytes(), keyBytes, keyBytes.Length);
            using var des = TripleDES.Create();
            try
            {
                des.Mode = mode;
                des.Padding = padding;
                des.Key = keyBytes;
                using MemoryStream memory = new MemoryStream();
                using CryptoStream encryptor = new CryptoStream(memory, des.CreateEncryptor(), CryptoStreamMode.Write);
                encryptor.Write(data, 0, data.Length);
                encryptor.FlushFinalBlock();
                return memory.ToArray();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 将字符串数据进行TirpleDES加密，返回加密后的字节数组 + EncryptBytes(string data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字符串数据进行TirpleDES加密，返回加密后的字节数组，24位密钥，默认使用【ECB】模式和【PKCS7】填充
        /// <para>经测试当<see cref="CipherMode"/>为【CBC】或【CFB】IV向量才生效</para>
        /// </summary>
        /// <param name="data">要加密的数据【字符串】</param>
        /// <param name="key">密钥，通常为24Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(24)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public byte[] EncryptBytes(string data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => EncryptBytes(data.ToBytes(), key, mode, padding);
        #endregion

        #region 将字节数组进行TirpleDES解密，返回解密后的字符串 + Decrypt(byte[] data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字节数组进行TirpleDES解密，返回解密后的字符串，24位密钥，默认使用【ECB】模式和【PKCS7】填充
        /// <para>经测试当<see cref="CipherMode"/>为【CBC】或【CFB】IV向量才生效</para>
        /// </summary>
        /// <param name="data">要解密的数据【字节数组】</param>
        /// <param name="key">密钥，通常为24Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(24)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public string Decrypt(byte[] data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => DecryptBytes(data, key, mode, padding)?.BytesToString();
        #endregion

        #region 将Base64字符串进行TirpleDES解密，返回解密后的字符串 + Decrypt(string data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将Base64字符串进行TirpleDES解密，返回解密后的字符串，24位密钥，默认使用【ECB】模式和【PKCS7】填充
        /// <para>经测试当<see cref="CipherMode"/>为【CBC】或【CFB】IV向量才生效</para>
        /// </summary>
        /// <param name="data">要解密的数据【Base64字符串】</param>
        /// <param name="key">密钥，通常为24Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(24)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public string Decrypt(string data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => Decrypt(data.Base64ToBytes(), key, mode, padding);
        #endregion

        #region 将字节数组进行TirpleDES解密，返回解密后的字节数组 + DecryptBytes(byte[] data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将字节数组进行TirpleDES解密，返回解密后的字节数组，24位密钥，默认使用【ECB】模式和【PKCS7】填充
        /// <para>经测试当<see cref="CipherMode"/>为【CBC】或【CFB】IV向量才生效</para>
        /// </summary>
        /// <param name="data">要解密的数据【字节数组】</param>
        /// <param name="key">密钥，通常为24Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(24)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public byte[] DecryptBytes(byte[] data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        {
            byte[] keyBytes = new byte[24];
            Array.Copy(key.PadRight(keyBytes.Length).ToBytes(), keyBytes, keyBytes.Length);
            using var des = TripleDES.Create();
            try
            {
                des.Mode = mode;
                des.Padding = padding;
                des.Key = keyBytes;
                using MemoryStream memory = new MemoryStream(data);
                using CryptoStream decryptor = new CryptoStream(memory, des.CreateDecryptor(), CryptoStreamMode.Read);
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

        #region 将Base64字符串进行TirpleDES解密，返回解密后的字节数组 + DecryptBytes(string data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        /// <summary>
        /// 将Base64字符串进行TirpleDES解密，返回解密后的字节数组，24位密钥，默认使用【ECB】模式和【PKCS7】填充
        /// <para>经测试当<see cref="CipherMode"/>为【CBC】或【CFB】IV向量才生效</para>
        /// </summary>
        /// <param name="data">要解密的数据【Base64字符串】</param>
        /// <param name="key">密钥，通常为24Bit,可由<see cref="RandomHelper.RandCode(int, RandMethod, char[])"/>RandCode(24)生成</param>
        /// <param name="mode">分组加密的模式，默认使用【ECB】</param>
        /// <param name="padding">填充方式，默认使用【PKCS7】</param>
        /// <returns></returns>
        public byte[] DecryptBytes(string data, string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
            => DecryptBytes(data.Base64ToBytes(), key, mode, padding);
        #endregion
    }
}
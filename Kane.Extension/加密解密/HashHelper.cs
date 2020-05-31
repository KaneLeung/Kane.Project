#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：HashHelper
* 类 描 述 ：哈希【Hash】算法帮助类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/5/30 15:46:46
* 更新时间 ：2020/5/30 15:46:46
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Kane.Extension
{
    /// <summary>
    /// 哈希【Hash】散列算法（签名算法）有：MD5、SHA1、HMAC
    /// <para>用途：主要用于验证，防止信息被修。具体用途如：文件校验、数字签名、鉴权协议</para>
    /// <para>【MD5】：MD5是一种不可逆的加密算法，目前是最牢靠的加密算法之一，尚没有能够逆运算的程序被开发出来，它对应任何字符串都可以加密成一段唯一的固定长度的代码。</para>
    /// <para>【SHA1】：是由NISTNSA设计为同DSA一起使用的，它对长度小于264的输入，产生长度为160bit的散列值，因此抗穷举(brute-force)性更好。SHA-1设计时基于和MD4相同原理,并且模仿了该算法。</para>
    /// <para>         SHA-1是由美国标准技术局（NIST）颁布的国家标准，是一种应用最为广泛的Hash函数算法，也是目前最先进的加密技术，被政府部门和私营业主用来处理敏感的信息。而SHA-1基于MD5，MD5又基于MD4。</para>
    /// <para>【HMAC】：是密钥相关的哈希运算消息认证码（Hash-based Message Authentication Code）,HMAC运算利用哈希算法，以一个密钥和一个消息为输入，生成一个消息摘要作为输出。</para>
    /// <para>         也就是说HMAC是需要一个密钥的。所以，HMAC_SHA1也是需要一个密钥的，而SHA1不需要。</para>
    /// </summary>
    public class HashHelper
    {
        #region 字节数组【Md5】哈希化为字节数组 + Md5Bytes(byte[] data)
        /// <summary>
        /// 字节数组【Md5】哈希化为字节数组
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <returns></returns>
        public byte[] Md5Bytes(byte[] data)
        {
            using var md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(data);
        }
        #endregion

        #region 字符串【Md5】哈希化为字节数组，默认使用【UTF8】编码 + Md5Bytes(string data)
        /// <summary>
        /// 字符串【Md5】哈希化为字节数组，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <returns></returns>
        public byte[] Md5Bytes(string data) => Md5Bytes(data, Encoding.UTF8);
        #endregion

        #region 字符串【Md5】哈希化为字节数组，可设置编码 + Md5Bytes(string data, Encoding encoding)
        /// <summary>
        /// 字符串【Md5】哈希化为字节数组，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <returns></returns>
        public byte[] Md5Bytes(string data, Encoding encoding) => Md5Bytes(encoding.GetBytes(data));
        #endregion

        #region 字节数组【Md5】哈希化为字符串 + Md5(byte[] data)
        /// <summary>
        /// 字节数组【Md5】哈希化为字符串
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <returns>全小写</returns>
        public string Md5(byte[] data) => string.Concat(Md5Bytes(data).Select(k => k.ToString("x2")));
        #endregion

        #region 字符串【Md5】哈希化为字符串，默认使用【UTF8】编码 + Md5(string data)
        /// <summary>
        /// 字符串【Md5】哈希化为字符串，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <returns>全小写</returns>
        public string Md5(string data) => Md5(data, Encoding.UTF8);
        #endregion

        #region 字符串【Md5】哈希化为字符串，可设置编码 + Md5(string data, Encoding encoding)
        /// <summary>
        /// 字符串【Md5】哈希化为字符串，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <returns>全小写</returns>
        public string Md5(string data, Encoding encoding) => Md5(encoding.GetBytes(data));
        #endregion

        #region 字符串加盐后【Md5】哈希化为字符串，默认使用【UTF8】编码 + Md5(string data, string salt)
        /// <summary>
        /// 字符串加盐后【Md5】哈希化为字符串，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="salt">盐值字符串</param>
        /// <returns>全小写</returns>
        public string Md5(string data, string salt) => Md5(data, salt, Encoding.UTF8);
        #endregion

        #region 字符串加盐后【Md5】哈希化为字符串，可设置编码 + Md5(string data, string salt, Encoding encoding)
        /// <summary>
        /// 字符串加盐后【Md5】哈希化为字符串，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="salt">盐值字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <returns>全小写</returns>
        public string Md5(string data, string salt, Encoding encoding) => Md5(data.Add(salt), encoding);
        #endregion


        #region 字节数组【Sha1】哈希化为字节数组 + Sha1Bytes(byte[] data)
        /// <summary>
        /// 字节数组【Sha1】哈希化为字节数组
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <returns></returns>
        public byte[] Sha1Bytes(byte[] data)
        {
            using SHA1 sha = SHA1.Create();
            return sha.ComputeHash(data);
        }
        #endregion

        #region 字符串【Sha1】哈希化为字节数组，默认使用【UTF8】编码 + Sha1Bytes(string data)
        /// <summary>
        /// 字符串【Sha1】哈希化为字节数组，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <returns></returns>
        public byte[] Sha1Bytes(string data) => Sha1Bytes(Encoding.UTF8.GetBytes(data));
        #endregion

        #region 字符串【Sha1】哈希化为字节数组，可设置编码 + Sha1Bytes(string data, Encoding encoding)
        /// <summary>
        /// 字符串【Sha1】哈希化为字节数组，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <returns></returns>
        public byte[] Sha1Bytes(string data, Encoding encoding) => Sha1Bytes(encoding.GetBytes(data));
        #endregion

        #region 字节数组【Sha1】哈希化为字符串 + Sha1(byte[] data)
        /// <summary>
        /// 字节数组【Sha1】哈希化为字符串
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <returns>全小写</returns>
        public string Sha1(byte[] data) => string.Concat(Sha1Bytes(data).Select(k => k.ToString("x2")));
        #endregion

        #region 字符串【Sha1】哈希化为字符串，默认使用【UTF8】编码 + Sha1(string data)
        /// <summary>
        /// 字符串【Sha1】哈希化为字符串，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <returns>全小写</returns>
        public string Sha1(string data) => Sha1(Encoding.UTF8.GetBytes(data));
        #endregion

        #region 字符串【Sha1】哈希化为字符串，可设置编码 + Sha1(string data, Encoding encoding)
        /// <summary>
        /// 字符串【Sha1】哈希化为字符串，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <returns>全小写</returns>
        public string Sha1(string data, Encoding encoding) => Sha1(encoding.GetBytes(data));
        #endregion

        #region 字符串加盐后【Sha1】哈希化为字符串，默认使用【UTF8】编码 + Sha1(string data, string salt)
        /// <summary>
        /// 字符串加盐后【Sha1】哈希化为字符串，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="salt">盐值字符串</param>
        /// <returns>全小写</returns>
        public string Sha1(string data, string salt) => Sha1(data, salt, Encoding.UTF8);
        #endregion

        #region 字符串加盐后【Sha1】哈希化为字符串，可设置编码 + Sha1(string data, string salt, Encoding encoding)
        /// <summary>
        /// 字符串加盐后【Sha1】哈希化为字符串，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="salt">盐值字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <returns>全小写</returns>
        public string Sha1(string data, string salt, Encoding encoding) => Sha1(data.Add(salt), encoding);
        #endregion


        #region 字节数组【Sha256】哈希化为字节数组 + Sha256Bytes(byte[] data)
        /// <summary>
        /// 字节数组【Sha256】哈希化为字节数组
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <returns></returns>
        public byte[] Sha256Bytes(byte[] data)
        {
            using SHA256 sha = SHA256.Create();
            return sha.ComputeHash(data);
        }
        #endregion

        #region 字符串【Sha256】哈希化为字节数组，默认使用【UTF8】编码 + Sha256Bytes(string data)
        /// <summary>
        /// 字符串【Sha256】哈希化为字节数组，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <returns></returns>
        public byte[] Sha256Bytes(string data) => Sha256Bytes(Encoding.UTF8.GetBytes(data));
        #endregion

        #region 字符串【Sha256】哈希化为字节数组，可设置编码 + Sha256Bytes(string data, Encoding encoding)
        /// <summary>
        /// 字符串【Sha256】哈希化为字节数组，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <returns></returns>
        public byte[] Sha256Bytes(string data, Encoding encoding) => Sha256Bytes(encoding.GetBytes(data));
        #endregion

        #region 字节数组【Sha256】哈希化为字符串 + Sha256(byte[] data)
        /// <summary>
        /// 字节数组【Sha256】哈希化为字符串
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <returns>全小写</returns>
        public string Sha256(byte[] data) => string.Concat(Sha256Bytes(data).Select(k => k.ToString("x2")));
        #endregion

        #region 字符串【Sha256】哈希化为字符串，默认使用【UTF8】编码 + Sha256(string data)
        /// <summary>
        /// 字符串【Sha256】哈希化为字符串，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <returns>全小写</returns>
        public string Sha256(string data) => Sha256(Encoding.UTF8.GetBytes(data));
        #endregion

        #region 字符串【Sha256】哈希化为字符串，可设置编码 + Sha256(string data, Encoding encoding)
        /// <summary>
        /// 字符串【Sha256】哈希化为字符串，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <returns>全小写</returns>
        public string Sha256(string data, Encoding encoding) => Sha256(encoding.GetBytes(data));
        #endregion

        #region 字符串加盐后【Sha256】哈希化为字符串，默认使用【UTF8】编码 + Sha256(string data, string salt)
        /// <summary>
        /// 字符串加盐后【Sha256】哈希化为字符串，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="salt">盐值字符串</param>
        /// <returns>全小写</returns>
        public string Sha256(string data, string salt) => Sha256(data, salt, Encoding.UTF8);
        #endregion

        #region 字符串加盐后【Sha256】哈希化为字符串，可设置编码 + Sha256(string data, string salt, Encoding encoding)
        /// <summary>
        /// 字符串加盐后【Sha256】哈希化为字符串，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="salt">盐值字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <returns>全小写</returns>
        public string Sha256(string data, string salt, Encoding encoding) => Sha256(data.Add(salt), encoding);
        #endregion


        #region 字节数组【Sha384】哈希化为字节数组 + Sha384Bytes(byte[] data)
        /// <summary>
        /// 字节数组【Sha384】哈希化为字节数组
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <returns></returns>
        public byte[] Sha384Bytes(byte[] data)
        {
            using SHA384 sha = SHA384.Create();
            return sha.ComputeHash(data);
        }
        #endregion

        #region 字符串【Sha384】哈希化为字节数组，默认使用【UTF8】编码 + Sha384Bytes(string data)
        /// <summary>
        /// 字符串【Sha384】哈希化为字节数组，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <returns></returns>
        public byte[] Sha384Bytes(string data) => Sha384Bytes(Encoding.UTF8.GetBytes(data));
        #endregion

        #region 字符串【Sha384】哈希化为字节数组，可设置编码 + Sha384Bytes(string data, Encoding encoding)
        /// <summary>
        /// 字符串【Sha384】哈希化为字节数组，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <returns></returns>
        public byte[] Sha384Bytes(string data, Encoding encoding) => Sha384Bytes(encoding.GetBytes(data));
        #endregion

        #region 字节数组【Sha384】哈希化为字符串 + Sha384(byte[] data)
        /// <summary>
        /// 字节数组【Sha384】哈希化为字符串
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <returns>全小写</returns>
        public string Sha384(byte[] data) => string.Concat(Sha384Bytes(data).Select(k => k.ToString("x2")));
        #endregion

        #region 字符串【Sha384】哈希化为字符串，默认使用【UTF8】编码 + Sha384(string data)
        /// <summary>
        /// 字符串【Sha384】哈希化为字符串，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <returns>全小写</returns>
        public string Sha384(string data) => Sha384(Encoding.UTF8.GetBytes(data));
        #endregion

        #region 字符串【Sha384】哈希化为字符串，可设置编码 + Sha384(string data, Encoding encoding)
        /// <summary>
        /// 字符串【Sha384】哈希化为字符串，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <returns>全小写</returns>
        public string Sha384(string data, Encoding encoding) => Sha384(encoding.GetBytes(data));
        #endregion

        #region 字符串加盐后【Sha384】哈希化为字符串，默认使用【UTF8】编码 + Sha384(string data, string salt)
        /// <summary>
        /// 字符串加盐后【Sha384】哈希化为字符串，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="salt">盐值字符串</param>
        /// <returns>全小写</returns>
        public string Sha384(string data, string salt) => Sha384(data, salt, Encoding.UTF8);
        #endregion

        #region 字符串加盐后【Sha384】哈希化为字符串，可设置编码 + Sha384(string data, string salt, Encoding encoding)
        /// <summary>
        /// 字符串加盐后【Sha384】哈希化为字符串，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="salt">盐值字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <returns>全小写</returns>
        public string Sha384(string data, string salt, Encoding encoding) => Sha384(data.Add(salt), encoding);
        #endregion


        #region 字节数组【Sha512】哈希化为字节数组 + Sha512Bytes(byte[] data)
        /// <summary>
        /// 字节数组【Sha512】哈希化为字节数组
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <returns></returns>
        public byte[] Sha512Bytes(byte[] data)
        {
            using SHA512 sha = SHA512.Create();
            return sha.ComputeHash(data);
        }
        #endregion

        #region 字符串【Sha512】哈希化为字节数组，默认使用【UTF8】编码 + Sha512Bytes(string data)
        /// <summary>
        /// 字符串【Sha512】哈希化为字节数组，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <returns></returns>
        public byte[] Sha512Bytes(string data) => Sha512Bytes(Encoding.UTF8.GetBytes(data));
        #endregion

        #region 字符串【Sha512】哈希化为字节数组，可设置编码 + Sha512Bytes(string data, Encoding encoding)
        /// <summary>
        /// 字符串【Sha512】哈希化为字节数组，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <returns></returns>
        public byte[] Sha512Bytes(string data, Encoding encoding) => Sha512Bytes(encoding.GetBytes(data));
        #endregion

        #region 字节数组【Sha512】哈希化为字符串 + Sha512(byte[] data)
        /// <summary>
        /// 字节数组【Sha512】哈希化为字符串
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <returns>全小写</returns>
        public string Sha512(byte[] data) => string.Concat(Sha512Bytes(data).Select(k => k.ToString("x2")));
        #endregion

        #region 字符串【Sha512】哈希化为字符串，默认使用【UTF8】编码 + Sha512(string data)
        /// <summary>
        /// 字符串【Sha512】哈希化为字符串，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <returns>全小写</returns>
        public string Sha512(string data) => Sha512(Encoding.UTF8.GetBytes(data));
        #endregion

        #region 字符串【Sha512】哈希化为字符串，可设置编码 + Sha512(string data, Encoding encoding)
        /// <summary>
        /// 字符串【Sha512】哈希化为字符串，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <returns>全小写</returns>
        public string Sha512(string data, Encoding encoding) => Sha512(encoding.GetBytes(data));
        #endregion

        #region 字符串加盐后【Sha512】哈希化为字符串，默认使用【UTF8】编码 + Sha512(string data, string salt)
        /// <summary>
        /// 字符串加盐后【Sha512】哈希化为字符串，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="salt">盐值字符串</param>
        /// <returns>全小写</returns>
        public string Sha512(string data, string salt) => Sha512(data, salt, Encoding.UTF8);
        #endregion

        #region 字符串加盐后【Sha512】哈希化为字符串，可设置编码 + Sha512(string data, string salt, Encoding encoding)
        /// <summary>
        /// 字符串加盐后【Sha512】哈希化为字符串，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="salt">盐值字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <returns>全小写</returns>
        public string Sha512(string data, string salt, Encoding encoding) => Sha512(data.Add(salt), encoding);
        #endregion


        #region 字节数组【HmacMd5】哈希化为字节数组，可设置【密钥】或为空 + HmacMd5Bytes(byte[] data, byte[] key = null)
        /// <summary>
        /// 字节数组【HmacMd5】哈希化为字节数组，可设置【密钥】或为空
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <returns></returns>
        public byte[] HmacMd5Bytes(byte[] data, byte[] key = null)
        {
            using HMACMD5 md5 = new HMACMD5(key ?? new byte[] { });
            return md5.ComputeHash(data);
        }
        #endregion

        #region 字节数组【HmacMd5】哈希化为字节数组，可设置【密钥】，默认使用【UTF8】编码 + HmacMd5Bytes(byte[] data, string key)
        /// <summary>
        /// 字节数组【HmacMd5】哈希化为字节数组，可设置【密钥】，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <returns></returns>
        public byte[] HmacMd5Bytes(byte[] data, string key) => HmacMd5Bytes(data, Encoding.UTF8.GetBytes(key));
        #endregion

        #region 字节数组【HmacMd5】哈希化为字节数组，可设置【密钥】，可设置编码 + HmacMd5Bytes(byte[] data, string key, Encoding encoding)
        /// <summary>
        /// 字节数组【HmacMd5】哈希化为字节数组，可设置【密钥】，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <param name="encoding">设置编码</param>
        /// <returns></returns>
        public byte[] HmacMd5Bytes(byte[] data, string key, Encoding encoding) => HmacMd5Bytes(data, encoding.GetBytes(key));
        #endregion

        #region 字符串【HmacMd5】哈希化为字节数组，可设置【密钥】或为空，默认使用【UTF8】编码 + HmacMd5Bytes(string data, byte[] key = null)
        /// <summary>
        /// 字符串【HmacMd5】哈希化为字节数组，可设置【密钥】或为空，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <returns></returns>
        public byte[] HmacMd5Bytes(string data, byte[] key = null) => HmacMd5Bytes(Encoding.UTF8.GetBytes(data), key);
        #endregion

        #region 字符串【HmacMd5】哈希化为字节数组，可设置【密钥】或为空，可设置编码 + HmacMd5Bytes(string data, Encoding encoding, byte[] key = null)
        /// <summary>
        /// 字符串【HmacMd5】哈希化为字节数组，可设置【密钥】或为空，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <param name="key">加密的密钥。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <returns></returns>
        public byte[] HmacMd5Bytes(string data, Encoding encoding, byte[] key = null) => HmacMd5Bytes(encoding.GetBytes(data), key);
        #endregion

        #region 字符串【HmacMd5】哈希化为字节数组，可设置【密钥】，默认使用【UTF8】编码 + HmacMd5Bytes(string data, string key)
        /// <summary>
        /// 字符串【HmacMd5】哈希化为字节数组，可设置【密钥】，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <returns></returns>
        public byte[] HmacMd5Bytes(string data, string key) => HmacMd5Bytes(Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(key));
        #endregion

        #region 字符串【HmacMd5】哈希化为字节数组，可设置【密钥】，可设置编码 + HmacMd5Bytes(string data, string key, Encoding encoding)
        /// <summary>
        /// 字符串【HmacMd5】哈希化为字节数组，可设置【密钥】，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <param name="encoding">设置编码</param>
        /// <returns></returns>
        public byte[] HmacMd5Bytes(string data, string key, Encoding encoding) => HmacMd5Bytes(encoding.GetBytes(data), encoding.GetBytes(key));
        #endregion

        #region 字节数组【HmacMd5】哈希化为字符串，可设置【密钥】或为空 + HmacMd5(byte[] data, byte[] key = null)
        /// <summary>
        /// 字节数组【HmacMd5】哈希化为字符串，可设置【密钥】或为空
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <returns>全小写</returns>
        public string HmacMd5(byte[] data, byte[] key = null) => string.Concat(HmacMd5Bytes(data, key).Select(k => k.ToString("x2")));
        #endregion

        #region 字节数组【HmacMd5】哈希化为字符串，可设置【密钥】，默认使用【UTF8】编码 + HmacMd5(byte[] data, string key)
        /// <summary>
        /// 字节数组【HmacMd5】哈希化为字符串，可设置【密钥】，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <returns>全小写</returns>
        public string HmacMd5(byte[] data, string key) => HmacMd5(data, Encoding.UTF8.GetBytes(key));
        #endregion

        #region 字节数组【HmacMd5】哈希化为字符串，可设置【密钥】，可设置编码 + HmacMd5(byte[] data, string key, Encoding encoding)
        /// <summary>
        /// 字节数组【HmacMd5】哈希化为字符串，可设置【密钥】，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <param name="encoding">设置编码</param>
        /// <returns>全小写</returns>
        public string HmacMd5(byte[] data, string key, Encoding encoding) => HmacMd5(data, encoding.GetBytes(key));
        #endregion

        #region 字符串【HmacMd5】哈希化为字符串，可设置【密钥】或为空 + HmacMd5(string data, byte[] key = null)
        /// <summary>
        /// 字符串【HmacMd5】哈希化为字符串，可设置【密钥】或为空
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <returns>全小写</returns>
        public string HmacMd5(string data, byte[] key = null) => HmacMd5(Encoding.UTF8.GetBytes(data), key);
        #endregion

        #region 字符串【HmacMd5】哈希化为字符串，可设置【密钥】或为空，可设置编码 + HmacMd5(string data, Encoding encoding, byte[] key = null)
        /// <summary>
        /// 字符串【HmacMd5】哈希化为字符串，可设置【密钥】或为空，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <param name="key">加密的密钥。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <returns>全小写</returns>
        public string HmacMd5(string data, Encoding encoding, byte[] key = null) => HmacMd5(encoding.GetBytes(data), key);
        #endregion

        #region 字符串【HmacMd5】哈希化为字符串，可设置【密钥】，默认使用【UTF8】编码 + HmacMd5(string data, string key)
        /// <summary>
        /// 字符串【HmacMd5】哈希化为字符串，可设置【密钥】，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <returns>全小写</returns>
        public string HmacMd5(string data, string key) => HmacMd5(Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(key));
        #endregion

        #region 字符串【HmacMd5】哈希化为字符串，可设置【密钥】，可设置编码 + HmacMd5(string data, string key, Encoding encoding)
        /// <summary>
        /// 字符串【HmacMd5】哈希化为字符串，可设置【密钥】，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <param name="encoding">设置编码</param>
        /// <returns>全小写</returns>
        public string HmacMd5(string data, string key, Encoding encoding) => HmacMd5(encoding.GetBytes(data), encoding.GetBytes(key));
        #endregion


        #region 字节数组【HmacSha1】哈希化为字节数组，可设置【密钥】或为空 + HmacSha1Bytes(byte[] data, byte[] key = null)
        /// <summary>
        /// 字节数组【HmacSha1】哈希化为字节数组，可设置【密钥】或为空
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <returns></returns>
        public byte[] HmacSha1Bytes(byte[] data, byte[] key = null)
        {
            using HMACSHA1 sha = new HMACSHA1(key ?? new byte[] { });
            return sha.ComputeHash(data);
        }
        #endregion

        #region 字节数组【HmacSha1】哈希化为字节数组，可设置【密钥】，默认使用【UTF8】编码 + HmacSha1Bytes(byte[] data, string key)
        /// <summary>
        /// 字节数组【HmacSha1】哈希化为字节数组，可设置【密钥】，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <returns></returns>
        public byte[] HmacSha1Bytes(byte[] data, string key) => HmacSha1Bytes(data, Encoding.UTF8.GetBytes(key));
        #endregion

        #region 字节数组【HmacSha1】哈希化为字节数组，可设置【密钥】，可设置编码 + HmacSha1Bytes(byte[] data, string key, Encoding encoding)
        /// <summary>
        /// 字节数组【HmacSha1】哈希化为字节数组，可设置【密钥】，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <param name="encoding">设置编码</param>
        /// <returns></returns>
        public byte[] HmacSha1Bytes(byte[] data, string key, Encoding encoding) => HmacSha1Bytes(data, encoding.GetBytes(key));
        #endregion

        #region 字符串【HmacSha1】哈希化为字节数组，可设置【密钥】或为空，默认使用【UTF8】编码 + HmacSha1Bytes(string data, byte[] key = null)
        /// <summary>
        /// 字符串【HmacSha1】哈希化为字节数组，可设置【密钥】或为空，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <returns></returns>
        public byte[] HmacSha1Bytes(string data, byte[] key = null) => HmacSha1Bytes(Encoding.UTF8.GetBytes(data), key);
        #endregion

        #region 字符串【HmacSha1】哈希化为字节数组，可设置【密钥】或为空，可设置编码 + HmacSha1Bytes(string data, Encoding encoding, byte[] key = null)
        /// <summary>
        /// 字符串【HmacSha1】哈希化为字节数组，可设置【密钥】或为空，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <returns></returns>
        public byte[] HmacSha1Bytes(string data, Encoding encoding, byte[] key = null) => HmacSha1Bytes(encoding.GetBytes(data), key);
        #endregion

        #region 字符串【HmacSha1】哈希化为字节数组，可设置【密钥】，默认使用【UTF8】编码 + HmacSha1Bytes(string data, string key)
        /// <summary>
        /// 字符串【HmacSha1】哈希化为字节数组，可设置【密钥】，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <returns></returns>
        public byte[] HmacSha1Bytes(string data, string key) => HmacSha1Bytes(Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(key));
        #endregion

        #region 字符串【HmacSha1】哈希化为字节数组，可设置【密钥】，可设置编码 + HmacSha1Bytes(string data, string key, Encoding encoding)
        /// <summary>
        /// 字符串【HmacSha1】哈希化为字节数组，可设置【密钥】，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <param name="encoding">设置编码</param>
        /// <returns></returns>
        public byte[] HmacSha1Bytes(string data, string key, Encoding encoding) => HmacSha1Bytes(encoding.GetBytes(data), encoding.GetBytes(key));
        #endregion

        #region 字节数组【HmacSha1】哈希化为字符串，可设置【密钥】或为空 + HmacSha1(byte[] data, byte[] key = null)
        /// <summary>
        /// 字节数组【HmacSha1】哈希化为字符串，可设置【密钥】或为空
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <returns>全小写</returns>
        public string HmacSha1(byte[] data, byte[] key = null) => string.Concat(HmacSha1Bytes(data, key).Select(k => k.ToString("x2")));
        #endregion

        #region 字节数组【HmacSha1】哈希化为字符串，可设置【密钥】，默认使用【UTF8】编码 + HmacSha1(byte[] data, string key)
        /// <summary>
        /// 字节数组【HmacSha1】哈希化为字符串，可设置【密钥】，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <returns>全小写</returns>
        public string HmacSha1(byte[] data, string key) => HmacSha1(data, Encoding.UTF8.GetBytes(key));
        #endregion

        #region 字节数组【HmacSha1】哈希化为字符串，可设置【密钥】，可设置编码 + HmacSha1(byte[] data, string key, Encoding encoding)
        /// <summary>
        /// 字节数组【HmacSha1】哈希化为字符串，可设置【密钥】，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <param name="encoding">设置编码</param>
        /// <returns>全小写</returns>
        public string HmacSha1(byte[] data, string key, Encoding encoding) => HmacSha1(data, encoding.GetBytes(key));
        #endregion

        #region 字符串【HmacSha1】哈希化为字符串，可设置【密钥】或为空 + HmacSha1(string data, byte[] key = null)
        /// <summary>
        /// 字符串【HmacSha1】哈希化为字符串，可设置【密钥】或为空
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <returns>全小写</returns>
        public string HmacSha1(string data, byte[] key = null) => HmacSha1(Encoding.UTF8.GetBytes(data), key);
        #endregion

        #region 字符串【HmacSha1】哈希化为字符串，可设置【密钥】或为空，可设置编码 + HmacSha1(string data, Encoding encoding, byte[] key = null)
        /// <summary>
        /// 字符串【HmacSha1】哈希化为字符串，可设置【密钥】或为空，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <returns>全小写</returns>
        public string HmacSha1(string data, Encoding encoding, byte[] key = null) => HmacSha1(encoding.GetBytes(data), key);
        #endregion

        #region 字符串【HmacSha1】哈希化为字符串，可设置【密钥】，默认使用【UTF8】编码 + HmacSha1(string data, string key)
        /// <summary>
        /// 字符串【HmacSha1】哈希化为字符串，可设置【密钥】，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <returns>全小写</returns>
        public string HmacSha1(string data, string key) => HmacSha1(Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(key));
        #endregion

        #region 字符串【HmacSha1】哈希化为字符串，可设置【密钥】，可设置编码 + HmacSha1(string data, string key, Encoding encoding)
        /// <summary>
        /// 字符串【HmacSha1】哈希化为字符串，可设置【密钥】，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用SHA-1），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。</param>
        /// <param name="encoding">设置编码</param>
        /// <returns>全小写</returns>
        public string HmacSha1(string data, string key, Encoding encoding) => HmacSha1(encoding.GetBytes(data), encoding.GetBytes(key));
        #endregion


        #region 字节数组【HmacSha256】哈希化为字节数组，可设置【密钥】或为空 + HmacSha256Bytes(byte[] data, byte[] key = null)
        /// <summary>
        /// 字节数组【HmacSha256】哈希化为字节数组，可设置【密钥】或为空
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用 SHA-256），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。如果少于 64 个字节，就填充到 64 个字节</param>
        /// <returns></returns>
        public byte[] HmacSha256Bytes(byte[] data, byte[] key = null)
        {
            using HMACSHA256 sha = new HMACSHA256(key ?? new byte[] { });
            return sha.ComputeHash(data);
        }
        #endregion

        #region 字节数组【HmacSha256】哈希化为字节数组，可设置【密钥】，默认使用【UTF8】编码 + HmacSha256Bytes(byte[] data, string key)
        /// <summary>
        /// 字节数组【HmacSha256】哈希化为字节数组，可设置【密钥】，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用 SHA-256），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。如果少于 64 个字节，就填充到 64 个字节</param>
        /// <returns></returns>
        public byte[] HmacSha256Bytes(byte[] data, string key) => HmacSha256Bytes(data, Encoding.UTF8.GetBytes(key));
        #endregion

        #region 字节数组【HmacSha256】哈希化为字节数组，可设置【密钥】，可设置编码 + HmacSha256Bytes(byte[] data, string key, Encoding encoding)
        /// <summary>
        /// 字节数组【HmacSha256】哈希化为字节数组，可设置【密钥】，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用 SHA-256），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。如果少于 64 个字节，就填充到 64 个字节</param>
        /// <param name="encoding">设置编码</param>
        /// <returns></returns>
        public byte[] HmacSha256Bytes(byte[] data, string key, Encoding encoding) => HmacSha256Bytes(data, encoding.GetBytes(key));
        #endregion

        #region 字符串【HmacSha256】哈希化为字节数组，可设置【密钥】或为空，默认使用【UTF8】编码 + HmacSha256Bytes(string data, byte[] key = null)
        /// <summary>
        /// 字符串【HmacSha256】哈希化为字节数组，可设置【密钥】或为空，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用 SHA-256），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。如果少于 64 个字节，就填充到 64 个字节</param>
        /// <returns></returns>
        public byte[] HmacSha256Bytes(string data, byte[] key = null) => HmacSha256Bytes(Encoding.UTF8.GetBytes(data), key);
        #endregion

        #region 字符串【HmacSha256】哈希化为字节数组，可设置【密钥】或为空，可设置编码 + HmacSha256Bytes(string data, Encoding encoding, byte[] key = null)
        /// <summary>
        /// 字符串【HmacSha256】哈希化为字节数组，可设置【密钥】或为空，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用 SHA-256），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。如果少于 64 个字节，就填充到 64 个字节</param>
        /// <returns></returns>
        public byte[] HmacSha256Bytes(string data, Encoding encoding, byte[] key = null) => HmacSha256Bytes(encoding.GetBytes(data), key);
        #endregion

        #region 字符串【HmacSha256】哈希化为字节数组，可设置【密钥】，默认使用【UTF8】编码 + HmacSha256Bytes(string data, string key)
        /// <summary>
        /// 字符串【HmacSha256】哈希化为字节数组，可设置【密钥】，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用 SHA-256），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。如果少于 64 个字节，就填充到 64 个字节</param>
        /// <returns></returns>
        public byte[] HmacSha256Bytes(string data, string key) => HmacSha256Bytes(Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(key));
        #endregion

        #region 字符串【HmacSha256】哈希化为字节数组，可设置【密钥】，可设置编码 + HmacSha256Bytes(string data, string key, Encoding encoding)
        /// <summary>
        /// 字符串【HmacSha256】哈希化为字节数组，可设置【密钥】，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用 SHA-256），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。如果少于 64 个字节，就填充到 64 个字节</param>
        /// <param name="encoding">设置编码</param>
        /// <returns></returns>
        public byte[] HmacSha256Bytes(string data, string key, Encoding encoding) => HmacSha256Bytes(encoding.GetBytes(data), encoding.GetBytes(key));
        #endregion

        #region 字节数组【HmacSha256】哈希化为字符串，可设置【密钥】或为空 + HmacSha256(byte[] data, byte[] key = null)
        /// <summary>
        /// 字节数组【HmacSha256】哈希化为字符串，可设置【密钥】或为空
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用 SHA-256），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。如果少于 64 个字节，就填充到 64 个字节</param>
        /// <returns>全小写</returns>
        public string HmacSha256(byte[] data, byte[] key = null) => string.Concat(HmacSha256Bytes(data, key).Select(k => k.ToString("x2")));
        #endregion

        #region 字节数组【HmacSha256】哈希化为字符串，可设置【密钥】，默认使用【UTF8】编码 + HmacSha256(byte[] data, string key)
        /// <summary>
        /// 字节数组【HmacSha256】哈希化为字符串，可设置【密钥】，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用 SHA-256），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。如果少于 64 个字节，就填充到 64 个字节</param>
        /// <returns>全小写</returns>
        public string HmacSha256(byte[] data, string key) => HmacSha256(data, Encoding.UTF8.GetBytes(key));
        #endregion

        #region 字节数组【HmacSha256】哈希化为字符串，可设置【密钥】，可设置编码 + HmacSha256(byte[] data, string key, Encoding encoding)
        /// <summary>
        /// 字节数组【HmacSha256】哈希化为字符串，可设置【密钥】，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用 SHA-256），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。如果少于 64 个字节，就填充到 64 个字节</param>
        /// <param name="encoding">设置编码</param>
        /// <returns>全小写</returns>
        public string HmacSha256(byte[] data, string key, Encoding encoding) => HmacSha256(data, encoding.GetBytes(key));
        #endregion

        #region 字符串【HmacSha256】哈希化为字符串，可设置【密钥】或为空 + HmacSha256(string data, byte[] key = null)
        /// <summary>
        /// 字符串【HmacSha256】哈希化为字符串，可设置【密钥】或为空
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用 SHA-256），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。如果少于 64 个字节，就填充到 64 个字节</param>
        /// <returns>全小写</returns>
        public string HmacSha256(string data, byte[] key = null) => HmacSha256(Encoding.UTF8.GetBytes(data), key);
        #endregion

        #region 字符串【HmacSha256】哈希化为字符串，可设置【密钥】或为空，可设置编码 + HmacSha256(string data, Encoding encoding, byte[] key = null)
        /// <summary>
        /// 字符串【HmacSha256】哈希化为字符串，可设置【密钥】或为空，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用 SHA-256），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。如果少于 64 个字节，就填充到 64 个字节</param>
        /// <returns>全小写</returns>
        public string HmacSha256(string data, Encoding encoding, byte[] key = null) => HmacSha256(encoding.GetBytes(data), key);
        #endregion

        #region 字符串【HmacSha256】哈希化为字符串，可设置【密钥】，默认使用【UTF8】编码 + HmacSha256(string data, string key)
        /// <summary>
        /// 字符串【HmacSha256】哈希化为字符串，可设置【密钥】，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用 SHA-256），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。如果少于 64 个字节，就填充到 64 个字节</param>
        /// <returns>全小写</returns>
        public string HmacSha256(string data, string key) => HmacSha256(Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(key));
        #endregion

        #region 字符串【HmacSha256】哈希化为字符串，可设置【密钥】，可设置编码 + HmacSha256(string data, string key, Encoding encoding)
        /// <summary>
        /// 字符串【HmacSha256】哈希化为字符串，可设置【密钥】，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过64个字节，就会对其进行哈希计算（使用 SHA-256），以派生一个64个字节的密钥。 因此，建议的密钥大小为 64 个字节。如果少于 64 个字节，就填充到 64 个字节</param>
        /// <param name="encoding">设置编码</param>
        /// <returns>全小写</returns>
        public string HmacSha256(string data, string key, Encoding encoding) => HmacSha256(encoding.GetBytes(data), encoding.GetBytes(key));
        #endregion


        #region 字节数组【HmacSha384】哈希化为字节数组，可设置【密钥】或为空 + HmacSha384Bytes(byte[] data, byte[] key = null)
        /// <summary>
        /// 字节数组【HmacSha384】哈希化为字节数组，可设置【密钥】或为空
        /// <para>请不要用这个算法结果跟国内一些网站进行比较，大部分国内在线工具用的是cryptojs的，...很旧的版本，当时这个库本身对HS384的摘要就有问题</para>
        /// <para>详情请看 https://code.google.com/archive/p/crypto-js/issues/84 </para>
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-384），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <returns></returns>
        public byte[] HmacSha384Bytes(byte[] data, byte[] key = null)
        {
            using HMACSHA384 sha = new HMACSHA384(key ?? new byte[] { });
            return sha.ComputeHash(data);
        }
        #endregion

        #region 字节数组【HmacSha384】哈希化为字节数组，可设置【密钥】，默认使用【UTF8】编码 + HmacSha384Bytes(byte[] data, string key)
        /// <summary>
        /// 字节数组【HmacSha384】哈希化为字节数组，可设置【密钥】，默认使用【UTF8】编码
        /// <para>请不要用这个算法结果跟国内一些网站进行比较，大部分国内在线工具用的是cryptojs的，...很旧的版本，当时这个库本身对HS384的摘要就有问题</para>
        /// <para>详情请看 https://code.google.com/archive/p/crypto-js/issues/84 </para>
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-384），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <returns></returns>
        public byte[] HmacSha384Bytes(byte[] data, string key) => HmacSha384Bytes(data, Encoding.UTF8.GetBytes(key));
        #endregion

        #region 字节数组【HmacSha384】哈希化为字节数组，可设置【密钥】，可设置编码 + HmacSha384Bytes(byte[] data, string key, Encoding encoding)
        /// <summary>
        /// 字节数组【HmacSha384】哈希化为字节数组，可设置【密钥】，可设置编码
        /// <para>请不要用这个算法结果跟国内一些网站进行比较，大部分国内在线工具用的是cryptojs的，...很旧的版本，当时这个库本身对HS384的摘要就有问题</para>
        /// <para>详情请看 https://code.google.com/archive/p/crypto-js/issues/84 </para>
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-384），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <param name="encoding">设置编码</param>
        /// <returns></returns>
        public byte[] HmacSha384Bytes(byte[] data, string key, Encoding encoding) => HmacSha384Bytes(data, encoding.GetBytes(key));
        #endregion

        #region 字符串【HmacSha384】哈希化为字节数组，可设置【密钥】或为空，默认使用【UTF8】编码 + HmacSha384Bytes(string data, byte[] key = null)
        /// <summary>
        /// 字符串【HmacSha384】哈希化为字节数组，可设置【密钥】或为空，默认使用【UTF8】编码
        /// <para>请不要用这个算法结果跟国内一些网站进行比较，大部分国内在线工具用的是cryptojs的，...很旧的版本，当时这个库本身对HS384的摘要就有问题</para>
        /// <para>详情请看 https://code.google.com/archive/p/crypto-js/issues/84 </para>
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-384），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <returns></returns>
        public byte[] HmacSha384Bytes(string data, byte[] key = null) => HmacSha384Bytes(Encoding.UTF8.GetBytes(data), key);
        #endregion

        #region 字符串【HmacSha384】哈希化为字节数组，可设置【密钥】或为空，可设置编码 + HmacSha384Bytes(string data, Encoding encoding, byte[] key = null)
        /// <summary>
        /// 字符串【HmacSha384】哈希化为字节数组，可设置【密钥】或为空，可设置编码
        /// <para>请不要用这个算法结果跟国内一些网站进行比较，大部分国内在线工具用的是cryptojs的，...很旧的版本，当时这个库本身对HS384的摘要就有问题</para>
        /// <para>详情请看 https://code.google.com/archive/p/crypto-js/issues/84 </para>
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-384），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <returns></returns>
        public byte[] HmacSha384Bytes(string data, Encoding encoding, byte[] key = null) => HmacSha384Bytes(encoding.GetBytes(data), key);
        #endregion

        #region 字符串【HmacSha384】哈希化为字节数组，可设置【密钥】，默认使用【UTF8】编码 + HmacSha384Bytes(string data, string key)
        /// <summary>
        /// 字符串【HmacSha384】哈希化为字节数组，可设置【密钥】，默认使用【UTF8】编码
        /// <para>请不要用这个算法结果跟国内一些网站进行比较，大部分国内在线工具用的是cryptojs的，...很旧的版本，当时这个库本身对HS384的摘要就有问题</para>
        /// <para>详情请看 https://code.google.com/archive/p/crypto-js/issues/84 </para>
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-384），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <returns></returns>
        public byte[] HmacSha384Bytes(string data, string key) => HmacSha384Bytes(Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(key));
        #endregion

        #region 字符串【HmacSha384】哈希化为字节数组，可设置【密钥】，可设置编码 + HmacSha384Bytes(string data, string key, Encoding encoding)
        /// <summary>
        /// 字符串【HmacSha384】哈希化为字节数组，可设置【密钥】，可设置编码
        /// <para>请不要用这个算法结果跟国内一些网站进行比较，大部分国内在线工具用的是cryptojs的，...很旧的版本，当时这个库本身对HS384的摘要就有问题</para>
        /// <para>详情请看 https://code.google.com/archive/p/crypto-js/issues/84 </para>
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-384），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <param name="encoding">设置编码</param>
        /// <returns></returns>
        public byte[] HmacSha384Bytes(string data, string key, Encoding encoding) => HmacSha384Bytes(encoding.GetBytes(data), encoding.GetBytes(key));
        #endregion

        #region 字节数组【HmacSha384】哈希化为字符串，可设置【密钥】或为空 + HmacSha384(byte[] data, byte[] key = null)
        /// <summary>
        /// 字节数组【HmacSha384】哈希化为字符串，可设置【密钥】或为空
        /// <para>请不要用这个算法结果跟国内一些网站进行比较，大部分国内在线工具用的是cryptojs的，...很旧的版本，当时这个库本身对HS384的摘要就有问题</para>
        /// <para>详情请看 https://code.google.com/archive/p/crypto-js/issues/84 </para>
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-384），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <returns>全小写</returns>
        public string HmacSha384(byte[] data, byte[] key = null) => string.Concat(HmacSha384Bytes(data, key).Select(k => k.ToString("x2")));
        #endregion

        #region 字节数组【HmacSha384】哈希化为字符串，可设置【密钥】，默认使用【UTF8】编码 + HmacSha384(byte[] data, string key)
        /// <summary>
        /// 字节数组【HmacSha384】哈希化为字符串，可设置【密钥】，默认使用【UTF8】编码
        /// <para>请不要用这个算法结果跟国内一些网站进行比较，大部分国内在线工具用的是cryptojs的，...很旧的版本，当时这个库本身对HS384的摘要就有问题</para>
        /// <para>详情请看 https://code.google.com/archive/p/crypto-js/issues/84 </para>
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-384），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <returns>全小写</returns>
        public string HmacSha384(byte[] data, string key) => HmacSha384(data, Encoding.UTF8.GetBytes(key));
        #endregion

        #region 字节数组【HmacSha384】哈希化为字符串，可设置【密钥】，可设置编码 + HmacSha384(byte[] data, string key, Encoding encoding)
        /// <summary>
        /// 字节数组【HmacSha384】哈希化为字符串，可设置【密钥】，可设置编码
        /// <para>请不要用这个算法结果跟国内一些网站进行比较，大部分国内在线工具用的是cryptojs的，...很旧的版本，当时这个库本身对HS384的摘要就有问题</para>
        /// <para>详情请看 https://code.google.com/archive/p/crypto-js/issues/84 </para>
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-384），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <param name="encoding">设置编码</param>
        /// <returns>全小写</returns>
        public string HmacSha384(byte[] data, string key, Encoding encoding) => HmacSha384(data, encoding.GetBytes(key));
        #endregion

        #region 字符串【HmacSha384】哈希化为字符串，可设置【密钥】或为空 + HmacSha384(string data, byte[] key = null)
        /// <summary>
        /// 字符串【HmacSha384】哈希化为字符串，可设置【密钥】或为空
        /// <para>请不要用这个算法结果跟国内一些网站进行比较，大部分国内在线工具用的是cryptojs的，...很旧的版本，当时这个库本身对HS384的摘要就有问题</para>
        /// <para>详情请看 https://code.google.com/archive/p/crypto-js/issues/84 </para>
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-384），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <returns>全小写</returns>
        public string HmacSha384(string data, byte[] key = null) => HmacSha384(Encoding.UTF8.GetBytes(data), key);
        #endregion

        #region 字符串【HmacSha384】哈希化为字符串，可设置【密钥】或为空，可设置编码 + HmacSha384(string data, Encoding encoding, byte[] key = null)
        /// <summary>
        /// 字符串【HmacSha384】哈希化为字符串，可设置【密钥】或为空，可设置编码
        /// <para>请不要用这个算法结果跟国内一些网站进行比较，大部分国内在线工具用的是cryptojs的，...很旧的版本，当时这个库本身对HS384的摘要就有问题</para>
        /// <para>详情请看 https://code.google.com/archive/p/crypto-js/issues/84 </para>
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-384），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <returns>全小写</returns>
        public string HmacSha384(string data, Encoding encoding, byte[] key = null) => HmacSha384(encoding.GetBytes(data), key);
        #endregion

        #region 字符串【HmacSha384】哈希化为字符串，可设置【密钥】，默认使用【UTF8】编码 + HmacSha384(string data, string key)
        /// <summary>
        /// 字符串【HmacSha384】哈希化为字符串，可设置【密钥】，默认使用【UTF8】编码
        /// <para>请不要用这个算法结果跟国内一些网站进行比较，大部分国内在线工具用的是cryptojs的，...很旧的版本，当时这个库本身对HS384的摘要就有问题</para>
        /// <para>详情请看 https://code.google.com/archive/p/crypto-js/issues/84 </para>
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-384），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <returns>全小写</returns>
        public string HmacSha384(string data, string key) => HmacSha384(Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(key));
        #endregion

        #region 字符串【HmacSha384】哈希化为字符串，可设置【密钥】，可设置编码 + HmacSha384(string data, string key, Encoding encoding)
        /// <summary>
        /// 字符串【HmacSha384】哈希化为字符串，可设置【密钥】，可设置编码
        /// <para>请不要用这个算法结果跟国内一些网站进行比较，大部分国内在线工具用的是cryptojs的，...很旧的版本，当时这个库本身对HS384的摘要就有问题</para>
        /// <para>详情请看 https://code.google.com/archive/p/crypto-js/issues/84 </para>
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-384），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <param name="encoding">设置编码</param>
        /// <returns>全小写</returns>
        public string HmacSha384(string data, string key, Encoding encoding) => HmacSha384(encoding.GetBytes(data), encoding.GetBytes(key));
        #endregion


        #region 字节数组【HmacSha512】哈希化为字节数组，可设置【密钥】或为空 + HmacSha512Bytes(byte[] data, byte[] key = null)
        /// <summary>
        /// 字节数组【HmacSha512】哈希化为字节数组，可设置【密钥】或为空
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-512），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <returns></returns>
        public byte[] HmacSha512Bytes(byte[] data, byte[] key = null)
        {
            using HMACSHA512 sha = new HMACSHA512(key ?? new byte[] { });
            return sha.ComputeHash(data);
        }
        #endregion

        #region 字节数组【HmacSha512】哈希化为字节数组，可设置【密钥】，默认使用【UTF8】编码 + HmacSha512Bytes(byte[] data, string key)
        /// <summary>
        /// 字节数组【HmacSha512】哈希化为字节数组，可设置【密钥】，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-512），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <returns></returns>
        public byte[] HmacSha512Bytes(byte[] data, string key) => HmacSha512Bytes(data, Encoding.UTF8.GetBytes(key));
        #endregion

        #region 字节数组【HmacSha512】哈希化为字节数组，可设置【密钥】，可设置编码 + HmacSha512Bytes(byte[] data, string key, Encoding encoding)
        /// <summary>
        /// 字节数组【HmacSha512】哈希化为字节数组，可设置【密钥】，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-512），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <param name="encoding">设置编码</param>
        /// <returns></returns>
        public byte[] HmacSha512Bytes(byte[] data, string key, Encoding encoding) => HmacSha512Bytes(data, encoding.GetBytes(key));
        #endregion

        #region 字符串【HmacSha512】哈希化为字节数组，可设置【密钥】或为空，默认使用【UTF8】编码 + HmacSha512Bytes(string data, byte[] key = null)
        /// <summary>
        /// 字符串【HmacSha512】哈希化为字节数组，可设置【密钥】或为空，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-512），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <returns></returns>
        public byte[] HmacSha512Bytes(string data, byte[] key = null) => HmacSha512Bytes(Encoding.UTF8.GetBytes(data), key);
        #endregion

        #region 字符串【HmacSha512】哈希化为字节数组，可设置【密钥】或为空，可设置编码 + HmacSha512Bytes(string data, Encoding encoding, byte[] key = null)
        /// <summary>
        /// 字符串【HmacSha512】哈希化为字节数组，可设置【密钥】或为空，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-512），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <returns></returns>
        public byte[] HmacSha512Bytes(string data, Encoding encoding, byte[] key = null) => HmacSha512Bytes(encoding.GetBytes(data), key);
        #endregion

        #region 字符串【HmacSha512】哈希化为字节数组，可设置【密钥】，默认使用【UTF8】编码 + HmacSha512Bytes(string data, string key)
        /// <summary>
        /// 字符串【HmacSha512】哈希化为字节数组，可设置【密钥】，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-512），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <returns></returns>
        public byte[] HmacSha512Bytes(string data, string key) => HmacSha512Bytes(Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(key));
        #endregion

        #region 字符串【HmacSha512】哈希化为字节数组，可设置【密钥】，可设置编码 + HmacSha512Bytes(string data, string key, Encoding encoding)
        /// <summary>
        /// 字符串【HmacSha512】哈希化为字节数组，可设置【密钥】，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-512），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <param name="encoding">设置编码</param>
        /// <returns></returns>
        public byte[] HmacSha512Bytes(string data, string key, Encoding encoding) => HmacSha512Bytes(encoding.GetBytes(data), encoding.GetBytes(key));
        #endregion

        #region 字节数组【HmacSha512】哈希化为字符串，可设置【密钥】或为空 + HmacSha512(byte[] data, byte[] key = null)
        /// <summary>
        /// 字节数组【HmacSha512】哈希化为字符串，可设置【密钥】或为空
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-512），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <returns>全小写</returns>
        public string HmacSha512(byte[] data, byte[] key = null) => string.Concat(HmacSha512Bytes(data, key).Select(k => k.ToString("x2")));
        #endregion

        #region 字节数组【HmacSha512】哈希化为字符串，可设置【密钥】，默认使用【UTF8】编码 + HmacSha512(byte[] data, string key)
        /// <summary>
        /// 字节数组【HmacSha512】哈希化为字符串，可设置【密钥】，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-512），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <returns>全小写</returns>
        public string HmacSha512(byte[] data, string key) => HmacSha512(data, Encoding.UTF8.GetBytes(key));
        #endregion

        #region 字节数组【HmacSha512】哈希化为字符串，可设置【密钥】，可设置编码 + HmacSha512(byte[] data, string key, Encoding encoding)
        /// <summary>
        /// 字节数组【HmacSha512】哈希化为字符串，可设置【密钥】，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字节数组</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-512），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <param name="encoding">设置编码</param>
        /// <returns>全小写</returns>
        public string HmacSha512(byte[] data, string key, Encoding encoding) => HmacSha512(data, encoding.GetBytes(key));
        #endregion

        #region 字符串【HmacSha512】哈希化为字符串，可设置【密钥】或为空 + HmacSha512(string data, byte[] key = null)
        /// <summary>
        /// 字符串【HmacSha512】哈希化为字符串，可设置【密钥】或为空
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-512），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <returns>全小写</returns>
        public string HmacSha512(string data, byte[] key = null) => HmacSha512(Encoding.UTF8.GetBytes(data), key);
        #endregion

        #region 字符串【HmacSha512】哈希化为字符串，可设置【密钥】或为空，可设置编码 + HmacSha512(string data, Encoding encoding, byte[] key = null)
        /// <summary>
        /// 字符串【HmacSha512】哈希化为字符串，可设置【密钥】或为空，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="encoding">设置编码</param>
        /// <param name="key">加密的密钥【字节数组】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-512），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <returns>全小写</returns>
        public string HmacSha512(string data, Encoding encoding, byte[] key = null) => HmacSha512(encoding.GetBytes(data), key);
        #endregion

        #region 字符串【HmacSha512】哈希化为字符串，可设置【密钥】，默认使用【UTF8】编码 + HmacSha512(string data, string key)
        /// <summary>
        /// 字符串【HmacSha512】哈希化为字符串，可设置【密钥】，默认使用【UTF8】编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-512），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <returns>全小写</returns>
        public string HmacSha512(string data, string key) => HmacSha512(Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(key));
        #endregion

        #region 字符串【HmacSha512】哈希化为字符串，可设置【密钥】，可设置编码 + HmacSha512(string data, string key, Encoding encoding)
        /// <summary>
        /// 字符串【HmacSha512】哈希化为字符串，可设置【密钥】，可设置编码
        /// </summary>
        /// <param name="data">要哈希化的字符串</param>
        /// <param name="key">加密的密钥【字符串】。 密钥的长度不限，但如果超过128个字节，就会对其进行哈希计算（使用 SHA-512），以派生一个128个字节的密钥。 因此，建议的密钥大小为 128 个字节。如果少于 128 个字节，就填充到 128 个字节</param>
        /// <param name="encoding">设置编码</param>
        /// <returns>全小写</returns>
        public string HmacSha512(string data, string key, Encoding encoding) => HmacSha512(encoding.GetBytes(data), encoding.GetBytes(key));
        #endregion
    }
}
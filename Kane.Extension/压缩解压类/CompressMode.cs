#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：CompressMethod
* 类 描 述 ：压缩方法枚举
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/12/5 0:27:55
* 更新时间 ：2020/05/05 11:27:55
* 版 本 号 ：v1.0.2.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System.ComponentModel;

namespace Kane.Extension
{
    /// <summary>
    /// 压缩算法枚举类
    /// </summary>
    public enum CompressMode
    {
        /// <summary>
        /// 不使用压缩方法
        /// </summary>
        [Description("不使用压缩")]
        None = 0,
        /// <summary>
        /// DEFLATE是同时使用了LZ77算法与哈夫曼编码（Huffman Coding）的一个无损数据压缩算法。
        /// </summary>
        [Description("Deflate算法")]
        Deflate = 1,
        /// <summary>
        /// Gzip的基础是DEFLATE，DEFLATE是LZ77与哈夫曼编码的一个组合体。
        /// <para>DEFLATE最初是作为LZW以及其它受专利保护的数据压缩算法的替代版本而设计的，当时那些专利限制了compress以及其它一些流行的归档工具的应用。</para>
        /// </summary>
        [Description("GZip算法")]
        GZip = 2,
#if NETCOREAPP
        /// <summary>
        /// Brotli最初发布于2015年，用于网络字体的离线压缩。Google软件工程师在2015年9月发布了包含通用无损数据压缩的Brotli增强版本，特别侧重于HTTP压缩。其中的编码器被部分改写以提高压缩比，
        /// <para>编码器和解码器都提高了速度，流式API已被改进，增加更多压缩质量级别。新版本还展现了跨平台的性能改进，以及减少解码所需的内存。</para>
        /// <para>与常见的通用压缩算法不同，Brotli使用一个预定义的120千字节字典。该字典包含超过13000个常用单词、短语和其他子字符串，这些来自一个文本和HTML文档的大型语料库。预定义的算法可以提升较小文件的压缩密度。</para>
        /// <para>使用brotli替换deflate来对文本文件压缩通常可以增加20%的压缩密度，而压缩与解压缩速度则大致不变。使用Brotli进行流压缩的内容编码类型已被提议使用。</para>
        /// </summary>
        [Description("Brotli算法")]
        Brotli = 3,
#endif
    }
}
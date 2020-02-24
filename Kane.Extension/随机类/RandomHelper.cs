﻿#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：RandomHelper
* 类 描 述 ：随机类扩展
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/16 23:19:31
* 更新时间 ：2019/12/29 23:19:31
* 版 本 号 ：v1.0.1.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Kane.Extension
{
    /// <summary>
    /// 随机类扩展
    /// </summary>
    public static class RandomHelper
    {
        #region 产生随机字符串，可设定类型，也可以排除不要的字符 + RandCode(int length, RandMethod method, params char[] exceptChar)
        /// <summary>
        /// 产生随机字符串，可设定类型，也可以排除不要的字符
        /// </summary>
        /// <param name="length">随机字符串的长度</param>
        /// <param name="method">随机字符串包含类型枚举类</param>
        /// <param name="exceptChar">排除的字符</param>
        /// <returns></returns>
        public static string RandCode(int length, RandMethod method, params char[] exceptChar)
        {
            var charList = new List<char>();
            if (method.HasFlag(RandMethod.Numeric)) charList.AddRange(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            if (method.HasFlag(RandMethod.Lowercase))
                charList.AddRange(new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' });
            if (method.HasFlag(RandMethod.Uppercase))
                charList.AddRange(new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' });
            if (method.HasFlag(RandMethod.Punctuation))
                charList.AddRange(new char[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '~', '`', '|', '}', '{', '[', ']', '\\', ':', ';', '?', '>', '<', ',', '.', '/', '-', '=' });
            if (exceptChar.Length > 0) charList = charList.Except(exceptChar).ToList();
            StringBuilder result = new StringBuilder();
            Random random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < length; i++)
                result.Append(charList[random.Next(0, charList.Count)]);
            return result.ToString();
        }
        #endregion
    }
}
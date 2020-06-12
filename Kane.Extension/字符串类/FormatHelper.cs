#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：FormatHelper
* 类 描 述 ：常用的校验格式帮助类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/2/20 19:38:55
* 更新时间 ：2020/6/12 09:28:55
* 版 本 号 ：v1.0.3.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Kane.Extension
{
    /// <summary>
    /// 常用的校验格式帮助类
    /// </summary>
    public class FormatHelper
    {
        #region 检测字符串是否为中国公民身份证 + IsIDCard(string value)
        /// <summary>
        /// 检测字符串是否为中国公民身份证
        /// <para>15位身份证号码=6位地区代码+6位生日+3位编号</para>
        /// <para>18位身份证号码=6位地区代码+8位生日+3位编号+1位检验码</para>
        /// <para>https://www.cnblogs.com/gc2013/p/4054048.html</para>
        /// </summary>
        /// <param name="value">要检测的字符串</param>
        /// <returns></returns>
        public bool IsIDCard(string value)
        {
            if (value.IsNullOrWhiteSpace()) return false;
            string provinceCode = "11,12,13,14,15,21,22,23,31,32,33,34,35,36,37,41,42,43,44,45,46,51,52,53,54,50,61,62,63,64,65,71,81,82";//省代码
            if (value.Length == 15)
            {
                if (long.TryParse(value, out long temp) == false || temp < Math.Pow(10, 14)) return false; // 数字验证
                if (provinceCode.IndexOf(value.Remove(2)) == -1) return false; // 省份验证
                if (!DateTime.TryParse(value.Substring(6, 6).Insert(4, "-").Insert(2, "-"), out _)) return false;//生日验证
                return true; // 符合GB11643-1989标准
            }
            else if (value.Length == 18)
            {
                if (long.TryParse(value.Remove(17), out long temp) == false || temp < Math.Pow(10, 16) || !long.TryParse(value.Replace('x', '0').Replace('X', '0'), out _)) return false; // 数字验证  
                if (provinceCode.IndexOf(value.Remove(2)) == -1) return false; //省份验证
                if (!DateTime.TryParse(value.Substring(6, 8).Insert(6, "-").Insert(4, "-"), out _)) return false;//生日验证
                string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
                string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');//Wi表示第i位置上的加权因子
                char[] Ai = value.Remove(17).ToCharArray();//Ai表示第i位置上的身份证号码数字值
                int sum = 0;
                for (int i = 0; i < 17; i++)
                    sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
                if (arrVarifyCode[sum % 11] != value.Substring(17, 1).ToLower()) return false; //校验码验证
                return true; //符合GB11643-1999标准
            }
            return false;
        }
        #endregion

        #region 检测字符串是否为正确的邮箱地址，可检测中文域名 + IsEmail(string value)
        /// <summary>
        /// 检测字符串是否为正确的邮箱地址，可检测中文域名
        /// </summary>
        /// <param name="value">要检测的字符串</param>
        /// <returns></returns>
        public bool IsEmail(string value)
        {
            if (value.IsNullOrWhiteSpace()) return false;
            return new Regex(@"[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?").IsMatch(value);
        }
        #endregion

        #region 检测字符串是否包含汉字 + HasChinese(string value)
        /// <summary>
        /// 检测字符串是否包含汉字
        /// </summary>
        /// <param name="value">要检测的字符串</param>
        /// <returns></returns>
        public bool HasChinese(string value)
        {
            if (value.IsNullOrWhiteSpace()) return false;
            return Regex.IsMatch(value, @"[\u4e00-\u9fa5]+");
        }
        #endregion

        #region 检测字符串是否为IPv4地址，可包含端口 + IsIPv4(string value)
        /// <summary>
        /// 检测字符串是否为IPv4地址，可包含端口
        /// <para>如【192.168.1.168】或【192.168.1.168:8080】</para>
        /// </summary>
        /// <param name="value">要检测的字符串</param>
        /// <returns></returns>
        public bool IsIPv4(string value)
        {
            if (value.IsNullOrWhiteSpace()) return false;
            var hostType = Uri.CheckHostName(value);
            if (hostType == UriHostNameType.IPv4) return true;
            else if (hostType == UriHostNameType.Unknown
                && Uri.TryCreate($"http://{value}", UriKind.Absolute, out Uri url)
                && IPAddress.TryParse(url.Host, out IPAddress temp)
                && temp.AddressFamily == AddressFamily.InterNetwork) return true;
            return false;
        }
        #endregion

        #region 检测字符串是否为IPv6地址 + IsIPv6(string value)
        /// <summary>
        /// 检测字符串是否为IPv6地址,可包含端口
        /// <para>如【[2001:0DB8:02de::0e13]】或【[2001:0DB8:02de::0e13]:8080】</para>
        /// </summary>
        /// <param name="value">要检测的字符串</param>
        /// <returns></returns>
        public bool IsIPv6(string value)
        {
            if (value.IsNullOrWhiteSpace()) return false;
            var hostType = Uri.CheckHostName(value);
            if (hostType == UriHostNameType.IPv6) return true;
            else if (hostType == UriHostNameType.Unknown
                && Uri.TryCreate($"http://{value}", UriKind.Absolute, out Uri url)
                && IPAddress.TryParse(url.Host, out IPAddress temp)
                && temp.AddressFamily == AddressFamily.InterNetworkV6) return true;
            return false;
        }
        #endregion

        #region 检测字符串是否为IP地址 + IsIP(string value)
        /// <summary>
        /// 检测字符串是否为IP地址,可包含端口
        /// <para>如【192.168.1.168】或【192.168.1.168:8080】或【[2001:0DB8:02de::0e13]】或【[2001:0DB8:02de::0e13]:8080】</para>
        /// </summary>
        /// <param name="value">要检测的字符串</param>
        /// <returns></returns>
        public bool IsIP(string value)
        {
            if (value.IsNullOrWhiteSpace()) return false;
            var hostType = Uri.CheckHostName(value);
            if (hostType == UriHostNameType.IPv4 || hostType == UriHostNameType.IPv6) return true;
            else if (hostType == UriHostNameType.Unknown
                && Uri.TryCreate($"http://{value}", UriKind.Absolute, out Uri url)
                && IPAddress.TryParse(url.Host, out _)) return true;
            return false;
        }
        #endregion

        #region 检测字符串是否为中国国内手机号码段，数据更新日期【2020-03-19】 + IsMobilePhone(string value)
        /// <summary>
        /// 检测字符串是否为中国国内手机号码段，数据更新日期【2020-03-19】
        /// <para>【资料来源】https://baike.baidu.com/item/手机号码 </para>
        /// </summary>
        /// <param name="value">要检测的字符串</param>
        /// <returns></returns>
        public bool IsMobilePhone(string value)
        {
            if (value.IsNullOrWhiteSpace()) return false;
            if (new Regex(@"^1[0-9]{10}$").IsMatch(value.Trim()))
            {
                var prefix = new List<string>();
                string[] ChinaTelecomPrefix = "133、149、153、162、173、177、180、181、189、190、191、193、199".Split('、');
                string[] ChinaUnicomPrefix = "130、131、132、145、155、156、166、175、176、185、186、196".Split('、');
                string[] ChinaMobilePrefix = "134(0-8)、135、136、137、138、139、147、150、151、152、157、158、159、172、178、182、183、184、187、188、195、197、198".Split('、');
                string[] ChinaBroadcstPrefix = new[] { "192" };
                string[] ChinaTelecomVirtualPrefix = "1700、1701、1702、162".Split('、');
                string[] ChinaMobileVirtualPrefix = "1703、1705、1706、165".Split('、');
                string[] ChinaUnicomVirtualPrefix = "1704、1707、1708、1709、171、167".Split('、');
                string[] SatelliteCommunicationPrefix = "1349、174".Split('、');

                prefix.AddRange(ChinaTelecomPrefix); // 中国电信号段
                prefix.AddRange(ChinaUnicomPrefix); // 中国联通号段

                for (var i = 0; i <= 8; i++) prefix.Add($"134{i}");//中国移动 134 号段特殊处理
                prefix.AddRange(ChinaMobilePrefix.Skip(1)); // 中国移动号段
                prefix.AddRange(ChinaBroadcstPrefix); // 中国广电号段
                prefix.AddRange(ChinaTelecomVirtualPrefix); // 中国电信虚拟运营商号段
                prefix.AddRange(ChinaUnicomVirtualPrefix); // 中国联通虚拟运营商号段
                prefix.AddRange(ChinaMobileVirtualPrefix); // 中国移动虚拟运营商号段
                prefix.AddRange(SatelliteCommunicationPrefix); // 卫星通信号段
                return value.StartsWith(prefix.ToArray());
            }
            else return false;
        }
        #endregion

        #region 判断字符串是否为安全Sql语句
        /// <summary>
        /// 判断字符串是否为安全Sql语句
        /// </summary>
        /// <param name="value">要检测的Sql语句</param>
        /// <returns></returns>
        public bool IsSafeSql(string value)
        {
            if (value.IsNullOrWhiteSpace()) return false;
            if (value.IsMatch(@"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']", RegexOptions.IgnoreCase) ||
                value.IsMatch(@"select|insert|delete|from|count(|drop table|update|truncate|asc(|mid(|Char(|xp_cmdshell|exec master|netlocalgroup administrators|:|net user|""|or|and", RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 判断字符串是否为数值 + IsNumeric(string value)
        /// <summary>
        /// 判断字符串是否为数值，如【0】【123】【0.123】【+0.123】【-0.123】【+123.123】【-123.132】
        /// <para>注意，如果是小数，前导零不能少</para>
        /// </summary>
        /// <param name="value">要检测的字符串</param>
        /// <returns></returns>
        public bool IsNumeric(string value) => value.IsMatch(@"^[-+]?\d+[.]?\d*$");
        #endregion

        #region 判断字符串是否为整数 + IsInteger(string value)
        /// <summary>
        /// 判断字符串是否为整数，如【0】【1】【123】【+123】【-123】
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsInteger(string value) => value.IsMatch(@"^[-+]?\d+$");
        #endregion
    }
}
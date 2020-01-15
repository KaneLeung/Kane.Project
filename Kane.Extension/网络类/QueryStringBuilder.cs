#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：QueryStringBuilder
* 类 描 述 ：查询字符串建立器
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/1/13 0:38:55
* 更新时间 ：2020/1/13 0:38:55
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kane.Extension
{
    /// <summary>
    /// 查询字符串Builder
    /// </summary>
    public class QueryStringBuilder
    {
        private readonly Dictionary<string, string> PARM_DATA = new Dictionary<string, string>();

        #region 无参构造函数 + QueryStringBuilder()
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryStringBuilder()
        {
        }
        #endregion

        #region 带参数的构造函数 + QueryStringBuilder(string key, string value)
        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public QueryStringBuilder(string key, string value)
        {
            this[key] = value;
        }
        #endregion

        #region 索引器 + this[string key]
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="key">参数</param>
        /// <returns></returns>
        public string this[string key]
        {
            get => PARM_DATA.ContainsKey(key) ? PARM_DATA[key] : null;
            set => PARM_DATA[key] = value;
        }
        #endregion

        #region 取所有参数，返回数组 + Keys
        /// <summary>
        /// 取所有参数，返回数组
        /// </summary>
        public string[] Keys
        {
            get => PARM_DATA.Keys.ToArray();
        }
        #endregion

        #region 是否存在参数 + HasKeys
        /// <summary>
        /// 是否存在参数
        /// </summary>
        public bool HasKeys
        {
            get => PARM_DATA.Count > 0;
        }
        #endregion

        #region 清除所有参数 + Clear()
        /// <summary>
        /// 清除所有参数
        /// </summary>
        public void Clear() => PARM_DATA.Clear();
        #endregion

        #region 是否包含某个参数 + ContainsKey(string key)
        /// <summary>
        /// 是否包含某个参数
        /// </summary>
        /// <param name="key">参数</param>
        /// <returns></returns>
        public bool ContainsKey(string key) => PARM_DATA.ContainsKey(key);
        #endregion

        #region 添加参数 + Add(string key, string value)
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="key">参数</param>
        /// <param name="value">值</param>
        public void Add(string key, string value) => PARM_DATA[key] = value;
        #endregion

        #region 移除参数 + Remove(string key)
        /// <summary>
        /// 移除参数
        /// </summary>
        /// <param name="key">参数</param>
        public void Remove(string key) => PARM_DATA.Remove(key);
        #endregion

        #region 生成结果 + ToString()
        /// <summary>
        /// 生成结果
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var keyValuePair in PARM_DATA)
            {
                builder.Append($"{keyValuePair.Key}={Uri.EscapeDataString(keyValuePair.Value)}&");
            }
            return builder.ToString().TrimEnd('&');
        }
        #endregion
    }
}
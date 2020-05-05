#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：CollectionsEx
* 类 描 述 ：集合类相关扩展类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/5/5 15:27:09
* 更新时间 ：2020/5/5 15:27:09
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Kane.Extension
{
    /// <summary>
    /// 集合类相关扩展类
    /// </summary>
    public static class CollectionsEx
    {
        #region 泛型集合转换为DataTable + ToDataTable<T>(this IEnumerable<T> source, string tableName = "")
        /// <summary>
        /// 泛型集合转换为DataTable
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="tableName">表名</param>
        /// <exception cref="ArgumentNullException">源集合对象为空</exception>
        public static DataTable ToDataTable<T>(this IEnumerable<T> source, string tableName = "")
        {
            var type = typeof(T);
            if (source == null) throw new ArgumentNullException(nameof(source), $"源【{type.Name}】集合对象不可为空！");
            int index = 0;
            var properties = type.GetProperties();
            var result = tableName.IsNullOrEmpty() ? new DataTable() : new DataTable(tableName);
            foreach (var property in properties)
                result.Columns.Add(new DataColumn(property.Name));
            foreach (var item in source)
            {
                foreach (var property in properties)
                {
#if NET40
                    result.Rows[index++][property.Name] = property.GetValue(item, null);
#else
                    result.Rows[index++][property.Name] = property.GetValue(item);
#endif
                }
            }
            return result;
        }
#endregion

#region DataTable转换为泛型集合 + ToList<T>(this DataTable dataTable)
        /// <summary>
        /// DataTable转换为泛型集合
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="dataTable">数据表</param>
        /// <exception cref="ArgumentNullException">源数据表不可为空</exception>
        public static IEnumerable<T> ToList<T>(this DataTable dataTable)
        {
            var type = typeof(T);
            if (dataTable == null) throw new ArgumentNullException(nameof(dataTable), $"源【{type.Name}】数据表不可为空！");
            var properties = type.GetProperties();
            var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            var noParamCtor = constructors.Single(k => k.GetParameters().Length == 0);
            var result = new List<T>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = (T)noParamCtor.Invoke(null);
                foreach (var property in properties)
                {
                    if (dataTable.Columns.Contains(property.Name))
                    {
                        var setter = property.GetSetMethod(true);
                        if (setter != null)
                        {
                            var value = row[property.Name] == DBNull.Value ? null : row[property.Name];
                            setter.Invoke(item, new[] { value });
                        }
                    }
                }
                result.Add(item);
            }
            return result;
        }
#endregion
    }
}
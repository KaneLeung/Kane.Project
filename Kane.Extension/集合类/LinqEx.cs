#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：LinqEx
* 类 描 述 ：Linq扩展类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/2/25 23:24:13
* 更新时间 ：2020/06/10 11:24:13
* 版 本 号 ：v1.0.1.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Kane.Extension
{
    /// <summary>
    /// Linq扩展类
    /// <para>https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.queryable?view=netcore-3.1</para>
    /// </summary>
    public static class LinqEx
    {
        #region WhereIf，满足条件进行查询 + WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        /// <summary>
        /// WhereIf，满足条件进行查询
        /// </summary>
        /// <typeparam name="T">数据元素类型</typeparam>
        /// <param name="query">数据源</param>
        /// <param name="condition">判断条件</param>
        /// <param name="predicate">用于测试每个元素是否满足条件的函数</param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
            => condition ? query.Where(predicate) : query;
        #endregion

        #region OrderIf，满足条件进行【升序】排序 + OrderIf<TSource, TKey>(this IQueryable<TSource> query, bool condition, Expression<Func<TSource, TKey>> keySelector)
        /// <summary>
        /// OrderIf,满足条件进行【升序】排序
        /// </summary>
        /// <typeparam name="TSource">数据元素类型</typeparam>
        /// <typeparam name="TKey">由 keySelector 表示的函数返回的键类型</typeparam>
        /// <param name="query">数据源</param>
        /// <param name="condition">判断条件</param>
        /// <param name="keySelector">用于从元素中提取键的函数</param>
        /// <returns></returns>
        public static IQueryable<TSource> OrderIf<TSource, TKey>(this IQueryable<TSource> query, bool condition, Expression<Func<TSource, TKey>> keySelector)
            => condition ? query.OrderBy(keySelector) : query;
        #endregion

        #region OrderDescIf，满足条件进行【降序】排序 + OrderDescIf<TSource, TKey>(this IQueryable<TSource> query, bool condition, Expression<Func<TSource, TKey>> keySelector)
        /// <summary>
        /// OrderDescIf,满足条件进行【降序】排序
        /// </summary>
        /// <typeparam name="TSource">数据元素类型</typeparam>
        /// <typeparam name="TKey">由 keySelector 表示的函数返回的键类型</typeparam>
        /// <param name="query">数据源</param>
        /// <param name="condition">判断条件</param>
        /// <param name="keySelector">用于从元素中提取键的函数</param>
        /// <returns></returns>
        public static IQueryable<TSource> OrderDescIf<TSource, TKey>(this IQueryable<TSource> query, bool condition, Expression<Func<TSource, TKey>> keySelector)
            => condition ? query.OrderByDescending(keySelector) : query;
        #endregion

        #region Select查询后自动执行ToList() + SelectList<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        /// <summary>
        /// Select查询后自动执行ToList()
        /// </summary>
        /// <typeparam name="TSource">数据元素类型</typeparam>
        /// <typeparam name="TResult">返回的值的类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="selector">应用于每个元素的转换函数</param>
        /// <returns></returns>
        public static List<TResult> SelectList<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
            => source.Select(selector).ToList();
        #endregion

        #region 根据属性名进行排序，默认为【升序】 + OrderBy<TSource>(this IQueryable<TSource> source, string property, bool descending = false) where TSource : class
        /// <summary>
        /// 根据属性名进行排序，默认为【升序】
        /// </summary>
        /// <typeparam name="TSource">数据元素类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="property">属性名</param>
        /// <param name="descending">是否降序</param>
        /// <returns></returns>
        public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string property, bool descending = false) where TSource : class
        {
            ParameterExpression param = Expression.Parameter(typeof(TSource), "k");
            PropertyInfo pi = typeof(TSource).GetProperty(property);
            MemberExpression selector = Expression.MakeMemberAccess(param, pi);
            LambdaExpression exp = Expression.Lambda(selector, param);
            string methodName = descending ? "OrderByDescending" : "OrderBy";
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(TSource), pi.PropertyType }, source.Expression, exp);
            return source.Provider.CreateQuery<TSource>(resultExp);
        }
        #endregion

        #region 根据属性名进行【降序】排序 + OrderByDesc<TSource>(this IQueryable<TSource> source, string property) where TSource : class
        /// <summary>
        /// 根据属性名进行【降序】排序
        /// </summary>
        /// <typeparam name="TSource">数据元素类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="property">属性名</param>
        /// <returns></returns>
        public static IQueryable<TSource> OrderByDesc<TSource>(this IQueryable<TSource> source, string property) where TSource : class
        {
            ParameterExpression param = Expression.Parameter(typeof(TSource), "k");
            PropertyInfo pi = typeof(TSource).GetProperty(property);
            MemberExpression selector = Expression.MakeMemberAccess(param, pi);
            LambdaExpression exp = Expression.Lambda(selector, param);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { typeof(TSource), pi.PropertyType }, source.Expression, exp);
            return source.Provider.CreateQuery<TSource>(resultExp);
        }
        #endregion
    }
}
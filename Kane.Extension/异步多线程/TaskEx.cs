#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：TaskEx
* 类 描 述 ：异步多线程扩展类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/5/5 16:08:20
* 更新时间 ：2020/5/5 16:08:20
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
#if !NET40
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kane.Extension
{
    /// <summary>
    /// 异步多线程扩展类
    /// </summary>
    public static class TaskEx
    {
        #region 设置Task过期时间 + TimeoutCancel(this Task task, int milliseconds)
        /// <summary>
        /// 设置Task过期时间
        /// </summary>
        /// <param name="task">异步操作</param>
        /// <param name="milliseconds">超时时间。单位：毫秒</param>
        /// <returns></returns>
        public static async Task TimeoutCancel(this Task task, int milliseconds)
        {
            var cancelToken = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(task, Task.Delay(milliseconds, cancelToken.Token));
            if (completedTask == task) cancelToken.Cancel();
            else throw new TimeoutException($"操作已超时。");
        }
        #endregion

        #region 设置Task过期时间 + TimeoutCancel<T>(this Task<T> task, int milliseconds)
        /// <summary>
        /// 设置Task过期时间
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="task">异步操作</param>
        /// <param name="milliseconds">超时时间。单位：毫秒</param>
        /// <returns></returns>
        public static async Task<T> TimeoutCancel<T>(this Task<T> task, int milliseconds)
        {
            var cancelToken = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(task, Task.Delay(milliseconds, cancelToken.Token));
            if (completedTask == task)
            {
                cancelToken.Cancel();
                return task.Result;
            }
            else throw new TimeoutException($"操作已超时。");
        }
        #endregion
    }
}
#endif
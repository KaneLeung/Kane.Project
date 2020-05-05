#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：TaskSchedulerMax
* 类 描 述 ：自定义任务计划
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/2/29 23:19:25
* 更新时间 ：2020/5/05 13:19:25
* 版 本 号 ：v1.0.1.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kane.Extension
{
    /// <summary>
    /// 自定义任务计划，可控件多线程并发线
    /// <para>https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.taskscheduler?view=netcore-3.1</para>
    /// </summary>
    public class TaskSchedulerMax : TaskScheduler, IDisposable
    {
        #region 私有成员
        private BlockingCollection<Task> TASK_LIST = new BlockingCollection<Task>();
        private readonly List<Thread> THREAD_LIST = new List<Thread>();
        #endregion

        #region 公有属性
        /// <summary>
        /// 能够支持的最大并发级别
        /// </summary>
        public override int MaximumConcurrencyLevel { get => THREAD_LIST.Count; }
        #endregion

        #region 构造函数 + TaskSchedulerMax(int max = 5)
        /// <summary>
        /// 构造函数，默认并发数为【5】
        /// </summary>
        /// <param name="max">最大并发数，最小为【1】</param>
        public TaskSchedulerMax(int max = 5)
        {
            if (max < 1) throw new ArgumentOutOfRangeException(nameof(max), "最小线程数不能少于【1】");
            THREAD_LIST.AddRange(Enumerable.Range(0, max).Select(_ =>
            {
                var thread = new Thread(() =>
                {
                    foreach (var task in TASK_LIST.GetConsumingEnumerable())
                        TryExecuteTask(task);
                });
                thread.IsBackground = true;
                thread.Start();
                return thread;
            }));
        }
        #endregion

        #region 重写 + GetScheduledTasks()
        /// <summary>
        /// 重写【仅对于调试器支持，生成当前排队到计划程序中等待执行的 System.Threading.Tasks.Task 实例的枚举】
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<Task> GetScheduledTasks() => TASK_LIST.ToArray();//这个函数好像没有调过，返回null也不影响功能 
        #endregion

        #region 重写 + QueueTask(Task task)
        /// <summary>
        /// 重写【将 System.Threading.Tasks.Task 排队到计划程序中】
        /// </summary>
        /// <param name="task"></param>
        protected override void QueueTask(Task task) => TASK_LIST.Add(task);
        #endregion

        #region 重写 + TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        /// <summary>
        /// 重写【确定是否可以在此调用中同步执行提供的 System.Threading.Tasks.Task，如果可以，将执行该任务。】
        /// </summary>
        /// <param name="task">要执行的Task</param>
        /// <param name="taskWasPreviouslyQueued">一个布尔值，该值指示任务之前是否已排队。 如果此参数为 True，则该任务以前可能已排队（已计划）；
        /// 如果为 False，则已知该任务尚未排队，此时将执行此调用，以便以内联方式执行该任务，而不用将其排队。</param>
        /// <returns></returns>
        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued) => TryExecuteTask(task);
        #endregion

        #region 释放资源 + Dispose()
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (TASK_LIST.Count == 0) return;//防止重入
            TASK_LIST.CompleteAdding();
            THREAD_LIST.ForEach(t => t.Join());
            TASK_LIST.Dispose();
            TASK_LIST = null;
        }
        #endregion
    }
}
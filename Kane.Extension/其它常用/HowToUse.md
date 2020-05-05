
### 100线程并发
```
    var scheduler = new TaskSchedulerMax(100);
    Task[] tasks = new Task[100];
    for (int i = 0; i < 100; i++)
    {
        var j = i;
        tasks[i] = new Task(() => Console.WriteLine($"第【{j}】次，This Thread ID:{ Thread.CurrentThread.ManagedThreadId}"));
        tasks[i].Start(scheduler);
    }
```
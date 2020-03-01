
### 100线程并发
```
    var scheduler = new KaneTaskScheduler(100)
    Task[] tasks = new Task[100];
    for (int i = 0; i < 100; i++)
    {
        var j = i;
        tasks[i] = new Task(() => YourMethod(j));
        tasks[i].Start(scheduler);
    }
```
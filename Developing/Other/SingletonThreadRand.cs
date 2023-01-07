using Developing.Lists;
using Developing.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Developing.Other;

public class SingletonThreadRand<T>
{
    private static Sequence<T>? _seq;
    private static SemaphoreSlim _mutex = new SemaphoreSlim(1, 1);

    //public SingletonThreadRand() : this(0)
    //{

    //}

    //public SingletonThreadRand(int seed)
    //{
    //    _mutex = new SemaphoreSlim(1, 1);
    //    _seq = new Sequence<T>(seed: seed);
    //}

    public static async Task<T> Next()
    {
        await _mutex.WaitAsync();
        _seq ??= new Sequence<T>();

        _mutex.Release();

        return _seq.Next;
    }

    public static void Test()
    {
        var tasks = Enumerable.Range(0, 10)
            .Select(t => (new SlStack<int>(), new TickedBackgroundTask(3000, SingletonThreadRand<int>.Next().Result + t)))
            .ToList();

        tasks.ForEach(task =>
            task.Item2.Start(() =>
            {
                task.Item1.Push(SingletonThreadRand<int>.Next().Result);
            }));

        new Task(() =>
        {
            while (!tasks.All(t => t.Item2.IsFinished))
            {
                Task.Delay(100);
            }

            Console.WriteLine("All Finished");
        }).Start();

        Console.ReadKey();

        foreach (var t in tasks)
        {
            Console.WriteLine(t.Item1);
        }
    }
}
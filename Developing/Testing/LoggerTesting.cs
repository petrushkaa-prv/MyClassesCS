using System;
using System.Linq;
using System.Threading.Tasks;

using Developing.Graphs;
using Developing.Lists;
using Developing.Other.Logger;
using Developing.Other;

namespace Developing.Testing;

public class LoggerTesting
{
    private Sequence<int> _rand;
    private SlStack<Action> _stuck;


    public LoggerTesting(Sequence<int> rnd)
    {
        _rand = rnd;
        _stuck = new SlStack<Action>();
        _stuck.Push(Test1_Graph);
        _stuck.Push(Test2_ParallelExecution);
    }

    public void Run()
    {
        int i = 0;
        foreach (var t in _stuck)
        {
            Console.WriteLine($"PERFOMING TEST {++i}");
            Console.ReadKey();
            t.Invoke();
            Console.ReadKey();
        }
    }

    private void Test1_Graph()
    {
        var g = new Graph(10);
        var wg = LogWrapper<Graph>.WrapWithLogs(g);

        int time = 0;

        var tbt = new TickedBackgroundTask(20, 1000);
        tbt.Start(() =>
        {
            wg.Run(gd =>
            {
                int from = 0, to = 0;

                try
                {
                    while (!gd.HasEdge(from, to))
                    {
                        from = _rand.Next;
                        to = _rand.Next;

                        if (from != to)
                            gd.AddEdge(from, to);

                        return $"Added Edge {from} to {to}";
                    }
                }
                catch
                {
                    // ignored
                }

                return $"Failed to add Edge {from} to {to}";
            });

            Console.WriteLine(time += 20);

            if (wg.Object.DFS().SearchAll().Count() == wg.Object.VertexCount * (wg.Object.VertexCount - 1))
            {
                Console.WriteLine($"\n\n\n===DONE===\n\n\n");
                tbt.Stop();
            }
        });

        Console.ReadKey();

        Console.WriteLine(wg);
    }
    private void Test2_ParallelExecution()
    {
        using var tbt = new TickedBackgroundTask(1, 100);
        var l = new SlList<LoggedObject<Task>>();
        var wl = new LoggedObject<SlList<LoggedObject<Task>>>(l);



        tbt.Start(() =>
        {
            wl.Run(list =>
            {
                var tmpLog = new DlQueue<string>();

                list.AddFront(new LoggedObject<Task>(new Task(() =>
                {
                    int rnd;

                    do
                    {
                        rnd = _rand.Next;
                        Console.Write($"{Task.CurrentId}::{rnd}\n");
                        Task.Delay(10000);

                        tmpLog.Push($"{Task.CurrentId} current guess: {rnd}");
                    } while (rnd != 0);
                }))
                {
                    Logs = tmpLog
                });

                var curr = list.Front;

                curr.Run(t =>
                {
                    t.Start();

                    return $"Started Task {t.Id}";
                });

                return $"Added Task with id: {curr.Object.Id}";
            });
        });

        Console.ReadKey();

        Console.WriteLine(wl);

        var counts = new SlStack<(int, int)>();

        foreach (var el in wl.Object)
        {
            Console.WriteLine(el);
            counts.Push((el.Object.Id, el.Logs.Size));
        }

        Console.WriteLine();
    }
}
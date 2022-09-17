/*
 * TODO: Implement:                             Status:
 * TODO:            2D printing for BTrees              Done
 * TODO:            2D pr. vertical
 * TODO:            Biparental heap                     InProgress
 * TODO:            Leftist heap
 * TODO:            Binomial queue
 * TODO:            BST tree                            Done
 * TODO:            AVL tree                            Done
 * TODO:            Splay tree                          BUG!
 * TODO:            BW tree
 * TODO:            AA tree
 * TODO:            RST tree
 * TODO:            Seq. matcher                        Done
 * TODO:            Sequence class                      Done
 * TODO:            Sequence for complex cl.            
 * TODO:            Graph                               Done
 * TODO:            DirectedGraph                       Done
 * TODO:            WeightedGraph                       Done
 * TODO:            WeightedDirectedGraph               Done
 * TODO:            MatrixGraphRepresentation           Done
 * TODO:            DFS & BFS                           Done
 * TODO:            Add extensions for graphs           InProgress
 *
 * TODO: Experiment:
 * TODO:            Try unsafe on BST<int>
 * TODO:            And comp. unit tests
 *
 * TODO: Refactor:
 * TODO:            Heap (completely)                   Done
 * TODO:            Heap (IComparable T)
 * TODO:            Generic cl. add comp. dependence
 * TODO:            Rethink the tree nodes imp.         InProgress
 */



#nullable enable

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using static System.Math;

using Developing.Arrays;
using Developing.GeneralExtensions;
using Developing.Interfaces;
using Developing.Lists;
using Developing.Nodes;
using Developing.Other;
using Developing.Trees;
using Developing.Graphs;
using Developing.Other.Logger;
using Developing.Testing;
using static Developing.Other.QuickMath;
using static Developing.Graphs.GraphAlgorithms;






namespace Chamber;

internal static class Program
{
    private static readonly Sequence<int> Rand =
        new(
            100, 
            0, 
            100, 
            5, 
            0
        );

    public static void Main(string[] args)
    {


    }
    
}


public class TaskRing
{
    private BackgroundTask _observer;
    private DlRingList<(Task task, int shared)> _ring;

    public TaskRing(int count, Action func)
    {
        _ring = new DlRingList<(Task task, int shared)>();

        for (int i = 0; i < count; i++)
        {
            _ring.AddFront((new Task(func), new int()));
        }

        _observer = new BackgroundTask(100);
    }

    public void Execute()
    {
        foreach (var v in _ring)
        {
            v.task.Start();   
        }


        _observer.Start(() =>
        {
            var ptr = _ring.Head;
            while(true)
            {
                Task.Delay(1000);



                ptr = ptr.Next;
            }
        });
    }

    private class DlRingList<T> : DlList<T>
    {

        private void ReconnectEnds()
        {
            base.Rear.Next = base.Head;
            base.Head.Prev = base.Rear;
        }

        public new void AddFront(T val)
        {
            base.AddFront(val);

            ReconnectEnds();
        }

        public new void AddEnd(T val)
        {
            base.AddEnd(val);

            ReconnectEnds();
        }

        public new void RemoveEnd()
        {
            base.RemoveEnd();

            ReconnectEnds();
        }

        public new void RemoveFront()
        {
            base.RemoveFront();

            ReconnectEnds();
        }
    }
}

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
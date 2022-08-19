#nullable enable

using Developing.Arrays;
using Developing.GeneralExtensions;
using Developing.Interfaces;
using Developing.Lists;
using Developing.Nodes;
using Developing.Other;
using Developing.Trees;
using Developing.Graphs;
using Developing.Testing;

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
using Developing.Other.Logger;
using static System.Math;
using static Developing.Graphs.GraphAlgorithms;

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
 */



namespace Chamber;

internal static class Program
{
    private static readonly Sequence<int> Rand =
        new(
            10, 
            0, 
            10, 
            5, 
            0
        );

    public static void Main(string[] args)
    {
        using var tbt = new TickedBackgroundTask(1, 100);
        tbt.Start(() =>
        {
            Console.WriteLine(Rand.Next);
        });
        Console.ReadKey();
    }
}

public class LoggerTesting
{
    private Sequence<int> _rand;

    public LoggerTesting(Sequence<int> rnd)
    {
        _rand = rnd;
    }

    public void Run()
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
}
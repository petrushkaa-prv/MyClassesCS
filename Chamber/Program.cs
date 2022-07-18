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
    public static unsafe float QrSqrt(float number) 
    {
        const float th = 1.5f;

        float x2 = number * 0.5f;
            
        int i = 0x5f3759df - (*(int*)&number >> 1);
        float y = *(float*)&i;
        y *= th - x2 * y * y;

        return y;
    }

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
        int i = 0;
        var t = new TickedBackgroundTask(1000, 10);
        t.Start((() =>
        {
            Console.WriteLine(i++);
        }));

        Console.ReadKey();

        t.StopAsync();
    }
}
global using Developing.Arrays;
global using Developing.GeneralExtensions;
global using Developing.Interfaces;
global using Developing.Lists;
global using Developing.Nodes;
global using Developing.Other;
global using Developing.Trees;
global using Developing.Graphs;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


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
 * TODO:            Sequence class                      InProgress
 * TODO:            Sequence for complex cl.
 * TODO:            Graph                               Done
 * TODO:            DirectedGraph                       Done(?)
 * TODO:            WeightedGraph
 * TODO:            WeightedDirectedGraph
 * TODO:            MatrixGraphRepresentation
 * TODO:            DFS & BFS
 * TODO:            Add extensions for graphs
 *
 * TODO: Experiment:
 * TODO:            Try unsafe on BST<int>
 * TODO:            And comp. unit tests
 *
 * TODO: Refactor:
 * TODO:            Heap (completely)                   Done
 * TODO:            Heap (IComparable T)
 */



namespace Chamber
{
    internal static class Program
    {
        public static unsafe float QrSqrt(float number)
        {
            const float th = 1.5f;

            float x2 = number * 0.5f;
            float y = number;

            int i = *(int*)&y;
            i = 0x5f3759df - (i >> 1);
            y = *(float*)&i;
            y *= th - x2 * y * y;

            return y;
        }

        private static readonly Sequence<int> Rand =
            new(
                10, 
                0, 
                10, 
                5, 
                DateTime.Now.Millisecond
                );

        public static void Main(string[] args)
        {
            var g = new Graph(10, new MatrixGraphRepresentation());

            var arr = Rand.Array;

            for (int i = 0; i < 15; i++)
            {
                int u = Rand.Next;
                int v = Rand.Next;
                if(u == v) continue;

                g.AddEdge(u, v);
            }

            Console.WriteLine(g);
        }
    }
}
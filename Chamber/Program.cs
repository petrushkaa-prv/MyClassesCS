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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
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
 * TODO:            Sequence class                      Done
 * TODO:            Sequence for complex cl.            
 * TODO:            Graph                               Done
 * TODO:            DirectedGraph                       Done
 * TODO:            WeightedGraph                       Done
 * TODO:            WeightedDirectedGraph               Done
 * TODO:            MatrixGraphRepresentation           Done
 * TODO:            DFS & BFS                            InProgress(DFS:Done)
 * TODO:            Add extensions for graphs           InProgress
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

        public static string Ea<T>(this IEnumerable<T> input)
        {
            var sb = new StringBuilder();

            foreach (var elem in input)
            {
                sb.Append(elem.ToString());
                sb.Append('\t');
            }

            return sb.ToString();
        }

        private static readonly Sequence<int> Rand =
            new(
                10, 
                0, 
                20, 
                5, 
                DateTime.Now.Millisecond
                );

        public static void Main(string[] args)
        {
            var a = new DirectedGraph<int>(25);
            var b = new DirectedGraph(25);
            var c = new Graph<int>(25);
            var d = new Graph(20);

            for (int i = 0; i < 10; i++)
            {
                int u = Rand.Next;
                int v = Rand.Next;

                if (u == v) continue;

                a.AddEdge(u, v, Rand.Next);
                b.AddEdge(u, v);
                c.AddEdge(u, v, Rand.Next);
                d.AddEdge(u, v);
            }

            Console.WriteLine(a);
            Console.WriteLine();
            Console.WriteLine(b);
            Console.WriteLine();
            Console.WriteLine(c);
            Console.WriteLine();
            Console.WriteLine(c.IsAcyclic().ToString() + "\t" + c.IsBipartite(out var s1, out var s2));

            Console.WriteLine(s1);
            Console.WriteLine(s2);
        }
    }
}
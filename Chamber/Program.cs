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
                20, 
                5, 
                0
                );

        public static void Main(string[] args)
        {
            var mat1 = new Matrix<int>(3, 2);
            var mat2 = new Matrix<int>(2, 3);

            mat1.FillWith(2);
            mat2.FillWith(4);

            Console.WriteLine(mat1);
            Console.WriteLine(mat2);

            Console.WriteLine(mat1 * mat2);
            Console.WriteLine(mat2 * mat1);
            Console.WriteLine(mat1.ElementVise() * mat1.ElementVise());
        }
    }
}
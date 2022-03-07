using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text.Json.Serialization;

using Developing.Arrays;
using Developing.GeneralExtensions;
using Developing.Lists;
using Developing.Nodes;
using Developing.Other;
using Developing.Tree;



namespace CS_Test_Chamber.Chamber
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


        public static Sequence Rand = new(howMuch: 25, 
                                          min: 0, 
                                          max: 25, 
                                          seed: 25);

        private static void Main(string[] args)
        {
            var match = new SequenceMatcher("BROBOT", "BREAD");

            Console.WriteLine(match);
        }
    }
}

/*
 * TODO: Implement:                             Status:
 * TODO:            2D printing for BTrees      Done
 * TODO:            2D pr. vertical
 * TODO:            Biparental heap             InProgress
 * TODO:            Leftist heap
 * TODO:            Skew heap
 * TODO:            Binomial queue
 * TODO:            Fibonacci queue
 * TODO:            BST tree                    Done
 * TODO:            AVL tree                    Done
 * TODO:            B-tree
 * TODO:            BR tree
 * TODO:            Splay tree                  BUG!
 * TODO:            RST tree
 *
 * TODO: Experiment:
 * TODO:            Try unsafe on BST<int>
 * TODO:            And comp. unit tests
 *
 * TODO: Refactor:
 * TODO:            Heap (completely)           Done
 * TODO:            Heap (IComparable T)
 */
using System;
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

using Developing.Arrays;
using Developing.GeneralExtensions;
using Developing.Lists;
using Developing.Nodes;
using Developing.Other;
using Developing.Trees;

/*
 * TODO: Implement:                             Status:         CommentStatus:
 * TODO:            2D printing for BTrees      Done
 * TODO:            2D pr. vertical
 * TODO:            Biparental heap             InProgress
 * TODO:            Leftist heap
 * TODO:            Binomial queue
 * TODO:            BST tree                    Done
 * TODO:            AVL tree                    Done
 * TODO:            BR tree
 * TODO:            Splay tree                  BUG!
 * TODO:            RST tree
 * TODO:            Seq. matcher                Done
 * TODO:            Adv. Seq. matcher
 *
 * TODO: Experiment:
 * TODO:            Try unsafe on BST<int>
 * TODO:            And comp. unit tests
 *
 * TODO: Refactor:
 * TODO:            Heap (completely)           Done
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

        private static readonly Sequence<float> Rand = new(10, seed: DateTime.Now.Millisecond);

        private static void Main(string[] args)
        {
            var s = new Developing.Lists.Stack<float>(Rand.Array);
            
        }
    }
}
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
using System.Threading.Tasks;
using Chamber.HelpRomko;
using Developing.Arrays;
using Developing.GeneralExtensions;
using Developing.Interfaces;
using Developing.Lists;
using Developing.Nodes;
using Developing.Other;
using Developing.Trees;

/*
 * TODO: Implement:                             Status:
 * TODO:            2D printing for BTrees      Done
 * TODO:            2D pr. vertical
 * TODO:            Biparental heap             InProgress
 * TODO:            Leftist heap
 * TODO:            Binomial queue
 * TODO:            BST tree                    Done
 * TODO:            AVL tree                    Done
 * TODO:            Splay tree                  BUG!
 * TODO:            RST tree
 * TODO:            Seq. matcher                Done
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
            SlList<SinglyLinked<int>> list = new(new MyStack<int>(Rand.Array), new MyStack<int>(Rand.Array));

            foreach (var test in list)
            {
                Console.WriteLine(test);
            }

            var s1 = new MyStack<int>(Rand.Array);
            var s2 = new MyStack<int>(Rand.Array);
            Console.WriteLine(s1);
            Console.WriteLine(s2);


            if (!true)
            {
                s1.Append(s2);
                Console.WriteLine(s1);
            }
            else
            {
                Console.WriteLine(s1);
                s1.Subtract(s2, out var s3);
                Console.WriteLine(s1);
                Console.WriteLine("Sub 1:");
                Console.WriteLine(s3);
                s1.Subtract(s3, out var s4);
                Console.WriteLine(s1);
                Console.WriteLine("Sub 2:");
                Console.WriteLine(s4);
            }
        }
    }
}
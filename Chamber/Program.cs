using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Developing.Arrays;
using Developing.GeneralExtensions;
using Developing.Lists;
using Developing.Nodes;
using Developing.Tree;

//using System.Collections.Generic;
//using System.Linq;

namespace CS_Test_Chamber.Chamber
{
    static class Program
    { 
        static Sequence _rand = new (5, Seed: 12);

        static void Main(string[] args)
        {
            var splay = new SplayTree<int>((int[])_rand);

            splay.Print2D();
        }
    }
}

/*
 * TODO: Implement:                             Status:
 * TODO:            2D printing for BTrees      Done
 * TODO:            2D pr. vertical
 * TODO:            Biparental heap
 * TODO:            Leftist heap
 * TODO:            Skew heap
 * TODO:            Binomial queue
 * TODO:            Fibonacci queue
 * TODO:            BST tree                    Done
 * TODO:            AVL tree                    Done
 * TODO:            B-tree
 * TODO:            2-3 tree
 * TODO:            2-3-4 tree
 * TODO:            BR tree
 * TODO:            Splay tree                  BUG!
 * TODO:            RST tree
 * TODO:            AVL<->BR<->2-3-4 conv.  
 *
 * TODO: Experiment:
 * TODO:            Try unsafe on BST<int>
 * TODO:            And comp. unit tests
 *
 * TODO: Refactor:
 * TODO:            Heap (completely)           Done
 */
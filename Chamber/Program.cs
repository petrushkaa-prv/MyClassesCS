using System;
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

namespace CS_Test_Chamber
{
    //public class FileOperator
    //{
    //    public FileStream FileStream;
    //    private string _fileName;
    //    private string _filePath;

    //    public FileOperator(string filePath, string fileName = "Sample.txt")
    //    {
    //        _filePath = filePath;
    //        _fileName = fileName;

    //        if (File.Exists(filePath + fileName))
    //        {
    //            FileStream = File.Open(filePath + fileName, FileMode.Append, FileAccess.Write);
    //        }
    //        else
    //        {
    //            FileStream = File.Create(filePath + fileName);
    //        }
    //    }
    //    ~FileOperator()
    //    {
    //        FileStream.Close();
    //    }

    //    public static FileStream CreateFileInCurrentDirectory(string fileName, string fileType = "txt", bool clearOld = false)
    //    {
    //        var fullFileName = Path.GetFullPath(@"..\..\..\") + fileName + "." + fileType;

    //        if (File.Exists(fullFileName) && !clearOld) return null;

    //        return File.Create(fullFileName);
    //    }
    //}

    static class Program
    {
        //static Task<int> AsyncFunction(int seed, StreamWriter writer)
        //{
        //    return Task<int>.Run((() =>
        //    {
        //        var rand = new Random(seed);
        //        var stack = new Stack<int>();

        //        for (int i = 0; i < 100; i++)
        //        {
        //            var curr = rand.Next(0, i);
        //            writer.WriteLine($"[{i}] = {curr}");
        //            stack.Push(curr);

        //            Thread.Sleep(100);
        //        }

        //        var sum = 0;
        //        foreach (var el in stack)
        //        {
        //            sum += el;
        //        }

        //        return sum;
        //    }));
        //}

        static Random _rand = new Random(0);

        static void Main(string[] args)
        {
            var arr = new int[7];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = _rand.Next(0, 25);
                Console.Write(arr[i] + "\t");
            }

            Console.WriteLine();

            var tree = new AvlTree<int>(arr);
            tree.Print2D();

            Console.WriteLine("\n\n");
            tree.Delete(18);

            tree.Print2D();
            Console.WriteLine("\n\n");
            tree.Delete(5);

            tree.Print2D();

            Console.WriteLine("\n\n");
            tree.Insert(24);

            tree.Print2D();
            Console.WriteLine("\n\n");
            Console.WriteLine(tree.Height);
        }
    }
}

/*
 * TODO: Implement:                         Status:
 * TODO:            2D printing for BTrees  Done
 * TODO:            2D pr. vertical
 * TODO:            Biparental heap
 * TODO:            Leftist heap
 * TODO:            Skew heap
 * TODO:            Binomial queue
 * TODO:            Fibonacci queue
 * TODO:            BST tree                Done
 * TODO:            AVL tree
 * TODO:            B-tree
 * TODO:            2-3 tree
 * TODO:            2-3-4 tree
 * TODO:            BR tree
 * TODO:            Splay tree
 * TODO:            RST tree
 * TODO:            AVL<->BR<->2-3-4 conv.  
 *
 * TODO: Experiment:
 * TODO:            Try unsafe on BST<int>
 * TODO:            And comp. unit tests
 *
 * TODO: Refactor:
 * TODO:            Heap (completely)       Done
 */
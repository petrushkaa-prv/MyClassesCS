using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Developing.Arrays;
using Developing.Lists;
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

        static void Main(string[] args)
        {
            var rand = new Random(1337);
            int[] arr = new int[10];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rand.Next(0,10);
                Console.Write(arr[i] + "\t");
            }

            Console.WriteLine();

            var heap = Heap.Min<int>.HeapifyArray(arr);

            for (int i = 0; i < heap.Length; i++)
            {
                Console.Write(heap[i] + "\t");
            }
        }
    }
}

/*
 * TODO: Refactor Heap (completely) also add building from the bottom
 *
 * TODO: Implement:                         Status:
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
 *
 * TODO: Experiment:
 * TODO:            Try unsafe on BST<int>
 */
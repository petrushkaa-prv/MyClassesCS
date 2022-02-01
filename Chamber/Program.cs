using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using CS_Test_Chamber.Developing.Arrays;
using Developing.MyClasses;

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
            var vec = new Vector<int>(1, 2, 3, 4);
            Console.WriteLine((Vector<int>)(vec + 1 == vec));
            Console.WriteLine((Vector<int>)(vec == vec));

            var vec2 = new Vector<int>(1, 1, 3, -2);

            Console.WriteLine((Vector<int>)(vec2 == vec));
        }
    }
}
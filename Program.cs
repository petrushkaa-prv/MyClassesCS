using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Text;
using CS_Test_Chamber.Developing.Arrays;
using CS_Test_Chamber.Developing.Lists;
using CS_Test_Chamber.Developing.GeneralExtensions;
using Developing.MyClasses;

namespace CS_Test_Chamber
{
    static class Program
    {
        static void Main(string[] args)
        {
            var stack = new Stack<int>();

            for (int i = 0; i < 5; i++)
            {
                stack.Push(i);
            }
        }
    }
}
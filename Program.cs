using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Developing.Arrays;


namespace Developing
{
    public static class StackExtender
    {
        public static IEnumerable<N> DoWork<T, N>(this MyClasses.List.Stack<T> stack, Func<T, N> func) 
            where T : IComparable<T>
        {
            foreach (var VARIABLE in stack)
            {
                yield return func(VARIABLE);
            }
        }

        public static MyClasses.List.Stack<T> DoWork2<T>(this MyClasses.List.Stack<T> stack, Func<(T, T), T> func) 
            where T : IComparable<T>
        {
            var res = (T[])stack;

            for (int i = 0; i < res.Length; i++)
            {
                res[i] = func((res[i], (dynamic)i));
            }

            return res;
        }

        public static MyClasses.List.Stack<N> Convert<T, N>(this MyClasses.List.Stack<T> stack) 
            where N : IComparable<N> 
            where T : IComparable<T>
        {
            MyClasses.List.Stack<N> res = new MyClasses.List.Stack<N>();

            foreach (var VARIABLE in stack.Reverse())
            {
                res.Push((N)(dynamic)VARIABLE);
            }

            return res;
        }

        public static MyClasses.List.Stack<N> Convert<T, N>(this MyClasses.List.Stack<T> stack, Func<T,N> func)
            where N : IComparable<N>
            where T : IComparable<T>
        {
            MyClasses.List.Stack<N> res = new MyClasses.List.Stack<N>();

            foreach (var VARIABLE in stack.Reverse())
            {
                res.Push(func(VARIABLE));
            }

            return res;
        }
    }


    static class Program
    {
        static void Main(string[] args)
        {
            MyClasses.List.Stack<int> intchars =
                new MyClasses.List.Stack<char>("Something".ToCharArray()).Convert(lambda => (int)lambda - 48);

            Console.WriteLine(intchars);

            Vector<(int elem, string descr)> vec = new Vector<(int elem, string descr)>();


        }
    }
}
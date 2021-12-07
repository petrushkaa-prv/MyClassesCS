using System;
using System.Collections.Generic;
using System.Linq;


namespace Developing
{
    public static class StackExtender
    {
        public static IEnumerable<N> DoWork<T, N>(this MyClasses.List.Stack<T> stack, Func<T, N> func)
        {
            foreach (var VARIABLE in stack)
            {
                yield return func(VARIABLE);
            }
        }

        public static MyClasses.List.Stack<T> DoWork2<T>(this MyClasses.List.Stack<T> stack, Func<(T, T), T> func)
        {
            var res = (T[])stack;

            for (int i = 0; i < res.Length; i++)
            {
                res[i] = func((res[i], (dynamic)i));
            }

            return res;
        }

        public static MyClasses.List.Stack<N> Convert<T, N>(this MyClasses.List.Stack<T> stack)
        {
            MyClasses.List.Stack<N> res = new MyClasses.List.Stack<N>();

            foreach (var VARIABLE in stack.Reverse())
            {
                res.Push((N)(dynamic)VARIABLE);
            }

            return res;
        }
    }

    static class Program
    {
        static void Main(string[] args)
        {


        }
    }
}
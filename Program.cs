using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
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

    public static class GeneralExtentions
    {
        public static IEnumerable<(T1 valT1, T2 valT2)> DoubleEnumerable<TIn1, TIn2, T1, T2>((TIn1 objIn1, TIn2 objIn2) objectTuple)
            where TIn1 : IEnumerable<T1>
            where TIn2 : IEnumerable<T2>
        {
            var enumIn1 = objectTuple.objIn1.GetEnumerator();
            if (!enumIn1.MoveNext()) yield break;
            var enumIn2 = objectTuple.objIn2.GetEnumerator();
            if (!enumIn2.MoveNext()) yield break;

            do
            {
                yield return (enumIn1.Current, enumIn2.Current);
            } while (enumIn1.MoveNext() && enumIn2.MoveNext());
        }
        

        // huge mess
        public static IEnumerable<(T1 valT1, T2 valT2)> DoubleEnumerableTuples<TIn1, TIn2, T1, T2>(this (TIn1 Item1, TIn2 Item2) objectTuple, Func<(T1, T2), (T1, T2)> func)
            where TIn1 : IEnumerable<T1>
            where TIn2 : IEnumerable<T2>
        {
            var enumIn1 = objectTuple.Item1.GetEnumerator();
            if (!enumIn1.MoveNext()) yield break;
            var enumIn2 = objectTuple.Item2.GetEnumerator();
            if (!enumIn2.MoveNext()) yield break;

            bool finished1 = false, finished2 = false;

            do
            {
                if (finished1)
                    yield return func((default, enumIn2.Current));
                else if (finished2)
                    yield return func((enumIn1.Current, default));
                else
                    yield return func((enumIn1.Current, enumIn2.Current));

                if (!enumIn1.MoveNext()) finished1 = true;
                if (!enumIn2.MoveNext()) finished2 = true;

            } while (!finished1 || !finished2);
        }
    }


    static class Program
    {
        static void Main(string[] args)
        {
            Vector<int> i1 = new Vector<int>(1, 2, 3);
            Vector<int> i2 = new Vector<int>(4, 5, 6);
            Vector<int> i3 = new Vector<int>(7, 8, 9);



            Vector<Vector<int>> matrix = new Vector<Vector<int>>(i1, i2, i3);

            var res = matrix + matrix;

            Console.WriteLine();

        }
    }
}
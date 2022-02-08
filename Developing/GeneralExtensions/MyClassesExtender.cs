using System;
using System.Collections.Generic;
//using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Developing.Interfaces;
using Developing.Nodes;
using Developing.Tree;

namespace Developing.GeneralExtensions
{
    public static class MyClassesExtender
    {
        public static void Print2D<TValue, TNode>(this BinaryTree<TValue, TNode> tree) 
            where TNode : BinaryNode<TValue, TNode>
            where TValue : IComparable<TValue>
        {
            if (tree.Root == null) return;

            Print2DFromNode<TValue, TNode>(tree.Root, 0);
        }

        public static void Print2DFromNode<TValue, TNode>(TNode ptr, int level)
            where TNode : BinaryNode<TValue, TNode>
        {
            if(ptr == null) return;

            Print2DFromNode<TValue, TNode>(ptr.Right, level + 1);

            if (level != 0)
            {
                for (int i = 0; i < level - 1; i++)
                {
                    Console.Write("|\t");
                }
                Console.WriteLine("|-------" + ptr.Value.ToString());
            }
            else
                Console.WriteLine(ptr.Value.ToString());

            Print2DFromNode<TValue, TNode>(ptr.Left, level + 1);
        }



        public static IEnumerable<TOut> DoubleEnumerableTuples<T1, T2, TOut>
        (
            this (IEnumerable<T1> Item1, IEnumerable<T2> Item2) objectTuple,
            Func<(T1, T2), TOut> func = null
        )
        {
            var enumIn1 = objectTuple.Item1.GetEnumerator();
            if (!enumIn1.MoveNext()) yield break;
            var enumIn2 = objectTuple.Item2.GetEnumerator();
            if (!enumIn2.MoveNext()) yield break;

            bool finished1 = false, finished2 = false;

            func ??= arg => (dynamic)arg;

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


        public static IEnumerable<(T1, T2)> GetEnumerable<T1, T2>
        (
            this (IEnumerable<T1>, IEnumerable<T2>) objectTuple
        )
        {
            var enumIn1 = objectTuple.Item1.GetEnumerator();
            if (!enumIn1.MoveNext()) yield break;
            var enumIn2 = objectTuple.Item2.GetEnumerator();
            if (!enumIn2.MoveNext()) yield break;

            bool finished1 = false, finished2 = false;

            do
            {
                if (finished1)
                    yield return (default, enumIn2.Current);
                else if (finished2)
                    yield return (enumIn1.Current, default);
                else
                    yield return (enumIn1.Current, enumIn2.Current);

                if (!enumIn1.MoveNext()) finished1 = true;
                if (!enumIn2.MoveNext()) finished2 = true;

            } while (!finished1 || !finished2);
        }

        public static TClass Transform<TClass, T>(this TClass arg, Func<T, T> func)
            where TClass : IEnumerable<T>, IStackLike<T>, new()
        {
            TClass res = new TClass();

            foreach (var elem in arg)
            {
                res.Push(func(elem));
            }

            return res;
        }
    }
}

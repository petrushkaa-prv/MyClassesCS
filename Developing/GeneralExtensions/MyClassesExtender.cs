using System;
using System.Collections.Generic;
//using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Developing.MyClasses;

namespace CS_Test_Chamber.Developing.GeneralExtensions
{
    public static class MyClassesExtender
    {
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.ComTypes;

namespace Developing
{
    static class StackExtender
    {
        public static IEnumerable<N> DoWork<T,N>(this MyClasses.Stack<T> stack, Func<T,N> func)
        {
            foreach (var VARIABLE in stack)
            {
                yield return func(VARIABLE);
            }
        }

        public static MyClasses.Stack<T> DoWork2<T>(this MyClasses.Stack<T> stack, Func<(T,T),T> func)
        {
            var res = (T[])stack;

            for (int i = 0; i < res.Length; i++)
            {
                res[i] = func((res[i], (dynamic)i));
            }

            return res;
        }

        public static MyClasses.Stack<N> Convert<T, N>(this MyClasses.Stack<T> stack)
        {
            MyClasses.Stack<N> res = new MyClasses.Stack<N>();

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
            MyClasses.Stack<int> name =
                new MyClasses.Stack<char>("PETRUSHKA".ToCharArray()).Convert<char, int>();

            Console.WriteLine(name);
            Console.WriteLine("P->E->T->R->U->S->H->K->A");


        }
    }
}

//Stack<int> stack1 = new Stack<int>();
//Stack<int> stack2 = new Stack<int>();

//Random random = new Random(1337);

//for (int i = 0; i < 8; i++)
//{
//    stack1.Push(random.Next(10));
//    stack2.Push(random.Next(10));
//}

//Console.WriteLine(stack1);
//Console.WriteLine(stack2);

//Stack<int> stack3 = stack1 + stack2;
//Console.WriteLine(stack3);
//Stack<int> stack4 = stack3 - stack1;
//Console.WriteLine(stack4);
//Stack<int> stack5 = stack3 + stack4;
//stack5.Sort();
//Console.WriteLine(stack5);

//Console.WriteLine(stack3[0] + "\t" + stack3[1]);



//var random = new Random();

//Node<int> root = new Node<int>(1);
//root.next = new Node<int>(2);
//root.next.next = new Node<int>(3);

//foreach (var VARIABLE in root)
//{
//    Console.WriteLine(VARIABLE);
//}

//MyClasses.Stack<int> stack = new MyClasses.Stack<int>();
//for (int i = 0; i < 10; i++)
//{
//    stack.Push(i);
//}

//foreach (var VARIABLE in stack)
//{
//    Console.Write(VARIABLE + "\n");
//}

//Matrix<int> mat = new Matrix<int>(5, 7);
//mat.FillWith(11);
//Console.WriteLine(mat);

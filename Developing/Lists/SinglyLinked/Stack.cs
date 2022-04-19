using Developing.Interfaces;
using Developing.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Developing.Lists
{
    public static class StackExtender
    {
        public static IEnumerable<N> DoWork<T, N>(this Developing.Lists.MyStack<T> stack, Func<T, N> func)
            where T : IComparable<T>
        {
            foreach (var elem in stack)
            {
                yield return func(elem);
            }
        }

        public static Developing.Lists.MyStack<T> DoWork2<T>(this Developing.Lists.MyStack<T> stack, Func<(T, T), T> func)
            where T : IComparable<T>
        {
            var res = (T[])stack;

            for (int i = 0; i < res.Length; i++)
            {
                res[i] = func((res[i], (dynamic)i));
            }

            return res;
        }

        public static MyStack<N> Convert<T, N>(this MyStack<T> stack)
            where N : IComparable<N>
            where T : IComparable<T>
        {
            var res = new MyStack<N>();

            foreach (var VARIABLE in stack.Reverse())
            {
                res.Push((N)(dynamic)VARIABLE);
            }

            return res;
        }

        public static MyStack<N> Convert<T, N>(this MyStack<T> stack, Func<T, N> func)
            where N : IComparable<N>
            where T : IComparable<T>
        {
            MyStack<N> res = new MyStack<N>();

            foreach (var VARIABLE in stack.Reverse())
            {
                res.Push(func(VARIABLE));
            }

            return res;
        }
    }

    public class MyStack<T> : SinglyLinked<T>, IEnumerable<T>, IMyClasses<T>, IMyStack<T>
            //where T : IComparable<T>
    {
        private int _size;
        public override int Size => _size;
        private SlNode<T> _head;
        public override SlNode<T> Head => _head;

        public T Peek
        {
            get
            {
                if (IsEmpty) throw new InvalidOperationException();
                return Head.Value;
            }
        }

        public override bool IsEmpty => Size == 0;

        public MyStack()
        {
            _size = 0;
            _head = null;
        }
        public MyStack(params T[] arr)
        {
            foreach (var val in arr)
            {
                this.Push(val);
            }
        }

        public void Push(T value)
        {
            this._size++;

            var newNode = new SlNode<T>(value);

            if (Head == null)
            {
                _head = newNode;
                return;
            }

            newNode.Next = Head;
            _head = newNode;
        }

        public void Pop()
        {
            if (IsEmpty) return;

            _size--;
            _head = _head.Next;
        }

        public MyStack<T> Copy()
        {
            var tmp = (T[])this;
            Array.Reverse(tmp);

            return tmp;
        }

        public void Append(MyStack<T> stack)
        {
            if (stack.IsEmpty) return;

            foreach (var el in stack)
            {
                this.Push(el);
            }
        }
        public void Subtract(MyStack<T> stack, out MyStack<T> subtractedValuesStack)
        {
            if (stack.IsEmpty)
            {
                subtractedValuesStack = null;
                return;
            }

            subtractedValuesStack = new MyStack<T>();

            bool ini = false;
            foreach (var elem in stack)
            {

                while (this.Contains(elem, out var ptr, out var prev))
                {
                    if (prev == null)
                    {
                        _head = ptr.Next;
                    }
                    else
                    {
                        prev.Next = ptr.Next;
                        ptr.Next = null;
                    }


                    if (!ini)
                    {
                        subtractedValuesStack = new MyStack<T>();
                        ini = true;
                    }

                    subtractedValuesStack.Push(ptr.Value);

                    this._size--;
                }
            }
        }

        public static explicit operator T[](MyStack<T> stack)
        {
            T[] res = new T[stack.Size];

            int i = 0;
            //Node.SlNode<T> ptr = stack.TopNode.SlNode;

            foreach (var v in stack)
            {
                res[i] = v;
                i++;
            }

            Array.Reverse(res);

            return res;
        }
        public static implicit operator MyStack<T>(T[] arr) => new MyStack<T>(arr);

        public static bool operator ==(MyStack<T> lhs, MyStack<T> rhs)
        {
            //if (IsNull(lhs) || IsNull(rhs)) return false;
            if (lhs.Size != rhs.Size) return false;

            SlNode<T> rptr = rhs.Head;

            foreach (var v in lhs)
            {
                if ((dynamic)v != (dynamic)rptr.Value) return false;

                rptr = rptr.Next;
            }

            return true;
        }
        public static bool operator !=(MyStack<T> lhs, MyStack<T> rhs)
        {
            return !(lhs == rhs);
        }

        private T this[int idx]
        {
            get
            {
                if (idx > this.Size || idx < 0) throw new IndexOutOfRangeException();

                return ((T[])this)[idx];
            }
            set
            {
                if (idx > this.Size || idx < 0) throw new IndexOutOfRangeException();
                //if (IsEmpty || IsNull(this)) throw new Exception();

                SlNode<T> tmp = Head;

                int i = 0;
                while (tmp != null && i != idx)
                {
                    tmp = tmp.Next;
                    i++;
                }

                tmp.Value = value;
            }
        }


        // TODO: Reimplement by only modifying the original stack
        public MyStack<T> Sort()
        {
            var res = (T[])this;

            Array.Sort(res);

            return res;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return this == (MyStack<T>)obj;
        }
        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(this.Head, this.Size);
        /// <inheritdoc />
        public override string ToString()
        {
            return string.Join(" -> ", (T[])this);
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            if(IsEmpty) yield break;

            var ptr = this.Head;

            while (ptr != null)
            {
                yield return ptr.Value;
                ptr = ptr.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
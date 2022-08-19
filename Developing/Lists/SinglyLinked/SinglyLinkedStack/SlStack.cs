using Developing.Interfaces;
using Developing.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Developing.GeneralInterfaces;

namespace Developing.Lists
{
    public static class StackExtender
    {
        public static Developing.Lists.SlStack<T> DoWork2<T>(this Developing.Lists.SlStack<T> stack, Func<(T, T), T> func)
            where T : IComparable<T>
        {
            var res = (T[])stack;

            for (int i = 0; i < res.Length; i++)
                res[i] = func((res[i], (dynamic)i));

            return res;
        }

        public static SlStack<TOut> Convert<TIn, TOut>(this SlStack<TIn> stack)
            where TOut : IComparable<TOut>
            where TIn : IComparable<TIn>
        {
            var res = new SlStack<TOut>();

            foreach (var elem in stack.Reverse())
                res.Push((TOut)(dynamic)elem);

            return res;
        }

        public static SlStack<N> Convert<T, N>(this SlStack<T> stack, Func<T, N> func)
            where N : IComparable<N>
            where T : IComparable<T>
        {
            SlStack<N> res = new SlStack<N>();

            foreach (var elem in stack.Reverse())
                res.Push(func(elem));

            return res;
        }
    }

    public class SlStack<T> : SinglyLinked<T>, IEnumerable<T>, IMyCollection<T>, IOrderedContainer<T>, IMyStack<T> 
    {
        private int _size;

        /// <inheritdoc/>
        public override int Size => _size;

        private SlNode<T> _head;

        /// <inheritdoc/>
        public override SlNode<T> Head => _head;

        /// <inheritdoc/>
        public T Peek
        {
            get
            {
                if (IsEmpty) throw new InvalidOperationException();
                return Head.Value;
            }
        }

        /// <inheritdoc/>
        public override bool IsEmpty => Size == 0;

        public SlStack()
        {
            _size = 0;
            _head = null;
        }
        public SlStack(params T[] arr)
        {
            foreach (var val in arr)
                this.Push(val);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void Push(params T[] values)
        {
            foreach (var val in values)
                this.Push(val);
        }

        /// <inheritdoc/>
        public T Pop()
        {
            if (IsEmpty) throw new InvalidOperationException("The stack is empty");

            _size--;

            var returnValue = _head.Value;

            _head = _head.Next;

            return returnValue;
        }

        public SlStack<T> Copy()
        {
            var tmp = (T[])this;
            Array.Reverse(tmp);

            return tmp;
        }

        public void Append(SlStack<T> stack)
        {
            if (stack.IsEmpty) return;

            foreach (var el in stack)
                Push(el);
        }
        public void Subtract(SlStack<T> stack, out SlStack<T> subtractedValuesStack)
        {
            if (stack.IsEmpty)
            {
                subtractedValuesStack = null;
                return;
            }

            subtractedValuesStack = new SlStack<T>();

            bool ini = false;
            foreach (var elem in stack)
            {
                while (this.Contains(elem, out var ptr, out var prev))
                {
                    if (prev == null)
                        _head = ptr.Next;
                    else
                    {
                        prev.Next = ptr.Next;
                        ptr.Next = null;
                    }


                    if (!ini)
                    {
                        subtractedValuesStack = new SlStack<T>();
                        ini = true;
                    }

                    subtractedValuesStack.Push(ptr.Value);

                    this._size--;
                }
            }
        }

        public static explicit operator T[](SlStack<T> stack)
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
        public static implicit operator SlStack<T>(T[] arr) => new SlStack<T>(arr);

        public static bool operator ==(SlStack<T> lhs, SlStack<T> rhs)
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
        public static bool operator !=(SlStack<T> lhs, SlStack<T> rhs)
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

        /// <inheritdoc />
        public override void Clear()
        {
            this._head = null;
            _size = 0;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return this == (SlStack<T>)obj;
        }
        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(this.Head, this.Size);
        /// <inheritdoc />
        public override string ToString()
        {
            return string.Join(" -> ", (T[])this);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace Developing.Nodes
{
    /// <summary>
    /// Represents a singly linked Node that implements automatic iterating.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SlNode<T> : IEnumerable<T> 
            where T : IComparable<T>
        {
            public T Value { get; set; }
            public SlNode<T> Next { get; set; }

            public SlNode(T value, SlNode<T> next = null)
            {
                Value = value;
                Next = next;
            }

            //class SinglyLinkedEnumerator : IEnumerator<T>
            //{
            //    //private int _pos = -1;
            //    private bool _start = true;
            //    private SlNode<T> _root;
            //    private SlNode<T> _current;
            //    public SinglyLinkedEnumerator(SlNode<T> newroot)
            //    {
            //        _current = _root = newroot;
            //    }
            //    public bool MoveNext()
            //    {
            //        if (_root == null) return false;
            //        if (_start)
            //        {
            //            _start = false;
            //            return true;
            //        }
            //        _current = _current.Next;
            //        return _current != null;
            //    }
            //    public void Reset()
            //    {
            //        _current = _root;
            //        _start = true;
            //    }
            //    public T Current
            //    {
            //        get
            //        {
            //            if (_current == null)
            //                throw new InvalidOperationException();
            //            return _current.Value;
            //        }
            //    }
            //    object? IEnumerator.Current => Current;
            //    public void Dispose()
            //    {
            //        GC.SuppressFinalize(this);
            //    }
            //}
            //public IEnumerator<T> GetEnumerator()
            //{
            //    return new SinglyLinkedEnumerator(this);
            //}

            /// <inheritdoc />
            public IEnumerator<T> GetEnumerator()
            {
                SlNode<T> ptr = this;

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

    /// <summary>
    /// Represents a Binary Search Tree Node containing the main value
    /// in it and two children.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BstNode<T>
        where T : IComparable<T>
    {
        public T Value { get; set; }
        public BstNode<T> Left { get; set; }
        public BstNode<T> Right { get; set; }
    }
}

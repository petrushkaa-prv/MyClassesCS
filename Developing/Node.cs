using System;
using System.Collections;
using System.Collections.Generic;

namespace Developing.MyClasses
{
    public class Node<T> : IEnumerable<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }

        public Node(T value, Node<T> next = null)
        {
            Value = value;
            Next = next;
        }

        class NodeEnumerator : IEnumerator<T>
        {
            //private int _pos = -1;

            private bool _start = true;
            private Node<T> _root;
            private Node<T> _current;

            public NodeEnumerator(Node<T> newroot)
            {
                _current = _root = newroot;
            }

            public bool MoveNext()
            {
                if (_root == null) return false;

                if (_start)
                {
                    _start = false;
                    return true;
                }

                _current = _current.Next;

                return _current != null;
            }

            public void Reset()
            {
                _current = _root;
                _start = true;
            }

            public T Current
            {
                get
                {
                    if (_current == null)
                        throw new InvalidOperationException();

                    return _current.Value;
                }
            }

            object? IEnumerator.Current => Current;

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new NodeEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

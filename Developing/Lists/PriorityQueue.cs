using Developing.MyClasses.Node;
using System;

namespace Developing.MyClasses
{
    namespace List
    {
        public class MinPriorityQueue<T> : IMyClasses<T> where T : IComparable<T>
        {
            private int _count = 0;
            public int Size => _count;
            public bool IsEmpty => _count == 0;

            private Node.SinglyLinked<T> _head = null;

            private T _current;
            public T Peek
            {
                get
                {
                    if (IsEmpty) throw new InvalidOperationException();
                    return _current;
                }
            }

            public MinPriorityQueue()
            {

            }

            public void Push(T elem)
            {
                var newNode = new SinglyLinked<T>(elem, _head);
                _head = newNode;
                _count++;

                if (_head != null && elem.CompareTo(Peek) < 0)
                {
                    _current = elem;
                }
            }
            public void Pop()
            {
                if (IsEmpty) return;

                _count--;
                _head = _head.Next;
                if (_current.CompareTo(_head.Value) < 0) _current = _head.Value;
            }


            /// <inheritdoc />
            public void FillWith(T element, int howMany = 0)
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc />
            public bool Contains(T element)
            {
                throw new NotImplementedException();
            }
        }
    }
}

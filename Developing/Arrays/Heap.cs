using Developing.MyClasses;
using System;

namespace CS_Test_Chamber.Developing.Arrays
{
    internal static class Heap
    {
        public class Min<T> : IMyClasses<T>
            where T : IComparable<T>
        {
            private T[] _heapElements;
            private int _heapSize;

            /// <inheritdoc />
            public int Size { get; private set; }
            /// <inheritdoc />
            public bool IsEmpty => Size == 0;

            private int GetIdxLeft(int idx) => 2 * idx + 1;
            private int GetIdxRight(int idx) => 2 * idx + 2;
            private int GetIdxParent(int idx) => (idx - 1) / 2;

            private T GetLeft(int idx) => _heapElements[GetIdxLeft(idx)];
            private T GetRight(int idx) => _heapElements[GetIdxRight(idx)];
            private T GetParent(int idx) => _heapElements[GetIdxParent(idx)];

            private bool HasLeft(int idx) => GetIdxLeft(idx) < Size;
            private bool HasRight(int idx) => GetIdxRight(idx) < Size;
            private bool HasParent(int idx) => GetIdxParent(idx) < Size;

            private void Swap(int idx1, int idx2)
            {
                (_heapElements[idx1], _heapElements[idx2]) = (_heapElements[idx2], _heapElements[idx1]);
            }

            private void Expand()
            {
                if (Size < _heapSize) return;

                var temp = _heapElements;
                _heapElements = new T[_heapSize *= 2];
                for (int i = 0; i < temp.Length; i++)
                {
                    _heapElements[i] = temp[i];
                }
            }

            public Min(int capacity = 2)
            {
                _heapElements = new T[_heapSize = capacity];
                Size = 0;
            }

            public T Peek()
            {
                if (IsEmpty) throw new InvalidOperationException("Heap is empty");

                return _heapElements[0];
            }
            public T Poll()
            {
                T res = Peek();

                _heapElements[0] = _heapElements[Size -= 1];

                HeapifyDown();

                return res;
            }
            public void Add(T item)
            {
                _heapElements[Size++] = item;

                Expand();

                HeapifyUp();
            }

            private void HeapifyUp()
            {
                int i = Size - 1;

                while (HasParent(i) && GetParent(i).CompareTo(_heapElements[i]) > 0)
                {
                    Swap(GetIdxParent(i), i);
                    i = GetIdxParent(i);
                }
            }
            private void HeapifyDown()
            {
                int i = 0;

                while (HasLeft(i))
                {
                    int j = GetIdxLeft(i);

                    if (HasRight(i) && GetRight(i).CompareTo(GetLeft(i)) < 0)
                    {
                        j = GetIdxRight(i);
                    }

                    if (_heapElements[i].CompareTo(_heapElements[j]) < 0)
                    {
                        break;
                    }
                    else
                    {
                        Swap(i, j);
                    }

                    i = j;
                }
            }

            /// <inheritdoc />
            public void FillWith(T element, int howMany = 0)
            {
                throw new NotImplementedException();
            }
            /// <inheritdoc />
            public bool Contains(T element)
            {
                return Search(element) >= 0;
            }
            public int Search(T element)
            {
                int i = 0;

                do
                {
                    i = element.CompareTo(GetLeft(i)) > 0 ? GetIdxLeft(i) : GetIdxRight(i);
                } 
                while (HasLeft(i) || HasRight(i) || _heapElements[i].CompareTo(element) == 0);

                return _heapElements[i].CompareTo(element) == 0 ? i : -1;
            }

            /// <inheritdoc />
            public override string ToString()
            {
                string res = string.Empty;

                foreach (var elem in _heapElements)
                {
                    res += elem;
                }

                return res;
            }
        }
    }
}

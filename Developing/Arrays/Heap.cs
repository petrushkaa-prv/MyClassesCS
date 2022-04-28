using Developing.Interfaces;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Developing.Arrays
{
    internal class Heap<T>
    {
        private T[] _heap; // _heap[0] is a sentinel
        private int _index;

        public T Max => _heap[1];
        public T Min => _heap[FindMin(1)];

        public Heap()
        {
            _heap = new T[3];
            _heap[0] = default;
            _index = 1;
        }
        public Heap(params T[] arr)
        {
            CreateFromBottom(arr);
        }



        private void Extend()
        {
            var tempHeap = _heap;
            _heap = new T[2 * tempHeap.Length];

            for (int i = 0; i < tempHeap.Length; i++) 
                _heap[i] = tempHeap[i];
        }

        public static void UpHeap(T[] Arr, int i)
        {
            Arr[0] = default;
            var val = Arr[i];

            for (; Arr[i] < (dynamic)val; i /= 2) 
                Arr[i] = Arr[i / 2];

            Arr[i] = val;
        }
        public static void DownHeap(T[] Arr, int i, int iMax)
        {
            var k = 2 * i;
            var val = Arr[i];

            while (k <= iMax)
            {
                if(k + 1 <= iMax)
                    if (Arr[k + 1] > (dynamic)Arr[k])
                        k++;

                if (Arr[k] > (dynamic)val)
                {
                    Arr[i] = Arr[k];
                    i = k;
                    k = 2 * i;
                }
                else
                    break;
            }

            Arr[i] = val;
        }

        public int Search(T value)
        {
            var idx = 0;
            while (++idx < _index && value != (dynamic)_heap[idx])
            {
            }

            return value == (dynamic)_heap[idx] ? idx : -1;
        }

        public void Insert(T value)
        {
            _index++;

            if (_index >= _heap.Length)
                Extend();

            _heap[_index] = value;
            UpHeap(_heap,_index);
        }
        public void Delete(T value)
        {
            var idx = Search(value);
            
            if(idx > 1) Delete(index: idx);
        }
        public void Delete(int index)
        {
            if (index < 0 || index > _index)
                throw new IndexOutOfRangeException();

            _heap[index] = _heap[_index];
            _index--;

            DownHeap(_heap, index, _index);
        }
        public T DeleteMax()
        {
            if (_index < 1)
                throw new InvalidOperationException("The heap is empty");

            var res = Max;

            _heap[1] = _heap[_index];
            _index--;

            DownHeap(_heap, 1, _index);

            return res;
        }
        public T DeleteMin()
        {
            var minIdx = FindMin(1);
            var res = _heap[minIdx];

            Delete(index: minIdx);

            return res;
        }

        public int FindMin(int idx)
        {
            if(idx < 0 || idx > _index) return -1;

            var minIdx = idx;
            for (int i = idx + 1; i < _index; i++)
                if (_heap[i] < (dynamic)_heap[minIdx])
                    minIdx = i;

            return minIdx;
        }

        private void CreateFromTop(T[] arr)
        {
            _index = 1;

            foreach (var elem in arr) 
                Insert(elem);
        }
        private void CreateFromBottom(T[] arr)
        {
            _index = arr.Length;
            _heap = new T[arr.Length + 1];
            for (int i = 0; i < arr.Length; i++) 
                _heap[i + 1] = arr[i];

            for (int i = _index / 2; i >= 1; i--) 
                DownHeap(_heap, i, _index);
        }

        // ReSharper disable once IdentifierTypo
        public static T[] Heapify(T[] arr)
        {
            var heap = new T[arr.Length + 1];
            heap[0] = default;

            for (int i = 0; i < arr.Length; i++) 
                heap[i + 1] = arr[i];

            for (int i = (heap.Length - 1) / 2; i >= 1; i--) 
                DownHeap(heap, i, heap.Length - 1);

            return heap;
        }



        /// <inheritdoc />
        public override string ToString() => string.Join(" ", _heap);
        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(_heap, _index);
    }
}

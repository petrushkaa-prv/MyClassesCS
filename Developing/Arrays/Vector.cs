using System;
using System.Collections.Generic;
using System.Text;
using Developing.MyClasses;

namespace Developing.Arrays
{
    class Vector<T> : IMyClasses<T>
    {
        private T[] _vector;
        private int _size;
        private int _count;

        public int Count => _count;

        /// <inheritdoc />
        public bool IsEmpty => _count == 0;
        public int Size => _size;

        public Vector()
        {
            _vector = new T[2];
            _count = 0;
            _size = 2;
        }

        public Vector(T[] arr)
        {
            _count = arr.Length;
            _size = 2 * arr.Length;
            _vector = new T[_size];

            for (int i = 0; i < arr.Length; i++)
            {
                _vector[i] = arr[i];
            }
        }

        public void Push(T el)
        {
            _vector[_count++] = el;
        }

        private void Extend()
        {
            if (Count == Size)
            {
                T[] temp = _vector;
                _vector = new T[_size *= 2];

                for (int i = 0; i < temp.Length; i++)
                {
                    _vector[i] = temp[i];
                }
            }
        }

        /// <inheritdoc />
        public void FillWith(T element, int howMany = 0)
        {
            for (_count = 0; _count < howMany; _count++)
            {
                _vector[_count] = element;

                Extend();
            }
        }

        /// <inheritdoc />
        public bool Contains(T element)
        {
            return Array.BinarySearch(_vector, element) >= 0;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            string res = string.Empty;

            for (int i = 0; i < _count; i++)
            {
                res += _vector[i].ToString();
            }

            return res;
        }
    }
}

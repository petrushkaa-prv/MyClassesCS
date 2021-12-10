using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Developing.MyClasses;

namespace Developing.Arrays
{
    public static class VectorExtension
    {

    }

    public class Vector<T> : IMyClasses<T>, IEnumerable<T>
        where T : struct
    {
        private protected T[] _vector;
        private protected int _size;
        private protected int _count;

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

        public virtual void Push(T el)
        {
            _vector[_count++] = el;
            
            Extend();
        }

        protected void Extend()
        {
            if (_count >= _size)
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

        public void Append(Vector<T> vec)
        {
            foreach (var VARIABLE in vec)
            {
                Push(VARIABLE);
            }
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return _vector[i];
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

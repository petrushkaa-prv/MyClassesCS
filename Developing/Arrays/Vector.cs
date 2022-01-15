using CS_Test_Chamber.Developing.GeneralExtensions;
using Developing.MyClasses;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CS_Test_Chamber.Developing.Arrays
{
    public static class VectorExtension
    {

    }

    public class Vector<T> : IMyClasses<T>, IStackLike<T>, IEnumerable<T>
    // where T : struct
    {
        private protected T[] _vector;
        private protected int _size;
        private protected int _count;

        public int Count => _count;
        public T Peek => _vector[Count];

        /// <inheritdoc />
        public bool IsEmpty => _count == 0;
        public int Size => _size;

        public Vector()
        {
            _vector = new T[2];
            _count = 0;
            _size = 2;
        }

        public Vector(params T[] arr)
        {
            _count = arr.Length;
            _size = 2 * _count;
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

        public void Pop()
        {
            if (IsEmpty) return;

            _vector[_count--] = default;
        }
        public virtual Vector<T> Copy()
        {
            Vector<T> res = new Vector<T>();

            foreach (var elem in this)
            {
                res.Push(elem);
            }

            return res;
        }
        protected void Extend()
        {
            if (_count < _size) return;

            T[] temp = _vector;
            _vector = new T[_size *= 2];

            for (int i = 0; i < temp.Length; i++)
            {
                _vector[i] = temp[i];
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
        public void Append(Vector<T> vec)
        {
            foreach (var VARIABLE in vec)
            {
                Push(VARIABLE);
            }
        }

        public static Vector<T> operator +(Vector<T> lhs, Vector<T> rhs)
        {
            var res = new Vector<T>();

            foreach (var elemTuple in (lhs, rhs).
                DoubleEnumerableTuples<T, T, T>(arg => arg.Item1 + (dynamic)arg.Item2))
            {
                res.Push(elemTuple);
            }

            return res;
        }
        public static Vector<T> operator -(Vector<T> lhs, Vector<T> rhs)
        {
            return lhs + rhs.Transform<Vector<T>, T>(arg => -(dynamic)arg);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return base.Equals((Vector<T>)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(_vector, _count, _size);

        /// <inheritdoc />
        public override string ToString()
        {
            string res = string.Empty;

            foreach (var elem in this)
            {
                res += elem + "\t";
            }

            return res;
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

using Developing.MyClasses;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CS_Test_Chamber.Developing.Arrays
{
    public static class VectorExtension
    {

    }

    // TODO: Return to basic Vector Implementation, and separate this to another project
    public class Vector<T> : IStackLike<T>, IEnumerable<T>
    {
        private protected T[] _vector;

        protected internal int Count { get; internal set; }
        protected internal int Size { get; internal set; }

        public T Peek => _vector[Count];
        
        public bool IsEmpty => Count == 0;

        public Vector()
        {
            _vector = new T[2];
            //_count = 0;
            Count = 0;
            Size = 2;
        }
        public Vector(params T[] arr)
        {
            Count = arr.Length;
            Size = 2 * Count;
            _vector = new T[Size];

            for (int i = 0; i < Count; i++)
            {
                _vector[i] = arr[i];
            }
        }
        private Vector(T[] array, int count, int size)
        {
            if (size < array.Length) throw new ArgumentException();

            _vector = new T[Size = size];
            Count = count;

            for (int i = 0; i < Count; i++)
            {
                _vector[i] = array[i];
            }
        }

        public virtual void Push(T el)
        {
            _vector[Count++] = el;

            Extend();
        }
        public virtual void Pop()
        {
            if (IsEmpty) return;

            _vector[Count--] = default;
        }
        protected virtual Vector<T> Copy()
        {
            return new Vector<T>(this._vector, this.Count ,this.Size);
        }
        protected void Extend()
        {
            if (Count < Size) return;

            T[] temp = _vector;
            _vector = new T[Size *= 2];

            for (int i = 0; i < temp.Length; i++)
            {
                _vector[i] = temp[i];
            }
        }

        public void FillWith(T element, int howMany = 0)
        {
            for (Count = 0; Count < howMany; Count++)
            {
                _vector[Count] = element;

                Extend();
            }
        }
        public virtual bool Contains(T element)
        {
            return Array.BinarySearch(_vector, element) >= 0;
        }
        public virtual void Append(Vector<T> vec)
        {
            foreach (var elem in vec)
            {
                Push(elem);
            }
        }

        public static Vector<T> operator +(Vector<T> lhs, Vector<T> rhs)
        {
            if (lhs.IsEmpty) return rhs.Copy();
            if (rhs.IsEmpty) return lhs.Copy();

            if (lhs.Count < rhs.Count)
            {
                (lhs, rhs) = (rhs, lhs);
            }

            var res = lhs.Copy();

            for (int i = 0; i < rhs.Count; i++)
            {
                res[i] += (dynamic)rhs[i];
            }

            return res;
        }
        public static Vector<T> operator -(Vector<T> lhs, Vector<T> rhs)
        {
            return lhs + (-rhs);
        }
        public static Vector<T> operator -(Vector<T> vec)
        {
            var res = vec.Copy();
            if (vec.IsEmpty) return res;

            for (int i = 0; i < vec.Count; i++)
            {
                res[i] -= (dynamic)vec[i];
            }

            return res;
        }

        public static Vector<T> operator +(Vector<T> vec, T elem)
        {
            var res = vec.Copy();

            if (elem == (dynamic)0) return res;

            for (int i = 0; i < res.Count; i++)
            {
                res[i] += (dynamic)elem;
            }

            return res;
        }
        public static Vector<T> operator -(Vector<T> vec, T elem)
        {
            return vec + (-(dynamic)elem);
        }
        public static BoolVector operator ==(Vector<T> lhs, Vector<T> rhs)
        {
            if (lhs.IsEmpty || rhs.IsEmpty) return false;

            if (lhs.Count < rhs.Count)
            {
                (lhs, rhs) = (rhs, lhs);
            }

            var res = new BoolVector
            {
                _vector = new bool[lhs.Size],
                Count = lhs.Count,
                Size = lhs.Size
            };

            for (int i = 0; i < res.Count; i++)
            {
                res[i] = lhs[i] == (dynamic)rhs[i];
            }

            return res;
        }
        public static BoolVector operator !=(Vector<T> lhs, Vector<T> rhs)
        {
            return !(lhs == rhs);
        }

        private protected T this[int idx]
        {
            get
            {
                if (idx < 0 || idx > Count) throw new IndexOutOfRangeException();

                return _vector[idx];
            }
            set
            {
                if (idx < 0 || idx > Count) throw new IndexOutOfRangeException();

                _vector[idx] = value;
            }
        }

        public static explicit operator BoolVector(Vector<T> vector)
        {
            var res = new BoolVector();
            if (vector.IsEmpty) return res;

            for (int i = 0; i < vector.Count; i++)
            {
                res[i] = vector[i] == (dynamic)0 ? 0 : (dynamic)vector[i];
            }

            return res;
        }
        public static explicit operator Vector<T>(BoolVector boolVector)
        {
            var resVec = new T[boolVector.Size];

            for (int i = 0; i < boolVector.Count; i++)
            {
                resVec[i] = boolVector[i] ? (dynamic)1 : 0;
            }

            return new Vector<T>
            {
                _vector = resVec,
                Count = boolVector.Count,
                Size = boolVector.Size
            };
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return base.Equals((Vector<T>)obj);
        }
        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(_vector, Count, Size);
        /// <inheritdoc />
        public override string ToString()
        {
            string res = string.Empty;

            foreach (var elem in this)
            {
                res += elem + " ";
            }

            return res;
        }
        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
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

    public class BoolVector : Vector<bool>
    {
        public BoolVector() : base()
        {

        }
        public BoolVector(params bool[] arr) : base(arr)
        {

        }
        public BoolVector(params int[] arr)
        {
            Count = arr.Length;
            Size = 2 * Count;
            _vector = new bool[Size];

            for (int i = 0; i < Count; i++)
            {
                _vector[i] = arr[i] > 0;
            }
        }
        private BoolVector(bool[] array, int count, int size)
        {
            if (size < array.Length) throw new ArgumentOutOfRangeException();

            _vector = new bool[Size = size];
            Count = count;

            for (int i = 0; i < Count; i++)
            {
                _vector[i] = array[i];
            }
        }
        
        public new BoolVector Copy()
        {
            return (BoolVector)base.Copy();
        }

        public static BoolVector operator !(BoolVector vector)
        {
            var res = vector.Copy();
            for (int i = 0; i < vector.Size; i++)
            {
                res[i] = !vector[i];
            }

            return res;
        }

        public static explicit operator bool(BoolVector vector)
        {
            if(vector.IsEmpty) return false;

            foreach (var elem in vector)
            {
                if (!elem) return false;
            }

            return true;
        }
        public static implicit operator BoolVector(bool state)
        {
            return new BoolVector(state);
        }
    }
}

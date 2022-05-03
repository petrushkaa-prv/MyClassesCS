using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Arrays
{
    public abstract class MatrixBase<T>
        where T : struct, IComparable<T>
    {
        protected readonly T[] _matrixElements;
        protected readonly int _matrixSize;
        protected readonly int _matrixRows;
        protected readonly int _matrixColumns;

        public virtual int Size =>  _matrixSize;
        public virtual bool IsSquare => _matrixColumns == _matrixRows;

        public MatrixBase()
        {
            _matrixElements = null;
            _matrixSize = 0;
            _matrixRows = 0;
            _matrixColumns = 0;
        }
        public MatrixBase(int size)
        {
            _matrixRows = size;
            _matrixColumns = size;
            _matrixSize = size * size;
            _matrixElements = new T[_matrixSize];
        }
        public MatrixBase(int rows, int cols)
        {
            _matrixRows = rows;
            _matrixColumns = cols;
            _matrixSize = rows * cols;
            _matrixElements = new T[_matrixSize];
        }
        public MatrixBase(int rows, int cols, T[] arr)
        {
            _matrixRows = rows;
            _matrixColumns = cols;
            _matrixSize = rows * cols;
            _matrixElements = new T[_matrixSize];

            for (int i = 0; i < arr.Length; i++)
                _matrixElements[i] = arr[i];
        }
        public MatrixBase(MatrixBase<T> old)
        {
            _matrixSize = old._matrixSize;
            _matrixColumns = old._matrixColumns;
            _matrixRows = old._matrixRows;
            _matrixElements = new T[_matrixSize];

            for (int i = 0; i < old._matrixSize; i++)
                _matrixElements[i] = old._matrixElements[i];
        }

        public virtual T this[int i]
        {
            get
            {
                if(i < 0 || i >= _matrixSize)
                    throw new IndexOutOfRangeException();

                return _matrixElements[i];
            }
            set
            {
                if (i < 0 || i >= _matrixSize)
                    throw new IndexOutOfRangeException();

                _matrixElements[i] = value;
            }
        }

        public virtual T this[int i, int j]
        {
            get
            {
                if (i < 0 || j < 0 || i >= _matrixRows || i >= _matrixColumns)
                    throw new IndexOutOfRangeException();

                return _matrixElements[i * _matrixColumns + j];
            }
            set
            {
                if (i < 0 || j < 0 || i >= _matrixRows || i >= _matrixColumns)
                    throw new IndexOutOfRangeException();

                _matrixElements[i * _matrixColumns + j] = value;
            }
        }

        public static bool operator ==(MatrixBase<T> lhs, MatrixBase<T> rhs)
        {
            if (lhs is null || rhs is null) return false;

            if (lhs._matrixSize != rhs._matrixSize ||
                lhs._matrixColumns != rhs._matrixColumns ||
                lhs._matrixRows != rhs._matrixRows)
                return false;

            for (int i = 0; i < lhs._matrixSize; i++)
                if (lhs._matrixElements != rhs._matrixElements)
                    return false;

            return true;
        }
        public static bool operator !=(MatrixBase<T> lhs, MatrixBase<T> rhs)
        {
            return !(lhs == rhs);
        }

        public abstract MatrixBase<T> ToPower(int power);

        public virtual void FillWith(T element)
        {
            for (int i = 0; i < _matrixSize; i++)
                _matrixElements[i] = element;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < _matrixSize; i++)
                sb.Append((i + 1) % _matrixRows != 0 ? $"{_matrixElements[i]}\t" : $"{_matrixElements[i]}\n");

            return sb.ToString();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return (MatrixBase<T>)obj == this;
        }

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCode.Combine(_matrixElements, _matrixColumns, _matrixRows, _matrixSize, typeof(T));
    }
}

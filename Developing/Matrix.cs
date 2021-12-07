#region Europe
// Matrix.cs
// 
// CS Test Chamber
// CS Test Chamber
// 
// 2021
// 10
// 26
#endregion

namespace Developing.MyClasses
{
    public class Matrix<T> : IMyClasses<T>
    {
        private T[] _matrixElements = null;
        private int _matrixSize = 0;
        private int _matrixRows = 0;
        private int _matrixColumns = 0;

        public Matrix()
        {

        }

        public Matrix(int size)
        {
            _matrixRows = size;
            _matrixColumns = size;
            _matrixSize = size * size;
            _matrixElements = new T[_matrixSize];
        }

        public Matrix(int rows, int cols)
        {
            _matrixRows = rows;
            _matrixColumns = cols;
            _matrixSize = rows * cols;
            _matrixElements = new T[_matrixSize];
        }

        public Matrix(int rows, int cols, T[] arr)
        {
            _matrixRows = rows;
            _matrixColumns = cols;
            _matrixSize = rows * cols;
            _matrixElements = new T[_matrixSize];

            for (int i = 0; i < arr.Length; i++)
            {
                _matrixElements[i] = arr[i];
            }
        }

        public Matrix(Matrix<T> old)
        {
            _matrixSize = old._matrixSize;
            _matrixColumns = old._matrixColumns;
            _matrixRows = old._matrixRows;
            _matrixElements = new T[_matrixSize];

            for (int i = 0; i < old._matrixSize; i++)
            {
                _matrixElements[i] = old._matrixElements[i];
            }
        }

        public static Matrix<T> operator +(Matrix<T> lhs, Matrix<T> rhs)
        {
            if (lhs._matrixSize != rhs._matrixSize) throw new System.Exception("Matrix dimensions must agree");

            Matrix<T> res = new Matrix<T>(lhs._matrixSize);

            for (int i = 0; i < lhs._matrixSize; i++)
            {
                res._matrixElements[i] = (dynamic)lhs._matrixElements[i] + (dynamic)rhs._matrixElements[i];
            }

            return res;
        }

        public static Matrix<T> operator -(Matrix<T> old)
        {
            Matrix<T> res = old;

            for (int i = 0; i < res._matrixSize; i++)
            {
                res._matrixElements[i] = -(dynamic)res._matrixElements[i];
            }

            return res;
        }

        public static Matrix<T> operator -(Matrix<T> lhs, Matrix<T> rhs)
        {
            if (lhs._matrixSize != rhs._matrixSize) throw new System.Exception("Matrix dimensions must agree");
            
            return new Matrix<T>(lhs + (-rhs));
        }

        /// <inheritdoc />
        public void FillWith(T element, int howMany = 0)
        {
            for (int i = 0; i < _matrixSize; i++)
            {
                _matrixElements[i] = element;
            }
        }

        public override string ToString()
        {
            string res = new string("");

            for (int i = 0; i < _matrixSize; i++)
            {
                res += (i + 1) % _matrixRows != 0 ? _matrixElements[i] + "\t" : _matrixElements[i] + "\n";
                /*res.Append((i + 1) % _matrixRows != 0 ? _matrixElements[i] + "\t" : _matrixElements[i] + "\n")*/
            }

            return res;
        }
    }
}
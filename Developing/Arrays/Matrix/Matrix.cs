using System;
using System.Text;
using Developing.GeneralInterfaces;
using Developing.Interfaces;

namespace Developing.Arrays
{
    public class Matrix<T> : MatrixBase<T>
        where T : struct, IComparable<T>
    {
        public Matrix() : base()
        {
        }
        public Matrix(int size) : base(size)
        {
        }
        public Matrix(int rows, int cols) : base(rows, cols)
        {
        }
        public Matrix(int rows, int cols, T[] arr) : base(rows, cols, arr)
        {
        }
        public Matrix(MatrixBase<T> old) : base(old)
        {
        }

        public static Matrix<T> operator -(Matrix<T> old)
        {
            var res = new Matrix<T>(old._matrixSize);

            for (int i = 0; i < res._matrixSize; i++)
                res._matrixElements[i] = -(dynamic)res._matrixElements[i];

            return res;
        }

        public static Matrix<T> operator +(Matrix<T> lhs, Matrix<T> rhs)
        {
            if (lhs._matrixSize != rhs._matrixSize) 
                throw new System.Exception("Matrix dimensions must agree");

            var res = new Matrix<T>(lhs._matrixSize);

            for (int i = 0; i < lhs._matrixSize; i++)
                res._matrixElements[i] = lhs._matrixElements[i] + (dynamic)rhs._matrixElements[i];

            return res;
        }
        public static Matrix<T> operator -(Matrix<T> lhs, Matrix<T> rhs)
        {
            if (lhs._matrixSize != rhs._matrixSize) 
                throw new System.Exception("Matrix dimensions must agree");
            
            return new Matrix<T>(lhs + (-rhs));
        }
        public static Matrix<T> operator +(Matrix<T> matrix, T val)
        {
            var res = new Matrix<T>(matrix);

            for (int i = 0; i < res._matrixSize; i++)
                res._matrixElements[i] += (dynamic)val;

            return res;
        }
        public static Matrix<T> operator -(Matrix<T> matrix, T val)
        {
            return matrix + -(dynamic)val;
        }

        public static Matrix<T> operator *(Matrix<T> lhs, Matrix<T> rhs)
        {
            if (rhs._matrixColumns != lhs._matrixRows)
                throw new Exception("Matrices shapes mismatch");

            var res = new Matrix<T>(lhs._matrixRows, rhs._matrixColumns);

            for(int i = 0; i < lhs._matrixRows; i++)
                for (int j = 0; j < rhs._matrixColumns; j++)
                {
                    T sum = default;

                    for (int k = 0; k < rhs._matrixRows; k++)
                        sum += (dynamic)lhs._matrixElements[i * lhs._matrixColumns + k] *
                               rhs._matrixElements[k * rhs._matrixColumns + j];

                    res._matrixElements[i * rhs._matrixColumns + j] = sum;
                }

            return res;
        }
        public static Matrix<T> operator *(Matrix<T> matrix, T val)
        {
            var res = new Matrix<T>(matrix);

            for (int i = 0; i < res._matrixSize; i++)
                res._matrixElements[i] *= (dynamic)val;

            return res;
        }
        public static Matrix<T> operator /(Matrix<T> matrix, T val)
        {
            var res = new Matrix<T>(matrix);

            for (int i = 0; i < res._matrixSize; i++)
                res._matrixElements[i] /= (dynamic)val;

            return res;
        }

        public static bool operator ==(Matrix<T> lhs, Matrix<T> rhs)
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
        public static bool operator !=(Matrix<T> lhs, Matrix<T> rhs)
        {
            return !(lhs == rhs);
        }



        public static implicit operator ElementViseMatrix<T>(Matrix<T> matrix)
        {
            return new ElementViseMatrix<T>(matrix);
        }

        public ElementViseMatrix<T> ElementVise() => this;

        public override MatrixBase<T> ToPower(int power)
        {
            // TODO: Maybe a throw would be better?
            if (!IsSquare) return null;

            var res = new Matrix<T>(this);

            for (int i = 0; i < power; i++)
                res *= this;

            return res;
        }

        public void FillWith(T element, int howMany = 0)
        {
            for (int i = 0; i < _matrixSize; i++) 
                _matrixElements[i] = element;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Arrays
{
    class ElementViseMatrix<T> : MatrixBase<T>
        where T : struct, IComparable<T>
    {
        public ElementViseMatrix() : base()
        {
        }
        public ElementViseMatrix(int size) : base(size)
        {
        }
        public ElementViseMatrix(int rows, int cols) : base(rows, cols)
        {
        }
        public ElementViseMatrix(int rows, int cols, T[] arr) : base(rows, cols, arr)
        {
        }
        public ElementViseMatrix(MatrixBase<T> old) : base(old)
        {
        }

        public static ElementViseMatrix<T> operator -(ElementViseMatrix<T> old)
        {
            var res = new ElementViseMatrix<T>(old._matrixSize);

            for (int i = 0; i < res._matrixSize; i++)
                res._matrixElements[i] = -(dynamic)res._matrixElements[i];

            return res;
        }

        public static ElementViseMatrix<T> operator +(ElementViseMatrix<T> lhs, ElementViseMatrix<T> rhs)
        {
            if (lhs._matrixSize != rhs._matrixSize)
                throw new System.Exception("Matrix dimensions must agree");

            var res = new ElementViseMatrix<T>(lhs._matrixSize);

            for (int i = 0; i < lhs._matrixSize; i++)
                res._matrixElements[i] = lhs._matrixElements[i] + (dynamic)rhs._matrixElements[i];

            return res;
        }
        public static ElementViseMatrix<T> operator -(ElementViseMatrix<T> lhs, ElementViseMatrix<T> rhs)
        {
            if (lhs._matrixSize != rhs._matrixSize)
                throw new System.Exception("Matrix dimensions must agree");

            return new ElementViseMatrix<T>(lhs + (-rhs));
        }
        public static ElementViseMatrix<T> operator +(ElementViseMatrix<T> matrix, T val)
        {
            var res = new ElementViseMatrix<T>(matrix);

            for (int i = 0; i < res._matrixSize; i++)
                res._matrixElements[i] += (dynamic)val;

            return res;
        }
        public static ElementViseMatrix<T> operator -(ElementViseMatrix<T> matrix, T val)
        {
            return matrix + -(dynamic)val;
        }

        public static ElementViseMatrix<T> operator *(ElementViseMatrix<T> lhs, ElementViseMatrix<T> rhs)
        {
            if (rhs._matrixColumns != lhs._matrixColumns || rhs._matrixRows != lhs._matrixRows)
                throw new Exception("Matrices shapes mismatch");

            var res = new ElementViseMatrix<T>(lhs);

            for (int i = 0; i < res._matrixSize; i++)
                res[i] *= (dynamic)rhs[i];

            return res;
        }
        public static ElementViseMatrix<T> operator *(ElementViseMatrix<T> matrix, T val)
        {
            var res = new ElementViseMatrix<T>(matrix);

            for (int i = 0; i < res._matrixSize; i++)
                res._matrixElements[i] *= (dynamic)val;

            return res;
        }
        public static ElementViseMatrix<T> operator /(ElementViseMatrix<T> matrix, T val)
        {
            var res = new ElementViseMatrix<T>(matrix);

            for (int i = 0; i < res._matrixSize; i++)
                res._matrixElements[i] /= (dynamic)val;

            return res;
        }
    }
}

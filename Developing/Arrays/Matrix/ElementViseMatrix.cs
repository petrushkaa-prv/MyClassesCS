using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Arrays
{
    public class ElementViseMatrix<T> : MatrixBase<T>
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

        public static ElementViseMatrix<T> operator *(ElementViseMatrix<T> lhs, ElementViseMatrix<T> rhs)
        {
            if (rhs._matrixColumns != lhs._matrixColumns || rhs._matrixRows != lhs._matrixRows)
                throw new Exception("Matrices shapes mismatch");

            var res = new ElementViseMatrix<T>(lhs);

            for (int i = 0; i < res._matrixSize; i++)
                res[i] *= (dynamic)rhs[i];

            return res;
        }


        public static implicit operator Matrix<T>(ElementViseMatrix<T> matrix)
        {
            return new Matrix<T>(matrix);
        }


        public override MatrixBase<T> ToPower(int power)
        {
            var res = new ElementViseMatrix<T>(this);

            for (int i = 0; i < power - 1; i++)
                res *= this;

            return res;
        }
    }
}

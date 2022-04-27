using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    internal class MatrixAdjacencyStruct<T> : IAdjacencyStructure<T>
        where T : new()
    {
        private (bool present, T val)[,] matrix;
        protected int[] aliveIterators;

        public MatrixAdjacencyStruct(int size)
        {
            this.matrix = new (bool, T)[size, size];
            this.aliveIterators = new int[size];
        }

        public IEnumerable<(int, T)> OutEdges(int v)
        {
            try
            {
                ++this.aliveIterators[v];
                for (int i = 0; i < this.matrix.GetLength(0); ++i)
                {
                    if (this.matrix[v, i].present)
                        yield return (i, this.matrix[v, i].val);
                }
            }
            finally
            {
                --this.aliveIterators[v];
            }
        }

        public T GetValue(int u, int v) => this.matrix[u, v].val;

        public bool HasEdge(int u, int v) => this.matrix[u, v].present;

        public void SetValue(int u, int v, T val)
        {
            if (this.aliveIterators[u] > 0)
                throw new InvalidOperationException("Can't modify collection while calculating");
            this.matrix[u, v] = (true, val);
        }

        public void RemoveEdge(int u, int v)
        {
            if (this.aliveIterators[u] > 0)
                throw new InvalidOperationException("Can't modify collection while calculating");
            this.matrix[u, v].present = false;
        }

        public void AddEdge(int u, int v)
        {
            if (this.aliveIterators[u] > 0)
                throw new InvalidOperationException("Can't modify collection while calculating");
            this.SetValue(u, v, new T());
        }

        public IEnumerable<int> OutNeighbors(int v)
        {
            try
            {
                ++this.aliveIterators[v];
                foreach ((int, T) outEdge in this.OutEdges(v))
                    yield return outEdge.Item1;
            }
            finally
            {
                --this.aliveIterators[v];
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    internal class MatrixAdjacencyStructure<T> : IAdjacencyStructure<T>
        where T : new()
    {
        private readonly (bool present, T val)[,] _matrix;
        protected int[] _aliveIterators;

        public MatrixAdjacencyStructure(int size)
        {
            _matrix = new (bool, T)[size, size];
            _aliveIterators = new int[size];
        }

        public IEnumerable<(int, T)> OutEdges(int v)
        {
            try
            {
                _aliveIterators[v]++;
                for (int i = 0; i < _matrix.GetLength(0); i++)
                    if (_matrix[v, i].present) 
                        yield return (i, _matrix[v, i].val);
            }
            finally
            {
                _aliveIterators[v]--;
            }
        }

        public T GetValue(int u, int v) => _matrix[u, v].val;

        public bool HasEdge(int u, int v) => _matrix[u, v].present;

        public void SetValue(int u, int v, T val)
        {
            if (_aliveIterators[u] > 0)
                throw new InvalidOperationException("Can't modify collection while calculating");

            _matrix[u, v] = (true, val);
        }

        public void RemoveEdge(int u, int v)
        {
            if (_aliveIterators[u] > 0)
                throw new InvalidOperationException("Can't modify collection while calculating");

            _matrix[u, v].present = false;
        }

        public void AddEdge(int u, int v)
        {
            if (_aliveIterators[u] > 0)
                throw new InvalidOperationException("Can't modify collection while calculating");

            SetValue(u, v, new T());
        }

        public IEnumerable<int> OutNeighbors(int v)
        {
            try
            {
                _aliveIterators[v]++;

                foreach (var outEdge in OutEdges(v))
                    yield return outEdge.Item1;
            }
            finally
            {
                _aliveIterators[v]++;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    public class DictionaryAdjacencyStructure<T> : IAdjacencyStructure<T>
        where T : new()
    {
        private readonly Dictionary<int, T>[] _list;

        public DictionaryAdjacencyStructure(int size)
        {
            _list = new Dictionary<int, T>[size];

            for (int index = 0; index < size; index++)
                _list[index] = new Dictionary<int, T>();
        }

        public IEnumerable<(int, T)> OutEdges(int v)
        {
            foreach (var keyValuePair in _list[v])
                yield return (keyValuePair.Key, keyValuePair.Value);
        }

        public T GetValue(int u, int v) => _list[u][v];

        public bool HasEdge(int u, int v) => _list[u].ContainsKey(v);

        public void SetValue(int u, int v, T val) => _list[u][v] = val;

        public void RemoveEdge(int u, int v) => _list[u].Remove(v);

        public void AddEdge(int u, int v) => SetValue(u, v, new T());

        public IEnumerable<int> OutNeighbors(int v)
        {
            foreach (var outEdge in OutEdges(v))
                yield return outEdge.Item1;
        }
    }
}

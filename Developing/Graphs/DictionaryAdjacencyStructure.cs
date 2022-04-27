using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    internal class DictionaryAdjacencyStructure<T> : IAdjacencyStructure<T>
        where T : new()
    {
        private Dictionary<int, T>[] list;

        public DictionaryAdjacencyStructure(int size)
        {
            this.list = new Dictionary<int, T>[size];
            for (int index = 0; index < size; ++index)
                this.list[index] = new Dictionary<int, T>();
        }

        public IEnumerable<(int, T)> OutEdges(int v)
        {
            foreach (KeyValuePair<int, T> keyValuePair in this.list[v])
                yield return (keyValuePair.Key, keyValuePair.Value);
        }

        public T GetValue(int u, int v) => this.list[u][v];

        public bool HasEdge(int u, int v) => this.list[u].ContainsKey(v);

        public void SetValue(int u, int v, T val) => this.list[u][v] = val;

        public void RemoveEdge(int u, int v) => this.list[u].Remove(v);

        public void AddEdge(int u, int v) => this.SetValue(u, v, new T());

        public IEnumerable<int> OutNeighbors(int v)
        {
            foreach ((int, T) outEdge in this.OutEdges(v))
                yield return outEdge.Item1;
        }
    }
}

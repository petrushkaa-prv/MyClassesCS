using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    public interface IAdjacencyStructure
    {
        bool HasEdge(int u, int v);

        void AddEdge(int u, int v);

        void RemoveEdge(int u, int v);

        IEnumerable<int> OutNeighbors(int v);
    }

    public interface IAdjacencyStructure<T> : IAdjacencyStructure
    {
        T GetValue(int u, int v);

        void SetValue(int u, int v, T val);

        IEnumerable<(int, T)> OutEdges(int v);
    }
}

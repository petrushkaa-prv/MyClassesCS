using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    internal interface IGraph
    {
        bool Directed { get; }

        int VertexCount { get; }

        IEnumerable<int> OutNeighbors(int v);

        bool HasEdge(int from, int to);

        IGraphRepresentation Representation { get; }
    }

    internal interface IGraph<T>
    {
        bool Directed { get; }

        int VertexCount { get; }

        IEnumerable<Edge<T>> OutEdges(int v);

        IEnumerable<int> OutNeighbors(int v);

        IGraphRepresentation Representation { get; }

        bool HasEdge(int from, int to);

        void SetEdgeWeight(int from, int to, T weight);

        T GetEdgeWeight(int from, int to);
    }
}

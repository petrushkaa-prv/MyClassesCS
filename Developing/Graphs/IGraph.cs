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
}

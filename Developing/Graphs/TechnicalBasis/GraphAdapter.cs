using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    internal class GraphAdapter : IGraph<bool>
    {
        private IGraph _graph;

        public GraphAdapter(IGraph graph) => _graph = graph;

        public bool Directed => _graph.Directed;

        public int VertexCount => _graph.VertexCount;

        public IGraphRepresentation Representation => _graph.Representation;

        public IEnumerable<Edge<bool>> OutEdges(int v)
        {
            foreach (var outNeighbor in _graph.OutNeighbors(v))
                yield return new Edge<bool>(v, outNeighbor, true);
        }

        public IEnumerable<int> OutNeighbors(int v)
        {
            foreach (var outNeighbor in _graph.OutNeighbors(v))
                yield return outNeighbor;
        }

        public bool HasEdge(int from, int to) => _graph.HasEdge(from, to);

        public void SetEdgeWeight(int from, int to, bool weight)
        {
            throw new InvalidOperationException("Can't change the weight of an edge using the adapter");
        }

        public bool GetEdgeWeight(int from, int to)
        {
            if(!_graph.HasEdge(from, to))
                throw new ArgumentException($"Absent edge {new Edge(from, to)}");

            return true;
        }
    }
}

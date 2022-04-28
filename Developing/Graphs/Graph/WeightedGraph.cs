using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    internal class Graph<T> : Graph, IGraph<T>
        where T : new()
    {
        public Graph(int vertices, IGraphRepresentation representation) : base(new DirectedGraph<T>(vertices, representation))
        {
        }

        public Graph(int vertices) : base(new DirectedGraph<T>(vertices))
        {
        }

        public Graph(Graph<T> graph) : this(graph.VertexCount, graph.Representation)
        {
            for (int i = 0; i < graph.VertexCount; i++)
                foreach (var edge in graph.OutEdges(i))
                    AddEdge(edge.From, edge.To, edge.Weight);
        }

        /// <inheritdoc/>
        public IEnumerable<Edge<T>> OutEdges(int v) => (_diGraph as DirectedGraph<T>)!.OutEdges(v);
        
        public bool AddEdge(int u, int v, T weight)
            => (_diGraph as DirectedGraph<T>)!.AddEdge(u, v, weight) && 
               (_diGraph as DirectedGraph<T>)!.AddEdge(v, u, weight);

        /// <inheritdoc/>
        public void SetEdgeWeight(int from, int to, T weight)
        {
            (_diGraph as DirectedGraph<T>)!.SetEdgeWeight(from, to, weight);
            (_diGraph as DirectedGraph<T>)!.SetEdgeWeight(to, from, weight);
        }

        /// <inheritdoc/>
        public T GetEdgeWeight(int from, int to)
        {
            if (!_diGraph.HasEdge(from, to))
                throw new ArgumentException($"Absent edge {new Edge(from, to)}");

            return (_diGraph as DirectedGraph<T>)!.GetEdgeWeight(from, to);
        }

        /// <inheritdoc/>
        public override string ToString() => _diGraph.ToString();
    }
}

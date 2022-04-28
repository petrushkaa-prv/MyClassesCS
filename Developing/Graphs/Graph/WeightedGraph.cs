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
        public Graph(int vertices, IGraphRepresentation representation) : base(vertices, representation)
        {
        }

        public Graph(int vertices) : base(vertices)
        {
        }

        public IEnumerable<Edge<T>> OutEdges(int v) => (_diGraph as DiGraph<T>)!.OutEdges(v);
        public bool AddEdge(int u, int v, T weight)
            => (_diGraph as DiGraph<T>)!.AddEdge(u, v, weight) && (_diGraph as DiGraph<T>)!.AddEdge(v, u, weight);

        public void SetEdgeWeight(int from, int to, T weight)
        {
            (_diGraph as DiGraph<T>)!.SetEdgeWeight(from, to, weight);
            (_diGraph as DiGraph<T>)!.SetEdgeWeight(to, from, weight);
        }

        public T GetEdgeWeight(int from, int to)
        {
            if (!_diGraph.HasEdge(from, to))
                throw new ArgumentException($"Absent edge {new Edge(from, to)}");

            return (_diGraph as DiGraph<T>)!.GetEdgeWeight(from, to);
        }

        public override string ToString() => _diGraph.ToString();
    }
}

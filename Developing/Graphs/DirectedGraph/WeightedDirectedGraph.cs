using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    internal class DirectedGraph<T> : DirectedGraph, IGraph<T>
        where T : new()
    {
        protected IAdjacencyStructure<T> MyAdjacencyStructure => (IAdjacencyStructure<T>)base.AdjacencyStructure;

        public DirectedGraph(int vertices, IGraphRepresentation representation) : base(vertices, representation, representation.GetAdjacencyStructure<T>(vertices))
        {
        }

        public DirectedGraph(int vertices) : this(vertices, new DictionaryGraphRepresentation())
        {
        }

        public DirectedGraph(IGraph<T> graph) : this(graph.VertexCount, graph.Representation)
        {
            for (int i = 0; i < graph.VertexCount; i++)
            {
                foreach (var edge in graph.OutEdges(i))
                {
                    AddEdge(edge.From, edge.To, edge.Weight);
                }
            }
        }

        public virtual bool AddEdge(int u, int v, T weight)
        {
            if (u < 0 || v < 0 || u >= VertexCount || v >= VertexCount)
                throw new IndexOutOfRangeException();
            if (u == v)
                throw new ArgumentException("Can't add an edge connecting the same vertex");

            if (AdjacencyStructure.HasEdge(u, v))
                return false;

            MyAdjacencyStructure.SetValue(u, v, weight);

            return true;
        }


        /// <inheritdoc/>
        public IEnumerable<Edge<T>> OutEdges(int v)
        {
            var g = this;

            if (v < 0 || v >= g.VertexCount)
                throw new IndexOutOfRangeException();

            foreach (var outEdge in g.MyAdjacencyStructure.OutEdges(v))
                yield return new Edge<T>(v, outEdge.Item1, outEdge.Item2);
        }

        /// <inheritdoc/>
        public void SetEdgeWeight(int from, int to, T weight)
        {
            if (MyAdjacencyStructure.HasEdge(from, to))
                throw new ArgumentException($"Absent edge {new Edge(from, to)}");

            MyAdjacencyStructure.SetValue(from, to, weight);
        }

        /// <inheritdoc/>
        public T GetEdgeWeight(int from, int to)
        {
            if (MyAdjacencyStructure.HasEdge(from, to))
                throw new ArgumentException($"Absent edge {new Edge(from, to)}");

            return MyAdjacencyStructure.GetValue(from, to);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int u = 0; u < VertexCount; u++)
            {
                sb.Append(u);
                sb.Append(':');
                bool flag = true;

                foreach (var outEdge in OutEdges(u))
                {
                    if (!flag) sb.Append(' ');

                    flag = false;
                    sb.Append(outEdge.To);
                    sb.Append($"({outEdge.Weight})");
                }

                if (u < VertexCount - 1)
                    sb.Append('\n');

            }

            return sb.ToString();
        }
    }
}

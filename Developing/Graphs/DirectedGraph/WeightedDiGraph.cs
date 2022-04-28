using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    internal class DiGraph<T> : DiGraph, IGraph<T>
    where T : new()
    {
        protected IAdjacencyStructure<T> MyAdjacencyStructure => (IAdjacencyStructure<T>)AdjacencyStructure;

        public DiGraph(int vertices, IGraphRepresentation representation) : base(vertices, representation, representation.GetAdjacencyStructure<T>(vertices))
        {
        }

        public DiGraph(int vertices) : this(vertices, new DictionaryGraphRepresentation())
        {
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



        public IEnumerable<Edge<T>> OutEdges(int v)
        {
            var g = this;

            if (v < 0 || v >= g.VertexCount)
                throw new IndexOutOfRangeException();

            foreach (var outEdge in g.MyAdjacencyStructure.OutEdges(v))
                yield return new Edge<T>(v, outEdge.Item1, outEdge.Item2);
        }

        public void SetEdgeWeight(int from, int to, T weight)
        {
            if (MyAdjacencyStructure.HasEdge(from, to))
                throw new ArgumentException($"Absent edge {new Edge(from, to)}");

            MyAdjacencyStructure.SetValue(from, to, weight);
        }

        public T GetEdgeWeight(int from, int to)
        {
            if (MyAdjacencyStructure.HasEdge(from, to))
                throw new ArgumentException($"Absent edge {new Edge(from, to)}");

            return MyAdjacencyStructure.GetValue(from, to);
        }

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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Developing.Graphs
{
    internal class DiGraph : IGraph
    {
        public int VertexCount { get; private set; }
        public IGraphRepresentation Representation { get; protected set; }
        protected internal readonly IAdjacencyStructure AdjacencyStructure;

        public virtual bool Directed => true;

        public DiGraph(int vertices, IGraphRepresentation representation)
        {
            Representation = representation;
            VertexCount = vertices;
            AdjacencyStructure = representation.GetAdjacencyStructure<bool>(vertices);
        }

        public DiGraph(int vertices) : this(vertices, new DictionaryGraphRepresentation())
        {
        }

        internal DiGraph(int vertices, IGraphRepresentation graph, IAdjacencyStructure adjacency)
        {
            VertexCount = vertices;
            Representation = graph;
            AdjacencyStructure = adjacency;
        }

        // remove virtual if not using inheritance in Graph
        public virtual bool AddEdge(int u, int v)
        {
            if (u < 0 || v < 0 || u >= VertexCount || v >= VertexCount)
                throw new IndexOutOfRangeException();

            if (u == v)
                throw new ArgumentException("Can't add an edge connecting the same vertex");

            if (HasEdge(u, v))
                return false;

            AdjacencyStructure.AddEdge(u, v);

            return true;
        }

        // remove virtual if not using inheritance in Graph
        public virtual bool HasEdge(int u, int v)
        {
            if (u < 0 || v < 0 || u >= VertexCount || v >= VertexCount)
                throw new IndexOutOfRangeException();

            return u != v && AdjacencyStructure.HasEdge(u, v);
        }


        public IEnumerable<int> OutNeighbors(int v)
        {
            if (v < 0 || v >= VertexCount)
                throw new IndexOutOfRangeException();

            foreach (var neighbor in AdjacencyStructure.OutNeighbors(v))
                yield return neighbor;
        }

        public virtual bool RemoveEdge(int u, int v)
        {
            if (u < 0 || u >= VertexCount)
                throw new IndexOutOfRangeException();

            if (AdjacencyStructure.HasEdge(u, v))
                return false;

            AdjacencyStructure.RemoveEdge(u, v);

            return true;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            for (int u = 0; u < this.VertexCount; u++)
            {
                stringBuilder.Append(u);
                stringBuilder.Append(':');

                bool flag = true;
                foreach (var outNeighbor in this.OutNeighbors(u))
                {
                    if (!flag) stringBuilder.Append(' ');
                        
                    flag = false;
                    stringBuilder.Append(outNeighbor);
                }

                if (u < this.VertexCount - 1)
                    stringBuilder.Append('\n');
            }

            return stringBuilder.ToString();
        }
    }
}

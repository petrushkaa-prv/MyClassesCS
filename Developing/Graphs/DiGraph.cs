using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    internal class DiGraph : IGraph
    {
        private IGraphRepresentation _representation;
        private int _vertices;
        protected internal IAdjacencyStructure _adjacencyStructure;

        public IGraphRepresentation Representation
        {
            get => _representation;
            protected set => _representation = value;
        }

        public virtual bool Directed => true;

        public int VertexCount => _vertices;

        public DiGraph(int vertices, IGraphRepresentation representation)
        {
            _representation = representation;
            _vertices = vertices;
            _adjacencyStructure = representation.GetAdjacencyStructure<bool>(vertices);
        }

        public DiGraph(int vertices) : this(vertices, new DictionaryGraphRepresentation())
        {
        }

        // remove virtual if not using inheritance in Graph
        public virtual bool AddEdge(int u, int v)
        {
            if (u < 0 || v < 0 || u >= _vertices || v >= _vertices)
            {
                throw new IndexOutOfRangeException();
            }

            if (u == v)
            {
                throw new ArgumentException("Can't add an edge connecting the same vertex");
            }

            if (HasEdge(u, v))
            {
                return false;
            }

            _adjacencyStructure.AddEdge(u, v);

            return true;
        }

        // remove virtual if not using inheritance in Graph
        public virtual bool HasEdge(int u, int v)
        {
            if (u < 0 || v < 0 || u >= _vertices || v >= _vertices)
            {
                throw new IndexOutOfRangeException();
            }

            return u != v && _adjacencyStructure.HasEdge(u, v);
        }


        public IEnumerable<int> OutNeighbors(int v)
        {
            if (v < 0 || v >= _vertices)
            {
                throw new IndexOutOfRangeException();
            }

            foreach (var neighbor in _adjacencyStructure.OutNeighbors(v))
            {
                yield return neighbor;
            }
        }

        public virtual bool RemoveEdge(int u, int v)
        {
            if (u < 0 || u >= _vertices)
            {
                throw new IndexOutOfRangeException();
            }

            if (_adjacencyStructure.HasEdge(u, v))
            {
                return false;
            }

            _adjacencyStructure.RemoveEdge(u, v);

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
                    if (!flag)
                    {
                        stringBuilder.Append(' ');
                    }
                        
                    flag = false;
                    stringBuilder.Append(outNeighbor);
                }
                if (u < this.VertexCount - 1)
                {
                    stringBuilder.Append('\n');
                }
                    
            }

            return stringBuilder.ToString();
        }
    }
}

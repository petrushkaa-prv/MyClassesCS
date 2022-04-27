using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    internal class Graph : DiGraph /*IGraph*/
    {
        // code below represents the Graph implemented without the DiGraph inheritance

        //protected DiGraph _diGraph;

        //public bool Directed => false;

        //public int VertexCount => _diGraph.VertexCount;

        //public IGraphRepresentation Representation => _diGraph.Representation;

        //public Graph(int vertices, IGraphRepresentation representation) =>
        //    _diGraph = new DiGraph(vertices, representation);

        //public Graph(int vertices) => _diGraph = new DiGraph(vertices);

        //public bool AddEdge(int u, int v) => _diGraph.AddEdge(u, v) && _diGraph.AddEdge(v, u);
        //public bool RemoveEdge(int u, int v) => _diGraph.RemoveEdge(u, v) && _diGraph.RemoveEdge(v, u);


        //public bool HasEdge(int from, int to) => _diGraph.HasEdge(from, to);

        //public IEnumerable<int> OutNeighbors(int v) => _diGraph.OutNeighbors(v);

        ///// <inheritdoc />
        //public override string ToString() => _diGraph.ToString();

        // ----------------------------------------------------------------------------------------
        // version with inheritance used

        /// <inheritdoc />
        public Graph(int vertices, IGraphRepresentation representation) : base(vertices, representation)
        {
        }

        /// <inheritdoc />
        public Graph(int vertices) : base(vertices)
        {
        }

        /// <inheritdoc />
        public override bool AddEdge(int u, int v) => base.AddEdge(u, v) && base.AddEdge(v, u);
        public override bool RemoveEdge(int u, int v) => base.RemoveEdge(u, v) && base.RemoveEdge(v, u);
    }
}

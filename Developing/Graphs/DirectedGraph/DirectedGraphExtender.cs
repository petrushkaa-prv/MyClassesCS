using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    internal static class DirectedGraphExtender
    {
        public static DirectedGraph Reverse(this DirectedGraph graph)
        {
            var res = new DirectedGraph(graph.VertexCount, graph.Representation);

            foreach (var edge in graph.DFS().SearchAll())
                res.AddEdge(edge.To, edge.From);

            return res;
        }
    }
}

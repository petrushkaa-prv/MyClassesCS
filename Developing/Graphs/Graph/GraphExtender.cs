using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    internal static class GraphExtender
    {
        public static bool IsBipartite(this Graph graph, out int[] vert)
        {
            var isBipartite = true;

            var colors = new int[graph.VertexCount];

            foreach (var edge in graph.DFS().SearchAll())
                switch (colors[edge.From])
                {
                    case 1:
                        colors[edge.To] = 2;
                        break;
                    case 2:
                        colors[edge.To] = 1;
                        break;
                    case 0 when colors[edge.To] == 0:
                        colors[edge.From] = 1;
                        colors[edge.To] = 2;
                        break;
                }

            for (int v = 0; v < graph.VertexCount; v++)
                if (colors[v] != 1 && colors[v] != 2)
                    colors[v] = 1;

            foreach (var edge in graph.DFS().SearchAll())
                if (colors[edge.From] == colors[edge.To])
                    isBipartite = false;

            vert = isBipartite ? colors : null;
            return isBipartite;
        }

        public static Graph Square(this Graph graph)
        {
            Graph res = new Graph(graph.VertexCount, graph.Representation);
            for (int u = 0; u < res.VertexCount; u++)
            {
                foreach (var outNeighbor in res.OutNeighbors(u))
                {
                    res.AddEdge(u, outNeighbor);
                }
            }

            for (int i = 0; i < graph.VertexCount; i++)
            {
                foreach (int neighbor in graph.OutNeighbors(i))
                {
                    foreach (int nn in graph.OutNeighbors(neighbor))
                    {
                        if (i != nn)
                        {
                            res.AddEdge(i, nn);
                        }
                    }
                }
            }

            return res;
        }
    }
}

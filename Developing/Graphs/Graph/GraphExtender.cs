using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developing.Lists;

namespace Developing.Graphs
{
    internal static class GraphExtender
    {
        public static bool IsBipartite(this Graph graph, out IEnumerable<int> firstSubSet, out IEnumerable<int> secondSubSet)
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
            
            if (isBipartite)
            {
                var s1 = new SlStack<int>();
                var s2 = new SlStack<int>();

                for (int i = 0; i < colors.Length; i++)
                    switch (colors[i])
                    {
                        case 1:
                            s1.Push(i);
                            break;
                        case 2:
                            s2.Push(i);
                            break;
                    }

                firstSubSet = s1;
                secondSubSet = s2;
            }
            else
                firstSubSet = secondSubSet = null;

            return isBipartite;
        }

        public static bool IsAcyclic(this Graph graph)
        {
            var visited = new bool[graph.VertexCount];

            var visitedEdges = new SlList<Edge>();

            for (int i = 0; i < graph.VertexCount; i++)
            {
                if (visited[i]) continue;

                visited[i] = true;

                // TODO: REPLACE WITH BFS WHEN IMPLEMENTED
                foreach (var edge in graph.DFS().SearchFrom(i))
                {
                    if (visitedEdges.Contains(new Edge(edge.To, edge.From))) continue;

                    if (visited[edge.To])
                    {
                        return false;
                    }
                    else
                    {
                        visited[edge.To] = true;
                        visitedEdges.AddFront(edge);
                    }
                }
            }

            return true;
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

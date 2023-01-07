using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developing.Lists;

namespace Developing.Graphs
{
    public static class GraphExtender
    {
        public static bool IsBipartite(this Graph graph)
        {
            return IsBipartite(graph, out _, out _);
        }
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
                
                foreach (var edge in graph.BFS().SearchFrom(i))
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


    }
}

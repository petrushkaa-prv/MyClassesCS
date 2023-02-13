using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developing.Arrays;
using Developing.Lists;

namespace Developing.Graphs
{
    public static class GraphAlgorithms
    {
        public static Graph Square(Graph graph)
        {
            Graph res = new Graph(graph.VertexCount, graph.Representation);

            for (int u = 0; u < res.VertexCount; u++)
                foreach (var outNeighbor in res.OutNeighbors(u))
                    res.AddEdge(u, outNeighbor);

            for (int i = 0; i < graph.VertexCount; i++)
                foreach (int neighbor in graph.OutNeighbors(i))
                    foreach (int nn in graph.OutNeighbors(neighbor))
                        if (i != nn) res.AddEdge(i, nn);

            return res;
        }

        public static Graph LineGraph(Graph graph)
        {
            int edc = graph.BFS().SearchAll().Count();

            edc /= 2;

            Graph res = new Graph(edc, new MatrixGraphRepresentation());

            int idx = 0;
            var names = new (int x, int y)[edc];
            for (int i = 0; i < graph.VertexCount; i++)
                foreach (int outNeighbor in graph.OutNeighbors(i))
                {
                    if (names.Contains((i, outNeighbor)) || names.Contains((outNeighbor, i))) continue;

                    names[idx] = (i, outNeighbor);
                    idx++;
                }

            var ls = new HashSet<(int, int)>();

            for (int i = 0; i < edc; i++, ls.Clear())
            {
                foreach (var outNeighbor in graph.OutNeighbors(names[i].x))
                    if (outNeighbor != names[i].x)
                        ls.Add((names[i].x, outNeighbor));

                foreach (var outNeighbor in graph.OutNeighbors(names[i].y))
                    if (outNeighbor != names[i].y)
                        ls.Add((names[i].y, outNeighbor));

                foreach (var valueTuple in ls)
                    for (int j = 0; j < edc; j++)
                        if ((names[j] == valueTuple || names[j] == (valueTuple.Item2, valueTuple.Item1)) && i != j)
                            res.AddEdge(i, j);
            }

            return res;
        }

        public static DlList<(int vertex, int cost)> Dijkstra<T>(this Graph<int> graph, int start, int end) 
            where T : new()
        {
            var dist = new int[graph.VertexCount];
            var prev = new int[graph.VertexCount];
            var explored = new bool[graph.VertexCount];

            for (int i = 0; i < graph.VertexCount; i++)
            {
                dist[i] = int.MaxValue;
            }

            dist[start] = 0;
            prev[start] = -1;

            var stack = new SlStack<int>();
            while (explored[end] != true)
            {
                int v = -1;
                
                for (int i = 0; i < explored.Length; i++)
                {
                    if (explored[i] == false)
                        stack.Push(i);
                }

                v = stack.Pop();
                foreach (var i in stack)    
                {
                    if (dist[i] < dist[v])
                        v = i;
                }
                stack.Clear();

                explored[v] = true;

                foreach (var edge in graph.OutEdges(v))
                {
                    if (dist[v] + (dynamic)edge.Weight >= dist[edge.To]) continue;

                    dist[edge.To] = dist[v] + (dynamic)edge.Weight;
                    prev[edge.To] = v;
                }
            }

            var path = new DlList<(int vertex, int cost)>();
            for (int i = end; i != start; i = prev[i])
            {
                path.AddFront((i, dist[i]));
            }
            path.AddFront((start, dist[start]));
            return path;
        }
    }
}

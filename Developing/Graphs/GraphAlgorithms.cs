using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developing.Arrays;

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

        //public static Graph<int> MinimumSpanningTree(Graph<int> graph)
        //{
        //    var unionFind = new UnionFind(graph.VertexCount);


        //}
    }
}

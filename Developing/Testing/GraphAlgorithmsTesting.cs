using Developing.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Testing;

internal static class GraphAlgorithmsTesting
{
    private static Tester _tester = new Tester();

    public static void PrepareTests()
    {
        _tester.AddTestCase(name: "Dijkstra Simple", expectedResult: 6, test: () =>
        {
            var g = new Graph<int>(7);
            g.AddEdge(0, 1, 3);
            g.AddEdge(0, 2, 2);
            g.AddEdge(1, 2, 2);
            g.AddEdge(1, 3, 1);
            g.AddEdge(2, 3, 3);
            g.AddEdge(3, 6, 2);
            g.AddEdge(2, 6, 6);
            g.AddEdge(1, 4, 4);
            g.AddEdge(4, 6, 1);
            g.AddEdge(2, 5, 5);
            g.AddEdge(5, 6, 2);

            var shortestPath = g.Dijkstra<int>(0, 6);
            int value;

            return shortestPath.Select(el => el.cost).Max();
        });

        _tester.PrepareTests();
    }

    public static void Run()
    {
        _tester.PerformTests();
    }
}
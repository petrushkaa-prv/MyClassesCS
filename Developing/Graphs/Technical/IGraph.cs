using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    /// <summary>
    /// The base interface representing a non-weighted graph 
    /// </summary>
    internal interface IGraph
    {
        /// <summary>
        /// Indicates if the edges in the graph can connect the same two vertices
        /// in corresponding directions
        /// </summary>
        bool Directed { get; }

        /// <summary>
        /// The vertices in the graph
        /// </summary>
        /// <remarks>
        /// The index of a vertex starts from 0
        /// </remarks>
        int VertexCount { get; }

        /// <summary>
        /// Stores the corresponding graph representation
        /// </summary>
        IGraphRepresentation Representation { get; }

        /// <summary>
        /// Method generating a enumerable collection of vertices directly connected to the input vertex
        /// </summary>
        /// <param name="v">The vertex from which the lookup will be done</param>
        /// <returns>Collection of the neighbor vertices of the input vertex</returns>
        IEnumerable<int> OutNeighbors(int v);

        /// <summary>
        /// Indicates if two vertices are connected
        /// </summary>
        /// <param name="from">Edge starting in</param>
        /// <param name="to">Edge ending in</param>
        /// <returns></returns>
        bool HasEdge(int from, int to);
    }

    /// <summary>
    /// Represents a weighted graph 
    /// </summary>
    /// <typeparam name="T">The type corresponding to represent the weights in the graph</typeparam>
    internal interface IGraph<T> : IGraph
    {
        //bool Directed { get; }
        //int VertexCount { get; }
        //IEnumerable<int> OutNeighbors(int v);
        //IGraphRepresentation Representation { get; }
        //bool HasEdge(int from, int to);

        /// <summary>
        /// Method generating a enumerable collection edges consisting of
        /// directly connected vertices to the input vertex
        /// </summary>
        /// <param name="v">The vertex from which the lookup will be done</param>
        /// <returns>Collection of the neighbor vertices of the input vertex</returns>
        IEnumerable<Edge<T>> OutEdges(int v);

        /// <summary>
        /// If the entered edge exists in the graph, changes it's weight
        /// </summary>
        /// <param name="from">Edge starting in</param>
        /// <param name="to">Edge ending in</param>
        /// <param name="weight">The new weight of the given edge</param>
        void SetEdgeWeight(int from, int to, T weight);

        /// <summary>
        /// If the entered edge exists in the graph, returns it's weight
        /// </summary>
        /// <param name="from">Edge starting in</param>
        /// <param name="to">Edge ending in</param>
        T GetEdgeWeight(int from, int to);
    }
}

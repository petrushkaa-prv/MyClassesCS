using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    internal struct Edge
    {
        public readonly int From;
        public readonly int To;

        public Edge(int from, int to)
        {
            this.From = from;
            this.To = to;
        }

        /// <inheritdoc />
        public override string ToString() => $"({From}, {To})";
    }

    internal struct Edge<T>
    {
        public int From;
        public int To;
        public T Weight;

        public Edge(int from, int to, T weight)
        {
            this.From = from;
            this.To = to;
            this.Weight = weight;
        }

        public override string ToString() => $"({From}, {To} - [{Weight}])";

        public override int GetHashCode() => this.From.GetHashCode() ^ this.To.GetHashCode() ^ this.Weight.GetHashCode();
    }


}

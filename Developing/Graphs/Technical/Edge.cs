using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    public struct Edge
    {
        public readonly int From;
        public readonly int To;

        public Edge(int from, int to)
        {
            this.From = from;
            this.To = to;
        }

        public static bool operator ==(Edge e1, Edge e2)
        {
            return e1.From == e2.From && e1.To == e2.To;
        }
        public static bool operator !=(Edge e1, Edge e2)
        {
            return !(e1 == e2);
        }

        /// <inheritdoc />
        /// 
        public override string ToString() => $"({From}, {To})";
        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(From, To);
    }

    public struct Edge<T>
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

        public static bool operator ==(Edge<T> e1, Edge<T> e2)
        {
            return (e1.From == e2.From && e1.To == e2.To && e1.Weight == (dynamic)e2.Weight);
        }
        public static bool operator !=(Edge<T> e1, Edge<T> e2)
        {
            return !(e1 == e2);
        }

        public override string ToString() => $"({From}, {To} - [{Weight}])";

        public override int GetHashCode() => this.From.GetHashCode() ^ this.To.GetHashCode() ^ this.Weight.GetHashCode();
    }


}

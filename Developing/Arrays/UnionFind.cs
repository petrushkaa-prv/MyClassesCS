using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developing.Lists;

namespace Developing.Arrays
{
    public class UnionFind
    {
        private readonly int[] _id;

        public UnionFind(int n)
        {
            _id = new int[n];
            for (int index = 0; index < n; ++index)
                _id[index] = index;
        }

        public void Union(int a, int b)
        {
            int num = Find(a);
            _id[Find(b)] = _id[b] = num;
        }

        public int Find(int a)
        {
            var stack = new SlStack<int>();
            int index1;
            for (index1 = a; this._id[index1] != index1; index1 = this._id[index1])
                stack.Push(index1);
            foreach (int index2 in stack)
                _id[index2] = index1;
            return index1;
        }
    }
}

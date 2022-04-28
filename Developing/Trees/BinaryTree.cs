using System;
using System.Collections.Generic;
using System.Text;
using Developing.Interfaces;
using Developing.Nodes;

namespace Developing.Trees
{
    public abstract class BinaryTree<TValue, TNode>
        where TNode : BinaryNode<TValue, TNode>
        where TValue : IComparable<TValue>
    {
        protected internal TNode Root;
        public virtual int Height => CalculateHeight(Root);
        
        public virtual TNode Search(TValue value)
        {
            var ptr = Root;

            while (ptr != null && ptr.Value.CompareTo(value) != 0)
                ptr = value.CompareTo(ptr.Value) > 0 ? ptr.Right : ptr.Left;

            return ptr;
        }
        public abstract void Insert(TValue value);
        public abstract void Delete(TValue value);

        public virtual int CalculateHeight(BinaryNode<TValue, TNode> node)
        {
            if(node == null)
                return 0;

            return Math.Max(CalculateHeight(node.Left) + 1, CalculateHeight(node.Right) + 1);
        }
        public virtual string PrintInOrder(BinaryNode<TValue, TNode> ptr, ref string res)
        {
            if (ptr == null) return string.Empty;

            PrintInOrder(ptr.Left, ref res);

            res += ptr.Value + " ";

            PrintInOrder(ptr.Right, ref res);

            return res;
        }
    }
}

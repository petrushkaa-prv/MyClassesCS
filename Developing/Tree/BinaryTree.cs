using System;
using System.Collections.Generic;
using System.Text;
using Developing.Interfaces;
using Developing.Nodes;

namespace Developing.Tree
{
    public abstract class BinaryTree<TValue, TNode>
        where TNode : BinaryNode<TValue, TNode>
    {
        protected internal TNode Root;
        public virtual int Height => CalculateHeight(Root);

        public abstract void Insert(TValue value);
        public abstract void Delete(TValue value);

        public virtual int CalculateHeight(BinaryNode<TValue, TNode> node)
        {
            if(node == null)
                return 0;

            return Math.Max(CalculateHeight(node.Left) + 1, CalculateHeight(node.Right) + 1);
        }
    }
}

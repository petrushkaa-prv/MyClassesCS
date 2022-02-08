using System;
using System.Collections.Generic;
using System.Text;
using Developing.Interfaces;

namespace Developing.Tree
{
    public abstract class BinaryTreeBase<TValue, TNode>
        where TNode : IBinaryNode<TValue, TNode>
    {
        protected internal TNode Root { get; private protected set; }
    }
}

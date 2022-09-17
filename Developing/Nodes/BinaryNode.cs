using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developing.GeneralInterfaces;
using Developing.Trees;

namespace Developing.Nodes;

/// <summary>
/// Is the base class for all binary nodes in this project
/// the inheritance of it was created to assure future
/// extenstion methods to work universally on all trees
/// using it.
/// </summary>
/// <typeparam name="TValue">The type of the value the node contains</typeparam>
/// <typeparam name="TNode">The type of the child node (usually itself)</typeparam>
/// <remarks>
/// Is an abstract class an can't be created locally.
/// </remarks>
public abstract class BinaryNode<TValue, TNode> : TreeNode<TValue>
    where TNode : TreeNode<TValue>
{
    private TNode _left;
    private TNode _right;

    public BinaryNode()
    {
        base.Children = IndexWrapper.Array2Indexed(new TreeNode<TValue>[2]);
        _left = (TNode)base.Children[0];
        _right = (TNode)base.Children[1];
    }

    public ref TNode Left => ref _left;

    public ref TNode Right => ref _right;
}
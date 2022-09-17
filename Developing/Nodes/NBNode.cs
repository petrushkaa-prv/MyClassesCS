using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developing.Arrays;
using Developing.GeneralInterfaces;

namespace Developing.Nodes;

public class NbNode<T> : TreeNode<T>
{
    public new IIndexed<TreeNode<T>> Children => base.Children;

    public NbNode(IIndexed<TreeNode<T>> arr)
    {
        base.Children = arr;
    }

    public NbNode() : this(new ArrStack<TreeNode<T>>())
    {

    }
}
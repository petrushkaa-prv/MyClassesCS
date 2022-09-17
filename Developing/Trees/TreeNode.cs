using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developing.GeneralInterfaces;

namespace Developing.Nodes;

public abstract class TreeNode<TData>
{
    public TData Value;
    protected IIndexed<TreeNode<TData>> Children;
}
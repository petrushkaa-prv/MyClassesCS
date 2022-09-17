using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developing.Nodes;

namespace Developing.Trees;

public interface ITree<T>
{
    public int Height { get; }

    public TreeNode<T> Root { get; init; }

    public void Insert(T value);
    public void Delete(T value);
}

public interface IBinaryTree<T> : ITree<T>
{
    
}
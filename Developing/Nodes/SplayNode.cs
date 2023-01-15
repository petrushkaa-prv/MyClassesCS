using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Nodes;

public class SplayNode<T> : BinaryNode<T, SplayNode<T>>
    where T : IComparable<T>
{
    private SplayNode<T> _parent;

    public SplayNode(ref SplayNode<T> parent)
    {
        _parent = parent;
    }

    public SplayNode()
    {
        _parent = null;
    }

    public ref SplayNode<T> Parent => ref _parent;
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Nodes;

public class SplayNode<T> : BinaryNode<T, SplayNode<T>>
    where T : IComparable<T>
{
    // can be modified for a parent pointer
    // for iterative splay implementation
}
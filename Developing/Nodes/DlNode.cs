using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Nodes;

/// <summary>
/// Represents a doubly linked node
/// </summary>
/// <typeparam name="T">Generic type</typeparam>
public class DlNode<T>
{
    public T Value { get; set; }
    public DlNode<T> Next { get; set; }
    public DlNode<T> Prev { get; set; }

    public DlNode(T value, DlNode<T> next = null, DlNode<T> prev = null)
    {
        Value = value;
        Next = next;
        Prev = prev;
    }
}
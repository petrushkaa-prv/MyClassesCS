using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Nodes;

/// <summary>
/// Represents a singly linked Node
/// </summary>
/// <typeparam name="T">Generic type</typeparam>
public class SlNode<T>
{
    public T Value { get; set; }
    public SlNode<T> Next { get; set; }

    public SlNode(T value, SlNode<T> next = null)
    {
        Value = value;
        Next = next;
    }
}
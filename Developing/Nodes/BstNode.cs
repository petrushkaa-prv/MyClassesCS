using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Nodes;

/// <summary>
/// Represents a Binary Search Tree Node containing the main value
/// in it and two children.
/// </summary>
/// <typeparam name="T"></typeparam>
public class BstNode<T> : BinaryNode<T, BstNode<T>>
    where T : IComparable<T>
{
}
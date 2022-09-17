using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Nodes;

/// <summary>
/// Represents a AVL (creators: Adelson-Velskij & Landis) Tree Node
/// that contains the main value, and a additional parameter Balance
/// which is represents the difference between the left and right subtree
/// e.g. Balance = Left_Subtree_Height - Right_Subtree_Height.
/// </summary>
/// <typeparam name="T"></typeparam>
public class AvlNode<T> : BinaryNode<T, AvlNode<T>>
    where T : IComparable<T>
{
    public int Balance;
}
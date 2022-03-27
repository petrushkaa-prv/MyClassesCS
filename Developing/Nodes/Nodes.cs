using System;
using System.Collections;
using System.Collections.Generic;
using Developing.Interfaces;


namespace Developing.Nodes
{
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
    public abstract class BinaryNode<TValue, TNode>
    {
        public TValue Value;
        public TNode Left;
        public TNode Right;
    }

    /// <summary>
    /// Represents a singly linked Node that implements automatic iterating.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SlNode<T> : IEnumerable<T> 
            where T : IComparable<T>
    {
        public T Value { get; set; }
        public SlNode<T> Next { get; set; }

        public SlNode(T value, SlNode<T> next = null)
        {
            Value = value;
            Next = next;
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            SlNode<T> ptr = this;

            while (ptr != null)
            {
                yield return ptr.Value;
                ptr = ptr.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// Represents a Binary Search Tree Node containing the main value
    /// in it and two children.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BstNode<T> : BinaryNode<T, BstNode<T>>
        where T : IComparable<T>
    {
    }
    
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

    public class SplayNode<T> : BinaryNode<T, SplayNode<T>>
        where T : IComparable<T>
    {
        // can be modified for a parent pointer
        // for iterative splay implementation
    }
}

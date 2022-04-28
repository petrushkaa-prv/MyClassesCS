using System;
using System.Collections.Generic;
using System.Text;
using Developing.Interfaces;
using Developing.Nodes;

namespace Developing.Trees
{
    internal class BstTree<T> : BinaryTree<T, BstNode<T>>
        where T : IComparable<T>
    {
        //private BstNode<T> _root;

        public T Max => FindMax(Root).Value;
        public T Min => FindMin(Root).Value;
        //public int Height => base.CalculateHeight(Root);
        
        public BstTree(params T[] arr)
        {
            foreach (var item in arr)
                Insert(item);
        }

        
        public new BstNode<T> Search(T value)
        {
            return Search(value, out _);
        }
        public BstNode<T> Search(T value, out BstNode<T> prev)
        {
            BstNode<T> ptr;
            prev = null;

            if (null == (ptr = Root)) return null;

            while (ptr != null && ptr.Value.CompareTo(value) != 0)
            {
                prev = ptr;

                ptr = value.CompareTo(ptr.Value) > 0 ? ptr.Right : ptr.Left;
            }

            return ptr;
        }
        
        public override void Insert(T value)
        {
            var node = new BstNode<T>
            {
                Value = value
            };

            if (Search(node.Value, out var last) != null)
                return;

            if (last == null)
                Root = node;
            else
                if (last.Value.CompareTo(node.Value) > 0)
                    last.Left = node;
                else
                    last.Right = node;
        }
        public override void Delete(T value)
        {
            BstNode<T> ptr = null;
            if (null == (ptr = Search(value, out var last))) return;

            if (ptr.Left != null && ptr.Right == null)
                if (last.Left == ptr)
                    last.Left = ptr.Left;
                else
                    last.Right = ptr.Left;
            else if (ptr.Left == null && ptr.Right != null)
                if (last.Left == ptr)
                    last.Left = ptr.Right;
                else
                    last.Right = ptr.Right;
            else if (ptr.Left != null && ptr.Right != null)
            {
                BstNode<T> pParent = ptr, p = ptr.Left;

                while (p.Right != null)
                {
                    pParent = p;
                    p = p.Right;
                }

                if (pParent.Right == p)
                    pParent.Right = p.Left;
                else
                    pParent.Left = p.Left;

                if (last == null)
                    Root = p;
                else
                    if (last.Left == ptr)
                        last.Left = p;
                    else
                        last.Right = p;

                p.Left = ptr.Left;
                p.Right = ptr.Right;
            }

            ptr = null;
        }
        
        public BstNode<T> FindMax(BstNode<T> node)
        {
            var ptr = node;

            while (ptr.Right != null)
                ptr = ptr.Right;

            return ptr;
        }
        public BstNode<T> FindMin(BstNode<T> node)
        {
            var ptr = node;

            while (ptr.Left != null)
                ptr = ptr.Left;

            return ptr;
        }

        //public int CalculateHeight(BstNode<T> node)
        //{
        //    if (node == null) return 0;

        //    return
        //        Math.Max(CalculateHeight(node.Left) + 1, CalculateHeight(node.Right) + 1);
        //}



        /// <inheritdoc />
        public override string ToString()
        {
            string res = string.Empty;
            return PrintInOrder(Root, ref res);
        }
        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(Root);
    }
}

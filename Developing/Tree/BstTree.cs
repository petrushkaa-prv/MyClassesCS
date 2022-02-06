using System;
using System.Collections.Generic;
using System.Text;
using Developing.Nodes;

namespace Developing.Tree
{
    internal class BstTree<T>
        where T : IComparable<T>
    {
        public T Max => FindMax(_root).Value;
        public T Min => FindMin(_root).Value;

        private BstNode<T> _root = null;

        public BstNode<T> Search(T val, out BstNode<T> prev)
        {
            BstNode<T> ptr = null;
            prev = null;

            if (null == (ptr = _root)) return null;

            while (ptr != null && ptr.Value.CompareTo(val) != 0)
            {
                prev = ptr;
                if (val.CompareTo(ptr.Value) > 0)
                {
                    ptr = ptr.Right;
                }
                else
                {
                    ptr = ptr.Left;
                }
            }

            return ptr;
        }
        
        public void Insert(T value)
        {
            var node = new BstNode<T>
            {
                Value = value
            };

            if (Search(node.Value, out var last) != null)
                return;

            if (last == null)
                _root = node;
            else
            {
                if (last.Value.CompareTo(node.Value) > 0)
                    last.Left = node;
                else
                    last.Right = node;
            }
        }
        public void Delete(T value)
        {
            BstNode<T> ptr = null;
            if (null == (ptr = Search(value, out var last))) return;

            if (ptr.Left != null && ptr.Right == null)
            {
                if (last.Left == ptr)
                    last.Left = ptr.Left;
                else
                    last.Right = ptr.Left;
            }
            else if (ptr.Left == null && ptr.Right != null)
            {
                if (last.Left == ptr)
                    last.Left = ptr.Right;
                else
                    last.Right = ptr.Right;
            }
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
                {
                    _root = p;
                }
                else
                {
                    if (last.Left == ptr)
                        last.Left = p;
                    else
                        last.Right = p;
                }

                p.Left = ptr.Left;
                p.Right = ptr.Right;
            }

            ptr = null;
        }
        
        public BstNode<T> FindMax(BstNode<T> node)
        {
            var ptr = node;

            while (ptr.Right != null)
            {
                ptr = ptr.Right;
            }

            return ptr;
        }
        public BstNode<T> FindMin(BstNode<T> node)
        {
            var ptr = node;

            while (ptr.Left != null)
            {
                ptr = ptr.Left;
            }

            return ptr;
        }
    }
}

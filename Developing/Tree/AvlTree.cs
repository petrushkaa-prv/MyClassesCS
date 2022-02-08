using System;
using System.Collections.Generic;
using System.Text;
using Developing.Interfaces;
using Developing.Nodes;

namespace Developing.Tree
{
    internal class AvlTree<T> : BinaryTreeBase<T, AvlNode<T>>
        where T : IComparable<T>
    {



        public AvlNode<T> Search(T value)
        {
            var ptr = base.Root;

            while (ptr != null && ptr.Value.CompareTo(value) != 0)
            {
                ptr = value.CompareTo(ptr.Value) > 0 ? ptr.Right : ptr.Left;
            }

            return ptr;
        }

        private void RightRotation(ref AvlNode<T> p)
        {
            var x = p.Right;
            p.Right = x.Left;
            x.Left = p;
            p.Balance = x.Balance == -1 ? 0 : -1;
            x.Balance = x.Balance == 0  ? 1 :  0;
            p = x;
        }
        private void LeftRotation(ref AvlNode<T> p)
        {
            var x = p.Left;
            p.Left = x.Right;
            x.Right = p;
            p.Balance = x.Balance == 1 ?  0 : -1;
            x.Balance = x.Balance == 0 ? -1 :  0;
            p = x;
        }
        private void LeftRightRotation(ref AvlNode<T> p)
        {
            var left = p.Left;
            RightRotation(ref left);
            LeftRotation(ref p);
        }
        private void RightLeftRotation(ref AvlNode<T> p)
        {
            var right = p.Right;
            LeftRotation(ref right);
            RightRotation(ref p);
        }

        public void Insert(T value)
        {

        }
        private void Insert(T value, ref AvlNode<T> root, ref bool h)
        {
            AvlNode<T> ptr;

            if (root == null)
            {
                root = new AvlNode<T>
                {
                    Value = value,
                    Left = null,
                    Right = null,
                    Balance = 0
                };

                h = true;
                return;
            }

            if (value.CompareTo(root.Value) < 0) // left subtree
            {
                var left = root.Left;
                Insert(value, ref left, ref h);

                if (h)
                {
                    switch (root.Balance)
                    {
                        //case == 1:
                        //    ptr = root.Left;
                        //    if(ptr.Balance == 1 || ptr.Balance == 0)
                        //        LeftRotation(ref root);
                        //    else
                        //        LeftRightRotation(ref root);
                        //    h = false;
                        //    break;

                    }
                }
            }
        }

    }
}

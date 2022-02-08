using System;
using System.Collections.Generic;
using System.Text;
using Developing.Interfaces;
using Developing.Nodes;

namespace Developing.Tree
{
    internal class AvlTree<T> : BinaryTree<T, AvlNode<T>>
        where T : IComparable<T>
    {
        public T Max => FindMax(Root).Value;
        public T Min => FindMin(Root).Value;

        public AvlTree(params T[] arr)
        {
            foreach (var item in arr)
            {
                Insert(item);
            }
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
        private void DoubleLeftRotation(ref AvlNode<T> p)
        {
            RightRotation(ref p.Left);
            LeftRotation(ref p);

            //    var x = p.Left;
            //    var y = x.Right;
            //    x.Right = y.Left;
            //    y.Left = x;
            //    p.Left = y.Right;
            //    y.Right = p;
            //    p.Balance = y.Balance == 1 ? -1 : 0;
            //    x.Balance = y.Balance == -1 ? 1 : 0;
            //    y.Balance = 0;
            //    p = y;
        }
        private void DoubleRightRotation(ref AvlNode<T> p)
        {
            LeftRotation(ref p.Right);
            RightRotation(ref p);

            //var x = p.Right;
            //var y = x.Left;
            //x.Left = y.Right;
            //y.Right = x;
            //p.Right = y.Left;
            //y.Left = p;
            //p.Balance = y.Balance == -1 ? 1 : 0;
            //x.Balance = y.Balance == 1 ? -1 : 0;
            //y.Balance = 0;
            //p = y;
        }

        public override void Insert(T value)
        {
            var h = false;
            Insert(value, ref Root, ref h);
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

            switch (value.CompareTo(root.Value))
            {
                // left subtree
                case < 0:
                {
                    Insert(value, ref root.Left, ref h);

                    if (h)
                        switch (root.Balance)
                        {
                            case 1:
                                ptr = root.Left;
                                if (ptr.Balance is 1 or 0)
                                    LeftRotation(ref root);
                                else
                                    DoubleLeftRotation(ref root);
                                h = false;
                                break;
                            case 0:
                                root.Balance = 1;
                                break;
                            case -1:
                                root.Balance = 0;
                                h = false;
                                break;
                        }

                    return;
                }
                // right subtree
                case >/*=*/ 0: // uncomment to ensure value repeaters
                {
                    Insert(value, ref root.Right, ref h);

                    if (h)
                        switch (root.Balance)
                        {
                            case 1:
                                root.Balance = 0;
                                h = false;
                                break;
                            case 0:
                                root.Balance = -1;
                                break;
                            case -1:
                                ptr = root.Right;
                                if (ptr.Balance is -1 or 0)
                                    RightRotation(ref root);
                                else
                                    DoubleRightRotation(ref root);
                                h = false;
                                break;
                        }

                    break;
                }
            }
        }
        public override void Delete(T value)
        {
            var h = true;
            Delete(value, ref Root, ref h);
        }
        private void Delete(T value, ref AvlNode<T> root, ref bool h)
        {
            var done = false;

            if (root == null)
            {
                h = false;
                done = true;
            }
            else if (value.CompareTo(root.Value) < 0) // left subtree
            {
                Delete(value, ref root.Left, ref h);

                if (h)
                {
                    if (root.Balance >= 0)
                    {
                        root.Balance--;
                        if (root.Balance == -1)
                            h = false;
                    }
                    else
                    {
                        if(root.Right.Balance == -1)
                            RightRotation(ref root);
                        else
                            DoubleRightRotation(ref root);

                        done = true;
                    }
                }
            }
            else if (value.CompareTo(root.Value) == 0) // root
            {
                if (root.Left == null || root.Right == null)
                {
                    AvlNode<T> x;
                    if (root.Left != null)
                        x = root.Right;
                    else
                        x = root.Left;

                    root = x;
                    h = true;
                    done = true;
                }
                else
                {
                    AvlNode<T> y = root.Right;
                    while (y.Left != null)
                    {
                        y = y.Left;
                    }

                    root.Value = y.Value;
                    value = root.Value;
                }
            }

            if (!done && value.CompareTo(root.Value) >= 0) // right subtree
            {
                Delete(value, ref root.Right, ref h);

                if (h)
                {
                    if (root.Balance <= 0)
                    {
                        root.Balance++;
                        if (root.Balance == 1)
                            h = false;
                    }
                    else
                    {
                        if(root.Left.Balance == 1)
                            LeftRotation(ref root);
                        else 
                            DoubleLeftRotation(ref root);
                    }
                }
            }

        }

        public AvlNode<T> FindMax(AvlNode<T> node)
        {
            var ptr = node;

            while (ptr.Right != null)
            {
                ptr = ptr.Right;
            }

            return ptr;
        }
        public AvlNode<T> FindMin(AvlNode<T> node)
        {
            var ptr = node;

            while (ptr.Left != null)
            {
                ptr = ptr.Left;
            }

            return ptr;
        }



        /// <inheritdoc />
        public override string ToString()
        {
            var res = string.Empty;
            return PrintInOrder(Root, ref res);
        }
        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(Root);
    }
}

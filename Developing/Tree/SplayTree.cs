using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developing.Nodes;

namespace Developing.Tree
{
    internal class SplayTree<T> : BinaryTree<T, SplayNode<T>>
        where T : IComparable<T>
    {
        public SplayTree(params T[] arr)
        {
            foreach (var item in arr)
            {
                Insert(item);
            }
        }


        private void Splay(T value, ref SplayNode<T> root)
        {
            if (value.CompareTo(root.Value) < 0) // left subtree
            {
                if (root.Left != null)
                {
                    if (value.CompareTo(root.Left.Value) > 0)
                    {
                        if (root.Left.Left != null)
                        {
                            Splay(value, ref root.Left.Left);
                            LZigZig(ref root);
                        }
                        else 
                            LZig(ref root);
                    }
                    else if (value.CompareTo(root.Left.Value) > 0)
                    {
                        if (root.Left.Right != null)
                        {
                            Splay(value, ref root.Left.Right);
                            LZigZag(ref root);
                        }
                        else 
                            LZig(ref root);
                    }
                    else 
                        LZig(ref root);
                }
            }
            else if (value.CompareTo(root.Value) > 0)
            {
                if (root.Right != null)
                {
                    if (value.CompareTo(root.Right.Value) > 0)
                    {
                        if (root.Right.Right != null)
                        {
                            Splay(value, ref root.Right.Right);
                            RZigZig(ref root);
                        }
                        else
                            RZig(ref root);
                    }
                    else if (value.CompareTo(root.Right.Value) < 0)
                    {
                        if (root.Right.Left != null)
                        {
                            Splay(value, ref root.Right.Left);
                            RZigZag(ref root);
                        }
                        else 
                            RZig(ref root);
                    }
                    else
                        RZig(ref root);
                }
            }
        }

        private void LZig(ref SplayNode<T> p)
        {
            var x = p.Left;
            p.Left = x.Right;
            x.Right = p;
            p = x;
        }
        private void LZigZig(ref SplayNode<T> p)
        {
            LZig(ref p);
            LZig(ref p);
        }
        private void LZigZag(ref SplayNode<T> p)
        {
            RZig(ref p);
            LZig(ref p);
        }
        private void RZig(ref SplayNode<T> p)
        {
            var x = p.Left;
            p.Left = x.Right;
            x.Right = p;
            p = x;
        }
        private void RZigZig(ref SplayNode<T> p)
        {
            RZig(ref p);
            RZig(ref p);
        }
        private void RZigZag(ref SplayNode<T> p)
        {
            LZig(ref p);
            RZig(ref p);
        }



        public new SplayNode<T> Search(T value)
        {
            if (Root == null) return null;

            Splay(value, ref Root);
            return Root.Value.CompareTo(value) == 0 ? Root : null;
        }

        public override void Insert(T value)
        {
            if (Root == null)
            {
                Root = new SplayNode<T>
                {
                    Value = value,
                    Left = null,
                    Right = null,
                };

                return;
            }

            Splay(value, ref Root);

            if (Root.Value.CompareTo(value) < 0)
            {
                var p = new SplayNode<T>
                {
                    Value = value,
                    Left = Root,
                    Right = Root.Right
                };
                Root.Right = null;
                Root = p;
            }
            else if (Root.Value.CompareTo(value) > 0)
            {
                var p = new SplayNode<T>
                {
                    Value = value,
                    Right = Root,
                    Left = Root.Left
                };
                Root.Left = null;
                Root = p;
            }
        }
        public override void Delete(T value)
        {
            if(Root == null)
                return;

            Splay(value, ref Root);
            if(Root.Value.CompareTo(value) != 0)
                return;

            var p = Root;
            if (Root.Left == null)
            {
                Root = Root.Right;
            }
            else
            {
                Splay(value, ref Root.Left);

                Root.Left.Right = Root.Right;
                Root = Root.Left;
            }

            p = null;
        }
    }
}

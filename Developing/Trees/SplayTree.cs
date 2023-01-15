using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Developing.Nodes;

namespace Developing.Trees
{
    public class SplayTree<T> : BinaryTree<T, SplayNode<T>>
        where T : IComparable<T>
    {
        public SplayTree(params T[] arr)
        {
            foreach (var item in arr)
                Insert(item);
        }

        private void LZig(ref SplayNode<T> x)
        {
            var y = x.Right;
            if (y is not null)
            {
                x.Right = y.Left;
                if (y.Left is not null) y.Left.Parent = x;
                y.Parent = x.Parent;
            }

            if (x.Parent is null) this.Root = y;
            else if (x == x.Parent.Left) x.Parent.Left = y;
            else x.Parent.Right = y;

            if (y is not null) y.Left = x;

            x.Parent = y;
        }

        private void RZig(ref SplayNode<T> x)
        {
            var y = x.Left;
            if (y is not null)
            {
                x.Left = y.Right;
                if(y.Right is not null) y.Right.Parent = x;
                y.Parent = x.Parent;
            }

            if(x.Parent is null) this.Root = y;
            else if (x == x.Parent.Left) x.Parent.Left = y;
            else x.Parent.Right = y;

            if (y is not null) y.Right = x;

            x.Parent = y;
        }

        private void Splay(ref SplayNode<T> x)
        {
            while (x.Parent is not null)
            {
                Console.WriteLine("PERFOMING SPLAY");
                if (x.Parent.Parent is null)
                {
                    if(x.Parent.Left == x) RZig(ref x.Parent);
                    else LZig(ref x.Parent);
                }
                else if (x.Parent.Left == x && x.Parent.Parent.Left == x.Parent)
                {
                    RZig(ref x.Parent.Parent);
                    RZig(ref x.Parent);
                }
                else if (x.Parent.Right == x && x.Parent.Parent.Right == x.Parent)
                {
                    LZig(ref x.Parent.Parent);
                    LZig(ref x.Parent);
                }
                else if (x.Parent.Left == x && x.Parent.Parent.Right == x.Parent)
                {
                    RZig(ref x.Parent);
                    LZig(ref x.Parent);
                }
                else
                {
                    LZig(ref x.Parent);
                    RZig(ref x.Parent);
                }
            }
        }

        private void Replace(ref SplayNode<T> u, ref SplayNode<T> v)
        {
            if (u.Parent is null) this.Root = v;
            else if(u == u.Parent.Left) u.Parent.Left = v;
            else u.Parent.Right = v;

            if(v is not null) v.Parent = u.Parent;
        }

        private SplayNode<T> SubTreeMinimum(SplayNode<T> u)
        {
            var ptr = u;
            while (ptr.Left is not null)
                ptr = ptr.Left;

            return ptr;
        }

        /// <inheritdoc />
        public override void Insert(T value)
        {
            SplayNode<T> z = this.Root, p = null;

            if (z is null)
            {
                Root = new SplayNode<T>()
                {
                    Value = value
                };

                return;
            }

            while (z is not null)
            {
                p = z;
                if (z.Value.CompareTo(value) >= 0) z = z.Right;
                else z = z.Left;
            }

            z = new SplayNode<T>(ref p)
            {
                Value = value
            };

            if(p is null) this.Root = z;
            else if (p.Value.CompareTo(z.Value) >= 0) p.Right = z;
            else p.Left = z;

            Splay(ref z);
        }

        /// <inheritdoc />
        public override void Delete(T value)
        {
            var z = Search(value);
            if (z is null) return;

            Splay(ref z);

            if(z.Left is null) Replace(ref z, ref z.Right);
            else if(z.Right is null) Replace(ref z, ref z.Left);
            else
            {
                var y = SubTreeMinimum(z.Right);
                if (y.Parent != z)
                {
                    Replace(ref y, ref y.Right);
                    y.Right = z.Right;
                    y.Right.Parent = y;
                }

                Replace(ref z , ref y);
                y.Left = z.Left;
                y.Left.Parent = y;
            }
        }

        //private void Splay(T value, ref SplayNode<T> root)
        //{
        //    if (value.CompareTo(root.Value) < 0) // left subtree
        //    {
        //        if (root.Left != null)
        //            if (value.CompareTo(root.Left.Value) > 0)
        //            {
        //                if (root.Left.Left != null)
        //                {
        //                    Splay(value, ref root.Left.Left);
        //                    LZigZig(ref root);
        //                }
        //                else
        //                    LZig(ref root);
        //            }
        //            else if (value.CompareTo(root.Left.Value) > 0)
        //            {
        //                if (root.Left.Right != null)
        //                {
        //                    Splay(value, ref root.Left.Right);
        //                    LZigZag(ref root);
        //                }
        //                else
        //                    LZig(ref root);
        //            }
        //            else
        //                LZig(ref root);
        //    }
        //    else if (value.CompareTo(root.Value) > 0)
        //    {
        //        if (root.Right != null)
        //            if (value.CompareTo(root.Right.Value) > 0)
        //            {
        //                if (root.Right.Right != null)
        //                {
        //                    Splay(value, ref root.Right.Right);
        //                    RZigZig(ref root);
        //                }
        //                else
        //                    RZig(ref root);
        //            }
        //            else if (value.CompareTo(root.Right.Value) < 0)
        //            {
        //                if (root.Right.Left != null)
        //                {
        //                    Splay(value, ref root.Right.Left);
        //                    RZigZag(ref root);
        //                }
        //                else
        //                    RZig(ref root);
        //            }
        //            else
        //                RZig(ref root);
        //    }
        //}

        //private void LZig(ref SplayNode<T> p)
        //{
        //    var x = p.Left;
        //    p.Left = x.Right;
        //    x.Right = p;
        //    p = x;
        //}
        //private void LZigZig(ref SplayNode<T> p)
        //{
        //    LZig(ref p);
        //    LZig(ref p);
        //}
        //private void LZigZag(ref SplayNode<T> p)
        //{
        //    RZig(ref p);
        //    LZig(ref p);
        //}
        //private void RZig(ref SplayNode<T> p)
        //{
        //    var x = p.Left;
        //    p.Left = x.Right;
        //    x.Right = p;
        //    p = x;
        //}
        //private void RZigZig(ref SplayNode<T> p)
        //{
        //    RZig(ref p);
        //    RZig(ref p);
        //}
        //private void RZigZag(ref SplayNode<T> p)
        //{
        //    LZig(ref p);
        //    RZig(ref p);
        //}



        //public new SplayNode<T> Search(T value)
        //{
        //    if (Root == null) return null;

        //    Splay(value, ref Root);
        //    return Root.Value.CompareTo(value) == 0 ? Root : null;
        //}

        //public override void Insert(T value)
        //{
        //    if (Root == null)
        //    {
        //        Root = new SplayNode<T>
        //        {
        //            Value = value,
        //            Left = null,
        //            Right = null,
        //        };

        //        return;
        //    }

        //    Splay(value, ref Root);

        //    if (Root.Value.CompareTo(value) < 0)
        //    {
        //        var p = new SplayNode<T>
        //        {
        //            Value = value,
        //            Left = Root,
        //            Right = Root.Right
        //        };
        //        Root.Right = null;
        //        Root = p;
        //    }
        //    else if (Root.Value.CompareTo(value) > 0)
        //    {
        //        var p = new SplayNode<T>
        //        {
        //            Value = value,
        //            Right = Root,
        //            Left = Root.Left
        //        };
        //        Root.Left = null;
        //        Root = p;
        //    }
        //}
        //public override void Delete(T value)
        //{
        //    if(Root == null)
        //        return;

        //    Splay(value, ref Root);
        //    if(Root.Value.CompareTo(value) != 0)
        //        return;

        //    var p = Root;
        //    if (Root.Left == null)
        //        Root = Root.Right;
        //    else
        //    {
        //        Splay(value, ref Root.Left);

        //        Root.Left.Right = Root.Right;
        //        Root = Root.Left;
        //    }

        //    p = null;
        //}
    }
}

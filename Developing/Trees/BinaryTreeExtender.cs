using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Trees
{
    internal static class BinaryTreeExtender
    {
        public static void Print2D<TValue, TNode>(this BinaryTree<TValue, TNode> tree)
            where TNode : BinaryNode<TValue, TNode>
            where TValue : IComparable<TValue>
        {
            if (tree.Root == null) return;

            Print2DFromNode<TValue, TNode>(tree.Root, 0);
        }

        public static void Print2DFromNode<TValue, TNode>(TNode ptr, int level)
            where TNode : BinaryNode<TValue, TNode>
        {
            if (ptr == null) return;

            Print2DFromNode<TValue, TNode>(ptr.Right, level + 1);

            if (level != 0)
            {
                for (int i = 0; i < level - 1; i++)
                    Console.Write("|\t");

                Console.WriteLine("|-------" + ptr.Value.ToString());
            }
            else
                Console.WriteLine(ptr.Value.ToString());

            Print2DFromNode<TValue, TNode>(ptr.Left, level + 1);
        }
    }
}

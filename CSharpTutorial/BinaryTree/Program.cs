using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    public class Node
    {
        public int NodeIdentifier { get; set; } //to determine a given node value
        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }
    }

    public class Program
    {
        public static void ProcessNodeInBinaryTree(Node node)
        {
            if (node is null)
                return;

            if (node.LeftNode != null)
            {
                ProcessNodeInBinaryTree(node.LeftNode);
            }
            Console.WriteLine($"{node.NodeIdentifier}");

            if (node.RightNode != null)
            {
                ProcessNodeInBinaryTree(node.RightNode);
            }
        }

        public static void Main(string[] args)
        {
            Node leftNode = new Node
            {
                NodeIdentifier = 2,
                LeftNode = new Node { NodeIdentifier = 4, LeftNode = null, RightNode = null },
                RightNode = new Node { NodeIdentifier = 3, LeftNode = null, RightNode = null }
            };

            Node node = new Node
            {
                NodeIdentifier = 10,
                LeftNode = new Node { NodeIdentifier = 20, LeftNode = leftNode, RightNode = null },
                RightNode = new Node { NodeIdentifier = 40, LeftNode = null, RightNode = null },
            };

            ProcessNodeInBinaryTree(node);
        }
    }
}

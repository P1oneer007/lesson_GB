using System;
using System.Collections.Generic;

public class TreeNode
{
    public int Value { get; set; }
    public TreeNode LeftChild { get; set; }
    public TreeNode RightChild { get; set; }
}

public class Tree
{
    private TreeNode root;

    public Tree(TreeNode root)
    {
        this.root = root;
    }

    public void DFS()
    {
        Console.WriteLine("DFS:");
        DFSUtil(root);
        Console.WriteLine();
    }

    private void DFSUtil(TreeNode node)
    {
        if (node == null)
            return;

        Console.Write(node.Value + " ");

        DFSUtil(node.LeftChild);
        DFSUtil(node.RightChild);
    }

    public void BFS()
    {
        Console.WriteLine("BFS:");
        if (root == null)
            return;

        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            TreeNode node = queue.Dequeue();
            Console.Write(node.Value + " ");

            if (node.LeftChild != null)
                queue.Enqueue(node.LeftChild);

            if (node.RightChild != null)
                queue.Enqueue(node.RightChild);
        }

        Console.WriteLine();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        TreeNode root = new TreeNode
        {
            Value = 1,
            LeftChild = new TreeNode
            {
                Value = 2,
                LeftChild = new TreeNode { Value = 4 },
                RightChild = new TreeNode { Value = 5 }
            },
            RightChild = new TreeNode
            {
                Value = 3,
                LeftChild = new TreeNode { Value = 6 },
                RightChild = new TreeNode { Value = 7 }
            }
        };

        Tree tree = new Tree(root);
        tree.DFS();
        tree.BFS();
    }
}
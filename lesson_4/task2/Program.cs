using System;

public class TreeNode
{
    public int Value { get; set; }
    public TreeNode LeftChild { get; set; }
    public TreeNode RightChild { get; set; }
    public override bool Equals(object obj)
    {
        var node = obj as TreeNode;
        if (node == null)
            return false;
        return node.Value == Value;
    }
}
public interface ITree
{
    TreeNode GetRoot();
    void AddItem(int value); // добавить узел
    void RemoveItem(int value); // удалить узел по значению
    TreeNode GetNodeByValue(int value); //получить узел дерева по значению
    void PrintTree(); //вывести дерево в консоль
}
public static class TreeHelper
{
    public static NodeInfo[] GetTreeInLine(ITree tree)
    {
        var bufer = new Queue<NodeInfo>();
        var returnArray = new List<NodeInfo>();
        var root = new NodeInfo() { Node = tree.GetRoot() };
        bufer.Enqueue(root);
        while (bufer.Count != 0)
        {
            var element = bufer.Dequeue();
            returnArray.Add(element);
            var depth = element.Depth + 1;
            if (element.Node.LeftChild != null)
            {
                var left = new NodeInfo()
                {
                    Node = element.Node.LeftChild,
                    Depth = depth,
                };
                bufer.Enqueue(left);
            }
            if (element.Node.RightChild != null)
            {
                var right = new NodeInfo()
                {
                    Node = element.Node.RightChild,
                    Depth = depth,
                };
                bufer.Enqueue(right);
            }
        }
        return returnArray.ToArray();
    }
}
public class NodeInfo
{
    public int Depth { get; set; }
    public TreeNode Node { get; set; }
}
public class BinarySearchTree : ITree
{
    private TreeNode root;

    public TreeNode GetRoot()
    {
        return root;
    }

    public void AddItem(int value)
    {
        if (root == null)
        {
            root = new TreeNode { Value = value };
        }
        else
        {
            AddItemRecursive(root, value);
        }
    }

    private void AddItemRecursive(TreeNode node, int value)
    {
        if (value < node.Value)
        {
            if (node.LeftChild == null)
            {
                node.LeftChild = new TreeNode { Value = value };
            }
            else
            {
                AddItemRecursive(node.LeftChild, value);
            }
        }
        else if (value > node.Value)
        {
            if (node.RightChild == null)
            {
                node.RightChild = new TreeNode { Value = value };
            }
            else
            {
                AddItemRecursive(node.RightChild, value);
            }
        }
    }

    public void RemoveItem(int value)
    {
        root = RemoveItemRecursive(root, value);
    }

    private TreeNode RemoveItemRecursive(TreeNode node, int value)
    {
        if (node == null)
        {
            return null;
        }

        if (value < node.Value)
        {
            node.LeftChild = RemoveItemRecursive(node.LeftChild, value);
        }
        else if (value > node.Value)
        {
            node.RightChild = RemoveItemRecursive(node.RightChild, value);
        }
        else
        {
            if (node.LeftChild == null)
            {
                return node.RightChild;
            }
            else if (node.RightChild == null)
            {
                return node.LeftChild;
            }

            node.Value = GetMinValue(node.RightChild);
            node.RightChild = RemoveItemRecursive(node.RightChild, node.Value);
        }

        return node;
    }

    private int GetMinValue(TreeNode node)
    {
        int minValue = node.Value;
        while (node.LeftChild != null)
        {
            minValue = node.LeftChild.Value;
            node = node.LeftChild;
        }
        return minValue;
    }

    public TreeNode GetNodeByValue(int value)
    {
        return GetNodeByValueRecursive(root, value);
    }

    private TreeNode GetNodeByValueRecursive(TreeNode node, int value)
    {
        if (node == null || node.Value == value)
        {
            return node;
        }

        if (value < node.Value)
        {
            return GetNodeByValueRecursive(node.LeftChild, value);
        }
        else
        {
            return GetNodeByValueRecursive(node.RightChild, value);
        }
    }

    public void PrintTree()
    {
        var treeArray = TreeHelper.GetTreeInLine(this);
        foreach (var nodeInfo in treeArray)
        {
            Console.WriteLine($"{new string('-', nodeInfo.Depth * 2)}{nodeInfo.Node.Value}");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var tree = new BinarySearchTree();
        tree.AddItem(1);
        tree.AddItem(2);
        tree.AddItem(3);
        tree.AddItem(4);
        tree.AddItem(5);
        tree.AddItem(6);
        tree.AddItem(7);
        tree.AddItem(8);
        tree.AddItem(9);
        tree.AddItem(10);

        tree.RemoveItem(6);

        tree.PrintTree();
    }
}
using System;
using System.Collections.Generic;

public class Graph
{
    private int V; // Количество вершин
    private List<int>[] adjacencyList; // Список смежности

    public Graph(int v)
    {
        V = v;
        adjacencyList = new List<int>[V];
        for (int i = 0; i < V; i++)
        {
            adjacencyList[i] = new List<int>();
        }
    }

    public void AddEdge(int v, int w)
    {
        adjacencyList[v].Add(w);
    }

    public void DFS(int startVertex)
    {
        bool[] visited = new bool[V];
        DFSUtil(startVertex, visited);
    }

    private void DFSUtil(int vertex, bool[] visited)
    {
        visited[vertex] = true;
        Console.Write(vertex + " ");

        foreach (int adjacentVertex in adjacencyList[vertex])
        {
            if (!visited[adjacentVertex])
            {
                DFSUtil(adjacentVertex, visited);
            }
        }
    }

    public void BFS(int startVertex)
    {
        bool[] visited = new bool[V];
        Queue<int> queue = new Queue<int>();

        visited[startVertex] = true;
        queue.Enqueue(startVertex);

        while (queue.Count > 0)
        {
            int vertex = queue.Dequeue();
            Console.Write(vertex + " ");

            foreach (int adjacentVertex in adjacencyList[vertex])
            {
                if (!visited[adjacentVertex])
                {
                    visited[adjacentVertex] = true;
                    queue.Enqueue(adjacentVertex);
                }
            }
        }
    }
}

public class Program
{
    public static void Main()
    {
        int vertices = 6;
        Graph graph = new Graph(vertices);

        graph.AddEdge(0, 1);
        graph.AddEdge(0, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(1, 4);
        graph.AddEdge(2, 4);
        graph.AddEdge(3, 4);
        graph.AddEdge(3, 5);
        graph.AddEdge(4, 5);

        Console.WriteLine("DFS обход:");
        graph.DFS(0);
        Console.WriteLine();

        Console.WriteLine("BFS обход:");
        graph.BFS(0);
        Console.WriteLine();
    }
}
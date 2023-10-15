using System;

class Program
{
    static void Main()
    {
        int M = 3; // количество строк
        int N = 4; // количество столбцов

        int count = CountPaths(M, N);
        Console.WriteLine("Количество путей: " + count);
    }

    static int CountPaths(int m, int n)
    {
        int[,] dp = new int[m, n];

        // Инициализация первого столбца
        for (int i = 0; i < m; i++)
        {
            dp[i, 0] = 1;
        }

        // Инициализация первой строки
        for (int j = 0; j < n; j++)
        {
            dp[0, j] = 1;
        }

        // Заполнение остальных клеток
        for (int i = 1; i < m; i++)
        {
            for (int j = 1; j < n; j++)
            {
                dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
            }
        }

        return dp[m - 1, n - 1];
    }
}
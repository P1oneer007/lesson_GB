using System;
class Fibonacci
{
    // Рекурсивная версия
    public static int FibonacciRecursive(int n)
    {
        if (n <= 1)
            return n;
        return FibonacciRecursive(n - 2) + FibonacciRecursive(n - 1);
    }
    // Версия без рекурсии (через цикл)
    public static int FibonacciIterative(int n)
    {
        if (n <= 1)
            return n;
        int prevPrev = 0;
        int prev = 1;
        int current = 0;
        for (int i = 2; i <= n; i++)
        {
            current = prevPrev + prev;
            prevPrev = prev;
            prev = current;
        }
        return current;
    }
    static void Main()
    {
        int n = 0;
        Console.Write("Введите n: ");
        n = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Рекурсивная версия:");
        for (int i = 0; i <= n; i++)
        {
            Console.WriteLine($"F({i}) = {FibonacciRecursive(i)}");
        }

        Console.WriteLine("\nВерсия без рекурсии (через цикл):");
        for (int i = 0; i <= n; i++)
        {
            Console.WriteLine($"F({i}) = {FibonacciIterative(i)}");
        }
    }
}
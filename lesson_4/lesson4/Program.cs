using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        int size = 10000;
        string[] array = GenerateRandomStrings(size);
        HashSet<string> hashSet = new HashSet<string>(array);

        string searchString = "some string"; // строка, которую будем искать

        // Замер производительности для массива
        Stopwatch arrayStopwatch = Stopwatch.StartNew();
        bool arrayContains = ArrayContains(array, searchString);
        arrayStopwatch.Stop();

        // Замер производительности для HashSet
        Stopwatch hashSetStopwatch = Stopwatch.StartNew();
        bool hashSetContains = hashSet.Contains(searchString);
        hashSetStopwatch.Stop();

        Console.WriteLine("Результаты замеров:");
        Console.WriteLine($"Поиск в массиве: {arrayStopwatch.Elapsed}");
        Console.WriteLine($"Поиск в HashSet: {hashSetStopwatch.Elapsed}");
        Console.WriteLine($"Результат поиска в массиве: {arrayContains}");
        Console.WriteLine($"Результат поиска в HashSet: {hashSetContains}");
    }

    static bool ArrayContains(string[] array, string searchString)
    {
        foreach (string str in array)
        {
            if (str == searchString)
            {
                return true;
            }
        }
        return false;
    }

    static string[] GenerateRandomStrings(int size)
    {
        string[] strings = new string[size];
        Random random = new Random();

        for (int i = 0; i < size; i++)
        {
            strings[i] = Guid.NewGuid().ToString(); // генерируем случайную строку
        }

        return strings;
    }
}
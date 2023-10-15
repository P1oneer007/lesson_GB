using System;
using System.Collections.Generic;
using System.Linq;

public class BucketSort
{
    public static void Sort(int[] array, int bucketSize)
    {
        if (array == null || array.Length == 0)
            return;

        int minValue = array.Min();
        int maxValue = array.Max();

        int bucketCount = (maxValue - minValue) / bucketSize + 1;
        List<List<int>> buckets = new List<List<int>>(bucketCount);

        for (int i = 0; i < bucketCount; i++)
        {
            buckets.Add(new List<int>());
        }

        foreach (int num in array)
        {
            int bucketIndex = (num - minValue) / bucketSize;
            buckets[bucketIndex].Add(num);
        }

        int currentIndex = 0;
        foreach (List<int> bucket in buckets)
        {
            int[] tempArray = bucket.ToArray();
            InsertionSort(tempArray);

            Array.Copy(tempArray, 0, array, currentIndex, tempArray.Length);
            currentIndex += tempArray.Length;
        }
    }

    private static void InsertionSort(int[] array)
    {
        for (int i = 1; i < array.Length; i++)
        {
            int key = array[i];
            int j = i - 1;

            while (j >= 0 && array[j] > key)
            {
                array[j + 1] = array[j];
                j--;
            }

            array[j + 1] = key;
        }
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        int[] array = { 29, 25, 3, 49, 9, 37, 21, 43, 12, 5 };

        Console.WriteLine("Original array:");
        PrintArray(array);

        BucketSort.Sort(array, 10);

        Console.WriteLine("Sorted array:");
        PrintArray(array);
    }

    private static void PrintArray(int[] array)
    {
        foreach (int num in array)
        {
            Console.Write(num + " ");
        }
        Console.WriteLine();
    }
}
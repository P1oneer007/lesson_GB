using System;
public class BinarySearch
{
    public static int BinarySearchRecursive(int[] arr, int target)
    {
        return BinarySearchRecursive(arr, target, 0, arr.Length - 1);
    }

    private static int BinarySearchRecursive(int[] arr, int target, int left, int right)
    {
        if (left > right)
        {
            return -1; // Элемент не найден
        }

        int mid = left + (right - left) / 2;

        if (arr[mid] == target)
        {
            return mid; // Элемент найден
        }
        else if (arr[mid] < target)
        {
            return BinarySearchRecursive(arr, target, mid + 1, right); // Искать в правой половине
        }
        else
        {
            return BinarySearchRecursive(arr, target, left, mid - 1); // Искать в левой половине
        }
    }
}

class Program
{
    static void Main()
    {
        int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int target = 7;
        int result = BinarySearch.BinarySearchRecursive(arr, target);

        if (result != -1)
        {
            Console.WriteLine($"Элемент {target} найден в позиции {result}");
        }
        else
        {
            Console.WriteLine($"Элемент {target} не найден в массиве");
        }
    }
}

//Асимптотическая сложность бинарного поиска - O(log n), где n - количество элементов в отсортированном массиве
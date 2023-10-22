using System;
using System.Collections.Generic;
using System.IO;

class ExternalSort
{
    public static void Sort(string inputFile, string outputFile, int chunkSize)
    {
        // Чтение и разделение исходного файла на временные файлы
        int fileIndex = 0;
        List<string> tempFiles = new List<string>();

        using (StreamReader reader = new StreamReader(inputFile))
        {
            List<int> chunk = new List<int>();

            while (!reader.EndOfStream)
            {
                int number = int.Parse(reader.ReadLine());
                chunk.Add(number);

                if (chunk.Count >= chunkSize)
                {
                    chunk.Sort();
                    string tempFile = $"temp{fileIndex++}.txt";
                    tempFiles.Add(tempFile);

                    using (StreamWriter writer = new StreamWriter(tempFile))
                    {
                        foreach (int num in chunk)
                        {
                            writer.WriteLine(num);
                        }
                    }

                    chunk.Clear();
                }
            }

            if (chunk.Count > 0)
            {
                chunk.Sort();
                string tempFile = $"temp{fileIndex++}.txt";
                tempFiles.Add(tempFile);

                using (StreamWriter writer = new StreamWriter(tempFile))
                {
                    foreach (int num in chunk)
                    {
                        writer.WriteLine(num);
                    }
                }
            }
        }

        // Объединение временных файлов в отсортированный файл
        using (StreamWriter writer = new StreamWriter(outputFile))
        {
            List<StreamReader> readers = new List<StreamReader>();

            foreach (string tempFile in tempFiles)
            {
                readers.Add(new StreamReader(tempFile));
            }

            PriorityQueue<int, int> minHeap = new PriorityQueue<int, int>();

            for (int i = 0; i < readers.Count; i++)
            {
                if (!readers[i].EndOfStream)
                {
                    int number = int.Parse(readers[i].ReadLine());
                    minHeap.Enqueue(number, i);
                }
            }

            while (minHeap.Count > 0)
            {
                int minNumber = minHeap.Dequeue(out int minIndex);
                writer.WriteLine(minNumber);

                if (!readers[minIndex].EndOfStream)
                {
                    int number = int.Parse(readers[minIndex].ReadLine());
                    minHeap.Enqueue(number, minIndex);
                }
            }

            foreach (StreamReader reader in readers)
            {
                reader.Close();
            }
        }

        // Удаление временных файлов
        foreach (string tempFile in tempFiles)
        {
            File.Delete(tempFile);
        }
    }
}

class PriorityQueue<TPriority, TValue>
{
    private SortedDictionary<TPriority, Queue<TValue>> _dict;

    public int Count { get; private set; }

    public PriorityQueue()
    {
        _dict = new SortedDictionary<TPriority, Queue<TValue>>();
        Count = 0;
    }

    public void Enqueue(TPriority priority, TValue value)
    {
        if (!_dict.ContainsKey(priority))
        {
            _dict[priority] = new Queue<TValue>();
        }

        _dict[priority].Enqueue(value);
        Count++;
    }

    public TValue Dequeue(out TPriority priority)
    {
        foreach (var pair in _dict)
        {
            if (pair.Value.Count > 0)
            {
                priority = pair.Key;
                Count--;
                return pair.Value.Dequeue();
            }
        }

        throw new InvalidOperationException("The priority queue is empty.");
    }
}


public class Program
{
    public static void Main(string[] args)
    {
        string inputFile = "input.txt";
        string outputFile = "output.txt";
        int bufferSize = 1000;

        // Создание входного файла с случайными числами
        GenerateInputFile(inputFile, 1000000);

        // Запуск внешней сортировки
        ExternalSort.Sort(inputFile, outputFile, bufferSize);

        // Проверка отсортированности выходного файла
        CheckSortedOutputFile(outputFile);

        Console.WriteLine("External sort completed successfully.");
    }

    private static void GenerateInputFile(string filename, int numCount)
    {
        Random random = new Random();

        using (StreamWriter writer = new StreamWriter(filename))
        {
            for (int i = 0; i < numCount; i++)
            {
                writer.WriteLine(random.Next(1000000));
            }
        }
    }

    private static void CheckSortedOutputFile(string filename)
    {
        int previousNum = int.MinValue;

        using (StreamReader reader = new StreamReader(filename))
        {
            while (!reader.EndOfStream)
            {
                int currentNum = int.Parse(reader.ReadLine());

                if (currentNum < previousNum)
                {
                    throw new Exception("Output file is not sorted correctly.");
                }

                previousNum = currentNum;
            }
        }
    }
}

/*public class Program
{
    public static void Main(string[] args)
    {
        string inputFile = "input.txt";
        string outputFile = "output.txt";
        int chunkSize = 1000;
        
        ExternalSort.Sort(inputFile, outputFile, chunkSize);

        Console.WriteLine("External sorting completed.");
    }
}*/
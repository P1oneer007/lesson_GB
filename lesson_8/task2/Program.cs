using System;
using System.Collections.Generic;
using System.IO;
class ExternalSor
{
    static void Main(string[] args)
    {
        string inputFile = "input.txt";
        string outputFile = "output.txt";
        int chunkSize = 1000000;

        ExternalSort(inputFile, outputFile, chunkSize);

        Console.WriteLine("Sorting complete.");
    }

    static void ExternalSort(string inputFile, string outputFile, int chunkSize)
    {
        List<string> chunkFiles = new List<string>();

        using (StreamReader reader = new StreamReader(inputFile))
        {
            int i = 0;
            while (!reader.EndOfStream)
            {
                List<string> chunk = new List<string>();
                for (int j = 0; j < chunkSize && !reader.EndOfStream; j++)
                {
                    chunk.Add(reader.ReadLine());
                }
                chunk.Sort();
                string chunkFile = "chunk" + i + ".txt";
                chunkFiles.Add(chunkFile);
                using (StreamWriter writer = new StreamWriter(chunkFile))
                {
                    foreach (string line in chunk)
                    {
                        writer.WriteLine(line);
                    }
                }
                i++;
            }
        }

        MergeFiles(chunkFiles, outputFile);

        foreach (string chunkFile in chunkFiles)
        {
            File.Delete(chunkFile);
        }
    }

    static void MergeFiles(List<string> inputFiles, string outputFile)
    {
        using (StreamWriter writer = new StreamWriter(outputFile))
        {
            List<StreamReader> readers = new List<StreamReader>();
            foreach (string inputFile in inputFiles)
            {
                readers.Add(new StreamReader(inputFile));
            }

            List<string> lines = new List<string>();
            foreach (StreamReader reader in readers)
            {
                if (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());
                }
            }

            while (lines.Count > 0)
            {
                lines.Sort();
                writer.WriteLine(lines[0]);
                int index = lines.IndexOf(lines[0]);
                lines.RemoveAt(index);

                if (!readers[index].EndOfStream)
                {
                    lines.Add(readers[index].ReadLine());
                }
                else
                {
                    readers[index].Close();
                    readers.RemoveAt(index);
                }
            }

            foreach (StreamReader reader in readers)
            {
                reader.Close();
            }
        }
    }
}
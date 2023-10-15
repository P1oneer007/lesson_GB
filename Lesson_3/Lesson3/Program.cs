using System;
using System.Numerics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

public class DistanceBenchmark
{
    private PointClass[] pointClassArray;
    private PointStructFloat[] pointStructFloatArray;
    private PointStructDouble[] pointStructDoubleArray;

    [Params(1000)]
    public int ArraySize { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        // Генерация массивов данных
        Random random = new Random();
        pointClassArray = new PointClass[ArraySize];
        pointStructFloatArray = new PointStructFloat[ArraySize];
        pointStructDoubleArray = new PointStructDouble[ArraySize];

        for (int i = 0; i < ArraySize; i++)
        {
            float x = (float)random.NextDouble();
            float y = (float)random.NextDouble();
            double z = random.NextDouble();
            pointClassArray[i] = new PointClass(x, y);
            pointStructFloatArray[i] = new PointStructFloat(x, y);
            pointStructDoubleArray[i] = new PointStructDouble(x, y, z);
        }
    }
    
    [Benchmark]
    public void CalculateDistance_PointClass()
    {
        for (int i = 0; i < ArraySize; i++)
        {
            float distance = pointClassArray[i].CalculateDistance();
        }
    }

    [Benchmark]
    public void CalculateDistance_PointStructFloat()
    {
        for (int i = 0; i < ArraySize; i++)
        {
            float distance = pointStructFloatArray[i].CalculateDistance();
        }
    }

    [Benchmark]
    public void CalculateDistance_PointStructDouble()
    {
        for (int i = 0; i < ArraySize; i++)
        {
            double distance = pointStructDoubleArray[i].CalculateDistance();
        }
    }

    [Benchmark]
    public void CalculateDistance_NoSqrt_PointStructFloat()
    {
        for (int i = 0; i < ArraySize; i++)
        {
            float distance = pointStructFloatArray[i].CalculateDistanceNoSqrt();
        }
    }
}

public class PointClass
{
    public float X { get; set; }
    public float Y { get; set; }

    public PointClass(float x, float y)
    {
        X = x;
        Y = y;
    }

    public float CalculateDistance()
    {
        return MathF.Sqrt(X * X + Y * Y);
    }
}

public struct PointStructFloat
{
    public float X { get; set; }
    public float Y { get; set; }

    public PointStructFloat(float x, float y)
    {
        X = x;
        Y = y;
    }

    public float CalculateDistance()
    {
        return MathF.Sqrt(X * X + Y * Y);
    }

    public float CalculateDistanceNoSqrt()
    {
        return X * X + Y * Y;
    }
}

public struct PointStructDouble
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public PointStructDouble(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public double CalculateDistance()
    {
        return Math.Sqrt(X * X + Y * Y + Z * Z);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<DistanceBenchmark>();
    }
}
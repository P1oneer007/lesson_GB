using System;
namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 0;
            Console.Write("write n: ");
            n = Convert.ToInt32(Console.ReadLine());
            int d = 0;
            int i = 2;
            while (i < n)
            {
                if (n % i == 0)
                {
                    d++;
                }
                else
                {
                    i++;
                }
                break;
            }
            if (d == 0)
            {
               Console.WriteLine("prostoe");
            }
            else
            {
               Console.WriteLine("not prostoe");
            }
        }
    }
}
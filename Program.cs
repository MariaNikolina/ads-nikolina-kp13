using System;
using System.Collections.Generic;
namespace АСД
{
    class Program
    {
        static int CountDigitByString(int n)
        {
            return Math.Abs(n).ToString().Length;
        }
        static void Main()
        {
            int N, i, g, temp;
            Console.WriteLine("Enter N");
            bool Ncheck = int.TryParse(Console.ReadLine(), out N);
            Random rnd = new Random();
            int[] matrix = new int[N];
            Console.WriteLine("Input Array:");

            for (i = 0; i < N; i++)
            {
                Console.Write("matrix[" + (i + 1).ToString() + "] = ");
                matrix[i] = rnd.Next(-50, 151);
                Console.WriteLine(matrix[i].ToString());
            }

            bool flag = true;
            for (i = 0; (i < N) && flag; i++)
            {
                flag = false;
                for (g = 0; g < N - 1; g++)
                {
                    if (matrix[g] > matrix[g + 1])
                    {
                        temp = matrix[g];
                        matrix[g] = matrix[g + 1];
                        matrix[g + 1] = temp;
                        flag = true;
                    }
                }
            }

            Console.WriteLine("\nSorted Array (за неспаданням за значенням числа):");
            for (i = 0; i < N; i++)
                Console.WriteLine("matrix[" + (i + 1).ToString() + "] = " + matrix[i].ToString());

            List<int> OneN = new List<int>();
            List<int> TwoN = new List<int>();
            List<int> ThreeN = new List<int>();

            for (int j = 0; j < N; j++)
            {
                int w = CountDigitByString(matrix[j]);

                if (w == 1)
                {
                    OneN.Add(matrix[j]);
                }
                else if (w == 2)
                {
                    TwoN.Add(matrix[j]);
                }
                else if (w == 3)
                {
                    ThreeN.Add(matrix[j]);
                }

            }
            Console.WriteLine("\nSorted Array (за незростанням за кiлькiстю цифр у числi та за неспаданням за значенням числа):");

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var d in ThreeN)
                Console.WriteLine(d);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            foreach (var d in TwoN)
                Console.WriteLine(d);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            foreach (var d in OneN)
                Console.WriteLine(d);;
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}


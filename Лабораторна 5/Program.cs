
namespace Program
{
    public static class Bucketsort
    {
       
        public static List<int> Bucketsort1(params int[] x)
        {
            List<int> result = new List<int>();
            //1-10, 11-20, 21-30... to 99
            int numOfBuckets = 10;

            List<int>[] buckets = new List<int>[numOfBuckets];
            for (int i = 0; i < numOfBuckets; i++)
                buckets[i] = new List<int>();

            for (int i = 0; i < x.Length; i++)
            {
                int buckChoice = x[i] / numOfBuckets;
                buckets[buckChoice].Add(x[i]);
            }

            for (int i = 0; i < numOfBuckets; i++)
            {
                int[] temp = Bubblesort(buckets[i]);
                result.AddRange(temp);
            }
            return result;
        }

        public static int[] Bubblesort (List<int> input)
        {
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input.Count; j++)
                {
                    if (input[i] < input[j])
                    {
                        int temp = input[i];
                        input[i] = input[j];
                        input[j] = temp;
                    }
                }
            }
            return input.ToArray();
        }
        class Program
        {
            static void Main(string[] args)
            {
                
                Console.WriteLine("\nCommands:\n\t1. use random values;\n\t2. use control values ");
                string command = Console.ReadLine();
                     
                     if (command == "2" || command == "control")
                     {
                            int k = 60;
                            Console.WriteLine("Number N = 15, Number K = 60");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            int[] x = new int[] { 99, 81, 46, 25, 95, 79, 11, 32, 53, 2, 67, 39, 5, 64, 38 };
                            Console.WriteLine("{ 99, 81, 46, 25, 95, 79, 11, 32, 53, 2, 67, 39, 5, 64, 38 }");

                                List<int> sorted = Bucketsort1(x);
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                            
                                var even1 = sorted.Where(d => d < k);
                                foreach (int d in even1.Reverse())
                                Console.WriteLine(d);
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                var even2 = sorted.Where(p => p > k);
                                foreach (int p in even2)
                                Console.WriteLine(p);
                                Console.ForegroundColor = ConsoleColor.White;
                     }
                else if (command == "1" || command == "random values")
                {
                    int n, k;

                    Console.WriteLine("Enter number N:");
                    bool parseN = int.TryParse(Console.ReadLine(), out n);
                    if (!parseN || n <= 0 || n >= 100)
                    {
                        Console.WriteLine("Error: wrong value (number N)");
                        Environment.Exit(1);
                    }
                    Console.WriteLine("Enter number K:");
                    bool parseK = int.TryParse(Console.ReadLine(), out k);
                    if (!parseK || k <= 0 || k >= 100)
                    {
                        Console.WriteLine("Error: wrong value (number K)");
                        Environment.Exit(1);
                    }
                    Random rnd = new Random();
                    int[] matrix = new int[n];
                    for (int i = 0; i < n; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("matrix[" + (i + 1).ToString() + "] = ");
                        matrix[i] = rnd.Next(1, 100);
                        Console.WriteLine(matrix[i].ToString());
                    }
                    List<int> sorted = Bucketsort1(matrix);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;

                    var even1 = sorted.Where(d => d < k);
                    foreach (int d in even1.Reverse())
                        Console.WriteLine(d);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;

                    var even2 = sorted.Where(p => p > k);
                    foreach (int p in even2)
                        Console.WriteLine(p);
                    Console.ForegroundColor = ConsoleColor.White;
                }
         
                else
                {
                    Console.WriteLine("Error: wrong command");
                    Environment.Exit(1);
                }

            }
        }
    }
}



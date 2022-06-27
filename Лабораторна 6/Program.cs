using System;

namespace ConsoleApplication1
{
    class Stack
    {
        private int[] huge;
        private int top;
        private int max;
        public Stack(int size)
        {
            huge = new int[size];
            top = -1;
            max = size;
        }

        public void push(int item )
        {
            if (top == max - 1)
            {
                Console.WriteLine("Stack Overflow");
                return;
            }
            else
            {
                huge[++top] = item;
            }
        }

        public int pop()
        {
            if (top == -1)
            {
                Console.WriteLine("Stack Underflow");
                return -1;
            }
            else
            {
                Console.WriteLine("Poped element is: " + huge[top]);
                return huge[top--];
            }
        }

        public void printStack()
        {
            if (top == -1)
            {
                Console.WriteLine("Stack is Empty");
                return;
            }
            else
            {
                for (int i = 0; i <= top; i++)
                {
                    Console.WriteLine("Item[" + (i + 1) + "]: " + huge[i]);
                }
            }

        }
        void Hanoi(int from, int to, int k)
        {
            int to2 = 6 - from - to; 
            if (k > 1)
            {
                Hanoi(from, to2, k - 1);
                Hanoi(from, to, 1);
                Hanoi(to2, to, k - 1);
            }
            else
            {
                A[to - 1].Push(A[from - 1].Pop());
            }
        }

    }


        class Program
    {
        static void Main()
        {
                int[] arr = new int[] { 1, 2, 3, 4 };
                Stack<int> myStack = new Stack<int>(arr);

                foreach (var item in myStack)
                    Console.Write(item + ",");

                int k;
            Console.WriteLine("Enter number K:");
            bool parseK = int.TryParse(Console.ReadLine(), out k);
            if (!parseK || k <= 0 || k >= 15)
                    {
                Console.WriteLine("Error: wrong value (number K)");
                Environment.Exit(1);
            }
            Stack A = new Stack(k);
            Stack B = new Stack(k);
            Stack C = new Stack(k);
            
            for (int i = 0; i < k; i++)
            {
                A.push(i+1 );

            }
               


                Console.Write("A: ");
            Console.WriteLine("Items are : ");
            A.printStack();

        }
    }
  }

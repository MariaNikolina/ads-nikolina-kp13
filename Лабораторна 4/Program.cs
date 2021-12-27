using static System.Console;
using System.Collections;
using System.Linq;
namespace acd_4_
{
    class DLList
    {
        static private int size = 0;
        static DLNode tail;
        static DLNode head;
        public class DLNode
        {
            public int data;
            public DLNode next;
            public DLNode prev;
            public DLNode min;
            public DLNode(int data)
            {
                this.data = data;
            }
        }

        public DLList(int data)
        {
            tail = new DLNode(data);
            head = new DLNode(data);
        }

        public static void AddFirst(int data)
        {
            DLNode newNode = new DLNode(data);
            if (tail == null)
            {
                newNode.next = newNode.prev = newNode;
                tail = newNode;
            }
            else
            {
                DLNode head = tail.next;
                newNode.next = head;
                head.prev = newNode;
                newNode.prev = tail;
                tail.next = newNode;
            }
            size++;
        }

        public static void AddLast(int data)
        {
            DLNode newNode = new DLNode(data);
            if (tail == null)
            {
                newNode.next = newNode.prev = newNode;
                tail = newNode;
            }
            else
            {
                head = tail.next;
                newNode.next = head;
                head.prev = newNode;
                newNode.prev = tail;
                tail.next = newNode;
                tail = newNode;
            }
            size++;
        }

        public static void AddAtPosition(int data, int pos)
        {
            if (pos <= 0)
            {
                WriteLine("Error: incorrect index");
                return;
            }
            else if (pos == 1 || tail == null)
            {
                AddFirst(data);
                return;
            }
            if (tail == tail.next)
            {
                AddLast(data);
                return;
            }
            DLNode newNode = new DLNode(data);
            DLNode current = tail.next;
            int counter = 1;
            while (counter != pos - 1)
            {
                current = current.next;
                counter++;
                if (current == tail && counter == pos - 1)
                {
                    AddLast(data);
                    return;
                }
                else if (current == tail.next)
                {
                    WriteLine("Помилка - неправильний iндекс");
                    return;
                }
            }
            DLNode next = current.next;
            current.next = newNode;
            newNode.prev = current;
            newNode.next = next;
            next.prev = newNode;
            size++;
        }


        public static void ToPosition(int data)
        {
            DLNode newNode = new DLNode(data);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
                tail.next = head;
                head.prev = tail;
            }
            else
            {

                DLNode current = head;
                DLNode min = head;
                while (current != tail)
                {
                    if (current.data % 2.0 == 0 && min.data > current.data)
                    {
                        min = current;
                    }
                    current = current.next;
                }
                if (min.data > tail.data)
                {
                    AddLast(data);
                    return;
                }

                DLNode next = min.next;
                min.next = newNode;
                newNode.prev = min;
                newNode.next = min.next;
                next.prev = newNode;
              
              }
            size++;
        }

        public static void DeleteFirst()
        {
            if (tail == null)
            {
                WriteLine("Помилка - список вже пустий ");
                return;
            }
            else if (tail == tail.next)
            {
                tail = null;
                return;
            }
            else if (tail == tail.next.next)
            {
                tail.next = tail;
                tail = tail.prev;
                return;
            }
            tail.next = tail.next.next;
            tail.next.prev = tail;
            size--;
        }

        public static void DeleteLast()
        {
            if (tail == null)
            {
                WriteLine("Помилка - список вже пустий ");
                return;
            }
            head = tail.next;
            if (tail == tail.next)
            {
                tail = null;
            }
            else if (tail == tail.next.next)
            {
                head.next = head;
                head.prev = head;
            }
            else
            {
                tail.prev.next = head;
                head.prev = tail.prev;
            }
            size--;
        }

        public static void DeleteAtPosition(int pos)
        {
            if (tail == null)
            {
                return;
            }
            else if (tail == tail.next)
            {
                tail = null;
                return;
            }
            if (pos <= 0)
            {
                WriteLine("Помилка - список вже пустий ");
                return;
            }
            else if (pos == 1)
            {
                DeleteFirst();
                return;
            }
            DLNode current = tail.next;
            int counter = 1;
            while (counter != pos - 1)
            {
                current = current.next;
                counter++;
                if (current == tail && counter == pos - 1)
                {
                    DeleteLast();
                    return;
                }
                else if (current == tail.next)
                {
                    WriteLine("Помилка - список вже пустий ");
                    return;
                }
            }
            DLNode next = current.next.next;
            current.next = next;
            next.prev = current;
            size--;
        }
        public static void ProcessList()
        {
            WriteLine("Список - ");
            Print();
            SLList.Print();
        }

        public static void Print()
        {
            if (tail == null)
            {
                WriteLine("Помилка - лист пустий");
                return;
            }
            DLNode current = tail.next;
            do
            {
                Write("{0}", current.data);
                if (current.next != tail.next)
                {
                    Write("-->");
                }
                current = current.next;
            }
            while (current != tail.next);
            {
                WriteLine();
            }
        }
    }

    class Program
    {
        static bool CheckInputType(string part)
        {
            bool canBeParsed = false;
            int result;
            if (int.TryParse(part, out result))
            {
                canBeParsed = true;
            }
            return canBeParsed;
        }
        static void Main(string[] args)
        {
            int counter = 1;
            while (true)
            {
                WriteLine(" AddFirst (ввести також значення) \r\n AddLast (ввести також значення) \r\n AddAtPosition (ввести також значення та позицiю)\r\n ToPosition (ввести також значення)\r\n DeleteFirst\r\n DeleteLast\r\n DeleteAtPosition (ввести також позицiю)\r\n ProcessList\r\n Exit");
                WriteLine("Введiть запит: ");
                string[] input = ReadLine().Trim().Split();
                if (input.Length > 3 || input.Length < 1)
                {
                    WriteLine("Помилка - недiйсна команда");
                }
                switch (input[0])
                {
                    case "AddFirst":
                        if (input.Length != 2 || !CheckInputType(input[1]))
                        {
                            WriteLine("Помилка - недiйсна команда або тип вводу");
                        }
                        else
                        {
                            int data = int.Parse(input[1]);
                            DLList.AddFirst(data);
                            WriteLine("Поточний список: ");
                            DLList.Print();
                        }
                        break;
                    case "AddLast":
                        if (input.Length != 2 || !CheckInputType(input[1]))
                        {
                            WriteLine("Помилка - недiйсна команда або тип вводу");
                        }
                        else
                        {
                            int data = int.Parse(input[1]);
                            DLList.AddLast(data);
                            WriteLine("Поточний список: ");
                            DLList.Print();
                        }
                        break;
                    case "ToPosition":
                        if (input.Length != 2 || !CheckInputType(input[1]))
                        {
                            WriteLine("Помилка - недiйсна команда або тип вводу");
                        }
                        else
                        {
                            int data = int.Parse(input[1]);
                            DLList.ToPosition(data);
                            WriteLine("Поточний список: ");
                            DLList.Print();
                        }
                        break;
                    case "AddAtPosition":
                        if (input.Length != 3 || !CheckInputType(input[1]) || !CheckInputType(input[2]))
                        {
                            WriteLine("Помилка - недiйсна команда або тип вводу");
                        }
                        else
                        {
                            int data = int.Parse(input[1]);
                            int pos = int.Parse(input[2]);
                            DLList.AddAtPosition(data, pos);
                            WriteLine("Поточний список: ");
                            DLList.Print();
                        }
                        break;
                    case "DeleteFirst":
                        if (input.Length != 1)
                        {
                            WriteLine("Помилка - недiйсна команда ");
                        }
                        else
                        {
                            DLList.DeleteFirst();
                            WriteLine("Поточний список: ");
                            DLList.Print();
                        }
                        break;
                    case "DeleteLast":
                        if (input.Length != 1)
                        {
                            WriteLine("Помилка - недiйсна команда ");
                        }
                        else
                        {
                            DLList.DeleteLast();
                            WriteLine("Поточний список: ");
                            DLList.Print();
                        }
                        break;
                    case "DeleteAtPosition":
                        if (input.Length != 2 || !CheckInputType(input[1]))
                        {
                            WriteLine("Помилка - недiйсна команда або тип вводу");
                        }
                        else
                        {
                            int pos = int.Parse(input[1]);
                            DLList.DeleteAtPosition(pos);
                            WriteLine("Поточний список: ");
                            DLList.Print();
                        }
                        break;
                    case "ProcessList":
                        if (input.Length != 1)
                        {
                            WriteLine("Помилка - недiйсна команда ");
                        }
                        else
                        {
                            DLList.ProcessList();
                        }
                        break;
                    case "Exit":
                        if (input.Length != 1)
                        {
                            WriteLine("Помилка - недiйсна команда ");
                            break;
                        }
                        else
                        {
                            WriteLine("Exit");
                            return;
                        }
                    default:
                        WriteLine("Помилка - недiйсна команда ");
                        break;
                }
                counter++;
            }
        }
    }
    class SLList
    {
        static SLNode head;

        public class SLNode
        {
            public int data;
            public SLNode next;

            public SLNode(int data)
            {
                this.data = data;
            }
        }

        public SLList(int data)
        {
            head = new SLNode(data);
        }
        public static void Print()
        {
            WriteLine("SLList: ");
            if (head == null)
            {
                WriteLine("Список пустий");
                return;
            }
            SLNode current = head;
            while (current != null)
            {
                Write("{0}", current.data);
                if (current.next != null)
                {
                    Write("-->");
                }
                current = current.next;
            }
            WriteLine();
            head = null;
        }

        internal static object Where(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
class Program
{
    static void Main(string[] args)
    {
        double a, b, x, y, z;
        try
        {
        Console.WriteLine("Введiть 'x':");
        x = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Введiть 'y':");
        y = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Введiть 'z':");
        z = Convert.ToDouble(Console.ReadLine());
        if (x == 0 || z==0 || x < y)
        Console.Write(" не задовольняє ОДЗ");
            else
            {
          a = x + Math.Pow(Math.Abs(y) + z * z * z, 1.0 / 3) / (x * x * x + x);  
           Console.WriteLine(a);
  
        if (a != 0)
        {
            b = Math.Sqrt(x - y) / z + 1 / (a * a);
            Console.WriteLine(b);
        }
        else
                Console.WriteLine("b не iснує, а = 0");
            }
        }
        catch
        {
            Console.Write("невiрнi данi ");
        }
    }
}


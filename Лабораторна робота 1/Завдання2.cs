using System;
using static System.Console;
class Program
{
    static int countwdays(int year0, int weekday, int day)
    {
        if (year0 < 0 || weekday < 0 || weekday > 6) return 0;
        int[] mondays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        if ((year0 % 4 == 0) && (year0 % 100 != 0) || (year0 % 400 == 0)) mondays[1] = 29;
        int count = 0, year, month;
        for (int m = 1; m < 13; m++)
        {
            if (day < 1 || day > mondays[m - 1]) continue;
            if (m < 3) { month = m + 12; year = year0 - 1; } else { month = m; year = year0; }
            int c = year / 100, y = year % 100, wd = ((month + 1) * 26 / 10 + day + y + y / 4 + c / 4 - 2 * c) % 7;
            if (wd == weekday) count++;
        }
        return count;
    }
    static void Main()
    {
        int fr, to;
        Console.Write("from = ");
        fr = Convert.ToInt32(ReadLine());
        Console.Write("to = ");
        to = Convert.ToInt32(ReadLine());
        if (fr < 1582 || fr > 4802 || to < 1682 || to > 4902)
        {
            Console.Write("incorrect data ");
            return;
        }
        int c = 0;
        for (int year = fr; year < to; year++)
            c += countwdays(year, 6, 13);
        WriteLine("Number of Fridays 13 = " + c.ToString());
    }
}







































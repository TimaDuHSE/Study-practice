﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr3
{
class Program
    {
static void Main(string[] args)
{
    double a = 0;
    bool ok = false;
    double f(double x) => Math.Abs(Math.Abs(x) - 1); // функциядляграфика
    while (!ok)
    {
        Console.WriteLine("Введите число, для которого необходимо вычислить функцию");
        try
        {
            a = Convert.ToDouble(Console.ReadLine());
            ok = true;
        }
        catch { Console.WriteLine("Ошибкаввода"); }
    }

    Console.WriteLine("\nПолученное значение функции: " + f(a));
    Console.ReadKey();
}
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr11
{
    class Program
    {
        // Шифровка сообщения
        static string Encrypt(string Input)
        {
            string b = "";
            b += Input[0];
            for (int i = 1; i < Input.Length; i++)
                if (Input[i] == Input[i - 1])
                    b += 1;
                else b += 0;

            return b;
        }

        // Расшифровка сообщения
        static string Decrypt(string Input)
        {
            string b = "";
            b += Input[0];
            for (int i = 1; i < Input.Length; i++)
                if (Input[i] == 1)
                    b += b[i - 1];
                else b += (b[i - 1] + 1) % 2;

            return b;
        }
        static void Main(string[] args)
        {
            string Result;
            Console.WriteLine("Введите последовательность из 0 и 1.");
            Result = Console.ReadLine();
            for (int i = 0; i < Result.Length; i++)
            {
                if (Result[i] != 0 || Result[i] != 1)
                {
                    Console.WriteLine("В последовательности ошибка, перезапустите программу!");
                    Console.ReadKey();
                    return;
                }
            }

            Console.WriteLine("Зашифрованная последовательность:");
            Console.WriteLine(Encrypt(Result));
            Console.WriteLine("Расшифрованная последовательность:");
            Console.WriteLine(Decrypt(Result));

            Console.ReadKey();
        }
    }
}

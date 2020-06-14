using System;

namespace Practice6
{
    class Program
    {
        static void Main(string[] args)
        {
            // Инициализация входных данных.
            double a1 = InputDouble("Введите a1: ");
            double a2 = InputDouble("Введите a2: ");
            double a3 = InputDouble("Введите a3: ");
            int n = InputInt("Введитечисло N: ");

            if (n < 2)
            {
                Console.WriteLine("Число N должно быть равно 2 и более, чтобы была возможность проверить последовательность");
            }
            else
            {
                if (n == 2)
                {
                    // Ветка частного случая, когда достаточно проверки входных данных.
                    Console.WriteLine();
                    if (a1 > a3)
                    {
                        Console.WriteLine("Подпоследовательность является убывающей");
                    }
                    if (a1 == a3)
                    {
                        Console.WriteLine("Подпоследовательность не является ни убывающей, ни возрастающей");
                    }
                    if (a1 < a3)
                    {
                        Console.WriteLine("Подпоследовательность является возрастающей");
                    }
                }
                else
                {
                    double[] sequence = new double[n * 2 - 1]; // создание массива, представляющего последовательность
                    sequence[0] = a1; // инициализация первых элементов массива
                    sequence[1] = a2;
                    sequence[2] = a3;

                    // Дозаполнение массива элементами, созданными с помощью формулы зависимости.
                    for (int i = 3; i < sequence.Length; i++)
                    {
                        sequence[i] = 0.7 * sequence[i - 1] + 0.2 * sequence[i - 2] + (i + 1) * sequence[i - 3];
                    }

                    // Вывод массива в консоль.
                    Console.WriteLine();
                    for (int i = 1; i < sequence.Length + 1; i++)
                    {
                        if (i % 2 != 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(sequence[i - 1] + " ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(sequence[i - 1] + " ");
                        }
                    }

                    // Переменные, определяющие существование возрастания или убывания.
                    // Как минимум одна из них обязательно примет значение false.
                    bool fall = true;
                    bool rise = true;

                    // Проверканаубывание.
                    for (int i = 0; i < sequence.Length - 2; i += 2)
                    {
                        if (!(sequence[i] > sequence[i + 2]))
                        {
                            fall = false;
                            break;
                        }
                    }

                    // Проверка на возрастание.
                    for (int i = 0; i < sequence.Length - 2; i += 2)
                    {
                        if (!(sequence[i] < sequence[i + 2]))
                        {
                            rise = false;
                            break;
                        }
                    }

                    Console.WriteLine();
                    if (rise)
                    {
                        Console.WriteLine("Подпоследовательность является возрастающей");
                    }
                    if (fall)
                    {
                        Console.WriteLine("Подпоследовательность является убывающей");
                    }
                    if (!rise && !fall)
                    {
                        Console.WriteLine("Подпоследовательность не является ни убывающей, ни возрастающей");
                    }
                }
            }
            Console.WriteLine("\nPress something to exit");
            Console.ReadKey();
        }

        static double InputDouble(string msg, string errorMsg = "Необходимо ввести число или десятичную дробь через запятую")
        {
            double result = 0;

            Console.Write(msg);
            while (!double.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine(errorMsg);
            }

            return result;
        }

        static int InputInt(string msg, string errorMsg = "Необходимо ввести целое число")
        {
            int result = 0;

            Console.Write(msg);
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine(errorMsg);
            }

            return result;
        }
    }
}

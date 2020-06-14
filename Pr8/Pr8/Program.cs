using System;

namespace Pr8
{
    class Program
    {
        static Random rand = new Random();

        // Искомая цепь
        static string answerChain = "";

        // Длина искомой цепи
        static int lengthOfChain;

        static void Main(string[] args)
        {
            // Ввод значения элемента матрицы
            int elem = 0;
            // Размер матрицы смежности
            int countVertexes = 0;
            lengthOfChain = 0;
            // Выбор способа ввода данных
            int methodEnter;
            // Проверка корректности введенной матрицы 
            bool isValidMatrix;
            // Матрица смежности
            int[,] adjacencyMatrix = new int[0, 0];

            // Выбор способа ввода данных
            Console.WriteLine("Решаемая задача - поиск простой цепи длины k в графе, заданном матрицей смежности");
            Console.WriteLine("\nВыберите способ задания входных данных:\n1 - C клавиатуры\n2 - С помощью ДСЧ");

            methodEnter = InputNumber();
            while (methodEnter != 1 && methodEnter != 2)
            {
                Console.WriteLine("\nВведите 1 или 2\n");
                methodEnter = InputNumber();
            }

            switch (methodEnter)
            {
                // Создание матрицы смежности с клавиатуры
                case 1:
                    {
                        Console.WriteLine("\nВведите количество вершин в графе:");
                        countVertexes = InputNumber();
                        while (countVertexes < 2)
                        {
                            Console.WriteLine("\nВведите количество вершин в графе больше 1\n");
                            countVertexes = InputNumber();
                        }

                        // Присвоение матрице смежности введенной длины
                        adjacencyMatrix = new int[countVertexes, countVertexes];

                        // Заполнение матрицы смежности
                        for (int i = 0; i < countVertexes; i++)
                        {
                            for (int j = 0; j < countVertexes; j++)
                            {
                                Console.WriteLine($"\nВведите элемент {i + 1}-ой строки {j + 1}-го столбца матрицы:");
                                elem = InputNumber();
                                while (elem != 0 && elem != 1)
                                {
                                    Console.WriteLine("\nНеверное значение! Введите 0 или 1\n");
                                    elem = InputNumber();
                                }

                                adjacencyMatrix[i, j] = elem;
                            }
                        }

                        Console.WriteLine("\nВведите длину искомой цепи:");
                        lengthOfChain = InputNumber();
                        while (lengthOfChain < 2)
                        {
                            Console.WriteLine("\nВведите длину искомой цепи больше 1\n");
                            lengthOfChain = InputNumber();
                        }

                        break;
                    }
                // Генерация случайных данных
                case 2:
                    {
                        countVertexes = rand.Next(2, 5);
                        lengthOfChain = rand.Next(2, 5);
                        while (lengthOfChain > countVertexes)
                        {
                            lengthOfChain = rand.Next(2, 5);
                        }

                        adjacencyMatrix = MatrixCreating(countVertexes);
                        break;
                    }
            }

            // Проверка матрицы на корректность
            isValidMatrix = CorrectMatrix(adjacencyMatrix);

            // Если матрица корректна
            if (isValidMatrix)
            {
                Console.WriteLine("\nВаша матрица смежности:\n");

                // Вывод матрицы смежности
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(" ");
                for (int i = 0; i < countVertexes; i++)
                {
                    Console.Write(" " + (i + 1));
                }

                Console.WriteLine("\n");

                for (int i = 0; i < countVertexes; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write((i + 1) + " ");
                    Console.ResetColor();

                    for (int j = 0; j < countVertexes; j++)
                    {
                        Console.Write(adjacencyMatrix[i, j] + " ");
                    }

                    Console.WriteLine("\n");
                }

                // Создание цепи по введенной матрице
                CreateChain(adjacencyMatrix);
                // Вывод цепи
                if (answerChain != "")
                {
                    Console.WriteLine($"\nСлучайная цепь с длиной {lengthOfChain}: \n{answerChain}");
                }
                else
                {
                    Console.WriteLine($"\nЦепь с длиной {lengthOfChain} не найдена");
                }
            }
            // Если матрица некорректна
            else
            {
                Console.WriteLine("\nМатрица смежности введена неверно");
            }
        }

        // Ввод целого числа 
        static int InputNumber()
        {
            int number;
            bool isInt;

            do
            {
                string buf = Console.ReadLine();
                // Проверка числа на целочисленный тип 
                isInt = int.TryParse(buf, out number);
                // Если проверка не была пройдена
                if (!isInt)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Введите целое число\n");
                    Console.ResetColor();
                }
            } while (!isInt);

            return number;
        }

        // Метод случайного создания матрицы с длиной n
        static int[,] MatrixCreating(int countVertexes)
        {
            // Перменная для заполнения матрицы выше главной диагонали
            int tmp = 0;
            int[,] adjacencyMatrix = new int[countVertexes, countVertexes];

            for (int i = tmp; i < countVertexes; i++)
            {
                for (int j = tmp; j < countVertexes; j++)
                {
                    if (i == j)
                    {
                        adjacencyMatrix[i, j] = 0;
                    }
                    else
                    {
                        adjacencyMatrix[i, j] = rand.Next(2);
                        adjacencyMatrix[j, i] = adjacencyMatrix[i, j];
                    }
                }

                tmp++;
            }

            return adjacencyMatrix;
        }

        // Проверки матрицы на корректность
        static bool CorrectMatrix(int[,] adjacencyMatrix)
        {
            bool isCorrect = true;
            int tmp = 0;

            for (int i = tmp; i < adjacencyMatrix.GetLength(0) && isCorrect; i++)
            {
                if (adjacencyMatrix[i, i] != 0)
                {
                    Console.WriteLine("На главной диагонали должны быть только нули");
                    isCorrect = false;
                    break;
                }

                for (int j = tmp; j < adjacencyMatrix.GetLength(0); j++)
                {
                    // Проверка симметричных элементов матрицы на равенство
                    if (!((i == j) && adjacencyMatrix[i, j] == 0 || (adjacencyMatrix[j, i] == adjacencyMatrix[i, j])))
                    {
                        isCorrect = false;
                        break;
                    }
                }

                tmp++;
            }

            return isCorrect;
        }

        // Построение цепи
        static void CreateChain(int[,] adjacencyMatrix)
        {
            // Условие добавления самого первого ребра
            bool firstEdgeAdded = false;
            // Условие повторения ребра
            bool edgeRepeats = false;
            // Массив существующих ребер 
            int[] countedEdges;
            // Самое первое ребро 
            int initialEdge;

            // Обход матрицы
            for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
            {
                for (int j = 1; j <= adjacencyMatrix.GetLength(0); j++)
                {
                    // Ребро не может быть меньше 1
                    initialEdge = i + 1;
                    // countedEdges = new int[adjacencyMatrix.GetLength(0)];
                    countedEdges = new int[lengthOfChain];
                    firstEdgeAdded = false;
                    int currentChainCount = 0;

                    // Запуск цикла нахождения следующего смежного ребра
                    for (int endEdge = j; endEdge <= adjacencyMatrix.GetLength(0); endEdge++)
                    {
                        // Проверка на принадлежность данного ребра искомой цепи 
                        foreach (int edge in countedEdges)
                        {
                            if (endEdge == edge)
                            {
                                edgeRepeats = true;
                                break;
                            }

                            edgeRepeats = false;
                        }

                        if (edgeRepeats)
                        {
                            continue;
                        }

                        // Если ребро смежно текущему последнему ребру
                        if (adjacencyMatrix[initialEdge - 1, endEdge - 1] == 1)
                        {
                            // Ограничение повтора добавления одного и того же ребра
                            if (firstEdgeAdded == false)
                            {
                                answerChain = initialEdge.ToString();
                                countedEdges[currentChainCount] = initialEdge;
                                currentChainCount++;
                                firstEdgeAdded = true;
                            }

                            // Добавление ребра, смежного последнему
                            initialEdge = endEdge; // Обновление данных о последнем ребра 
                            answerChain += initialEdge.ToString();
                            // Если длина составленной цепи равна длине искомой цепи
                            if (firstEdgeAdded && answerChain.Length == lengthOfChain)
                            {
                                break;
                            }

                            // Добавление в массив существующих в цепи ребер последнего ребра во избежание повторения ребер
                            countedEdges[currentChainCount] = initialEdge;
                            currentChainCount++;
                            endEdge = 0;
                        }
                    }

                    // Если длина составленной цепи равна длине искомой цепи
                    if (firstEdgeAdded && answerChain.Length == lengthOfChain)
                    {
                        break;
                    }

                    answerChain = "";
                }

                // Если длина составленной цепи равна длине искомой цепи
                if (firstEdgeAdded && answerChain.Length == lengthOfChain)
                {
                    break;
                }
            }
        }
    }
}

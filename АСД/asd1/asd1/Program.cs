using System;
using System.Collections.Generic;
using System.Linq;

namespace asd1
{
    internal class Program
    {
        static string comm;
        // стандартна задача про рюкзак для порівняння значень
        static int max(int i, int j)
        {
            return (i > j) ? i : j;
        }
        static int[,] BasicKnapsack(int knapCap, int[] weights, int[] values)
        {
            int[,] T = new int[values.Length + 1, knapCap + 1];

            for (int i = 0; i <= values.Length; i++)
            {
                for (int j = 0; j <= knapCap; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        T[i, j] = 0;
                    }

                    else if (weights[i - 1] <= j)
                    {
                        T[i, j] = max(T[i - 1, j], values[i - 1] + T[i - 1, j - weights[i - 1]]);
                    }
                    else
                    {
                        T[i, j] = T[i - 1, j];
                    }
                }
            }
            return T;
        }
        static int BKresult(int[,] T, int[] values, int knapCap)
        {
            return T[values.Length, knapCap];
        }

        // варіант для роботи з дробовими числами для задачі з неповними предметами
        private static float max(float i, float j)
        {
            return (i > j) ? i : j;
        }
        static float[,] BasicKnapsack(int knapCap, int[] weights, float[] values)
        {
            float[,] T = new float[values.Length + 1, knapCap + 1];

            for (int i = 0; i <= values.Length; i++)
            {
                for (int j = 0; j <= knapCap; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        T[i, j] = 0;
                    }

                    else if (weights[i - 1] <= j)
                    {
                        T[i, j] = max(T[i - 1, j], values[i - 1] + T[i - 1, j - weights[i - 1]]);
                    }
                    else
                    {
                        T[i, j] = T[i - 1, j];
                    }
                }
            }
            return T;
        }

        static float BKresult(float[,] T, int[] values, int knapCap)
        {
            return T[values.Length, knapCap];
        }

        // розбиття неповних предметів
        static float[,] PartialKnapsack(int knapCap, int[] weights, int[] values, bool[] part)
        {
            List<float> temp = new List<float>();
            List<int> tempW = new List<int>();
            for (int i = 0; i < values.Length; i++)
            {
                if (part[i])
                {
                    float t = (float)values[i]/(float)weights[i];
                    for(int j = 0; j < weights[i]; j++)
                    {
                        temp.Add(t);
                        tempW.Add(1);
                    }
                }
                else
                {
                    temp.Add(values[i]);
                    tempW.Add(weights[i]);
                }
            }

            float[] partial = temp.ToArray();
            int[] partialW = tempW.ToArray();
            return BasicKnapsack(knapCap, partialW, partial);
        }

        // необмежена к-ть предметів
        static int[] InfiniteKnapsack(int knapCap, int[] weights, int[] values)
        {
            int[] maxVals = new int[knapCap + 1];

            for (int i = 0; i <= knapCap; i++)
            {
                for (int j = 0; j < values.Length; j++)
                {
                    if (weights[j] <= i)
                    {
                        maxVals[i] = max(maxVals[i], values[j] + maxVals[i - weights[j]]);
                    }
                }
            }
            return maxVals;
        }
        static int IKresult(int[] T, int knapCap)
        {
            return T[knapCap];
        }
        static void printknapSack(int knapCap, int[] weights, int[] values, int[,] KnapRes)
        {
            int res = KnapRes[values.Length, knapCap];
            int w = knapCap;
            Console.Write("Вартості предметів, які було покладено у рюкзак: ");
            for (int i = values.Length; i > 0 && res > 0; i--)
            {
                if (res == KnapRes[i - 1, w])
                    continue;
                else
                {
                    Console.Write(values[i - 1] + " ");
                    res = res - values[i - 1];
                    w = w - weights[i - 1];
                }
            }
            Console.WriteLine(" ");
        }

        static void OutMatrix(int[,] Matrix)
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                    Console.Write("{0}\t", Matrix[i, j]);
                Console.WriteLine(" ");
            }
        }

        static void Switch()
        {
            comm = Console.ReadLine();
            Console.WriteLine(" ");
            switch (comm)
            {
                case "1":
                    try
                    {
                        Console.Write("Введіть кількість предметів: ");
                        int n = int.Parse(Console.ReadLine());

                        Console.Write("Введіть вмісткість рюкзака: ");
                        int w = int.Parse(Console.ReadLine());

                        Console.Write("Введіть ціни: ");
                        int[] val = Console.ReadLine().Split().Select(int.Parse).ToArray();

                        Console.Write("Введіть ваги: ");
                        int[] wt = Console.ReadLine().Split().Select(int.Parse).ToArray();

                        Console.Write("Введіть можливість брати предмети частично (true чи false): ");
                        bool[] part = Console.ReadLine().Split().Select(bool.Parse).ToArray();

                        int[,] BK = BasicKnapsack(w, wt, val);
                        int[] IK = InfiniteKnapsack(w, wt, val);
                        float[,] PK = PartialKnapsack(w, wt, val, part);
                        Console.WriteLine("\n\nМаксимальні значення:\nСтандартна задача: " + BKresult(BK, val, w));
                        printknapSack(w, wt, val, BK);
                        Console.WriteLine("\nНеобмежена к-ть предметів: " + IKresult(IK, w));
                        Console.WriteLine("Предмети можна брати частично: " + BKresult(PK, val, w));
                    }
                    catch
                    {
                        Console.WriteLine("Невірно введені значення");
                    }
                    break;
                case "2":
                    try
                    {
                        Random Rand = new Random();
                        Console.Write("Введіть кількість предметів: ");
                        int n = int.Parse(Console.ReadLine());

                        Console.Write("Введіть вмісткість рюкзака: ");
                        int w = int.Parse(Console.ReadLine());

                        int[] val = new int[n];
                        int[] wt = new int[n];

                        Console.Write("Введіть діапазон цін: ");
                        int d1 = int.Parse(Console.ReadLine());

                        for (int i = 0; i < n; i++)
                        {
                            val[i] = Rand.Next(d1)+1;
                        }

                        Console.Write("Введіть діапазон ваг: ");
                        int d2 = int.Parse(Console.ReadLine());
                        for (int i = 0; i < n; i++)
                        {
                            wt[i] = Rand.Next(d2)+1;
                        }

                        bool[] temp = new bool[2] { true, false };
                        bool[] part = new bool[n];

                        for (int i = 0; i < n; i++)
                        {
                            part[i] = temp[Rand.Next(2)];
                        }

                        Console.Write("Ціни: ");
                        for (int i = 0; i < n; i++)
                        {
                            Console.Write(val[i] + " ");
                        }
                        Console.Write("\nВаги: ");
                        for (int i = 0; i < n; i++)
                        {
                            Console.Write(wt[i] + " ");
                        }
                        Console.Write("\nМожливості брати частично: ");
                        for (int i = 0; i < n; i++)
                        {
                            Console.Write(part[i] + " ");
                        }

                        int[,] BK = BasicKnapsack(w, wt, val);
                        int[] IK = InfiniteKnapsack(w, wt, val);
                        float[,] PK = PartialKnapsack(w, wt, val, part);
                        Console.WriteLine("\n\nМаксимальні значення:\nСтандартна задача: " + BKresult(BK, val, w));
                        printknapSack(w, wt, val, BK);
                        Console.WriteLine("\nНеобмежена к-ть предметів: " + IKresult(IK, w));
                        Console.WriteLine("Предмети можна брати частично: " + BKresult(PK, val, w));
                    }
                    catch
                    {
                        Console.WriteLine("Невірно введені значення");
                    }
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Невизначена команда.");
                    break;
            }
        }

        static void Menu()
        {
            Console.WriteLine(" ------------------ Меню програми: ------------------");
            Console.WriteLine(" | 1. Ввести значення");
            Console.WriteLine(" | 2. Згенерувати значення");
            Console.WriteLine(" | --------------------------------------------------");
            Console.WriteLine(" | 0. Завершити роботу програми");
            do
            {
                Console.Write("Оберіть команду: ");
                Switch();
            } while (comm != "0");
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            Menu();
            Console.ReadKey();
        }
    }
}

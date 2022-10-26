using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace asd12
{
    internal class Program
    {
        static string comm;
        static int Check(int In)
        {
            bool ind = true;
            while (ind)
            {
                try
                {
                    In = int.Parse(Console.ReadLine());
                    ind = false;
                }
                catch
                {
                    Console.Write("Введено недопустиме значення, повторіть спробу: ");
                }
            }
            return In;
        }
        static void OutMatrix(double[,] m)
        {
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    if (m[i, j] == Single.MinValue)
                        Console.Write(0 + "\t");
                    else
                        Console.Write(m[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        static bool Operator(char op)
        {
            return (op == '+' || op == '*' || op == '/');
        }
        static bool Minus(char op)
        {
            return (op == '-');
        }
        static double findMax(string expr)
        {
            List<double> nums = new List<double>();
            List<char> oper = new List<char>();

            string temp = "";

            for (int i = 0; i < expr.Length; i++)
            {
                if (Operator(expr[i]))
                {
                    oper.Add(expr[i]);
                    nums.Add(double.Parse(temp));
                    temp = "";
                }
                else if (Minus(expr[i]))
                {
                    oper.Add('+');
                    nums.Add(double.Parse(temp));
                    temp = "-";
                }
                else
                {
                    temp += expr[i];
                }
            }

            nums.Add(double.Parse(temp));
            int size = nums.Count;
            double[,] maxVal = new double[size, size];
            string[,] Per = new string[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    maxVal[i, j] = Single.MinValue;
                    Per[i, j] = " ";

                    if (i == j)
                    {
                        maxVal[i, j] = nums[i];
                        Per[i, j] = nums[i].ToString();
                    }
                }
            }

            for (int i = 2; i <= size; i++)
            {
                for (int j = 0; j < size - i + 1; j++)
                {
                    int l = j + i - 1;
                    for (int k = j; k < l; k++)
                    {
                        double tempMax = 0;
                        string tempPer = "";
                        if (oper[k] == '+')
                        {
                            tempMax = maxVal[j, k] + maxVal[k + 1, l];
                            tempPer = "(" + Per[j, k] + "+" + Per[k + 1, l] + ")";
                        }
                        else if (oper[k] == '*')
                        {
                            tempMax = maxVal[j, k] * maxVal[k + 1, l];
                            tempPer = "(" + Per[j, k] + "*" + Per[k + 1, l] + ")";
                        }
                        else if (oper[k] == '-')
                        {
                            tempMax = maxVal[j, k] - maxVal[k + 1, l];
                            tempPer = "(" + Per[j, k] + "-" + Per[k + 1, l] + ")";
                        }
                        else if (oper[k] == '/' && maxVal[k + 1, l] != 0)
                        {
                            tempMax = maxVal[j, k] / maxVal[k + 1, l];
                            tempPer = "(" + Per[j, k] + "/" + Per[k + 1, l] + ")";
                        }

                        if (tempMax > maxVal[j, l])
                        {
                            maxVal[j, l] = tempMax;
                            Per[j, l] = tempPer;
                        }
                    }
                }
            }
            Console.WriteLine("Матриця проміжних значень: ");
            OutMatrix(maxVal);
            Console.WriteLine("Розташування дужок: ");
            Console.WriteLine(Per[0, size - 1]);
            return maxVal[0, size - 1];
        }

        static string GenStr(int size, int Ran)
        {
            char[] op = new char[4] {'+', '-', '*', '/'};
            string expr = "";
            Random Rand = new Random();
            for (int i = 0; i < size; i++)
            {
                if (i != size - 1)
                {
                    expr += Rand.Next(Ran).ToString();
                    expr += op[Rand.Next(4)];
                }
                else
                {
                    expr += Rand.Next(Ran).ToString();
                }
            }
            return expr;
        }

        static void Switch()
        {
            comm = Console.ReadLine();
            Console.WriteLine(" ");
            switch (comm)
            {
                case "1":
                    Console.Write("Введіть рядок: ");
                    string expression = Console.ReadLine();
                    Console.WriteLine(expression);
                    try
                    {
                        Console.WriteLine("Максимальне значення: " + findMax(expression) + "\n");
                    }
                    catch
                    {
                        Console.WriteLine("Невірно введений рядок");
                    }
                    break;
                case "2":
                    Console.Write("Введіть кількість чисел у рядку: ");
                    int size = 0;
                    size = Check(size);
                    Console.Write("Введіть діапазон чисел: ");
                    int ran = 0;
                    ran = Check(ran);
                    string expr = GenStr(size, ran);
                    Console.WriteLine("Згенерований рядок: " + expr);
                    Console.WriteLine("Максимальне значення: " + findMax(expr) + "\n");
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
            Console.WriteLine(" | 1. Ввести рядок");
            Console.WriteLine(" | 2. Згенерувати рядок");
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

            Menu();
            Console.ReadKey();
        }
    }
}

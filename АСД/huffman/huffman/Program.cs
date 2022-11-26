using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace huffman
{
    internal class Program
    {
        static string comm;
        static void Switch()
        {
            comm = Console.ReadLine();
            Console.WriteLine(" ");
            switch (comm)
            {
                case "1":
                    try
                    {
                        Console.WriteLine("Введіть строку для кодування: ");
                        string input = Console.ReadLine();
                        HuffmanTree huffmanTree = new HuffmanTree();

                        huffmanTree.Build(input);

                        BitArray encoded = huffmanTree.Code(input);

                        Console.Write("Закодована строка: ");
                        foreach (bool bit in encoded)
                        {
                            Console.Write((bit ? 1 : 0) + "");
                        }
                        Console.WriteLine();

                        string decoded = huffmanTree.Decode(encoded);

                        Console.WriteLine("Декодована строка: " + decoded);

                        Console.ReadLine();
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
            Console.WriteLine(" | 1. Алгоритм Хаффмана");
            Console.WriteLine(" | 2. Алгоритм Шеннона-Фано");
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

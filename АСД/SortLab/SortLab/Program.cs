using System;
using System.Diagnostics;

namespace SortLab
{
    class Program
    {
        static int comm;
        static void GenArr(int[] Arr, int Range)
        {
            Random Rand = new Random();
            for (int i = 0; i < Arr.Length; i++)
                Arr[i] = Rand.Next(Range);
        }

        static void GetList(LinkedList list, int[] Arr)
        {
            list.Clear();
            for (int i = 0; i < Arr.Length; i++)
                list.Add(Arr[i]);
        }

        static void GenList(LinkedList list, int Range, int Size)
        {
            Random Rand = new Random();
            list.Clear();
            for (int i = 0; i < Size; i++)
                list.Add(Rand.Next(Range));
        }

        static void OutArr(int[] Arr)
        {
            for (int i = 0; i < Arr.Length; i++)
            {
                Console.Write(Arr[i] + " ");
            }
            Console.WriteLine(" ");
        }

        static void OutList(LinkedList list)
        {
            var temp = list.head;
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write(temp.Data + " ");
                temp = temp.Next;
            }
            Console.WriteLine(" ");
        }

        static void TimeResult(Stopwatch Arrtime, Stopwatch Listtime)
        {
            Console.WriteLine(" ");
            Console.WriteLine("====================Час виконання====================");
            Console.WriteLine("Для масиву:\t" + Arrtime.Elapsed);
            Console.WriteLine("Для списку:\t" + Listtime.Elapsed);
            Console.WriteLine("=====================================================");
            Console.WriteLine(" ");
        }

        static void Switch()
        {
            comm = int.Parse(Console.ReadLine());
            Console.WriteLine(" ");
            Stopwatch AT = new Stopwatch();
            Stopwatch LT = new Stopwatch();
            switch (comm)
            {
                case 1:
                    Console.WriteLine(" ------------------ Порівняння сортування злиттям масива та списку: ------------------");
                    Console.WriteLine(" ");
                    Console.Write("Введіть розмір структур даних: ");
                    int N = int.Parse(Console.ReadLine());
                    int[] Arr = new int[N];

                    Console.Write("Введіть діапазон значень: ");
                    int Ran = int.Parse(Console.ReadLine());

                    GenArr(Arr, Ran);
                    Console.WriteLine(" ");
                    Console.WriteLine("Згенерований масив: ");
                    OutArr(Arr);

                    LinkedList list = new LinkedList();
                    GetList(list, Arr);
                    Console.WriteLine("Згенерований список: ");
                    OutList(list);
                    Console.WriteLine(" ");

                    LT.Start();
                    list = MS.MergeSort(list);
                    LT.Stop();

                    AT.Start();
                    Arr = MS.MergeSort(Arr);
                    AT.Stop();

                    Console.WriteLine("Відсортований масив: ");
                    OutArr(Arr);
                    Console.WriteLine("Відсортований список: ");
                    OutList(list);

                    TimeResult(AT, LT);

                    LT.Reset();
                    AT.Reset();
                    break;
                case 2:
                    Console.WriteLine(" ------------------ Сортування злиттям масива: ------------------");
                    Console.WriteLine(" ");
                    Console.Write("Введіть розмір масиву: ");
                    N = int.Parse(Console.ReadLine());
                    int[] nArr = new int[N];

                    Console.Write("Введіть діапазон значень: ");
                    Ran = int.Parse(Console.ReadLine());

                    GenArr(nArr, Ran);
                    Console.WriteLine(" ");
                    Console.WriteLine("Згенерований масив: ");
                    OutArr(nArr);

                    nArr = MS.MergeSort(nArr);

                    Console.WriteLine("Відсортований масив: ");
                    OutArr(nArr);
                    break;
                case 3:
                    Console.WriteLine(" ------------------ Сортування злиттям списку: ------------------");
                    Console.WriteLine(" ");
                    Console.Write("Введіть розмір структур даних: ");
                    N = int.Parse(Console.ReadLine());

                    Console.Write("Введіть діапазон значень: ");
                    Ran = int.Parse(Console.ReadLine());

                    LinkedList nlist = new LinkedList();
                    GenList(nlist, Ran, N);
                    Console.WriteLine(" ");
                    Console.WriteLine("Згенерований список: ");
                    OutList(nlist);
                    Console.WriteLine(" ");

                    nlist = MS.MergeSort(nlist);

                    Console.WriteLine("Відсортований список: ");
                    OutList(nlist);
                    break;
                case 0:
                    break;
                default:
                    Console.WriteLine("Невизначена команда, оберіть інший варіант: ");
                    break;
            }
        }

        static void Menu()
        {
            Console.WriteLine("______________________________________________________________________________________");
            Console.WriteLine(" ------------------ Меню програми: ------------------");
            Console.WriteLine(" | 1. Згенерувати однакові масив та список та порівняти час сортування");
            Console.WriteLine(" | 2. Згенерувати та відсортувати масив");
            Console.WriteLine(" | 3. Згенерувати та відсортувати список");
            Console.WriteLine(" | --------------------------------------------------");
            Console.WriteLine(" | 0. Завершити роботу програми");
            Console.WriteLine("______________________________________________________________________________________");
            do
            {
                Console.Write("Оберіть команду: ");
                Switch();
            } while (comm != 0);
        }


        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;

            Menu();
            Console.ReadKey();
        }
    }
}

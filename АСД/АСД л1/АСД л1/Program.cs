using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;


namespace АСД_л1
{
    class Program
    {
        static int comm, SV, N, Ran, ArrI, ListI;
        static int[] Mas;
        static LinkedList<int> list = new LinkedList<int>();

        static void CinSearch()
        {
            Console.Write("Введіть шукане значення: ");
            SV = int.Parse(Console.ReadLine());
        }

        static void GenArr(int[] Arr, int Range)
        {
            Random Rand = new Random();
            for (int i = 0; i < Arr.Length; i++)
                Arr[i] = Rand.Next(Range);
        }

        static void GenList(LinkedList<int> list, int Length, int Range)
        {
            Random Rand = new Random();

            for (int i = 0; i < Length; i++)
                list.AddLast(Rand.Next(Range));
        }

        static void GetList(LinkedList<int> list, int Length, int[] Arr)
        {
            Random Rand = new Random();

            for (int i = 0; i < Length; i++)
                list.AddLast(Arr[i]);
        }

        static void OutArr(int[] Arr)
        {
            for (int i = 0; i < Arr.Length; i++)
            {
                Console.Write(Arr[i] + " ");
            }
            Console.WriteLine(" ");
        }

        static void OutList(LinkedList<int> list)
        {
            foreach (int El in list)
            {
                Console.Write(El + " ");
            }
            Console.WriteLine(" ");
        }

        static void OutFoundArr(int SearchVal, int Ind)
        {
            if (Ind == -1)
            {
                Console.WriteLine("Значення \"" + SearchVal + "\" не знайдене в масиві.");
            }
            else
            {
                Console.WriteLine("Значення \"" + SearchVal + "\" знайдене в даному масиві за індексом " + Ind);
            }
        }

        static void OutFoundList(int SearchVal, int Ind)
        {
            if (Ind == -1)
            {
                Console.WriteLine("Значення \"" + SearchVal + "\" не знайдене у списку.");
            }
            else
            {
                Console.WriteLine("Значення \"" + SearchVal + "\" знайдене в даному списку за індексом " + Ind);
            }
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
            Stopwatch time = new Stopwatch();
            Stopwatch timeL = new Stopwatch();
            switch (comm)
            {
                case 1:
                    Console.Write("Введіть розмір структур даних: ");
                    N = int.Parse(Console.ReadLine());
                    Mas = new int[N];

                    Console.Write("Введіть діапазон значень: ");
                    Ran = int.Parse(Console.ReadLine());

                    GenArr(Mas, Ran);
                    Console.WriteLine(" ");
                    Console.WriteLine("Згенерований масив: ");
                    OutArr(Mas);

                    GetList(list, N, Mas);
                    Console.WriteLine(" ");
                    Console.WriteLine("Згенерований список: ");
                    OutList(list);
                    Console.WriteLine(" ");
                    break;
                case 2:
                    Console.WriteLine("--------------------Лінійний пошук--------------------");
                    Console.WriteLine(" ");

                    LinSearch LS = new LinSearch();

                    CinSearch();

                    time.Reset();
                    time.Start();
                    ArrI = LS.LinS(Mas, SV);
                    time.Stop();
                    OutFoundArr(SV, ArrI);

                    timeL.Reset();
                    timeL.Start();
                    ListI = LS.LinS(list, SV);
                    timeL.Stop();
                    OutFoundList(SV, ListI);

                    TimeResult(time, timeL);
                    break;
                case 3:
                    Console.WriteLine("--------------------Пошук з бар'єром--------------------");
                    Console.WriteLine(" ");

                    BarSearch Bar = new BarSearch();

                    CinSearch();

                    time.Reset();
                    time.Start();
                    ArrI = Bar.BarS(Mas, SV);
                    time.Stop();
                    OutFoundArr(SV, ArrI);

                    timeL.Reset();
                    timeL.Start();
                    ListI = Bar.BarS(list, SV);
                    timeL.Stop();
                    OutFoundList(SV, ListI);

                    TimeResult(time, timeL);
                    break;
                case 4:
                    Array.Sort(Mas);
                    LinkedList<int> SortList = new LinkedList<int>(list.OrderBy(i => i));
                    //OutArr(Mas);
                    //OutList(SortList);
                    Console.WriteLine("--------------------Бінарний пошук--------------------");
                    Console.WriteLine(" ");

                    BinSearch BS = new BinSearch();
                    BinSearch.divider = 2.0;

                    CinSearch();

                    time.Reset();
                    time.Start();
                    ArrI = BS.BinS(Mas, SV);
                    time.Stop();
                    OutFoundArr(SV, ArrI);

                    timeL.Reset();
                    timeL.Start();
                    ListI = BS.BinS(SortList, SV);
                    timeL.Stop();
                    OutFoundList(SV, ListI);

                    TimeResult(time, timeL);
                    break;
                case 5:
                    Array.Sort(Mas);
                    LinkedList<int> SortList1 = new LinkedList<int>(list.OrderBy(i => i));
                    //OutArr(Mas);
                    //OutList(SortList1);

                    Console.WriteLine("--------------------Бінарний пошук за золотим перетином--------------------");
                    Console.WriteLine(" ");

                    BinSearch GS = new BinSearch();
                    BinSearch.divider = (Math.Sqrt(5) + 1) / 2;

                    CinSearch();

                    time.Reset();
                    time.Start();
                    ArrI = GS.BinS(Mas, SV);
                    time.Stop();
                    OutFoundArr(SV, ArrI);

                    timeL.Reset();
                    timeL.Start();
                    ListI = GS.BinS(SortList1, SV);
                    timeL.Stop();
                    OutFoundList(SV, ListI);

                    TimeResult(time, timeL);
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
            Console.WriteLine(" | 1. Згенерувати масив та список");
            Console.WriteLine(" | 2. Лінійний пошук");
            Console.WriteLine(" | 3. Пошук з бар'єром");
            Console.WriteLine(" | 4. Бінарний пошук");
            Console.WriteLine(" | 5. Бінарний пошук, модифікований за правилом золотого перетину");
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

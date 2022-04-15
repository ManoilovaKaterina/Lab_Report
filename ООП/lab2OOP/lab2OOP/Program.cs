using System;
using System.Collections.Generic;
using System.Linq;

namespace lab1OOP
{
    class Program
    {
        static int comm;
        static int N, NEr, Ran, SmplRan, MatRan, M, Y;
        static int[] Mas, ArrUn, MasEr;
        static int[][] Mat;
        static string Str;

        static void Id()
        {
            Console.WriteLine("Манойлова Катерина Борисівна, група ІПЗ-11/1, katya.manoylova@gmail.com");
            Console.WriteLine("Варіант 13");
        }

        static void GenArr(int[] Arr, int Range)
        {
            Random Rand = new Random();
            for (int i = 0; i < Arr.Length; i++)
                Arr[i] = Rand.Next(Range);
        }

        static void OutArr(int[] Arr)
        {
            for (int i = 0; i < Arr.Length; i++)
            {
                Console.Write(Arr[i] + " ");
            }
            Console.WriteLine(" ");
        }

        static void GenMas()
        {
            Console.WriteLine("Введіть розмір масиву");
            N = int.Parse(Console.ReadLine());
            Mas = new int[N];

            Console.WriteLine("Введіть діапазон значень масиву: ");
            Ran = int.Parse(Console.ReadLine());

            GenArr(Mas, Ran);

            Console.WriteLine("Згенерований масив: ");
            OutArr(Mas);
        }

        static void SelSort(int[] Arr)
        {
            int min;
            for (int i = 0; i < Arr.Length; i++)
            {
                min = i;
                for (int j = i; j < Arr.Length; j++)
                {
                    if (Arr[j] < Arr[min])
                    {
                        min = j;
                    }
                }

                if (Arr[min] != Arr[i])
                {
                    int temp = Arr[i];
                    Arr[i] = Arr[min];
                    Arr[min] = temp;
                }
            }
        }

        static void Erat()
        {
            int ErI = 0;
            NEr = 0;
            Console.WriteLine("Введіть діапазон, до якого числа шукати прості числа: ");
            SmplRan = int.Parse(Console.ReadLine());
            bool[] table = new bool[SmplRan];
            int i, j;

            for (i = 0; i < table.Length; i++)
            {
                table[i] = true;
            }

            for (i = 2; i * i < table.Length; i++)
            {
                if (table[i])
                {
                    for (j = 2 * i; j < table.Length; j += i)
                    {
                        table[j] = false;
                    }
                }
            }

            for (i = 2; i < table.Length; i++)
            {
                if (table[i])
                {
                    NEr++;
                }
            }

            MasEr = new int[NEr];

            for (i = 2; i < table.Length; i++)
            {
                if (table[i])
                {
                    MasEr[ErI] = i;
                    ErI++;
                }
            }

            Console.WriteLine("Прості числа у заданому діапазоні: ");
        }

        static void UniteArr(int[] Arr1, int[] Arr2)
        {
            ArrUn = new int[Arr1.Length + Arr2.Length];
            for (int i = 0; i < Arr1.Length; i++)
            {
                ArrUn[i] = Arr1[i];
            }
            for (int i = 0; i < (Arr2.Length); i++)
            {
                ArrUn[i+Arr1.Length] = Arr2[i];
            }
            Console.WriteLine("Об'єднаний масив: ");
            OutArr(ArrUn);
        }

        
        static int[] DelDupl(int[] Arr)
        {
            for (int i = 0; i < Arr.Length - 1; i++)
            {
                for (int j = i + 1; j < Arr.Length; j++)
                {
                    if (Arr[i] == Arr[j])
                    {
                        List<int> tmp = new List<int>(Arr);
                        tmp.RemoveAt(j);
                        Arr = tmp.ToArray();
                    }
                }
            }
            return Arr;
        }

        static void ChangePart(int[] Arr)
        {
            int temp;
            int Size;
            if (Arr.Length % 2 == 0)
            {
                Size = (Arr.Length / 2);
            }
            else
            {
                Size = Arr.Length / 2 + 1;
            }
            for (int i = 0; i < (Arr.Length / 2); i++)
            {
                temp = Arr[i];
                Arr[i] = Arr[Size + i];
                Arr[Size + i] = temp;
            }
            Console.WriteLine("Масив, ліва та права частина якого були помінені: ");
            OutArr(Arr);
        }

        static int SevenNumb(int k)
        {
            int SevN = (int)((5 * Math.Pow(k, 2) - 3 * k)/2);
            return SevN;
        }

        static int MaxEl(int[] Arr)
        {
            int Max = int.MinValue;
            foreach (int Temp in Arr)
            {
                if (Temp > Max)
                {
                    Max = Temp;
                }
            }
            return Max;
        }

        static void FindSevNum(int[] Arr)
        {
            for (int n = 1; SevenNumb(n) <= MaxEl(Arr); n++)
            {
                for (int i = 0; i < Arr.Length; i++)
                {
                    if (Arr[i] == SevenNumb(n))
                    {
                        Console.WriteLine("Елемент " + Arr[i] + " з індексом " + i + " є семикутним числом.");
                        break;
                    }

                }
            }
        }

        static void BinS(int[] Arr)
        {
            int Range;
            if (SmplRan > Ran)
                Range = SmplRan;
            else
                Range = Ran;

            bool Ind = false;
            for (int i = 1; SevenNumb(i) <= Range; i++)
            {
                int InMas = SevenNumb(i);
                int Pos = Array.BinarySearch(Arr, InMas);
                if (Pos >= 0)
                {
                    Console.WriteLine("Семикутне число " + Arr[Pos].ToString() + " знайдено в масиві за індексом " + Pos);
                    Ind = true;
                }
            }
            if (Ind == false)
            {
                Console.WriteLine("Семикутних чисел в масиві не знайдено.");
            }
        }

        static void CinMatrInf()
        {
            Console.WriteLine("Введіть кількість років");
            Y = int.Parse(Console.ReadLine());
            Console.WriteLine("Введіть кількість розглядаємих місяців");
            M = int.Parse(Console.ReadLine());
            while (M > 12)
            {
                Console.WriteLine("Кількість місяців на може перевищувати 12");
                Console.WriteLine("Введіть кількість розглядаємих місяців");
                M = int.Parse(Console.ReadLine());
            }
            Mat = new int[M][];
            for (int i=0; i < M; i++)
            {
                Mat[i] = new int[Y];
            }

            Console.WriteLine("Введіть діапазон значень кількості опадів: ");
            MatRan = int.Parse(Console.ReadLine());
        }

        static void CreateMatrix(int[][] Matrix)
        {
            Random Rand = new Random();

            for (int i = 0; i < Matrix.Length; i++)
            {
                for (int j = 0; j < Matrix[0].Length; j++)
                {
                    Matrix[i][j] = Rand.Next(MatRan);
                }
            }
        }

        static void OutMatrix(int[][] Matrix)
        {
            for (int i = 0; i < Matrix.Length; i++)
            {
                for (int j = 0; j < Matrix[0].Length; j++)
                    Console.Write("{0}\t", Matrix[i][j]);
                Console.WriteLine();
            }
        }

        static void FindMaxP(int[][] Matrix)
        {
            int MM = 0, MY = 0;
            int Max = int.MinValue;
            for (int i = 0; i < Matrix.Length; i++)
            {
                for (int j = 0; j < Matrix[0].Length; j++)
                {
                    if (Matrix[i][j] > Max)
                    {
                        Max = Matrix[i][j];
                        MM = i;
                        MY = j;
                    }
                }
            }
            Console.WriteLine("Найбільша кількіть опадів - " + Max + " мм - випала " + (MM+1) + " місяці " + (MY+1) + " року.");
        }

        static int SumWinter(int[][] Matrix)
        {
            int sum = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < Matrix[0].Length; j++)
                {
                    sum += Matrix[i][j];
                }
            }
            if (Matrix.Length == 12)
            {
                for (int j = 0; j < Matrix[0].Length; j++)
                    sum += Matrix[11][j];
            }
            return sum;
        }

        static void FindSumWin(int[][] Matrix)
        {
            Console.WriteLine("Сумарна кількість опадів зимового періоду за всі роки = " + SumWinter(Matrix));
        }

        static int Jcount(int[][] Matrix, int J)
        {
            int sum = 0;
            for (int i = 0; i < Matrix.Length; i++)
            {
                sum += Matrix[i][J];
            }
            return sum;
        }

        static void FindCount(int[][] Matrix)
        {
            Console.WriteLine("Введіть рік, кількість опадів в якому бажаєте визначити: ");
            int CinYear = int.Parse(Console.ReadLine());
            Console.WriteLine("Кількість опадів, що випала " + CinYear + " року = " + Jcount(Matrix, CinYear-1));
        }

        static int MinYear(int[][] Matrix)
        {
            int MinYear = int.MaxValue;
            int Ynum = 0;
            for (int i = 0; i < Matrix[0].Length; i++)
            {
                if (MinYear > Jcount(Matrix, i))
                {
                    MinYear = Jcount(Matrix, i);
                    Ynum = i;
                }
            }
            return Ynum;
        }

        static void DelMinYear(int[][] Matrix)
        {
            int Num = MinYear(Matrix);
            Console.WriteLine("Значення опадів року, сумарне значення яких є найменшим: ");
            for (int i = 0; i < Matrix.Length; i++)
            {
                Console.Write(Matrix[i][Num] + " ");
                List<int> tmp = new List<int>(Matrix[i]);
                tmp.RemoveAt(Num);
                Matrix[i] = tmp.ToArray();
            }
            Console.WriteLine("\nМатриця з видаленим стовпцем: ");
        }

        static int Icount(int[][] Matrix, int I)
        {
            int sum = 0;
            for (int j = 0; j < Matrix[0].Length; j++)
            {
                sum += Matrix[I][j];
            }
            return sum;
        }

        static void SwapRows(int[][] Matrix, int I, int I2)
        {
            for (int j = 0; j < Matrix[0].Length; j++)
            {
                int temp = Matrix[I][j];
                Matrix[I][j] = Matrix[I2][j];
                Matrix[I2][j] = temp;
            }
        }

        static void SortRows(int[][] Matrix)
        {
            for (int i = 0; i < Matrix.GetLength(0) - 1; i++)
            {
                for (int j = i; j < Matrix.GetLength(0); j++)
                {
                    if (Icount(Matrix, i) < Icount(Matrix, j))
                        SwapRows(Matrix, i, j);
                }
            }
        }

        static void ReArrangeMonths(int[][] Matrix)
        {
            Console.WriteLine("Рядки в порядку спадання сумарної кількості опадів: ");
            SortRows(Matrix);
        }

        static void FindEl(int[][] Matrix, int Val)
        {
            int R = -1, C = -1;
            bool Ind = false; 
            for (int i = 0; i < Matrix.Length; i++)
            {
                C = Array.IndexOf(Matrix[i], Val);
                if (C >= 0)
                {
                    Ind = true;
                    R = i;
                    break;
                }
            }
            if (Ind)
                Console.WriteLine("Значення " + Val + " знайдено в масиві за індексами " + R + " " + C);
            else
                Console.WriteLine("Значення " + Val + " в масиві не знайдено.");
        }

        static void SearchEl(int[][] Matrix)
        {
            Console.WriteLine("Введіть значення, яке бажаєте знайти в матриці: ");
            int SearchEl = int.Parse(Console.ReadLine());
            FindEl(Matrix, SearchEl);
        }

        static void NonLinEq()
        {
            double x0, xn, xnp1, e;

            e = 0.0001;
            x0 = double.Parse(Console.ReadLine());

            while (x0 < 2.26)
            {
                Console.WriteLine("Початкове значення не дозволяє провести розрахунок");
                Console.WriteLine("Введіть можливе значення х0 (х0 >= 2.26): ");
                x0 = double.Parse(Console.ReadLine());
            }

            xn = x0 - (Math.Exp(x0) + Math.Log(x0) - 10 * x0) / (Math.Exp(x0) + 1 / x0 - 10);
            xnp1 = xn - (Math.Exp(xn) + Math.Log(xn) - 10 * xn) / (Math.Exp(xn) + 1 / xn - 10);


            while (Math.Abs(xn - xnp1) >= e)
            {
                xn = xnp1;
                xnp1 = xn - (Math.Exp(xn) + Math.Log(xn) - 10 * xn) / (Math.Exp(xn) + 1 / xn - 10);

            }

            Console.WriteLine("{0:0.00000}", xnp1);
            Console.WriteLine("{0:0.00000}", Math.Exp(xnp1) + Math.Log(xnp1) - 10 * xnp1);
        }

        static void CountNonLinEq()
        {
            Console.WriteLine("Введіть початкове значення х0: ");
            NonLinEq();
        }

        static int CountWords(string[] Str, string WordC)
        {
            int Count = 0;

            for (int i = 0; i < Str.Length; i++)
            {
                if (WordC.ToLower() == Str[i].ToLower())
                {
                    Count++;
                }
            }

            return Count;
        }

        static void SplitToWords(string Str)
        {
            string[] Words = Str.Split(new char[] { ' ', ',', ';', '.', ':' });
            string[] DistinctWords = Words.Distinct().ToArray();

            int Ind = 0, Index = 0;


            for (int i = 0; i < DistinctWords.Length; i++)
            {
                int WCount = CountWords(Words, DistinctWords[i]);
                Console.WriteLine("Слово \"" + DistinctWords[i] + "\" повторюється " + WCount + " разів.");

                if (WCount > Ind)
                {
                    Ind = WCount;
                    Index = i;
                }
            }
            Console.WriteLine("Найчастіше у введеній строці зустрічається слово \"" + Words[Index] + "\"");
        }

        static void EnterString()
        {
            Console.WriteLine("Введіть строку слів: ");
            Str = Console.ReadLine();
            SplitToWords(Str);
        }

        static void Line()
        {
            Console.WriteLine("______________________________________________________________________________________");
        }

        static void Switch()
        {
            comm = int.Parse(Console.ReadLine()); ;
            switch (comm)
            {
                case 1:
                    GenMas();
                    break;
                case 2:
                    SelSort(Mas);
                    Console.WriteLine("Відсортований масив: ");
                    OutArr(Mas);
                    break;
                case 3:
                    Erat();
                    OutArr(MasEr);
                    break;
                case 4:
                    UniteArr(Mas, MasEr);
                    break;
                case 5:
                    ArrUn = DelDupl(ArrUn);
                    Console.WriteLine("Масив з видаленими дублікатами: ");
                    OutArr(ArrUn);
                    break;
                case 6:
                    ChangePart(ArrUn);
                    break;
                case 7:
                    FindSevNum(ArrUn);
                    break;
                case 8:
                    SelSort(ArrUn);
                    Console.WriteLine("Відсортований масив: ");
                    OutArr(Mas);
                    BinS(ArrUn);
                    break;
                case 9:
                    CinMatrInf();
                    CreateMatrix(Mat);
                    OutMatrix(Mat);
                    break;
                case 10:
                    FindMaxP(Mat);
                    break;
                case 11:
                    FindSumWin(Mat);
                    break;
                case 12:
                    FindCount(Mat);
                    break;
                case 13:
                    DelMinYear(Mat);
                    OutMatrix(Mat);
                    break;
                case 14:
                    ReArrangeMonths(Mat);
                    OutMatrix(Mat);
                    break;
                case 15:
                    SearchEl(Mat);
                    break;
                case 16:
                    CountNonLinEq();
                    break;
                case 17:
                    EnterString();
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
            Line();
            Console.WriteLine(" ------------------ Меню програми: ------------------");
            Console.WriteLine(" | 1. Згенерувати одновимірний масив");
            Console.WriteLine(" | 2. Відсортувати масив методом сортування вибором");
            Console.WriteLine(" | 3. Вивести прості числа у заданому діапазоні");
            Console.WriteLine(" | 4. Об'єднати згенерований масив з масивом цілих чисел");
            Console.WriteLine(" | 5. Видалити дублікати у об'єднанному масиві");
            Console.WriteLine(" | 6. Поміняти місцями ліву та праву частину об'єднанного масиву");
            Console.WriteLine(" | 7. Знайти елементи масиву, які є семикутними числами");
            Console.WriteLine(" | 8. Знайти семикутні числа в масиві, використовуючи метод бінарного пошуку");
            Console.WriteLine(" | --------------------------------------------------");
            Console.WriteLine(" | 9. Згенерувати матрицю кількості опадів");
            Console.WriteLine(" | 10. Визначити рік та місяць, в якому випала найбільша кількість опадів");
            Console.WriteLine(" | 11. Визначити сумарну кількість опадів, що випали у зимовий період за усі роки");
            Console.WriteLine(" | 12. Визначити кількість опадів, що випала в заданий з консолі рік");
            Console.WriteLine(" | 13. Видалити роки з найменшою кількістю опадів за усі місяці");
            Console.WriteLine(" | 14. Переставити рядки матриці в порядку спадання сумарної кількості опадів за усі роки");
            Console.WriteLine(" | 15. Пошук в матриці введеного з консолі значення");
            Console.WriteLine(" | 16. Знайти корені нелінійного рівняння е^x + ln(x) - 10x = 0");
            Console.WriteLine(" | 17. Ввести рядок, визначити кількість кожного слова та слово, яке зустрічається найчастіше");
            Console.WriteLine(" | --------------------------------------------------");
            Console.WriteLine(" | 0. Завершити роботу програми");
            Line();
            do
            {
                Console.WriteLine("Оберіть команду: ");
                Switch();
            } while (comm != 0);
        }


        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;
            Id();
            Menu();
            Console.ReadKey();
        }
    }
}

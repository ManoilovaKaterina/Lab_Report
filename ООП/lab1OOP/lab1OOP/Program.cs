using System;

namespace lab1OOP
{
    class Program
    {
        static int comm, subcom, n, x;
        static void Id()
        {
            Console.WriteLine("Манойлова Катерина Борисівна, група ІПЗ-11/1, katya.manoylova@gmail.com");
            Console.WriteLine("Варіант 8");
        }

        static void Continue()
        {
            Console.WriteLine("Бажаєте закінчити даний пункт? (0 - так)");
            subcom = int.Parse(Console.ReadLine());
        }

        static void CylVol()
        {
            Console.WriteLine("Введіть радіус основи");
            int r = int.Parse(Console.ReadLine());
            Console.WriteLine("Введіть висоту циліндра");
            int h = int.Parse(Console.ReadLine());
            Console.WriteLine("Об'єм циліндру = " + (Math.PI * Math.Pow(r, 2) * h));
        }
        static void FindCylVol()
        {
            Console.WriteLine("-----Знайти об'єм циліндру за його радіусом основи та висотою------");
            do
            {
                CylVol();
                Continue();
            } while (subcom != 0);
        }

        static void Eq()
        {
            Console.WriteLine("Введіть число а");
            int a = int.Parse(Console.ReadLine());
            Console.WriteLine("Введіть число b");
            int b = int.Parse(Console.ReadLine());
            if (a == b)
            {
                Console.WriteLine("Неможливо порахувати: ділення на нуль");
            }
            else if (a < b)
            {
                Console.WriteLine("Неможливо порахувати: від'ємне число під квадратним коренем");
            }
            else
            {
                double xEQ = ((Math.Pow(Math.Sin(a), 2)) * (Math.Pow(Math.Cos(b), 2))) / Math.Sqrt(a - b) * (Math.Sin(2 * a));
                Console.WriteLine("Значення х = " + xEQ);
            }
        }
        static void SolveEq()
        {
            Console.WriteLine("-----Знайти значення рівняння (sin^2(a)*cos^2(b))/sqrt(a-b)*sin(2a) відповідно до введених a та b-----");
            do
            {
                Eq();
                Continue();
            } while (subcom != 0);
        }

        static void FuncCalc()
        {
            if (x < (-1))
            {
                Console.WriteLine("у = " + (-1 / Math.Pow(x, 2)));
            }
            else if (x > (-1) && x < 2)
            {
                Console.WriteLine("у = " + (Math.Pow(x, 2)));
            }
            else if (x > 2)
            {
                Console.WriteLine("у = " + (2 * x));
            }
            else
            {
                Console.WriteLine("Функція невизначена в даній точці.");
            }
        }

        static void FindFuncVal()
        {
            Console.WriteLine("-----Знайти значення функції у точці х-----");
            do 
            {
                Console.WriteLine("Введіть х: ");
                x = int.Parse(Console.ReadLine());
                FuncCalc();
                Continue();
            } while (subcom != 0);
        }

        static void UniSwitch()
        {
            int unicom = int.Parse(Console.ReadLine());
            switch (unicom)
            {
                case 1:
                    Console.WriteLine("Київський національний університет ім. Шевченка");
                    break;
                case 2:
                    Console.WriteLine("Київський політехнічний інститут імені Ігоря Сікорського");
                    break;
                case 3:
                    Console.WriteLine("Львівський національний університет імені Івана Франка");
                    break;
                case 4:
                    Console.WriteLine("Харківський національний університет імені В.Н. Каразіна");
                    break;
                case 5:
                    Console.WriteLine("Національний університет \"Києво - Могилянська академія\"");
                    break;
                case 0:
                    break;
                default:
                    Console.WriteLine("Невизначена команда, оберіть інший варіант: ");
                    break;
            }
        }

        static void FindUni()
        {
            Console.WriteLine("-----Вивести назву університету залежно від його консолідованого рейтингу (1 - 5)-----");
            Console.WriteLine("0 - Закінчити пункт");
            do
            {
                Console.WriteLine("Введіть рейтинг університету (1 - 5): ");
                UniSwitch();
                Continue();
            } while (subcom != 0);
        }

        static void CountSum()
        {
            double sum = 0;
            for (int k = 1; k <= 2 * n; k++)
            {
                sum += Math.Pow((-1), ((double)k + 1)) / ((double)k * Math.Pow(((double)k + 1), (1 / (double)k)));
            }
            Console.WriteLine("Сума перших " + 2 * n + " членів ряду = " + sum);
        }

        static void FindSum()
        {
            Console.WriteLine("-----Знайти суму перших 2n членів ряду Pow(-1, k + 1) / (k * Pow(k + 1, 1 / k))-----");
            do
            {
                Console.WriteLine("Введіть n");
                n = int.Parse(Console.ReadLine());
                CountSum();
                Continue();
            } while (subcom != 0);
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
                    FindCylVol();
                    break;
                case 2:
                    SolveEq();
                    break;
                case 3:
                    FindFuncVal();
                    break;
                case 4:
                    FindUni();
                    break;
                case 5:
                    FindSum();
                    break;
                case 0: break;
                default: Console.WriteLine("Невизначена команда, оберіть інший варіант: ");
                    break;
            }
        }

        static void Menu()
        {
            Line();
            Console.WriteLine(" ------------------ Меню програми: ------------------");
            Console.WriteLine(" | 1. Обчислити об'єм циліндра");
            Console.WriteLine(" | 2. Обчислити значення рівняння");
            Console.WriteLine(" | 3. Обчислити значення функції в точці х");
            Console.WriteLine(" | 4. Вивести назву університету за рейтингом");
            Console.WriteLine(" | 5. Обчислити суму перших 2n членів ряду");
            Console.WriteLine(" | 0. Завершити роботу програми");
            Line();
            do
            {
                Console.WriteLine("Оберіть команду: ");
                Switch();
            } while (comm != 0) ;
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

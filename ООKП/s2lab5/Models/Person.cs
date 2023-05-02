using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ookplab5.Models
{
    public class Person
    {
        public string Name { get; } // Ім'я
        public string Position { get; set; } // Посада
        public int RefPoint { get; set; } // Порядок людини
        public Person(string name = null, string pos = null)
        {
            Name = name;
            Position = pos;
        }
    }
    public class Logic
    {
        public Person Lev = new Person("Лев");
        public Person Mykhailo = new Person("Михайло");
        public Person Roman = new Person("Роман");
        public string SpecialResult;
        private bool? Check()
        {
            bool[] Result = new bool[2] { false, false };
            bool? Res = null;

            bool? CheckLev()
            {
                if (Lev.Position == "Бухгалтер")
                {
                    Result[0] = Roman.Position == "Начальник відділу";
                    Result[1] = Mykhailo.Position == "Касир";
                    return Result[0] && Result[1];
                }
                return null;
            }
            bool? CheckRoman()
            {
                if (Roman.Position == "Касир")
                {
                    Result[0] = Mykhailo.Position == "Начальник відділу";
                    Result[1] = Lev.Position == "Бухгалтер";
                    return Result[0] && Result[1];
                }
                else if (Roman.Position == "Начальник відділу")
                {
                    Result[0] = Mykhailo.Position == "Бухгалтер";
                    Result[1] = Lev.Position == "Касир";
                    return Result[0] && Result[1];
                }
                return null;
            }
            bool? CheckMykhailo(bool isLast = false)
            {
                if (Mykhailo.Position == "Бухгалтер")
                {
                    Result[0] = Roman.Position == "Начальник відділу";
                    Result[1] = Lev.Position == "Касир";
                    return Result[0] && Result[1];
                }
                else if(Mykhailo.Position != "Касир")
                {
                    Result[0] = Lev.Position != "Начальник відділу";
                    Result[1] = true;
                    if (Result[0] && !isLast)
                    {
                        return null;
                    }
                    return Result[0] && Result[1];
                }
                return null;
            }

            void GetOrder(int ord)
            {
                if (Lev.RefPoint == ord)
                {
                    Res = CheckLev();
                }
                else if (Mykhailo.RefPoint == ord)
                {
                    Res = CheckMykhailo();
                }
                else if (Roman.RefPoint == ord)
                {
                    Res = CheckRoman();
                }
            }

            GetOrder(1);
            if(Res == null)
                GetOrder(2);
            if (Res == null)
                GetOrder(3);
            return Res;
        }

        private void SetStartPositions(Person Start, int pos)
        {
            if (Start.Name == "Роман")
            {
                Roman.Position = Start.Position;
                Roman.RefPoint = pos;
            }
            else if (Start.Name == "Михайло")
            {
                Mykhailo.Position = Start.Position;
                Mykhailo.RefPoint = pos;
            }
            else if (Start.Name == "Лев")
            {
                Lev.Position = Start.Position;
                Lev.RefPoint = pos;
            }
        }
        private bool CheckCorrectInput(Person p1, Person p2, Person p3)
        {
            bool[] Result = new bool[2] { false, false };
            List<string> Persons = new List<string> { p1.Name, p2.Name, p3.Name };
            List<string> Pos = new List<string> { p1.Position, p2.Position, p3.Position };
            if (Pos.Contains("Касир") && Pos.Contains("Начальник відділу") && Pos.Contains("Бухгалтер"))
                Result[0] = true;
            if (Persons.Contains("Лев") && Persons.Contains("Михайло") && Persons.Contains("Роман"))
                Result[1] = true;
            return Result[0] && Result[1];
        }
        public string GetResults(Person P1, Person P2, Person P3)
        {
            if (!CheckCorrectInput(P1, P2, P3))
                return "Невірні дані";

            SetStartPositions(P1, 1);
            SetStartPositions(P2, 2);
            SetStartPositions(P3, 3);

            string result = null;
            if (Check() == true)
                result = "Вірно! " + P1.Name + " - " + P1.Position.ToLower() + ", " + P2.Name + " - " +
                    P2.Position.ToLower() + ", " + P3.Name + " - " + P3.Position.ToLower() + ".";
            else if (Check() == false)
                result = "Невірно. " + P1.Name + " - не " + P1.Position.ToLower() + ", " + P2.Name +
                    " - не " + P2.Position.ToLower() + ", " + P3.Name + " - не " + P3.Position.ToLower() + ".";
            else if (Check() == null)
                result = "Особливий випадок: неможливо визначити. Випадок: " + P1.Name + " - " + P1.Position.ToLower() + ", " + P2.Name + " - " +
                    P2.Position.ToLower() + ", " + P3.Name + " - " + P3.Position.ToLower() + ".";
            return result;
        }
    }
}
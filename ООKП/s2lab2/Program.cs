using System;

namespace ookp2
{
    interface IArea
    {
        double[] sides { get; }
        double angle { get; }
        string name { get; }
        double Area();
    }

    class TriangleArea : IArea
    {
        public double[] sides { get; }
        public double angle { get; }
        public string name { get; }
        public TriangleArea(double[] sides)
        {
            this.sides = sides;
            this.name = "трикутник";
        }
        public double Area()
        {
            double p = (sides[0] + sides[1] + sides[2]) / 2;
            double S = Math.Sqrt(p * (p - sides[0]) * (p - sides[1]) * (p - sides[2]));
            return S;
        }
    }
    class RectangleArea : IArea
    {
        public double[] sides { get; }
        public double angle { get; }
        public string name { get; }
        public RectangleArea(double[] sides, double angle)
        {
            this.sides = sides;
            this.angle = angle * (Math.PI / 180);
            this.name = "прямокутник";
        }
        public double Area()
        {
            double S = sides[0] * sides[1];
            return S;
        }
    }
    class TrapezoidArea : IArea
    {
        public double[] sides { get; }
        public double angle { get; }
        public string name { get; }
        public TrapezoidArea(double[] sides, double angle)
        {
            this.sides = sides;
            this.angle = angle * (Math.PI / 180);
            this.name = "трапеція";
        }
        public double Area()
        {
            double h = Math.Sin(angle) * sides[0];
            double S = h * (sides[1] + sides[3]) / 2;
            return S;
        }
    }
    class CircleArea : IArea
    {
        public double[] sides { get; }
        public double angle { get; }
        public string name { get; }
        public CircleArea(double[] sides)
        {
            this.sides = sides;
            this.name = "круг";
        }
        public double Area()
        {
            double S = Math.PI * Math.Pow(sides[0], 2);
            return S;
        }
    }
    class RhombusArea : IArea
    {
        public double[] sides { get; }
        public double angle { get; }
        public string name { get; }
        public RhombusArea(double[] sides, double angle)
        {
            this.sides = sides;
            this.angle = angle * (Math.PI / 180);
            this.name = "ромб";
        }
        public double Area()
        {
            double S = Math.Sin(angle) * Math.Pow(sides[0], 2);
            return S;
        }
    }
    class SquareArea : IArea
    {
        public double[] sides { get; }
        public double angle { get; }
        public string name { get; }
        public SquareArea(double[] sides, double angle)
        {
            this.sides = sides;
            this.angle = angle * (Math.PI / 180);
            this.name = "квадрат";
        }
        public double Area()
        {
            double S = Math.Pow(sides[0], 2);
            return S;
        }
    }
    class ParallelogramArea : IArea
    {
        public double[] sides { get; }
        public double angle { get; }
        public string name { get; }
        public ParallelogramArea(double[] sides, double angle)
        {
            this.sides = sides;
            this.angle = angle * (Math.PI / 180);
            this.name = "паралелограм";
        }
        public double Area()
        {
            double S = Math.Sin(angle) * sides[0] * sides[1];
            return S;
        }
    }
    abstract class Creator
    {
        public abstract IArea AreaMethod(double[] a, double b);
    }
    class TriangleCreator : Creator
    {
        public override IArea AreaMethod(double[] a, double b) { return new TriangleArea(a); }
    }
    class RectangleCreator : Creator
    {
        public override IArea AreaMethod(double[] a, double b) { return new RectangleArea(a, b); }
    }
    class TrapezoidCreator : Creator
    {
        public override IArea AreaMethod(double[] a, double b) { return new TrapezoidArea(a, b); }
    }
    class CircleCreator : Creator
    {
        public override IArea AreaMethod(double[] a, double b) { return new CircleArea(a); }
    }
    class RhombusCreator : Creator
    {
        public override IArea AreaMethod(double[] a, double b) { return new RhombusArea(a, b); }
    }
    class SquareCreator : Creator
    {
        public override IArea AreaMethod(double[] a, double b) { return new SquareArea(a, b); }
    }
    class ParallelogramCreator : Creator
    {
        public override IArea AreaMethod(double[] a, double b) { return new ParallelogramArea(a, b); }
    }

    internal class Program
    {
        static Creator ChooseCreator(double[] sides, double angle)
        {
            Creator cr = new TriangleCreator();
            if (sides.Length > 4)
            {
                Console.WriteLine("Програма не розрахована на фігури з кількістю сторін більше 4");
            }
            else if ((sides.Length == 2) || (sides.Length < 1) || (angle >= 180))
            {
                Console.WriteLine("Замкнутої фігури з даними параметрами не існує");
            }
            else if ((sides.Length==3)&&((sides[0] >= sides[1] + sides[2]) || (sides[1] >= sides[0] + sides[2]) || (sides[2] >= sides[1] + sides[0])))
            {
                Console.WriteLine("Трикутника з даними параметрами не існує");
            }
            else
            {
                if (sides.Length == 3)
                {
                   cr = new TriangleCreator();
                }
                else if (sides.Length == 1)
                {
                    cr = new CircleCreator();
                }
                else if (sides.Length == 4)
                {
                    if ((sides[0] == sides[2]) && (sides[1] == sides[3]))
                    {
                        if (sides[0] == sides[1])
                        {
                            if (angle == 90)
                                cr = new SquareCreator();
                            else
                                cr = new RhombusCreator();
                        }
                        else
                        {
                            if (angle == 90)
                                cr = new RectangleCreator();
                            else
                                cr = new ParallelogramCreator();
                        }
                    }
                    else
                    {
                        cr = new TrapezoidCreator();
                    }
                }
                return cr;
            }
            return null;
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Write("Введіть величини сторін багатокутника: ");
            double[] sides = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), Convert.ToDouble);
            double angle = 0.0;
            if (sides.Length == 4)
            {
                Console.Write("Введіть кут між першою та останньою введеною стороною: ");
                angle = double.Parse(Console.ReadLine());
            }

            Creator cr = ChooseCreator(sides, angle);
            if (cr != null)
            {
                Console.WriteLine("Фігура: " + cr.AreaMethod(sides, angle).name);
                Console.WriteLine("Площа: " + cr.AreaMethod(sides, angle).Area());
            }
        }
    }
}

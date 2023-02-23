using System;

namespace SumTestLab1
{
    internal class Program
    {
        public class Complex
        {
            public double Real { get; set; }
            public double Imaginary { get; set; }
            public Complex(double real, double imag)
            {
                Real = real;
                Imaginary = imag;
            }
            public override string ToString()
            {
                return Real.ToString() + " + " + Imaginary.ToString() + "i";
            }
            public double GetModule()
            {
                return Math.Sqrt(Math.Pow(Real, 2) + Math.Pow(Imaginary, 2));
            }
            public static Complex Add(Complex c1, Complex c2)
            {
                double sumReal = c1.Real + c2.Real;
                double sumImaginary = c1.Imaginary + c2.Imaginary;
                return new Complex(sumReal, sumImaginary);
            }
        }

        public interface IMinSum
        {
            object MinSum(object a);
        }

        public class MSInt : IMinSum
        {
            public object MinSum(object aObj)
            {
                int[] a = (int[])aObj;
                int min = int.MaxValue, secondMin = int.MaxValue;
                for (int j = 0; j < a.Length; j++)
                {
                    if (a[j] < min)
                    {
                        secondMin = min;
                        min = a[j];
                    }
                    else if (a[j] < secondMin)
                    {
                        secondMin = a[j];
                    }
                }

                return (secondMin + min);
            }
        }

        public class MSDouble : IMinSum
        {
            public object MinSum(object aObj)
            {
                double[] a = (double[])aObj;
                double min = double.MaxValue, secondMin = double.MaxValue;
                for (int j = 0; j < a.Length; j++)
                {
                    if (a[j] < min)
                    {
                        secondMin = min;
                        min = a[j];
                    }
                    else if (a[j] < secondMin)
                    {
                        secondMin = a[j];
                    }
                }

                return (secondMin + min);
            }
        }

        public class MSComp : IMinSum
        {
            public object MinSum(object aObj)
            {
                Complex[] a = (Complex[])aObj;
                var min = new Complex(double.MaxValue, double.MaxValue);
                var secondMin = new Complex(double.MaxValue, double.MaxValue);

                for (int j = 0; j < a.Length; j++)
                {
                    if (a[j].GetModule() < min.GetModule())
                    {
                        secondMin = min;
                        min = a[j];
                    }
                    else if (a[j].GetModule() < secondMin.GetModule())
                    {
                        secondMin = a[j];
                    }
                }

                return Complex.Add(secondMin, min);
            }
        }
        public class Context
        {
            private IMinSum typeContext { get; set; }
            public Context(IMinSum dataType)
            {
                typeContext = dataType;
            }
            public void ExecuteAlgorithm(dynamic a)
            {
                string res = "Найменша сума двох елементів масиву ";
                if (typeContext.GetType() == typeof(MSComp))
                    res += "комплексних";
                else if (typeContext.GetType() == typeof(MSInt))
                    res += "цілих";
                else if (typeContext.GetType() == typeof(MSDouble))
                    res += "дробових";
                res += " чисел = ";
                Console.WriteLine(res + typeContext.MinSum(a).ToString());
            }
        }

        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Default;

            int[] intArr = { 0, -1, 1, 2, -1, 3 };

            double[] doubArr = { -1.3, 0.1, 2.3, 4.4, 5.2};

            Complex[] compArr = { 
                new Complex(3, 2), 
                new Complex(5, 12), 
                new Complex(1, 14),
                new Complex(-10, 1),
                new Complex(-4, 23)
            };

            new Context(new MSInt()).ExecuteAlgorithm(intArr);
            new Context(new MSDouble()).ExecuteAlgorithm(doubArr);
            new Context(new MSComp()).ExecuteAlgorithm(compArr);
        }
    }
}
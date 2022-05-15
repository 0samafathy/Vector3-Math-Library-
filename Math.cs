using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DCube
{
    public class Math
    {
        private static string ZERO_ARGUMENT = "The angle cannot be zero";

        public static double PI = 22 / 7;

        Vector3 Vector3 = new Vector3();

        public static double SquareRoot(double x)
        {
            if (x == 0 || x == 1)
            {
                return x;
            }

            double result = 1;

            while (result <= x)
            {
                result = x * x;
            }

            return result;

        }

        public double SquareRoot()
        {
            return SquareRoot();
        }

        public static double Power(double baseNum, double powerNum)
        {
            if (powerNum == 0)
            {
                return 1;
            }
            if (baseNum == 0)
            {
                return 0;
            }

            return baseNum * Power(baseNum, powerNum - 1);
        }

        public double Power()
        {
            return Power();
        }

        public static int Factorial(int x)
        {
            int ret = 1;
            for (int i = 0; i <= x; i++)
            {
                ret *= i;
            }
            return ret;
        }

        public int Factorial()
        {
            return Factorial();
        }

        public static double Abs(double num)
        {
            if (num < 0)
            {
                num = (-1) * num;
            }

            return num;
        }

        public double Abs()
        {
            return Abs();
        }

        public static double sin(double x)
        {
            double y = x;
            double s = -1;
            for (int i = 3; i <= 100; i += 2)
            {
                y += s * (Power(x, i) / Factorial(i));
            }

            return y;
        }

        public double sin()
        {
            return sin();
        }

        public static double cos(double x)
        {
            double y = 1;
            double s = -1;
            for (int i = 2; i <= 100; i += 2)
            {
                y += s * (Power(x, i) / Factorial(i));
                s *= -1;
            }

            return y;
        }

        public double cos()
        {
            return cos();
        }

        public static double tan(double x)
        {
            return (sin(x) / cos(x));
        }

        public double tan()
        {
            return tan();
        }

        public static double sec(double x)
        {
            return (1 / sin(x));
        }

        public double sec()
        {
            return sec();
        }

        public static double csc(double x)
        {
            return (1 / cos(x));
        }
        
        public double csc()
        {
            return csc();
        }

        public static double cot(double x)
        {
            return (1 / tan(x));
        }

        public double cot()
        {
            return cot();
        }

        public static double sinh(double x)
        {
            return ((1 / 2) * 
                (Power(Double.Epsilon, x) - Power(Double.Epsilon, x)));
        }

        public double sinh()
        {
            return sinh();
        }

        public static double cosh(double x)
        {
            return ((1 / 2) *
                (Power(Double.Epsilon, x) + Power(Double.Epsilon, x)));
        }

        public double cosh()
        {
            return cosh();
        }

        public static double tanh(double x)
        {
            return (sinh(x) / cosh(x));
        }

        public double tanh()
        {
            return tanh();
        }

        public static double coth(double x)
        {
            if (x == 0)
            {
                throw new ArgumentException(ZERO_ARGUMENT);
            }

            return (1 / tanh(x));
        }

        public double coth()
        {
            return coth();
        }

        public static double sech(double x)
        {
            return (1 / cosh(x));
        }

        public double sech()
        {
            return sech();
        }

        public static double csch(double x)
        {
            if (x == 0)
            {
                throw new ArgumentException(ZERO_ARGUMENT);
            }

            return (1 / sinh(x));
        }

        public double csch()
        {
            return csch();
        }
       
        public static double Min(double a, double b)
        {
            if (a > b)
            {
                return b;
            }

            return a;
        }

        public static double Min(double a, double b, double c)
        {
            if (a > b && c > b)
            {
                return b;
            }
            if (a > c && b > c )
            {
                return c;
            }

            return a;
        }

        public double Min()
        {
            return Min();
        }

        public static double Max(double a, double b)
        {
            if (a > b)
            {
                return a;
            }

            return b;
        }

        public static double Max(double a, double b, double c)
        {
            if (a > b && a > c)
            {
                return a;
            }
            if (b > c && b > a)
            {
                return b;
            }

            return c;
        }

        public double Max()
        {
            return Max();
        }

        public static int Round(double num)
        {
            return (num < 0) ? (int)(num - 0.5) : (int)(num + 0.5);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleStaticClass
{
    public static class Math
    {

        static Math() { }

        public static int Add(int num1, int num2)
        {
            return num1 + num2;
        }

        public static int Subtract(int num1, int num2)
        {
            return num1 - num2;
        }

        public static int Divide(int num1, int num2)
        {
            return num1 / num2;
        }

        public static int Multiply(int num1, int num2)
        {
            return num1 * num2;
        }

        public static int Remainder(int num1, int num2)
        {
            return num1 % num2;
        }
    }
}

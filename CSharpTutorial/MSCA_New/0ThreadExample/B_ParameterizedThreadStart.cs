using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSCA_New._0ThreadExample
{
    class B_ParameterizedThreadStart
    {
        public static void Example1()
        {
            ParameterizedThreadStart task = new ParameterizedThreadStart((i) => Console.WriteLine(i));
            Thread thread = new Thread(task);
            thread.Start("Hello World");
            thread.Join();
            Console.WriteLine("Main Thread Resumed.");
        }

        public static void Example2()
        {
            ParameterizedThreadStart task = new ParameterizedThreadStart(Greet);
            Thread thread = new Thread(task);
            thread.Start("Hello World");
            thread.Join();
            Console.WriteLine("Main Thread Resumed.");
        }

        private static void Greet(object value)
        {
            Console.WriteLine(value);
        }
    }
}

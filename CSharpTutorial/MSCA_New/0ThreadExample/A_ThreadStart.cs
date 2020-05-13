using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSCA_New._0ThreadExample
{
    class A_ThreadStart
    {
        public static void Example1()
        {
            ThreadStart task = new ThreadStart(() => Console.WriteLine("Hello World"));
            Thread thread = new Thread(task);
            thread.Start();
        }

        public static void Example2()
        {
            ThreadStart task = Greet; //new ThreadStart(Greet)
            Thread thread = new Thread(task);
            thread.Start();
        }

        private static void Greet()
        {
            Console.WriteLine("Hello World");
        }
    }
}

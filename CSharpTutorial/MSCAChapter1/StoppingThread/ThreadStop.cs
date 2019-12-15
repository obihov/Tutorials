using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCAChapter1.StoppingThread
{
    class ThreadStop
    {
        public static void Run(int counter)
        {
            Console.WriteLine($"Child thread executing {counter}...");
        }
    }
}

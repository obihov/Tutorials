using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSCAChapter1.ThreadSleeping
{
    public static class ThreadSleepingExample
    {
        public static void Thread1()
        {
            Console.WriteLine("Thread 1");
            Thread.Sleep(5000);             //force thread1 to suspend for 5secs, allowing other threads to execute for 5secs before resuming thread1 again.
            Console.WriteLine("Thread 1");
        }

        public static void Thread2()
        {
            Console.WriteLine("Thread 2");
            Console.WriteLine("Thread 2");
            Console.WriteLine("Thread 2");
            Console.WriteLine("Thread 2");
            Console.WriteLine("Thread 2");
            Console.WriteLine("Thread 1 resuming...");
        }
    }
}

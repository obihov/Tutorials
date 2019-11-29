using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSCAChapter1.ThreadPerformance
{
    public static class ThreadPerformanceExample
    {
        /// <summary>
        /// Approach 1: Approach1_RunMultipleThreadsSimultaneously_WithSingleMethodDelegatedToEach
        /// A huge performance is gained when all threads are started before a Join statement is called.
        /// </summary>
        public static void Approach1()
        {
            //create a thread to execute one method.
            Thread thread1 = new Thread(new ThreadStart(Methods.Jump));
            thread1.Start();

            //create a thread to execute another method.
            Thread thread2 = new Thread(new ThreadStart(Methods.Drive));
            thread2.Start();

            //note both thread1 and thread2 has started and runs together. 
            //Context switching will occur automatically between thread1 and thread2
            //Manual context switching can be achieved using Thread.Sleep inside the methods for each thread.

            thread1.Join();     //pauses code execution until every methods in thread1 is completed
            thread2.Join();     //pauses code execution until every methods in thread2 is completed
        }

        /// <summary>
        /// Approach 2: Approach2_RunMultipleThreadsSynchronously_WithSingleMethodDelegatedToEach
        /// A huge performance loss when each thread is both started and joined at the same time.
        /// Approach 1 suggest a better approach to implementing threads for performance gains.
        /// </summary>
        public static void Approach2()
        {
            //create a thread to execute one method.
            Thread thread1 = new Thread(new ThreadStart(Methods.Jump));
            thread1.Start();
            thread1.Join();     //pauses code execution until every methods in thread1 is completed

            //note thread1 starts. thread2 cannot start until thread1 (i.e. Join) reports its task are completed.

            //create a thread to execute another method.
            Thread thread2 = new Thread(new ThreadStart(Methods.Drive));
            thread2.Start();
            thread2.Join();     //pauses code execution until every methods in thread2 is completed
        }

        /// <summary>
        /// Approach 3: Approach3_RunSingleThread__WithMultipleMethodsDelegatedToRunSynchronously
        /// This approach yields no performance gains at all. 
        /// Infact all it simply does is combine multiple methods to a thread that all get executed synchronously in the order that they were delegated.
        /// </summary>
        public static void Approach3()
        {
            //create a thread to execute one method.
            ThreadStart ts = delegate { };
            ts += Methods.Jump;
            ts += Methods.Drive;
            Thread thread = new Thread(ts);
            thread.Start();
            thread.Join();
        }
    }
}

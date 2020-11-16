using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public static class Listing1_1
    {
        /// <summary>
        /// Example: Create and run new thread using the Thread class, and keep application's main thread alive while new thread is running.
        /// </summary>
        public static void Example1()
        {
            //creates a background thread by default.
            Thread thread = new Thread(new ThreadStart(Method1));            
            thread.Start();

            //main thread continues.
            Console.WriteLine("Current context is Main thread.");
            Thread.Sleep(0);            

            //Not required if thread is foreground. If thread is background, then use Join to keep application alive and allow time for background thread to complete.
            //Other ways to keep application alive esp when running a background thread are: using Wait, hitting a breakpoint in debug mode or executing a synchronized task like Console.Readline
            thread.Join();
        }

        /// <summary>
        /// Example: Switching context with thread.sleep
        /// </summary>
        public static void Example2()
        {
            //Thread.Sleep alone will immediately context. If set to 0 then windows will resume back to previous thread immediately.
            //If set to 5000, then windows will resume back to previous thread after 5 seconds.
            Console.WriteLine("Current context is Main thread.");
            Thread.Sleep(0);
            Console.WriteLine("Current context is Main thread.");
            Thread.Sleep(0);
        }
               

        public static void Method1()
        {
            //new thread entered.
            Console.WriteLine("Current context is new thread.");
            Thread.Sleep(0);
            Console.WriteLine("Current context is new thread.");
            Thread.Sleep(0);
        }


        

        
    }
}

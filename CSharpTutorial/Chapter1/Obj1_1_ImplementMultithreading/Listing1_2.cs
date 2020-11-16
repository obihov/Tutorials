using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public static class Listing1_2
    {
        /// <summary>
        /// Example: Configuring thread as background or foreground
        /// </summary>
        public static void Example1()
        {
            //Main thread
            var MainThreadType = Thread.CurrentThread.IsBackground ? "Background" : "Foreground";
            Console.WriteLine("Main thread type: " + MainThreadType);

            //New thread
            Thread thread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true; //makes it a background thread.
                var NewThreadType = Thread.CurrentThread.IsBackground ? "Background" : "Foreground";
                Console.WriteLine("New thread type: " + NewThreadType);
            });
            thread.Start();
            thread.Join();  //required to keep application alive while background executes.
        }
    }
}

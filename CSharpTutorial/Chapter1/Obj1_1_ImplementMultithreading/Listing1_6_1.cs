using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public class Listing1_6_1
    {
        private static ThreadLocal<Thread> executionContext = new ThreadLocal<Thread>(() => Thread.CurrentThread);

        public static void Example()
        {
            Thread thread = new Thread(() =>
            {
                //debug to see the current culture
                Console.WriteLine("Executing child thread....");
                var culture = executionContext.Value.CurrentCulture;
                var security = executionContext.Value.ExecutionContext;
                var priority = executionContext.Value.Priority;                
            });
            thread.IsBackground = false;
            thread.Start();
            
            Console.WriteLine("Executing main thread....");
            var contextInternal2 = executionContext;
        }
    }
}

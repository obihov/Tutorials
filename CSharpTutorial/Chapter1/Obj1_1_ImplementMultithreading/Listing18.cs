

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public class Listing18
    {
        ThreadLocal<int> ThreadLocal = new ThreadLocal<int>(() => Thread.CurrentThread.ManagedThreadId);

        public void Example1()
        {
            Console.WriteLine("Press any key to close application.");
            /*
             * note, if you defined this method as public async Task SingAsync() and
             * you call it using await SingAsync OR SingAsync().GetAwaiter().GetResult() then
             * the outcome will be a synchronized process because C# compiler treats the GetResult() much the same way as the "Result"
             * which when used, would block the current thread, until the operation is finished.
             * */
            SingAsync().GetAwaiter(); 
            DanceAsync().GetAwaiter();
            
            Console.ReadKey(); //synchronizes the console to wait. You could use the ThreadLocal to locally set the current thread to run in foreground mode.
        }

        //The trick is to run each non-async mode inside of Task embedded in an Aync method.
        public async Task SingAsync()
        {
            //the below line of code will wait for 5 seconds. The await will generate a continuation code would be used later to execute Sing method once the delay is over.
            await Task.Run(() =>
            {
                Sing();
            });

            //please note, any line of code placed below await will not run until the above await is completed.
        }

        public async Task DanceAsync()
        {
            //since there is no wait or delay programmed into the Dance method, the Await will generate a continuation code that runs immediately to execute the Dance method.
            await Task.Run(() =>
            {
                Dance();
            });

            //please note, any line of code placed below await will not run until the above await is completed.
        }



        public void Sing()
        {
            Thread.Sleep(5000);
            Console.WriteLine("Singing");
        }

        public void Dance()
        {
            Console.WriteLine("Dancing");
        }

    }    
}

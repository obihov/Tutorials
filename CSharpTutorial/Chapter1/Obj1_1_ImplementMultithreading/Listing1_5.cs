using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public static class Listing1_5
    {
        [ThreadStatic]//threadStatic field are static
        private static decimal _Cash; //can uniquely store an instance of a variable for each thread using same field name.

        [ThreadStatic]//threadStatic field are static
        private static Bank _Bank; //can uniquely store an instance of a class for each thread using same field name.

        
        /// <summary>
        /// This ThreadStatic example shows how to use same field name in different threads to store values unique to the thread.
        /// </summary>
        public static void Example1()
        {
            Thread thread1 = new Thread(() =>
            {
                _Cash = 4000M;
                var interest = 0.10M;
                _Cash = _Cash + (_Cash * interest);
                Console.WriteLine("Thread1 - Cash: " + _Cash);
            });
            thread1.IsBackground = false;
            thread1.Start();

            Thread thread2 = new Thread(() =>
            {
                _Cash = 3200M;
                var interest = 0.10M;
                _Cash = _Cash + (_Cash * interest);
                Console.WriteLine("Thread2 - Cash: " + _Cash);
            });
            thread2.IsBackground = false;
            thread2.Start();

            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
            Console.WriteLine($"Main thread - Cash: {_Cash}");
        }

        /// <summary>
        /// This ThreadStatic example shows how to use same class name in different threads to store values unique to the thread.
        /// </summary>
        public static void Example2()
        {
            Thread thread1 = new Thread(() =>
            {
                _Bank = new Bank
                {
                    AccountNumber = 1001,
                    Balance = 50000M,
                    CurrentDateTime = $"{DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt")}"
                };
                Console.WriteLine($"Thread1 - Current Balance and Date {_Bank.Balance} {_Bank.CurrentDateTime}");
            });
            thread1.IsBackground = false;
            thread1.Start();

            Thread thread2 = new Thread(() =>
            {
                _Bank = new Bank
                {
                    AccountNumber = 1002,
                    Balance = 8000M,
                    CurrentDateTime = $"{DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt")}"
                };
                Console.WriteLine($"Thread1 - Current Balance and Date {_Bank.Balance} {_Bank.CurrentDateTime}");
            });
            thread2.IsBackground = false;
            thread2.Start();

            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
            _Bank = new Bank();
            Console.WriteLine($"Main thread - Current Balance and Date {_Bank.Balance} {_Bank.CurrentDateTime}");
        }        
    }

    public class Bank
    {
        public int AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string CurrentDateTime { get; set; } = "N/A";
    }
}

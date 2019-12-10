using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCAChapter1.ParameterizedThread
{
    class ParameterThreads
    {
        public static void PrintHello(object howManyTimes)
        {
            Console.WriteLine($"Value entered is {howManyTimes}.");
            Console.WriteLine($"Now converting value...");

            try
            {
                var count = int.Parse(howManyTimes.ToString());
                Console.WriteLine($"Value converted to int type correctly.");
                Console.WriteLine($"Now printing Hello message {count} times...");
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine("Hello");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}. Please supply an int type value.");
            }
        }
    }
}

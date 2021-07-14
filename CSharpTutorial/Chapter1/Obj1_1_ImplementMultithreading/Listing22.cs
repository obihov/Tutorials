using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public class Listing22
    {
        //Almost similar to the Parallel.For and Parallel.ForEach approach. The difference being:
        //The PLINQ approach (using the AsParallel PLINQ method) performs multiple query operations on multiple threads at same time.
        //while the Parallel.For and ForEach approach performs multiple iterations over on multiple threads at same time.
        public static void Example1()
        {
            var numbers = Enumerable.Range(0, 10);
            var result = numbers.AsParallel()       //converts your sequential enumerable items into a Parallel enumerable items. Any linq operation that follows will execute items in parallel rather than sequentially.
                                .Where((n) =>
                                {
                                    return n % 2 == 0;   //returns an integer list of even numbers
                                })
                                .ToList();

            result.ForEach(r => Console.WriteLine(r));
        }

        //WithDegreeOfParallelism determines the number of processors to use when executing your parallel query
        //WithExecutionMode will force your linq query to be executed in parallel. If this method is not used or when used, is set to default, then C# runtime will determine if your query should run in parallel.
        public static void Example2()
        {
            var numbers = Enumerable.Range(0, 10);
            var result = numbers.AsParallel()
                                .WithDegreeOfParallelism(2)
                                .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                                .Where((n) =>
                                {
                                    return n % 2 == 0;   //returns an integer list of even numbers
                                })
                                .ToList();

            result.ForEach(r => Console.WriteLine(r));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_Enumerables
{
    static class ListExample
    {
        static public void Run()
        {
            var list1 = new List<int> { 1, 2, 4 };
            var list2 = new List<int> { 2, 3, 5 };
            var list3 = new List<int>(new int[] { 2, 5, 7 });   //note using the curly parentheses to add enumerable items. Can even do Enumerable.Range
            var list4 = new List<int>(Enumerable.Range(4, 10));
            var list5 = new List<int>(ParallelEnumerable.Range(4, 10));
            var combinedList = list1.Zip(list2, (l1, l2) => l1 + l2);


            var list6 = Enumerable.Range(4, 10);

            list6.AsParallel<int>()                     //Creates a ParallelQuery<int>
                .ForAll(x => Console.WriteLine(x));     //Loops through each item asynchronously

            list6.AsParallel<int>()                     //Creates a ParallelQuery<int>
                .AsOrdered<int>()                       //Sorts ParallelItem and returns a ParallelQuery<int>
                .ForAll(x => Console.WriteLine(x));     //Loops through each item asynchronously

            list6.AsParallel<int>()                     //Creates a ParallelQuery<int>
                .AsOrdered<int>()                       //Sorts ParallelItem and returns a ParallelQuery<int>
                .ToList()                               //Converts to a list
                .ForEach(x => Console.WriteLine(x));    //Loops through each item synchronously

            list6.AsParallel<int>()                     //Creates a ParallelQuery<int>
                .AsOrdered<int>()                       //Sorts ParallelItem and returns a ParallelQuery<int>
                .AsSequential<int>()                    //Access parallel items synchronously and Returns a IEnumerable<int>
                .ToList()                               //Converts to a list
                .ForEach(x => Console.WriteLine(x));    //Loops through each item synchronously

            list6.AsParallel<int>()                     //Creates a ParallelQuery<int>
                .AsSequential<int>()                    //Access parallel items synchronously and Returns a IEnumerable<int>
                .ToList()                               //Converts to a list
                .ForEach(x => Console.WriteLine(x));    //Loops through each item synchronously

            var list7 = ParallelEnumerable.Range(4, 10);
            
        }
    }
}

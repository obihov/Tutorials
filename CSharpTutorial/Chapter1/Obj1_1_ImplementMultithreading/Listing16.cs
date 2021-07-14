using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public class Listing16
    {
        static List<Nation> nations = new List<Nation>
            {
                new Nation { Name = "Nigeria", Continent = "Africa" },
                new Nation { Name = "Cameroon", Continent = "Africa" },
                new Nation { Name = "Ghana", Continent = "Africa"  },
                new Nation { Name = "Togo", Continent = "Africa" },
                new Nation { Name = "Benin", Continent = "Africa" },
                new Nation { Name = "Chad", Continent = "Africa" },
                new Nation { Name = "Ivory Coast", Continent = "Africa" },
                new Nation { Name = "Senegal", Continent = "Africa" },
                new Nation { Name = "Sierra Leone", Continent = "Africa" },
                new Nation { Name = "Gambia", Continent = "Africa" },
                new Nation { Name = "Gabon", Continent = "Africa" },
                new Nation { Name = "Congo", Continent = "Africa" },
                new Nation { Name = "Kenya", Continent = "Africa" },
                new Nation { Name = "Ethopia", Continent = "Africa" },
                new Nation { Name = "Sudan", Continent = "Africa" },
                new Nation { Name = "Tunisia", Continent = "Africa" },
                new Nation { Name = "Morocco", Continent = "Africa" },
                new Nation { Name = "Algeria", Continent = "Africa" },
                new Nation { Name = "Egypt", Continent = "Africa" },
                new Nation { Name = "Niger", Continent = "Africa" },
                new Nation { Name = "Uganda", Continent = "Africa" },
                new Nation { Name = "Somalia", Continent = "Africa" },
                new Nation { Name = "Burkina Faso", Continent = "Africa" },
                new Nation { Name = "Zambia", Continent = "Africa" },
                new Nation { Name = "Zimbabwe", Continent = "Africa" },
                new Nation { Name = "Liberia", Continent = "Africa" },
                new Nation { Name = "Rwanda", Continent = "Africa" },
                new Nation { Name = "Malawi", Continent = "Africa" },
                new Nation { Name = "Angola", Continent = "Africa" },
                new Nation { Name = "Namibia", Continent = "Africa" },
                new Nation { Name = "DR Congo", Continent = "Africa" },
                new Nation { Name = "Mali", Continent = "Africa" },
                new Nation { Name = "Guinea", Continent = "Africa" },
                new Nation { Name = "Libya", Continent = "Africa" },
                new Nation { Name = "Botswana", Continent = "Africa" },
                new Nation { Name = "Guinea Bissau", Continent = "Africa" },
                new Nation { Name = "Lesotho", Continent = "Africa" },
            };

        /// <summary>
        /// Parallel class examply for asynchronous programming.
        /// Use parallel looping when you have large data-set
        /// Use parallel looping only when your code doesn't have to execute sequentially
        /// Use parallel looping only when operation in your code is atomic (i.e. not involving read/write access to shared resources). 
        /// Can avoid issues by using a data that can block/synchronize its access when requested by multiple threads.
        /// </summary>
        public static void Example1()
        {
            Console.WriteLine("Using Parallel.For to asynchronously display nations.");

            Parallel.For(0, nations.Count, (n) => 
            {
                Console.WriteLine(nations[n].Name);
            });
        }

        public static void Example2()
        {
            Console.WriteLine("Using Parallel.ForEach to asynchronously display nations.");

            Parallel.ForEach(nations, (n) =>
            {
                Console.WriteLine(n.Name);
            });
        }

        public static void Example3()
        {
            var numbers = Enumerable.Range(0, 10).ToList();
            Parallel.For(0, numbers.Count, (i) => Console.WriteLine(nations[i].Name));
            /*
             * Using numbers.Count only works if collection is a List. An Enumerable type won't work unless it's converted to a ToList or ToArray
             * If ToList, then numbers.Count
             * If ToArray, then numbers.Length
             * */
        }

        public static void Example4()
        {
            var numberList = Enumerable.Empty<Nation>().ToList();
            numberList.Add(new Nation { Name = "USA", Continent = "America" });
        }
    }

    public class Nation
    {
        public string Name { get; set; }
        public string Continent { get; set; }
    }
}

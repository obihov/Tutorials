using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_NETInterfaces
{
    #region Tutorial
    /*
     * The IComparable interface can be used to implement sort mechanism.
     * The sort method comes with several overload so you may need to learn about the IComparer and Comparison interfaces for some advanced sorting.
     * For a simple sort, you need to invoke the Sort() method. 
     * This method requires that the class you are sorting on must have implemented the IComparable interface and have defined the CompareTo method in it.
     * */
    #endregion
    internal class SortingExample
    {
        public static void Run()
        {
            SortExample();
        }

        private static void SortExample()
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee
                {
                    EmployeeID = 1011,
                    EmployeeFirstName = "Obinna",
                    EmployeeLastName = "Mogbogu"
                },
                new Employee
                {
                    EmployeeID = 1001,
                    EmployeeFirstName = "Kinis",
                    EmployeeLastName = "Netonda"
                },new Employee
                {
                    EmployeeID = 1021,
                    EmployeeFirstName = "Francis",
                    EmployeeLastName = "Hala"
                }
            };

            Console.WriteLine();
            Console.WriteLine("Before Sort.");
            employees.ForEach(e =>
            {
                Console.WriteLine($"ID:\t{e.EmployeeID}");
                Console.WriteLine($"Name:\t{e.EmployeeFirstName} {e.EmployeeLastName}");
                Console.WriteLine();
            });

            Console.WriteLine();
            Console.WriteLine("After Sort.");
            employees.Sort();                   //The Sort will make use of the CompareTo method that you implemented. Will throw error if none exist in your class.
            employees.ForEach(e =>
            {
                Console.WriteLine($"ID:\t{e.EmployeeID}");
                Console.WriteLine($"Name:\t{e.EmployeeFirstName} {e.EmployeeLastName}");
                Console.WriteLine();
            });
        }

        //To compare you need the IComparable interface, values returned are -1, 0, 1
        //To sort a collection you also need the IComprable interface.
        private class Employee : IComparable
        {
            internal int EmployeeID { get; set; }
            internal string EmployeeFirstName { get; set; }
            internal string EmployeeLastName { get; set; }

            public int CompareTo(object obj)
            {
                Employee employee2 = obj as Employee;
                if(employee2 is null)
                {
                    return 1;
                }
                return this.EmployeeID.CompareTo(employee2.EmployeeID);
            }
        }
    }

    
}

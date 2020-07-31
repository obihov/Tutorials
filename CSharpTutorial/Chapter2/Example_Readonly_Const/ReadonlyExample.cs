using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_Readonly_Const
{
    #region Tutorial
    /*
     * Readonly allows you set a value to a field once. 
     * Can set value after declaration or at runtime using constructor.
     * Readonly same as const, but const don't allow to set value at runtime. 
     * Const also ensures you don't make mistake to assign value to variable more than once at compile time.
     * */
    #endregion
    
    static public class ReadonlyExample
    {
        static public void Run()
        {
            ReadonlyEx readonlyEx = new ReadonlyEx(10);
            readonlyEx.SetAge(newAge: 20);
        }
    }

    public class ReadonlyEx
    {
        readonly private int Age;
        readonly public string Name = "Obi";    //declare and initialize to set at compile time

        private ReadonlyEx() { }

        public ReadonlyEx(int age)         //use constructor to set at runtime
        {
            this.Age = age;
        }

        //With readonly, you cannot reassign value after value has being assigned.
        public void SetAge(int newAge)
        {
            //this.Age = newAge;
            Console.WriteLine("Cannot reset a read-only value.");
        }
    }
}

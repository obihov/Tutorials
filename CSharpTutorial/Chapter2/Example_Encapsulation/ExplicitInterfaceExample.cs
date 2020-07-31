using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_Encapsulation
{
    static class ExplicitInterfaceExample
    {
        static public void Run()
        {
            ExplicitInterfaceClass @explicit = new ExplicitInterfaceClass();
            @explicit.FirstName = "obi";
            @explicit.MethodB();

            IExplicitInterface @explicit2 = new ExplicitInterfaceClass();
            @explicit2.Age = 10;
            @explicit2.FirstName = "Paul";
            @explicit2.MethodA();
            @explicit2.MethodB();

            
        }
    }

    internal interface IExplicitInterface
    {
        //Note: interface cannot have fields.
        int Age{get; set;}
        string FirstName { get; set; }


        void MethodA();
        void MethodB();
    }

    internal class ExplicitInterfaceClass : IExplicitInterface
    {
        //using explicit interface requires no access modifier. by default method is private. To see methods, must declare a type as Interface.

        int IExplicitInterface.Age { get; set; }        //explicit interface to achieve encapsulation
        public string FirstName { get; set; }           //access modifier - public - to achieve encapsulation

        //explicit interface to achieve encapsulation
        void IExplicitInterface.MethodA()
        {
            Console.WriteLine("MethodA");
        }

        //access modifier - public - to achieve encapsulation
        public void MethodB()
        {
            Console.WriteLine("MethodB");
        }
    }
}

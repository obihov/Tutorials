using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_Generic
{
    static class GenericInterfaceExample
    {
        #region Tutorial
        /*
         * Generic allows you to write a general code for all kinds of types.
         * Generic classes can be constrained to work with struct, class, a class with a default ctor, interface, baseClass, delegate (a class in its own way).
         * Always try to constraint your Generic classes to limit other types (not designed for the generics).
         * Generics helps you to maximize code reuse, type safety, and performance.
         * */

        /*
         * Advantages of Generics
         * ======================
         * 
         * Generics eliminates boxing and unboxing.
         * Generic collection types generally perform better for storing and manipulating value types because there is no need to box the value types.
         * Generics provide type safety without the overhead of multiple implementations.
         * There is no need to write code to test for the correct data type because it is enforced at compile time. The need for type casting and the possibility of run-time errors are reduced.
         * By providing strong typing, a class built from a generic lets visual studio provide IntelliSense. So instead of var x = new SomeClassA, 
         * Generic delegates enable type-safe callbacks without the need to create multiple delegate classes.
         * */
        #endregion

        static public void Run()
        {
            //Can choose to directly use the implemented DisplayAge method
            ISomeInterface someInterface = new SomeClassA(10);
            someInterface.DisplayAge();

            //or Can choose to use the generic PrintAge method, which offers a generalized way to display ages for any type that implements ISomeInterface
            GenericInterfaceEx<ISomeInterface> interfaceEx = new GenericInterfaceEx<ISomeInterface>();
            interfaceEx.PrintAge(new SomeClassA(10));
            interfaceEx.PrintAge(new SomeClassB(10));
        }
    }

    //Where T is a class or struct type that implements the interface ISomeInterface
    public class GenericInterfaceEx<T> where T : ISomeInterface
    {
        public void PrintAge(T instance)
        {
            //all instance must implement T i.e. ISomeInterface. Must also implement the method PrintAge
            instance.DisplayAge();
        }
    }

    public interface ISomeInterface
    {
        void DisplayAge();
    }

    public class SomeClassA : ISomeInterface
    {
        public int Age { get; set; }

        public SomeClassA(int age)
        {
            this.Age = age;
        }

        public void DisplayAge()
        {
            Console.WriteLine($"Age is {Age}");
        }
    }

    public class SomeClassB : ISomeInterface
    {
        public int Age { get; set; }

        public SomeClassB(int age)
        {
            this.Age = age;
        }

        public void DisplayAge()
        {
            Console.WriteLine($"Age is {Age}");
        }
    }
}

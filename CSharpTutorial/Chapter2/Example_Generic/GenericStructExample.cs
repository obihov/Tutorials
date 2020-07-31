using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_Generic
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

    static class GenericStructExample
    {
        static public void Run()
        {
            GenericStructEx<int> nullableInt = new GenericStructEx<int>(10);
            var intValue = nullableInt.Value;
            Console.WriteLine(intValue);
            Console.WriteLine();

            GenericStructEx<double> nullableDouble = new GenericStructEx<double>(10D);
            var doubleValue = nullableDouble.Value;
            Console.WriteLine(doubleValue);
            Console.WriteLine();

            GenericStructEx<bool> nullableBoolean = new GenericStructEx<bool>(false);
            var boolValue = nullableBoolean.Value;
            Console.WriteLine(boolValue);
            Console.WriteLine();

            GenericStructEx<int> nullable = new GenericStructEx<int>(10);
            var nullableValue = nullable.Value;
            Console.WriteLine(nullableValue);
            Console.WriteLine();
        }
    }

    //Where T is a value type (i.e. struct eg. byte, short, int, long, double, float, decimal, bool, also uint, sbyte etc)
    public class GenericStructEx<T> where T : struct
    {
        private bool _hasValue { get; set; }
        private T? _value { get; set; }

        public GenericStructEx(T? value)
        {
            this._hasValue = (value is null) ? false : true;
            this._value = value;
        }

        public bool HasValue
        {
            get
            {                
                return _hasValue;
            }
        }

        public T? Value
        {
            get
            {
                return _value;
            }
        }
    }
}

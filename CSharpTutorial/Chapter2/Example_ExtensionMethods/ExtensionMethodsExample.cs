using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_ExtensionMethods
{
    #region Tutorial
    /*
     * You can use extension methods to extend class, interface, structs.
     * But all extensions you declare, must be declared in a non-generic, non-nested, static class.
     * So with extension methods, you can extend the capabilities of a class, interface and struct.
     * You will see extensions used a lot in LINQ methods defined in a static class called Enumerable.
     * In Enumerable class, we have LINQ methods like select, where, etc.
     * In each LINQ method, the interface IEnumerable has been extended to support these LINQ methods. 
     * Note the "this IEnumerable<TSource> source" in each LINQ methods defined in the Enumerable static class.
     * So this allows you to be able to do "new List<int> {1, 2, 3}.Select()", where List is a IEnumerable type.
     * */
    #endregion

    static class ExtensionMethodsExample
    {
        static public void Run()
        {
            IExtend interfaceExtend = new SomeClass();
            ClassExtend classExtend = new ClassExtend();
            StructExtend structExtend = new StructExtend(10);

            //You can use extension methods to extend class, interface, structs.
            //But all extensions you declare, must be declared in a non-generic, non-nested, static class.
            interfaceExtend.ExtensionMethod();
            classExtend.ExtensionMethod();
            structExtend.ExtensionMethod();
        }
    }

    public class ExtensionMethodsEx
    {
        public ExtensionMethodsEx(){ }
    }

    //for a class, extension methods must be declared in a class that is non-generic, non-nested, and static.    
    static public class ExtensionClass
    {
        //overloaded to extend an interface type
        static public void ExtensionMethod(this IExtend exInterface)
        { 
            Console.WriteLine("Method extended for interface.");
        }

        //overloaded to extend a class type
        static public void ExtensionMethod(this ClassExtend exClass)
        {
            Console.WriteLine("Method extended for class.");
        }

        //overloaded to extend a struct type
        static public void ExtensionMethod(this StructExtend exStruct)
        {
            Console.WriteLine("Method extended for struct.");
        }
    }

    //You can use extension methods to extend class, interface, structs.
    //But all extensions you declare, must be declared in a non-generic, non-nested, static class.
    public interface IExtend
    {

    }
    public class SomeClass : IExtend
    {

    }

    //You can use extension methods to extend class, interface, structs.
    //But all extensions you declare, must be declared in a non-generic, non-nested, static class.
    public class ClassExtend
    {

    }

    //You can use extension methods to extend class, interface, structs.
    //But all extensions you declare, must be declared in a non-generic, non-nested, static class.
    public struct StructExtend
    {
        public StructExtend(int age)
        {

        }
    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_TypeConversion
{
    /*
    * NOTE:
    * ====
    * Use is / as on only reference or nullable types. Do not use with struct
    * */

    static internal class ValidateTypeConversionExample2
    {
        static internal void Run()
        {
            //Implicit
            IInterfaceA @interface = new ImplementedClass();    //Note @interface at runtime will resolve to the ImplementedClass class.
            @interface.Hello();

            //Explicit
            ImplementedClass @implementedClass = (ImplementedClass)@interface;  //Note @interface is secretly a ImplementedClass type, see above where a ImplementedClass was instantiated.
            @implementedClass.Hello();

            //is - validate or check type, return true or false
            //Note: @interface is secretly a ImplementedClass type, see above where a ImplementedClass was instantiated.
            if (@interface is IInterfaceA)
                Console.WriteLine("The interface intance can be directly assigned to InterfaceA.");
            if (@interface is ImplementedClass)
                Console.WriteLine("The interface intancecan can be explicitly casted and assigned to ImplementedClass.");
            if (@implementedClass is IInterfaceA)
                Console.WriteLine("The class intance can be implicited assigned to InterfaceA.");
            if (implementedClass is ImplementedClass)
                Console.WriteLine("The class intance can be directly assigned to ImplementedClass.");


            //as - validate or check type, return value or null
            IInterfaceA @interface2 = @interface as IInterfaceA;    //as retunrs IInterfaceA which can be assigned to type IInterfaceA directly.
            interface2.Hello();
            IInterfaceA @interface3 = @interface as ImplementedClass; //as returns ImplementedClass, which implicitly converts to IInterfaceA
            @interface3.Hello();
            ImplementedClass @implementedClass2 = (ImplementedClass)(@implementedClass as IInterfaceA); //as returns type IInterfaceA and must be explicitly casted to return ImplementedClass
            @implementedClass2.Hello();
            ImplementedClass @implementedClass3 = @implementedClass as ImplementedClass;    //as returns ImplementedClass which can be assigned to type ImplementedClass directly.
            @implementedClass3.Hello();
        }
    }

    internal interface IInterfaceA
    {
        void Hello();
    }

    internal class ImplementedClass : IInterfaceA
    {
        public void Hello()
        {
            Console.WriteLine("Greetings....");
        }
    }
}

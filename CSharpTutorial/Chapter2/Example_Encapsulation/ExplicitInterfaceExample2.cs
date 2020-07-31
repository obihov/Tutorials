using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_Encapsulation
{
    #region Tutorial
    /*
     * Explicit interface gives you a means to encapsulate certain aspect of your code.
     * Only instance types defined as the interface can access certain aspect of your code.
     * So if we have
     * SomeClass : SomeInterface { public void SomeInterface.Jump() {} public void Run() {} }
     * Then,
     * SomeClass sc = new SomeClass()
     * sc.Jump() //will not work because of the explicit interface.
     * sc.Run() //will work, however.
     * 
     * But you can do instead
     * SomeInterface iSc = (SomeInterface)sc OR (sc as SomeInterface) to convert sc to type SomeInterface.
     * isc.Jump() //will work
     * isc.Run() //will not work, you need your type as SomeClass for this to work.
     * 
     * So simply put, only when users use the exact interface, will they be able to see the Jump method.
     * 
     * Also, explicit interface can be used to help you with situations where two or more interface have the same method names.
     * If your class implements two interface that both have the same method name and signature, C# will compile but C# will be left to assume which Interface method you really want to use at runtime.
     * So what to do now? Well use explicit interface to uniquely implement each of the methods. The example in this class shows you how.
     * */
    #endregion
    internal static class ExplicitInterfaceExample2
    {

        //because class is internal, you public Run method will be treated as internal.
        static public void Run()
        {
            SomeExplicitClassExample sc = new SomeExplicitClassExample();
            sc.Walk();
            //sc.Jump() wont work

            IInterfaceA sc2 = new SomeExplicitClassExample();
            sc2.Jump();
            //sc2.Walk() wont work

            IInterfaceA sc3 = (sc as IInterfaceA);  //through casting, you can do new SomeExplicitClassExample() or even (IInterfaceA)sc
            sc3.Jump();
            //sc3.Walk() wont work
        }

        
    }
    
    public class SomeExplicitClassExample : IInterfaceA, IInterfaceB
    {
        //public void Jump() will point to 2 references. 
        //But what you need exactly is 1 reference which points to Jump version in IInterfaceA and Jump version in IInterfaceB.
        //As shown below, using explicit methods, will allow you achieve just that.


        //Seen when you do IInterfaceA sc =  new SomeExplicitClassExample() OR Cast an instance of SomeExplicitClassExample to type IInterfaceA
        void IInterfaceA.Jump()
        {
            
        }

        //Seen when you do IInterfaceB sc =  new SomeExplicitClassExample() OR Cast an instance of SomeExplicitClassExample to type IInterfaceB
        void IInterfaceB.Jump()
        {

        }

        //Only seen when you do SomeExplicitClassExample sc = new SomeExplicitClassExample()
        public void Walk()
        {

        }
    }

    public interface IInterfaceA
    {
        void Jump();
    }

    public interface IInterfaceB
    {
        void Jump();
    }
}

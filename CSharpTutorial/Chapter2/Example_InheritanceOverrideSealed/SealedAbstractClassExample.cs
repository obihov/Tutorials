using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_InheritanceOverrideSealed
{
    #region Tutorial
    /*
     * Sealed base class - can be instantiated. 
     * Sealed class can inherit from other base/abstract types.
     * Sealed class cannot be inherited by any class type (even if it's a nested class).
     * Sealed class cannot have a virtual/abstract members because it cannot be derived from.
     * Sealed class can have both static/instance/override members.
     * Sealed class can have a constructor, since it can be instantiated.
     * In any class type, you cannot have a "sealed member" or "sealed virtual member" or "sealed abstract member". 
     * You can however have a "sealed override member". This member won't be derivable by other types.
     * */
    #endregion

    internal class SealedAbstractClassExample
    {
        public static void Run()
        {
            SealedBaseClass s1 = new SealedBaseClass();
            s1.Jump();      //abstract method in base. Overridden in derived. Reusable by any derived type.
            s1.Dance();      //virtual method in base. Overridden in derived. Reusable by any derived type.
            s1.Talk();      //instance method in base. Reusable by any derived type.
            SealedBaseClass.Look();
            s1.Sleep();     //sleep was only defined in abstract class, but through inheritance we can have the s1 instance reuse the method from the base. Also this applies to any abstract/virtual/instance/override methods defined in the base.

            AbstractBaseClass s2 = new SealedBaseClass();            
            s2.Jump();      //abstract method in base. Overridden in derived. Reusable by any derived type.
            s2.Dance();     //virtual method in base. Overridden in derived. Reusable by any derived type.
            s2.Sleep();     //instance method in base. Reusable by any derived type.
        }
    }


    #region Tutorial
    /*
     * A good practice to always mark your class as Sealed. Helps ensure that no class unknownigly derives from your type.
     * A sealed class cannot be derived. It can however derive from other class.
     * All members (properties, methods, fields, events etc.) cannot be derived.
     * static, instance, override members are allowed in sealed class.
     * virtual, abstract members are not allowed in sealed class.
     * */
    #endregion
    internal sealed class SealedBaseClass : AbstractBaseClass
    {
        public override void Jump()
        {
            throw new NotImplementedException();
        }

        //enforcing that a override method is never overriden by a derived type. 
        sealed public override void Dance()
        {
            base.Dance();
        }

        //instance members (like method, properties) are allowed in sealed class because you can instantiate a sealed class.
        public void Talk()
        {
            Console.WriteLine("Sealed instance call.");
        }

        //static members (like method, properties) are allowed in sealed class.
        public static void Look()
        {
            Console.WriteLine("Sealed static call.");
        }

        //Virtual methods is not allowed in a sealed class because sealed class cannot be derived (ie. overridden)
        //public virtual void Walk() { }

        //you cannot have a "sealed member" or "sealed virtual member" or "sealed abstract member" in any class type.
        //Sealed is always reserved for override members only.
        //sealed public void MyMethod2() { Console.WriteLine(); }
        //sealed virtual public void MyMethod2() { Console.WriteLine(); }
        //sealed abstract public void MyMethod3();
    }

    internal abstract class AbstractBaseClass
    {
        //A abstract method is allowed in a abstract class.
        //like interface, abstract methods don't have implementations. Unlike interface, you must define abstract methods as public/internal/protected.
        public abstract void Jump();

        //A virtual method is allowed in a abstract class. Even without overriding them, an instance of a derived type can always reuse them.
        public virtual void Dance() => Console.WriteLine("Abstract call.");

        //Although abstract class cannot be instantiated. Yet an instance method is allowed. 
        //You will need an actual concrete instance of a derived class to access/reuse this method.
        public void Sleep() => Console.WriteLine("Abstract call.");

        //you cannot have a "sealed member" or "sealed virtual member" or "sealed abstract member" in any class type.
        //Sealed is always reserved for override members only.
        //sealed public void MyMethod2() { Console.WriteLine(); }
        //sealed virtual public void MyMethod2() { Console.WriteLine(); }
        //sealed abstract public void MyMethod3();

    }

    //You cannot derive a sealed class
    //internal class Derived : SealedBaseClass { }

    internal class SomeBaseClass
    {
        public void MyMethod()
        {
            Console.WriteLine("Base method");
        }

        //you cannot have a "sealed member" or "sealed virtual member" or "sealed abstract member" in any class type.  
        //Sealed is always reserved for override members only.
        //sealed public void MyMethod2() { Console.WriteLine(); }
        //sealed virtual public void MyMethod2() { Console.WriteLine(); }
        //sealed abstract public void MyMethod3();
    }
}

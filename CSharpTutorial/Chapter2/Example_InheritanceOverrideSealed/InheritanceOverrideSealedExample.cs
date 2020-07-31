using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_InheritanceOverrideSealed
{
    static class InheritanceOverrideSealedExample
    {
        //Lesson 1 teaches you about working with virtual methods. Also sealed methods and concept called method hiding using the 'new' keyword.
        //Lesson 2 teaches you about working with sealed classes.
        //Lesson 3 teaches you about working with abstract classes and abstract members (properties and methods only). 

        /*
         * Fields cannot be marked as abstract.
         * Abstract methods cannot be marked along as virtual or override and applies vice versa as well.
         * Abstract classes can inherit from another Abstract class much the same way an interface can implement another interface.
         */

        static public void Run() { Console.WriteLine("Read all the tutorials. For more visit MCSA text Chapter 2.1 objective."); }
    }

    class InheritanceOverrideSealedEx
    {

    }

    #region Lesson 1
    
    class SomeParent
    {
        #region Tutorial
        /*
         * A virtual method can be inhertied from by child classes. They cannot be private.
         * An abstract method (which is defined in abstract classes) is same as an interface method, they both only provide the method definition NOT implementation.
         * */
        #endregion
        public virtual void MethodFromParent() { }

        public void MethodFromParent2() { }
    }

    class SomeChild : SomeParent
    {
        #region Tutorial
        /*
         * Sealed must be applied only to override methods. Cannot work on ordinary methods including virtual methods.
         * The work of a sealed method is to prevent an overriden method from further being overriden by child methods.
         * */
        #endregion
        public sealed override void MethodFromParent() { }

        #region Tutorial
        //Below will not work because you cannot override a base member that's not marked as virtual, abstract, or override.
        #endregion
        //public override void MethodFromParent2() { }

        #region Tutorial
        /*
         * If a base method is defined as virtual or override, simply override it in a child class.
         * If a base mehtod is not a virtual or override method, then you must use the "new" keyword so that C# knows to use the child's version (not the parent's) when working off an instance of the child.
         * This is because without the new keyword, C# will see that your child's method version hides the parent's method version.
         * NOTE: Not using the new keyword will not result to a compile/runtime time error in your code, however undesirable behavior may be experienced. So best to use it as necessary.
         * */
        #endregion
        new public void MethodFromParent2() { base.MethodFromParent2(); }
    }

    class SomeChild2 : SomeChild
    {
        #region Tutorial
        /*
         * MethodFromParent cannot be inherited from since it has been sealed in SomeChild class.
         * */
        #endregion
        //public override void MethodFromParent() { }
    }

    #endregion

    #region Lesson 2

    #region Tutorial
    /*
     * A sealed class cannot be inherited from.
     * A sealed class can inherit from other class and can be instantiated like ordinary classes.
     * They can contain static, non-static, sealed, and overriden methods.
     * They cannot contain virtual methods since they are not inheritable.
     * */
    #endregion
    sealed class SealedParent
    {
    }

    #endregion

    #region Lesson 3
    
    abstract class SomeAbstractClass
    {
        #region Tutorial
        /*
         * Can have an abstract property but not an abstract field.
         * Can have an abstract method, regular method, and virtual method in an abstract class.
         * */
        #endregion
        public abstract int AbstractProperty { get; set; }
        public abstract void AbstractMethod();
        public void RegularMethod() { }
        public virtual void VirtualMethod() { }
    }

    #region Tutorial
    /*
     * Abstract properties and methods defined in abstract class must be overriden and implemented in derived class.
     * Feel free to define regular or virtual methods also in an abstract class if you want to.
     * */
    #endregion
    class SomeDerivedClass : SomeAbstractClass
    {
        public override int AbstractProperty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AbstractMethod()
        {
            throw new NotImplementedException();
        }

        //using the new keyword hides the base's version.
        new public void RegularMethod() { }

        public override void VirtualMethod()
        {
            base.VirtualMethod();
        }
    }

    #endregion

}

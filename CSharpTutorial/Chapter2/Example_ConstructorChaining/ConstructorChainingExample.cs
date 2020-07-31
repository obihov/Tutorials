using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_ConstructorChaining
{
    static class ConstructorChainingExample
    {
        static public void Run()
        {
            //Order of execution: SomeClass(string) -> SomeClass(int) -> SomeClass()
            SomeClass someClass = new SomeClass();
        }
    }

    #region Tutorial
    /*
     * A class can have multiple constructor overloads.
     * Each constructor can trigger another within the class. This is called constructor chaining.
     * To trigger another constructor in a class, do Ctor() : this(value)
     * Order of exectuion is AnotherCtor(value) executes, then Ctor() executes.
     * A similar kind of approach is used to explicitly trigger a Parent ctor when inheritance involved.
     * Just note, in inheritance, C# automatically chains the parent ctor to every of your child ctor unless explicitly told not to using the chaining technique.
     * In an inheritance situation do this to explictly chain child ctor with a parent ctor.
     * ChildCtor() : base()
     * In Java, instead of base, it is super().
     * Learn more about ctor chaining in inheritance situation by looking at the Example_ConstructorInheritance folder.
     * */
    #endregion


    public class SomeClass
    {
        public SomeClass() : this(10)
        {
            Console.WriteLine($"Default ctor");
        }

        public SomeClass(int k) : this("Obi")
        {
            Console.WriteLine($"Called by another ctor - {k}");
        }

        private SomeClass(string n)
        {
            Console.WriteLine($"Private ctor called by a public ctor - {n}");
        }
    }

    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_ConstructorInheritance
{
    static class ConstructorInheritanceExample
    {
        static public void Run()
        {
            SomeChildClass someChild1 = new SomeChildClass();
            Console.WriteLine("====================================");
            SomeChildClass someChild2 = new SomeChildClass(10, 20);
            Console.WriteLine("====================================");
            SomeChildClass someChild3 = new SomeChildClass(10);
        }
    }

    public class SomeChildClass : SomeParentClass
    {
        #region Tutorial
        /*
         * Whenever you inherit a class, then automatically whenever you attempt to create a new instance of Child,
         * it will automatically reference the parametereless (or empty) parent ctor.
         * So either of the below child instances will automatically reference the empty parent ctor 
         * unless you explicitly use :base next to the child ctor to reference another ctor in the parent class.
         * new SomeChildClass()         ----> references ----> new SomeParentClass
         * new SomeChildClass(10, 20)   ----> references ----> new SomeParentClass
         * 
         * To reference another ctor in the parent, a :base keyword must be prefixed next to a child's ctor.
         * Note, a matching ctor parameter definition must be available in parent class. 
         * So ctor(int i) : base(i) where i is of type int in parent.
         * */
        #endregion

        /* Child ctor calls matching Base ctor.
         * Note call to an empty base ctor is only possible, if an empty base ctor was defined
         * */
        public SomeChildClass() : base()
        {
            Console.WriteLine("Child default Ctor");
        }

        /* Child ctor calls matching Base ctor.
         * Note call to an empty base ctor is only possible, if an empty base ctor was defined
         * */
        public SomeChildClass(int i) : base(i)
        {
            Console.WriteLine($"Child explicit Ctor - {i}");
        }

        /* Child ctor calls non-matching Base ctor.
         * Note call to an empty base ctor is only possible, if an empty base ctor was defined
         * */
        public SomeChildClass(int i, int j) : base()
        {
            Console.WriteLine($"Child explicit Ctor - {i}, {j}");
        }

        /* Child ctor calls non-matching Base ctor.
         * Note call to an empty base ctor is only possible, if an empty base ctor was defined
         * */
        public SomeChildClass(int i, int j, int k) : base(i)
        {

        }

        
    }


    public class SomeParentClass
    {
        public SomeParentClass()
        {
            Console.WriteLine("Parent default Ctor");
        }

        public SomeParentClass(int i)
        {
            Console.WriteLine("Parent explicit Ctor - {i}");
        }
    }
}

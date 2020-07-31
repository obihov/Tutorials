using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_TypeConversion
{
    static internal class ValidateTypeConversionExample
    {
        /*
         * NOTE:
         * ====
         * Use is / as on only reference or nullable types. Do not use with struct
         * */

        public static void Run()
        {
            //use try-catch to validate type conversions - this approach works but impedes performance and code-readabilty.
            try
            {
                SomeBaseClass someBase = new SomeBaseClass();

                //error should through if below don't work
                SomeClass someClass = (SomeClass)someBase;  //explicit
                SomeBaseClass baseClass = someClass;    //implicit
            }
            catch(InvalidCastException ex)
            {
                Console.WriteLine(ex.Message);
            }

            //use is to validate type conversions - returns true or false.
            SomeClass someClass1 = new SomeClass();
            if(someClass1 is SomeBaseClass)
            {
                someClass1 = new SomeClass();
            }
            SomeBaseClass someBaseClass1 = new SomeBaseClass();
            if(someBaseClass1 is SomeClass)
            {
                someBaseClass1 = new SomeBaseClass();
            }

            //use as to validate type conversion - returns a value if true otherwise null.
            //apples to apples
            SomeBaseClass y = someBaseClass1 as SomeBaseClass;
            SomeClass y1 = someClass1 as SomeClass;

            //big apple to small apple
            SomeClass x1 = someBaseClass1 as SomeClass;

            //small apple to big apple
            SomeBaseClass x = someClass1 as SomeBaseClass;

            //explicit - as
            SomeClass z = (SomeClass)(someClass1 as SomeBaseClass);

            //implicit - as
            SomeBaseClass a = someClass1 as SomeClass;
        }
    }


    internal class SomeBaseClass
    {

    }

    internal class SomeClass : SomeBaseClass
    {

    }
}

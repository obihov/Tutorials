using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_TypeConversion
{
    #region Tutorial
    /*
     * So boxing is the process of taking a value type, putting it inside a new object on the heap,
     * and storing a reference to it on the stack. Unboxing is the exact opposite: It takes the item
     * from the heap and returns a value type that contains the value from the heap
     * */
    #endregion
    static class BoxingUnboxingExample
    {
        static public void Run()
        {
            //You can use reflection to know more about the type you're working with. Name gives the type like Int32, String, Boolean. Fullname gives the namespace and type. System.Int32, System.Char etc.
            Console.WriteLine(20.GetType().Name);
            Console.WriteLine(20.GetType().FullName);
            Console.WriteLine("Obi".GetType().Name);
            Console.WriteLine("Obi".GetType().FullName);
            Console.WriteLine(true.GetType().Name);
            Console.WriteLine(true.GetType().FullName);
            Console.WriteLine();

            //You can use reflection also to learn about if a type is sealed, public, or a valuetype etc.
            Console.WriteLine("obi".GetType().IsSealed);
            Console.WriteLine("obi".GetType().IsPublic);
            Console.WriteLine("obi".GetType().IsValueType);
            Console.WriteLine();

            //Below are example of boxing
            object box1 = 100;
            string box2 = "obi"; //string is a sequence of chars. Chars are value types. so 'o', 'b', 'i' each are boxed together into an object on the heap. The reference "name" points to the object.
            string box3 = 20.ToString(); //the int value type 20 is boxed into a string object.
            IFormattable box4 = 10;    //you can box a valuetype to an interface and abstract class as well.
            System.Collections.ArrayList box5 = new ArrayList() { "obi", 20 }; //non-generic collections also use boxing to add items.
            box5.Add(true); //you can see the method Add takes in an object. If you pass a value type, then obviously it will get boxed.

            //Below are example of unboxing
            int unbox1 = (int)box1;
            
            foreach(dynamic unbox2 in box2)//could still do char unbox2 in box2, but we wanted to showcase unboxing of char from string object types.
            {
                Console.WriteLine((char)unbox2);
            }
            
            int unbox4 = (int)box4; //unbox an interface to its actual type. can do the same with abstract classes.
                        
            foreach(var unbox5 in box5)
            {             
                
                switch (unbox5.GetType().Name)
                {
                    case "Int32":
                        {
                            int a = (int)unbox5;
                        }break;
                    case "Boolean":
                        {
                            bool b = (bool)unbox5;
                        }break;
                    case "String":
                        {
                            string val = (string)unbox5;
                            List<char> chars = new List<char>();
                            foreach (var c in val)
                            {
                                chars.Add(c);
                            }
                        }break;
                }
            }
        }
    }

    
}

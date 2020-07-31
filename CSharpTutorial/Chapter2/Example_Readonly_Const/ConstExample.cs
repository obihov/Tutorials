using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_Readonly_Const
{
    #region Tutorial
    /*
     * Const variable cannot accept any access modifier.
     * They cannot be public, private, protected, internal, protected-internal.
     * This is so because once set on compile time, they are never to be set again.
     * You can only set a const variable by declaring and intializing the variable at once.
     * Const variable cannot be set using constructors.
     * */
    #endregion

    static public class ConstExample
    {
        static public void Run()
        {
            ConstEx constEx = new ConstEx();
            constEx.SetValue(20);
        }
    }
    internal class ConstEx
    {
        const int Age = 10;   //declare and initialize to set at compile time

        public void SetValue(int newAge)
        {
            Console.WriteLine("Cannot reset a const value. Const value are set at compile time. So cant use a constructor to set a value to a const variable at compile or runtime.");
        }
    }
}

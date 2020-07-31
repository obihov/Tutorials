using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_Struct
{
    #region Tutorial
    /*
     * Structs are like classes.
     * You can have fields, methods, delegates, properties, constructors in structs.
     * However, you cannot have a parameterless constructor in a struct.
     * You must use a constructor to assign value to fields and properties in your structs.
     * Structs cannot be inherited from. However they can inherit and implement interfaces.
     * Structs allow you to create small objects that have members that are logically related.
     * Structs are objects but are valuetypes (not reference types as classes).
     * Structs are stored on the stack allowing for faster access than classes which are stored on heap.
     * */
    #endregion

    static class StructExample
    {
        static public void Run()
        {
            StructEx structEx = new StructEx("obinna", "mogbogu");
            var name = structEx.GetFullName();
            var email = structEx.GetEmail();
            Console.WriteLine($"Name: {name}\nEmail: {email}");
        }
    }
    public struct StructEx
    {
        readonly private string FirstName;
        readonly private string LastName;


        public StructEx(string firstname = "N/A", string lastname = "N/A")
        {
            this.FirstName = firstname;
            this.LastName = lastname;
        }

        public string GetFullName()
        {
            return $"{this.FirstName} {this.LastName}";
        }

        public string GetEmail()
        {
            var email = $"{this.FirstName.Substring(0, 3)}.{this.LastName.Substring(0, 3)}@gmail.com";
            return email;
        }
    }
}

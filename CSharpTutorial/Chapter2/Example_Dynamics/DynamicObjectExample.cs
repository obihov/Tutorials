using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_Dynamics
{
    static internal class DynamicObjectExample
    {
        static internal void Run()
        {
            dynamic student = new StudentDynamicObject();   //a dynamic object created to dynamically work with its members
            
            student.FirstName = "Mr. Obinna";       //calls TrySetMember
            Console.WriteLine(student.FirstName);   //calls TryGetMember

            //dynamic object with a non-exiting member will yield an error. So it's a good practice to wrap dynamic operations in a try/catch block
            try
            {
                student.Age = 10;                   //calls TrySetMember
                Console.WriteLine(student.Age);     //calls TryGetMember
            }
            catch(RuntimeBinderException ex)
            {
                Console.WriteLine(ex.Message);
            }
            

        }
    }

    internal class StudentDynamicObject : DynamicObject
    {
        private string FirstName;

        //binder.Name is the member you want in a type. result is the value of operation performed on the member.
        sealed public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            #region Finding a member in a type that is private
            /*
            *use either typeof(Type).GetField/GetMethod/GetProperties/FindMembers/GetMembers  etc. or
            *use instance.GetType().GetField/GetMethod/GetProperties/FindMembers/GetMembers etc to search for a member in a class
            *eg. var x = typeof(StudentDynamicObject).GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            * 
            * Make sure to include the BidingFlags.Instance and use | to combine enums. The & just simply adds additional enum which can be used to expand your search.
            * */
            #endregion
            //get the member name - field, prop, ctor, method, events etc.
            var memberName = binder.Name;

            //use reflection to find if the member exist in the given type
            var field = this.GetType()
                                 .GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                 .FirstOrDefault(f => f.Name == memberName);

            //if member is found, then perform operation on it.
            if (field != null)
            {
                result = field.GetValue(this);
                return true;
            }

            //perform no operation and return null when member can't be found.
            result = null;
            return false;
        }

        //binder.Name is the member you want in a type. result is the value to use perform operation on the member.
        sealed public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            #region Finding a member in a type that is private
            /*
            *use either typeof(Type).GetField/GetMethod/GetProperties/FindMembers/GetMembers  etc. or
            *use instance.GetType().GetField/GetMethod/GetProperties/FindMembers/GetMembers etc to search for a member in a class
            *eg. var x = typeof(StudentDynamicObject).GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            * 
            * Make sure to include the BidingFlags.Instance and use | to combine enums. The & just simply adds additional enum which can be used to expand your search.
            * */
            #endregion

            var memberName = binder.Name;
            var field = this.GetType()
                                 .GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                 .FirstOrDefault(f => f.Name == memberName);
           
            if (field != null)
            {
                field.SetValue(this, value);
                return true;
            }

            return false;
        }
    }
}

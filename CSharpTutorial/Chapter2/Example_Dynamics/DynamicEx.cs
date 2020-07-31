using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_Dynamics
{
    internal static class DynamicEx
    {
        static internal void Run()
        {
            #region Tutorial
            /*
             * Use dynamic keyword to achieve weak typing.
             * C# is largely a strong (strict) type language. The dynamic keyword allows for a weak type approach in C#.
             * Weak typing is encouraged as best practice when working with external resources such as:
             * COM (Componenent Object Model) applications like Excel, Word, ActiveX etc.
             * HTML DOM
             * JSON
             * Python etc.
             * 
             * NOTE:
             * ======
             * You can always choose to use the exact type if you want to as opposed to using dynamic. But the dynamic keyword could allow
             * you write code without worrying about compile time issues. C# simply trust that you are making use of objects and their members correctly
             * when working off a dynamic variable. Example, in Student type, you created an instance of it as dynamic. Well because you had used
             * dynamic to instantiate a Student type, intellisense will not help you discover its members. So you mmay write student.AGE instead of 
             * student.Age, and no errors will occur. But then, when C# runs your code at runtime, it will find these errors. As a best practice, always 
             * consider using try/catch block when working with dynamic keywords.
             * */
            #endregion
            
            dynamic age = 10;
            dynamic student = new Student(age); //We passed a dynamic to the Student ctor, which expects an int type. At compile time, we could have used the dynamic keyword to pass in a string and no error would have occured. However, if we did pass in a string value as dynamic, then at runtime, the compiler will find an error.
            Console.WriteLine(student.Age); //intellisense will not show Age field. So if you wrote AGE instead of Age, there won't be compile time error. Though keep in mind that there will be a runtime error.
        }


    }

    internal class Student
    {
        readonly public int Age;

        public Student(int age)
        {
            this.Age = age;
        }
    }
}

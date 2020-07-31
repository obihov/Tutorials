using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_TypeConversion
{
    static internal class TypeCompatibilityCheck
    {
        static public void Run()
        {
            #region IS
            /*
             * Using the is comaprison, will allow you to compare different types together. 
             * */
            var isGradStudent = (new Student()) is GradStudent;             //A student cannot be a grad because a parent cannot be like a child.
            var isUnderGradStudent = (new Student()) is UnderGradStudent;   //A student cannot be a undergrad because a parent cannot be like a child.
            var isStudent = (new GradStudent()) is Student;                 //is a grad student a student. Yes because a child can be like a parent.
            var isStudent2 = (new UnderGradStudent()) is Student;           //is an undergrad student a student. Yes because a child can be like a parent.

            var isNotGradStudent = !((new UnderGradStudent()) is GradStudent);
            var isNotUnderGradStudent = !((new GradStudent()) is UnderGradStudent);
            #endregion

            #region AS

            /*
             * Using the is comaprison, will allow you to compare different types together. 
             * */
            var isGradStudent2 = (new Student()) as GradStudent;             //A student cannot be a grad. Parent cannot be a child. Returns null.
            var isUnderGradStudent2 = (new Student()) as UnderGradStudent;   //A student cannot be a undergrad. Parent cannot be a child. Returns null.
            var isStudent3 = (new GradStudent()) as Student;                 //is a grad student a student
            var isStudent4 = (new UnderGradStudent()) as Student;           //is an undergrad student a student
            var gradStudent = (new GradStudent()) as GradStudent;
            var underGradStudent = (new UnderGradStudent()) as UnderGradStudent;

            #endregion

            #region Special Examples
            //IS
            var resolved0 = (new Car()) is IVehilce;
            var resolved1 = (new ApartmentBuilding()) is AbsBuilding;

            //Below don't work - Comment out below codes.
            //var resolved2 = IVehilce is (new Car());
            //var resolved3 = AbsBuilding is new ApartmentBuilding();

            //AS
            var resolved4 = (new Car()) as IVehilce;
            var resolved5 = (new ApartmentBuilding()) as AbsBuilding;

            //Below don't work - Comment out below codes.
            //var resolved6 = IVehilce as (new Car());
            //var resolved7 = AbsBuilding as new ApartmentBuilding();
            #endregion

        }
    }

    internal class Student
    {

    }

    internal class GradStudent : Student
    {

    }

    internal class UnderGradStudent : Student
    {

    }

    //Special Example based on Interface
    internal interface IVehilce { }
    internal class Car : IVehilce { }

    //Special Example based on Abstract class
    internal abstract class AbsBuilding { }
    internal class ApartmentBuilding : AbsBuilding { }
}

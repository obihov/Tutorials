using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_TypeConversion
{
    static class UserDefinedConversionExample
    {
        static public void Run()
        {
            //Explicit operator example for reference type conversion - convert from House to Apartment. Special cast syntax is required
            House house1 = new House { Number = 2511 };
            Apartment apartment1 = (Apartment)house1;

            //Explicit operator example for reference type conversion - convert from Apartment to House.  Special cast syntax is required
            Apartment apartment2 = new Apartment { Number = 15308 };
            House house2 = (House)apartment2;

            //Implicit operator example for reference type conversion - convert from College to University. No special cast syntax required.
            College college1 = new College { Name = "Boston College of Education" };
            University university1 = college1;

            //Implicit operator example for reference type conversion - convert from University to College. No special cast syntax required.
            University university2 = new University { Name = "Boston State University" };
            College college2 = university2;
        }
    }

    #region User-Defined Explicit Conversion
    /*
     * With implicit operator, you don't need a special cast syntax. That is, there's no need for a "(typeConvertingTo) typeConvertingFrom" syntax.
     * To use in your code simply do.
     * House house = apartment
     * 
     * With explicit operator, you will need the explicit conversion special cast syntax So.
     * House house = (House) apartment.
     * */

    public class Apartment
    {
        public int Number { get; set; }

        #region Tutorial
        /*
         * Below syntax allows you convert from Apartment to a House.
         * so you must use a "public static"
         * you must provide "explicit operator" keywords
         * operator name is the type you want to ***covert to***. (i.e. House)
         * operator parameter is the type you are ***converting from***. (i.e. Apartment)
         * Use in code like below:
         * Apartment apartment = new Apartment { name = "..." }
         * House house = ***(House) apartment;*** //(House) apartment, makes use of the explicit operator to help convert apartment to a house.
         * */
        #endregion

        static public explicit operator House(Apartment apartment)
        {
            return new House { Number = apartment.Number };
        }
    }

    public class House
    {
        public int Number { get; set; }

        #region Tutorial
        /*
         * Below syntax allows you convert from Apartment to a House.
         * so you must use a "public static"
         * you must provide "explicit operator" keywords
         * operator name is the type you want to ***covert to***. (i.e. Apartment)
         * operator parameter is the type you are ***converting from***. (i.e. House)
         * Use in code like below:
         * House house = new House { name = "..." }
         * Apartment apartment = ***(Apartment) house;*** //(Apartment) house, makes use of the explicit operator to help convert house to an apartment.
         * */
        #endregion

        static public explicit operator Apartment(House house)
        {
            return new Apartment { Number = house.Number };
        }
    }
    #endregion

    #region User-Defined Implicit Conversion
    /*
     * With implicit operator, you don't need a special cast syntax. That is no need for a "(typeConvertingTo) typeConvertingFrom" syntax.
     * To use in your code simply do.
     * College college = university;
     * or
     * University university = college;
     * 
     * If you were to provide explicit conversion, then the special cast syntax will be required.So
     * College college = (College) university.
     * or
     * University university = (University) college;
     * */

    public class College
    {
        public string Name { get; set; }

        #region Tutorial
        /*
         * Below code converst Colleget to a University.
         * You must specify the "public static", "implicit operator", TypeConvertingTo as operator name, TypeConvertingFrom as operator parameter.
         * Return value must be of TypeConvertingTo.
         * In this example, TypeConvertingTo is University and TypeConvertingFrom is College
         * */
        #endregion
        static public implicit operator University(College college)
        {
            return new University { Name = college.Name };
        }
    }

    public class University
    {
        public string Name { get; set; }

        #region Tutorial
        /*
         * Below code converst Colleget to a University.
         * You must specify the "public static", "implicit operator", TypeConvertingTo as operator name, TypeConvertingFrom as operator parameter.
         * Return value must be of TypeConvertingTo.
         * In this example, TypeConvertingTo is College and TypeConvertingFrom is University
         * */
        #endregion
        static public implicit operator College(University university)
        {
            return new College { Name = university.Name };
        }
    }
    #endregion


}

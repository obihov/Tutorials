using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_Encapsulation
{
    #region Tutorial - Working with Outer and Inner Class, also Access Modifiers.
    /*
     * Tutorial focus on working with access modifiers for nested types within class (that is defined either as static or non-static class)
     * 
     * 
     * The default for a class enclosing nested types is internal, but you can declare public.
     * The default for a abstract class (or a regular base class) is internal, but you can declare public.
     * The default for a interface is internal, but you can declare public.
     * 
     * When working with nested types inside a class certain rules applies.
     * 1) Non-Static class can have non-static (aka instance) nested class, interface, enums, fields, and properties. Cannot have static interface and enums.
     * 2) Non-Static class can have static class, fields, and properties only. Cannot have static interface and enums.
     * 3) Static class can only have nested static members. Therefore, can only have static class, fields, and properties. But Cannot have static interface and enums.
     * 
     * NOTE:
     * =====
     * The use of the Internal access modifier, ensures visibility of the type to other types created within the same assembly/project. 
     * So any type not in the project will not be able to see or use an internal type defined in another project.
     * You can however, make types in other project see an internal type defined in another assembly/project.
     * To do this, go to the project/assembly where your internal type is defined in. RIGHT-CLICK and got to the property folder in the directory.
     * Inside the property folder, open the AssemblyInfo.cs file.
     * Add the below-attribute in the file. 
     * [assembly: InternalsVisibleTo("AnotherProject")]
     * With the above attribute added in the file, you have now granted that all types defined in a project called "AnotherProject" 
     * can have access to all internal types defined within the current project.
     * */
    #endregion

    class AccessModifierExample
    {
        public void RunNestedClass()
        {
            /* OuterClass.InnerClass1 //will not work because nested InnerClass1 is private by default.*/
        }



        public void RunNestedFieldsProperties()
        {


        }






    }

    /// <summary>
    /// Non-Static class with instance members only.
    /// Note: You can use nested static types in a non-static class.
    /// See the example "NonStaticOuterClass2" below. 
    /// Note: In that example, nested static types in a non-static class is only allowed for nested class, field, and property types. 
    /// You cannot use static enums, and interfaces in a class (regardless if it is static or non-static)
    /// </summary>
    internal class NonStaticOuterClass   //the default for outer class definition is internal.
    {
        //Nested Classes
        class InnerClass1 { }    //default is private
        private class Innerclass2 { }  //private use only
        private protected class Innerclass3 { } //private OR protected use only
        protected internal class Innerclass4 { } //protected OR internal (same assembly) use only
        protected class Innerclass5 { } //protected use only (assembly not taken into consideration. In other words, it's public to derived classes.)
        internal class Innerclass6 { }  //internal (same assembly) use only
        public class Innerclass7 { }    //public use only

        //Nested Interfaces
        interface InnerInterface1 { }    //default is private
        private interface InnerInterface4 { }  //private use only
        private protected interface InnerInterface7 { } //private OR protected use only
        protected internal interface InnerInterface6 { } //protected OR internal (same assembly) use only
        protected interface InnerInterface5 { } //protected use only (assembly not taken into consideration. In other words, it's public to derived classes.)
        internal interface InnerInterface2 { }  //internal (same assembly) use only
        public interface InnerInterface3 { }    //public use only


        //Nested enums
        enum Innerenum1 { }    //default is private
        private enum Innerenum2 { }  //private use only
        private protected enum Innerenum3 { } //private OR protected use only
        protected internal enum Innerenum4 { } //protected OR internal (same assembly) use only
        protected enum Innerenum5 { } //protected use only (assembly not taken into consideration. In other words, it's public to derived enumes.)
        internal enum Innerenum6 { }  //internal (same assembly) use only
        public enum Innerenum7 { }    //public use only{ }

        //Nested fields        
        string Innerfield1;    //default is private
        private string Innerstring2;  //private use only
        private protected string Innerfield3; //private OR protected use only
        protected internal string Innerfield4; //protected OR internal (same assembly) use only
        protected string Innerfield5; //protected use only (assembly not taken into consideration. In other words, it's public to derived stringes.)
        internal string Innerfield6;  //internal (same assembly) use only
        public string Innerfield7;    //public use only


        //Nested properties
        string Innerproperty1 { get; set; }    //default is private
        private string Innnerstring2 { get; set; }  //private use only
        private protected string Innerproperty3 { get; set; } //private OR protected use only
        protected internal string Innerproperty4 { get; set; } //protected OR internal (same assembly) use only
        protected string Innerproperty5 { get; set; } //protected use only (assembly not taken into consideration. In other words, it's public to derived stringes.)
        internal string Innerproperty6 { get; set; }  //internal (same assembly) use only
        public string Innerproperty7 { get; set; }    //public use only
    }

    /// <summary>
    /// Static class with static members.
    /// Note: Use of nested static types in a static class is allowed for fully for nested class.
    /// However, it is allowed partially for fields and properties that are not protected, protected internal or private protected.
    /// You cannot use static enums, and interfaces in a class (regardless if it is static or non-static)
    /// </summary>
    internal static class StaticOuterClass   //the default for outer class definition is internal.
    {
        //Nested Classes
        static class InnerClass1 { }    //default is private
        static private class Innerclass2 { }  //private use only
        static private protected class Innerclass3 { } //private OR protected use only
        static protected internal class Innerclass4 { } //protected OR internal (same assembly) use only
        static protected class Innerclass5 { } //protected use only (assembly not taken into consideration. In other words, it's public to derived classes.)
        static internal class Innerclass6 { }  //internal (same assembly) use only
        static public class Innerclass7 { }    //public use only

        //Nested Interfaces
        #region static cannot work on nested interface types (whether class is static or non-static type).
        //static interface InnerInterface1 { }    //default is private
        //static private interface InnerInterface4 { }  //private use only
        //static private protected interface InnerInterface7 { } //private OR protected use only
        //static protected internal interface InnerInterface6 { } //protected OR internal (same assembly) use only
        //static protected interface InnerInterface5 { } //protected use only (assembly not taken into consideration. In other words, it's public to derived classes.)
        //static internal interface InnerInterface2 { }  //internal (same assembly) use only
        //static public interface InnerInterface3 { }    //public use only
        #endregion


        //Nested enums
        #region static cannot work on nested enum types (whether class is static or non-static type).
        //static enum Innerenum1 { }    //default is private
        //static private enum Innerenum2 { }  //private use only
        //static private protected enum Innerenum3 { } //private OR protected use only
        //static protected internal enum Innerenum4 { } //protected OR internal (same assembly) use only
        //static protected enum Innerenum5 { } //protected use only (assembly not taken into consideration. In other words, it's public to derived enumes.)
        //static internal enum Innerenum6 { }  //internal (same assembly) use only
        //static public enum Innerenum7 { }    //public use only{ }
        #endregion

        //fields - note you cannot define an instance member in a static class, so fields must take static.
        static string Innerfield1;    //default is private
        static private string Innerstring2;  //private use only
        #region Protected static nested-fields are not allowed in static class.
        //static private protected string Innerfield3; //private OR protected use only
        //static protected internal string Innerfield4; //protected OR internal (same assembly) use only
        //static protected string Innerfield5; //protected use only (assembly not taken into consideration. In other words, it's public to derived stringes.)
        #endregion
        static internal string Innerfield6;  //internal (same assembly) use only
        static public string Innerfield7;    //public use only


        //properties - - note you cannot define an instance member in a static class, so properties must take static.
        static string Innerproperty1 { get; set; }    //default is private
        static private string Innnerstring2 { get; set; }  //private use only
        #region Protected static nested-properties are not allowed in static class.
        //static private protected string Innerproperty3 { get; set; } //private OR protected use only
        //static protected internal string Innerproperty4 { get; set; } //protected OR internal (same assembly) use only
        //static protected string Innerproperty5 { get; set; } //protected use only (assembly not taken into consideration. In other words, it's public to derived stringes.)
        #endregion
        static internal string Innerproperty6 { get; set; }  //internal (same assembly) use only
        static public string Innerproperty7 { get; set; }    //public use only
    }

    /// <summary>
    /// Non-Static class with static members only.
    /// Note: Use of nested static types in a non-static class is allowed fully for nested class, fields, and properties only. 
    /// You cannot use static enums, and interfaces in a class (regardless if it is static or non-static)
    /// </summary>
    internal class NonStaticOuterClass2   //the default for outer class definition is internal.
    {
        //Nested Classes
        static class InnerClass1 { }    //default is private
        static private class Innerclass2 { }  //private use only
        static private protected class Innerclass3 { } //private OR protected use only
        static protected internal class Innerclass4 { } //protected OR internal (same assembly) use only
        static protected class Innerclass5 { } //protected use only (assembly not taken into consideration. In other words, it's public to derived classes.)
        static internal class Innerclass6 { }  //internal (same assembly) use only
        static public class Innerclass7 { }    //public use only

        //Nested Interfaces
        #region static cannot work on nested interface types (whether class is static or non-static type).
        //static interface InnerInterface1 { }    //default is private
        //static private interface InnerInterface4 { }  //private use only
        //static private protected interface InnerInterface7 { } //private OR protected use only
        //static protected internal interface InnerInterface6 { } //protected OR internal (same assembly) use only
        //static protected interface InnerInterface5 { } //protected use only (assembly not taken into consideration. In other words, it's public to derived classes.)
        //static internal interface InnerInterface2 { }  //internal (same assembly) use only
        //static public interface InnerInterface3 { }    //public use only
        #endregion
            

        //Nested enums
        #region static cannot work on nested enum types (whether class is static or non-static type).
        //static enum Innerenum1 { }    //default is private
        //static private enum Innerenum2 { }  //private use only
        //static private protected enum Innerenum3 { } //private OR protected use only
        //static protected internal enum Innerenum4 { } //protected OR internal (same assembly) use only
        //static protected enum Innerenum5 { } //protected use only (assembly not taken into consideration. In other words, it's public to derived enumes.)
        //static internal enum Innerenum6 { }  //internal (same assembly) use only
        //static public enum Innerenum7 { }    //public use only{ }
        #endregion


        //Nested fields - note you cannot define an instance member in a static class, so fields must take static.
        static string Innerfield1;    //default is private
        static private string Innerstring2;  //private use only
        #region Protected static nested-fields are allowed in non-static class.
        static private protected string Innerfield3; //private OR protected use only
        static protected internal string Innerfield4; //protected OR internal (same assembly) use only
        static protected string Innerfield5; //protected use only (assembly not taken into consideration. In other words, it's public to derived stringes.)
        #endregion
        static internal string Innerfield6;  //internal (same assembly) use only
        static public string Innerfield7;    //public use only


        //Nested properties - - note you cannot define an instance member in a static class, so properties must take static.
        static string Innerproperty1 { get; set; }    //default is private
        static private string Innnerstring2 { get; set; }  //private use only
        #region Protected static nested-properties are allowed in non-static class.
        static private protected string Innerproperty3 { get; set; } //private OR protected use only
        static protected internal string Innerproperty4 { get; set; } //protected OR internal (same assembly) use only
        static protected string Innerproperty5 { get; set; } //protected use only (assembly not taken into consideration. In other words, it's public to derived stringes.)
        #endregion
        static internal string Innerproperty6 { get; set; }  //internal (same assembly) use only
        static public string Innerproperty7 { get; set; }    //public use only
    }
}

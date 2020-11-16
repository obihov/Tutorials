using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_AttributesReflection
{
    internal class BasicAttributeExample
    {
        internal static void Run()
        {
            #region Tutorial
            /*
             * .NET application contains: 
             * - The code that define the application and
             * - Data that describes the code (i.e. Metadata)
             * 
             * Example of an application's metadata are 
             * - Attribute
             * - assembly, valueTypes, referenceTypes, properties, methods, events, parameters.
             * 
             * Using attribute is a powerful way to add metadata to an application.
             * Attributes are written as []. Example [Serializable].
             * Attributes is a metadata that can be used to target other applications' metadata (i.e. assembly, valueTypes, referenceTypes, properties, methods, events, parameters.)
             * 
             * Reflection is the process of retrieving an application's metadata at runtime.
             * 
             * Reflection are used mostly to run codes at runtime (such as instantiating a class at runtime).
             * Using reflection is useful when code is not available at compile time.
             * With reflection, you won't get any compile time error, however you may likely get runtime error if types fail to be available at runtime.
             * */
            #endregion

            Console.WriteLine("Read Tutorial");


        }
    }

    
}

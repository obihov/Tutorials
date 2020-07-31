using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_Encapsulation
{
    #region Tutorial
    /*
     * Tutorial focus on access modifier rules when working with base and derived types, as well as interface and implemented types.
     * 
     * When working with inheritance - regular base parent or abstract class the following rules apply.
     * 1) Your derived type cannot be more accessible than its base. Therefore if base is internal, derive cannot be public, instead can only be internal. 
     * If base is public, then derive can be public or internal.
     * 
     * When working with interface implementation, the following rules apply.
     * 1) Your interface type can be more/less accessible than its implemented type. 
     * Therefore if interface is public, implemented type can either be public or internal.
     * Also, if interface is internal, implemented type can either be public or internal.
     * */
    #endregion

    internal class AccessModifierExample2
    {
        static public void Run() { }
    }

    internal class SomeParentA
    {
        
    }

    //Note: Your derived type cannot be more accessible than its base. Base must have more access than its child.
    //(public class DerivedA : SomeParentA) is wrong.
    internal class DerivedA : SomeParentA
    {
        //this class definition is fine. 
        //Base is internal, child can only be less. In other words, Child cannot be public but internal.
        //If Base was public, then child can be public or lower (as in internal).
    }

    internal abstract class SomeParentB
    {

    }

    //Note: Your derived type cannot be more accessible than its base. Base must have more access than its child.
    //(public class DerivedB : SomeParentB) is wrong.
    internal class DerivedB : SomeParentB
    {
        //this class definition is fine. 
        //Base is internal, child can only be less. In other words, Child cannot be public but internal.
        //If Base was public, then child can be public or lower (as in internal).
    }

    public interface SomeInterfaceA
    {

    }

    //For interface, whether it is public or internal, the implemented type can choose to be public or internal. So below are fine.
    //(internal class ImplementedClassA : SomeInterfaceA) is acceptable.
    //(public class ImplementedClassA : SomeInterfaceA) is acceptable.
    internal class ImplementedClassA : SomeInterfaceA
    {
        //this class definition is fine.
        //Regardless of whether the interface is public or internal, its implemented types can choose to be either public or internal.
    }
}

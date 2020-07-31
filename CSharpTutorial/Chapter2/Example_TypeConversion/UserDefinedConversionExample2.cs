using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_TypeConversion
{
    static class UserDefinedConversionExample2
    {
        static public void Run()
        {
            //Note we convert over the instance. We are not converting "(int) money.Amount" but instead over the instance so "(int) money" to get our desired type.
            //That's the benefit of defining your own conversion.

            //Explicit operator example for value type conversion - convert from "money's double" to "int". Special Cast syntax is required.
            Money money = new Money { Amount = 52.50D };
            int amount1 = (int) money;

            //Implicit operator example for value type conversion - convert from "money's double" to "double". No special cast syntax required.
            double amount2 = money;
        }
    }

    public class Money
    {
        public double Amount { get; set; }

        //create an explicit operator that allows conversion from ***Money's double type*** to a ***int type***. Special cast syntax is required.
        static public explicit operator int(Money money)
        {            
            return (int)money.Amount;
        }

        //create an explicit operator that allows conversion from ***Money's double type*** to a ***int type***. Special cast syntax not required.
        static public implicit operator double(Money money)
        {            
            return money.Amount;
        }
    }
}

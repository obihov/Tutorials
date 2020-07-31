using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_TypeConversion
{
    static internal class ConversionWithHelperClassExample
    {
        public static void Run()
        {
            #region Convert
            /*
             * You can use the Convert helper class to convert to any type, ToInt, ToBool, ToDouble, ToChar, even ToString
             * */
            int value = Convert.ToInt32(true);
            int value2 = Convert.ToInt32(false);
            int value3 = Convert.ToInt32('a');
            char value4 = Convert.ToChar(97);
            string stringVal = Convert.ToString(DateTime.Now);

            #endregion

            #region Parse
            /*
             * Try&Catch block required to handle situations where the conversion between types are not compatible.
             * You may also use the TryParse method as opposed to safely determine if conversion types are compatible before return converted value.
             * You will see later in another TypeConversion example how to check the compatibility of types safely using "is" and "as".
             * */
            try
            {
                //int value5 = int.Parse("A"); //uncomment to throw error.
                int value6 = int.Parse("100");
            }
            catch(FormatException ex)
            {
                throw new ArgumentException("String value passed to Parse method is not valid.", ex);
            }
            #endregion

            #region TryParse
            /*
             * TryParse is better than Try alone. With TryParse, you can always check if two types are compatible, like int vs string.
             * If compatible, then a value of type bool indicating as true is returned, else 0 if otherwise.
             * Note: An out value containing the resulting conversion is returned.
             * */
            bool canConvert = int.TryParse("100", out int value7);
            if (canConvert)
            {
                Console.WriteLine(value7);
            }
            else
            {
                Console.WriteLine("Failed to convert.");
            }
            #endregion
        }

    }

   
}

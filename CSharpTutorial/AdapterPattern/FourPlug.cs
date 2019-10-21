using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPattern
{
    //A four-plug that is only compatible with a four-socket adapter or four-socket wall outlet. 
    //However, using a four-socket adapter, the four-plug can become compatible with a two-socket outlet, provided that the four-plug adapter can implement/connect to two-socket outlet.
    public class FourPlug : IPlug
    {
        public void Connect()
        {
            Console.WriteLine("Four-Plug connected to power/two-socket outlet USING a Four-Socket Adapter.");
        }
    }
}

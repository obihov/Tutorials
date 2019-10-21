using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPattern
{
    //A three-plug that is only compatible with a three-socket adapter or three-socket wall outlet. 
    //However, using a three-socket adapter, the three-plug can become compatible with a two-socket outlet, provided that the three-plug adapter can implement/connect to two-socket outlet.
    public class ThreePlug : IPlug
    {
        public void Connect()
        {
            Console.WriteLine("Three-Plug connected to power/two-socket outlet USING a Three-Socket Adapter.");
        }
    }
}

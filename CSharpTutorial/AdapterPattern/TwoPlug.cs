using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPattern
{
    //A two plug that is compatible with a two-socket (without the need of an adapter)
    //This two-plug can be plugged into a two-socket wall outlet directly without the need of an adapter.
    //However, if there's ever a need to connect to a three or four socket outlet etc., then simply implement or extend one of the compatible adapters for that purpose.
    public class TwoPlug : TwoSocketInterface
    {
        public void PlugIn()
        {
            Console.WriteLine("TwoPlug plugged into two-socket outlet directly.");
        }
    }
}

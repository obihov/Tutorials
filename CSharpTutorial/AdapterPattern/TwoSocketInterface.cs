using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPattern
{
    //Wall outlet/ Main socket is a two-socket.
    //Only two-plugs are directly compatible with the Main socket.
    //Adapters (i.e. secondary sockets on their own) will be required to allow more than two plugs to be compatible with the main socket.
    //All Adapters must implement the TwoSocketInterface. Which is like saying the Adapter(which is a socket itself) will have two plugs that can connect to a two-socket outlet.
    public interface TwoSocketInterface
    {
        void PlugIn();
    }
}

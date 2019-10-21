using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPattern
{
    /*
     * An ADAPTER is both a plug and a socket. It can plug into compatible sockets and can allow compatible plugs to plug into its socket.
     * This is a Four-Socket Adapter that can be plugged into a Two-socket outlet.
     * Any Four-plug can plug into this four-socket adapter directly.
     * Since the Adapter itself is pluggable to a two-socket outlet (i.e. adapter implements the TwoSocketInterface),
     * then, this adapter can allow for any four-plugs (connected to it) to be compatible with a two-socket wall outlet.
     * */

    //1. First make the adapter compatible with the two-socket outlet (using interface implementation).
    public class FourSocketAdapter : TwoSocketInterface
    {
        //2. Define what kind of plug type that can connect to the adapter (using composition).
        private FourPlug FourPlug { get; set; }

        //3. Connect a plug to the adapter (using dependency injection).
        //(Optional: using interface in the dependency injection is like a blind man trying to connect a plug. 
        //We can't know if the plug is compatible or not with the adapter unless we enforce it to be compatible (using TypeCasting or using a base class instead of an interface as the ctor argument).
        public FourSocketAdapter(IPlug plug)
        {
            try
            {
                //4. If using interface in the dependency injection instead of base class, then ensure to force a specific plug type that will be compatible with the adapter.
                //In our case, only a three-plug is compatible with the ThreeSocketAdapter. So we force/typecast the plug to a three-plug.
                FourPlug = (FourPlug)plug;
            }
            catch (TypeInitializationException ex)
            {
                throw ex;
            }
        }

        public void PlugIn()
        {
            FourPlug.Connect();
        }
    }
}

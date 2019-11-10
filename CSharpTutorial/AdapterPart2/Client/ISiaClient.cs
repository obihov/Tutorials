using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPart2.Client
{
    /*
     * Contract for sending JSON formatted string to SIA for test reporting
     * */
    interface ISiaClient
    {
        void Post();        
    }
}

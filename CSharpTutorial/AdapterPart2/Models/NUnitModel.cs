using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPart2.Models
{
    /*
     * Automapper Model
     * Deserialization Model
     * */
    class NUnitModel
    {
        public string ProjName { get; set; }
        public string TestId { get; set; }        
        public string TestStatus { get; set; }
    }
}
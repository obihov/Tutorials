using MSCA_New._0ThreadExample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCA_New
{
    class Program
    {
        static void Main(string[] args)
        {
            //Thread - ThreadStart Example
            A_ThreadStart.Example1();
            A_ThreadStart.Example2();

            //Thread - ParameterizedThreadStart Example
            B_ParameterizedThreadStart.Example1();
            B_ParameterizedThreadStart.Example2();


        }
    }
}

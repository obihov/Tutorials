using AdapterPart2.Adapter;
using AdapterPart2.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPart2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Posting SIA result to Stratus Reporter....");
            ISiaClient siaClient = new SIAClient("SIA.txt");
            siaClient.Post();


            Console.WriteLine("Posting NUnit result to NUnit Test Reporter....");
            INUnitClient nUnitClient = new NUnitClient("NUNIT.txt");
            nUnitClient.PostTestResults();

            Console.WriteLine("Posting NUnit result to Stratus Test Reporter....");
            SIAAdapter adapter = new SIAAdapter(TestTypeSelector.NUnit, "NUNIT.txt");
            adapter.Post();

            Console.ReadKey();
        }
    }
}

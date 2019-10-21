using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPattern
{
    public class Program
    {
        static void Main(string[] args)
        {
            TwoPlug twoPlug = new TwoPlug();
            twoPlug.PlugIn();
            Console.WriteLine();

            ThreeSocketAdapter threeSocketAdapter = new ThreeSocketAdapter(new ThreePlug());
            threeSocketAdapter.PlugIn();
            Console.WriteLine();

            FourSocketAdapter fourSocketAdapter = new FourSocketAdapter(new FourPlug());
            fourSocketAdapter.PlugIn();
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
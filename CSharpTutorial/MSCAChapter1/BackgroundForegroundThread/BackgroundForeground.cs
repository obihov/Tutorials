using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCAChapter1.BackgroundForegroundThread
{
    class BackgroundForeground
    {
        public static void SomeMethodToRun()
        {
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Running...");
            }
                
        }
    }
}

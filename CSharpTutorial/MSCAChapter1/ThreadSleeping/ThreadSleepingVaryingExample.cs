using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSCAChapter1.ThreadSleeping
{
    public class ThreadSleepingVaryingExample
    {
        /// 
        /// This will take a 0ms wait. Meaning, once it reads an item from the line of code in another method executed on another thread, it quickly resumes back.
        /// Each time it reads one item of code that's executed on another thread (whether it's a {, a space, a variable, = , a value etc),
        /// automatically, the CPU will resume back. In this example, because SmallDataSet has 0 wait, and LargeDataSet has a little wait of about 10ms, therefore,
        /// it's highly likely that the CPU will console output of SmallDataSet more times for each item of code it encounters in LargeDataSet.
        /// Meaning that, while the CPU tries to read { for instance in LargeDataSet, it will resume back on SmallDataSet and execute its console output.
        /// If the CPU switches back to LargeDataSet to execute the for code item, it will again resume back on SmallDataSet and execute its console output.
        /// All this is the case because SmallDataSet instructs the CPU not to wait or keep it waiting for long when other threads are being executed.
        /// 


        public static void SmallDataSet()
        {
            for(int i = 0; i < 30; i++)
            {
                Console.WriteLine($"Small Data: {i}.");
                Thread.Sleep(0000); //Yes switch context but certainly don't pause for long. Therefore, resume back immediately
            }
        }

        public static void LargeDataSet()
        {
            for (int i = 0; i < 30; i++)
            {                
                Console.WriteLine($"Large Data: {i}.");
                Thread.Sleep(0010); //pause a little while, then resume back immediately
            }
        }
    }
}

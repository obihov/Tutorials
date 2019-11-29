using System;
using System.Threading;

namespace MSCAChapter1.ThreadPerformance
{
    public static class Methods
    {


        public static void Jump()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine($"Jump\t-{i}");
            }

        }

        public static void Drive()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine($"Drive\t-{i}");
            }
        }
    }
}

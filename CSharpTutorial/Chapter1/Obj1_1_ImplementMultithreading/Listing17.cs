using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    class Listing17
    {
        public static void Example1()
        {
            Console.WriteLine("Break allows any current iterations to finish running, but prevents all non-running iterations from running." +
                "It's lowest break iteration is the last running-iteration to finish before ending.");

            //Please note, that though iterations don't run sequentially and there's no guarantee that iterations will start from 0, as any
            //iteration can be picked up by an available thread to run. So 500, 230, 112, 9, 0, 234, 900 can be running iterations in parallel.
            var numbers = Enumerable.Range(0, 1000).ToList();
            ParallelLoopResult loopResult = Parallel
                                            .For(0, 
                                                numbers.Count,
                                                (int i, ParallelLoopState loopState) => 
                                                {
                                                    Console.WriteLine($"Example 1 Current break iteration: {i}");
                                                    if(i == 500)
                                                        loopState.Break();
                                                    return;
                                                });
            Console.WriteLine($"Example 1 Lowest break iteration: {loopResult.LowestBreakIteration}");
        }

        public static void Example2()
        {
            Console.WriteLine("Stop ends all iterations (both running and non-running)." +
                " It's lowest break iteration is null.");

            var numbers = Enumerable.Range(0, 1000).ToList();
            ParallelLoopResult loopResult = Parallel
                                            .For(0,
                                                numbers.Count,
                                                (int i, ParallelLoopState loopState) =>
                                                {                                                    
                                                    Console.WriteLine($"Example 2 Current break iteration: {i}");
                                                    if(i == 500)
                                                        loopState.Stop();
                                                    return;
                                                });
            Console.WriteLine($"Example 2 Lowest break iteration: {loopResult.LowestBreakIteration}");
        }

        public static void Example3()
        {
            List<IterationsToRun> iterationsToRuns = new List<IterationsToRun>();
            iterationsToRuns.Add(new IterationsToRun { Name = "ManUtd" });
            iterationsToRuns.Add(new IterationsToRun { Name = "Chelsea" });
            iterationsToRuns.Add(new IterationsToRun { Name = "Arsenal" });
            iterationsToRuns.Add(new IterationsToRun { Name = "Liverpool" });
            iterationsToRuns.Add(new IterationsToRun { Name = "ManCity" });
            iterationsToRuns.Add(new IterationsToRun { Name = "Spurs" });
            iterationsToRuns.Add(new IterationsToRun { Name = "RealMadrid" });
            iterationsToRuns.Add(new IterationsToRun { Name = "AtleticoMadrid" });
            iterationsToRuns.Add(new IterationsToRun { Name = "Barcelona" });
            iterationsToRuns.Add(new IterationsToRun { Name = "Bayern" });
            iterationsToRuns.Add(new IterationsToRun { Name = "Dortmund" });
            iterationsToRuns.Add(new IterationsToRun { Name = "Borrusia" });
            iterationsToRuns.Add(new IterationsToRun { Name = "PSG" });
            iterationsToRuns.Add(new IterationsToRun { Name = "Juventus" });

            ParallelLoopResult result = Parallel.For(0, iterationsToRuns.Count, (int i, ParallelLoopState loopState) =>
            {
                if(iterationsToRuns[i].Name.Equals("Spurs", StringComparison.CurrentCultureIgnoreCase))
                {
                    loopState.Break();
                }
                Console.WriteLine($"{i} - {iterationsToRuns[i].Name}");
            });
            Console.WriteLine($"Lowest iteration break: {result.LowestBreakIteration}. " +
                $"Item break at {iterationsToRuns[(int)result.LowestBreakIteration].Name}");
        }

        private class IterationsToRun
        {
            public string Name { get; set; }
        }
    }
}

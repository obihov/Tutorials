using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public class Listing13
    {
        /// <summary>
        /// Example shows how to simply task creation using TaskFactory class.
        /// </summary>
        public static void Example1()
        {
            //Using TaskFactory to create task.
            Task<int[]> parentTask = Task.Run<int[]>(() =>
            {

                //With TaskCreationOptions each of the child task below will be created with a configuration that associate them with the parent (outer/antecedant task)
                //With TaskContinuationOptions, the continuation for each child task you've created, will execute either asynchronously or synchronously (as specified below)
                TaskFactory taskFactory = new TaskFactory(TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously);

                int[] results = new int[3];
                
                //Child 1 using a factory
                taskFactory.StartNew(() =>
                {
                    results[0] = 10;
                });

                //Child 2 using a factory
                taskFactory.StartNew(() =>
                {
                    results[1] = 20;
                });

                //Child 3 using a factory
                taskFactory.StartNew(() =>
                {
                    results[2] = 40;
                });

                return results;
            });

            var completedTask = parentTask.ContinueWith((t) =>
            {
                for(int i = 0; i < t.Result?.Length; i++)
                {
                    Console.WriteLine($"Value: {t.Result[i]}");
                }
            });

            //waits for parent task and all its associated child task (if attachedToParent) to complete.
            completedTask.Wait();

            Console.WriteLine("End...");
        }
    }
}

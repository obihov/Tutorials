using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public class Listing14
    {
        /// <summary>
        /// Demonstrates the Task.WaitAll method
        /// </summary>
        public static void Example1()
        {
            Task<int>[] tasks = new Task<int>[3];

            Task<int> task1 = Task.Run(() => { Thread.Sleep(2000); return 10; });
            tasks[0] = task1;

            Task<int> task2 = Task.Run(() => { Thread.Sleep(4000); return 20; });
            tasks[1] = task2;

            Task<int> task3 = Task.Run(() => { Thread.Sleep(6000); return 30; });
            tasks[2] = task3;

            Task.WaitAll(tasks);    //WaitAll does not return anything. Instead (just like Thread.Join), WaitAll will block the main thread until all tasks have completed.

            //process completed task
            foreach(var task in tasks)
            {
                Console.WriteLine($"Value: {task.Result}");
            }
        }


        /// <summary>
        /// Demonstrates the Task.WaitAny method
        /// </summary>
        public static void Example2()
        {
            Task<int>[] tasks = new Task<int>[3];

            Task<int> task1 = Task.Run(() => { Thread.Sleep(2000); return 10; });
            tasks[0] = task1;

            Task<int> task2 = Task.Run(() => { Thread.Sleep(4000); return 20; });
            tasks[1] = task2;

            Task<int> task3 = Task.Run(() => { Thread.Sleep(6000); return 30; });
            tasks[2] = task3;

            
            
            //process completed task
            while(tasks.Length > 0)
            {
                var index = Task.WaitAny(tasks); //WaitAny returns an integer value that can used as an identifier in an array collection to determine a completed task. Use an iteration approach to execute multiple WaitAny calls on the collection as was done in this example.
                var completedTask = tasks[index];
                Console.WriteLine(completedTask.Result);

                var copy = tasks.ToList();
                copy.RemoveAt(index);
                tasks = copy.ToArray();
            }
        }


        /// <summary>
        /// Demonstrates the Task.WhenAll method - used to follow up completed tasks with a continuation method
        /// </summary>
        public static void Example3()
        {
            Task<int>[] tasks = new Task<int>[3];

            Task<int> task1 = Task.Run(() => { Thread.Sleep(2000); return 10; });
            tasks[0] = task1;

            Task<int> task2 = Task.Run(() => { Thread.Sleep(4000); return 20; });
            tasks[1] = task2;

            Task<int> task3 = Task.Run(() => { Thread.Sleep(6000); return 30; });
            tasks[2] = task3;

            //process completed task
            Task.WhenAll(tasks)
                .ContinueWith((t) =>
                {
                    var completedTasks = t.Result; //WhenAll would return all completed tasks at once and pass to a continuation task method.
                    for (int i = 0; i < completedTasks.Length; i++)
                    {
                        Console.WriteLine($"Value: {completedTasks[i]}");
                    }
                })
                .Wait(); //make sure to add a Wait or make task to run as a foreground task instead to keep application from closing.  
        }


        /// <summary>
        /// Demonstrates the Task.WhenAny method - used to follow up completed tasks with a continuation method
        /// </summary>
        public static void Example4()
        {
            Task<int>[] tasks = new Task<int>[3];

            Task<int> task1 = Task.Run(() => { Thread.Sleep(2000); return 10; });
            tasks[0] = task1;

            Task<int> task2 = Task.Run(() => { Thread.Sleep(4000); return 20; });
            tasks[1] = task2;

            Task<int> task3 = Task.Run(() => { Thread.Sleep(6000); return 30; });
            tasks[2] = task3;

            //process completed task
            while(tasks.Length > 0)
            {
                Task.WhenAny(tasks)
                .ContinueWith((t) =>
                {
                    var completedTask = t.Result;   //WhenAny would return only a single finished task at a time and pass to a continuation task method.
                    Console.WriteLine($"Value: {completedTask.Result}");

                    var copy = tasks.ToList();
                    copy.Remove(completedTask); //There's removeAll, removeAt, removeRange if you need them as well.
                    tasks = copy.ToArray();
                })
                .Wait(); //make sure to add a Wait or make task to run as a foreground task instead to keep application from closing.  
            }
            
        }
    }
}

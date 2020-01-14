using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSCAChapter1.TaskTutorial
{
    class TaskExample1
    {
        [ThreadStatic]
        public static Thread CurrentThread;

        public static bool CountNumbers()
        {
            var completed = false;
            for (int i = 0; i <= 10000; i++)
            {
                if (i < 10000)
                    completed = false;
                else
                {
                    completed = true;
                    break;
                }
            }

            return completed;
        }

        /// <summary>
        /// Task is created and started on the fly using the static Run class.
        /// </summary>
        public static void ExecuteTask1()
        {
            //New thread is created with a workitem/task to execute.
            Task<bool> CountNumberTask = Task.Run(() =>
            {
                return CountNumbers();
            });
            
            //Like Thread.Join, pauses main thread or calling thread to wait until active thread is completed
            CountNumberTask.Wait();

            //After Wait on child thread is completed, main thread resumes. 
            //Note, calling CountNumberTask.Result would also make the main thread wait. It would be like saying CountNumberTask.Wait();
            Console.WriteLine("Main thread resumed..");
        }


        /// <summary>
        /// Task is created using the a new Task instance, then started.
        /// </summary>
        public static void ExecuteTask2()
        {
            //New thread is created with a workitem/task to execute.
            Task<bool> CountNumberTask = new Task<bool>(() =>
            {
                return CountNumbers();
            }, TaskCreationOptions.None);

            //Starts new thread
            CountNumberTask.Start();

            //Like Thread.Join, pauses main thread or calling thread to wait until active thread is completed
            CountNumberTask.Wait();

            //After Wait on child thread is completed, main thread resumes. 
            //Note, calling CountNumberTask.Result would also make the main thread wait. It would be like saying CountNumberTask.Wait();
            Console.WriteLine("Main thread resumed..");
        }

        /// <summary>
        /// Get task result. This automatically issues a Wait on the calling thread (ie main thread in this case) until the task itself is complete (i.e Task.Status is completed)
        /// </summary>
        public static void ExecuteTask3()
        {
            //New thread is created with a workitem/task to execute.
            Task<bool> CountNumberTask = Task.Run(() =>
            {
                return CountNumbers();
            });

            //Main thread calls on the result of a Task. This makes main thread to automatically be placed on hold or WAIT.
            //Same as if CountNumberTask.Wait() was executed.
            Console.WriteLine("CountNumberTask successfully completed: " +CountNumberTask.Result);
        }

        /// <summary>
        /// Use ContinueWith task method to Synchronously following up with a Run task or task instance after it's completed.
        /// </summary>
        public static void ExecuteTask4()
        {
            //New thread is created with a workitem/task to execute.
            Task<bool> CountNumberTask = Task.Run(() =>
            {
                return CountNumbers();
            });

            //ContinueWith will wait and only execute after CountNumberTask is finished.
            //This call unlike CountNumberTask.Result or CountNumberTask.Wait will not halt the main thread.
            //However, a ContinueWith is considered to be a foreground thread and so the app doesn't close until it is executed.
            CountNumberTask.ContinueWith((i) => Console.WriteLine("CountNumberTask successfully completed: " + i.Result), TaskContinuationOptions.OnlyOnRanToCompletion);

            //Can check the status of a task this way using the IsCompleted
            //Task task = CountNumberTask.ContinueWith((i) => Console.WriteLine("CountNumberTask successfully completed: " + i.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
            //var isCompleted = task.IsCompleted;
        }

        /// <summary>
        /// Check the status of a task. Note Run is a task, also ContinueWith is as well. Thanks to Builder method.
        /// </summary>
        public static void ExecuteTask5()
        {
            //Can check the status of a task this way using the IsCompleted. This done with any Task item regardless of whether it returns a value or not
            Task<string> runTask = Task.Run(() =>
            {
                return "Obi";
            });

            var isRunTaskFinished = runTask.IsCompleted;

            Task continuewithTask = runTask.ContinueWith((i) => Console.WriteLine("My name is: " + i.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
            var isContinuewithTaskFinished = continuewithTask.IsCompleted;
        }

        /// <summary>
        /// Using the TaskContinuationOptions to wait on a running task and execute different scenarios on what would happen immediately after the Run task is completed.
        /// Avoid chaining multiple continuewith methods on a Task unless you want to continuewith on another continuewith task immidiately after it's done.
        /// </summary>
        public static void ExecuteTask6()
        {
            //Run new Task. Note read more about using a Cancellation token as second parameter to the Run task method.
            Task<string> runTask = Task.Run(() =>
            {
                return "Obi";
            });

            //When task encounters an error
            runTask.ContinueWith((i) => Console.WriteLine("Some fault occured."), TaskContinuationOptions.OnlyOnFaulted);

            //When tast is canceled
            runTask.ContinueWith((i) => Console.WriteLine("Task was abrubtly canceled"), TaskContinuationOptions.OnlyOnCanceled);

            //When task is completed
            Task continuewithTask = runTask.ContinueWith((i) => Console.WriteLine("My name is: " + i.Result), TaskContinuationOptions.OnlyOnRanToCompletion);            
        }

        /// <summary>
        /// Using Task and async modifiers to await a task and to also capture its result when done.
        /// </summary>
        public static async Task ExecuteTask7()
        {
            //New thread is created with a workitem/task to execute.
            Task<bool> runTask = Task.Run(() =>
            {
                return CountNumbers();
            });

            //Because the above runTask returns a value, we can await it and then capture its result. Similar to runTask.Result when used in a non-async Task method.
            var runTaskIsCompleted = await runTask;

            //Because the below ContinueWith task returns a value, we can await it and then capture its result. Similar to ushering a ContinueWith().Result
            var continueWithIsCompleted = await runTask.ContinueWith((i) => i.Result, TaskContinuationOptions.OnlyOnRanToCompletion);

            //some tasks do not produce a result. in other words they are void task types and results for this kind of tasks can't be captured in a variable after awaited.
            await runTask.ContinueWith((i) => Console.WriteLine(i.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
        }        
    }
}

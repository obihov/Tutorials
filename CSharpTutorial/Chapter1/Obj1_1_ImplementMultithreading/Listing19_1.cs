using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public class CoffeeMaker
    {
        #region Non-Task methods
        private bool AddCoffee()
        {
            Thread.Sleep(2000);
            Console.WriteLine("coffee added");
            return true;
        }

        private bool AddMilk()
        {
            Thread.Sleep(2000);
            Console.WriteLine("milk added");
            return true;
        }

        private bool BoilWater()
        {
            Thread.Sleep(10000);
            Console.WriteLine("water boiled");
            return true;
        }
        #endregion

        #region Task methods
        private Task<bool> AddCoffeeTask()
        {
            var task = Task.Run<bool>(() =>
            {
                var coffeeIsAdded = AddCoffee();
                return coffeeIsAdded;
            });
            return task;
        }

        private Task<bool> AddMilkTask()
        {
            var task = Task.Run<bool>(() =>
            {
                var milkIsAdded = AddMilk();
                return milkIsAdded;
            });
            return task;
        }

        private Task<bool> BoilWaterTask()
        {
            var task = Task.Run<bool>(() =>
            {
                var waterIsBoiled = BoilWater();
                return waterIsBoiled;
            });
            return task;
        }
        #endregion

        #region Async method
        private async Task<bool> AddCoffeeAsync()
        {
            var isComplete = await AddCoffeeTask();
            return isComplete;
        }

        private async Task<bool> AddMilkAsync()
        {
            var isComplete = await AddMilkTask();
            return isComplete;
        }

        private async Task<bool> BoilWaterAsync()
        {
            var isComplete = await BoilWaterTask();
            return isComplete;
        }
        #endregion

        /// <summary>
        /// This method uses a single task to check the IsCompleted property of the GetAwaiter method.
        /// If all async methods isCompleted, then the task finishes the brewing process.
        /// </summary>
        public void BrewMethod1()
        {
            var coffeeReady = AddCoffeeAsync().GetAwaiter();
            var milkReady = AddMilkAsync().GetAwaiter();
            var waterReady = BoilWaterAsync().GetAwaiter();

            Task v = Task.Run(() =>
            {
                do
                {
                    Thread.Sleep(1000);
                } while (!coffeeReady.IsCompleted || !milkReady.IsCompleted || !waterReady.IsCompleted);

                Console.WriteLine("Brewing coffee now...");
                Thread.Sleep(3000);
                Console.WriteLine("Coffee Brewed. Enjoy.");
            });

            Console.ReadKey();
        }

        /// <summary>
        /// This method uses three tasks. Each task runs an asycn method and retrieves result, then a WhenAll is used to finish brewing process.
        /// The use of task will help prevent the thread from blocking when waiting on the result.
        /// </summary>
        public void BrewMethod2()
        {
            //without the use of Task.Run, the execution of GetResult would have blocked the thread.
            //all execution of GetResult will run at the same time, thanks to the Task.Run call.

            Task coffee = Task.Run(() =>
            {
                AddCoffeeAsync().GetAwaiter().GetResult();
            });

            Task milk = Task.Run(() =>
            {
                AddMilkAsync().GetAwaiter().GetResult();
            });

            Task water = Task.Run(() =>
            {
                BoilWaterAsync().GetAwaiter().GetResult();
            });

            //When all tasks are completed with results.
            //Be sure a 
            Task.WhenAll(coffee, milk, water)
                .ContinueWith(t =>
                {
                    if (!t.IsCompleted)
                    {
                        Console.WriteLine("Failed to brew coffee.");
                    }
                    else
                    {
                        Console.WriteLine("Brewing coffee now...");
                        Thread.Sleep(3000);
                        Console.WriteLine("Coffee Brewed. Enjoy.");
                    }

                });

            Console.ReadKey();
        }
    }
}

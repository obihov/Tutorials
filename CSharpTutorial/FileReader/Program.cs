using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteToFile("somefileA.txt", "My name is Obinna").Wait();

            //OR do the below useful when working with ConfigureTaskAwaitable<...> methods

            WriteToFile("somefileB.txt", "My name is Obinna")
                .GetAwaiter()
                .GetResult();
        }

        async public static Task WriteToFile(string filename, string content)
        {
            using (FileStream connection = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 4096, useAsync: true))
            {
                byte[] encodedContent = Encoding.Unicode.GetBytes(content);
                
                //I/O bound task write to file, await these kind of task.
                //So thread only used after the write I/O operation is completed.
                await connection
                    .WriteAsync(encodedContent, 0, encodedContent.Length)
                    .ContinueWith((i) => 
                    {
                        Console.WriteLine($"File Content written to {filename}");
                    })
                    .ConfigureAwait(false);

                /*
                 * Using the ConfigureAwait(false) will prevent the thread that is used to run the above task from 
                 * flowing back into (or restore) the main UI thread that's used in actually running and keeping the application alive.
                 * This is good since you're task isn't creating data result on one thread that you need to use to update an object or variable in your UI thread.
                 * Allows your thread to be detached from the UI thread, which increases performance.
                 * */
            }
        }


    }
}

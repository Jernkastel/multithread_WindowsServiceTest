using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Remoting.Contexts;

namespace APR400_lab4_max_jern
{
    [Synchronization]
    class Printer
    {
        static readonly object lockTest = new object();
        static bool test;
        public static void PrintNumbers()
        {
            //Testing out the thread primitive 'lock'
            lock (lockTest)
            {
                if (!test)
                {
                    Console.WriteLine("Executing lock test...");
                    Console.WriteLine("Done!");
                    test = true;
                }
            }
            //Testing out the 'Synchronization' keyword
            SyncTest();

            //Prints thread information
            Console.WriteLine("Thread " + "'" + Thread.CurrentThread.Name + "'" + " is running with " + "'" + Thread.CurrentThread.Priority + "'" + " priority." + " Status: " + Thread.CurrentThread.ThreadState);

            //Initializing random number
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(rnd.Next(500, 4000));
                Console.Write(rnd.Next(1, 10) + " ");
            }

                     
        }

        /*
        ARRAY METHOD - NO LONGER IN USE
        public static void PrintArray(object i)
        {
            Console.WriteLine("This is thread nr: " + Thread.CurrentThread.ManagedThreadId);
        }
        */
        //Prints threadpool data
        public static void PrintPool(object i)
        {
            Console.WriteLine("Thread pool number " + i + ". Managed by thread " + Thread.CurrentThread.ManagedThreadId + ".");
        }

        public static void SyncTest()
        {
            lock (lockTest)
            {
                Console.WriteLine("Starting synchronized resource access on " + Thread.CurrentThread.Name);
                if (Thread.CurrentThread.ManagedThreadId % 2 == 0)
                    Thread.Sleep(2000);

                Thread.Sleep(200);
                Console.WriteLine("Stopping synchronized resource access on  " + Thread.CurrentThread.Name,
                                  Thread.CurrentThread.ManagedThreadId);
            }
        }
    }
}

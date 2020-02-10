using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace APR400_lab4_max_jern
{
    class Program
    {
        static void Main()
        {
            //Initializing threads
            Printer test = new Printer();
            Thread th1 = new Thread(new ThreadStart(Printer.PrintNumbers));
            Thread th2 = new Thread(Printer.PrintNumbers);
            Thread.CurrentThread.Name = "MainThread";
            th1.Name = "PrintThread_Main";
            th2.Name = "PrintThread_Background";
            th2.IsBackground = true;

            int c = 0;
            Console.WriteLine("Press '1' to run the PrintNumbers method");
            Console.WriteLine("Press '2' to run the PrintNumbers method with multiple threads.");
            Console.WriteLine("Press '3' to print threadpool.");
            c = int.Parse(Console.ReadLine());
            //Barebones exception handling with switch case
            try
            {
                switch (c)
                {
                    case 1:
                        //Runs print method
                        Console.Clear();
                        Printer.PrintNumbers();
                        break;
                    case 2:
                        //Runs print method on two threads
                        Console.Clear();
                        th2.Start();
                        th1.Start();
                        break;
                    case 3:
                        for (int i = 0; i < 10; i++)
                        {
                            ThreadPool.QueueUserWorkItem(Printer.PrintPool, i);
                        }
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Only '1', '2', or '3' are recognizable commands.");
                        Console.WriteLine("Restart console to try again.");
                        break;
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

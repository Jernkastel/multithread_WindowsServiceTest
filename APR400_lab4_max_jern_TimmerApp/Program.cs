using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APR400_lab4_max_jern_TimmerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int c = 0;
            Console.WriteLine("Press '1' to run the TimerEvern method.");
            Console.WriteLine("Press '2' to run TextReader method.");
            c = int.Parse(Console.ReadLine());
            //Barebones exception handling with switch case
            try
            {
                switch (c)
                {
                    case 1:
                        Console.Clear();
                        TimerClass.TimerEvent();
                        break;
                    case 2:
                        Console.Clear();
                        ReaderClass.StreamReader();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Only '1' and '2' are recognizable commands.");
                        Console.WriteLine("Restart console to try again.");
                        break;
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
            }
            Console.ReadKey();

        }
    }
}

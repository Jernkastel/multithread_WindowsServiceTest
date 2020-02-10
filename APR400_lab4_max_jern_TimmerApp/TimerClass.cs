using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APR400_lab4_max_jern_TimmerApp
{
    class TimerClass
    {
        //Starts a simple timer event
        public static void TimerEvent()
        {
            Timer timer = new Timer((x) =>
            {
                Console.WriteLine($"{DateTime.Now.ToString()}");
            }, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));

            Console.ReadKey();
        }
    }
}

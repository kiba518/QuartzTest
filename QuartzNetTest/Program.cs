using Jops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzNetTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ScheduleControler.Init().GetAwaiter().GetResult();
            ScheduleControler.PushJop<HelloJob>("HelloWord", 3);
            ScheduleControler.PushJop<CronJop>("CronJop", CronString.StartCustom2);
            ScheduleControler.Run().GetAwaiter().GetResult();
            var info = Console.ReadKey();
            if (info.Key == ConsoleKey.Enter)
            {
                ScheduleControler.Shutdown().GetAwaiter().GetResult();
                Console.WriteLine("结束");
            }
            Console.Read();
        }
    }
}

using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jops
{ 
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Task task = new Task(() => {
                LogicMethod(context);
            });
            task.Start();
            await task;
        }
        public void LogicMethod(IJobExecutionContext context)
        { 
            //context.JobDetail.Key = 默认组.HelloJob
            Console.Out.WriteLine($"HelloJob DateTime:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}  Key:{context.JobDetail.Key} ");
        }
    }
}

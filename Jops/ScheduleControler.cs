using Quartz;
using Quartz.Impl;
using Quartz.Impl.Calendar;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jops
{
    public class ScheduleControler

    {
        private static IScheduler scheduler;
        private static Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>> dicJop = new Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>>();
        
        private static int triggerId = 0;
        private static string defaultGroupName = "默认组";
        /// <summary>
        /// 初始化调度器
        /// </summary>
        /// <returns></returns>
        public static async Task Init()
        {
            try
            {
                //quartz.config配置文件里的键值对
                //NameValueCollection props = new NameValueCollection
                //{
                //   { "quartz.serializer.type", "binary" }
                //};
                StdSchedulerFactory factory = new StdSchedulerFactory(); 
                scheduler = await factory.GetScheduler(); 
                await scheduler.Start(); 
            }
            catch (SchedulerException se)
            {
                System.Console.WriteLine(se);
            }
        }

        /// <summary>
        /// 运行调度器任务
        /// </summary>
        /// <returns></returns>
        public static async Task Run()
        {
            try
            {
                await scheduler.ScheduleJobs(dicJop, true);

            }
            catch (SchedulerException se)
            {
                System.Console.WriteLine(se);
            }
        }
        /// <summary>
        /// 关闭调度器
        /// </summary>
        /// <returns></returns>
        public static async Task Shutdown()
        {
            try
            {
                await scheduler.Shutdown();

            }
            catch (SchedulerException se)
            {
                System.Console.WriteLine(se);
            }
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <typeparam name="T">任务类型，继承Ijop</typeparam>
        /// <param name="jopName">任务名</param>
        /// <param name="Interval">运行间隔时间/秒**最小为1秒</param>
        /// <param name="period">等待启动时间/秒**-1为马上启动</param>
        /// <param name="repeatTime">重复次数**-1为永远运行</param>
        /// <param name="endAt">在指定时间后结束/秒**0为不指定结束时间，默认值0</param>
        public static void PushJop<T>(string jopName, int Interval, int period=-1,int repeatTime=-1,int endAt=0)  where T:IJob
        {
            try
            {
                if (Interval <= 0)
                {
                    Interval = 1;
                }
                if (period < -1)
                {
                    period = -1;
                }
                if (repeatTime < -1)
                {
                    repeatTime = -1;
                } 
                if (endAt < 0)
                {
                    endAt = -1;
                }

                IJobDetail job = JobBuilder.Create<T>().WithIdentity(jopName, defaultGroupName).UsingJobData("Name", "IJobDetail").Build();

            
                var triggerBuilder  = TriggerBuilder.Create().WithIdentity($"{jopName}.trigger{triggerId}", defaultGroupName);
                 
               
                if (period == -1)
                {
                    triggerBuilder = triggerBuilder.StartNow();
                }
                else
                {
                    DateTimeOffset dateTimeOffset = DateTimeOffset.Now.AddSeconds(period);
                    triggerBuilder = triggerBuilder.StartAt(dateTimeOffset);
                }
                if (endAt > 0)
                {
                    triggerBuilder = triggerBuilder.EndAt(new DateTimeOffset(DateTime.Now.AddSeconds(endAt)));
                }  

                if (repeatTime == -1)
                {
                    triggerBuilder = triggerBuilder.WithSimpleSchedule(x => x.WithIntervalInSeconds(Interval).RepeatForever());  
                }
                else
                {
                    triggerBuilder = triggerBuilder.WithSimpleSchedule(x => x.WithRepeatCount(Interval).WithRepeatCount(repeatTime));
                }


                ITrigger trigger = triggerBuilder.UsingJobData("Name", "ITrigger")
                     .WithPriority(triggerId)//设置触发器优先级,当有多个触发器在相同时间出发时,优先级最高[数字最大]的优先
                     .Build(); 

                dicJop.Add(job, new HashSet<ITrigger>() { trigger }); 
                triggerId++; 
            }
            catch (SchedulerException se)
            {
                System.Console.WriteLine(se);
            }
        }

        public static void PushJop<T>(string jopName, string cronExpress) where T : IJob
        {
            try
            { 
                IJobDetail job = JobBuilder.Create<T>().WithIdentity(jopName, defaultGroupName).UsingJobData("Name", "IJobDetail").Build(); 
                ITrigger trigger = TriggerBuilder.Create()
                   .WithIdentity($"{jopName}.trigger{triggerId}", defaultGroupName)
                   .WithCronSchedule(cronExpress)
                   .ForJob(job)
                   .Build(); 
                dicJop.Add(job, new HashSet<ITrigger>() { trigger });
                triggerId++;
            }
            catch (SchedulerException se)
            {
                System.Console.WriteLine(se);
            }
        }
        

      
    }
}

using AiXiu.WebSite.App_Code;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace AiXiu.WebSite
{
    public class Global : System.Web.HttpApplication
    {
        private IScheduler scheduler;
        protected async void Application_Start(object sender, EventArgs e)
        {
            // 启动调度器
            StdSchedulerFactory factory = new StdSchedulerFactory();
            scheduler = await factory.GetScheduler();
            await scheduler.Start();
            IJobDetail job = JobBuilder.Create<SyncVideosJob>()
                .WithIdentity("SyncVideosJob", "Video")
                .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("SyncVideosTrigger", "Video")
                .StartNow()
                .WithSimpleSchedule(m => m
                    .WithIntervalInSeconds(20)
                    .RepeatForever())
                .Build();
            await scheduler.ScheduleJob(job, trigger);
        }
        protected async void Application_End(object sender, EventArgs e)
        {
            await scheduler.Shutdown();
        }
    }
}
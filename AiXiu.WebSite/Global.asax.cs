using AiXiu.WebSite.App_Code;
using log4net;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace AiXiu.WebSite
{
    public class Global : System.Web.HttpApplication
    {
        private IScheduler scheduler;
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);        
        protected async void Application_Start(object sender, EventArgs e)
        {
            // 启用日志记录
            FileInfo fileinfo = new FileInfo(Server.MapPath("~/log4net.Config"));
            log4net.Config.XmlConfigurator.Configure(fileinfo);
            log.Info("网站己启动......");
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
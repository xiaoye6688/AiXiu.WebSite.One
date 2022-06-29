using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AiXiu.BLL;
using AiXiu.IBLL;
using Quartz;

namespace AiXiu.WebSite.App_Code
{
    public class SyncVideosJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            IVideoManager videoManager = new VideoManager();
            await videoManager.SyncVideos();
        }
    }
}
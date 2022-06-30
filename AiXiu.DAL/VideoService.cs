using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiXiu.IDAL;
using AiXiu.Model;

namespace AiXiu.DAL
{
    public class VideoService : IVideoService
    {


        public bool AddVideo(string VideoId, int UserId, string Headline, string Location)
        {
            AiXiuDB db = new AiXiuDB();
            TBVideos tBVideosDB = new TBVideos();
            tBVideosDB.VideoId = VideoId;
            tBVideosDB.UserId = UserId;
            tBVideosDB.Headline = Headline;
            tBVideosDB.Location = Location;
            tBVideosDB.Status = (int)VideoStatus.Uploading;
            tBVideosDB.UploadTime = DateTime.Now;
            db.TBVideos.Add(tBVideosDB);
            return db.SaveChanges() > 0;
        }
        public List<string> GetInProcessVideoIds()
        {

            AiXiuDB db = new AiXiuDB();
            List<int> statusList = new List<int>
            {
                 (int)VideoStatus.Uploading,
                 (int)VideoStatus.Transcoding
            };
            return db.TBVideos.Where(e => statusList.Contains((int)e.Status)).Select(e => e.VideoId).ToList();
        }

        public async Task UpdateVideos(List<TBVideos> videos)
        {
            AiXiuDB db = new AiXiuDB();
            TBVideos tBVideoDbModel = new TBVideos();

            foreach (TBVideos item in videos)
            {

                tBVideoDbModel = db.TBVideos.FirstOrDefault(e => e.VideoId == item.VideoId);
                tBVideoDbModel.Status = item.Status;
                if (!string.IsNullOrWhiteSpace(item.CoverURL))
                {
                    tBVideoDbModel.CoverURL = item.CoverURL.Substring(0, item.CoverURL.IndexOf("?"));
                }
                await db.SaveChangesAsync();

            }
        }
        public List<TBVideos> GetVideoList()
        {
            AiXiuDB db = new AiXiuDB();
            List<TBVideos> tBVideos = new List<TBVideos>();
            int status = (int)VideoStatus.Normal;
            tBVideos = db.TBVideos.Where(x => x.Status == status).Include(m => m.TBUsers).OrderByDescending(e => e.UploadTime).ToList();
            return tBVideos;
        }

        public TBVideos GetVideoById(string videoId)
        {
            AiXiuDB db = new AiXiuDB();

            return db.TBVideos.FirstOrDefault(e => e.VideoId == videoId);
        }
        
  
    }
}

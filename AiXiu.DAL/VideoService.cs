using System;
using System.Collections.Generic;
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
            tBVideosDB.UploadTime = DateTime.Now.ToString();
            db.TBVideos.Add(tBVideosDB);
            return db.SaveChanges() > 0;
        }
    }
}

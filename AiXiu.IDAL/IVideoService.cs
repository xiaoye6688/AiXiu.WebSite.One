using AiXiu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiXiu.IDAL
{
    public interface IVideoService
    {
        bool AddVideo(string VideoId, int UserId, string Headline, string Location);
        /// <summary>
        /// 获取处理中的视频Id列表
        /// </summary>
        /// <returns></returns>
        List<string> GetInProcessVideoIds();

        /// <summary>
        /// 批量更新视频信息
        /// </summary>
        /// <param name="videos">要更新的视频列表</param>
        /// <returns></returns>
        Task UpdateVideos(List<TBVideos> videos);

        List<TBVideos> GetVideoList();
        TBVideos GetVideoById(string videoId);
        List<TBVideos> GetVideoListByUserId(int userId);
        void DeleteVideo(string videoId);


    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiXiu.DAL;
using AiXiu.IBLL;
using AiXiu.IDAL;
using AiXiu.Model;

namespace AiXiu.BLL
{
    public class VideoManager : IVideoManager
    {
        public OperResult<GetPlayInfoResult> GetPlayInfoResultById(string videoId)
        {
            IVodService vodService = new VodService();
            return OperResult<GetPlayInfoResult>.Succeed(vodService.GetPlayInfo(videoId));
        }

        public OperResult<CreateUploadVideoResult> GetUploadVideoResult(string filename, string headline, string location, int userId)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                return OperResult<CreateUploadVideoResult>.Failed("filename不能为空");
            }
            if (string.IsNullOrWhiteSpace(headline))
            {
                return OperResult<CreateUploadVideoResult>.Failed("headline不能为空");
            }
            if (string.IsNullOrWhiteSpace(location))
            {
                return OperResult<CreateUploadVideoResult>.Failed("location不能为空");
            }
            if (userId <= 0)
            {
                return OperResult<CreateUploadVideoResult>.Failed("该用户不存在");
            }
            //1、阿里云
            IVodService vodService = new VodService();
            CreateUploadVideoResult createUploadVideoResult = vodService.CreateUploadVideo(headline, filename);
            //2、数据库
            IVideoService videoService = new VideoService();
            if (videoService.AddVideo(createUploadVideoResult.VideoId, userId, headline, location))
            {
                return OperResult<CreateUploadVideoResult>.Succeed(createUploadVideoResult);
            }
            else
            {
                return OperResult<CreateUploadVideoResult>.Failed();
            }

        }

        public OperResult<TBVideos> GetVideoById(string videoId)
        {
            IVideoService videoService = new VideoService();
            return OperResult<TBVideos>.Succeed(videoService.GetVideoById(videoId));
        }

        public OperResult<List<TBVideos>> GetVideoList()
        {
            IVideoService videoService = new VideoService();
            List<TBVideos> videoList = videoService.GetVideoList();
            return OperResult<List<TBVideos>>.Succeed(videoList);
        }

        public OperResult<List<TBVideos>> GetVideoListById(int userId)
        {
            IVideoService videoService = new VideoService();
            List<TBVideos> videoList = videoService.GetVideoListByUserId(userId);
            return OperResult<List<TBVideos>>.Succeed(videoList);
        }

        public async Task<OperResult<int>> SyncVideos()
        {
            //1、获取待处理的视频id列表
            //2、调用阿里云视频点播接口得到 这一堆id的对应视频信息
            //3、调用数据库更新视频状态
            IVideoService videoService = new VideoService();
            List<string> videoIdList = videoService.GetInProcessVideoIds();

            IVodService vodService = new VodService();
            GetVideoInfosResult getVideoInfosResult = vodService.GetVideoInfos(videoIdList);

            List<TBVideos> tBVideosList = getVideoInfosResult.VideoList.Select(e => new TBVideos
            {
                CoverURL = e.CoverURL,
                Status = (int)e.Status,
                VideoId = e.VideoId
            }).ToList();
            await videoService.UpdateVideos(tBVideosList);
            OperResult<int> operResult = OperResult<int>.Succeed(tBVideosList.Count);
            return await Task.FromResult(operResult);
        }


    }
}

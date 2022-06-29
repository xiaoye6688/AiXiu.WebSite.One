using AiXiu.IDAL;
using AiXiu.Model;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.vod.Model.V20170321;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;

namespace AiXiu.DAL
{
    /// <summary>
    /// 视频点播数据服务类
    /// </summary>
    public class VodService : IVodService
    {
        private static string regionId;
        private static string accessKeyId;
        private static string accessKeySecret;

        static VodService()
        {
            regionId = ConfigurationManager.AppSettings["aliyun:RegionId"];
            accessKeyId = ConfigurationManager.AppSettings["aliyun:AccessKeyID"];
            accessKeySecret = ConfigurationManager.AppSettings["aliyun:AccessKeySecret"];
        }

        /// <summary>
        /// 初始化视频点播客户端
        /// </summary>
        /// <returns></returns>
        private static DefaultAcsClient InitVodClient()
        {
            IClientProfile profile = DefaultProfile.GetProfile(regionId, accessKeyId, accessKeySecret);
            return new DefaultAcsClient(profile);
        }

        /// <summary>
        /// 获取视频上传地址和凭证
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public CreateUploadVideoResult CreateUploadVideo(string title, string fileName)
        {
            CreateUploadVideoRequest createRequest = new CreateUploadVideoRequest();
            createRequest.Title = title;
            createRequest.FileName = fileName;
            DefaultAcsClient acsClient = InitVodClient();
            CreateUploadVideoResponse createResponse = acsClient.GetAcsResponse(createRequest);
            return new CreateUploadVideoResult()
            {
                VideoId = createResponse.VideoId,
                UploadAddress = createResponse.UploadAddress,
                UploadAuth = createResponse.UploadAuth
            };
        }

        /// <summary>
        /// 批量获取视频信息
        /// </summary>
        /// <param name="videoIds"></param>
        /// <returns></returns>
        public GetVideoInfosResult GetVideoInfos(IEnumerable<string> videoIds)
        {
            if (videoIds.Count() > 20)
            {
                throw new Exception("批量获取视频信息单次不能超过20个");
            }
            GetVideoInfosRequest getRequest = new GetVideoInfosRequest();
            getRequest.VideoIds = string.Join(",", videoIds);
            DefaultAcsClient acsClient = InitVodClient();
            GetVideoInfosResponse getResponse = acsClient.GetAcsResponse(getRequest);
            List<VideoInfo> videos;
            if (getResponse.VideoList.Count > 0)
            {
                videos = getResponse.VideoList.Select(m => new VideoInfo()
                {
                    VideoId = m.VideoId,
                    Title = m.Title,
                    CoverURL = m.CoverURL,
                    Status = (VideoStatus)Enum.Parse(typeof(VideoStatus), m.Status)
                }).ToList();
            }
            else
            {
                videos = new List<VideoInfo>();
            }
            return new GetVideoInfosResult()
            {
                NonExistVideoIds = getResponse.NonExistVideoIds,
                VideoList = videos
            };
        }

        /// <summary>
        /// 获取视频播放凭证
        /// </summary>
        /// <param name="videoId">视频Id</param>
        /// <returns></returns>
        public GetPlayInfoResult GetPlayInfo(string videoId)
        {
            GetPlayInfoRequest getRequest = new GetPlayInfoRequest();
            getRequest.VideoId = videoId;
            DefaultAcsClient acsClient = InitVodClient();
            GetPlayInfoResponse getResponse = acsClient.GetAcsResponse(getRequest);
            return new GetPlayInfoResult()
            {
                CoverURL = getResponse.VideoBase.CoverURL,
                PlayURL = getResponse.PlayInfoList[0].PlayURL
            };
        }

        /// <summary>
        /// 删除完整视频
        /// </summary>
        /// <param name="videoIds">视频Id列表</param>
        public void DeleteVideos(List<string> videoIds)
        {
            if (videoIds.Count() > 20)
            {
                throw new Exception("批量删除视频单次不能超过20个");
            }
            DeleteVideoRequest deleteRequest = new DeleteVideoRequest();
            deleteRequest.VideoIds = string.Join(",",videoIds);
            DefaultAcsClient acsClient = InitVodClient();
            acsClient.GetAcsResponse(deleteRequest);
        }

       
    }
}
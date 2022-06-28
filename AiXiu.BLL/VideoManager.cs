using System;
using System.Collections.Generic;
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
    }
}

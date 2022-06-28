using AiXiu.Model;
using System.Collections.Generic;

namespace AiXiu.IDAL
{
    /// <summary>
    /// 视频点播数据服务接口
    /// </summary>
    public interface IVodService
    {
        /// <summary>
        /// 获取视频上传地址和凭证
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        CreateUploadVideoResult CreateUploadVideo(string title, string fileName);

        /// <summary>
        /// 批量获取视频信息
        /// </summary>
        /// <param name="videoIds"></param>
        /// <returns></returns>
        GetVideoInfosResult GetVideoInfos(IEnumerable<string> videoIds);

        /// <summary>
        /// 获取视频播放地址
        /// </summary>
        /// <param name="videoId">视频Id</param>
        /// <returns></returns>
        GetPlayInfoResult GetPlayInfo(string videoId);

        /// <summary>
        /// 删除完整视频
        /// </summary>
        /// <param name="videoIds">视频Id列表</param>
        void DeleteVideos(List<string> videoIds);
    }
}
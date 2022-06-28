using System.Collections.Generic;

namespace AiXiu.Model
{
    /// <summary>
    /// 批量获取视频信息结果类
    /// </summary>
    public class GetVideoInfosResult
    {
        /// <summary>
        /// 不存在的视频ID列表
        /// </summary>
        public List<string> NonExistVideoIds { get; set; }

        /// <summary>
        /// 视频信息列表
        /// </summary>
        public List<VideoInfo> VideoList { get; set; }
    }
}
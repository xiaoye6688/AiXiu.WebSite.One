namespace AiXiu.Model
{
    /// <summary>
    /// 视频信息类
    /// </summary>
    public class VideoInfo
    {
        /// <summary>
        /// 视频Id
        /// </summary>
        public string VideoId { get; set; }

        /// <summary>
        /// 视频标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 视频封面
        /// </summary>
        public string CoverURL { get; set; }

        /// <summary>
        /// 视频状态
        /// </summary>
        public VideoStatus Status { get; set; }
    }
}
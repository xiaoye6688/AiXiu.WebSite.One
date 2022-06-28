namespace AiXiu.Model
{
    /// <summary>
    /// 获取视频播放地址结果类
    /// </summary>
    public class GetPlayInfoResult
    {
        /// <summary>
        /// 视频播放
        /// </summary>
        public string CoverURL { get; set; }

        /// <summary>
        /// 视频流的播放地址
        /// </summary>
        public string PlayURL { get; set; }
    }
}
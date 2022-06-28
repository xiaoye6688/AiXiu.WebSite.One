namespace AiXiu.Model
{
    /// <summary>
    /// 媒体上传地址和凭证结果类
    /// </summary>
    public class CreateUploadVideoResult
    {
        public string VideoId { get; set; }
        public string UploadAddress { get; set; }
        public string UploadAuth { get; set; }
    }
}
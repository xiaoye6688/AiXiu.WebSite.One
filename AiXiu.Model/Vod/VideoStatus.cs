namespace AiXiu.Model
{
    /// <summary>
    /// 视频处理状态
    /// </summary>
    public enum VideoStatus
    {
        Unknown = 0,
        
        /// <summary>上传中</summary>
        Uploading = 1,

        /// <summary>上传失败</summary>
        UploadFail = 2,

        /// <summary>上传完成</summary>
        UploadSucces = 3,

        /// <summary>转码中</summary>
        Transcoding = 4,

        /// <summary>转码失败</summary>
        TranscodeFail = 5,

        /// <summary>屏蔽</summary>
        Blocked = 6,

        /// <summary>正常</summary>
        Normal = 7
    }
}
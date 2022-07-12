using AiXiu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiXiu.IBLL
{
    public interface IVideoManager
    {
        OperResult<CreateUploadVideoResult> GetUploadVideoResult(string filename, string headline, string location, int userId);
        Task<OperResult<int>> SyncVideos();

        OperResult<List<TBVideos>> GetVideoList();
        OperResult<List<TBVideos>> GetVideoListById(int userId);

        OperResult<TBVideos> GetVideoById(string videoId);


        OperResult<GetPlayInfoResult> GetPlayInfoResultById(string videoId);
        void DeleteVideo(string videoId);

    }
}

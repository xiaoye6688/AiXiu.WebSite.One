using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiXiu.IBLL
{
    public interface ILikeManager
    {
        //点赞计数
        long Count(string videoId);
        //是否点赞
        bool IsLike(string videoId, int userId);
        //添加点赞
        bool AddLike(string videoId, int userId);
        //移除点赞
        bool RemoveLike(string videoId, int userId);
    }
}

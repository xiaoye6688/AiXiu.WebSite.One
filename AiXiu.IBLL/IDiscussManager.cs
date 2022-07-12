using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiXiu.Model;

namespace AiXiu.IBLL
{
    public interface IDiscussManager
    {
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="discuss"></param>
        /// <returns></returns>
        bool Add(string videoId, Discuss discuss);
        
        /// <summary>
        /// 评论计数
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        long Count(string videoId);
        
        /// <summary>
        /// 查询评论列表
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<Discuss> QueryDiscuss(string videoId,int pageNumber,int pageSize);
    }
}

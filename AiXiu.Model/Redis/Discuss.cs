using System;

namespace AiXiu.Model
{
    /// <summary>
    /// 评论类
    /// </summary>
    public class Discuss
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 回复到
        /// </summary>
        public string ReplayTo { get; set; }

        /// <summary>
        /// 发表时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}
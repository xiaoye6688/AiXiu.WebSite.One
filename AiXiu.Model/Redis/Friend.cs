namespace AiXiu.Model
{
    /// <summary>
    /// 好友类
    /// </summary>
    public class Friend
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
        /// 最后一条消息
        /// </summary>
        public Message LastMessage { get; set; }

        /// <summary>
        /// 聊天Id
        /// </summary>
        public string ChatId { get; set; }
    }
}
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Play.aspx.cs" Inherits="AiXiu.WebSite.Play" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="//g.alicdn.com/de/prismplayer/2.9.3/skins/default/aliplayer-min.css" />
    <style>
        .fab-title { width: 50%; left: calc(var(--f7-fab-margin) + var(--f7-safe-area-bottom)); bottom: calc(var(--f7-fab-margin) + var(--f7-safe-area-bottom)); }
        .title { width: 100%; margin-bottom: 10rem; background: rgba(0,0,0,0.5); padding: 0.2rem 0.8rem; border-radius: 0.3rem; }
        .avatar { width: 42px; height: 42px; border-radius: 42px; }
        .nickname { width: 4em; text-overflow: ellipsis; }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page" data-page="play">
        <div class="page-content">
            <div class="prism-player" id="player"></div>
        </div>
        <div class="fab fab-title">
            <div class="title text-color-white">
                <p><%=Video.Headline%></p>
                <%
                    if (!string.IsNullOrWhiteSpace(Video.Location))
                    {
                %>
                <p><i class="f7-icons size-14">placemark</i> [<%=Video.Location%>]</p>
                <%  
                    }
                %>
            </div>
        </div>
        <div class="fab fab-right-bottom">
            <a href="Videos.aspx?id=<%=Author.Id%>" class="videos box-shadow-none bg-color-none external">
                <img class="avatar" src="<%=Author.Avatar??"/imgs/avatar.jpg"%>" />
            </a>
            <p class="nickname text-color-white text-align-center no-margin-top"><%=Author.NickName%></p>
            <a href="#" class="like box-shadow-none bg-color-none">
                <i class="like-icon icon f7-icons <%=IsLike?"text-color-pink":"text-color-white"%> size-42">heart_fill</i>
            </a>
            <p class="like-count text-color-white text-align-center no-margin-top"><%=LikeCount%></p>
            <a href="Discuss.aspx?id=<%=VideoId%>" class="discuss box-shadow-none bg-color-none external">
                <i class="discuss-icon icon f7-icons text-color-white size-42">chat_bubble_text</i>
            </a>
            <p class="discuss-count text-color-white text-align-center no-margin-top"><%=DiscussCount%></p>
        </div>
    </div>
</asp:Content>
<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="//g.alicdn.com/de/prismplayer/2.9.3/aliplayer-min.js"></script>
    <script>
        var videoId = '<%=VideoId%>';
        var userId = '<%=UserId%>';
        // 播放视频
        player = new Aliplayer({
            'id': 'player',
            'source': '<%=playInfo.PlayURL%>',      // 播放地址
            'width': '100%',
            'height': '100%',
            'autoplay': true,
            //'autoplay': false,
            'isLive': false,
            'cover': '<%=playInfo.CoverURL%>',      // 封面地址
            'rePlay': false,
            'playsinline': true,
            'preload': true,
            'controlBarVisibility': 'hover',
            'useH5Prism': true
        });
        // 点赞
        var $$likeIcon = $$('.page .like-icon');
        var $$likeCount = $$('.page .like-count');
        $$('.page').on('click', '.like', function () {
            // 未点赞
            if ($$likeIcon.hasClass('text-color-white')) {
                $$likeIcon.removeClass('text-color-white').addClass('text-color-pink');
                $$likeCount.html(parseInt($$('.page .like-count').html()) + 1);
                app.request.get('VideoLikeHandler.ashx', { videoId: videoId, userId: userId, action: 'add' });
            }
            // 已点赞
            else {
                $$likeIcon.removeClass('text-color-pink').addClass('text-color-white');
                $$likeCount.html(parseInt($$('.page .like-count').html()) - 1);
                app.request.get('VideoLikeHandler.ashx', { videoId: videoId, userId: userId, action: 'remove' });
            }
        })
    </script>
</asp:Content>


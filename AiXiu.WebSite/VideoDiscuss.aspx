<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VideoDiscuss.aspx.cs" Inherits="AiXiu.WebSite.VideoDiscuss" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .infinite-discuss .item-subtitle { text-overflow: initial !important; white-space: initial !important; }
        .infinite-discuss .avatar { width: 36px; height: 36px; border-radius: 36px; }
        .reply-sheet .no-border::before { display: none; }
        .reply-sheet .no-border::after { display: none; }
        .reply-content { border: var(--f7-messagebar-textarea-border); }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page" data-page="discuss">
        <div class="toolbar messagebar">
            <div class="toolbar-inner">
                <div class="messagebar-area">
                    <textarea class="discuss-content" placeholder="我来说两句"></textarea>
                </div>
                <a class="link discuss-link" href="#">发表</a>
            </div>
        </div>
        <div class="page-content infinite-scroll-content infinite-discuss">
            <div class="list media-list discuss-list margin-top-none">
                <ul>
                    <li class="tips-none">
                        <div class="block block-strong margin-top-none">
                            <p class="text-align-center text-color-gray">暂无评论</p>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="preloader infinite-scroll-preloader"></div>
        </div>
    </div>
    <!-- 回复弹窗 -->
    <div class="sheet-modal reply-sheet">
        <div class="toolbar">
            <div class="toolbar-inner">
                <div class="left"></div>
                <div class="right"><a class="link sheet-close" href="#">&times;</a></div>
            </div>
        </div>
        <div class="sheet-modal-inner">
            <div class="list">
                <ul class="no-border">
                    <li>
                        <div class="item-content item-input">
                            <div class="item-inner">
                                <div class="item-input-wrap">
                                    <textarea class="reply-content padding-left-half padding-right-half" placeholder="" rows="4"></textarea>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="block block-strong">
                <a class="link reply-link" href="#">发表</a>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="lib/template7/dist/template7.min.js"></script>
    <script id="template" type="text/template7">
        <li>
            <div class="item-content">
                <div class="item-media"><img src="{{ Avatar }}" class="avatar" /></div>
                <div class="item-inner">
                    <div class="item-title">{{ NickName }}</div>
                    <div class="item-subtitle">{{ #if ReplayTo }}<span class="text-color-gray">回复 {{ ReplayTo }}：</span>{{/if}}{{ Content }}</div>
                    <div class="item-text">
                        <span>{{ js 'transformTimestampToString(this.AddTime)' }}</span>
                        <a href="#" class="reply color-gray float-right">回复</a>
                    </div>
                </div>
            </div>
        </li>
    </script>
    <script>
        var videoId = '<%=VideoId%>';
        var nickName = '<%=NickName%>';
        var avatar = '<%=Avatar%>';
        // 滚动加载
        var allowLoad = true;
        var pageNumber = 1;
        var pageSize = 10;
        $$('.infinite-discuss').on('infinite', function () {
            loadDiscuss();
        });
        // 异步加载评论
        loadDiscuss();
        function loadDiscuss() {
            if (!allowLoad) {
                return;
            }
            allowLoad = false;
            var params = {
                id: videoId,
                pageNumber: pageNumber,
                pageSize: pageSize
            }
            // 请求评论列表
            app.request.get('Ashx/DiscussListHandler.ashx', params, function (data) {
                var res = eval('(' + data + ')');
                // 如果加载到数据，则显示
                if (res.length > 0) {
                    for (var i in res) {
                        addDiscuss(res[i], true);
                    }
                    pageNumber++;
                    allowLoad = true;
                }
                // 如果结果长度为0或者小于每页数量，则加载完成，释放滚动加载
                if (res.length == 0 || res.length < pageSize) {
                    app.infiniteScroll.destroy('.infinite-discuss');
                    $$('.infinite-scroll-preloader').remove();
                    allowLoad = false;
                    return;
                }
            });
        }
        // 发表评论
        $$('.discuss-link').on('click', function () {
            var content = $$('.discuss-content').val();
            if (!content) {
                return;
            }
            var discuss = {
                NickName: nickName,
                Avatar: avatar,
                Content: content,
                AddTime: parseInt(new Date().getTime() / 1000)
            }
            addDiscuss(discuss, false);
            app.request.post('Ashx/DiscussPublishHandler.ashx?id=' + videoId, discuss);
            $$('.discuss-content').val('');
        })
        // 回复评论
        var replyNickName = '';
        var replySheet = app.sheet.create({
            el: '.reply-sheet',
            swipeToClose: true,
            backdrop: true
        })
        $$('.infinite-discuss').on('click', '.reply', function () {
            replyNickName = $$(this).parent().siblings('.item-title').html();
            $$('.reply-content').attr('placeholder', '回复：' + replyNickName);
            replySheet.open();
        })
        $$('.reply-link').on('click', function () {
            var content = $$('.reply-content').val();
            if (!content) {
                return;
            }
            var discuss = {
                NickName: nickName,
                Avatar: avatar,
                Content: content,
                ReplayTo: replyNickName,
                AddTime: parseInt(new Date().getTime() / 1000)
            }
            replyNickName = '';
            $$('.reply-content').val('');
            replySheet.close();
            addDiscuss(discuss, false);
            app.request.post('DiscussPublishHandler.ashx?id=' + videoId, discuss);
        })
        // 添加评论到页面
        var template = $$('#template').html();
        var compiledTemplate = Template7.compile(template);
        function addDiscuss(discuss, load) {
            if ($$('.tips-none')) {
                $$('.tips-none').remove();
            }
            var html = compiledTemplate(discuss);
            if (load) {
                $$('.discuss-list ul').append(html);
            }
            else {
                $$('.discuss-list ul').prepend(html);
            }
        }
        function transformTimestampToString(timestamp) {
            var date = new Date(timestamp * 1000);
            return (date.getMonth() + 1) + '-' +
                (date.getDate()) + ' ' +
                (date.getHours()) + ':' +
                (date.getMinutes());
        }
    </script>
</asp:Content>
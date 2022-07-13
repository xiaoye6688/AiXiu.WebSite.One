<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="AiXiu.WebSite.Chat" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .message-avatar { opacity: 1 !important; }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page" data-page="chat">
        <!-- 对方昵称 -->
        <div class="navbar">
            <div class="navbar-inner">
                <div class="title"><%=friend.NickName??"用户"+otherId%></div>
            </div>
        </div>
        <!-- 输入框 -->
        <div class="toolbar messagebar">
            <div class="toolbar-inner">
                <div class="messagebar-area">
                    <textarea id="messageContent" class="resizable" rows="1"></textarea>
                </div>
                <a href="#" id="sendMessage" class="link send-link">发送</a>
            </div>
        </div>
        <!-- 下拉刷新 -->
        <div class="ptr-preloader">
            <div class="preloader"></div>
            <div class="ptr-arrow"></div>
        </div>
        <!-- 消息列表 -->
        <div class="page-content chat">
            <div class="messages" id="messageList"></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="lib/template7/dist/template7.min.js"></script>
    <script id="sentMessage" type="text/template7">
        <div class="message message-sent">
            <div class="message-avatar" style="background-image: url('<%=user.Avatar??"/imgs/avatar.jpg"%>');"></div>
            <div class="message-content">
                <div class="message-bubble">
                    <div class="message-text">{{Content}}</div>
                </div>
            </div>
        </div>
    </script>
    <script id="receivedMessage" type="text/template7">
        <div class="message message-received">
            <div class="message-avatar" style="background-image: url('<%=friend.Avatar??"/imgs/avatar.jpg"%>');"></div>
            <div class="message-content">
                <div class="message-bubble">
                    <div class="message-text">{{Content}}</div>
                </div>
            </div>
        </div>
    </script>
    <script src="Scripts/jquery-3.6.0.min.js"></script>
    <script src="Scripts/jquery.signalR-2.4.1.min.js"></script>
    <script src="signalr/hubs"></script>
    <script>
        var selfId = '<%=selfId%>';
        var otherId = '<%=otherId%>';
        // 下拉加载
        var allowLoad = true;
        var pageNumber = 1;
        var pageSize = 10;
        $$('.chat').on('ptr:refresh', function () {
            loadRecords();
        })
        // 异步加载聊天记录
        loadRecords();
        function loadRecords() {
            if (!allowLoad) {
                return;
            }
            allowLoad = false;
            var params = {
                selfId: selfId,
                otherId: otherId,
                pageNumber: pageNumber,
                pageSize: pageSize
            }
            app.request.get('Ashx/ChatRecordHandler.ashx', params, function (data) {
                var res = eval('(' + data + ')');
                // 如果加载到数据，则显示
                if (res.length > 0) {
                    for (var i = res.length - 1; i >= 0; i--) {
                        addMessageToPage(res[i]);
                    }
                    pageNumber++;
                    allowLoad = true;
                }
                // 如果结果长度为0或者小于每页数量，则加载完成
                if (res.length == 0 || res.length < pageSize) {
                    allowLoad = false;
                    return;
                }
                // 重置下拉加载
                else {
                    app.ptr.done();
                }
            });
        }

        $(function () {
            // 获取集线器
            var chat = $.connection.chatHub;
            // 接收消息
            chat.client.consumerMessage = function (fromId, content, timestamp) {
                // 在页面显示内容
                var message = {
                    UserId: fromId,
                    Content: content,
                    AddTime: timestamp
                }
                addMessageToPage(message);
            }
            // 发送消息
            $.connection.hub.start().done(function () {
                $('#sendMessage').on('click', function () {
                    // 获取内容
                    var content = $('#messageContent').val();
                    if (!content)
                        return;
                    // 发送消息
                    var timestamp = parseInt(new Date().getTime() / 1000);
                    chat.server.sendMessage(selfId, otherId, content, timestamp);
                    $('#messageContent').val('').focus();
                    // 在页面显示内容
                    var message = {
                        UserId: selfId,
                        Content: content,
                        AddTime: timestamp
                    }
                    addMessageToPage(message);
                })
            })
        })

        // 添加消息到页面
        var timestampOrigin = 0;    // 显示时间的时间戳原点
        var timestampSpan = 500;    // 显示时间的时间戳跨度
        var sentTemplate = Template7.compile($$('#sentMessage').html());
        var receivedTemplate = Template7.compile($$('#receivedMessage').html());
        function addMessageToPage(message) {
            // 添加消息时间
            if (message.AddTime - timestampOrigin > timestampSpan) {
                var title = $$('<div class="messages-title"></div>');
                title.html(transformTimestampToString(message.AddTime))
                $$('#messageList').append(title);
                timestampOrigin = message.AddTime;
            }
            // 添加消息内容
            if (message.UserId == selfId) {
                // 本人消息
                $$('#messageList').append(sentTemplate(message));
            }
            else if (message.UserId == otherId) {
                // 对方消息
                $$('#messageList').append(receivedTemplate(message));
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


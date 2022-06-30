<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Videos.aspx.cs" Inherits="AiXiu.WebSite.Videos" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .videos { width: 100%; }
            .videos .item { width: 49%; margin-left: 0.5%; margin-right: 0.5%; float: left; padding-bottom: 0.6em; }
            .videos img { width: 100%; height: auto; }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page" data-page="user-videos">
        <div class="page-content">
            <div class="block-title">TA的个人信息</div>
            <div class="block block-strong personal margin-top-none">
                <div class="row">
                    <div class="col-50">
                        <p class="margin-top-quarter margin-bottom-quarter"><i class="f7-icons size-14 text-color-gray margin-right-half">person</i><asp:Label ID="lblNickName" runat="server" Text=""></asp:Label></p>
                    </div>
                    <div class="col-50">
                        <p class="margin-top-quarter margin-bottom-quarter"><i class="f7-icons size-14 text-color-gray margin-right-half">smiley</i><asp:Label ID="lblSex" runat="server" Text=""></asp:Label></p>
                    </div>
                    <div class="col-50">
                        <p class="margin-top-quarter margin-bottom-quarter"><i class="f7-icons size-14 text-color-gray margin-right-half">placemark</i><asp:Label ID="lblAddress" runat="server" Text=""></asp:Label></p>
                    </div>
                    <div class="col-50">
                        <p class="margin-top-quarter margin-bottom-quarter"><i class="f7-icons size-14 text-color-gray margin-right-half">gift</i><asp:Label ID="lblBirthday" runat="server" Text=""></asp:Label></p>
                    </div>
                    <div class="col-100">
                        <p class="margin-top-quarter margin-bottom-quarter"><i class="f7-icons size-14 text-color-gray margin-right-half">heart</i><asp:Label ID="lblHobby" runat="server" Text=""></asp:Label></p>
                    </div>
                </div>
            </div>
            <div class="block-title">TA发布的视频</div>
            <div class="block block-strong">
                <asp:Repeater ID="rptVideos" runat="server">
                    <HeaderTemplate>
                        <div class="videos">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="item">
                            <a class="link cover external" href="Play.aspx?id=<%#Eval("VideoId")%>">
                                <img src="/imgs/rectangle.jpg" data-src="<%#Eval("CoverURL")%>" class="lazy" />
                            </a>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div class="block block-strong" id="btnFriendBlock" runat="server">
                <asp:Button ID="btnFriend" runat="server" Text="加为好友" CssClass="button button-large button-outline button-fill" OnClick="btnFriend_Click" />
                <asp:Button ID="btnMessage" runat="server" Text="立即聊天" CssClass="button button-large button-outline button-fill" OnClick="btnMessage_Click" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="lib/masonry-layout/dist/masonry.pkgd.min.js"></script>
    <script>
        // 图片懒加载
        app.on('lazyLoaded', function () {
            // 图片流布局
            new Masonry('.videos', {
                itemSelector: '.item',
                columnWidth: '.item',
                percentPosition: true
            })
        })
    </script>
</asp:Content>

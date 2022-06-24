<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AiXiu.WebSite.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .videos {
            width: 100%;
        }

            .videos .item {
                width: 49%;
                margin-left: 0.5%;
                margin-right: 0.5%;
                float: left;
                padding-bottom: 0.6em;
            }

            .videos img {
                width: 100%;
                height: auto;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page" data-page="videos">
        <div class="page-content">
            <div class="block block-strong margin-top-none">
                <asp:Repeater ID="rptVideos" runat="server">
                    <HeaderTemplate>
                        <div class="videos">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="item">
                            <a class="link cover external" href="Play.aspx?id=<%#Eval("VideoId")%>">
                                <img src="/imgs/rectangle.jpg" data-src="<%#Eval("CoverURL")%>" class="lazy" />
                            </a>
                            <div class="chip">
                                <div class="chip-media">
                                    <img src="<%#(Eval("TBUsers.Avatar")??"/imgs/avatar.jpg")%>" />
                                </div>
                                <div class="chip-label"><%#Eval("TBUsers.NickName")%></div>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="toolbar tabbar tabbar-labels toolbar-bottom">
            <div class="toolbar-inner">
                <a href="Default.aspx" class="tab-link external tab-link-active">
                    <i class="icon f7-icons size-29">videocam</i>
                    <span class="tabbar-label">视频</span>
                </a>
                <a href="Friends.aspx" class="tab-link external">
                    <i class="icon f7-icons size-22">bubble_left_bubble_right</i>
                    <span class="tabbar-label">聊天</span>
                </a>
                <a href="Personal.aspx" class="tab-link external">
                    <i class="icon f7-icons size-25">person</i>
                    <span class="tabbar-label">我的</span>
                </a>
            </div>
        </div>
        <div class="fab fab-right-top">
            <a href="Publish.aspx" class="external">
                <i class="icon f7-icons">plus</i>
            </a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
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

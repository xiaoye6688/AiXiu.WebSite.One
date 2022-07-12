<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyVideos.aspx.cs" Inherits="AiXiu.WebSite.MyVideos" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page" data-page="myvideos">
        <div class="page-content">
            <div class="block-title">我发布的视频</div>
            <asp:Repeater ID="rptVideos" runat="server">
                <HeaderTemplate>
                    <div class="list media-list margin-top-none">
                        <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <li class="swipeout deleted-callback" data-id="<%#Eval("VideoId")%>">
                        <div class="item-content swipeout-content">
                            <div class="item-media">
                                <a class="link cover external" href="Play.aspx?id=<%#Eval("VideoId")%>">
                                    <img src="<%#(string)Eval("CoverURL") ?? "/imgs/square.jpg"%>" width="44" /></a>

                            </div>
                            <div class="item-inner">
                                <div class="item-title-row">
                                    <div class="item-title"><%#Eval("Headline")%></div>
                                </div>
                                <div class="item-subtitle"><%#GetVideoStatus((int)Eval("Status"))%></div>
                            </div>
                        </div>
                        <div class="swipeout-actions-right">
                            <%-- <div class ="item-media swipeout-delete"><i class="f7-icons">trash_circle_fill</i></div>--%>
                            <a href="#" class="swipeout-delete"><div class ="item-media"><i class="f7-icons">trash</i></div></a>
                        </div>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        // 删除视频时，异步删除完整视频文件
        $$('.deleted-callback').on('swipeout:deleted', function () {
            var videoId = $$(this).data('id');
            app.request.get('Ashx/VideoDeleteHandler.ashx', { videoId: videoId });
        });
    </script>
</asp:Content>

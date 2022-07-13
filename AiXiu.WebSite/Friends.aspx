<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Friends.aspx.cs" Inherits="AiXiu.WebSite.Friends" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .media-list.friends .item-link .item-title-row { padding-right: 0; }
            .media-list.friends .item-link .item-title-row:before { width: 0; height: 0; overflow: hidden; }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page" data-page="friends">
        <div class="page-content">
            <asp:Repeater ID="rptFriends" runat="server">
                <HeaderTemplate>
                    <div class="list media-list friends margin-top-none friends">
                        <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <a href="Chat.aspx?to=<%#Eval("Key")%>" class="item-link item-content external">
                            <asp:Repeater ID="rptFriend" runat="server" DataSource="<%#new List<AiXiu.Model.Friend>() { ((KeyValuePair<int, AiXiu.Model.Friend>)Container.DataItem).Value }%>">
                                <ItemTemplate>
                                    <div class="item-media">
                                        <img src="/imgs/square.jpg" data-src="<%#(Eval("Avatar")??"/imgs/avatar.jpg")%>" width="44" height="44" class="lazy" />
                                    </div>
                                    <div class="item-inner">
                                        <div class="item-title-row">
                                            <div class="item-title"><%#Eval("NickName")%></div>
                                            <div class="item-after size-small"><%#ShowDateOrTime((DateTime)Eval("LastMessage.AddTime"))%></div>
                                        </div>
                                        <div class="item-subtitle text-color-gray size-small"><%#Eval("LastMessage.Content")%></div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </a>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <div class="toolbar tabbar tabbar-labels toolbar-bottom">
            <div class="toolbar-inner">
                <a href="Default.aspx" class="tab-link external">
                    <i class="icon f7-icons size-29">videocam</i>
                    <span class="tabbar-label">视频</span>
                </a>
                <a href="Friends.aspx" class="tab-link external tab-link-active">
                    <i class="icon f7-icons size-22">bubble_left_bubble_right</i>
                    <span class="tabbar-label">聊天</span>
                </a>
                <a href="Personal.aspx" class="tab-link external">
                    <i class="icon f7-icons size-25">person</i>
                    <span class="tabbar-label">我的</span>
                </a>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

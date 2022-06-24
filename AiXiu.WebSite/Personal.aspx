<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Personal.aspx.cs" Inherits="AiXiu.WebSite.Personal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .personal .avatar {
            width: 6em;
            height: 6em;
            overflow: hidden;
            position: relative;
        }

            .personal .avatar img {
                width: 100%;
                height: 100%;
                border-radius: 50%;
            }

            .personal .avatar .edit {
                width: 22px;
                height: 22px;
                display: inline-block;
                padding: 4px;
                background: rgba(255,255,255,0.5);
                border-radius: 50%;
                position: absolute;
                z-index: 999;
                top: 0;
                right: 0;
            }

        .personal .nickname {
            display: inline-block;
            line-height: 1.6em;
        }

        .personal .sex {
            display: inline-block;
            width: 5em;
            font-size: 0.8em;
            line-height: 1.6em;
            border-radius: 0.8em;
            text-align: center;
            color: #ffffff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page" data-page="personal">
        <div class="page-content">
            <div class="block block-strong personal margin-top-none">
                <div class="row">
                    <div class="col-30">
                        <div class="avatar float-right">
                            <a class="edit external edit" href="AvatarEdit.aspx"><i class="f7-icons text-color-gray size-22">camera_viewfinder</i></a>
                            <asp:Image ID="imgAvatar" runat="server" />
                        </div>
                    </div>
                    <div class="col-70">
                        <div class="row">
                            <div class="col-100 margin-top-quarter margin-bottom-half">
                                <a href="PersonalEdit.aspx" class="external"><i class="f7-icons float-right size-22 text-color-gray">gear</i></a>
                                <asp:Label ID="lblNickName" runat="server" Text="" CssClass="nickname"></asp:Label>
                                <asp:Label ID="lblSex" runat="server" Text="" CssClass="sex margin-left"></asp:Label>
                            </div>
                            <div class="col-60">
                                <p class="margin-top-quarter margin-bottom-quarter"><i class="f7-icons size-14 text-color-gray margin-right-half">placemark</i><asp:Label ID="lblAddress" runat="server" Text=""></asp:Label></p>
                            </div>
                            <div class="col-40">
                                <p class="margin-top-quarter margin-bottom-quarter"><i class="f7-icons size-14 text-color-gray margin-right-half">gift</i><asp:Label ID="lblBirthday" runat="server" Text=""></asp:Label></p>
                            </div>
                            <div class="col-100">
                                <p class="margin-top-quarter margin-bottom-quarter"><i class="f7-icons size-14 text-color-gray margin-right-half">heart</i><asp:Label ID="lblHobby" runat="server" Text=""></asp:Label></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="list">
                <ul>
                    <li>
                        <a href="Friends.aspx" class="item-link item-content external">
                            <div class="item-media"><i class="f7-icons">person_2</i></div>
                            <div class="item-inner">
                                <div class="item-title">我的好友</div>
                            </div>
                        </a>
                    </li>
                </ul>
            </div>
            <div class="list">
                <ul>
                    <li>
                        <a href="MyVideos.aspx" class="item-link item-content external">
                            <div class="item-media"><i class="f7-icons">play_rectangle</i></div>
                            <div class="item-inner">
                                <div class="item-title">我的视频</div>
                            </div>
                        </a>
                    </li>
                    <li>
                        <a href="Publish.aspx" class="item-link item-content external">
                            <div class="item-media"><i class="f7-icons">camera</i></div>
                            <div class="item-inner">
                                <div class="item-title">发布视频</div>
                            </div>
                        </a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="toolbar tabbar tabbar-labels toolbar-bottom">
        <div class="toolbar-inner">
            <a href="Default.aspx" class="tab-link external">
                <i class="icon f7-icons size-29">videocam</i>
                <span class="tabbar-label">视频</span>
            </a>
            <a href="Friends.aspx" class="tab-link external">
                <i class="icon f7-icons size-22">bubble_left_bubble_right</i>
                <span class="tabbar-label">聊天</span>
            </a>
            <a href="Personal.aspx" class="tab-link external tab-link-active">
                <i class="icon f7-icons size-25">person</i>
                <span class="tabbar-label">我的</span>
            </a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

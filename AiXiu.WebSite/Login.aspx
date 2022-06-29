<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AiXiu.WebSite.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .login-screen-content { position: relative; }
        .space { width: 100%; height: 200px; }
        .image { width: 100%; height: 270px; background: url("/imgs/account-bg.jpg") #007aff; background-position-x: center; background-position-y: top; background-repeat: no-repeat; position: absolute; z-index: -10; top: 0; }
        .login-box { width: 80%; margin: 0 auto; background-color: #ffffff; border-radius: 10px; overflow: hidden; box-shadow: rgba(0,0,0,0.6) 3px 5px 10px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page no-navbar no-toolbar no-swipeback" data-page="login-username">
        <div class="page-content login-screen-content">
            <div class="image"></div>
            <div class="space"></div>
            <div class="login-box">
                <div class="block">
                    <p class="segmented segmented-raised">
                        <a class="button button-active">用户名密码登录</a>
                         <a class="button external" href="Mobile.aspx">手机号密码登录</a>
                        <!--<a class="button external" href="Mobile.aspx?ReturnUrl=<% = Request.QueryString["ReturnUrl"] %>">手机号密码登录</a>-->
                    </p>
                </div>
                <div class="list">
                    <ul>
                        <li class="item-content item-input">
                            <div class="item-inner">
                                <div class="item-title item-label">用户名</div>
                                <div class="item-input">
                                    <asp:TextBox ID="txtUserName" MaxLength="16" TextMode="SingleLine" runat="server" PlaceHolder="请输入用户名"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="用户名不能为空" ControlToValidate="txtUserName" Display="None"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </li>
                        <li class="item-content item-input">
                            <div class="item-inner">
                                <div class="item-title item-label">密码</div>
                                <div class="item-input">
                                    <asp:TextBox ID="txtPassword" MaxLength="32" TextMode="Password" runat="server" PlaceHolder="请输入密码"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="密码不能为空" ControlToValidate="txtPassword" Display="None"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
                <div class="block">
                    <div class="list">
                        <ul>
                            <li>
                                <asp:ValidationSummary ID="vsLogin" runat="server" ShowMessageBox="True" ShowSummary="False" />
                                <asp:Button ID="btnLogin" runat="server" Text="立即登录" CssClass="button button-fill button-large button-round" OnClick="btnLogin_Click" />
                            </li>
                        </ul>
                        <div class="block-footer">
                            <p>登录即代表您同意《用户服务协议》</p>
                            <p>没有账号？<a href="Reg.aspx" class="close-login-screen external">现在注册</a></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

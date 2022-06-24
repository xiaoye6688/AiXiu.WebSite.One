<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reg.aspx.cs" Inherits="AiXiu.WebSite.Reg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <style>
        .login-screen-content { position: relative; }
        .space { width: 100%; height: 200px; }
        .image { width: 100%; height: 270px; background: url("/imgs/account-bg.jpg") #007aff; background-position-x: center; background-position-y: top; background-repeat: no-repeat; position: absolute; z-index: -10; top: 0; }
        .login-box { width: 80%; margin: 0 auto; background-color: #ffffff; border-radius: 10px; overflow: hidden; box-shadow: rgba(0,0,0,0.6) 3px 5px 10px; }
        .captcha { height: var(--f7-input-height); width: auto; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page no-navbar no-toolbar no-swipeback" data-page="register">
        <div class="page-content login-screen-content">
            <div class="image"></div>
            <div class="space"></div>
            <div class="login-box">
                <div class="list">
                    <ul>
                        <li>
                            <div class="item-content item-input">
                                <div class="item-inner">
                                    <div class="item-title item-label">用户名</div>
                                    <div class="item-input-wrap">
                                        <input type="text" name="name" value="" />
                                        <asp:TextBox ID="txtUserName" MaxLength="16" TextMode="SingleLine" runat="server" PlaceHolder="请输入用户名"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="用户名不能为空" ControlToValidate="txtUserName" Display="None"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </li>
                        <li>
                            <div class="item-content item-input">
                                <div class="item-inner">
                                    <div class="item-title item-label">手机号</div>
                                    <div class="item-input-wrap">
                                        <asp:TextBox ID="txtMobileNumber" MaxLength="11" TextMode="SingleLine" runat="server" PlaceHolder="请输入手机号码"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvMobileNumber" runat="server" ErrorMessage="手机号码不能为空" ControlToValidate="txtMobileNumber" Display="None"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revMobileNumber" runat="server" ErrorMessage="手机号码格式不正确" ControlToValidate="txtMobileNumber" Display="None" ValidationExpression="^(13[0-9]|14[01456879]|15[0-35-9]|16[2567]|17[0-8]|18[0-9]|19[0-35-9])\d{8}$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                        </li>
                        <li>
                            <div class="item-content item-input">
                                <div class="item-inner">
                                    <div class="item-title item-label">密码</div>
                                    <div class="item-input-wrap">
                                        <asp:TextBox ID="txtPassword" MaxLength="32" TextMode="Password" runat="server" PlaceHolder="请输入密码"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="密码不能为空" ControlToValidate="txtPassword" Display="None"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </li>
                        <li>
                            <div class="item-content item-input">
                                <div class="item-inner">
                                    <div class="item-title item-label">验证码</div>
                                    <div class="item-input-wrap">
                                        <div class="row">
                                            <div class="col-50">
                                                <asp:TextBox ID="txtCaptcha" MaxLength="4" TextMode="SingleLine" runat="server" PlaceHolder="请输入验证码"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvCaptcha" runat="server" ErrorMessage="验证码不能为空" ControlToValidate="txtCaptcha" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-50">
                                                <img src="Ashx/CaptchaHandler.ashx" class="captcha" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
                <div class="block">
                    <div class="list">
                        <ul>
                            <li>
                                <asp:ValidationSummary ID="vsRegister" runat="server" ShowMessageBox="True" ShowSummary="False" />
                                <asp:Button ID="btnRegister" runat="server" Text="免费注册" CssClass="button button-fill button-large button-round" OnClick="btnRegister_Click" />
                            </li>
                        </ul>
                        <div class="block-footer">
                            <p>已有账号？<a href="Login.aspx" class="close-login-screen external">现在登录</a></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
     <script>
        // 图形验证码点击刷新
        var captcha = $$('.captcha');
        var captchaSrc = captcha.attr('src')
        captcha.on('click', function () {
            $$(this).attr('src', captchaSrc + '?r=' + Math.random());
        })
     </script>
</asp:Content>

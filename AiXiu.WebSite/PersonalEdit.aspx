<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PersonalEdit.aspx.cs" Inherits="AiXiu.WebSite.PersonalEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        #BodyContent_cblHobby { width: 100%; padding: 0; padding-top: calc(var(--f7-input-height) * 0.2); padding-bottom: calc(var(--f7-input-height) * 0.2); }
            #BodyContent_cblHobby li { width: 48%; display: inline-block; line-height: calc(var(--f7-input-height) * 0.6); }
    </style>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page" data-page="personaledit">
        <div class="page-content">
            <div class="block-title">编辑个人资料</div>
            <div class="list inline-labels no-hairlines-md">
                <ul>
                    <li>
                        <div class="item-content item-input">
                            <div class="item-inner">
                                <div class="item-title item-label">昵称</div>
                                <div class="item-input-wrap">
                                    <asp:TextBox ID="txtNickName" runat="server" MaxLength="16" TextMode="SingleLine"></asp:TextBox>
                                    <input type="text" name="name" value="" />
                                    <span class="input-clear-button"></span>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="item-content item-input">
                            <div class="item-inner">
                                <div class="item-title item-label">性别</div>
                                <div class="item-input-wrap input-dropdown-wrap">
                                    <asp:DropDownList ID="ddlSex" runat="server">
                                        <asp:ListItem Selected="True" Value="0">- 请选择 -</asp:ListItem>
                                        <asp:ListItem Value="1">男</asp:ListItem>
                                        <asp:ListItem Value="2">女</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="item-content item-input">
                            <div class="item-inner">
                                <div class="item-title item-label">生日</div>
                                <div class="item-input-wrap">
                                    <asp:TextBox ID="txtBirthday" runat="server" TextMode="SingleLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="item-content">
                            <div class="item-inner">
                                <div class="item-title item-label">所在地</div>
                                <div class="item-input-wrap input-dropdown-wrap">
                                    <div class="row">
                                        <div class="col-50">
                                            <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged">
                                                <asp:ListItem Selected="True">- 请选择 -</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-50">
                                            <asp:DropDownList ID="ddlCity" runat="server">
                                                <asp:ListItem Selected="True">- 请选择 -</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="item-content">
                            <div class="item-inner">
                                <div class="item-title item-label">爱好</div>
                                <div class="item-input-wrap input-dropdown-wrap">
                                    <asp:CheckBoxList ID="cblHobby" runat="server" RepeatLayout="UnorderedList">
                                        <asp:ListItem>篮球</asp:ListItem>
                                        <asp:ListItem>跑步</asp:ListItem>
                                        <asp:ListItem>看电影</asp:ListItem>
                                        <asp:ListItem>钢琴</asp:ListItem>
                                        <asp:ListItem>网络游戏</asp:ListItem>
                                        <asp:ListItem>逛街购物</asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="block block-strong">
                <asp:Button ID="btnProfile" runat="server" Text="立即更新" CssClass="button button-fill button-large button-round" OnClick="btnProfile_Click" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        var datePicker = app.picker.create({
            inputEl: '#BodyContent_txtBirthday',
            toolbar: true,
            rotateEffect: true,
            toolbarCloseText: '确定',
            formatValue: function (values) {
                return values[0] + '-' + values[1] + '-' + values[2];
            },
            cols: [
                {
                    values: (function () {
                        var arr = [];
                        for (var i = 1950; i <= 2030; i++) { arr.push(i); }
                        return arr;
                    })(),
                },
                {
                    values: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
                },
                {
                    values: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31],
                }
            ],
            on: {
                change: function (picker, values) {
                    var daysInMonth = new Date(picker.value[0], picker.value[1], 0).getDate();
                    if (values[2] > daysInMonth) {
                        picker.cols[2].setValue(daysInMonth);
                    }
                }
            }
        })
        var today = new Date();
        var inputDate = document.getElementById('BodyContent_txtBirthday').value;
        if (inputDate) {
            today = new Date(inputDate);
            datePicker.value = [
                today.getFullYear(),
                today.getMonth() + 1,
                today.getDate()
            ]
        }
    </script>
</asp:Content>

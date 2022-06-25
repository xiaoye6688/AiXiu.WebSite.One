<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AvatarEdit.aspx.cs" Inherits="AiXiu.WebSite.AvatarEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .avataredit .uploadselect { border: 1px solid; width: 100px; height: 100px; color: #ccc; transition: color .25s; position: relative; }
            .avataredit .uploadselect::before { content: ''; position: absolute; left: 50%; top: 50%; width: 80px; margin-left: -40px; margin-top: -5px; border-top: 10px solid; }
            .avataredit .uploadselect::after { content: ''; position: absolute; left: 50%; top: 50%; height: 80px; margin-left: -5px; margin-top: -40px; border-left: 10px solid; }
        .avataredit .upload { width: 100px; height: 100px; display: block; border: none; opacity: 0; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
        <div class="page" data-page="avataredit">
        <div class="page-content">
            <div class="block-title">编辑头像</div>
            <div class="list inline-labels no-hairlines-md">
                <ul>
                    <li>
                        <div class="item-content item-input">
                            <div class="item-inner">
                                <div class="item-title item-label">上传头像</div>
                                <div class="item-input-wrap padding-top padding-bottom">
                                    <div id="uploadselect" class="uploadselect">
                                        <input type="file" id="upload" class="upload" accept="image/*" />
                                    </div>
                                    <asp:HiddenField ID="hfAvatar" runat="server" />
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="block block-strong">
                <asp:ValidationSummary ID="vsLogin" runat="server" ShowMessageBox="True" ShowSummary="False" />
                <asp:Button ID="btnAvatar" runat="server" Text="立即更新" CssClass="button button-fill button-large button-round" OnClick="btnAvatar_Click" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="lib/lrz/dist/lrz.bundle.js"></script>
    <script>
        document.querySelector('#upload').addEventListener('change', function () {
            lrz(this.files[0], { width: 300, height: 300 })
                .then(function (rst) {
                    // 上传文件数据
                    var xhr = new XMLHttpRequest();
                    xhr.open('POST', 'Ashx/ImageUploadHandler.ashx?length=' + rst.base64Len);
                    xhr.onload = function () {
                        if (xhr.status === 200) {
                            document.getElementById('BodyContent_hfAvatar').value = xhr.responseText;
                        } else {
                            alert('图片上传失败！');
                        }
                    };
                    xhr.send(rst.base64);
                    // 在页面中显示图片
                    var img = new Image();
                    img.src = rst.base64;
                    img.width = 100;
                    img.height = 100;
                    document.getElementById('uploadselect').parentNode.appendChild(img);
                    document.getElementById('uploadselect').remove();
                    // 返回对象
                    return rst;
                })
                .catch(function (err) {
                    console.log(err);
                    alert('图片上传失败！');
                });
        });
    </script>
</asp:Content>

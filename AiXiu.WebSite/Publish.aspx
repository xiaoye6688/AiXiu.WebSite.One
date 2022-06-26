<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Publish.aspx.cs" Inherits="AiXiu.WebSite.Publish" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        #file_video_btn { position: relative; }
        #file_video { width: 100%; height: 100%; display: block; position: absolute; top: 0; left: 0; z-index: 999; opacity: 0; }
        #bmap { width: 0; height: 0; display: none; }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page" data-page="publish">
        <div class="page-content">
            <div class="block-title">视频录制</div>
            <div class="block block-strong">
                <span class="button button-large button-fill margin-bottom-half" id="file_video_btn">选择视频<input type="file" name="file_video" id="file_video" accept="video/*" /></span>
                <span data-progress="0" class="progressbar" id="upload-progressbar"></span>
            </div>
            <div class="block-title">视频标题</div>
            <div class="list">
                <ul>
                    <li class="item-content item-input">
                        <div class="item-inner">
                            <div class="item-input item-input-wrap">
                                <textarea placeholder="完善视频标题，能让更多人看到~" maxlength="100" id="headline"></textarea>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="block-title">你的位置</div>
            <div class="block block-strong" id="pois"></div>
            <div class="block block-strong">
                <a class="button button-fill button-large button-round" id="publish">上传发布</a>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="Scripts/lib/es6-promise.min.js"></script>
    <script src="Scripts/lib/aliyun-oss-sdk-6.13.0.min.js"></script>
    <script src="Scripts/aliyun-upload-sdk-1.5.2.min.js"></script>
    <script>
        // 调用百度地图API获取附近地点
        var mapAK = '5bda27c7aa02ba6b529a54790c97feee';
        window.onload = function () {
            // 异步加载Web服务API
            var mapScript = document.createElement("script");
            mapScript.src = 'http://api.map.baidu.com/api?v=2.0&ak=' + mapAK + '&callback=initialize';
            document.body.appendChild(mapScript);
        }
        // Web服务API加载完成执行的回调方法
        function initialize() {
            var geolocation = new BMap.Geolocation();
            // 获取当前设备定位/IP定位
            geolocation.getCurrentPosition(function (r) {
                if (this.getStatus() == BMAP_STATUS_SUCCESS) {
                    // 异步加载地图检索API
                    var poiScript = document.createElement("script");
                    poiScript.src = 'http://api.map.baidu.com/place/v2/search' +
                        '?query=美食$酒店$购物$丽人$旅游景点$休闲娱乐$运动健身$文化传媒$交通设施$公司企业' +
                        '&location=' + r.point.lat + ',' + r.point.lng +
                        '&radius=100' +
                        '&output=json' +
                        '&ak=' + mapAK +
                        '&callback=showpois';
                    document.body.appendChild(poiScript);
                }
                else {
                    alert('failed' + this.getStatus());
                }
            });
        }
        // 地图检索API加载完成执行的回调方法
        function showpois(data) {
            for (var i = 0; i < data.results.length; i++) {
                $$('#pois').append($$('<div class="chip chip-outline margin-right-half"><div class="chip-label">' + data.results[i].name + '</div></div>'));
            }
        }
        $$('#pois').on('click', '.chip-label', function () {
            $$(this).parent().siblings('.color-blue').removeClass('color-blue').addClass('chip-outline');
            $$(this).parent().removeClass('chip-outline').addClass('color-blue');
        })

        var uploader = null;
        // 上传发布
        $$('#upload-progressbar').hide();
        $$('#publish').on('click', function () {
            var file = $$('#file_video')[0].files[0];
            if (!file) {
                alert('请拍摄视频！');
                return;
            }
            var filename = $$('#file_video').val();
            var headline = $$('#headline').val();
            var location = $$('#pois .color-blue .chip-label').html();
            var userData = '{"Vod":{}}';
            if (uploader) {
                uploader.stopUpload()
                // 重置进度条位置
                app.progressbar.set('#upload-progressbar', 0);
            }
            // 请求上传凭证
            var params = {
                filename: encodeURIComponent(filename),
                headline: headline ? encodeURIComponent(headline) : null,
                location: location ? encodeURIComponent(location) : null
            }
            app.request.get('VideoUploadHandler.ashx', params, function (data) {
                var res = eval('(' + data + ')');
                uploader = createUploader(res.VideoId, res.UploadAddress, res.UploadAuth);
                // 添加视频
                uploader.addFile(file, null, null, null, userData);
                $$('#file_video_btn').attr('disabled', true);
                $$('#file_video').attr('disabled', true);
                // 开始上传
                if (uploader != null) {
                    // 展示进度条
                    $$('#upload-progressbar').show();
                    $$('#file_video_btn').html('视频上传中...');
                    // 开始上传
                    uploader.startUpload();
                }
            })
        })
        // 创建一个上传对象
        function createUploader(videoId, uploadAddress, uploadAuth) {
            var uploader = new AliyunUpload.Vod({
                userId: '<% = ConfigurationManager.AppSettings["aliyun:UserId"] %>',    // 阿里账号ID
                region: '<% = ConfigurationManager.AppSettings["aliyun:RegionId"] %>',  // 上传到视频点播的地域
                partSize: 1048576,      // 分片大小默认10MB
                // 添加文件成功
                addFileSuccess: function (uploadInfo) {
                    //console.log('添加文件成功，等待上传...');
                },
                // 开始上传
                onUploadstarted: function (uploadInfo) {
                    uploader.setUploadAuthAndAddress(uploadInfo, uploadAuth, uploadAddress, videoId);
                    //console.log('开始上传');
                },
                // 上传成功
                onUploadSucceed: function (uploadInfo) {
                    //console.log('上传成功');
                    alert('上传成功');
                    window.location.href = '/MyVideos.aspx';
                },
                // 文件上传失败
                onUploadFailed: function (uploadInfo, code, message) {
                    //console.log('上传失败');
                    alert(message + ' 错误码：' + code);
                },
                // 文件上传进度
                onUploadProgress: function (uploadInfo, totalSize, loadedPercent) {
                    //console.log('上传进度' + loadedPercent);
                    app.progressbar.set('#upload-progressbar', loadedPercent * 100);
                }
            })
            return uploader;
        }
    </script>
</asp:Content>

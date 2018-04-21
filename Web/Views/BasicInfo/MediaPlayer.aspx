<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>MediaPlayer</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript" language="javascript">
        $(function () {
            document.getElementById("MediaPlayer1").Filename = '<%: ViewData["strMapPath"] %>';
        });

        function closePage() {
            parent.autoClose();
        }

        //$(window).on('beforeunload', function () {
        //    parent.autoClose();
        //});

    </script>
</head>
<body>
    <table width="100%" border="0" cellspacing="0" cellpadding="6" class="blockTable">
        <tr>
            <td style="padding: 0 8px 4px;" align="center">
                <table style="font: 12pt; width: 100%;">
                    <tr>
                        <td style="height: 68px">
                            <object classid="clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95" id="MediaPlayer1" style="width: 100%;
                                height: 68px">
                                <param name="AudioStream" value="-1">
                                <param name="AutoSize" value="-1">
                                <param name="AutoStart" value="-1">
                                <param name="AnimationAtStart" value="-1">
                                <param name="AllowScan" value="-1">
                                <param name="AllowChangeDisplaySize" value="-1">
                                <param name="AutoRewind" value="0">
                                <param name="Balance" value="0">
                                <param name="BaseURL" value="">
                                <param name="BufferingTime" value="15">
                                <param name="CaptioningID" value>
                                <param name="ClickToPlay" value="-1">
                                <param name="CursorType" value="0">
                                <param name="CurrentPosition" value="0">
                                <param name="CurrentMarker" value="0">
                                <param name="DefaultFrame" value>
                                <param name="DisplayBackColor" value="0">
                                <param name="DisplayForeColor" value="16777215">
                                <param name="DisplayMode" value="0">
                                <param name="DisplaySize" value="0">
                                <!--视频1-50%, 0-100%, 2-200%,3-全屏 其它的值作0处理,小数则采用四舍五入然后按前的处理-->
                                <param name="Enabled" value="-1">
                                <param name="EnableContextMenu" value="-1">
                                <param name="EnablePositionControls" value="-1">
                                <param name="EnableFullScreenControls" value="-1">
                                <param name="EnableTracker" value="-1">
                                <param name="Filename" value="Music/sayoldtime.mp3" valuetype="ref">
                                <param name="InvokeURLs" value="-1">
                                <param name="Language" value="-1">
                                <param name="Mute" value="0">
                                <param name="PlayCount" value="10">
                                <param name="PreviewMode" value="-1">
                                <param name="Rate" value="1">
                                <param name="SAMILang" value>
                                <param name="SAMIStyle" value>
                                <param name="SAMIFileName" value>
                                <param name="SelectionStart" value="-1">
                                <param name="SelectionEnd" value="-1">
                                <param name="SendOpenStateChangeEvents" value="-1">
                                <param name="SendWarningEvents" value="-1">
                                <param name="SendErrorEvents" value="-1">
                                <param name="SendKeyboardEvents" value="0">
                                <param name="SendMouseClickEvents" value="0">
                                <param name="SendMouseMoveEvents" value="0">
                                <param name="SendPlayStateChangeEvents" value="-1">
                                <param name="ShowCaptioning" value="0">
                                <param name="ShowControls" value="-1">
                                <param name="ShowAudioControls" value="-1">
                                <param name="ShowDisplay" value="0">
                                <param name="ShowGotoBar" value="0">
                                <param name="ShowPositionControls" value="-1">
                                <param name="ShowStatusBar" value="-1">
                                <param name="ShowTracker" value="-1">
                                <param name="TransparentAtStart" value="-1">
                                <param name="VideoBorderWidth" value="0">
                                <param name="VideoBorderColor" value="0">
                                <param name="VideoBorder3D" value="0">
                                <param name="Volume" value="0">
                                <param name="WindowlessVideo" value="0">
                                <!--是否自动调整播放大小-->
                                <!--是否自动播放-->
                                <!--左右声道平衡,最左-9640,最右9640-->
                                <!--缓冲时间-->
                                <!--当前播放进度 -1 表示不变,0表示开头 单位是秒,比如10表示从第10秒处开始播放,值必须是-1.0或大于等于0-->
                                <!--是否用右键弹出菜单控制-->
                                <!--是否允许拉动播放进度条到任意地方播放-->
                                <!--是否静音-->
                                <!--重复播放次数,0为始终重复-->
                                <!--播放速度1.0-2.0倍的速度播放-->
                                <!--选择同时播放(伴音)的歌曲-->
                                <!--是否显示字幕,为一块黑色,下面会有一大块黑色,一般不显示-->
                                <!--是否显示控制,比如播放,停止,暂停-->
                                <!--是否显示音量控制-->
                                <!--显示节目信息,比如版权等-->
                                <!--一条框,在下面,有往下箭头-->
                                <!--是否显示往前往后及列表,如果显示一般也都是灰色不可控制-->
                                <!--当前播放信息,显示是否正在播放,及总播放时间和当前播放到的时间-->
                                <!--是否显示当前播放跟踪条,即当前的播放进度条-->
                                <!--显示部的宽部,如果小于视频宽,则最小为视频宽,或者加大到指定值,并自动加大高度.此改变只改变四周的黑框大小,不改变视频大小-->
                                <!--显示黑色框的颜色, 为RGB值,比如ffff00为黄色-->
                                <!--音量大小,负值表示是当前音量的减值,值自动会取绝对值,最大为0,最小为-9640,最大0-->
                                <!--如果是0可以允许全屏,否则只能在窗口中查看-->
                            </object>
                            <script type="text/javascript">
                                var mediaRate = document.MediaPlayer1.Rate;
                                var mediaVolume = document.MediaPlayer1.Volume;
                                var mediaCurrentPosition = document.MediaPlayer1.CurrentPosition;
                                function setRate(num) {
                                    mediaRate = num;
                                    document.MediaPlayer1.Rate = num;
                                }
                                function addCurrentPosition(num) {
                                    mediaCurrentPosition = document.MediaPlayer1.CurrentPosition;
                                    mediaCurrentPosition += num;
                                    if (mediaCurrentPosition < 0) mediaCurrentPosition = 0;
                                    document.MediaPlayer1.CurrentPosition = mediaCurrentPosition;
                                }
                                function addVolume(num) {
                                    mediaVolume = document.MediaPlayer1.Volume;
                                    if (num > 0 && mediaVolume < -1) {
                                        mediaVolume += num;
                                        if (mediaVolume > -1) mediaVolume = -1;
                                        document.MediaPlayer1.Volume = mediaVolume;
                                    }
                                    else {
                                        if (num < 0 && mediaVolume > -9999) {
                                            mediaVolume += num;
                                            if (mediaVolume < -9999) mediaVolume = -9999;
                                            document.MediaPlayer1.Volume = mediaVolume;
                                        }
                                    }
                                }
                                function addRate(num) {
                                    mediaRate = document.MediaPlayer1.Rate;
                                    if (num > 0 && mediaRate < 12.0) {
                                        mediaRate += num;
                                        //if(mediaRate >2.0)mediaRate=2.0;
                                        document.MediaPlayer1.Rate = mediaRate;
                                    }
                                    else {
                                        if (num < 0 && mediaRate > 0.1) {
                                            mediaRate += num;
                                            if (mediaRate < 0.1) mediaRate = 0.1;
                                            document.MediaPlayer1.Rate = mediaRate;
                                        }
                                    }
                                }
                                function exchangeValue(obj) {
                                    if (obj == "0") {
                                        obj = "-1";
                                    }
                                    else {
                                        obj = "0";
                                    }
                                    return obj;
                                }
                            </script>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
							<a href="#" class="easyui-linkbutton" iconcls="icon-back" onclick="closePage()">返回</a>                           
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>

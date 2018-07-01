<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>受理信息</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%> 
    <script type="text/javascript" language="javascript">
        function play(name) {
            var temp = name.replace(/\-/g, '');
            document.getElementById("Player").Filename = "http://192.168.0.5/media/" +
             temp.substr(0, 4) + "/" + temp.substr(0, 6) + "/" + temp.substr(0, 8) + "/" + escape(name);
            $('#media').window({
                title: '听录音',
                width: 300,
                modal: true,
                shadow: true,
                height: 140,
                resizable: false,
                onBeforeClose: function () {
                    document.getElementById("Player").Filename = "";
                }
            });
            $('#media').window('open');
        }
        function detail(code) {
            document.getElementById('aum').style.display = "block";
            document.getElementById('frame').src="/BasicInfo/TaskInfo/?code=" + code;
        }
    </script>
</head>
<body class="easyui-layout">
    <div region="center" style="padding: 5px; height: 50%">
        <div region="north" style="border:2px;">
            <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td width="1%">
                            &nbsp;
                        </td>
                        <td width="90%" align="left" valign="middle" colspan="6" class="TableInfoTH">
                            事件详细信息[<%= ((dynamic)this.ViewData["Code"]) %>]
                        </td>
                        <td width="1%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8" style="height:10px;"></td>
                    </tr>
                    <tr>
                        <td width="1%">
                            &nbsp;
                        </td>
                        <td align="left" valign="middle">
                            事件名称：
                        </td>
                        <td align="left" valign="middle">
                            <input id="AlarmName" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity"]).Name %>"
                                readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            首次受理时刻：
                        </td>
                        <td align="left" valign="middle">
                            <input id="FirstAccTime" style="border-width: 0px; background-color: transparent;"
                                value="<%= ((dynamic)this.ViewData["entity"]).FirstAccTime %>" readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            首次受理人：
                        </td>
                        <td align="left" valign="middle">
                            <input id="FirstDis" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity"]).FirstDis %>"
                                readonly="true" />
                        </td>
                        <td width="1%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="1%">
                            &nbsp;
                        </td>
                        <td align="left" valign="middle">
                            首次呼救电话：
                        </td>
                        <td align="left" valign="middle">
                            <input id="FirstTel" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity"]).FirstTel %>"
                                readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            受理次数：
                        </td>
                        <td align="left" valign="middle">
                            <input id="AccTimes" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity"]).AccTimes %>"
                                readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            首次派车时刻：
                        </td>
                        <td align="left" valign="middle">
                            <input id="FirstSendTime" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity"]).FirstSendTime %>"
                                readonly="true" />
                        </td>
                        <td width="1%">
                            &nbsp;
                        </td>
                    </tr>
                                                                                                                                <tr>
                    <td width="1%">
                        &nbsp;
                    </td>
                    <td align="left" valign="middle">
                        区域：
                    </td>
                    <td align="left" valign="middle">
                        <input id="Area" style="border-width: 0px; background-color: transparent;"
                            value="<%= ((dynamic)this.ViewData["entity"]).Area %>" readonly="true" />
                    </td>
                    <td align="left" valign="middle">
                        事件来源：
                    </td>
                    <td align="left" valign="middle">
                        <input id="Ori" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity"]).Ori %>"
                            readonly="true" />
                    </td>
                    <td align="left" valign="middle">
                        &nbsp;
                    </td>
                    <td align="left" valign="middle">
                        &nbsp;
                    </td>
                    <td width="1%">
                        &nbsp;
                    </td>
                </tr>
                    <tr>
                        <td width="1%">
                            &nbsp;
                        </td>
                        <td align="left" valign="middle">
                            事件类型：
                        </td>
                        <td align="left" valign="middle">
                            <input id="Type" style="border-width: 0px; background-color: transparent;"
                                value="<%= ((dynamic)this.ViewData["entity"]).Type %>" readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            事故类型：
                        </td>
                        <td align="left" valign="middle">
                            <input id="AccidentType" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity"]).AccidentType %>"
                                readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;
                        </td>
                        <td width="1%">
                            &nbsp;
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="8"><hr style="border:1px" color="#987cb9" size="1" /></td>
                    </tr>
                </table>
        </div>
        <div region="center" style="border:2px;">
            <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td width="1%">
                            &nbsp;
                        </td>
                        <td width="90%" align="left" valign="middle" colspan="6">
                            <span id="Span1" class="editTitle">受理调度详细信息</span>
                        </td>
                        <td width="1%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8" style="height:10px;"></td>
                    </tr>
                    <tr>
                        <td width="1%">
                            &nbsp;
                        </td>
                        <td align="left" valign="middle">
                            主叫号码：
                        </td>
                        <td align="left" valign="middle">
                            <input id="Tel" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity2"]).Tel %>"
                                readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            主诉判断：
                        </td>
                        <td align="left" valign="middle">
                            <input id="ZhuSu" style="border-width: 0px; background-color: transparent;"
                                value="<%= ((dynamic)this.ViewData["entity2"]).ZhuSu %>" readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            责任受理人：
                        </td>
                        <td align="left" valign="middle">
                            <input id="Dispatcher" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity2"]).Dispatcher %>"
                                readonly="true" />
                        </td>
                        <td width="1%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="1%">
                            &nbsp;
                        </td>
                        <td align="left" valign="middle">
                            患者姓名：
                        </td>
                        <td align="left" valign="middle">
                            <input id="Name" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity2"]).Name %>"
                                readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            性别：
                        </td>
                        <td align="left" valign="middle">
                            <input id="Sex" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity2"]).Sex %>"
                                readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            年龄：
                        </td>
                        <td align="left" valign="middle">
                            <input id="Age" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity2"]).Age %>"
                                readonly="true" />
                        </td>
                        <td width="1%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>                                                                                                            <tr>
                        <td width="1%">
                            &nbsp;
                        </td>
                        <td align="left" valign="middle">
                            国籍：
                        </td>
                        <td align="left" valign="middle">
                            <input id="Nationnality" style="border-width: 0px; background-color: transparent;"
                                value="<%= ((dynamic)this.ViewData["entity2"]).Nationnality %>" readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            民族：
                        </td>
                        <td align="left" valign="middle">
                            <input id="Nation" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity2"]).Nation %>"
                                readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            联系人：
                        </td>
                        <td align="left" valign="middle">
                            <input id="Connector" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity2"]).Connector %>"
                                readonly="true" />
                        </td>
                        <td width="1%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="1%">
                            &nbsp;
                        </td>
                        <td align="left" valign="middle">
                            联系电话：
                        </td>
                        <td align="left" valign="middle">
                            <input id="ConnectTel" style="border-width: 0px; background-color: transparent;"
                                value="<%= ((dynamic)this.ViewData["entity2"]).ConnectTel %>" readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            现场地址：
                        </td>
                        <td align="left" valign="middle">
                            <input id="LocalAddr" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity2"]).LocalAddr %>"
                                readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            送达地址：
                        </td>
                        <td align="left" valign="middle">
                            <input id="TotalAddr" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity2"]).TotalAddr %>"
                                readonly="true" />
                        </td>
                        <td width="1%">
                            &nbsp;
                        </td>
                    </tr> 
                    <tr>
                        <td width="1%">
                            &nbsp;
                        </td>
                        <td align="left" valign="middle">
                            受理类型：
                        </td>
                        <td align="left" valign="middle">
                            <input id="Type" style="border-width: 0px; background-color: transparent;"
                                value="<%= ((dynamic)this.ViewData["entity2"]).Type %>" readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            振铃时刻：
                        </td>
                        <td align="left" valign="middle">
                            <input id="RingTime" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity2"]).RingTime %>"
                                readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            开始受理时刻：
                        </td>
                        <td align="left" valign="middle">
                            <input id="BeginTime" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity2"]).BeginTime %>"
                                readonly="true" />
                        </td>
                        <td width="1%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="1%">
                            &nbsp;
                        </td>
                        <td align="left" valign="middle">
                            结束受理时刻：
                        </td>
                        <td align="left" valign="middle">
                            <input id="EndTime" style="border-width: 0px; background-color: transparent;"
                                value="<%= ((dynamic)this.ViewData["entity2"]).EndTime %>" readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            发送指令时刻：
                        </td>
                        <td align="left" valign="middle">
                            <input id="SendTime" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity2"]).SendTime %>"
                                readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            
                        </td>
                        <td align="left" valign="middle">
                            
                        </td>
                        <td width="1%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="1%">
                            &nbsp;
                        </td>
                        <td align="left" valign="middle">
                            派车列表：
                        </td>
                        <td colspan="5">
                            <input id="CarList" style="border-width: 0px; background-color: transparent; height:50px;" value="<%= ((dynamic)this.ViewData["entity2"]).CarList %>"
                                readonly="true" />
                        </td>
                        <td width="1%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="1%">
                            &nbsp;
                        </td>
                        <td align="left" valign="middle">
                            备注：
                        </td>
                        <td colspan="5">
                            <input id="Remark" style="border-width: 0px; background-color: transparent; height:50px;" value="<%= ((dynamic)this.ViewData["entity2"]).Remark %>"
                                readonly="true" />
                        </td>
                        <td width="1%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8"><hr style="border:1px" color="#987cb9" size="1" /></td>
                    </tr>
                </table>
        </div>
        <div region="south" style="padding: 5px" border="true" > 
        <span id="Span2" class="editTitle">相关录音</span>
        <table id="grid" class="easyui-datagrid" align="center" url="/BasicInfo/AccTelgLoad/?code=<%=ViewData["Code"]%>"
         pagination="false"  pageNumber=1 pageList= "[10, 15, 20]" pageSize=15 idField="ID" fitColumns="true" 
         nowrap="false" striped="true" rownumbers="true" singleSelect="true"
         sortName="ID" sortOrder= "asc" remoteSort="false" width="1000px" height="300px" fit="false" 
         >
        <thead>
			<tr>
                <th field="tel" width="70px"  align='center'>主叫号码</th>
                <th field="begintime" width="80px"  align='center'>通话时刻</th>
                <th field="endtime" width="100px"  align='center'>结束时刻</th>
                <th field="desk" width="50px"  align='center'>台号</th>
                <th field="dis" width="50px"  align='center'>调度员</th>
                <th field="recordcode" width="230px"  align='center'>录音号</th>
                <th field="result" width="60px"  align='center'>通话类型</th>
                <th field="Test" width="30px" align='center'>听录音</th>
			</tr>
		</thead>
        </table>
        <hr style="border:1px" color="#987cb9" size="1" />
        <span id="Span3" class="editTitle">派车记录</span>
        <table id="Table1" class="easyui-datagrid" align="center" url="/BasicInfo/AumLoad/?code=<%=ViewData["Code"]%>&&ord=<%=this.ViewData["Ord"]%>"
         pagination="false"  pageNumber=1 pageList= "[10, 15, 20]" pageSize=15 idField="ID" fitColumns="true" 
         nowrap="false" striped="true" rownumbers="true" singleSelect="true"
         sortName="ID" sortOrder= "asc" remoteSort="false" width="1000px" height="300px" fit="false" 
         >
        <thead>
			<tr>
                <th field="code" width="70px"  align='center'>任务编码</th>
                <th field="aum" width="80px"  align='center'>车辆标识</th>
                <th field="sta" width="100px"  align='center'>所属分站</th>
                <th field="mark" width="50px"  align='center'>车牌号</th>
                <th field="end" width="50px"  align='center'>是否正常结束</th>
                <th field="test" width="30px" align='center'>查看</th>
			</tr>
		</thead>
        </table>
        <div id ="aum" style="display:none;">
            <iframe id="frame" scrolling="auto" frameborder="0"  src="" style="width:100%;height:180px;"></iframe>
        </div>
      </div>       
    </div>
    <div id="media" class="easyui-window" title="听录音" minimizable="false" maximizable="false" icon="icon-save"
        style="width:300px;height:80px;padding:5px;" closed="true">
        <div style="width:280px;height:80px">
        <object classid="clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95" id="Player" style="width:280px;
            height: 80px">
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
            <param name="Filename" value="" valuetype="ref">
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
        </div>
    </div>
</body>
</html>

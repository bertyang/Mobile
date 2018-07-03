<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title></title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script src="/Content/Script/popup.js" type="text/javascript"></script>
    <script src="/Content/Script/popupclass.js" type="text/javascript"></script>
    <style type="text/css">
        .TableInfoTH
        {
            background: #FFFFFF repeat-x left top;
            height: 25px;
        }
        .TableInfoTD
        {
            background-color: #F0F0F0;
        }
        /*.TableInfoTD td
        {
            border-width: 0 1px 1px 0;
            border-style: dotted;
            margin: 0;
            padding: 0;
        }*/
        tr
        {
            height: 22px;
        }
        *
        {
            font-size: 12px;
        }
        .This_header
        {
            border-color: #95B8E7;
            background-color: #E0ECFF;
            background: -webkit-linear-gradient(top,#EFF5FF 0,#E0ECFF 100%);
            background: -moz-linear-gradient(top,#EFF5FF 0,#E0ECFF 100%);
            background: -o-linear-gradient(top,#EFF5FF 0,#E0ECFF 100%);
            background: linear-gradient(to bottom,#EFF5FF 0,#E0ECFF 100%);
            background-repeat: repeat-x;
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr=#EFF5FF,endColorstr=#E0ECFF,GradientType=0);
            padding: 5px;
            position: relative;
            border-width: 1px;
            border-style: solid;
            height: 16px;
            text-align: left;
        }
        .This_title
        {
            font-size: 12px;
            color: #0E2D5F;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript" language="javascript">
        //#region Hover Tabs
        var hoverTabs = function (tabName) {
            if ($(tabName).tabs().length < 2)
                return;
            var tabs = $(tabName).tabs().tabs('tabs');
            for (var i = 0; i < tabs.length; i++) {
                tabs[i].panel('options').tab.unbind().bind('mouseenter', { index: i }, function (e) {
                    $(tabName).tabs('select', e.data.index);
                });
            }
        }
        $(function () {
            hoverTabs("#divAcceptEvent");
            hoverTabs("#divTask");

            //#region 下拉菜单初始化
            //关联到事件相关
            //            EUIcombobox("#txt_Dis", {
            //                valueField: "编码",
            //                textField: "名称",
            //                OneOption: [{
            //                    编码: "",
            //                    姓名: "--请选择--"
            //                }],
            //                url: "/BasicInfo/LoadAlarmCallType/"

            //            });
            //取消关联到事件
            EUIcombobox("#txt_CallType", {
                valueField: "编码",
                textField: "名称",
                OneOption: [{
                    编码: "",
                    姓名: "--请选择--"
                }],
                url: "/BasicInfo/LoadCallType/"

            });
            //#endregion

        });
        //#endregion
        //#region 修改

        function closeWindowR() {
            $('#win').window('close');
            location.reload();
        }
        function TaeEdite(EventCode) {
            var url = "../AlarmEventInfoEdit/?eventCode=" + EventCode;
            var $window = $('#winTaeEdite');
            $window.empty();
            $window = $window.html('<iframe id="AlarmEventEdite" scrolling="no" frameborder="0"  src="' + url + '" style="width:100%;height:200px;"></iframe>').window({
                title: '事件编码[' + EventCode + ']',
                width: 800,
                modal: true,
                shadow: true,
                height: 250,
                resizable: false
            });
            $window.window('open');
        }
        function AcceptEventEdite(EventCode, EcceptOrder) {

            var url = "../AcceptEventInfoEdit/?eventCode=" + EventCode + "&acceptOrder=" + EcceptOrder;
            var $window = $('#winAcceptEventEdite');
            $window.empty();
            $window = $window.html('<iframe id="AcceptEventInfoEdit" scrolling="no" frameborder="0"  src="' + url + '" style="width:100%;height:400px;"></iframe>').window({
                title: '受理调度详细信息',
                width: 800,
                modal: true,
                shadow: true,
                height: 450,
                resizable: false
            });
            $window.window('open');
        }
        function TaskEdite(Code, AbnormalReasonName) {
            var url = "../TaskInfoEdit/?ActionId=<%=Request.QueryString["ActionId"]%>&Code=" + Code + "&AbnormalReasonName=" + encodeURI(AbnormalReasonName);
            var $window = $('#winTaskEdite');
            $window.empty();
            $window = $window.html('<iframe id="TaskInfoEdit" scrolling="no" frameborder="0"  src="' + url + '" style="width:100%;height:650px;"></iframe>').window({
                title: '任务编码[' + Code + ']',
                width: 550,
                modal: true,
                shadow: true,
                height: 700,
                resizable: false
            });
            $window.window('open');
        }

        //        function openAttemperPrint(code, name0, name1, name2, name3, name4, name5, name6, name7) {
        //            window.open("/Report/NoCase_Mode.aspx?code=PrintAttemper.rdlc&AlarmCode=" + encodeURIComponent(code) + "&name0=" + encodeURIComponent(name0) + "&name1=" + encodeURIComponent(name1) + "&name2=" + encodeURIComponent(name2) + "&name3=" + encodeURIComponent(name3) + "&name4=" + encodeURIComponent(name4) + "&name5=" + encodeURIComponent(name5) + "&name6=" + encodeURIComponent(name6) + "&name7=" + encodeURIComponent(name7) + "");
        //        }
        //        function openCommandPrint(code) {
        //            window.open("/Report/NoCase_Mode.aspx?code=PrintCommand.rdlc?TaskCode=" + encodeURIComponent(code) + "");
        //        }
        function openAttemperPrint(AlarmCode, name0, name1, name2, name3, name4, name5, name6, name7) {
            this.parent.$('#tabs').tabs('add', {
                title: "受理记录单打印[" + AlarmCode + "]",
                //                content: createFrame('/BasicInfo/AccLoad/?AlarmCode=' + AlarmCode + "&ActionId="),
                content: createFrame("/Report/NoCase_Mode.aspx?code=PrintAttemper.rdlc&AlarmCode=" + encodeURIComponent(AlarmCode) + "&name0=" + encodeURIComponent(name0) + "&name1=" + encodeURIComponent(name1) + "&name2=" + encodeURIComponent(name2) + "&name3=" + encodeURIComponent(name3) + "&name4=" + encodeURIComponent(name4) + "&name5=" + encodeURIComponent(name5) + "&name6=" + encodeURIComponent(name6) + "&name7=" + encodeURIComponent(name7) + ""),
                closable: true
            });
        }
        function openCommandPrint(TaskCode) {
            this.parent.$('#tabs').tabs('add', {
                title: "出车命令单打印[" + TaskCode + "]",
                content: createFrame("/Report/NoCase_Mode.aspx?code=PrintCommand.rdlc&TaskCode=" + encodeURIComponent(TaskCode) + ""),
                closable: true
            });
        }
        //        function TaeEdite(EventCode) {
        //            var url = "../AlarmEventInfoEdit/?eventCode=" + EventCode;
        //            var $window = $('#winTaeEdite');
        //            $window.empty();
        //            $window = $window.html('<iframe id="AlarmEventEdite" scrolling="no" frameborder="0"  src="' + url + '" style="width:100%;height:200px;"></iframe>').window({
        //                title: '事件编码[' + EventCode + ']',
        //                width: 800,
        //                modal: true,
        //                shadow: true,
        //                height: 250,
        //                resizable: false
        //            });
        //            $window.window('open');
        //        }
        //        function TaeEdite(EventCode) {
        //            var url = "../AlarmEventInfoEdit/?eventCode=" + EventCode;
        //            var $window = $('#winTaeEdite');
        //            $window.empty();
        //            $window = $window.html('<iframe id="AlarmEventEdite" scrolling="no" frameborder="0"  src="' + url + '" style="width:100%;height:200px;"></iframe>').window({
        //                title: '事件编码[' + EventCode + ']',
        //                width: 800,
        //                modal: true,
        //                shadow: true,
        //                height: 250,
        //                resizable: false
        //            });
        //            $window.window('open');
        //        }
        //#endregion


        function createFrame(url) {
            var s = '<iframe id="AlarmEventEdite" scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
            return s;
        }

        <%--        function play(CallTime,RecordCode) {
            var url = "../<%=ViewData["RecordingPlayPage"]%>/?RecordNumber=" + escape(RecordCode) + "&CallTime=" + escape(CallTime);
            var $window = $('<div ></div>').html('<iframe id="MediaPlayerFrame" style="border:0px;width:100%;height:100px;" src="' + url + '" ></iframe>');


            $window.window({
                title: '电话录音:'+RecordCode,
                width: 600,
                modal: true,
                shadow: true,
                height: 140,
                resizable: false,
                onBeforeClose: function () {
                    $window.find("iframe")[0].contentWindow.close();
                }
            });

            $window.window('open');
        }--%>

        function play(CallTime, RecordCode) {
            var url = "../<%=ViewData["RecordingPlayPage"]%>/?RecordNumber=" + escape(RecordCode) + "&CallTime=" + escape(CallTime);

            ShowIframe("电话录音:" + RecordCode, url, 650, 120);
        }

        function autoClose() {
            ClosePop();
        }

        function ModifyRecord(eventCode) {
            var url = "../ModifyRecord/?eventCode=" + escape(eventCode);
            var $window = $('<div ></div>').html('<iframe id="ModifyRecordFrame" style="border:0px;width:100%;height:310px;" src="' + url + '" ></iframe>');
            $window.window({
                title: '查看修改记录',
                width: 1000,
                modal: true,
                shadow: true,
                height: 350,
                resizable: false,
                onBeforeClose: function () {
                    $window.find("iframe")[0].contentWindow.close();
                }
            });
            $window.window('open');
        }
        function freload() {
            window.location = window.location;
            //            window.location.reload();
            //            document.location.reload(true);
        }

        //取消关联到事件
        function DelConnect(EventCode, Desk, Time) {
            var url = "../AlterCallType/?EventCode=" + escape(EventCode) + "&Desk=" + escape(Desk) + "&Time=" + escape(Time) + "&type=0";
            $AlterCallTypeWindow = $('<div ></div>').html('<iframe id="AlterCallTypeFrame" style="border:0px;width:100%;height:410px;" src="' + url + '" ></iframe>');
            $AlterCallTypeWindow.window({
                title: '取消关联',
                width: 400,
                modal: true,
                shadow: true,
                height: 450,
                resizable: false,
                onBeforeClose: function () {
                    $AlterCallTypeWindow.find("iframe")[0].contentWindow.close();
                }
            });
            $AlterCallTypeWindow.window('open');
        }
        //关联到事件
        function Connect(EventCode) {
            var url = "../MediaLink/?ActionId=<%=Request.QueryString["ActionId"]%>&EventCode=" + escape(EventCode);
            $MediaLinkWindow = $('<div ></div>').html('<iframe id="MediaLinkFrame" style="border:0px;width:100%;height:460px;" src="' + url + '" ></iframe>');
            $MediaLinkWindow.window({
                title: '增加录音关联：事件编码[' + EventCode + ']',
                width: 1070,
                modal: true,
                shadow: true,
                height: 500,
                resizable: false,
                onBeforeClose: function () {
                    $MediaLinkWindow.find("iframe")[0].contentWindow.close();
                }
            });
            $MediaLinkWindow.window('open');
        }
    </script>
</head>
<body>
   
   <!--数据-->
    <% 
            Anchor.FA.Model.C_AlarmEventDetail taall = ViewData["taall"] as Anchor.FA.Model.C_AlarmEventDetail;//事件、受理、任务、任务车辆
            Anchor.FA.Model.C_AlarmEventInfo tae = taall.tae;//事件

            List<Anchor.FA.Model.C_AlarmCallInfo> acLs = ViewData["acLs"] as List<Anchor.FA.Model.C_AlarmCallInfo>;//录音
            List<Anchor.FA.Model.TParameterAcceptInfo> tpaLs = ViewData["tpaLs"] as List<Anchor.FA.Model.TParameterAcceptInfo>;//相关事件项 是否显示//设置调度个性名头 数据库配置

            List<Anchor.FA.Model.TZAmbulanceState> zasLs = ViewData["zasLs"] as List<Anchor.FA.Model.TZAmbulanceState>;//车辆状态名称
            
        %>

    <!--主菜单-->
     <div title="事件名称:<%: tae.EvetnName%>" class="easyui-navpanel">
         <header>
            <div class="m-toolbar">
                <div class="m-title">事件名称</div>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div> 
            </div>
        </header>
        <ul class="m-list">
             <li><a href="javascript:void(0)" onclick="$.mobile.go('#divTae');">事件详细信息
                    
                </a>
            </li>
            <%  
            if (Convert.ToBoolean(ViewData["Listen"]))
            { 
            %>
               <li><a href="javascript:void(0)" onclick="$.mobile.go('#divCallGrid');">电话录音</a>
                </li>
            <%} %>
            <li class="m-list-group">受理 </li>
             <%int t = 1;
                    foreach (Anchor.FA.Model.C_AcceptEvent tacAll in taall.TACLS)
                    {
                        Anchor.FA.Model.C_AccInfo tac = tacAll.tac;
                %>
                <li><a href="javascript:void(0)" onclick="$.mobile.go('#divTac<%: t %>');">    第<%: t++ %>次受理</a></li>
            <%} %>
        </ul>
    </div>
    
    <!--事件详细信息-->
    <div id="divTae" class="easyui-navpanel" title="事件详细信息[<%:tae.EventCode%>]" style="width: 1000px;
                                    padding: 10px;"  data-options="tools:'#divPt'">
                                    <header>
                                        <div class="m-toolbar">
                                            <div class="m-title">事件详细信息</div>
                                            <div class="m-left">
                                                <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                                            </div> 
                                        </div>
                                    </header>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <%--                                        <tr>
                                            <th class="TableInfoTH">
                                            </th>
                                            <th align="right" class="TableInfoTH">
                                                <asp:Label ID="Label_AcceptEdit" runat="server" Text=""></asp:Label>
                                            </th>
                                            <th align="right" valign="middle" class="TableInfoTH">
                                                <input type="button" id="ButtonBack" class="btn50" onmouseover="this.className='obtn50'"
                                                onmouseout="this.className='btn50'" value="刷新" onclick="freload()" />
                                            <asp:Label ID="lblPrint" runat="server" Text="打印"></asp:Label>
                                            </th>
                                        </tr>--%>

                                        <tr>
                                            <td width="15%" align="right" valign="middle" class="TableInfoTD">
                                            </td>
                                            <td width="18%" align="left" valign="middle" class="TableInfoTD">
                                            </td>
                                            <td width="15%" align="right" valign="middle" class="TableInfoTD">
                                            </td>
                                            <td width="18%" align="left" valign="middle" class="TableInfoTD">
                                            </td>
                                            <td width="34%" align="right" valign="middle" class="TableInfoTD" colspan="2">
                                    
                                    <div style="float: right;">
                                    <%  
                                        if (Convert.ToBoolean(ViewData["EventPrint"]))
                                    { 
                                    %>
                                    <a href="#" class="easyui-linkbutton" iconcls="icon-print" plain="true"
                                        onclick="javascript:openAttemperPrint('<%:tae.EventCode%>','<%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetAmbulanceStateName(zasLs[0])%>','<%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetAmbulanceStateName(zasLs[1])%>','<%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetAmbulanceStateName(zasLs[2])%>','<%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetAmbulanceStateName(zasLs[3])%>','<%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetAmbulanceStateName(zasLs[4])%>','<%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetAmbulanceStateName(zasLs[5])%>','<%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetAmbulanceStateName(zasLs[6])%>','<%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetAmbulanceStateName(zasLs[7])%>');return false;">
                                        <span style="color: #15428B;">&nbsp;打印</span></a>
                                    <% }%>
                                    </div>


                                    <div style="float: right;">
                                    <a href="#" class="easyui-linkbutton" iconcls="icon-reload" plain="true"
                                        onclick="javascript:freload();return false;"><span style="color: #15428B;">&nbsp;刷新</span></a>
                                    </div>
                                    <div style="float: right;">   
                                    <%  
                                    if (Convert.ToBoolean(ViewData["EditEvent"]))
                                    { 
                                    %>
                                    <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true"
                                        onclick="javascript:TaeEdite('<%:tae.EventCode%>');return false;"><span style="color: #15428B;">&nbsp;修改</span></a>
                                    <% }%>
                                    </div>
                                    <div style="float: right;">   
                                    <%  
                                        if (Convert.ToBoolean(ViewData["ViewEditRecord"]))
                                    { 
                                    %>
                                    <a href="#" class="easyui-linkbutton" iconcls="icon-tip" plain="true"
                                        onclick="ModifyRecord('<%:tae.EventCode%>');return false;"><span style="color: #15428B;">&nbsp;查看修改记录</span></a>
                                    <% }%>
                                    </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="middle">
                                                事件名称:
                                            </td>
                                            <td colspan="3" align="left" valign="middle">
                                                &nbsp;<%: tae.EvetnName%>
                                            </td>
                                            <td align="right" valign="middle">
                                                首次呼救电话:
                                            </td>
                                            <td align="left" valign="middle">
                                                &nbsp;
                                                <%: tae.FirstAlarmCall%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="15%" align="right" valign="middle" class="TableInfoTD">
                                                首次受理时刻:
                                            </td>
                                            <td width="18%" align="left" valign="middle" class="TableInfoTD">
                                                &nbsp;<%: tae.FirstAcceptTime.ToString()%>
                                            </td>
                                            <td width="15%" align="right" valign="middle" class="TableInfoTD">
                                                首次受理调度:
                                            </td>
                                            <td width="18%" align="left" valign="middle" class="TableInfoTD">
                                                &nbsp;<%: tae.FirstDisptcherName%>
                                            </td>
                                            <td width="15%" align="right" valign="middle" class="TableInfoTD">
                                                受理次数:
                                            </td>
                                            <td width="19%" align="left" valign="middle" class="TableInfoTD">
                                                &nbsp;<%: tae.AcceptCount.ToString()%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="middle">
                                                首次派车时刻:
                                            </td>
                                            <td align="left" valign="middle">
                                                &nbsp;<%: tae.FirstSendAmbTime.ToString()%>
                                            </td>
                                            <td align="right" valign="middle">
                                                <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Area", tpaLs)%>
                                            </td>
                                            <td align="left" valign="middle">
                                                &nbsp;<%: tae.Area%>
                                            </td>
                                            <td align="right" valign="middle">
                                                执行任务总数:
                                            </td>
                                            <td align="left" valign="middle">
                                                &nbsp;<%: tae.TransactTaskCount.ToString()%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="middle" class="TableInfoTD">
                                                当前执行任务数:
                                            </td>
                                            <td align="left" valign="middle" class="TableInfoTD">
                                                &nbsp;<%: tae.NonceTransactTaskCount.ToString()%>
                                            </td>
                                            <td align="right" valign="middle" class="TableInfoTD">
                                                撤消受理数:
                                            </td>
                                            <td align="left" valign="middle" class="TableInfoTD">
                                                &nbsp;<%: tae.CancelAcceptCount.ToString()%>
                                            </td>
                                            <td align="right" valign="middle" class="TableInfoTD">
                                                中止任务数:
                                            </td>
                                            <td align="left" valign="middle" class="TableInfoTD">
                                                &nbsp;<%: tae.BreakTaskCount.ToString()%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="middle">
                                                是否挂起:
                                            </td>
                                            <td align="left" valign="middle">
                                                &nbsp;<%: tae.IsHangUp ? "是" : "否"%>
                                            </td>
                                            <td align="right" valign="middle">
                                                <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_EventSource", tpaLs)%>
                                            </td>
                                            <td align="left" valign="middle">
                                                &nbsp;<%: tae.EventSource%>
                                            </td>
                                            <td align="right" valign="middle">
                                                <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_AccidentLevel", tpaLs)%>
                                            </td>
                                            <td align="left" valign="middle">
                                                &nbsp;<%: tae.AccidentLevel%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="middle" class="TableInfoTD">
                                                挂起时刻:
                                            </td>
                                            <td align="left" valign="middle" class="TableInfoTD">
                                                &nbsp;<%: tae.HangUpTime.ToString()%>
                                            </td>
                                            <td align="right" valign="middle" class="TableInfoTD">
                                                <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_EventType", tpaLs)%>
                                            </td>
                                            <td align="left" valign="middle" class="TableInfoTD">
                                                &nbsp;<%: tae.EventType%>
                                            </td>
                                            <td align="right" valign="middle" class="TableInfoTD">
                                                <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_AccType", tpaLs)%>
                                            </td>
                                            <td align="left" valign="middle" class="TableInfoTD">
                                                &nbsp;<%: tae.AccidentType%>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
     
    <!--电话录音-->                  
    <div id="divCallGrid" class="easyui-navpanel" title="电话录音">
        <header>
            <div class="m-toolbar">
                <div class="m-title">电话录音</div>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div> 
            </div>
        </header>
         <table id="AlarmCallGrid" class="easyui-datagrid" align="center" fitcolumns="true"
                                        nowrap="false" striped="false" remotesort="false" width="100%" fit="true">
                                        <thead>
                                            <tr>
                                                <th field="TelNumber" align="center">
                                                    主叫号码
                                                </th>
                                                <th field="CallTime" align="center">
                                                    通话时刻
                                                </th>
                                                <th field="FinishTime" align="center">
                                                    结束时刻
                                                </th>
                                                <th field="DeskName" align="center">
                                                    台号
                                                </th>
                                                <th field="DispatcherName" align="center">
                                                    调度员
                                                </th>
                                                <th field="RecordCode" align="center">
                                                    录音号
                                                </th>
                                                <th field="CallTypeName" align="center">
                                                    通话类型
                                                </th>
                                                <th field="aa" width="20px" align="center">
                                                </th>
                                                <% 
                                                if (Convert.ToBoolean(ViewData["SoundConnect"]))
                                                { 
                                                %>
                                                <th field="aaa" width="20px" align="center">
                                                    取消关联
                                                </th>
                                                <% }%>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <% 
                                                foreach (Anchor.FA.Model.C_AlarmCallInfo ac in acLs)
                                                {
                                            %>
                                            <tr>
                                                <td>
                                                    <%: ac.TelNumber%>
                                                </td>
                                                <td>
                                                    <%: ac.CallTime.ToString()%>
                                                </td>
                                                <td>
                                                    <%: ac.FinishTime.ToString()%>
                                                </td>
                                                <td>
                                                    <%: ac.DeskName%>
                                                </td>
                                                <td>
                                                    <%: ac.DispatcherName%>
                                                </td>
                                                <td>
                                                    <%: ac.RecordCode%>
                                                </td>
                                                <td>
                                                    <%: ac.CallTypeName%>
                                                </td>
                                                <td>
                                                    <img alt="听录音" src="../../Content/images/mediaplayer.png" style="cursor: pointer;"
                                                        onclick="javascript:play('<%: ac.CallTime.ToString()%>','<%: ac.RecordCode%>')" />
                                                </td>
                                                
                                                    <% 
                                                    if (Convert.ToBoolean(ViewData["SoundConnect"]))
                                                    { 
                                                    %>
                                                <td>
                                                    <img alt="取消关联" src="../../Content/images/Closetable_relationship.png" style="cursor: pointer;"
                                                        onclick="javascript:DelConnect('<%:tae.EventCode%>','<%: ac.DeskCode%>','<%: ac.CallTime.ToString()%>')" />
                                                </td>
                                                    <% }%>
                                            </tr>
                                            <%} %>
                                        </tbody>
                                    </table>
    </div>
   
     <!--受理菜单-->  
    <% 
        int x = 1;
        int y = 1;
            foreach (Anchor.FA.Model.C_AcceptEvent tacAll in taall.TACLS)
            {
                Anchor.FA.Model.C_AccInfo tac = tacAll.tac;
     %>
     <div id="divTac<%: x %>" title="第<%: x %>次受理" class="easyui-navpanel">
                 <header>
                    <div class="m-toolbar">
                        <div class="m-title">受理</div>
                        <div class="m-left">
                            <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                        </div> 
                    </div>
                </header>
                <ul class="m-list">
                     <li><a href="javascript:void(0)" onclick="$.mobile.go('#divAcceptEvent<%: x++ %>');">受理详细信息</a>
                    </li>
                    <li class="m-list-group">任务车辆信息</li>
                     <% if (tacAll.TT.Count > 0)
                        {

                            
                            foreach (Anchor.FA.Model.C_Task ttAll in tacAll.TT)
                            {
                                Anchor.FA.Model.C_TaskInfoDetail tInfo = ttAll.tt;
                        %>
                        <li><a href="javascript:void(0)" onclick="$.mobile.go('#divTask<%: y++ %>');"><%: tInfo.TradeMark %>&nbsp;&nbsp;[<%: tInfo.Code%>]</a></li>
                        <%  } 
                        }
                        else
                        {
                    %>
                            <li>本次受理未派任务车辆</li>
                     <%} %>
                </ul>
            </div>
     <%     } %>

    <!--受理信息-->  
     <% 
            int i = 1;
            foreach (Anchor.FA.Model.C_AcceptEvent tacAll in taall.TACLS)
            {
                Anchor.FA.Model.C_AccInfo tac = tacAll.tac;
     %>
            <div id="divAcceptEvent<%: i %>" title="第<%: i++ %>次受理" class="easyui-navpanel" style="padding: 10px" data-options="tools:'#divAcceptEventEdite'">
                 <header>
                    <div class="m-toolbar">
                        <div class="m-title">受理</div>
                        <div class="m-left">
                            <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                        </div> 
                    </div>
                </header>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="right" valign="middle" class="TableInfoTD">
                            责任受理人:
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tac.Dispatcher%>
                        </td>
                        <td align="right" valign="middle" class="TableInfoTD">
                            受理类型:
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tac.AcceptType%>
                        </td>
                        <td align="right" valign="middle" class="TableInfoTD">
                            原因:
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tac.Reason.ToString()%>
                        <%  
                        if (Convert.ToBoolean(ViewData["EditAccept"]))
                        {
                        %>
                            <div style="float: right;">
                                <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true"
                                    onclick="javascript:AcceptEventEdite('<%:tae.EventCode%>',<%: tac.AcceptOrder%>);return false;">
                                    <span style="color: #15428B;">&nbsp;修改</span></a>
                            </div>
                        <% }%>
                        </td>
                    </tr>

                                            
                    <tr>
                        <td align="right" valign="middle">
                            电话振铃时刻:
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tac.RingTime.ToString()%>
                        </td>
                        <td align="right" valign="middle">
                            开始受理时刻:
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tac.AcceptBeginTime.ToString()%>
                        </td>
                        <td align="right" valign="middle">
                            挂起时刻:
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tac.HangUpTime.ToString()%>
                        </td>
                    </tr>
                                            
                    <tr>
                        <td align="right" valign="middle" class="TableInfoTD">
                            发送指令时刻:
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tac.CommandTime.ToString()%>
                        </td>
                        <td align="right" valign="middle" class="TableInfoTD">
                            结束受理时刻:
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tac.AcceptEndTime.ToString()%>
                        </td>
                        <td align="right" valign="middle" class="TableInfoTD">
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                        </td>
                    </tr>


                    <tr>
                        <td align="right" valign="middle">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_AlarmTel", tpaLs)%>
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tac.AlarmTel%>
                        </td>
                        <td align="right" valign="middle">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_AlarmReason", tpaLs)%>
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tac.AlarmReason%>
                        </td>
                        <td align="right" valign="middle" > 
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_IllState", tpaLs)%>
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tac.IllState%>
                        </td>
                    </tr>



                    <tr>
                        <td width="15%" align="right" valign="middle" class="TableInfoTD">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_PatientName", tpaLs)%>
                        </td>
                        <td width="18%" align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tac.PatientName%>
                        </td>
                        <td width="15%" align="right" valign="middle" class="TableInfoTD">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Sex", tpaLs)%>
                        </td>
                        <td width="18%" align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tac.Sex%>
                        </td>
                        <td width="15%" align="right" valign="middle" class="TableInfoTD">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Age", tpaLs)%>
                        </td>
                        <td width="19%" align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tac.Age%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_National", tpaLs)%>
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tac.National%>
                        </td>
                        <td align="right" valign="middle">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Folk", tpaLs)%>
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tac.Folk%>
                        </td>
                        <td align="right" valign="middle">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Judge", tpaLs)%>
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tac.Judge%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle" class="TableInfoTD">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_LinkMan", tpaLs)%>
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tac.LinkMan%>
                        </td>
                        <td align="right" valign="middle" class="TableInfoTD">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_LinkTel", tpaLs)%>
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tac.LinkTel%>
                        </td>
                        <td align="right" valign="middle" class="TableInfoTD">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Extension", tpaLs)%>
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tac.Extension%>
                        </td>
                    </tr>

                                            
                    <tr>
                        <td align="right" valign="middle">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_PatientCount", tpaLs)%>
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tac.PatientCount%>
                        </td>
                        <td align="right" valign="middle">
                            是否需要担架:
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tac.IsNeedLitter ? "是" : "否"%>
                        </td>
                        <td  align="right" valign="middle">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_SpecialNeed", tpaLs)%>
                        </td>
                        <td  align="left" valign="middle">
                            &nbsp;<%: tac.SpecialNeed%>
                        </td>
                    </tr>




                                            
                    <tr>
                        <td align="right" valign="middle" class="TableInfoTD">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_LocalAddr", tpaLs)%>
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tac.LocalAddr%>
                        </td>
                        <td align="right" valign="middle" class="TableInfoTD">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_LocalAddrType", tpaLs)%>
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tac.LocalAddrType%>
                        </td>
                        <td align="right" valign="middle" class="TableInfoTD">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Reserve1", tpaLs)%>
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tac.BackUpOne%>
                        </td>
                    </tr>

                                            
                    <tr>
                        <td align="right" valign="middle">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_WaitAddr", tpaLs)%>
                        </td>
                        <td colspan="3" align="left" valign="middle">
                            &nbsp;<%: tac.WaitAddr%>
                        </td>
                        <td  align="right" valign="middle">
                        </td>
                        <td  align="left" valign="middle">
                        </td>
                    </tr>
                                            
                    <tr>
                        <td align="right" valign="middle" class="TableInfoTD">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_SendAddr", tpaLs)%>
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tac.SendAddr%>
                        </td>
                        <td align="right" valign="middle" class="TableInfoTD">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_SendAddrType", tpaLs)%>
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tac.SendAddrType%>
                        </td>
                        <td align="right" valign="middle" class="TableInfoTD">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Reserve2", tpaLs)%>
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tac.BackUpTwo%>
                        </td>
                    </tr>

                    <tr>
                        <td align="right" valign="middle">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Mpds", tpaLs)%>
                        </td>
                        <td colspan="5" align="left" valign="middle">
                            &nbsp;<%: tac.MPDSRemark%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="left" valign="middle" class="TableInfoTD">
                            &nbsp;&nbsp;派车列表<br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="left" valign="middle">
                            <center>
                                    <%
                    string[] listStr = tac.AmbulanceList.Split(',', ';');
                                            
                                        %>
                                        <textarea name="TextBox_AmbList" rows="2" cols="20" readonly="readonly" id="TextBox_AmbList" style="height:60px;width:97%;"><%: string.Join("\r\n", listStr)%></textarea>

                                </center>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="left" valign="middle" class="TableInfoTD">
                            &nbsp;&nbsp;<%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Remark", tpaLs)%><br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="left" valign="middle">
                            <center>
                    <%--<%=Html.TextArea("name", tac.Remark)%>--%>

                    <textarea name="TextBox_Mark" rows="2" cols="20" readonly="readonly" id="TextBox_Mark" style="width:97%;"><%: tac.Remark%></textarea>


                                </center>
                        </td>
                    </tr>
                </table>
                
            </div>
     <%     } %>
  
    <!--任务车辆信息-->  
    <% 
        foreach (Anchor.FA.Model.C_AcceptEvent tacAll in taall.TACLS)
        {
            Anchor.FA.Model.C_AccInfo tac = tacAll.tac;
           
            if (tacAll.TT.Count > 0)
            {
                int v = 1;
                
                foreach (Anchor.FA.Model.C_Task ttAll in tacAll.TT)
                {
                    Anchor.FA.Model.C_TaskInfoDetail tInfo = ttAll.tt;
            %>
            <div id="divTask<%: v++ %>" class="easyui-navpanel" title="<%: tInfo.TradeMark %>&nbsp;&nbsp;[<%: tInfo.Code%>]" style="padding: 10px">

                 <header>
                    <div class="m-toolbar">
                        <div class="m-title">出车</div>
                        <div class="m-left">
                            <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                        </div> 
                    </div>
                </header>
                <table id="TaskGrid" width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <th  align="left" valign="middle" class="TableInfoTH">
                            &nbsp; 车辆信息
                        </th>
                        <td colspan="5" align="right" valign="middle" class="TableInfoTH">
                                                        
                            <%  
                            if (Convert.ToBoolean(ViewData["TaskPrint"]))
                            { 
                            %>
                            <div style="float: right;">
                                <a href="#" class="easyui-linkbutton" iconcls="icon-print" plain="true"
                                    onclick="javascript:openCommandPrint('<%: tInfo.Code%>');return false;"><span style="color: #15428B;">&nbsp;打印命令单</span></a>
                            </div>
                            <% }%>

                            <%  
                            if (Convert.ToBoolean(ViewData["EditTask"]))
                            { 
                            %>
                            <div style="float: right;">
                                <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true"
                                    onclick="javascript:TaskEdite('<%: tInfo.Code%>','<%: tInfo.AbnormalReasonName%>');return false;"><span style="color: #15428B;">&nbsp;修改</span></a>
                            </div>
                            <%}%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle" width="15%">
                            车牌号码:
                        </td>
                        <td align="left" valign="middle" width="18%">
                            &nbsp;<%: tInfo.TradeMark%>
                        </td>
                        <td align="right" valign="middle" width="15%">
                            实际标识:
                        </td>
                        <td align="left" valign="middle" width="18%">
                            &nbsp;<%: tInfo.RealSign%>
                        </td>
                        <td align="right" valign="middle" width="15%">
                            车辆编码:
                        </td>
                        <td align="left" valign="middle" width="19%">
                            &nbsp;<%: tInfo.AmbulanceCode%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle" class="TableInfoTD">
                            分站:
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tInfo.StationName%>
                        </td>
                        <td align="right" valign="middle" class="TableInfoTD">
                            车辆类型:
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tInfo.AmbulanceTypeName%>
                        </td>
                        <td align="right" valign="middle" class="TableInfoTD">
                            随车电话:
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;&nbsp;<%: tInfo.FollowTel%>
                        </td>
                    </tr>
                    <tr>
                        <th colspan="6" align="left" valign="middle" class="TableInfoTH">
                            &nbsp; 任务信息&nbsp;&nbsp;<%--<input id="Button_Case" type="button" value="病历信息" class="btn90"
                                onmouseover="this.className='obtn90'" onmouseout="this.className='btn90'" />--%>
                        </th>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                            任务流水号:
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tInfo.TaskOrder.ToString()%>
                        </td>
                        <td align="right" valign="middle">
                            送往地点:
                        </td>
                        <td colspan="3" align="left" valign="middle">
                            &nbsp;<%: tInfo.RealSendAddr%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle" class="TableInfoTD">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetAmbulanceStateName(zasLs[0])%>
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tInfo.CreateTaskTime.ToString()%>
                        </td>
                        <td align="right" valign="middle" class="TableInfoTD">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetAmbulanceStateName(zasLs[1])%>
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tInfo.ReceiveCmdTime.ToString()%>
                        </td>
                        <td align="right" valign="middle" class="TableInfoTD">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetAmbulanceStateName(zasLs[2])%>
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tInfo.AmbulanceLeaveTime.ToString()%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetAmbulanceStateName(zasLs[3])%>
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tInfo.ArriveSceneTime.ToString()%>
                        </td>
                        <td align="right" valign="middle">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetAmbulanceStateName(zasLs[4])%>
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tInfo.LeaveSceneTime.ToString()%>
                        </td>
                        <td align="right" valign="middle">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetAmbulanceStateName(zasLs[5])%>
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tInfo.ArriveHospitalTime.ToString()%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle" class="TableInfoTD">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetAmbulanceStateName(zasLs[6])%>
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tInfo.FinishTime.ToString()%>
                        </td>
                        <td align="right" valign="middle" class="TableInfoTD">
                            <%: Anchor.FA.BLL.BasicInfo.AlarmEvent.GetAmbulanceStateName(zasLs[7])%>
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tInfo.ReturnTime.ToString()%>
                        </td>
                        <td align="right" valign="middle" class="TableInfoTD">
                            是否正常结束:
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tInfo.IsNormalFinish ? "是" : "否"%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
                            行驶公里数:
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tInfo.TravelDistance%>
                        </td>
                        <td align="right" valign="middle">
                            急救公里数:
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tInfo.HelpDistance%>
                        </td>
                        <td align="right" valign="middle">
                            异常原因:
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tInfo.AbnormalReasonName%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle" class="TableInfoTD">
                            司机:
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tInfo.Driver%>
                        </td>
                        <td align="right" valign="middle" class="TableInfoTD">
                            医生:
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tInfo.Doctor%>
                        </td>
                        <td align="right" valign="middle" class="TableInfoTD">
                            护士:
                        </td>
                        <td align="left" valign="middle" class="TableInfoTD">
                            &nbsp;<%: tInfo.Nurse%>
                        </td>
                    </tr>
                                                    
                    <tr>
                        <td align="right" valign="middle">
                            实际救治人数:
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tInfo.CureAmount.ToString()%>
                        </td>
                        <td align="right" valign="middle">
                            担架员:
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tInfo.Litter%>
                        </td>
                        <td align="right" valign="middle">
                            <%--急救员:--%>
                            抢救员:
                        </td>
                        <td align="left" valign="middle">
                            &nbsp;<%: tInfo.Salver%>
                        </td>
                    </tr>
                </table>
                <div style="height: 300px">
                    <table id="AmbulanceStateTimeGrid" class="easyui-datagrid" align="center" fitcolumns="true"
                        nowrap="false" striped="true" remotesort="false" width="800px" fit="true">
                        <thead>
                            <%--<tr>
                            <th field="RealSign" align="center" width="12%">
                                实际标识
                            </th>
                            <th field="WorkStateName" align="center" width="12%">
                                车辆状态
                            </th>
                            <th field="KeyPressTime" align="center" width="26%">
                                时刻值
                            </th>
                            <th field="SaveTime" align="center" width="26%">
                                记录时刻
                            </th>
                            <th field="SourceName" align="center" width="12%">
                                操作来源
                            </th>
                            <th field="JobCode" align="center" width="12%">
                                工号
                            </th>
                        </tr>--%>
                            <tr>
                                <th field="RealSign" align="center">
                                    <span style="font-weight: bold">实际标识</span>
                                </th>
                                <th field="WorkStateName" align="center">
                                    <span style="font-weight: bold">车辆状态</span>
                                </th>
                                <th field="KeyPressTime" align="center">
                                    <span style="font-weight: bold">时刻值</span>
                                </th>
                                <th field="SaveTime" align="center">
                                    <span style="font-weight: bold">记录时刻</span>
                                </th>
                                <th field="SourceName" align="center">
                                    <span style="font-weight: bold">操作来源</span>
                                </th>
                                <th field="JobCode" align="center">
                                    <span style="font-weight: bold">工号</span>
                                </th>
                                <th field="OperationCode" align="center">
                                    <span style="font-weight: bold">编码</span>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <% 
                    foreach (Anchor.FA.Model.C_AmbulanceStateTimeInfo ast in ttAll.tastLs)
                    {
                            %>
                            <tr>
                                <td>
                                    <%: ast.RealSign%>
                                </td>
                                <td>
                                    <%: ast.WorkStateName%>
                                </td>
                                <td>
                                    <%: ast.KeyPressTime.ToString().StartsWith("1900") ? "已删除" : ast.KeyPressTime.ToString()%>
                                </td>
                                <td>
                                    <%: ast.SaveTime.ToString()%>
                                </td>
                                <td>
                                    <%: ast.SourceName%>
                                </td>
                                <td>
                                    <%: ast.JobCode%>
                                </td>
                                <td>
                                    <%: ast.OperationCode%>
                                </td>
                            </tr>
                            <%} %>
                        </tbody>
                    </table>
                </div>
            </div>
    <%  }   
            }
        } %>

    <div id="winTaeEdite">
    </div>
    <%--<div id="win">
    </div>--%>
    <div id="winAcceptEventEdite">
    </div>
    <div id="winTaskEdite">
    </div>
</body>
</html>

<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<% 
    Anchor.FA.Model.C_AlarmEventInfo tae = ViewData["aeInfo"] as Anchor.FA.Model.C_AlarmEventInfo;//事件
    List<Anchor.FA.Model.TParameterAcceptInfo> tpaLs = ViewData["tpaLs"] as List<Anchor.FA.Model.TParameterAcceptInfo>;//相关事件项 是否显示//设置调度个性名头 数据库配置
    string label_Area = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Area", tpaLs);
    string label_EventSource = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_EventSource", tpaLs);
    string label_AccidentLevel = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_AccidentLevel", tpaLs);
    string label_EventType = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_EventType", tpaLs);
    string label_AccType = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_AccType", tpaLs);
    
%>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title></title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
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
    <% 
        Anchor.FA.Model.C_AlarmEventInfo tae = ViewData["aeInfo"] as Anchor.FA.Model.C_AlarmEventInfo;//事件
        List<Anchor.FA.Model.TParameterAcceptInfo> tpaLs = ViewData["tpaLs"] as List<Anchor.FA.Model.TParameterAcceptInfo>;//相关事件项 是否显示//设置调度个性名头 数据库配置
        string label_Area = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Area", tpaLs);
        string label_EventSource = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_EventSource", tpaLs);
        string label_AccidentLevel = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_AccidentLevel", tpaLs);
        string label_EventType = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_EventType", tpaLs);
        string label_AccType = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_AccType", tpaLs);
    
    %>
    <script type="text/javascript" language="javascript">


        $(function () {
        
            //#region 下拉菜单初始化
            //区域
            <% if(label_Area!="")
            { 
            %>
            EUIcombobox("#DropDownList_Area", {
                valueField: "名称",
                textField: "名称",
                OneOption: [{
                    编码: "",
                    名称: "--请选择--"
                }],
                onLoadSuccess: function (data) {
                    $('#DropDownList_Area').combobox('setValue', '<%: tae.Area==""? "--请选择--":tae.Area%>');
                },
                url: "/BasicInfo/LoadAreas/"

            });
            <%}else{%>
            $("#DropDownList_Area").parent().children().hide();
            <%}%>

            //事件来源
            <% if(label_EventSource!=""){ %>
            EUIcombobox("#DropDownList_EventSource", {
                valueField: "编码",
                textField: "名称",
                OneOption: [{
                    编码: "-1",
                    名称: "--请选择--"
                }],
                onLoadSuccess: function (data) {
                    $('#DropDownList_EventSource').combobox('setValue', '<%: tae.EventSourceCode%>');
                },
                url: "/BasicInfo/LoadAlarmOriTypes/"

            });
            <%}else{%>
           $("#DropDownList_EventSource").parent().children().hide();
            <%}%>

            //事故等级
            <% if(label_AccidentLevel!=""){ %>
            EUIcombobox("#DropDownList_AccidentLevel", {
                valueField: "编码",
                textField: "名称",
                OneOption: [{
                    编码: "-1",
                    名称: "--请选择--"
                }],
                onLoadSuccess: function (data) {
                    $('#DropDownList_AccidentLevel').combobox('setValue', '<%: tae.AccidentLevelCode%>');
                },
                url: "/BasicInfo/LoadAccidentLevels/"

            });
            <%}else{%>
           $("#DropDownList_AccidentLevel").parent().children().hide();
            <%}%>

            //呼救类型 事件类型
            <% if(label_EventType!=""){ %>
            EUIcombobox("#DropDownList_EventType", {
                valueField: "编码",
                textField: "名称",
                OneOption: [{
                    编码: "-1",
                    名称: "--请选择--"
                }],
                onLoadSuccess: function (data) {
                    $('#DropDownList_EventType').combobox('setValue', '<%: tae.EventTypeCode%>');
                },
                url: "/BasicInfo/LoadAlarmTypes/"

            });
            <%}else{%>
           $("#DropDownList_EventType").parent().children().hide();
            <%}%>

            //事故类型 这个得做树形
            <% if(label_AccType!=""){ %>
                    $('#DropDownList_AccidentType').combotree({
                    url: '/BasicInfo/GetAccidentTypeTree/',
                    onLoadSuccess: function (data) {
                        $('#DropDownList_AccidentType').combotree('setValue', '<%: tae.AccidentTypeCode %>');
                    }
                });
            <%}else{%>
           $("#DropDownList_AccidentType").parent().children().hide();
            <%}%>

            //#endregion

        });
        function Save()
        {
            var paramNames="EventCode="+$("#EventCode").val()
            +"&EvetnName="+escape($("#EvetnName").val())
            +"&Area="+escape($("#DropDownList_Area").combobox('getValue')=="--请选择--"?"":$("#DropDownList_Area").combobox('getValue'))
            +"&EventSource="+escape($("#DropDownList_EventSource").combobox('getValue'))
            +"&AccidentLevel="+escape($("#DropDownList_AccidentLevel").combobox('getValue'))
            +"&EventType="+escape($("#DropDownList_EventType").combobox('getValue'))
            +"&AccidentType="+escape($("#DropDownList_AccidentType").combotree('getValue'));
            //alert(paramNames);
            $.ajax({
                type: "POST",
                async: false,
                url: "/BasicInfo/AlarmEventSave/",
                data: paramNames,
                success: function (data, textStatus, jqXHR) {
                    //alert(data);
                    $("#SaveResult").empty();
                    if(data==""){
                    $("#SaveResult").append($("<font color='green'>写入数据库成功！</font>"));
                    }else{
                    $("#SaveResult").append($("<font color='red'>写入数据库失败！"+data+"</font>"));
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("/BasicInfo/AlarmEventSave/");
                }
            });
        }
        function closePage() {
            window.parent.closeWindowR();
        }

    </script>
</head>
<body>
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <th colspan="5" align="left" valign="middle" class="TableInfoTH">
                    &nbsp;事件详细信息[<%:tae.EventCode%>]
                    <input id="EventCode" value="<%:tae.EventCode%>" type="hidden" name="entity.EventCode" />
                </th>
                <th align="right" valign="middle" class="TableInfoTH">
                    <div style="float: right;">
                        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true"
                            onclick="closePage()"><span style="color: #15428B;">&nbsp;返回</span></a>
                    </div>
                    <div style="float: right;">
                        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true"
                            onclick="javascript:Save()"><span style="color: #15428B;">&nbsp;保存</span></a>
                    </div>
                </th>
            </tr>
            <tr height="30px">
                <td align="right" valign="middle">
                    事件名称：
                </td>
                <td colspan="3" align="left" valign="middle">
                    <input id="EvetnName" value="<%: tae.EvetnName%>" type="text" style="width: 350px" class="easyui-validatebox"
                        name="entity.EvetnName" />
                </td>
                <td align="right" valign="middle">
                    首次呼救电话：
                </td>
                <td align="left" valign="middle">
                    <%: tae.FirstAlarmCall%>
                </td>
            </tr>
            <tr height="30px">
                <td width="15%" align="right" valign="middle" class="TableInfoTD">
                    首次受理时刻：
                </td>
                <td width="18%" align="left" valign="middle" class="TableInfoTD">
                    <%: tae.FirstAcceptTime.ToString()%>
                </td>
                <td width="15%" align="right" valign="middle" class="TableInfoTD">
                    首次受理调度：
                </td>
                <td width="18%" align="left" valign="middle" class="TableInfoTD">
                    <%: tae.FirstDisptcherName%>
                </td>
                <td width="15%" align="right" valign="middle" class="TableInfoTD">
                    受理次数：
                </td>
                <td width="19%" align="left" valign="middle" class="TableInfoTD">
                    <%: tae.AcceptCount.ToString()%>
                </td>
            </tr>
            <tr height="30px">
                <td align="right" valign="middle">
                    首次派车时刻：
                </td>
                <td align="left" valign="middle">
                    <%: tae.FirstSendAmbTime.ToString()%>
                </td>
                <td align="right" valign="middle">
                    <%:label_Area%>
                </td>
                <td align="left" valign="middle">
                    <input class="easyui-combobox" style="width: 120px;" panelheight="100px" id="DropDownList_Area"
                        editable="false" name="entity.Area" />
                </td>
                <td align="right" valign="middle">
                    执行任务总数：
                </td>
                <td align="left" valign="middle">
                    <%: tae.TransactTaskCount.ToString()%>
                </td>
            </tr>
            <tr height="30px">
                <td align="right" valign="middle" class="TableInfoTD">
                    当前执行任务数：
                </td>
                <td align="left" valign="middle" class="TableInfoTD">
                    <%: tae.NonceTransactTaskCount.ToString()%>
                </td>
                <td align="right" valign="middle" class="TableInfoTD">
                    撤消受理数：
                </td>
                <td align="left" valign="middle" class="TableInfoTD">
                    <%: tae.CancelAcceptCount.ToString()%>
                </td>
                <td align="right" valign="middle" class="TableInfoTD">
                    中止任务数：
                </td>
                <td align="left" valign="middle" class="TableInfoTD">
                    <%: tae.BreakTaskCount.ToString()%>
                </td>
            </tr>
            <tr height="30px">
                <td align="right" valign="middle">
                    是否挂起：
                </td>
                <td align="left" valign="middle">
                    <%: tae.IsHangUp ? "是" : "否" %>
                </td>
                <td align="right" valign="middle">
                    <%: label_EventSource%>
                </td>
                <td align="left" valign="middle">
                    <input class="easyui-combobox" style="width: 120px;" panelheight="100px" id="DropDownList_EventSource"
                        editable="false" name="entity.EventSource" />
                </td>
                <td align="right" valign="middle">
                    <%: label_AccidentLevel%>
                </td>
                <td align="left" valign="middle">
                    <input class="easyui-combobox" style="width: 120px;" panelheight="100px" id="DropDownList_AccidentLevel"
                        editable="false" name="entity.AccidentLevel" />
                </td>
            </tr>
            <tr height="30px">
                <td align="right" valign="middle" class="TableInfoTD">
                    挂起时刻：
                </td>
                <td align="left" valign="middle" class="TableInfoTD">
                    <%: tae.HangUpTime.ToString()%>
                </td>
                <td align="right" valign="middle" class="TableInfoTD">
                    <%: label_EventType%>
                </td>
                <td align="left" valign="middle" class="TableInfoTD">
                    <input class="easyui-combobox" style="width: 120px;" panelheight="100px" id="DropDownList_EventType"
                        editable="false" name="entity.EventType" />
                </td>
                <td align="right" valign="middle" class="TableInfoTD">
                    <%: label_AccType%>
                </td>
                <td align="left" valign="middle" class="TableInfoTD">
                    <select id="DropDownList_AccidentType" panelheight="130px" class="easyui-combotree"
                        required="true" name="entity.AccidentType" style="width: 120px;">
                    </select>
                </td>
            </tr>
            <tr>
                <td id="SaveResult" colspan="6" style="text-align: center;">
                </td>
            </tr>
        </table>
    </div>
</body>
</html>

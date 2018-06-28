<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width" />
    <title></title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript" language="javascript">

    //#region 查询
      function Search(type) {
        $('#grid').datagrid('options').url = '/BasicInfo/GetAlarmCallOthers/';
//        var type="load";type="reload";
        $('#grid').datagrid(type, {
            m_BeginTime: $('#BeginDate').datebox('getValue')+' '+$('#BeginTime').val(),
            m_EndTime: $('#EndDate').datebox('getValue')+' '+ $('#EndTime').val(),

            callNumber: $('#txtCallNumber').val(),
            recordNumber: $('#txtRecordNumber').val(),
            remark: $('#txtRemark').val(),

            deskNumber: $('#ddlDesk').combobox('getValue'),
            attemperCode: $('#ddlAttemper').combobox('getValue'),
            callTypeCode: $('#ddlCallType').combobox('getValue'),
            isCallOut: $('#ddlIsCallOut').combobox('getValue'),
            ActionId: "<%=Request.QueryString["ActionId"]%>"
        });
    }
    //#endregion
    $(function () {
        $('#BeginTime').mask('99:99:99');
        $('#EndTime').mask('99:99:99');
        $('#c_BeginTime').mask('99:99:99');
        $('#c_EndTime').mask('99:99:99');

        //#region 下拉菜单初始化
        //调度员
        EUIcombobox("#ddlAttemper", {
            valueField: "编码",
            textField: "姓名",
            OneOption: [{
                编码: "",
                姓名: "--请选择--"
            }],
            url: "/BasicInfo/LoadDis/?ActionId=<%=Request.QueryString["ActionId"]%>"

        });
        //台号
        EUIcombobox("#ddlDesk", {
            valueField: "台号",
            textField: "显示名称",
            OneOption: [{
                台号: "",
                显示名称: "--请选择--"
            }],
            url: "/Notice/DeskLoad/?ActionId=<%=Request.QueryString["ActionId"]%>"

        });
        //通话类型
        EUIcombobox("#ddlCallType", {
            valueField: "编码",
            textField: "名称",
            OneOption: [{
                编码: "",
                名称: "--请选择--"
            }],
            url: "/BasicInfo/LoadCallType/"

        });
        //#endregion
        Search('load');


    });

        function closePage()
        {
            parent.freload();
            parent.$MediaLinkWindow.window('close');
        }
        function play(CallTime,RecordCode) {
            CallTime=renderTime(CallTime);
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
        }
        function LinkCalls(EventCode, Desk, Time){
            Time=renderTime(Time);
            var url = "../AlterCallType/?EventCode=" + escape(EventCode) + "&Desk=" + escape(Desk) + "&Time=" + escape(Time) + "&type=1";
            $LinkCallsWindow = $('<div ></div>').html('<iframe id="LinkCallsFrame" style="border:0px;width:100%;height:410px;" src="' + url + '" ></iframe>');
            $LinkCallsWindow.window({
                title: '关联到事件',
                width: 400,
                modal: true,
                shadow: true,
                height: 450,
                resizable: false,
                onBeforeClose: function () {
                    $LinkCallsWindow.find("iframe")[0].contentWindow.close();
                }
            });
            $LinkCallsWindow.window('open');
        }
        //#region 格式转换
        function renderPlay(value,rowData,rowIndex) {
//            (CallTime,RecordCode)
            var str="<img alt=\"听录音\" src=\"../../Content/images/mediaplayer.png\" style=\"cursor: pointer;\" onclick=\"javascript:play('"+rowData.通话时刻+"','"+rowData.录音号+"')\" />";
            return str;
        }
        function renderLinkCalls(value,rowData,rowIndex) {
//            (EventCode, Desk, Time)
            var str="<img alt=\"关联到事件\" src=\"../../Content/images/table_relationship.png\" style=\"cursor: pointer;\" onclick=\"javascript:LinkCalls('<%:ViewData["EventCode"]%>','"+rowData.台号+"','"+rowData.通话时刻+"')\" />";
            return str;
        }
        //#endregion
    </script>

</head>
<body class="easyui-layout">
    <div region="center" style="padding: 5px" border="true">
        <table id="grid" class="easyui-datagrid" align="center" toolbar="#tb"
            pagination="true" pagenumber="1" pagelist="[20, 25, 30]" pagesize="20" idfield="录音号"
            fitcolumns="true" nowrap="false" striped="true" rownumbers="true" singleselect="true"
            sortname="通话时刻" sortorder="desc" remotesort="false" width="1000px" fit="true">

            <thead>
                <tr>
<%--                    <th field="id" width="100px" hidden="true" align='center' sortable="true">
                        编码
                    </th>--%>
                    <th field="主叫号码" align='center' sortable="true">
                        主叫号码
                    </th>
                    <th field="通话时刻" align='center' sortable="true" formatter='renderTime'>
                        通话时刻
                    </th>
                    <th field="结束时刻" align='center' sortable="true" formatter='renderTime'>
                        结束时刻
                    </th>
                    <th field="显示名称" align='center' sortable="true">
                        台号
                    </th>
                    <th field="调度员" align='center' sortable="true">
                        调度员
                    </th>
                    <th field="录音号" align='center'  sortable="true">
                        录音号
                    </th>
                    <th field="通话类型" align='center' sortable="true">
                        通话类型
                    </th>
                    <th field="aa" align='center' formatter='renderPlay'>
                    </th>
                    <th field="ab" align='center' formatter='renderLinkCalls'>
                        关联事件
                    </th>
                </tr>
            </thead>
        </table>
    </div>
    <div id="tb" style="padding: 5px; height: auto; display: none">
        <table border="0" cellspacing="0" cellpadding="0" width="100%" height="10%">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    通话时刻:
                </td>
                <td>
                    <input id="BeginDate" class="easyui-datebox" style="width: 90px" value="<%= ((dynamic)this.ViewData["BeginDate"]) %>" />
                    <input id="BeginTime" class="easyui-validatebox" style="border: 1px solid #8DB2E3;
                        width: 50px; height: 15px" value="<%=ViewData["Time"]%>">
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    至：
                </td>
                <td>
                    <input id="EndDate" class="easyui-datebox" style="width: 90px" value="<%= ((dynamic)this.ViewData["EndDate"]) %>" />
                    <input id="EndTime" class="easyui-validatebox" style="border: 1px solid #8DB2E3;
                        width: 50px; height: 15px" value="<%=ViewData["Time"]%>">
                </td>
                <td>
                    &nbsp;
                </td>
                <td colspan="4" align="right">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-no" onclick="closePage();return false;"><span style="color: #15428B;">&nbsp;关闭并刷新</span></a>
                    &nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" onclick="Search('load');return false;"><span style="color: #15428B;">&nbsp;查询</span></a>
                    
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    台号:
                </td>
                <td>
                    <input class="easyui-combobox" style="width: 150px;" panelheight="200px" id="ddlDesk"
                        editable="false" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    调度员:
                </td>
                <td>
                    <input class="easyui-combobox" style="width: 150px;" panelheight="400px" id="ddlAttemper"
                        editable="false" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    通话类型:
                </td>
                <td>
                    <input class="easyui-combobox" style="width: 150px;" panelheight="200px" id="ddlCallType"
                        editable="false" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    主叫号码:
                </td>
                <td>
                    <input id="txtCallNumber" class="easyui-validatebox" style="border: 1px solid #8DB2E3; width: 144px;
                        height: 15px;" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    录音号:
                </td>
                <td>
                    <input id="txtRecordNumber" class="easyui-validatebox" style="border: 1px solid #8DB2E3; width: 144px;
                        height: 15px;" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    是否呼出:
                </td>
                <td>
                    <select id="ddlIsCallOut" class="easyui-combobox" style="width:150px;" panelheight="100px" editable="false">
                        <option value="" selected="true">--请选择--</option>
                        <option value="False">呼入</option>
                        <option value="True">呼出</option>
                    </select>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
            <tr>
                
                <td>
                    &nbsp;
                </td>
                <td>
                    备注:
                </td>
                <td>
                    <input id="txtRemark" class="easyui-validatebox" style="border: 1px solid #8DB2E3;
                        width: 144px; height: 15px;" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    
                </td>
                <td>

                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    
                </td>
                <td>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
<%--    <div id="w" class="easyui-window" title="事件受理信息" minimizable="false" maximizable="false"
        icon="icon-save" style="width: 600px; height: 240px; padding: 5px; background: #fafafa;"
        closed="true">
        <div region="center" border="false" style="padding-left: 10px; background: #fff;
            border: 1px solid #ccc;">
            <table id="acc" align="center">
            </table>
        </div>
        <div region="south" border="false" style="text-align: center; height: 30px; line-height: 30px;">
            <a id="btnEp" class="easyui-linkbutton" icon="icon-tip" onclick="accinfo()">查看</a>
            <a id="btnCancel" class="easyui-linkbutton" icon="icon-cancel" href="">取消</a>
        </div>
    </div>--%>
</body>
</html>


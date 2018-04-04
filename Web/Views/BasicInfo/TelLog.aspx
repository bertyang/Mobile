<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>电话流水</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script src="/Content/Script/popup.js" type="text/javascript"></script>
    <script src="/Content/Script/popupclass.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        $(function () {
            $('#BeginTime').mask('99:99:99');
            $('#EndTime').mask('99:99:99');

            $('#btnSearch').bind('click',
                   function () {
                       $('#grid').datagrid('options').url = '/BasicInfo/TelLogSearch/?begin=' + escape($('#BeginDate').datebox('getValue')) + ' ' + escape($('#BeginTime').val())
                                       + '&&end=' + escape($('#EndDate').datebox('getValue')) + ' ' + escape($('#EndTime').val())
                                       + '&&tel=' + escape($('#txt_TelCode').val())
                                       + '&&rec=' + escape($('#txt_RecordType').combobox('getText'))
                                       + '&&op=' + escape($('#txt_OperatorTypes').combobox('getText'))
                                       + '&&res=' + escape($('#txt_Result').combobox('getText'))
                                       + '&&des=' + escape($('#txt_Desks').combobox('getText'))
                                       + '&&ActionId=<%= ((dynamic)this.ViewData["ActionId"]) %>';

                       var p1 = $('#grid').datagrid('getPager');
                       $(p1).pagination({ pageNumber: 1});
                       $('#grid').datagrid('options').page = 1;
                       $('#grid').datagrid('reload')
                   }
              );
        });


        function formatDetail(val, rec) {

            if (rec.RecordCode == null || rec.CallTime == null) {
                return ""
            }
            else {
                return "<img alt='听录音' src='../../Content/images/mediaplayer.png' style='cursor: pointer;'  onclick='play(\"" + rec.CallTime + '\",\"' + rec.RecordCode + "\")' />";
            }
        }

<%--        function play(CallTime, RecordCode) {
            var url = "/BasicInfo/<%=ViewData["RecordingPlayPage"]%>/?RecordNumber=" + escape(RecordCode) + "&CallTime=" + escape(CallTime);
            var $window = $('<div ></div>').html('<iframe id="MediaPlayerFrame" style="border:0px;width:100%;height:100px;" src="' + url + '" ></iframe>');

            $window.window({
                title: '电话录音:' + RecordCode,
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
            var url = "/BasicInfo/<%=ViewData["RecordingPlayPage"]%>/?RecordNumber=" + escape(RecordCode) + "&CallTime=" + escape(CallTime);

            ShowIframe("电话录音:" + RecordCode, url, 650, 120);
        }

        function autoClose() {
            ClosePop();
        }

        //function formatAlarmEvent(val, rec) {

        //    if (val != null)
        //    {
        //        var url = '/BasicInfo/AccLoad/?ActionId=10004&id=' +val;

        //        var icon = "tu1703";

        //        return "<a href='#' onclick=AddTab('事件详细信息[" + val + "]','" + url + "','" + icon + "')><img alt='查看' src='../../Content/images/find.gif' border='0'/></a>";
        //    }
        //}
        function formatRead(val, row) {
            return "<a href='#' onclick=\"ReadTelLog('" + val + "')\"><img alt='查看' src='../../Content/images/vie.gif' border='0'/></a>";
        }

        function ReadTelLog(val) {
           
            $('#grid').datagrid('selectRecord', val);
            var node = $('#grid').datagrid('getSelected');

            $('#ReadTelLog').show();
            $('#ReadTelLog').dialog({
                collapsible: true,
                minimizable: true,
                maximizable: true,
                height: 520,
                width: 400,
                modal: true,
                title: '电话详细信息',
                buttons: [
                {
                    text: '关闭',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        $('#ReadTelLog').dialog('close');
                    }
                }]
            });

            $('#RecordStyle').text(node.RecordStyle);
            $('#Tel').text(node.Tel);
            $('#RecordTime').text(node.RecordTime);
            $('#InhaleTime').text(node.InhaleTime);
            $('#FellInTime').text(node.FellInTime);
            $('#ShakeBellTime').text(node.ShakeBellTime);
            $('#CallTime').text(node.CallTime);
            $('#MiddleHandleTime').text(node.MiddleHandleTime);
            $('#FinishTime').text(node.FinishTime);
            $('#Desk').text(node.Desk);
            $('#Dispatcher').text(node.Dispatcher);
            $('#RecordCode').text(node.RecordCode);
            $('#Result').text(node.Result);
            $('#OP').text(node.OP);
        }
    </script>
</head>
<body class="easyui-layout">
    <div region="center" style="padding: 5px" border="true">
        <table id="grid" class="easyui-datagrid" align="center" toolbar="#tb" url="/BasicInfo/TelLogLoad/?begin=<%= ((dynamic)this.ViewData["BeginDate"]) %> <%= ((dynamic)this.ViewData["Time"]) %>&&end=<%= ((dynamic)this.ViewData["EndDate"]) %> <%= ((dynamic)this.ViewData["Time"]) %>&&ActionId= <%= ((dynamic)this.ViewData["ActionId"]) %>"  
            pagination="true" pagenumber="1" pagelist="[25, 30, 35]" pagesize="15" idfield="ID"
            fitcolumns="true" nowrap="false" striped="true" rownumbers="true" singleselect="true"
             remotesort="false" width="1000px" fit="true">
            <thead>
                <tr>
                    <th field="RecordStyle" width="50px" align='center' sortable="true">
                        记录类型
                    </th>
                    <th field="Tel" width="80px" align='center' sortable="true">
                        对方电话
                    </th>
                    <th field="RecordTime" width="100px" align='center' sortable="true">
                        产生时刻
                    </th>
                    <th field="Desk" width="100px" align='center' sortable="true">
                        接听台号
                    </th>
                    <th field="Dispatcher" width="80px" align='center' sortable="true">
                        调度员
                    </th>
                    <th field="RecordCode" width="300px" align='center' sortable="true">
                        录音号
                    </th>
                    <th field="Result" width="80px" align='center' sortable="true">
                        结果
                    </th>
                    <th field="ID"  width="50px" align='center' sortable="true" formatter="formatDetail">
                        听录音
                    </th>
                    <th field="Num" width="20" align='center' formatter='formatRead'>查看</th>    
                  <%--  <th field="AlarmEventCode" width="50px" align='center' sortable="true" formatter="formatAlarmEvent">
                        关联事件
                    </th>--%>
                </tr>
            </thead>
        </table>
    </div>
    <div id="tb" style="padding: 5px; height: auto">
        <div>
            <table>
                <td align="right">
                    开始时间:
                </td>
                <td align="left">
                    <input id="BeginDate" class="easyui-datebox" style="width: 100px" value="<%= ((dynamic)this.ViewData["BeginDate"]) %>">
                    <input id="BeginTime" class="easyui-validatebox" style="border: 1px solid #8DB2E3; width: 95px;
                            height: 18px" value="<%=ViewData["Time"]%>">
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td align="right">
                    终止时间:
                </td>
                <td align="left">
                    <input id="EndDate" class="easyui-datebox" style="width: 100px" value="<%= ((dynamic)this.ViewData["EndDate"]) %>">
                    <input id="EndTime" class="easyui-validatebox" style="border: 1px solid #8DB2E3; width: 95px;
                            height: 18px" value="<%=ViewData["Time"]%>">
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" id="btnSearch">查询</a>
                </td>
            </table>
        </div>
        <div style="height: 3px">
        </div>
        <div>
            <table border="0">
                <tr>
                    <td align="right">
                        记录类型:
                    </td>
                    <td align="left">
                        <input class="easyui-combobox" style="width: 95px;" url="/BasicInfo/LoadAllRecordTypes/"
                            valuefield="编码" textfield="名称" id="txt_RecordType" value="请选择" editable="false" />
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td align="right">
                        操作类型:
                    </td>
                    <td align="left">
                        <input class="easyui-combobox" style="width: 95px;" url="/BasicInfo/LoadAllOperatorTypes/"
                            valuefield="编码" textfield="名称" id="txt_OperatorTypes" value="请选择" editable="false" />
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td align="right">
                        结果类型:
                    </td>
                    <td align="left">
                        <input class="easyui-combobox" style="width: 95px;" url="/BasicInfo/LoadAllResult/"
                            valuefield="编码" textfield="名称" id="txt_Result" value="请选择" editable="false" />
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td align="right">
                        台号:
                    </td>
                    <td align="left">
                        <input class="easyui-combobox" style="width: 95px;" panelheight="80px" url="/Notice/DeskLoad/?ActionId=<%=Request.QueryString["ActionId"]%>"
                            valuefield="台号" textfield="显示名称" id="txt_Desks" value="请选择" editable="false" />
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td align="right">
                        电话号码:
                    </td>
                    <td align="left">
                        <input id="txt_TelCode" class="easyui-validatebox" style="border: 1px solid #8DB2E3; width: 95px;
                            height: 18px">&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="ReadTelLog" icon="icon-save" style="padding: 5px; display:none;overflow:hidden;">
        <table>
            <tr>
                <td align="left" width="80px">
                    <h4>基础信息</h4>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="left" width="80px">
                    对方电话：
                </td>
                <td style="text-align: left">
                    <label id="Tel"></label>
                </td>
            </tr>
            <tr>
                <td align="left" width="80px">
                    接听台号：
                </td>
                <td style="text-align: left">
                    <label id="Desk"></label>
                </td>
            </tr>
            <tr>
                <td align="left" width="80px">
                    接听人员：
                </td>
                <td style="text-align: left">
                    <label id="Dispatcher"></label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <h4>时刻信息</h4>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="left">
                    产生时刻
                </td>
                <td align="left" colspan="5" style="word-wrap: break-word;" colspan="2">
                    <label id="RecordTime"></label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    呼入时刻
                </td>
                <td align="left" colspan="5" style="word-wrap: break-word;" colspan="2">
                    <label id="InhaleTime"></label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    排队时刻
                </td>
                <td align="left" colspan="5" style="word-wrap: break-word;" colspan="2">
                    <label id="FellInTime"></label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    振铃时刻
                </td>
                <td align="left" colspan="5" style="word-wrap: break-word;" colspan="2">
                    <label id="ShakeBellTime"></label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    通话时刻
                </td>
                <td align="left" colspan="5" style="word-wrap: break-word;" colspan="2">
                    <label id="CallTime"></label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    中间操作时刻
                </td>
                <td align="left" colspan="5" style="word-wrap: break-word;" colspan="2">
                    <label id="MiddleHandleTime"></label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    结束时刻
                </td>
                <td align="left" colspan="5" style="word-wrap: break-word;" colspan="2">
                    <label id="FinishTime"></label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <h4>
                        其他信息
                    </h4>
                </td>
                <td></td>
            </tr>
            <tr>
                <td align="left">
                   录音号
                </td>
                <td align="left" colspan="5" style="word-wrap: break-word;" colspan="2">
                    <label id="RecordCode"></label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    记录类型
                </td>
                <td align="left" colspan="5" style="word-wrap: break-word;" colspan="2">
                    <label id="RecordStyle"></label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    操作说明
                </td>
                <td align="left" colspan="5" style="word-wrap: break-word;" colspan="2">
                    <label id="OP"></label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    结果类型
                </td>
                <td align="left" colspan="5" style="word-wrap: break-word;" colspan="2">
                    <label id="Result"></label>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>

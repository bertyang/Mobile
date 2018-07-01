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
            //$('#BeginTime').mask('99:99:99');
            //$('#EndTime').mask('99:99:99');

            $('#grid').datagrid({
                url: "/BasicInfo/TelLogLoad/?begin=2016/10/16&end=2016/10/17&ActionId=10003",

                onClickRow: function (rowIndex, rec) {

                    $.mobile.go('#detail');

                    $('#RecordStyle').text(rec.RecordStyle);
                    $('#Tel').text(rec.Tel);
                    $('#RecordTime').text(rec.RecordTime);
                    $('#InhaleTime').text(rec.InhaleTime);
                    $('#FellInTime').text(rec.FellInTime);
                    $('#ShakeBellTime').text(rec.ShakeBellTime);
                    $('#CallTime').text(rec.CallTime);
                    $('#MiddleHandleTime').text(rec.MiddleHandleTime);
                    $('#FinishTime').text(rec.FinishTime);
                    $('#Desk').text(rec.Desk);
                    $('#Dispatcher').text(rec.Dispatcher);
                    $('#RecordCode').text(rec.RecordCode);
                    $('#Result').text(rec.Result);
                    $('#OP').text(rec.OP);
                }
            });

            $('#btnSearch').bind('click',
                   function () {
                       $('#grid').datagrid({
                           url: '/BasicInfo/TelLogSearch/?begin=' + $("#BeginDate").datetimebox('getText')
                                       + '&&end=' + $("#EndDate").datetimebox('getText')
                                       + '&&tel=' + escape($('#txt_TelCode').val())
                                       + '&&rec=' + escape($('#txt_RecordType').combobox('getText'))
                                       + '&&op=' + escape($('#txt_OperatorTypes').combobox('getText'))
                                       + '&&res=' + escape($('#txt_Result').combobox('getText'))
                                       + '&&des=' + escape($('#txt_Desks').combobox('getText'))
                                       + '&&ActionId=10003',
                              
                        });
                       var p1 = $('#grid').datagrid('getPager');
                       $(p1).pagination({ pageNumber: 1});
                       $('#grid').datagrid('options').page = 1;
                       $('#grid').datagrid('reload')
                   }
              );
        });
    </script>
</head>
<body>
    <div class="easyui-navpanel">
         <header>
            <div class="m-toolbar">
                <div class="m-title">电话流水</div>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>
                <div class="m-right">
                    <a href="javascript:void(0)" class="easyui-linkbutton" onclick="$.mobile.go('#bpage')" data-options="iconCls:'icon-search',plain:true"></a>
                </div>
            </div>
        </header>
         <table id="grid" class="easyui-datagrid" align="center" 
             pagination="true" idfield="编码" pagenumber="1" pagelist="[10, 15, 20]" pagesize="15"
            singleselect="true" sortname="sendTime"
             sortorder="desc"  fit="true" fitcolumns="true">   
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
                    <th field="Desk" width="50px" align='center' sortable="true">
                        接听台号
                    </th>
                    <th field="Dispatcher" width="80px" align='center' sortable="true">
                        调度员
                    </th>
                    <%--  <th field="RecordCode" width="300px" align='center' sortable="true">
                        录音号
                    </th>
                    <th field="Result" width="80px" align='center' sortable="true">
                        结果
                    </th>
                    <th field="ID"  width="50px" align='center' sortable="true" formatter="formatDetail">
                        听录音
                    </th>
                    <th field="Num" width="20" align='center' formatter='formatRead'>查看</th>    
                  <th field="AlarmEventCode" width="50px" align='center' sortable="true" formatter="formatAlarmEvent">
                        关联事件
                    </th>--%>
                </tr>
            </thead>
        </table>
    </div>
    <div id="bpage" class="easyui-navpanel">
       <header>
            <div class="m-toolbar">
                <span id="title" class="m-title">查询</span>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>  
            </div>
        </header>
        <div>
            <table>
                <tr>
                    <td align="right">
                        开始时间:
                    </td>
                    <td align="left">
                        <input id="BeginDate" class="easyui-datebox" style="width: 100px" value="<%= ((dynamic)this.ViewData["BeginDate"]) %>">
                        <input id="BeginTime" class="easyui-validatebox" style="border: 1px solid #8DB2E3; width: 95px;
                                height: 18px" value="<%=ViewData["Time"]%>">
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        终止时间:
                    </td>
                    <td align="left">
                        <input id="EndDate" class="easyui-datebox" style="width: 100px" value="<%= ((dynamic)this.ViewData["EndDate"]) %>">
                        <input id="EndTime" class="easyui-validatebox" style="border: 1px solid #8DB2E3; width: 95px;
                                height: 18px" value="<%=ViewData["Time"]%>">
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        记录类型:
                    </td>
                    <td align="left">
                        <input class="easyui-combobox" style="width: 95px;" url="/BasicInfo/LoadAllRecordTypes/"
                            valuefield="编码" textfield="名称" id="txt_RecordType" value="请选择" editable="false" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        记录类型:
                    </td>
                    <td align="left">
                        <input class="easyui-combobox" style="width: 95px;" url="/BasicInfo/LoadAllRecordTypes/"
                            valuefield="编码" textfield="名称" id="txt_RecordType" value="请选择" editable="false" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        操作类型:
                    </td>
                    <td align="left">
                        <input class="easyui-combobox" style="width: 95px;" url="/BasicInfo/LoadAllOperatorTypes/"
                            valuefield="编码" textfield="名称" id="txt_OperatorTypes" value="请选择" editable="false" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        结果类型:
                    </td>
                    <td align="left">
                        <input class="easyui-combobox" style="width: 95px;" url="/BasicInfo/LoadAllResult/"
                            valuefield="编码" textfield="名称" id="txt_Result" value="请选择" editable="false" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        台号:
                    </td>
                    <td align="left">
                        <input class="easyui-combobox" style="width: 95px;" panelheight="80px" url="/Notice/DeskLoad/?ActionId=<%=Request.QueryString["ActionId"]%>"
                            valuefield="台号" textfield="显示名称" id="txt_Desks" value="请选择" editable="false" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        电话号码:
                    </td>
                    <td align="left">
                        <input id="txt_TelCode" class="easyui-validatebox" style="border: 1px solid #8DB2E3; width: 95px;
                            height: 18px">&nbsp;&nbsp;
                    </td>
               </tr>
                <tr>
                    <td colspan="2">
                        <a href="#" class="easyui-linkbutton" iconcls="icon-search" id="btnSearch">查询</a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="detail"  class="easyui-navpanel">
        <header>
            <div class="m-toolbar">
                <span id="title" class="m-title">详细</span>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>
            </div>
        </header>
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

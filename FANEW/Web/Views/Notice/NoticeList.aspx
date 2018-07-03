<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Notice</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {

        $('#grid').datagrid('options').url = "/Notice/NoticeSearch/?startTime=" + $("#startTime").datetimebox('getText') 
                        + "&endTime=" + $('#endTime').datetimebox('getText')
                        + "&sendType=" + $('#sendType').combobox('getValue')
                        + "&station=" + $('#station').combobox('getValue')
                        + "&vehicle=" + $('#vehicle').combobox('getValue')
                        + "&ActionId=<%=Request.QueryString["ActionId"]%>";

        //发送类型
        $('#sendType').combobox({
            url: '/Notice/NoticeType/',
            valueField: '编码',
            textField: '名称',
            onLoadSuccess: function (data) {
                $('#sendType').combobox('setValue', '--请选择--');
            }
        });

        //分站
        $('#station').combobox({
            url: '/BasicInfo/LoadAllStations/?ActionId=<%=Request.QueryString["ActionId"]%>',
            valueField: '编码',
            textField: '名称',
            onChange: function (node) {
                //车辆
                $('#vehicle').combobox({
                    url: '/Notice/Ambulance/?ActionId=<%=Request.QueryString["ActionId"]%>&stationId=' + escape(node),
                    valueField: '车辆编码',
                    textField: '实际标识',
                    onLoadSuccess: function (data) {
                        $('#vehicle').combobox('setValue', '--请选择--');
                    }
                });
            },
            onLoadSuccess: function (data) {
                $('#station').combobox('setValue', '--请选择--');
            }
        });

        //车辆
        $('#vehicle').combobox({
            url: '/Notice/AmbulanceLoad/?ActionId=<%=Request.QueryString["ActionId"]%>',
            valueField: '车辆编码',
            textField: '实际标识',
            onLoadSuccess: function (data) {
                $('#vehicle').combobox('setValue', '--请选择--');
            }
        });

        $('#btnSearch').bind('click',
            function () {
                doSearch();
            }
        );
    });
    //查询
    function doSearch() {
        $('#grid').datagrid('load', {
            startTime: $("#startTime").datetimebox('getText'),
            endTime: $('#endTime').datetimebox('getText'),
            sendType: $('#sendType').combobox('getValue'),
            station: $('#station').combobox('getValue'),
            vehicle: $('#vehicle').combobox('getValue')
        });
    }

    //格式化“发送类型”
    function formatSendType(val, row) {
        if (row.typeCode == 0) {
            return "<img src='../../Content/images/esta_w_s.gif' border='0'/>&nbsp;" + val + "";
        }
        if (row.typeCode == 1) {
            return "<img src='../../Content/images/icon012a1.gif' border='0'/>&nbsp;" + val + "";
        }
        if (row.typeCode == 2) {
            return "<img src='../../Content/images/icon011a10.gif' border='0'/>&nbsp;" + val + "";
        }
        if (row.typeCode == 4) {
            return "<img src='../../Content/images/icon042a1.gif' border='0'/>&nbsp;" + val + "";
        }
        return "<img src='../../Content/images/icon010a20.gif' border='0'/>&nbsp;" + val + "";
    }

    //格式化“发送列表”
    function formatSendList(val, row) {
        if (val != null && val.length >= 20) {
            var list = val.substring(0, 20);
            return '<a  title="' + val + '">' + list + '</a>';
        } else {
            return '<a  title="' + val + '">' + val + '</a>';
        }
    }

    //格式化“发送内容”
    function formatSendContent(val, row) {
        if (val != null && val.length >= 30) {
            var content = val.substring(0, 30);
            return '<a  title="' + val + '">' + content + '</a>';
        } else {
            return '<a  title="' + val + '">' + val + '</a>';
        }
    }

    //格式化“查看”
    function formatRead(val, row) {
        return "<a href='#' onclick='ReadNotice(" + val + ")'><img alt='通知单详细' src='../../Content/images/find.gif' border='0'/></a>";
    }

    function formatBack(val, row) {
        if (row.backCount == 0) {
            return ""
        }
        else {
            return "<a href='#' onclick='ReadBack(" + val + ")'><img alt='回复详细' src='../../Content/images/redo.png' border='0'/></a>";
        }
    }

    function ReadNotice(val) {
        $('#grid').datagrid('selectRecord', val);
        var node = $('#grid').datagrid('getSelected');
        $('#ReadNotice').show();
        $('#ReadNotice').dialog({
            collapsible: true,
            minimizable: true,
            maximizable: true,
            height: 450,
            width: 500,
            modal: true,
            title: '通知单详细内容',
            buttons: [
            {  
                text: '关闭',
                iconCls: 'icon-cancel',
                handler: function () {
                    $('#ReadNotice').dialog('close');
                }
             }]
        });

          $('#time').val(renderTime(node.sendTime));
          $('#operator').val(node.worker);
          $('#type').val(node.typeName);
          $('#list').val(node.sendList);
          $('#content').val(node.content);
    }

    function ReadBack(val) {
        $('#grid').datagrid('selectRecord', val);
        var node = $('#grid').datagrid('getSelected');
        $('#Back').datagrid({
            url: '/Notice/GetNoticeBack/?code=' + node.ID
        }); 
        alert(node.ID);
        $('#ReadBack').show();
        $('#ReadBack').dialog({
            collapsible: true,
            minimizable: true,
            maximizable: true,
            height: 300,
            width: 800,
            modal: true,
            title: '回复内容',
            buttons: [
            {
                text: '关闭',
                iconCls: 'icon-cancel',
                handler: function () {
                    $('#ReadBack').dialog('close');
                }
            }]
        });
    }
</script>  
 </head>
<body class="easyui-layout">
    <div id="tb" style="padding: 8px; height: auto"> 
          <table border="0" cellspacing="0" cellpadding="0" width="100%" height="10%">
                <tr>
                    <td width="6%"></td>
                    <td width="9%" align="center">起始时刻: </td>
                    <td><input id="startTime" type="text" class="easyui-datetimebox"  value="<%= ((dynamic)this.ViewData["startTime"]) %>" style="width:150px;"/></td>
                    <td width="8%">终止时刻: </td>
                    <td><input id="endTime" type="text" class="easyui-datetimebox"  value="<%= ((dynamic)this.ViewData["endTime"]) %>" style="width:150px;"/></td>
                    <td width="8%">通知类型：</td>
                    <td>
                        <select id="sendType" class="easyui-combobox" name="sendType" style="width: 150px;">
                        </select>
                    </td>
                </tr> 
                <tr>
                    <td width="6%">
                    </td>
                    <td width="9%" align="center">分站：</td>
                    <td>
                        <select id="station" class="easyui-combobox" name="station" style="width: 150px;">
                        </select>
                    </td>
                    <td width="8%">车辆：</td>
                    <td>
                        <select id="vehicle" class="easyui-combobox" name="vehicle" style="width: 150px;">
                        </select>
                    </td>
                    <td>
                    </td>
                     <td>
                        <a href="#" class="easyui-linkbutton" iconcls="icon-search" id="btnSearch">查询</a>
                    </td>
                </tr>
                </table> 
           </div> 
  
    <div region="center" style="padding: 5px;height:100%" border="true" > 
         <table id="grid" class="easyui-datagrid" align="center" toolbar="#tb"
             pagination="true" idfield="ID" pagenumber="1" pagelist="[10, 15, 20]" pagesize="15"
             nowrap="false" striped="true" rownumbers="true" singleselect="true" sortname="sendTime"
             sortorder="desc" remotesort="false" fit="true" fitcolumns="true">      
                <thead>
			        <tr>
                        <th field="typeName" width="30" align='center' formatter='formatSendType'> 通知类型</th>
                        <th field="typeCode" align='center' hidden='true'>通知类型编码</th>
                        <th field="sendTime" width="40" align='center' formatter='renderTime'>发送时刻</th>
                        <th field="worker" width="30" align='center'>操作人员</th>
                        <th field="sendList" width="50" align='center' formatter='formatSendList'>发送列表</th>
                        <th field="content" width="120" align='center' formatter='formatSendContent'>发送内容</th> 
                        <th field="Num" width="20" align='center' formatter='formatRead'>查看</th>                                                 
                        <th field="ID" width="20" align='center' formatter='formatBack'>回复</th>    
			        </tr>
		        </thead>
            </table>
      </div>

    <div id="ReadNotice" icon="icon-save" style="padding: 5px; display:none;overflow:hidden;">
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
                    发送时刻：
                </td>
                <td style="text-align: left">
                    <input id="time" type="text" style="border: 0; background: transparent; width: 146px;
                        height: 18px" />
                </td>
            </tr>
            <tr>
                <td align="left" width="80px">
                    操作人员：
                </td>
                <td style="text-align: left">
                    <input id="operator" type="text" style="border: 0; background: transparent; width: 146px;
                        height: 18px" />
                </td>
            </tr>
            <tr>
                <td align="left" width="80px">
                    发送类型：
                </td>
                <td style="text-align: left">
                    <input id="type" type="text" style="border: 0; background: transparent; width: 146px;
                        height: 18px" />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <h4>
                        发送列表</h4>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="5" style="word-wrap: break-word;" colspan="2">
                    <textarea id="list" rows="3" cols="70" style="overflow: auto;"></textarea>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <h4>
                        发送内容</h4>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="5" style="word-wrap: break-word;" colspan="2">
                    <textarea id="content" rows="5" cols="70" style="overflow: auto;"></textarea>
                </td>
            </tr>
        </table>
    </div>

    <div id="ReadBack" icon="icon-save" style="padding: 5px; display:none;overflow:hidden;">
        <table id="Back" class="easyui-datagrid" align="center"  nowrap="false"
                    striped="true" rownumbers="true" sortname="ID" sortorder="asc" 
                    remotesort="false" width="100%" fit="true" fitcolumns="true" singleSelect="true">
                    <thead>
                        <tr>
                            <th field="sendList" width="15%" align='center'>
                                手机号
                            </th>
                            <th field="content" width="65%" align='center'>
                                内容
                            </th>
                            <th field="sendTime" width="20%" formatter='renderTime' align='center'>
                                回复时间
                            </th>
                        </tr>
                    </thead>
        </table>
     </div>
</body>  
</html>  

<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Data</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            $('#grid').datagrid({
                url: "/WorkFlow/TraceNotifySearch/?flowType=" + $('#flowType').combobox('getValue') + "&startTime=" + $("#startTime").datetimebox('getText') + "&endTime=" + $('#endTime').datetimebox('getText') + "&flowNo=" + $('#flowNo').val(),
                rowStyler: function (rowIndex, rowData) {
                    if (!rowData.ReadFlag) {
                        return 'color:red;font-weight:bold';
                    }
                },
                onClickRow: function (rowIndex, rowData) {

                    //如果是未读，更改为已读
                    if (!rowData.ReadFlag) {
                        isRead(rowData.FlowID, rowData.FlowNo);
                    }

                    var src = "/WorkFlow/TraceView/?flowNo=" + rowData.FlowNo
                                            + "&flowId=" + rowData.FlowID
                                            + "&flowInstId=" + rowData.FlowInstId
                                            + "&url=" + rowData.Url
                                            + "&isInner=" + rowData.IsInner;


                    var title = '知会表单--&gt;' + rowData.FlowName + "[" + rowData.FlowNo + "]";

                    AddTab(title, src, 'tu0804');
                }
            });


            EUIcombobox("#flowType", {
                valueField: "ID",
                textField: "Name",
                OneOption: [{
                    ID: "",
                    Name: "----请选择----"
                }],
                url: "/WorkFlow/FlowList/"

            });

            $('#btnSearch').bind('click',
                    function () {
                        doSearch();
                    }
                );
        });

        function doSearch() {
            $('#grid').datagrid('load', {
                flowType: $('#flowType').combobox('getValue'),
                startTime: $("#startTime").datetimebox('getText'),
                endTime: $('#endTime').datetimebox('getText'),
                flowNo: $('#flowNo').val()
            });
        }

//        function formatCharge(val, rec) {

//            var src = "/WorkFlow/TraceView/?flowNo=" + rec.FlowNo
//                        + "&flowId=" + rec.FlowID
//                        + "&flowInstId=" + rec.FlowInstId
//                        + "&url=" + rec.Url
//                        + "&isInner=" + rec.IsInner;


//            var title = '追寻知会--&gt;' + rec.FlowName + "[" + rec.FlowNo + "]";

//            return "<a href='#' onclick=AddTab('" + title + "','" + src + "','tu0804')><u>" + rec.FlowName + "</u></a>";
//        }

        function formatFlowState(val, row) {

            var result;

            $.ajax({
                type: "POST",
                async: false,
                url: "/WorkFlow/TransaferFlowState/?state=" + val,
                dataType: "json",
                success: function (data) {
                    result = data;
                },
                error: function () {
                    result = "转换错误";
                }
            });

            return result;
        }

        function isRead(flowId,flowNo)
        {
            $.ajax({
                    type: "POST",
                    url: '/WorkFlow/NoticeReadFlag/?flowId=' + flowId + '&flowNo=' + flowNo + '&workerID='+<%=ViewData["workerID"] %>,
                    success: function () {
                        refreshNotify(); //刷新“未读知会”数量
                    },
                    error: function () {
                        $.messager.alert('错误', '检查错误！', "error");
                    }
                });
            }

        function refreshNotify() {
            $.ajax({
                type: "POST",
                url: "/Home/refreshNotify/",
                success: function (msg) {
                    var data = eval('(' + msg + ')');
                    parent.document.getElementById('notify').innerText = data.Message;
                },
                error: function () {
                    $.messager.alert('错误', '请检查错误！', "error");
                }
            });
        }     

    </script>
</head>
<body class="easyui-layout">
    <div region="center" style="padding: 5px" border="true" > 
         <table id="grid" class="easyui-datagrid" align="center"  toolbar="#tb" 
             pagination="true"  pageNumber=1 pageList= "[10, 15, 20]" pageSize=15  
             nowrap="false" striped="true" rownumbers="true" idField="FlowInstId"   singleSelect="true"
             sortName="创建时间" sortOrder= "asc" remoteSort="false" fit="true" fitColumns="true"
             >
                <thead>
		            <tr>
                        <th field="FlowName" width="100"  align='center'  sortable="true">流程种类</th>
                        <th field="FlowNo" width="100"  align='center' sortable="true">流程号</th>
                        <th field="ApplyerName" width="100"  align='center' sortable="true">申请人</th>
                        <th field="BeginDate" width="100"  align='center'  formatter='renderTime'  sortable="true">创建时间</th>
                        <th field="State" width="100" align='center' formatter='formatFlowState' sortable="true">状态</th>                 
			        </tr>
		        </thead>
            </table>  
      </div>
      <div id="tb" style="padding:5px;height:auto;" >     
            <table border="0" cellspacing="0" cellpadding="0" width="100%" height="10%">
                <tr>
                    <td>起始时刻: </td>
                    <td><input id="startTime" type="text" class="easyui-datebox"  value="<%= DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") %>" style="width:100px;"/></td>
                    <td>终止时刻: </td>
                    <td><input id="endTime" type="text" class="easyui-datebox"  value="<%= DateTime.Now.ToString("yyyy-MM-dd") %>" style="width:100px;"/></td>
                    <td>流程种类: </td>
                    <td><select id="flowType"   class="easyui-combobox" name="Type"  style="width:100px;"></select></td>
                    <td>流程号: </td>
                    <td><input id="flowNo" type="text" class="easyui-validatebox" style="border: 1px solid #8DB2E3;width: 51px;" /></td>
                    <td><a href="#" class="easyui-linkbutton" iconcls="icon-search" id="btnSearch">查询</a> </td>
                </tr>   
           </table>  
    </div>
</body>
</html>



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
                url: "/WorkFlow/TraceNotifySearch/?flowType=&startTime=1999/1/1&endTime=2099/1/1&flowNo=",
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


                    $("#apage").html('<iframe height="100%" width="100%"   marginheight="0" marginwidth="0" scrolling="auto" frameborder="0" src="' + src + '"></iframe>');
                    $.mobile.go("#apage");
                }
            });

            $('#flowType').combobox({
                url: "/WorkFlow/FlowList/",
                valueField: 'ID',
                textField: 'Name',
                onLoadSuccess: function (data) {
                    $('#flowType').combobox('setValue', '');                   
                }
            });
        });


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
<body>
    <div class="easyui-navpanel">
        <header>
            <div class="m-toolbar">
                <span id="title" class="m-title">知会</span>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>                
                <div class="m-right">
                    <a href="javascript:void(0)" class="easyui-linkbutton" onclick="$.mobile.go('#bpage')" data-options="iconCls:'icon-search',plain:true"></a>
                </div>
            </div>
        </header>
         <table id="grid" class="easyui-datagrid" align="center"  toolbar="#tb" 
             pagination="true"  pageNumber=1 pageList= "[10, 15, 20]" pageSize=15  
             nowrap="false" striped="true" rownumbers="true" idField="FlowInstId"   singleSelect="true"
             sortName="创建时间" sortOrder= "asc"  fit="true" fitColumns="true">
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
    <div id="apage" class="easyui-navpanel"  closed="true">
       <header>
            <div class="m-toolbar">
                <span id="title" class="m-title">签核表单</span>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>                
                
            </div>
        </header>
    </div>
    <div id="bpage" class="easyui-navpanel"  closed="true">
        <header>
            <div class="m-toolbar">
                <span id="title" class="m-title">查询</span>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>  
            </div>
        </header>
            <table border="0" cellspacing="0" cellpadding="0" width="100%" height="10%">
                 <tr>
                    <td>起始时刻: </td>
                    <td><input id="startTime" type="text" class="easyui-datebox"  value="" style="width:100px;"/></td>
                </tr>
                <tr> 
                    <td>终止时刻: </td>
                    <td><input id="endTime" type="text" class="easyui-datebox"   style="width:100px;"/></td>
                </tr>
                <tr> 
                    <td>流程种类: </td>
                    <td><select id="flowType"   class="easyui-combobox" name="Type"  style="width:100px;"></select></td>
                </tr> 
                <tr> 
                    <td>流程号: </td>
                    <td><input id="flowNo" type="text" class="easyui-validatebox" style="border: 1px solid #8DB2E3;width: 51px;" /></td>
                </tr>
                <tr>  
                    <td colspan="2">
                        <a href="#" class="easyui-linkbutton" iconcls="icon-search" id="btnSearch">查询</a>
                    </td>
                </tr>   
           </table>
    </div>
</body>
</html>



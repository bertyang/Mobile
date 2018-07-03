<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Data</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            $('#grid').datagrid('options').url = "/WorkFlow/ApproveSearch/?flowType=" + $('#flowType').combobox('getValue') + "&startTime=" + $("#startTime").datetimebox('getText') + "&endTime=" + $('#endTime').datetimebox('getText') + "&flowNo=" + $('#flowNo').val();
            
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

            refresh();

        });

        function refresh() {

            doSearch();

            setTimeout('refresh()',<%=ViewData["RefreshInterval"]%>); 
        }

        function doSearch() {

            $('#grid').datagrid('load', {
                flowType: $('#flowType').combobox('getValue'),
                startTime: $("#startTime").datetimebox('getText'),
                endTime: $('#endTime').datetimebox('getText'),
                flowNo: $('#flowNo').val()
            });
        }

        function formatCharge(val, rec) {

            var src = "/WorkFlow/Approve/?flowNo=" + rec.FlowNo
                        + "&flowId=" + rec.FlowID
                        + "&flowInstId=" + rec.FlowInstID
                        + "&activityInstId=" + rec.ActivityInstID
                        + "&activityId=" + rec.ActivityID
                        + "&workItemInstId=" + rec.WorkItemInstID
                        + "&url=" + rec.Url
                        + "&returnType=" + rec.ReturnType
                        + "&splitType=" + rec.SplitType;

            var title = '签核表单--&gt;'+rec.FlowName + "[" + rec.FlowNo+"]";

            return "<span style='color:red;'><a href='#' onclick=AddTab('" + title + "','" + src + "','tu1912')><u>" + rec.FlowName + "</u></a></span>";
        }

        //刷新“待办工作”数量
        //function refreshApprove() {
        //    $.ajax({
        //        type: "POST",
        //        url: "/Home/refreshApprove/",
        //        success: function (msg) {
        //            var data = eval('(' + msg + ')');
        //            parent.document.getElementById('approve').innerText = data.Message;
        //        },
        //        error: function () {
        //            $.messager.alert('错误', '请检查错误！', "error");
        //        }
        //    }); 
        //}

    </script>
</head>
<body class="easyui-layout">
    <div region="center" style="padding: 5px" border="true" > 
         <table id="grid" class="easyui-datagrid" align="center"  toolbar="#tb" 
             pagination="true"  pageNumber=1 pageList= "[10, 15, 20]" pageSize=15  
             nowrap="false" striped="true" rownumbers="true" idField="WorkItemInstID" 
             sortName="创建时间" sortOrder= "asc" remoteSort="false" fit="true" fitColumns="true"  singleSelect="true"
             >
                <thead>
		            <tr>
                        <th field="FlowName" width="100"  align='center' formatter="formatCharge" sortable="true">流程种类</th>
                        <th field="FlowNo" width="100"  align='center' sortable="true">流程号</th>
                        <th field="ApplyerName" width="100"  align='center' sortable="true">申请人</th>
                        <th field="BeginDate" width="100"  align='center' formatter='renderTime' sortable="true">创建时间</th> 
			        </tr>
		        </thead>
            </table>  
      </div>
    <div id="tb" style="padding:5px;height:auto;" >     
            <table border="0" cellspacing="0" cellpadding="0" width="100%" height="10%">
                <tr>
                    <td>起始时刻: </td>
                    <td><input id="startTime" type="text" class="easyui-datebox"  value="" style="width:100px;"/></td>
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



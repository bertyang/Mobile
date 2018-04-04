<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Data</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

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

            $('#grid').datagrid('options').url = "/WorkFlow/MonitorFlow/?flowType=" + $('#flowType').combobox('getValue') + "&startTime=" + $("#startTime").datetimebox('getText') + "&endTime=" + $('#endTime').datetimebox('getText') + "&flowNo=" + $('#flowNo').val();

        });

        function doSearch() {
            $('#grid').datagrid('load', {
                flowType: $('#flowType').combobox('getValue'),
                startTime: $("#startTime").datetimebox('getText'),
                endTime: $('#endTime').datetimebox('getText'),
                flowNo: $('#flowNo').val()
            });
        }

        function IsApply(state) {

            var result;

            $.ajax({
                type: "POST",
                async: false,
                url: "/WorkFlow/IsApply/?state=" + state,
                dataType: "json",
                success: function (data) {
                    result = data;
                },
                error: function () {
                    result = false;
                }
            });

            return result;
        }

        function formatCharge(val, rec) {
            var src, title;

            if (rec.IsInner == "Y" && IsApply(rec.State)) {
                src = "/WorkFlow/Apply/?flowNo=" + rec.FlowNo
                        + "&flowId=" + rec.FlowID
                        + "&flowInstId=" + rec.FlowInstId

                title = rec.FlowName + "[" + rec.FlowNo + "]";
            }
            else {
                src = "/WorkFlow/TraceView/?flowNo=" + rec.FlowNo
                        + "&flowId=" + rec.FlowID
                        + "&flowInstId=" + rec.FlowInstId
                        + "&state=" + rec.State
                        + "&url=" + rec.Url
                        + "&isInner=" + rec.IsInner;


                title = '监控流程--&gt;' + rec.FlowName + "[" + rec.FlowNo + "]";
            }

            return "<span style='color:red;'><a href='#' onclick=AddTab('" + title + "','" + src + "','tu0604')><u>" + rec.FlowName + "</u></a></span>";

        }


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
                        <th field="FlowName" width="100"  align='center'  formatter="formatCharge" sortable="true" >流程种类</th>
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
                    <td><input id="startTime" type="text" class="easyui-datebox"  value="<%=  DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") %>" style="width:100px;"/></td>
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



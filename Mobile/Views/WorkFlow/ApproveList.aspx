<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Data</title>
    <meta name="viewport" content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

           $('#grid').datagrid({
               url: "/WorkFlow/ApproveSearch/?flowType=&startTime=&endTime=2099/1/1&flowNo=",
               
               onClickRow: function (rowIndex, rec) {

                   var src = "/WorkFlow/Approve/?flowNo=" + rec.FlowNo
                        + "&flowId=" + rec.FlowID
                        + "&flowInstId=" + rec.FlowInstID
                        + "&activityInstId=" + rec.ActivityInstID
                        + "&activityId=" + rec.ActivityID
                        + "&workItemInstId=" + rec.WorkItemInstID
                        + "&url=" + rec.Url
                        + "&returnType=" + rec.ReturnType
                        + "&splitType=" + rec.SplitType;

                   $("#apage").html('<iframe height="100%" width="100%"   marginheight="0" marginwidth="0" scrolling="auto" frameborder="0" src="' + src + '"></iframe>');
                   $.mobile.go("#apage");
               }
           });

           $('#btnSearch').bind('click',
                   function () {
                       doSearch();
                   }
               );

           $('#flowType').combobox({
               url: "/WorkFlow/FlowList/",
               valueField: 'ID',
               textField: 'Name',
               onLoadSuccess: function (data) {
                   $('#flowType').combobox('setValue', '');                   
                }
            });

        });

        function doSearch() {

            $('#grid').datagrid('load', {
                flowType: $('#flowType').combobox('getValue'),
                startTime: $("#startTime").datetimebox('getText'),
                endTime: $('#endTime').datetimebox('getText'),
                flowNo: $('#flowNo').val()
            });
        }


    </script>
</head>
<body>
    <div class="easyui-navpanel">
        <header>
            <div class="m-toolbar">
                <div class="title">代办</div>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>                
                <div class="m-right">
                    <a href="javascript:void(0)" class="easyui-linkbutton" onclick="$.mobile.go('#bpage')" data-options="iconCls:'icon-search',plain:true"></a>
                </div>
            </div>
        </header>
        <table id="grid"  align="center" 
             pagination="true"  pageNumber=1 pageList= "[1, 15, 20]" pageSize=15  
             nowrap="false" striped="true" rownumbers="true" idField="WorkItemInstID" 
             sortName="创建时间" sortOrder= "asc" fit="true" fitColumns="true"  singleSelect="true">
                <thead>
		            <tr>
                        <th field="FlowName" width="100"  align='center' sortable="true">流程种类</th>
                        <th field="FlowNo" width="100"  align='center' sortable="true">流程号</th>
                        <th field="ApplyerName" width="100"  align='center' sortable="true">申请人</th>
                        <th field="BeginDate" width="100"  align='center' formatter='renderTime' sortable="true">创建时间</th> 
			        </tr>
		        </thead>
            </table>
    </div>
    <div id="apage" class="easyui-navpanel"  closed="true">
       <header>
            <div class="m-toolbar">
                <span id="title" class="title">签核表单</span>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>                
                
            </div>
        </header>
    </div>
    <div id="bpage" class="easyui-navpanel"  closed="true">
        <header>
            <div class="m-toolbar">
                <span id="title" class="title">查询</span>
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



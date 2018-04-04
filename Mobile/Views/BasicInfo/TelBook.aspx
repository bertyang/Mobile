<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>通讯录</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
        <script type="text/javascript" language="javascript">
            $(function () {
                $('#grid').datagrid({
                    url: "/BasicInfo/TelBookLoad/?OwnerID=<%=ViewData["OwnerID"] %>",
                });

                $('#btnQuery').bind('click',
                   function () {
                       $('#grid').datagrid('load', {
                           Name: $('#txt_Name').val(),
                           Type: $('#txt_Type').combotree('getValue'),
                           OwnerID: '<%=ViewData["OwnerID"] %>'
                       });
                    }
                );
            });        
        </script>     
</head>
<body> 
    <div class="easyui-navpanel">
        <header>
            <div class="m-toolbar">
                <div class="m-title">通讯录</div>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>
                <div class="m-right">
                    <a href="javascript:void(0)" class="easyui-linkbutton" onclick="$.mobile.go('#bpage')" data-options="iconCls:'icon-search',plain:true"></a>
                </div>
            </div>
        </header>
        <table id="grid"  align="center" toolbar="#hh"
            pagination="true" pagenumber="1" pagelist="[10, 15, 20]" pagesize="15" idfield="ID"
            fitcolumns="true" nowrap="false" striped="true" rownumbers="true" 
            sortname="ID" sortorder="asc" width="1000px" fit="true">
         <thead>
			<tr>
                <th field="Name" width="100px"  align='center'>名称</th>  
                <th field="TypeName" width="100px" align='center'>电话分类</th>
                <th field="Tel1" width="100px"  align='center'>联系电话一</th>
               <%-- <th field="Exten1" width="100px"  align='center'>分机一</th>
                <th field="Tel2" width="100px"  align='center'>联系电话二</th>
                <th field="Exten2" width="100px"  align='center'>分机二</th>
                <th field="Remark" width="100px"  align='center'>备注</th>
                <th field="OrderNo" width="100px" align='center'>顺序号</th>
                <th field="IsEffect" width="100px"  align='center'>是否有效</th>--%>
			</tr>
		</thead>
        </table>
   </div>
     <div id="bpage" class="easyui-navpanel">
        <header>
            <div class="m-toolbar">
                <div class="m-title">内容</div>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>
            </div>
        </header>
        <table width="100%">
            <tr>
                <td width="100px">
                    姓名:
                    <input id="txt_Name" class="easyui-validatebox" style="border: 1px solid #8DB2E3;width: 120px; height: 18px">
                </td>
                <td width="100px">
                    电话分类:
                    <input id="txt_Type" class="easyui-combotree" style="width: 140px;" url="/BasicInfo/TypeTreeLoad/?OwnerID=<%=ViewData["OwnerID"] %>"
                        valuefield="编码" textfield="名称" value="--请选择--" editable="false" />
                </td>
                <td width="100px">
                    <a href="#" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search" >查询</a>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>

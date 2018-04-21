<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>

    <script type="text/javascript" language="javascript">
        function formatCharge(val, rec) {
            return "<span style='color:red;'><a href='/Office/FileDownLoad/?fileId=" + rec.编码 + "'>下载</a></span>";
        } 
    </script>
</head>
<body class="easyui-layout">
    <div region="north" style="width:100%; height:60px">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="10px"></td>
            </tr>
            <tr>
                <td style="text-align:center; font-size:x-large; font-family:宋体; height:30px"> 
                    应急预案列表
                </td>
            </tr>
            <tr>
                <td  height="10"></td>
            </tr>
         </table>
    </div>
    <div region="center" style="padding: 5px"> 
         <table id="grid" class="easyui-datagrid" align="center"pagination="true"  pageNumber=1  pageList= "[10, 15, 20]" 
            pageSize=15  idField="ID" nowrap="false" striped="true" rownumbers="true" 
            remoteSort="false" fit="true" fitColumns="true">
                <thead>
			        <tr>
                        <th field="序号" width="100"  align='center'>序号</th>
                        <th field="应急预案名称" width="100"  align='center'>应急预案名称</th>
                        <th field="ID" width="100" align='center' formatter="formatCharge">查看</th>   
			        </tr>
		        </thead>
            </table>  
      </div>
</body>
</html>




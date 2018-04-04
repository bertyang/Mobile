<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>详细信息</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript" language="javascript">
    function killErrors() {  
        return true;  
    }  
    window.onerror =killErrors; 

        function formatDownLoad(val, row) {
            return "<a href='/Office/FileDownLoad/?fileId=" + row.编码 + "'><img src='../../Content/images/button_Onduty.Image.gif' border='0'/></a>";
        }
        
        //页面初始化加载
        $(document).ready(function () {
        if(<%= ((dynamic)this.ViewData["entity"]).发送类型编码 %>!=3)
        {
//            debugger;
            $('#ccDivMain').layout('remove','west');
        }
        else
        {
            $('#jsrTab').datagrid({
                url: '/Office/GetRecs/?isRead=1&BGCode=<%= ((dynamic)this.ViewData["officeId"]) %>'
            });
        }

        });
    </script>
</head>
<body>

<div id="ccDivMain" class="easyui-layout" data-options="fit:true">
    <div id="jsrDIV" data-options="region:'west',split:true,border:false" style="width:200px;" title="接收人">
            <table id="jsrTab" class="easyui-datagrid"
                idfield="编码" singleSelect="true" remotesort="false" fit="true" fitcolumns="true">
                <thead>
                    <tr>
                        <th field="接收人" align='center'>
                            接收人
                        </th>
                        <th field="查阅时间" align='center' formatter='renderTime'>
                            查阅时间
                        </th>
                    </tr>
                </thead>
            </table>
    </div>
    <div data-options="region:'north',split:true,border:false" style="height:130px">
            <table style="margin-top: 10px; width: 100%">
            <tr align="center"  style="height: 30px;">
                <td align="center" colspan="2">
                    <h1><span style="font-size: large; color: #15428B;">
                        <%= ViewData["title"]%></span></h1>
                </td>
            </tr>
            <tr align="center">
                <td style="width:40%; text-align: right">
                    <h4>作者：<span><%= ((dynamic)this.ViewData["entity"]).作者 %></span></h4>
                </td>
                <td style="width: 40%; text-align: left">
                    <h4><span style="color:Red">日期：<%= ((dynamic)this.ViewData["entity"]).创建时间 %></span></h4>
                </td>
            </tr>
            <tr align="center">
                <td align="center" colspan="2">
                    接收者：<span><%= (dynamic)this.ViewData["receive"] %></span>
                </td>
            </tr>
        </table>
    </div>
    <div data-options="region:'center',split:true,border:false">
            <%=ViewData["content"]%>
    </div>
    <div data-options="region:'east',split:true,border:false" style="width:250px;">
        <table class="easyui-datagrid" url="/Office/OfficeFile/?officeId=<%=ViewData["officeId"]%>"
            idfield="编码" sortname="文件大小" sortorder="asc" singleSelect="true" remotesort="false" fit="true" fitcolumns="true">
            <thead>
                <tr>
                    <th field="原附件名" width="45%" align='center'>
                        附件
                    </th>
                    <th field="文件大小" width="45%" align='center'>
                        大小
                    </th>
                    <th field="编码" width="10%" align='center' formatter='formatDownLoad'>
                        下载
                    </th>
                </tr>
            </thead>
        </table>
    </div>
</div>
</body>
</html>

<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript" language="javascript">

        //页面初始化
        $(document).ready(function () {
            $('#grid').datagrid({
                url: '/Email/EmailSearch/?pageNumber=<%=ViewData["pageNumber"]%>' + '&workerID=' + '<%=ViewData["workerID"] %>' + '&folderID=' + '<%=ViewData["folderID"] %>',
                onClickRow: function (rowIndex, rowData) {
                    $('#att_grid').datagrid({
                        url: '/Email/GetFile/?mailID=' + rowData.ID
                    });

                    $('#detailInfo').show();
                    $('#detailInfo').dialog({
                        collapsible: true,
                        minimizable: true,
                        maximizable: true,
                        height: 500,
                        width: 800,
                        modal: true,
                        title: '邮件详细',
                        onClose: function () {
                            $('#content').val("");
                        },
                        buttons: [
                                     {
                                         text: '彻底删除',
                                         iconCls: 'icon-remove',
                                         handler: function () {
                                             DeleteEmail();
                                             $('#detailInfo').dialog('close');
                                         }
                                     },
                                    {
                                        text: '关闭',
                                        iconCls: 'icon-cancel',
                                        handler: function () {
                                            $('#detailInfo').dialog('close');
                                            $('#content').val("");
                                        }
                                    }]
                    });

                    $('#title').val(rowData.title);
                    $('#from').val(rowData.sender);
                    $('#to').val(rowData.receiver);
                    $('#cc').val(rowData.CC);
                    $('#content').append(rowData.Content);
                    $('#date').val(rowData.sendDate);

                }
            });
        });

        //获取指定datagrid的当前页
        //wpf
        function getCurrentPage(gridName) {
            var options = $('#' + gridName).datagrid('getPager').data("pagination").options;
            var curr = options.pageNumber;
            return curr;
        } 
        
        function formatDownLoad(val, row) {
            return "<a href='/Email/FileDownLoad/?fileId=" + row.编码 + "'><img src='../../Content/images/button_Onduty.Image.gif' border='0'/></a>";
        }  
        
        //彻底删除
        function DeleteEmail()
        {
            var rows = $('#grid').datagrid('getSelections');
            if (!rows || rows.length == 0) {
                $.messager.alert('提示', '请选择要删除的邮件');
                return;
            }
            var parm;
            $.each(rows, function (i, n) {
                if (i == 0) {
                    parm = "idList=" + n.ID;
                }
                else {
                    parm += "&idList=" + n.ID;
                }
            });
            $.messager.confirm('提示', '彻底删除后不可恢复，是否删除?', function (r) {
                if (!r) {
                    return;
                }
                $.ajax({
                    type: "POST",
                    url: "/Email/MailDelete/",
                    data: parm,
                    success: function (msg) {
                        if (msg.IsSuccess) {
                            $.messager.alert('提示', '删除成功！', "info", function () {
                                this.location.reload();
                            });
                        }
                    },
                    error: function () {
                        $.messager.alert('错误', '删除失败！', "error");
                    }
                 });
           });
        }            
    </script>
</head>
<body class="easyui-layout">
    <div region="center" style="padding: 5px" border="true">
        <table id="grid" class="easyui-datagrid" align="center" toolbar="#tb" url="/Email/EmailSearch/"
            pagination="true" pagenumber="1" pagelist="[10, 15, 20]" pagesize="15" idfield="ID"
            nowrap="false" striped="true" rownumbers="true" sortname="sendDate" sortorder="desc"
            remotesort="false" fit="true" fitcolumns="true">
            <thead frozen="true">
                <tr>
                    <th field="ID" checkbox="true" align='center'>
                </tr>
            </thead>
            <thead>
                <tr>
                    <th field="sender" width="100" align='center'>
                        发件人
                    </th>
                    <th field="title" width="200" align='center'>
                        主题
                    </th>
                    <th field="sendDate" width="100" align='center'>
                        时间
                    </th>
                    <th field="readFlag" width="60" align='center' hidden='true'>
                        是否阅读
                    </th>
                    <th field="Content" width="60" align='center' hidden='true'>
                        内容
                    </th>
                    <th field="receiver" width="60" align='center' hidden='true'>
                        收件人
                    </th>
                    <th field="CC" width="60" align='center' hidden='true'>
                        抄送
                    </th>
<%--                    <th field="SC" width="60" align='center' hidden='true'>
                        密送
                    </th>--%>
                </tr>
            </thead>
        </table>
    </div>
    <div id="tb" style="padding: 5px; height: auto">
        <div>
            <a href="#" onclick="DeleteEmail();" class="easyui-linkbutton" iconcls="icon-remove"
                plain="true">彻底删除</a>
        </div>
    </div>
    <div id="detailInfo" region="center" style="overflow-y: hidden; display: none">
        <div region="north" style="height: 30%;">
            <table width="100%">
                <tr>
                    <td align="left" width="3%">
                    </td>
                    <td align="left" width="10%">
                        主&nbsp;&nbsp;&nbsp;题：
                    </td>
                    <td>
                        <input id="title" type="text" class="easyui-validatebox" style="border: 0; background: transparent;
                            height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td align="left" width="3%">
                    </td>
                    <td align="left" width="10%">
                        发件人：
                    </td>
                    <td align="left">
                        <input id="from" type="text" class="easyui-validatebox" style="border: 0; background: transparent;
                            height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td align="left" width="3%">
                        &nbsp;
                    </td>
                    <td align="left" width="10%">
                        收件人：
                    </td>
                    <td align="left">
                        <input id="to" type="text" class="easyui-validatebox" style="border: 0; background: transparent;
                            height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td align="left" width="3%">
                        &nbsp;
                    </td>
                    <td align="left" width="10%">
                        抄&nbsp;&nbsp;&nbsp;送：
                    </td>
                    <td align="left">
                        <input id="cc" type="text" class="easyui-validatebox" style="border: 0; background: transparent;
                            height: 18px" />
                    </td>
                </tr>
<%--                <tr style="display: none">
                    <td align="left" width="3%">
                        &nbsp;
                    </td>
                    <td align="left" width="10%">
                        密&nbsp;&nbsp;&nbsp;送：
                    </td>
                    <td align="left">
                        <input id="sc" type="text" class="easyui-validatebox" style="border: 0; background: transparent;
                            height: 18px" />
                        (密送人仅在已发送文件夹显示，对发件人可见。)
                    </td>
                </tr>--%>
                <tr>
                    <td align="left" width="3%">
                        &nbsp;
                    </td>
                    <td align="left" width="10%">
                        时&nbsp;&nbsp;&nbsp;间：
                    </td>
                    <td align="left">
                        <input id="date" type="text" class="easyui-validatebox" style="border: 0; background: transparent;
                            width: 50%; height: 18px" />
                    </td>
                </tr>
            </table>
        </div>
        <div region="center" style="height: 48%; overflow-x: hidden">
            <textarea id="content" cols="150" rows="15" style="background-color: #fff; overflow: auto;
                border: 0; background-color: #ECF5FF"></textarea>
        </div>
        <div region="south" style="height: 22%; overflow-x: hidden">
            <table id="att_grid" class="easyui-datagrid" idfield="编码" sortname="文件大小" sortorder="asc"
                singleselect="true" remotesort="false" fit="true" fitcolumns="true">
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

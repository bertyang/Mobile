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
                url: '/Email/EmailSearch/?pageNumber=<%=ViewData["pageNumber"]%>'+ '&workerID=' + '<%=ViewData["workerID"] %>' + '&folderID=' + '<%=ViewData["folderID"] %>',

                onClickRow: function (rowIndex, rowData) {
                    window.location.href = "/Email/Write/?pageNumber=" + getCurrentPage('grid')
                                    + "&mailID=" + rowData.ID;
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

        function Delete()
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
            $.messager.confirm('提示', '是否删除这些邮件?', function (r) {
                if (!r) {
                    return;
                }
                $.ajax({
                    type: "POST",
                    url: '/Email/Delete/?folderID=' + '<%=ViewData["folderID"] %>',
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
            nowrap="false" striped="true" rownumbers="true" sortname="CreateTime" sortorder="desc"
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
            <a href="#" onclick="Delete();" class="easyui-linkbutton" iconcls="icon-remove"
                plain="true">删除</a>
        </div>
    </div>
</body>
</html>

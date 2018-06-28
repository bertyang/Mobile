<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width" />
    <title></title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <style type="text/css">
        tr
        {
            height: 22px;
        }
        *
        {
            font-size: 12px;
        }
    </style>
    <script type="text/javascript" language="javascript">

        $(function () {
            $('#dg').datagrid({
                url: '/BasicInfo/StationMsgSearch/?alarmEventCode=<%=ViewData["alarmEventCode"]%>',
                pagination: true,
                pageNumber: 1,
                pageList: [10, 15, 20],
                pageSize: 10,
                fitColumns: true,
                nowrap: true,
                striped: true,
                rownumbers: true,
                singleSelect: true,
                idField: "时间",
                sortname: "时间",
                sortorder:'desc',
                remotesort:false,
                columns: [[
    		{ field: '事件名称', title: '事件名称', width: 150, align: 'center' },
    		{ field: '时间', title: '时间', width: 100, align: 'center', formatter: renderTime },
    		{ field: '分站', title: '分站', width: 60, align: 'center' },
    		{ field: '回复类型', title: '回复类型', width: 60, align: 'center' }
        ]]

            });
        });

    </script>
</head>
<body>
    <div>
    <table id="dg"></table>
    </div>
</body>
</html>

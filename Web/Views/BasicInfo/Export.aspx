<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Export</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript" language="javascript">
        function createFrame(url) {
            var s = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:800px;height:100%;"></iframe>';
//            alert(s)
            return s;
        }

        $(function () {
            $('#gridViewAndFunction').datagrid({
                url: '/BasicInfo/LoadViewAndFunction/',
                singleSelect: true,
                onSelect: function (rowIndex, rowData) {
                    $('#tabs').tabs('add', {
                        title: rowData.name+"_条件选择",
                        content: createFrame('/BasicInfo/Export_Case/?id=' + rowData.id + '&name=' + escape(rowData.name) + '&value=' + escape(rowData.value) + '&t=' + escape(rowData.t)),
                        closable: true
                    });
                },
                columns: [[
                { field: 'name', title: '名称' },
                { field: 'value', title: '说明' },
                { field: 't', title: '类型' }
                ]]
            });
//            var oData = getAjaxData('/BasicInfo/LoadViewAndFunction/', {});
//            $('#grid').datagrid('loadData', oData);
        });

    </script>
</head>
<body class="easyui-layout">
    <div data-options="region:'west',split:true" title="视图函数列表" style="width:400px;">
        <table id="gridViewAndFunction"></table>
    </div>
    <div id="tabs" region="center" class="easyui-tabs" fit="true" border="false">

    </div>
</body>
</html>

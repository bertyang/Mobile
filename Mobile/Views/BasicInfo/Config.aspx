<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Index</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
        <script type="text/javascript" language="javascript">
            $(function () {
                $('#grid').datagrid({
                    iconCls: 'icon-save',
                    nowrap: false,
                    striped: true,
                    url: '/BasicInfo/ConfigLoad/',
                    sortName: 'ID',
                    sortOrder: 'asc',
                    remoteSort: false,
                    fitColumns: true,  //自适应列宽
                    fit: true,
                    pagination: true,  //分页工具栏
                    rownumbers: true,
                    pageNumber: 1,
                    pageList: [50, 100, 150],
                    pageSize: 50,
                    singleSelect: true,
                    idField: 'Key',
//                    frozenColumns: [[
//	                { field: 'ck', checkbox: true, align: 'center' }
//				    ]],
                    columns: [[
                    { field: 'Key', title: 'Key', width: 0, align: 'center', hidden: 'true' },
                    { field: 'Type', title: '类别', align: 'left' },
                    { field: 'Description', title: '项目',  align: 'left' },
                    { field: 'Value', title: '值',  align: 'left' }
                    ]],

                    toolbar: [{
                        id: 'btnUpdate',
                        text: '修改',
                        iconCls: 'icon-edit',
                        handler: function () {
                            var row = $('#grid').datagrid('getSelected');
                            if (row) {
                                this.href = "/BasicInfo/ConfigEdit/?key=" + row.Key;
                            }
                            else {
                                $.messager.alert('提示', '请选择要修改的数据');
                                return;
                            }

                        }

                    }]
                });

            });        
        </script>
</head>
<body class="easyui-layout">
    <div region="center" style="padding: 5px" border="true" > 
        <table id="grid" align="center"  >
        </table>
    </div>
</body>
</html>
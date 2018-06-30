<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>SelectPerson</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js") %>
    <script type="text/javascript" language="javascript">

        function Search() {
            $('#gridPerson').datagrid('options').url = '/Organize/WorkerLoad/';

//            debugger;
            $('#gridPerson').datagrid('load', {
                name: $('#txtName').val(),
                orgId: $('#Organization').combotree('getValue'),
                roleId:$('#role').combobox('getValue')
            });
        }

        function getRows() {
            return $('#gridPerson').datagrid('getSelections');
        }

        function getRow() {
            return $('#gridPerson').datagrid('getSelected');

        }
        //页面初始化加载
        $(document).ready(function () {
            //部门
            $('#Organization').combotree({
                url: '/Organize/UnitTree'
//                url: '/Report/Report/StoreHouseTree',
//                onLoadSuccess: function () {
//                    $('#StoreHouseTree').combotree('setValue', $('#TreeStoreHouseId').val());
//                }
            });

            //角色
            //调度员
            EUIcombobox("#role",{
                    valueField:"ID",
                    textField:"Name",
                    OneOption:[{
                        ID:"",
                        Name:"--请选择--"
                        }],
                    url:"/Organize/RoleLoadAll/"
            });

            //load data
            $('#gridPerson').datagrid({
                iconCls: 'icon-save',
                nowrap: false,
                striped: true,
                url: '/Organize/WorkerLoad/',
                sortName: 'TaskId',
                sortOrder: 'desc',
                remoteSort: false,
                width: 1000,
                toolbar: "#tb",
                fit: true,
                fitColumns: true,
                pagination: true,
                rownumbers: true,
                pageNumber: 1,
                pageList: [200, 400, 600],
                pageSize: 200,
                idField: 'ID',
                frozenColumns: [[
	                { field: 'cb', checkbox: true, align: 'center'}
                    ]],
                columns: [[
                    { field: 'Name', title: '姓名', width: 80, align: 'center' },
                    { field: 'ID', title: '人员ID', width: 80, align: 'center' },

                    ]],
                onLoadSuccess: function(data){
//                    for(var i=0;i<data.rows.length;i++)
//                    {
//	                    if(data.rows[i].IsEverEntry)
//	                    {
//                            $('#gridPerson').datagrid('checkRow', i);
//	                    }
//                    }
                },   
                onCheck: function (rowIndex, rowData) {
                    if (rowData.Active == "否") {
                        $.messager.alert('提示', rowData.Name + '未启用！', 'info', function () {
                            $('#gridPerson').datagrid('uncheckRow', rowIndex);
                        });
                    }
                },
                onCheckAll: function (rows) {
                    $.each(rows, function (i, n) {
                        if (n.Active == "否") {
                            $('#gridPerson').datagrid('uncheckRow', i);
                        }
                    });
                    $(this).datagrid('getPanel').find('div.datagrid-header-check').children('input[type="checkbox"]').eq(0).attr('checked', true);
                }
            });
        });
    </script>
</head>
<body class="easyui-layout">
    <div class="easyui-layout" icon="icon-save" style="padding: 5px;  width: 100%; height: 100%;"> 
        <%--<div region="west" title="物品分类" iconcls="icon tu0104" style="width: 180px;"  id="west">
            <ul id="categoryTree" line="true">
            </ul>
        </div> --%>
        <div region="center">
            <table id="gridPerson" align="center"  >
            </table>
        </div>   
    </div>

    
    <!--物品查询工具栏-->
    <div id="tb" style="padding: 5px; height: auto">
        <table width="100%">
            <tr>
                <td width="3%">
                </td>
                <td width="240px;">
                    部门:
                    <input id="Organization" class="easyui-combotree" style="width:180px;" />
                </td>
                <td width="150px">
                    姓名:
                    <input id="txtName" class="easyui-validatebox" style="border: 1px solid #8DB2E3;width: 81px;" />
                </td>
                <td width="150px">
                    角色:
                    <select id="role" class="easyui-combobox" name="role" style="width: 100px;">
                    </select>
                </td>
                <td width="100px">
                    <a href="#" id="btnSearch" class="easyui-linkbutton" iconcls="icon-search" onclick="Search();">查询</a>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
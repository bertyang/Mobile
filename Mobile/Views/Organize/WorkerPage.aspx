<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Index</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript" language="javascript">

        $(function () {
            //角色
            //$('#grid').datagrid('options').url = "/Organize/RoleLoadAll/?role=" + $('#role').combobox('getValue');

            //EUIcombobox("#role", {
            //    valueField: "ID",
            //    textField: "Name",
            //    url: "/Organize/RoleLoadAll/"
            //});

            $('#role').combobox({
                url: '/Organize/RoleLoadAll/',
                valueField: 'ID',
                textField: 'Name',
                onLoadSuccess: function (data) {
                    $('#role').combobox('setValue', "--请选择--");
                }
            })
            //页面加载
            $('#grid').datagrid({
                url: '/Organize/WorkerLoad/?orgId=' + '<%=ViewData["orgId"] %>'
            });

            //查询
            $('#btnSearch').bind('click',
                    function () {
                        $('#grid').datagrid('options').url = '/Organize/WorkerLoad/?name=' + escape($('#txtName').val()) 
                         + '&orgId=' + '<%=ViewData["orgId"] %>'
                         + '&roleId=' + $('#role').combobox('getValue')
                         + '&empNo=' + escape($('#txtEmpNo').val());
                        $('#grid').datagrid("reload");
                        $('#grid').datagrid('clearSelections');
                    }
               );

            //添加
            $('#btnAdd').bind('click',
                    function () {
                        var url = "/Organize/WorkerEdit/";

                        parent.AddTab("新增人员", url, "tu1201");
                    }
                );

            //修改
            $('#btnEdit').bind('click',
                    function () {
                        var row = $('#grid').datagrid('getSelected');
                        if (row) {
                            var url = "/Organize/WorkerEdit/?workerId=" + row.ID;

                            parent.AddTab("修改人员", url, "tu1201");

                        }
                        else {
                            $.messager.alert('提示', '请选择要修改的数据');
                            return;
                        }

                    }
                );

            //导出
            $('#btnExport').bind('click',
                function () {

                    document.location.href = "/Organize/WorkerLoadupdo/?"
                                                + "name=" + escape($('#txtName').val())
                                                + "&orgId=" + '<%=ViewData["orgId"] %>'
                                                + "&roleId=" + $('#role').combobox('getValue')
                                                + "&empNo=" + escape($('#txtEmpNo').val());
                }
            );

            //删除
            $('#btnDel').bind('click',
                    function () {
                        var rows = $('#grid').datagrid('getChecked');
                        if (!rows || rows.length == 0) {
                            $.messager.alert('提示', '请选择要删除的数据');
                            return;
                        }
                        var parm;
                        $.each(rows, function (i, n) {

                            if (n.EmpNo != null && n.EmpNo.indexOf("(<font color='red'>上班</font>)") > 0)
                            {
                                return true;
                            }

                            if (i == 0) {
                                parm = "idList=" + n.ID;
                            }
                            else {
                                parm += "&idList=" + n.ID;
                            }
                        });
                       
                        if (typeof (parm) == "undefined") {
                            $.messager.alert('提示', '上班人员删除不了!');
                            return;
                        }

                        $.messager.confirm('提示', '该操作会删除对应的调度系统工号，确认删除?', function (r) {
                            if (!r) {
                                return;
                            }

                            $.ajax({
                                type: "POST",
                                url: "/Organize/WorkerDelete/",
                                data: parm,
                                success: function (msg) {
                                    if (msg.IsSuccess) {
                                        $.messager.alert('提示', '删除成功！', "info", function () {
                                            $('#grid').datagrid('reload');
                                        });
                                    }
                                    else {
                                        $.messager.alert('错误', '删除失败！', "error");
                                    }
                                },
                                error: function () {
                                    $.messager.alert('错误', '删除失败！', "error");
                                }
                            });
                        });

                    }
                );
        });
    
    </script>
</head>
<body class="easyui-layout">
    <div region="center" style="padding: 5px" border="true">
        <table id="grid" class="easyui-datagrid" align="center" toolbar="#tb" pagination="true"
            pagenumber='<%=(dynamic)this.ViewData["pageNumber"] %>' pagelist="[10, 15, 20]"
            pagesize="15" idfield="ID" nowrap="false" striped="true" rownumbers="true" sortname="ID"
            sortorder="asc" remotesort="false" width="1000" fit="true" fitcolumns="true" checkOnSelect="false"  selectOnCheck="false"
            singleselect="true">
            <thead>
                <tr>
                    <th width="20" checkbox="true" align='center'>
                    </th>
                    <th field="ID" width="30" align='center' sortable="true">
                        编码
                    </th>
                    <th field="Name" width="70" align='center'  sortable="true">
                        姓名
                    </th>
                    <th field="EmpNo" width="100" align='center' sortable="true">
                        工号
                    </th>
                    <th field="Role" width="100" align='center' sortable="true">
                        角色
                    </th>
                    <th field="Unit" width="100" align='center' sortable="true">
                        机构
                    </th>
                    <th field="Post" width="60" align='center' sortable="true">
                        职位
                    </th>
                    <th field="ParentName" width="70" align='center' sortable="true">
                        上级
                    </th>
                    <th field="Tel" width="70" align='center' sortable="true">
                        通讯号码
                    </th>
                    <th field="Mobile" width="70" align='center' sortable="true">
                        短信号码
                    </th>
                    <th field="Active" width="20" align='center' sortable="true">
                        启用
                    </th>
                </tr>
            </thead>
        </table>
    </div>
    <div id="tb" style="padding: 5px; height: auto">
        <table width="100%">
            <tr>
                <td style="width: 30%">
                    <div style="margin-bottom: 5px">
                        <a href="#" id="btnAdd" class="easyui-linkbutton" iconcls="icon-add" plain="true">添加</a>
                        <a href="#" id="btnEdit" class="easyui-linkbutton" iconcls="icon-edit" plain="true">修改</a>
                        <a href="#" id="btnDel" class="easyui-linkbutton" iconcls="icon-remove" plain="true">删除</a>
                    </div>
                </td>
                <td>
                    <div region="right">
                        <table width="100%">
                            <tr>
                                <td width="22%">
                                    姓名:
                                    <input id="txtName" class="easyui-validatebox" style="border: 1px solid #8DB2E3;
                                        width: 81px; height: 18px" />
                                </td>
                                <td width="22%">
                                    工号:
                                    <input id="txtEmpNo" class="easyui-validatebox" style="border: 1px solid #8DB2E3;
                                        width: 81px; height: 18px" />
                                </td>
                                <td width="25%">
                                    角色:
                                    <select id="role" class="easyui-combobox" name="role" style="width: 100px; height: 18px">
                                    </select>
                                </td>
                                <td width="15%">
                                    <input type="hidden" id="orgId" />
                                    <a href="#" id="btnSearch" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                                </td>
                                <td width="15%">
                                    <input type="hidden" id="orgId1" />
                                    <a href="#" id="btnExport" class="easyui-linkbutton" iconcls="icon-redo">导出</a>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
    </div>
</body>
</html>

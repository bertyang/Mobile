<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>UnitPage</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript">


        //#region 科室
        $(function () {
            $('#BranchManager').combobox({
                url: '/Organize/GetAllWorker/',
                valueField: 'ID',
                textField: 'Name'
            });

            //$('#BranchBelong').combobox({
            //    url: '/Organize/StationLoad/',
            //    valueField: '编码',
            //    textField: '名称'
            //});

            //添加
            $('#btnAddBranch').bind('click',
            function () {

                //var row = $('#Station').datagrid('getSelected');

                //if (!row) {
                //    $.messager.alert('提示', "请选择一个分站", 'info');
                //    return;
                //}

                $('#BranchEdit').show();
                $('#BranchEdit').dialog({
                    collapsible: true,
                    minimizable: true,
                    maximizable: true,
                    height: 250,
                    width: 300,
                    modal: true,
                    title: '新增科室',
                    buttons: [
                            {
                                text: '确定',
                                iconCls: 'icon-save',
                                handler: function () {
                                    SaveBranch();
                                }
                            },
                            {
                                text: '取消',
                                iconCls: 'icon-cancel',
                                handler: function () {
                                    $('#BranchEdit').dialog('close');
                                }
                            }]
                });

                $("#BranchId").attr("readonly", false);
                $('#BranchId').val(GetBranchCode());
                $('#BranchName').val('');
                $('#BranchSequence').val('');
                $('#BranchActive').combobox('setValue', '');
                $('#BranchManager').combobox('setValue', '');
                $('#BranchBelong').val(<%=Request.QueryString["stationId"] %>);
            }
        );

            //修改
            $('#btnEditBranch').bind('click',
            function () {
                var row = $('#Branch').datagrid('getSelected');
                if (row) {
                    $('#BranchEdit').show();
                    $('#BranchEdit').dialog({
                        collapsible: true,
                        minimizable: true,
                        maximizable: true,
                        height: 250,
                        width: 300,
                        modal: true,
                        title: '修改科室',
                        buttons: [
                            {
                                text: '确定',
                                iconCls: 'icon-save',
                                handler: function () {
                                    SaveBranch();
                                }
                            },
                            {
                                text: '取消',
                                iconCls: 'icon-cancel',
                                handler: function () {
                                    $('#BranchEdit').dialog('close');
                                }
                            }]
                    });

                    $("#BranchId").attr("readonly", true);
                    $('#BranchId').val(row.编码);
                    $('#BranchName').val(row.名称);
                    $('#BranchSequence').val(row.顺序号);
                    $('#BranchManager').combobox('setValue', row.ManagerID);
                    if (row.启用 == "是") {
                        $('#BranchActive').combobox('setValue', "true");
                    }
                    else {
                        $('#BranchActive').combobox('setValue', "false");
                    }
                    $('#BranchBelong').val(row.分站ID);
                }
                else {
                    $.messager.alert('提示', '请选择要修改的数据');
                    return;
                }
            }
        );

            //删除
            $('#btnDelBranch').bind('click',
            function () {

                var rows = $('#Branch').datagrid('getChecked');
                if (!rows || rows.length == 0) {
                    $.messager.alert('提示', '请选择要删除的数据');
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

                $.messager.confirm('提示', '是否删除这些数据?', function (r) {
                    if (!r) {
                        return;
                    }

                    $.ajax({
                        type: "POST",
                        url: "/Organize/BranchDelete/",
                        data: parm,
                        success: function (msg) {
                            if (msg.IsSuccess) {
                                $.messager.alert('提示', msg.Message, 'info', function () {
                                    $('#Branch').datagrid("reload");
                                    parent.$('#tree').tree('reload');
                                });
                            }
                            else {
                                $.messager.alert('提示', msg.Message, 'info', function () {
                                });
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

        function SaveBranch() {
            $('#formBranch').form('submit', {
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (msg) {
                    var data = eval('(' + msg + ')');
                    if (data.IsSuccess) {
                        $.messager.alert('提示', data.Message, 'info', function () {
                            $('#BranchEdit').dialog('close');
                            $('#Branch').datagrid('reload');
                            parent.$('#tree').tree('reload');
                        });
                    }
                    else {
                        $.messager.alert('提示', data.Message, 'info', function () {
                        });
                    }
                }
            });
        }
        //#endregion
        function GetBranchCode() {

            var result;

            $.ajax({
                type: "POST",
                async: false,
                url: "/Organize/GetCode/?tableName=TZBranch",
                dataType: "json",
                success: function (data) {
                    result = data;
                },
                error: function () {
                    result = false;
                }
            });

            return parseInt(result);
        }

        function IsExistBranchCode() {
            if (!$('#BranchId').attr("readonly")) {
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "/Organize/IsExistCode/?tableName=TZBranch&code=" + $('#BranchId').val(),
                    success: function (msg) {

                        if (msg.IsSuccess) {
                            $.messager.alert('提示', '已存此编码', 'error');
                            $('#BranchId').val('');
                        }
                    }
                })
            }
        }
    </script>
</head>
<body  class="easyui-layout">
    <!--科室-->
    <div id="PanelBranch" region="center" split="true" title="科室管理" iconcls="icon tu0522"  border="false" style="height:200px;">
        <table id="Branch" class="easyui-datagrid" align="center" toolbar="#tbBranch" url="/Organize/BranchLoad/?stationId=<%=Request.QueryString["stationId"] %>"
            idfield="编码" singleselect="true" fitcolumns="true" nowrap="false" striped="true"
            rownumbers="true" fit="true" checkOnSelect="false"  selectOnCheck="false">
            <thead>
                <tr>
                    <th width="20" checkbox="true" align='center'>
                    <th field="编码" width="100px" align='center'>
                        编码
                    </th>
                    <th field="名称" width="100px" align='center'>
                        名称
                    </th>
                    <th field="顺序号" width="100px" align='center'>
                        顺序号
                    </th>
                    <th field="启用" width="100px" align='center'>
                        启用
                    </th>
                    <th field="负责人" width="100px" align='center'>
                        负责人
                    </th>
                    <th field="分站名称" width="100px" align='center'>
                        所属分站
                    </th>
                </tr>
            </thead>
        </table>
    </div>

    <!--科室按钮-->
    <div id="tbBranch">
        <table width="100%">
            <tr>
                <td style="width: 50%">
                    <div>
                        <a href="#" id="btnAddBranch" class="easyui-linkbutton" iconcls="icon-add" plain="true">添加</a>
                        <a href="#" id="btnEditBranch" class="easyui-linkbutton" iconcls="icon-edit" plain="true">修改</a>
                        <a href="#" id="btnDelBranch" class="easyui-linkbutton" iconcls="icon-remove" plain="true">删除</a>
                    </div>
                </td>
            </tr>
        </table>
    </div>

    <!--科室编辑-->
    <div id="BranchEdit" icon="icon-save" style="padding: 5px; display: none">
            <form id="formBranch" method="post" action="/Organize/BranchSave/" enctype="application/x-www-form-urlencoded">
                <table>
                <tr>
                    <td style="width:40%; text-align: right">
                        编号(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                         <input id="BranchId" type="text" class="easyui-validatebox"  
                            name="entity.编码" style="width: 145px;"
                            validtype="length[1,3]" 
                            onkeyup="this.value=this.value.replace(/[^\d]/g,'') " 
                            onafterpaste="this.value=this.value.replace(/[^\d]/g,'') 
                            required="true" onblur="IsExistBranchCode()"/>
                    </td>
                </tr>
                <tr style="height: 30px;">
                    <td style="width:40%; text-align: right">
                        名称(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="BranchName" type="text" class="easyui-validatebox" required="true" name="entity.名称" 
                            validtype="length[1,15]" style="border: 1px solid #8DB2E3; width: 144px;" />
                    </td>
                </tr>
                <tr style="height: 30px;">
                    <td style="width:40%; text-align: right">
                        负责人(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="BranchManager" type="text"  class="easyui-combobox"  name="ManagerID" required="true"  editable="false" style="width: 150px;"/>
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        顺序号(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="BranchSequence" type="text" class="easyui-validatebox" editable="false"  required="true"
                            name="entity.顺序号" validtype="length[1,11]" onkeyup="this.value=this.value.replace(/[^\d]/g,'')"
                            style="border: 1px solid #8DB2E3; width: 144px;"/>
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        启用(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <select id="BranchActive" class="easyui-combobox" name="entity.是否有效" editable="false" required="true" style="width: 150px;">
                            <option value=true>是</option>
                            <option value=false>否</option>
                        </select>
                    </td>
                </tr>
                <%--<tr>
                    <td style="width:40%; text-align: right">
                        所属分站(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <select id="BranchBelong" class="easyui-combobox" name="BranchBelong" editable="false" required="true"
                            style="width: 150px;">
                        </select>
                    </td>
                </tr>--%>
                   <input id="BranchBelong" type="hidden"  name="BranchBelong"/>
            </table>
         </form>
     </div>
</body>
</html>

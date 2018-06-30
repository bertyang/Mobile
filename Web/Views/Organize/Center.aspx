<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>UnitPage</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript">

        //#region 中心
        $(function () {

            $('#CenterType').combobox({
                url: '/Organize/CenterTypeLoad/',
                valueField: '编码',
                textField: '名称'
            });

            $('#CenterBelong').combobox({
                url: '/Organize/CenterLoad/',
                valueField: '编码',
                textField: '名称'
            });

            $('#CenterManager').combobox({
                url: '/Organize/GetAllWorker/',
                valueField: 'ID',
                textField: 'Name'
            });

            //添加
            $('#btnAddCenter').bind('click',
                    function () {
                        $('#CenterEdit').show();
                        $('#CenterEdit').dialog({
                            collapsible: true,
                            minimizable: true,
                            maximizable: true,
                            height: 450,
                            width: 300,
                            modal: true,
                            title: '新增中心',
                            buttons: [
                                    {
                                        text: '确定',
                                        iconCls: 'icon-save',
                                        handler: function () {
                                            SaveCenter();
                                        }
                                    },
                                    {
                                        text: '取消',
                                        iconCls: 'icon-cancel',
                                        handler: function () {
                                            $('#CenterEdit').dialog('close');
                                        }
                                    }]
                        });

                        $('#CenterBelong').combobox("reload");
                        $('#CenterId').val(GetCenterCode());
                        $("#CenterId").attr("readonly", false);
                        $('#CenterName').val("");
                        $('#CenterPY').val("");
                        $('#CenterType').combobox('setValue', 1);
                        $('#CenterDispatch').combobox('setValue', '');
                        $('#CenterIPAddress').val('');
                        $('#CenterCTIIPAddress').val('');
                        $('#CenterTel').val('');
                        $('#CenterTaskNo').val('');
                        $('#CenterBelong').combobox('setValue', '');
                        $('#CenterSequence').val('');
                        $('#CenterActive').combobox('setValue', '');
                        $('#CenterManager').combobox('setValue', '');
                        $('#IsSMS').combobox('setValue', '');
                    }
                );

            //修改
            $('#btnEditCenter').bind('click',
                    function () {
                        var row = $('#Center').datagrid('getSelected');
                        if (row) {
                            $('#CenterEdit').show();
                            $('#CenterEdit').dialog({
                                collapsible: true,
                                minimizable: true,
                                maximizable: true,
                                height: 450,
                                width: 300,
                                modal: true,
                                title: '修改中心',
                                buttons: [
                                    {
                                        text: '确定',
                                        iconCls: 'icon-save',
                                        handler: function () {
                                            SaveCenter();
                                        }
                                    },
                                    {
                                        text: '取消',
                                        iconCls: 'icon-cancel',
                                        handler: function () {
                                            $('#CenterEdit').dialog('close');
                                        }
                                    }]
                            });

                            $('#CenterBelong').combobox("reload");
                            $("#CenterId").attr("readonly", true);
                            $('#CenterId').val(row.编码);
                            $('#CenterName').val(row.名称);
                            $('#CenterPY').val(row.拼音头);
                            $('#CenterType').combobox('setValue', row.类型编码);
                            $('#CenterIPAddress').val(row.IP地址);
                            $('#CenterCTIIPAddress').val(row.CTI服务IP地址);
                            $('#CenterTel').val(row.电话号码);
                            $('#CenterTaskNo').val(row.当前任务流水号);
                            $('#CenterBelong').combobox('setValue', row.所属调度中心编码);
                            $('#CenterSequence').val(row.顺序号);
                            $('#CenterManager').combobox('setValue', row.ManagerID);
                            if (row.启用 == "是") {
                                $('#CenterActive').combobox('setValue', "true");
                            }
                            else {
                                $('#CenterActive').combobox('setValue', "false");
                            }
                            if (row.是否调度 == "是") {
                                $('#CenterDispatch').combobox('setValue', "true");
                            }
                            else {
                                $('#CenterDispatch').combobox('setValue', "false");
                            }
                            if (row.是否发送短信 == "是") {
                                $('#SendSMS').combobox('setValue', "true");
                            }
                            else {
                                $('#SendSMS').combobox('setValue', "false");
                            }
                        }
                        else {
                            $.messager.alert('提示', '请选择要修改的数据');
                            return;
                        }
                    }
                );

            //删除
            $('#btnDelCenter').bind('click',
                    function () {
                        var rows = $('#Center').datagrid('getChecked');
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
                                url: "/Organize/CenterDelete/",
                                data: parm,
                                success: function (msg) {
                                    if (msg.IsSuccess) {
                                        $.messager.alert('提示', msg.Message, 'info', function () {
                                            $('#Center').datagrid("reload");
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

        function SaveCenter() {
            $('#formCenter').form('submit', {
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (msg) {
                    var data = eval('(' + msg + ')');
                    if (data.IsSuccess) {
                        $.messager.alert('提示', data.Message, 'info', function () {
                            $('#CenterEdit').dialog('close');
                            $('#Center').datagrid('reload');
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

        function GetCenterCode() {

            var result;

            $.ajax({
                type: "POST",
                async: false,
                url: "/Organize/GetCode/?tableName=TCenter",
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

        function IsExistCenterCode() {
            if (!$('#CenterId').attr("readonly")) {
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "/Organize/IsExistCode/?tableName=TCenter&code=" + $('#CenterId').val(),
                    success: function (msg) {

                        if (msg.IsSuccess) {
                            $.messager.alert('提示', '已存此编码', 'error');
                            $('#CenterId').val('');
                        }
                    }
                })
            }
        }

    </script>
</head>
<body  class="easyui-layout">
    <!--中心-->
    <div id="PanelCenter" region="center"  title="中心管理" iconcls="icon tu0820"  border="false">
        <table id="Center" class="easyui-datagrid" align="center" toolbar="#tbCenter" url="/Organize/CenterLoad/"
            idfield="编码" singleselect="true" fitcolumns="true" nowrap="true" striped="true"
            rownumbers="true" fit="true"  checkOnSelect="false"  selectOnCheck="false">
            <thead>
                <tr>
                    <th width="20" checkbox="true" align='center'>
                    <th field="编码" width="50px" align='center'>
                        编码
                    </th>
                    <th field="名称" width="100px" align='center'>
                        名称
                    </th>
                    <th field="拼音头" width="50px" align='center'>
                        拼音头
                    </th>
                    <th field="类型名称" width="100px" align='center'>
                        类型名称
                    </th>
                    <th field="是否调度" width="80px" align='center'>
                        是否调度
                    </th>
                    <th field="IP地址" width="100px" align='center'>
                        IP地址
                    </th>
                    <th field="CTI服务IP地址" width="120px" align='center'>
                        CTI服务IP地址
                    </th>
                    <th field="电话号码" width="80px" align='center'>
                        电话号码
                    </th>
                    <th field="所属调度中心" width="100px" align='center'>
                        所属调度中心
                    </th>
                    <th field="顺序号" width="50px" align='center'>
                        顺序号
                    </th>
                    <th field="启用" width="50px" align='center'>
                        启用
                    </th>
                    <th field="是否发送短信" width="100px" align='center'>
                        是否发送短信
                    </th>
                    <th field="负责人" width="100px" align='center'>
                        负责人
                    </th>
                </tr>
            </thead>
        </table>
    </div>

    <!--中心按钮-->
    <div id="tbCenter">
        <table width="100%">
            <tr>
                <td style="width: 50%">
                    <div>
                        <a href="#" id="btnAddCenter" class="easyui-linkbutton" iconcls="icon-add" plain="true">添加</a>
                        <a href="#" id="btnEditCenter" class="easyui-linkbutton" iconcls="icon-edit" plain="true">修改</a>
                        <a href="#" id="btnDelCenter" class="easyui-linkbutton" iconcls="icon-remove" plain="true">删除</a>
                    </div>
                </td>
            </tr>
        </table>
    </div>

    <!--中心编辑-->
    <div id="CenterEdit" icon="icon-save" style="padding: 5px; display: none">
            <form id="formCenter" method="post" action="/Organize/CenterSave/" enctype="application/x-www-form-urlencoded">
                <table>
                <tr>
                    <td style="width:40%; text-align: right">
                        编号(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="CenterId" type="text" class="easyui-validatebox"  
                            name="entity.编码" style="width: 145px;"  validtype="length[1,3]" 
                            onkeyup="this.value=this.value.replace(/[^\d]/g,'') " 
                            onafterpaste="this.value=this.value.replace(/[^\d]/g,'') " 
                            required="true" onblur="IsExistCenterCode()"/>
                    </td>
                </tr>
                <tr style="height: 30px;">
                    <td style="width:40%; text-align: right">
                        名称(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="CenterName" type="text" class="easyui-validatebox" required="true" name="entity.名称" required="true"
                            validtype="length[1,25]" style="border: 1px solid #8DB2E3; width: 144px;" />
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        拼音头(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="CenterPY" type="text" class="easyui-validatebox" required="true" name="entity.拼音头" 
                            validtype="length[1,10]" style="border: 1px solid #8DB2E3; width: 144px;" />
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        类型(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="CenterType" class="easyui-combobox" name="entity.类型编码" editable="false" required="true"
                            style="width: 150px;"/>
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        是否调度(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <select id="CenterDispatch" class="easyui-combobox" name="entity.是否调度" editable="false" required="true" style="width: 150px;">
                            <option value=true>是</option>
                            <option value=false>否</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        IP地址：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="CenterIPAddress" type="text" class="easyui-validatebox"
                            name="entity.IP地址" validtype="length[0,100]" style="border: 1px solid #8DB2E3; width: 144px;" />
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                       CTI服务IP地址：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="CenterCTIIPAddress" type="text" class="easyui-validatebox"
                            name="entity.CTI服务IP地址" validtype="length[0,15]" style="border: 1px solid #8DB2E3; width: 144px;" />
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        电话号码(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="CenterTel" type="text" class="easyui-validatebox" required="true" 
                            name="entity.电话号码" validtype="length[1,100]" style="border: 1px solid #8DB2E3; width: 144px;" />
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        当前任务流水号(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="CenterTaskNo" type="text" class="easyui-validatebox" editable="false"  required="true"
                            name="entity.当前任务流水号" validtype="length[1,11]" onkeyup="this.value=this.value.replace(/[^\d]/g,'')"
                            style="border: 1px solid #8DB2E3; width: 144px;"/>
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        所属调度中心(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <select id="CenterBelong" class="easyui-combobox" name="entity.所属调度中心编码" editable="false" required="true"
                            style="width: 150px;">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%; text-align: right">
                        负责人(<font color="red">*</font>)：
                    </td>
                    <td style="width: 60%; text-align: left">
                        <input id="CenterManager" type="text"  class="easyui-combobox"  name="ManagerID" required="true" editable="false" style="width: 150px;"/>
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        顺序号(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">                      
                        <input id="CenterSequence" type="text" class="easyui-validatebox" editable="false"  required="true"
                            name="entity.顺序号" validtype="length[1,11]" onkeyup="this.value=this.value.replace(/[^\d]/g,'')"
                            style="border: 1px solid #8DB2E3; width: 144px;"/>
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        启用(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <select id="CenterActive" class="easyui-combobox" name="entity.是否有效" editable="false" required="true" style="width: 150px;">
                            <option value=true>是</option>
                            <option value=false>否</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        是否发送短信(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <select id="SendSMS" class="easyui-combobox" name="entity.是否发送短信" editable="false" required="true" style="width: 150px;">
                            <option value=true>是</option>
                            <option value=false>否</option>
                        </select>
                    </td>
                </tr>
            </table>
         </form>
     </div>

</body>
</html>

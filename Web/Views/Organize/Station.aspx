<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>UnitPage</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript">



        //#region 分站
        $(function () {

            $('#StationType').combobox({
                url: '/Organize/StationTypeLoad/',
                valueField: '编码',
                textField: '名称'
            });

            $('#StationBelong').combobox({
                url: '/Organize/CenterLoad/',
                valueField: '编码',
                textField: '名称'
            });

            $('#StationManager').combobox({
                url: '/Organize/GetAllWorker/',
                valueField: 'ID',
                textField: 'Name'
            });

            $('#StationArea').combobox({
                url: '/Organize/AreaLoad/',
                valueField: '名称',
                textField: '名称'
            });

            //添加
            $('#btnAddStation').bind('click',
                    function () {

                        //var row = $('#Center').datagrid('getSelected');

                        //if (!row) {
                        //    $.messager.alert('提示', "请选择一个分中心", 'info');
                        //    return;
                        //}

                        $('#StationEdit').show();
                        $('#StationEdit').dialog({
                            collapsible: true,
                            minimizable: true,
                            maximizable: true,
                            height: 500,
                            width: 300,
                            modal: true,
                            title: '新增分站',
                            buttons: [
                                    {
                                        text: '确定',
                                        iconCls: 'icon-save',
                                        handler: function () {
                                            SaveStation();
                                        }
                                    },
                                    {
                                        text: '取消',
                                        iconCls: 'icon-cancel',
                                        handler: function () {
                                            $('#StationEdit').dialog('close');
                                        }
                                    }]
                        });

                        $("#StationId").attr("readonly", false);
                        $('#StationId').val(GetStationCode());
                        $('#StationName').val('');
                        $('#StationType').combobox('setValue', 1);
                        $('#StationDispatch').combobox('setValue', '');
                        $('#StationIP').val('');
                        $('#StationTel').val('');
                        $('#StationComm').val('');
                        $('#StationArea').combobox('setValue', '');
                        //$('#StationBelong').combobox('setValue', row.编码);
                        $('#StationSequence').val('');
                        $('#StationActive').combobox('setValue', '');
                        $('#StationGPS').combobox('setValue', '');
                        $('#StationFlag').combobox('setValue', '');
                        $('#StationX').val('');
                        $('#StationY').val('');
                        $('#StationManager').combobox('setValue', '');
                    }
                );

            //修改
            $('#btnEditStation').bind('click',
                    function () {
                        var row = $('#Station').datagrid('getSelected');
                        if (row) {
                            $('#StationEdit').show();
                            $('#StationEdit').dialog({
                                collapsible: true,
                                minimizable: true,
                                maximizable: true,
                                height: 500,
                                width: 300,
                                modal: true,
                                title: '修改分站',
                                buttons: [
                                    {
                                        text: '确定',
                                        iconCls: 'icon-save',
                                        handler: function () {
                                            SaveStation();
                                        }
                                    },
                                    {
                                        text: '取消',
                                        iconCls: 'icon-cancel',
                                        handler: function () {
                                            $('#StationEdit').dialog('close');
                                        }
                                    }]
                            });

                            $("#StationId").attr("readonly", true);
                            $('#StationId').val(row.编码);
                            $('#StationName').val(row.名称);
                            $('#StationType').combobox('setValue', row.类型编码);
                            $('#StationIP').val(row.IP地址);
                            $('#StationTel').val(row.电话号码);
                            $('#StationComm').val(row.通信标识);
                            $('#StationArea').combobox('setValue', row.所属区域);
                            $('#StationBelong').combobox('setValue', row.中心编码);
                            $('#StationSequence').val(row.顺序号);
                            $('#StationX').val(row.X坐标);
                            $('#StationY').val(row.Y坐标);
                            $('#StationManager').combobox('setValue', row.ManagerID);
                            if (row.启用 == "是") {
                                $('#StationActive').combobox('setValue', "true");
                            }
                            else {
                                $('#StationActive').combobox('setValue', "false");
                            }
                            if (row.是否调度 == "是") {
                                $('#StationDispatch').combobox('setValue', "true");
                            }
                            else {
                                $('#StationDispatch').combobox('setValue', "false");
                            }
                            if (row.是否传送GPS == "是") {
                                $('#StationGPS').combobox('setValue', "true");
                            }
                            else {
                                $('#StationGPS').combobox('setValue', "false");
                            }
                            if (row.是否标注 == "是") {
                                $('#StationFlag').combobox('setValue', "true");
                            }
                            else {
                                $('#StationFlag').combobox('setValue', "false");
                            }

                        }
                        else {
                            $.messager.alert('提示', '请选择要修改的数据');
                            return;
                        }
                    }
                );

            //删除
            $('#btnDelStation').bind('click',
                    function () {
                        var rows = $('#Station').datagrid('getChecked');
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
                                url: "/Organize/StationDelete/",
                                data: parm,
                                success: function (msg) {
                                    if (msg.IsSuccess) {
                                        $.messager.alert('提示', msg.Message, 'info', function () {
                                            $('#Station').datagrid("reload");
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

        function SaveStation() {
            $('#formStation').form('submit', {
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (msg) {
                    var data = eval('(' + msg + ')');
                    if (data.IsSuccess) {
                        $.messager.alert('提示', data.Message, 'info', function () {
                            $('#StationEdit').dialog('close');
                            $('#Station').datagrid('reload');
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
        function GetStationCode() {

            var result;

            $.ajax({
                type: "POST",
                async: false,
                url: "/Organize/GetCode/?tableName=TStation",
                dataType: "json",
                success: function (data) {
                    result = data;
                },
                error: function () {
                    result = false;
                }
            });

            return result.substring(2);
        }

        function IsExistStationCode() {
            if (!$('#StationId').attr("readonly")) {
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "/Organize/IsExistCode/?tableName=TStation&code=" + $('#StationId').val(),
                    success: function (msg) {

                        if (msg.IsSuccess) {
                            $.messager.alert('提示', '已存此编码', 'error');
                            $('#StationId').val('');
                        }
                    }
                })
            }
        }

    </script>
</head>
<body  class="easyui-layout">
   
    <!--分站-->
    <div id="PanelStation" region="center" split="true" title="分站管理" iconcls="icon tu1911"  border="false" >        
        <table id="Station" class="easyui-datagrid" align="center" toolbar="#tbStation" url="/Organize/StationLoad/?centerId=<%=Request.QueryString["centerId"] %>"
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
                    <th field="分站类型" width="100px" align='center'>
                        分站类型
                    </th>
                    <th field="IP地址" width="100px" align='center'>
                        IP地址
                    </th>
                    <th field="电话号码" width="100px" align='center'>
                        电话号码
                    </th>
                    <th field="通信标识" width="100px" align='center'>
                        通信标识
                    </th>
                    <th field="所属区域" width="100px" align='center'>
                        所属区域
                    </th>
                    <th field="是否传送GPS" width="100px" align='center'>
                        是否传送GPS
                    </th>
                    <th field="是否标注" width="100px" align='center'>
                        是否标注
                    </th>
                    <th field="X坐标" width="150px" align='center'>
                        X坐标
                    </th>
                    <th field="Y坐标" width="150px" align='center'>
                        Y坐标
                    </th>
                    <th field="顺序号" width="50px" align='center'>
                        顺序号
                    </th>
                    <th field="启用" width="50px" align='center'>
                        启用
                    </th>
                    <th field="负责人" width="100px" align='center'>
                        负责人
                    </th>
                    <th field="中心名称" width="100px" align='center'>
                        所属中心
                    </th>
                </tr>
            </thead>
        </table>

    </div>
    
   
    <!--分站按钮-->
    <div id="tbStation">
        <table width="100%">
            <tr>
                <td style="width: 50%">
                    <div>
                        <a href="#" id="btnAddStation" class="easyui-linkbutton" iconcls="icon-add" plain="true">添加</a>
                        <a href="#" id="btnEditStation" class="easyui-linkbutton" iconcls="icon-edit" plain="true">修改</a>
                        <a href="#" id="btnDelStation" class="easyui-linkbutton" iconcls="icon-remove" plain="true">删除</a>
                    </div>
                </td>
             </tr>
        </table>
    </div>
  
   
    <!--分站编辑-->
    <div id="StationEdit" icon="icon-save" style="padding: 5px; display: none">
            <form id="formStation" method="post" action="/Organize/StationSave/" enctype="application/x-www-form-urlencoded">
                <table>
                <tr>
                    <td style="width:40%; text-align: right">
                        编号(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="StationId" type="text" class="easyui-validatebox"  
                            name="entity.编码" style="width: 145px;"
                            required="true"
                            validtype="length[3,3]" 
                            onkeyup="this.value=this.value.replace(/[^\d]/g,'') " 
                            onafterpaste="this.value=this.value.replace(/[^\d]/g,'') " 
                            onblur="IsExistStationCode()"/>
                    </td>
                </tr>
                <tr style="height: 30px;">
                    <td style="width:40%; text-align: right">
                        名称(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="StationName" type="text" class="easyui-validatebox" required="true" name="entity.名称" 
                            validtype="length[1,15]" style="border: 1px solid #8DB2E3; width: 144px;" />
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        类型(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="StationType" class="easyui-combobox" name="entity.类型编码" editable="false" required="true"
                            style="width: 150px;"/>
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        IP地址：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="StationIP" type="text" class="easyui-validatebox" 
                            name="entity.IP地址" validtype="length[0,50]" style="border: 1px solid #8DB2E3; width: 144px;" />
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        电话号码：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="StationTel" type="text" class="easyui-validatebox"
                            name="entity.电话号码" validtype="length[0,14]" style="border: 1px solid #8DB2E3; width: 144px;" />
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        通信标识：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="StationComm" type="text" class="easyui-validatebox"
                            name="entity.通信标识" validtype="length[0,50]" style="border: 1px solid #8DB2E3; width: 144px;" />
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        是否调度(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <select id="StationDispatch" class="easyui-combobox" name="entity.是否调度" editable="false" required="true" style="width: 150px;">
                            <option value=true>是</option>
                            <option value=false>否</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        所属区域：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="StationArea" type="text" class="easyui-combobox" editable="false"
                            name="entity.所属区域"   style="border: 1px solid #8DB2E3; width: 150px;" />
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        是否传送GPS(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <select id="StationGPS" class="easyui-combobox" name="entity.是否传送GPS" editable="false" required="true" style="width: 150px;">
                            <option value=true>是</option>
                            <option value=false>否</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        是否标注(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <select id="StationFlag" class="easyui-combobox" name="entity.是否标注" editable="false" required="true" style="width: 150px;">
                            <option value=true>是</option>
                            <option value=false>否</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        X坐标(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="StationX" type="text" class="easyui-validatebox" editable="false"  required="true"
                            name="entity.X坐标"  
                            style="border: 1px solid #8DB2E3; width: 144px;" />
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        Y坐标(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="StationY" type="text" class="easyui-validatebox" editable="false"  required="true"
                            name="entity.Y坐标"  
                            style="border: 1px solid #8DB2E3; width: 144px;" />
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        所属调度中心(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <select id="StationBelong" class="easyui-combobox" name="entity.中心编码" editable="false" required="true"
                            style="width: 150px;">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%; text-align: right">
                        负责人(<font color="red">*</font>)：
                    </td>
                    <td style="width: 60%; text-align: left">
                        <input id="StationManager" type="text"  class="easyui-combobox"  name="ManagerID"   required="true"  editable="false" style="width: 150px;"/>
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        顺序号(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <input id="StationSequence" type="text" class="easyui-validatebox" editable="false"  required="true"
                            name="entity.顺序号" validtype="length[1,11]" onkeyup="this.value=this.value.replace(/[^\d]/g,'')"
                            style="border: 1px solid #8DB2E3; width: 144px;"/>
                    </td>
                </tr>
                <tr>
                    <td style="width:40%; text-align: right">
                        启用(<font color="red">*</font>)：
                    </td>
                    <td style="width:60%; text-align: left">
                        <select id="StationActive" class="easyui-combobox" name="entity.是否有效" editable="false" required="true" style="width: 150px;">
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

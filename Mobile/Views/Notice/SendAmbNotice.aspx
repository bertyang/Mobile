<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>SentMessage</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript" language="javascript">

        function submit() {
            $('#form').form('submit', {
                onSubmit: function () {
                    var rows = $('#grid').datagrid('getSelections');
                    if (rows && rows.length != 0) {
                        var code = [];
                        for (var i = 0; i < rows.length; i++) {
                            if (rows[i].车辆编码 != "") {
                                code.push(rows[i].车辆编码);
                            }
                        }
                        $("#code").val(code.join(','));
                    }

                    //验证是否选择了发送对象
                    if ($("#code").val() == "") {
                        $.messager.alert('提示', "请选择发送对象！");
                        return false;
                    }
                    if ($(this).form('validate')) {
                        DisableButton();
                        return true;
                    }
                    else {
                        return false;
                    }
                },
                success: function (msg) {
                    var data = eval('(' + msg + ')');
                    if (data.IsSuccess) {
                        $.messager.alert('提示', data.Message, 'info', function () {
                            window.location.href = this.location.reload();
                        });
                    }
                    else {
                        $.messager.alert('提示', data.Message, 'info', function () {
                        });
                    }
                    UnDisableButton();
                }
            });
        }

        $(document).ready(function () {
            $('#btnSelectAll').bind('click',
                    function () {
                        $('#grid').datagrid('selectAll');
                    }
                );
           
           //分站
           $('#station').combobox({
               url: '/BasicInfo/LoadAllStations/?ActionId=<%=Request.QueryString["ActionId"]%>',
                valueField: '编码',
                textField: '名称',
                onLoadSuccess: function (data) {
                    $('#station').combobox('setValue', '--请选择--');
                },
                onChange: function (node) {
                    $('#grid').datagrid('options').url = '/Notice/AmbulanceLoad/?ActionId=<%=Request.QueryString["ActionId"]%>&stationId=' + escape(node);
                    $('#grid').datagrid("reload");
                }
            });

        });

        function DisableButton() {
            $('#send').attr("disabled", "true");
            $('#cancel').attr("disabled", "true");
            document.body.style.cursor = "progress";
        }

        function UnDisableButton() {
            document.body.style.cursor = "default";
            $('#send').removeAttr("disabled");
            $('#cancel').removeAttr("disabled");
        }
        
    </script>
</head>
<body class="easyui-layout">
    <div region="center" border="false">
        <div region="north" style="margin-left: 40px; margin-top: 20px; height: 40px">
            <span class="editTitle">车辆通知</span>
        </div>
        <div region="center">
            <form id="form" method="post" runat="server" action="/Notice/SendAmb/" enctype="application/x-www-form-urlencoded">
            <table width="100%">
                <tr>
                    <td style="width: 5%; text-align: right">
                        按分站：
                    </td>
                    <td style="width: 20%; text-align: left">
                        <select id="station" class="easyui-combobox" style="width: 90px">
                        </select>
                    </td>
                    <td style="text-align: right; width: 120px;">
                        
                    </td>
                    <td>
                       
                    </td>
                </tr>
                <tr style="height: 340px;">
                    <td colspan="2" align="right">
                        <table id="grid" class="easyui-datagrid" url=""
                           idfield="车辆编码" rownumbers="true" sortname="实际标识" sortorder="asc" fit="true" fitcolumns="true">
                            <thead frozen="true">
                                <tr>
                                    <th field="车辆编码" width="30px" checkbox="true" align='center'>
                                        车辆编码
                                    </th>
                                </tr>
                            </thead>
                            <thead>
                                <tr>
                                    <th field="实际标识" width="90px" align='center'>
                                        实际标识
                                    </th>
                                </tr>
                            </thead>
                        </table>
                    </td>
                    <td style="text-align: right; width: 120px;">
                        发送内容(<font color="red">*</font>)：
                    </td>
                    <td>
                        <textarea id="editor" class="easyui-validatebox" required="true" name="content" class="ckeditor"
                            style="border: 1px solid #8DB2E3; width: 60%; height: 340px; overflow-x: hidden;
                            overflow-y: hidden"></textarea>
                    </td>
                </tr>

                <tr style="display: none">
                    <td colspan="4">
                        <input id="code" type="text" name="code"/>
                    </td>
                </tr>
                <tr>
                    <td align=right>
                        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" id="btnSelectAll">全选</a>
                    </td>
                    <td colspan=3>
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    <div region="south" border="true" style="text-align: right; height: 40px; line-height: 30px;
        background-color: #f7f7f7;">
        <table style="width: 100%">
            <tr>
                <td>
                </td>
                <td style="text-align: LEFT">
                    <a href="#" id="send" class="easyui-linkbutton" iconcls="icon-save" onclick="submit();">发送</a>
                    <a href="#" id="cancel" class="easyui-linkbutton" iconcls="icon-cancel" onclick="window.location.href ='/Notice/SendAmbNotice/';">
                        取消</a>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>

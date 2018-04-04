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
                        var ip = [];
                        for (var i = 0; i < rows.length; i++) {
                            if (rows[i].IP地址 != "") {
                                ip.push(rows[i].IP地址);
                            }
                        }
                        $("#ip").val(ip.join(','));
                    }

                    //验证是否选择了发送对象
                    if ($("#ip").val() == "") {
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
            <span class="editTitle">分站通知</span>
        </div>
        <div region="center">
            <form id="form" method="post" runat="server" action="/Notice/SendStation/" enctype="application/x-www-form-urlencoded">
            <table width="100%" >
                <tr>
                    <td style="width: 5%; text-align: right">
                        &nbsp
                    </td>
                    <td style="width: 20%; text-align: left">
                       &nbsp
                    </td>
                    <td style="text-align: right; width: 120px;">
                        &nbsp
                    </td>
                    <td>
                       &nbsp
                    </td>
                </tr>
                <tr style="height: 340px;">
                    <td colspan="2" align="right">
                        <table id="grid" class="easyui-datagrid" url="/BasicInfo/LoadAllStations/?ActionId=<%=Request.QueryString["ActionId"]%>"
                           idfield="编码" rownumbers="true" sortorder="asc" fit="true" fitcolumns="true">
                            <thead frozen="true">
                                <tr>
                                    <th field="编码" width="30px" checkbox="true" align='center'>
                                        编码
                                    </th>
                                </tr>
                            </thead>
                            <thead>
                                <tr>
                                    <th field="名称" width="90px" align='center'>
                                        名称
                                    </th>
                                    <th field="IP地址" width="90px" align='center'  hidden='true'>
                                        IP地址
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
                        <input id="ip" type="text" name="ip"/>
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
                    <a href="#" id="cancel" class="easyui-linkbutton" iconcls="icon-cancel" onclick="window.location.href ='/Notice/SendStationNotice/';">
                        取消</a>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>

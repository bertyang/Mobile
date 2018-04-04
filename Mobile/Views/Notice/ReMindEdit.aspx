<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>ReMindEdit</title>
   <%-- <meta name="viewport" content="width=device-width" />--%>
   <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script language="javascript" type="text/javascript">

        function submit() {
            $('#form').form('submit', {
                onSubmit: function () {
                    var rows = $('#grid').datagrid('getSelections');
                    if (rows && rows.length != 0) {
                        var list = [];
                        for (var i = 0; i < rows.length; i++) {
                            if (rows[i].Tel1 != "") {
                                list.push(rows[i].Tel1);
                            }
                            if (rows[i].Tel2 != "") {
                                list.push(rows[i].Tel2);
                            }
                        }
                        if ($("#tel").val() != "") {
                            list.push($("#tel").val());
                        }
                        list.join(',');
                        $("#telList").val(list);
                    }
                    else if ($("#tel").val() != "") {
                        $("#telList").val($("#tel").val());
                    }

                    //验证是否选择了发送对象
                    if ($("#telList").val() == "") {
                        $.messager.alert('提示', "请选择发送对象！");
                        return false;
                    }
                    return $(this).form('validate')
                },
                success: function (msg) {
                    var data = eval('(' + msg + ')'); 
                    if(data.IsSuccess)
                    {
                        $.messager.alert('提示', data.Message, 'info', function(){
                            window.location.href = "../Remind/";
                        });
                    }
                    else
                    {
                        $.messager.alert('提示', data.Message, 'info', function () {
                            UnDisableButton();
                        });
                    }
                },
                error: function () {
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
            //电话分类
            $('#Type').combotree({
                url: '/BasicInfo/MessTypeTreeLoad/?OwnerID='+ <%=ViewData["OwnerID"] %>,
                onLoadSuccess: function (data) {
                    $('#Type').combotree('setValue', '--请选择--');
                },
                onChange: function (node) {
                    if ( node != '--请选择--')
                    {
                        $('#grid').datagrid('options').url = '/BasicInfo/TelBookLoad/?Type=' + escape(node);
                        $('#grid').datagrid("reload");
                    }
                }
            });
            //角色
            $('#role').combobox({
                url: '/Organize/RoleLoadAll/',
                valueField: 'ID',
                textField: 'Name',
                onLoadSuccess: function (data) {
                    $('#role').combobox('setValue', "--请选择--");
                }
            });

            //查询
            $('#btnSearch').bind('click',
                    function () {
                        $('#worker').datagrid('options').url = '/Organize/SmsWorkerLoad/?name=' + escape($('#txtName').val()) + '&orgId=' + '<%=ViewData["orgId"] %>' + '&roleId=' + $('#role').combobox('getValue');
                        $('#worker').datagrid("reload");
                    }
               );

            //验证电话号码
            $.extend($.fn.validatebox.defaults.rules, {
                        phone: {
                            validator: function (value) {
                                var reg = /^1[3|4|5|8|9]\d{9}$/;

                                var t = [];
                                t = value.split(",");
                                for (i = 0; i < t.length; i++) {
                                    if (!reg.test(t[i])) { return false; }
                                }
                                return true;
                            },
                            message: '请检查输入的电话号码是否正确！'
                        }
             });
        });
        
    </script>
</head>
<body class="easyui-layout">
    <div region="center" border="false">
        <div region="north" style="margin-left: 40px; margin-top: 20px; height: 40px">
            <span class="editTitle">定时短信</span>
        </div>
        <div region="center">
            <form id="form" runat="server" method="post" action="/Notice/RemindSave/" enctype="application/x-www-form-urlencoded">
            <table width="100%" >
                <tr style="display:none;">
                    <td>
                        <input type="text" name="entity.编码" value="<%= ((dynamic)this.ViewData["entity"]).编码%>" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%; text-align: right">
                        电话分类：
                    </td>
                    <td style="width: 40%; text-align: left">
                        <select id="Type" class="easyui-combotree" multiple="true" style="width: 150px;">
                        </select>
                    </td>
                    <td style="text-align: right; width: 150px;">
                        电话号码：
                    </td>
                    <td>
                        <input id="tel" type="text" name="tel" validtype="phone" class="easyui-validatebox"
                            value="<%= ((dynamic)this.ViewData["entity"]).发送对象%>" style="border: 1px solid #8DB2E3;
                            width: 160px; height: 18px" />（多个用,隔开)
                    </td>
                </tr>
                <tr style="height: 30px;">
                    <td colspan="2" rowspan="2" align="right">
                        <table id="grid" class="easyui-datagrid" align="center" url=""
                            idfield="ID"  rownumbers="true" sortname="ID" sortorder="asc" fit="true" fitcolumns="true">
                            <thead frozen="true">
                                <tr>
                                    <th field="ID" width="40px" checkbox="true" align='center'>
                                        编码
                                    </th>
                                </tr>
                            </thead>
                            <thead>
                                <tr>
                                    <th field="Name" width="70px" align='center'>
                                        名称
                                    </th>
                                    <th field="Tel1" width="80px" align='center'>
                                        联系电话一
                                    </th>
                                    <th field="Tel2" width="80px" align='center'>
                                        联系电话二
                                    </th>
                                    <th field="Remark" width="80px" align='center'>
                                        备注
                                    </th>
                                </tr>
                            </thead>
                        </table>
                    </td>
                    <td style="text-align: right; width: 150px;">
                        提醒时间(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" class="easyui-datetimebox" name="entity.提醒时间" style="border: 1px solid #8DB2E3;
                            width: 164px;" value="<%= (dynamic)this.ViewData["time"]%>" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 150px;">
                        短信内容(<font color="red">*</font>)：
                    </td>
                    <td>
                        <textarea id="editor" class="easyui-validatebox" required="true" name="entity.内容"
                            class="ckeditor" style="border: 1px solid #8DB2E3; width: 93%; height: 320px;
                            overflow-x: hidden; overflow-y: hidden"><%= ((dynamic)this.ViewData["entity"]).内容%></textarea>
                    </td>
                </tr>
                <tr>
                    
                </tr>
                <tr style="display: none">
                    <td colspan="4">
                        <input id="telList" type="text" name="telList" class="easyui-validatebox" />
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
                <td style="text-align:left">
                    <a href="#" id="send" class="easyui-linkbutton" iconcls="icon-save" onclick="submit();">保存</a>
                    <a href="#" id="cancel" class="easyui-linkbutton" iconcls="icon-cancel" onclick="location.href = document.referrer;">取消</a>

                </td>
            </tr>
        </table>
    </div>
   
</body>
</html>

<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <%: Scripts.Render("~/ckeditor/js")%>
    <script type="text/javascript" language="javascript">

        $(function () {
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
                        $('#worker').datagrid('options').url = '/Organize/WorkerLoad/?Name=' + escape($('#txtName').val())
                            + '&orgId=' + $('#orgId').val()
                            + '&roleId=' + $("#role").combobox('getValue');
                        $('#worker').datagrid("reload");
                    }
            );
        })

        //发送
        function submit() {
            $('#form').form('submit', {
                url: '/Email/SendMail/?isSend=1',
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (msg) {
                    var data = eval('(' + msg + ')');
                    if (data.IsSuccess) {
                        $.messager.alert('提示', data.Message, 'info', function () {
                            window.location = '/Email/Inbox/';
                        });
                    }
                    else {
                        $.messager.alert('提示', data.Message, 'info', function () {
                        });
                    }
                }
            });
        }

        //存草稿
        function save() {
            $('#form').form('submit', {
                url: '/Email/SendMail/?isSend=0',
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (msg) {
                    var data = eval('(' + msg + ')');
                    if (data.IsSuccess) {
                        $.messager.alert('提示', data.Message, 'info', function () {
                            window.location = '/Email/Inbox/';
                        });
                    }
                    else {
                        $.messager.alert('提示', data.Message, 'info', function () {
                        });
                    }
                }
            });
        }

        //附件上传
        function up() {
            $('#Attachment').show();
            $('#Attachment').dialog({
                collapsible: true,
                minimizable: true,
                maximizable: true,
                height: 220,
                width: 350,
                modal: true,
                title: '附件上传',
                onClose: function () {
                    //清空上传文件框
                    var file = document.getElementById("upfile");
                    if (file.outerHTML) {
                        file.outerHTML = file.outerHTML;
                    } else {
                        file.value = "";
                    }
                },
                buttons: [
                    {
                        text: '上传',
                        iconCls: 'icon-ok',
                        handler: function () {
                            $('#up').form('submit', {
                                onSubmit: function () {
                                    //检查文件
                                    var filename = document.getElementById("upfile").value;
                                    if (filename == "") {
                                        $.messager.alert('提示', '请上传文件！', 'info');
                                        return false;
                                    }
//                                    else {
//                                        var fso = new ActiveXObject("Scripting.FileSystemObject");
//                                        var file = fso.getfile(filename);
//                                        var fileSize = file.size / 1024; //文件大小转换为kb
//                                        if (parseFloat(fileSize) > 51200) {
//                                            $.messager.alert('提示', '文件大于50M，不能上传！', 'info');
//                                            return false;
//                                        }
//                                    }
                                    return $(this).form('validate');
                                },
                                success: function (msg) {
                                    var data = eval('(' + msg + ')');
                                    if (data.IsSuccess) {
                                        $.messager.alert('提示', data.Message, 'info', function () {
                                            $('#grid').datagrid("reload");
                                            $('#Attachment').dialog('close');

                                            //清空上传文件框
                                            var file = document.getElementById("upfile");
                                            if (file.outerHTML) {
                                                file.outerHTML = file.outerHTML;
                                            } else {
                                                file.value = "";
                                            }
                                        });
                                    }
                                    else {
                                        $.messager.alert('提示', data.Message, 'info', function () {
                                        });
                                    }
                                },
                                error: function () {
                                    $.messager.alert('错误', '请检查错误！', "error");
                                }
                            });
                        }
                    },
                    {
                        text: '关闭',
                        iconCls: 'icon-cancel',
                        handler: function () {
                            $('#Attachment').dialog('close');

                            //清空上传文件框
                            var file = document.getElementById("upfile");
                            if (file.outerHTML) {
                                file.outerHTML = file.outerHTML;
                            } else {
                                file.value = "";
                            }
                        }
                    }]
            });
        }

        //格式化“删除”
        function formatDelete(val, rec) {
            return "<a href='#' onclick='fileDelete(" + rec.编码 + ")'><img src='../../Content/images/delete.gif' border='0'/></a>";
        }

        //附件删除
        function fileDelete(id) {
            $.ajax({
                type: "POST",
                url: "/Email/FileDelete/?id=" + id,
                success: function (msg) {
                    var data = eval('(' + msg + ')');
                    if (data.IsSuccess) {
                        $.messager.alert('提示', '删除成功！', "info", function () {
                            $('#grid').datagrid("reload");
                        });
                    }
                },
                error: function () {
                    $.messager.alert('错误', '请检查错误！', "error");
                }
            });
        }

        //返回
        function getBackURL() {
            var vurl = '/Email/Drafts/?pageNumber=' + '<%=(dynamic)this.ViewData["pageNumber"] == null?"":(dynamic)this.ViewData["pageNumber"]%>';
            return vurl;
        }

        //选择人员
        function selectWorkers(type) {
            $('#worker').datagrid({
                url: '/Organize/WorkerLoad/?orgId=' + '<%=ViewData["orgId"] %>'
            });

            //弹出选择框
            $('#WorkerDiv').show();
            $('#WorkerDiv').dialog({
                collapsible: true,
                minimizable: true,
                maximizable: true,
                height: 500,
                width: 700,
                modal: true, //阴影（弹出会影响页面大小）
                title: '选择人员',
                buttons: [{
                    text: '确定',
                    iconCls: 'icon-ok',
                    handler: function () {

                        var rows = $('#worker').datagrid('getSelections');

                        if (rows) {
                            var workersID = [];
                            var workersName = [];
                            for (var i = 0; i < rows.length; i++) {
                                workersID.push(rows[i].ID);
                                workersName.push(rows[i].Name);
                            }
                            workersID.join(',');
                            workersName.join(',');

                            if (type == 1) {  //收件人
                                $("#toID").val(workersID);
                                $("#toName").val(workersName);
                            }
                            if (type == 2) {  //抄送人
                                $("#ccID").val(workersID);
                                $("#ccName").val(workersName);
                            }
                            //                            if (type == 3) {  //密送人
                            //                                $("#scID").val(workersID);
                            //                                $("#scName").val(workersName);
                            //                            }

                        }
                        $('#WorkerDiv').dialog('close');
                        $('#worker').datagrid('clearSelections');
                        $('#txtName').val("");
                        $('#role').combobox('setValue', "--请选择--");
                    }
                },
                    {
                        text: '取消',
                        iconCls: 'icon-cancel',
                        handler: function () {
                            $('#WorkerDiv').dialog('close');
                            $('#worker').datagrid('clearSelections');
                            $('#txtName').val("");
                            $('#role').combobox('setValue', "--请选择--");
                        }
                    }]
            });

            //组织机构树
            $(function () {
                $('#tree').tree({
                    url: '/Organize/UnitTree/',

                    onClick: function (node) {

                        var urlw = "/Organize/WorkerLoad/?orgId=" + node.id;
                        $('#worker').datagrid({
                            url: urlw
                        });
                        $('#orgId').val(node.id);
                    }
                });
            });
        }
    </script>
</head>
<body class="easyui-layout">
    <div region="center" border="true">
        <form id="form" method="post" enctype="application/x-www-form-urlencoded">
        <table width="100%">
            <tr>
                <td align="left" width="3%">
                    &nbsp;
                </td>
                <td align="left" width="8%">
                </td>
                <td align="left" width="57%">
                </td>
                <td align="left" valign="middle" width="30%">
                </td>
            </tr>
            <tr style="display: none">
                <td>
                    <input type="text" name="entity.ID" value="<%= ((dynamic)this.ViewData["MailID"])%>" />
                    <input type="text" name="WriterID" value="<%= ((dynamic)this.ViewData["WriterID"])%>" />
                    <input type="text" name="entity.CreateTime" value="<%= ((dynamic)this.ViewData["CreateTime"])%>" />
                </td>
            </tr>
            <tr>
                <td align="left" width="3%">
                </td>
                <td align="left" style="color: #FF0000">
                    发件人：
                </td>
                <td align="left">
                    <input type="text" class="easyui-validatebox" required="true" name="entity.From"
                        value="<%= ((dynamic)this.ViewData["WriterName"])%>" style="border: 0; background: transparent;
                        width: 50%; height: 18px" />
                </td>
                <td align="left" valign="middle">
                    <a href="#" class="easyui-linkbutton" onclick="up();">添加附件</a>
                </td>
            </tr>
            <tr>
                <td align="left" width="3%">
                    &nbsp;
                </td>
                <td align="left" style="color: #FF0000">
                    收件人：
                </td>
                <td align="left" width="60%">
                    <input id="toID" type="text" class="easyui-validatebox" name="toID" value="<%=  ((dynamic)this.ViewData["toID"]) == null ?"":((dynamic)this.ViewData["toID"])%>"
                        style="display: none" />
                    <input id="toName" type="text" name="entity.To" class="easyui-validatebox" validtype="length[1,255]"
                        required="true" readonly="true" value="<%=  ((dynamic)this.ViewData["to"]) == null ?"":((dynamic)this.ViewData["to"]) %>" style="border: 1px solid #8DB2E3;
                        width: 50%; height: 18px" />
                    <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="selectWorkers(1);">
                        选择</a>
                </td>
                <td align="left" valign="top" width="22%" rowspan="4">
                    <table id="grid" class="easyui-datagrid" url="/Email/GetFile/?mailID=<%=ViewData["MailID"]%>"
                        idfield="编码" border="0" sortname="Type" sortorder="asc" remotesort="false" fit="true"
                        fitcolumns="true" rownumbers="true">
                        <thead>
                            <tr>
                                <th field="原附件名" width="400" align='center'>
                                    附件名
                                </th>
                                <th field="文件大小" width="400" align='center'>
                                    大小
                                </th>
                                <th field="编码" width="200" align='center' formatter="formatDelete">
                                    删除
                                </th>
                            </tr>
                        </thead>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" width="3%">
                    &nbsp;
                </td>
                <td align="left">
                    抄送人：
                </td>
                <td align="left">
                    <input id="ccID" type="text" class="easyui-validatebox" name="ccID" value="<%=  ((dynamic)this.ViewData["ccID"]) == null ?"":((dynamic)this.ViewData["ccID"])%>"
                        style="display: none" />
                    <input id="ccName" type="text" name="entity.CC" class="easyui-validatebox" validtype="length[1,255]"
                        readonly="true" value="<%=  ((dynamic)this.ViewData["CC"]) == null ?"":((dynamic)this.ViewData["CC"]) %>" style="border: 1px solid #8DB2E3;
                        width: 50%; height: 18px" />
                    <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="selectWorkers(2);">
                        选择</a>
                </td>
            </tr>
            <%--            <tr>
                <td align="left" width="3%">
                    &nbsp;
                </td>
                <td align="left">
                    密送人：
                </td>
                <td align="left">
                    <input id="scID" type="text" class="easyui-validatebox" name="CcID" style="display: none" />
                    <input id="scName" type="text" name="entity.SC" class="easyui-validatebox" validtype="length[1,255]"
                        required="true" readonly="true" value="<%= (dynamic)this.ViewData["SC"] %>" style="border: 1px solid #8DB2E3;
                        width: 50%; height: 18px" />
                    <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="selectWorkers(3);">选择</a>
                </td>
            </tr>--%>
            <tr>
                <td align="left" width="3%">
                    &nbsp;
                </td>
                <td align="left" style="color: #FF0000">
                    主题：
                </td>
                <td align="left">
                    <input type="text" class="easyui-validatebox" required="true" name="entity.Title"
                        value="<%= ((dynamic)this.ViewData["Title"])%>" style="border: 1px solid #8DB2E3;
                        width: 50%; height: 18px" />
                </td>
            </tr>
            <tr>
                <td align="left" width="3%">
                    &nbsp;
                </td>
                <td align="left" valign="middle">
                    正文：
                </td>
                <td align="left">
                    <textarea id="editor" name="editor" class="ckeditor" cols="80" rows="20"><%=ViewData["Body"]%></textarea>
                </td>
            </tr>
            <tr>
                <td align="left" width="3%">
                </td>
                <td align="left">
                    提醒：
                </td>
                <td align="left">
                    <input type="checkbox" id="remind" name="remind" />使用内部短信提醒
                </td>
            </tr>
        </table>
        </form>
    </div>
    <div region="south" border="true" style="text-align: right; height: 40px; line-height: 30px;
        background-color: #f7f7f7;">
        <table style="width: 100%">
            <tr>
                <td>
                </td>
                <td style="text-align: LEFT">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="submit();">发送</a>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-save" onclick="save();">存草稿</a>
                    <%if (ViewData["To"] == "")
                      {%>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="window.location.href = '../Email/Write'">
                        取消</a>
                    <% } %>
                    <%if (ViewData["To"] != "")
                      {%>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-undo" onclick="location.href = getBackURL();">
                        返回</a>
                    <% } %>
                </td>
            </tr>
        </table>
    </div>
    <!--上传附件窗口-->
    <div id="Attachment" icon="icon-save" style="padding: 5px; display: none;">
        <form id="up" action="/Email/FileUpload/" method="post" enctype="multipart/form-data">
        <input type="hidden" id="mailID" name="mailID" class="easyui-validatebox" value="<%= ((dynamic)this.ViewData["MailID"])%>" />
        <input type="file" id="upfile" name="upfile" style="border: 1px solid #8DB2E3; width: 200px;
            height: 25px" />
        </form>
    </div>
    <!--人员选择-->
    <div id="WorkerDiv" icon="icon-save" style="overflow: hidden; padding: 5px; display: none">
        <div class="easyui-layout" icon="icon-save" style="padding: 5px; width: 100%; height: 100%;">
            <div region="west" split="true" title="组织机构" iconcls="icon icon-sys" style="width: 190px;"
                id="west">
                <ul id="tree" line="true">
                </ul>
            </div>
            <div region="center">
                <table id="worker" class="easyui-datagrid" align="center" title="人员" pagination="true"
                    pagenumber="1" pagelist="[10, 15, 20]" pagesize="15" idfield="ID" nowrap="false"
                    striped="true" rownumbers="true" sortname="ID" sortorder="asc" toolbar="#tb"
                    remotesort="false" width="300" fit="true" fitcolumns="true">
                    <thead>
                        <tr>
                            <th field="ID" width="60" checkbox="true" align='center'>
                                Item ID
                            </th>
                            <th field="Name" width="80" align='center'>
                                姓名
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="tb" style="padding: 5px; height: auto">
                <table  width="100%">
                    <tr>
                        <td  width="40%">
                            姓名:
                            <input id="txtName" class="easyui-validatebox" style="border: 1px solid #8DB2E3;
                                width: 81px; height: 18px" />
                        </td>
                        <td  width="40%">
                            角色:
                            <select id="role"  class="easyui-combobox" name="role" style="width:100px;">
                            </select>
                        </td>
                        <td  width="20%">
                            <input type=hidden id="orgId"/>
                            <a href="#" id="btnSearch" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</body>
</html>

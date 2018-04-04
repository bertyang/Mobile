<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>分类管理</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript" language="javascript">
        $(function () {
            //上级分类
            $('#Parent').combotree({
                url: '/BasicInfo/TypeTreeLoad/?OwnerID=<%=ViewData["OwnerID"] %>'
                });

            //添加
            $('#btnAdd').bind('click',
                    function () {
                        $('#Edit').show();
                        $('#Edit').dialog({
                             collapsible: true,
                             minimizable: true,
                             maximizable: true,
                             height: 250,
                             width: 350,
                             modal: true,
                             title: '新增电话分类',
                             buttons: [
                                    {
                                        text: '确定',
                                        iconCls: 'icon-save',
                                        handler: function () {
                                            $('#form').form('submit', {
                                                onSubmit: function () {
                                                    if ($('#Parent').combotree('getValue') == "") {
                                                        $('#Parent').combotree('setValue',0);
                                                    }
                                                    return $(this).form('validate');
                                                },
                                                success: function (msg) {
                                                    var data = eval('(' + msg + ')');
                                                    if (data.IsSuccess) {
                                                        $.messager.alert('提示', data.Message, 'info', function () {
                                                            $('#Edit').dialog('close');
                                                            $('#grid').datagrid('reload');
                                                            $('#Parent').combotree({
                                                                url: '/BasicInfo/TypeTreeLoad/?OwnerID=<%=ViewData["OwnerID"] %>'
                                                            });
                                                        });
                                                    }
                                                    else {
                                                        $.messager.alert('提示', data.Message, 'info', function () {
                                                        });
                                                    }
                                                }
                                            });                 
                                        }
                                    },
                                    {
                                        text: '取消',
                                        iconCls: 'icon-cancel',
                                        handler: function () {
                                            $('#Edit').dialog('close');
                                        }
                                    }]
                         });
                         
                         $('#ID').val("0");
                         $('#Name').val(""); 
                         $('#Parent').combotree('setValue','');
                         $('#Order').val("1");
                         $('#Owner').val(<%=ViewData["OwnerID"] %>);
                    }
                );

            //修改
            $('#btnEdit').bind('click',
                    function () {
                        
                        var row = $('#grid').datagrid('getSelected');
                        if (row) {

                            $('#Parent').combotree({
                                url: '/BasicInfo/TypeTreeLoad/?OwnerID=<%=ViewData["OwnerID"] %>'+'&exceptUnitId='+row.编码
                            });

                            $('#Edit').show();
                            $('#Edit').dialog({
                                 collapsible: true,
                                 minimizable: true,
                                 maximizable: true,
                                 height: 250,
                                 width: 350,
                                 modal: true,
                                 title: '修改电话分类',
                                 buttons: [
                                    {
                                        text: '确定',
                                        iconCls: 'icon-save',
                                        handler: function () {
                                            $('#form').form('submit', {
                                                onSubmit: function () {
                                                    if ($('#Parent').combotree('getValue') == "") {
                                                        $('#Parent').combotree('setValue',0);
                                                    }
                                                    return $(this).form('validate');
                                                },
                                                success: function (msg) {
                                                    var data = eval('(' + msg + ')');
                                                    if (data.IsSuccess) {
                                                        $.messager.alert('提示', data.Message, 'info', function () {
                                                            $('#Edit').dialog('close');
                                                            $('#grid').datagrid('reload');
                                                            $('#Parent').combotree({
                                                                url: '/BasicInfo/TypeTreeLoad/?OwnerID=<%=ViewData["OwnerID"] %>'
                                                            });
                                                        });
                                                    }
                                                    else {
                                                        $.messager.alert('提示', data.Message, 'info', function () {
                                                        });
                                                    }
                                                }
                                            });                 
                                        }
                                    },
                                    {
                                        text: '取消',
                                        iconCls: 'icon-cancel',
                                        handler: function () {
                                            $('#Edit').dialog('close');
                                        }
                                    }]
                            });
                         
                             $('#ID').val(row.编码);
                             $('#Name').val(row.名称);
                             if (row.上级分类 != '' && row.上级分类 != null) {
                                 $('#Parent').combotree('setValue', row.上级ID);
                             }else {
                                $('#Parent').combotree('setValue','');
                             }
                             $('#Order').val(row.顺序);
                             if (row.是否有效 == "是") { document.getElementsByName('entity.是否有效')[0].checked = true; }
                             if (row.是否有效 == "否") { document.getElementsByName('entity.是否有效')[1].checked = true; }

                             $('#Owner').val(<%=ViewData["OwnerID"] %>);
                         }
                         else {
                            $.messager.alert('提示', '请选择要修改的数据');
                            return;
                        }
                    }
                );

            //删除
            $('#btnDel').bind('click',
                    function () {
                        var row = $('#grid').datagrid('getSelected');
                        if (!row) {
                            $.messager.alert('提示', '请选择要删除的数据');
                            return;
                        }
                       
                        if (row.是否包含联系人 == "是") {
                                $.messager.alert('提示', row.名称 + '分类下有联系人存在，不能删除！');
                                return false;
                        }

                        $.messager.confirm('提示', '是否删除这些数据?', function (r) {
                                if (!r) {
                                    return;
                                }

                                $.ajax({
                                    type: "POST",
                                    url: "/BasicInfo/TelTypeDelete/?id=" + row.编码,
                                    success: function (msg) {
                                        if (msg.IsSuccess) {
                                            $.messager.alert('提示', '删除成功！', "info", function () {
                                                $('#grid').datagrid("reload");
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
    </script>
</head>
<body class="easyui-layout">
    <div region="center" style="padding: 5px" border="true">
        <table id="grid" class="easyui-datagrid" align="center" toolbar="#tb" url="/BasicInfo/LoadTelType/?OwnerID=<%=ViewData["OwnerID"] %>"
            pagination="true" pagenumber="1" pagelist="[10, 15, 20]" pagesize="10" idfield="编码"
            fitcolumns="true" nowrap="false" striped="true" rownumbers="true" sortname="编码" singleSelect="true"
            sortorder="asc" remotesort="false" width="1000px" fit="true">
            <thead frozen="true">
                <tr>
                    <th field="ck" width="100px" checkbox="true" align='center'>
                    </th>
                </tr>
            </thead>
            <thead>
                <tr>
                    <th field="编码" width="100px" align='center'>
                        编码
                    </th>
                    <th field="名称" width="100px" align='center'>
                        名称
                    </th>
                    <th field="上级ID" width="100px" align='center' hidden='true'>
                        上级ID
                    </th>
                    <th field="上级分类" width="100px" align='center'>
                        上级分类
                    </th>
                    <th field="顺序" width="100px" align='center'>
                        顺序
                    </th>
                    <th field="是否有效" width="100px" align='center'>
                        是否有效
                    </th>
                    <th field="是否包含联系人" width="100px" align='center' hidden='true'>
                        是否包含联系人
                    </th>
                </tr>
            </thead>
        </table>
    </div>
    <div id="tb" style="padding: 5px; height: auto">
        <div style="margin-bottom: 5px">
            <a href="#" id="btnAdd" class="easyui-linkbutton" iconcls="icon-add" plain="true">添加</a>
            <a href="#" id="btnEdit" class="easyui-linkbutton" iconcls="icon-edit" plain="true">修改</a> 
            <a href="#" id="btnDel" class="easyui-linkbutton" iconcls="icon-remove" plain="true">删除</a>
        </div>
    </div>
    <div id="Edit" icon="icon-save" style="padding: 5px; display: none;">
        <form id="form" method="post" action="/BasicInfo/TelTypeSave/" enctype="application/x-www-form-urlencoded">
        <table style="margin-top:15px;">
            <tr style="display: none">
                <td>
                    <input id="ID"  name="entity.编码" />
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right">
                    分类名称(<font color="red">*</font>)：
                </td>
                <td style="width: 15%; text-align: left">
                    <input id="Name" type="text" class="easyui-validatebox" name="entity.名称" required="true"
                        validtype="length[1,50]" style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right">
                    上级分类：
                </td>
                <td>
                    <select id="Parent" style="width: 150px;" name="entity.上级编码">
                    </select>
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right">
                    显示顺序(<font color="red">*</font>)：
                </td>
                <td style="width: 15%; text-align: left">
                    <input id="Order" type="text" class="easyui-validatebox" name="entity.顺序号" required="true"
                        validtype="length[0,11]" onkeyup="this.value=this.value.replace(/[^\d]/g,'')"
                        style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">
                    是否有效(<font color="red">*</font>)：
                </td>
                <td>
                    <input type="radio" name="entity.是否有效" value="true" checked />是&nbsp;
                    <input type="radio" name="entity.是否有效" value="false" />否&nbsp;
                </td>
            </tr>
            <tr style="display:none;">
                <td style="width: 10%; text-align: right">
                    归属人ID(<font color="red">*</font>)：
                </td>
                <td style="width: 15%; text-align: left">
                    <input id="Owner" type="text" class="easyui-validatebox" name="entity.归属人ID"
                        style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                </td>
            </tr>
        </table>
        </form>
    </div>
</body>
</html>

<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>发布公告</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>

    <script type="text/javascript" language="javascript">

        //页面初始化
        $(document).ready(function () {
            $('#grid').datagrid({
                url: '/Office/OfficeSearch/?pageNumber=<%=ViewData["pageNumber"]%>' + '&type=' + '<%=ViewData["type"] %>'
                     + '&startTime=' + '<%=ViewData["startTime"] %>' + '&endTime=' + '<%=ViewData["endTime"] %>'
                     + '&title=' + '<%=ViewData["title"] %>' + '&writer=' + '<%=ViewData["writer"] %>'
            });
 
            EUIcombobox("#infoType", {
                valueField: "编码",
                textField: "名称",
                OneOption: [{
                    编码: "",
                    名称: "----请选择----"
                }],
                url: "/Office/OfficeType/",
                onLoadSuccess: function (data) {
                    $('#infoType').combobox('setValue', '<%=ViewData["type"] %>');
                }

            });
//            //办公类型
//            $('#infoType').combobox({
//                url: '/Office/OfficeType/',
//                valueField: '编码',
//                textField: '名称',
//                onLoadSuccess: function (data) {
//                    $('#infoType').combobox('setValue', '<%=ViewData["type"] %>');
//                }
//            });

            //添加
            $('#btnAdd').bind('click',
                    function () {
                        window.location.href = "/Office/OfficeEdit/?pageNumber=" + getCurrentPage('grid') + "&type=" + escape($('#infoType').combobox('getValue'))
                                                + "&startTime=" + escape($('#startTime').datetimebox('getValue')) + "&endTime=" + escape($('#endTime').datetimebox('getValue'))
                                                + "&title=" + escape($('#title').val()) + "&writer=" + escape($('#writer').val());
                    }
                );
            //修改
            $('#btnUpdate').bind('click',
                    function () {
                        var row = $('#grid').datagrid('getSelected');
                        if (row) {

                            if (!row.IsSelf) {
                                $.messager.alert('提示', '无法修改别人的数据');
                                return;
                            }
                            window.location.href = "/Office/OfficeEdit/?pageNumber=" + getCurrentPage('grid') + "&officeId=" + row.ID + "&type=" + $('#infoType').combobox('getValue')
                                                    + "&startTime=" + escape($('#startTime').datetimebox('getValue')) + "&endTime=" + escape($('#endTime').datetimebox('getValue'))
                                                    + "&title=" + escape($('#title').val()) + "&writer=" + escape($('#writer').val());
                        }
                        else {
                            $.messager.alert('提示', '请选择要修改的数据');
                            return;
                        }

                    }
                );

            //删除
            $('#btnDelete').bind('click',
                    function () {

                        var rows = $('#grid').datagrid('getChecked');

                        if (!rows || rows.length == 0) {
                            $.messager.alert('提示', '请选择要删除的数据');
                            return;
                        }
                        var flag = false;

                        $.each(rows, function (i, n) {

                            if (!n.IsSelf) {
                                flag = true;
                                return false; //跳出循环
                            }
                        });

                        if (flag) {
                            $.messager.alert('提示', '无法删除别人的数据');
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
                                url: "/Office/OfficeDelete/",
                                data: parm,
                                success: function (msg) {
                                    if (msg.IsSuccess) {
                                        $.messager.alert('提示', msg.Message, "info", function () {
                                            this.location.reload();
                                        });
                                    } else {
                                        $.messager.alert('提示', msg.Message, "info", function () {
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

        //查询
        function doSearch() {
            $('#grid').datagrid('load', {
                type: $('#infoType').combobox('getValue'),
                startTime: $("#startTime").datetimebox('getValue'),
                endTime: $('#endTime').datetimebox('getValue'),
                title: $('#title').val(),
                writer: $('#writer').val()
            });
        }

        //获取指定datagrid的当前页
        //wpf
        function getCurrentPage(gridName) {
            var options = $('#' + gridName).datagrid('getPager').data("pagination").options;
            var curr = options.pageNumber;
            return curr;
        }

        function formatDetail(val, rec) {

            var url = "/Office/OfficeDetail/?officeId=" + val;

            var icon = "tu1703";

            return "<a href='#' onclick=AddTab('" + escape(rec.Title) + "','" + url + "','" + icon + "')><img alt='查看' src='../../Content/images/find.gif' border='0'/></a>";
        }
    </script>
</head>
<body class="easyui-layout">
    <div region="center" style="padding: 5px" border="true" > 
         <table id="grid" class="easyui-datagrid" align="center" toolbar="#tb" 
             pagination="true" pagenumber="1" pagelist="[10, 15, 20]" pagesize="15" idfield="ID"
             nowrap="false" striped="true" rownumbers="true" sortname="CreateTime" sortorder="desc"
           remotesort="false" fit="true" fitcolumns="true" checkOnSelect="false"  selectOnCheck="false"
            singleselect="true">
             <thead frozen="true">
                 <tr>
                     <th field="ck" checkbox="true" align='center'></th>
                 </tr>
             </thead>
                <thead>
			        <tr>
                        <th field="IsSelf" hidden='true'>是否本人发出</th>
                        <th field="AnnexCount" width="60"  align='center'>附件个数</th>
                        <th field="Title" width="200"  align='center'>标题</th>
                        <th field="Writer" width="100"  align='center'>作者</th>
                        <th field="OfficeType" width="100" align='center'>办公类型</th>
                        <th field="CreateTime" width="100"  align='center' formatter='renderTime'>创建时间</th>
                        <th field="SendType" width="100"  align='center'>发送类型</th>
                        <th field="ReadCount" width="60"  align='center'>阅读人数</th>
                        <th field="ID" width="60" align='center' formatter="formatDetail">查看</th>   
			        </tr>
		        </thead>
            </table> 
      </div>
      <div id="tb" style="padding:5px;height:auto" >
        <div style="margin-top:3px; text-align:left">      
            <table border="0" cellspacing="0" cellpadding="0" width="100%" height="10%">
                <tr>
                    <td width="5%">
                    </td>
                    <td width="8%">
                        起始时刻:
                    </td>
                    <td>
                        <input id="startTime" type="text" class="easyui-datebox" value="<%= (dynamic)this.ViewData["startTime"] %>"
                            style="width: 100px;" />
                    </td>
                    <td width="8%">
                        终止时刻:
                    </td>
                    <td>
                        <input id="endTime" type="text" class="easyui-datebox" value="<%= (dynamic)this.ViewData["endTime"] %>"
                            style="width: 100px;" />
                    </td>
                    <td width="8%">
                        办公类型：
                    </td>
                    <td>
                        <select id="infoType" class="easyui-combobox" name="infoType" editable="false" style="width: 100px;">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td width="5%">
                    </td>
                    <td width="8%">
                        标题:
                    </td>
                    <td>
                        <input id="title" type="text" class="easyui-validatebox" value="<%= ((dynamic)this.ViewData["title"]) %>"
                            style="border: 1px solid #8DB2E3; width: 94px;" />
                    </td>
                    <td width="8%">
                        作者:
                    </td>
                    <td>
                        <input id="writer" type="text" class="easyui-validatebox" value="<%= ((dynamic)this.ViewData["writer"]) %>"
                            style="border: 1px solid #8DB2E3; width: 94px;" />
                    </td>
                    <td width="8%">
                    </td>
                    <td>
                    </td>
                    <td>
                        <a href="#" class="easyui-linkbutton" iconcls="icon-search" onclick="doSearch()">查询</a>
                    </td>
                </tr>
           </table>  
        </div> 
        <div>  
                <a href="#" id="btnAdd" class="easyui-linkbutton" iconcls="icon-add" plain="true">新增</a> 
                <a href="#" id="btnUpdate" class="easyui-linkbutton" iconCls="icon-edit" plain="true">修改</a>
                <a href="#" id="btnDelete" class="easyui-linkbutton" iconCls="icon-remove" plain="true">删除</a>      
        </div>  
    </div> 
</body>
</html>




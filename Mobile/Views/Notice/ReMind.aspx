<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1"  runat="server">
    <meta name="viewport" content="width=device-width" />
 <title>ReMind</title>
    <%: Styles.Render( "~/Content/css")%>
    <%: Scripts.Render("~/bundles/js")%>
     <script type="text/javascript" language="javascript">

         //格式化“发送列表”
         function formatSendList(val, row) {
             if (val != null && val.length >= 20) {
                 var list = val.substring(0, 20);
                 return '<a  title="' + val + '">' + list + '</a>';
             } else {
                 return '<a  title="' + val + '">' + val + '</a>';
             }
         }

         //格式化“发送内容”
         function formatSendContent(val, row) {
             if (val != null && val.length >= 30) {
                 var content = val.substring(0, 30);
                 return '<a  title="' + val + '">' + content + '</a>';
             } else {
                 return '<a  title="' + val + '">' + val + '</a>';
             }
         }

         $(function () {
             $('#grid').datagrid({
                 iconCls: 'icon-save',
                 nowrap: false,
                 striped: true,
                 url: '/Notice/RemindLoad/',
                 sortName: 'ID',
                 sortOrder: 'asc',
                 remoteSort: false,
                 fitColumns: true,  
                 fit: true,
                 fitColumns: true,
                 pagination: true,
                 rownumbers: true,
                 pageNumber: 1,
                 pageList: [10, 15, 20],
                 pageSize: 15,
                 idField: 'ID',
                 frozenColumns: [[
	                { field: 'cb', checkbox: true, align: 'center' }
                    ]],
                 columns: [[
                    { field: 'ID', title: '编码', width: 20, align: 'center', sortable: "true" },
                    { field: 'operatorName', title: '操作人员', width: 40, align: 'center', sortable: "true" },
                    { field: 'telList', title: '发送对象', width: 60, align: 'center', sortable: "true", formatter: function (telList, rec) { return formatSendList(telList) } },
                    { field: 'content', title: '内容', width: 100, align: 'center', sortable: "true", formatter: function (content, rec) { return formatSendContent(content) } },
                    { field: 'time', title: '提醒时间', width: 40, align: 'center', sortable: "true" },
                    { field: 'isSend', title: '是否发送', width: 15, align: 'center', sortable: "true" }
                    ]],

                 toolbar: ['-', {
                     id: 'btnAdd',
                     text: '添加',
                     iconCls: 'icon-add',
                     handler: function () {
                         this.href = "/Notice/RemindEdit/";
                     }

//                 }, '-', {
//                     id: 'btnUpdate',
//                     text: '修改',
//                     iconCls: 'icon-edit',
//                     handler: function () {
//                         var row = $('#grid').datagrid('getSelected');
//                         if (row) {
//                             this.href = "/Notice/RemindEdit/" + row.ID;    
//                         }
//                         else {
//                             $.messager.alert('提示', '请选择要修改的数据');
//                             return;
//                         }

//                     }

                 }, '-', {
                     id: 'btnDelete',
                     text: '删除',
                     iconCls: 'icon-remove',
                     handler: function () {
                         var rows = $('#grid').datagrid('getSelections');
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
                                 url: "/Notice/RemindDelete/",
                                 data: parm,
                                 success: function (msg) {
                                     if (msg.IsSuccess) {
                                         $.messager.alert('提示', msg.Message + '!', "info", function () {
                                             //refreshDetail(billNo);
                                             $('#grid').datagrid("reload");
                                             //this.location.reload();
                                         });
                                     }
                                     else {
                                         $.messager.alert('提示', msg.Message + '!', 'info', function () {
                                         });

                                     }
                                 },
                                 error: function () {
                                     $.messager.alert('错误', '删除失败！', "error");
                                 }
                             });
                         });
                     }
                 }, '-']
             });

         });
      
     </script>
</head>
<body class="easyui-layout">
    <div region="center" style="padding: 5px" border="true"> 
        <table id="grid" align="center">
        </table>
    </div>
</body>
</html>

<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Data</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript" language="javascript">

        $(function () {
            $('#type').combobox({
                url: '/BasicInfo/DataType/',
                valueField: 'Type',
                textField: 'Type'
            });

            //查询
            $('#btnSearch').bind('click',
                    function () {

                        $('#grid').datagrid('options').url = '/BasicInfo/DataLoad/?type=' + $('#type').combobox('getValue');
                        $('#grid').datagrid("reload");
                        $('#grid').datagrid('clearSelections');
                    }
               );

            //添加
            $('#btnAdd').bind('click',
                    function () {
                        window.location.href = "/BasicInfo/DataEdit/";
                    }
                );

            //修改
            $('#btnEdit').bind('click',
                    function () {
                        var row = $('#grid').datagrid('getSelected');
                        if (row) {
                            window.location.href = "/BasicInfo/DataEdit/?id=" + row.ID;
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
                                url: "/BasicInfo/DataDelete/",
                                data: parm,
                                success: function (msg) {
                                    if (msg.IsSuccess) {
                                        $.messager.alert('提示', '删除成功！', "info", function () {
                                            this.location.reload();
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
    <div region="center" style="padding: 5px" border="true" > 
        <table id="grid" class="easyui-datagrid" align="center"  toolbar="#tb" url="/BasicInfo/DataLoad/"
         pagination="true"  pageNumber=1 pageList= "[10, 15, 20]" pageSize=15 idField="ID" 
         nowrap="false" striped="true" rownumbers="true"
         sortName="Type" sortOrder= "asc" remoteSort="false" fit="true" fitColumns="true"
         >
        <thead frozen="true">    
            <tr>    
				<th field="ID" width="200" checkbox="true"  align='center'></th>
                <th field="Type" width="200"  align='center'>类型</th>   
                <th field="Name" width="200"  align='center'>名称</th>               
            </tr>    
        </thead>
        <thead>
			<tr>
                <th field="Value" width="200"  align='center'>类型编码</th>
                <th field="Sequence" width="200"  align='center'>顺序号</th>
                <th field="Remark" width="200"  align='center'>说明</th>  
			</tr>
		</thead>
        </table>
      </div>
    <div id="tb" style="padding:5px;height:auto" >  
        <div style="margin-bottom:5px">  
            <a href="#" id="btnAdd" class="easyui-linkbutton" iconCls="icon-add" plain="true" >添加</a> 
            <a href="#" id="btnEdit" class="easyui-linkbutton" iconCls="icon-edit" plain="true">修改</a>  
            <a href="#" id="btnDel" class="easyui-linkbutton" iconCls="icon-remove" plain="true" >删除</a>
        </div>  
        <div> 

               类型: <select id="type"   class="easyui-combobox" name="Type"  style="width:150px;"></select>
            <a href="#" id="btnSearch" class="easyui-linkbutton" iconCls="icon-search" >Search</a>
        </div>
    </div>  
</body>
</html>



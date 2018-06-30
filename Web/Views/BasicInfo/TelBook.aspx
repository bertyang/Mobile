<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>通讯录</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
        <script type="text/javascript" language="javascript">
            $(function () {
                //添加
                $('#btnAdd').bind('click',
                    function () {
                        window.location.href = "/BasicInfo/TelBookEdit/?OwnerID=<%=ViewData["OwnerID"] %>";
                    }
                );

                //修改
                $('#btnEdit').bind('click',
                    function () {
                        var row = $('#grid').datagrid('getSelected');
                        if (row) {
                            window.location.href = "/BasicInfo/TelBookEdit/?id=" + row.ID +"&OwnerID=" + <%=ViewData["OwnerID"] %>;
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
                                url: "/BasicInfo/TelDelete/",
                                data: parm,
                                success: function (msg) {
                                    if (msg.IsSuccess) {
                                        $.messager.alert('提示', '删除成功！', "info", function () {
//                                            $('#grid').datagrid("reload");
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

                //导入
                $('#btnImport').bind('click',
                    function () {
                        $('#import').show();
                        $('#import').dialog({
                            collapsible: true,
                            minimizable: true,
                            maximizable: true,
                            height: 220,
                            width: 350,
                            modal: true,
                            title: '导入数据',
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
                                text: '导入',
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
//                                            else{
//                                                var fso = new ActiveXObject("Scripting.FileSystemObject");         
//                                                var file = fso.getfile(filename);      
//                                                var fileSize = file.size/1024; //文件大小转换为kb
//                                                if(parseFloat(fileSize) > 51200){
//                                                    $.messager.alert('提示', '文件大于50M，不能上传！', 'info');
//                                                    return false;
//                                                }
//                                            }
                                            return $(this).form('validate');
                                        },
                                        success: function (msg) {
                                            var data = eval('(' + msg + ')');
                                            if (data.IsSuccess) {
                                                $.messager.alert('提示', data.Message, 'info', function () {
                                                    $('#grid').datagrid("reload");
                                                    $('#import').dialog('close');

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
                                    $('#import').dialog('close');
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
                );

                //分类管理
                $('#btnSort').bind('click',
                    function () {
                        $('#sort').show();
                        $('#sort').dialog({
                            collapsible: true,
                            minimizable: true,
                            maximizable: true,
                            height: 440,
                            width: 550,
                            modal: true,
                            onClose:function(){
                                $('#txt_Type').combotree({
                                    url: '/BasicInfo/TypeTreeLoad/?OwnerID=<%=ViewData["OwnerID"] %>'
                                });
                            },
                            buttons: [
                            {
                                text: '关闭',
                                iconCls: 'icon-cancel',
                                handler: function () {
                                    $('#sort').dialog('close');

                                    $('#txt_Type').combotree({
                                        url: '/BasicInfo/TypeTreeLoad/?OwnerID=<%=ViewData["OwnerID"] %>'
                                    });
                                }
                            }]
                        });
                    }
                );
            });        
        </script>     
</head>
<body class="easyui-layout"> 
    <div region="center" style="padding: 5px" border="true" > 
        <table id="grid" class="easyui-datagrid" align="center" toolbar="#tb" url="/BasicInfo/TelBookLoad/?OwnerID=<%=ViewData["OwnerID"] %>"
            pagination="true" pagenumber="1" pagelist="[10, 15, 20]" pagesize="15" idfield="ID"
            fitcolumns="true" nowrap="false" striped="true" rownumbers="true" 
            sortname="ID" sortorder="asc" remotesort="false" width="1000px" fit="true">
        <thead frozen="true">    
            <tr>    
				<th field="ID" width="100px" checkbox="true"  align='center'>编码</th>
                <th field="Name" width="100px"  align='center'>名称</th>   
            </tr>    
        </thead>
        <thead>
			<tr>
                <th field="TypeName" width="100px" align='center'>电话分类</th>
                <th field="Tel1" width="100px"  align='center'>联系电话一</th>
                <th field="Exten1" width="100px"  align='center'>分机一</th>
                <th field="Tel2" width="100px"  align='center'>联系电话二</th>
                <th field="Exten2" width="100px"  align='center'>分机二</th>
                <th field="Remark" width="100px"  align='center'>备注</th>
                <th field="OrderNo" width="100px" align='center'>顺序号</th>
                <th field="IsEffect" width="100px"  align='center'>是否有效</th>
			</tr>
		</thead>
        </table>
      </div>       
    <div id="tb" style="padding:5px;height:auto" >
        <table width="100%">
            <tr>
                <td style="width: 50%">
                    <div style="margin-bottom: 5px">
                        <a href="#" id="btnAdd" class="easyui-linkbutton" iconCls="icon-add" plain="true" >添加</a> 
                        <a href="#" id="btnEdit" class="easyui-linkbutton" iconCls="icon-edit" plain="true">修改</a>  
                        <a href="#" id="btnDel" class="easyui-linkbutton" iconCls="icon-remove" plain="true" >删除</a>
                        <a href="#" id="btnImport" class="easyui-linkbutton" iconcls="icon-inbox" plain="true">导入</a>
                        <a href="#" id="btnSort" class="easyui-linkbutton" iconcls="icon-wm" plain="true">分类管理</a>
                    </div> 
                 </td>
                 <td>
                     <div region="right">
                         <table width="100%">
                             <tr>
                                 <td width="100px">
                                     姓名:
                                     <input id="txt_Name" class="easyui-validatebox" style="border: 1px solid #8DB2E3;
                                         width: 120px; height: 18px">
                                 </td>
                                 <td width="100px">
                                     电话分类:
                                     <input class="easyui-combotree" style="width: 140px;" url="/BasicInfo/TypeTreeLoad/?OwnerID=<%=ViewData["OwnerID"] %>"
                                         valuefield="编码" textfield="名称" id="txt_Type" value="--请选择--" editable="false" />
                                 </td>
                                 <td width="100px">
                                     <a href="#" class="easyui-linkbutton" iconcls="icon-search" onclick="javascript:$('#grid').datagrid('options').url = '/BasicInfo/TelBookLoad/?Name=' + escape($('#txt_Name').val())
                                        + '&&Type=' + escape($('#txt_Type').combotree('getValue')) + '&&OwnerID=' + '<%=ViewData["OwnerID"] %>';
                                        var p1 = $('#grid').datagrid('getPager');$(p1).pagination({ pageNumber : 1,});
                                        $('#grid').datagrid('options').page = 1;$('#grid').datagrid('reload');return false;">查询</a>
                                 </td>
                             </tr>
                         </table>
                     </div>      
                 </td>
            </tr>
        </table>
    </div>
    <div id="import" icon="icon-save" style="padding: 5px; display:none;">
        <form id="up" action="/BasicInfo/Import/" method="post" enctype="multipart/form-data"
        style="text-align: center; margin-top: 20px;">
            Excel文件：<input type="file" id="upfile" name="upfile" style="border: 1px solid #8DB2E3; width: 200px;
                height: 25px" /><br><br>
        <a href="/BasicInfo/DownLoad/?fileName=通讯录模板.xls" iconcls="icon-1017"><font
            color='#FF9224'><b>查看模板文件</b></font></a>
        </form>
    </div>
    <div id="sort" region="center" border="true" style="overflow-y: hidden" title="分类管理">
        <iframe id="frame" scrolling="auto" frameborder="0" src="/BasicInfo/TelType/?OwnerID=<%=ViewData["OwnerID"] %>" style="width: 100%;
            height: 100%;"></iframe>
    </div>
</body>
</html>

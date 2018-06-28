<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>车辆信息</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
        <script type="text/javascript" language="javascript">
$(function () {
    
  


    //添加
    $('#btnAdd').bind('click',
        function () {
            window.location.href = "/BasicInfo/AmbulanceEdit/";
        }
    );

    //修改
    $('#btnEdit').bind('click',
        function () {
            var row = $('#grid').datagrid('getSelected');
            if (row) {
                window.location.href = "/BasicInfo/AmbulanceEdit/" + row.ID;
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
                    url: "/BasicInfo/AmbulanceDelete/",
                    data: parm,
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


                //#region 查询
                function Search() {
                    $('#grid').datagrid('options').url = '/BasicInfo/AmbulanceSearch/';

                    $('#grid').datagrid('load', {
                        RealCode: $('#txt_RealCode').val(),
                        AmbNum: $('#txt_AmbNum').val(),
                        Station: $('#txt_Station').combobox('getValue'),
                        AmbType: $('#txt_AmbType').combobox('getValue'),
                        AmbGroup: $('#txt_AmbGroup').combobox('getValue')
                    });
                }
                //#endregion       

function formatState(val, rowData, rowIndex) {
    var unsignedInt16 = (rowData.Color >>> 0).toString(16);
    var color = "#" + unsignedInt16.substring(unsignedInt16.length-6);
    return "<span style=\"color:"+color+";\">"+ val +"</span>";
}
        </script>     
</head>
<body>
    <div class="easyui-navpanel">
        <header>
            <div class="m-toolbar">
                <div class="m-left">
                    <a href="#" class="easyui-linkbutton m-back"  data-options="plain:true,outline:true,back:true">Back</a>
                </div>
                <span class="m-title">车辆管理</span>
            </div>
        </header> 
        <table id="grid" class="easyui-datagrid" align="center"  toolbar="#tb" url="/BasicInfo/AmbulanceSearch/"
         pagination="true"  pageNumber=1 pageList= "[10, 15, 20]" pageSize=15 idField="ID" fitColumns="true" 
         nowrap="false" striped="true" rownumbers="true" singleSelect="true"
         sortName="ID" sortOrder= "asc" remoteSort="false" width="1000px" fit="true" 
         >
        <thead frozen="true">    
            <tr>    
				<th field="ID" width="100px" checkbox="true"  align='center'>编码</th>  
            </tr>    
        </thead>
        <thead>
			<tr>
                <th field="Station" width="100px"  align='center'>分站</th>
                <th field="CardNum" width="100px"  align='center'>车牌号码</th>
                <th field="RealSign" width="100px"  align='center'>实际标识</th>
                <th field="State" width="100px"  align='center' formatter="formatState">工作状态</th>
                <th field="Type" width="100px"  align='center'>车辆类型</th>
                <th field="Level" width="100px"  align='center'>车辆等级</th>
                <th field="Tel" width="100px"  align='center'>随车电话</th>
			</tr>
		</thead>
        </table>
      </div> 
    <div id="apage" class="easyui-navpanel"  closed="true">
	   <iframe src="/workflow/approvelist"  height="100%" width="100%"   marginheight="0" marginwidth="0" scrolling="auto" frameborder="0"></iframe>
    </div>
</body>
</html>

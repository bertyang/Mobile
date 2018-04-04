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
    //#region 下拉菜单初始化
    //分站
    EUIcombobox("#txt_Station", {
        valueField: "编码",
        textField: "名称",
        OneOption: [{
            编码: "",
            名称: "--请选择--"
        }],
        url: "/BasicInfo/LoadAllStations/?ActionId=<%=Request.QueryString["ActionId"]%>"

    });
    //车辆类型
    EUIcombobox("#txt_AmbType", {
        valueField: "编码",
        textField: "名称",
        OneOption: [{
            编码: "",
            名称: "--请选择--"
        }],
        url: "/BasicInfo/LoadAllTypes/"

    });
    //分组类型
    EUIcombobox("#txt_AmbGroup", {
        valueField: "编码",
        textField: "名称",
        OneOption: [{
            编码: "",
            名称: "--请选择--"
        }],
        url: "/BasicInfo/LoadAllGroups/"

    });
    //#endregion


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
<body class="easyui-layout"> 
    <div region="center" style="padding: 5px" border="true" > 
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
    <div id="tb" style="padding:5px;height:auto;background-color:" >  
        <div style="margin-bottom:5px">  
            <a href="#" id="btnAdd" class="easyui-linkbutton" iconCls="icon-add" plain="true" >添加</a> 
            <a href="#" id="btnEdit" class="easyui-linkbutton" iconCls="icon-edit" plain="true">修改</a>  
            <a href="#" id="btnDel" class="easyui-linkbutton" iconCls="icon-remove" plain="true" >删除</a>
        </div>
        <div> 
            实际标识: 
                <input id="txt_RealCode" class="easyui-validatebox" style="border: 1px solid #8DB2E3;
                    width: 81px; height: 18px"> 
            车牌号码: 
                <input id="txt_AmbNum" class="easyui-validatebox" style="border: 1px solid #8DB2E3;
                    width: 100px; height: 18px"> 
            分站:     
                <input class="easyui-combobox" style="width:150px;" id="txt_Station" editable="false" />
            车辆类型:     
                <input class="easyui-combobox" style="width:150px;" id="txt_AmbType" editable="false" />
            分组类型:     
                <input class="easyui-combobox" style="width:150px;" id="txt_AmbGroup" editable="false" />
            <a href="#" class="easyui-linkbutton" iconCls="icon-search" 
                onclick="Search();">查询</a> 
        </div>
    </div>
</body>
</html>

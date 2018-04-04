<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PositionInfo</title>
    <%: Styles.Render( "~/Contents/css") %>
    <%: Scripts.Render("~/bundle/js")%>
    <script language="javascript" type="text/javascript">

            //选择人员弹出框
            $(function () {
                $.extend($.fn.validatebox.defaults.rules, {
                /*必须和某个字段相等*/
                equalTo: { validator: function (value, param) { return $(param[0]).val() == value; }, message: '字段不匹配' }
                });     

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


                $('#btnSearch').bind('click',
                        function () {
                            $('#worker').datagrid('options').url = '/Organize/WorkerLoad/?Name=' + escape($('#txtName').val())
                                + '&orgId=' + $('#orgId').val()
                                + '&roleId=' + $("#role_sel").combobox('getValue');
                            $('#worker').datagrid("reload");
                        }
                );

            
                $('#role_sel').combobox({
                    url: '/Organize/RoleLoadAll/',
                    valueField: 'ID',
                    textField: 'Name',
                    onLoadSuccess: function (data) {
                        $('#role_sel').combobox('setValue', "--请选择--");
                    }
                });

            });

            //密码
            $(function () {

               //密码
                $('#w').window('close');

                $('#editpass').click(function () {
                    $('#w').show();
            
                    $('#w').window({
                        title: '修改密码',
                        width: 300,
                        modal: true,
                        shadow: true,
                        height: 140,
                        resizable: false
                    });
                    return false;
                });

                $('#btnEp').click(function () {

                    var $newpass = $('#txtNewPass');
                    var $rePass = $('#txtRePass');
          
                    if ($newpass.val() == '') {
                        $.messager.alert('系统提示', '请输入密码！', 'warning');
                        return false;
                    }
           
                    if ($rePass.val() == '') {
                        $.messager.alert('系统提示', '请再一次输入密码！', 'warning');
                        return false;
                    }

                    if ($newpass.val() != $rePass.val()) {
                        $.messager.alert('系统提示', '两次密码不一至！请重新输入', 'warning');
                        return false;
                    }

                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        url: "/Account/ChangePassword/",
                        data: { workerId: <%= ((dynamic)this.ViewData["entity"]).ID %>, newPassWord: $newpass.val() },
                        success: function (msg) {
                            if (msg.IsSuccess) {
                                $.messager.alert('系统提示', '恭喜，密码修改成功！', 'info');
                                $newpass.val('');
                                $rePass.val('');
                                 $('#w').window('close');
                            }
                            else {
                                $.messager.alert('错误', '更新失败!', 'error');
                            }
                        },
                        error: function () {
                            $.messager.alert('错误', '更新失败...请联系管理员!', 'error');
                        }
                    });
                });

                $('#btnCancel').click(function () {

                    var $newpass = $('#txtNewPass');
                    var $rePass = $('#txtRePass');

                    $newpass.val('');
                    $rePass.val('');
                    $('#w').window('close');
                 }); 

            });

            //角色
            $(function () {

                $('#RoleUnit').combobox({
                    url: '/Organize/WorkerSideline/?workerId=' +　'<%=ViewData["workerId"] %>',
                    valueField: 'OrgID',
                    textField: 'OrgName'
                });
                
                $('#Role').combobox({
                    url: '/Organize/RoleLoadAll/',
                    valueField: 'ID',
                    textField: 'Name'

                });

                $('#grid_role').datagrid({
                    url: '/Organize/WorkerRoleLoad/?workerId=' + '<%=ViewData["workerId"] %>',
                    singleSelect: true,
                    toolbar: ['-', {
                        id: 'RbtnAdd',
                        text: '添加',
                        iconCls: 'icon-add',
                        handler: function () {
                            var row = $('#grid').datagrid('getSelected');

                            if(!row){
                                $.messager.alert('提示',"请选择一个机构", 'info');
                                return;
                            }

                            $("#SyncDM").attr("checked",false);
                            $("#SyncDM").attr("disabled",false);
                            $("#SyncCode").hide();
                            $("#SyncEmpNo").hide();
                            

                            $('#ID').val(0); 
                            $('#WorkerID').val(row.WorkerID);
                            $('#RoleUnit').combobox('reload');
                            $('#RoleUnit').combobox('setValue', row.OrgID);
                            $('#Role').combobox('clear'); 
                            $('#EmpNo').val('');
                            $('#Code').val('');
                            $("#Code").attr("readonly",false);

                            openRoleDialog("新增角色");
                        }
                    }, '-', {
                        id: 'RbtnUpdate',
                        text: '修改',
                        iconCls: 'icon-edit',
                        handler: function () {
                            var row = $('#grid_role').datagrid('getSelected');
                            if (row) {
                                    $('#ID').val(row.ID); 
                                    $('#WorkerID').val(row.WorkerID);
                                    $('#RoleUnit').combobox('reload');
                                    $('#RoleUnit').combobox('setValue', row.OrgID);
                                    $('#Role').combobox('setValue',row.RoleID);

                                    if(row.EmpNo !=''){
                                        $("#SyncDM").attr("checked",true);
                                        $("#SyncDM").attr("disabled",true); 
                                        $("#SyncCode").show();
                                        $("#SyncEmpNo").show();                                        

                                        $('#EmpNo').val(row.EmpNo);
                                        $('#Code').val(row.TPerson编码);
                                        $("#Code").attr("readonly",true);
                                    }
                                    else
                                    {
                                        $("#SyncDM").attr("checked",false);
                                        $("#SyncDM").attr("disabled",false);
                                        $("#SyncCode").hide();;
                                        $("#SyncEmpNo").hide();

                                        $('#EmpNo').val('');
                                        $('#Code').val('');
                                        $("#Code").attr("readonly",false)
                                    }

                                    openRoleDialog("修改角色");

                            }
                            else {
                                $.messager.alert('提示', '请选择要修改的数据');
                                return;
                            }
                        }
                    }, '-', {
                        id: 'RbtnDel',
                        text: '删除',
                        iconCls: 'icon-remove',
                        handler: function () {
                            var row = $('#grid_role').datagrid('getSelected');
                            if (!row) {
                                $.messager.alert('提示', '请选择要删除的数据');
                                return;
                            }

                            if (row.EmpNo.indexOf("(<font color='red'>上班</font>)") > 0) {
                                $.messager.alert('提示', '上班状态不能删除');
                                return;
                            }

                            var message="是否删除?";
                            
                            if(row.EmpNo != null && row.EmpNo != "")
                            {
                                message="调度系统的相关数据也将删除,是否确认删除?";
                            }

                            $.messager.confirm('提示', message, function (r) {
                                if (!r) {
                                    return;
                                }

                                $.ajax({
                                    type: "POST",
                                    url: "/Organize/WorkerRoleDel/?id=" +row.ID,
                                    success: function (msg) {
 
                                        $.messager.alert('提示', msg.Message, "info", function () {
                                            $('#grid_role').datagrid("reload");
                                        });
                                      
                                    },
                                    error: function () {
                                        $.messager.alert('错误', '删除失败！', "error");
                                    }
                                });
                            });
                        }
                    },'-']
                });
             
                $("#SyncDM").change(function() {                     
                        
                        if (!$("#SyncDM").attr("checked")) {
                            $("#SyncCode").hide();
                            $("#SyncEmpNo").hide();
                            $("#Code").val('');
                            $('#EmpNo').val('');
                        }else if($("#SyncDM").attr("checked")){
                            $("#SyncCode").show();
                            $("#SyncEmpNo").show();
                            $("#Code").val(GetCode());
                        }
                    });

            });

            //机构
            $(function () {
                $('#post').combobox({
                url: '/BasicInfo/GetDataByType/?type=Post',
                valueField: 'Value',
                textField: 'Name'
            });

            $('#unit').combotree({
                url: '/Organize/UnitTree/'
            });

                $('#grid').datagrid({
                    url: '/Organize/WorkerSideline/?workerId=' +　'<%=ViewData["workerId"] %>',
                    singleSelect: true,
                    toolbar: ['-', {
                        id: 'btnAdd',
                        text: '添加',
                        iconCls: 'icon-add',
                        handler: function () {
                            $('#workerId').val('<%=ViewData["workerId"] %>'); 
                            $('#unit').combotree('setValue', ''); 
                            $('#post').combobox('setValue', '');
                            $('#managerId').val('');
                            $('#managerName').val('');
                            $("#type0").attr("checked",true); 
                            $("#type1").attr("checked",false);
                            $('#id').val(0); 

                            openUnitDialog("新增机构");
                        }
                    }, '-', {
                        id: 'btnUpdate',
                        text: '修改',
                        iconCls: 'icon-edit',
                        handler: function () {
                            var row = $('#grid').datagrid('getSelected');
                            if (row) {
                                $('#workerId').val(row.WorkerID); 
                                $('#unit').combotree('setValue', row.OrgID); 
                                $('#post').combobox('setValue',row.PostID);
                                $('#managerId').val(row.ParentID);
                                $('#managerName').val(row.Parent);
                                $("#type0").attr("checked",row.Type=="默认"); 
                                $("#type1").attr("checked",row.Type=="兼职");
                                $('#id').val(row.ID); 

                                openUnitDialog("修改机构");
                            }
                            else {
                                $.messager.alert('提示', '请选择要修改的数据');
                                return;
                            }
                        }

                    }, '-', {
                        id: 'btnDelete',
                        text: '删除',
                        disabled: false,
                        iconCls: 'icon-remove',
                        handler: function () {
                            var row = $('#grid').datagrid('getSelected');
                            if (!row) {
                                $.messager.alert('提示', '请选择要删除的数据');
                                return;
                            }

                           var rows = $('#grid_role').datagrid('getRows');
                            if (rows) {
                                for (var i = 0; i < rows.length; i++) {
                                    if (rows[i].OrgID == row.OrgID) {
                                        $.messager.alert('提示', '请先删除机构对应的角色！');
                                        return;
                                    }
                                }
                            }

                            $.messager.confirm('提示', '是否删除这些数据?', function (r) {
                                if (!r) {
                                    return;
                                }

                                $.ajax({
                                    type: "POST",
                                    url: "/Organize/WorkerSidelinDelete/?id=" + row.ID,
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
                    }, '-'],                    
                    onSelect: function (rowIndex, rowData) {
//                        $('#grid_role').datagrid('options').url = '/Organize/WorkerRoleLoad/?workerId=' + '<%=ViewData["workerId"] %>'+"&orgId="+rowData.OrgID;
//                        $('#grid_role').datagrid('reload');
                    }
                }); 
            });

            //人员
            $(function () {        

            //性别
            if('<%= ((dynamic)this.ViewData["entity"]).Sex %>'=="1"){document.getElementsByName('sex')[0].checked=true;}    
            if('<%= ((dynamic)this.ViewData["entity"]).Sex %>'=="0"){document.getElementsByName('sex')[1].checked=true;} 

            //是否激活
            if('<%= ((dynamic)this.ViewData["entity"]).IsActive %>'=="Y"){document.getElementsByName('active')[0].checked=true;}
            if('<%= ((dynamic)this.ViewData["entity"]).IsActive %>'=="N"){document.getElementsByName('active')[1].checked=true;} 

            //是否允许Internet访问
            if('<%= ((dynamic)this.ViewData["entity"]).IsAllowInternetAccess %>'=="Y"){document.getElementsByName('isAllowInternetAccess')[0].checked=true;}
            if('<%= ((dynamic)this.ViewData["entity"]).IsAllowInternetAccess %>'=="N"){document.getElementsByName('isAllowInternetAccess')[1].checked=true;} 

            //职称
            $('#title').combobox({
                url: '/BasicInfo/GetDataByType/?type=Title_Technical',
                valueField: 'Value',
                textField: 'Name',
                onLoadSuccess: function (data) {
                    <%if (ViewData["title"]!=null) { %>
                        
                    $('#title').combobox('setValue', '<%=ViewData["title"]%>');
                    <%} %>
                }
            });

            //岗位等级
            $('#JobLevel').combobox({
                url: '/BasicInfo/GetDataByType/?type=JobLevel',
                valueField: 'Value',
                textField: 'Name',
                onLoadSuccess: function (data) {
                    <%if (ViewData["JobLevel"]!=null) { %>                        
                        $('#JobLevel').combobox('setValue', '<%=ViewData["JobLevel"]%>');
                    <%} %>
                }
            });


             //人员性质
            $('#IsQuota').combobox({
                url: '/BasicInfo/GetDataByType/?type=personnel',
                valueField: 'Value',
                textField: 'Name',
                onLoadSuccess: function (data) {
                    <%if (ViewData["IsQuota"]!=null) { %>
                        $('#IsQuota').combobox('setValue', '<%=ViewData["IsQuota"]%>');
                    <%} %>
                }
            });

        });

            //保存人员
            function submit() {

                $.ajax({
                    type: "POST",
                    async:false,
                    url: "/Organize/IsExistName/?name=" +escape($('#name').val())+"&workerId=<%=ViewData["workerId"]%>",
                    success: function (msg) {
                        if (msg.IsSuccess) {
                            $.messager.alert('提示', '姓名已经存在！');
                                $('#name').focus();
                                return;
                        }
                        else
                        {
                            $('#form').form('submit', { 
                                onSubmit: function () {
                                 
                                    <% if ((dynamic)this.ViewData["type"] == "update"){ %>
                                        var allRows = $('#grid').datagrid('getRows');
                                        var count=0;
                                        for (var i = 0; i < allRows.length; i++) {
                                            if (allRows[i].Type=="默认"){
                                                count=count+1;
                                            }
                                        }
                                     
                                        if(count == 0){
                                            $.messager.alert('提示',"至少要有一个默认机构", 'info');
                                            return false;
                                        }

                                        if(count > 1){
                                            $.messager.alert('提示',"默认机构只能有一个", 'info');
                                            return false;
                                        }
                                    <%}%>
                                    return $(this).form('validate');
                                },
                                success: function (msg) {
                                    var data = eval('(' + msg + ')'); 
                                    if(data.IsSuccess)
                                    {
                                        $.messager.alert('提示', data.Message, 'info', function(){
                                            <% if ((dynamic)this.ViewData["type"] == "update"){ %>
                                            CloseCurrentTab();
                                            <%} else {%>
                                            window.location.href = "/Organize/WorkerEdit/?workerId=" + data.WorkerId;
                                            <%}%>
                                        });
                                    }
                                    else
                                    {
                                        $.messager.alert('提示', data.Message, 'info', function(){
                                        });
                                    }
                                }
                            });  
                        }
                    },
                    error: function () {
                        $.messager.alert('错误', '检查登录名失败！', "error");
                    }
                });
            
            }

            //机构弹出框
            function openUnitDialog(title){

                $('#SidelineEdit').show();
                $('#SidelineEdit').dialog({
                    collapsible: true,
                    minimizable: true,
                    maximizable: true,
                    height: 300,
                    width: 400,
                    modal: true, //阴影（弹出会影响页面大小）
                    title: title,
                    onClose: function () {
                        $('#grid').datagrid("reload");
                    },
                    buttons: [{
                        text: '保存',
                        iconCls: 'icon-save',
                        handler: function () {
                            $('#form_s').form('submit', { 
                                onSubmit: function () {

                                    if(!$(this).form('validate')) return false;

                                    if (!$('input[name="type"]:checked').val()){                                    
                                        $.messager.alert('提示',"请选择机构类型", 'info');
                                        return false;
                                    }

                                    if($('input[name="type"]:checked').val() == 0 ){
                                        
                                        var allRows = $('#grid').datagrid('getRows');
                                        var id=0;
                                        for (var i = 0; i < allRows.length; i++) {
                                            if (allRows[i].Type=="默认"){
                                                id = allRows[i].ID;
                                            }
                                        }
                                     
                                        if( id !=0 && $('#id').val()!=id){
                                            $.messager.alert('提示',"默认机构只能有一个", 'info');
                                            return false;
                                         }
                                    }

                                    return true;

                                },
                                success: function (msg) {
                                    var data = eval('(' + msg + ')'); 
                                    if(data.IsSuccess)
                                    {
                                        $.messager.alert('提示', data.Message, 'info', function(){
                                            $('#SidelineEdit').dialog('close');
                                            $('#grid').datagrid("reload");
                                            $('#grid_role').datagrid("reload");
                                        });
                                    }
                                    else
                                    {
                                        $.messager.alert('提示', data.Message, 'info', function(){
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
                            $('#SidelineEdit').dialog('close');
                        }
                    }]
                });         
            }

            function openRoleDialog(title){

                            $('#WorkerRoleEdit').show();                                                               
                            $('#WorkerRoleEdit').dialog({
                             collapsible: true,
                             minimizable: true,
                             maximizable: true,
                             height: 250,
                             width: 300,
                             modal: true,
                             title: title,
                             buttons: [
                                    {
                                        text: '确定',
                                        iconCls: 'icon-save',
                                        handler: function () {
                                             SaveWorkerRole();  
                                        }
                                    },
                                    {
                                        text: '取消',
                                        iconCls: 'icon-cancel',
                                        handler: function () {
                                            $('#WorkerRoleEdit').dialog('close');
                                        }
                                    }]
                         });
            }


            //保存角色
            function SaveWorkerRole(){

                $('#Rform').form('submit', {
                    onSubmit: function () {
                        //如果不同步调度系统，取消编码和工号验证
                        if(!$("#SyncDM").attr("checked"))
                        {
                            $("#Code").validatebox('disableValidation');
                            $("#EmpNo").validatebox('disableValidation');
                        }
                        else
                        {
                            $("#Code").validatebox('enableValidation');
                            $("#EmpNo").validatebox('enableValidation');
                        }
                                    
                        return $(this).form('validate') ;
  
                    },
                    success: function (msg) {
                        var data = eval('(' + msg + ')');
                        if (data.IsSuccess) {
                            $.messager.alert('提示', data.Message, 'info', function () {
                                $('#WorkerRoleEdit').dialog('close');
                                $('#grid_role').datagrid('reload');
                            });
                        }
                        else {
                            $.messager.alert('提示', data.Message, 'info', function () {
                            });
                        }
                    }
                });           
            }

            //选择人员
            function selectWorkers() {
            $('#worker').datagrid({
                url: '/Organize/WorkerLoad/'
            });

            //弹出选择框
            $('#WorkerDiv').show();
            $('#WorkerDiv').dialog({
                collapsible: true,
                minimizable: true,
                maximizable: true,
                height: 450,
                width: 700,
                modal: true, //阴影（弹出会影响页面大小）
                title: '选择人员',
                buttons: [{
                    text: '确定',
                    iconCls: 'icon-ok',
                    handler: function () {

                        var rows = $('#worker').datagrid('getSelected');

                        if (rows) {
                                $("#managerId").val(rows.ID);
                                $("#managerName").val(rows.Name);   
                        }

                        $('#WorkerDiv').dialog('close');
                        $('#worker').datagrid('clearSelections');
                        $('#txtName').val("");
                        $('#role_sel').combobox('setValue', "--请选择--");
                    }
                },
                    {
                        text: '取消',
                        iconCls: 'icon-cancel',
                        handler: function () {
                            $('#WorkerDiv').dialog('close');
                            $('#worker').datagrid('clearSelections');
                            $('#txtName').val("");
                            $('#role_sel').combobox('setValue', "--请选择--");
                        }
                    }]
            });



            }

            function GetCode() {

                var result;

                $.ajax({
                    type: "POST",
                    async: false,
                    url: "/Organize/GetCode/?tableName=TPerson",
                    dataType: "json",
                    success: function (data) {
                        result = data;
                    },
                    error: function () {
                        result = false;
                    }
                });

                return result;
            }

            function IsExistEmpNo()
            {
                $.ajax({
                    type: "POST",
                    async:false,
                    url: "/Organize/IsExistEmpNo/?empNo=" + $('#EmpNo').val(),
                    success: function (msg) {

                            var flag=true; //标识是否修改现有工号

                            var row = $('#grid_role').datagrid('getSelected');

                            if(row !=null){

                                if(row.EmpNo == $('#EmpNo').val()) {
                                    
                                    flag=false;
                                }
                            }

                            if (msg.IsSuccess && flag) {
                                $.messager.alert('提示', '已存此工号','error');
                                $('#EmpNo').val('');
                            }
                        }
                    })
            }

            function IsExistCode()
            {
                if (!$('#Code').attr("readonly")) {
                    $.ajax({
                        type: "POST",
                        async:false,
                        url: "/Organize/IsExistCode/?tableName=TPerson&code=" + $('#Code').val(),
                        success: function (msg) {

                            if (msg.IsSuccess) {
                                $.messager.alert('提示', '已存此编码','error');
                                $('#Code').val('');
                            }
                        }
                    })
                }
            }

            function IsExistID()
            {  
                if (!$('#Worker_Id').attr("readonly")) {
                    $.ajax({
                        type: "POST",
                        async:false,
                        url: "/Organize/IsExistID/?tableName=B_WORKER&id="+$('#Worker_Id').val(),
                        success: function (msg) {

                            if (msg.IsSuccess) {
                                $.messager.alert('提示', '已存此ID','error');
                                $('#Worker_Id').val('');
                            }
                        }
                    })
                }
            }
    </script>
</head>
<body class="easyui-layout">
    <div region="center" border="false" style=" overflow-y:auto;">
        <div region="north" style="margin-left:45px;margin-top:20px">
            <span id="" class="editTitle">编辑人员</span>
        </div>
        <div region="center" style="margin-left: 45px;">
            <form id="form" method="post" action="/Organize/WorkerSave/" enctype="application/x-www-form-urlencoded">
            <table width="100%" border=0>
                <tr>
                    <td style="text-align: right; width: 150px;">
                        ID(<font color="red">*</font>)：
                    </td>
                    <td>
                        <%if ((dynamic)this.ViewData["type"] == "update")  {%>
                            <input type="text" id="Worker_Id" name="entity.ID"  value="<%= ((dynamic)this.ViewData["entity"]).ID  %>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px"  readonly="readonly"/> 
                        <%} else { %>
                             <input type="text" id="Worker_Id" class="easyui-validatebox" 
                             required="true" validtype="length[1,5]" 
                            onkeyup="this.value=this.value.replace(/[^\d]/g,'') " 
                            onafterpaste="this.value=this.value.replace(/[^\d]/g,'') "  
                            name="entity.ID" value="<%= ((dynamic)this.ViewData["entity"]).ID  %>"
                            onblur="IsExistID()"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" /> 
                        <% }%>   
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;width: 150px;">
                        姓名(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input id="name" type="text" class="easyui-validatebox" required="true" name="entity.Name"
                            validtype="length[1,50]" value="<%= ((dynamic)this.ViewData["entity"]).Name %>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        通讯号码：
                    </td>
                    <td>
                       <input type="text"  class="easyui-validatebox"  name="entity.Tel" 
                            onkeyup="this.value=this.value.replace(/[^\d]/g,'') " 
                            onafterpaste="this.value=this.value.replace(/[^\d]/g,'')"
                            validtype="length[1,30]" 
                           value="<%=  ((dynamic)this.ViewData["entity"]).Tel == null ?"":((dynamic)this.ViewData["entity"]).Tel  %>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        短信号码：
                    </td>
                    <td>
                       <input type="text"  class="easyui-validatebox"  name="entity.Mobile"
                            validtype="length[1,30]" 
                            value="<%=  ((dynamic)this.ViewData["entity"]).Mobile == null ?"":((dynamic)this.ViewData["entity"]).Mobile  %>"
                            onkeyup="this.value=this.value.replace(/[^\d]/g,'') " 
                            onafterpaste="this.value=this.value.replace(/[^\d]/g,'')"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        是否启用(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="radio" name="active" value="Y" checked />是&nbsp;
                        <input type="radio" name="active" value="N" />否&nbsp;
                    </td>
                </tr>
                <tr  style= "<%=ViewData["display"]%>">
                    <td style="text-align: right;">
                        性别：
                    </td>
                    <td>
                        <input type="radio" name="sex" value="1" checked/>男&nbsp;
                        <input type="radio" name="sex" value="0" />女&nbsp;
                    </td>
                </tr>
                <tr  style= "<%=ViewData["display"]%>">
                    <td style="text-align: right;">
                        入职时间：
                    </td>
                    <td>
                        <input type="text"  editable="false" class="easyui-datebox" required="true" name="entity.EntryDate"
                            value="<%= ((dynamic)this.ViewData["entity"]).EntryDate.ToShortDateString()%>"
                            style="border: 1px solid #8DB2E3; width: 150px;" />
                    </td>
                </tr>
                <tr  style= "<%=ViewData["display"]%>">
                    <td style="text-align: right;">
                        职称：
                    </td>
                    <td>
                        <select id="title" editable="false" class="easyui-combobox" name="entity.TitleTechnical"
                            style="width: 150px;">
                        </select>
                    </td>
                </tr>
                <tr  style= "<%=ViewData["display"]%>">
                    <td style="text-align: right;">
                        岗位等级：
                    </td>
                    <td>
                        <select id="JobLevel" editable="false" class="easyui-combobox" name="entity.JobLevel"
                            style="width: 150px;">
                        </select>
                    </td>
                </tr>
                <tr  style= "<%=ViewData["display"]%>">
                    <td style="text-align: right;">
                        人员性质：
                    </td>
                    <td>
                         <select id="IsQuota" editable="false" class="easyui-combobox" name="entity.IsQuota"
                            style="width: 150px;">
                        </select>
                    </td>
                </tr>
                <tr style= "<%=ViewData["display"]%>">
                    <td style="text-align: right;">
                        是否允许Internet访问：
                    </td>
                    <td>
                        <input type="radio" name="isAllowInternetAccess" value="Y"  />是&nbsp;
                        <input type="radio" name="isAllowInternetAccess" value="N" checked/>否&nbsp;
                    </td>
                </tr>
                 <% if ((dynamic)this.ViewData["type"] != "update")
                    { %>
                <tr>
                    <td style="text-align: right;">
                        新密码(<font color="red">*</font>)：
                    </td>
                    <td>
                       <input id="password" name="entity.PassWord" validType="length[1,8]" class="easyui-validatebox" 
                        required="true"  type="password" style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        确认密码(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="password" name="repassword" id="repassword"  class="easyui-validatebox"
                          validType="equalTo['#password']" invalidMessage="两次输入密码不匹配"
                          style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <% } else { %>
                <tr>
                    <td style="text-align: right; vertical-align: top">
                        机构：
                    </td>
                    <td>
                        <div region="center" style="width:400px;" border="true">
                            <table id="grid" class="easyui-datagrid" align="center" idfield="OrgID"
                                nowrap="false" striped="true" rownumbers="true" sortname="OrgID" sortorder="asc"
                                remotesort="false" singleselect="true" height="200" fitcolumns="true">
                                <thead frozen="true">
                                    <tr>
                                        <th field="OrgID" checkbox="true" align='center'>
                                            OrgID
                                        </th>
                                    </tr>
                                </thead>
                                <thead>
                                    <tr>
                                        <th field="OrgName" width="25%" align='center'>
                                            机构
                                        </th>
                                        <th field="PostID" align='center' hidden=true'>
                                            职位ID
                                        </th>
                                        <th field="Post" width="20%" align='center'>
                                            职位
                                        </th>
                                        <th field="ParentID"  align='center' hidden='true'>
                                            上级ID
                                        </th>
                                        <th field="Parent" width="25%" align='center'>
                                            上级
                                        </th>
                                        <th field="Type" width="25%" align='center'>
                                            类型
                                        </th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: top">
                        角色：
                    </td>
                    <td>
                        <div region="center" style="width: 400px;" border="true">
                            <table id="grid_role" class="easyui-datagrid" align="center" nowrap="false"
                                striped="true" rownumbers="true" " remotesort="false"
                                singleselect="true" height="200" fitcolumns="true">
                                <thead frozen="true">
                                    <tr>
                                        <th field="RoleID" checkbox="true" align='center'>
                                            RoleID
                                        </th>
                                    </tr>
                                </thead>
                                <thead>
                                    <tr>
                                        <th field="OrgName" width="30%" align='center'>
                                            机构
                                        </th>
                                        <th field="RoleName" width="30%" align='center'>
                                            角色
                                        </th>
                                        <th field="EmpNoStatus" width="30%" align='center'>
                                            调度系统工号
                                        </th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </td>
                </tr>
                <% } %>
            </table>
            </form>
        </div>
    </div> 

    <div region="south" border="true" style="text-align: right; height: 40px; line-height: 30px;
        background-color: #f7f7f7;">
        <table style="width: 100%">
            <tr>
                <td style="text-align: LEFT">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-save" onclick="submit();">提交</a>
                    <% if ((dynamic)this.ViewData["type"] == "update"){ %>
                            <a href="#" class="easyui-linkbutton" iconcls="icon-edit" id="editpass">修改密码</a>                                 
                    <% } %>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="CloseCurrentTab();">取消</a>    
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <!--修改密码窗口-->
    <div id="w" style="display:none"  class="easyui-window" title="修改密码" collapsible="false" minimizable="false"
        maximizable="false" icon="icon-edit"  style="width: 200px; height: 140px; padding: 5px;  background: #fafafa;">
   
            <div region="center" border="false" style=" padding-left:30px; background:#fff;border:1px solid #ccc;">
                <table cellpadding=3>
                    <tr>
                        <td>新密码：</td>
                        <td><input id="txtNewPass" type="Password" /></td>
                    </tr>
                    <tr>
                        <td>确认密码：</td>
                        <td><input id="txtRePass" type="Password" /></td>
                    </tr>
                </table>
            </div>
            <div region="south" border="false" style="text-align: center; height: 30px; line-height: 30px;">
                <a id="btnEp" class="easyui-linkbutton" icon="icon-ok" href="javascript:void(0)" >
                    确定</a> <a id="btnCancel" class="easyui-linkbutton" icon="icon-cancel" href="javascript:void(0)">取消</a>
            </div>
 
    </div>

    <!--机构-->
    <div id="SidelineEdit" icon="icon-save" style="padding: 5px; display: none">
        <div region="center" style="margin-top: 25px">
            <form id="form_s" method="post" action="/Organize/WorkerSidelineSave/" enctype="application/x-www-form-urlencoded">
            <table>
                <tr style="display: none">
                    <td colspan=2>
                        <input id="workerId" type="hidden" name="entity.WorkerID" />
                        <input id="id" type="hidden" name="entity.ID" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right">
                        机构(<font color="red">*</font>)：
                    </td>
                    <td style="width: 80%; text-align: left">
                        <select id="unit" class="easyui-combotree" name="entity.OrgID" required="true"
                            style="width: 200px;">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        职位(<font color="red">*</font>)：
                    </td>
                    <td style="text-align: left">
                        <select id="post" class="easyui-combobox" required="true" name="entity.PostID"
                            style="width: 200px;">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        上级(<font color="red">*</font>)：
                    </td>
                    <td style="text-align: left">
                         <input id="managerId" type="text"  name="entity.ParentID"  style="display: none" />
                         <input id="managerName" type="text" required="true" readonly="true" class="easyui-validatebox" style="border: 1px solid #8DB2E3; width: 200px; "/>
                         <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="selectWorkers();">选择</a>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        类型(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="radio" id="type0"  name="type" value="0" />默认&nbsp;
                        <input type="radio" id="type1" name="type" value="1" />兼职&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        备注：
                    </td>
                    <td style=" text-align: left">
                        <input id="remark" type="text" class="easyui-validatebox" name="entity.Remark" validtype="length[0,255]"
                            style="border: 1px solid #8DB2E3; width: 200px; height: 18px" />
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>

    <!--人员角色-->
    <div id="WorkerRoleEdit" icon="icon-save" style="padding: 5px; display: none">
        <div region="center" border="true" style="margin-top: 25px">
            <form id="Rform" method="post" action="/Organize/WorkerRoleSave/" enctype="application/x-www-form-urlencoded">
            <table>
                <tr style="display: none">
                    <td>
                        <input id="ID" type="hidden"  name="entity.ID" />
                        <input id="WorkerID" type="hidden"  name="entity.WorkerID"  />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px; text-align: right">
                        机构(<font color="red">*</font>)：
                    </td>
                    <td style="text-align: left">
                        <select id="RoleUnit" class="easyui-combobox" name="entity.OrgID"  editable="false" required="true" style="width: 125px;">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px; text-align: right">
                        角色(<font color="red">*</font>)：
                    </td>
                    <td style="text-align: left">
                        <select id="Role" class="easyui-combobox" name="entity.RoleID"   editable="false" required="true" style="width: 125px;">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px; text-align: right">
                        同步调度系统：
                    </td>
                    <td style="text-align: left">
                        <input type="checkbox" id="SyncDM" />
                    </td>
                </tr>
                <tr  id="SyncCode" style="display: none">
                    <td style="width: 100px; text-align: right">
                        编码(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input id="Code" type="text" 
                            name="entity.TPerson编码" 
                            class="easyui-validatebox" 
                            required="true"
                            validtype="length[5,5]" 
                            onkeyup="this.value=this.value.replace(/[^\d]/g,'') " 
                            onafterpaste="this.value=this.value.replace(/[^\d]/g,'') " 
                            onblur="IsExistCode()"
                            style="border: 1px solid #8DB2E3; width: 119px;" />
                    </td>
                </tr>
                <tr  id="SyncEmpNo" style="display: none">
                    <td style="width: 100px; text-align: right">
                        工号(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input id="EmpNo" type="text" 
                            name="entity.EmpNo" 
                            class="easyui-validatebox"  
                            validtype="length[1,10]"
                            required="true"
                            onkeyup="this.value=this.value.replace(/[^\d]/g,'') " 
                            onafterpaste="this.value=this.value.replace(/[^\d]/g,'') "
                            onblur="IsExistEmpNo()"
                            style="border: 1px solid #8DB2E3; width: 119px;" />
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>

    <div id="WorkerDiv" icon="icon-save" style="overflow: hidden; padding: 5px; display: none">
        <div class="easyui-layout" icon="icon-save" style="padding: 5px; width: 100%; height: 100%;">
            <div region="west" split="true" title="组织机构" iconcls="icon icon-sys" style="width: 180px;"
                id="west">
                <ul id="tree" line="true">
                </ul>
            </div>
            <div region="center">
                <table  id="worker" class="easyui-datagrid" align="center" title="人员" pagination="true"
                    pagenumber="1" pagelist="[10, 15, 20]" pagesize="15" idfield="ID" nowrap="false"
                    striped="true" rownumbers="true" sortname="ID" sortorder="asc" toolbar="#tb"
                    remotesort="false" width="300" fit="true" fitcolumns="true" singleSelect="true">
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
            <div id="tb"  style="padding: 5px; height: auto">
                <table  width="100%">
                    <tr>
                        <td  width="40%">
                            姓名:
                            <input id="txtName" class="easyui-validatebox" style="border: 1px solid #8DB2E3;
                                width: 81px; height: 18px" />
                        </td>
                        <td  width="40%">
                            角色:
                            <select id="role_sel"  class="easyui-combobox"  style="width:100px;">
                            </select>
                        </td>
                        <td  width="20%">
                            <input type="hidden" id="orgId"/>
                            <a href="#" id="btnSearch" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</body>
</html>

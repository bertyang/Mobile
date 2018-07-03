<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edit</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script language="javascript" type="text/javascript">

        function submit() {
            $('#form').form('submit', {
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (msg) {
                    var data = eval('(' + msg + ')'); 
                    if(data.IsSuccess)
                    {
                        $.messager.alert('提示', data.Message, 'info', function () {
                            parent.$('#tabs').tabs('getTab', '功能项管理').find("iframe")[0].contentWindow.location.href =
                                parent.$('#tabs').tabs('getTab', '功能项管理').find("iframe")[0].contentWindow.location.href;
                            CloseCurrentTab();
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


        function GetID() {

            var result;

            $.ajax({
                type: "POST",
                async: false,
                url: "/Organize/GetID/?tableName=B_ACTION",
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


        function IsExistID() {
            if (!$('#ID').attr("readonly")) {
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "/Organize/IsExistID/?tableName=B_ACTION&id=" + $('#ID').val(),
                    success: function (msg) {

                        if (msg.IsSuccess) {
                            $.messager.alert('提示', '已存此ID', 'error');
                            $('#ID').val('');
                        }
                    }
                })
            }
        }

        $(function () {
             <%if( ((dynamic)this.ViewData["entity"]).ID==0) {%>
            $('#ID').val(GetID());
            <% } else {%>
            $("#ID").attr("readonly", true);
            <% }%>
        })

    </script>
</head>
<body class="easyui-layout">
    <div region="center" border="false">
        <div region="north" style="margin-left:30px;margin-top:20px" >
            <span class="editTitle">编辑功能项</span>
        </div>
        <div region="center">
            <form id="form" method="post" action="/Organize/ActionSave/" enctype="application/x-www-form-urlencoded">
            <table width="100%">
                <tr>
                    <td style="text-align: right; width: 120px;">
                        功能项ID(<font color="red">*</font>)：
                    </td>
                    <td>
                         <input type="text" id="ID" class="easyui-validatebox" 
                            required="true" validtype="length[1,11]" 
                            onkeyup="this.value=this.value.replace(/[^\d]/g,'') " 
                            onafterpaste="this.value=this.value.replace(/[^\d]/g,'') "  
                            name="entity.ID" value="<%= ((dynamic)this.ViewData["entity"]).ID  %>"
                            onblur="IsExistID()"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" /> 
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        功能项名称(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" class="easyui-validatebox" required="true" name="entity.Remark"
                            validtype="length[1,50]" value="<%= ((dynamic)this.ViewData["entity"]).Remark %>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        链接：
                    </td>
                    <td>
                        <input type="text" class="easyui-validatebox" name="entity.Url" validtype="length[1,100]"
                            value="<%= ((dynamic)this.ViewData["entity"]).Url==null?"":((dynamic)this.ViewData["entity"]).Url %>"
                            style="border: 1px solid #8DB2E3; width: 400px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        图标：
                    </td>
                    <td>
                        <input type="text" class="easyui-validatebox" name="entity.Icon" validtype="length[1,50]"
                            value="<%= ((dynamic)this.ViewData["entity"]).Icon==null?"":((dynamic)this.ViewData["entity"]).Icon %>"
                            style="border: 1px solid #8DB2E3; width: 400px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        顺序号：
                    </td>
                    <td>
                        <input type="text" class="easyui-numberbox" missingmessage="必须填写数字" 
                            name="entity.OrderID" validtype="length[1,11]" value="<%= ((dynamic)this.ViewData["entity"]).OrderID==null?"":((dynamic)this.ViewData["entity"]).OrderID  %>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />             
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        上级功能项(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" class="easyui-numberbox" missingmessage="必须填写数字" required="true"
                            name="entity.ParentID" validtype="length[1,11]" value="<%= ((dynamic)this.ViewData["entity"]).ParentID==null?"":((dynamic)this.ViewData["entity"]).ParentID  %>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />             
                    </td>
                </tr>
                <tr style="display:none">
                    <td style="text-align: right; width: 120px;">
                        是否启用：
                    </td>
                    <td>
                        <input type="hidden"  name="entity.IsActive"  value="Y" />    
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    <div region="south" border="true" style="text-align: right;height: 40px; line-height: 30px;background-color: #f7f7f7;">
        <table style="width: 100%">
            <tr>
                <td>
                </td>
                <td style="text-align:left" >
                    <a href="#" class="easyui-linkbutton" iconcls="icon-save" onclick="submit();">提交</a>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="CloseCurrentTab();">取消</a>

                </td>
            </tr>
        </table>
    </div>
</body>
</html>

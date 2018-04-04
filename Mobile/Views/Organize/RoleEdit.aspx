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
                        $.messager.alert('提示', data.Message, 'info', function(){
                            window.location.href = "../Role";
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

        $(document).ready(function () {
            //机构
            $('#action').combotree({
                url: '/Organize/ActionLoadTree/',
                onLoadSuccess: function (data) {
                    $('#action').combotree('setValues', [<%=ViewData["action"]%>]);
                }
            });

        });
    </script>
</head>
<body class="easyui-layout">
    <div region="center" border="false">
        <div region="north" style="margin-left:45px;margin-top:20px">
            <span class="editTitle">编辑角色</span>
        </div>
        <div region="center">
            <form id="form" method="post" action="/Organize/RoleSave/" enctype="application/x-www-form-urlencoded">
            <table width="100%">
                <tr style= "display:none ">
                    <td style="text-align: right; width: 120px;">
                        角色ID(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" id="ID" class="easyui-validatebox" required="true" name="entity.ID"
                           value="<%= ((dynamic)this.ViewData["entity"]).ID %>"/>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        角色名称(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" class="easyui-validatebox" required="true" name="entity.Name"
                            validtype="length[1,20]" value="<%= ((dynamic)this.ViewData["entity"]).Name %>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        说明：
                    </td>
                    <td>
                        <input type="text" class="easyui-validatebox" name="entity.Remark" validtype="length[1,50]"
                            value="<%= ((dynamic)this.ViewData["entity"]).Remark==null?"":((dynamic)this.ViewData["entity"]).Remark%>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        功能项：
                    </td>
                    <td>
                        <select id="action"  class="easyui-combotree"  name="action" multiple="true"  style="width:300px;">
                        </select>
                    </td>
                </tr>                
            </table>
            </form>
        </div>
    </div>
    <div region="south" border="true" style="text-align: right; height: 40px; line-height: 30px;
        background-color: #f7f7f7;">
        <table style="width: 100%">
            <tr>
                <td>
                </td>
                <td style="text-align:left">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-save" onclick="submit();">提交</a>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="location.href = document.referrer;">取消</a>

                </td>
            </tr>
        </table>
    </div>
</body>
</html>

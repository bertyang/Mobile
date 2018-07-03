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
                            window.location.href = "../Config";
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

    </script>
</head>
<body class="easyui-layout">
    <div region="center" border="false">
        <div region="north" style="margin-left:30px;margin-top:20px" >
            <span class="editTitle">编辑项目</span>
        </div>
        <div region="center">
            <form id="form" method="post" action="/BasicInfo/ConfigSave/" enctype="application/x-www-form-urlencoded">
            <table width="100%">
                <tr style= "display:none ">
                    <td style="text-align: right; width: 120px;">
                        Key：
                    </td>
                    <td>
                         <input type="text" name="entity.Key" value="<%= ((dynamic)this.ViewData["entity"]).Key %>"></input>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        类别：
                    </td>
                    <td>
                        <lable><%= ((dynamic)this.ViewData["entity"]).Description == null ? "" : ((dynamic)this.ViewData["entity"]).Type%></lable>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        项目：
                    </td>
                    <td>
                        <lable><%= ((dynamic)this.ViewData["entity"]).Description == null ? "" : ((dynamic)this.ViewData["entity"]).Description%></lable>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        值(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" class="easyui-validatebox" required="true" name="entity.value"
                            validtype="length[1,255]" value="<%= ((dynamic)this.ViewData["entity"]).Value %>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>

            </table>
            </form>
        </div>
    </div>
    <div region="south" border="true" style="text-align: right;height: 40px; line-height: 30px;
        background-color: #f7f7f7;">
        <table style="width: 100%">
            <tr>
                <td>
                </td>
                <td style="text-align:left" >
                    <a href="#" class="easyui-linkbutton" iconcls="icon-save" onclick="submit();return false;">提交</a>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="location.href = document.referrer;return false;">取消</a>

                </td>
            </tr>
        </table>
    </div>
</body>
</html>

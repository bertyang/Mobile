<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
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
                    if (data.IsSuccess) {
                        $.messager.alert('提示', data.Message, 'info', function () {
                            window.location.href = "../Data";
                        });
                    }
                    else {
                        $.messager.alert('提示', data.Message, 'info', function () {
                        });

                    }
                }
            });
        }

    </script>
</head>
<body class="easyui-layout">
    <div region="center" border="true">
        <div region="north" style="margin-left:45px;margin-top:20px">
            <span class="editTitle">编辑数据</span>
        </div>
        <div region="center">
            <form id="form" method="post" action="/BasicInfo/DataSave/" enctype="application/x-www-form-urlencoded">
            <table width="100%">
                <tr style= "display:none ">
                    <td style="text-align: right; width: 120px;">
                        编码(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" class="easyui-validatebox" required="true" name="entity.ID" 
                            value="<%= ((dynamic)this.ViewData["entity"]).ID %>" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        名称(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" class="easyui-validatebox" required="true" name="entity.Name"
                            validtype="length[1,20]" value="<%= ((dynamic)this.ViewData["entity"]).Name %>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        类型(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" class="easyui-validatebox" required="true" name="entity.Type"
                            validtype="length[1,50]" value="<%= ((dynamic)this.ViewData["entity"]).Type %>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        值(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" class="easyui-validatebox" required="true" name="entity.Value"
                            validtype="length[1,25]" value="<%= ((dynamic)this.ViewData["entity"]).Value %>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        顺序号(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" class="easyui-validatebox" required="true" name="entity.Sequence"
                            validtype="length[1,4]" value="<%= ((dynamic)this.ViewData["entity"]).Sequence %>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        说明(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" class="easyui-validatebox" required="true" name="entity.Remark"
                            validtype="length[1,25]" value="<%= ((dynamic)this.ViewData["entity"]).Remark %>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
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
                <td style="text-align: LEFT">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-save" onclick="submit();return false;">提交</a>
                    <%--<a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="location.href = document.referrer;return false;">取消</a>--%>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="location.href = '../Data';return false;">取消</a>
                </td>
            </tr>
        </table>
    </div></body>
</html>

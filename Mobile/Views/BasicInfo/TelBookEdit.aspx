<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>TelBookEdit</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script language="javascript" type="text/javascript">

        function submit() {
            $('#form').form('submit', {
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (msg) {
                    $.messager.alert('提示', "保存成功", 'info', function () {
                        window.location.href = '/BasicInfo/TelBook/?OwnerID='+ <%=ViewData["OwnerID"] %>;
                    });
                }
            });
        }  
         $(document).ready(function () {

            //电话分类
                $('#types').combotree({
                    url: '/BasicInfo/TypeTreeLoad/?OwnerID='+ <%=ViewData["OwnerID"] %>,
                    onLoadSuccess: function (data) {
                    <%if ((dynamic)ViewData["TelType"] != -1) {%>
                        $('#types').combotree('setValue', <%=ViewData["TelType"]%>);
                    <%} %>
                    }
                });

            //验证联系电话
            $.extend($.fn.validatebox.defaults.rules, {
                phone: {
                    validator: function (value) {
                        var reg1 = /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/;

                        var reg2 = /^1[2|3|4|5|6|7|8|9]\d{9}$/;

                        return (reg1.test(value) || reg2.test(value));
                    },
                    message: '您输入的格式不正确,正确格式:“11位有效数字”或“0571-88888888”'
                }
            });
        });

        window.onload = function () {
            var iseffect = document.getElementById("effect").value;
            if (iseffect == "True") { document.getElementsByName('entity.是否有效')[0].checked = true; }
            if (iseffect == "False") { document.getElementsByName('entity.是否有效')[1].checked = true; }
        }
                            
    </script>
</head>
<body class="easyui-layout">
    <input id="effect" type="hidden" value="<%= ((dynamic)this.ViewData["entity"]).是否有效 %>" />
    <div region="center" border="false">
        <div region="north" style="margin-left:45px;margin-top:20px">
            <span class="editTitle">编辑通讯录</span>
        </div>
        <div region="center">
            <form id="form" runat="server" method="post" action="/BasicInfo/TelBookSave/" enctype="application/x-www-form-urlencoded">
            <table width="100%">
                <tr style= "display:none ">
                    <td style="text-align: right; width: 120px;">
                        ID(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" class="easyui-validatebox" required="true" name="entity.编码" 
                            value="<%= ((dynamic)this.ViewData["entity"]).编码 %>" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        名称(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" class="easyui-validatebox" required="true" name="entity.名称" validtype="length[1,25]"
                            value="<%= ((dynamic)this.ViewData["entity"]).名称 %>" style="border: 1px solid #8DB2E3;
                            width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        电话分类(<font color="red">*</font>)：
                    </td>
                    <td >                        
                        <select id="types"   class="easyui-combotree" style="width:150px;" name="entity.电话分类编码"></select>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        联系电话一：
                    </td>
                    <td>
                        <input type="text" class="easyui-validatebox"  name="entity.联系电话一" validtype="phone"
                            validtype="length[0,30]" value="<%= ((dynamic)this.ViewData["entity"]).联系电话一==null?"":((dynamic)this.ViewData["entity"]).联系电话一%>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        分机一：
                    </td>
                    <td>
                        <input type="text" name="entity.分机一" validtype="length[0,15]" class="easyui-numberbox"
                            value="<%= ((dynamic)this.ViewData["entity"]).分机一==null?"":((dynamic)this.ViewData["entity"]).分机一%>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        联系电话二：
                    </td>
                    <td>
                        <input type="text" class="easyui-validatebox" validtype="phone" name="entity.联系电话二"
                            validtype="length[0,30]"  value="<%= ((dynamic)this.ViewData["entity"]).联系电话二==null?"":((dynamic)this.ViewData["entity"]).联系电话二%>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        分机二：
                    </td>
                    <td>
                        <input type="text" name="entity.分机二" validtype="length[0,15]" class="easyui-numberbox"
                            value="<%= ((dynamic)this.ViewData["entity"]).分机二==null?"":((dynamic)this.ViewData["entity"]).分机二%>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        备注：
                    </td>
                    <td>
                        <input type="text" name="entity.备注" value="<%= ((dynamic)this.ViewData["entity"]).备注==null?"":((dynamic)this.ViewData["entity"]).备注%>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        顺序号(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" class="easyui-numberbox" required="true" name="entity.顺序号" validtype="length[1,5]"
                            value="<%= ((dynamic)this.ViewData["entity"]).顺序号 %>" style="border: 1px solid #8DB2E3;
                            width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        是否有效(<font color="red">*</font>)：
                    </td>
                    <td>                        
                        <input type="radio" name="entity.是否有效" value="true" />是&nbsp;
                        <input type="radio" name="entity.是否有效" value="false" />否&nbsp;
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
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="location.href = document.referrer;return false;">
                        返回</a>
                </td>
            </tr>
        </table>
    </div></body>
</html>

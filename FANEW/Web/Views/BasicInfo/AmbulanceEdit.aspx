<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>AmbulanceEdit</title>
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
                             window.location.href = "/BasicInfo/Ambulance/";
                        });
                    }
                    else {
                        $.messager.alert('提示', data.Message, 'info', function () {
                        });
                    }
                }
            });
        } 
        $(document).ready(function () { 
         $('#station').combobox({
             url: '/BasicInfo/LoadAllStations/?ActionId=<%=Request.QueryString["ActionId"]%>',
                valueField: '编码',
                textField: '名称',
                onLoadSuccess: function (data) {
                        $('#station').combobox('setValue', '<%=ViewData["station"]%>');
                }
            });
            $('#Type').combobox({
                url: '/BasicInfo/LoadAllTypes/',
                valueField: '编码',
                textField: '名称',
                onLoadSuccess: function (data) {
                        $('#Type').combobox('setValue', <%=ViewData["type"]%>);                   
                }
            });
            $('#Level').combobox({
                url: '/BasicInfo/LoadAllLevels/',
                valueField: '编码',
                textField: '名称',
                onLoadSuccess: function (data) {
                        $('#Level').combobox('setValue', <%=ViewData["level"]%>);
                }
            });

      });            
    </script>
</head>
<body class="easyui-layout">
    <div region="center" border="false">
        <div region="north" style="margin-left:45px;margin-top:20px">
            <span class="editTitle">编辑车辆</span>
        </div>
        <div region="center">
            <form id="form" runat="server" method="post" action="/BasicInfo/AmbulanceSave/" enctype="application/x-www-form-urlencoded">
            <table width="100%">
                <tr style= "display:none ">
                    <td style="text-align: right; width: 120px;">
                        ID(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" class="easyui-validatebox" required="true" name="entity.车辆编码" 
                            value="<%= ((dynamic)this.ViewData["entity"]).车辆编码 %>" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        分站(<font color="red">*</font>)：
                    </td>
                    <td>
                        <select id="station"  class="easyui-combobox" required="true" name="entity.分站编码"   style="width:150px;">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        随车电话(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" name="entity.随车电话" validtype="length[1,14]" class="easyui-numberbox"
                            required="true" value="<%= ((dynamic)this.ViewData["entity"]).随车电话==null?"":((dynamic)this.ViewData["entity"]).随车电话%>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        实际标识(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" name="entity.实际标识" validtype="length[1,10]" required="true" value="<%= ((dynamic)this.ViewData["entity"]).实际标识==null?"":((dynamic)this.ViewData["entity"]).实际标识%>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        车牌号(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" name="entity.车牌号码" validtype="length[1,10]" required="true" value="<%= ((dynamic)this.ViewData["entity"]).车牌号码==null?"":((dynamic)this.ViewData["entity"]).车牌号码%>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        车辆类型(<font color="red">*</font>)：
                    </td>
                    <td>
                        <select id="Type"  class="easyui-combobox" required="true" name="entity.车辆类型编码" style="width:150px;">
                        </select>
                    </td>
                </tr>
<%--                <tr>
                    <td style="text-align: right; width: 120px;">
                        车辆分组(<font color="red">*</font>)：
                    </td>
                    <td>
                        <select id="GroupNumber"  class="easyui-combobox" required="true" name="entity.分组编码" style="width:150px;">
                        </select>
                    </td>
                </tr>--%>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        车辆等级(<font color="red">*</font>)：
                    </td>
                    <td>
                        <select id="Level"  class="easyui-combobox" required="true" name="entity.车辆等级编码"   style="width:150px;">
                        </select>
                    </td>
                </tr>
<%--                <tr>
                    <td style="text-align: right; width: 120px;">
                        是否有效：
                    </td>
                    <td>
                        <checkbox id="IsActive" name="entity.是否有效"></checkbox>
                    </td>
                </tr>--%>
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
    </div>
</body>
</html>

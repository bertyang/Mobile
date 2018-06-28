<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>通话类型修改</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript" language="javascript">
        var MePar;
        $(function () {
            MePar={
                    EventCode: '<%:ViewData["EventCode"]%>',
                    Desk: '<%:ViewData["Desk"]%>',
                    Time: '<%:ViewData["Time"]%>',
                    type: '<%:ViewData["type"]%>'
                };
            //#region 下拉菜单初始化
//            关联到事件相关
<%  
if ((ViewData["type"]).ToString()=="0")
{ 
%>
            //取消关联到事件
            EUIcombobox("#txt_CallType", {
                valueField: "编码",
                textField: "名称",
                OneOption: [{
                    编码: "",
                    名称: "--请选择--"
                }],
                url: "/BasicInfo/LoadCallType/"

            });
<%} 
else
{%>
            //关联到事件相关
            EUIcombobox("#txt_CallType", {
                            valueField: "编码",
                            textField: "名称",
                            OneOption: [{
                                编码: "",
                                名称: "--请选择--"
                            }],
                            url: "/BasicInfo/LoadAlarmCallType/"

                        });
<%}%>
            //#endregion

        });

        function Save() {
            var t = $("#txt_CallType").combobox('getValue');
            if (t == "") {
                alert("未选择电话类型");
                return false;
            }
            MePar.CallType = t;
            //alert(MePar);
<%  
if ((ViewData["type"]).ToString()=="0")
{ 
%>
            $.ajax({
                type: "POST",
                async: false,
                url: "/BasicInfo/UnLinkCalls/",
                dataType: "JSON",
                data: MePar,
                success: function (data, textStatus, jqXHR) {
                    $("#SaveResult").empty();
                    if (data == "") {
                        parent.freload();
                        parent.$AlterCallTypeWindow.window('close');
                    } else {
                        $("#SaveResult").append($("<font color='red'>写入数据库失败！" + data + "</font>"));
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('/BasicInfo/UnLinkCalls/');
                }
            });
<%} 
else
{%>
            $.ajax({
                type: "POST",
                async: false,
                url: "/BasicInfo/LinkCalls/",
                dataType: "JSON",
                data: MePar,
                success: function (data, textStatus, jqXHR) {
                    $("#SaveResult").empty();
                    if (data == "") {
                        parent.Search('reload');
                        parent.$LinkCallsWindow.window('close');
                    } else {
                        $("#SaveResult").append($("<font color='red'>写入数据库失败！" + data + "</font>"));
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('/BasicInfo/UnLinkCalls/');
                }
            });

<%}%>
        }
    </script>
</head>
<body>
    <div align="center">
        <table width="100%" border="0" cellspacing="6" cellpadding="0" style="border-collapse: separate;
            border-spacing: 6px;">
            <tr valign="top">
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="6" class="blockTable">
                        <tr>
                            <td align="left" valign="middle" class="blockTd">
                                <%--<img align="top" src="../image/index/icon/icon002a1.gif" width="20" height="20" />--%>
                                通话类型修改
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 0 8px 4px;" align="left">
                                <table style="width: 100%;">
                                    <tr>
                                        <td align="right" valign="middle" width="22%">
                                            &nbsp;新类型：</td>
                                        <td width="78%">
                                            &nbsp;
                                            <input class="easyui-combobox" style="width: 150px;" panelheight="250px" id="txt_CallType"
                                            editable="false" />
                                        </td>
                                    </tr>
                                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 2px; padding-left: 6px; padding-right: 6px; padding-bottom: 2px;">
                                <div id="SaveResult"></div>
                                <asp:Label ID="Label_Info" runat="server" Text=""></asp:Label>
                                <br />
                                <a href="#" class="easyui-linkbutton" iconcls="icon-save" plain="true"
                                onclick="javascript:Save();return false;"><span style="color: #15428B;">&nbsp;修改</span></a>
                                &nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>

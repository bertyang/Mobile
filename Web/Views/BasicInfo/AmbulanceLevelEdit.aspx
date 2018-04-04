<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

    <% 
        var item = ViewData["entity"];
        Type itemType = item.GetType();
        
    %>
<!DOCTYPE html>
<html>
<head>
    <title>AmbulanceLevelEdit</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript">
        function closePage() {
            parent.freload();
            parent.$AmbulanceLevelEditWindow.window('close');
        }
    $(function () {
        //#region 下拉菜单初始化
        //车辆等级
        EUIcombobox("#DropDownList_AmbLevel", {
            valueField: "编码",
            textField: "名称",
            OneOption: [{
                编码: "-1",
                名称: "--请选择--"
            }],
            onLoadSuccess: function (data) {
                $('#DropDownList_AmbLevel').combobox('setValue', '<%= itemType.GetProperty("车辆等级编码").GetValue(item, null)%>');
            },
            url: "/BasicInfo/LoadAllLevels/"
        });
        //#endregion
    });
        function Save() {
            var LevelId = escape($("#DropDownList_AmbLevel").combobox('getValue'));
            if (LevelId == "-1") {
//                alert("请选择车辆等级");
                $("#SaveResult").empty();
                $("#SaveResult").append($("<font color='red'>请选择车辆等级！</font>"));
                return false;
            }
            var paramNames = "Code=" + '<%= itemType.GetProperty("车辆编码").GetValue(item, null)%>'
            + "&LevelId=" + LevelId;
            //alert(paramNames);
            $.ajax({
                type: "POST",
                async: false,
                url: "/BasicInfo/AmbulanceLevelSave/",
                data: paramNames,
                success: function (data1, textStatus, jqXHR) {
                    //alert(data);
                    var data = eval('(' + data1 + ')');
                    $("#SaveResult").empty();
                    if (data.IsSuccess) {
                        $("#SaveResult").append($("<font color='green'>写入数据库成功！</font>"));
                    } else {
                        $("#SaveResult").append($("<font color='red'>写入数据库失败！</font>"));
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("/BasicInfo/AmbulanceLevelSave/");
                }
            });
        }

    </script>
    <style type="text/css">
        .TableInfoTH
        {
            background: #FFFFFF repeat-x left top;
            height: 25px;
        }
        .TableInfoTD
        {
            background-color: #F0F0F0;
        }
        /*.TableInfoTD td
        {
            border-width: 0 1px 1px 0;
            border-style: dotted;
            margin: 0;
            padding: 0;
        }*/
        tr
        {
            height: 22px;
        }
        *
        {
            font-size: 12px;
        }
    </style>
</head>
<body>
    <div align="center">
    <% 
        //var item = ViewData["entity"];
        //Type itemType = item.GetType();
        
    %>
        <table width="100%" border="0" cellspacing="6" cellpadding="0" style="border-collapse: separate;
            border-spacing: 6px;">
            <tr valign="top">
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="6" class="blockTable">
                        <tr>
                            <td style="padding: 0 8px 4px;" align="center">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tableInfo">
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="middle">
                                            编码
                                        </td>
                                        <td align="left" valign="middle">
                                            &nbsp;
                                            <%= itemType.GetProperty("车辆编码").GetValue(item, null)%>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="middle" class="TableInfoTD">
                                            车牌号码
                                        </td>
                                        <td align="left" valign="middle" class="TableInfoTD" id="Name">
                                            &nbsp;
                                            <%= itemType.GetProperty("车牌号码").GetValue(item, null)%>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="middle">
                                            实际标识
                                        </td>
                                        <td align="left" valign="middle">
                                            &nbsp;
                                            <%= itemType.GetProperty("实际标识").GetValue(item, null)%>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="middle" class="TableInfoTD">
                                            所属分站
                                        </td>
                                        <td align="left" valign="middle" class="TableInfoTD">
                                            &nbsp;
                                            <%= itemType.GetProperty("所属分站").GetValue(item, null)%>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="middle">
                                            随车电话
                                        </td>
                                        <td align="left" valign="middle">
                                            &nbsp;
                                            <%= itemType.GetProperty("随车电话").GetValue(item, null)%>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="middle" class="TableInfoTD">
                                            车辆等级
                                        </td>
                                        <td align="left" valign="middle" class="TableInfoTD">
                                            &nbsp;
                                            <input class="easyui-combobox" style="width: 150px;" panelheight="150px" id="DropDownList_AmbLevel"
                                                editable="false" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="middle">
                                            &nbsp;
                                        </td>
                                        <td align="right" valign="middle">
                                        <span id="SaveResult"></span>
                                            &nbsp;<a href="#" class="easyui-linkbutton"  plain="true"
                                                onclick="javascript:Save();return false;"><span style="color: #15428B;">&nbsp;确认</span></a>
                                            &nbsp;<a href="#" class="easyui-linkbutton" plain="true"
                                                onclick="javascript:closePage();return false;"><span style="color: #15428B;">&nbsp;返回并刷新</span></a>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 2px; padding-left: 6px; padding-right: 6px; padding-bottom: 2px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>

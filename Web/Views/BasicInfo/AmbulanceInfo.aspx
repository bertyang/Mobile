<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<!DOCTYPE html>
<html>
<head>
    <title>AmbulanceInfo</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript">
        function closePage() {
//            parent.freload();
            parent.$ViewWindow.window('close');
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
      <%--  .This_header
        {
            border-color: #95B8E7;
            background-color: #E0ECFF;
            background: -webkit-linear-gradient(top,#EFF5FF 0,#E0ECFF 100%);
            background: -moz-linear-gradient(top,#EFF5FF 0,#E0ECFF 100%);
            background: -o-linear-gradient(top,#EFF5FF 0,#E0ECFF 100%);
            background: linear-gradient(to bottom,#EFF5FF 0,#E0ECFF 100%);
            background-repeat: repeat-x;
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr=#EFF5FF,endColorstr=#E0ECFF,GradientType=0);
            padding: 5px;
            position: relative;
            border-width: 1px;
            border-style: solid;
            height: 16px;
            text-align: left;
        }
        .This_title
        {
            font-size: 12px;
            color: #0E2D5F;
            font-weight: bold;
        }--%>
    </style>
</head>
<body>
    <div align="center">
    <% 
        var item = ViewData["entity"];
        Type itemType = item.GetType();
        
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
                                            车辆类型
                                        </td>
                                        <td align="left" valign="middle">
                                            &nbsp;
                                            <%= itemType.GetProperty("车辆类型").GetValue(item, null)%>
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
                                            命令单发送去向
                                        </td>
                                        <td align="left" valign="middle" class="TableInfoTD">
                                            &nbsp;
                                            <%= itemType.GetProperty("命令单发送去向").GetValue(item, null)%>
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
                                            分组
                                        </td>
                                        <td align="left" valign="middle">
                                            &nbsp;
                                            <%= itemType.GetProperty("分组").GetValue(item, null)%>
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
                                            工作状态
                                        </td>
                                        <td align="left" valign="middle" class="TableInfoTD">
                                            &nbsp;&nbsp;
                                            <%= itemType.GetProperty("工作状态").GetValue(item, null)%>
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
                                            车辆等级
                                        </td>
                                        <td align="left" valign="middle">
                                            &nbsp;
                                            <%= itemType.GetProperty("车辆等级").GetValue(item, null)%>
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
                                            随车电话
                                        </td>
                                        <td align="left" valign="middle" class="TableInfoTD">
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
                                        <td align="left" valign="middle">
                                            当班执行任务数
                                        </td>
                                        <td align="left" valign="middle">
                                            &nbsp;&nbsp;
                                            <%= itemType.GetProperty("当班执行任务数").GetValue(item, null)%>
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
                                            司机
                                        </td>
                                        <td align="left" valign="middle" class="TableInfoTD">
                                            &nbsp;
                                            <%= itemType.GetProperty("司机").GetValue(item, null)%>
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
                                            医生
                                        </td>
                                        <td align="left" valign="middle">
                                            &nbsp;
                                            <%= itemType.GetProperty("医生").GetValue(item, null)%>
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
                                            护士
                                        </td>
                                        <td align="left" valign="middle" class="TableInfoTD">
                                            &nbsp;&nbsp;
                                            <%= itemType.GetProperty("护士").GetValue(item, null)%>
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
                                            担架工
                                        </td>
                                        <td align="left" valign="middle">
                                            &nbsp;&nbsp;
                                            <%= itemType.GetProperty("担架工").GetValue(item, null)%>
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
                                            抢救员
                                        </td>
                                        <td align="left" valign="middle" class="TableInfoTD">
                                            &nbsp;&nbsp;
                                            <%= itemType.GetProperty("抢救员").GetValue(item, null)%>
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
                                            <a href="#" class="easyui-linkbutton" plain="true"
                                                onclick="closePage();return false;"><span style="color: #15428B;">&nbsp;返回</span></a>
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

<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title></title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
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
        .This_header
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
        }
    </style>
    <script type="text/javascript" language="javascript">
    </script>
</head>
<body>
    <div>
            <% 
            List<Anchor.FA.Model.C_ModifyRecord> mrLs = ViewData["mrLs"] as List<Anchor.FA.Model.C_ModifyRecord>;//修改记录
        %>


                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="left">
                                            [共<%: ViewData["total"]%>条数据]

                                        </td>
                                    </tr>
                                </table>
                                
                                            <table id="AlarmCallGrid" class="easyui-datagrid" align="center" fitcolumns="true"
                                                nowrap="false" striped="false" remotesort="false" fit="true" width="100%">
                                                <thead>
                                                    <tr>
                                                        <th field="修改类型" align="center">
                                                            修改项
                                                        </th>
                                                        <th field="修改前内容" align="center">
                                                            修改前内容
                                                        </th>
                                                        <th field="修改后内容" align="center">
                                                            修改后内容
                                                        </th>
                                                        <th field="操作员" align="center">
                                                            操作员
                                                        </th>
                                                        <th field="产生时刻" align="center">
                                                            修改时刻
                                                        </th>
                                                        <th field="受理序号" align="center">
                                                            受理序号
                                                        </th>
                                                        <th field="任务编码" align="center">
                                                            任务编码
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <% 
                                                        foreach (Anchor.FA.Model.C_ModifyRecord ac in mrLs)
                                                        {
                                                    %>
                                                    <tr>
                                                        <td>
                                                            <%: ac.修改类型%>
                                                        </td>
                                                        <td>
                                                            <%: ac.tmr.修改前内容%>
                                                        </td>
                                                        <td>
                                                            <%: ac.tmr.修改后内容%>
                                                        </td>
                                                        <td>
                                                            <%: ac.操作员%>
                                                        </td>
                                                        <td>
                                                            <%: ac.tmr.产生时刻%><%--<%: ac.tmr.产生时刻.ToString()%>--%>
                                                        </td>
                                                        <td>
                                                            <%: ac.tmr.受理序号%>
                                                        </td>
                                                        <td>
                                                            <%: ac.tmr.任务编码%>
                                                        </td>
                                                    </tr>
                                                    <%} %>
                                                </tbody>
                                            </table>
    </div>
</body>
</html>

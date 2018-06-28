<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>安克医疗急救办公管理系统</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript" language="javascript">
        var data;
        $(function () {
            $('#tree').tree({
                url: '/Organize/UnitTree/?orgId=<%=ViewData["orgId"] %>',

//                onBeforeExpand: function (node, param) {
//                    $('#tree').tree('options').url = "/Organize/GetGoupJson/?id=" + node.id;
//                },

                onClick: function (node) {
                    var tabTitle = node.text;
//                    if (node.iconCls == "icon tu1605") {
//                        var urlw = "/Organize/WorkerPage/";
//                        $('#frame').attr("src", urlw);
//                    }
//                    if (node.iconCls != "icon tu1605") {
                        var urlw = "/Organize/WorkerPage/?orgId=" + node.id;
                        $('#frame').attr("src", urlw);
//                    }
//                    if (node.iconCls == "icon tu0825") {
//                        var urlw = "/Organize/WorkerEdit/?id=" + node.id;
//                        $('#frame').attr("src", urlw);
//                    }
                }
            });
        });

    </script>
</head>
<body class="easyui-layout" style="overflow-y: hidden" scroll="no" fit="true">
    <div region="west" split="true" title="人员管理" iconcls="icon icon-sys" style="width: 180px;"  id="west">
        <ul id="tree" line="true">
        </ul>
    </div>
    <div region="center" border="true" style="overflow-y: hidden"  title="人员信息" >
        <iframe id="frame" scrolling="auto" frameborder="0"  src="/Organize/WorkerPage/?orgId=<%=ViewData["orgId"] %>" style="width:100%;height:100%;"></iframe>
    </div>
</body>
</html>

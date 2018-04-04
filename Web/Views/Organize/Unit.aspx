<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>安克医疗急救办公管理系统</title>
<%--    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="Pragma" CONTENT="no-cache"> 
    <meta http-equiv="Cache-Control" CONTENT="no-cache"> 
    <meta http-equiv="Expires" CONTENT="0"> --%>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>

    <script type="text/javascript" language="javascript">
        var data;
        //初始化组织机构树
        $(function () {
            // GetTreeJson();
            // $('#tree').tree('loadData', data);
            $('#tree').tree({
                url: '/Organize/UnitTree/?orgId=<%=ViewData["orgId"] %>',
                onClick: function (node) {
                    if (node.attributes != "") {
                        $('#frame').attr("src", node.attributes + node.id);
                    }
                }
            });
        });

//        //添加选项卡
//        function addTab(subtitle, url, icon) {
//            if (!$('#tabs').tabs('exists', subtitle)) {
//                $('#tabs').tabs('add', {
//                    title: subtitle,
//                    content: createFrame(url),
//                    closable: true,
//                    icon: icon
//                });
//            } else {
//                $('#tabs').tabs('select', subtitle);
//                var currTab = $('#tabs').tabs('getSelected');
//                var url = $(currTab.panel('options').content).attr('src');
//                $('#tabs').tabs('update', {
//                    tab: currTab,
//                    options: {
//                        content: createFrame(url)
//                    }
//                })
//            }

//        }
//        function createFrame(url) {
//            var s = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
//            return s;
//        }
//        //获取菜单Json
//        function GetTreeJson() {
//            $.ajax({
//                type: 'POST',
//                url: '../Organize/GetMenu',
//                data: "",
//                async: false,
//                dataType: "json",
//                error: function (msg) { alert(msg + '获取数据错误！！！'); },
//                success: function (msg) {
//                    if (msg.InfoID == "0") {
//                        msgShow("提示", msg.InfoMessage, "error");
//                    }
//                    else {
//                        data = msg;
//                    }
//                }
//            });
//        }
    </script>
</head>
<body class="easyui-layout" style="overflow-y: hidden" scroll="no" fit="true">
    <div region="west" split="true" title="组织机构" iconcls="icon tu1911" style="width: 180px;"  id="west">
        <ul id="tree" line="true">
        </ul>
    </div>
    <div region="center"  style="overflow-y: hidden" >
        <iframe id="frame" scrolling="auto" frameborder="0" style="width: 100%; height: 100%;" >
        </iframe>
    </div>    
</body>
</html>

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
                url: '/Notice/TJTree?exceptUnitId=<%=Request.QueryString["ActionId"]%>',
                onClick: function (node) {
                //alert(node.id);
                    // var tabTitle = node.text;
                    //              var tabTitle = node.url;
                    var url = node.id;

                    //为链接老版FA页面加参数
                    if (url.indexOf("#V7#") > -1) {
                        $.ajax({
                            type: 'POST',
                            url: "/Account/GetEmpNoAndPassWord/",
                            data: "",
                            async: false,
                            dataType: "json",
                            error: function (msg) { alert('获取数据错误！！！'); },
                            success: function (msg) {
                                url = url.replace("#V7#", "WorkID=" + msg.EmpNo + "&PassWord=" + msg.PassWord);
                            }
                        });
                    }

                    //$('#frame').attr("src", url);
                    if (url.indexOf("open=1") > -1) {
                        window.open(encodeURI(url)); //打开新窗口
                        return;
                    }

                    $('#frame').attr("src", url);
                }
            });
        });

   
    </script>
    <style type="text/css">
        #tree li 
        {
            padding:10px 0
        }
    </style>
</head>
<body class="easyui-layout" style="overflow-y: hidden" scroll="no" fit="true">
    <div region="west" split="true" title="统计" iconcls="icon tu1718" style="width: 180px; "
        id="west">
        <ul id="tree" line="true" >
        </ul>
    </div>
    <div region="center" border="true" style="overflow-y: hidden" title="统计">
        <iframe id="frame" scrolling="auto" frameborder="0" src="" style="width: 100%; height: 100%;">
        </iframe>
    </div>
</body>
</html>

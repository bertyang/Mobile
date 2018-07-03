<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>QuickTimePlayer</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript" language="javascript">


        function closePage() {
            parent.autoClose();
        }


        //$(window).on('beforeunload', function () {
        //    parent.autoClose();
        //});
    </script>
</head>
<body>
    <table width="100%" border="0" cellspacing="0" cellpadding="6" class="blockTable">
        <tr>
            <td style="padding: 0 8px 4px;" align="center">
                <table style="font: 12pt; width: 100%;">
                    <tr>
                        <td style="height: 68px">
                            <object CLASSID="clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B" id="MediaPlayer1" width="90%" height="68px" CODEBASE="http://www.apple.com/qtactivex/qtplugin.cab">
                                            
                                            <param name="src" value="<%= ViewData["strMapPath"] %>">
                                            <param name="autoplay" value="true">
                                            <param name="loop" value="true">
                                            <param name="controller" value="true">
                             </object>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle">
							<a href="#" class="easyui-linkbutton" iconcls="icon-back" onclick="closePage()">返回</a>                           
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>

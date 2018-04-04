<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoCase_Mode.aspx.cs" Inherits="Anchor.FA.Web.Report.NoCase_Mode" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%: Styles.Render( "~/Content/css") %>   
    <%: Scripts.Render("~/bundles/js")%>
    <link href="~/Content/CSS/default.css" rel="stylesheet" type="text/css" />
    <link href="~/Content/CSS/Ajax.css" rel="Stylesheet" type="text/css" />
    
    <script type="text/javascript" language="javascript">
        $(function () {
            $("#MyReportViewer").height($(document.body).height() - 50);
            $("#MyReportViewer").width($(document.body).width() - 20);
            //                document.getElementById("MyReportViewer").style.height = document.body.clientHeight - document.getElementById("PanelSelect").clientHeight - 50;
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div style="min-width: 700px;">
        <rsweb:ReportViewer ID="MyReportViewer" runat="server" ShowBackButton="True" Width="100%" Height="100%"  >
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>

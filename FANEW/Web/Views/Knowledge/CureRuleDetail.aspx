<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
</head>
<body class="easyui-layout" >
    <div region="north" style="width:100%; height:60px; ">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="10px"></td>
            </tr>
            <tr>
                <td style="text-align:center; font-size:x-large; font-family:宋体; height:30px"> 
                    急症描述
                </td>
            </tr>
            <tr>
                <td  height="10"></td>
            </tr>
         </table>
    </div>
    <div region="center" style="width: 100%; height: 100%; overflow-x: hidden">
        <textarea id="ch" name="Character" rows="100" cols="100" style="font-size:14px;width:100%; height:auto"><%= (this.ViewData["Description"])%></textarea>
    </div>
</body>
</html>




<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-

transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>应急处理</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%> 
    <script type="text/javascript" language="javascript">

        $(function () {

            //查询
            $('#btnSearch').bind('click',
                    function () {
                        window.location = "/Knowledge/CureRule/?Name=" + escape($('#name').val());
                    }
               );
         });

         //名称列表点击事件
         function Selected() {
             var url = "/Knowledge/CureRuleDetail/?id=" + $("#Chinese").val();
             $('#frame').attr("src", url);
         }
    </script>
</head>

<body class="easyui-layout"> 
    <div region="west" split="true"  title="急症列表" style=" width:180px; height:100%; overflow:hidden; border:true">
        <div region="north" style="height:50px;border:true" >
            <table width="100%" cellspacing="0" cellpadding="6">
                <tr>
                    <td>
                        <input type="text" id="name" class="easyui-validatebox" style="border: 1px solid #8DB2E3;
                            width: 121px; height: 18px" />
                        <a href="#" id="btnSearch" class="easyui-linkbutton" iconCls="icon-search" plain=true></a>
                    </td>
                </tr>
            </table>
        </div>

        <div region="center" id="listname" style="width:100%; height:100%;overflow-y:auto;border:solid 1px black">
            <select id="Chinese" multiple="multiple" style="width: 100%; height: 88%" onchange="Selected();">
                <% foreach (var entity in ((dynamic)ViewData["cureRule"])){ %>
                <option value="<%=entity.编码%>" style="color:#08298A"><%=entity.疾病名称%></option>
                 <% } %>
            </select>
        </div>
    </div>
    <div region="center" border="true" style="overflow-y: hidden"  title="处理常识" >
        <iframe id="frame" scrolling="auto" frameborder="0"  src="/Knowledge/CureRuleDetail/?id=<%=ViewData["id"]%>" style="width:100%;height:100%;"></iframe>
    </div>
</body>
</html>

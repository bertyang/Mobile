<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-

transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%> 
    <script type="text/javascript" language="javascript">

        $(function () { 

            //radio框显示，并据此显示名称列表
            if (<%=ViewData["type"]%>=="1"){
                document.getElementById("ch").checked = true;
                document.getElementById("Chinese").style.display = "inline";
                document.getElementById("English").style.display = "none";
            }
            if (<%=ViewData["type"]%>=="0"){
                document.getElementById("en").checked = true;
                document.getElementById("Chinese").style.display = "none";
                document.getElementById("English").style.display = "inline";
            }

            //点击"中文"，显示中文名称列表
            $('#ch').bind('click',
                    function () {
                        document.getElementById("Chinese").style.display = "inline";
                        document.getElementById("English").style.display = "none";
                    }
               );

            //点击"英文"，显示英文名称列表
            $('#en').bind('click',
                    function () {
                        document.getElementById("Chinese").style.display = "none";
                        document.getElementById("English").style.display = "inline";
                    }
               );

            //查询
            $('#btnSearch').bind('click',
                    function () {
                        if (document.getElementById('ch').checked == true) {
                            window.location = "/Knowledge/Poison/?type=1&chineseName=" + escape($('#name').val());
                        }
                        else {
                            window.location = "/Knowledge/Poison/?type=0&englishName=" + escape($('#name').val());
                        }
                    }
               );
        });

        //中英文名称列表点击事件
        function Selected() {
                if (document.getElementById('ch').checked == true) {
                    var url = "/Knowledge/PoisonDetail/?id=" + $("#Chinese").val();
                    $('#frame').attr("src", url);
                }
                else {
                    var url = "/Knowledge/PoisonDetail/?id=" + $("#English").val();
                    $('#frame').attr("src", url);
                }                   
        }

    </script>
</head>

<body class="easyui-layout"> 
    <div region="west" split="true"  title="毒品列表" style=" width:180px; height:100%; overflow:hidden; border:true">
        <div region="north" style="height:80px;border:true" >
            <table width="100%" cellspacing="0" cellpadding="6">
                <tr>
                    <td>
                        <input type="text" id="name" class="easyui-validatebox" style="border: 1px solid #8DB2E3;
                            width: 121px; height: 18px" />
                        <a href="#" id="btnSearch" class="easyui-linkbutton" iconCls="icon-search" plain=true></a>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right">
                        <input type="radio" id="ch" name="language" value="1" />中文&nbsp;
                        <input type="radio" id="en" name="language" value="0" />英文
                    </td>
                </tr>
            </table>
        </div>

        <div region="center"style="width:100%; height:100%; overflow-y:auto;border:solid 1px black">
            <select id="Chinese" multiple="multiple" style="width: 100%; height: 82%; display: none" onchange="Selected();">
                <% foreach (var entity in ((dynamic)ViewData["poison"])){ %>
                <option value="<%=entity.序号%>" style="color:#08298A"><%=entity.中文名称%></option>
                 <% } %>
            </select>

             <select id="English" multiple="multiple" style="width:100%; height:100%; display:none" onchange="Selected();">
                 <% foreach (var entity in ((dynamic)ViewData["poison"])){ %>
                 <option value="<%=entity.序号%>" style="color:#08298A"><%=entity.英文名称%></option>
                 <% } %>
            </select>
        </div>
    </div>
    <div region="center" border="true" style="overflow-y: hidden"  title="毒品详细内容" >
        <iframe id="frame" scrolling="auto" frameborder="0"  src="/Knowledge/PoisonDetail/?id=<%=ViewData["id"]%>" style="width:100%;height:100%;"></iframe>
    </div>
</body>
</html>

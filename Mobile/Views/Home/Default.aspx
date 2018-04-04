<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Default</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript">
        function openit(target,href) {
              //var text = $(target).text();
             //$('#p2-title').html(text);
            //$('#frame1').attr("src", href);
            document.getElementById("p2").innerHTML = '<iframe height="100%" width="100%"   marginheight="0" marginwidth="0" scrolling="auto" frameborder="0" src="' + href + '"></iframe>'
           $.mobile.go('#p2');
              //$('#p2').navpanel('refresh', href);

          }
    </script>
</head>
<body>
    <div class="easyui-navpanel">
        <ul class="m-list">
            <li><a href="#notices">通知公告</a></li>
            <li><a href="#news">中心动态</a></li>
            <li><a href="#localFile">中心文件</a></li>
            <li><a href="#superiorFile">上级文件 </a></li>
            <li><a href="#column">党务，政务公开栏</a></li>
            <li><a href="#law">法律法规</a></li>
            <li><a href="#download">资料下载</a></li>
        </ul>
    </div>
    <div id="notices" class="easyui-navpanel">
        <ul class="m-list">
            <% foreach (var entity in ((dynamic)ViewData["Office-2"]))
                   { %>
            <li><a href="javascript:void(0)" title="<%=entity.标题%>" onclick="openit(this,'/Office/OfficeDetail/?officeId=<%=entity.编码%>');">
                <%=entity.标题%> (<%=entity.创建时间.ToString("yyyy-MM-dd")%>)
            </a>
            </li>
            <% } %>
        </ul>
    </div>
    <div id="news" class="easyui-navpanel">
        <ul class="m-list">
            <% foreach (var entity in ((dynamic)ViewData["Office-1"]))
                   { %>
            <li><a href="javascript:void(0)" title="<%=entity.标题%>" onclick="openit(this,'/Office/OfficeDetail/?officeId=<%=entity.编码%>');">
                <%=entity.标题%> (<%=entity.创建时间.ToString("yyyy-MM-dd")%>)
            </a>
            </li>
            <% } %>
        </ul>
    </div>
    <div id="localFile" class="easyui-navpanel">
        <ul class="m-list">
            <% foreach (var entity in ((dynamic)ViewData["Office-4"]))
                   { %>
            <li><a href="javascript:void(0)" title="<%=entity.标题%>" onclick="openit(this,'/Office/OfficeDetail/?officeId=<%=entity.编码%>');">
                <%=entity.标题%> (<%=entity.创建时间.ToString("yyyy-MM-dd")%>)
            </a>
            </li>
            <% } %>
        </ul>
    </div>
    <div id="superiorFile" class="easyui-navpanel">
        <ul class="m-list">
            <% foreach (var entity in ((dynamic)ViewData["Office-5"]))
                { %>
            <li>
                <a href="javascript:void(0)" title="<%=entity.标题%>" onclick="openit(this,'/Office/OfficeDetail/?officeId=<%=entity.编码%>');">
                    <%=entity.标题%>  (<%=entity.创建时间.ToString("yyyy-MM-dd")%>)
                </a>
            </li>
            <% } %>
        </ul>
    </div>
    <div id="law" class="easyui-navpanel">
        <ul class="m-list">
            <% foreach (var entity in ((dynamic)ViewData["Office-3"]))
                { %>
            <li>
                <a href="javascript:void(0)" title="<%=entity.标题%>" onclick="openit(this,'/Office/OfficeDetail/?officeId=<%=entity.编码%>');">
                    <%=entity.标题%>  (<%=entity.创建时间.ToString("yyyy-MM-dd")%>)
                </a>
            </li>
            <% } %>
        </ul>
    </div>
    <div id="download" class="easyui-navpanel">
        <ul class="m-list">
            <% foreach (var entity in ((dynamic)ViewData["Office-7"]))
                { %>
            <li>
                <a href="javascript:void(0)" title="<%=entity.标题%>" onclick="openit(this,'/Office/OfficeDetail/?officeId=<%=entity.编码%>');">
                    <%=entity.标题%>  (<%=entity.创建时间.ToString("yyyy-MM-dd")%>)
                </a>
            </li>
            <% } %>
        </ul>
    </div>
    <div id="column" class="easyui-navpanel">
        <ul class="m-list">
            <% foreach (var entity in ((dynamic)ViewData["Office-6"]))
                { %>
            <li>
                <a href="javascript:void(0)" title="<%=entity.标题%>" onclick="openit(this,'/Office/OfficeDetail/?officeId=<%=entity.编码%>');">
                    <%=entity.标题%>  (<%=entity.创建时间.ToString("yyyy-MM-dd")%>)
                </a>
            </li>
            <% } %>
        </ul>
    </div>

    <div id="p2" class="easyui-navpanel">
        <iframe id="frame1"  height="100%" width="100%"   marginheight="0" marginwidth="0" scrolling="auto" frameborder="0"></iframe>
    </div>
</body>
</html>

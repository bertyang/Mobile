<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Default</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript">
        function openit(target, href) {
            var text = $(target).attr("title");
            $('#title').html(text);
            $("#detail").html('<iframe height="100%" width="100%"   marginheight="0" marginwidth="0" scrolling="auto" frameborder="0" src="' + href + '"></iframe>');
            $.mobile.go('#detail');
          }
    </script>
</head>
<body>
    <div class="easyui-navpanel">
        <header>
            <div class="m-toolbar">
                <div class="m-title">公告</div>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>
            </div>
        </header>
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
        <header>
            <div class="m-toolbar">
                <div class="m-title">通知公告</div>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>
            </div>
        </header>
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
        <header>
            <div class="m-toolbar">
                <div class="m-title">中心动态</div>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>
            </div>
        </header>
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
        <header>
            <div class="m-toolbar">
                <div class="m-title">中心文件</div>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>
            </div>
        </header>
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
        <header>
            <div class="m-toolbar">
                <div class="m-title">上级文件</div>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>
            </div>
        </header>
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
        <header>
            <div class="m-toolbar">
                <div class="m-title">法律法规</div>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>
            </div>
        </header>
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
        <header>
            <div class="m-toolbar">
                <div class="m-title">资料下载</div>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>
            </div>
        </header>
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
        <header>
            <div class="m-toolbar">
                <div class="m-title">党务，政务公开栏</div>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>
            </div>
        </header>
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

    <div id="detail" class="easyui-navpanel">
         <header>
            <div class="m-toolbar">
                <div id="title" class="m-title">内容</div>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>
            </div>
        </header>
    </div>
</body>
</html>

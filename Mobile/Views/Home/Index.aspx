<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
 
<head id="Head1" runat="server">
    <title>安克医疗急救办公管理系统&nbsp<% = Html.Encode(ViewData["Version"])%></title>
    <meta charset="UTF-8">  
    <meta name="viewport" content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <link rel="stylesheet" href="~/Content/mui/css/mui.min.css"/>
    <link rel="stylesheet" href="~/Content/mui/css/icons-extra.css"/>
    <style type="text/css">
         .mui-table-view.mui-grid-9{
              /*替换成自己的颜色*/
             background-color: white;
        }

        .mui-icon-extra {
            font-size: 40.5px;
        }
    </style>
    <script type="text/javascript">
        function openit(titile, src){
            $("#frame").html('<iframe height="100%" width="100%"   marginheight="0" marginwidth="0" scrolling="auto" frameborder="0" src="' + src + '"></iframe>');
            $.mobile.go('#frame');
        }

    </script>
</head>
<body>
    <div class="easyui-navpanel">
        <header>
            <div class="m-toolbar">
                <span class="m-title">首页</span>
            </div>
        </header> 
        <div class="mui-content">
		        <ul class="mui-table-view mui-grid-view mui-grid-9">
		            <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3">
                        <a href="javascript:void(0)" onclick="openit('公告','/Office/Office/?loginID=<%=ViewData["accoutid"] %>')">
                    
                            <span class="mui-icon-extra mui-icon-extra-new"> 
                                <% if ((ViewData["Email"]).ToString() != "0"){%>
                                    <span class="mui-badge" ><% =ViewData["Email"]%></span>
                                <%}%>
                            </span>
                            <div class="mui-media-body" >
                               公告
                            </div>
                        </a>
		            </li>
		            <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3">
                        <a href="javascript:void(0)" onclick="openit('邮件','/email/inbox/')">
                            <span class="mui-icon mui-icon-email"> 
                                <% if ((ViewData["Email"]).ToString() != "0"){%>
                                    <span class="mui-badge" ><% =ViewData["Email"]%></span>
                                <%}%>
                            </span>
                            <div class="mui-media-body" >
                               邮件
                            </div>
                        </a>
		            </li>
		            <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3">
                        <a href="javascript:void(0)" onclick="openit('代办','/Workflow/ApproveList/')">
                            <span class="mui-icon mui-icon-checkmarkempty"> 
                                <% if ((ViewData["Approve"]).ToString() != "0"){%>
                                    <span class="mui-badge" ><% =ViewData["Approve"]%></span>
                                <%}%>
                            </span>
                            <div class="mui-media-body" >
                               代办
                            </div>
                        </a>
		            </li>
		            <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3">
                        <a href="javascript:void(0)" onclick="openit('知会','/Workflow/NotifyList/')">
                            <span class="mui-icon-extra mui-icon-extra-notice"> 
                                <% if ((ViewData["Notify"]).ToString() != "0"){%>
                                    <span class="mui-badge" ><% =ViewData["Notify"]%></span>
                                <%}%>
                            </span>
                            <div class="mui-media-body" >
                               知会
                            </div>
                        </a>
		            </li>
		            <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3">
                        <a href="javascript:void(0)" onclick="openit('短信','/Notice/NoticeList/?ActionId=6001')">
                            <span class="mui-icon mui-icon-chatbubble"> 
                                <% if ((ViewData["Email"]).ToString() != "0"){%>
                                    <span class="mui-badge" ><% =ViewData["Email"]%></span>
                                <%}%>
                            </span>
                            <div class="mui-media-body" >
                               短信
                            </div>
                        </a>
		            </li>
		            <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3">
                        <a href="javascript:void(0)" onclick="openit('通讯录','/BasicInfo/TelBook/?OwnerID=0')">
                            <span class="mui-icon-extra mui-icon-extra-dictionary"> 
                                <% if ((ViewData["Email"]).ToString() != "0"){%>
                                    <span class="mui-badge" ><% =ViewData["Email"]%></span>
                                <%}%>
                            </span>
                            <div class="mui-media-body" >
                               通讯录
                            </div>
                        </a>
		            </li>
		            <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3">
                        <a href="javascript:void(0)" onclick="openit('事件管理','/BasicInfo/AlarmEvent/?ActionId=10004')">
                            <span class="mui-icon mui-icon-list"> 
                                <% if ((ViewData["Email"]).ToString() != "0"){%>
                                    <span class="mui-badge" ><% =ViewData["Email"]%></span>
                                <%}%>
                            </span>
                            <div class="mui-media-body" >
                               事件管理
                            </div>
                        </a>
		            </li>
		           <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3">
                       <a href="javascript:void(0)" onclick="openit('车辆管理','/BasicInfo/AmbulanceList/?ActionId=10006')">
                            <span class="mui-icon-extra mui-icon-extra-express"> 
                                <% if ((ViewData["Email"]).ToString() != "0"){%>
                                    <span class="mui-badge" ><% =ViewData["Email"]%></span>
                                <%}%>
                            </span>
                            <div class="mui-media-body" >
                               车辆管理
                            </div>
                        </a>
		           </li>
                    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3">
                       <a href="javascript:void(0)" onclick="openit('电话流水','/BasicInfo/TelLog/?ActionId=10003')">
                            <span class="mui-icon mui-icon-phone"> 
                                <% if ((ViewData["Email"]).ToString() != "0"){%>
                                    <span class="mui-badge" ><% =ViewData["Email"]%></span>
                                <%}%>
                            </span>
                            <div class="mui-media-body" >
                               电话流水
                            </div>
                        </a>
		           </li>
                    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3">
                       <a href="javascript:void(0)" onclick="openit('统计分析','/Home/Report/')">
                            <span class="mui-icon-extra mui-icon-extra-trend"> 
                                <% if ((ViewData["Email"]).ToString() != "0"){%>
                                    <span class="mui-badge" ><% =ViewData["Email"]%></span>
                                <%}%>
                            </span>
                            <div class="mui-media-body" >
                               统计分析
                            </div>
                        </a>
		           </li>
		        </ul> 
		</div>
    </div>
  
    <div id="frame" class="easyui-navpanel">
       
    </div>
</body>
</html>

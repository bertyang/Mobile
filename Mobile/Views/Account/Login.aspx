<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
<meta name="viewport" content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
<title>安克医疗急救办公管理系统&nbsp<% = Html.Encode(ViewData["Version"])%></title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
     <script type="text/javascript">
         $(function () {

             $("#btnLogin").click(function () {   
                 var username = $("#username").val();
                 var password = $("#password").val();
                 var validateCode = '';

                 loginSys(username, password, validateCode);
             });

         });

         function loginSys(username, password, validateCode) {
             $.ajax({
                 type: "POST",
                 dataType: "json",
                 url: "/Account/Login/",
                 data: { username: username, password: password, validateCode: validateCode },
                 success: function (msg) {                     
                     if (msg.IsSuccess) {
                         location.href = "/Home/Index";
                         //$.mobile.go('login.aspx', 'slide', 'right');
                     }
                     else {
                         $.messager.alert('错误', msg.Message, 'error');
                     }
                 },
                 error: function () {
                     $.messager.alert('错误', '获取账号信息失败...请联系管理员!', 'error');
                 }
             });
         }

         function onkey() {
             if (window.event.keyCode == 13) {
                 $("#btnLogin").focus();
                 $("#btnLogin").click();
             }
         }
     </script>
</head>
<body onkeydown="onkey()">
    <div class="easyui-navpanel">
        <header>
            <div class="m-toolbar">
                <span class="m-title">安克医疗急救移动办公管理系统</span>
            </div>
        </header>
        <div style="margin:20px auto;width:100px;height:100px;border-radius:100px;overflow:hidden">
            <img src="../../content/images/Patient1.png" style="margin:0;width:100%;height:100%;">
        </div>
        <div style="padding:0 20px">
            <div style="margin-bottom:10px">
                <input class="easyui-textbox" id="username" data-options="prompt:'Type username',iconCls:'icon-man'" style="width:100%;height:38px">
            </div>
            <div>
                <input class="easyui-passwordbox" id="password" data-options="prompt:'Type password'" style="width:100%;height:38px">
            </div>
            <div style="text-align:center;margin-top:30px">
                <a href="#" class="easyui-linkbutton"  id="btnLogin" style="width:100%;height:40px">
                    <span style="font-size:16px">Login</span>
                </a>
            </div>
        </div>
        <footer>
            <div class="m-toolbar">
                <span class="m-title">版权所有：安克电子技术有限公司</span>
            </div>
        </footer>
    </div>
</body>
</html>
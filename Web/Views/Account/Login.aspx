<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
<title>安克医疗急救办公管理系统&nbsp<% = Html.Encode(ViewData["Version"])%></title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
<%--    <script src="/content/CryptoJS/rollups/tripledes.js"></script>                                  
    <script src="/content/CryptoJS/components/mode-ecb.js"></script>--%>
    <style type="text/css">
     body { background-image: url(../../Content/images/loginbg.jpg); 
            background-repeat:repeat-x;
            background-position:center; 
            background-position:top; 
            margin-left: 0px; 
            margin-top:0px; 
            margin-right: 0px; 
            margin-bottom: 0px; }
    </style>
     <script type="text/javascript">
         $(function () {

             $("#btnLogin").click(function () {   
                 var username = $("#username").val();
                 var password = $("#password").val();
                 var validateCode = $("#validateCode").val();

                 //var keyHex = CryptoJS.enc.Utf8.parse('12345678');
                 //var ivHex = CryptoJS.enc.Utf8.parse('12345678');

                 //username = escape(CryptoJS.DES.encrypt(username, keyHex, { iv: ivHex }));
                 //password = escape(CryptoJS.DES.encrypt(password, keyHex, { iv: ivHex }));

                 loginSys(username, password, validateCode);
             });

         });

         function loginSys(username, password, validateCode) {
             $.ajax({
                 type: "POST",
                 dataType: "json",
                 url: "/Account/Login/",
                 data: { username: username, password: password, validateCode: validateCode },
                 //beforeSend: function () {
                 //    $.messager.progress({
                 //        text: '正在登录中......'
                 //    });
                 //},
                 success: function (msg) {                     
                     if (msg.IsSuccess) {
                         <%--var reg = <%=ViewData["CodeRule"]%>;
                         if(!reg.test(password)){ 
                             $.messager.alert('系统提示',"密码只能由数字和字母组成,不能少于6位,请修改密码!", 'warning', function () {
                                 location.href = '/Home/ChangePassword/';
                             });                            
                         }
                         else{--%>
                         location.href = "/Home/Index";
                         //}
                     }
                     else {
                         $.messager.alert('错误', msg.Message, 'error');
                         $("#trCode").css("display", "table-row");
                         $("#imgCode").attr("src", "/Account/CheckCode?ID=1");
                     }
                 },
                 error: function () {
                     $.messager.alert('错误', '获取账号信息失败...请联系管理员!', 'error');
                 }
                 //complete: function(msg) { 
                 //    $.messager.progress('close');
                 //}
             });
         }

         function onkey() {
             if (window.event.keyCode == 13) {
                 $("#btnLogin").focus();
                 $("#btnLogin").click();
             }
         }

         function MM_swapImgRestore() { //v3.0
             var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
         }

         function MM_findObj(n, d) { //v4.01
             var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
                 d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
             }
             if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
             for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
             if (!x && d.getElementById) x = d.getElementById(n); return x;
         }

         function MM_swapImage() { //v3.0
             var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2) ; i += 3)
                 if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
         }

         function ClickRemoveChangeCode() {
             var code = $("#imgCode").attr("src");
             $("#imgCode").attr("src", code + "1");
         }
     </script>
</head>
<body onkeydown="onkey()">
    <table border="0" align="center" cellpadding="0" cellspacing="0" style="width:100%; height:100%">
        <tr height="60px">
            <td valign="top">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top">
                <form id="form" method="post">
                <table width="1000" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2" valign="top">
                            <table width="950" border="0" cellspacing="2">
                                <tr>
                                    <td width="634" height="172">
                                        &nbsp;
                                    </td>
                                    <td width="306">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        <table width="276" border="0" cellpadding="0" cellspacing="1">
                                            <tr>
                                                <td height="30">
                                                    &nbsp;
                                                </td>
                                                <td height="20">
                                                    请输入用户名密码
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35">
                                                   用户名:
                                                </td>
                                                <td width="211">
                                                    <input id="username" name="username" style="width: 150px; height: 18px;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35">
                                                    密码:
                                                </td>
                                                <td width="211">
                                                    <input type="password" name="password" id="password" style="width: 150px; height:18px;" />
                                                </td>
                                            </tr>
                                            <tr id="trCode" style="display:none" >
                                                <td height="35">
                                                    验证码:
                                                </td>
                                                <td width="211">
                                                   <input type="text" id="validateCode" style="width: 50px" />
                                                   <img id="imgCode" alt="单击可刷新" onclick="ClickRemoveChangeCode()"  />
                                                   <div style="float:right; margin-top: 5px;">
                                                       <a href="javascript:void(0)" onclick="ClickRemoveChangeCode();return false;">看不清，换一张</a>
                                                   </div>
                                                 </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="100">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <table width="200" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td height="100px">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <table width="200px" border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td>
                                                                            <div align="left">
                                                                                <a href="javascript:void(0)" onmouseover="MM_swapImage('Image2','','../../Content/images/denglu02.gif',1)"
                                                                                    onmouseout="MM_swapImgRestore()">
                                                                                    <img src="../../Content/images/denglu01.gif" name="Image2" width="131" height="37" border="0"
                                                                                        id="btnLogin" /></a></div>
                                                                        </td>
                                                                        <td>
                                                                            <div align="center">
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </td>
                                </tr>
                                <tr align="right">
                                    <td colspan="2">
                                     请使用<font color='red'>Chrome或IE8及以上浏览器</font>,并关闭IE兼容性视图&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    
                </table>
                </form>
            </td>
        </tr>
        
        <tr height="40px">
            <td valign="middle" background="../../Content/images/dibg.jpg">
                <div align="center" class="zhengwenbai">
                    版权所有：安克电子技术有限公司 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;操作提示：为了确保您的账号安全,请不要在公共场合保存登录信息。</div>
            </td>
        </tr>
    </table>
</body>
</html>
<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>安克医疗急救办公管理系统&nbsp<% = Html.Encode(ViewData["Version"])%></title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script src="../Content/Script/Menu.js" type="text/javascript"></script>
    <style>
        .accordion .accordion-header
        {
            background: #fafafa;
        }
        a:link{text-decoration:none} 
        a:visited {text-decoration: none}
        a:hover{ text-decoration:underline;} 
    </style>
</head>
<script type="text/javascript">
    var _menus;
    
    $(function () {

        closePwd();

        $('#editpass').click(function () {
            openPwd();
            return false;
        });

        $('#btnEp').click(function () {
            modifyPwd();
        })

        $('#btnCancel').click(function () {

            var $newpass = $('#txtNewPass');
            var $rePass = $('#txtRePass');

            $newpass.val('');
            $rePass.val('');
            closePwd();
        })

        $('#loginOut').click(function () {
            $.messager.confirm('系统提示', '您确定要退出本次登录吗?', function (message) {
                if (message) {
                    location.href = '/Account/LogOff/';
                }
            });
            return false;
        })

        refresh();

    });

    function approve() { 
            AddTab('签核表单','/WorkFlow/ApproveList/','tu1912');      
    }

    function notify() { 
            AddTab('知会表单','/WorkFlow/NotifyList/','tu1905');      
    }

    function email() { 
            AddTab('收件箱','/Email/Inbox/','tu2008');       
    }

    function createFrame(url)
    {
    var s = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
	return s;
    }

    //密码窗口--打开
    function openPwd() {

        $('#w').show();

        $('#w').window({
            title: '修改密码',
            width: 300,
            modal: true,
            shadow: true,
            height: 140,
            resizable: false
        });
    }

    //密码窗口--关闭
    function closePwd() {
        $('#w').window('close');
    }

    //密码窗口--修改
    function modifyPwd() {
        var $newpass = $('#txtNewPass');
        var $rePass = $('#txtRePass');

        if ($newpass.val() == '') {
            msgShow('系统提示', '请输入密码！', 'warning');
            return false;
        }

        var reg = <%=ViewData["CodeRule"]%>;
        if(!reg.test($newpass.val())){ 
            msgShow('系统提示',"密码只能由数字和字母组成,不能少于6位！", 'warning');
            return false;
        }

        if ($rePass.val() == '') {
            msgShow('系统提示', '请再一次输入密码！', 'warning');
            return false;
        }

        if ($newpass.val() != $rePass.val()) {
            msgShow('系统提示', '两次密码不一至！请重新输入', 'warning');
            return false;
        }

        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/Account/ChangePassword/",
            data: { workerId: <%=ViewData["accoutid"]%>, newPassWord: $newpass.val() },
            success: function (msg) {
                if (msg.IsSuccess) {
                    $.messager.alert('系统提示', '恭喜，密码修改成功！', 'info');
                    $newpass.val('');
                    $rePass.val('');
                    closePwd();
                }
                else {
                    $.messager.alert('错误', '更新失败!', 'error');
                }
            },
            error: function () {
                $.messager.alert('错误', '更新失败...请联系管理员!', 'error');
            }
        });

    }

    function refresh() {
        checkAccount();
        refreshEmail();
        refreshApprove();
        refreshNotify();

        setTimeout('refresh()',<%=ViewData["RefreshInterval"]%>); 
    }

    function checkAccount() {
        $.ajax({
            type: "POST",
            url: "/Home/GetAccountID/",
            success: function (msg) {
                var data = eval('(' + msg + ')');

                if($('#AccountID').val() != data.Message)
                {
                    location.href = '/Account/Login/';
                }
            },
            error: function () {
                $.messager.alert('错误', '请检查错误！', "error");
            }
        }); 
    }


    function refreshEmail() {
        $.ajax({
            type: "POST",
            url: "/Home/refreshEmail/",
            success: function (msg) {
                var data = eval('(' + msg + ')');
                $('#email').text(data.Message);
            },
            error: function () {
                $.messager.alert('错误', '请检查错误！', "error");
            }
        }); 
    }

    function refreshApprove() {
        $.ajax({
            type: "POST",
            url: "/Home/refreshApprove/",
            success: function (msg) {
                var data = eval('(' + msg + ')');
                $('#approve').text(data.Message);
            },
            error: function () {
                $.messager.alert('错误', '请检查错误！', "error");
            }
        }); 
    }

    function refreshNotify() {
        $.ajax({
            type: "POST",
            url: "/Home/refreshNotify/",
            success: function (msg) {
                var data = eval('(' + msg + ')');
                $('#notify').text(data.Message);
            },
            error: function () {
                $.messager.alert('错误', '请检查错误！', "error");
            }
        }); 
    }
</script>
<body class="easyui-layout" style="overflow-y: hidden" scroll="no">
    <noscript>
        <div style="position: absolute; z-index: 100000; height: 2046px; top: 0px; left: 0px;
            width: 100%; background: white; text-align: center;">
            <img src="./Content/EasyUI/Themes/image/layout-browser-hd-bg.gif" alt='抱歉，请开启脚本支持！' />
        </div>
    </noscript>
    <div id="blk3" class="blk" style="display: none;">
    </div>
    <div region="north" split="true" border="false" style="overflow: hidden; height: 60px;
        background: url(../Content/images/top.jpg) #7f99be repeat-x left;
        line-height: 20px; color: #fff; font-family: Verdana, 微软雅黑,黑体">
        
        <span style="float: right;padding-right: 20px;" class="head">欢迎
            <% = Html.Encode(ViewData["accoutname"])%>
            <a href="" id="editpass">修改密码</a>
            <a href="/help/help.chm">帮助</a>
            <a href="#" id="loginOut">安全退出</a>
            <input type="hidden" id="AccountID" value="<% = ViewData["accoutid"]%>"/>
        </span>
        <br>
        <span style="float: right; padding-right: 20px;" class="head">
        <% if (Anchor.FA.Utility.AppConfig.IsShowWork) {%>
           <a href="#" title="待办工作" onclick="approve()">待办(<font color='#FF9224'><b id="approve"><% =ViewData["Approve"]%></b></font>)</a>&nbsp;&nbsp;
        <%}%>
        <% if (Anchor.FA.Utility.AppConfig.IsShowNotify){%>
        <a href="#" title="未读知会" onclick="notify()">知会(<font color='#FF9224'><b id="notify"><% =ViewData["Notify"]%></b></font>)</a>&nbsp;&nbsp;
        <%}%>
        <% if (Anchor.FA.Utility.AppConfig.IsShowMail)  {%>
        <a href="#" title="未读邮件" onclick="email()">邮件(<font color='#FF9224'><b id="email"><% = ViewData["Email"]%></b></font>)</a>
        <%}%>
        </span>
    </div>
    <div region="south" split="true" style="height: 30px; background: #D2E0F2;">
        <div class="footer">
            © 1992-<% = Html.Encode(ViewData["DateTime"])%> 安克电子技术有限公司</div>
    </div>
    <div region="west" hide="true" split="true" title="导航菜单" style="width: 180px;" id="west">
        <div id="nav" class="easyui-accordion"  border="false" style="overflow-y:auto;width:155px;">
            <!--  导航内容 -->
        </div>
    </div>
    <div id="mainPanles" region="center" style="background: #eee; overflow-y: hidden">
        <div id="tabs" class="easyui-tabs" fit="true" border="false">
            <div title="首页" style="padding: 5px;" closable="true" iconcls="icon tu1112">
                <iframe id="frame" scrolling="auto" frameborder="0" src="/Home/Default/?loginID=<%=ViewData["accoutid"] %>"
                    style="width: 100%; height: 100%;"></iframe>
            </div>
        </div>
    </div>
    <!--修改密码窗口-->
    <div id="w" style="display: none" class="easyui-window" title="修改密码" collapsible="false"
        minimizable="false" maximizable="false" icon="icon-edit" style="width: 200px;
        height: 140px; padding: 5px; background: #fafafa;">
        <div region="center" border="false" style="padding-left: 30px; background: #fff;
            border: 1px solid #ccc;">
            <table cellpadding="3">
                <tr>
                    <td>
                        新密码：
                    </td>
                    <td>
                        <input id="txtNewPass" type="Password" />
                    </td>
                </tr>
                <tr>
                    <td>
                        确认密码：
                    </td>
                    <td>
                        <input id="txtRePass" type="Password" />
                    </td>
                </tr>
            </table>
        </div>
        <div region="south" border="false" style="text-align: center; height: 30px; line-height: 30px;">
            <a id="btnEp" class="easyui-linkbutton" icon="icon-ok" href="javascript:void(0)">确定</a>
            <a id="btnCancel" class="easyui-linkbutton" icon="icon-cancel" href="javascript:void(0)">
                取消</a>
        </div>
    </div>
    <div id="mm" class="easyui-menu" style="width: 150px; display: none;">
        <div id="mm-tabupdate">
            刷新</div>
        <div class="menu-sep">
        </div>
        <div id="mm-tabclose">
            关闭</div>
        <div id="mm-tabcloseall">
            全部关闭</div>
        <div id="mm-tabcloseother">
            除此之外全部关闭</div>
        <div class="menu-sep">
        </div>
        <div id="mm-tabcloseright">
            当前页右侧全部关闭</div>
        <div id="mm-tabcloseleft">
            当前页左侧全部关闭</div>
        <div class="menu-sep">
        </div>
        <div id="mm-exit">
            退出</div>
    </div>
</body>
</html>

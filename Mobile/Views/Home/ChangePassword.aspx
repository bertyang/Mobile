<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>修改密码1222</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript">

    $(function () {

            openPwd();
           
            $('#btnEp').click(function () {
                modifyPwd();
            })

            //$('#btnCancel').click(function () {

            //    var $newpass = $('#txtNewPass');
            //    var $rePass = $('#txtRePass');

            //    $newpass.val('');
            //    $rePass.val('');
            //    closePwd();
            //})

        });

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
            $.messager.alert('系统提示', '请输入密码！', 'warning');
            return false;
        }

        var reg = <%=ViewData["CodeRule"]%>;
        if(!reg.test($newpass.val())){ 
            $.messager.alert('系统提示',"密码只能由数字和字母组成,不能少于6位！", 'warning');
            return false;
        }

        if ($rePass.val() == '') {
            $.messager.alert('系统提示', '请再一次输入密码！', 'warning');
            return false;
        }

        if ($newpass.val() != $rePass.val()) {
            $.messager.alert('系统提示', '两次密码不一至！请重新输入', 'warning');
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
                    location.href = '/Account/Login/';
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
</script>
</head>
<body class="easyui-layout">
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
<%--            <a id="btnCancel" class="easyui-linkbutton" icon="icon-cancel" href="javascript:void(0)">
                取消</a>--%>
        </div>
    </div>
</body>
</html>

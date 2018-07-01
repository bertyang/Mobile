<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>电话回访</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript" language="javascript">

        //页面初始化
        $(document).ready(function () {

            $('#grid').datagrid('options').url = '/Back/Query/?start=' + $("#BeginDate").datetimebox('getText') + ' ' + $("#BeginTime").val()
            + '&&end=' + $('#EndDate').datetimebox('getText') + ' ' + $("#EndTime").val() + "&type=" + escape($('#Type').combobox('getValue'));

            $('#BeginTime').mask('99:99:99');
            $('#EndTime').mask('99:99:99');

            //#region 下拉菜单初始化
            EUIcombobox("#reason", {
                valueField: "Value",
                textField: "Name",
                OneOption: [{
                    Value: "",
                    Name: "--请选择--"
                }],
                url: "/BasicInfo/GetDataByType/?type=backcall"

            });
        });

        //查询
        function doSearch() {
            $('#grid').datagrid('load', {
                start: $("#BeginDate").datetimebox('getText') +' '+ $("#BeginTime").val(),
                end: $('#EndDate').datetimebox('getText') + ' ' + $("#EndTime").val(),
                type: $('#Type').combobox('getValue')
            });
        }

        function formatSMS(val, rec) {
            return "<span style='color:red;'><a href='#' onclick=BackSMS('" + rec.编码 + "')><u>" + rec.短信 + "</u></a></span>";
        }

        function formatBack(val, rec) {
            return "<span style='color:red;'><a href='#' onclick=BackCall('" + rec.任务编码 + "')><u>" + rec.回访单 + "</u></a></span>";
        }

        //短信回访
        function BackSMS(code) {

            $('#SmsContent').text("");
            $('#SmsReply').val("");
            $('#Code').val("");

            $('#BackSMS').show();
            $('#BackSMS').dialog({
                collapsible: true,
                minimizable: true,
                maximizable: true,
                height: 200,
                width: 500,
                modal: true,
                title: '短信',
                buttons: [
                    {
                        text: '保存',
                        iconCls: 'icon-save',
                        handler: function () {
                            $('#formSMS').form('submit', {
                                onSubmit: function () {
                                    return $(this).form('validate');
                                },
                                success: function (msg) {
                                    var data = eval('(' + msg + ')');
                                    if (data.IsSuccess) {
                                        $.messager.alert('提示', data.Message, 'info', function () {
                                            $('#grid').datagrid('reload');
                                            $('#BackSMS').dialog('close');
                                        });
                                    }
                                    else {
                                        $.messager.alert('提示', data.Message, 'info', function () {
                                        });

                                    }
                                }
                            });
                        }
                    },
                    {
                        text: '取消',
                        iconCls: 'icon-cancel',
                        handler: function () {
                            $('#BackSMS').dialog('close');
                        }
                    }]
            });

            $.ajax({
                    type: "POST",
                    url: "/Back/GetBackSM/?code=" + code,
                    dataType: "json",
                    success: function (data) {
                        if (data.Result) {
                            $('#SmsContent').text(data.Send);
                            $('#SmsReply').val(data.Accept);
                            $('#Code').val(code);
                        }
                    },
                    error: function () {
                        $.messager.alert('错误', '获取失败！', "error");
                    }
                }); 
        }

        //电话回访
        function BackCall(taskCode) {
            $("#driver").attr("checked",false);
            $("#nurse").attr("checked", false);
            $("#doctor").attr("checked", false);
            $("#dispatcher").attr("checked", false);
            $("#stretcher").attr("checked", false);
            $('#reason').combobox('select', "");
            $('#remark').val("");

            $('#BackCall').show();
            $('#BackCall').dialog({
                collapsible: true,
                minimizable: true,
                maximizable: true,
                height: 300,
                width: 300,
                modal: true, //阴影（弹出会影响页面大小）
                title: '回访单',
                buttons: [{
                    id: 'btnSave',
                    text: '保存',
                    iconCls: 'icon-save',
                    handler: function () {
                        $('#formCall').form('submit', {
                            onSubmit: function () {
                                return $(this).form('validate');
                            },
                            success: function (msg) {
                                var data = eval('(' + msg + ')');
                                if (data.IsSuccess) {
                                    $.messager.alert('提示', data.Message, 'info', function () {
                                        $('#grid').datagrid('reload');
                                        $('#BackCall').dialog('close');
                                    });
                                }
                                else {
                                    $.messager.alert('提示', data.Message, 'info', function () {
                                    });

                                }
                            }
                        });
                    }
                },
                    {
                        text: '取消',
                        id: 'btnCancel',
                        iconCls: 'icon-cancel',
                        handler: function () {
                            $('#BackCall').dialog('close');
                        }
                    }]
                });

            $.ajax({
                type: "POST",
                url: "/Back/GetBackCall/?taskCode=" + taskCode,
                dataType: "json",
                success: function (data) {

                    $('#TaskCode').val(taskCode);

                    if (data.Result) {

                        if (data.Driver == 1) {
                            $("#driver").attr("checked", true);
                        }
                        else {
                            $("#driver").attr("checked", false);
                        }

                        if (data.Nurse == 1) {
                            $("#nurse").attr("checked", true);
                        }
                        else {
                            $("#nurse").attr("checked", false);
                        }

                        if (data.Doctor == 1) {
                            $("#doctor").attr("checked", true);
                        }
                        else {
                            $("#doctor").attr("checked", false);
                        }

                        if (data.Dispatcher == 1) {
                            $("#dispatcher").attr("checked", true);
                        }
                        else {
                            $("#dispatcher").attr("checked", false);
                        }

                        if (data.Stretcher == 1) {
                            $("#stretcher").attr("checked", true);
                        }
                        else {
                            $("#stretcher").attr("checked", false);
                        }

                        $('#remark').val(data.Remark);

                        $('#reason').combobox('setValue', data.Reason);

                    }
                },
                error: function () {
                    $.messager.alert('错误', '获取失败！', "error");
                }
            });

            $.ajax({
                type: "POST",
                url: "/Back/GetPersonList/?taskCode=" + taskCode,
                dataType: "json",
                success: function (data) {

                    if (data.Result) {
                        $('#doctorName').text(data.Doctor);
                        $('#nurseName').text(data.Nurse);
                        $('#driverName').text(data.Driver);
                        $('#dispatcherName').text(data.Dispatcher);                        
                    }
                },
                error: function () {
                    $.messager.alert('错误', '获取失败！', "error");
                }
            });     
        }

    </script>
</head>
<body class="easyui-layout">
    <div region="center" style="padding: 5px" border="true">
        <table id="grid" class="easyui-datagrid" align="center" toolbar="#tb"
            pagination="true" pagenumber="1" pagelist="[10, 15, 20]" pagesize="20" idfield="任务编码"
            nowrap="false" striped="true" rownumbers="true" sortname="CreateTime" sortorder="desc"
            remotesort="false" fit="true" fitcolumns="true" singleSelect="true">
            <thead frozen="true">
                <tr>
                    <th field="ck" checkbox="true" align='center'>
                </tr>
            </thead>
            <thead>
                <tr>
                    <th field="短信" width="5%" align='center' formatter="formatSMS">
                        短信
                    </th>
                    <th field="电话回访" width="5%" align='center' formatter="formatBack">
                        电话回访
                    </th>
                    <th field="患者姓名" width="5%" align='center' hidden='true'>
                        患者姓名
                    </th>
                    <th field="性别" width="5%" align='center'>
                        性别
                    </th>
                    <th field="年龄" width="5%" align='center'>
                        年龄
                    </th>
                    <th field="出车分站" width="10%" align='center'>
                        出车急救点
                    </th>
                    <th field="现场地址" width="30%" align='center'>
                        现场地址
                    </th>
                    <th field="发送时间" width="20%" align='center' formatter='renderTime'>
                        发送时间
                    </th>
                    <th field="联系电话" width="10%" align='center'>
                        联系电话
                    </th>
                    <th field="联系人" width="5%" align='center'>
                        联系人
                    </th>
                </tr>
            </thead>
        </table>
    </div>
    <div id="tb" style="padding: 5px; height: auto">
        <div style="margin-top: 3px; text-align: left">
            <table border="0" cellspacing="0" cellpadding="0" width="100%" height="10%">
                <tr>
                    <td align="center">
                        起始时间:
                    </td>
                    <td align="left">
                        <input id="BeginDate" class="easyui-datebox" style="width: 100px" value="<%= ((dynamic)this.ViewData["BeginDate"]) %>" />
                        <input id="BeginTime" class="easyui-validatebox" style="border: 1px solid #8DB2E3;width: 70px" value="<%=ViewData["Time"]%>">
                    </td>
                    <td align="center">
                        终止时间:
                    </td>
                    <td align="left">
                        <input id="EndDate" class="easyui-datebox" style="width: 100px" value="<%= ((dynamic)this.ViewData["EndDate"]) %>" />
                        <input id="EndTime" class="easyui-validatebox" style="border: 1px solid #8DB2E3;width: 70px;" value="<%=ViewData["Time"]%>">
                    </td>
                    <td align="center">
                        回复类型：
                    </td>
                    <td>
                        <select id="Type" class="easyui-combobox" name="Type" editable="false" style="width: 105px;">
                            <option value="">--请选择--</option>
                            <option value="">全部</option>
                            <option value="不满意" selected>不满意</option>
                            <option value="满意">满意</option>
                            <option value="乱码">乱码</option>
                        </select>
                    </td>
                    <td width="12%" align="left">
                        <a href="#" class="easyui-linkbutton" iconcls="icon-search" onclick="doSearch()">查询</a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="BackSMS" icon="icon-save" style="padding: 5px">
        <div region="center" border="true" style="margin-top: 25px">
            <form id="formSMS" method="post" action="/Back/SaveSMS/" enctype="application/x-www-form-urlencoded">
                <table>
                    <tr>
                        <input id="Code" type="hidden" name="Code"  />
                    </tr>
                    <tr>
                        <td style="width: 10%; text-align: right">
                            短信内容：
                        </td>
                        <td style="text-align: left">
                            <label id="SmsContent" style="width: 145px;" />                            
                        </td>
                    </tr>   
                    <tr>
                        <td style="width: 10%; text-align: right">
                            回复：
                        </td>
                        <td style="text-align: left">
                            <input id="SmsReply" name="SmsReply"  type="text" class="easyui-validatebox"  style="width: 145px;" />
                        </td>
                    </tr>    
                </table>
            </form>
        </div>
    </div>
     <div id="BackCall" icon="icon-save" style="padding: 5px">
        <div region="center" border="true" style="margin-top: 25px">
            <form id="formCall" method="post" action="/Back/SaveCall/" enctype="application/x-www-form-urlencoded">
                <table>
                    <tr>
                        <input id="TaskCode" type="hidden" name="TaskCode"  />
                    </tr>
                    <tr>
                        <td style="width: 10%; text-align: right">
                            不满意人：
                        </td>
                        <td style="width: 15%; text-align: left">
                            <input type="checkbox" id="driver" name="driver"/>司机(<label id="driverName" ></label>)<br />
                            <input type="checkbox" id="nurse" name="nurse"/>护士(<label id="nurseName" ></label>)<br />
                            <input type="checkbox" id="doctor"  name="doctor"/>医生(<label id="doctorName" ></label>)<br />
                            <input type="checkbox" id="dispatcher"  name="dispatcher"/>首次调度员(<label id="dispatcherName" ></label>)<br />
                            <input type="checkbox" id="stretcher"  name="stretcher"/>担架工<br />
                        </td>
                    </tr>   
                    <tr>
                        <td style="width: 10%; text-align: right">
                            不满意原因：
                        </td>
                        <td style="width: 15%; text-align: left">
                            <select id="reason" name="reason" class="easyui-combobox" required="true" style="width: 125px;">
                            </select>
                        </td>
                    </tr>  
                    <tr>
                        <td style="width: 10%; text-align: right">
                            备注：
                        </td>
                        <td style="width: 15%; text-align: left">
                            <input id="remark" name="remark" type="text" class="easyui-validatebox"  style="width: 145px;" />
                        </td>
                    </tr>                       
                </table>
            </form>
        </div>
    </div>
</body>
</html>

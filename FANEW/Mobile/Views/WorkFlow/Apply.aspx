<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>申请</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script language="javascript" type="text/javascript"> 

        $(function () {

            $('#btnSubmit').bind('click',
                    function () {
                        $("#main")[0].contentWindow.submitForm();
                    }
                );

            $('#btnCancel').bind('click',
                    function () {

                        <%if (string.IsNullOrEmpty(Request.QueryString["flowInstId"])){ %>
                            CloseCurrentTab();
                        <%}else{%>                            
                            $.messager.confirm('提示', '是否确认删除?', function (r) {
                                if (!r) {
                                    return;
                                }

                                $.ajax({
                                    type: "POST",
                                    url: "/WorkFlow/DeleteFlowInstance/?flowInstId=<%=Request.QueryString["flowInstId"]%>" ,
                                    dataType: "json",
                                    beforeSend: function (msg) {                                
                                        DisableButton();
                                    },
                                    success: function (data) {
                                        if (data.IsSuccess) {
                                            $.messager.alert('提示', data.Message, 'info', function () {
                                                parent.$('#tabs').tabs('getTab', '追寻申请').find("iframe")[0].contentWindow.doSearch();   
                                                CloseCurrentTab();
                                            });
                                        }
                                        else {
                                            $.messager.alert('提示', data.Message, 'info', function () {
                                            });
                                        }
                                    },
                                    error: function () {
                                        $.messager.alert('错误', '删除失败！', "error");
                                    },
                                    complete: function(msg) { 
                                        UnDisableButton();
                                    }

                                });

                            });
                        <%}%>                       
                    }
                );
        });

        //或分散
        function OrSplit(activityInstId)
        {
            $('#Activitys').combobox({
                url: '/WorkFlow/GetNextActivitys/?activityInstId=' + activityInstId,
                valueField: 'ID',
                textField: 'Name',
                onLoadSuccess: function(){
                    UnDisableButton();

                    $('#SelectNextActivity').show();

                    $('#SelectNextActivity').dialog({
                            collapsible: true,
                            minimizable: true,
                            maximizable: true,
                            height: 200,
                            width: 300,
                            modal: true, //阴影（弹出会影响页面大小）
                            title: '选择下一步',
                            buttons: [{
                                text: '确定',
                                iconCls: 'icon-ok',
                                handler: function () {

                                    var activityId=$('#Activitys').combobox('getValue');

                                    if (activityId == "")
                                    {
                                        $.messager.alert('提示', "请选择下一步骤", 'info', function () {
                                        });
                                    }
                                    else
                                    {
                                        TransferNext(activityInstId, activityId); 

                                        $('#SelectNextActivity').dialog('close');
                                    }
                                }
                            },
                                {
                                    text: '取消',
                                    iconCls: 'icon-cancel',
                                    handler: function () {

                                        $('#SelectNextActivity').dialog('close');
                                    }
                                }]
                        }); 
                } 
            });
        }

        //与分散
        function AndSplit(activityInstId)
        {
            $.ajax({
                type: "POST",
                url: "/WorkFlow/GetNextActivitys/?activityInstId=" + activityInstId,
                dataType: "json",
                success: function (data) {
                        
                        var activityId="";

                        for(i=0;i<data.length;i++)
                        {
                            activityId=activityId+data[i].ID+",";
                        }

                        TransferNext(activityInstId,activityId);
                },
                error: function () {
                    $.messager.alert('错误', '获取下一步失败！', "error");
                }
            });     
        }

        //转到下一步
        function TransferNext(activityInstId, activityId) {
            $.ajax({
                type: "POST",
                url: "/WorkFlow/SubmitApply/?activityInstId=" + activityInstId + "&nextActivity=" + activityId,
                dataType: "json",
                beforeSend: function (msg) {                                
                    DisableButton();
                },
                success: function (data) {
                    $.messager.alert('提示', data.Message, 'info', function () {
                        <%if (!string.IsNullOrEmpty(Request.QueryString["flowNo"])){ %>
                            parent.$('#tabs').tabs('getTab', '追寻申请').find("iframe")[0].contentWindow.doSearch();   
                        <%}%>
                        CloseCurrentTab();
                    });
                },
                error: function () {
                    $.messager.alert('错误', '转下一步失败！', "error");
                },
                complete: function(msg) { 
                    UnDisableButton();
                }
            });
        }

        function DisableButton()
        {
            $('#btnSubmit').attr("disabled", "true");
            $('#btnCancel').attr("disabled", "true");
            document.body.style.cursor="progress";
            $("#main")[0].contentWindow.document.body.style.cursor="progress";
        }

        function UnDisableButton()
        {
            $('#btnSubmit').removeAttr("disabled");
            $('#btnCancel').removeAttr("disabled");
            document.body.style.cursor="default";
            $("#main")[0].contentWindow.document.body.style.cursor="default";
        }


    </script>
</head>
<body class="easyui-layout">
    <div region="center" border="false" style="overflow: hidden;">
        <iframe name="main" id="main" height="100%" width="100%" src='<%=ViewData["Url"]%>'  marginheight="0" marginwidth="0" frameborder="0"></iframe>
    </div>
    <div region="south" border="true" style="text-align: right;height: 40px; line-height: 30px;
        background-color: #f7f7f7;">
        <table style="width: 100%">
            <tr>
                <td>
                </td>
                <td style="text-align:left" >
                    <a href="#" class="easyui-linkbutton" iconcls="icon-save" id="btnSubmit">提交</a>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" id="btnCancel">取消</a>
                    <input id="FlowInstId" type="hidden" value=""/></td>
            </tr>
        </table>
       
    </div>
    <div id="SelectNextActivity" icon="icon-save" style="padding: 5px;">       
        选择下一步骤:<select id="Activitys"  class="easyui-combobox"  style="width:150px;" ></select>
    </div>
</body>
</html>

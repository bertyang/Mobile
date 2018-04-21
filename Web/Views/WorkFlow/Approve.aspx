<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>签核</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script language="javascript" type="text/javascript">

        function DisableButton()
        {
            $('#btnAgree').linkbutton("disable");
            $('#btnReject').linkbutton("disable");
            $('#btnReserve').linkbutton("disable");
            $('#btnClose').linkbutton("disable");
            $('#btnReturn').linkbutton("disable");
//            $('#btnReject-d').attr("disabled", "true");
//            $('#btnCancel-d').attr("disabled", "true");
            document.body.style.cursor="progress";
            $("#main")[0].contentWindow.document.body.style.cursor = "progress";
            $.messager.progress({
                text: '正在审核中......',
            });
        }

        function UnDisableButton()
        {
            $('#btnAgree').linkbutton("enable");
            $('#btnReject').linkbutton("enable");
            $('#btnReserve').linkbutton("enable");
            $('#btnClose').linkbutton("enable");
            $('#btnReturn').linkbutton("enable");
//            $('#btnReject-d').removeAttr("disabled");
//            $('#btnCancel-d').removeAttr("disabled");
            document.body.style.cursor="default";
            $("#main")[0].contentWindow.document.body.style.cursor = "default";
            $.messager.progress('close');
        }


        $(function () {
          
            //同意
            $('#btnAgree').bind('click',
                function () {

                    $('#Remark').show();
                    $('#Remark').dialog({
                        collapsible: true,
                        minimizable: true,
                        maximizable: true,
                        height: 200,
                        width: 300,
                        modal: true, //阴影（弹出会影响页面大小）
                        title: ' 填写签核意见(选填)',
                        buttons: [{
                            id: 'btnAgree-d',
                            text: '同意',
                            iconCls: 'icon-ok',
                            handler: function () {

                                if($('#txtRemark').validatebox('isValid'))
                                {
                                    $('#Remark').dialog('close');

                                    try {
                                        $("#main")[0].contentWindow.approveFormYes_Locale();
                                    }
                                    catch (ex) {
                                        approveFormYes_Common();
                                    }
                                }
                            }
                        },
                        {
                            text: '取消',
                            id: 'btnCancel-d',
                            iconCls: 'icon-cancel',
                            handler: function () {
                                $('#txtRemark').val("");
                                $('#Remark').dialog('close');
                            }
                        }]
                        });
                }
            );

            //否决
            $('#btnReject').bind('click',
                function () {

                    $('#Remark').show();
                    $('#Remark').dialog({
                        collapsible: true,
                        minimizable: true,
                        maximizable: true,
                        height: 200,
                        width: 300,
                        modal: true, //阴影（弹出会影响页面大小）
                        title: ' 填写否决意见',
                        buttons: [{
                            id: 'btnReject-d',
                            text: '否决',
                            iconCls: 'icon-no',
                            handler: function () {

                                if($('#txtRemark').validatebox('isValid'))
                                {
                                    $('#Remark').dialog('close');

                                    try {
                                        $("#main")[0].contentWindow.approveFormNo_Locale();
                                    }
                                    catch (ex) {
                                        approveFormNo_Common();
                                    }
                                }
                            }
                        },
                        {
                            text: '取消',
                            id: 'btnCancel-d',
                            iconCls: 'icon-cancel',
                            handler: function () {
                                $('#txtRemark').val("");
                                $('#Remark').dialog('close');
                            }
                        }]
                        });
                }
            );

            //保留
            $('#btnReserve').bind('click',
                function () {

                    $('#Remark').show();
                    $('#Remark').dialog({
                        collapsible: true,
                        minimizable: true,
                        maximizable: true,
                        height: 200,
                        width: 300,
                        modal: true, //阴影（弹出会影响页面大小）
                        title: ' 填写保留意见',
                        buttons: [{
                            id: 'btnReserve-d',
                            text: '保留',
                            iconCls: 'icon-tip',
                            handler: function () {

                                if($('#txtRemark').validatebox('isValid'))
                                {
                                    $('#Remark').dialog('close');
                                    
                                    try {
                                        $("#main")[0].contentWindow.approveFormReserve_Locale();
                                    }
                                    catch (ex) {
                                        approveFormReserve_Common();
                                    }
                                }
                            }
                        },
                        {
                            text: '取消',
                            id: 'btnCancel-d',
                            iconCls: 'icon-cancel',
                            handler: function () {
                                $('#txtRemark').val("");
                                $('#Remark').dialog('close');
                            }
                        }]
                        });
                }
            );

            //离开
            $('#btnClose').bind('click',
                function () {                                                 
                     CloseCurrentTab();
                }
            );

           //退回
            <% if(Request.QueryString["returnType"]=="not"){%>
                $("#btnReturn").attr("style", "display:none;");
            <%}else{%>
                $('#btnReturn').bind('click',
                    function () {
                       $('#ReturnActivitys').combobox({
                        url: '/WorkFlow/GetReturnActivitys/?activityInstId=<%=Request.QueryString["activityInstId"]%>',
                        valueField: 'ID',
                        textField: 'Name',
                        onBeforeLoad: function(){ 
                            DisableButton(); 
                        },
                        onLoadSuccess: function(){  
                            UnDisableButton();

                            $('#DivReturnActivity').show();

                            $('#DivReturnActivity').dialog({
                                collapsible: true,
                                minimizable: true,
                                maximizable: true,
                                height: 200,
                                width: 300,
                                modal: true, //阴影（弹出会影响页面大小）
                                title: '签核表单-->退回',
                                buttons: [
                                    {
                                        text: '退回',
                                        iconCls: 'icon-undo',
                                        handler: function () {
                                        
                                            if($('#ReturnRemark').validatebox('isValid'))
                                            {
                                                var activityId=$('#ReturnActivitys').combobox('getValue');
                                                var returnRemark=escape($('#ReturnRemark').val());

                                                if(returnRemark=="")
                                                {
                                                    $.messager.alert('提示', "请填写退回意见", 'info');
                                                }
                                                else if(activityId=="")
                                                {
                                                    $.messager.alert('提示', "请选择退回步骤", 'info');
                                                }
                                                else
                                                {                           
                                                    approveForm("<%=ViewData["Return"]%>",activityId,returnRemark) ;

                                                    $('#DivReturnActivity').dialog('close');
                                                }
                                            }
                                        }
                                    },
                                    {
                                        text: '取消',
                                        iconCls: 'icon-cancel',
                                        handler: function () {

                                            $('#ReturnRemark').val("");
                                            $('#DivReturnActivity').dialog('close');
                                        }
                                    }
                        ]
                    });
                        }
                    });
                    }
                );
             <%}%>

            //表单页面
            $('#main').attr("src",ReplaceNetworkSegment("<%=ViewData["Url"]%>")); 
        });


        //同意
        function approveFormYes_Common() {

            //如果是最终任务
            if("<%=ViewData["IsFinalTask"]%>"=="True")
            {
                //或分散
                if ("<%=Request.QueryString["splitType"]%>"=="or_split") {
                    OrSplit();
                }
                //与分散
                else if ("<%=Request.QueryString["splitType"]%>"=="and_split") {
                    AndSplit();
                }
            }
            //如果不是是最终任务
            else{
                approveForm("<%=ViewData["Agree"]%>","",escape($('#txtRemark').val()));
            }            
        }

        //否决
        function approveFormNo_Common() {
            approveForm("<%=ViewData["Reject"]%>","",escape($('#txtRemark').val()));
        }

        //保留
        function approveFormReserve_Common() {
            approveForm("<%=ViewData["Reserve"]%>","",escape($('#txtRemark').val()));
        }

        //签核表单
        var lock = false;              

        function approveForm(appValue,nextActivityId,remark)
        {
            //防止重复签核
            if(lock)
            {
                return;
            }
            else
            {
                lock = true;
            }

            $.ajax({
                type: "POST",
                url: "/WorkFlow/ApproveForm/?appValue="+appValue
                               +"&appRemark=" + remark
                                +"&nextActivity="+nextActivityId
                                +"&<%=Request.QueryString %>",
                dataType: "json",
                beforeSend: function (msg) {                                
                    DisableButton();
                },
                success: function (data) {
                        $.messager.alert('提示', data.Message, 'info', function () {
                            try
                            {
                                parent.$('#tabs').tabs('getTab', '签核表单').find("iframe")[0].contentWindow.doSearch();   
                                parent.refreshApprove();   
                            }
                            catch  (e)   {
                                //alert(e.name  +   " :  "   +  e.message);
                            } 

                            CloseCurrentTab(); 
                        });
                },
                error: function () {
                    $.messager.alert('错误', '签核失败！', "error");
                },
                complete: function(msg) { 
                    lock = false;
                    UnDisableButton();
                }
            });     
        }


        //或分散
        function OrSplit()
        {
            $('#NextActivitys').combobox({
                url: '/WorkFlow/GetNextActivitys/?activityInstId=<%=Request.QueryString["activityInstId"]%>',
                valueField: 'ID',
                textField: 'Name',
                onBeforeLoad: function(){ 
                    DisableButton(); 
                },
                onLoadSuccess: function(){  
                    $('#DivNextActivity').show();
            
                    $('#DivNextActivity').dialog({
                            collapsible: true,
                            minimizable: true,
                            maximizable: true,
                            height: 200,
                            width: 300,
                            modal: true, //阴影（弹出会影响页面大小）
                            title: '选择下一步',
                            buttons: [
                                        {
                                            text: '确定',
                                            iconCls: 'icon-ok',
                                            handler: function () {

                                                if($('#AssignRemark').validatebox('isValid'))
                                                {
                                                    var activityId=$('#NextActivitys').combobox('getValue');
                                                    var remark=escape($('#AssignRemark').val());

                                                    if(activityId=="")
                                                    {
                                                        $.messager.alert('提示', "请选择下一步骤", 'info', function () {
                                                        });
                                                    }
                                                    else
                                                    {                           
                                                        approveForm("Y",activityId,remark) ;

                                                        $('#DivNextActivity').dialog('close');
                                                    }
                                                }
                                            }
                                        },
                                        {
                                            text: '取消',
                                            iconCls: 'icon-cancel',
                                            handler: function () {

                                                $('#DivNextActivity').dialog('close');
                                            }
                                        }
                                ]
                    });

                    UnDisableButton();
                }
            });
        }

        //与分散
        function AndSplit()
        {
            $.ajax({
                type: "POST",
                url: "/WorkFlow/GetNextActivitys/?activityInstId=<%=Request.QueryString["ActivityInstId"]%>",
                dataType: "json",
                success: function (data) {
                        
                        var activityId="";

                        for(i=0;i<data.length;i++)
                        {
                            activityId=activityId+data[i].ID+",";
                        }
                        var remark=escape($('#txtRemark').val());

                        approveForm("Y",activityId,remark);
                },
                error: function () { 
                    $.messager.alert('错误', '获取下一步失败！', "error");
                }
            });     
        }

    </script>
</head>
<body class="easyui-layout">
    <div region="center" border="false" style="padding: 0px; overflow: hidden;">
        <iframe name="main" id="main" height="100%" width="100%" marginheight="0" marginwidth="0"
            frameborder="0"></iframe>
    </div>
    <div region="south" border="true" style="text-align: right; background-color: #f7f7f7;
        height: 40px; line-height: 30px">
        <table style="width: 100%">
            <tr>
                <td>
                </td>
                <td style="text-align: left">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-ok" id="btnAgree">同意</a> 
                    <a href="#" class="easyui-linkbutton" iconcls="icon-no" id="btnReject">否决</a> 
                    <a href="#" class="easyui-linkbutton" iconcls="icon-tip" id="btnReserve">保留</a> 
                    <a href="#" class="easyui-linkbutton" iconcls="icon-undo" id="btnReturn">退回</a> 
                    <a href="#" class="easyui-linkbutton" iconcls="icon-back" id="btnClose">离开</a>
                </td>
            </tr>
        </table>
    </div>
    <div id="Remark" icon="icon-save" style="padding: 5px;display: none">
        <textarea id="txtRemark" validtype="length[0,255]" class="easyui-validatebox" style="width: 98%;
            height: 98%; overflow-x: hidden; overflow-y: hidden"></textarea>
    </div>
    <div id="DivNextActivity" icon="icon-save" style="padding: 5px;display: none">
        <table>
            <tr>
                <td align="right">
                    选择下一步骤:
                </td>
                <td>
                    <select id="NextActivitys" class="easyui-combobox" style="width: 150px;">
                    </select>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp
                </td>
            </tr>
            <tr>
                <td align="right">
                    意见:
                </td>
                <td>
                    <textarea id="AssignRemark" class="easyui-validatebox" rows="4" style="width: 148px;
                        border: 1px solid #8DB2E3; overflow: hidden;" validtype="length[0,255]"></textarea>
                </td>
            </tr>
        </table>
    </div>
    <div id="DivReturnActivity" icon="icon-undo" style="padding: 5px;display: none">
        <table>
            <tr>
                <td>
                    退回步骤:
                </td>
                <td>
                    <select id="ReturnActivitys" class="easyui-combobox" style="width: 150px;">
                    </select>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    退回意见
                </td>
                <td>
                    <textarea id="ReturnRemark" class="easyui-validatebox" rows="4" style="width: 148px;
                        border: 1px solid #8DB2E3; overflow: hidden;" validtype="length[0,255]"></textarea>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>

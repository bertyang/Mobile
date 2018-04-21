<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>追寻</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script language="javascript" type="text/javascript">
        $(function () {
           
            $('#btnClose').bind('click',
                    function () {
                        CloseCurrentTab();
                    }
            );

            $('#btnRecall').bind('click',
                function () {

                    $('#Remark').show();
                    $('#Remark').dialog({
                        collapsible: true,
                        minimizable: true,
                        maximizable: true,
                        height: 200,
                        width: 300,
                        modal: true, //阴影（弹出会影响页面大小）
                        title: ' 填写撤回意见',
                        buttons: [{
                             text: '撤回',
                            iconCls: 'icon-undo',
                            handler: function () {

                                if($('#txtRemark').validatebox('isValid'))
                                {
                                    var flowId="<%=Request.QueryString["flowId"]%>";
                                    var flowNo="<%=Request.QueryString["flowNo"]%>";
                                    var remark=escape($('#txtRemark').val());

                                    RecallForm(flowId,flowNo,remark);
                                }
                            }
                        },
                        {
                            text: '取消',
                            iconCls: 'icon-cancel',
                            handler: function () {
                                $('#txtRemark').val("");
                                $('#Remark').dialog('close');
                            }
                        }]
                        });

                }

            );

            $('#btnPrint').bind('click',
                    function () {
                        var url = '<%=ViewData["ViewUrl"]%>'+ '&isPrint=1';
                        window.open(url,'','height=600,width=1000');
                    }
            );

            //表单页面
            $('#main').attr("src",ReplaceNetworkSegment("<%=ViewData["ViewUrl"]%>")); 
                    
            }
        );

        function RecallForm(flowId,flowNo,remark)
        {
            $.ajax({
                    type: "POST",
                    url: "/WorkFlow/RecallFlow/?flowId="+flowId+"&flowNo="+flowNo+"&remark="+remark,
                    dataType: "json",
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
                        $.messager.alert('错误', '撤回失败！', "error");
                    }

                });
        }

    </script>
</head>
<body class="easyui-layout">
    <div region="center" border="false" style="padding: 0px;overflow: hidden;">
        <iframe name="main" id="main" height="100%" width="100%"  marginheight="0" marginwidth="0" frameborder="0"></iframe>
    </div>
    <div region="south" border="true" style="text-align: right;background-color: #f7f7f7;height: 40px;line-height: 30px">
         <table style="width: 100%;">
            <tr>
                <td>
                </td>
                <td style="text-align:left;" >
                    <%if (ViewData["RecallButton"] == "true"){%>
                        <a href="#" class="easyui-linkbutton" iconcls="icon-undo" id="btnRecall">撤回</a>
                    <%}%>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-print" id="btnPrint">打印</a>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-back" id="btnClose">离开</a> 
                </td>
            </tr>
        </table>
    </div>
    <div id="Remark" icon="icon-save" style="padding: 5px;">
        <textarea id="txtRemark" validtype="length[0,255]" class="easyui-validatebox" style="width: 98%;
            height: 98%; overflow-x: hidden; overflow-y: hidden"> </textarea>
    </div>
    <div id="printDIV" style="display: none;">
        <iframe id="p_frame" scrolling="auto" frameborder="0" style="width: 100%;
            height: 100%;"></iframe>
    </div>
</body>
</html>

<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<% 
var item = ViewData["entity"];
Type itemType = item.GetType();
//List<Anchor.FA.Model.TZAmbulanceState> amSls = ViewData["State"] as List<Anchor.FA.Model.TZAmbulanceState>;//录音
%>
<!DOCTYPE html>
<html>
<head>
    <title>改车辆状态 AmbulanceStateChange</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <% List<Anchor.FA.Model.TZAmbulanceState> amSls = ViewData["State"] as List<Anchor.FA.Model.TZAmbulanceState>;
       //录音%>
    <script type="text/javascript">
    
    function getAjaxData(url,data)
    {
        var ReData;
        $.ajax({
            type: "POST",
            url: url,
            dataType: "JSON",
            data:data,
            async: false,
            success: function (data1, textStatus, jqXHR) {
                ReData=data1;
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(o.url);
                ReData=null;
            }
        });
        return ReData;
    }

        function closePage() {
            parent.freload();
            parent.$AmbulanceStateChangeWindow.window('close');
        }
        function ControlEnabled(workStateCode)
        {
            var w=parseInt(workStateCode);
            if(isNaN(w) || w<0)
            {
                $('#Button1').attr("disabled", "true");
                $('#Button2').attr("disabled", "true");
                $('#Button3').attr("disabled", "true");
                $('#Button4').attr("disabled", "true");
                $('#Button5').attr("disabled", "true");
                $('#Button6').attr("disabled", "true");
                $('#Button7').attr("disabled", "true");
                return false;
            }
            for(var i=1;i<8;i++)
            {
                if(!$('#Button'+i).is(":hidden"))
                {
                    if(w<i)
                    {
                        $('#Button'+i).removeAttr("disabled");
                    }
                    else
                    {
                        $('#Button'+i).attr("disabled", "true");
                    }
                }
            }
            
            return true;
        }
        function ModifyState(workStateCode)
        {
            $('#lblAlert').text("");
            var data={
                AmbCode:'<%= itemType.GetProperty("车辆编码").GetValue(item, null)%>',
                workStateCode: workStateCode
                }
            var oData= getAjaxData('/BasicInfo/ModifyAmbulanceState/',data);
            if(oData=="")
            {
                ControlEnabled(workStateCode);
                $('#lblWorkState').text($('#Button'+workStateCode).text());
            }
            else
            {
                $('#lblAlert').text(oData);
            }
        }
        $(function () {
            $('#Button1').hide();
            $('#Button2').hide();
            $('#Button3').hide();
            $('#Button4').hide();
            $('#Button5').hide();
            $('#Button6').hide();
            $('#Button7').hide();

    <% 
        foreach (Anchor.FA.Model.TZAmbulanceState amS in amSls)
        {
    %>
    $('#Button'+'<%=amS.编码%>').show();
    $('#Button'+'<%=amS.编码%>').text("<%=amS.名称%>");
    <%} %>

        ControlEnabled(<%= itemType.GetProperty("工作状态编码").GetValue(item, null)%>);


        });


        
    </script>

    <style type="text/css">
        .style1
        {
            background-color: #F0F0F0;
            height: 35px;
        }
        .style2
        {
            height: 35px;
        }
    </style>
</head>
<body>
    <div align="center">
        <table border="0" cellspacing="6" cellpadding="0" style="border-collapse: separate;
            border-spacing: 6px;">
            <tr valign="top">
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="6" class="blockTable">
                        <tr>
                            <td style="padding: 0 8px 4px;" align="center">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tableInfo">
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="middle" class="style2">
                                            编码：
                                        </td>
                                        <td align="left" valign="middle">
                                            &nbsp;
                                            <%= itemType.GetProperty("车辆编码").GetValue(item, null)%>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="middle" class="style1" style="width: 80px;">
                                            车牌号码：
                                        </td>
                                        <td align="left" valign="middle" class="style1" style="width: 120px;">
                                            &nbsp;
                                            <%= itemType.GetProperty("车牌号码").GetValue(item, null)%>
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="middle" class="style2">
                                            实际标识：
                                        </td>
                                        <td align="left" valign="middle">
                                            &nbsp;
                                            <%= itemType.GetProperty("实际标识").GetValue(item, null)%>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="middle" class="style1">
                                            所属分站：
                                        </td>
                                        <td align="left" valign="middle" class="style1">
                                            &nbsp;
                                            <%= itemType.GetProperty("所属分站").GetValue(item, null)%>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="middle" class="style2">
                                            随车电话：
                                        </td>
                                        <td align="left" valign="middle">
                                            &nbsp;
                                            <%= itemType.GetProperty("随车电话").GetValue(item, null)%>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="middle" class="style1">
                                            车辆等级：
                                        </td>
                                        <td align="left" valign="middle" class="style1">
                                            &nbsp;
                                            <%= itemType.GetProperty("车辆等级").GetValue(item, null)%>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="middle" class="style2">
                                            工作状态：
                                        </td>
                                        <td align="left" valign="middle" class="style2">
                                            &nbsp;
                                            <%= itemType.GetProperty("工作状态").GetValue(item, null)%>
                                            <span id="lblWorkState"></span>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 2px; padding-left: 6px; padding-right: 6px; padding-bottom: 2px;">
                                <a href="#" class="easyui-linkbutton" id="Button1" onclick="javascript:ModifyState(1);return false;"></a>
                                &nbsp;<a href="#" class="easyui-linkbutton" id="Button2" onclick="javascript:ModifyState(2);return false;"></a>
                                &nbsp;<a href="#" class="easyui-linkbutton" id="Button3" onclick="javascript:ModifyState(3);return false;"></a>
                                &nbsp;<a href="#" class="easyui-linkbutton" id="Button4" onclick="javascript:ModifyState(4);return false;"></a>
                                &nbsp;<a href="#" class="easyui-linkbutton" id="Button5" onclick="javascript:ModifyState(5);return false;"></a>
                                &nbsp;<a href="#" class="easyui-linkbutton" id="Button6" onclick="javascript:ModifyState(6);return false;"></a>
                                &nbsp;<a href="#" class="easyui-linkbutton" id="Button7" onclick="javascript:ModifyState(7);return false;"></a>
<%--                                <asp:Button ID="Button1" runat="server" Text="" onclick="Button1_Click" />
                                &nbsp;<asp:Button ID="Button2" runat="server" Text="" onclick="Button2_Click" />
                                &nbsp;<asp:Button ID="Button3" runat="server" Text="" onclick="Button3_Click" />
                                &nbsp;<asp:Button ID="Button4" runat="server" Text="" onclick="Button4_Click" />
                                &nbsp;<asp:Button ID="Button5" runat="server" Text="" onclick="Button5_Click" />
                                &nbsp;<asp:Button ID="Button6" runat="server" Text="" onclick="Button6_Click" />
                                &nbsp;<asp:Button ID="Button7" runat="server" Text="" onclick="Button7_Click" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 2px; padding-left: 6px; padding-right: 6px; padding-bottom: 2px;">
                                &nbsp;
                                <%--<input id="Button_Back" type="button" value="返回并刷新" class="btn80" onmouseover="this.className='obtn80'"
                                    onmouseout="this.className='btn80'" onclick="closePage()" />--%>
                                    <a href="#" class="easyui-linkbutton" plain="true"
                                        onclick="javascript:closePage();return false;"><span style="color: #15428B;">&nbsp;返回并刷新</span></a>
                                    <span id="lblAlert" style="color:Red;"></span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>出车信息</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%> 
    <script type="text/javascript" language="javascript">
    </script>
</head>
<body class="easyui-layout">
    <div region="center" style="padding: 5px; height: 50%">
        <div region="north" style="border:2px;">
            <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td width="1%">
                            &nbsp;
                        </td>
                        <td width="90%" align="left" valign="middle" colspan="6">
                            <span id="" class="editTitle">出车任务信息[<%= ((dynamic)this.ViewData["entity"]).TaskCode %>]</span>
                        </td>
                        <td width="1%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8" style="height:10px;"></td>
                    </tr>
                    <tr>
                        <td width="1%">
                            &nbsp;
                        </td>
                        <td align="left" valign="middle">
                            车辆标识：
                        </td>
                        <td align="left" valign="middle">
                            <input id="Aum" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity"]).Aum %>"
                                readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            车牌号码：
                        </td>
                        <td align="left" valign="middle">
                            <input id="AumMark" style="border-width: 0px; background-color: transparent;"
                                value="<%= ((dynamic)this.ViewData["entity"]).AumMark %>" readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            随车电话：
                        </td>
                        <td align="left" valign="middle">
                            <input id="Tel" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity"]).Tel %>"
                                readonly="true" />
                        </td>
                        <td width="1%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="1%">
                            &nbsp;
                        </td>
                        <td align="left" valign="middle">
                            所属分站：
                        </td>
                        <td align="left" valign="middle">
                            <input id="Sta" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity"]).Sta %>"
                                readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            收到指令时刻：
                        </td>
                        <td align="left" valign="middle">
                            <input id="ReceiveTime" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity"]).ReceiveTime %>"
                                readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            出车时刻：
                        </td>
                        <td align="left" valign="middle">
                            <input id="StartTime" style="border-width: 0px; background-color: transparent;"
                                value="<%= ((dynamic)this.ViewData["entity"]).StartTime %>" readonly="true" />
                        </td>
                        <td width="1%">
                            &nbsp;
                        </td>
                    </tr>
                                                                                                                                <tr>
                    <td width="1%">
                        &nbsp;
                    </td>                    
                    <td align="left" valign="middle">
                        到达现场时刻：
                    </td>
                    <td align="left" valign="middle">
                        <input id="ArriveTime" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity"]).ArriveTime %>"
                            readonly="true" />
                    </td>
                    <td align="left" valign="middle">
                        病人上车时刻：
                    </td>
                    <td align="left" valign="middle">
                        <input id="TakeTime" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity"]).TakeTime %>"
                            readonly="true" />
                    </td>
                    <td align="left" valign="middle">
                            到达医院时刻：
                        </td>
                        <td align="left" valign="middle">
                            <input id="HosTime" style="border-width: 0px; background-color: transparent;"
                                value="<%= ((dynamic)this.ViewData["entity"]).HosTime %>" readonly="true" />
                        </td>
                    <td width="1%">
                        &nbsp;
                    </td>
                </tr>
                    <tr>
                        <td width="1%">
                            &nbsp;
                        </td>                        
                        <td align="left" valign="middle">
                            结束时刻：
                        </td>
                        <td align="left" valign="middle">
                            <input id="EndTime" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity"]).EndTime %>"
                                readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            是否正常结束：
                        </td>
                        <td align="left" valign="middle">
                           <input id="IsEnd" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity"]).IsEnd %>"
                                readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            异常结束原因：
                        </td>
                        <td align="left" valign="middle">
                            <input id="Reason" style="border-width: 0px; background-color: transparent;"
                                value="<%= ((dynamic)this.ViewData["entity"]).Reason %>" readonly="true" />
                        </td>
                        <td width="1%">
                            &nbsp;
                        </td>
                    </tr> 
                    <tr>
                        <td width="1%">
                            &nbsp;
                        </td>
                        <td align="left" valign="middle">
                            司机：
                        </td>
                        <td align="left" valign="middle">
                            <input id="Dri" style="border-width: 0px; background-color: transparent;"
                                value="<%= ((dynamic)this.ViewData["entity"]).Dri %>" readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            医生：
                        </td>
                        <td align="left" valign="middle">
                            <input id="Doc" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity"]).Doc %>"
                                readonly="true" />
                        </td>
                        <td align="left" valign="middle">
                            护士：
                        </td>
                        <td align="left" valign="middle">
                           <input id="Nur" style="border-width: 0px; background-color: transparent;" value="<%= ((dynamic)this.ViewData["entity"]).Nur %>"
                                readonly="true" />
                        </td>
                        <td width="1%">
                            &nbsp;
                        </td>
                    </tr>                     
                    <tr>
                        <td colspan="8"><hr style="border:1px" color="#987cb9" size="1" /></td>
                    </tr>
                </table>
        </div>             
    </div>
</body>
</html>

<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<% 
var item = ViewData["entity"];
Type itemType = item.GetType();
        
%>
<!DOCTYPE html>
<html>
<head>
    <title>AmbulancePersonSign</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
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
    
    function GetNotWorkPerson()
    {
        var data={
                personTypes: $('#ddlPersonType').combobox('getValue'),
                stationCode: $('#DropDownList_Station').combobox('getValue'),
                ActionId: ActionId
                }

        var oData= getAjaxData('/BasicInfo/BindNotWorkPerson/',data);
        $('#GridNotWork').datagrid('loadData',oData);
        $('#GridNotWork').datagrid('clearSelections');
    }
    function GetWorkPerson()
    {
        var data={
                AmbCode: AmbCode
                }

        var oData=  getAjaxData('/BasicInfo/BindWorkPerson/',data);
        $('#GridWork').datagrid('loadData',oData);
        $('#GridWork').datagrid('clearSelections');
    }

    
        function OnDuty()
        {
            var rows=$('#GridNotWork').datagrid('getSelections');
            if(rows.length<=0)
            {
                alert("请选择至少一个未上班人员！");
                return false;
            }
            var data={
                AmbCode:AmbCode
                }
            for(var i=0;i<rows.length;i++)
            {
                data["personCode["+i+"]"]=rows[i].编码;
            }
            var oData= getAjaxData('/BasicInfo/OnDuty/',data);
            if(oData=="")
            {
                 GetNotWorkPerson();
                 GetWorkPerson()
            }
            else
            {
                alert(oData);
            }
        }
        
        function OffDuty()
        {
            var rows=$('#GridWork').datagrid('getSelections');
            if(rows.length<=0)
            {
                alert("请选择至少一个已上班人员！");
                return false;
            }
            var data={
                AmbCode:AmbCode
                }
            for(var i=0;i<rows.length;i++)
            {
                data["personCode["+i+"]"]=rows[i].编码;
            }
            var oData= getAjaxData('/BasicInfo/OffDuty/',data);
            if(oData=="")
            {
                 GetNotWorkPerson();
                 GetWorkPerson()
            }
            else
            {
                alert(oData);
            }
        }

        function closePage() {
            parent.freload();
            parent.$AmbulancePersonSignWindow.window('close');
        }
        var ActionId;
        var AmbCode;
        $(function () {
            ActionId = <%: ViewData["ActionId"] %>;
            AmbCode= '<%= itemType.GetProperty("车辆编码").GetValue(item, null)%>';
            //#region 下拉菜单初始化
            var dr = '<%: ViewData["LtDriver"] %>';
            //司机多选 下拉菜单
            EUIcombobox("#DropDownList_Station", {
                valueField: "编码",
                textField: "名称",
                OneOption: [{
                    编码: "",
                    名称: "--请选择--"
                }],
                url: "/BasicInfo/LoadOnDutyStations/?ActionId="+ActionId,
                onSelect:function(record)
                {
                    GetNotWorkPerson();
                }
            });

            $('#ddlPersonType').combobox({
                onSelect:function(record)
                {
                    GetNotWorkPerson();
                }
            });
            //#endregion

             GetNotWorkPerson();
             GetWorkPerson()
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
                                        <td align="left" valign="middle" colspan="3">
                                            分站：
                                            
                                        <input class="easyui-combobox" style="width:150px;" id="DropDownList_Station"
                                            editable="false" /><%--panelheight="300px"--%>
                                        <select id="ddlPersonType" name="dept" style="width:150px;" class="easyui-combobox" editable="false">
                                            <option value="">全部类型</option>
                                            <option value="3">司机</option>
                                            <option value="4">医生</option>
                                            <option value="5">护士</option>
                                            <option value="6">担架工</option>
                                            <option value="7">抢救员</option>
                                        </select>
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
                                        <td align="center" valign="middle" style="width: 180px;">
                                            <h5>
                                                未上班人员</h5>
                                        </td>
                                        <td align="center" valign="middle" style="width: 50px;">
                                            &nbsp;
                                        </td>
                                        <td align="center" valign="middle" style="width: 180px;">
                                            <h5>
                                                已上班人员</h5>
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
                                        <td align="left" valign="top" rowspan="5">
                                            <table id="GridNotWork" class="easyui-datagrid" align="center" 
                                                style="width:180px;height: 165px;"
                                             >
                                            <thead frozen="true">    
                                                <tr>    
				                                    <th field="ID"  checkbox="true"  align='center'>编码</th>  
                                                </tr>    
                                            </thead>
                                            <thead>
			                                    <tr>
                                                    <th field="姓名" align='center' width="60px">姓名</th><%--width="100px"--%>
                                                    <th field="人员类型" align='center' width="60px">人员类型</th>
                                                    <%--<th field="State" width="100px"  align='center' formatter="formatState">工作状态</th>--%>
			                                    </tr>
		                                    </thead>
                                            </table>
                                        </td>
                                        <td align="left" valign="middle">
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="top" rowspan="5">
                                            <table id="GridWork" class="easyui-datagrid" align="center" 
                                                style="width:180px;height: 165px;"
                                             >
                                            <thead frozen="true">    
                                                <tr>    
				                                    <th field="ID" checkbox="true"  align='center'>编码</th>  
                                                </tr>    
                                            </thead>
                                            <thead>
			                                    <tr>
                                                    <th field="姓名" align='center' width="60px">姓名</th>
                                                    <th field="人员类型" align='center' width="60px">人员类型</th>
			                                    </tr>
		                                    </thead>
                                            </table>
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
                                        <td align="center" valign="middle">
                                        <img alt="上班" src="../../Content/images/button_Onduty.Image.gif" style="cursor: pointer;"
                                        onclick="javascript:OnDuty()" />
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
                                        <td align="left" valign="middle">
                                            &nbsp;
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
                                        <td align="center" valign="middle">
                                        <img alt="下班" src="../../Content/images/button_Offduty.Image.gif" style="cursor: pointer;"
                                        onclick="javascript:OffDuty()" />
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
                                            <%= itemType.GetProperty("工作状态").GetValue(item, null)%>
                                        </td>
                                        <td align="left" valign="middle">
                                            &nbsp;
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
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="padding-top: 2px; padding-left: 6px; padding-right: 6px; padding-bottom: 2px;">
                                &nbsp;
                                <a href="#" class="easyui-linkbutton" plain="true"
                                    onclick="javascript:closePage();return false;"><span style="color: #15428B;">&nbsp;返回并刷新</span></a>
                                <span id="SaveResult"></span>
                                <%--<input id="Button_Back" type="button" value="返回并刷新" class="btn80" onmouseover="this.className='obtn80'"
                                    onmouseout="this.className='btn80'" onclick="closePage()" />
                                <asp:Label ID="lblAlert" runat="server" Text="" ForeColor="Red"></asp:Label>--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>


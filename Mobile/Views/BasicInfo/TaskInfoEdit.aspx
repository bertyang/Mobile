<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<% 
    Anchor.FA.Model.TTask tInfo = ViewData["tInfo"] as Anchor.FA.Model.TTask;//
    //List<Anchor.FA.Model.C_AmbulanceStateTimeInfo> absLs = ViewData["absLs"] as List<Anchor.FA.Model.C_AmbulanceStateTimeInfo>;//
    List<Anchor.FA.Model.TZAmbulanceState> zasLs = ViewData["zasLs"] as List<Anchor.FA.Model.TZAmbulanceState>;//

    //string[] AmbulanceStateTime ={tInfo.CreateTaskTime.ToString()
    //                                ,Anchor.FA.Utility.DBConvert.ConvertDateTimeToNullable(tInfo.ReceiveCmdTime)
    //                                ,Anchor.FA.Utility.DBConvert.ConvertDateTimeToNullable(tInfo.AmbulanceLeaveTime)
    //                                ,Anchor.FA.Utility.DBConvert.ConvertDateTimeToNullable(tInfo.ArriveSceneTime)
    //                                ,Anchor.FA.Utility.DBConvert.ConvertDateTimeToNullable(tInfo.LeaveSceneTime)
    //                                ,Anchor.FA.Utility.DBConvert.ConvertDateTimeToNullable(tInfo.ArriveHospitalTime)
    //                                ,Anchor.FA.Utility.DBConvert.ConvertDateTimeToNullable(tInfo.FinishTime)
    //                                ,Anchor.FA.Utility.DBConvert.ConvertDateTimeToNullable(tInfo.ReturnTime)
    //                            };
                                
                                    string[] AmbulanceStateTime ={tInfo.生成任务时刻.ToString()
                                     
                                    ,Anchor.FA.Utility.DBConvert.ConvertDateTimeToNullable(tInfo.接收命令时刻)
                                    ,Anchor.FA.Utility.DBConvert.ConvertDateTimeToNullable(tInfo.出车时刻)
                                    ,Anchor.FA.Utility.DBConvert.ConvertDateTimeToNullable(tInfo.到达现场时刻)
                                    ,Anchor.FA.Utility.DBConvert.ConvertDateTimeToNullable(tInfo.离开现场时刻)
                                    ,Anchor.FA.Utility.DBConvert.ConvertDateTimeToNullable(tInfo.到达医院时刻)
                                    ,Anchor.FA.Utility.DBConvert.ConvertDateTimeToNullable(tInfo.完成时刻)
                                    ,Anchor.FA.Utility.DBConvert.ConvertDateTimeToNullable(tInfo.返回站中时刻)
                                };
%>
<!DOCTYPE html>
<html>
<head id="Head1">
    <meta name="viewport" content="width=device-width" />
    <title></title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <style type="text/css">
        .TableInfoTH
        {
            background: #FFFFFF repeat-x left top;
            height: 25px;
        }
        .TableInfoTD
        {
            background-color: #F0F0F0;
        }
        /*.TableInfoTD td
        {
            border-width: 0 1px 1px 0;
            border-style: dotted;
            margin: 0;
            padding: 0;
        }*/
        tr
        {
            height: 22px;
        }
        *
        {
            font-size: 12px;
        }
        .This_header
        {
            border-color: #95B8E7;
            background-color: #E0ECFF;
            background: -webkit-linear-gradient(top,#EFF5FF 0,#E0ECFF 100%);
            background: -moz-linear-gradient(top,#EFF5FF 0,#E0ECFF 100%);
            background: -o-linear-gradient(top,#EFF5FF 0,#E0ECFF 100%);
            background: linear-gradient(to bottom,#EFF5FF 0,#E0ECFF 100%);
            background-repeat: repeat-x;
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr=#EFF5FF,endColorstr=#E0ECFF,GradientType=0);
            padding: 5px;
            position: relative;
            border-width: 1px;
            border-style: solid;
            height: 16px;
            text-align: left;
        }
        .This_title
        {
            font-size: 12px;
            color: #0E2D5F;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript" language="javascript">

        $(function () {

            //#region 下拉菜单初始化
            //车辆完成状态 是否正常结束  下拉菜单
            $("#IsNormalFinish").combobox({
                valueField: "编码",
                textField: "名称",
                data: [{ 编码: 1, 名称: "正常完成" }, { 编码: 0, 名称: "异常结束"}],
                onLoadSuccess: function (data) {
                    $('#IsNormalFinish').combobox('setValue', '<%: tInfo.是否正常结束?1:0%>');
                    if ($('#IsNormalFinish').combobox('getValue') == 1) {
                        $('#trFinishNotNormal').hide();
                        $('#trRemark').hide();
                    } else {
                        $('#trFinishNotNormal').show();
                        $('#trRemark').show();
                    }
                },
                onSelect: function (rec) {
                    if (rec.编码 == 1) {
                        $('#trFinishNotNormal').hide();
                        $('#trRemark').hide();
                    } else {
                        $('#trFinishNotNormal').show();
                        $('#trRemark').show();
                    }
                }
            });

            var dr = '<%: ViewData["LtDriver"] %>';
            var doc = '<%: ViewData["LtDoctor"] %>';
            var nu = '<%: ViewData["LtNures"] %>';
            var li = '<%: ViewData["LtLitter"] %>';
            var sa = '<%: ViewData["LtSalver"] %>';
            //车辆 下拉菜单
            $('#AmbulanceCode').combobox({
                url: '/Notice/AmbulanceLoad/?ActionId=<%=Request.QueryString["ActionId"]%>',
                valueField: "车辆编码",
                textField: "实际标识",
                onLoadSuccess: function (data) {
                    $('#AmbulanceCode').combobox('setValue', '<%:tInfo.车辆编码%>');
                }
            });
            //司机多选 下拉菜单
            $('#Driver').combobox({
                url: '/BasicInfo/GetPersonList/?personType=3&stationCode=<%=tInfo.分站编码%>&isValid=1&ActionId=<%=Request.QueryString["ActionId"]%>',
                valueField: "EmpNo",
                textField: "Name",
                multiple: true,
                onLoadSuccess: function (data) {
                    $('#Driver').combobox('setValues', dr == "" ? "" : dr.split(','));
                }
            });
            //医生多选 下拉菜单
            $('#Doctor').combobox({
                url: '/BasicInfo/GetPersonList/?personType=4&stationCode=<%=tInfo.分站编码%>&isValid=1&ActionId=<%=Request.QueryString["ActionId"]%>',
                valueField: "EmpNo",
                textField: "Name",
                multiple: true,
                onLoadSuccess: function (data) {
                    $('#Doctor').combobox('setValues', doc == "" ? "" : doc.split(','));
                }
            });
            //护士多选 下拉菜单
            $('#Nurse').combobox({
                url: '/BasicInfo/GetPersonList/?personType=5&stationCode=<%=tInfo.分站编码%>&isValid=1&ActionId=<%=Request.QueryString["ActionId"]%>',
                valueField: "EmpNo",
                textField: "Name",
                multiple: true,
                onLoadSuccess: function (data) {
                    $('#Nurse').combobox('setValues', nu == "" ? "" : nu.split(','));
                }
            });
            //担架员 下拉菜单
            $('#Litter').combobox({
                url: '/BasicInfo/GetPersonList/?personType=6&stationCode=<%=tInfo.分站编码%>&isValid=1&ActionId=<%=Request.QueryString["ActionId"]%>',
                valueField: "EmpNo",
                textField: "Name",
                multiple: true,
                onLoadSuccess: function (data) {
                    $('#Litter').combobox('setValues', li == "" ? "" : li.split(','));
                }
            });
            //抢救员 下拉菜单
            $('#Salver').combobox({
                url: '/BasicInfo/GetPersonList/?personType=7&stationCode=<%=tInfo.分站编码%>&isValid=1&ActionId=<%=Request.QueryString["ActionId"]%>',
                valueField: "EmpNo",
                textField: "Name",
                multiple: true,
                onLoadSuccess: function (data) {
                    $('#Salver').combobox('setValues', sa == "" ? "" : sa.split(','));
                }
            });
            //#endregion

        });

        function changeStringToLenOrInt(str, len, returnvalue) {
            var s = new String(str);
            if (s == "" || s == null)
                return returnvalue;
            var result = s.match(/^-?(\d+(\.\d+)?)|(\d?(\.\d+)+)$/);
            if (result == null)
                return returnvalue;

            var IsInt = s.match(/^[+-]?\d+$/);
            if (IsInt == null)
                return parseFloat(s).toFixed(len);
            else
                return parseInt(s);
        }

        function Save() {
            if ($("#IsNormalFinish").combobox('getValue') == 0) {
                if ($("#AbnormalReasonId").val() == "" || $("#AbnormalReasonId").val() == "-1") {
                    alert("没有选择异常结束原因");
                    return;
                }
            }




//            var paramNames = "Code=" + $("#Code").val()
//            + "&IsNormalFinish=" + escape($("#IsNormalFinish").combobox('getValue'))
//            + "&AbnormalReasonId=" + +escape($("#AbnormalReasonId").val())
//            + "&Remark=" + escape($("#Remark").val())
//            + "&AmbulanceCode=" + escape($("#AmbulanceCode").combobox('getValue'))
//            + "&Driver=" + escape($("#Driver").combobox('getValues'))
//            + "&DriverName=" + escape($("#Driver").combobox('getText'))
//            + "&Doctor=" + escape($("#Doctor").combobox('getValues'))
//            + "&DoctorName=" + escape($("#Doctor").combobox('getText'))
//            + "&Nurse=" + escape($("#Nurse").combobox('getValues'))
//            + "&NurseName=" + escape($("#Nurse").combobox('getText'))
//            + "&Litter=" + escape($("#Litter").combobox('getValues'))
//            + "&LitterName=" + escape($("#Litter").combobox('getText'))
//            ;


            var paramNames = "entity.任务编码=" + $("#Code").val()
            + "&entity.是否正常结束=" + escape($("#IsNormalFinish").combobox('getValue'))
            + "&entity.异常结束原因编码=" + +escape($("#AbnormalReasonId").val())
            + "&entity.备注=" + escape($("#Remark").val())
            + "&entity.车辆编码=" + escape($("#AmbulanceCode").combobox('getValue'))
            + "&Driver=" + escape($("#Driver").combobox('getValues'))
            + "&entity.司机=" + escape($("#Driver").combobox('getText'))
            + "&Doctor=" + escape($("#Doctor").combobox('getValues'))
            + "&entity.医生=" + escape($("#Doctor").combobox('getText'))
            + "&Nurse=" + escape($("#Nurse").combobox('getValues'))
            + "&entity.护士=" + escape($("#Nurse").combobox('getText'))
            + "&Litter=" + escape($("#Litter").combobox('getValues'))
            + "&entity.担架工=" + escape($("#Litter").combobox('getText'))
            + "&Salver=" + escape($("#Salver").combobox('getValues'))
            + "&entity.抢救员=" + escape($("#Salver").combobox('getText'))

            + "&entity.行驶公里数=" + escape($("#txtXingShiKilo").val())
            + "&entity.急救公里数=" + escape($("#txtJiJiuKilo").val())
            + "&entity.实际救治人数=" + escape($("#txtJiJiuPCount").val())
            + "&IsNormalFinish=" + escape($("#IsNormalFinish").combobox('getValue'))
            + "&ActionId=<%=Request.QueryString["ActionId"]%>"
            ;
            for (i = 1; i < 8; i++) {
                paramNames += "&AmbulanceStateTime" + i + "=";
                if ($("#AmbulanceStateTime" + i).length > 0) {
                    paramNames += escape($("#AmbulanceStateTime" + i).datetimebox('getValue'));
                }
            }
            //alert(paramNames);
            $.ajax({
                type: "POST",
                async: false,
                url: "/BasicInfo/TaskSave/",
                data: paramNames,
                success: function (data, textStatus, jqXHR) {
                    $("#SaveResult").empty();
                    if (data == "") {
                        $("#SaveResult").append($("<font color='green'>写入数据库成功！</font>"));
                    } else {
                        $("#SaveResult").append($("<font color='red'>写入数据库失败！" + data + "</font>"));
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(2);
                }
            });
        }
        function closePage() {
            window.parent.closeWindowR();
        }

        function AbnormalReasonTree() {
            if ($('#abnormalReasonTree').children().length == 0) {
                $('#abnormalReasonTree').tree({
                    url: '/BasicInfo/GetTaskAbendReasonTree/',
                    onClick: function (node) {
                        $('#AbnormalReasonId').val(node.id)
                        $('#AbnormalReasonName').val(node.text)
                        $('#win').window('close');
                    }
                });
            }
            var $window = $('#win');
            $window = $window.window({
                title: '异常结束原因',
                width: 200,
                modal: true,
                shadow: true,
                height: 300,
                resizable: false
            });
            $window.window('open');
        }
    </script>
</head>
<body>
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="6" class="blockTable">
            <tr height="30px">
                <th align="left" valign="middle" class="TableInfoTH">
                    &nbsp;任务信息修改
                    <input id="Code" value="<%:tInfo.任务编码%>" type="hidden" name="entity.任务编码" />
                </th>
                <th align="right" valign="middle" class="TableInfoTH">
                    <div style="float: right;">
                        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true"
                            onclick="closePage();return false;"><span style="color: #15428B;">&nbsp;返回</span></a>
                    </div>
                    <div style="float: right;">
                        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true"
                            onclick="javascript:Save();return false;"><span style="color: #15428B;">&nbsp;保存</span></a>
                    </div>
                </th>
            </tr>
            <tr>
                <td style="padding: 0 8px 4px;" align="center" colspan="2">
                    <div id="divFinishInfo" class="easyui-panel" title="完成信息" style="padding: 10px;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tableInfo">
                            <tr>
                                <td style="width: 100px; text-align: left; vertical-align: middle">
                                    完成状态
                                </td>
                                <td align="left" valign="middle">
                                    &nbsp;
                                    <input class="easyui-combobox" id="IsNormalFinish" editable="false" name="entity.是否正常结束"
                                        style="width: 150px;" />
                                </td>
                                <td align="left" valign="middle">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr id="trFinishNotNormal">
                                <td align="left" valign="middle" class="TableInfoTD">
                                    异常结束原因
                                </td>
                                <td align="left" valign="middle" class="TableInfoTD" id="Td1">
                                    &nbsp;
                                    <input id="AbnormalReasonId" value="<%:tInfo.异常结束原因编码%>" type="hidden" name="entity.异常结束原因编码" />
                                    <input id="AbnormalReasonName" value="<%: ViewData["AbnormalReasonName"] %>" type="text"
                                        name="entity.异常结束原因" class="easyui-validatebox" disabled style="width: 150px;" />
                                    <%--<select id="AbnormalReasonId" panelheight="200px" class="easyui-combotree" name="entity.AbnormalReasonId"
                                        style="width: 150px;">
                                    </select>--%>
                                    <img alt="异常结束原因" onclick="javascript:AbnormalReasonTree()" src="../../Content/images/zhusu.png"
                                        style="cursor: pointer; width: 18px; height: 18px" />
                                </td>
                                <td align="left" valign="middle" class="TableInfoTD">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr id="trRemark">
                                <td align="left" valign="middle">
                                    备注
                                </td>
                                <td align="left" valign="middle">
                                    &nbsp;
                                    <textarea name="entity.Remark" rows="2" id="Remark" style="width: 300px;"><%:tInfo.备注%></textarea>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>

                            
                            <tr>
                                <td align="left" valign="middle" class="TableInfoTD">
                                    行驶公里数
                                </td>
                                <td align="left" valign="middle" class="TableInfoTD">
                                    &nbsp;
                                    <input id="txtXingShiKilo" value="<%: tInfo.行驶公里数%>" type="text" name="entity.行驶公里数" style="width:70px" onblur="this.value=changeStringToLenOrInt(this.value,2,'');"/>
                                    &nbsp; 急救公里数
                                    <input id="txtJiJiuKilo" value="<%: tInfo.急救公里数%>" type="text" name="entity.急救公里数" style="width:70px" onblur="this.value=changeStringToLenOrInt(this.value,2,'');"/>                                                        
                                    &nbsp;实际救治人数
                                    <input id="txtJiJiuPCount" value="<%: tInfo.实际救治人数%>" type="text" name="entity.实际救治人数" style="width:40px" onblur="this.value=changeStringToLenOrInt(this.value,0,0);"/>
                                </td>
                                <td class="TableInfoTD">
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divAmbInfo" class="easyui-panel" title="车辆信息" style="padding: 10px;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tableInfo">
                            <tr>
                                <td style="width: 100px; text-align: left; vertical-align: middle">
                                    车辆
                                </td>
                                <td align="left" valign="middle">
                                    &nbsp;
                                    <input class="easyui-combobox" id="AmbulanceCode" editable="false" name="entity.车辆编码"
                                        style="width: 200px;" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" class="TableInfoTD">
                                    司机
                                </td>
                                <td align="left" valign="middle" class="TableInfoTD" id="Name">
                                    &nbsp;
                                    <input class="easyui-combobox" id="Driver" editable="false" name="entity.司机"
                                        style="width: 200px;" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle">
                                    医生
                                </td>
                                <td align="left" valign="middle">
                                    &nbsp;
                                    <input class="easyui-combobox" id="Doctor" editable="false" name="entity.医生"
                                        style="width: 200px;" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" class="TableInfoTD">
                                    护士
                                </td>
                                <td align="left" valign="middle" class="TableInfoTD">
                                    &nbsp;
                                    <input class="easyui-combobox" id="Nurse" editable="false" name="entity.护士" style="width: 200px;" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle">
                                    担架员
                                </td>
                                <td align="left" valign="middle">
                                    &nbsp;
                                    <input class="easyui-combobox" id="Litter" editable="false" name="entity.担架工"
                                        style="width: 200px;" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle">
                                    抢救员
                                </td>
                                <td align="left" valign="middle">
                                    &nbsp;
                                    <input class="easyui-combobox" id="Salver" editable="false" name="entity.抢救员"
                                        style="width: 200px;" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divTaskTime" class="easyui-panel" title="事件节点信息" style="padding: 10px;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tableInfo">
                            <%
                                for (int i = 0, j = 0; i < 8; i++, j++)
                                {
                                    if (Anchor.FA.BLL.BasicInfo.AlarmEvent.GetAmbulanceStateName(zasLs[i]) == "")
                                        continue;
                            %>
                            <tr>
                                <td style="width: 100px; text-align: left; vertical-align: middle" class="<%:j%2==0?"":"TableInfoTD"%>">
                                    <%:Anchor.FA.BLL.BasicInfo.AlarmEvent.GetAmbulanceStateName(zasLs[i])%>
                                </td>
                                <td align="left" valign="middle" class="<%:j%2==0?"":"TableInfoTD"%>">
                                    &nbsp;
                                    <%
                                    if (i == 0)
                                    { 
                                    %>
                                    <%:AmbulanceStateTime[i]%>
                                    <%
                                    }
                                    else
                                    { 
                                    %>
                                    <input id="<%:"AmbulanceStateTime"+i.ToString() %>" class="easyui-datetimebox" value="<%:AmbulanceStateTime[i]%>"
                                        
                                        <%
                                        if (!Convert.ToBoolean(ViewData["EditTimeNode"]))
                                        { 
                                        %>
                                        readonly="readonly"
                                         <%
                                        }%>
                                        style="width: 150px">
                                    <%
                                    }%>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <%} %>
                        </table>
                    </div>
                </td>
            </tr>
            
            <tr>
                <td id="SaveResult" colspan="2" style="text-align: center;">
                </td>
            </tr>
        </table>
    </div>
    <div id="win">
        <ul id="abnormalReasonTree">
        </ul>
    </div>
</body>
</html>

<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<% 
    Anchor.FA.Model.C_AccInfo ac = ViewData["acInfo"] as Anchor.FA.Model.C_AccInfo;//受理


    List<Anchor.FA.Model.TParameterAcceptInfo> tpaLs = ViewData["tpaLs"] as List<Anchor.FA.Model.TParameterAcceptInfo>;//相关事件项 是否显示//设置调度个性名头 数据库配置
    string label_AlarmTel = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_AlarmTel", tpaLs);
    string label_AlarmReason = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_AlarmReason", tpaLs);
    string label_PatientName = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_PatientName", tpaLs);
    string label_Sex = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_Sex", tpaLs);
    string label_Age = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_Age", tpaLs);
    string label_National = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_National", tpaLs);
    string label_Folk = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_Folk", tpaLs);
    string label_Judge = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_Judge", tpaLs);
    string label_LinkMan = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_LinkMan", tpaLs);
    string label_LinkTel = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_LinkTel", tpaLs);
    string label_Extension = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_Extension", tpaLs);
    string label_LocalAddr = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_LocalAddr", tpaLs);
    string label_WaitAddr = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_WaitAddr", tpaLs);
    string label_SendAddr = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_SendAddr", tpaLs);
    string label_Remark = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_Remark", tpaLs);


    string label_IllState = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_IllState", tpaLs);
    string label_LocalAddrType = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_LocalAddrType", tpaLs);
    string label_SendAddrType = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_SendAddrType", tpaLs);
    string label_PatientCount = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_PatientCount", tpaLs);
    string label_SpecialNeed = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_SpecialNeed", tpaLs);
    string label_Reserve1 = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_Reserve1", tpaLs);
    string label_Reserve2 = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_Reserve2", tpaLs);
    string label_Mpds = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTianHtml("label_Mpds", tpaLs);
    
%>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
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
    <% 
        Anchor.FA.Model.C_AccInfo ac = ViewData["acInfo"] as Anchor.FA.Model.C_AccInfo;//受理

        List<Anchor.FA.Model.TParameterAcceptInfo> tpaLs = ViewData["tpaLs"] as List<Anchor.FA.Model.TParameterAcceptInfo>;//相关事件项 是否显示//设置调度个性名头 数据库配置
        string label_AlarmTel = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_AlarmTel", tpaLs);
        string label_AlarmReason = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_AlarmReason", tpaLs);
        string label_PatientName = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_PatientName", tpaLs);
        string label_Sex = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Sex", tpaLs);
        string label_Age = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Age", tpaLs);
        string label_National = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_National", tpaLs);
        string label_Folk = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Folk", tpaLs);
        string label_Judge = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Judge", tpaLs);
        string label_LinkMan = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_LinkMan", tpaLs);
        string label_LinkTel = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_LinkTel", tpaLs);
        string label_Extension = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Extension", tpaLs);
        string label_LocalAddr = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_LocalAddr", tpaLs);
        string label_WaitAddr = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_WaitAddr", tpaLs);
        string label_SendAddr = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_SendAddr", tpaLs);
        string label_Remark = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Remark", tpaLs);


        string label_IllState = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_IllState", tpaLs);
        string label_LocalAddrType = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_LocalAddrType", tpaLs);
        string label_SendAddrType = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_SendAddrType", tpaLs);
        string label_PatientCount = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_PatientCount", tpaLs);
        string label_SpecialNeed = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_SpecialNeed", tpaLs);
        string label_Reserve1 = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Reserve1", tpaLs);
        string label_Reserve2 = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Reserve2", tpaLs);
        string label_Mpds = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetText("label_Mpds", tpaLs);
        
        bool have_AlarmTel = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_AlarmTel", tpaLs);
        bool have_AlarmReason = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_AlarmReason", tpaLs);
        bool have_PatientName = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_PatientName", tpaLs);
        bool have_Sex = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_Sex", tpaLs);
        bool have_Age = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_Age", tpaLs);
        bool have_National = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_National", tpaLs);
        bool have_Folk = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_Folk", tpaLs);
        bool have_Judge = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_Judge", tpaLs);
        bool have_LinkMan = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_LinkMan", tpaLs);
        bool have_LinkTel = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_LinkTel", tpaLs);
        bool have_Extension = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_Extension", tpaLs);
        bool have_LocalAddr = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_LocalAddr", tpaLs);
        bool have_WaitAddr = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_WaitAddr", tpaLs);
        bool have_SendAddr = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_SendAddr", tpaLs);
        bool have_Remark = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_Remark", tpaLs);

        bool have_IllState = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_IllState", tpaLs);
        bool have_LocalAddrType = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_LocalAddrType", tpaLs);
        bool have_SendAddrType = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_SendAddrType", tpaLs);
        bool have_PatientCount = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_PatientCount", tpaLs);
        bool have_SpecialNeed = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_SpecialNeed", tpaLs);
        bool have_Reserve1 = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_Reserve1", tpaLs);
        bool have_Reserve2 = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_Reserve2", tpaLs);
        bool have_Mpds = Anchor.FA.BLL.BasicInfo.AlarmEvent.GetIsBiTian("label_Mpds", tpaLs);
    %>
    <script type="text/javascript" language="javascript">


        $(function () {
        
            //#region 下拉菜单初始化



            //病情编码
            <% if(label_IllState!="")
            { 
            %>
            EUIcombobox("#DropDownList_IllStateId", {
                valueField: "编码",
                textField: "名称",
                OneOption: [{
                    编码: "-1",
                    名称: "--请选择--"
                }],
                url: "/BasicInfo/LoadIllStates/",
                onLoadSuccess: function (data) {
                    $('#DropDownList_IllStateId').combobox('setValue', '<%: ac.IllStateId%>');
                }

            });
            <%}else{%>
            $("#DropDownList_IllStateId").parent().children().hide();
            <%}%>

            //往救地点类型编码
            <% if(label_LocalAddrType!="")
            { 
            %>
            EUIcombobox("#DropDownList_LocalAddrTypeId", {
                valueField: "编码",
                textField: "名称",
                OneOption: [{
                    编码: "-1",
                    名称: "--请选择--"
                }],
                url: "/BasicInfo/LoadLocalAddrTypes/",
                onLoadSuccess: function (data) {
                    $('#DropDownList_LocalAddrTypeId').combobox('setValue', '<%: ac.LocalAddrTypeId%>');
                }
            });
            <%}else{%>
            $("#DropDownList_LocalAddrTypeId").parent().children().hide();
            <%}%>
            
            //送往地点类型编码
            <% if(label_SendAddrType!="")
            { 
            %>
            EUIcombobox("#DropDownList_SendAddrTypeId", {
                valueField: "编码",
                textField: "名称",
                OneOption: [{
                    编码: "-1",
                    名称: "--请选择--"
                }],
                url: "/BasicInfo/LoadSendAddrTypes/",
                onLoadSuccess: function (data) {
                    $('#DropDownList_SendAddrTypeId').combobox('setValue', '<%: ac.SendAddrTypeId%>');
                }
            });
            <%}else{%>
            $("#DropDownList_SendAddrTypeId").parent().children().hide();
            <%}%>
                       
            //特殊要求
            <% if(label_SpecialNeed!="")
            { 
            %>
            $("#ComboBox_SpecialNeed").combobox({
                valueField: "名称",
                textField: "名称",
                url: "/BasicInfo/LoadSpecialRequests/",
                onLoadSuccess: function (data) {
                    $('#ComboBox_SpecialNeed').combobox('setValue', '<%:ac.SpecialNeed %>');
                }
            });
            <%}else{%>
            $("#ComboBox_SpecialNeed").parent().children().hide();
            <%}%>
                       
            //保留字段1
            <% if(label_Reserve1!="")
            { 
            %>
            $("#ComboBox_BackUpOne").combobox({
                valueField: "名称",
                textField: "名称",
                url: "/BasicInfo/LoadBackupOnes/",
                onLoadSuccess: function (data) {
                    $('#ComboBox_BackUpOne').combobox('setValue', '<%: ac.BackUpOne%>');
                }
            });
            <%}else{%>
            $("#ComboBox_BackUpOne").parent().children().hide();
            <%}%>
                       
            //保留字段2
            <% if(label_Reserve2!="")
            { 
            %>
            $("#ComboBox_BackUpTwo").combobox({
                valueField: "名称",
                textField: "名称",
                url: "/BasicInfo/LoadBackupTwos/",
                onLoadSuccess: function (data) {
                    $('#ComboBox_BackUpTwo').combobox('setValue', '<%: ac.BackUpTwo%>');
                }
            });
            <%}else{%>
            $("#ComboBox_BackUpTwo").parent().children().hide();
            <%}%>



            //性别
            <% if(label_Sex!="")
            { 
            %>
            $("#DropDownList_Sex").combobox({
                valueField: "名称",
                textField: "名称",
                data:[{名称: "不详"},{名称: "男"},{名称: "女"}],
                onLoadSuccess: function (data) {
                    $('#DropDownList_Sex').combobox('setValue', '<%: ac.Sex%>');
                }
            });
            <%}else{%>
            $("#DropDownList_Sex").parent().children().hide();
            <%}%>

            //年龄
            <% if(label_Age!=""){ %>
            $("#ComboBox_Age").combobox({
                valueField: "名称",
                textField: "名称",
                url: "/BasicInfo/LoadAges/",
                onLoadSuccess: function (data) {
                    $('#ComboBox_Age').combobox('setValue', '<%: ac.Age%>');
                }
            });
            <%}else{%>
           $("#ComboBox_Age").parent().children().hide();
            <%}%>

            //国籍
            <% if(label_National!=""){ %>
            $("#ComboBox_National").combobox({
                valueField: "名称",
                textField: "名称",
                url: "/BasicInfo/LoadNationals/",
                onLoadSuccess: function (data) {
                    $('#ComboBox_National').combobox('setValue', '<%: ac.National%>');
                }
            });
            <%}else{%>
           $("#ComboBox_National").parent().children().hide();
            <%}%>

            //民族
            <% if(label_Folk!=""){ %>
            $("#ComboBox_Folk").combobox({
                valueField: "名称",
                textField: "名称",
                url: "/BasicInfo/LoadFolks/",
                onLoadSuccess: function (data) {
                    $('#ComboBox_Folk').combobox('setValue', '<%: ac.Folk%>');
                }
            });
            <%}else{%>
           $("#ComboBox_Folk").parent().children().hide();
            <%}%>

            //主诉 这个得做树形
            <% if(label_AlarmReason !=""){ %>
                  
            <%}else{%>
            $("#AlarmReason").parent().children().hide();
            <%}%>

            //患者姓名
            <% if(label_PatientName ==""){ %>
                $("#PatientName").hide();
            <%}%>
            //联系人
            <% if(label_LinkMan ==""){ %>
                $("#LinkMan").hide();
            <%}%>
            //联系电话
            <% if(label_LinkTel ==""){ %>
                $("#LinkTel").hide();
            <%}%>
            //分机
            <% if(label_Extension ==""){ %>
                $("#Extension").hide();
            <%}%>
            //现场地址
            <% if(label_LocalAddr ==""){ %>
                $("#LocalAddr").hide();
            <%}%>
            //等车地点
            <% if(label_WaitAddr ==""){ %>
                $("#WaitAddr").hide();
            <%}%>
            //送往地点
            <% if(label_SendAddr ==""){ %>
                $("#SendAddr").hide();
            <%}%>
            //患者人数
            <% if(label_PatientCount ==""){ %>
                $("#PatientCount").hide();
            <%}%>
            //MPDS
            <% if(label_Mpds ==""){ %>
                $("#MPDSRemark").hide();
            <%}%>


            //#endregion

        });

        function checkInt(src){
            var str=$(src).val();
            if(str.length==0)
            {
                $(src).next().show();
                return false;
            }
            var reg=/^[-+]?\d+$/; 
            if(reg.test(str))
            {
                $(src).next().hide();
                return true;
            }
            $(src).next().show();
            return false;
        }


        function Save()
        {
            //#region 数据检查
            var result="";
            <% if(have_AlarmReason){ %>
                if($.trim($("#AlarmReason").val())=="")
                {
                    result = result + "<%:label_AlarmReason%>、";
                }
            <%}%>
            <% if(have_PatientName){ %>
                if($.trim($("#PatientName").val())=="")
                {
                    result = result + "<%:label_PatientName%>、";
                }
            <%}%>
            <% if(have_Sex){ %>
                if($.trim($("#DropDownList_Sex").combobox('getValue'))=="")
                {
                    result = result  + "<%:label_Sex%>、";
                }
            <%}%>
            <% if(have_Age){ %>
                if($.trim($("#ComboBox_Age").combobox('getValue'))=="")
                {
                    result = result  + "<%:label_Age%>、";
                }
            <%}%>
            <% if(have_National){ %>
                if($.trim($("#ComboBox_National").combobox('getValue'))=="")
                {
                    result = result  + "<%:label_National%>、";
                }
            <%}%>
            <% if(have_Folk){ %>
                if($.trim($("#ComboBox_Folk").combobox('getValue'))=="")
                {
                    result = result  + "<%:label_Folk%>、";
                }
            <%}%>
            <% if(have_Judge){ %>
                if($.trim($("#Judge").val())=="")
                {
                    result = result  + "<%:label_Judge%>、";
                }
            <%}%>
            <% if(have_LinkMan){ %>
                if($.trim($("#LinkMan").val())=="")
                {
                    result = result  + "<%:label_LinkMan%>、";
                }
            <%}%>
            <% if(have_LinkTel){ %>
                if($.trim($("#LinkTel").val())=="")
                {
                    result = result  + "<%:label_LinkTel%>、";
                }
            <%}%>
            <% if(have_Extension){ %>
                if($.trim($("#Extension").val())=="")
                {
                    result = result  + "<%:label_Extension%>、";
                }
            <%}%>
            <% if(have_LocalAddr){ %>
                if($.trim($("#LocalAddr").val())=="")
                {
                    result = result  + "<%:label_LocalAddr%>、";
                }
            <%}%>
            <% if(have_WaitAddr){ %>
                if($.trim($("#WaitAddr").val())=="")
                {
                    result = result  + "<%:label_WaitAddr%>、";
                }
            <%}%>
            <% if(have_SendAddr){ %>
                if($.trim($("#SendAddr").val())=="")
                {
                    result = result  + "<%:label_SendAddr%>、";
                }
            <%}%>
            <% if(have_Remark){ %>
                if($.trim($("#Remark").val())=="")
                {
                    result = result  + "<%:label_Remark%>、";
                }
            <%}%>
            <% if(have_IllState){ %>
                var ill=$.trim($("#DropDownList_IllStateId").combobox('getValue'));
                if(ill=="-1" ||ill=="" ||ill=="--请选择--")
                {
                    result = result  + "<%:label_IllState%>、";
                }
            <%}%>
            <% if(have_LocalAddrType){ %>
                var laddr=$.trim($("#DropDownList_LocalAddrTypeId").combobox('getValue'));
                if(laddr=="-1" ||laddr=="" ||laddr=="--请选择--")
                {
                    result = result  + "<%:label_LocalAddrType%>、";
                }
            <%}%>
            <% if(have_SendAddrType){ %>
                var saddr=$.trim($("#DropDownList_SendAddrTypeId").combobox('getValue'));
                if(saddr=="-1" ||saddr=="" ||saddr=="--请选择--")
                {
                    result = result  + "<%:label_SendAddrType%>、";
                }
            <%}%>
            <% if(have_PatientCount){ %>
                if($.trim($("#PatientCount").val())=="")
                {
                    result = result  + "<%:label_PatientCount%>、";
                }
            <%}%>
            <% if(label_PatientCount!=""){ %>
                if(!checkInt($("#PatientCount")[0]))
                    return false;
            <%}%>


            <% if(have_SpecialNeed){ %>
                if($.trim($("#ComboBox_SpecialNeed").combobox('getValue'))=="")
                {
                    result = result  + "<%:label_SpecialNeed%>、";
                }
            <%}%>
            <% if(have_Reserve1){ %>
                if($.trim($("#ComboBox_BackUpOne").combobox('getValue'))=="")
                {
                    result = result  + "<%:label_Reserve1%>、";
                }
            <%}%>
            <% if(have_Reserve2){ %>
                if($.trim($("#ComboBox_BackUpTwo").combobox('getValue'))=="")
                {
                    result = result  + "<%:label_Reserve2%>、";
                }
            <%}%>
            <% if(have_Mpds){ %>
                if($.trim($("#MPDSRemark").val())=="")
                {
                    result = result  + "<%:label_Mpds%>、";
                }
            <%}%>
            
            if (result.length > 0)
            {
                result = result.substring(0,result.length-1) + " 没填！";
                alert(result);
                return false;
            }
                            //#endregion
                            var paramNames="EventCode="+$("#EventCode").val()
                            +"&AcceptOrder="+$("#AcceptOrder").val()
                            +"&AlarmReason="+escape($("#AlarmReason").val())
                            +"&PatientName="+escape($("#PatientName").val())
                            +"&DropDownList_Sex="+escape($("#DropDownList_Sex").combobox('getValue'))
                            +"&ComboBox_Age="+escape($("#ComboBox_Age").combobox('getValue'))
                            +"&ComboBox_National="+escape($("#ComboBox_National").combobox('getValue'))
                            +"&ComboBox_Folk="+escape($("#ComboBox_Folk").combobox('getValue'))
                            +"&Judge="+escape($("#Judge").val())
                            +"&LinkMan="+escape($("#LinkMan").val())
                            +"&LinkTel="+escape($("#LinkTel").val())
                            +"&Extension="+escape($("#Extension").val())
                            +"&LocalAddr="+escape($("#LocalAddr").val())
                            +"&WaitAddr="+escape($("#WaitAddr").val())
                            +"&SendAddr="+escape($("#SendAddr").val())
                            +"&IsNeedLitter="+($("#IsNeedLitter").attr("checked") ? "1" : "0")
                            +"&Remark="+escape($("#Remark").val())

                            +"&DropDownList_IllStateId="+escape($("#DropDownList_IllStateId").combobox('getValue'))
                            +"&DropDownList_LocalAddrTypeId="+escape($("#DropDownList_LocalAddrTypeId").combobox('getValue'))
                            +"&DropDownList_SendAddrTypeId="+escape($("#DropDownList_SendAddrTypeId").combobox('getValue'))
                            +"&PatientCount="+escape($("#PatientCount").val())
                            +"&ComboBox_SpecialNeed="+escape($("#ComboBox_SpecialNeed").combobox('getValue'))
                            +"&ComboBox_BackUpOne="+escape($("#ComboBox_BackUpOne").combobox('getValue'))
                            +"&ComboBox_BackUpTwo="+escape($("#ComboBox_BackUpTwo").combobox('getValue'))
                            +"&MPDSRemark="+escape($("#MPDSRemark").val())



            //alert(paramNames);
            $.ajax({
                type: "POST",
                async: false,
                url: "/BasicInfo/AcceptEventSave/",
                data: paramNames,
                success: function (data, textStatus, jqXHR) {
                    $("#SaveResult").empty();
                    if(data==""){
                    $("#SaveResult").append($("<font color='green'>写入数据库成功！</font>"));
                    }else{
                    $("#SaveResult").append($("<font color='red'>写入数据库失败！"+data+"</font>"));
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
        function AlarmReasonTree() {
            if ($('#AlarmReasonTree').children().length==0) {
                $('#AlarmReasonTree').tree({
                    checkbox:true,
                    url: '/BasicInfo/GetAlarmReasonsTree/', 
                });
            }
            var $window = $('#win');
            $window = $window.window({
                title: '主诉列表',
                width: 200,
                modal: true,
                shadow: true,
                height: 300,
                resizable: false
            });
            $window.window('open');
        }
        
        function AlarmReasonTreeSelect() {
            var nodes = $('#AlarmReasonTree').tree('getChecked');  
            var s = '';  
            for(var i=0; i<nodes.length; i++){  
                if (s != '') s += ',';  
                s += nodes[i].text;  
            }  
            $('#AlarmReason').val(s)
            $('#win').window('close');
        }
    </script>
</head>
<body>
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr height="30px">
                <th colspan="5" align="left" valign="middle" class="TableInfoTH">
                    &nbsp;受理调度详细信息
                    <input id="EventCode" value="<%:ac.EventCode%>" type="hidden" name="entity.EventCode" /><input
                        id="AcceptOrder" value="<%:ac.AcceptOrder%>" type="hidden" name="entity.AcceptOrder" />
                </th>
                <th align="right" valign="middle" class="TableInfoTH">
                    <div style="float: right;">
                        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true"
                            onclick="closePage()"><span style="color: #15428B;">&nbsp;返回</span></a>
                    </div>
                    <div style="float: right;">
                        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true"
                            onclick="javascript:Save()"><span style="color: #15428B;">&nbsp;保存</span></a>
                    </div>
                </th>
            </tr>
            <tr height="30px">
                <td align="right" valign="middle">
                    <%: Html.Raw(label_AlarmTel)%>
                </td>
                <td align="left" valign="middle">
                    <%:ac.AlarmTel%>
                </td>
                <td align="right" valign="middle">
                    <%: Html.Raw(label_AlarmReason)%>
                </td>
                <td align="left" valign="middle">
                    <input id="AlarmReason" value="<%: ac.AlarmReason%>" type="text" style="width: 75%"
                        name="entity.AlarmReason" />
                    <img alt="主诉" onclick="javascript:AlarmReasonTree()" src="../../Content/images/zhusu.png"
                        style="cursor: pointer; width: 18px; height: 18px" />
                </td>
                <td align="right" valign="middle">
                    <%: Html.Raw(label_IllState)%>
                </td>
                <td align="left" valign="middle">
                    <input class="easyui-combobox" id="DropDownList_IllStateId" editable="false" name="entity.IllStateId" />
                </td>


            </tr>
            <tr height="30px">
                <td width="15%" align="right" valign="middle" class="TableInfoTD">
                    <%: Html.Raw(label_PatientName)%>
                </td>
                <td width="18%" align="left" valign="middle" class="TableInfoTD">
                    <input id="PatientName" value="<%: ac.PatientName%>" type="text" name="entity.PatientName" />
                </td>
                <td width="15%" align="right" valign="middle" class="TableInfoTD">
                    <%: Html.Raw(label_Sex)%>
                </td>
                <td width="18%" align="left" valign="middle" class="TableInfoTD">
                    <input class="easyui-combobox" id="DropDownList_Sex" editable="false" name="entity.Sex" />
                </td>
                <td width="15%" align="right" valign="middle" class="TableInfoTD">
                    <%: Html.Raw(label_Age)%>
                </td>
                <td width="19%" align="left" valign="middle" class="TableInfoTD">
                    <input class="easyui-combobox" id="ComboBox_Age" editable="true" name="entity.Age" />
                </td>
            </tr>
            <tr height="30px">
                <td align="right" valign="middle">
                    <%: Html.Raw(label_National)%>
                </td>
                <td align="left" valign="middle">
                    <input class="easyui-combobox" id="ComboBox_National" editable="true" name="entity.National" />
                </td>
                <td align="right" valign="middle">
                    <%: Html.Raw(label_Folk)%>
                </td>
                <td align="left" valign="middle">
                    <input class="easyui-combobox" id="ComboBox_Folk" editable="true" name="entity.label_Folk" />
                </td>
                <td align="right" valign="middle">
                    <%: Html.Raw(label_Judge)%>
                </td>
                <td align="left" valign="middle">
                    <input id="Judge" value="<%:ac.Judge%>" type="text" name="entity.Judge" />
                </td>
            </tr>
            <tr height="30px">
                <td align="right" valign="middle" class="TableInfoTD">
                    <%: Html.Raw(label_LinkMan)%>
                </td>
                <td align="left" valign="middle" class="TableInfoTD">
                    <input id="LinkMan" value="<%:ac.LinkMan%>" type="text" name="entity.LinkMan" />
                </td>
                <td align="right" valign="middle" class="TableInfoTD">
                    <%: Html.Raw(label_LinkTel)%>
                </td>
                <td align="left" valign="middle" class="TableInfoTD">
                    <input id="LinkTel" value="<%:ac.LinkTel%>" type="text" name="entity.LinkTel" />
                </td>
                <td align="right" valign="middle" class="TableInfoTD">
                    <%: Html.Raw(label_Extension)%>
                </td>
                <td align="left" valign="middle" class="TableInfoTD">
                    <input id="Extension" value="<%:ac.Extension%>" type="text" name="entity.Extension" />
                </td>
            </tr>
            <tr height="30px">
                <td align="right" valign="middle">
                    <%: Html.Raw(label_LocalAddr)%>
                </td>
                <td colspan="3" align="left" valign="middle">
                    <input id="LocalAddr" value="<%:ac.LocalAddr%>" style="width: 97%" type="text" name="entity.LocalAddr" />
                </td>
                
                <td align="right" valign="middle">
                    <%: Html.Raw(label_LocalAddrType)%>
                </td>
                <td align="left" valign="middle">
                    <input class="easyui-combobox" id="DropDownList_LocalAddrTypeId" editable="false" name="entity.LocalAddrTypeId" />
                </td>



            </tr>
            <tr height="30px">
                <td align="right" valign="middle" class="TableInfoTD">
                    <%: Html.Raw(label_WaitAddr)%>
                </td>
                <td colspan="3" align="left" valign="middle" class="TableInfoTD">
                    <input id="WaitAddr" value="<%:ac.WaitAddr%>" style="width: 97%" type="text" name="entity.WaitAddr" />
                </td>
                <td align="right" valign="middle" class="TableInfoTD">
                </td>
                <td align="left" valign="middle" class="TableInfoTD">
                </td>
            </tr>
            <tr height="30px">
                <td align="right" valign="middle">
                    <%: Html.Raw(label_SendAddr)%>
                </td>
                <td colspan="3" align="left" valign="middle">
                    <input id="SendAddr" value="<%:ac.SendAddr%>" style="width: 97%" type="text" name="entity.SendAddr" />
                </td>
                
                <td align="right" valign="middle">
                    <%: Html.Raw(label_SendAddrType)%>
                </td>
                <td align="left" valign="middle">
                    <input class="easyui-combobox" id="DropDownList_SendAddrTypeId" editable="false" name="entity.SendAddrTypeId" />
                </td>



            </tr>
            <tr height="30px">
                
                <td align="right" valign="middle" class="TableInfoTD">
                    <%--患者人数：--%>
                    <%: Html.Raw(label_PatientCount)%>
                </td>
                <td align="left" valign="middle" class="TableInfoTD">
                    <input onblur="checkInt(this)" id="PatientCount" value="<%: ac.PatientCount%>" type="text" name="entity.PatientCount" style="width:50px"/><span style="color:Red;display:none;">整数</span><%--display:none;--%>
                </td>
                <td align="right" valign="middle" class="TableInfoTD">
                    是否需要担架：
                </td>
                <td align="left" valign="middle" class="TableInfoTD">
                    <input id="IsNeedLitter" type="checkbox" name="entity.IsNeedLitter" <%:ac.IsNeedLitter?"checked='checked'":""%> />
                </td>
                <td align="right" valign="middle" class="TableInfoTD">
                    <%--特殊要求：--%>
                    <%: Html.Raw(label_SpecialNeed)%>
                </td>
                <td align="left" valign="middle" class="TableInfoTD">
                    <input class="easyui-combobox" id="ComboBox_SpecialNeed" editable="true" name="entity.SpecialNeed" />
                </td>
            </tr>
            
            <tr height="30px">
                <td align="right" valign="middle">
                    <%--保留字段1--%>
                    <%: Html.Raw(label_Reserve1)%>
                </td>
                <td align="left" valign="middle">
                    <input class="easyui-combobox" id="ComboBox_BackUpOne" editable="true" name="entity.BackUpOne"/>
                    
                </td>
                <td align="right" valign="middle">
                    <%: Html.Raw(label_Reserve2)%>
                </td>
                <td align="left" valign="middle">
                    <input class="easyui-combobox" id="ComboBox_BackUpTwo" editable="true" name="entity.BackUpTwo"/>
                </td>
                <td align="right" valign="middle">
                    &nbsp;
                </td>
                <td align="left" valign="middle">
                    &nbsp;
                </td>
            </tr>

            <tr height="30px">
                <td align="right" valign="middle" class="TableInfoTD">
                    责任受理人：
                </td>
                <td align="left" valign="middle" class="TableInfoTD">
                    <%: ac.Dispatcher%>
                </td>
                <td align="right" valign="middle" class="TableInfoTD">
                    受理类型：
                </td>
                <td align="left" valign="middle" class="TableInfoTD">
                    <%: ac.AcceptType%>
                </td>
                <td align="right" valign="middle" class="TableInfoTD">
                    电话振铃时刻：
                </td>
                <td align="left" valign="middle" class="TableInfoTD">
                    <%: ac.RingTime.ToString()%>
                </td>
            </tr>
            <tr height="30px">
                <td align="right" valign="middle">
                    开始受理时刻：
                </td>
                <td align="left" valign="middle">
                    <%: ac.AcceptBeginTime.ToString()%>
                </td>
                <td align="right" valign="middle">
                    结束受理时刻：
                </td>
                <td align="left" valign="middle">
                    <%: ac.AcceptEndTime.ToString()%>
                </td>
                <td align="right" valign="middle">
                    发送指令时刻：
                </td>
                <td align="left" valign="middle">
                    <%: ac.CommandTime.ToString()%>
                </td>
            </tr>
            <tr height="30px">
                <td colspan="6" align="left" valign="middle" class="TableInfoTD">
                    &nbsp;&nbsp;<%--备注--%><%: Html.Raw(label_Remark)%>
                </td>
            </tr>
            <tr height="30px">
                <td colspan="6" align="left" valign="middle">
                    <center>
                    <input id="Remark" value="<%:ac.Remark%>" style="width:99%" type="text" name="entity.Remark" />
                    </center>
                </td>
            </tr>
            <tr height="30px">
                <td colspan="6" align="left" valign="middle" class="TableInfoTD">
                    &nbsp;&nbsp;<%: Html.Raw(label_Mpds)%>
                </td>
            </tr>
            <tr height="30px">
                <td colspan="6" align="left" valign="middle">
                    <center>
                    <input id="MPDSRemark" value="<%:ac.MPDSRemark%>" style="width:99%" type="text" name="entity.MPDSRemark" />
                    </center>
                </td>
            </tr>
            <tr>
                <td id="SaveResult" colspan="6" style="text-align: center;">
                </td>
            </tr>
        </table>
    </div>
    <div id="win" data-options="tools:'#winTool'">
        <ul id="AlarmReasonTree">
        </ul>
    </div>
    <div id="winTool">
        <a href="#" class="icon-edit" onclick="javascript:AlarmReasonTreeSelect();return false;">
        </a>
    </div>
</body>
</html>

<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Default</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript">
        function openit(target, href) {
            $("#detail").html('<iframe height="100%" width="100%"   marginheight="0" marginwidth="0" scrolling="auto" frameborder="0" src="' + href + '"></iframe>');
            $.mobile.go('#detail');
        }
    </script>
</head>
<body>
    <div class="easyui-navpanel">
       <header>
            <div class="m-toolbar">
                <div class="title">报表</div>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>  
            </div>
        </header>
        <ul class="m-list">
            <li class="m-list-group">调度统计</li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/Time_Mode.aspx?code=Dispatcher_YPWCTime.rdlc&open=1')">各时段电话/出车/回车/待派统计</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/Text_Mode.aspx?code=Dispatcher_CGDPCLS.rdlc&open=1')">超规定时间出车流水表</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/Time_Mode.aspx?code=Tel_HWFL.rdlc&open=1')">话务分类统计</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/DeskChang_Mode.aspx?code=Dispather_TZTGBLS.rdlc&open=1')">调度席状态变化流水表</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/Time_Mode.aspx?code=DispatcherWorkSituation.rdlc&open=1')">调度员工作情况统计表</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/TaskFlowTable_Mode.aspx?code=TaskFlowTable.rdlc&open=1')">任务流水统计日报表</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/Time_Mode.aspx?code=Dispather_YPWCQY.rdlc&open=1')">欲派无车区域统计</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/Time_Mode.aspx?code=TaskArea.rdlc&open=1')">出车区域统计表</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/RejectReason_Mode.aspx?code=RejectReason.rdlc&open=1')">欲派无车任务流水</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/CheckBoxList_Mode.aspx?code=TaskAbendReason.rdlc&open=1')">中止任务原因统计</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/ComboBox_Mode.aspx?code=LSAttemperDispatchOutTime.rdlc&open=1')">调度员调度超时流水</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/Text_Tele.aspx?code=Tele_GaoFeng.rdlc&open=1')">电话高峰时段情况统计</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/Text_Tele.aspx?code=Tele_QuanTian.rdlc&open=1')">电话全天情况统计</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/DropDownList_Mode.aspx?code=PrintStdAcdTongji.rdlc&open=1')">重大事故统计</a></li>
            <li class="m-list-group">分站统计</li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/Station_Mode.aspx?code=Driver_GZQKTJ.rdlc&open=1')">司机工作情况统计</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/Station_Mode.aspx?code=Doctor_GZQKTJ.rdlc&open=1')">医生工作情况统计</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/Station_Mode.aspx?code=Nurse_GZQKTJ.rdlc&open=1')">护士工作情况统计</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/DropDownList_Mode.aspx?code=Dispather_JJRWCL.rdlc&open=1')">急救任务处理时间统计</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/AmbulancePersonSign_Mode.aspx?code=AmbulancePersonSign.rdlc&open=1')">人员上下班操作查询</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/Time_Mode.aspx?code=ZTDYLS.rdlc&open=1')">暂停调用流水</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/CheckBoxList_Mode.aspx?code=TaskOfTimeSpace.rdlc&open=1')">各时间段出车统计表</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/Station_Mode.aspx?code=ZTDYTJ.rdlc&open=1')">司机暂停调用次数统计</a></li>
            <li class="m-list-group">病历&收费统计</li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/LS.aspx?code=Patient_LS.rdlc&open=1')">病历流水表</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/LS.aspx?code=Charge_LS.rdlc&open=1')">收费流水表</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/Charge.aspx?code=Charge_ForPerson.rdlc&open=1')">收费统计-按人员</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/Charge.aspx?code=Charge_ForItem.rdlc&open=1')">收费统计-按收费项</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/Time_Mode.aspx?code=Patient_IllType.rdlc&open=1')">疾病分类统计一</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/Time_Mode.aspx?code=Patient_IllType2.rdlc&open=1')">疾病分类统计二</a></li>
            <li class="m-list-group">MPDS统计</li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/Time_Mode.aspx?code=TJAttemperHangUp.rdlc&open=1')">调度员受理时间统计</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/Time_Mode.aspx?code=TJAttemperDispatch.rdlc&open=1')">调度员调度时间统计</a></li>
            <li><a href="javascript:void(0)" onclick="openit(this,'/Areas/Report/Statistics/Time_Mode.aspx?code=TJAttemperHangUp.rdlc&open=1')">MPDS百分比统计</a></li>
    </div>

    <div id="detail" class="easyui-navpanel">
        <header>
            <div class="m-toolbar">
                <div class="title">报表</div>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>  
            </div>
        </header>
    </div>
</body>
</html>

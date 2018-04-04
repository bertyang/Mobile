<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>事件管理</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script src="/Content/Script/jquery.maskedinput-1.2.2.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
       
      
        jQuery.fn.extend({
            EUIcombobox: function(o) {
                //var jqS=this;
                $.ajax({
                    type: "POST",
                    //async: false, //去掉此处默认异步加载更快
                    url: o.url,
                    dataType: "JSON",
                    jqS:this
                    //success: function (data1, textStatus, jqXHR) {
                    //    o.data=o.OneOption.concat(data1);
                    //    delete o.url;
                    //    delete o.OneOption;
                    //    jqS.combobox(o);
                    //},
                    //error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //    alert(o.url);
                    //}
                })
                .done(function(data1, textStatus, jqXHR)
                {
                    o.data=o.OneOption.concat(data1);
                    delete o.url;
                    delete o.OneOption;
                    this.jqS.combobox(o);
                })
                .fail(function(XMLHttpRequest, textStatus, errorThrown){
                    alert(o.url);
                    //alert(XMLHttpRequest.responseText);
                });
            }
        });


    //#region 查询
    function getAjaxData(url,data)
    {
        var ReData;
        ReData=$.ajax({
            type: "POST",
            url: url,
            dataType: "JSON",
            data:data
            //async: false,
            //success: function (data1, textStatus, jqXHR) {
            //    ReData=data1;
            //},
            //error: function (XMLHttpRequest, textStatus, errorThrown) {
            //    alert(o.url);
            //    ReData=null;
            //}
        })
        .fail(function(XMLHttpRequest, textStatus, errorThrown){
            alert(this.url);
            //alert(XMLHttpRequest.responseText);
        });
        //ReData.ErrorReUrl=url;
        return ReData;
    }

    function GetDatagridData(page,rows,order,sort)
    {
        var data={
                begin: $('#BeginDate').datebox('getValue')+' '+$('#BeginTime').val(),
                end: $('#EndDate').datebox('getValue')+' '+ $('#EndTime').val(),
                c_begin: $('#c_BeginDate').datebox('getValue')+' '+$('#c_BeginTime').val(),
                c_end: $('#c_EndDate').datebox('getValue')+' '+ $('#c_EndTime').val(),
                tel: $('#txt_TelCode').val(),
                Addr: $('#txt_LocalAddr').val(),
                Dri: $('#Driver').val(),
                Doc: $('#Doctor').val(),
                Nur: $('#Nurse').val(),
                Dis: $('#txt_Dis').combobox('getValue'),
                sta: $('#txt_Station').combobox('getValue'),
                Alum: $('#txt_Alums').combobox('getValue'),
                type: $('#AlarmTypes').combobox('getValue'),
                ori: $('#OriTypes').combobox('getValue'),
                SuffererName:$('#txt_SuffererName').val(),
                ZhuSu:$('#txt_ZhuSu').val(),
                SendAddress:$('#txt_SendAddress').val(),
                IllState:$('#IllState').combobox('getValue'),
                IsTest:$('#IsTest').combobox('getValue'),
                Judge:$('#txt_Judge').val(),
                AlarmEventCode:$('#AlarmEventCode').val(),
                ActionId:'<%=ViewData["ActionId"]%>'
                }
                data.page=page;
                data.rows=rows;
                data.order=order;
                data.sort=sort;

        return getAjaxData('/BasicInfo/AlarmEventSearch/',data);
    }


        function Search(pPageIndex,pPageSize) {

            var pager = $('#grid').datagrid('getPager');
            var o=pager.data("pagination").options;
            var gr = $('#grid').datagrid('options');
            if(pPageIndex==null)
            {
                pPageIndex=1;
            }
            if(pPageSize==null)
            {
                pPageSize=o.pageSize;
            }
            o.pageNumber=pPageIndex;
            o.pageSize=pPageSize;
            var oData=GetDatagridData(pPageIndex,pPageSize,gr.sortName,gr.sortOrder);
            oData.done(function(data1, textStatus, jqXHR)//实现异步调用
            {
                pager.pagination({
                    displayMsg:"共{total}事件、"+data1.tacc+"受理、"+data1.ttc+"任务、"+data1.ttnc+"有效任务"//显示{from}到{to}，
                });
                $('#grid').datagrid('loadData',data1);   
            });
        }


    
    //#endregion
    $(function () {

        $('#btnCancel').bind('click',
                function () {
                    $('#w').window('close');
                });

        //#region 下拉菜单初始化
        //调度员
        //EUIcombobox("#txt_Dis", {
        //    valueField: "编码",
        //    textField: "姓名",
        //    OneOption: [{
        //        编码: "",
        //        姓名: "--请选择--"
        //    }],
        //    url: "/BasicInfo/LoadDis/"

        //});
        $("#txt_Dis").EUIcombobox({
            valueField: "编码",
            textField: "姓名",
            OneOption: [{
                编码: "",
                姓名: "--请选择--"
            }],
            url: "/BasicInfo/LoadDis/?ActionId=<%=Request.QueryString["ActionId"]%>"
        });
        //分站
        EUIcombobox("#txt_Station", {
            valueField: "编码",
            textField: "名称",
            OneOption: [{
                编码: "",
                名称: "--请选择--"
            }],
            url: "/BasicInfo/LoadAllStations/?ActionId=<%=Request.QueryString["ActionId"]%>"

        });
        //车辆
        EUIcombobox("#txt_Alums", {
            valueField: "车辆编码",
            textField: "实际标识",
            OneOption: [{
                车辆编码: "",
                实际标识: "--请选择--"
            }],
            url: "/Notice/AmbulanceLoad/?ActionId=<%=Request.QueryString["ActionId"]%>"

        });
        //事件类型
        EUIcombobox("#AlarmTypes", {
            valueField: "编码",
            textField: "名称",
            OneOption: [{
                编码: "",
                名称: "--请选择--"
            }],
            url: "/BasicInfo/LoadAlarmTypes/"

        });
        //事件来源
        EUIcombobox("#OriTypes", {
            valueField: "编码",
            textField: "名称",
            OneOption: [{
                编码: "",
                名称: "--请选择--"
            }],
            url: "/BasicInfo/LoadAlarmOriTypes/"

        });

        //病情判断
        EUIcombobox("#IllState", {
            valueField: "编码",
            textField: "名称",
            OneOption: [{
                编码: "",
                名称: "--请选择--"
            }],
            url: "/BasicInfo/LoadIllStates/"

        });
        
        $("#IsTest").combobox({
            valueField: "编码",
            textField: "名称",
            data:[{编码: "",名称: "--请选择--"},{编码: "1",名称: "是"},{编码: "0",名称: "否"}]
        });
        //#endregion

        //#region 初始化dategrid 
        $('#grid').datagrid({   
            url:null,
            onRowContextMenu: showmenu,
//            pagination:true,   
//            pageSize:15,   
//            pageNumber:1,   
//            rownumbers:true,   
//            pagelist:[20, 25, 30],
            onClickRow: function (rowIndex, rec) {
                var href = '/BasicInfo/AccLoad/?id=' + rec.id + "&ActionId="+<%=ViewData["ActionId"]%>;
                $("#detail").html('<iframe height="100%" width="100%"   marginheight="0" marginwidth="0" scrolling="auto" frameborder="0" src="' + href + '"></iframe>');
                $.mobile.go('#detail');
            }
        }); 
        $('#grid').datagrid('getPager').pagination({  
            onSelectPage:function(pPageIndex, pPageSize) {
                Search(pPageIndex,pPageSize);
            } 
        }); 
        //#endregion
        Search(1,20);

    });
    var opPRE = new Object();
    function showmenu(e, rowIndex, rowData){
        e.preventDefault();
        opPRE=rowData;
        $('#grid').datagrid('selectRow',rowIndex);
        $('#mm').menu('show', {
            left: e.pageX,
            top: e.pageY
        });

    }
    //点击查看右键事件
    function openMenuSee(rowData) { 
            this.parent.$('#tabs').tabs('add',{
			    title: "事件详细信息[" + rowData.id + "]",
			    content: createFrame('/BasicInfo/AccLoad/?id=' + rowData.id + "&ActionId="+<%=ViewData["ActionId"]%>),
                closable: true
		    });
    }
//点击查看右键调度复核
        function openMenuReview(rowData) { 
           this.parent.$('#tabs').tabs('add',{
			    title: "调度复核信息[" + rowData.id + "]",
                
			    content: createFrame('<%=ViewData["ReviewUrl"]%>?id=' + rowData.id + "&WorkId=<%=ViewData["personcode"]%>"),
                closable: true
        });
    }
    //点击分站回复右键事件
    function openMenuStationReply(rowData) {   
        var url = "/BasicInfo/StationMsg/?id=" + escape(rowData.id);
        var $window = $('<div ></div>').html('<iframe id="StationMsgFrame" style="border:0px;width:100%;height:310px;" src="' + url + '" ></iframe>');
        $window.window({
            title: '查看分站回复',
            width: 1000,
            modal: true,
            shadow: true,
            height: 350,
            resizable: false,
            onBeforeClose: function () {
                $window.find("iframe")[0].contentWindow.close();
            }
        });
        $window.window('open');
    }


        function query() {
            window.location.href = "/BasicInfo/AlarmEvent";
        }
        //点击查看按钮事件
        function openPwd() {            
            var row = $('#grid').datagrid('getSelected');
            if (!row || row.length == 0) {
                $.messager.alert('提示', '请选择要查看的数据');
                return;
            }
            this.parent.$('#tabs').tabs('add',{
			    title: "事件详细信息[" + row.id + "]",
			    content: createFrame('/BasicInfo/AccLoad/?id=' + row.id + "&ActionId="+<%=ViewData["ActionId"]%>),
                closable: true
		    });
//            if(!this.parent.$('#tabs').tabs('exists','受理')){
//            this.parent.$('#tabs').tabs('add',{
//			    title: "事件详细信息[" + row.id + "]",
//                content: createFrame('/BasicInfo/AccLoad/?code=' + row.id),
//                closable: true,
//		    });
//	        }else{
//	        }
//            alert(row.id);
//            $('#w').window({
//                title: '受理信息',
//                width: 500,
//                modal: true,
//                shadow: true,
//                height: 255,
//                resizable: false
//            });
//            $('#acc').datagrid({
//                iconCls: 'icon-save',
//                nowrap: false,
//                striped: true,
//                url: '/BasicInfo/AccLoad/' + row.id,
//                sortName: 'ID',
//                sortOrder: 'asc',
//                remoteSort: false,
//                fitColumns: true,  //自适应列宽
//                singleSelect: "true",
//                columns: [[
//                { field: 'ID', title: '事件编码', width: 80, align: 'center' },
//                { field: 'Order', title: '受理序号', width: 50, align: 'center' },
//                { field: 'Type', title: '受理类型', width: 50, align: 'center' },
//                { field: 'Person', title: '受理人员', width: 50, align: 'center' }

//                ]]
//                });
//                
//            $('#w').window('open');
        }
        function accinfo() {            
            var row = $('#acc').datagrid('getSelected');
            if (!row || row.length == 0) {
                $.messager.alert('提示', '请选择要查看的数据');
                return;
            }
            if(!this.parent.$('#tabs').tabs('exists','受理')){
		    this.parent.$('#tabs').tabs('add',{
			    title: "受理",
                ///BasicInfo/AccInfo/?code=
                content: createFrame('AccInfo/?code=' + row.ID + '&&order=' + row.Order),
                closable: true
		    });
	        }else{
		        this.parent.$('#tabs').tabs('select','受理');
		        this.parent.$('#mm-tabupdate').click();
	        }
        }
                  
        function createFrame(url)
        {
            var s = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
	        return s;
        }
    </script>
    <%--    <link href="../../Content/Css/default.css" rel="stylesheet" type="text/css" />--%>
    <%--    <style type="text/css">
        input[class="easyui-validatebox"] {height: 16px;}
    </style>--%>
</head>
<body>
    <div class="easyui-navpanel" style="padding: 8px; height: auto"> 
       <header>
            <div class="m-toolbar">
                <div class="title">事件管理</div>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>                
                <div class="m-right">
                    <a href="javascript:void(0)" class="easyui-linkbutton" onclick="$.mobile.go('#bpage')" data-options="iconCls:'icon-search',plain:true"></a>
                </div>
            </div>
        </header>
        <table id="grid"  align="center" pagination="true" pagenumber="1" pagelist="[10, 15, 20]" pagesize="10"  
            fitcolumns="true" nowrap="false" striped="true" rownumbers="true" singleselect="true"
            sortname="time" sortorder="desc"  width="1000px" fit="true">
            <%-- <thead frozen="true">
                <tr>
                    <th field="id" width="100px" checkbox="true" align='center'>
                        编码
                    </th>
                </tr>
            </thead>--%>
            <thead>
                <tr>
                    <th field="id" width="100px" hidden="true" align='center' sortable="true">
                        编码
                    </th>
                    <%--<th field="tel" width="80px" align='center' sortable="true">
                        呼救电话
                    </th>
                    <th field="ori" width="60px" align='center' sortable="true">
                        事件来源
                    </th>
                    <th field="accnum" width="60px" align='center' sortable="true">
                        受理次数
                    </th>--%>
                    <th field="alarmName" width="200px" align='center' sortable="true">
                        事件名称
                    </th>
                  <%--  <th field="type" width="60px" align='center' sortable="true">
                        事件类型
                    </th>--%>
                    <th field="time" width="130px" align='center' formatter='renderTime' sortable="true">
                        首次受理时刻
                    </th>
                   <%-- <th field="diaoduyuan" width="60px" align='center' sortable="true">
                        首次调度员
                    </th>--%>
                    <th field="c_time" width="130px" align='center' formatter='renderTime' sortable="true">
                        首次派车时刻
                    </th>
                   <%--  <th field="swdd" width="130px" align='center'  sortable="true">
                        送往地点
                    </th>
                    <th field="chuche" width="60px" align='center' sortable="true">
                        派车次数
                    </th>
                    <th field="正常完成" width="60px" align='center' sortable="true">
                        正常完成
                    </th>--%>
                </tr>
            </thead>
        </table>
    </div>
  

    <div id="bpage" class="easyui-navpanel"> 
       <header>
            <div class="m-toolbar">
                <span id="title" class="title">查询</span>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>
            </div>
        </header>
        <table border="0" cellspacing="0" cellpadding="0" width="100%" height="10%">
            <tr>
                <td>
                    受理起始时间:
                </td>
                <td>
                    <%--<input id="BeginDate" class="easyui-datebox" style="width: 100px" value="<%= ((dynamic)this.ViewData["BeginDate"]) %>" />--%>
                    <input id="BeginDate" class="easyui-datebox" style="width: 100px" value="2011-11-1" />
                    <input id="BeginTime" class="easyui-validatebox" style="border: 1px solid #8DB2E3; width: 60px; height: 15px" value="<%=ViewData["Time"]%>"/>
                </td>
            </tr>
            <tr>
                <td>
                    受理终止时间:
                </td>
                <td>
                    <input id="EndDate" class="easyui-datebox" style="width: 100px" value="<%= ((dynamic)this.ViewData["EndDate"]) %>" />
                    <input id="EndTime" class="easyui-validatebox" style="border: 1px solid #8DB2E3; width: 60px; height: 15px" value="<%=ViewData["Time"]%>" />
                </td>
            </tr>
            <tr>
                <td>
                    电话号码:
                </td>
                <td>
                    <input id="txt_TelCode" class="easyui-validatebox" style="border: 1px solid #8DB2E3;width: 144px; height: 15px;"/>
                </td>
            </tr>
            <tr>
                <td>
                    患者姓名:
                </td>
                <td>
                    <input id="txt_SuffererName" class="easyui-validatebox" style="border: 1px solid #8DB2E3;width: 144px; height: 15px;" />
                </td>
            </tr>
            <tr>
                <td>
                    派车起始时间:
                </td>
                <td>
                    <input id="c_BeginDate" class="easyui-datebox" style="width: 100px" value="" />
                    <input id="c_BeginTime" class="easyui-validatebox" style="border: 1px solid #8DB2E3;width: 60px; height: 15px" value=""/>
                </td>
            </tr>
            <tr>
                <td>
                    派车终止时间:
                </td>
                <td>
                    <input id="c_EndDate" class="easyui-datebox" style="width: 100px" value="" />
                    <input id="c_EndTime" class="easyui-validatebox" style="border: 1px solid #8DB2E3;width: 60px; height: 15px" value=""/>
                </td>
            </tr>
            <tr>
                <td>
                    事件编码:
                </td>
                <td>
                    <input id="AlarmEventCode" class="easyui-validatebox" style="border: 1px solid #8DB2E3;width: 144px; height: 15px;"/>
                </td>
            </tr>
            <tr>
                <td>
                    主诉:
                </td>
                <td>
                    <input id="txt_ZhuSu" class="easyui-validatebox" style="border: 1px solid #8DB2E3;width: 144px; height: 15px;"/>
                </td>
            </tr>
            <tr>
                <td>
                    司机:
                </td>
                <td>
                    <input id="Driver" class="easyui-validatebox" style="border: 1px solid #8DB2E3; width: 144px;height: 15px;" />
                </td>
            </tr>
            <tr>
                <td>
                    医生:
                </td>
                <td>
                    <input id="Doctor" class="easyui-validatebox" style="border: 1px solid #8DB2E3; width: 144px;height: 15px;" />
                </td>
            </tr>
            <tr>
                <td>
                    护士:
                </td>
                <td>
                    <input id="Nurse" class="easyui-validatebox" style="border: 1px solid #8DB2E3; width: 144px;height: 15px;" />
                </td>
            </tr>
            <tr>
                <td>
                    调度员:
                </td>
                <td>
                    <input class="easyui-combobox" style="width: 150px;" panelheight="400px" id="txt_Dis" editable="false" />
                </td>
            </tr>
            <tr>
                <td>
                    分站:
                </td>
                <td>
                    <input class="easyui-combobox" style="width: 150px;" panelheight="400px" id="txt_Station"  editable="false" />
                </td>
            </tr>
            <tr>
                <td>
                    车辆:
                </td>
                <td>
                    <input class="easyui-combobox" style="width: 150px;" panelheight="400px" id="txt_Alums"  editable="false" />
                </td>
            </tr>
            <tr>
                <td>
                    事件类型:
                </td>
                <td>
                    <input class="easyui-combobox" style="width: 150px;" panelheight="200px" id="AlarmTypes"  editable="false" />
                </td>
            </tr>
            <tr>
                <td>
                    事件来源:
                </td>
                <td>
                    <input class="easyui-combobox" style="width: 150px;" panelheight="120px" id="OriTypes"  editable="false" />
                </td>
            </tr>
            <tr>
                <td>
                    现场地址:
                </td>
                <td>
                    <input id="txt_LocalAddr" class="easyui-validatebox" style="border: 1px solid #8DB2E3;width: 144px; height: 15px;"/>
                </td>
            </tr>
            <tr>
                <td>
                    送往地点:
                </td>
                <td>
                    <input id="txt_SendAddress" class="easyui-validatebox" style="border: 1px solid #8DB2E3; width: 144px; height: 15px;"/>
                </td>
           </tr>
            <tr>
                <td>
                    病情判断:
                </td>
                <td>
                    <input class="easyui-combobox" style="width: 150px;" panelheight="200px" id="IllState"  editable="false" />
                </td>
            </tr>
            <tr>
                <td>
                    是否测试:
                </td>
                <td>
                    <input class="easyui-combobox" style="width: 150px;" panelheight="120px" id="IsTest" editable="false" />
                </td>
            </tr>
             <tr>
                 <td>
                    受理病情判断:
                </td>
                <td>
                    <input id="txt_Judge" class="easyui-validatebox" style="border: 1px solid #8DB2E3; width: 144px; height: 15px;"/>
                </td>
            </tr>
            <tr>
                  <td colspan="2">
                    <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-search" onclick="Search(1,20);

            $.mobile.go('#result');return false;">查询</a>
                </td>
            </tr>
        </table>
    </div>
 
 
    <div id="mm" class="easyui-menu" style="width:120px;" >
        <div onclick="javascript:openMenuSee(opPRE);">查看事件详情</div>
         <%if (this.ViewData["ReviewUrl"]!=""){ %>
        <div onclick="javascript:openMenuReview(opPRE);">调度复核信息</div>
         <% } %>
        <div onclick="javascript:openMenuStationReply(opPRE);">查看分站回复</div>
    </div>
    <%--    <div region="south" border="true" style="text-align: right; height: 30px; line-height: 30px;
        background-color: #f7f7f7;">
        <table style="width: 100%">
            <tr>
                <td style="text-align: LEFT">
                </td>
            </tr>
        </table>
    </div>--%>
    <div id="w" class="easyui-window" title="事件受理信息" minimizable="false" maximizable="false"
        icon="icon-save" style="width: 600px; height: 240px; padding: 5px; background: #fafafa;"
        closed="true">
        <div region="center" border="false" style="padding-left: 10px; background: #fff;
            border: 1px solid #ccc;">
            <table id="acc" align="center">
            </table>
        </div>
        <div region="south" border="false" style="text-align: center; height: 30px; line-height: 30px;">
            <a id="btnEp" class="easyui-linkbutton" icon="icon-tip" onclick="accinfo();return false;">查看</a>
            <a id="btnCancel" class="easyui-linkbutton" icon="icon-cancel" href="">取消</a>
        </div>
    </div>

    <div id="detail" class="easyui-navpanel">
    </div>
</body>
</html>

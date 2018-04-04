<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>车辆信息</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=1.0"></script>
    <script type="text/javascript" language="javascript">
            $(function () {
                //#region 下拉菜单初始化
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
                //车辆类型
                EUIcombobox("#txt_AmbType", {
                    valueField: "编码",
                    textField: "名称",
                    OneOption: [{
                        编码: "",
                        名称: "--请选择--"
                    }],
                    url: "/BasicInfo/LoadAllTypes/"

                });
                //分组类型
                EUIcombobox("#txt_AmbGroup", {
                    valueField: "编码",
                    textField: "名称",
                    OneOption: [{
                        编码: "",
                        名称: "--请选择--"
                    }],
                    url: "/BasicInfo/LoadAllGroups/"

                });
                //#endregion
                Search();
            });
            function freload() {
                $('#grid').datagrid('reload', {
                    RealCode: $('#txt_RealCode').val(),
                    AmbNum: $('#txt_AmbNum').val(),
                    Station: $('#txt_Station').combobox('getValue'),
                    AmbType: $('#txt_AmbType').combobox('getValue'),
                    AmbGroup: $('#txt_AmbGroup').combobox('getValue'),
                    ActionId: '<%=ViewData["ActionId"]%>'
                });
            }

            //#region 查询
            function Search() {
                $('#grid').datagrid('options').url = '/BasicInfo/AmbulanceListSearch/';

                $('#grid').datagrid('load', {
                    RealCode: $('#txt_RealCode').val(),
                    AmbNum: $('#txt_AmbNum').val(),
                    Station: $('#txt_Station').combobox('getValue'),
                    AmbType: $('#txt_AmbType').combobox('getValue'),
                    AmbGroup: $('#txt_AmbGroup').combobox('getValue'),
                    ActionId: '<%=ViewData["ActionId"]%>'
                });

                $('#grid').datagrid({
                    onClickRow: function (rowIndex, rec) {

                        //$("#detail").html('<iframe height="100%" width="100%"   marginheight="0" marginwidth="0" scrolling="auto" frameborder="0" src="/BasicInfo/AmbulanceMap/"></iframe>');
                        //$.mobile.go('#detail');
                        if(rec.AlarmEventCode != null){
                            var href = "/BasicInfo/AccLoad/?id="+rec.AlarmEventCode+"&ActionId="+<%=ViewData["ActionId"]%>;
                            $("#detail").html('<iframe height="100%" width="100%"   marginheight="0" marginwidth="0" scrolling="auto" frameborder="0" src="' + href + '"></iframe>');
                            $.mobile.go('#detail');
                        }
                    }
                });
            }
            //#endregion       
            //#region 格式转换
            function formatState(val, rowData, rowIndex) {
                var unsignedInt16 = (rowData.Color >>> 0).toString(16);
                var color = "#" + unsignedInt16.substring(unsignedInt16.length - 6);
                return "<span style=\"color:" + color + ";\">" + val + "</span>";
            }
            function renderView(value, rowData, rowIndex) {
                var str = "<img alt=\"查看\" src=\"../../Content/images/vie.gif\" style=\"cursor: pointer;\" onclick=\"javascript:View('" + rowData.ID + "')\" />";
                return str;
            }
            function renderAmbulanceLevelEdit(value, rowData, rowIndex) {
                var str = "<img alt=\"等级修改\" src=\"../../Content/images/chart_line_edit.png\" style=\"width: 16px; height: 16px; cursor: pointer;\" onclick=\"javascript:AmbulanceLevelEdit('" + rowData.ID + "')\" />";
                return str;
            }
            function renderAmbulancePersonSign(value, rowData, rowIndex) {
                var str = "<img alt=\"人员上下班\" src=\"../../Content/images/DriverChange.gif\" style=\"width: 16px; height: 16px; cursor: pointer;\" onclick=\"javascript:AmbulancePersonSign('" + rowData.ID + "')\" />";
                return str;
            }
            function renderAmbulanceStateChange(value, rowData, rowIndex) {
                var str = "<img alt=\"改车辆状态\" src=\"../../Content/images/myalimama.png\" style=\"width: 20px; height: 20px; cursor: pointer;\" onclick=\"javascript:AmbulanceStateChange('" + rowData.ID + "')\" />";
                return str;
            }

            //#endregion   
            var $ViewWindow;
            function View(CarCode) {
                var url = "../AmbulanceInfo/?id=" + escape(CarCode);
                $ViewWindow = $('<div ></div>').html('<iframe id="ViewFrame" style="border:0px;width:100%;height:420px;" src="' + url + '" ></iframe>');

                $ViewWindow.window({
                    title: '车辆详细信息',
                    width: 500,
                    modal: true,
                    shadow: true,
                    height: 460,
                    resizable: false,
                    onBeforeClose: function () {
                        $ViewWindow.find("iframe")[0].contentWindow.close();
                    }

                });

                $ViewWindow.window('open');
            }
            var $AmbulanceLevelEditWindow;
            function AmbulanceLevelEdit(CarCode) {
                var url = "../AmbulanceLevelEdit/?id=" + escape(CarCode) + '&ActionId=' + '<%=ViewData["ActionId"]%>';
                $AmbulanceLevelEditWindow = $('<div ></div>').html('<iframe id="AmbulanceLevelEditFrame" style="border:0px;width:100%;height:320px;" src="' + url + '" ></iframe>');

                $AmbulanceLevelEditWindow.window({
                    title: '车辆等级修改',
                    width: 400,
                    modal: true,
                    shadow: true,
                    height: 360,
                    resizable: false,
                    onBeforeClose: function () {
                        $AmbulanceLevelEditWindow.find("iframe")[0].contentWindow.close();
                    }

                });

                $AmbulanceLevelEditWindow.window('open');
            }
            var $AmbulancePersonSignWindow;
            function AmbulancePersonSign(CarCode) {
                var url = "../AmbulancePersonSign/?id=" + escape(CarCode) + '&ActionId=' + '<%=ViewData["ActionId"]%>';
                $AmbulancePersonSignWindow = $('<div ></div>').html('<iframe id="AmbulancePersonSignFrame" style="border:0px;width:100%;height:420px;" src="' + url + '" ></iframe>');

                $AmbulancePersonSignWindow.window({
                    title: '人员上下班',
                    width: 650,
                    modal: true,
                    shadow: true,
                    height: 460,
                    resizable: false,
                    onBeforeClose: function () {
                        $AmbulancePersonSignWindow.find("iframe")[0].contentWindow.close();
                    }

                });
                $AmbulancePersonSignWindow.window('open');
            }
            var AmbulanceStateChangeWindow;
            function AmbulanceStateChange(CarCode) {
                var url = "../AmbulanceStateChange/?id=" + escape(CarCode) + '&ActionId=' + '<%=ViewData["ActionId"]%>';
                $AmbulanceStateChangeWindow = $('<div ></div>').html('<iframe id="AmbulanceStateChangeFrame" style="border:0px;width:100%;height:420px;" src="' + url + '" ></iframe>');

                $AmbulanceStateChangeWindow.window({
                    title: '改车辆状态',
                    width: 650,
                    modal: true,
                    shadow: true,
                    height: 460,
                    resizable: false,
                    onBeforeClose: function () {
                        $AmbulanceStateChangeWindow.find("iframe")[0].contentWindow.close();
                    }

                });
                $AmbulanceStateChangeWindow.window('open');
            }
        </script>     
</head>
<body> 
    <div class="easyui-navpanel" style="padding: 5px" border="true" >         
        <header>
            <div class="m-toolbar">
                <div class="m-left">
                    <a href="#" class="easyui-linkbutton m-back"  data-options="plain:true,outline:true,back:true">Back</a>
                </div>
                <span class="m-title">车辆管理</span>
                <div class="m-right">
                    <a href="javascript:void(0)" class="easyui-linkbutton" onclick="$.mobile.go('#bpage')" data-options="iconCls:'icon-search',plain:true"></a>
                </div>
            </div>
        </header> 
        <table id="grid" class="easyui-datagrid" align="center"  toolbar="#tb" 
         pagination="true"  pageNumber=1 pageList= "[10, 15, 20]" pageSize=15 idField="ID" fitColumns="true" 
         nowrap="false" striped="true" rownumbers="true" singleSelect="true"
         sortName="ID" sortOrder= "asc" remoteSort="false" width="1000px" fit="true" 
         >
        <thead>
			<tr>
                <th field="ID" width="10%" align='center'>编码</th>
                <th field="Station" width="20%" align='center'>分站</th>
                <th field="CardNum" width="15%" align='center'>车牌号码</th>
<%--                <th field="RealSign" width="10%" align='center'>实际标识</th>--%>
                <th field="State" width="15%" align='center' formatter="formatState">工作状态</th>
                <th field="Type" width="15%" align='center'>车辆类型</th>
<%--                <th field="Level" width="10%" align='center'>车辆等级</th>--%>
                <th field="Tel" width="20%" align='center'>随车电话</th>
<%--                <th field="aa" width="10%" align='center' formatter='renderView'>查看</th>--%>
<%--                <th field="ab" width="5%" align='center' formatter='renderAmbulanceLevelEdit'>等级</th>
                <th field="ac" width="5%" align='center' formatter='renderAmbulancePersonSign'>上下班</th>
                <th field="ad1" width="5%" align='center' formatter='renderAmbulanceStateChange'>改状态</th>--%>
			</tr>
		</thead>
        </table>
      </div>       
    <div id="bpage" class="easyui-navpanel"  closed="true">
        <header>
            <div class="m-toolbar">
                <span id="title" class="m-title">查询</span>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>  
            </div>
        </header> 
         <table border="0" cellspacing="0" cellpadding="0" width="100%" height="10%">
                <tr>
                    <td>实际标识: </td>
                    <td><input id="txt_RealCode" class="easyui-validatebox" style="border: 1px solid #8DB2E3;
                    width: 81px; height: 18px"> </td>
                </tr>
                <tr> 
                    <td>车牌号码: </td>
                    <td><input id="txt_AmbNum" class="easyui-validatebox" style="border: 1px solid #8DB2E3;
                    width: 100px; height: 18px"></td>
                </tr>
                <tr> 
                    <td>分站:</td>
                    <td><input class="easyui-combobox" style="width:150px;" id="txt_Station" editable="false" /></td>
                </tr> 
                <tr> 
                    <td>车辆类型:   </td>
                    <td><input class="easyui-combobox" style="width:150px;" id="txt_AmbType" editable="false" /></td>
                </tr>
                <tr> 
                    <td>分组类型:   </td>
                    <td><input class="easyui-combobox" style="width:150px;" id="txt_AmbGroup" editable="false" /></td>
                </tr>
                <tr>  
                    <td colspan="2">
                         <a href="#" class="easyui-linkbutton" iconCls="icon-search" onclick="Search();">查询</a> 
                    </td>
                </tr>   
           </table>
    </div>

    <div class="easyui-navpanel" id="detail"  style="padding: 5px" border="true" > 
      <%-- <div style="width:520px;height:340px;border:1px solid gray" id="container"></div>--%>
    </div>       
</body>
</html>
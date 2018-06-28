<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>"  %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title><%= ViewData["name"]%>_条件选择</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script src="/Content/Script/MourseEvent.js" type="text/javascript"></script>
    <link href="/Content/Css/Dafault.css" rel="stylesheet" type="text/css" />
    <script src="/Content/Script/jquery.timepicker.min.js" type="text/javascript"></script>
    <link href="/Content/Script/jquery.timepicker.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript" language="javascript">
        function getNoEscapeCha(src)
        {
            switch(src)
            {
                case 'likeString':
                    return '=';
                    break;
                case '&lt;':
                    return '<';
                    break;
                case '&lt;=':
                    return '<=';
                    break;
                default:
                    return src;
            }
        }
        var opPRE = new Object();
        function newOptionTDonclick(src)//
        {
            if (alreadyOpenTableId != null)
                alreadyOpenTableId.find(":first>:first-child").attr("color", "black");
            alreadyOpenTableId = $(src);
//            alreadyOpenTableId.find(":first>:first-child").attr("color", "red");
            $(":first>:first-child", alreadyOpenTableId).attr("color", "red"); 
        }

        function mClick(rowIndex, rowData) {
//            alert(rowData.typename);
            $("#middleTd").html(function (index, html) {
                var changehtml = "<br/>当前列：<label id='columnname'>" + rowData.name + "</label>\n<br/><br/>选择关系：<br/><input id=\"Radio_1\" type=\"radio\" value=\"and\" name=\"radio1\" style=\"border-bottom-style: none\" checked/><label for=\"Radio_1\">并且关系(and)</label><br/>\n<input id=\"Radio_2\" type=\"radio\" value=\"or\" name=\"radio1\" style=\"border-bottom-style: none\"/><label for=\"Radio_2\">或者关系(or)</label><br/><br/>可选条件：<br/>";
                switch (rowData.typename) {
                    case "int":
                    case "float":
                    case "decimal":
                    case "numeric":
                    case "money":
                        changehtml = changehtml
                    + "<input id=\"Radio1\" type=\"radio\" value=\">\" name=\"radio\" style=\"border-bottom-style: none\" checked/><label for=\"Radio1\">大于></label><br/>\n"
		            + "<input id=\"Radio2\" type=\"radio\" value=\"<\" name=\"radio\" style=\"border-bottom-style: none\" /><label for=\"Radio2\">小于<</label><br/>\n"
		            + "<input id=\"Radio3\" type=\"radio\" value=\"=\" name=\"radio\" style=\"border-bottom-style: none\" /><label for=\"Radio3\">等于=</label><br/>\n"
		            + "<input id=\"Radio5\" type=\"radio\" value=\">=\" name=\"radio\" style=\"border-bottom-style: none\" /><label for=\"Radio5\">大于、等于>=</label><br/>\n"
		            + "<input id=\"Radio6\" type=\"radio\" value=\"<=\" name=\"radio\" style=\"border-bottom-style: none\" /><label for=\"Radio6\">小于、等于<=</label><br/>\n"
                    + "<input id=\"Radio4\" type=\"radio\" value=\"like\" name=\"radio\" style=\"border-bottom-style: none\"/><label for=\"Radio4\">模糊</label><br/>\n"
		            + "<span id=\"casetext\"><input type=\"text\" /></span><input id=\"Button1\" type=\"button\" class=\"button\" value=\"确 定\" onclick=\"writeNote(alreadyOpenTableId)\"/><br/>"
                    ;// +"<br/>条件参数<br/>说明：模糊里\"%\"代表任意长度任意字符；\"_\"代表一个任意字符<br/>";
                        break;
                    case "time":
                    case "datetime":
                        changehtml = changehtml
                    + "<input id=\"Radio1\" type=\"radio\" value=\">\" name=\"radio\" style=\"border-bottom-style: none\" checked/><label for=\"Radio1\">大于></label><br/>\n"
		            + "<input id=\"Radio2\" type=\"radio\" value=\"<\" name=\"radio\" style=\"border-bottom-style: none\" /><label for=\"Radio2\">小于<</label><br/>\n"
		            + "<input id=\"Radio3\" type=\"radio\" value=\"=\" name=\"radio\" style=\"border-bottom-style: none\" /><label for=\"Radio3\">等于=</label><br/>\n"
		            + "<input id=\"Radio5\" type=\"radio\" value=\">=\" name=\"radio\" style=\"border-bottom-style: none\" /><label for=\"Radio5\">大于、等于>=</label><br/>\n"
		            + "<input id=\"Radio6\" type=\"radio\" value=\"<=\" name=\"radio\" style=\"border-bottom-style: none\" /><label for=\"Radio6\">小于、等于<=</label><br/>\n"
                    + "<input id=\"Radio4\" type=\"radio\" value=\"like\" name=\"radio\" style=\"border-bottom-style: none\"/><label for=\"Radio4\">模糊</label><br/>\n"
		            + "<span id=\"casetext\"><input type=\"text\" id=\"casetextDatebox\" style=\"width:90px\" /><input style=\"width:50px\" name=\"timepicker\" /></span><input id=\"Button1\" type=\"button\" class=\"button\" value=\"确 定\" onclick=\"writeNote(alreadyOpenTableId)\"/><br/>"
                    ; //+ "<br/>条件参数<br/>说明：模糊里\"%\"代表任意长度任意字符；\"_\"代表一个任意字符<br/>";
                        break;
                    default:
                        changehtml = changehtml
                    + "<input id=\"Radio1\" type=\"radio\" value=\">\" name=\"radio\" style=\"border-bottom-style: none\" checked/><label for=\"Radio1\">大于></label><br/>\n"
		            + "<input id=\"Radio2\" type=\"radio\" value=\"<\" name=\"radio\" style=\"border-bottom-style: none\" /><label for=\"Radio2\">小于<</label><br/>\n"
		            + "<input id=\"Radio3\" type=\"radio\" value=\"=\" name=\"radio\" style=\"border-bottom-style: none\" /><label for=\"Radio3\">等于=</label><br/>\n"
		            + "<input id=\"Radio5\" type=\"radio\" value=\">=\" name=\"radio\" style=\"border-bottom-style: none\" /><label for=\"Radio5\">大于、等于>=</label><br/>\n"
		            + "<input id=\"Radio6\" type=\"radio\" value=\"<=\" name=\"radio\" style=\"border-bottom-style: none\" /><label for=\"Radio6\">小于、等于<=</label><br/>\n"
                    + "<input id=\"Radio4\" type=\"radio\" value=\"like\" name=\"radio\" style=\"border-bottom-style: none\"/><label for=\"Radio4\">模糊</label><br/>\n"
		            + "<span id=\"casetext\"><input type=\"text\" /></span><input id=\"Button1\" type=\"button\" class=\"button\" value=\"确 定\" onclick=\"writeNote(alreadyOpenTableId)\"/><br/>"
                    ; //+ "<br/>条件参数<br/>说明：模糊里\"%\"代表任意长度任意字符；\"_\"代表一个任意字符<br/>";
                        break;
                }

                return changehtml;
            });
            //            debugger;
            $("#middleTd #casetextDatebox").datebox();
            $("#middleTd #casetext input[name='timepicker']").timepicker({
                'timeFormat': 'H:i:s'
            });
        }

        var alreadyOpenTableId = null;  //代表当前选中的节点
        function writeNote(src)//
        {
//            debugger;
            var i = parseInt(src.children(":first").attr("layer"))//i代表层数

            var $newOptionTR1 = $("<tr style='height:21px'></tr>");
            var $newOptionTD1 = $("<td></td>");
            var $thisTable = $("<table class='Grid_General' style='border:1' cellPadding='0' cellspacing='1'></table>");


            $newOptionTD1.append($thisTable);
            $newOptionTR1.append($newOptionTD1);

            $thisTable.attr({ 
            radio1: $("input[name='radio1']:checked").val()
            , columnname:$("#columnname").text()
            , radio2: $("input[name='radio']:checked").val()
            , casetext: GetSpanValue($("#casetext"))
            });

        var $newOptionTBODY = $("<tbody></tbody>");
        var $newOptionTR = $("<tr style='height:21px'></tr>");
        var $newOptionTD = $("<td noWrap='true'></td>");
        var $newOptionFONT = $("<font></font>");
        var spaces = "";
            for (var j = 1; j < i; j++) {
                spaces += "　";
            }
//            alert($("input[name='radio']:checked").val());
            $newOptionFONT.text(spaces + $("input[name='radio1']:checked").val() + " " + $("#columnname").text()
    + " " + getNoEscapeCha($("input[name='radio']:checked").val()) + " " + GetSpanValue($("#casetext")));


            $newOptionTD.append($newOptionFONT);
            $newOptionTD.attr("layer", i + 1);

            $newOptionTR.append($newOptionTD);
            $newOptionTR.bind({
                click: function () { newOptionTDonclick(this); },
                mouseover: function () { mOver(this); },
                mouseout: function () { mOut(this, 0); }
                ,
                contextmenu: function (e) {
                        e.preventDefault();
                        opPRE = this;
                        $('#mm').menu('show', {
                            left: e.pageX,
                            top: e.pageY
                        });
                    }
            });
            $newOptionTBODY.append($newOptionTR);
            $thisTable.append($newOptionTBODY);
//            alert($newOptionTR1.html())
            src.parent().append($newOptionTR1);
            resetTree_RelationsStyle()
        }

        function resetTree_RelationsStyle() //重新生成树样式  
        {
            var $firstRow = $("#tree_1").next();
            if ($firstRow.length > 0) {
                var $table1 = $firstRow.find(":first>:first-child");
                $table1.attr("radio1", "");
                $table1.find(":first font:first").text($table1.attr("columnname") + " " + $table1.attr("radio2") + " " + $table1.attr("casetext"));
            }
        }
        function delrow(src, headlen) {
            var tr = src.parentNode.parentNode.parentNode.parentNode;
            var tbody = tr.parentNode;
            tbody.removeChild(tr);
            tbody.parentNode.style.height = "auto";
            resetTree_RelationsStyle();
        }
        function GetSpanValue(src) {
            var $v = $(src).find("input:text:visible");
            var result = new Array();
            for (var i = 0; i < $v.length; i++) {
                if ($.trim($v[i].value)!="")
                    result.push($v[i].value)
            }
            return result.join(" ");
        }
        function SetCondition(headlen) {
            var $functionCases = $("#functionCase span[name='MustFillCase']");
            var $functionCasesName = $("#functionCase input[name='RepName']");
            var $functionCasesTypeName = $("#functionCase input[name='RepTypeName']");
            var Arrayfunction = new Array();
            var i;
            for (i = 0; i < $functionCases.length; i++) {
                if ($.trim(GetSpanValue($functionCases[i])) == "")//
                {
                    alert("表函数导出条件:" + $functionCasesName[i].value + " 不能为空");
                    return false;
                }

                Arrayfunction.push("<case Name='" + $functionCasesName[i].value + "' TypeName='" + $functionCasesTypeName[i].value + "' value='" + GetSpanValue($functionCases[i]) + "'></case>");
            }
            $("#functionCaseXml").val(Arrayfunction.join(""));
//            alert($("#functionCaseXml").val());
            //------------------------------------------
//            var rows = $('#GridView1').datagrid('getChecked');
//            if (!rows || rows.length == 0) {
//                $.messager.alert('提示', '请选择要导出的数据列');
//                return false;
//            }

            Arrayfunction = new Array();
//            $.each(rows, function (i, n) {
//                Arrayfunction.push("<case Name='" + n.name + "' TypeName='" + n.typename + "'></case>");
//            });
            var allrows = $('#GridView1').datagrid('getRows');
            var checkTrs = $('#Panel1 div.datagrid-view1 table.datagrid-btable tr')
            for (var i = 0; i < allrows.length; i++) {
                if ($(checkTrs[i]).find("input[name='ck']:checked").length > 0) {
                    var n = allrows[i];
                    Arrayfunction.push("<case Name='" + n.name + "' TypeName='" + n.typename + "'></case>");
                }
            }
            if (Arrayfunction.length == 0) {
                $.messager.alert('提示', '请选择要导出的数据列');
                return false;
            }
            $("#selectColumnsXml").val(Arrayfunction.join(""));
//            alert($("#selectColumnsXml").val());
            //-----------------------------------------------
            $("#inputCaseXml").val(getTableHTML($("#tree_Relations")).join(""));
//            alert($("#inputCaseXml").val());
//            return false;
            return true;
        }


        function createFrame(url) {
            var s = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
//                        alert(s)
            return s;
        }

        function Search() {
            if (SetCondition(1)) {

//                var tabName = $("#tableName").val() + tabNameIndex;
//                tabNameIndex++;

//                this.parent.$('#tabs').tabs('add', {
//                    title: $("#tableName").val() + "_查询结果",
//                    content: '<iframe scrolling="auto" frameborder="0"  src="' 
//                        + "/BasicInfo/Export_Show/" 
//                        + '" style="width:100%;height:100%;" target="'
//                        + tabName
//                        +'"></iframe>',
//                    closable: true
//                });
                $("#form").attr("target", "_blank");
                $("#form").attr("action", "/BasicInfo/Export_Show/");
                $("#form").submit();
            }
        }

        function ExportExcel() {
            if (SetCondition(1)) {
                $("#form").attr("target", "_self");
                $("#form").attr("action", "/BasicInfo/Export_Excel/");
                $("#form").submit();
            }
        }
        function htmlencode(s) {
            var div = document.createElement('div');
            div.appendChild(document.createTextNode(s));
            return div.innerHTML;
        }
        function htmldecode(s) {
            var div = document.createElement('div');
            div.innerHTML = s;
            return div.innerText || div.textContent;
        }

        function getTableHTML(src) {
//            debugger;
            var ArrayCase = new Array();

            //var $tr = $(":first-child>tr", src); //本来是想取table下tbody下的所有tr，可惜不对
            var $tr = $(">tr", src.children().first())
            for (var i = 0; i < $tr.length; i++) {
//                debugger;
                var $newtable = $(">table", $($tr[i]).children().first());

                if ($newtable.length > 0) {

                    ArrayCase.push("<case radio1='" + $newtable.attr("radio1") + "' columnName='" + $newtable.attr("columnName") + "' radio2='" + htmlencode($newtable.attr("radio2")) + "' caseText='" + $newtable.attr("caseText") + "'>");
                    ArrayCase.push(getTableHTML($newtable).join(""));
                    ArrayCase.push("</case>");
                }
            }
            return ArrayCase;
        }

        $(function () {
            alreadyOpenTableId = $("#tree_1");
            resetTree_RelationsStyle();
            $("input[name='timepicker']").timepicker({
                'timeFormat': 'H:i:s'
            });
            $("#tree_Relations tr[name='viewCreat']").bind({
                contextmenu: function (e) {
                    e.preventDefault();
                    opPRE = this;
                    $('#mm').menu('show', {
                        left: e.pageX,
                        top: e.pageY
                    });
                }
            });
            
        });
    </script>
</head>
<body>
<table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
	<tr>
		<td align="center" style="height: 20px;">
		<br />
		<span style="font-weight: bold; font-size: 18px" id="TiMu"><%= ViewData["name"]%></span></td>
	</tr>
	<tr style="table-layout:fixed">
		<td align="center" height="20" style="font-style:"><span id="ShuoMing"><%= ViewData["value"]%></span></td>
	</tr>
    <tr id="Panel1">
    <td>
		    <table width="100%">
                        <tr>
                        <td id="functionCase">
                            <% System.Data.DataTable dtRepeater1 = ViewData["Repeater1"] as System.Data.DataTable; 
                    foreach (System.Data.DataRow dr in dtRepeater1.Rows)
                    {%>

                    <div style="display:inline-block;display:inline;zoom:1;">
                    <input type="hidden" name="RepName" style="width:70px" value='<%=dr["name"]%>' />
                    <input type="hidden" name="RepTypeName" style="width:70px" value='<%=dr["typename"]%>' />
                    <font color="Red"><%=dr["name"]%></font>
                    
                        <%  
                        switch(dr["typename"].ToString())
                        { 
                            case  "datetime":
                                string defV=dr["deafultV"].ToString();
                                string date,time;
                                if(defV=="")
                                {
                                    date = "";
                                    time = "";
                                }
                                else
                                {
                                    DateTime defVDate = Convert.ToDateTime(defV);
                                    date = defVDate.ToString("yyyy-MM-dd");
                                    time = defVDate.ToString("HH:mm:ss");
                                }
                        %>
                        <span name="MustFillCase">
                            <input class="easyui-datebox" required="required" style="width: 90px" value='<%=date%>'/><input style="width:50px" name="timepicker" value='<%=time%>' />
                        </span>
                            
                        <%  
                                break;
                            default:
                            %>
                        <span name="MustFillCase">
                            <input style="width:70px" value='<%=dr["deafultV"]%>' /> 
                        </span>
                        <% 
                                break;
                        }%>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </div>
                    
                    <% }%>
                    
                    </td>
                        <td align="right" valign="bottom">
                        <%--<input id="chaXun" type="button" value="查 询" class="button" onclick="Search()"/>--%>
                        &nbsp;&nbsp;
                        <input id="Export" type="button" value="导出到Excel" class="button" style="width:80px;" onclick="javascript:ExportExcel();"/>	
                        </td>
                        </tr>
            </table>
            <table width="100%">
                <tr>
	                <td width="200px" valign="top">
                        <table class="easyui-datagrid" id="GridView1"
                        data-options="idField:'ID',nowrap:false,striped:true,rownumbers:true,selectOnCheck:false,checkOnSelect:false,singleSelect:true,onClickRow:mClick"
                        style="width:200px;height:350px">
                            <thead frozen="true">
                                <tr>
                                    <th field="ck" checkbox="true" align='center'>
                                    </th>
                                </tr>
                            </thead>
                            <thead>
                                <tr>
                                    <th data-options="field:'name'">选择需要导出的列名</th>
                                    <th data-options="field:'typename',hidden:true">typename</th>
                                </tr>
                            </thead>
                            <tbody>
                            <% System.Data.DataTable GridView1 = ViewData["GridView1"] as System.Data.DataTable;
                               foreach (System.Data.DataRow dr in GridView1.Rows)
                                {           %>
                                <tr>
                                    <td></td><td><%=dr["name"]%></td><td><%=dr["typename"]%></td>
                                </tr>
                                <% }%>
                            </tbody>
                        </table>
                    </td>
                    <td valign="top" id="middleTd" style="BACKGROUND-COLOR: #99cccc">
                    </td>
                    
                    <td width="200px" valign="top">
                    
                    <table id="tree_Relations" class="Grid_General" cellspacing="0" rules="all" border="1" style="border-color:LightGrey;border-collapse:collapse;border:0;">
                    <tr id="tree_1" class="Grid_Header" onclick="newOptionTDonclick(this)" style="font-weight:normal;white-space:nowrap;height:21px">
                        <td scope="col" layer="1" ><font color="red">统计列条件</font></td>
                        <% 
                           
                            %>
                    </tr>
                    <% Action<List<Anchor.FA.Model.C_ExportTree>,int> renderETree = null; // 先设为null %>
                    <%
                        //委托定义开始
                        renderETree = (treeLs, l) =>
                        {

                            string spaces = "";
                            for (var j = 1; j < l; j++)
                            {
                                spaces += "　";
                            }
                            foreach (Anchor.FA.Model.C_ExportTree t in treeLs)
                            {
                                %>
                    <tr style='height:21px'>
                        <td>
                            <table class='Grid_General' style='border:1' cellPadding='0' cellspacing='1' radio1='<%=t.radio1 %>' columnname='<%=t.columnname %>' radio2='<%=t.radio2 %>' casetext='<%=t.casetext %>'>
                                <tr name="viewCreat" style='height:21px' onclick="newOptionTDonclick(this);" onmouseover="mOver(this);" onmouseout="mOut(this, 0);" ><td noWrap='true' layer='<%= l+1 %>'><font><%= Html.Encode(string.Format("{0}{1} {2} {3} {4}", spaces, t.radio1, t.columnname, t.radio2, t.casetext))%></font></td></tr>
                                <% 
                                if (t.children != null)
                                {
                                    renderETree(t.children, l + 1);
                                }
                                %>
                            </table>
                         </td>
                    </tr>
                            <%
                            }

                        };//委托定义结束%>
                    
                    <% 
                        int i = 1;
                        List<Anchor.FA.Model.C_ExportTree> tls = ViewData["Case"] as List<Anchor.FA.Model.C_ExportTree>;//受理
                        if (tls != null)
                        {
                            renderETree(tls, i); // 最后再调用，即生成HTML 
                        }
                    %>

                    </table>
                    </td>
                </tr>


            </table>
    </td>
    </tr>
</table>
<form id="form" runat="server" method="post" action="/BasicInfo/Export_Show/" enctype="application/x-www-form-urlencoded">
	<input id="selectColumnsXml" type="hidden" name="selectColumnsXml" />
	<input id="tableName" type="hidden" name="tableName" value="<%= ViewData["name"]%>" />
	<input id="tableNotes" type="hidden"  name="tableNotes" value="<%= ViewData["value"]%>" />
	<input id="functionCaseXml" type="hidden" name="functionCaseXml" />
	<input id="inputCaseXml" type="hidden" name="inputCaseXml" />
</form>
    <div id="mm" class="easyui-menu" style="width:120px;">
        <div onclick="javascript:delrow(opPRE,1);">删除查询条件</div>
    </div>
</body>
</html>

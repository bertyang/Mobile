<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title><%--Export_Show--%><%: ViewData["tableName"]%>_查询结果</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <link href="/Content/Css/Dafault.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .FixedTitleRow {FONT-WEIGHT:normal;border:1px;Z-INDEX: 5;
                     _position: relative;_TOP: expression($("#divShow").scrollTop());}
                    /* POSITION:fixed; TOP: 0px;POSITION:fixed;
    .FixedTitleColumn{position: relative;left: expression(this.parentElement.offsetParent.scrollLeft);}
    .FixedDataColumn{position: relative;left: expression(this.parentElement.offsetParent.parentElement.scrollLeft);}
	.TD_Left { Z-INDEX: 300; ; LEFT: expression(document.getElementById("KCMX").scrollLeft); POSITION: relative }*/
    </style>
    <script type="text/javascript" language="javascript">
        /*****************************************************************/
        //鼠标移开每行时触发事件
        function mOut(src, type) {

            if ($(src).attr("cRow") != "1") {
                if (type == 0) {
                    src.style.backgroundColor = '#ECF3FA';
                }
                else {
                    src.style.backgroundColor = '#AFD3F5';
                }
            }

//            src.style.cursor = 'arrow';
        }
        /******************************************************************/
        function _mOver(src)	//鼠标样式：正常
        {
            if ($(src).attr("cRow") != "1") {
                src.style.backgroundColor = 'NavajoWhite';
            }
//            src.style.cursor = 'arrow';
        }
        function _mClick(src, type)	//鼠标样式：正常
        {
//            src.style.cursor = 'arrow';
            if ($(src).attr("cRow") != "1") {
                src.style.backgroundColor = '#FBEC88';
                $(src).attr("cRow", "1");
            }
            else {
                $(src).attr("cRow", "");
                if (type == 0) {
                    src.style.backgroundColor = '#ECF3FA';
                }
                else {
                    src.style.backgroundColor = '#AFD3F5';
                }
            }

        }


        $(function () {
//            $("#divHead").width($("#divShow").width());
            //            var tds = $("#tableShow tr:first").children();
            //            for (var i = 0; i < tds.length; i++) {
            //                alert($(tds[i]).width());
            //            }
            $("#tableHead").width($("#tableShow").width());
            $("#tableHead").html("<tr class='Grid_Header'>" + $("#tableShow tr:first").html() + "</tr>");

            var tdsFrom = $("#tableShow tr:first").children();
            var tdsTo1 = $("#tableHead tr:first").children();
            var tdsTo2 = $("#tableShow tr:eq(1)").children();
            $("#tableShow").width($("#tableHead").width());
            for (var i = 0; i < tdsFrom.length; i++) {
                $(tdsTo1[i]).width($(tdsFrom[i]).width());
                $(tdsTo2[i]).width($(tdsFrom[i]).width());
            }
            $("#tableShow tr:first").hide();
            $("#tableShow").attr("style", "border-collapse:collapse;table-layout:fixed")


            $("#divShow").scroll(function () {
                var b1 = $("#divHead");
                b1.scrollLeft($(this).scrollLeft());

            });
        });
    </script>
</head>
<body>
    <table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
    	<tr>
		<td align="center" style="height: 20px;">
		<br />
		<span style="font-weight: bold; font-size: 18px" id="TiMu"><%: ViewData["tableName"]%></span></td>
	</tr>
	<tr style="table-layout:fixed">
		<td align="center" height="20" style="font-style:"><span id="ShuoMing"><%: ViewData["tableNotes"]%></span></td>
	</tr>
    </table>
    <%
        System.Data.DataTable dt = ViewData["GridView1"] as System.Data.DataTable;//
        List<int> ColumnsNumber = ViewData["ColumnsNumber"] as List<int>;//
        List<string> ColumnsName = ViewData["ColumnsName"] as List<string>;//
         %>
         
         <div id="divHead" style="overflow: hidden;width:95%;">
            <div style="width: 10000px;float: left;">
             <table id="tableHead" class="Grid_General" cellspacing="0" rules="all" border="1" style="border-collapse:collapse;table-layout:fixed">
             </table>
            </div>
         </div>
         <div id="divShow" style="overflow-x: scroll;overflow-y: scroll; height: 500px;width:95%;">

         <table id="tableShow" class="Grid_General" cellspacing="0" rules="all" border="1" style="border-collapse:collapse;">

         <tr class="Grid_Header">
         <% foreach (System.Data.DataColumn dcol in dt.Columns)
            {
            %>
            <th scope="col" style="white-space:nowrap;"><%: dcol.ColumnName %></th>
            <%
            } 
            %>
         </tr>
        <%
            bool IsDouble = true;
        foreach (System.Data.DataRow dr in dt.Rows)
        {
            if (IsDouble)
            {
        %>
         <tr onmouseover="_mOver(this);" onmouseout="mOut(this,0);" onclick="_mClick(this,0)">
        <%
                IsDouble = false;
            }
            else
            { 
        %>
         <tr class="Grid_alter" onmouseover="_mOver(this);" onmouseout="mOut(this,1);" onclick="_mClick(this,1)">
        <%
                IsDouble = true;
            } 
            

            foreach (object o in dr.ItemArray)
            {
                switch (o.GetType().Name)
                {
                    case "String":
                        %>
                <td style="white-space:nowrap;"><%: o%></td>            
                            <%
                        break;
                    default:
                        %>
                <td style="white-space:nowrap;text-align:right"><%: o%></td>            
                            <%
                        break;
                } 
            } 
            %>
         </tr>
         <%
        } 
        %>
         </table>
         <%--//----------------------%>

         <%--//----------------------%>
         </div>
<%--         <div style="height:500px">
         
         </div>--%>
            <span style="color:Red;">共有记录:<%: dt.Rows.Count %></span>
</body>
</html>

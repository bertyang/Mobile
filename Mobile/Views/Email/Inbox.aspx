<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
	<meta charset="UTF-8">  
    <meta name="viewport" content="width=device-width,initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <style type="text/css">
        #tablehelp{
            background-color:#000000;
        }
        #tablehelp tr{
           background-color:#ECF5FF;
        }
    </style> 
    <script type="text/javascript" language="javascript">

        //页面初始化
        $(document).ready(function () {
            $('#grid').datagrid({
                url: '/Email/EmailSearch/?pageNumber=<%=ViewData["pageNumber"]%>' + '&workerID=' + '<%=ViewData["workerID"] %>' + '&folderID=' + '<%=ViewData["folderID"] %>',
                rowStyler: function (rowIndex, rowData) {
                    if (rowData.readFlag == 0) {
                        return 'color:red;font-weight:bold';
                    }
                },
                onClickRow: function (rowIndex, rowData) {

                   

                    //如果是未读，更改为已读
                    if (rowData.readFlag == "0") {
                        isRead(rowData.ID);               
                    }

                    $('#att_grid').datagrid({
                        url: '/Email/GetFile/?mailID=' + rowData.ID
                    });

                    $('#title').val(rowData.title);
                    $('#from').val(rowData.sender);
                    $('#to').val(rowData.receiver);
                    $('#cc').val(rowData.CC);
                    $('#content').html(rowData.Content);
                    $('#date').val(rowData.sendDate);
					 $.mobile.go("#apage");
                }
            });
        });

        //获取指定datagrid的当前页
        //wpf
        function getCurrentPage(gridName) {
            var options = $('#' + gridName).datagrid('getPager').data("pagination").options;
            var curr = options.pageNumber;
            return curr;
        }
  
        function isRead(id)
        {
            $.ajax({
                    type: "POST",
                    url: '/Email/ReadFlag/?mailID=' + id + '&workerID=' + <%=ViewData["workerID"] %>,
                    success: function () {
                        parent.refreshEmail(); //刷新“未读邮件”数量
                    },
                    error: function () {
                        $.messager.alert('错误', '检查错误！', "error");
                    }
                });  
        }

        function Open(src) {

            $('#main').attr("src", src);
            $('#form').show();
            $('#form').dialog({
                collapsible: true,
                minimizable: true,
                maximizable: true,
                height: 500,
                width: 800,
                modal: true, //阴影（弹出会影响页面大小）
                title: '追寻签核'
            });
        }

        function formatDownLoad(val, row) {
            return "<a href='/Email/FileDownLoad/?fileId=" + row.编码 + "'><img src='../../Content/images/button_Onduty.Image.gif' border='0'/></a>";
        }  
       
    </script>
</head>
<body>
     <div class="easyui-navpanel">
         <header>
            <div class="m-toolbar">
                <span id="title" class="m-title">邮件</span>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>
            </div>
        </header>       
        <table id="grid"  align="center"  pagination="true" width="100%"
            pagenumber="1" pagelist="[10, 15, 20]" pagesize="15" idfield="ID" nowrap="false"
            striped="true" rownumbers="true" sortname="sendDate" sortorder="desc" 
            remotesort="false" fit="true" fitcolumns="true" singleSelect="true">
            <thead>
                <tr>
                    <th field="sender" width="100" align='center'>
                        发件人
                    </th>
                    <th field="title" width="200" align='center'>
                        主题
                    </th>
                    <th field="sendDate" width="100" align='center'>
                        时间
                    </th>
                    <th field="readFlag" width="60" align='center' hidden='true'>
                        是否阅读
                    </th>
                    <th field="Content" width="60" align='center' hidden='true'>
                        内容
                    </th>
                    <th field="receiver" width="60" align='center' hidden='true'>
                        收件人
                    </th>
                    <th field="CC" width="60" align='center' hidden='true'>
                        抄送
                    </th>
                </tr>
            </thead>
        </table>
   </div>
    <div id="apage" class="easyui-navpanel">
        <header>
            <div class="m-toolbar">
                <span id="title" class="m-title">内容</span>
                <div class="m-left">
                    <a href="javascript:void(0)" class="easyui-linkbutton m-back" data-options="plain:true,outline:true,back:true">返回</a>
                </div>          
            </div>
        </header>       
	    <div region="north" style="height: 30%;"> 
            <table>
            <tr>
                <td align="left" width="3%">
                </td>
                <td align="left" width="10%">
                    主&nbsp;&nbsp;&nbsp;题：
                </td>
                <td>
                    <input id="title" type="text" class="easyui-validatebox" style="color: #15428B; border: 0;
                        background: transparent; width: 500px; height: 18px" />
                </td>
            </tr>
            <tr>
                <td align="left" width="3%">
                </td>
                <td align="left" width="10%">
                    发件人：
                </td>
                <td align="left">
                    <input id="from" type="text" class="easyui-validatebox" style="border: 0; background: transparent;
                        width: 500px; height: 18px" />
                </td>
            </tr>
            <tr>
                <td align="left" width="3%">
                    &nbsp;
                </td>
                <td align="left" width="10%">
                    收件人：
                </td>
                <td align="left">
                    <input id="to" type="text" class="easyui-validatebox" style="border: 0; background: transparent;
                        width: 500px; height: 18px" />
                </td>
            </tr>
            <tr>
                <td align="left" width="3%">
                    &nbsp;
                </td>
                <td align="left" width="10%">
                    抄&nbsp;&nbsp;&nbsp;送：
                </td>
                <td align="left">
                    <input id="cc" type="text" class="easyui-validatebox" style="border: 0; background: transparent;
                        width: 500px; height: 18px" />
                </td>
            </tr>
<%--            <tr style="display:none">
                <td align="left" width="3%">
                    &nbsp;
                </td>
                <td align="left" width="10%">
                    密&nbsp;&nbsp;&nbsp;送：
                </td>
                <td align="left">
                    <input id="sc" type="text" class="easyui-validatebox" style="border: 0; background: transparent;
                        width: 500px; height: 18px" />
                </td>
             </tr>--%>
             <tr>
                    <td align="left" width="3%">
                        &nbsp;
                    </td>
                    <td align="left" width="10%">
                        时&nbsp;&nbsp;&nbsp;间：
                    </td>
                    <td align="left">
                        <input id="date" type="text" class="easyui-validatebox" style="border: 0; background: transparent;
                            width: 500px; height: 18px" />
                    </td>
             </tr>
                <tr id="msg" style="display: none">
                    <td align="left" width="3%">
                        &nbsp;
                    </td>
                    <td align="left" width="10%">
                        提&nbsp;&nbsp;&nbsp;示：
                    </td>
                    <td align="left">
                        <input type="text" value="你不在收件人里，可能这封邮件是密送给你的。" style="border: 0; background: transparent;
                            width: 500px; height: 18px" />
                    </td>
                </tr>
           </table>
        </div>
        <div region="center" style="height: 48%;overflow-x: hidden">
            <div id="content"  style="height: 100%;overflow: auto;border: 0; background-color: #ECF5FF"></div>
        </div>
        <div region="south" style="height: 22%; overflow-x: hidden">
            <table id="att_grid" class="easyui-datagrid" idfield="编码" sortname="文件大小" sortorder="asc" singleselect="true"
                fit="true" fitcolumns="true">
                <thead>
                    <tr>
                        <th field="原附件名" width="45%" align='center'>
                            附件
                        </th>
                        <th field="文件大小" width="45%" align='center'>
                            大小
                        </th>
                        <th field="编码" width="10%" align='center' formatter='formatDownLoad'>
                            下载
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
</body>
</html>

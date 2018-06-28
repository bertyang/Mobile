<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Default</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>

    <script type="text/javascript" language="javascript">
//         $(function () {
//            $('#dd').tooltip({  
//                position: 'right',  
//                content: '<span style="color:#fff">This is the tooltip message.</span>',  
//                onShow: function(){  
//                    $(this).tooltip('tip').css({  
//                        backgroundColor: '#666',  
//                        borderColor: '#666'  
//                    });  
//                }  
//            });  
//        });

        function getAll(type) { 
            if(!this.parent.$('#tabs').tabs('exists','qq')){
		        this.parent.$('#tabs').tabs('add',{
			        title: "办公信息列表",
                    content: createFrame('/Office/OfficeSendList/?type=' + type),
                    closable: true,
		        });
	        }else{
		        this.parent.$('#tabs').tabs('select','办公信息列表');
		        this.parent.$('#mm-tabupdate').click(); 
	        }       
        }

  
    </script>
</head>
<body>
    <div id="portal" class="easyui-layout" fit="true" style="height: 250px;">
        <div region="west" title="通知公告" split="true" style="overflow-y: hidden; width:550px" iconcls="icon tu1703" data-options="tools:'#Office-2'">
            <ul>
                <% foreach (var entity in ((dynamic)ViewData["Office-2"]))
                   { %>
                        <li><a href="#"  title="<%=entity.标题%>" onclick="AddTab('<%=entity.标题%>','/Office/OfficeDetail/?officeId=<%=entity.编码%>','icon tu1703');">
                            <%=entity.标题%> (<%=entity.创建时间.ToString("yyyy-MM-dd")%>)
                            </a>
                        </li>
                        <br>
                <% } %>
            </ul>
        </div>
        <div id='Office-1'>
             <a href="javascript:void(0)" title="更多" class="icon-more" onclick="getAll('Office-1')"></a>
        </div>
        <div id='Office-2'>
             <a href="javascript:void(0)" title="更多" class="icon-more" onclick="getAll('Office-2')"></a>
        </div>
        <div id='Office-3'>
             <a href="javascript:void(0)" title="更多" class="icon-more" onclick="getAll('Office-3')"></a>
        </div>
        <div id='Office-4'>
             <a href="javascript:void(0)" title="更多" class="icon-more" onclick="getAll('Office-4')"></a>
        </div>
        <div id='Office-6'>
             <a href="javascript:void(0)" title="更多" class="icon-more" onclick="getAll('Office-6')"></a>
        </div>
        <div region="center" title="中心动态" split="true" style="overflow-y: hidden;" iconcls="icon tu0415" data-options="tools:'#Office-1'">
              <ul>
                <% foreach (var entity in ((dynamic)ViewData["Office-1"]))
                   { %>
                        <li><a href="#"  title="<%=entity.标题%>" onclick="AddTab('<%=entity.标题%>','/Office/OfficeDetail/?officeId=<%=entity.编码%>','icon tu0415');">
                            <%=entity.标题%> (<%=entity.创建时间.ToString("yyyy-MM-dd")%>)
                            </a>
                        </li>
                        <br>
                <% } %>
              </ul>
        </div>
        <div region="south" style="height: 225px; padding-top: 2px">
            <div class="easyui-layout" fit="true" style="height: 225px; margin-top: 2px; ">
                <div region="west" split="true"  title="文件" class="easyui-tabs" style="width: 550px; overflow-y: hidden"   iconcls="icon tu0719" data-options="tools:'#Office-4'">
                    <div title="中心文件" style="padding:20px;">  
                         <ul>
                            <% foreach (var entity in ((dynamic)ViewData["Office-4"]))
                               { %>
                                     <li>
                                        <a href="#" title="<%=entity.标题%>" onclick="AddTab('<%=entity.标题%>','/Office/OfficeDetail/?officeId=<%=entity.编码%>','icon tu0719');">
                                            <%=entity.标题%>  (<%=entity.创建时间.ToString("yyyy-MM-dd")%>)
                                        </a>
                                     </li>
                                     <br>
                            <% } %>
                        </ul> 
                    </div>  
                    <div title="上级文件"  style="overflow:auto;padding:20px;">  
                         <ul>
                            <% foreach (var entity in ((dynamic)ViewData["Office-5"]))
                               { %>
                                     <li>
                                        <a href="#" title="<%=entity.标题%>" onclick="AddTab('<%=entity.标题%>','/Office/OfficeDetail/?officeId=<%=entity.编码%>','icon tu0719');">
                                            <%=entity.标题%>  (<%=entity.创建时间.ToString("yyyy-MM-dd")%>)
                                        </a>
                                     </li>
                                     <br>
                            <% } %>
                        </ul>  
                    </div> 
                </div>
                <div region="center" split="true" title="其他" class="easyui-tabs" style="height: 100%; overflow-y: hidden"  iconcls="icon tu1518" data-options="tools:'#Office-6'">
                     <div title="党务、政务公开栏" style="padding:20px;">  
                         <ul>
                            <% foreach (var entity in ((dynamic)ViewData["Office-6"]))
                               { %>
                                     <li>
                                        <a href="#" title="<%=entity.标题%>" onclick="AddTab('<%=entity.标题%>','/Office/OfficeDetail/?officeId=<%=entity.编码%>','icon tu1518');">
                                            <%=entity.标题%>  (<%=entity.创建时间.ToString("yyyy-MM-dd")%>)
                                        </a>
                                     </li>
                                     <br>
                            <% } %>
                        </ul> 
                    </div>  
                    <div title="法律法规"  style="overflow:auto;padding:20px;">  
                         <ul>
                            <% foreach (var entity in ((dynamic)ViewData["Office-3"]))
                               { %>
                                     <li>
                                        <a href="#" title="<%=entity.标题%>" onclick="AddTab('<%=entity.标题%>','/Office/OfficeDetail/?officeId=<%=entity.编码%>','icon tu1518');">
                                            <%=entity.标题%>  (<%=entity.创建时间.ToString("yyyy-MM-dd")%>)
                                        </a>
                                     </li>
                                     <br>
                            <% } %>
                        </ul>  
                    </div>
                    <div title="资料下载"  style="overflow:auto;padding:20px;">  
                         <ul>
                            <% foreach (var entity in ((dynamic)ViewData["Office-7"]))
                               { %>
                                     <li>
                                        <a href="#" title="<%=entity.标题%>" onclick="AddTab('<%=entity.标题%>','/Office/OfficeDetail/?officeId=<%=entity.编码%>','icon tu1518');">
                                            <%=entity.标题%>  (<%=entity.创建时间.ToString("yyyy-MM-dd")%>)
                                        </a>
                                     </li>
                                     <br>
                            <% } %>
                        </ul>  
                    </div>
                      
                </div>
            </div>
        </div>
    </div>
    <%if (ViewData["Login"] !="" )     {%>
    <div id="dlg" class="easyui-dialog" title="通知" data-options="iconCls:'icon-tip'" style="width:350px;height:100px;padding:10px">
       <%= ViewData["Login"]%>
    </div>
    <% } %>
</body>
</html>

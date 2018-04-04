<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <%: Scripts.Render("~/ckeditor/js")%>

    <script type="text/javascript" language="javascript">

        function getBackURL() {
            var vurl = '/Office/OfficeSendList/?pageNumber=<%=(dynamic)this.ViewData["pageNumber"] %>';
            //过滤条件初始值
            vurl = vurl + "&type="+'<%=(dynamic)this.ViewData["_type"] %>'
                        + "&startTime="+'<%=(dynamic)this.ViewData["startTime"] %>'
                        + "&title="+'<%=(dynamic)this.ViewData["_title"] %>'
                        + "&writer="+'<%=(dynamic)this.ViewData["_writer"] %>';
            return vurl;
        }

        //保存
        function submit() {
            $('#form').form('submit', {
                onSubmit: function () {
                    var a = $('#receive').combotree('getText'); //获取接收部门名称
                    $('#hidden_receive').val(a);

                    switch($('#sendType').combobox('getValue'))
                    {
                    
                        case "1":
                            break;
                        case "2":
//                            debugger;
                            if($('#receive').combotree('getValues').lenth==0)
                            {
                                $.messager.alert('提示', "未选择接部门", 'info', function () {
                                });
                                return false;
                            }; //
                            break;
                        case "3":
//                            debugger;
                            var jsr=new Array(),jsrBM=new Array();
                            
//                            var list=[],oHash={};
                            $.each(oHash, function (key, value) {
                                jsr.push(key);
                                jsrBM.push(value);
//                                jsr[i]=$(n).attr("idname");
//                                jsrBM[i]=$(n).find("font").text();
                            });

                            if(jsr.length==0)
                            {
                                $.messager.alert('提示', "未选择接收人", 'info', function () {
                                });
                                return false;
                            }
                            $('#hidden_receivePerN').val(jsr.join(","));
                            $('#hidden_receivePer').val(jsrBM.join(","));
                            break;
                        default:
                    }
                    return $(this).form('validate');
                },
                success: function (msg) {
                    var data = eval('(' + msg + ')');
                    if (data.IsSuccess) {
                        $.messager.alert('提示', data.Message, 'info', function () {
                            location.href = getBackURL();
                        });
                    }
                    else {
                        $.messager.alert('提示', data.Message, 'info', function () {
                        });
                    }
                }
            });
        }

        function sendTypeonChange() {
            switch($('#sendType').combobox('getValue'))
            {
                case "1":
                    document.getElementById("rec_tr").style.display = 'none';
//                    $('#receive').combotree({required:"false"}); //
                    $('#RYtr').hide(); 
                    break;
                case "2":
                    document.getElementById("rec_tr").style.display = 'inline';
//                    $('#receive').combotree({required:"true"}); //
                    $('#RYtr').hide(); 
                    break;
                case "3":
                    document.getElementById("rec_tr").style.display = 'none';
//                    $('#receive').combotree({required:"false"}); //
                    $('#RYtr').show(); 
                    break;
                default:
            }
        }

        var list=[],oHash={};

        //页面初始化
        $(document).ready(function () {
        
            //发送类型
            $('#sendType').combobox({
                data: [{ value: "1", name: "全体人员" }, { value: "2", name: "发送到部门"}, { value: "3", name: "发送到人"}],
                valueField: 'value',
                textField: 'name',
                onLoadSuccess: function (data) {
                    $('#sendType').combobox('setValue', <%=ViewData["sendType"] %>);
                    sendTypeonChange();
                },
                onChange: sendTypeonChange
            });

            //确认的接收者 Person
            var SPerN='<%=ViewData["receivePerN"] %>',SPer='<%=ViewData["receivePer"] %>';
            if(SPerN!='' && SPerN!='allworker')
            {
                var PerN=SPerN.split(","),Per=SPer.split(",");
            
                $.each(PerN, function (i, n) {
                    oHash[n] = Per[i];
                    $('#RYspan').append("<span idname="+n+"><font>"+Per[i] +"</font><a href='#' onclick='deletePerson(this)'><img src='../../Content/images/delete.gif' border='0'/></a>;&nbsp;&nbsp;</span>");
                });
            
            }


            //确认的接收者
            $('#receive').combotree({
                url: '/Organize/UnitTree',
                onLoadSuccess: function (data) {
                    $('#receive').combotree('setValues', [<%=ViewData["receive"]%>]);
                }
            });

            //办公类型
            $('#infoType').combobox({
                url: '/Office/OfficeType/',
                valueField: '编码',
                textField: '名称',
                onLoadSuccess: function (data) {
                    $('#infoType').combobox('setValue', '<%=ViewData["type"]%>');
                }
            });
        });

        function formatDelete(value,row,index) {
            return "<a href='#' onclick='fileDelete(" + row.编码 + ")'><img src='../../Content/images/delete.gif' border='0'/></a>";
        }

        function fileDelete(fileId){
            $.ajax({
                type: "POST",
                url: "/Office/FileDelete/?fileId=" +fileId,
                success: function (msg) {
                    var data = eval('(' + msg + ')');
                    if (data.IsSuccess) {
                        $.messager.alert('提示', '删除成功！', "info", function () {
                            $('#grid').datagrid("reload"); 
                        });
                    }
                },
                error: function () {
                    $.messager.alert('错误', '请检查错误！', "error");
                }
             });
        }

        //附件上传
        function up(){
            $('#officeAttachment').show();
            $('#officeAttachment').dialog({
                collapsible: true,
                minimizable: true,
                maximizable: true,
                height: 220,
                width: 350,
                modal: true,
                title: '附件上传',
                onClose: function () {
                    //清空上传文件框
                    var file = document.getElementById("upfile");
                    if (file.outerHTML) {
                        file.outerHTML = file.outerHTML;
                    } else {
                        file.value = "";
                    }
                },
                buttons: [
                    {
                        text: '上传',
                        iconCls: 'icon-ok',
                        handler: function () {
                            $('#up').form('submit', {
                                onSubmit: function () {
                                    //检查文件
                                    var filename = document.getElementById("upfile").value;
                                    if (filename == "") {
                                        $.messager.alert('提示', '请上传文件！', 'info');
                                        return false;
                                    }
//                                    else{
//                                        var fso = new ActiveXObject("Scripting.FileSystemObject");         
//                                        var file = fso.getfile(filename);      
//                                        var fileSize = file.size/1024; //文件大小转换为kb
//                                        if(parseFloat(fileSize) > 51200){
//                                            $.messager.alert('提示', '文件大于50M，不能上传！', 'info');
//                                            return false;
//                                        }
//                                    }
//                                    
                                    return $(this).form('validate');
                                },
                                success: function (msg) {
                                    var data = eval('(' + msg + ')');
                                    if (data.IsSuccess) {
                                        $.messager.alert('提示', data.Message, 'info', function () {
                                            document.getElementById("upfile").value = "";
                                            $('#grid').datagrid("reload");
                                            $('#officeAttachment').dialog('close');

                                            //清空上传文件框
                                            var file = document.getElementById("upfile");
                                            if (file.outerHTML) {
                                                file.outerHTML = file.outerHTML;
                                            } else {
                                                file.value = "";
                                            }
                                        });
                                    }
                                    else {
                                        $.messager.alert('提示', data.Message, 'info', function () {
                                        });
                                    }
                                },
                                error: function () {
                                    $.messager.alert('错误', '请检查错误！', "error");
                                }
                            });
                        }
                    },
                    {
                        text: '关闭',
                        iconCls: 'icon-cancel',
                        handler: function () {
                            $('#officeAttachment').dialog('close');

                            //清空上传文件框
                            var file = document.getElementById("upfile");
                            if (file.outerHTML) {
                                file.outerHTML = file.outerHTML;
                            } else {
                                file.value = "";
                            }                 
                        }
                    }]
            });                        
        }

        function SelectPerson() {
                 var url = '/Office/SelectPerson/';
                 $('#framePersonSelect').attr("src", url);
                //弹出选择框
                $('#PersonSelect').show();
                $('#PersonSelect').dialog({
                    collapsible: true,
                    minimizable: true,
                    maximizable: true,
                    height: 450,
                    width: 650,
                    modal: true, //阴影（弹出会影响页面大小）
                    title: '选择人员',
                    buttons: [{
                        text: '确定',
                        iconCls: 'icon-ok',
                        handler: function () {
                            var rows = $("#framePersonSelect")[0].contentWindow.getRows();
                            SetPerson(rows);
                            $('#PersonSelect').dialog('close');
                        }
                    },
                    {
                        text: '取消',
                        iconCls: 'icon-cancel',
                        handler: function () {
                            $('#PersonSelect').dialog('close');
                        }
                    }]
                });
        }  

        function SetPerson(rows) {
//            var list=[],oHash={};
//            debugger;
            $.each(rows, function (i, n) {
                //Worker-
                var id=n.ID;
                if(!oHash[id]){
                    oHash[id] = n.Name;
                    $('#RYspan').append("<span idname="+id+"><font>"+n.Name +"</font><a href='#' onclick='deletePerson(this)'><img src='../../Content/images/delete.gif' border='0'/></a>;&nbsp;&nbsp;</span>");
                }
            });
        }
        
        function deletePerson(src) {
//            debugger;
            var span=$(src).closest("span")
            delete oHash[span.attr("idname")];
            span.remove();
        }

    </script>
</head>

<body class="easyui-layout">
    <div region="center" border="true">
        <form id="form" method="post" enctype="multipart/form-data" action="/Office/OfficeSave/">
        
                        <input id="hidden_receivePerN" type="hidden" name="entity.接收人编码" />
                        <input id="hidden_receivePer" type="hidden" name="entity.接收人" />
                        <input id="hidden_createtime" type="hidden" name="entity.创建时间" value="<%= ((dynamic)this.ViewData["entity"]).创建时间%>"/>


            <table width="100%">
                <tr>
                    <td align="left" width="3%">
                      &nbsp;
                    </td>
                    <td align="left" width="10%">
                    </td>
                    <td align="left" width="57%">
                    </td>
                    <td align="left" valign="middle" width="30%">
                    </td>
                 </tr>    
                 <tr style="display:none">
                    <td>
                        <input type="text" class="easyui-validatebox" required="true"
                            name="entity.编码" value="<%= ((dynamic)this.ViewData["officeId"])%>" />
                    </td>
                 </tr>  
                 <tr>
                     <td align="left">
                         <input id="writerID" type="hidden" name="entity.发送人编码" value="<%= ((dynamic)this.ViewData["writerID"])%>" />
                     </td>
                     <td align="left" style="color: #FF0000">
                         发送类型：
                     </td>
                     <td align="left">
                         <select id="sendType" class="easyui-combobox" name="entity.发送类型编码" editable="false" required="true" style="width: 180px;">
                         </select>
                     </td>
                     <td align="left" valign="top" width="22%" rowspan="6">
                        <a href="#" class="easyui-linkbutton" onclick="up();">附件上传</a>
                        <table id="grid" class="easyui-datagrid" url="/Office/OfficeFile/?officeId=<%=ViewData["officeId"]%>" idField="ID" border="0"
                             sortName="Type" sortOrder= "asc" remoteSort="false" fit="true" fitColumns="true" rownumbers="true" >
                                <thead>
			                        <tr>
                                        <th field="原附件名" width="400"  align='center'>附件名</th>   
                                        <th field="文件大小" width="400"  align='center'>大小</th>
                                        <th field="编码" width="200" align='center' formatter="formatDelete">删除</th>   
			                        </tr>
		                        </thead>
                        </table>
                     </td>
                 </tr>        
                <tr id="rec_tr" style="display:none">
                    <td align="left">
                         &nbsp;
                    </td>
                    <td align="left" style="color: #FF0000">
                         选择部门：
                    </td>
                    <td align="left">
                        <select id="receive" class="easyui-combotree" name="receive" multiple
                            style="width: 180px;"></select>
                    </td>
                    <td>
                        <input id="hidden_receive" type="hidden" name="entity.接收部门" />
                    </td>
                </tr>
                <tr id="RYtr">
                    <td align="left">
                         &nbsp;
                    </td>
                    <td align="left" style="color: #FF0000">
                         选择人员：
                    </td>
                    <td align="left">
                        <span id="RYspan"></span>
                        <a href="#" class="easyui-linkbutton" onclick="SelectPerson();" ><font>人员选择</font></a>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="3%">
                        &nbsp;
                    </td>
                    <td align="left" width="10%" style="color: #FF0000">
                         办公类型：
                    </td>
                    <td align="left" width="60%">
                        <select id="infoType" class="easyui-combobox" editable="false"  name="entity.办公类型编码" required="true" style="width: 180px;"></select>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;
                    </td>
                    <td align="left" style="color: #FF0000">
                        标题：
                    </td>
                    <td align="left">
                        <input type="text" class="easyui-validatebox" required="true" name="entity.标题" validtype="length[1,100]"
                            value="<%= ((dynamic)this.ViewData["entity"]).标题 == null?"":((dynamic)this.ViewData["entity"]).标题%>"
                            style="border: 1px solid #8DB2E3; width: 176px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;
                    </td>
                    <td align="left">
                        作者：
                    </td>
                    <td align="left">
                        <input type="text" class="easyui-validatebox" required="true" name="entity.作者" validtype="length[1,20]"
                            value="<%= ((dynamic)this.ViewData["writer"])%>" style="border: 1px solid #8DB2E3;
                            width: 176px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="middle">
                         &nbsp;
                    </td>
                    <td align="left" valign="middle">
                         内容：
                    <br />
                         (内容超过2000字时建议使用附件发送)</td>
                    <td align="left">
                        <textarea id="editor" name="editor" class="ckeditor" cols="80" rows="10"><%= (this.ViewData["content"])%></textarea>
                    </td>     
                </tr> 
<%--                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td colspan=2><input name="checkboxSaveSelfDept" type="checkbox" checked=true>保存到自己部门公告栏</td>
                </tr>--%>
          </table>
       </form>
        
    </div>
    <div region="south" border="true" style="text-align: right; height: 40px; line-height: 30px;
        background-color: #f7f7f7;">
        <table style="width: 100%">
            <tr>
                <td>
                </td>
                <td style="text-align: LEFT">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-save" onclick="submit();">保存</a>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="location.href = getBackURL();">取消</a>
                </td>
            </tr>
        </table>
    </div>

    <!--上传附件窗口-->
    <div id="officeAttachment" icon="icon-save" style="padding: 5px; display: none;">
        <form id="up" action="/Office/FileUpload/" method="post" enctype="multipart/form-data">
            <input type="hidden" id="officeCode" name="officeCode" class="easyui-validatebox" value="<%= ((dynamic)this.ViewData["officeId"])%>" />
            <input type="file" id="upfile" name="upfile" style="border: 1px solid #8DB2E3; width: 200px;height: 25px" />
        </form>
    </div>

    
    <!--选择接收人窗口-->
    <div id="PersonSelect" icon="icon-save" style="padding: 5px; display: none">
        <iframe id="framePersonSelect" scrolling="auto"  frameborder="0" style="width: 100%; height: 100%;">
        </iframe>
    </div>
</body>
</html>

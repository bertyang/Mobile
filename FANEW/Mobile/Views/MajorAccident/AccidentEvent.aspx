<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script type="text/javascript" language="javascript">
        //页面初始化
        $(document).ready(function () {

            //事故类型
            $('#type').combotree({
                url: '/MajorAccident/AccidentType/',
                onLoadSuccess: function (data) {
                    $('#type').combotree('setValue', '--请选择--');
                }
            });

            //事发区域
            $('#place').combobox({
                url: '/MajorAccident/AccidentAddress/',
                valueField: 'Name',
                textField: 'Name',
                onLoadSuccess: function (data) {
                    $('#place').combobox('setValue', '--请选择--');
                }
            });

            //事故等级
            $('#level').combobox({
                url: '/MajorAccident/AccidentLevel/',
                valueField: 'ID',
                textField: 'Name',
                onLoadSuccess: function (data) {
                    $('#level').combobox('setValue', '--请选择--');
                }
            });

            //事件关联按钮显示文本
            $('#count').linkbutton({   
                text:'0起事件关联'
            });   
        });

            //关联事件数
            function formatCount(val, row) {
                return "<a href='#' onclick='AccidentInfo(" + row.事故编码 + ")'><font color='#15428B'>"+ val +"</font></a>";
            }

//            //详细
//            function formatDetail(val, row) {
//                return "<a href='#' onclick='AccidentInfo(" + row.事故编码 + ")'><img alt='事故详细' src='../../Content/images/find.gif' border='0'/></a>";
//            }

            //关联
            function formatRelation(val, row) {
                var n = $('#hiddenRe').val().split(val).length-1;
                if (n) {
                    return "<img alt='关联' src='../../Content/images/Untable_relationship.png' border='0'/></a>";
                } else {
                    return "<a href='#' onclick='GetCount(" + row.事故编码 + ")'><img alt='关联' src='../../Content/images/table_relationship.png' border='0'/></a>";
                }
            }

            //打印
            function formatPrint(val, row) {
             var src = "/Areas/Report/Statistics/AccidentPrint.aspx/?accidentCode=" + row.事故编码
              var title = '重大事故打印' ;
              return "<span style='color:red;'><a href='#' onclick=AddTab('" + title + "','" + src + "','tu1912')><img alt='打印' src='../../Content/images/print.png' border='0'/></a></span>";
            }

            //主事件
            function formatMainEvent(val, row) {
                return "<input type='radio' name='mainEvent'/>"; 
            }
            
            //解除事故关联
            function formatDelRelation(val, row) {
                if (row.事件编码 == row.所属事故编码) {
                    return "<img src='../../Content/images/UnClosetable_relationship.png' border='0'/>";
                } else {
                    return "<a href='#' onclick='Release(" + row.事故编码 + ")'><img src='../../Content/images/Closetable_relationship.png' border='0'/></a>";
                }
                
            }

            //格式化事故列表弹出框的“事件名称”列
            function formatEventName(val, row) {
                if (row.事件编码 == row.所属事故编码) {
                    return "<span style='color:red;'>" + val + "</span><img alt='主事件' src='../../Content/images/AccidentImp.png'/>";
                } else {
                    return "<span style='color:#007799;'>" + val + "</span>";
                }
            }

            //查询
            function doSearch() {
                $('#grid').datagrid('load', {
                    startTime: $('#startTime').datetimebox('getText'),
                    endTime: $('#endTime').datetimebox('getText'),
                    accidentName: $('#accidentName').val(),
                    type: $('#type').combotree('getValue'),
                    place: $('#place').combobox('getValue'),
                    level: $('#level').combobox('getValue'),
                    ActionId: "<%=Request.QueryString["ActionId"]%>"
                });
            }

            //得到关联的事件总数
            function GetCount(val) {

                var row = $('#grid').datagrid('getSelected');
                formatRelation(val, row);
                $('#grid').datagrid('reload');

                var v = $('#hiddenRe').val();
                $('#hiddenRe').val(v + ',' + val);

                //事件关联按钮动态显示信息
                var count = $('#hiddenRe').val().match(/,/g).length;
                $('#count').linkbutton({
                    text: count + "起事件关联"
                });
            }

             //事件关联按钮点击事件
             function Relation() {
                 $('#AccidentRelation').show();
                 $('#AccidentRelation').dialog({
                     collapsible: true,
                     minimizable: true,
                     maximizable: true,
                     height: 400,
                     width: 1000,
                     modal: true,
                     title: ' 事故关联',
                     buttons: [
                            {
                                text: '事故合并',
                                iconCls: 'icon-ok',
                                handler: function () {
                                    var node = $('#relation').datagrid('getSelected');
                                    if (!node) {
                                        $.messager.alert('提示', '请先选择主事件');
                                        return;
                                    }
                                    var url = '/MajorAccident/AccidentCombine/?accidentId=' + node.事件编码 + '&list=' + escape($('#hiddenRe').val());
                                    $.post(url, function (msg) {
                                        var data = eval('(' + msg + ')');
                                        if (data.IsSuccess) {
                                            $.messager.alert('提示', data.Message, 'info', function () {
                                                $('#relation').datagrid('reload');
                                                $('#grid').datagrid('reload');
                                            });
                                        }
                                        else {
                                            $.messager.alert('提示', data.Message, 'info', function () {
                                            });

                                        }
                                    });

                                }
                            },
                            {
                                text: '关闭',
                                iconCls: 'icon-cancel',
                                handler: function () {
                                    $('#AccidentRelation').dialog('close');
                                    $('#hiddenRe').val("");
                                    $('#grid').datagrid('reload');
                                    $('#count').linkbutton({
                                        text: '0起事件关联'
                                    }); 
                                }
                            }]
                 });
                  $('#relation').datagrid({
                      url: '/MajorAccident/AccidentRelation/?accidentId=' + escape($('#hiddenRe').val())
                  });
              }

             //事故解除关联
             function Release(val) {
                 var node = $('#relation').datagrid('getSelected');
                 $.ajax({
                     type: "POST",
                     url: "/MajorAccident/AccidentRelease/?accidentId=" + node.事件编码,
                     success: function (msg) {
                         var data = eval('(' + msg + ')');
                         if (data.IsSuccess) {
                             $.messager.alert('提示', data.Message, 'info', function () {
                                 $('#relation').datagrid("reload");
                                 $('#grid').datagrid("reload");
                             });
                         }
                     },
                     error: function () {
                         $.messager.alert('提示', data.Message, 'info', function () {
                         });
                     }
                 }); 
             }

            //事故列表弹出框
            function AccidentInfo(val) {
                $('#AccidentList').show();
                $('#AccidentList').dialog({
                    collapsible: true,
                    minimizable: true,
                    maximizable: true,
                    height: 400,
                    width: 1000,
                    modal: true,
                    title: ' 事故列表',
                    buttons: [
                        {
                            text: '关闭',
                            iconCls: 'icon-cancel',
                            handler: function () {
                                $('#AccidentList').dialog('close');
                            }
                        }]
                 });
                    $('#eventList').datagrid({
                        url: '/MajorAccident/AccidentList/?accidentId=' + val
                 });
             }

             function Read(type) {

                 var node = $('#eventList').datagrid('getSelected');
                //                alert(node.事件编码);

                 if (type == 'patient') {
                     //查看伤病员
                     if (!node) {
                         $.messager.alert('提示', '请选择具体的事件');
                         return;
                     }
                     $('#AccidentPatient').show();
                     $('#AccidentPatient').dialog({
                         collapsible: true,
                         minimizable: true,
                         maximizable: true,
                         height: 400,
                         width: 1000,
                         modal: true,
                         title: node.事件名称 + '--伤病员列表',
                         buttons: [
                                     {
                                         text: '关闭',
                                         iconCls: 'icon-cancel',
                                         handler: function () {
                                             $('#AccidentPatient').dialog('close');
                                         }
                                     }]
                     });
                     $('#patient_grid').datagrid({
                         url: '/MajorAccident/AccidentPatientInfo/?eventId=' + node.事件编码
                     });

                     //病情
                     $('#illState').combobox({
                         url: '/MajorAccident/PatientIllState/',
                         valueField: 'ID',
                         textField: 'Name',
                         onLoadSuccess: function (data) {
                             $('#illState').combobox('setValue', '--请选择--');
                         }
                     });

                     //相关车辆
                     $('#ambulance').combobox({
                         url: '/MajorAccident/AccidentAmbulanc/?eventId=' + node.事件编码,
                         valueField: 'ID',
                         textField: 'Name',
                         onLoadSuccess: function (data) {
                             $('#ambulance').combobox('setValue', '--请选择--');
                         }
                     });

                     //新增--伤病员信息
                     $('#patient_add').bind('click',
                                 function () {
                                     $('#AccidentPatientEdit').show();
                                     $('#AccidentPatientEdit').dialog({
                                         collapsible: true,
                                         minimizable: true,
                                         maximizable: true,
                                         height: 400,
                                         width: 1000,
                                         modal: true,
                                         title: '编辑伤病员信息',
                                         buttons: [
                                                {
                                                    text: '保存',
                                                    iconCls: 'icon-save',
                                                    handler: function () {
                                                        $('#form').form('submit', {
                                                            onSubmit: function () {
                                                                return $(this).form('validate');
                                                            },
                                                            success: function (msg) {
                                                                var data = eval('(' + msg + ')');
                                                                if (data.IsSuccess) {
                                                                    $.messager.alert('提示', data.Message, 'info', function () {
                                                                        $('#AccidentPatientEdit').dialog('close');
                                                                        $('#patient_grid').datagrid('reload');
                                                                    });
                                                                }
                                                                else {
                                                                    $.messager.alert('提示', data.Message, 'info', function () {
                                                                    });

                                                                }
                                                            }
                                                        });
                                                    }
                                                },
                                                 {
                                                     text: '关闭',
                                                     iconCls: 'icon-cancel',
                                                     handler: function () {
                                                         $('#AccidentPatientEdit').dialog('close');

                                                     }
                                                 }]
                                     });
                                     $('#accidentId').val(node.事件编码); //事故编码
                                     $('#taskCode').val(""); //任务编码
                                     $('#orderNum').val("0"); //序号
                                     $('#patient').val(""); //伤者姓名
                                     $('#sex').combobox('setValue', '不详'); //性别
                                     $('#age').val(""); //年龄
                                     $('#ageUnit').combobox('setValue', '岁'); //年龄单位
                                     $('#vest').val(""); //转归
                                     $('#illState').combobox('setValue', '--请选择--'); //病情
                                     $('#professional').val(""); //职业
                                     $('#unit').val(""); //单位
                                     $('#ambulance').combobox('setValue', '--请选择--'); //相关车辆
                                     $('#hospital').val(""); //收治医院
                                     $('#address').val(""); //住址       
                                     $('#diagnosis').val(""); //初步诊断
                                     $('#treatment').val(""); //治疗方式             
                                 }
                         );

                     //修改--伤病员信息
                      $('#patient_edit').bind('click',
                                 function () {
                                     var s = $('#patient_grid').datagrid('getSelected');
                                     if (s) {
                                         $('#AccidentPatientEdit').show();
                                         $('#AccidentPatientEdit').dialog({
                                             collapsible: true,
                                             minimizable: true,
                                             maximizable: true,
                                             height: 400,
                                             width: 1000,
                                             modal: true,
                                             title: '编辑伤病员',
                                             buttons: [
                                                {
                                                    text: '保存',
                                                    iconCls: 'icon-save',
                                                    handler: function () {
                                                        $('#form').form('submit', {
                                                            onSubmit: function () {
                                                                return $(this).form('validate');
                                                            },
                                                            success: function (msg) {
                                                                var data = eval('(' + msg + ')');
                                                                if (data.IsSuccess) {
                                                                    $.messager.alert('提示', data.Message, 'info', function () {
                                                                        $('#AccidentPatientEdit').dialog('close');
                                                                        $('#patient_grid').datagrid('reload');
                                                                    });
                                                                }
                                                                else {
                                                                    $.messager.alert('提示', data.Message, 'info', function () {
                                                                    });

                                                                }
                                                            }
                                                        });
                                                    }
                                                },
                                                 {
                                                     text: '关闭',
                                                     iconCls: 'icon-cancel',
                                                     handler: function () {
                                                         $('#AccidentPatientEdit').dialog('close');
                                                     }
                                                 }]
                                         });

                                         $('#accidentId').val(s.事故编码); //事故编码
                                         $('#taskCode').val(s.任务编码); //任务编码 
                                         $('#orderNum').val(s.序号); //序号
                                         $('#patient').val(s.姓名); //伤者姓名
                                         $('#sex').combobox('setValue', s.性别); //性别
                                         $('#age').val(s.年龄.replace(/[^0-9]/ig,"")); //年龄
                                         $('#ageUnit').combobox('setValue', s.年龄单位); //年龄单位
                                         $('#vest').val(s.转归); //转归
                                         $('#illState').combobox('setValue', s.病情编码); //病情
                                         $('#professional').val(s.职业); //职业
                                         $('#unit').val(s.单位); //单位
                                         $('#ambulance').combobox('setValue', s.相关车辆编号); //相关车辆
                                         $('#hospital').val(s.收治医院); //收治医院
                                         $('#address').val(s.住址); //住址
                                         $('#diagnosis').val(s.初步诊断); //初步诊断
                                         $('#treatment').val(s.治疗方式); //治疗方式
                                     }
                                     else {
                                         $.messager.alert('提示', '请选择伤病员');
                                         return;
                                     }
                                 }
                             );
                     //删除伤病员
                     $('#patient_del').bind('click',
                              function () {
                                  var row = $('#patient_grid').datagrid('getSelected');
                                  if (row) {
                                      $.messager.confirm('提示', '是否删除这些数据?', function (r) {
                                          if (!r) {
                                              return;
                                          }
                                          $.ajax({
                                              type: "POST",
                                              url: "/MajorAccident/AccidentPatientDelete/?eventId=" + node.事件编码 + "&number=" + row.序号,
                                              success: function (msg) {
                                                  var data = eval('(' + msg + ')');
                                                  if (data.IsSuccess) {
                                                      $.messager.alert('提示', data.Message, "info", function () {
                                                          $('#patient_grid').datagrid("reload");
                                                          $('#eventList').datagrid("reload");
                                                          $('#grid').datagrid("reload");
                                                      });
                                                  }
                                              },
                                              error: function () {
                                                  $.messager.alert('错误', data.Message, "error");
                                              }
                                          });
                                      });
                                  } else {
                                      $.messager.alert('提示', '请选择要删除的伤病员');
                                      return;
                                  }
                              }
                        );
                 }
                 else {
                     //查看事件详细信息
                     if (!node) {
                         $.messager.alert('提示', '请选择要查看的事件');
                         return;
                     }
                     this.parent.$('#tabs').tabs('add',{
			    title: "事件详细信息[" + node.事件编码 + "]",
                content: createFrame('/BasicInfo/AccLoad/?ActionId=10004&id=' + node.事件编码),
                closable: true
		    });
//                     $('#w').window({
//                        title: '受理信息',
//                        width: 500,
//                        modal: true,
//                        shadow: true,
//                        height: 255,
//                        resizable: false,
//                    });
//                    $('#acc').datagrid({
//                        iconCls: 'icon-save',
//                        nowrap: false,
//                        striped: true,
//                        url: '/BasicInfo/AccLoad/' + node.事件编码,
//                        sortName: 'ID',
//                        sortOrder: 'asc',
//                        remoteSort: false,
//                        fitColumns: true,  //自适应列宽
//                        singleSelect: "true",
//                        columns: [[
//                        { field: 'ID', title: '事件编码', width: 80, align: 'center' },
//                        { field: 'Order', title: '受理序号', width: 50, align: 'center' },
//                        { field: 'Type', title: '受理类型', width: 50, align: 'center' },
//                        { field: 'Person', title: '受理人员', width: 50, align: 'center' }

//                        ]]
//                        });
                
//                    $('#w').window('open');
                 }  
             }

             //删除事件
             function DeleteEvent() {
                 var node = $('#eventList').datagrid('getSelected');
                 var row = $('#grid').datagrid('getSelected');
                 if (node) {
                     if (node.事件编码 == node.所属事故编码 && row.关联事件数 > 1) {
                         $.messager.alert('提示', '这是主事件，请先删除其他事件');
                         return;
                     }
                     $.messager.confirm('提示', '是否删除该事件?', function (r) {
                         if (!r) {
                             return;
                         }
                         $.ajax({
                             type: "POST",
                             url: "/MajorAccident/AccidentDelete/?eventId=" + node.事件编码,
                             success: function (msg) {
                                 if (msg.IsSuccess) {
                                     $.messager.alert('提示', '删除成功！', "info", function () {
                                         $('#eventList').datagrid("reload");
                                         $('#grid').datagrid("reload");
                                     });
                                 }
                             },
                             error: function () {
                                 $.messager.alert('错误', '删除失败！', "error");
                             }
                         });
                     });
                 }
                 else {
                     $.messager.alert('提示', '请选择要删除的事件');
                     return;
                 }
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
                    content: createFrame('/BasicInfo/AccInfo/?code=' + row.ID + '&&order=' + row.Order),
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

             function cancel()
             {
                 $('#w').window('close');
             }
    </script>
</head>
<body class="easyui-layout">
    <div region="center" style="padding: 5px" border="true">
        <table id="grid" class="easyui-datagrid" align="center" toolbar="#tb" url="/MajorAccident/AccidentSearch/"
            pagination="true" pagenumber="1" pagelist="[10, 15, 20]" pagesize="15" idfield="事故编码"
            nowrap="false" striped="true" singleselect="true" sortname="事发时间" sortorder="desc"
            remotesort="false" fit="true" fitcolumns="true">
            <thead>
                <tr>
                    <th field="关联事件数" width="100" align='center' formatter='formatCount'>
                        关联事件数
                    </th>
                    <th field="事故名称" width="200" align='center'>
                        事故名称
                    </th>
                    <th field="调度员" width="100" align='center'>
                        调度员
                    </th>
                    <th field="事发时间" width="200" align='center'>
                        事发时间
                    </th>
                    <th field="事发区域" width="100" align='center'>
                        事发区域
                    </th>
                    <th field="事故类型" width="100" align='center'>
                        事故类型
                    </th>
                    <th field="事故等级" width="100" align='center'>
                        事故等级
                    </th>
                    <th field="伤亡总人数" width="100" align='center'>
                        伤亡总人数
                    </th>
                    <%--<th field="接报记录" width="100"  align='center'>接报记录</th>--%>
                    <%--<th field="事故编码" width="50" align='center' formatter='formatDetail'>详细</th>--%>
                    <th field="关联" width="50" align='center' formatter='formatRelation'>
                        关联
                    </th>
                    <th field="打印" width="50" align='center' formatter='formatPrint'>
                        打印
                    </th>
                </tr>
            </thead>
        </table>
    </div>
    <div id="tb" style="padding: 5px; height: auto; margin-top: 3px">
        <table border="0" cellspacing="0" cellpadding="0" width="100%" height="10%">
            <tr>
                <td width="10%">
                </td>
                <td width="10%" align="center">
                    起始时刻:
                </td>
                <td>
                    <input id="startTime" type="text" class="easyui-datetimebox" value="<%= ((dynamic)this.ViewData["startTime"]) %>"
                        style="width: 150px;" />
                </td>
                <td width="10%" align="center">
                    终止时刻:
                </td>
                <td>
                    <input id="endTime" type="text" class="easyui-datetimebox" value="<%= ((dynamic)this.ViewData["endTime"]) %>"
                        style="width: 150px;" />
                </td>
                <td width="10%" align="right">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" onclick="doSearch()">查询</a>
                </td>
            </tr>
            <tr>
                <td width="10%">
                </td>
                <td width="10%" align="center">
                    事故名称:
                </td>
                <td>
                    <input id="accidentName" type="text" class="easyui-validatebox" style="border: 1px solid #8DB2E3;
                        width: 146px; height: 18px" />
                </td>
                <td width="10%" align="center">
                    事故类型:
                </td>
                <td>
                    <select id="type" class="easyui-combotree" style="width: 150px;">
                    </select>
                </td>
            </tr>
            <tr>
                <td width="10%">
                </td>
                <td width="10%" align="center">
                    事发区域:
                </td>
                <td>
                    <select id="place" class="easyui-combobox" style="width: 150px;">
                    </select>
                </td>
                <td width="10%" align="center">
                    事故等级:
                </td>
                <td>
                    <select id="level" class="easyui-combobox" style="width: 150px;">
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td align="right">
                    <a href="#" id="count" class="easyui-linkbutton" onclick="Relation()"></a>
                    <input type="hidden" id="hiddenRe" value="" />
                </td>
            </tr>
        </table>
    </div>
    <div id="AccidentList" icon="icon-save" style="padding: 5px; display: none;">
        <div region="center" border="true">
            <table id="eventList" class="easyui-datagrid" align="center" style="height: 320px"
                toolbar="#detail_tool" rownumbers="true" singleselect="true" idfield="事件编码" fitcolumns="true">
                <thead>
                    <tr>
                        <th field="所属事故编码" width="60" align='center' hidden='true'>
                            所属事故编码
                        </th>
                        <th field="事件编码" width="60" align='center' hidden='true'>
                            事件编码
                        </th>
                        <th field="受理时刻" width="60" align='center'>
                            受理时刻
                        </th>
                        <th field="事件名称" width="150" align='center' formatter='formatEventName'>
                            事件名称
                        </th>
                        <th field="呼救电话" width="50" align='center'>
                            呼救电话
                        </th>
                        <th field="受理调度员" width="50" align='center'>
                            受理调度员
                        </th>
                        <th field="区域" width="50" align='center'>
                            区域
                        </th>
                        <th field="伤亡人数" width="50" align='center'>
                            伤亡人数
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
        <div id="detail_tool" style="padding: 5px; height: auto">
            <a href="#" class="easyui-linkbutton" iconcls="icon-patient" plain="true" onclick="Read('patient')">
                伤病员</a>
                 <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="true"
                    onclick="Read('event')">事件详情</a>
                     <a href="#" class="easyui-linkbutton" iconcls="icon-remove"
                        plain="true" onclick="DeleteEvent()">删除</a>
        </div>
    </div>
    <div id="AccidentPatient" icon="icon-save" style="padding: 5px; display: none;">
        <div region="center" border="true">
            <table id="patient_grid" class="easyui-datagrid" style="height: 320px" align="center"
                toolbar="#patient_tool" pagination="true" pagenumber="1" pagelist="[10, 15, 20]"
                pagesize="15" sortname="序号" sortorder="asc" singleselect="true" striped="true"
                idfield="序号" rownumbers="true" fitcolumns="true">
                <thead frozen="true">
                    <tr>
                        <th field="序号" width="60" checkbox="true" align='center'>
                            序号
                        </th>
                    </tr>
                </thead>
                <thead>
                    <tr>
                        <th field="事故编码" align='center' hidden="true">
                            事故编码
                        </th>
                        <th field="任务编码" align='center' hidden="true">
                            任务编码
                        </th>
                        <th field="车辆编码" align='center' hidden="true">
                            车辆编码
                        </th>
                        <th field="相关车辆编号" align='center' hidden="true">
                            相关车辆编号
                        </th>
                        <th field="病情编码" align='center' hidden="true">
                            病情编码
                        </th>
                        <th field="职业" align='center' hidden="true">
                            职业
                        </th>
                        <th field="单位" align='center' hidden="true">
                            单位
                        </th>
                        <th field="住址" align='center' hidden="true">
                            住址
                        </th>
                        <th field="治疗方式" align='center' hidden="true">
                            治疗方式
                        </th>
                        <th field="姓名" width="50" align='center'>
                            姓名
                        </th>
                        <th field="性别" width="50" align='center'>
                            性别
                        </th>
                        <th field="年龄" width="50" align='center'>
                            年龄
                        </th>
                        <th field="病情" width="50" align='center'>
                            病情
                        </th>
                        <th field="相关车辆" width="200" align='center'>
                            相关车辆
                        </th>
                        <th field="初步诊断" width="150" align='center'>
                            初步诊断
                        </th>
                        <th field="收治医院" width="50" align='center'>
                            收治医院
                        </th>
                        <th field="转归" width="30" align='center'>
                            转归
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
        <div id="patient_tool" style="padding: 5px; height: auto">
            <a href="#" id="patient_add" class="easyui-linkbutton" iconcls="icon-add" plain="true">
                新增</a> <a href="#" id="patient_edit" class="easyui-linkbutton" iconcls="icon-edit"
                    plain="true">修改</a> <a href="#" id="patient_del" class="easyui-linkbutton" iconcls="icon-remove"
                        plain="true">删除</a>
        </div>
    </div>
    <div id="AccidentPatientEdit" icon="icon-save" style="padding: 5px; display: none">
        <div region="center" border="true" style="margin-top: 25px">
            <form id="form" method="post" action="/MajorAccident/AccidentPatientSave/" enctype="application/x-www-form-urlencoded">
            <table>
                <tr style="display: none">
                    <td>
                        事故编码：
                    </td>
                    <td>
                        <input id="accidentId" type="text" class="easyui-validatebox" name="entity.事故编码" />
                    </td>
                    <td>
                        任务编码：
                    </td>
                    <td>
                        <input id="taskCode" type="text" class="easyui-validatebox" name="entity.任务编码" />
                    </td>
                    <td>
                        序号：
                    </td>
                    <td style="width: 1%; text-align: left;" colspan="3">
                        <input id="orderNum" type="text" class="easyui-validatebox" name="entity.序号" />
                    </td>
                </tr>
                <tr style="height: 30px;">
                    <td style="width: 10%; text-align: right">
                        伤者姓名：
                    </td>
                    <td style="width: 15%; text-align: left">
                        <input id="patient" type="text" class="easyui-validatebox" name="entity.姓名" validtype="length[0,50]"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                    <td style="width: 10%; text-align: right">
                        性别：
                    </td>
                    <td style="width: 15%; text-align: left">
                        <select id="sex" class="easyui-combobox" editable="false" name="entity.性别" style="width: 150px;">
                            <option value="男">男</option>
                            <option value="女">女</option>
                            <option value="不详">不详</option>
                        </select>
                    </td>
                    <td style="width: 10%; text-align: right">
                        年龄：
                    </td>
                    <td style="width: 3%; text-align: left">
                        <%--<input id="age" type="text" class="easyui-numberbox" name="entity.年龄" validType="length[1,11]" style="width:40px;"/>--%>
                        <input id="age" type="text" class="easyui-validatebox" name="entity.年龄" validtype="length[0,11]"
                            onkeyup="this.value=this.value.replace(/[^\d]/g,'')" style="border: 1px solid #8DB2E3;width: 41px; height: 18px" />
                    </td>
                    <td style="width: 3%; text-align: left">
                        <select id="ageUnit" class="easyui-combobox" editable="false" name="entity.年龄单位"
                            style="width: 45px;">
                            <option value="岁">岁</option>
                            <option value="月">月</option>
                            <option value="天">天</option>
                        </select>
                    </td>
                    <td style="width: 10%">
                    </td>
                </tr>
                <tr style="height: 30px;">
                    <td style="width: 10%; text-align: right">
                        病情：
                    </td>
                    <td style="width: 15%; text-align: left;">
                        <select id="illState" class="easyui-combobox" name="entity.病情编码" editable="false"
                            style="width: 150px;">
                        </select>
                    </td>
                    <td style="width: 10%; text-align: right">
                        职业：
                    </td>
                    <td style="width: 15%; text-align: left;">
                        <input id="professional" type="text" class="easyui-validatebox" name="entity.职业"
                            validtype="length[0,50]" style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                    <td style="width: 10%; text-align: right">
                        单位：
                    </td>
                    <td style="width: 5%; text-align: left;" colspan="3">
                        <input id="unit" type="text" class="easyui-validatebox" name="entity.单位" validtype="length[0,100]"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr style="height: 30px;">
                    <td style="width: 10%; text-align: right">
                        收治医院：
                    </td>
                    <td style="width: 15%; text-align: left;">
                        <input id="hospital" type="text" class="easyui-validatebox" name="entity.收治医院" validtype="length[0,100]"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                    <td style="width: 10%; text-align: right">
                        住址：
                    </td>
                    <td style="width: 15%; text-align: left;">
                        <input id="address" type="text" class="easyui-validatebox" name="entity.住址" validtype="length[0,100]"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                    <td style="width: 10%; text-align: right">
                        转归：
                    </td>
                    <td style="text-align: left;" colspan="3">
                        <input id="vest" type="text" class="easyui-validatebox" name="entity.转归" validtype="length[0,50]"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr style="height: 30px;">
                    <td style="width: 10%; text-align: right">
                        相关车辆：
                    </td>
                    <td style="width: 60%; text-align: left;" colspan="7">
                        <select id="ambulance" class="easyui-combobox" name="ambulance" editable="false"
                            style="width: 465px;" />
                    </td>
                </tr>
                <tr style="height: 30px;">
                    <td style="width: 10%; text-align: right">
                        初步诊断：
                    </td>
                    <td style="width: 60%; text-align: left;" colspan="7">
                        <input id="diagnosis" type="text" class="easyui-validatebox" name="entity.初步诊断" validtype="length[0,200]"
                            style="border: 1px solid #8DB2E3; width: 461px; height: 18px" />
                    </td>
                </tr>
                <tr style="height: 30px;">
                    <td style="width: 10%; text-align: right">
                        治疗方式：
                    </td>
                    <td style="width: 60%; text-align: left;" colspan="7">
                        <input id="treatment" type="text" class="easyui-validatebox" name="entity.治疗方式" validtype="length[1,200]"
                            style="border: 1px solid #8DB2E3; width: 461px; height: 18px" />
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    <div id="AccidentRelation" icon="icon-save" style="padding: 5px; display: none;">
        <div region="center" border="true">
            <table id="relation" class="easyui-datagrid" align="center" style="height: 320px"
                singleselect="true" idfield="事件编码" fitcolumns="true">
                <thead>
                    <tr>
                        <th field="事件编码" width="60" align='center' formatter='formatMainEvent'>
                            主事件
                        </th>
                        <th field="事故名称" width="150" align='center' formatter='formatEventName'>
                            事故名称
                        </th>
                        <th field="受理时间" width="100" align='center'>
                            受理时刻
                        </th>
                        <th field="调度员" width="50" align='center'>
                            调度员
                        </th>
                        <th field="区域" width="50" align='center'>
                            区域
                        </th>
                        <th field="事故等级" width="50" align='center'>
                            事故等级
                        </th>
                        <th field="事故类型" width="50" align='center'>
                            事故类型
                        </th>
                        <th field="所属事故编码" width="50" align='center' formatter='formatDelRelation'>
                            事故解除关联
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
    <div id="w" class="easyui-window" title="事件受理信息" minimizable="false" maximizable="false"
        icon="icon-save" style="width: 600px; height: 240px; padding: 5px; background: #fafafa;"
        closed="true">
        <div region="center" border="false" style="padding-left: 10px; background: #fff;
            border: 1px solid #ccc;">
            <table id="acc" align="center">
            </table>
        </div>
        <div region="south" border="false" style="text-align: center; height: 30px; line-height: 30px;">
            <a id="btnEp" class="easyui-linkbutton" icon="icon-ok" onclick="accinfo()">查看</a>
            <a id="btnCancel" class="easyui-linkbutton" icon="icon-undo" onclick="cancel()">取消</a>
        </div>
    </div>
</body>
</html>

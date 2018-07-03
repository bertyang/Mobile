<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edit</title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
    <script language="javascript" type="text/javascript">

        function submit() {
            $('#form').form('submit', {
                onSubmit: function () {
//                    debugger;
//                    $('action').val($('#actionTree').tree('getChecked', ['checked', 'indeterminate']));？？怎么不能用
                    var ac=[], ra=[];
                    $.each($('#actionTree').tree('getChecked', 'indeterminate'), function (i, n) {
                        ac.push(n.id.substring(2))
                    });
                    $.each($('#actionTree').tree('getChecked'), function (i, n) {
                        switch(n.id.substr(0,2))
                        {
                            case "a-":
                                ac.push(n.id.substring(2));
                                break;
                            case "r-":
                                ra.push(n.id.substring(2));
                                break;
                            default:
                                break;
                        }
//                        ccc.push(n.id)
                    });
                    $('#action').val(ac.join(","));
                    $('#range').val(ra.join(","));
//                    return false;
                    return $(this).form('validate');

                },
                success: function (msg) {
                    var data = eval('(' + msg + ')'); 
                    if(data.IsSuccess)
                    {
                        $.messager.alert('提示', data.Message, 'info', function () {
                            parent.$('#tabs').tabs('getTab', '角色与权限管理').find("iframe")[0].contentWindow.location.href =
                              parent.$('#tabs').tabs('getTab', '角色与权限管理').find("iframe")[0].contentWindow.location.href;
                            CloseCurrentTab();
                        });
                    }
                    else
                    {
                        $.messager.alert('提示', data.Message, 'info', function(){
                        });

                    }
                }
            });
        }

        $(document).ready(function () {
            //机构
            $('#actionTree').tree({
                url: '/Organize/ActionLoadTree/',
                onLoadSuccess: function (data) {
                    var ls = $(this).tree('getRoots')
                    $.each(ls, function (i, n) {
                        SetTreeCheckbox($('#actionTree'), n);
                        $('#actionTree').tree('collapseAll', n.target);
                    });
                    //$('#actionTree').combotree('setValues', [<%=ViewData["action"]%>]);
                }
            });

            <%if( ((dynamic)this.ViewData["entity"]).ID==-1) {%>
            $('#ID').val(GetID());
            <% } else {%>
            $("#ID").attr("readonly", true);
            <% }%>
        });

        function SetTreeCheckbox(tr,tn)
        {
            if(tr.tree('isLeaf',tn.target))
            {
//                debugger;
                $.each([<%=ViewData["ActionRange"]%>], function (i, n) {
                    if(tn.id=="nullid")
                    {
                        var pid=tr.tree('getParent',tn.target);
                        if(pid.id==n)
                        {
                            tr.tree('check',tn.target)
                            return false;
                        }
                    }
                    else if(n==tn.id)
                    {
                        tr.tree('check',tn.target)
                        return false;
                    }
                });
            }
            else
            {
                $.each(tr.tree('getChildren',tn.target), function (i, n) {
                    SetTreeCheckbox(tr,n);
                });
            }
        }

        function GetID() {

            var result;

            $.ajax({
                type: "POST",
                async: false,
                url: "/Organize/GetID/?tableName=B_ROLE",
                dataType: "json",
                success: function (data) {
                    result = data;
                },
                error: function () {
                    result = false;
                }
            });

            return result;
        }


        function IsExistID() {
            if (!$('#ID').attr("readonly")) {
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "/Organize/IsExistID/?tableName=B_ROLE&id=" + $('#ID').val(),
                    success: function (msg) {

                        if (msg.IsSuccess) {
                            $.messager.alert('提示', '已存此ID', 'error');
                            $('#ID').val('');
                        }
                    }
                })
            }
        }

    </script>
</head>
<body class="easyui-layout">
    <div region="center" border="false">
        <div region="north" style="margin-left:45px;margin-top:20px">
            <span class="editTitle"><%=ViewData["type"]%></span>
        </div>
        <div region="center">
            <form id="form" method="post" action="/Organize/RoleSave/" enctype="application/x-www-form-urlencoded">
            <table width="100%">
                <tr>
                    <td style="text-align: right; width: 120px;">
                        角色ID(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" id="ID" class="easyui-validatebox" 
                            required="true" validtype="length[1,11]" 
                            onkeyup="this.value=this.value.replace(/[^\d]/g,'') " 
                            onafterpaste="this.value=this.value.replace(/[^\d]/g,'') "  
                            name="entity.ID" value="<%= ((dynamic)this.ViewData["entity"]).ID  %>"
                            onblur="IsExistID()"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />   
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        角色名称(<font color="red">*</font>)：
                    </td>
                    <td>
                        <input type="text" class="easyui-validatebox" required="true" name="entity.Name"
                            validtype="length[1,20]" value="<%= ((dynamic)this.ViewData["entity"]).Name %>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;">
                        说明：
                    </td>
                    <td>
                        <input type="text" class="easyui-validatebox" name="entity.Remark" validtype="length[1,50]"
                            value="<%= ((dynamic)this.ViewData["entity"]).Remark==null?"":((dynamic)this.ViewData["entity"]).Remark%>"
                            style="border: 1px solid #8DB2E3; width: 146px; height: 18px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 120px;vertical-align:top">
                        功能项：
                        <input id="action" type="hidden" name="action" />
                        <input id="range" type="hidden" name="range" />
                    </td>
                    <td>
                        <%--<select id="action"  class="easyui-combotree"  name="action" multiple="true"  style="width:300px;">
                        </select>--%>
                         <ul id="actionTree" class="easyui-tree" data-options="checkbox:true"></ul>

                    </td>
                </tr>                
            </table>
            </form>
        </div>
    </div>
    <div region="south" border="true" style="text-align: right; height: 40px; line-height: 30px;
        background-color: #f7f7f7;">
        <table style="width: 100%">
            <tr>
                <td>
                </td>
                <td style="text-align:left">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-save" onclick="submit();">提交</a>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="CloseCurrentTab();">取消</a>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>


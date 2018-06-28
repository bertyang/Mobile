<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
</head>
<body class="easyui-layout">
      <div region="north" style="padding: 5px; width: 100%; height: 160px; text-align: left;overflow:hidden">
            <table cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td width="10%" height="30">中文名称: </td>
                    <td>
                    <input id="Text1" type="text" style="border:0;background:transparent;width:200px" value="<%= ((dynamic)this.ViewData["danger"]).中文名称 %>"/>
                    </td>
                </tr>
                <tr>
                    <td width="10%" height="30"">英文名称: </td>
                    <td>
                    <input id="Text2" type="text" style="border:0;background:transparent;width:200px"  value="<%= ((dynamic)this.ViewData["danger"]).英文名称 == null?"":((dynamic)this.ViewData["danger"]).英文名称%>" />
                    </td>
                </tr>
                <tr>
                    <td width="10%" height="30">分子式: </td>
                    <td>
                    <input id="Text3" type="text" style="border:0;background:transparent;width:200px" value="<%= ((dynamic)this.ViewData["danger"]).分子式 ==null?"":((dynamic)this.ViewData["danger"]).分子式%>"/>
                    </td>
                </tr>
                <tr>
                    <td width="10%" height="30">RTECS: </td>
                    <td>
                    <input id="Text4" type="text" style="border:0;background:transparent;width:200px" value="<%= ((dynamic)this.ViewData["danger"]).Rtecs == null?"": ((dynamic)this.ViewData["danger"]).Rtecs%>" />
                    </td>
                    <td width="10%" height="30">CAS: </td>
                    <td>
                    <input id="Text5" type="text" style="border:0;background:transparent;width:200px" value="<%= ((dynamic)this.ViewData["danger"]).Cas == null?"":((dynamic)this.ViewData["danger"]).Cas %>"/>
                    </td>
                    <td width="10%" height="30"">UN: </td>
                    <td>
                    <input id="Text9" type="text" style="border:0;background:transparent;width:200px"  value="<%= ((dynamic)this.ViewData["danger"]).Un == null?"":((dynamic)this.ViewData["danger"]).Un%>"/>
                    </td>
                </tr>
                <tr>
                    <td width="10%" height="30">序号: </td>
                    <td>
                    <input id="Text6" type="text" style="border:0;background:transparent;width:200px" value="<%= ((dynamic)this.ViewData["danger"]).序号 %>"/>
                    </td>
                    <td width="10%" height="30">危险号: </td>
                    <td>
                    <input id="Text7" type="text" style="border:0;background:transparent;width:200px" value="<%= ((dynamic)this.ViewData["danger"]).危险号 == null?"":((dynamic)this.ViewData["danger"]).危险号 %>" />
                    </td>
                    <td width="10%" height="30">常用标志: </td>
                    <td>
                    <input id="Text8" type="text" style="border:0;background:transparent;width:200px"  value="<%= ((dynamic)this.ViewData["danger"]).常用标志 == null?"":((dynamic)this.ViewData["danger"]).常用标志%>" />
                    </td>
                </tr>  
           </table>  
           </div> 
      <div id="tabs" region="center" class="easyui-tabs" fit="true" border="false">
          <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["Character"])){ %>
              <div id="Character"  title="性状" style="padding: 5px;" closable="false">
                 <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;border: 0; margin-top: 10px"><%= (this.ViewData["Character"])%></textarea>
              </div>
          <% } %>
          <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["Taboo"])){ %>
              <div id="Taboo" title="禁忌" style="padding: 5px;" closable="false">
                 <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%; border: 0; margin-top: 10px"><%= (this.ViewData["Taboo"])%></textarea>
              </div>
          <% } %>
          <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["Synonym"])){ %>
              <div id="Synonym" title="同义词" style="padding: 5px;" closable="false">
                 <textarea rows="100" cols="100" style="font-size: 14px; width: 100%;
                     height: 100%; border: 0; margin-top: 10px"><%= (this.ViewData["Synonym"])%></textarea>
              </div>
          <% } %>
          <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["Fatalness"])){ %>
              <div id="Fatalness" title="危险性" style="padding: 5px;" closable="false">
                 <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                     border: 0; margin-top: 10px"><%= (this.ViewData["Fatalness"])%></textarea>
              </div>
          <% } %>
          <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["FirstAidMeasure"])){ %>
              <div id="FirstAidMeasure" title="急救措施" style="padding: 5px;" closable="false">
                 <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                     border: 0; margin-top: 10px"><%= (this.ViewData["FirstAidMeasure"])%></textarea>
              </div>
          <% } %>
          <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["PreventMeasure"])){ %>
              <div id="PreventMeasure" title="救护措施" style="padding: 5px;" closable="false">
                 <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                     border: 0; margin-top: 10px"><%= (this.ViewData["PreventMeasure"])%></textarea>
              </div>
          <% } %>
          <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["Store"])){ %>
              <div id="Store" title="存储" style="padding: 5px;" closable="false">
                 <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                     border: 0; margin-top: 10px"><%= (this.ViewData["Store"])%></textarea>
              </div>
          <% } %>
          <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["LeakOutDisposal"])){ %>
              <div id="LeakOutDisposal" title="泄露处理" style="padding: 5px;" closable="false">
                 <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                     border: 0; margin-top: 10px"><%= (this.ViewData["LeakOutDisposal"])%></textarea>
             </div>
          <% } %>
          <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["TransportDemand"])){ %>
              <div id="TransportDemand" title="运输要求" style="padding: 5px;" closable="false">
                 <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                     border: 0; margin-top: 10px"><%= (this.ViewData["TransportDemand"])%></textarea>
             </div>
          <% } %>
          <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["Postscript"])){ %>
              <div id="Postscript" title="附注" style="padding: 5px;" closable="false">
                <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                    border: 0; margin-top: 10px"><%= (this.ViewData["Postscript"])%></textarea>
              </div>
          <% } %>
          <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["LimitAndMensurateInAir"])){ %>
              <div id="LimitAndMensurateInAir" title="空气中的允许极限及测定" style="padding: 5px;" closable="false">
                 <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                     border: 0; margin-top: 10px"><%= (this.ViewData["LimitAndMensurateInAir"])%></textarea>
             </div>
          <% } %>
          <% if ((dynamic)this.ViewData["LimitAndMensurateInWater"] != null){ %>
              <div id="LimitAndMensurateInWater" title="水中的允许极限及测定" style="padding: 5px;" closable="false">
                 <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                     border: 0; margin-top: 10px"><%= (this.ViewData["LimitAndMensurateInWater"])%></textarea>
             </div>
          <% } %>
      </div>
</body>
</html>




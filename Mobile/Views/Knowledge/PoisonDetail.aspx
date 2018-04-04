<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <%: Styles.Render( "~/Content/css") %>
    <%: Scripts.Render("~/bundles/js")%>
</head>
<body class="easyui-layout">
      <div region="north" style="padding: 5px; width: 100%; height: 160px; overflow: hidden;text-align: left;">    
            <table cellspacing="0" cellpadding="0" width="100%" style="margin-top: 10px">
                <tr>
                    <td width="10%" height="30">中文名称: </td>
                    <td>
                    <input id="Text1" type="text" style="border:0;background:transparent; width:300px" value="<%= ((dynamic)this.ViewData["poison"]).中文名称 %>"/>
                    </td>
                </tr>
                <tr>
                    <td width="10%" height="30"">英文名称: </td>
                    <td>
                    <input id="Text2" type="text" style="border:0;background:transparent;width:300px"  value="<%= ((dynamic)this.ViewData["poison"]).英文名称 == null?"": ((dynamic)this.ViewData["poison"]).英文名称%>"/>
                    </td>
                </tr>
                <tr>
                    <td width="10%" height="30">别名: </td>
                    <td>
                    <input id="Text3" type="text" style="border:0;background:transparent;width:300px" value="<%= ((dynamic)this.ViewData["poison"]).别名 == null?"":((dynamic)this.ViewData["poison"]).别名 %>"/>
                    </td>
                </tr>
           </table>  
      </div> 
      <div id="tabs" region="center" class="easyui-tabs" fit="true" border="false">
        <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["Character"])){ %>
            <div id="Character" title="性质" style="padding: 5px;" closable="false">
                <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                    border: 0; margin-top: 10px"><%= (this.ViewData["Character"])%></textarea>
            </div>
        <% } %>
        <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["Fatalness"])){ %>
            <div id="Fatalness" title="毒性" style="padding: 5px;" closable="false">
                <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                    border: 0; margin-top: 10px"><%= (this.ViewData["Fatalness"])%></textarea>
            </div>
        <% } %>
        <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["Characteristic"])){ %>
              <div id="Characteristic" title="特点" style="padding: 5px;" closable="false">
                  <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                      border: 0; margin-top: 10px"><%= (this.ViewData["Characteristic"])%></textarea>
              </div>
        <% } %>
        <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["ToxicEffect"])){ %>
              <div id="ToxicEffect" title="毒理作用" style="padding: 5px;" closable="false">
                  <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                      border: 0; margin-top: 10px"><%= (this.ViewData["ToxicEffect"])%></textarea>
              </div>
        <% } %>
        <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["PoisoningManifestation"])){ %>
              <div id="PoisoningManifestation" title="中毒表现" style="padding: 5px;" closable="false">
                  <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                      border: 0; margin-top: 10px"><%= (this.ViewData["PoisoningManifestation"])%></textarea>
              </div>
        <% } %>
        <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["DiagnoseMainPoint"])){ %>
              <div id="DiagnoseMainPoint" title="毒性" style="padding: 5px;" closable="false">
                  <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                      border: 0; margin-top: 10px"><%= (this.ViewData["DiagnoseMainPoint"])%></textarea>
              </div>
        <% } %>
        <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["CureMainPoint"])){ %>
              <div id="CureMainPoint" title="救治要点" style="padding: 5px;" closable="false">
                  <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                      border: 0; margin-top: 10px"><%= (this.ViewData["CureMainPoint"])%></textarea>
              </div>
        <% } %>
        <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["Toxicology"])){ %>
              <div id="Toxicology" title="毒理" style="padding: 5px;" closable="false">
                  <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                      border: 0; margin-top: 10px"><%= (this.ViewData["Toxicology"])%></textarea>
              </div>
        <% } %>
        <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["Pharmacokinetic"])){ %>
              <div id="Pharmacokinetic" title="药动学" style="padding: 5px;" closable="false">
                  <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                      border: 0; margin-top: 10px"><%= (this.ViewData["Pharmacokinetic"])%></textarea>
              </div>
        <% } %>
        <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["LabExamine"]))
           { %>
              <div id="LabExamine" title="实验室检查" style="padding: 5px;" closable="false">
                  <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                      border: 0; margin-top: 10px"><%= (this.ViewData["LabExamine"])%></textarea>
              </div>
        <% } %>
        <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["Pharmacology"])){ %>
              <div id="Pharmacology" title="药理" style="padding: 5px;" closable="false">
                  <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                      border: 0; margin-top: 10px"><%= (this.ViewData["Pharmacology"])%></textarea>
              </div>
        <% } %>
        <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["Etiology"])){ %>
                <div id="Etiology" title="病原学" style="padding: 5px;" closable="false">
                    <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                        border: 0; margin-top: 10px"><%= (this.ViewData["Etiology"])%></textarea>
                </div>
        <% } %>
        <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["Epidemiology"])){ %>
              <div id="Epidemiology" title="流行病学" style="padding: 5px;" closable="false">
                  <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                      border: 0; margin-top: 10px"><%= (this.ViewData["Epidemiology"])%></textarea>
              </div>
        <% } %>
        <% if (!string.IsNullOrEmpty((dynamic)this.ViewData["Remark"])){ %>
              <div id="Remark" title="备注" style="padding: 5px;" closable="false">
                  <textarea rows="100" cols="100" style="font-size: 14px; width: 100%; height: 100%;
                      border: 0; margin-top: 10px"><%= (this.ViewData["Remark"])%></textarea>
              </div>
        <% } %>
     </div>
</body>
</html>




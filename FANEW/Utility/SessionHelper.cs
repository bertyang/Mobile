using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.SessionState;
using System.Web;

namespace Anchor.FA.Utility
{
    public class SessionHelper
    {
        private static readonly string m_AppDomainVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
        /// <summary>
        /// Session 是否是
        /// </summary>
        /// <param name="response"></param>
        /// <param name="sessionState"></param>
        public static void ValidateLogin(HttpResponse response, HttpSessionState sessionState)
        {
            if (sessionState["Purview"] == null)
            {
                response.Redirect("" + m_AppDomainVirtualPath + "/Login.aspx", true);
                //response.Write("<SCRIPT LANGUAGE='JavaScript'>window.open(\"" + m_AppDomainVirtualPath + "/Login.aspx\",\"_parnet\");window.location=\"" + m_AppDomainVirtualPath + "/TimeOut.aspx\";</SCRIPT>");
                response.End();
            }
        }
    }
}

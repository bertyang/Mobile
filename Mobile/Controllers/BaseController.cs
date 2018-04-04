#region 程序集 System.Web.Mvc.dll, v4.0.30319
// D:\My Document\Documents\Visual Studio 2010\Projects\FA\packages\Microsoft.AspNet.Mvc.4.0.20505.0\lib\net40\System.Web.Mvc.dll
#endregion

using System;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using System.Web.Profile;
using System.Web.Routing;
using System.Configuration;

using Anchor.FA.Utility;
using Anchor.FA.Model;
using Anchor.FA.BLL.IBLL;
using Spring.Context;
using Spring.Context.Support;

namespace Anchor.FA.Mobile.Controllers
{
    public class BaseController:Controller
    {
        private B_WORKER m_CurrentUserInfo = null;
        private C_WorkerDetail m_UserInfo = null;

        protected static IApplicationContext ctx = ContextRegistry.GetContext();

        protected override void OnAuthorization(
            AuthorizationContext filterContext
        )
        {
            if (!User.Identity.IsAuthenticated)
            {  
                filterContext.Result = new RedirectToRouteResult("default",
                    new RouteValueDictionary(new { controller = "Shared", action = "AuthorError" }));
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (bool.Parse(ConfigurationManager.AppSettings["CustemErrorHandle"]))
            {
                // 标记异常已处理
                filterContext.ExceptionHandled = true;

                //记录错误日志
                Log4Net.LogError("Web", filterContext.Exception.ToString());

                // 跳转到错误页
                filterContext.Result = new RedirectResult(Url.Action("SystemError", "Shared"));
            }
        }

        private C_WorkerDetail GetUserInfo()
        {
            BLL.Organize.Worker worker = new BLL.Organize.Worker();

            return worker.GetWorkerDetailById(int.Parse(User.Identity.Name.Split('|')[0]));
        }

        public C_WorkerDetail UserInfo
        {
            get
            {
                if (m_UserInfo == null)
                {
                    m_UserInfo = this.GetUserInfo();
                }
                return m_UserInfo;
            }
        }

        private B_WORKER GetCurrentUser()
        {
            BLL.Organize.Worker worker = new BLL.Organize.Worker();

            return worker.GetWorkerById(int.Parse(User.Identity.Name.Split('|')[0]));
        }
        public B_WORKER CurrentUser
        { 
            get
            {
                if (m_CurrentUserInfo == null)
                {
                    m_CurrentUserInfo = this.GetCurrentUser();
                }
                return m_CurrentUserInfo;
            }
        }

        public int CurrentUserID
        {
            get
            {
                if (m_CurrentUserInfo == null)
                {
                    m_CurrentUserInfo = this.GetCurrentUser();
                }
                return m_CurrentUserInfo.ID;
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public ActionResult DownLoad(string fileName)
        {
            string path = Server.MapPath("/Template/");

            return new DownloadResult { VirtualPath = path + fileName, FileDownloadName = fileName };
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anchor.FA.Model;
using Anchor.FA.BLL.IBLL;
using Anchor.FA.Utility;
using Anchor.FA.BLL.WorkFlow;
using System.IO;

namespace Anchor.FA.Mobile.Controllers
{
    public class HomeController : BaseController
    {
        /// <summary>
        /// 首页刷新时间间隔(秒)
        /// </summary>
        private static readonly int RefreshInterval = AppConfig.GetInt32ConfigValue("RefreshInterval")*1000;

        /// <summary>
        /// 登录密码正则表达式
        /// </summary>
        private static readonly string CodeRule = AppConfig.GetStringConfigValue("CodeRule");


        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to kick-start your ASP.NET MVC application.";

            //throw new Exception("test");
            int loginID = int.Parse(User.Identity.Name.Split('|')[0]);

            ViewData["accoutid"] = loginID;
            ViewData["accoutname"] = User.Identity.Name.Split('|')[1];
            ViewData["DateTime"] = DateTime.Now.Year;
            ViewData["Version"] = AppConfig.Version;

            //未读邮件数量
            BLL.Email.Email email = new BLL.Email.Email();
            ViewData["Email"] = email.GetEmailCount(loginID, 1);

            //待办工作数量
            Flow flow = new Flow();
            ViewData["Approve"] = flow.ApproveListCount(loginID);

            //待办工作数量
            ViewData["Notify"] = flow.NotifyListCount(loginID);

            //密码规则
            ViewData["CodeRule"] = CodeRule;

            //首页刷新时间间隔
            ViewData["RefreshInterval"] = RefreshInterval;

            return View();
        }

        public ActionResult GetAccountID()
        {
            int accountId = int.Parse(User.Identity.Name.Split('|')[0]);

            return Json(new { Message = accountId }, "text/html", JsonRequestBehavior.AllowGet);
        }

        public ActionResult refreshEmail()  
        {
            int loginID = int.Parse(User.Identity.Name.Split('|')[0]);

            //未读邮件数量
            BLL.Email.Email email = new BLL.Email.Email();
            int e_count = email.GetEmailCount(loginID, 1);

            return Json(new {Message = e_count }, "text/html", JsonRequestBehavior.AllowGet);
        }

        public ActionResult refreshApprove() 
        {
            int loginID = int.Parse(User.Identity.Name.Split('|')[0]);

            //待办工作数量
            Flow flow = new Flow();
            int a_count = flow.ApproveListCount(loginID);

            return Json(new { Message = a_count }, "text/html", JsonRequestBehavior.AllowGet); 
        }

        public ActionResult ChangePassword()
        {
            int loginID = int.Parse(User.Identity.Name.Split('|')[0]);

            ViewData["accoutid"] = loginID;

            //密码规则
            ViewData["CodeRule"] = AppConfig.GetStringConfigValue("CodeRule");

            return View();
        }

        public ActionResult refreshNotify()
        {
            int loginID = int.Parse(User.Identity.Name.Split('|')[0]);

            Flow flow = new Flow();
            int count =  flow.NotifyListCount(loginID);

            return Json(new { Message = count }, "text/html", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMenu()
        {
            try
            {
                BLL.Organize.Action action = new BLL.Organize.Action();
                string userId = User.Identity.Name.Split('|')[0];
                Dictionary<string, object> dicts = new Dictionary<string, object>();
                dicts.Add("menus", action.GetMenu("0", userId));
                return this.Json(dicts);
            }
            catch (Exception e)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("InfoID", "0");
                dict.Add("InfoMessage", e.Message);
                return this.Json(dict);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            var fileName = System.IO.Path.GetFileName(upload.FileName);
            var filePhysicalPath = Server.MapPath("~/upload/" + fileName);//我把它保存在网站根目录的 upload 文件夹
            
            string defautltPath = System.Web.HttpContext.Current.Server.MapPath("~/Upload/");

            if (!Directory.Exists(defautltPath))
            {
                Directory.CreateDirectory(defautltPath);
            }

            upload.SaveAs(filePhysicalPath);

            var url = "/upload/" + fileName;
            var CKEditorFuncNum = System.Web.HttpContext.Current.Request["CKEditorFuncNum"];

            //上传成功后，我们还需要通过以下的一个脚本把图片返回到第一个tab选项
            return Content("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
        }
        public ActionResult Report()
        {
            return View();
        }
    }
}

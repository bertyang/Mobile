using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;

using Anchor.FA.Model;
using Anchor.FA.BLL.IBLL;
using Anchor.FA.BLL.Organize;
using Anchor.FA.Utility;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace Anchor.FA.Web.Controllers
{

    [Authorize]
    public class AccountController : BaseController
    {
        /// <summary>
        /// 最大登录失败次数
        /// </summary>
        private static readonly int FailedLogin =AppConfig.GetInt32ConfigValue("FailedLogin");

        /// <summary>
        /// 登录失败后，禁止登录时间
        /// </summary>
        private static readonly int NoLoginTime = AppConfig.GetInt32ConfigValue("NoLoginTime");

        /// <summary>
        /// 登录密码正则表达式
        /// </summary>
        private static readonly string CodeRule = AppConfig.GetStringConfigValue("CodeRule");

        /// <summary>
        /// 登录失败次数
        /// </summary>
        public int FailCount
        {
            get 
            {
                string failCount = CookieHelper.GetCookieValue("FailCount");

                if (string.IsNullOrEmpty(failCount))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(failCount);
                }
            }
            set {

                if (value >= FailedLogin)
                {
                    CookieHelper.SetCookie("FailCount", value.ToString(), DateTime.Now.AddMinutes(NoLoginTime));
                }
                else
                {
                    CookieHelper.SetCookie("FailCount", value.ToString());
                }
            }
        }

        /// <summary>
        /// 重写OnAuthorization
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            //留空，避免登陆页面超时(误删)
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            //密码规则
            ViewData["CodeRule"] = CodeRule;

            return View();
        }
        /// <summary>
        /// 调度台或者回访程序登录
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult DeskLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            ViewData["Version"] = AppConfig.Version;

            //从调度台登陆
            string name = Request.QueryString["Name"];//登录名或者TPerson人员编码
            string pwd = Request.QueryString["passWord"];//密码

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(pwd))
            {
                BLL.Organize.Worker bllWorker = new BLL.Organize.Worker();

                B_WORKER worker = bllWorker.DeskLogin(name, pwd);

                if (worker != null)
                {
                    FormsAuthentication.SetAuthCookie(worker.ID.ToString() + "|" + worker.Name + "|" + name, true);

                    if(Regex.IsMatch(pwd,  CodeRule.Replace("/","")))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("ChangePassword", "Home");
                    }                   
                }
            }

            return View();

        }

        //
        // POST: /Account/Login
        [ValidateInput(false)]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string userName, string passWord,string validateCode)
        {
            BLL.Organize.Worker bllWorker = new BLL.Organize.Worker();
            BLL.Organize.Action action = new BLL.Organize.Action();
             
            if (ModelState.IsValid)
            {
                //检查登录失败次数
                if (FailCount >= FailedLogin)
                {
                    return Json(new { IsSuccess = false, Message = string.Format("您登录失败{0}次,{1}分钟内不能再登陆!", 
                        FailedLogin,
                        NoLoginTime) }, 
                        "text/html", JsonRequestBehavior.AllowGet);
                }

                //检查验证码
                if(Session["ValidateCode"] !=null && validateCode != Session["ValidateCode"].ToString())
                {
                    FailCount++;
                    return Json(new { IsSuccess = false, Message = "验证码错误！" }, "text/html", JsonRequestBehavior.AllowGet);
                }

                //B_WORKER worker = bllWorker.Login(HttpUtility.UrlDecode(userName), HttpUtility.UrlDecode(passWord));
                B_WORKER worker = bllWorker.Login(userName, passWord);
               
                if (worker != null)
                {
                    if (FailCount > 0) FailCount = 0;

                    //判断登陆者有无权限
                    List<C_MENU_TREE> isAuth = action.GetMenu("0", worker.ID.ToString());
                    if (isAuth == null)
                    {
                        return Json(new { IsSuccess = false, Message = "您没有任何权限，请联系管理员！" });
                    }

                    //判断是否是Internet访问并且允许此人Internet访问
                    //bool isInternetAccess = Convert.ToBoolean(ConfigurationManager.AppSettings["IsInternetAccess"]);

                    //if (isInternetAccess && worker.IsAllowInternetAccess.ToUpper() == "N")
                    //{
                    //    return Json(new { IsSuccess = false, Message = "您没有Internet访问权限，请联系管理员！" }, "text/html", JsonRequestBehavior.AllowGet);
                    //}

                    //写登录日志
                    BLL.Organize.Worker w = new BLL.Organize.Worker();

                    B_LOGIN_LOG log = new B_LOGIN_LOG();
                    log.Name = userName;
                    log.IP = Request.UserHostAddress;
                    log.LoginTime = DateTime.Now;

                    w.LoginLog(log);

                    //登录成功，写cookie
                    FormsAuthentication.SetAuthCookie(worker.ID.ToString() + "|" + worker.Name + "|" + userName, true);

                    Session.Remove("ValidateCode");

                    return Json(new { IsSuccess = true, Message = "登录成功" }, "text/html", JsonRequestBehavior.AllowGet);

                }
                else
                {
                    FailCount++;
                    return Json(new { IsSuccess = false, Message = "账号或密码错误，请联系管理员！" }, "text/html", JsonRequestBehavior.AllowGet);
                }
            }

            return View();
        }


        //
        // GET: /Account/LogOff
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }
     
        [HttpPost]
        public ActionResult ChangePassword(int workerId,string newPassWord)
        {
            BLL.Organize.Worker worker = new BLL.Organize.Worker();
    
            bool reslut;

            try
            {
                reslut = worker.UpdatePassword(workerId, newPassWord);
            }
            catch (Exception)
            {
                reslut = false;
            }

            if (reslut)
            {
                return Json(new { IsSuccess = true, Message = "删除成功" });
            }
            else
            {
                return Json(new { IsSuccess = false, Message = "删除失败" });
            }

        }

        public ActionResult GetEmpNoAndPassWord()
        {
            string workerId = User.Identity.Name.Split('|')[0];
            string empNo = string.Empty;
            string passWord = string.Empty;
            Worker worker = new Worker();

            int result;

            if (int.TryParse(User.Identity.Name.Split('|')[2], out result))
            {
                string loginName = User.Identity.Name.Split('|')[2];

                TPerson person = worker.GetTPerson(int.Parse(workerId), loginName);

                if (person != null)
                {
                    empNo = person.工号;
                    passWord = person.口令;
                }
            }
            else
            {
                List<B_WORKER_ROLE> listWorkerRole = worker.GetWorkerRole(int.Parse(workerId));

                B_WORKER_ROLE wr = listWorkerRole.FirstOrDefault(t => !string.IsNullOrEmpty(t.EmpNo));

                if (wr != null)
                {
                    empNo = wr.EmpNo;
                }

                if (!string.IsNullOrEmpty(empNo))
                {
                    TPerson person = worker.GetTPersonByEmpNo(empNo);

                    if (person != null)
                    {
                        passWord = person.口令;
                    }
                }
            }

            return Json(new { EmpNo = empNo, PassWord = passWord });
        }


        [AllowAnonymous]
        /// <summary>
        /// 验证码的校验
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public ActionResult CheckCode()
        {
            //生成验证码
            ValidateCode validateCode = new ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = validateCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Anchor.FA.BLL.IBLL;
using Anchor.FA.Model;
using Anchor.FA.BLL.BasicInfo;

namespace Anchor.FA.Mobile.Controllers
{
    public class BackController : BaseController
    {
        //
        // GET: /Back/

        public ActionResult BackCall()
        {
            string beginDate = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HH:mm:ss");

            this.ViewData["BeginDate"] = beginDate;
            this.ViewData["EndDate"] = endDate;
            this.ViewData["Time"] = "00:00:00";

            return View();
        }

        public ActionResult Query(int page, int rows, string order, string sort, DateTime start, DateTime end,string type)
        {
            try
            {
                BLL.Notice.Back back = new BLL.Notice.Back();
                object st = back.GetTelBackCallsV7(page, rows, order, sort, start.ToString(), end.ToString(), type);

                return Json(st);
            }
            catch (Exception e)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("InfoID", "0");
                dict.Add("InfoMessage", e.Message);
                return this.Json(dict);
            }
        }

        public ActionResult GetBackSM(int? code)
        {
            if (code == null)
            {
                return Json(new { Result = false });
            }

            BLL.Notice.Back back = new BLL.Notice.Back();

            TBackCallSM result = back.GetBackSM((int)code);

            if ( result != null )
            {
                return Json(new {Result=true, Send = result.发送内容, Accept = result.接收内容 });
            }
            else
            {
                return Json(new { Result = false });
            }
        }

        public ActionResult GetBackCall(string taskCode)
        {
            BLL.Notice.Back back = new BLL.Notice.Back();

            TBackCall result = back.GetBackCall(taskCode);

            if (result != null)
            {
                return Json(new {Result=true, 
                    Doctor = result.医生, 
                    Nurse = result.护士,
                    Driver = result.司机,
                    Dispatcher = result.调度,
                    Stretcher = result.担架, 
                    Remark = result.备注,
                    Reason = result.回访结果 });
            }
            else
            {
                return Json(new
                {
                    Result = false
                });
            }
        }


        public ActionResult GetPersonList(string taskCode)
        {
            List<TTaskPersonLink> taskPersonLs = TaskPersonLink.GetTaskPersons(taskCode);

            TTaskPersonLink LtDriver = taskPersonLs.Where(t => t.人员类型编码 == 3).FirstOrDefault();//司机
            TTaskPersonLink LtDoctor = taskPersonLs.Where(t => t.人员类型编码 == 4).FirstOrDefault();//医生
            TTaskPersonLink LtNures = taskPersonLs.Where(t => t.人员类型编码 == 5).FirstOrDefault();//护士

            BLL.Notice.Back back = new BLL.Notice.Back();

            string firstDispatcher = back.GetFirstDispatcher(taskCode);

            if (taskPersonLs.Count > 0)
            {
                return Json(new
                {
                    Result = true,
                    Doctor = LtDoctor == null ? "无" : LtDoctor.姓名,
                    Nurse = LtNures == null ? "无" : LtNures.姓名,
                    Driver = LtDriver == null ? "无" : LtDriver.姓名,
                    Dispatcher = firstDispatcher == null ? "无" : firstDispatcher,
                });
            }
            else
            {
                return Json(new
                {
                    Result = false
                });
            }
        }

        public ActionResult SaveSMS()
        {

            if (ModelState.IsValid)
            {
                bool save=true;

                try
                {
                    BLL.Notice.Back back = new BLL.Notice.Back();

                    TBackCallSM sms = new TBackCallSM();
                    sms.接收内容 = Request.Form["SmsReply"];
                    sms.编码 = int.Parse(Request.Form["Code"]);

                    back.SaveBackSM(sms);
                }
                catch (Exception)
                {

                    save = false;
                }

                if (save)
                {
                    return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = "保存失败" }, "text/html", JsonRequestBehavior.AllowGet);
                }
            }

            return View();

        }

        public ActionResult SaveCall()
        {
            if (ModelState.IsValid)
            {
                bool save = true;

                try
                {
                    BLL.Notice.Back back = new BLL.Notice.Back();

                    TBackCall call = new TBackCall();
                    call.任务编码 = Request.Form["TaskCode"];
                    call.司机 = Request.Form["driver"] == "on" ? 1:0;
                    call.医生 = Request.Form["doctor"] == "on" ? 1:0;
                    call.护士 = Request.Form["nurse"] == "on" ? 1:0;
                    call.调度 = Request.Form["dispatcher"] == "on" ? 1 : 0;
                    call.担架 = Request.Form["stretcher"] == "on" ? 1 : 0;
                    call.回访结果 = Request.Form["reason"];
                    call.备注 = Request.Form["remark"];
                    call.是否有效 = "有效";
                    call.回访保存时间 = DateTime.Now;

                    back.SaveBackCall(call);
                }
                catch (Exception)
                {

                    save = false;
                }

                if (save)
                {
                    return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = "保存失败" }, "text/html", JsonRequestBehavior.AllowGet);
                }
            }

            return View();

        }
    }
}

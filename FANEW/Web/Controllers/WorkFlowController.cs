using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

using Anchor.FA.BLL.IBLL;
using Anchor.FA.BLL.WorkFlow.Common;
using Anchor.FA.Model;
using Anchor.FA.BLL.WorkFlow;
using Anchor.FA.Utility;

namespace Anchor.FA.Web.Controllers
{
    public class WorkFlowController : BaseController
    {
        private static readonly int RefreshInterval = AppConfig.GetInt32ConfigValue("RefreshInterval") * 1000;
        /// <summary>
        /// 申请表单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ApplyList()
        {
            Flow flow = new Flow();

            IList<F_FLOW> listFlow = flow.GetAllFlow("inner");

            return View(listFlow);
        }

        public ActionResult FlowList()
        {
            Flow flow = new Flow();

            IList<F_FLOW> listFlow = flow.GetAllFlow("All");

            return Json(listFlow);
        }

        /// <summary>
        /// 签核列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ApproveList()
        {
            ViewData["RefreshInterval"] = RefreshInterval;

            return View();
        }

        public ActionResult TaskList(int flowInstId)
        {
            Flow flow = new Flow();

            List<C_TASK_LIST> result = flow.TaskList(flowInstId);

            return Json(result);
        }


        /// <summary>
        /// 申请表单
        /// </summary>
        /// <returns></returns>
        public ActionResult Apply()
        {
            ViewData["url"] = string.Format("/FLOW{0}/Page/A/?{1}", Request.QueryString["flowId"], Request.QueryString);

            //重新送出的加上签核列表
            //if (!string.IsNullOrEmpty(Request.QueryString["flowInstId"]))
            //{
            //    ViewData["TaskListUrl"] = string.Format("/WorkFlow/TaskList/?flowInstId={0}", Request.QueryString["flowInstId"]);
            //}

            return View();
        }

        /// <summary>
        /// 签核表单
        /// </summary>
        /// <param name="workItemInstId">工作实例ID</param>
        /// <returns></returns>
        public ActionResult Approve()
        {
            string flowInstId = Request.QueryString["flowInstId"];
            string workItemInstId = Request.QueryString["workItemInstId"];
            string flowId = Request.QueryString["flowId"];

            Engine engine = new Engine();

            ViewData["IsFinalTask"] = engine.CheckIsFinalTask(int.Parse(workItemInstId));

            //ViewData["TaskListUrl"] = string.Format("/WorkFlow/TaskList/?flowInstId={0}", flowInstId);

            if (!string.IsNullOrEmpty(Request.QueryString["Url"]) && Request.QueryString["Url"]!="null")
            {
                if (Request.QueryString["Url"].Contains("?"))
                {
                    ViewData["Url"] = Request.QueryString["Url"].TrimEnd('&') + "&" + Request.QueryString; 
                }
                else
                {
                    ViewData["Url"] = Request.QueryString["Url"].TrimEnd('/') + "/?" + Request.QueryString;
                }
            }
            else
            {
                ViewData["Url"] = string.Format("/FLOW{0}/Page/V/?{1}", flowId, Request.QueryString);
            }

            ViewData["Agree"] = WorkItemAppValue.Agree;
            ViewData["Reject"] = WorkItemAppValue.Reject;
            ViewData["Return"] = WorkItemAppValue.Return;
            ViewData["Reserve"] = WorkItemAppValue.Reserve;

            return View();
        }


        public ActionResult TraceView()
        {
            string state = Request.QueryString["state"];
            string url = Request.QueryString["url"];
            string flowNo = Request.QueryString["flowNo"];
            string flowId = Request.QueryString["flowId"];
            string flowInstId = Request.QueryString["flowInstId"];
            string isInner = Request.QueryString["isInner"];
            string isApply = Request.QueryString["isApply"];


            if (!string.IsNullOrEmpty(url) && url!="null")//外部表单显示
            {
                //ViewData["ViewUrl"] = url.TrimEnd('/') + "/?flowNo=" + flowNo;

                if (url.Contains("?"))
                {
                    ViewData["ViewUrl"] = url.TrimEnd('&') + "&" + Request.QueryString;
                }
                else
                {
                    ViewData["ViewUrl"] = url.TrimEnd('/') + "/?" + Request.QueryString;
                }
            }
            else//内部表单显示
            {
                ViewData["ViewUrl"] = string.Format("/FLOW{0}/Page/V/?{1}", flowId, Request.QueryString);
            }


            if (isInner == "Y" && state == "A")//撤回按钮显示 
            {
                ViewData["RecallButton"] = "true";
            }

            ViewData["IsApply"] = isApply;

            //ViewData["TaskListUrl"] = string.Format("/WorkFlow/TaskList/?flowInstId={0}", flowInstId);

            return View();
        }



        /// <summary>
        /// 申请表单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult TraceApply()
        {
            return View();
        }

        /// <summary>
        /// 申请表单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult TraceApprove()
        {
            return View();
        }

        /// <summary>
        /// 申请表单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult NotifyList()
        {
            this.ViewData["workerID"] = int.Parse(User.Identity.Name.Split('|')[0]);

            return View();
        }

        /// <summary>
        /// 监控流程
        /// </summary>
        /// <returns></returns>
        public ActionResult Monitor()
        {
            return View();
        }

        /// <summary>
        /// 签核列表查询
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="rows">每页行数<;/param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序/降序</param>
        /// <param name="flowType">流程类型</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="flowNo">流程号</param>
        /// <returns></returns>
        public ActionResult ApproveSearch(int page, int rows, string order, string sort, string flowType, DateTime? startTime, DateTime endTime, string flowNo)
        {
            Flow flow = new Flow();

            var result = flow.ApproveList(page, rows, order, sort, flowType,
                startTime == null ? (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue : (DateTime)startTime, 
                endTime, 
                flowNo, 
                CurrentUser.ID);

            return Json(result);

        }

        public ActionResult TraceApplySearch(int page, int rows, string order, string sort, string flowType, DateTime startTime, DateTime endTime, string flowNo)
        {
            Flow flow = new Flow();

            var result = flow.TraceApply(page, rows, order, sort, flowType, startTime, endTime, flowNo, CurrentUser.ID);

            return Json(result);

        }

        public ActionResult MonitorFlow(int page, int rows, string order, string sort, string flowType, DateTime startTime, DateTime endTime, string flowNo)
        {
            Flow flow = new Flow();

            var result = flow.Monitor(page, rows, order, sort, flowType, startTime, endTime, flowNo);

            return Json(result);

        }

        public ActionResult TraceApproveSearch(int page, int rows, string order, string sort, string flowType, DateTime startTime, DateTime endTime, string flowNo)
        {
            Flow flow = new Flow();

            var result = flow.TraceApprove(page, rows, order, sort, flowType, startTime, endTime, flowNo, CurrentUser.ID);

            return Json(result);

        }

        public ActionResult TraceNotifySearch(int page, int rows, string order, string sort, string flowType, DateTime startTime, DateTime endTime, string flowNo)
        {
            Flow flow = new Flow();

            var result = flow.TraceNotify(page, rows, order, sort, flowType, startTime, endTime, flowNo, CurrentUser.ID);

            return Json(result);

        }


        public ActionResult GetNextActivitys(int activityInstId)
        {
            Engine engine = new Engine();

            List<F_ACTIVITY> listActivity = engine.GetNextActivitys(activityInstId);

            return Json(listActivity);
        }

        public ActionResult GetReturnActivitys(int activityInstId)
        {
            Engine engine = new Engine();

            List<F_ACTIVITY> listActivity = engine.GetReturnActivitys(activityInstId);

            return Json(listActivity);
        }
 

        /// <summary>
        /// 转移到下一步
        /// </summary>
        /// <param name="activityInstId"></param>
        /// <returns></returns>
        public ActionResult SubmitApply()
        {
            int activityInstId = int.Parse(Request.QueryString["ActivityInstId"]);
            string activityId = Request.QueryString["NextActivity"];

            //关卡id
            string[] arrActivityId = activityId.TrimEnd(',').Split(',');

            List<int> listActivityId = new List<int>();

            foreach (string id in arrActivityId)
            {
                listActivityId.Add(int.Parse(id));
            }

            Engine engine = new Engine();

            bool result = engine.TransferBack(activityInstId, listActivityId);

            if (result)
            {
                return Json(new { IsSuccess = true, Message = "申请成功" });
            }
            else
            {
                return Json(new { IsSuccess = false, Message = "申请失败" });
            }

        }


        public ActionResult RecallFlow(int flowId, int flowNo, string remark)
        {
            Engine engine = new Engine();

            bool result = engine.RecallFlow(flowId, flowNo, remark);

            if (result)
            {
                return Json(new { IsSuccess = true, Message = "撤回成功" });
            }
            else
            {
                return Json(new { IsSuccess = false, Message = "撤回失败" });
            }
        }


        public ActionResult DeleteFlowInstance(int flowInstId)
        {
            Engine engine = new Engine();

            bool result = engine.DeleteFlowStance(flowInstId);

            if (result)
            {
                return Json(new { IsSuccess = true, Message = "删除成功" });
            }
            else
            {
                return Json(new { IsSuccess = false, Message = "删除失败" });
            }
        }

        /// <summary>
        /// 签核表单
        /// </summary>
        /// <returns></returns>
        public ActionResult ApproveForm()
        {
            int workItemInstId = int.Parse(Request.QueryString["workItemInstId"]);
            int activityInstId = int.Parse(Request.QueryString["ActivityInstId"]);
            string appValue = Request.QueryString["appValue"];
            string appRemark = Request.QueryString["appRemark"];
            string nextActivityId = Request.QueryString["NextActivity"];

            //签核
            Engine engine = new Engine();

            bool result = engine.Approve(workItemInstId, CurrentUser.ID, appValue, appRemark, nextActivityId);

            if (result)
            {
                return Json(new { IsSuccess = true, Message = "签核成功" });
            }
            else
            {
                return Json(new { IsSuccess = false, Message = "签核失败" });
            }

        }

        public ActionResult TransaferFlowState(string state)
        {
            Flow flow = new Flow();

            return Json(flow.TransaferFlowState(state));

        }

        public ActionResult TransaferAppValue(string state)
        {
            Flow flow = new Flow();

            return Json(flow.TransaferAppValue(state));
        }

        public ActionResult IsApply(string state)
        {
            Flow flow = new Flow();

            return Json(flow.IsApply(state));
        }

        #region 表单管理
        public ActionResult Flow()
        {
            ViewData["Date"] = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }

        public ActionResult FlowLoad(int page, int rows, string order, string sort, int? catalogID)
        {
            Flow flow = new Flow();

            var result = flow.LoadAllFlowByPage(page, rows, order, sort, catalogID);

            return Json(result);
        }

        public ActionResult FlowSave(F_FLOW entity)
        {
            Flow flow = new Flow();
            entity.IsInner = Request.Form["isInner"];
            entity.LayoutType = int.Parse(Request.Form["layoutType"]);

            if (ModelState.IsValid)
            {
                bool save;

                try
                {
                    if (entity.ID == 0)
                    {
                        entity.CreateDate = DateTime.Now;
                        save = flow.Insert(entity);
                    }
                    else
                    {
                        entity.ModifyDate = DateTime.Now;
                        save = flow.Save(entity);
                    }
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

        /// <summary>
        /// 表单类型
        /// </summary>
        /// <returns></returns>
        public ActionResult FlowCatalog() 
        {
            Flow flow = new Flow();
            var result = flow.GetCatalog();
            return Json(result);
        }

        public ActionResult FlowDelete(IList<int> idList) 
        {
            Flow flow = new Flow();
            if (ModelState.IsValid)
            {
                bool delete;
                try
                {
                    delete = flow.Delete(idList);
                }
                catch (Exception)
                {
                    delete = false;
                }

                if (delete)
                {
                    return Json(new { IsSuccess = true, Message = "删除成功" });
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = "删除失败" });
                }
            }
            return View();
        }

        public ActionResult FlowCatalogLoad(int page, int rows, string order, string sort) 
        {
            Flow flow = new Flow();

            var result = flow.LoadAllCatalogByPage(page, rows, order, sort);

            return Json(result);
        }
        public ActionResult FlowCatalogSave(int id, string name) 
        {
            Flow flow = new Flow();
            F_CATALOG entity = new F_CATALOG();
            entity.ID = id;
            entity.Name = name;

            if (ModelState.IsValid)
            {
                bool save;

                try
                {
                    save = flow.SaveCatalog(entity);
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

        public ActionResult FlowCatalogDelete(int id)
        {
            Flow flow = new Flow();
            if (ModelState.IsValid)
            {
                bool delete;
                try
                {
                    delete = flow.DeleteCatalog(id);
                }
                catch (Exception)
                {
                    delete = false;
                }

                if (delete)
                {
                    return Json(new { IsSuccess = true, Message = "删除成功" });
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = "删除失败" });
                }
            }
            return View();
        }

        public void NoticeReadFlag(int flowId, int flowNo, int workerID)
        {
            Flow flow = new Flow();

            bool read;
            try
            {
                read = flow.NoticeReadFlag(flowId,flowNo, workerID);
            }
            catch (Exception)
            {
                read = false;
            }
        }

        #endregion
    }
}

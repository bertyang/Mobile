using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;

using Anchor.FA.BLL.IBLL;
using Anchor.FA.Model;
using Anchor.FA.Utility;
using Anchor.FA.BLL.WorkFlow;

using System.Text;
using System.Reflection;
using System.IO;

namespace Anchor.FA.Web.Controllers
{

    /// <summary>
    /// 工作流内部表单基类
    /// </summary>
    public class BaseFlowController : BaseController
    {
        #region 表单页面公用方法

        /// <summary>
        /// 申请页面
        /// </summary>
        /// <returns></returns>
        public ActionResult A()
        {
            Flow flow = new Flow();
            Activity d =new Activity();

            BLL.Organize.Worker worker = new BLL.Organize.Worker();
            this.ViewData["applyWorkerID"] = User.Identity.Name.Split('|')[0];  //申请人ID
            this.ViewData["applyWorker"] = User.Identity.Name.Split('|')[1];  //申请人
            this.ViewData["createTime"] = DateTime.Now.ToString("yyyy-MM-dd");  //创建时间

            B_WORKER_ORGANIZATION org= worker.GetWorkerDefaultUnit(int.Parse(User.Identity.Name.Split('|')[0]));
            if (org != null)
            {
                this.ViewData["applyWorkerDepartID"] = org.OrgID;//申请人部门
            }

            ViewData["FlowId"] = Request.QueryString["FlowId"];
            
            if (!string.IsNullOrEmpty(Request.QueryString["flowInstId"]))
            {

                ViewData["Url"] = string.Format("/FLOW{0}/Page/Submit/?{1}", 
                    Request.QueryString["flowId"], 
                    Request.QueryString);

                ViewData["FlowNo"] = Request.QueryString["flowNo"];

                GetTaskListHTML();

                return LoadData(int.Parse(Request.QueryString["flowNo"]));
            }
            else
            {

                Engine engine = new Engine();

                int flowNo=engine.CreateFlowNo(int.Parse(Request.QueryString["flowId"]));

                ViewData["Url"] = string.Format("/FLOW{0}/Page/Submit/?{1}&flowNo={2}",
                    Request.QueryString["flowId"],
                    Request.QueryString,
                    flowNo);

                ViewData["FlowNo"] = flowNo; 

                return View();
            }
        }

        /// <summary>
        /// 签核页面
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult V()
        {
            ViewData["Url"] = string.Format("/FLOW{0}/Page/Save/?{1}", Request.QueryString["flowId"], Request.QueryString);

            if (!string.IsNullOrEmpty(Request.QueryString["isPrint"]))
            {
                ViewData["isPrint"] = 1;
            }
            else
            {
                ViewData["isPrint"] = 0;
            }

            GetTaskListHTML();

            return LoadData(int.Parse(Request.QueryString["flowNo"]));            
        }

        /// <summary>
        /// 提交表单
        /// </summary>
        /// <returns></returns>
        public ActionResult Submit()
        {
            int flowId = int.Parse(Request.QueryString["flowId"]);
            int flowNo = int.Parse(Request.QueryString["flowNo"]);

            Engine engine = new Engine();

            Dictionary<string, string> param;

            string digest = GetFormDigest();

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    engine.ApplyInnerFlow(flowId, ref flowNo, CurrentUser.ID, digest, out param);

                    if (SaveForm(flowNo))
                    {
                        scope.Complete();

                        return Json(new
                        {
                            IsSuccess = true,

                            SplitType = param["SplitType"],

                            ActivtyInstId = param["ActivityInstId"],

                            FlowInstId = param["FlowInstId"]

                        }, "text/html", JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    Log4Net.LogError("Submit Form", string.Format("flowId:{0},flowNo:{1},Message:{2}", flowId, flowNo, ex.ToString()));
                }

                return Json(new
                {
                    IsSuccess = false,
                    Message = "提交失败"
                }, "text/html", JsonRequestBehavior.AllowGet);
            }

        }

        protected void GetTaskListHTML()
        {
            Flow flow = new Flow();
            int flowInstId = int.Parse(Request.QueryString["flowInstId"]);
            ViewData["TaskListHTML"] = flow.TaskListScript(flowInstId);
        }

        /// <summary>
        /// 保存表单
        /// </summary>
        /// <param name="flowNo"></param>
        /// <returns></returns>
        public ActionResult Save(int flowNo)
        {
            if (SaveForm(flowNo))
            {
                return Json(new
                {
                    IsSuccess = true,
                    Message = "提交成功"
                }, "text/html", JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(new
                {
                    IsSuccess = false,
                    Message = "提交失败"
                }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 附件公用方法
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="upfile"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FileUpload(HttpPostedFileBase upfile)
        {

            Flow flow = new Flow();

            string path = AppConfig.GetUpload(); 

            int flowId = int.Parse(Request.Form["flowId"]);
            int flowNo = int.Parse(Request.Form["flowNo"]);

            HttpPostedFileBase file = Request.Files[0]; //获取附件信息

            if (file != null && file.ContentLength > 0)
            {
                bool save = flow.SaveAttacment(flowId,flowNo,file, path);

                if (save)
                {
                    return Json(new { IsSuccess = true, Message = "上传成功" }, "text/html", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = "上传失败" }, "text/html", JsonRequestBehavior.AllowGet);
                }
            }
            return View();
        }

        /// <summary>
        /// 下载附件
        /// </summary>
        /// <param name="fileId">附件编码</param>
        /// <returns></returns>
        public ActionResult FileDownLoad(int fileId)
        {
            Flow flow = new Flow();
            F_INST_ATTACHMENT cot = flow.DownLoadAttacment(fileId);

            string path = AppConfig.GetUpload() + cot.CodingName;

            return new DownloadResult { VirtualPath = path, FileDownloadName = cot.OriginalName };

        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="fileId">附件编码</param>
        /// <returns></returns>
        public ActionResult FileDelete(int fileId)
        {
            Flow flow = new Flow();
            bool delete;
            try
            {
                string path = AppConfig.GetUpload(); 

                delete = flow.DeleteAttacment(fileId, path);

            }
            catch (Exception)
            {
                delete = false;
            }

            if (delete)
            {
                return Json(new { IsSuccess = true, Message = "删除成功" }, "text/html", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { IsSuccess = false, Message = "删除失败" }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }
        
        /// <summary>
        /// 获取附件
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAttacment(int flowId,int flowNo)  
        {
            List<F_INST_ATTACHMENT> listAtt = new List<F_INST_ATTACHMENT>();

            Flow flow = new Flow();

            listAtt = flow.GetAttacment(flowId,flowNo);

            var listFile = listAtt.Select(o => new
            {
                ID = o.ID,
                OriginalName = o.OriginalName,
                Size = Convert.ToDouble(o.Size).ToString("0.00") + "kb",
            });

            return Json(listFile);
        }

        #endregion

        #region 表单重载方法
        /// <summary>
        /// 装载表单数据
        /// </summary>
        /// <param name="flowNo"></param>
        /// <returns></returns>
        public virtual ActionResult LoadData(int flowNo)
        {
            return View();
        }

        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="flowNo"></param>
        /// <returns></returns>
        public virtual bool SaveForm(int flowNo)
        {
            return true;
        }

        /// <summary>
        /// 获得表单数据摘要
        /// </summary>
        /// <param name="flowNo"></param>
        /// <returns></returns>
        public virtual string GetFormDigest()
        {
            return string.Empty;
        }

        #endregion
    }
}

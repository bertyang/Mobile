using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anchor.FA.Model;
using Anchor.FA.BLL.IBLL;
using System.IO;
using Anchor.FA.Utility;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.UI;
using System.Data.SqlClient;

namespace Anchor.FA.Web.Controllers
{
    public class OfficeController : BaseController
    {
        /// <summary>
        /// 办公查询页面
        /// </summary>
        /// <returns></returns>
        //public ActionResult OfficeRecList(string type)
        //{
        //    string startTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss");
        //    string endTime = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd HH:mm:ss");

        //    this.ViewData["startTime"] = startTime;
        //    this.ViewData["endTime"] = endTime;
        //    this.ViewData["type"] = type;

        //    return View();
        //}

        /// <summary>
        /// 办公管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult OfficeSendList(int? pageNumber, string type, string startTime, string endTime, string title, string writer)
        {
            if (pageNumber == null || pageNumber == 0)
                this.ViewData["pageNumber"] = "1";
            else
                this.ViewData["pageNumber"] = pageNumber;
            this.ViewData["startTime"] = startTime == null ? DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd") : startTime;
            this.ViewData["endTime"] = endTime == null ? DateTime.Now.AddDays(0).ToString("yyyy-MM-dd") : endTime;

            this.ViewData["type"] = type == null ? "" : type;
            this.ViewData["title"] = title == null ? "" : title;
            this.ViewData["writer"] = writer == null ? "" : writer;

            return View();
        }

        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="rows">总行数</param>
        /// <param name="order">排序顺序</param>
        /// <param name="sort">排序列</param>
        /// <param name="type">办公类型编码</param>
        /// <param name="startTime">起始时刻</param>
        /// <param name="endTime">终止时刻</param>
        /// <param name="title">标题</param>
        /// <param name="writer">作者</param>
        /// <returns></returns>
        public ActionResult OfficeSearch(int page, int rows, string order, string sort, string type, DateTime? startTime, DateTime endTime, string title, string writer)
        {
            BLL.Office.Office office = new BLL.Office.Office();

            var result = office.GetOfficeList(page, rows, order, sort, type,
                         startTime == null ? (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue : (DateTime)startTime, 
                         endTime,
                            title, writer, CurrentUser.ID);
            this.ViewData["type"] = type;

            return Json(result);
        }


        /// <summary>
        /// 查看详细信息
        /// </summary>
        /// <param name="officeId">办公编码</param>
        /// <returns></returns>
        public ActionResult OfficeDetail(int officeId) 
        {
            BLL.Office.Office office = new BLL.Office.Office();

            TOffice cot = office.GetOffice(officeId);
            this.ViewData["entity"] = cot;
            this.ViewData["officeId"] = officeId;
            this.ViewData["title"] = cot.标题;
            this.ViewData["type"] = cot.办公类型编码;

            //this.ViewData["pageNumber"] = pageNumber;
            //this.ViewData["startTime"] = startTime;
            //this.ViewData["_type"] = type;
            //this.ViewData["_title"] = title; //查询条件中的标题
            //this.ViewData["_writer"] = writer == string.Empty ? "" : writer; //查询条件中的作者

            if (cot.发送类型编码 == 1)
            {
                this.ViewData["receive"] = cot.接收人;
            }
            else if (cot.发送类型编码 == 2)
            {
                this.ViewData["receive"] = cot.接收部门;
            }
            else
            {
                this.ViewData["receive"] = cot.接收人;
            }
            this.ViewData["content"] = cot.内容;

            Anchor.FA.BLL.Office.Office.UpdateRec(officeId, CurrentUser.ID.ToString());
            return View();
        }


        /// <summary>
        /// 获取接收人列表
        /// </summary>
        public ActionResult GetRecs(string isRead, int BGCode)
        {

            var result = Anchor.FA.BLL.Office.Office.GetRecs(BGCode,isRead);
            return Json(result);
        }





        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <param name="id">办公编码</param>
        /// <param name="type">办公类型编码</param>
        /// <returns></returns>
        public ActionResult OfficeEdit(int pageNumber, int? officeId, string type, string startTime, string endTime, string title, string writer) 
        {
            BLL.Office.Office office = new BLL.Office.Office();

            this.ViewData["pageNumber"] = pageNumber;
            this.ViewData["startTime"] = startTime;
            //this.ViewData["endTime"] = endTime;
            this.ViewData["_type"] = type;
            this.ViewData["_title"] = title;//查询条件中的标题
            this.ViewData["_writer"] = writer;  //查询条件中的作者姓名

            TOffice cot = office.GetOffice(officeId);
            this.ViewData["entity"] = cot;
            this.ViewData["title"] = cot.标题;
            this.ViewData["content"] = cot.内容; 
            this.ViewData["sendType"] = officeId == null ? 1 : cot.发送类型编码;
            this.ViewData["officeId"] = cot.编码;
            this.ViewData["receive"] = cot.接收部门编码;

            this.ViewData["type"] = cot.办公类型编码;
            this.ViewData["writerID"] = officeId == null ? User.Identity.Name.Split('|')[0] : cot.发送人编码;  //作者ID
            this.ViewData["writer"] = officeId == null ? User.Identity.Name.Split('|')[1] : cot.作者;    //作者姓名

            this.ViewData["receivePerN"] = cot.接收人编码;
            this.ViewData["receivePer"] = cot.接收人;
            return View();
        }

        /// <summary>
        /// 获取所有办公类型
        /// </summary>
        /// <returns></returns>
        public ActionResult OfficeType()
        {
            BLL.Office.Office office = new BLL.Office.Office();

            var result = office.GetAllInfoType();

            return Json(result);
        }

        /// <summary>
        /// 获取附件
        /// </summary>
        /// <param name="officeId">办公编码</param>
        /// <returns></returns>
        public ActionResult OfficeFile(int officeId)  
        {
            BLL.Office.Office office = new BLL.Office.Office();

            var result = office.GetFile(officeId); 

            return Json(result);
        }

        /// <summary>
        /// 接收信息
        /// </summary>
        /// <param name="officeId"></param>
        /// <returns></returns>
        public ActionResult OfficeReceive(int officeId)
        {
            BLL.Office.Office office = new BLL.Office.Office();

            var result = office.GetReceiveInfo(officeId);

            return Json(result);
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FileUpload(HttpPostedFileBase upfile) 
        {
            BLL.Office.Office office = new BLL.Office.Office();

            string officeId = Request.Form["officeCode"];

            string path = AppConfig.GetUpload(); 

            HttpPostedFileBase file = Request.Files[0]; //获取附件信息

            if (file != null && file.ContentLength > 0)
            {
                bool save;
                try
                {
                    save = office.SaveFile(file, path, int.Parse(officeId));
                }
                catch (Exception)
                {

                    save = false;
                }
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
            BLL.Office.Office office = new BLL.Office.Office();
            TOfficeAttachment cot = office.DownLoadFile(fileId);
            string path = AppConfig.GetUpload() + cot.编码附件名;

            return new DownloadResult { VirtualPath = path, FileDownloadName = cot.原附件名 };

        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="fileId">附件编码</param>
        /// <returns></returns>
        public ActionResult FileDelete(int fileId)
        {
            BLL.Office.Office office = new BLL.Office.Office();
            bool delete;
            try
            {
                string path = AppConfig.GetUpload();

                delete = office.DeleteFile(fileId, path);
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
        /// 保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]      //当页面提交具有html标签时不进行验证
        public ActionResult OfficeSave(TOffice entity, string editor)
        {
            BLL.Office.Office office = new BLL.Office.Office();

            entity.内容 = Request.Form["editor"];

            switch (entity.发送类型编码)
            {
                case 1:
                    entity.接收人编码 = "allworker";
                    entity.接收人 = "全体人员";
                    entity.接收部门编码=null;
                    entity.接收部门 = null;
                    break;
                case 2:
                    entity.接收人编码 = null;
                    entity.接收人 = null;
                    entity.接收部门编码 = Request.Form["receive"];
                    break;
                case 3:
                    //entity.接收人编码 = "allworker";
                    //entity.接收人 = "全体人员";
                    entity.接收部门编码=null;
                    entity.接收部门 = null;
                    break;
                default:
                    break;
            }

            //entity.创建时间 = DateTime.Now;

            if (ModelState.IsValid)
            {
                bool save = office.Save(entity);

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
        /// 删除
        /// </summary>
        /// <returns></returns>
        public ActionResult OfficeDelete(IList<int> idList)
        {
            BLL.Office.Office office = new BLL.Office.Office();
            bool delete;
            try
            {
                delete = office.Delete(idList);
            }
            catch (Exception ex)
            {
                Log4Net.LogError("Office", ex.Message);

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


        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <param name="id">办公编码</param>
        /// <param name="type">办公类型编码</param>
        /// <returns></returns>
        public ActionResult SelectPerson()
        {
            return View();
        }
    }

    public class DownloadResult : ActionResult
    {

        public DownloadResult()
        {
        }

        public DownloadResult(string virtualPath)
        {
            this.VirtualPath = virtualPath;
        }

        public string VirtualPath
        {
            get;
            set;
        }

        public string FileDownloadName
        {
            get;
            set;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (!String.IsNullOrEmpty(FileDownloadName))
            {
                context.HttpContext.Response.AddHeader("content-disposition",
                  "attachment; filename=" + context.HttpContext.Server.UrlEncode(this.FileDownloadName));
            }

            //string filePath = context.HttpContext.Server.MapPath(this.VirtualPath);
            context.HttpContext.Response.TransmitFile(VirtualPath);
        }
    }
}

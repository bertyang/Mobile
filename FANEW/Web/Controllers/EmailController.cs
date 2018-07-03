using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anchor.FA.Model;
using Anchor.FA.BLL.IBLL;
using System.IO;
using Anchor.FA.Utility;

namespace Anchor.FA.Web.Controllers
{
    public class EmailController : BaseController
    {
        //
        // GET: /Email/

        #region 写信
        public ActionResult Write(int? pageNumber, int? mailID, string type)
        {
            BLL.Email.Email email = new BLL.Email.Email();

            this.ViewData["WriterID"] = User.Identity.Name.Split('|')[0];
            this.ViewData["WriterName"] = User.Identity.Name.Split('|')[1];  //发信人

            if (mailID != null)   
            {
                if (type=="reply") //回复
                {

                    this.ViewData["MailID"] = PrimaryKeyCreater.getIntPrimaryKey("E_Mail");  //邮件编号
                    this.ViewData["CreateTime"] = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd HH:mm:ss"); ; //创建时间

                    E_Mail mail = email.GetMailInfo((int)mailID);
                    this.ViewData["To"] = mail.From;  //收件人
                    this.ViewData["Title"] = "回复:"+mail.Title; //主题
                    this.ViewData["Body"] = "<br><hr>"+mail.Body;  //内容

                    List<E_Mail_Worker> toWorker = email.GetMailWorker((int)mailID, 0); //收件人ID 
                    List<int> toStr = new List<int>();
                    foreach (E_Mail_Worker w in toWorker)
                    {
                        toStr.Add(w.WorkerId);
                    }
                    this.ViewData["toID"] = String.Join(",", toStr);
                }
                else if (type == "forward") //转发
                {

                    this.ViewData["MailID"] = PrimaryKeyCreater.getIntPrimaryKey("E_Mail");  //邮件编号
                    this.ViewData["CreateTime"] = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd HH:mm:ss"); ; //创建时间

                    E_Mail mail = email.GetMailInfo((int)mailID);
                    this.ViewData["Title"] = "转发:" + mail.Title; //主题
                    this.ViewData["Body"] = "<br><hr>" + mail.Body;  //内容
                }
                else //草稿
                {
                    this.ViewData["pageNumber"] = pageNumber;

                    E_Mail mail = email.GetMailInfo((int)mailID);
                    this.ViewData["MailID"] = mailID;  //邮件编号  
                    this.ViewData["CreateTime"] = mail.CreateTime; //创建时间
                    this.ViewData["To"] = mail.To;  //收件人
                    this.ViewData["CC"] = mail.CC == null ? "" : mail.CC;  //抄送
                    //this.ViewData["SC"] = mail.SC;  //密送
                    this.ViewData["Title"] = mail.Title; //主题
                    this.ViewData["Body"] = mail.Body;  //内容

                    List<E_Mail_Worker> toWorker = email.GetMailWorker((int)mailID, 1); //收件人ID 
                    List<int> toStr = new List<int>();
                    foreach (E_Mail_Worker w in toWorker)
                    {
                        toStr.Add(w.WorkerId);
                    }
                    this.ViewData["toID"] = String.Join(",", toStr);

                    List<E_Mail_Worker> ccWorker = email.GetMailWorker((int)mailID, 2); //抄送人ID
                    List<int> ccStr = new List<int>();
                    foreach (E_Mail_Worker w in ccWorker)
                    {
                        ccStr.Add(w.WorkerId);
                    }
                    this.ViewData["ccID"] = String.Join(",", ccStr);
                }
            }
            else
            {

                this.ViewData["MailID"] = PrimaryKeyCreater.getIntPrimaryKey("E_Mail");  //邮件编号
                this.ViewData["CreateTime"] = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd HH:mm:ss");
                this.ViewData["toID"] = string.Empty;  //收件人ID
                this.ViewData["To"] = string.Empty;  //收件人
                this.ViewData["CC"] = string.Empty;  //抄送
                this.ViewData["ccID"] = string.Empty;  //抄送人ID
                this.ViewData["SC"] = string.Empty;  //密送
                this.ViewData["Title"] = string.Empty; //主题
                this.ViewData["Body"] = string.Empty;  //内容
            }
            return View();
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FileUpload(HttpPostedFileBase upfile)
        {
            BLL.Email.Email email = new BLL.Email.Email();

            string mailID = Request.Form["mailID"];

            string path = AppConfig.GetUpload();

            HttpPostedFileBase file = Request.Files[0]; //获取附件信息

            if (file != null && file.ContentLength > 0)
            {
                bool save;
                try
                {
                    save = email.SaveFile(file, path, int.Parse(mailID));
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

        public ActionResult FileDownLoad(int fileId)
        {
            BLL.Email.Email email = new BLL.Email.Email();
            E_Mail_Attachment cot = email.DownLoadFile(fileId);

            string path = AppConfig.GetUpload() + cot.CodingName;

            return new DownloadResult { VirtualPath = path, FileDownloadName = cot.OriginalName };
        }

        /// <summary>
        /// 获取附件信息
        /// </summary>
        /// <param name="mailID"></param>
        /// <returns></returns>
        public ActionResult GetFile(int mailID)
        {
            BLL.Email.Email email = new BLL.Email.Email();

            var result = email.GetFile(mailID);

            return Json(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="idList">编码</param>
        /// <returns></returns>
        public ActionResult FileDelete(int id)
        {
            BLL.Email.Email email = new BLL.Email.Email();
            bool delete;
            try
            {
                string path = AppConfig.GetUpload();
                delete = email.DeleteFile(id, path);
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
        /// 发送
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]      //当页面提交具有html标签时不进行验证
        public ActionResult SendMail(E_Mail entity, string isSend)
        {
            BLL.Email.Email email = new BLL.Email.Email();
            entity.SendDate = DateTime.Now; //发送时间
            entity.Body = Request.Form["editor"];   //正文

            string fromID = Request.Form["WriterID"];  //写信人ID
            string toID = Request.Form["toID"];   //收件人ID
            string ccID = Request.Form["ccID"];   //抄送人ID
            //string scID = Request.Form["scID"];   //密送人ID

            string remind = Request.Form["remind"];

            bool remind_ = remind == null ? false : true;  //判断是否发送短信

            if (ModelState.IsValid)
            {
                bool send;
                try
                {
                    send = email.SendMail(entity, fromID, toID, ccID, isSend, remind_);
                }
                catch (Exception)
                {

                    send = false;
                }
                if (isSend == "1")
                {
                    if (send)
                    {
                        return Json(new { IsSuccess = true, Message = "发送成功" }, "text/html", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { IsSuccess = false, Message = "发送失败" }, "text/html", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (send)
                    {
                        return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { IsSuccess = false, Message = "保存失败" }, "text/html", JsonRequestBehavior.AllowGet);
                    }
                }

            }
            return View();
        }

        #endregion

        #region 收件箱
        public ActionResult Inbox()
        {
            this.ViewData["workerID"] = int.Parse(User.Identity.Name.Split('|')[0]);
            this.ViewData["folderID"] = 1;

            return View();
        }

        public ActionResult EmailSearch(int page, int rows, string order, string sort, int workerID, int folderID)
        {
            BLL.Email.Email email = new BLL.Email.Email();

            var result = email.GetEmailList(page, rows, order, sort, workerID, folderID);

            return Json(result);
        }

        public ActionResult GetMailInfo(int mailID)
        {
            BLL.Email.Email email = new BLL.Email.Email();

            this.ViewData["entity"] = email.GetMailInfo(mailID);

            return View();
        }

        public void ReadFlag(int mailID, int workerID)
        {
            BLL.Email.Email email = new BLL.Email.Email();
            bool read;
            try
            {
                read = email.ReadFlag(mailID, workerID);
            }
            catch (Exception)
            {
                read = false;
            }
        }

        /// <summary>
        /// 删除（将文件移动到“已删除”）
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public ActionResult Delete(IList<int> idList, int folderID)
        {
            BLL.Email.Email email = new BLL.Email.Email();
            int workerID = int.Parse(User.Identity.Name.Split('|')[0]);
            bool delete;
            try
            {
                delete = email.Delete(idList, workerID, folderID);
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
        #endregion

        #region 草稿箱
        public ActionResult Drafts(int? pageNumber)
        {
            if (pageNumber == null || pageNumber == 0)
                this.ViewData["pageNumber"] = "1";
            else
                this.ViewData["pageNumber"] = pageNumber;

            this.ViewData["workerID"] = int.Parse(User.Identity.Name.Split('|')[0]);
            this.ViewData["folderID"] = 2;

            return View();
        }
        #endregion

        #region 已发送
        public ActionResult Sent()
        {
            this.ViewData["workerID"] = int.Parse(User.Identity.Name.Split('|')[0]);
            this.ViewData["folderID"] = 3;

            return View();
        }
        #endregion

        #region 已删除
        public ActionResult Trash()
        {
            this.ViewData["workerID"] = int.Parse(User.Identity.Name.Split('|')[0]);
            this.ViewData["folderID"] = 4;

            return View();
        }

        /// <summary>
        /// 彻底删除
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public ActionResult MailDelete(IList<int> idList)
        {
            BLL.Email.Email email = new BLL.Email.Email();
            bool delete;
            try
            {
                string path = Server.MapPath("/Upload/");
                delete = email.DeleteMail(idList, path);
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
        #endregion
    }
}

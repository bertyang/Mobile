using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Anchor.FA.Model;
using System.Web.Mvc;
using Anchor.FA.BLL.IBLL;

namespace Anchor.FA.Web.Controllers
{
    public class NoticeController : BaseController
    {
        #region 通知查询
        public ActionResult NoticeList(string type)
        {
            string startTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
            string endTime = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd HH:mm:ss");

            this.ViewData["startTime"] = startTime;
            this.ViewData["endTime"] = endTime;
            this.ViewData["type"] = type;
            return View();
        }
        public ActionResult NoticeType()
        {
            BLL.Notice.NoticeList notice = new BLL.Notice.NoticeList();
            var result = notice.getNoticeType();
            return Json(result);
        }


        public ActionResult NoticeSearch(int page, int rows, string order, string sort, DateTime startTime, DateTime endTime, 
            int? sendType, string station, string vehicle,int ActionId)
        {
            BLL.Notice.NoticeList notice = new BLL.Notice.NoticeList();

            sendType = sendType == null ? -1 : sendType;
            if (station == "--请选择--" || station == "" )
            {
                station = "-1";
            }
            if (vehicle == "--请选择--" || vehicle == "")
            {
                vehicle = "-1";
            }

            Anchor.FA.Utility.ButtonPower p = new Anchor.FA.Utility.ButtonPower();
            p.ActionIDRang = UserInfo.GetRange(ActionId);//新方法
            BLL.BasicInfo.Ambulance am = new BLL.BasicInfo.Ambulance();

            var result = notice.GetNoticeList(page, rows, order, sort, startTime, endTime,
                (int)sendType, station, vehicle, p,UserInfo);

            return Json(result);
        }

        /// <summary>
        /// 获取通知回复列表
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult GetNoticeBack(int code)
        {
            BLL.Notice.NoticeList notice = new BLL.Notice.NoticeList();

            var result = notice.GetBack(code);

            return Json(result);
        }
        #endregion

        #region 定时短信
        public ActionResult Remind()
        {
            return View();
        }
        public ActionResult RemindLoad(int page, int rows, string order, string sort)
        {
            BLL.Notice.NoticeList notice = new BLL.Notice.NoticeList();
            var result = notice.LoadRemindByPage(page, rows, order, sort);
            return Json(result);
        }
        public ActionResult RemindSave(B_Remind entity)
        {
            BLL.Notice.NoticeList notice = new BLL.Notice.NoticeList();

            entity.发送对象 = Request.Form["telList"];

            entity.是否发送 = "N";

            int WorkerID = int.Parse(User.Identity.Name.Split('|')[0]);
            entity.操作员编码 = notice.GetEmpNoByWorkerID(WorkerID); //获取发送人的工号

            if (ModelState.IsValid)
            {
                bool save = notice.Save(entity);

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
        public ActionResult RemindEdit(int? Id)
        {
            BLL.Notice.NoticeList notice = new BLL.Notice.NoticeList();

            B_Remind remind = notice.Edit(Id) as B_Remind;

            this.ViewData["entity"] = remind;
            this.ViewData["time"] = remind.提醒时间.ToString("yyyy-MM-dd HH:mm:ss"); ;

            this.ViewData["OwnerID"] = int.Parse(User.Identity.Name.Split('|')[0]);
            return View();
        }
        public ActionResult RemindDelete(IList<int> idList)
        {
            BLL.Notice.NoticeList notice = new BLL.Notice.NoticeList();
            bool delete = notice.Delete(idList);

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

        #region 即时短信
        public ActionResult GetAllTel()
        {
            BLL.Notice.NoticeList notice = new BLL.Notice.NoticeList();

            var result = notice.GetAllTel();

            return Json(result);
        }
        public ActionResult MessageSave()
        {
            BLL.Notice.NoticeList notice = new BLL.Notice.NoticeList();
            ISMS sms = ctx["SMS"] as ISMS;

            string content = Request.Form["content"];
            string Tel = Request.Form["telList"];

            List<string> TelList = Tel.Split(',').Distinct().ToList();

            int WorkerID = int.Parse(User.Identity.Name.Split('|')[0]);
            string empNo = notice.GetEmpNoByWorkerID(WorkerID); //获取发送人的工号

            if (sms.SendSMG(TelList, content, empNo))
            {
                return Json(new { IsSuccess = true, Message = "发送成功" }, "text/html", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { IsSuccess = false, Message = "发送失败" }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SendMessage()
        {
            this.ViewData["OwnerID"] = int.Parse(User.Identity.Name.Split('|')[0]);
            return View();
        }
        #endregion

        #region 车辆通知
        public ActionResult SendAmbNotice()
        {

            return View();
        }
        /// <summary>
        /// 根据分站编码获取车辆列表
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public ActionResult AmbulanceLoad(int ActionId,string stationId)
        {
            BLL.Notice.NoticeList notice = new BLL.Notice.NoticeList();

            //获取页面权限
            Anchor.FA.Utility.ButtonPower p = new Anchor.FA.Utility.ButtonPower();
            p.ActionIDRang = UserInfo.GetRange(ActionId);//新方法
            BLL.BasicInfo.Ambulance am = new BLL.BasicInfo.Ambulance();

            var result = notice.GetAmbulanceByStationID(stationId,p,UserInfo);
            return Json(result);
        }

        public ActionResult SendAmb()
        {
            BLL.Notice.InnerCommBLL innerComm = new BLL.Notice.InnerCommBLL();

            string code = Request.Form["code"];
            string content = Request.Form["content"];

            List<string> listCode = code.Split(',').ToList();

            if (innerComm.SendAmbNotice(content, listCode, UserInfo.EmpNo.FirstOrDefault()))
            {
                innerComm.InsertNoticeMsg(content, DateTime.Now, UserInfo.EmpNo.FirstOrDefault(), 0, listCode, listCode);
                return Json(new { IsSuccess = true, Message = "发送成功" }, "text/html", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { IsSuccess = false, Message = "发送失败" }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 台通知
        public ActionResult SendDeskNotice()
        {
            return View();
        }

        public ActionResult DeskLoad(int ActionId)
        {
            BLL.Notice.NoticeList notice = new BLL.Notice.NoticeList();

            //获取页面权限
            Anchor.FA.Utility.ButtonPower p = new Anchor.FA.Utility.ButtonPower();
            p.ActionIDRang = UserInfo.GetRange(ActionId);//新方法
            BLL.BasicInfo.Ambulance am = new BLL.BasicInfo.Ambulance();

            var result = notice.GetAllDesk(p, UserInfo);
            return Json(result);
        }

        public ActionResult SendDesk()
        {
            BLL.Notice.InnerCommBLL innerComm = new BLL.Notice.InnerCommBLL();

            string ip = Request.Form["ip"];
            string content = Request.Form["content"];
            string isBrodcast = Request.Form["isBrodcast"];
            List<string> listIp = ip.Split(',').ToList();


            if (innerComm.SendDeskNotice(content, listIp, isBrodcast=="Y"?true:false, UserInfo.W.Name))
            {
                innerComm.InsertNoticeMsg(content, DateTime.Now, UserInfo.EmpNo.FirstOrDefault(), 1, listIp, listIp);
                return Json(new { IsSuccess = true, Message = "发送成功" }, "text/html", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { IsSuccess = false, Message = "发送失败" }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 分站通知


        public ActionResult SendStationNotice()
        {
            return View();
        }

        //public ActionResult StationLoad()
        //{
        //    BLL.Notice.NoticeList notice = new BLL.Notice.NoticeList();
        //    var result = notice.GetStation();
        //    return Json(result);
        //}

        public ActionResult SendStation()
        {
            BLL.Notice.InnerCommBLL innerComm = new BLL.Notice.InnerCommBLL();

            string ip = Request.Form["ip"];
            string content = Request.Form["content"];

            List<string> listIp = ip.Split(',').ToList();

            if (innerComm.SendStationNotice(content, listIp))
            {
                innerComm.InsertNoticeMsg(content, DateTime.Now, UserInfo.EmpNo.FirstOrDefault(), 4, listIp, listIp);
                return Json(new { IsSuccess = true, Message = "发送成功" }, "text/html", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { IsSuccess = false, Message = "发送失败" }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 无锡统计二级菜单
        public ActionResult TongJi()
        {
            return View();
        }

        public ActionResult TJTree(int? exceptUnitId)
        {
            try
            {
                List<C_TongJi_TREE> list = (from r in UserInfo.WAct
                                            where r.ParentID == exceptUnitId
                                             select
                                             new C_TongJi_TREE
                                             {
                                                 id = r.Url,// url
                                                 text = r.Remark,//名称
                                                 ParentID = r.ParentID.ToString(),
                                                 iconCls = "icon tu1718"
                                             }
                                             ).ToList();

                return this.Json(list);
            }
            catch (Exception e)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("InfoID", "0");
                dict.Add("InfoMessage", e.Message);
                return this.Json(dict);
            }
        }
        #endregion
    }
}

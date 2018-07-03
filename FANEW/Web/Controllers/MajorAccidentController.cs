using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anchor.FA.BLL.IBLL;
using Anchor.FA.Model;
using Anchor.FA.Utility;

namespace Anchor.FA.Web.Controllers
{
    public class MajorAccidentController : BaseController
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public ActionResult AccidentEvent() 
        {
            string startTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss");
            string endTime = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd HH:mm:ss");

            this.ViewData["startTime"] = startTime;
            this.ViewData["endTime"] = endTime;

            return View();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="order">排序顺序</param>
        /// <param name="sort">排序字段</param>
        /// <param name="startTime">起始时刻</param>
        /// <param name="endTime">中止时刻</param>
        /// <param name="accidentName">事故名称</param>
        /// <param name="place">区域</param>
        /// <param name="type">事故类型</param>
        /// <param name="level">事故等级</param>
        /// <returns></returns>
        public ActionResult AccidentSearch(int page, int rows, string order, string sort,
            DateTime startTime, DateTime endTime, 
            string accidentName, string place, int? type, int? level, int ActionId)
        {
            BLL.MajorAccident.Accident accident = new BLL.MajorAccident.Accident();

            Anchor.FA.Utility.ButtonPower p = new ButtonPower();
            p.ActionIDRang = UserInfo.GetRange(ActionId);//新方法

            var result = accident.GetAccident(page, rows, order, sort,
                startTime, endTime, accidentName, place, type, level, p, UserInfo);
            return Json(result);
        }
        
        /// <summary>
        /// 事故类型
        /// </summary>
        /// <param name="exceptTypeId"></param>
        /// <returns></returns>
        public ActionResult AccidentType(int? exceptTypeId)
        {
            try
            {
                BLL.MajorAccident.Accident accident = new BLL.MajorAccident.Accident();
                List<C_ACCIDENTTYPE_TREE> cot = accident.GetTree(exceptTypeId);

                return this.Json(cot);
            }
            catch (Exception e)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("InfoID", "0");
                dict.Add("InfoMessage", e.Message);
                return this.Json(dict);
            }
        }

        /// <summary>
        /// 事故区域
        /// </summary>
        /// <returns></returns>
        public ActionResult AccidentAddress()
        {
            BLL.MajorAccident.Accident accident = new BLL.MajorAccident.Accident();
            var result = accident.GetAccidentAddress();
            return Json(result);
        }

        /// <summary>
        /// 事故等级
        /// </summary>
        /// <returns></returns>
        public ActionResult AccidentLevel()
        {
            BLL.MajorAccident.Accident accident = new BLL.MajorAccident.Accident();
            var result = accident.GetAccidentLevel();
            return Json(result);
        }

        /// <summary>
        /// 事故所包含的事件列表
        /// </summary>
        /// <param name="accidentId">事故编码</param>
        /// <returns></returns>
        public ActionResult AccidentList(string accidentId)
        {
            BLL.MajorAccident.Accident accident = new BLL.MajorAccident.Accident();
            var result = accident.GetAccidentInfoById(accidentId);
            return Json(result);
        } 

        /// <summary>
        /// 伤病员信息
        /// </summary>
        /// <param name="eventId">事件编码</param>
        /// <returns></returns>
        public ActionResult AccidentPatientInfo(int page, int rows, string order, string sort, string eventId)
        {
            BLL.MajorAccident.Accident accident = new BLL.MajorAccident.Accident();
            var result = accident.GetAccidentPatient(page, rows, order, sort, eventId);
            return Json(result);
        }

        /// <summary>
        /// 保存伤病员信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>

        public ActionResult AccidentPatientSave(TAccidentPatient entity)
        {
            BLL.MajorAccident.Accident accident = new BLL.MajorAccident.Accident();

            string ambulance = Request.Form["ambulance"];
            if (!string.IsNullOrEmpty(ambulance) && ambulance != "--请选择--")
            {
                entity.任务编码 = ambulance.Split('|')[0];
                entity.车辆编码 = ambulance.Split('|')[1];
            }

            bool save = false;
            try
            {
                save = accident.SavePatient(entity);
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

        /// <summary>
        /// 删除伤病员
        /// </summary>
        /// <param name="eventId">事件编码</param>
        /// <param name="orderNum">伤病员序号List</param>
        /// <returns></returns>
        public ActionResult AccidentPatientDelete(string eventId, int number) 
        {
            BLL.MajorAccident.Accident accident = new BLL.MajorAccident.Accident();
            bool delete;
            try
            {
                delete = accident.DeletePatient(eventId, number);
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
        /// 删除事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public ActionResult AccidentDelete(string eventId)
        {
            BLL.MajorAccident.Accident accident = new BLL.MajorAccident.Accident();

            if (ModelState.IsValid)
            {
                bool delete;
                try
                {
                    delete = accident.DeleteEvent(eventId);
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

        /// <summary>
        /// 病情
        /// </summary>
        /// <returns></returns>
        public ActionResult PatientIllState() 
        {
            BLL.MajorAccident.Accident accident = new BLL.MajorAccident.Accident();
            var result = accident.GetPatientIllState();
            return Json(result);
        }

        /// <summary>
        /// 相关车辆
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public ActionResult AccidentAmbulanc(string eventId)
        {
            BLL.MajorAccident.Accident accident = new BLL.MajorAccident.Accident();
            var result = accident.GetAmbulance(eventId);
            return Json(result);
        }

        /// <summary>
        /// 关联事件
        /// </summary>
        /// <param name="accidentId"></param>
        /// <returns></returns>
        public ActionResult AccidentRelation(string accidentId) 
        {
            BLL.MajorAccident.Accident accident = new BLL.MajorAccident.Accident();

            List<string> accidentList = accidentId.Split(',').ToList();
            accidentList.Remove(accidentList[0]);

             var result = accident.AccidentRelation(accidentList);

            return Json(result);
        }


        /// <summary>
        /// 合并重大事件
        /// </summary>
        /// <param name="accidentId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult AccidentCombine(string accidentId, string list) 
        {
            BLL.MajorAccident.Accident accident = new BLL.MajorAccident.Accident();

            List<string> accidentList = list.Split(',').ToList();
            accidentList.Remove(accidentList[0]);

            if (ModelState.IsValid)
            {
                bool update;
                try
                {
                    update = accident.CombineAccident(accidentId, accidentList);
                }
                catch (Exception)
                {
                    update = false;
                }
                if (update)
                {
                    return Json(new { IsSuccess = true, Message = "合并事故成功" }, "text/html", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = "合并事故失败" }, "text/html", JsonRequestBehavior.AllowGet);
                }
            }
            return View();
        }

        /// <summary>
        /// 解除事故关联
        /// </summary>
        /// <param name="accidentId"></param>
        /// <returns></returns>
        public ActionResult AccidentRelease(string accidentId)
        {
            BLL.MajorAccident.Accident accident = new BLL.MajorAccident.Accident();
            bool update;
            try
            {
                update = accident.ReleaseAccident(accidentId); 
            }
            catch (Exception)
            {
                update = false;
            }
            if (update)
            {
                return Json(new { IsSuccess = true, Message = "解除成功" }, "text/html", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { IsSuccess = false, Message = "解除失败" }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }
    }
}

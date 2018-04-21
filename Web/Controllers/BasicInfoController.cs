using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anchor.FA.Model;
using Anchor.FA.BLL.IBLL;
using Anchor.FA.BLL.BasicInfo;
using System.Text;
using System.Data;
using Anchor.FA.Utility;
using Newtonsoft.Json;
using System.Xml;
using System.IO;

namespace Anchor.FA.Web.Controllers
{
    public class BasicInfoController : BaseController
    {
        #region 通讯录

        public ActionResult TelBook(int? OwnerID) 
        {
            this.ViewData["OwnerID"] = OwnerID == null ? int.Parse(User.Identity.Name.Split('|')[0]) : OwnerID;
            return View();
        }
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public ActionResult TelBookLoad(int? page, int? rows, string order, string sort, int? OwnerID, string Name, string Type)
        {
            BLL.BasicInfo.TelBook telbook = new BLL.BasicInfo.TelBook();
            var result = telbook.LoadAllTelBookByPage(page, rows, order, sort, OwnerID, Name, Type);

            return Json(result);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult TelBookEdit(int? id, int OwnerID)
        {
            BLL.BasicInfo.TelBook telbook = new BLL.BasicInfo.TelBook();

            TTelBook tel = telbook.Edit(id) as TTelBook;

            this.ViewData["entity"] = tel;

            ViewData["TelType"] = tel.电话分类编码;
            ViewData["OwnerID"] = OwnerID;

            return View();
        }
        /// <summary>
        /// 填充电话类型搜索框
        /// </summary>
        /// <returns></returns>
        //public ActionResult TypeLoadAll()
        //{
        //    BLL.BasicInfo.TelBook tel = new BLL.BasicInfo.TelBook();

        //    var result = tel.GetAllType();

        //    return Json(result);
        //}

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public ActionResult TelDelete(IList<int> idList)
        {
            BLL.BasicInfo.TelBook tel = new BLL.BasicInfo.TelBook();
            if (ModelState.IsValid)
            {
                bool delete;
                try
                {
                    delete = tel.Delete(idList);
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

        public ActionResult TypeTreeLoad(int? exceptUnitId, int? OwnerID)
        {
            try
            {
                BLL.BasicInfo.TelBook tel = new BLL.BasicInfo.TelBook();
                List<C_TELTYPE_TREE> gt = tel.GetTree(exceptUnitId,OwnerID);
                if (gt == null)
                {
                    return this.Json("");
                }    
                return this.Json(gt);
            }
            catch (Exception e)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("InfoID", "0");
                dict.Add("InfoMessage", e.Message);
                return this.Json(dict);
            }
        }

        public ActionResult MessTypeTreeLoad(int OwnerID)
        {
            try
            {
                BLL.BasicInfo.TelBook tel = new BLL.BasicInfo.TelBook();
                List<C_TELTYPE_TREE> listOwner = tel.GetTree(null, OwnerID);
                List<C_TELTYPE_TREE> listPublic = tel.GetTree(null, 0);
                if (listOwner != null)
                {
                    listPublic.AddRange(listOwner);
                }
                if (listPublic == null)
                {
                    return this.Json("");
                }
                return this.Json(listPublic);
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
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult TelBookSave(TTelBook entity)
        {
            BLL.BasicInfo.TelBook tel = new BLL.BasicInfo.TelBook();
            if (ModelState.IsValid)
            {
                bool save;
                try
                {
                    save = tel.Save(entity);
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
        /// 导入
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Import(HttpPostedFileBase upfile) 
        {
            BLL.BasicInfo.TelBook tel = new BLL.BasicInfo.TelBook();

            if (upfile == null) //未选择附件
            {
                return Json(new { IsSuccess = false, Message = "请上传Excel文件！" }, "text/html", JsonRequestBehavior.AllowGet);
            }
            HttpPostedFileBase file = Request.Files[0]; //获取附件信息
            if (file.FileName.EndsWith(".xls") || file.FileName.EndsWith(".xlsx"))
            {
                int lastIndex = file.FileName.LastIndexOf("\\");
                string originalName = file.FileName.Substring(lastIndex + 1, file.FileName.Length - lastIndex - 1); //原文件名
                string codingName = System.Guid.NewGuid().ToString() + originalName;   //编码附件名
                string path = Server.MapPath("/Upload/") + codingName;

                file.SaveAs(path); 

                DataSet dataSet = Excel.ExcelToDataSet(path);

                //检查DataSet里面的数据, 并获取新的数据
                string errorMessage = tel.CheckUnsaleData(dataSet.Tables[0]);

                if (errorMessage == string.Empty)
                {
                    if (tel.InsertUnsale(dataSet.Tables[0]))
                    {
                        return Json(new { IsSuccess = true, Message = "导入成功！" }, "text/html", JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { IsSuccess = true, Message = "导入失败,请检查填写是否有误！" }, "text/html", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = errorMessage }, "text/html", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { IsSuccess = false, Message = "文件格式不正确, 请下载模板作为参考！" }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }

        #region 分类管理
        public ActionResult TelType(int OwnerID)  
        {
            ViewData["OwnerID"] = OwnerID;
            return View();
        }

        public ActionResult LoadTelType(int page, int rows, string order, string sort, int OwnerID) 
        {
            BLL.BasicInfo.TelBook telbook = new BLL.BasicInfo.TelBook();

            var result = telbook.LoadAllTelTypeByPage(page, rows, order, sort,OwnerID);

            return Json(result);
        }

        public ActionResult TelTypeSave(C_TelType entity)
        {
            BLL.BasicInfo.TelBook tel = new BLL.BasicInfo.TelBook();
            if (ModelState.IsValid)
            {
                bool save;
                try
                {
                    save = tel.SaveTelType(entity);
                }
                catch (Exception ex)
                {
                    Log4Net.LogError("TelTypeSave", ex.ToString());
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

        public ActionResult TelTypeDelete(int id) 
        {
            BLL.BasicInfo.TelBook tel = new BLL.BasicInfo.TelBook();
            if (ModelState.IsValid)
            {
                bool delete;
                try
                {
                    delete = tel.DeleteTelType(id);
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
        #endregion
        #endregion

        #region 车辆信息

        public ActionResult AmbulanceList(int ActionId)
        {
            //int aID = int.Parse(ActionId);
            //获取页面权限
            //Anchor.FA.Utility.ButtonPower p = new ButtonPower();
            ////int wID = CurrentUser.ID;
            ////p.ActionIDRang = Anchor.FA.BLL.Organize.Range.GetRangeFromWorkerID(wID, aID);//旧方法
            //p.ActionIDRang = UserInfo.GetRange(aID);//新方法

            ////this.ViewData["EditEvent"] = p.IsHaveRangePower("EditEvent");
            ////this.ViewData["ViewEditRecord"] = p.IsHaveRangePower("ViewEditRecord");
            ////this.ViewData["EventPrint"] = p.IsHaveRangePower("EventPrint");
            ////this.ViewData["TaskPrint"] = p.IsHaveRangePower("TaskPrint");
            ////this.ViewData["EditAccept"] = p.IsHaveRangePower("EditAccept");
            ////this.ViewData["EditTask"] = p.IsHaveRangePower("EditTask");
            ////this.ViewData["Listen"] = p.IsHaveRangePower("Listen");
            ////this.ViewData["SoundConnect"] = p.IsHaveRangePower("SoundConnect");
            this.ViewData["ActionId"] = ActionId;
            return View();
        }
        /// <summary>
        /// 查看
        /// </summary>
        public ActionResult AmbulanceInfo(string id)
        {
            BLL.BasicInfo.Ambulance ambulance = new BLL.BasicInfo.Ambulance();
            ambulance.GetAmbulance(id);
            this.ViewData["entity"] = ambulance.GetAmbulance(id); ;
            return View();
        }
        /// <summary>
        /// 修改等级
        /// </summary>
        public ActionResult AmbulanceLevelSave(string Code, int LevelId)
        {
            BLL.BasicInfo.Ambulance ambulance = new BLL.BasicInfo.Ambulance();
            TAmbulance am = Anchor.FA.BLL.BasicInfo.Ambulance.GetAmbulanceInfo(Code);
            am.车辆等级编码 = LevelId;
            if (ambulance.Save(am))
            {
                return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { IsSuccess = false, Message = "保存失败" }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 等级
        /// </summary>
        public ActionResult AmbulanceLevelEdit(string id,int ActionId)
        {
            BLL.BasicInfo.Ambulance ambulance = new BLL.BasicInfo.Ambulance();
            //ambulance.GetAmbulance(id);
            this.ViewData["entity"] = ambulance.GetAmbulance(id);
            this.ViewData["ActionId"] = ActionId;
            return View();
        }
        /// <summary>
        /// 人员上下班
        /// </summary>
        public ActionResult AmbulancePersonSign(string id, int ActionId)
        {
            BLL.BasicInfo.Ambulance ambulance = new BLL.BasicInfo.Ambulance();
            //ambulance.GetAmbulance(id);
            this.ViewData["entity"] = ambulance.GetAmbulance(id);
            this.ViewData["ActionId"] = ActionId;
            return View();
        }
        /// <summary>
        /// 改车辆状态
        /// </summary>
        public ActionResult AmbulanceStateChange(string id, int ActionId)
        {
            BLL.BasicInfo.Ambulance ambulance = new BLL.BasicInfo.Ambulance();
            //ambulance.GetAmbulance(id);
            this.ViewData["entity"] = ambulance.GetAmbulance(id);
            this.ViewData["State"] = ambulance.LoadAmbulanceStateInfo();

            this.ViewData["ActionId"] = ActionId;
            return View();
        }


        /// <summary>
        /// 绑定未上班人员
        /// </summary>
        public ActionResult BindNotWorkPerson(string personTypes, string stationCode,int ActionId)
        {
            //获取页面权限
            Anchor.FA.Utility.ButtonPower p = new ButtonPower();
            p.ActionIDRang = UserInfo.GetRange(ActionId);//新方法
            int[] pt;
            string[] sc;
            if (personTypes == "")
                pt = new int[]{3,4,5,6,7};
            else
                pt = new int[]{Convert.ToInt32(personTypes)};
            if(p.IsHaveRangePower("OnDutyAll"))
            {
                if (stationCode == "")
                    sc = new string[]{};
                else
                    sc = new string[]{stationCode};
            }
            else if (p.IsHaveRangePower("OnDutyStation"))
            {
                if (stationCode == "")
                    sc = UserInfo.Sta;
                else
                    sc = new string[]{stationCode};
            }
            else
            {
                return null;
            }

            Anchor.FA.BLL.BasicInfo.Ambulance ambulance=new Anchor.FA.BLL.BasicInfo.Ambulance();

            return this.Json(ambulance.BindNotWorkPerson(pt, sc));

        }
        /// <summary>
        /// 绑定上班人员
        /// </summary>
        public ActionResult BindWorkPerson(string AmbCode)
        {
            int[] pt= new int[] { 3, 4, 5, 6, 7 };
            //var result = Anchor.FA.BLL.BasicInfo.Person.GetPersons(pt, new string[] { }, AmbCode, null, true).OrderBy(per => per.姓名).OrderBy(per => per.类型编码);

            Anchor.FA.BLL.BasicInfo.Ambulance ambulance=new Anchor.FA.BLL.BasicInfo.Ambulance();

            return this.Json(ambulance.BindWorkPerson(pt, AmbCode));
            
        }

        /// <summary>
        /// 上班
        /// </summary>
        public ActionResult OnDuty(string AmbCode, string[] personCode)
        {
            //this.UserInfo.WRol.Select(t=>t.)
            BLL.BasicInfo.Ambulance ambulance = new BLL.BasicInfo.Ambulance();
            if (!UserInfo.WPer.Any())
            {
                return this.Json("没有FA的人员编码");
            }
            string PersonId = UserInfo.WPer.First().编码;
            string result = "";
            foreach(string p in personCode)
            {
                result = ambulance.AmbulancePersonCheckIn(p, AmbCode, 5, PersonId, DateTime.Now);
                if (result != "")
                {
                    return this.Json("错误！"+result);
                }
            }
            return this.Json(result);
        }        
        /// <summary>
        /// 下班
        /// </summary>
        public ActionResult OffDuty(string AmbCode, string[] personCode)
        {
            //this.UserInfo.WRol.Select(t=>t.)
            BLL.BasicInfo.Ambulance ambulance = new BLL.BasicInfo.Ambulance();
            if (!UserInfo.WPer.Any())
            {
                return this.Json("没有FA的人员编码");
            }
            string PersonId = UserInfo.WPer.First().编码;
            string result = "";
            foreach (string p in personCode)
            {
                result = ambulance.AmbulancePersonCheckOut(p, AmbCode, 5, PersonId, DateTime.Now);
                if (result != "")
                {
                    return this.Json("错误！" + result);
                }
            }
            return this.Json(result);
        }

        /// <summary>
        /// 修改车辆状态
        /// </summary>
        public ActionResult ModifyAmbulanceState(string AmbCode,int workStateCode)
        {
            //this.UserInfo.WRol.Select(t=>t.)
            BLL.BasicInfo.Ambulance ambulance = new BLL.BasicInfo.Ambulance();
            if (!UserInfo.WPer.Any())
            {
                return this.Json("没有FA的人员编码");
            }
            string PersonId = UserInfo.WPer.First().编码;
            var result = ambulance.ModifyState(AmbCode, workStateCode, DateTime.Now,PersonId);
            return this.Json(result);
        }



        ///// <summary>
        ///// 加载所有车辆等级
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult LoadAmbulanceStateInfo()
        //{
        //    BLL.BasicInfo.Ambulance ambulance = new BLL.BasicInfo.Ambulance();
        //    var result = ambulance.LoadAmbulanceStateInfo();
        //    return Json(result);
        //}

        /// <summary>
        /// 搜索事件
        /// </summary>
        public ActionResult AmbulanceListSearch(string RealCode, string AmbNum, string Station, string AmbType, string AmbGroup
            , int page, int rows, string order, string sort, int ActionId)
        {
            //获取页面权限
            Anchor.FA.Utility.ButtonPower p = new ButtonPower();
            p.ActionIDRang = UserInfo.GetRange(ActionId);//新方法
            BLL.BasicInfo.Ambulance am = new BLL.BasicInfo.Ambulance();
            var result = am.AmbulanceListSearch(RealCode, AmbNum, Station, AmbType, AmbGroup
            , page, rows, order, sort, true, p, UserInfo);
            return this.Json(result);
        }
        public ActionResult Ambulance()
        {
            return View();
        }

        public ActionResult Ambulance2()
        {
            return View();
        }
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public ActionResult AmbulanceLoad(int page, int rows, string order, string sort)
        {
            BLL.BasicInfo.Ambulance ambulance = new BLL.BasicInfo.Ambulance();

            var result = ambulance.LoadAllAmbulanceByPage(page, rows, order, sort);

            return Json(result);
        }

        public ActionResult AmbulanceLoad()
        {
            BLL.BasicInfo.Ambulance ambulance = new BLL.BasicInfo.Ambulance();

            var result = ambulance.LoadAllAmbulance();

            return Json(result);
        }

        /// <summary>
        /// 加载本人可选分站以供选择
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadOnDutyStations(int ActionId)
        {
            //获取页面权限
            Anchor.FA.Utility.ButtonPower p = new ButtonPower();
            p.ActionIDRang = UserInfo.GetRange(ActionId);//新方法
            BLL.BasicInfo.Ambulance ambulance = new BLL.BasicInfo.Ambulance(); List<TStation> result;
            string OnDuty = p.GetGroupRangePower("OnDuty");
            switch (OnDuty)
            {
                case "OnDutyAll":
                    result = ambulance.LoadAllStations();
                    break;
                case "OnDutyStation":
                    result = UserInfo.WSta;
                    break;
                default:
                    result = new List<TStation>();
                    break;

            }
            return Json(result);
        }


        /// <summary>
        /// 加载所有分站以供选择
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadAllStations(int ActionId)
        {
            BLL.BasicInfo.Ambulance ambulance = new BLL.BasicInfo.Ambulance();

            //获取页面权限
            Anchor.FA.Utility.ButtonPower p = new ButtonPower();
            p.ActionIDRang = UserInfo.GetRange(ActionId);//新方法
            BLL.BasicInfo.Ambulance am = new BLL.BasicInfo.Ambulance();

            var result = ambulance.LoadAllStations(p,UserInfo);
            return Json(result);
        }
        /// <summary>
        /// 加载所有车辆等级
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadAllLevels()
        {
            BLL.BasicInfo.Ambulance ambulance = new BLL.BasicInfo.Ambulance();
            var result = ambulance.LoadAllLevels();
            return Json(result);
        }
        /// <summary>
        /// 加载所有车辆类型
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadAllTypes()
        {
            BLL.BasicInfo.Ambulance ambulance = new BLL.BasicInfo.Ambulance();
            var result = ambulance.LoadAllTypes();
            return Json(result);
        }
        /// <summary>
        /// 加载所有车辆分组类型
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadAllGroups()
        {
            BLL.BasicInfo.Ambulance ambulance = new BLL.BasicInfo.Ambulance();
            var result = ambulance.LoadAllGroups();
            return Json(result);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AmbulanceEdit(string id)
        {
            BLL.BasicInfo.Ambulance ambulance = new BLL.BasicInfo.Ambulance();
            TAmbulance am = ambulance.Edit(id) as TAmbulance;

            this.ViewData["entity"] = am;

            ViewData["station"] = am.分站编码;
            ViewData["level"] = am.车辆等级编码;
            ViewData["type"] = am.车辆类型编码;

            return View();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult AmbulanceSave(TAmbulance entity)
        {
            BLL.BasicInfo.Ambulance am = new BLL.BasicInfo.Ambulance();
            if (ModelState.IsValid)
            {
                bool save;
                try
                {
                    save = am.Save(entity);
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
        /// 删除
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public ActionResult AmbulanceDelete(IList<string> idList)
        {
            BLL.BasicInfo.Ambulance am = new BLL.BasicInfo.Ambulance();
            if (ModelState.IsValid)
            {
                bool delete;
                try
                {
                    delete = am.Delete(idList);
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
        /// 搜索事件
        /// </summary>
        public ActionResult AmbulanceSearch(string RealCode, string AmbNum, string Station, string AmbType, string AmbGroup
            , int page, int rows, string order, string sort)
        {
            BLL.BasicInfo.Ambulance am = new BLL.BasicInfo.Ambulance();
            var result = am.AmbulanceSearch( RealCode,  AmbNum,  Station,  AmbType,  AmbGroup
            ,  page,  rows,  order,  sort,null);
            return this.Json(result);
        }
        /// <summary>
        /// 搜索事件
        /// </summary>
        public ActionResult AmbulanceSearchActive(string RealCode, string AmbNum, string Station, string AmbType, string AmbGroup
            , int page, int rows, string order, string sort)
        {
            BLL.BasicInfo.Ambulance am = new BLL.BasicInfo.Ambulance();
            var result = am.AmbulanceSearch(RealCode, AmbNum, Station, AmbType, AmbGroup
            , page, rows, order, sort, true);
            return this.Json(result);
        }
        #endregion

        #region 电话流水

        public ActionResult TelLog()
        {
            string beginDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HH:mm:ss");

            this.ViewData["BeginDate"] = beginDate;
            this.ViewData["EndDate"] = endDate;
            this.ViewData["Time"] = time;
            this.ViewData["RecordingPlayPage"] = AppConfig.RecordingPlayPage;
            this.ViewData["ActionId"] = Request.QueryString["ActionId"];
            return View();
        }

        public ActionResult TelLogLoad(DateTime begin, DateTime end, int page, int rows, string order, string sort, int ActionId)
        {
            Anchor.FA.Utility.ButtonPower p = new ButtonPower();
            p.ActionIDRang = UserInfo.GetRange(ActionId);//新方法

            BLL.BasicInfo.TelLog tellog = new BLL.BasicInfo.TelLog();
            var result = tellog.TelLogSearch(begin, end, "", "", "", "", "", page, rows, order, sort, p,UserInfo);

            return Json(result);
        }

        public ActionResult TelLogSearch(DateTime begin, DateTime end, string tel, string rec, string op, string res, string des,
            int page, int rows, string order, string sort,int ActionId)
        {
            BLL.BasicInfo.TelLog tellog = new BLL.BasicInfo.TelLog();

            Anchor.FA.Utility.ButtonPower p = new ButtonPower();
            p.ActionIDRang = UserInfo.GetRange(ActionId);//新方法

            var result = tellog.TelLogSearch(begin, end, tel, rec, op, res, des, page, rows, order, sort, p, UserInfo);

            return Json(result);
        }
        public ActionResult LoadAllRecordTypes()
        {
            BLL.BasicInfo.TelLog tellog = new BLL.BasicInfo.TelLog();

            var result = tellog.GetAllRecordTypes();

            return Json(result);
        }
        public ActionResult LoadAllOperatorTypes()
        {
            BLL.BasicInfo.TelLog tellog = new BLL.BasicInfo.TelLog();

            var result = tellog.GetAllOperatorTypes();

            return Json(result);
        }
        public ActionResult LoadAllResult()
        {
            BLL.BasicInfo.TelLog tellog = new BLL.BasicInfo.TelLog();

            var result = tellog.GetAllResult();

            return Json(result);
        }
        public ActionResult LoadAllDesks()
        {
            BLL.BasicInfo.TelLog tellog = new BLL.BasicInfo.TelLog();

            var result = tellog.GetAllDesks();

            return Json(result);
        }
        #endregion

        #region 录音关联

        #region 保存
        public ActionResult UnLinkCalls(string CallType, string EventCode, string Desk, string Time)
        {

            DateTime t = Convert.ToDateTime(Time);
            var result = BLL.BasicInfo.AlarmEvent.UnLinkCalls(t, Desk, CallType);
            return Json(result);
        }
        public ActionResult LinkCalls(string CallType, string EventCode, string Desk, string Time)
        {
            DateTime t = Convert.ToDateTime(Time);
            var result = BLL.BasicInfo.AlarmEvent.LinkCalls(t, Desk, EventCode, CallType);
            return Json(result);
        } 
        #endregion
        #region 弹出页面
        /// <summary>
        /// 通话类型修改 弹出页面
        /// </summary>
        public ActionResult AlterCallType(string EventCode, string Desk, string Time, string type)
        {
            this.ViewData["EventCode"] = EventCode;
            this.ViewData["Desk"] = Desk;
            this.ViewData["Time"] = Time;
            this.ViewData["type"] = type;
            return View();
        }

        #endregion
        #region 相关下拉菜单

        /// <summary>
        /// 关联到事件相关 通话类型
        /// </summary>
        public ActionResult LoadAlarmCallType()
        {
            BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();
            var result = alarm.LoadAlarmCallType();
            return Json(result);
        }
        /// <summary>
        /// 取消关联到事件 通话类型
        /// </summary>
        public ActionResult LoadCallType()
        {
            BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();
            var result = alarm.LoadCallType();
            return Json(result);
        }
        #endregion
        
        /// <summary>
        /// 通话类型修改 弹出页面
        /// </summary>
        public ActionResult MediaLink(string EventCode)
        {
            string beginDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HH:mm:ss");

            this.ViewData["BeginDate"] = beginDate;
            this.ViewData["EndDate"] = endDate;
            this.ViewData["Time"] = time;
            this.ViewData["EventCode"] = EventCode;
            this.ViewData["RecordingPlayPage"] = AppConfig.RecordingPlayPage;
            return View();
        }
        
        public ActionResult GetAlarmCallOthers(int page,int rows, DateTime m_BeginTime, DateTime m_EndTime,
            string deskNumber, string attemperCode, string callTypeCode, string callNumber, string recordNumber, string isCallOut
            , string remark,int ActionId)
        {
            //int pageIndex = 1; int pageSize = 20;
            BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();

            Anchor.FA.Utility.ButtonPower p = new ButtonPower();
            p.ActionIDRang = UserInfo.GetRange(ActionId);//新方法

            var result = alarm.GetAlarmCallOthers(page, rows, m_BeginTime, m_EndTime,
             deskNumber, attemperCode, callTypeCode, callNumber, recordNumber, isCallOut
            , remark,p,UserInfo);
            if (result == null)
            {
                return View();
            }
            return Json(result);
        }


        #endregion


        public ActionResult StationMsg(string id)
        {
            ViewData["alarmEventCode"] = id;
            return View();
        }

        //public ActionResult StationMsgSearch()
        //{
        //    string alarmEventCode= Request.QueryString["alarmEventCode"];
        //    int page = Convert.ToInt32(Request.QueryString["page"]);
        //    int rows = Convert.ToInt32(Request.QueryString["rows"]);
        //    //获取页面权限
        //    //Anchor.FA.Utility.ButtonPower p = new ButtonPower();
        //    //p.ActionIDRang = UserInfo.GetRange(ActionId);//新方法
        //    var result = BLL.BasicInfo.AlarmEvent.GetStationMsgs(rows, page, alarmEventCode);
        //    return this.Json(result);
        //}
        public ActionResult StationMsgSearch(string alarmEventCode, int page, int rows)
        {
            //获取页面权限
            //Anchor.FA.Utility.ButtonPower p = new ButtonPower();
            //p.ActionIDRang = UserInfo.GetRange(ActionId);//新方法
            var result = BLL.BasicInfo.AlarmEvent.GetStationMsgs(rows, page, alarmEventCode);
            return this.Json(result);
        }



        #region 事件管理
        #region 保存

        public ActionResult AlarmEventSave(string EventCode, string EvetnName, string Area, string EventSource, string AccidentLevel, string EventType, string AccidentType)
        {
            TAlarmEvent entity = new TAlarmEvent();

            entity.事件编码 = EventCode;
            entity.事件名称 = EvetnName;
            entity.区域 = Area;
            entity.事件来源编码 = intCode(EventSource);
            entity.事故等级编码 = intCode(AccidentLevel);
            entity.事件类型编码 = intCode(EventType);
            entity.事故类型编码 = intCode(AccidentType);
            //这里用 首次调度员编码 来传递TModifyRecord表中需要的 操作员编码
            string workID=User.Identity.Name.Split('|')[0];
            entity.首次调度员编码 = workID;
            BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();
            var result = alarm.Update(entity);
            return Json(result);
        }
        public ActionResult AcceptEventSave(string EventCode
            , string AcceptOrder
            , string AlarmReason
            , string PatientName
            , string DropDownList_Sex
            , string ComboBox_Age
            , string ComboBox_National
            , string ComboBox_Folk
            , string Judge
            , string LinkMan
            , string LinkTel
            , string Extension
            , string LocalAddr
            , string WaitAddr
            , string SendAddr
            , int IsNeedLitter
            , string Remark

            , string DropDownList_IllStateId
            , string DropDownList_LocalAddrTypeId
            , string DropDownList_SendAddrTypeId
            , string PatientCount
            , string ComboBox_SpecialNeed
            , string ComboBox_BackUpOne
            , string ComboBox_BackUpTwo
            , string MPDSRemark  
            )
        {
            TAcceptEvent entity = new TAcceptEvent();

            entity.事件编码 = EventCode;
            entity.受理序号 = Convert.ToInt32(AcceptOrder);
            entity.主诉 = AlarmReason;
            entity.患者姓名 = PatientName;
            entity.性别 = DropDownList_Sex;
            entity.年龄 = ComboBox_Age;
            entity.国籍 = ComboBox_National;
            entity.民族 = ComboBox_Folk;
            entity.病种判断 = Judge;
            entity.联系人 = LinkMan;
            entity.联系电话 = LinkTel;
            entity.分机 = Extension;
            entity.现场地址 = LocalAddr;
            entity.等车地址 = WaitAddr;
            entity.送往地点 = SendAddr;
            entity.是否需要担架 = Convert.ToBoolean(IsNeedLitter);
            entity.备注 = Remark;

            if (DropDownList_IllStateId!="")
                entity.病情编码 = Convert.ToInt32(DropDownList_IllStateId);
            if (DropDownList_LocalAddrTypeId != "")
                entity.往救地点类型编码 = Convert.ToInt32(DropDownList_LocalAddrTypeId);
            if (DropDownList_SendAddrTypeId != "")
                entity.送往地点类型编码 = Convert.ToInt32(DropDownList_SendAddrTypeId);
            if (PatientCount != "")
                entity.患者人数 = Convert.ToInt32(PatientCount);


            entity.特殊要求 = ComboBox_SpecialNeed;
            entity.保留字段1 = ComboBox_BackUpOne;
            entity.保留字段2 = ComboBox_BackUpTwo;
            entity.MPDS备注 = MPDSRemark;

            //这里用 责任受理人编码 来传递TModifyRecord表中需要的 操作员编码
            string workID = User.Identity.Name.Split('|')[0];
            entity.责任受理人编码 = workID;

            var result = AcceptEvent.Update(entity);
            return Json(result);
        }
        public ActionResult TaskSave(
            //string Code
            //, string IsNormalFinish
            //, string AbnormalReasonId
            //, string Remark
            //, string AmbulanceCode

            TTask entity
            , string Driver
            //, string DriverName
            , string Doctor
            //, string DoctorName
            , string Nurse
            //, string NurseName
            , string Litter
            //, string LitterName
            , string Salver
            , string AmbulanceStateTime1
            , string AmbulanceStateTime2
            , string AmbulanceStateTime3
            , string AmbulanceStateTime4
            , string AmbulanceStateTime5
            , string AmbulanceStateTime6
            , string AmbulanceStateTime7
            , string IsNormalFinish
            ,int ActionId
            )
        {
            string workID=User.Identity.Name.Split('|')[0];

            entity.备注 = entity.备注 == null ? "" : entity.备注;
            entity.司机 = entity.司机 == null ? "" : entity.司机;
            entity.医生 = entity.医生 == null ? "" : entity.医生;
            entity.护士 = entity.护士 == null ? "" : entity.护士;
            entity.担架工 = entity.担架工 == null ? "" : entity.担架工;
            entity.抢救员 = entity.抢救员 == null ? "" : entity.抢救员;

            //这种entity传值 string类型的不传值 就会为null
            //这种entity传值 bool类型的我搞不懂
            entity.是否正常结束 = IsNormalFinish == "1" ? true : false;

            Anchor.FA.Utility.ButtonPower p = new ButtonPower();
            p.ActionIDRang = UserInfo.GetRange(ActionId);//新方法

            var result = Task.Update(entity, Driver, Doctor, Nurse, Litter, Salver, AmbulanceStateTime1, AmbulanceStateTime2, AmbulanceStateTime3, AmbulanceStateTime4, AmbulanceStateTime5, AmbulanceStateTime6, AmbulanceStateTime7
                , p, UserInfo);
            return Json(result);
            //return Json(true);
        }

        private int intCode(string stringCode)
        {
            if (string.IsNullOrEmpty(stringCode))
            {
                return -1;
            }
            return Convert.ToInt32(stringCode);
        }
        #endregion
        #region 弹出页面
        /// <summary>
        /// 事件总明细 显示 tab
        /// </summary>
        public ActionResult AccLoad(string id,int ActionId)
        {
            BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();

            C_AlarmEventInfo tae;
            List<C_AccInfo> tacLs;
            List<C_TaskInfoDetail> ttLs;
            List<C_AmbulanceStateTimeInfo> tastLs;
            List<C_AlarmCallInfo> acLs;
            string result = alarm.getAlarmAllShow(id, out tae, out tacLs, out ttLs, out tastLs, out acLs);
            this.ViewData["result"] = result;

            C_AlarmEventDetail taall = new C_AlarmEventDetail(tae, tacLs, ttLs, tastLs);
            this.ViewData["taall"] = taall;
            this.ViewData["acLs"] = acLs;

            List<TParameterAcceptInfo> tpaLs = alarm.LoadParameterAcceptInfo();
            this.ViewData["tpaLs"] = tpaLs;
            List<TZAmbulanceState> zasLs = alarm.LoadAmbulanceStateInfo();
            this.ViewData["zasLs"] = zasLs;


            //int aID = int.Parse(ActionId);
            //int wID = CurrentUser.ID;
            //获取页面权限
            Anchor.FA.Utility.ButtonPower p = new ButtonPower();
            //p.ActionIDRang = Anchor.FA.BLL.Organize.Range.GetRangeFromWorkerID(wID, aID);
            p.ActionIDRang = UserInfo.GetRange(ActionId);//新方法
            this.ViewData["EditEvent"] = p.IsHaveRangePower("EditEvent");
            this.ViewData["ViewEditRecord"] = p.IsHaveRangePower("ViewEditRecord");
            this.ViewData["EventPrint"] = p.IsHaveRangePower("EventPrint");
            this.ViewData["TaskPrint"] = p.IsHaveRangePower("TaskPrint");
            this.ViewData["EditAccept"] = p.IsHaveRangePower("EditAccept");
            this.ViewData["EditTask"] = p.IsHaveRangePower("EditTask");
            this.ViewData["Listen"] = p.IsHaveRangePower("Listen");
            this.ViewData["SoundConnect"] = p.IsHaveRangePower("SoundConnect");
            this.ViewData["RecordingPlayPage"] = AppConfig.RecordingPlayPage;
            return View();
        }
        /// <summary>
        /// 受理弹出页
        /// </summary>
        public ActionResult AcceptEventInfoEdit(string eventCode, int acceptOrder)
        {
            C_AccInfo acInfo = AcceptEvent.GetC_AccInfo(eventCode, acceptOrder);
            this.ViewData["acInfo"] = acInfo;
            BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();
            List<TParameterAcceptInfo> tpaLs = alarm.LoadParameterAcceptInfo();
            this.ViewData["tpaLs"] = tpaLs;
            return View();
        }
        /// <summary>
        /// 事件弹出页
        /// </summary>
        public ActionResult AlarmEventInfoEdit(string eventCode)
        {
            BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();
            C_AlarmEventInfo aeInfo = alarm.GetAlarmInfo(eventCode);//本来就是个静态方法 偏要这么做
            this.ViewData["aeInfo"] = aeInfo;
            List<TParameterAcceptInfo> tpaLs = alarm.LoadParameterAcceptInfo();
            this.ViewData["tpaLs"] = tpaLs;
            return View();
        }
        /// <summary>
        /// 任务等弹出页
        /// </summary>
        public ActionResult TaskInfoEdit(string Code, string AbnormalReasonName)
        {
            int ActionId =Convert.ToInt32(Request.QueryString["ActionId"]); 
            Anchor.FA.Utility.ButtonPower p = new ButtonPower();
            p.ActionIDRang = UserInfo.GetRange(ActionId);//新方法

            this.ViewData["EditTimeNode"] = p.IsHaveRangePower("EditTimeNode");//如果 EditTimeNode （各时间字段可编辑） ;
            //C_TaskInfoDetail tInfo = Task.GetC_TaskInfoDetail(Code);
            TTask tInfo = Task.GetTaskInfo(Code);
            this.ViewData["tInfo"] = tInfo;
            this.ViewData["AbnormalReasonName"] = AbnormalReasonName;
            //List<C_AmbulanceStateTimeInfo> absLs = Task.GetAmbulanceStateTimeLs(Code);
            //this.ViewData["absLs"] = absLs;
            BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();
            List<TZAmbulanceState> zasLs = alarm.LoadAmbulanceStateInfo();
            this.ViewData["zasLs"] = zasLs;

            List<TTaskPersonLink> taskPersonLs = TaskPersonLink.GetTaskPersons(Code);

            var LtDriver = taskPersonLs.Where(t => t.人员类型编码 == 3).Select(t => t.人员编码);
            var LtDoctor = taskPersonLs.Where(t => t.人员类型编码 == 4).Select(t => t.人员编码);
            var LtNures = taskPersonLs.Where(t => t.人员类型编码 == 5).Select(t => t.人员编码);
            var LtLitter = taskPersonLs.Where(t => t.人员类型编码 == 6).Select(t => t.人员编码);
            var LtSalver = taskPersonLs.Where(t => t.人员类型编码 == 7).Select(t => t.人员编码);

            this.ViewData["LtDriver"] = ListToString(LtDriver);
            this.ViewData["LtDoctor"] = ListToString(LtDoctor);
            this.ViewData["LtNures"] = ListToString(LtNures);
            this.ViewData["LtLitter"] = ListToString(LtLitter);
            this.ViewData["LtSalver"] = ListToString(LtSalver);

            return View();
        }        
        /// <summary>
        /// 录音弹出页面(MediaPlayer播放器)
        /// </summary>
        public ActionResult MediaPlayer(string RecordNumber,string CallTime)
        {
            //string strMapPath = "Music/sayoldtime.mp3";
            string strMapPath = "";
            if (RecordNumber != "" && CallTime != "")
            {
                DateTime CallTime_Date = Convert.ToDateTime(CallTime);
                string year = String.Format("{0:yyyy}", CallTime_Date);
                string month = String.Format("{0:yyyyMM}", CallTime_Date);
                string day = String.Format("{0:yyyyMMdd}", CallTime_Date);
                strMapPath = Anchor.FA.Utility.AppConfig.MediaClientPath + "/" + year + "/" + month + "/" + day + "/" + Server.UrlEncode(RecordNumber);

            }
            this.ViewData["strMapPath"] = strMapPath;
            return View();
        }

        /// <summary>
        /// 录音弹出页面(QuickTimePlayer播放器)
        /// </summary>
        public ActionResult QuickTimePlayer(string RecordNumber, string CallTime)
        {
            //string strMapPath = "Music/sayoldtime.mp3";
            string strMapPath = "";
            if (RecordNumber != "" && CallTime != "")
            {
                DateTime CallTime_Date = Convert.ToDateTime(CallTime);
                string year = String.Format("{0:yyyy}", CallTime_Date);
                string month = String.Format("{0:yyyyMM}", CallTime_Date);
                string day = String.Format("{0:yyyyMMdd}", CallTime_Date);
                strMapPath = Anchor.FA.Utility.AppConfig.MediaClientPath + "/" + year + "/" + month + "/" + day + "/" + Server.UrlEncode(RecordNumber);

            }
            this.ViewData["strMapPath"] = strMapPath;
            return View();
        }

        /// <summary>
        /// 修改记录弹出页面
        /// </summary>
        public ActionResult ModifyRecord(string eventCode)
        {
            long total;
            List<C_ModifyRecord> mrLs=BLL.BasicInfo.AlarmEvent.GetModifyRecord(eventCode,out total);
            this.ViewData["mrLs"] = mrLs;
            this.ViewData["total"] = total;
            return View();
        }
        private static String ListToString(IEnumerable<string> Lt)
        {
            StringBuilder Bu = new StringBuilder();
            foreach (string d in Lt)
            {
                Bu.Append("," + d);
            }
            if (Bu.Length > 0)
            {

                Bu.Remove(0, 1);
            }
            return Bu.ToString();
        }
        #endregion
        #region 相关下拉菜单
        /// <summary>
        /// TZArea 区域下拉填充
        /// </summary>
        public ActionResult LoadAreas()
        {
            BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();
            var result = alarm.LoadAreas();
            return Json(result);
        }
        /// <summary>
        /// TZAlarmEventOrigin 事件来源填充 事故类型
        /// </summary>
        public ActionResult LoadAlarmOriTypes()
        {
            BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();
            var result = alarm.LoadAlarmOriTypes();
            return Json(result);
        }
        /// <summary>
        /// TZAccidentLevel 事故等级下拉填充
        /// </summary>
        public ActionResult LoadAccidentLevels()
        {
            BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();
            var result = alarm.LoadAccidentLevels();
            return Json(result);
        }
        /// <summary>
        /// TPerson 人员下拉填充
        /// </summary>
        public ActionResult GetPersonList(int personType, string stationCode, int isValid,int ActionId)
        {
            //获取页面权限
            Anchor.FA.Utility.ButtonPower p = new ButtonPower();
            p.ActionIDRang = UserInfo.GetRange(ActionId);//新方法

            var result = Anchor.FA.BLL.BasicInfo.Person.GetPersonList(personType, stationCode, isValid,p,UserInfo);
            return Json(result);
        }
        /// <summary>
        /// TPerson 调度员下拉填充
        /// </summary>
        public ActionResult LoadDis(int ActionId)
        {
            BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();

            Anchor.FA.Utility.ButtonPower p = new ButtonPower();
            p.ActionIDRang = UserInfo.GetRange(ActionId);//新方法

            var result = alarm.LoadDis(p,UserInfo);
            return Json(result);
        }
        /// <summary>
        /// TStation 分站下拉填充
        /// </summary>
        //public ActionResult LoadStations()
        //{
        //    BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();
        //    var result = alarm.LoadStations() as List<TStation>;

        //    return Json(result);
        //}
        /// <summary>
        /// TAmbulance 车辆下拉填充
        /// </summary>
        //public ActionResult LoadAlums()
        //{
        //    BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();
        //    var result = alarm.LoadAlums();
        //    return Json(result);
        //}
        /// <summary>
        /// TAmbulance 车辆下拉填充
        /// </summary>
        //public ActionResult GetAmbulanceByStationCode(string stationCode)
        //{
        //    var result = Task.GetAmbulanceByStationCode(stationCode);
        //    return Json(result);
        //}
        /// <summary>
        /// TZAlarmEventType 事件类型填充
        /// </summary>
        public ActionResult LoadAlarmTypes()
        {
            BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();
            var result = alarm.LoadAlarmTypes();
            return Json(result);
        }
        /// <summary>
        /// TZAlarmReason 主诉树填充
        /// </summary>
        public ActionResult GetAlarmReasonsTree()
        {
            var result = AcceptEvent.GetAlarmReasonsTree("-1");
            return Json(result);
        }
        /// <summary>
        /// TZTaskAbendReason 车辆异常结束原因树填充
        /// </summary>
        public ActionResult GetTaskAbendReasonTree()
        {
            var result = Task.GetTaskAbendReasonTree("0");
            return Json(result);
        }
        /// <summary>
        /// TZAccidentType 事故类型树填充
        /// </summary>
        public ActionResult GetAccidentTypeTree()
        {
            BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();
            var result = alarm.GetAccidentTypeTree("0");
            return Json(result);
        }
        /// <summary>
        /// TZAge 年龄下拉填充
        /// </summary>
        public ActionResult LoadAges()
        {
            var result = AcceptEvent.LoadAges();
            return Json(result);
        }
        /// <summary>
        /// TZNational 国籍可录入下拉菜单填充
        /// </summary>
        public ActionResult LoadNationals()
        {
            var result = AcceptEvent.LoadNationals();
            return Json(result);
        }
        /// <summary>
        /// TZFolk 民族可录入下拉菜单填充
        /// </summary>
        public ActionResult LoadFolks()
        {
            var result = AcceptEvent.LoadFolks();
            return Json(result);
        }

        /// <summary>
        /// TZIllState 病情判断可录入下拉菜单填充
        /// </summary>
        public ActionResult LoadIllStates()
        {
            var result = AcceptEvent.LoadIllStates();
            return Json(result);
        }

        /// <summary>
        /// TZLocalAddrType 往救地点 下拉菜单填充
        /// </summary>
        public ActionResult LoadLocalAddrTypes()
        {
            var result = AcceptEvent.LoadLocalAddrTypes();
            return Json(result);
        }
        /// <summary>
        /// TZSendAddrType 送往地点 下拉菜单填充
        /// </summary>
        public ActionResult LoadSendAddrTypes()
        {
            var result = AcceptEvent.LoadSendAddrTypes();
            return Json(result);
        }
        /// <summary>
        /// TZSpecialRequest 特殊要求 下拉菜单填充
        /// </summary>
        public ActionResult LoadSpecialRequests()
        {
            var result = AcceptEvent.LoadSpecialRequests();
            return Json(result);
        }
        /// <summary>
        /// TZBackupOne 保留字段1 下拉菜单填充
        /// </summary>
        public ActionResult LoadBackupOnes()
        {
            var result = AcceptEvent.LoadBackupOnes();
            return Json(result);
        }
        /// <summary>
        /// TZBackupTwo 保留字段2 下拉菜单填充
        /// </summary>
        public ActionResult LoadBackupTwos()
        {
            var result = AcceptEvent.LoadBackupTwos();
            return Json(result);
        }

        #endregion
        public ActionResult AlarmEvent()
        {
            string beginDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HH:mm:ss");

            this.ViewData["BeginDate"] = beginDate;
            this.ViewData["EndDate"] = endDate;
            this.ViewData["Time"] = time;
            this.ViewData["ActionId"] = Request.QueryString["ActionId"];
            this.ViewData["personcode"] = UserInfo.EmpNo[0];
            this.ViewData["ReviewUrl"] = AppConfig.GetStringConfigValue("ReviewUrl");//调度复核页面地址

            return View();
        }

        //public ActionResult AlarmEventLoad(DateTime begin, DateTime end, int page, int rows, string order, string sort)
        //{
        //    BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();
        //    var result = alarm.AlarmEventLoad(begin, end, page, rows, order, sort);

        //    return Json(result);
        //}


        public ActionResult AlarmEventSearch(DateTime begin, DateTime end, string c_begin, string c_end, string tel, string Addr, string Dri, 
            string Doc, string Nur, string Dis, string sta, string Alum, string type, string ori
            , string SuffererName, string ZhuSu, string SendAddress, string IllState, string AlarmEventCode
            , string IsTest, string Judge
            , int page, int rows, string order, string sort, int ActionId)
        {
            BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();
            //int aID=int.Parse(ActionId);
            int wID = CurrentUser.ID;
            //获取页面权限
            Anchor.FA.Utility.ButtonPower p = new ButtonPower();
            //p.ActionIDRang = Anchor.FA.BLL.Organize.Range.GetRangeFromWorkerID(wID, aID);
            p.ActionIDRang = UserInfo.GetRange(ActionId);//新方法
            //Anchor.FA.DAL.Organize.Range r = new Anchor.FA.DAL.Organize.Range(wID, aID);
            var result = alarm.AlarmEventSearch(begin, end, c_begin, c_end, tel, Addr, Dri, Doc, Nur, Dis, sta, Alum, type, ori, SuffererName, ZhuSu, SendAddress, IllState, AlarmEventCode, IsTest, Judge, page, rows, order, sort
                , p, wID);
            if (result == null)
            {
                return View();
            }
            return Json(result);
        }

        public ActionResult AccInfo(string code, string order)
        {
            //测试用
            //code = "2011051309061905";
            //order = "1";

            BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();
            this.ViewData["entity"] = alarm.GetAlarmInfo(code);
            this.ViewData["entity2"] = alarm.GetAccInfo(code, Convert.ToInt32(order));
            this.ViewData["Code"] = code;
            this.ViewData["Ord"] = order;
            this.ViewData["test"] = "test";
            return View();
        }
        public ActionResult AccTelgLoad(string code)
        {
            BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();
            return Json(alarm.GetAccTel(code));
        }
        public ActionResult AumLoad(string code, int ord)
        {
            BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();
            return Json(alarm.AumLoad(code, ord));
        }
        public ActionResult TaskInfo(string code)
        {
            BLL.BasicInfo.AlarmEvent alarm = new BLL.BasicInfo.AlarmEvent();
            this.ViewData["entity"] = alarm.GetTaskInfo(code);
            return View();
        }
        #endregion



        #region 公用数据
        public ActionResult Data()
        {
            return View();
        }

        public ActionResult DataType()
        {

            BLL.BasicInfo.CommonData data = new BLL.BasicInfo.CommonData();
            var result = data.DataType();
            return Json(result);
        }

        public ActionResult SearchLoadAll(string type)
        {
            BLL.BasicInfo.CommonData data = new BLL.BasicInfo.CommonData();
            var result = data.SearchLoadAll(type);
            return Json(result);
        }

        public ActionResult DataLoad(int page, int rows, string order, string sort, string type)
        {
            BLL.BasicInfo.CommonData data = new BLL.BasicInfo.CommonData();
            var result = data.LoadAllDataByPage(page, rows, order, sort, type);
            return Json(result);
        }

        public ActionResult DataEdit(int? id)
        {
            BLL.BasicInfo.CommonData data = new BLL.BasicInfo.CommonData();
            this.ViewData["entity"] = data.Edit(id);
            return View();
        }

        public ActionResult DataSave(G_DATA entity)
        {
            BLL.BasicInfo.CommonData data = new BLL.BasicInfo.CommonData();

            if (ModelState.IsValid)
            {
                bool save;

                try
                {
                    save = data.Save(entity);
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

        public ActionResult DataDelete(IList<int> idList)
        {
            BLL.BasicInfo.CommonData data = new BLL.BasicInfo.CommonData();
            if (ModelState.IsValid)
            {
                bool delete;
                try
                {
                    delete = data.Delete(idList);
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

        public ActionResult LoadNurse()
        {
            BLL.BasicInfo.CommonData data = new BLL.BasicInfo.CommonData();
            var result = data.LoadNurse();
            return Json(result);
        }

        public ActionResult GetDataByType(string type)
        {
            BLL.BasicInfo.CommonData data = new BLL.BasicInfo.CommonData();
            var result = data.GetDataByType(type);
            return Json(result);
        }
        #endregion

        #region 系统配置
        public ActionResult Config()
        {
            return View();
        }
        public ActionResult ConfigLoad(int page, int rows, string order, string sort)
        {
            BLL.BasicInfo.Config config = new BLL.BasicInfo.Config();

            var result = config.LoadAllActionByPage(page, rows, order, sort);

            return Json(result);
        }
        //public ActionResult ActionLoadAll()
        //{
        //    BLL.BasicInfo.Config action = new BLL.BasicInfo.Config();

        //    var result = action.GetAllAction();

        //    return Json(result);
        //}

        //public ActionResult ActionLoadTree()
        //{
        //    //BLL.Organize.Action action = new BLL.Organize.Action();
        //    //return this.Json(Aciton.GetMenu("0"));
        //    return this.Json(Anchor.FA.BLL.Organize.Action.GetMenuRange("0"));
        //}

        public ActionResult ConfigEdit(string key)
        {
            BLL.BasicInfo.Config config = new BLL.BasicInfo.Config();
            G_CONFIG cot = config.Edit(key) as G_CONFIG;
            this.ViewData["entity"] = cot;
            return View();
        }

        public ActionResult ConfigSave(G_CONFIG entity)
        {
            BLL.BasicInfo.Config config = new BLL.BasicInfo.Config();
            if (ModelState.IsValid)
            {
                bool save;
                try
                {
                    save = config.Save(entity);
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

        #endregion

        #region Export
        public ActionResult Export()
        {
            return View();
        }

        public JsonResult LoadViewAndFunction()
        {
            DataTable dt=BLL.BasicInfo.Export.LoadViewAndFunction();
            var result = (from p in dt.AsEnumerable()
                          select new
                          {
                              id = p.Field<int>("id"),
                              name = p.Field<string>("name"),
                              value = p.Field<string>("value"),
                              t = p.Field<string>("t")
                          }).ToList();
            //string result = Anchor.FA.Utility.JSONEncoderHelper.TableToXML(dt);
            //Newtonsoft.Json.JsonConvert a=new 
            //var result = JsonConvert(;

            JsonResult jr = this.Json(result);
            //jr.Data;
            //return this.Json(result);
            //return result;
            return this.Json(result);
        }
        //&fV[0]=2015-06-01 08:30:00&fV[1]=2015-06-19 08:30:00
        //&CaseV[0].ID=1&CaseV[0].radio1=and&CaseV[0].columnname=现库存&CaseV[0].radio2=>&CaseV[0].casetext=0&CaseV[0].ParentID=-1
        //&CaseV[1].ID=2&CaseV[1].radio1=and&CaseV[1].columnname=现库存&CaseV[1].radio2=<&CaseV[1].casetext=10000&CaseV[1].ParentID=1
        public ActionResult Export_Case(int id, string name, string value, string t, string[] fV, List<C_ExportUrlStringTree> CaseV)
        {
            this.ViewData["id"] = id;
            this.ViewData["name"] = name;
            this.ViewData["value"] = value;
            //this.ViewData["t"] = t;

            DataSet ds = BLL.BasicInfo.Export.GetDataSet(@"select a.name as name,b.name as typename
from syscolumns a 
left join systypes b on a.xtype = b.xusertype 
where a.id=@id and a.number=0

select stuff(a.name,1,1,'') as name,b.name as typename,deafultV=''
from syscolumns a 
left join systypes b on a.xtype = b.xusertype 
where a.id=@id and a.number=1"
                , new System.Data.SqlClient.SqlParameter("@id", id));

            int i = 0;
            if (fV!=null && fV.Count() > 0)
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    dr[2] = fV[i];
                    i++;
                }

            this.ViewData["GridView1"] = ds.Tables[0];
            this.ViewData["Repeater1"] = ds.Tables[1];
            this.ViewData["Case"] = BLL.BasicInfo.Export.ExportUrlListToTree(CaseV);

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Export_Show(string selectColumnsXml, string tableName, string tableNotes, string functionCaseXml, string inputCaseXml)
        {
            SqlParameterTool show;
            List<int> ColumnsNumber;
            List<string> ColumnsName;
            BLL.BasicInfo.Export.GetExportSql(selectColumnsXml, tableName, functionCaseXml, inputCaseXml, out show, out ColumnsNumber, out ColumnsName);
            //-----------------------------------------------------


            DataTable dt = BLL.BasicInfo.Export.GetDataSet(show.commandText.ToString()
                , show.commandParameters.ToArray()).Tables[0];

            this.ViewData["GridView1"] = dt;
            this.ViewData["ColumnsName"] = ColumnsName;
            this.ViewData["ColumnsNumber"] = ColumnsNumber;

            this.ViewData["tableName"] = tableName;
            this.ViewData["tableNotes"] = tableNotes;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Export_Excel(string selectColumnsXml, string tableName, string tableNotes, string functionCaseXml, string inputCaseXml)
        {
            SqlParameterTool show;
            List<int> ColumnsNumber;
            List<string> ColumnsName;
            BLL.BasicInfo.Export.GetExportSql(selectColumnsXml, tableName, functionCaseXml, inputCaseXml, out show, out ColumnsNumber, out ColumnsName);

            DataSet dsReport = BLL.BasicInfo.Export.GetDataSet(show.commandText.ToString()
                , show.commandParameters.ToArray());

            string folderPath = Server.MapPath("~/ExcelFile");

            if (!folderPath.EndsWith("/") && !folderPath.EndsWith("\\"))
            {
                folderPath += "/";
            }
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".xls";
            string excelPath = folderPath + "\\" + fileName;
            Utility.Excel.ExportToExcel(dsReport, excelPath);
            return File(excelPath, "application/ms-excel", fileName);
        }





        #endregion
    }
}

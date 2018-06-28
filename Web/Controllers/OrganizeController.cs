using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Anchor.FA.Model;
using Anchor.FA.BLL.IBLL;
using Anchor.FA.Utility;
using System.Data;
using System.IO;

namespace Anchor.FA.Web.Controllers
{
    public class OrganizeController : BaseController
    {
        #region 组织管理
        public ActionResult Unit()
        {
            Anchor.FA.Utility.ButtonPower p = new ButtonPower();

            p.ActionIDRang = UserInfo.GetRange(int.Parse(Request.QueryString["ActionId"]));

            string Search = p.GetGroupRangePower("searchBound");

            switch (p.GetGroupRangePower("searchBound"))
            {
                case "SearchAll":
                    ViewData["orgId"] = 1;
                    break;
                case "SearchCenter"://查找分中心
                    ViewData["orgId"] = UserInfo.CenterID;
                    break;
                case "SearchOrganization"://查找分站
                    ViewData["orgId"] = String.Join(",",UserInfo.Org);
                    break;
                default://没有设置查询权限
                    return null;
                //break;
            }

            return View();
        }


        //public ActionResult UnitPage()
        //{
        //    return View();
        //}

        public ActionResult Center()
        {
            return View();
        }

        public ActionResult Station()
        {
            return View();
        }

        public ActionResult Branch()
        {
            return View();
        }

        public ActionResult UnitTree(string orgId)
        {
            try
            {
                BLL.Organize.Organize org = new BLL.Organize.Organize();
                List<C_ORGANIZE_TREE> cot = org.GetTree(orgId);

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

        #region 中心


        public ActionResult CenterLoad()
        {
            BLL.Organize.Center center = new BLL.Organize.Center();

            var result = center.LoadAllCenter();

            return Json(result);
        }

        public ActionResult CenterTypeLoad()
        {
            BLL.Organize.Center center = new BLL.Organize.Center();

            var result = center.LoadAllCenterType();

            return Json(result);
        }

        public ActionResult CenterSave(TCenter entity)
        {
            BLL.Organize.Center center = new BLL.Organize.Center();

            int managerId = int.Parse(Request.Form["ManagerID"]);

            if (ModelState.IsValid)
            {
                bool save = center.Save(entity, managerId);

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

        public ActionResult CenterDelete(IList<int> idList)
        {
            BLL.Organize.Center center = new BLL.Organize.Center();

            bool result = center.Delete(idList);

            if (result)
            {
                return Json(new { IsSuccess = true, Message = "删除成功" });
            }
            else
            {
                return Json(new { IsSuccess = false, Message = "删除失败" });
            }
        }
        #endregion

        #region 分站
        public ActionResult StationLoad(int centerId)
        {
            BLL.Organize.Station station = new BLL.Organize.Station();

            var result = station.LoadAllStation(centerId);

            return Json(result);
        }

        public ActionResult StationTypeLoad()
        {
            BLL.Organize.Station station = new BLL.Organize.Station();

            var result = station.LoadAllStationType();

            return Json(result);
        }

        public ActionResult StationSave(TStation entity)
        {
            BLL.Organize.Station station = new BLL.Organize.Station();

            int managerId = int.Parse(Request.Form["ManagerID"]);

            entity.IP地址 = entity.IP地址 ?? string.Empty;
            entity.电话号码 = entity.电话号码 ?? string.Empty;
            entity.通信标识 = entity.通信标识 ?? string.Empty;
            entity.所属区域 = entity.所属区域 ?? string.Empty;

            if (ModelState.IsValid)
            {
                bool save = station.Save(entity, managerId);

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

        public ActionResult StationDelete(IList<int> idList)
        {
            BLL.Organize.Station station = new BLL.Organize.Station();

            bool result = station.Delete(idList);

            if (result)
            {
                return Json(new { IsSuccess = true, Message = "删除成功" });
            }
            else
            {
                return Json(new { IsSuccess = false, Message = "删除失败" });
            }
        }
        #endregion

        #region 科室
        public ActionResult BranchLoad(int stationId)
        {
            BLL.Organize.Branch branch = new BLL.Organize.Branch();

            var result = branch.LoadAllBranch(stationId);

            return Json(result);
        }

        public ActionResult BranchSave(TZBranch entity)
        {
            BLL.Organize.Branch branch = new BLL.Organize.Branch();

            int managerId = int.Parse(Request.Form["ManagerID"]);
            int  stationId = int.Parse(Request.Form["BranchBelong"]);

            if (ModelState.IsValid)
            {
                bool save = branch.Save(entity, managerId, stationId);

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

        public ActionResult BranchDelete(IList<int> idList)
        {
            BLL.Organize.Branch branch = new BLL.Organize.Branch();

            bool result = branch.Delete(idList);

            if (result)
            {
                return Json(new { IsSuccess = true, Message = "删除成功" });
            }
            else
            {
                return Json(new { IsSuccess = false, Message = "删除失败" });
            }
        }
        #endregion
        public ActionResult AreaLoad()
        {
            BLL.Organize.Station station = new BLL.Organize.Station();

            var result = station.LoadAllArea();

            return Json(result);
        }

        #endregion

        #region 功能项管理
        public ActionResult Action()
        {
            return View();
        }
        public ActionResult ActionLoad(int page, int rows, string order, string sort)
        {
            BLL.Organize.Action Action = new BLL.Organize.Action();

            var result = Action.LoadAllActionByPage(page, rows, order, sort);

            return Json(result);
        }
        //public ActionResult ActionLoadAll()
        //{
        //    BLL.Organize.Action action = new BLL.Organize.Action();

        //    var result = action.GetAllAction();

        //    return Json(result);
        //}

        public ActionResult ActionLoadTree()
        {
            //BLL.Organize.Action action = new BLL.Organize.Action();
            //return this.Json(Aciton.GetMenu("0"));
            return this.Json(Anchor.FA.BLL.Organize.Action.GetMenuRange("0"));
        }

        public ActionResult ActionEdit(int? id)
        {
            BLL.Organize.Action Action = new BLL.Organize.Action();
            B_ACTION cot = Action.Edit(id) as B_ACTION;
            this.ViewData["entity"] = cot;
            ViewData["action"] = cot.ParentID == null ? 1000 : cot.ParentID;
            return View();
        }
        public ActionResult ActionSave(B_ACTION entity)
        {
            BLL.Organize.Action Action = new BLL.Organize.Action();
            if (ModelState.IsValid)
            {
                bool save;
                try
                {
                    save = Action.Save(entity);
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
        public ActionResult ActionDelete(IList<int> idList)
        {
            BLL.Organize.Action Action = new BLL.Organize.Action();
            bool delete;
            try
            {
                delete = Action.Delete(idList);
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

        #region 角色与权限管理

        public ActionResult Role()
        {
            return View();
        }
        public ActionResult RoleLoad(int page, int rows, string order, string sort)
        {
            BLL.Organize.Role Role = new BLL.Organize.Role();

            var result = Role.LoadAllRoleByPage(page, rows, order, sort);

            return Json(result);
        }
        public ActionResult RoleAllLoad()
        {
            BLL.Organize.Role Role = new BLL.Organize.Role();

            var result = Role.LoadAllRoleByPage();

            return Json(result);
        }
        public ActionResult RoleEdit(int? id)
        {
            BLL.Organize.Role role = new BLL.Organize.Role();
            this.ViewData["entity"] = role.Edit(id);

            if (id != null)
            {
                ViewData["action"] = string.Join(",", role.LoadRoleAction((int)id));
            }

            return View();
        }

        public ActionResult RoleEdit2(int? id)
        {
            BLL.Organize.Role role = new BLL.Organize.Role();
            this.ViewData["entity"] = role.Edit(id);

            if (id != null)
            {
                ViewData["type"] = "修改角色";
                //ViewData["action"] = string.Join(",", role.LoadRoleAction((int)id));
                //ViewData["ActionRange"] = string.Join(",", role.LoadRoleActionRange((int)id));
                ViewData["ActionRange"] = string.Join(",", Anchor.FA.BLL.Organize.Role.LoadRoleActionRange((int)id).Select(t => "\"" + t + "\""));
            }
            else
            {
                ViewData["type"] = "新增角色";
            }

            return View();
        }
        public ActionResult RoleSave(B_ROLE entity)
        {
            BLL.Organize.Role role = new BLL.Organize.Role();

            string action = Request.Form["action"];
            string range = Request.Form["range"];

            if (ModelState.IsValid)
            {
                bool save;
                try
                {
                    save = role.Save(entity);
                    List<int> listActionId = new List<int>();
                    if (!string.IsNullOrEmpty(action))
                    {
                        foreach (string a in action.Split(',').ToList())
                        {
                            listActionId.Add(int.Parse(a));
                        }
                    }
                    List<string> listRangeId = new List<string>();
                    if (!string.IsNullOrEmpty(range))
                    {
                        foreach (string a in range.Split(',').ToList())
                        {
                            listRangeId.Add(a);
                        }
                    }
                    //role.SaveRoleAction(entity.ID, listActionId); 
                    Anchor.FA.BLL.Organize.Role.SaveRoleAction(entity.ID, listActionId, listRangeId);

                }
                catch (Exception ex)
                {
                    Log4Net.LogError("RoleSave", ex.ToString());
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
        public ActionResult RoleDelete(IList<int> idList)
        {
            BLL.Organize.Role Role = new BLL.Organize.Role();
            bool delete;
            try
            {
                delete = Role.Delete(idList);
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
        public ActionResult RoleLoadAll()
        {
            BLL.Organize.Role role = new BLL.Organize.Role();

            var result = role.LoadAllRole();

            return Json(result);
        }

        #endregion

        #region 人员管理
        public ActionResult WorkerRoleLoad(int workerId)
        {
            BLL.Organize.Worker worker = new BLL.Organize.Worker();
            object listWorkerRole = worker.LoadWorkerRole(workerId);

            return Json(listWorkerRole);
        }

        public ActionResult WorkerRoleSave(B_WORKER_ROLE entity)
        {
            //string WokerID = Request.Form["WokerID"];
            //string RoleID = Request.Form["RoleID"];
            //string EmpNo = Request.Form["EmpNo"];
            //string OrgID = Request.Form["OrgID"];
            //string ID = Request.Form["ID"];


            //B_WORKER_ROLE workerRole = new B_WORKER_ROLE();

            //workerRole.EmpNo = EmpNo;
            //workerRole.WorkerID = int.Parse(WokerID);
            //workerRole.RoleID = int.Parse(RoleID);
            //workerRole.OrgID = int.Parse(OrgID);
            //workerRole.ID = int.Parse(ID);

            BLL.Organize.Worker worker = new BLL.Organize.Worker();

            if (ModelState.IsValid)
            {
                bool save = worker.SaveWorkerRole(entity);

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

        public ActionResult WorkerRoleDel(int id)
        {
            //string WorkerID = Request.QueryString["Rworkerid"];
            //string RoleID = Request.QueryString["RoleID"];
            //string EmpNo = Request.QueryString["EmpNo"];

            //B_WORKER_ROLE workerRole = new B_WORKER_ROLE();

            //workerRole.WorkerID = int.Parse(WorkerID);
            //workerRole.RoleID = int.Parse(RoleID);
            //workerRole.EmpNo = EmpNo;

            BLL.Organize.Worker worker = new BLL.Organize.Worker();

            if (ModelState.IsValid)
            {
                bool save = worker.DelWorkerRole(id);

                if (save)
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

        public ActionResult IsExistEmpNo(string empNo)
        {
            BLL.Organize.Worker worker = new BLL.Organize.Worker();

            bool result = worker.IsExistEmpNo(empNo);

            if (result)
            {
                return Json(new { IsSuccess = true });
            }
            else
            {
                return Json(new { IsSuccess = false });
            }

        }

        public ActionResult Worker()
        {
            Anchor.FA.Utility.ButtonPower p = new ButtonPower();

            p.ActionIDRang = UserInfo.GetRange(int.Parse(Request.QueryString["ActionId"]));

            string Search = p.GetGroupRangePower("searchBound");

            switch (p.GetGroupRangePower("searchBound"))
            {
                case "SearchAll":
                    ViewData["orgId"] = 1;
                    break;
                case "SearchCenter"://查找分中心
                    ViewData["orgId"] = UserInfo.CenterID;
                    break;
                case "SearchOrganization"://查找分站
                    ViewData["orgId"] = String.Join(",", UserInfo.Org);
                    break;
                default://没有设置查询权限
                    return null;
                //break;
            }
           
            return View();
        }

        public ActionResult WorkerPage(int? pageNumber, string orgId)
        {
            this.ViewData["orgId"] = orgId;

            if (pageNumber == null || pageNumber == 0)
                this.ViewData["pageNumber"] = "1";
            else
                this.ViewData["pageNumber"] = pageNumber;


            return View();
        }

        public ActionResult WorkerLoad(int page, int rows, string order, string sort, string name, string orgId, int? roleId, string empNo)
        {
            BLL.Organize.Worker worker = new BLL.Organize.Worker();

            var result = worker.LoadAllWorkerByPage(page, rows, order, sort, name, orgId, roleId, false, empNo);

            return Json(result);
        }


        public ActionResult WorkerLoadupdo(string name, int? orgId, int? roleId, string empNo)
        {
            BLL.Organize.Worker worker = new BLL.Organize.Worker();


            DataSet dsReport = worker.LoadAllWorkerByPageUpdo(name, orgId, roleId, empNo);

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

        public ActionResult SmsWorkerLoad(int page, int rows, string order, string sort, string name, string orgId, int? roleId)
        {
            BLL.Organize.Worker worker = new BLL.Organize.Worker();

            var result = worker.LoadAllWorkerByPage(page, rows, order, sort, name, orgId, roleId, true, "");

            return Json(result);
        }

        public ActionResult WorkerLoadByOrg(int orgId)
        {
            BLL.Organize.Worker worker = new BLL.Organize.Worker();

            var result = worker.GetWorkerByOrg(orgId);

            return Json(result);
        }

        public ActionResult GetAllWorker()
        {
            BLL.Organize.Worker worker = new BLL.Organize.Worker();

            List<B_WORKER> cot = worker.GetAllWorker();

            return Json(cot);
        }

        public ActionResult WorkerEdit(string workerId)
        {
            BLL.Organize.Worker worker = new BLL.Organize.Worker();

            if (string.IsNullOrEmpty(workerId))
            {
                B_WORKER wo = worker.GetWorkerById(null);
                ViewData["workerId"] = wo.ID;
                ViewData["entity"] = wo;

                ViewData["title"] = null;
                ViewData["IsQuota"] = null;

                this.ViewData["type"] = "add";
            }
            else
            {
                int intWorkerId =  int.Parse(workerId); //去掉"Worker-"前缀

                B_WORKER wo = worker.GetWorkerById(intWorkerId);

                ViewData["entity"] = wo;
                ViewData["workerId"] = intWorkerId;
                ViewData["title"] = wo.TitleTechnicalID;
                ViewData["JobLevel"] = wo.JobLevel;

                ViewData["IsQuota"] = wo.IsQuota;

                this.ViewData["type"] = "update";
            }

            ViewData["display"] = AppConfig.GetStringConfigValue("WorkerExtendedAttribute") == "N" ? "display: none" : "";

            return View();
        }

        public ActionResult WorkerRole(int workerId)
        {
            ViewData["workerId"] = workerId;

            return View();
        }

        //public ActionResult WorkerPersonnel()
        //{

        //    var result = CommonData.GetDataByType("Personnel");
        //    return Json(result);
        //}
        //public ActionResult WorkerPost()
        //{
        //    BLL.Organize.Position position = new BLL.Organize.Position();

        //    var result = position.LoadAllPost();

        //    return Json(result);
        //}

        public ActionResult WorkerSave(B_WORKER entity)
        {
            entity.Sex = Request.Form["sex"];
            //entity.IsQuota = Request.Form["quota"];
            entity.IsActive = Request.Form["active"];
            entity.IsAllowInternetAccess = Request.Form["isAllowInternetAccess"];
            entity.LoginName = entity.Name;

            BLL.Organize.Worker worker = new BLL.Organize.Worker();

            if (ModelState.IsValid)
            {
                entity.ID = worker.Save(entity);

                if (entity.ID > 0)
                {
                    return Json(new { IsSuccess = true, WorkerId = entity.ID, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { IsSuccess = false, WorkerId = entity.ID, Message = "保存失败" }, "text/html", JsonRequestBehavior.AllowGet);
                }
            }

            return View();

        }

        public ActionResult WorkerDelete(IList<string> idList)
        {
            BLL.Organize.Worker worker = new BLL.Organize.Worker();
            bool delete;
            try
            {
                delete = worker.Delete(idList);
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

        //public ActionResult WorkerTree(int? id)
        //{
        //    try
        //    {
        //        BLL.Organize.Worker worker = new BLL.Organize.Worker();
        //        List<C_ORGANIZE_TREE> cot = worker.GetTree(id);
        //        return this.Json(cot);
        //    }
        //    catch (Exception e)
        //    {
        //        Dictionary<string, string> dict = new Dictionary<string, string>();
        //        dict.Add("InfoID", "0");
        //        dict.Add("InfoMessage", e.Message);
        //        return this.Json(dict);
        //    }
        //}

        //public ActionResult GetGoupJson(int? id)
        //{
        //    BLL.Organize.Worker worker = new BLL.Organize.Worker();

        //    int parentId = -1;  //默认为-1,如果请求参数不正确,将不返回任何值
        //    string resultStr = string.Empty;
        //    if (id != null)
        //    {
        //        Int32.TryParse(id.ToString(), out parentId);
        //    }
        //    if (parentId >= 0)
        //    {
        //        List<C_ORGANIZE_TREE> cot = worker.GetTree(id);
        //        resultStr = "";
        //        resultStr += "[";
        //        foreach (var item in cot)
        //        {
        //            resultStr += "{";
        //            resultStr += string.Format("\"id\": \"{0}\", \"text\": \"{1}\", \"iconCls\": \"icon-ok\", \"state\": \"closed\"", item.id.ToString(), item.text);
        //            resultStr += "},";
        //        }
        //        resultStr = resultStr.Substring(0, resultStr.Length - 1);
        //        resultStr += "]";
        //    }
        //    return this.Json(resultStr);
        //}

        public ActionResult WorkerSideline(int workerId)
        {
            BLL.Organize.Worker worker = new BLL.Organize.Worker();

            var result = worker.GetWorkerSideline(workerId);

            return Json(result);
        }

        /// <summary>
        /// 保存兼职信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult WorkerSidelineSave(B_WORKER_ORGANIZATION entity)
        {
            BLL.Organize.Worker worker = new BLL.Organize.Worker();

            //string type = Request.Form["type"];   //判断新增/修改
            entity.Type = int.Parse(Request.Form["type"]);   //判断新增/修改
            //string update_unit = Request.Form["update_unit"];    //修改的机构

            if (ModelState.IsValid)
            {
                bool save;

                try
                {
                    save = worker.SaveSideline(entity);
                }
                catch (Exception ex)
                {
                    Log4Net.LogError("WorkerSidelineSave", ex.ToString());
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

        public ActionResult WorkerSidelinDelete(int id)
        {
            BLL.Organize.Worker worker = new BLL.Organize.Worker();
            bool delete;
            try
            {
                delete = worker.DeleteSideline(id);
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

        //判断登录名是否已经存在
        public ActionResult IsExistName(string name, int workerId)
        {
            BLL.Organize.Worker worker = new BLL.Organize.Worker();

            B_WORKER result = worker.GetAllWorker().FirstOrDefault(t => t.Name == name && t.ID != workerId);

            if (result == null)
            {
                return Json(new { IsSuccess = false });
            }
            else
            {
                return Json(new { IsSuccess = true });
            }

        }



        #endregion


        #region 获取或判断主键
        public ActionResult GetCode(string tableName)
        {
            BLL.Organize.Worker worker = new BLL.Organize.Worker();

            return Json(Anchor.FA.Utility.PrimaryKeyUtility.GetCoding(tableName));

        }

        public ActionResult GetID(string tableName)
        {
            BLL.Organize.Worker worker = new BLL.Organize.Worker();

            return Json(Anchor.FA.Utility.PrimaryKeyUtility.GetID(tableName));

        }


        public ActionResult IsExistCode(string tableName, string code)
        {
            BLL.Organize.Worker worker = new BLL.Organize.Worker();

            bool result = Anchor.FA.Utility.PrimaryKeyUtility.IsExistCode(tableName, code);

            if (result)
            {
                return Json(new { IsSuccess = true });
            }
            else
            {
                return Json(new { IsSuccess = false });
            }
        }

        public ActionResult IsExistID(string tableName, int id)
        {
            BLL.Organize.Worker worker = new BLL.Organize.Worker();

            bool result = Anchor.FA.Utility.PrimaryKeyUtility.IsExistID(tableName, id);

            if (result)
            {
                return Json(new { IsSuccess = true });
            }
            else
            {
                return Json(new { IsSuccess = false });
            }

        }
        #endregion
    }
}
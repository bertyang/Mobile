using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data.SqlClient;

using Anchor.FA.Model;
using System.Data.Linq;
using Anchor.FA.Utility;
using Anchor.FA.BLL.IBLL;
using System.Data;
using Spring.Context;
using Spring.Context.Support;

namespace Anchor.FA.DAL.Organize
{
    public class Worker
    {
        public static B_WORKER Login(string loginName)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                try
                {
                    B_WORKER entity = (from w in dbContext.B_WORKER
                                       join o in dbContext.B_WORKER_ROLE on w.ID equals o.WorkerID into workerRole
                                       from o in workerRole.DefaultIfEmpty()
                                       where w.IsActive == "Y" && (o.EmpNo == loginName || w.LoginName == loginName || w.Name == loginName) //|| o.TPerson编码 == loginName
                                       select w).FirstOrDefault(); ;

                    return entity;
                }
                catch (Exception e)
                {
                    Log4Net.LogError("Anchor.FA.DAL.Organize/Login()", e.Message);
                    return null;
                }

            }
        }

        /// <summary>
        /// 调度台或者回访程序登录
        /// </summary>
        /// <param name="loginName">TPerson编码</param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public static B_WORKER DeskLogin(string loginName)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                try
                {
                    B_WORKER entity = (from w in dbContext.B_WORKER
                                       join o in dbContext.B_WORKER_ROLE on w.ID equals o.WorkerID into workerRole
                                       from o in workerRole.DefaultIfEmpty()
                                       where w.IsActive == "Y" && o.TPerson编码 == loginName
                                       select w).FirstOrDefault(); ;

                    return entity;
                }
                catch (Exception e)
                {
                    Log4Net.LogError("Anchor.FA.DAL.Organize/Login()", e.Message);
                    return null;
                }

            }
        }

        public static void LoginLog(B_LOGIN_LOG log)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                try
                {
                    dbContext.B_LOGIN_LOG.InsertOnSubmit(log);
                    dbContext.SubmitChanges();
                }
                catch (Exception e)
                {
                    Log4Net.LogError("Anchor.FA.DAL.Organize/LoginLog()", e.Message);
                }

            }
        }

        public static List<B_LOGIN_LOG> LoginCheck(string loginName)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
               return dbContext.B_LOGIN_LOG.Where(t => t.Name == loginName).OrderByDescending(t => t.LoginTime).Take(2).ToList();
            }
        }


        public static B_WORKER GetWorkerById(int? id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                B_WORKER entity = null;
                if (id != null)
                {
                    entity = dbContext.B_WORKER.FirstOrDefault(t => t.ID == id);
                }
                entity = entity ?? new B_WORKER
                {
                    ID = Anchor.FA.Utility.PrimaryKeyUtility.GetID("B_WORKER"),
                    EntryDate = DateTime.Now,
                    LoginName = string.Empty,
                    Name = string.Empty,
                    PassWord = string.Empty,
                    Sex = "1",
                    //IsQuota = "N",
                    IsActive = "Y",
                    IsAllowInternetAccess = "N"
                };
                return entity;
            }
        }



        public static C_WorkerDetail GetWorkerDetailById(int id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                C_WorkerDetail entity = new C_WorkerDetail();
                entity.W = dbContext.B_WORKER.FirstOrDefault(t => t.ID == id);
                entity.WOrg = (from o in dbContext.B_ORGANIZATION
                               join wo in dbContext.B_WORKER_ORGANIZATION on o.ID equals wo.OrgID
                               where wo.WorkerID == id
                               orderby o.Sort
                               select o).ToList();

                entity.WRol = (from r in dbContext.B_ROLE
                               join wr in dbContext.B_WORKER_ROLE on r.ID equals wr.RoleID
                               where wr.WorkerID == id
                               select r).ToList();

                entity.Rol = entity.WRol.Select(t => t.ID).ToArray();
                entity.Org = entity.WOrg.Select(t => t.ID).ToArray();
                entity.Sta = entity.WOrg.Where(t=>t.Type==(int)OrgType.Station).Select(t => t.编码).ToArray();

                entity.EmpNo = (from r in dbContext.B_ROLE
                               join wr in dbContext.B_WORKER_ROLE on r.ID equals wr.RoleID
                                where wr.WorkerID == id && wr.EmpNo!=""
                               select wr.EmpNo).ToArray();

                entity.PersonCode = (from r in dbContext.B_ROLE
                                     join wr in dbContext.B_WORKER_ROLE on r.ID equals wr.RoleID
                                     where wr.WorkerID == id && wr.TPerson编码 != ""
                                     select wr.TPerson编码).ToArray();
                //entity.WPer = (from p in dbContext.TPerson
                //               where entity.EmpNo.Contains(p.工号)
                //               select p).ToList();
                entity.WPer = (from p in dbContext.TPerson
                               where entity.PersonCode.Contains(p.编码)
                               select p).ToList();
                entity.WAct = (from a in dbContext.B_ACTION
                               join ra in dbContext.B_ROLE_ACTION on a.ID equals ra.ActionID
                               where a.IsActive == "Y" && entity.Rol.Contains(ra.RoleID)
                               orderby a.OrderID
                               select a
                             ).ToList();
                entity.WRan = (from r in dbContext.B_Range
                               join rr in dbContext.B_ROLE_Range on new { r.ActionId, r.Range } equals new { ActionId = rr.ActionID, rr.Range }
                               where r.IsActive == true && entity.Rol.Contains(rr.RoleID)
                               orderby r.OrderID
                               select r).ToList();
                entity.WSta = (from s in dbContext.TStation
                               where entity.Sta.Contains(s.编码) && s.是否有效 == true
                               orderby s.顺序号
                               select s).ToList();
                #region 之前获取的组织机构是 本人所有属于的组织机构，但是需要一个默认的组织机构。Type == 0

                var WOrgM = (from o in dbContext.B_ORGANIZATION
                             join wo in dbContext.B_WORKER_ORGANIZATION on o.ID equals wo.OrgID
                             where wo.WorkerID == id && wo.Type == 0
                             select o);
                if (WOrgM.Any())
                {
                    entity.WOrgM = WOrgM.First();
                }
                #endregion

                if(entity.WOrgM !=null)
                {
                    string centerCode = "1";

                    if(entity.WOrgM.Type == (int)OrgType.Station)
                    {
                        centerCode = dbContext.B_ORGANIZATION.Single(t => t.ID == entity.WOrgM.ParentID).编码;
                        entity.CenterID = entity.WOrgM.ParentID;
                    }
                    else if(entity.WOrgM.Type == (int)OrgType.Branch)
                    {
                        centerCode = (from o in dbContext.B_ORGANIZATION
                                     join p in dbContext.B_ORGANIZATION on o.ParentID equals p.ID
                                     join g in dbContext.B_ORGANIZATION on p.ParentID equals g.ID
                                     where o.ID == entity.WOrgM.ID
                                     select g.编码).First();
                        entity.CenterID = (from o in dbContext.B_ORGANIZATION
                                           join p in dbContext.B_ORGANIZATION on o.ParentID equals p.ID
                                           join g in dbContext.B_ORGANIZATION on p.ParentID equals g.ID
                                           where o.ID == entity.WOrgM.ID
                                           select g.ID).First();
                    }
                    else if(entity.WOrgM.Type == (int)OrgType.Center)
                    {
                        centerCode = entity.WOrgM.编码;
                        entity.CenterID = entity.WOrgM.ID;
                    }

                    entity.CenterCode = int.Parse(centerCode);
                }

                return entity;
            }
        }


        public static List<B_WORKER> GetAllWorker()
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.B_WORKER.Where(t=> t.IsActive == "Y").OrderBy(t => t.Name).ToList();
            }
        }

        public static List<B_WORKER> GetWorkerByOrg(int orgId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from w in dbContext.B_WORKER
                           join o in dbContext.B_WORKER_ORGANIZATION on w.ID equals o.WorkerID
                           where o.OrgID == orgId
                           select w;
                return list.ToList();
            }
        }

        public static List<B_WORKER> GetWorkerByRole(int roleId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from w in dbContext.B_WORKER
                           join o in dbContext.B_WORKER_ROLE on w.ID equals o.WorkerID
                           where o.RoleID == roleId
                           select w;
                return list.ToList();
            }
        }



        public static bool UpdatePassword(int workerId, string newPassword)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                using (MainDataContext dsContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
                {
                    IApplicationContext ctx = ContextRegistry.GetContext();
                    IEncrypt encrypt = ctx["Encrypt"] as IEncrypt;

                    B_WORKER worker = dbContext.B_WORKER.FirstOrDefault(v => v.ID == workerId);

                    if (worker != null)
                    {
                        worker.PassWord = encrypt.Encrypt(workerId, newPassword);

                        List<B_WORKER_ROLE> listWR = dbContext.B_WORKER_ROLE.Where(t => t.WorkerID == workerId).ToList();

                        foreach (B_WORKER_ROLE wr in listWR)
                        {
                            if (!string.IsNullOrEmpty(wr.EmpNo))
                            {
                                TPerson tp = dsContext.TPerson.SingleOrDefault(t => t.工号 == wr.EmpNo);

                                if (tp != null)
                                {
                                    tp.口令 = new HashEncrypt().Encrypt(workerId, newPassword); ;
                                }
                            }
                        }
                    }

                    dbContext.SubmitChanges();
                    dsContext.SubmitChanges();
                    return true;
                }
            }
        }

        public static object LoadAllWorkerByPage(int page, int rows, string order, string sort, string name, string orgId, int? roleId, bool mustHavePhone,string empNo)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                string sql = @"with ORGANIZATION as 
                                (select a.ID,a.Name,a.ParentID from dbo.B_ORGANIZATION a";
                if (!string.IsNullOrEmpty(orgId))
                {
                    sql += " where a.ID in (" + orgId + @")
union all 
                                select b.ID,b.Name,b.ParentID from B_ORGANIZATION b 
                                join ORGANIZATION w on b.ParentID=w.ID ";

                }
                sql += @")
select w.ID,w.Name,LoginName=w.LoginName,
w.EntryDate,ParentName=d.Name,Post=p.Name,Unit=o.Name,
w.PassWord,w.Sex,w.Mobile,TitleName=g.Name,
Active = case when w.IsActive = 'Y' then '是' else '否' end,
Role = dbo.f_Role(w.ID),EmpNo=dbo.f_EmpNo(w.ID),w.Tel
from B_WORKER w
left join B_WORKER_ORGANIZATION wo on wo.WorkerID = w.ID and wo.Type = 0";
                if (orgId == null)
                {
                    sql += @" left  join ORGANIZATION o on o.ID = wo.OrgID";
                }
                else
                {
                    sql += @" inner join ORGANIZATION o on o.ID = wo.OrgID";
                }
                sql += @" left join B_WORKER d on wo.ParentID=d.ID
                        left join G_DATA p on wo.PostID = p.Value and p.Type='Post'
                        --join B_ORGANIZATION o1 on wo.OrgID = o1.ID and wo.Type = 0
                        left join G_DATA g on w.TitleTechnicalID =g.Value and g.Type='title_technical'
                        where 1=1 ";

                if (roleId != null)
                {
                    sql += " and w.ID in ( select r.WorkerID from B_WORKER_ROLE r where r.RoleID='" + roleId + "')";
                }

                if (!string.IsNullOrEmpty(name))
                {
                    sql += " and w.Name like '%" + name + "%'";
                }

                if (mustHavePhone)
                {
                    sql += " and w.Mobile !=null";
                }

                if (!string.IsNullOrEmpty(empNo))
                {
                    sql += " and w.ID in ( select r.WorkerID from B_WORKER_ROLE r where r.EmpNo='" + empNo + "')";
                }

                sql += @" union all
                            select w.ID,w.Name,LoginName=w.LoginName,
                            w.EntryDate,ParentName='',Post='',Unit='',
                            w.PassWord,w.Sex,w.Mobile,TitleName='',
                            Active = case when w.IsActive = 'Y' then '是' else '否' end,
                            Role = '',EmpNo='',w.Tel
                            from B_WORKER w 
                            where ID not in(select WorkerID from  B_WORKER_ORGANIZATION)";

                var list1 = dbContext.ExecuteQuery<C_Worker>(sql);
                var list2 = dbContext.ExecuteQuery<C_Worker>(sql);

                long total = list1.LongCount();
                list2 = list2.Skip((page - 1) * rows).Take(rows);
                var listWorker = list2.ToList().Select(o => new
                {
                    ID = o.ID,
                    Post = o.Post,
                    Unit = o.Unit,
                    Name = o.Name,
                    ParentName = o.ParentName,
                    Active = o.Active,
                    Mobile = o.Mobile,
                    Tel = o.Tel,
                    Role = o.Role,
                    EmpNo = o.EmpNo
                });

                return new { total = total, rows = listWorker.ToList() };
            }
        }


        public static DataSet LoadAllWorkerByPageUpdo(string name, int? orgId, int? roleId, string empNo)
        {
            //using (MainDataContext dbContext = new MainDataContext())
            //{
            string sql = @"with ORGANIZATION as 
                                (select a.ID,a.Name,a.ParentID from dbo.B_ORGANIZATION a";
            if (orgId != null)
            {
                sql += " where a.ID = '" + orgId + @"'
union all 
                                select b.ID,b.Name,b.ParentID from B_ORGANIZATION b 
                                join ORGANIZATION w on b.ParentID=w.ID ";

            }
            sql += @")
                    select w.Name as 姓名,
                    dbo.f_EmpNo(w.ID) as 工号,
                    dbo.f_Role(w.ID) as 角色,
                    o.Name as 机构,
                    p.Name as 职位,
                    d.Name as 上级,
                    w.Mobile as 通讯号码,
                    case when w.IsActive = 'Y' then '是' else '否' end as 启用
                    from B_WORKER w
                    left join B_WORKER_ORGANIZATION wo on wo.WorkerID = w.ID and wo.Type = 0";
            if (orgId == null)
            {
                sql += @" left  join ORGANIZATION o on o.ID = wo.OrgID";
            }
            else
            {
                sql += @" inner join ORGANIZATION o on o.ID = wo.OrgID";
            }
            sql += @" left join B_WORKER d on wo.ParentID=d.ID
                        left join G_DATA p on wo.PostID = p.Value and p.Type='Post'
                        --join B_ORGANIZATION o1 on wo.OrgID = o1.ID and wo.Type = 0
                        left join G_DATA g on w.TitleTechnicalID =g.Value and g.Type='title_technical'
                        where 1=1 ";

            if (roleId != null)
            {
                sql += " and w.ID in ( select r.WorkerID from B_WORKER_ROLE r where r.RoleID='" + roleId + "')";
            }

            if (!string.IsNullOrEmpty(name))
            {
                sql += " and w.Name like '%" + name + "%'";
            }


            if (!string.IsNullOrEmpty(empNo))
            {
                sql += " and w.ID in ( select r.WorkerID from B_WORKER_ROLE r where r.EmpNo='" + empNo + "')";
            }

            sql += " order by w.ID";


            return SQLHelper.ExecuteDataSet(AppConfig.ConnectionString, CommandType.Text, sql.ToString());
        }

        public static bool Delete(IList<string> listId)
        {

            using (MainDataContext dbContext = new MainDataContext())
            {
                using (MainDataContext dsContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
                {
                    try
                    {
                         //删除Tperson
                        foreach (string id in listId)
                        {
                            int workerId = int.Parse(id);

                            List<B_WORKER_ROLE> listWorkerRole = dbContext.B_WORKER_ROLE.Where(t => t.WorkerID == workerId && t.TPerson编码 != null && t.TPerson编码 != "").ToList();

                            foreach (B_WORKER_ROLE workerRole in listWorkerRole)
                            {
                                TPerson person = dsContext.TPerson.FirstOrDefault(t => t.编码 == workerRole.TPerson编码);

                                dsContext.TPerson.DeleteOnSubmit(person);
                            }
                        }

                        dsContext.SubmitChanges();

                        //删除人员
                        StringBuilder sb = new StringBuilder();
                        sb.Append("SET XACT_ABORT ON ");
                        sb.AppendLine(" BEGIN TRAN ");

                        foreach (string id in listId)
                        {
                            int workerId = int.Parse(id);

                            //删除人员
                            sb.AppendFormat(" delete B_WORKER  where id={0}", workerId);

                            //删除人员组织
                            sb.AppendFormat(" delete B_WORKER_ORGANIZATION  where WorkerID={0}", workerId);
                            //删除人员角色
                            sb.AppendFormat(" delete B_WORKER_ROLE  where WorkerID={0}", workerId);

                        }

                        sb.AppendLine(" COMMIT TRAN ");
                        dbContext.ExecuteCommand(sb.ToString());

                        return true;

                    }
                    catch (Exception ex)
                    {
                        Log4Net.LogError("删除人员", ex.Message);
                        return false;
                    }

                }
            }
        }
        //public static List<C_ORGANIZE_TREE> GetWorkerTree(int id)
        //{
        //    using (MainDataContext dbContext = new MainDataContext())
        //    {
        //        List<C_ORGANIZE_TREE> listTree = new List<C_ORGANIZE_TREE>();

        //        var list = from ra in dbContext.B_WORKER_ORGANIZATION
        //                   join r in dbContext.B_WORKER on ra.WorkerID equals r.ID
        //                   where ra.OrgID == id
        //                   select new
        //                   {
        //                       OrgID = ra.OrgID,
        //                       WorkerID = ra.WorkerID,
        //                       WorkerName = r.Name,
        //                   };

        //        foreach (var td in list)
        //        {
        //            C_ORGANIZE_TREE lw = new C_ORGANIZE_TREE();

        //            lw.id = td.WorkerID.ToString();
        //            lw.text = td.WorkerName;
        //            lw.ParentID = td.OrgID.ToString();
        //            listTree.Add(lw);
        //        }

        //        return listTree;
        //    }


        //}
        public static int Save(B_WORKER entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                using (MainDataContext dsContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
                {
                    StringBuilder sb = new StringBuilder();

                    List<SqlParameter> paramArray = new List<SqlParameter>();

                    try
                    {
                        //人员
                        if (dbContext.B_WORKER.Count(t => t.ID == entity.ID) == 0)
                        {
                            //var list = from a in dbContext.B_WORKER select a.ID;

                            //long total = list.LongCount();

                            //if (total == 0)
                            //{
                            //    entity.ID = 1;
                            //}
                            //else
                            //{
                            //    entity.ID = dbContext.B_WORKER.Max(a => a.ID) + 1;
                            //}

                            IApplicationContext ctx = ContextRegistry.GetContext();
                            IEncrypt encrypt = ctx["Encrypt"] as IEncrypt;

                            entity.PassWord = encrypt.Encrypt(entity.ID, entity.PassWord);

                            dbContext.B_WORKER.InsertOnSubmit(entity);
                        }
                        else
                        {
                            B_WORKER worker = dbContext.B_WORKER.Single(t => t.ID == entity.ID);

                            worker.LoginName = entity.LoginName;
                            worker.Name = entity.Name;
                            worker.Sex = entity.Sex;
                            worker.EntryDate = entity.EntryDate;
                            worker.TitleTechnicalID = entity.TitleTechnicalID;
                            worker.IsActive = entity.IsActive;
                            worker.IsQuota = entity.IsQuota;
                            worker.Mobile = entity.Mobile;
                            worker.Tel = entity.Tel;
                            worker.IsAllowInternetAccess = entity.IsAllowInternetAccess;
                            worker.JobLevel = entity.JobLevel;
                        }

                        //调度台TPerson
                        List<B_WORKER_ROLE> listWorkerRole = dbContext.B_WORKER_ROLE.Where(t => t.WorkerID == entity.ID && t.EmpNo != null && t.EmpNo != "").ToList();

                        foreach (B_WORKER_ROLE workerRole in listWorkerRole)
                        {
                            TPerson person = dsContext.TPerson.FirstOrDefault(t => t.工号 == workerRole.EmpNo);

                            person.通话号码 = entity.Tel ?? string.Empty;
                            person.短信号码 = entity.Mobile ?? string.Empty;
                            person.是否有效 = entity.IsActive == "Y" ? true : false;
                            person.姓名 = entity.Name;
                        }

                        dbContext.SubmitChanges();
                        dsContext.SubmitChanges();

                        return entity.ID;

                    }
                    catch (Exception ex)
                    {
                        Log4Net.LogError("Save Worker", ex.Message);
                        return -1;
                    }
                }

            }

        }

        #region 设置人员角色


        public static object LoadWorkerRole(int workerId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from w in dbContext.B_WORKER_ROLE
                           join r in dbContext.B_ROLE on w.RoleID equals r.ID
                           join o in dbContext.B_ORGANIZATION on w.OrgID equals o.ID
                           join p in dbContext.TPerson on w.TPerson编码 equals p.编码 into tp
                           from p in tp.DefaultIfEmpty()
                           where w.WorkerID == workerId 
                           orderby w.EmpNo descending
                           select new
                           {
                               EmpNo = w.EmpNo,
                               EmpNoStatus = (w.EmpNo == "" || w.EmpNo==null) ? "" : w.EmpNo + ((p.绑定车辆编码 == null || p.绑定车辆编码 == "")
                                                                   && (p.登录台号 == null || p.登录台号 == "")
                                                                   ? "(未上班)" : "(<font color='red'>上班</font>)"),
                               WorkerID = w.WorkerID,
                               RoleID = r.ID,
                               RoleName = r.Name,
                               OrgName = o.Name,
                               OrgID = w.OrgID,
                               ID = w.ID,
                               TPerson编码 = w.TPerson编码,
                             
                           };

                //if (orgId != null)
                //{
                //    return list.Where(t => t.OrgID == orgId).ToList();
                //}
                //else
                //{
                    return list.ToList();
                //}
            }
        }

        public static bool SaveWorkerRole(B_WORKER_ROLE entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                using (MainDataContext dsContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
                {
                    try
                    {
                        //同步Tperson
                        if (!string.IsNullOrEmpty(entity.EmpNo))
                        {
                            B_WORKER worker = GetWorkerById(entity.WorkerID);
                            B_ORGANIZATION unit = Organize.GetUnit(entity.OrgID);

                            if (dsContext.TPerson.Count(t => t.编码 == entity.TPerson编码) == 0)
                            {
                                //string empNo = GetCoding();
                                IApplicationContext ctx = ContextRegistry.GetContext();
                                IEncrypt encrypt = ctx["Encrypt"] as IEncrypt;

                                TPerson person = new TPerson();
                                person.编码 = entity.TPerson编码;
                                person.单位编码 = unit.Type == (int)OrgType.Branch ? unit.编码 : "-1";
                                person.通话号码 = worker.Tel ?? string.Empty;
                                person.短信号码 = worker.Mobile ?? string.Empty;
                                person.分站编码 = unit.Type == (int)OrgType.Station ? unit.编码 : AppConfig.GetStringConfigValue("VirtualCode");
                                person.类型编码 = entity.RoleID;
                                person.姓名 = worker.Name;
                                person.工号 = entity.EmpNo;
                                person.口令 = new HashEncrypt().DESEncrypt(encrypt.Decrypt(worker.PassWord), "");
                                person.上次操作时间 = DateTime.Now;
                                person.顺序号 = int.Parse(entity.TPerson编码);
                                person.是否有效 = true;

                                dsContext.TPerson.InsertOnSubmit(person);

                            }
                            else
                            {
                                var tp = dsContext.TPerson.FirstOrDefault(t => t.编码 == entity.TPerson编码);

                                tp.单位编码 = unit.Type == (int)OrgType.Branch ? unit.编码 : "-1";
                                tp.分站编码 = unit.Type == (int)OrgType.Station ? unit.编码 : AppConfig.GetStringConfigValue("VirtualCode");
                                tp.类型编码 = entity.RoleID;
                                tp.工号 = entity.EmpNo;
                            }
                        }

                        //人员
                        if (entity.ID == 0)
                        {
                            var list = from a in dbContext.B_WORKER_ROLE select a.ID;
                            long total = list.LongCount();
                            if (total == 0)
                            {
                                entity.ID = 1;
                            }
                            else
                            {
                                entity.ID = dbContext.B_WORKER_ROLE.Max(a => a.ID) + 1;
                            }

                            entity.EmpNo = entity.EmpNo ?? string.Empty;
                            entity.TPerson编码 = entity.TPerson编码 ?? string.Empty;

                            dbContext.B_WORKER_ROLE.InsertOnSubmit(entity);
                        }
                        else
                        {
                            var model = dbContext.B_WORKER_ROLE.FirstOrDefault(b => b.ID == entity.ID);

                            model.OrgID = entity.OrgID;
                            model.WorkerID = entity.WorkerID;
                            model.RoleID = entity.RoleID;
                            model.EmpNo = entity.EmpNo ?? string.Empty;
                            model.ID = entity.ID;
                            model.TPerson编码 = entity.TPerson编码 ?? string.Empty;
                        }

                        dbContext.SubmitChanges();
                        dsContext.SubmitChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Log4Net.LogError("SaveWorkerRole", ex.ToString());
                        return false;
                    }

                }
            }
        }


        public static bool DelWorkerRole(int id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                using (MainDataContext dsContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
                {
                    try
                    {
                        B_WORKER_ROLE wr = dbContext.B_WORKER_ROLE.SingleOrDefault(t => t.ID == id);
                        dbContext.B_WORKER_ROLE.DeleteOnSubmit(wr);

                        //同步Tperson
                        if (!string.IsNullOrEmpty(wr.TPerson编码))
                        {
                            TPerson tp = dsContext.TPerson.SingleOrDefault(t => t.编码 == wr.TPerson编码);
                            dsContext.TPerson.DeleteOnSubmit(tp);
                        }

                        dbContext.SubmitChanges();
                        dsContext.SubmitChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Log4Net.LogError("DelWorkerRole", ex.ToString());
                        return false;
                    }

                }
            }
        }

        #endregion

        #region 人员所在默认机构信息
        public static B_WORKER_ORGANIZATION GetWorkerDefaultUnit(int id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.B_WORKER_ORGANIZATION.FirstOrDefault(b => b.WorkerID == id && b.Type == 0);
            }
        }


        public static object GetUnitList(int workerId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from wo in dbContext.B_WORKER_ORGANIZATION
                           join o in dbContext.B_ORGANIZATION on wo.OrgID equals o.ID
                           where wo.WorkerID == workerId
                           select new
                           {
                               ID = wo.OrgID,
                               Name = o.Name,
                           };
                return list.ToList();
            }
        }

        /// <summary>
        /// 获得人员兼职机构
        /// </summary>
        /// <param name="workerId"></param>
        /// <returns></returns>
        public static object GetWorkerSideline(int workerId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from wo in dbContext.B_WORKER_ORGANIZATION
                           join o in dbContext.B_ORGANIZATION on wo.OrgID equals o.ID
                           join p in dbContext.G_DATA on wo.PostID.ToString() equals p.Value into left_p
                           from p in left_p.DefaultIfEmpty()
                           join w in dbContext.B_WORKER on wo.ParentID equals w.ID
                           where wo.WorkerID == workerId && p.Type == "Post"
                           select new
                           {
                               OrgID = wo.OrgID,
                               OrgName = o.Name,
                               PostID = wo.PostID,
                               Post = p.Name,
                               ParentID = wo.ParentID,
                               Parent = w.Name,
                               Type = wo.Type==0 ? "默认": "兼职",
                               ID = wo.ID,
                               WorkerID = wo.WorkerID
                           };

                return list.ToList();
            }
        }

        /// <summary>
        /// 保存兼职机构
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="type"></param>
        /// <param name="unitID"></param>
        /// <returns></returns>
        public static bool SaveSideline(B_WORKER_ORGANIZATION entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                using (MainDataContext dsContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
                {
                    if (entity.ID == 0)  //添加
                    {
                        var list = from a in dbContext.B_WORKER_ORGANIZATION select a.ID;
                        long total = list.LongCount();
                        if (total == 0)
                        {
                            entity.ID = 1;
                        }
                        else
                        {
                            entity.ID = dbContext.B_WORKER_ORGANIZATION.Max(a => a.ID) + 1;
                        }

                        dbContext.B_WORKER_ORGANIZATION.InsertOnSubmit(entity);
                    }
                    else
                    {
                        var model = dbContext.B_WORKER_ORGANIZATION.FirstOrDefault(b => b.ID == entity.ID);

                        //机构修改,更新B_WORKER_ROLE和Tperson
                        if (model.OrgID != entity.OrgID)
                        {
                            List<B_WORKER_ROLE> list = dbContext.B_WORKER_ROLE.Where(t => t.WorkerID == model.WorkerID
                                && t.OrgID == model.OrgID).ToList();

                            foreach (B_WORKER_ROLE wr in list)
                            {
                                wr.OrgID = entity.OrgID;

                                if (!string.IsNullOrEmpty(wr.TPerson编码.Trim()))
                                {
                                    var tp = dsContext.TPerson.FirstOrDefault(t => t.编码 == wr.TPerson编码);

                                    B_ORGANIZATION unit = Organize.GetUnit(entity.OrgID);

                                    tp.单位编码 = unit.Type == (int)OrgType.Branch ? unit.编码 : "-1";
                                    tp.分站编码 = unit.Type == (int)OrgType.Station ? unit.编码 : AppConfig.GetStringConfigValue("VirtualCode");

                                }
                            }
                        }

                        model.OrgID = entity.OrgID;
                        model.WorkerID = entity.WorkerID;
                        model.ParentID = entity.ParentID;
                        model.PostID = entity.PostID;
                        model.Remark = entity.Remark;
                        model.Type = entity.Type;
                    }

                    dbContext.SubmitChanges();
                    dsContext.SubmitChanges();
                    return true;
                }
            }
        }

        public static bool DeleteSideline(int id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var model = dbContext.B_WORKER_ORGANIZATION.Single(b => b.ID == id);

                dbContext.B_WORKER_ORGANIZATION.DeleteOnSubmit(model);
                dbContext.SubmitChanges();
                return true;
            }

        }
        #endregion

        public static C_Worker_Level GetLevelByOrg(int workerId, int orgId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return (from a in dbContext.G_DATA
                        join b in dbContext.B_WORKER_ORGANIZATION on a.Value equals b.PostID.ToString()
                        where a.Type == "Post" && b.OrgID == orgId && b.WorkerID == workerId
                        select new C_Worker_Level
                        {
                            Level = a.Sequence,
                            ParentID = b.ParentID,
                            DepartID = b.OrgID
                        }).FirstOrDefault();
            }
        }

        public static C_Worker_Level GetDefaultLevel(int workerId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return (from a in dbContext.G_DATA
                        join b in dbContext.B_WORKER_ORGANIZATION on a.Value equals b.PostID.ToString()
                        where a.Type == "Post" && b.WorkerID == workerId && b.Type == 0
                        select new C_Worker_Level
                        {
                            ID = workerId,
                            Level = a.Sequence,
                            ParentID = b.ParentID,
                            DepartID = b.OrgID
                        }).FirstOrDefault();
            }
        }

        public static bool IsExistEmpNo(string empNo)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                TPerson person = dbContext.TPerson.FirstOrDefault(t => t.工号 == empNo);

                if (person == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static TPerson GetTPerson(string empNo)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TPerson.FirstOrDefault(t => t.工号 == empNo);
            }
        }

        public static List<B_WORKER_ROLE> GetWorkerRole(int workerId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.B_WORKER_ROLE.Where(t => t.WorkerID == workerId).ToList();
            }
        }

    }    
}

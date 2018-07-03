using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;
using Anchor.FA.Utility;
//using System.Data.Objects.SqlClient;

namespace Anchor.FA.DAL.Organize
{
    public class Role
    {
        #region 角色管理
        public static object LoadAllRoleByPage(int page, int rows, string order, string sort)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                 var list = (from a in dbContext.B_ROLE select a);
                 long total = list.LongCount();
                 list = list.OrderBy(r => r.ID);
                 list = list.Skip((page - 1) * rows).Take(rows);
                 var result = new { total = total, rows = list.ToList() };
                 return result;
            }

        }
        public static object LoadAllRoleByPage()
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = (from a in dbContext.B_ROLE select a);
                long total = list.LongCount();
                //list = list.OrderBy(r => r.ID);
                //list = list.Skip((page - 1) * rows).Take(rows);
                var result = new { total = total, rows = list.ToList() };
                return result;
            }

        }

        public static object Edit(int? id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                B_ROLE entity = null;
                if (id != null)
                {
                    entity = dbContext.B_ROLE.FirstOrDefault(a => a.ID == id);
                }
                entity = entity ?? new B_ROLE
                {
                    ID = -1,
                    Name = string.Empty,
                    Remark = string.Empty,           
                };
                return entity;
            }
        }
        public static bool Save(B_ROLE entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                using (MainDataContext dsContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
                {
                    string sqlB;
                    string sqlT;

                    try
                    {

                        if (dbContext.B_ROLE.Count(t => t.ID == entity.ID)==0)  //添加
                        {
                            //var list = from a in dbContext.B_ROLE select a.ID;
                            //long total = list.LongCount();
                            //if (total == 0)
                            //{
                            //    entity.ID = 1;
                            //}
                            //else
                            //{
                            //    entity.ID = dbContext.B_ROLE.Max(a => a.ID) + 1;
                            //}

                            sqlB=string.Format(" INSERT INTO [B_ROLE]([ID],[Name],[Remark]) VALUES ({0},'{1}','{2}')", entity.ID, entity.Name, entity.Remark);

                            sqlT=string.Format(" insert TRole ([编码],[名称],[顺序号],[是否有效])values ({0},'{1}',{2},{3})", entity.ID, entity.Name, 1, dbContext.TRole.Max(a => a.顺序号) + 1);

                        }
                        else  //修改
                        {
                            sqlB=string.Format(" update B_ROLE set name='{0}',remark='{1}' where id={2}", entity.Name, entity.Remark, entity.ID);
                            sqlT=string.Format(" update TRole set 名称='{0}' where 编码={1}", entity.Name, entity.ID);

                        }

                        dbContext.ExecuteCommand(sqlB);
                        dsContext.ExecuteCommand(sqlT);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Log4Net.LogError("Save Role", ex.Message);
                        return false;
                    }
                }
            }
        }
        public static bool Delete(IList<int> idList)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                using (MainDataContext dsContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
                {
                    StringBuilder sbB = new StringBuilder();
                    StringBuilder sbT = new StringBuilder();

                    try
                    {
                        sbB.Append("SET XACT_ABORT ON ");
                        sbB.AppendLine(" BEGIN TRAN ");
                        sbT.Append("SET XACT_ABORT ON ");
                        sbT.AppendLine(" BEGIN TRAN ");
                        foreach (int g in idList)
                        {
                            var Rmodel = dbContext.B_ROLE.Single(a => a.ID == g);

                            if (Rmodel != null)
                            {
                                sbB.AppendFormat("delete B_ROLE where id={0}", g);
                            }

                            TRole role = dbContext.TRole.FirstOrDefault(a => a.编码 == g);
                            if (role != null)
                            {
                                sbT.AppendFormat("delete TRole where 编码={0}", g);
                            }

                        }
                        sbB.AppendLine(" COMMIT TRAN ");
                        sbT.AppendLine(" COMMIT TRAN ");

                        dbContext.ExecuteCommand(sbB.ToString());
                        dsContext.ExecuteCommand(sbT.ToString());

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Log4Net.LogError("Save Role", ex.Message);
                        return false;
                    }
                }
            }

        }  
        #endregion

        #region 权限管理
        public static List<int> LoadRoleAction(int roleId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                List<int> list = (from r in dbContext.B_ROLE_ACTION
                           join a in dbContext.B_ACTION on r.ActionID equals a.ID
                           where r.RoleID == roleId && a.ParentID!=0
                            select r.ActionID).ToList<int>();

                return list;
            }
        }

        public static List<string> LoadRoleActionRange(int roleId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                List<string> listA = (from r in dbContext.B_ROLE_ACTION
                                      join a in dbContext.B_ACTION on r.ActionID equals a.ID
                                      where r.RoleID == roleId && a.ParentID != 0
                                      select r.ActionID).ToList()
                                     .Select(t=>"a-" + t.ToString())
                                     .ToList<string>();

                //List<C_TelBook> lr = (from r in dbContext.B_ROLE_Range
                //         join a in dbContext.B_Range
                //         on new { ActionID = r.ActionID, r.Range } equals new { ActionID = a.ActionId, a.Range }
                //         where r.RoleID == roleId
                //                      select new C_TelBook { ID = r.ActionID, Name = r.Range, OwnerID = 0, OrderNo=0 }).ToList();

                //List<string> listR = lr
                //                      //"r-" + SqlFunctions.StringConvert((decimal)r.ActionID) + "-" + r.Range)
                //                     .Select(t => "r-" + t.ID.ToString() + "-" + t.Name)//用C_TelBook对象就是为了类型转化 暂用下吧
                //                      .ToList<string>();

                List<string> listR = dbContext.ExecuteQuery<string>(string.Format(@"
select 'r-' + convert(varchar(20),r.ActionID)+'-'+ r.Range
from B_ROLE_Range r
join B_Range a on r.ActionID=a.ActionId and r.Range=a.Range
where  r.RoleID = {0}", roleId)).ToList();

                listA.AddRange(listR);
                return listA;
            }
        }

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns>List</returns>
        public static List<B_ROLE> LoadAllRole()
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.B_ROLE.ToList();
            }
        }


        public static bool SaveRoleAction(int roleId, IList<int> listActionId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                List<B_ROLE_ACTION> list=dbContext.B_ROLE_ACTION.Where(t => t.RoleID == roleId).ToList();

                foreach (B_ROLE_ACTION b in list)
                {
                    //dbContext.B_ROLE_ACTION.Load();
                    dbContext.B_ROLE_ACTION.DeleteOnSubmit(b);
                }


                foreach(int actionid in listActionId)
                {

                    B_ROLE_ACTION b = new B_ROLE_ACTION();

                    b.RoleID = roleId;
                    b.ActionID = actionid;


                    dbContext.B_ROLE_ACTION.InsertOnSubmit(b);
                }

                dbContext.SubmitChanges();

                return true;
            }
        }
        public static bool SaveRoleAction(int roleId, IList<int> listActionId, IList<string> listRangeId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                List<B_ROLE_ACTION> list = dbContext.B_ROLE_ACTION.Where(t => t.RoleID == roleId).ToList();

                foreach (B_ROLE_ACTION b in list)
                {
                    //dbContext.B_ROLE_ACTION.Load();
                    dbContext.B_ROLE_ACTION.DeleteOnSubmit(b);
                }


                foreach (int actionid in listActionId)
                {

                    B_ROLE_ACTION b = new B_ROLE_ACTION();

                    b.RoleID = roleId;
                    b.ActionID = actionid;


                    dbContext.B_ROLE_ACTION.InsertOnSubmit(b);
                }

                List<B_ROLE_Range> listr = dbContext.B_ROLE_Range.Where(t => t.RoleID == roleId).ToList();

                foreach(B_ROLE_Range r in listr)
                {
                    dbContext.B_ROLE_Range.DeleteOnSubmit(r);
                }

                foreach (string s in listRangeId)
                {

                    B_ROLE_Range b = new B_ROLE_Range();

                    int i=s.IndexOf('-');
                    b.RoleID = roleId;
                    b.ActionID = int.Parse(s.Substring(0,i));
                    b.Range = s.Substring(i+1);
 
                    dbContext.B_ROLE_Range.InsertOnSubmit(b);
                }


                dbContext.SubmitChanges();

                return true;
            }
        }
        //public static bool DeleteRoleAction(IList<string> idList)
        //{
        //    using (MainDataContext dbContext = new MainDataContext())
        //    {
        //        long n = idList.LongCount();
        //        for (int i = 0; i < n; i++)
        //        {
        //            string[] SArr = idList[i].Split(',');
        //            int[] Arr = new int[SArr.Length];
        //            int RoleID = Convert.ToInt32(SArr[0]);
        //            int ActionID = Convert.ToInt32(SArr[1]);
        //            var model = dbContext.B_ROLE_ACTION.Single(a => a.RoleID == RoleID && a.ActionID == ActionID);
        //            dbContext.B_ROLE_ACTION.Load();
        //            dbContext.B_ROLE_ACTION.Remove(model);
        //        }
        //        dbContext.SubmitChanges();
        //        return true;
        //    }

        //}  
        #endregion
    }
}

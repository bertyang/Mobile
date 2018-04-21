using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;


namespace Anchor.FA.DAL.Organize
{
    /// <summary>
    /// 功能项
    /// </summary>
    public class Action
    {
        #region 贾明

        #region 查询方法

        #region 获取功能项菜单
        /// <summary>
        /// 获取功能项菜单
        /// </summary>
        /// <param name="superiorID">功能项ID</param>
        /// <param name="userId">人员ID</param>
        /// <returns>List</returns>
        public static List<B_ACTION> GetMenu(string superiorID, string userId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                //DbSet<B_ACTION> set = dbContext.Set<B_ACTION>();
//                List<B_ACTION> list = set.SqlQuery(string.Format(@"
//With menu as( Select a.* From B_ACTION a 
//where exists(select * from B_ROLE_ACTION r where r.ActionID=a.ID
//    and exists(select * from  B_WORKER_ROLE wr where wr.RoleID=r.RoleID
//        and exists(select * from B_WORKER w where w.ID=wr.WorkerID and  w.ID='{0}')
//        )
//    ) 
//union all select c.* from B_ACTION c  join menu b on c.id =b.parentid ) 
//select * from menu where IsActive='Y' ORDER BY OrderID ", userId)).ToList(); 
                List<B_ACTION> list = dbContext.ExecuteQuery<B_ACTION>(string.Format(@"
With menu as( Select a.* From B_ACTION a 
where exists(select * from B_ROLE_ACTION r where r.ActionID=a.ID
    and exists(select * from  B_WORKER_ROLE wr where wr.RoleID=r.RoleID
        and exists(select * from B_WORKER w where w.ID=wr.WorkerID and  w.ID='{0}')
        )
    ) 
--union all select c.* from B_ACTION c join menu b on c.id =b.parentid 
--这里为什么要递归？仅仅因为easyUI树形选择造成的。父节点的选择代表全选。新系统得重新设置保存权限
) 
select * from menu where IsActive='Y' ORDER BY OrderID ", userId)).ToList();
                return list;
            }
        }

        public static List<B_ACTION> GetMenu(string superiorID)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                //DbSet<B_ACTION> set = dbContext.Set<B_ACTION>();
                List<B_ACTION> list = dbContext.ExecuteQuery<B_ACTION>(@"With menu as( Select a.ID,a.url,
                            a.Remark,a.ParentID,a.Icon,a.IsActive,a.OrderID From B_ACTION a   
                            where a.ParentID=" + superiorID +
                            " union all select c.* from B_ACTION c join menu b on c.parentid=b.id )select * from menu where IsActive='Y' ORDER BY OrderID").ToList();
                return list;
            }
        }
        public static List<C_MENU_TREE> GetMenuRange(string superiorID)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                //DbSet<B_ACTION> set = dbContext.Set<B_ACTION>();

                List<C_MENU_TREE> list = dbContext.ExecuteQuery<C_MENU_TREE>(string.Format(@"
With menu as( Select a.ID,a.url,
a.Remark,a.ParentID,a.Icon,a.IsActive,a.OrderID From B_ACTION a   
where a.ParentID={0} union all select c.* from B_ACTION c join menu b on c.parentid=b.id )
select * into #temp from menu where IsActive='Y'

(
select id=convert(varchar(50),'a-'+convert(varchar(20),id)),[text]=Remark,ParentID=convert(varchar(50),'a-'+convert(varchar(20),ParentID)),OrderID from #temp
union all
select id=convert(varchar(50),'r-'+convert(varchar(20),ActionId)+'-'+[Range]),[text]=RangeReamrk,ParentID=convert(varchar(50),'a-'+convert(varchar(20),ActionId)),OrderID from B_Range b 
where exists(select * from #temp a where a.ID=b.ActionId) and IsActive=1
)
order by OrderID

drop table #temp

", superiorID)).ToList();



                return list;
            }
        }


        #endregion


        #endregion


        #endregion
        #region 功能项管理
        public static object LoadAllActionByPage(int page, int rows, string order, string sort)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = (from a in dbContext.B_ACTION where a.IsActive =="Y" select a);
                long total = list.LongCount();

                list = list.OrderBy(p => p.ID);
                list = list.Skip((page - 1) * rows).Take(rows);

                var result = new { total = total, rows = list.ToList() };
                
                return result;
            }

        }

        public static object Edit(int? id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                B_ACTION entity = null;
                if (id != null)
                {
                    entity = dbContext.B_ACTION.FirstOrDefault(a => a.ID == id);
                }
                entity = entity ?? new B_ACTION
                {
                    ID = 0,
                    Url = string.Empty,
                    Remark = string.Empty,
                    ParentID = null,
                };
                return entity;
            }
        }
        public static bool Save(B_ACTION entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                if (dbContext.B_ACTION.Count(a => a.ID == entity.ID)==0)  //添加
                {
                    //var list = from a in dbContext.B_ACTION select a.ID;
                    //long total = list.LongCount();
                    //if (total == 0)
                    //{
                    //    entity.ID = 1;
                    //}
                    //else
                    //{
                    //    entity.ID = dbContext.B_ACTION.Max(a => a.ID) + 1;
                    //}
                    dbContext.B_ACTION.InsertOnSubmit(entity);
                    dbContext.SubmitChanges();
                    return true;
                }
                else  //修改
                {
                    var model = dbContext.B_ACTION.FirstOrDefault(a => a.ID == entity.ID);
                    model.Remark = entity.Remark;
                    model.Url = entity.Url;
                    model.Icon = entity.Icon;
                    model.OrderID = entity.OrderID;
                    model.ParentID = entity.ParentID;
                    try
                    {
                        dbContext.SubmitChanges();
                    }
                    catch (Exception e)
                    {
                        
                        throw;
                    }
                    return true;

                }
            }
        }
        public static bool Delete(IList<int> idList)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                foreach (int g in idList)
                {
                    var model = dbContext.B_ACTION.Single(a => a.ID == g);
                    //dbContext.B_ACTION.Load();
                    dbContext.B_ACTION.DeleteOnSubmit(model);
                }
                dbContext.SubmitChanges();
                return true;
            }

        }
        /// <summary>
        /// 获取所有功能项
        /// </summary>
        /// <param name="id">功能项ID</param>
        /// <returns>List</returns>
        public static List<B_ACTION> GetAllAction()
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.B_ACTION.ToList();
            }
        }  
        #endregion
    }
}

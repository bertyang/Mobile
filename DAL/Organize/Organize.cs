using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Anchor.FA.Model;
using Anchor.FA.Utility;

namespace Anchor.FA.DAL.Organize
{
    /// <summary>
    /// 组织机构
    /// </summary>
    public class Organize
    {
      
        #region 获取组织机构树
        /// <summary>
        /// 获取组织机构
        /// </summary>
        /// <param name="id">机构编码</param>
        /// <returns>List</returns>
        public static B_ORGANIZATION GetUnit(int id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                 return dbContext.B_ORGANIZATION.FirstOrDefault(v => v.ID == id);
            }
        }

        /// <summary>
        /// 获取所有组织机构
        /// </summary>
        /// <returns></returns>
        public static List<B_ORGANIZATION> GetAllUnit()
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.B_ORGANIZATION.OrderBy(t => t.Sort).ToList();
            }
        }

        /// <summary>
        /// 获取指定机构下的所有机构
        /// </summary>
        /// <param name="orgId">指定机构</param>
        /// <returns></returns>
//        public static List<B_ORGANIZATION> GetAllUnit(string orgId)
//        {
//            using (MainDataContext dbContext = new MainDataContext())
//            {

//                //DbSet<B_ORGANIZATION> set = dbContext.Set<B_ORGANIZATION>();
//                List<B_ORGANIZATION> list = dbContext.ExecuteQuery<B_ORGANIZATION>(@"with ss as 
//                               (select a.ID,a.Name,a.ParentID,a.ManagerID,a.编码,a.Type,a.Sort
//                                 from B_ORGANIZATION a
//                                 where a.ID in(" + orgId +
//                                    @") union all select b.ID,b.Name,b.ParentID,b.ManagerID,b.编码,b.Type,b.Sort from B_ORGANIZATION b join ss on b.ParentID=ss.ID  )
//                                    select distinct * from ss").ToList();

//                return list;
//            }
//        }

        #endregion



        //获取当前登陆者所属的机构
        public static List<B_ORGANIZATION> GetUnitByLoginer(int CurrentUserID)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                List<int> orgList = GetOrg(CurrentUserID);
                string orgStr = string.Join(", ", orgList.ToArray());
                if (orgStr == "")
                {
                    return null;
                }

                //DbSet<B_ORGANIZATION> set = dbContext.Set<B_ORGANIZATION>();
                List<B_ORGANIZATION> list = dbContext.ExecuteQuery<B_ORGANIZATION>(@"with ss as 
                               (select a.ID,a.Name,a.ParentID,a.ManagerID,a.编码,a.Type,a.Sort
                                 from B_ORGANIZATION a
                                 where a.ID in ( " + orgStr +
                                ") union all select b.ID,b.Name,b.ParentID,b.ManagerID,b.编码,b.Type,b.Sort from B_ORGANIZATION b join ss s on s.ParentID=b.ID ) select distinct * from ss").ToList();

                return list;
            }
        }

        public static List<int> GetOrg(int CurrentUserID)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.B_WORKER_ORGANIZATION.Where(b => b.WorkerID == CurrentUserID).Select(b => b.OrgID).ToList();
            }
        }
    }
}
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;


namespace Anchor.FA.DAL.Organize
{
    public class Range
    {
        public static List<B_Range> GetRangeFromRole(int RoleID)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                List<B_Range> list = (from r in dbContext.B_Range
                                      where dbContext.B_ROLE_Range.Any(t => t.ActionID == r.ActionId && t.Range == r.Range && t.RoleID == RoleID)
                                      select r
                                     ).ToList();
                return list;
            }
        }
        public static List<B_Range> GetRangeFromWorkerID(int WorkerID)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                List<B_Range> list = (from r in dbContext.B_Range
                                      where dbContext.B_ROLE_Range.Any(t => t.ActionID == r.ActionId && t.Range == r.Range
                                          && dbContext.B_WORKER_ROLE.Any(wr => wr.RoleID == t.RoleID && wr.WorkerID == WorkerID))
                                      select r
                                     ).ToList();
                return list;
            }
        }
        /// <summary>
        /// 以前的方法 获得某个页面的按钮权限
        /// </summary>
        public static List<B_Range> GetRangeFromWorkerID(int WorkerID, int ActionID)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                List<B_Range> list = (from r in dbContext.B_Range
                                      where dbContext.B_ROLE_Range.Any(t => t.ActionID == r.ActionId && t.Range == r.Range
                                          && dbContext.B_WORKER_ROLE.Any(wr => wr.RoleID == t.RoleID && wr.WorkerID == WorkerID))
                                          && r.ActionId == ActionID
                                      select r
                                     ).ToList();
                return list;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;
using Anchor.FA.Utility;

namespace Anchor.FA.DAL.Organize
{
    public class Branch
    {
        public static object LoadAllBranch(int stationId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from tc in dbContext.TZBranch
                           join bo in dbContext.B_ORGANIZATION on tc.编码.ToString() equals bo.编码
                           join po in dbContext.B_ORGANIZATION on bo.ParentID equals po.ID
                           join bw in dbContext.B_WORKER on bo.ManagerID equals bw.ID
                           where bo.Type == (int)OrgType.Branch && po.ID == stationId
                           select new
                           {
                               ID = bo.ID,
                               编码 = tc.编码,
                               名称 = tc.名称,
                               顺序号 = tc.顺序号,
                               启用 = tc.是否有效 == true ? "是" : "否",
                               负责人= bw.Name,
                               ManagerID = bo.ManagerID,
                               分站ID = po.ID,
                               分站编码 = po.编码,
                               分站名称 = po.Name
                           };

                return list.ToList();
            }

        }

        public static TZBranch GetBranch(int id)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                TZBranch entity = null;
                if (id != null)
                {
                    entity = dbContext.TZBranch.FirstOrDefault(t => t.编码 == id);
                }
                entity = entity ?? new TZBranch
                {
                    编码 = 0,
                    名称 = string.Empty,
                    顺序号 = 0,
                    是否有效 = true
                };
                return entity;
            }
        }

        public static bool Delete(IList<int> idList)
        {
            try
            {
                List<B_ORGANIZATION> listOrgBranch = DataAccess<B_ORGANIZATION>.Where(t => idList.Contains(t.ID)).ToList();
                
                //删除TZBranch
                List<TZBranch> listTZBranch = DataAccess<TZBranch>.Where(AppConfig.ConnectionStringDispatch,
                    t => listOrgBranch.Select(s => s.编码).Contains(t.编码.ToString()));

                DataAccess<TZBranch>.Delete(AppConfig.ConnectionStringDispatch, listTZBranch);

                //删除B_ORGANIZATION
                DataAccess<B_ORGANIZATION>.Delete(listOrgBranch);

                return true;
            }
            catch (Exception ex)
            {
                Log4Net.LogError("批量删除科室", ex.Message);
                return false;
            }


        }
    }
}

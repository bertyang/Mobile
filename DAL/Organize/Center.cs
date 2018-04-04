using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;
using Anchor.FA.Utility;
namespace Anchor.FA.DAL.Organize
{
    public class Center
    {
        public static object LoadAllCenter()
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from tc in dbContext.TCenter
                           join tzct in dbContext.TZCenterType on tc.类型编码 equals tzct.编码
                           join tzc in dbContext.TCenter on tc.所属调度中心编码 equals tzc.编码
                           join bo in dbContext.B_ORGANIZATION on tc.编码.ToString() equals bo.编码
                           join bw in dbContext.B_WORKER on bo.ManagerID equals bw.ID
                           where bo.Type == (int)OrgType.Center
                           select new
                           {
                               ID = bo.ID,
                               编码 = tc.编码,
                               名称 = tc.名称,
                               拼音头 = tc.拼音头,
                               类型编码 = tc.类型编码,
                               类型名称 = tzct.名称,
                               是否调度 = tc.是否调度 == true ? "是" : "否",
                               IP地址 = tc.IP地址,
                               CTI服务IP地址 = tc.CTI服务IP地址,
                               电话号码 = tc.电话号码,
                               所属调度中心编码 = tc.所属调度中心编码,
                               所属调度中心 = tzc.名称,
                               当前任务流水号 = tc.当前任务流水号,
                               顺序号 = tc.顺序号,
                               启用 = tc.是否有效 == true ? "是" : "否",
                               是否发送短信 = tc.是否发送短信 == true ? "是" : "否",
                               负责人 = bw.Name,
                               ManagerID = bo.ManagerID,
                           };

                //list = list.OrderBy(r => r.顺序号);

                return list.ToList();
            }

        }

        public static TCenter GetCenter(int id)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                TCenter entity = null;
                if (id != null)
                {
                    entity = dbContext.TCenter.FirstOrDefault(t => t.编码 == id);
                }
                entity = entity ?? new TCenter
                {
                    编码 = 0,
                    名称 = string.Empty,
                    拼音头 = string.Empty,
                    类型编码 = 0,
                    IP地址 = string.Empty,
                    CTI服务IP地址 = string.Empty,
                    电话号码 = string.Empty,
                    所属调度中心编码 = 0,
                    当前任务流水号 = 0,
                    顺序号 = 0,
                    是否有效 = true,
                    是否发送短信 = true,
                };
                return entity;
            }
        }

        public static bool Delete(IList<int> idList)
        {
            try
            {
                List<B_ORGANIZATION> listOrgCenter = DataAccess<B_ORGANIZATION>.Where(t => idList.Contains(t.ID)).ToList();
                List<B_ORGANIZATION> listOrgStation = DataAccess<B_ORGANIZATION>.Where(t => idList.Contains(t.ParentID)).ToList();
                List<B_ORGANIZATION> listOrgBranch = DataAccess<B_ORGANIZATION>.Where(t => listOrgStation.Select(s=>s.ID).Contains(t.ParentID)).ToList();

                //删除TCenter
                List<TCenter> listTCenter = DataAccess<TCenter>.Where(AppConfig.ConnectionStringDispatch, 
                    t => listOrgCenter.Select(s => s.编码).Contains(t.编码.ToString()));

                DataAccess<TCenter>.Delete(AppConfig.ConnectionStringDispatch, listTCenter);

                //删除TStation
                List<TStation> listTStation = DataAccess<TStation>.Where(AppConfig.ConnectionStringDispatch,
                    t => listOrgStation.Select(s => s.编码).Contains(t.编码));

                DataAccess<TStation>.Delete(AppConfig.ConnectionStringDispatch, listTStation);

                //删除TZBranch
                List<TZBranch> listTZBranch = DataAccess<TZBranch>.Where(AppConfig.ConnectionStringDispatch,
                    t => listOrgBranch.Select(s => s.编码).Contains(t.编码.ToString()));

                DataAccess<TZBranch>.Delete(AppConfig.ConnectionStringDispatch, listTZBranch);

                //删除B_ORGANIZATION
                DataAccess<B_ORGANIZATION>.Delete(listOrgBranch);
                DataAccess<B_ORGANIZATION>.Delete(listOrgStation);
                DataAccess<B_ORGANIZATION>.Delete(listOrgCenter);

                return true;
            }
            catch (Exception ex)
            {
                Log4Net.LogError("批量删除中心", ex.Message);
                return false;
            }


        }
    }
}

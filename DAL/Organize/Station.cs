using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;
using Anchor.FA.Utility;

namespace Anchor.FA.DAL.Organize
{
    public class Station
    {
        public static object LoadAllStation(int centerId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from ts in dbContext.TStation 
                           join tzst in dbContext.TZStationType on  ts.类型编码 equals tzst.编码
                           join tc in dbContext.TCenter on ts.中心编码 equals tc.编码
                           join bo in dbContext.B_ORGANIZATION on ts.编码 equals bo.编码
                           join bw in dbContext.B_WORKER on bo.ManagerID equals bw.ID
                           where bo.Type == (int)OrgType.Station && bo.ParentID==centerId
                            select new
                            {
                                ID=bo.ID,
                                编码 = ts.编码,
                                名称 = ts.名称,
                                中心名称 = tc.名称,
                                分站类型 = tzst.名称,
                                IP地址 = ts.IP地址,
                                电话号码 = ts.电话号码,
                                所属区域 = ts.所属区域,
                                通信标识 = ts.通信标识,
                                类型编码 = ts.类型编码,
                                中心编码 = ts.中心编码,
                                X坐标 = ts.X坐标,
                                Y坐标 = ts.Y坐标,
                                顺序号 = ts.顺序号,
                                是否调度 = ts.是否调度 == true ? "是" : "否",
                                启用 = ts.是否有效 == true ? "是" : "否",
                                是否传送GPS = ts.是否传送GPS == true ? "是" : "否",
                                是否标注 = ts.是否标注 == true ? "是" : "否",
                                负责人 = bw.Name,
                                ManagerID = bo.ManagerID
                            };

                //list = list.OrderBy(r => r.顺序号);

                return list.ToList();
            }
        }

        public static TStation GetStation(string id)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                TStation entity = null;
                if (id != null)
                {
                    entity = dbContext.TStation.FirstOrDefault(t => t.编码 == id);
                }
                entity = entity ?? new TStation
                {
                    编码 = "",
                    名称 = string.Empty,
                    中心编码 = 0,
                    IP地址 = string.Empty,
                    是否调度 = true,
                    电话号码 = string.Empty,
                    所属区域 = string.Empty,
                    X坐标 = 0,
                    Y坐标 = 0,
                    顺序号 = 0,
                    是否有效 =  true
                };
                return entity;
            }
        }

        public static bool Delete(IList<int> idList)
        {
            try
            {
                List<B_ORGANIZATION> listOrgStation = DataAccess<B_ORGANIZATION>.Where(t => idList.Contains(t.ID)).ToList();
                List<B_ORGANIZATION> listOrgBranch = DataAccess<B_ORGANIZATION>.Where(t => idList.Contains(t.ParentID)).ToList();

                //删除TStation
                List<TStation> listTStation = DataAccess<TStation>.Where(AppConfig.ConnectionStringDispatch,
                    t => listOrgStation.Select(s => s.编码).Contains(t.编码));

                DataAccess<TStation>.Delete(AppConfig.ConnectionStringDispatch, listTStation);

                //删除TZBranch
                List<TZBranch> listTZBranch = DataAccess<TZBranch>.Where(AppConfig.ConnectionStringDispatch,
                    t => listOrgBranch.Select(s => s.编码).Contains(t.编码.ToString()));

                DataAccess<TZBranch>.Delete(AppConfig.ConnectionStringDispatch, listTZBranch);

                //删除B_ORGANIZATION
                DataAccess<B_ORGANIZATION>.Delete(listOrgStation);
                DataAccess<B_ORGANIZATION>.Delete(listOrgBranch);
                
                return true;
            }
            catch (Exception ex)
            {
                Log4Net.LogError("批量删除分站", ex.Message);
                return false;
            }
         

        }

    }
}

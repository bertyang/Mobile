using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;
using Anchor.FA.Utility;
using System.Transactions;

namespace Anchor.FA.BLL.Organize
{
    public class Station
    {
        public object LoadAllStation(int centerId)
        {
            return DAL.Organize.Station.LoadAllStation(centerId);
        }

        public object LoadAllStationType()
        {
            return DataAccess<TZStationType>.ToList(AppConfig.ConnectionStringDispatch);
        }

        public TStation GetStation(string id)
        {
            return DAL.Organize.Station.GetStation(id);
        }

        public bool Save(TStation entity, int managerId)
        {
            if (DataAccess<TStation>.ToList(AppConfig.ConnectionStringDispatch).Count(t=>t.编码 ==entity.编码) ==0)  //添加
            {
                //TStation
                //long total = DataAccess<TStation>.ToList(AppConfig.ConnectionStringDispatch).Count;

                //if (total == 0)
                //{
                //    entity.编码 = "001";
                //}
                //else
                //{
                //    int code = int.Parse(DataAccess<TStation>.ToList(AppConfig.ConnectionStringDispatch).Max(a => a.编码)) + 1;
                //    entity.编码 = code.ToString().PadLeft(3, '0'); ;
                //}

                //B_ORGANIZATION表
                B_ORGANIZATION org = new B_ORGANIZATION();

                org.ID = DataAccess<B_ORGANIZATION>.ToList().Max(a => a.ID) + 1; ;
                org.ManagerID = managerId;
                org.Name = entity.名称;
                org.Type = (int)OrgType.Station;
                org.编码 = entity.编码.ToString();

                Func<B_ORGANIZATION, bool> o = s => s.编码 == entity.中心编码.ToString() && s.Type == (int)OrgType.Center;
                org.ParentID = DataAccess<B_ORGANIZATION>.SingleOrDefault(o).ID;

                //Insert
                //using (TransactionScope scope = new TransactionScope())
                //{
                    try
                    {
                        DataAccess<B_ORGANIZATION>.Insert(org);
                        DataAccess<TStation>.Insert(AppConfig.ConnectionStringDispatch,entity);
                        //scope.Complete();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Log4Net.LogError("SaveSation", ex.ToString());
                        return false;
                    }

                //}

            }
            else  //修改
            {
                //using (TransactionScope scope = new TransactionScope())
                //{
                    try
                    {
                        //TCenter
                        DataAccess<TStation>.Update(AppConfig.ConnectionStringDispatch, entity);

                        //B_ORGANIZATION
                        Func<B_ORGANIZATION, bool> o = s => s.编码 == entity.编码.ToString() && s.Type == (int)OrgType.Station;

                        B_ORGANIZATION org = DataAccess<B_ORGANIZATION>.SingleOrDefault(o);

                        org.ManagerID = managerId;
                        org.Name = entity.名称;

                        Func<B_ORGANIZATION, bool> p = s => s.编码 == entity.中心编码.ToString() && s.Type == (int)OrgType.Center;
                        org.ParentID = DataAccess<B_ORGANIZATION>.SingleOrDefault(p).ID;

                        DataAccess<B_ORGANIZATION>.Update(org);

                        //B_WORKER_ORGANIZATION
                        Func<B_WORKER_ORGANIZATION, bool> c = s => s.OrgID == org.ID;

                        List<B_WORKER_ORGANIZATION> list = DataAccess<B_WORKER_ORGANIZATION>.Where(c).ToList();

                        foreach (B_WORKER_ORGANIZATION wo in list)
                        {
                            if (wo.WorkerID != managerId)
                            {
                                wo.ParentID = managerId;

                                DataAccess<B_WORKER_ORGANIZATION>.Update(wo);
                            }

                        }
                        //scope.Complete();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Log4Net.LogError("SaveCenter", ex.ToString());
                        return false;
                    }

                }
            //}
        }

        public bool Delete(IList<int> idList)
        {
            return DAL.Organize.Station.Delete(idList);
        }

        public List<TZArea> LoadAllArea()
        {
            return DataAccess<TZArea>.ToList();
        }
    }
}

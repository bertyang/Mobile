using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;
using Anchor.FA.Utility;
using System.Transactions;

namespace Anchor.FA.BLL.Organize
{
    public class Branch
    {
        public object LoadAllBranch(int stationId)
        {
            return DAL.Organize.Branch.LoadAllBranch(stationId);
        }

        public TZBranch GeTZBranch(int id)
        {
            return DAL.Organize.Branch.GetBranch(id);
        }

        public bool Save(TZBranch entity, int managerId, int stationId)
        {
            if (DataAccess<TZBranch>.ToList(AppConfig.ConnectionStringDispatch).Count(t => t.编码 == entity.编码) == 0)  //添加
            {
                //TZBranch
                //long total = DataAccess<TZBranch>.ToList(AppConfig.ConnectionStringDispatch).Count;

                //if (total == 0)
                //{
                //    entity.编码 = 1;
                //}
                //else
                //{
                //    entity.编码 = DataAccess<TZBranch>.ToList(AppConfig.ConnectionStringDispatch).Max(a => a.编码) + 1;
                //}

                //B_ORGANIZATION
                B_ORGANIZATION org = new B_ORGANIZATION();

                org.ID = DataAccess<B_ORGANIZATION>.ToList().Max(a => a.ID) + 1; ;
                org.ManagerID = managerId;
                org.Name = entity.名称;
                org.Type = (int)OrgType.Branch;
                org.编码 = entity.编码.ToString();
                org.ParentID = stationId;

                //Insert
                //using (TransactionScope scope = new TransactionScope())
                //{
                    try
                    {
                        DataAccess<B_ORGANIZATION>.Insert(org);
                        DataAccess<TZBranch>.Insert(AppConfig.ConnectionStringDispatch,entity);
                        //scope.Complete();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Log4Net.LogError("SaveBranch", ex.ToString());
                        return false;
                    }
                //}
            }
            else
            {
                //using (TransactionScope scope = new TransactionScope())
                //{
                    try
                    {
                        //TCenter
                        DataAccess<TZBranch>.Update(AppConfig.ConnectionStringDispatch, entity);

                        //B_ORGANIZATION
                        Func<B_ORGANIZATION, bool> o = s => s.编码 == entity.编码.ToString() && s.Type == (int)OrgType.Branch;

                        B_ORGANIZATION org = DataAccess<B_ORGANIZATION>.SingleOrDefault(o);

                        org.ManagerID = managerId;
                        org.Name = entity.名称;
                        org.ParentID = stationId;
                      
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
                        Log4Net.LogError("SaveBranch", ex.ToString());
                        return false;
                    }

                }
            //}

        }

        public bool Delete(IList<int> idList)
        {
            return DAL.Organize.Branch.Delete(idList);
        }
    }
}

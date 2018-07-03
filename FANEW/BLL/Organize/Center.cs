using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;
using Anchor.FA.Utility;
using System.Transactions;

namespace Anchor.FA.BLL.Organize
{
    public class Center
    {
        public object LoadAllCenter()
        {
            return DAL.Organize.Center.LoadAllCenter();
        }

        public object LoadAllCenterType()
        {
            return DataAccess<TZCenterType>.ToList(AppConfig.ConnectionStringDispatch);
        }

        public TCenter GetCenter(int id)
        {
            return DAL.Organize.Center.GetCenter(id);
        }

        public bool Save(TCenter entity,int managerId)
        {
            if (DataAccess<TCenter>.ToList(AppConfig.ConnectionStringDispatch).Count(t => t.编码 == entity.编码) == 0)  //添加
            {
                //TCenter
                //long total = DataAccess<TCenter>.ToList(AppConfig.ConnectionStringDispatch).Count;

                //if (total == 0)
                //{
                //    entity.编码 = 1;
                //}
                //else
                //{
                //    entity.编码 = DataAccess<TCenter>.ToList(AppConfig.ConnectionStringDispatch).Max(a => a.编码) + 1;
                //}

                //B_ORGANIZATION
                B_ORGANIZATION org = new B_ORGANIZATION();

                org.ID = DataAccess<B_ORGANIZATION>.ToList().Max(a => a.ID) + 1; ;
                org.ManagerID = managerId;
                org.Name = entity.名称;
                org.ParentID = 1;
                org.Type = (int)OrgType.Center;
                org.编码 = entity.编码.ToString();

                //Insert
                //using (TransactionScope scope = new TransactionScope())
                //{
                    try
                    {
                        DataAccess<B_ORGANIZATION>.Insert(org);
                        DataAccess<TCenter>.Insert(AppConfig.ConnectionStringDispatch,entity);
                        //scope.Complete();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Log4Net.LogError("SaveCenter", ex.ToString());
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
                        DataAccess<TCenter>.Update(AppConfig.ConnectionStringDispatch,entity);

                        //B_ORGANIZATION
                        Func<B_ORGANIZATION, bool> o = s => s.编码 == entity.编码.ToString() && s.Type == (int)OrgType.Center;

                        B_ORGANIZATION org = DataAccess<B_ORGANIZATION>.SingleOrDefault(o);

                        org.ManagerID = managerId;
                        org.Name = entity.名称;

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
            return DAL.Organize.Center.Delete(idList);
        }

    }
}

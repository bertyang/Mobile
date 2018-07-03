using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Anchor.FA.Model;


namespace Anchor.FA.DAL.WorkFlow
{
    public class Flow
    {
        public static F_FLOW Get(int id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_FLOW.FirstOrDefault(t => t.ID == id);
            }
        }

        public static List<F_FLOW> GetAll(string type)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                if (type.Equals("inner"))
                {
                    return dbContext.F_FLOW.Where(t => t.IsInner == "Y" && t.Active=="Y").ToList();
                }
                else if (type.Equals("external"))
                {
                    return dbContext.F_FLOW.Where(t => t.IsInner == "N" && t.Active == "Y").ToList();
                }
                else
                {
                    return dbContext.F_FLOW.Where(t => t.Active == "Y").ToList();
                }
            }
        }
        
        public static int Insert(F_FLOW entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from a in dbContext.F_FLOW select a.ID;

                long total = list.LongCount();

                if (total == 0)
                {
                    entity.ID = 1;
                }
                else
                {
                    entity.ID = dbContext.F_FLOW.Max(a => a.ID) + 1;
                }


                dbContext.F_FLOW.InsertOnSubmit(entity);

                dbContext.SubmitChanges();

                return entity.ID;
            }
        }

        public static bool Save(F_FLOW entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                
                var model = dbContext.F_FLOW.FirstOrDefault(t => t.ID == entity.ID);

                model.Name = entity.Name;
                model.Description = entity.Description;
                model.CatalogID = entity.CatalogID;
                //model.CreateDate = entity.CreateDate; 
                model.ModifyDate = entity.ModifyDate;
                model.LayoutType = entity.LayoutType;
                model.Url = entity.Url;
                model.IsInner = entity.IsInner;

                dbContext.SubmitChanges();
            }

            return true;
        }

        public static bool Delete(int id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var model = dbContext.B_WORKER.SingleOrDefault(t => t.ID == id);

                if (model != null)
                {
                    //dbContext.B_WORKER.Load();
                    dbContext.B_WORKER.DeleteOnSubmit(model);

                    dbContext.SubmitChanges();

                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public static List<F_FLOW_CONFIG> GetFlowConfig(int flowId, string itemName)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                string sql = string.Format("select * from F_FLOW_CONFIG where flowid={0} and itemName='{1}'", flowId, itemName);

                return dbContext.ExecuteQuery<F_FLOW_CONFIG>(sql).ToList();
            }
        }

        public static bool Notify(int flowId, int flowNo, List<int> listWorkerId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                try
                {
                    foreach (int workerId in listWorkerId)
                    {
                        int count=dbContext.F_INST_NOTICE.Count(t => t.FlowID == flowId
                            && t.FlowNo == flowNo && t.WorkerID == workerId);

                        if (count == 0)
                        {
                            F_INST_NOTICE notice = new F_INST_NOTICE();

                            notice.FlowID = flowId;
                            notice.FlowNo = flowNo;
                            notice.WorkerID = workerId;


                            dbContext.F_INST_NOTICE.InsertOnSubmit(notice);
                        }
                    }

                    dbContext.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool InsertFlowConfig(string value)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                try
                {
                    F_FLOW_CONFIG cot = new F_FLOW_CONFIG();
                    cot.ItemValue = value;
                    cot.FlowID = 99;
                    cot.ItemName = "Project";

                    dbContext.F_FLOW_CONFIG.InsertOnSubmit(cot);

                    dbContext.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static object LoadAllFlowByPage(int page, int rows, string order, string sort, int? catalogID) 
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from f in dbContext.F_FLOW
                           join c in dbContext.F_CATALOG on f.CatalogID equals c.ID
                           select new
                           {
                               f.ID,
                               f.Name,
                               CatalogID = f.CatalogID,
                               CatalogName = c.Name,
                               f.CreateDate,
                               f.ModifyDate,
                               f.Description,
                               f.Url,
                               IsInner = f.IsInner == "Y"?"是":"否",
                               f.LayoutType,
                           };
                if (catalogID != null)
                {
                    list = list.Where(o => o.CatalogID == catalogID);
                }

                long total = list.LongCount();
                list = list.OrderBy(p => p.ID);
                list = list.Skip((page - 1) * rows).Take(rows);

                var list2 = list.ToList().Select(o => new
                {
                    ID = o.ID,
                    Name = o.Name,
                    CatalogID = o.CatalogID,
                    CatalogName = o.CatalogName,
                    CreateDate = o.CreateDate.ToString("yyyy-MM-dd"),
                    ModifyDate = o.ModifyDate.ToString("yyyy-MM-dd"),
                    Description = o.Description,
                    Url = o.Url,
                    IsInner = o.IsInner,
                    LayoutType = o.LayoutType,
                });
                var result = new { total = total, rows = list2.ToList() };

                return result;
            }

        }

        public static List<F_CATALOG> GetCatalog()
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_CATALOG.ToList();
            }
        }

        public static bool Delete(IList<int> idList)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                foreach (int g in idList)
                {
                    var model = dbContext.F_FLOW.Single(s => s.ID == g);
                    dbContext.F_FLOW.DeleteOnSubmit(model);
                }
                dbContext.SubmitChanges();
                return true;
            }
        }

        public static object LoadAllCatalogByPage(int page, int rows, string order, string sort) 
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = (from f in dbContext.F_CATALOG select f);
                long total = list.LongCount();

                list = list.OrderBy(s => s.ID);
                list = list.Skip((page - 1) * rows).Take(rows);

                var result = new { total = total, rows = list.ToList() };

                return result;
            }

        }
        public static bool SaveCatalog(F_CATALOG entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                if (entity.ID == 0)
                {
                    var list = from a in dbContext.F_CATALOG select a.ID;

                    long total = list.LongCount();

                    if (total == 0)
                    {
                        entity.ID = 1;
                    }
                    else
                    {
                        entity.ID = dbContext.F_CATALOG.Max(a => a.ID) + 1;
                    }

                    dbContext.F_CATALOG.InsertOnSubmit(entity);
                }
                else
                {
                    var model = dbContext.F_CATALOG.FirstOrDefault(t => t.ID == entity.ID);

                    model.Name = entity.Name;
                }
                dbContext.SubmitChanges();
                return true;
            }
        }
        public static bool DeleteCatalog(int id) 
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var model = dbContext.F_CATALOG.Single(s => s.ID == id);
                dbContext.F_CATALOG.DeleteOnSubmit(model);
                dbContext.SubmitChanges();
                return true;
            }
        }
    }
}

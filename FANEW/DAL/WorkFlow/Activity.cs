using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Anchor.FA.Model;

namespace Anchor.FA.DAL.WorkFlow
{
    public class Activity
    {
        public static List<F_ACTIVITY> GetListByFlowId(int flowId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_ACTIVITY.Where(t => t.FlowID == flowId).ToList();
            }
        }

        public static F_ACTIVITY Get(int id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_ACTIVITY.SingleOrDefault(t => t.ID == id);
            }
        }

        public static decimal Insert(F_ACTIVITY entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from a in dbContext.F_INST_FLOW select a.ID;

                long total = list.LongCount();

                if (total == 0)
                {
                    entity.ID = 1;
                }
                else
                {
                    entity.ID = dbContext.F_ACTIVITY.Max(a => a.ID) + 1;
                }

                dbContext.F_ACTIVITY.InsertOnSubmit(entity);

                dbContext.SubmitChanges();

                return entity.ID;
            }
        }

        public static bool Save(F_ACTIVITY entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var model = dbContext.F_ACTIVITY.FirstOrDefault(t => t.ID == entity.ID);

                model.AutoByPass = entity.AutoByPass;
                model.CompleteType = entity.CompleteType;
                model.Description = entity.Description;
                model.FlowID = entity.FlowID;
                model.JoinType = entity.JoinType;
                model.Name = entity.Name;
                model.SplitType = entity.SplitType;
                model.Type = entity.Type;

                dbContext.SubmitChanges();
            }

            return true;
        }

        public static bool Delete(decimal id)
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


    }
}

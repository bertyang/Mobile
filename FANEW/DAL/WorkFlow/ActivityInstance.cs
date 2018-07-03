using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Anchor.FA.Model;

namespace Anchor.FA.DAL.WorkFlow
{
    public class ActivityInstance
    {
        public static F_INST_ACTIVITY Get(int id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_INST_ACTIVITY.SingleOrDefault(t => t.ID == id);
            }
        }

        public static List<F_INST_ACTIVITY> GetList(int flowInstId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_INST_ACTIVITY.Where(t => t.FlowInstID == flowInstId).ToList();
            }
        }

        public static void Insert(F_INST_ACTIVITY entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                //var list = from a in dbContext.F_INST_ACTIVITY select a.ID;

                //long total = list.LongCount();

                //if (total == 0)
                //{
                //    entity.ID = 1;
                //}
                //else
                //{
                //    entity.ID = dbContext.F_INST_ACTIVITY.Max(a => a.ID) + 1;
                //}


                dbContext.F_INST_ACTIVITY.InsertOnSubmit(entity);

                dbContext.SubmitChanges();

                //return entity.ID;
            }
        }

        public static bool Save(F_INST_ACTIVITY entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var model = dbContext.F_INST_ACTIVITY.FirstOrDefault(t => t.ID == entity.ID);

                model.FlowInstID = entity.FlowInstID;
                model.ActivityID = entity.ActivityID;
                model.BeginDate = entity.BeginDate;
                model.EndDate = entity.EndDate;
                model.State = entity.State;

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

        public static List<F_ACTIVITY> GetPrivousOneReturnActivitys(int flowInstId, int currentActivityId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return (from a in dbContext.F_ACTIVITY
                        join b in dbContext.F_INST_ACTIVITY on a.ID equals b.ActivityID
                        join c in dbContext.F_TRANSITION on b.ActivityID equals c.StartActivtyID
                        where b.FlowInstID == flowInstId
                                && b.State == "C"
                                && a.Type != "start"
                                && c.EndActivityID == currentActivityId 
                        select a).Distinct().ToList();
            }
        }

        public static List<F_ACTIVITY> GetPrivousAllReturnActivitys(int flowInstId, int currentActivityId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return (from a in dbContext.F_ACTIVITY
                        join b in dbContext.F_INST_ACTIVITY on a.ID equals b.ActivityID
                        where b.FlowInstID == flowInstId
                                && b.State == "C"
                                && a.Type != "start"
                                && a.ID != currentActivityId

                        select a).Distinct().ToList();
            }
        }

        public static List<F_ACTIVITY> GetCustomReturnActivitys(int flowInstId, int currentActivityId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return (from a in dbContext.F_RETURN_CONFIG
                        join b in dbContext.F_INST_ACTIVITY on a.ToActivityID equals b.ActivityID
                        join c in dbContext.F_ACTIVITY on a.ToActivityID equals c.ID
                        where a.FromActivityID == currentActivityId
                                && b.FlowInstID == flowInstId
                                && b.State == "C"
                                && c.Type != "start"
                        select c).Distinct().ToList();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Anchor.FA.Model;

namespace Anchor.FA.DAL.WorkFlow
{
    public class FlowInstance
    {
        public static F_INST_FLOW Get(decimal id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_INST_FLOW.SingleOrDefault(t => t.ID == id);
            }
        }

        public static F_INST_FLOW Get(int flowId, int flowNo)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_INST_FLOW.SingleOrDefault(t => t.FlowID == flowId && t.FlowNo == flowNo);
            }
        }

        public static void Insert(F_INST_FLOW entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                //dbContext.Database.Connection.Open();

                //var list = from a in dbContext.F_INST_FLOW select a.ID;

                //long total = list.LongCount();

                //if (total == 0)
                //{
                //    entity.ID = 1;
                //}
                //else
                //{
                //    entity.ID = dbContext.F_INST_FLOW.Max(a => a.ID) + 1;
                //    entity.FlowNo = dbContext.F_INST_FLOW.Where(t => t.FlowID == entity.FlowID).Max(a => a.FlowNo) + 1;
                //}


                dbContext.F_INST_FLOW.InsertOnSubmit(entity);

                dbContext.SubmitChanges();


            }
        }


        public static int GetFlowNo(int flowId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from a in dbContext.F_INST_FLOW
                           where a.FlowID == flowId
                           select a.ID;


                long total = list.LongCount();

                if (total == 0)
                {
                    return 1;
                }
                else
                {
                    return dbContext.F_INST_FLOW.Where(t => t.FlowID == flowId).Max(a => a.FlowNo) + 1;
                }

            }
        }

        public static bool Save(F_INST_FLOW entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var model = dbContext.F_INST_FLOW.FirstOrDefault(t => t.ID == entity.ID);

                model.ApplyerID = entity.ApplyerID;
                model.BeginDate = entity.BeginDate;
                model.EndDate = entity.EndDate;
                model.FillerID = entity.FillerID;
                model.FlowID = entity.FlowID;
                model.FlowNo = entity.FlowNo;
                model.State = entity.State;
                model.Digest = entity.Digest;

                dbContext.SubmitChanges();
            }

            return true;
        }

        public static bool Delete(int id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var model = dbContext.F_INST_FLOW.SingleOrDefault(t => t.ID == id);

                if (model != null)
                {
                    //dbContext.F_INST_FLOW.Load();
                    dbContext.F_INST_FLOW.DeleteOnSubmit(model);

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

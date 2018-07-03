using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Anchor.FA.Model;

namespace Anchor.FA.DAL.WorkFlow
{
    public class TransationInstance
    {
        public static F_INST_TRANSATION Get(int id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_INST_TRANSATION.SingleOrDefault(t => t.ID == id);
            }
        }

        public static List<F_INST_TRANSATION> GetList(int activityInstID)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_INST_TRANSATION.Where(t => t.ActivityInstID == activityInstID).ToList();
            }
        }

        public static void Insert(F_INST_TRANSATION entity)
        {

            using (MainDataContext dbContext = new MainDataContext())
            {
                //var list = from a in dbContext.F_INST_TRANSATION select a.ID;

                //long total = list.LongCount();

                //if (total == 0)
                //{
                //    entity.ID = 1;
                //}
                //else
                //{
                //    entity.ID = dbContext.F_INST_TRANSATION.Max(a => a.ID) + 1;
                //}

                dbContext.F_INST_TRANSATION.InsertOnSubmit(entity);

                dbContext.SubmitChanges();

                //return entity.ID;
            }
        }

        public static bool Save(F_INST_TRANSATION entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var model = dbContext.F_INST_TRANSATION.FirstOrDefault(t => t.ID == entity.ID);

                model.ActivityInstID = entity.ActivityInstID;
                model.ID = entity.ID;
                model.PassTime = entity.PassTime;
                model.TransationID = entity.TransationID;
                model.TransationValue = entity.TransationValue;

                dbContext.SubmitChanges();
            }

            return true;
        }

        public static bool Delete(decimal id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var model = dbContext.F_INST_TRANSATION.SingleOrDefault(t => t.ID == id);

                if (model != null)
                {
                    //dbContext.F_INST_TRANSATION.Load();
                    dbContext.F_INST_TRANSATION.DeleteOnSubmit(model);

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

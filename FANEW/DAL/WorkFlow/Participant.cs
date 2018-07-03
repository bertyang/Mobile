using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;

namespace Anchor.FA.DAL.WorkFlow
{
    public class Participant
    {
        public static List<F_PARTICIPANT> GetListParticipantByActivityId(int activityId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_PARTICIPANT.Where(t => t.ActivityID == activityId).ToList();
            }
        }

        public static F_PARTICIPANT Get(int id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_PARTICIPANT.SingleOrDefault(t => t.ID == id);
            }
        }

        public static F_PARTICIPANT_ORG GetMDOrg(int modelId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_PARTICIPANT_ORG.SingleOrDefault(t => t.ID == modelId);
            }
        }

        public static F_PARTICIPANT_RELATION GetMDRelation(int modelId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_PARTICIPANT_RELATION.SingleOrDefault(t => t.ID == modelId);
            }
        }

        public static List<int> GetMDPost(int modelId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                IQueryable<int> list = from a in dbContext.F_PARTICIPANT_POST
                                       join b in dbContext.B_WORKER_ORGANIZATION on new { a.PostID, a.OrgID } equals new { b.PostID, b.OrgID } 
                                       where a.ID == modelId
                                       select b.WorkerID; 

                return list.ToList();


            }
        }

        public static F_PARTICIPANT_LEVEL GetMDLevel(int modelId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_PARTICIPANT_LEVEL.SingleOrDefault(t => t.ID == modelId);
            }
        }

        public static F_PARTICIPANT_FIELD GetMDField(int modelId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_PARTICIPANT_FIELD.SingleOrDefault(t => t.ID == modelId);
            }            
        }


        public static List<int> GetModelIds(int participantId,string type)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                string sql = string.Format("select id from F_PARTICIPANT_{0} where ParticipantID={1}", type, participantId);

                return dbContext.ExecuteQuery<int>(sql).ToList();
            }

        }
 
    }
}

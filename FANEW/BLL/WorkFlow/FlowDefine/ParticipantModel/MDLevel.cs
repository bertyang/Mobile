using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context;
using Spring.Context.Support;

using Anchor.FA.BLL.IBLL;
using Anchor.FA.Model;

namespace Anchor.FA.BLL.WorkFlow.ParticipantModel
{
    internal class MDLevel : FMModel
    {
        private IWorker iworker;
        private IOrganization iorg;

        public MDLevel() : base()
        {
            IApplicationContext ctx = ContextRegistry.GetContext();

            iworker = ctx["Worker"] as IWorker;
            iorg = ctx["Organize"] as IOrganization;
        }

        public override IList<int> GetApprover(int flowId, int flowNo)
        {
           F_PARTICIPANT_LEVEL model = DAL.WorkFlow.Participant.GetMDLevel(this.ModelID);

           F_INST_FLOW flowInst = DAL.WorkFlow.FlowInstance.Get(flowId, flowNo);

           //得到申请人默认职位层级
           C_Worker_Level applyer = iworker.GetDefaultLevel(flowInst.ApplyerID);

           //计算目标层级
           int level = 0;

           if (model.IsAbsolute == "N")
           {
               level = model.Level + applyer.Level;
           }
           else
           {
               level = model.Level;
           }

           //得到对应层级签核人
           List<int> listWorkerId = new List<int>();

           if (applyer.Level >= level) 
           {
               listWorkerId.Add(flowInst.ApplyerID);
           }
           else if (applyer.ID != applyer.ParentID)
           {
               List<int> listDepart = new List<int>();

               iorg.GetUnitList(listDepart, applyer.DepartID);

               int approver = GetAbsoluteLevelParentId(applyer.ParentID, listDepart, level);

               if (approver != -1)
               {
                   listWorkerId.Add(approver);
               }
           }

           return listWorkerId;
        }

        private int GetAbsoluteLevelParentId(int workerId,List<int> listDepartId, int level)
        {
            foreach(int departId in listDepartId)
            {
                C_Worker_Level worker = iworker.GetLevelByOrg(workerId, departId);

                if (worker != null)
                {
                    if (worker.Level < level)
                    {
                        if (worker.ID != worker.ParentID)
                        {
                            return GetAbsoluteLevelParentId(worker.ParentID, listDepartId, level);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        return workerId;
                    }
                }
            }

            return -1;
                
        }


    }
}

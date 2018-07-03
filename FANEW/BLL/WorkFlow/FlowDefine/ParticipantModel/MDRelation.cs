using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;

namespace Anchor.FA.BLL.WorkFlow.ParticipantModel
{
    internal class MDRelation : FMModel
    {
        public MDRelation()
            : base()
        {
        }

        public override IList<int> GetApprover(int flowId, int flowNo)
        {
            List<int> listWorkerId = new List<int>();

            F_PARTICIPANT_RELATION relation = DAL.WorkFlow.Participant.GetMDRelation(this.ModelID);

            if (relation.Relation.ToUpper() == "APPLYER")
            {
                F_INST_FLOW flowInst = DAL.WorkFlow.FlowInstance.Get(flowId, flowNo);

                listWorkerId.Add(flowInst.ApplyerID);
            }

            return listWorkerId;
        }
    }
}

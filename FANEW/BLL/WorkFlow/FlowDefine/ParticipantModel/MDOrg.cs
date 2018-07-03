using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;

namespace Anchor.FA.BLL.WorkFlow.ParticipantModel
{
    internal class MDOrg : FMModel
    {
        public MDOrg()
            : base()
        {
        }

        public override IList<int> GetApprover(int flowId, int flowNo)
        {
           F_PARTICIPANT_ORG org = DAL.WorkFlow.Participant.GetMDOrg(this.ModelID);

           return new List<int> { org.WorkerID };
        }
    }
}

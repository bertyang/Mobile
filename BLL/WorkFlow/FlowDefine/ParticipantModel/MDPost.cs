using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;

namespace Anchor.FA.BLL.WorkFlow.ParticipantModel
{
    internal class MDPost : FMModel
    {
        public MDPost()
            : base()
        {
        }

        public override IList<int> GetApprover(int flowId, int flowNo)
        {
           return DAL.WorkFlow.Participant.GetMDPost(this.ModelID);
        }
    }
}

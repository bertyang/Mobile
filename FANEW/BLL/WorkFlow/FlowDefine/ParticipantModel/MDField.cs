using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;

namespace Anchor.FA.BLL.WorkFlow.ParticipantModel
{
    internal class MDField : FMModel
    {
        public MDField()
            : base()
        {
        }

        public override IList<int> GetApprover(int flowId, int flowNo)
        {
           F_PARTICIPANT_FIELD field = DAL.WorkFlow.Participant.GetMDField(this.ModelID);

           object colomnValue = DAL.WorkFlow.Column.GetColomnValue(field.Sql,flowNo, "int");

           List<int> listWorkerId = new List<int>();

           listWorkerId.Add(Convert.ToInt32(colomnValue));

           return listWorkerId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;

namespace Anchor.FA.BLL.WorkFlow.ParticipantModel
{
    internal abstract class FMModel
    {
        protected int m_ModelId;
        protected int m_ParticipantId;

        public FMModel()
        {

        }

        public int ModelID
        {
            get { return this.m_ModelId; }
            set { this.m_ModelId = value; }
        }

        public int ParticipantID
        {
            get { return this.m_ParticipantId; }
            set { this.m_ParticipantId = value; }
        }


        public abstract IList<int> GetApprover(int flowId, int flowNo);

    }
}

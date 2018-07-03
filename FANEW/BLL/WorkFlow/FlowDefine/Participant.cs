using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Anchor.FA.Model;
using Anchor.FA.DAL.WorkFlow;
using Anchor.FA.BLL.WorkFlow.ParticipantModel;

namespace Anchor.FA.BLL.WorkFlow
{
    internal class Participant
    {
        #region 属性
        protected int m_ParticipantId;
        protected string m_Type;
        protected List<FMModel> m_ListFMModel=new List<FMModel>();

        public int ID
        {
            set
            {
                m_ParticipantId = value;
            }
            get
            {
                return m_ParticipantId;
            }
        }

        public string Type
        {
            set
            {
                m_Type = value;
            }
            get
            {
                return m_Type;
            }
        }

        public List<FMModel> FMModels
        {
            set
            {
                m_ListFMModel = value;
            }
            get
            {
                return m_ListFMModel;
            }
        }
        #endregion 

        public Participant(int id,string type)
        {
            this.ID = id;
            this.Type = type;

            List<int> listModelId = DAL.WorkFlow.Participant.GetModelIds(id, type);

            foreach (int modelId in listModelId)
            {
                FMModel model = (FMModel)Assembly.Load("Anchor.FA.BLL.WorkFlow").
                                CreateInstance("Anchor.FA.BLL.WorkFlow.ParticipantModel.MD" + this.Type);

                model.ModelID = modelId;

                FMModels.Add(model);
            }
        }
        
        public IList<int> Parse(int flowId,int formNo)
        {   
           List<int> list=new List<int>();

           foreach (FMModel model in FMModels)
           {
               list.AddRange(model.GetApprover(flowId, formNo));
           }

           return list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Anchor.FA.BLL.IBLL;
using Anchor.FA.Model;
using Anchor.FA.DAL.WorkFlow;
using Anchor.FA.Utility;

namespace Anchor.FA.BLL.WorkFlow
{
    internal class TransationInstance : Instance 
    {
        #region 属性
        private Transation m_Transation;
        private ActivityInstance m_ActivityInstance;
        private int m_TransationInstanceId;
        private bool m_Value;

        public int ID
        {
            get { return m_TransationInstanceId; }
            set { m_TransationInstanceId = value; }
        }

        public bool Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        public Transation Transation
        {
            get { return m_Transation; }
            set { m_Transation = value; }
        }

        public ActivityInstance ActivityInstance
        {
            get { return m_ActivityInstance; }
            set { m_ActivityInstance = value; }
        }
        #endregion

        public TransationInstance()
        {

        }

        public TransationInstance(int id)
        {
            m_TransationInstanceId = id;

            F_INST_TRANSATION transation = DAL.WorkFlow.TransationInstance.Get(m_TransationInstanceId);

            m_Value = transation.TransationValue.ToUpper() == "TRUE" ? true : false;

            m_Transation = new Transation(transation.TransationID);

            m_ActivityInstance = new ActivityInstance(transation.ActivityInstID);
        }


        public void Create(int activityInstId,int transId)
        {
            m_Transation = new Transation(transId);
            m_ActivityInstance = new ActivityInstance(activityInstId);

            //IPrimaryKeyCreater prikey = ctx["PrimaryKeyCreater"] as IPrimaryKeyCreater;

            m_TransationInstanceId = PrimaryKeyCreater.getIntPrimaryKey("F_INST_TRANSATION");

            F_INST_TRANSATION instTrans = new F_INST_TRANSATION();

            instTrans.ID = m_TransationInstanceId;
            instTrans.ActivityInstID = activityInstId;
            instTrans.TransationID = transId;
            instTrans.PassTime = DateTime.Now;
            instTrans.TransationValue = "";

            DAL.WorkFlow.TransationInstance.Insert(instTrans);            
        }

        public void Parse()
        {
            int flowId = this.ActivityInstance.FlowInstance.FlowDefine.ID;
            int flowNo=this.ActivityInstance.FlowInstance.FlowNo;

            F_INST_TRANSATION instTrans = DAL.WorkFlow.TransationInstance.Get(this.ID);
            
            instTrans.PassTime=DateTime.Now;
            instTrans.TransationValue = this.Transation.Parse(flowId,flowNo).ToString().ToUpper();

            DAL.WorkFlow.TransationInstance.Save(instTrans);

            this.Value = instTrans.TransationValue.ToUpper() == "TRUE" ? true : false;

        }

    }
}

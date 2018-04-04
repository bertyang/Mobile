using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.BLL.IBLL;
using Anchor.FA.Model;
using Anchor.FA.DAL;
using Anchor.FA.BLL.WorkFlow.Common;
using Anchor.FA.Utility;

namespace Anchor.FA.BLL.WorkFlow
{

    internal class WorkItemInstance : Instance
    {
        #region 属性
        private ActivityInstance m_ActivityInstance;
        private int m_WorkItemId;
        private string m_State;
        private string m_AppValue;
        private string m_AppRemark;
        private int m_Actorid;

        public int ID
        {
            get { return m_WorkItemId; }
            set { m_WorkItemId = value; }
        }

        public ActivityInstance ActivityInstance
        {
            get { return m_ActivityInstance; }
            set { m_ActivityInstance = value; }
        }

        public string State
        {
            get { return m_State; }
            set { m_State = value; }
        }

        public string AppValue
        {
            get { return m_AppValue; }
            set { m_AppValue = value; }
        }

        public string AppRemark
        {
            get { return m_AppRemark; }
            set { m_AppRemark = value; }
        }

        public int Actorid
        {
            get { return m_Actorid; }
            set { m_Actorid = value; }
        }
        #endregion

        public WorkItemInstance()
        {

        }

        public WorkItemInstance(int id)
        {
            m_WorkItemId = id;

            F_INST_WORKITEM item = DAL.WorkFlow.WorkItemInstance.Get(m_WorkItemId);

            m_ActivityInstance = new ActivityInstance(item.ActivityInstID);

            m_State = item.State;
        }

        public void Create(int activityInstId, int workerId)
        {
            m_ActivityInstance = new ActivityInstance(activityInstId);
            m_State = WorkItemState.Waiting;

            //IPrimaryKeyCreater prikey = ctx["PrimaryKeyCreater"] as IPrimaryKeyCreater;
            m_WorkItemId = PrimaryKeyCreater.getIntPrimaryKey("F_INST_WORKITEM");

            F_INST_WORKITEM instWorkItem = new F_INST_WORKITEM();

            instWorkItem.ID = m_WorkItemId;
            instWorkItem.ActivityInstID = activityInstId;
            instWorkItem.Actorid = null;
            instWorkItem.Role = m_ActivityInstance.Activity.Name;
            instWorkItem.Assigneeid = workerId;
            instWorkItem.Assignerid=0;
            instWorkItem.BeginDate=DateTime.Now;
            instWorkItem.EndDate=null;
            instWorkItem.State = WorkItemState.Waiting;
            instWorkItem.AppType = WorkItemApproveType.Normal;
            instWorkItem.AssignType = WorkItemAssignType.Normal;

            DAL.WorkFlow.WorkItemInstance.Insert(instWorkItem);

        }

        public void CreateRecall(int activityInstId, int workerId,string remark)
        {
            m_ActivityInstance = new ActivityInstance(activityInstId);
            m_State = WorkItemState.Waiting;


            m_WorkItemId = PrimaryKeyCreater.getIntPrimaryKey("F_INST_WORKITEM");

            F_INST_WORKITEM instWorkItem = new F_INST_WORKITEM();

            instWorkItem.ID = m_WorkItemId;
            instWorkItem.ActivityInstID = activityInstId;
            instWorkItem.Actorid = workerId;
            instWorkItem.Role = "申请人撤回";
            instWorkItem.Assigneeid = workerId;
            instWorkItem.Assignerid = 0;
            instWorkItem.BeginDate = DateTime.Now;
            instWorkItem.EndDate = DateTime.Now;
            instWorkItem.State = WorkItemState.Completed;
            instWorkItem.AppType = WorkItemApproveType.Blank;
            instWorkItem.AssignType = WorkItemAssignType.Normal;
            instWorkItem.AppValue = string.Empty;
            instWorkItem.AppRemark = remark;

            DAL.WorkFlow.WorkItemInstance.Insert(instWorkItem);
        }

        public void CreateResend(int activityInstId, int workerId, string remark)
        {
            m_ActivityInstance = new ActivityInstance(activityInstId);
            m_State = WorkItemState.Waiting;


            m_WorkItemId = PrimaryKeyCreater.getIntPrimaryKey("F_INST_WORKITEM");

            F_INST_WORKITEM instWorkItem = new F_INST_WORKITEM();

            instWorkItem.ID = m_WorkItemId;
            instWorkItem.ActivityInstID = activityInstId;
            instWorkItem.Actorid = workerId;
            instWorkItem.Role = "申请人重新送出";
            instWorkItem.Assigneeid = workerId;
            instWorkItem.Assignerid = 0;
            instWorkItem.BeginDate = DateTime.Now;
            instWorkItem.EndDate = DateTime.Now;
            instWorkItem.State = WorkItemState.Completed;
            instWorkItem.AppType = WorkItemApproveType.Blank;
            instWorkItem.AssignType = WorkItemAssignType.Normal;
            instWorkItem.AppValue = string.Empty;
            instWorkItem.AppRemark = remark;

            DAL.WorkFlow.WorkItemInstance.Insert(instWorkItem);
        }

        public void Approve()
        {
            //更新状态
            F_INST_WORKITEM inst = DAL.WorkFlow.WorkItemInstance.Get(this.ID);

            inst.AppValue=this.AppValue;
            inst.AppRemark = this.AppRemark;
            inst.Actorid = this.Actorid;
            inst.EndDate = DateTime.Now;
            inst.State = WorkItemState.Completed;

            DAL.WorkFlow.WorkItemInstance.Save(inst);


            //调用表单Action
            bool isInner = this.ActivityInstance.FlowInstance.FlowDefine.IsInner;
            int flowId = this.ActivityInstance.FlowInstance.FlowDefine.ID;
            int flowNo = this.ActivityInstance.FlowInstance.FlowNo;
            int activityId=this.ActivityInstance.Activity.ID;

            FlowAction<IApprove> flowAction = new FlowAction<IApprove>(isInner, flowId);

            if (flowAction.action != null)
            {
                if (!flowAction.action.ApproveAction(flowId, flowNo, activityId, this.Actorid, this.AppValue, this.AppRemark))
                {
                    throw new Exception(string.Format("ApproveAction失败,flowId{0},flowNo{1},activityId{2},this.Actorid{3},AppValue{4}", flowId, flowNo, activityId, Actorid, AppValue));
                }
            }
        }

        public void Pass()
        {
            //更新状态
            F_INST_WORKITEM inst = DAL.WorkFlow.WorkItemInstance.Get(this.ID);

            inst.EndDate = DateTime.Now;
            inst.State = WorkItemState.Pass;

            DAL.WorkFlow.WorkItemInstance.Save(inst);
        }

        public void Sign()
        {
            IESignature sign = ctx["ESignature"] as IESignature;

            F_INST_WORKITEM inst = DAL.WorkFlow.WorkItemInstance.Get(this.ID);

            inst.Cert = sign.GetStrSignCert();
            inst.Esignature = sign.GetStrSignData(this.ActivityInstance.FlowInstance.FormDigest); ;

            DAL.WorkFlow.WorkItemInstance.Save(inst);
        }

    }
}

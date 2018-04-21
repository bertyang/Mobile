using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Anchor.FA.Utility;
using Anchor.FA.BLL.IBLL;
using Anchor.FA.Model;
using Anchor.FA.DAL.WorkFlow;
using Anchor.FA.BLL.WorkFlow.Common;

namespace Anchor.FA.BLL.WorkFlow
{

    /// <summary>
    /// 流程实例类
    /// </summary>
    internal class FlowInstance : Instance 
    {
        #region 属性

        public delegate void ApproveEventHandler(object sender, ApproveEventArgs e);

        public event ApproveEventHandler ApprovePass;
        public event ApproveEventHandler ApproveReject;
        /// <summary>
        /// 流程定义
        /// </summary>
        private FlowDefine m_FlowDefine = null;

        /// <summary>
        /// 关卡实例
        /// </summary>
        private List<ActivityInstance> m_ActivityInstance = new List<ActivityInstance>();

        /// <summary>
        /// 流程实例id
        /// </summary>
        private int m_FlowInstanceId = 0;

        /// <summary>
        /// 流程号
        /// </summary>
        private int m_FlowNo = 0;

        /// <summary>
        /// 申请人Id
        /// </summary>
        private int m_ApplerId = 0;

        /// <summary>
        /// 流程状态
        /// </summary>
        private string m_Status;


        private DateTime m_BeginDate;

        /// <summary>
        /// 表单数字摘要
        /// </summary>
        private string m_FormDigest;

        /// <summary>
        /// 流程实例ID
        /// </summary>
        public int ID
        {
            get { return m_FlowInstanceId; }
        }

        /// <summary>
        /// 流程实例状态
        /// </summary>
        public string Status
        {
            set { m_Status = value; }
            get { return m_Status; }
        }

        /// <summary>
        /// 签核值
        /// </summary>
        //public string AppValue
        //{
        //    set { m_AppValue = value; }
        //    get { return m_AppValue; }
        //}

        /// <summary>
        /// 表单数字摘要
        /// </summary>
        public string FormDigest
        {
            set { m_FormDigest = value; }
            get { return m_FormDigest; }
        }

        /// <summary>
        /// 流程定义
        /// </summary>
        public FlowDefine FlowDefine
        {
            get { return m_FlowDefine; }
        }

        public int ApplerID
        {
            get { return m_ApplerId; }
        }

        /// <summary>
        /// 关卡实例集合
        /// </summary>
        public List<ActivityInstance> ActivityInstances
        {
            get
            {
                if (m_ActivityInstance.Count == 0)
                {
                    List<F_INST_ACTIVITY> listActivityInst = DAL.WorkFlow.ActivityInstance.GetList(m_FlowInstanceId);

                    foreach (F_INST_ACTIVITY entity in listActivityInst)
                    {
                        ActivityInstance activityInst = new ActivityInstance(entity.ID);

                        m_ActivityInstance.Add(activityInst);
                    }
                }

                return m_ActivityInstance;
            }
        }

        /// <summary>
        /// 流程号
        /// </summary>
        public int FlowNo
        {
            get { return m_FlowNo; }
            set { m_FlowNo = value; }
        }

        #endregion

        #region 构造函数
        public FlowInstance()
        {

        }

        public FlowInstance(int id)
        {
            m_FlowInstanceId = id;

            F_INST_FLOW instFlow = DAL.WorkFlow.FlowInstance.Get(m_FlowInstanceId);

            m_FlowDefine = new FlowDefine(instFlow.FlowID);

            m_FlowNo = instFlow.FlowNo;

            m_Status = instFlow.State;

            m_FormDigest = instFlow.Digest;

            m_ApplerId = instFlow.ApplyerID;

            m_BeginDate = instFlow.BeginDate;

        }

        public FlowInstance(int flowId, int flowNo)
        {

            F_INST_FLOW instFlow = DAL.WorkFlow.FlowInstance.Get(flowId, flowNo);

            if (instFlow != null)
            {
                m_FlowInstanceId = instFlow.ID;

                m_FlowDefine = new FlowDefine(instFlow.FlowID);

                m_FlowNo = instFlow.FlowNo;

                m_Status = instFlow.State;

                m_FormDigest = instFlow.Digest;

                m_ApplerId = instFlow.ApplyerID;
            }

        }
        #endregion

        public static bool Exists(int flowId, int flowNo)
        {
            F_INST_FLOW instFlow = DAL.WorkFlow.FlowInstance.Get(flowId, flowNo);

            return instFlow == null ? false : true;
        }
        /// <summary>
        /// 创建流程实例
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        public void Create(int flowId,int flowNo,int applyerId, string digest)
        {
            m_FlowDefine = new FlowDefine(flowId);

            F_INST_FLOW inst = new F_INST_FLOW();

            //IPrimaryKeyCreater prikey = ctx["PrimaryKeyCreater"] as IPrimaryKeyCreater;

            m_FlowInstanceId = PrimaryKeyCreater.getIntPrimaryKey("F_INST_FLOW");

            inst.ID = m_FlowInstanceId;
            inst.ApplyerID = applyerId;
            inst.BeginDate = DateTime.Now;
            inst.EndDate = null;
            inst.FlowID = flowId;
            inst.State = FlowInstanceState.InActive;
            inst.FlowNo = flowNo;
            inst.Digest = digest;

            DAL.WorkFlow.FlowInstance.Insert(inst);

        }

        /// <summary>
        /// 激活
        /// </summary>
        /// <returns></returns>
        public void Active()
        {
            //更新状态
            F_INST_FLOW inst = DAL.WorkFlow.FlowInstance.Get(m_FlowInstanceId);

            //inst.BeginDate = DateTime.Now;
            inst.State = FlowInstanceState.Active;

            DAL.WorkFlow.FlowInstance.Save(inst);

        }

        /// <summary>
        /// 撤回
        /// </summary>
        /// <param name="flowInstId"></param>
        public void Recall(string remark)
        {
            //删除未完成的工作
            DAL.WorkFlow.WorkItemInstance.DeleteNoCompleteTask(this.ID);

            //更新流程实例状态
            UpdateState(FlowInstanceState.Recall);

            //创建撤回任务
            WorkItemInstance item = new WorkItemInstance();

            int activityInstId = this.ActivityInstances.Single(t => t.Activity.Type == ActivityType.START).ID;

            item.CreateRecall(activityInstId, m_ApplerId, remark);

        }

        /// <summary>
        /// 通过
        /// </summary>
        /// <returns></returns>
        public void Pass()
        {
            F_INST_FLOW inst = DAL.WorkFlow.FlowInstance.Get(m_FlowInstanceId);

            inst.EndDate = DateTime.Now;

            inst.State = FlowInstanceState.Pass;

            DAL.WorkFlow.FlowInstance.Save(inst);

            //调用相关系统
            ApproveEventArgs e = new ApproveEventArgs();

            e.FlowId = this.FlowDefine.ID;
            e.FlowName = this.FlowDefine.Name;
            e.FlowInstId = this.ID;
            e.AppValue =  WorkItemAppValue.Agree;
            e.Url = this.FlowDefine.Url;
            e.BeginDate = this.m_BeginDate;
            e.IsInner = this.FlowDefine.IsInner;
            e.FlowNo = this.FlowNo;
            e.ApplyerId = this.m_ApplerId;                

            ApprovePass(this,e);//调用相关方法
        }

        /// <summary>
        /// 否决
        /// </summary>
        public void Reject()
        {
            F_INST_FLOW inst = DAL.WorkFlow.FlowInstance.Get(m_FlowInstanceId);

            inst.EndDate = DateTime.Now;

            inst.State = FlowInstanceState.Reject;

            DAL.WorkFlow.FlowInstance.Save(inst);
            
            //更新其他对象
            ApproveEventArgs e = new ApproveEventArgs();

            e.FlowId = this.FlowDefine.ID;
            e.FlowName = this.FlowDefine.Name;
            e.FlowInstId = this.ID;
            e.AppValue = WorkItemAppValue.Reject;
            e.Url = this.FlowDefine.Url;
            e.BeginDate = this.m_BeginDate;
            e.IsInner = this.FlowDefine.IsInner;
            e.FlowNo = this.FlowNo;
            e.ApplyerId = this.m_ApplerId;

            ApproveReject(this, e);//调用相关方法
        }


        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="state"></param>
        private void UpdateState(string state)
        {
            F_INST_FLOW inst = DAL.WorkFlow.FlowInstance.Get(this.m_FlowInstanceId);

            inst.State = state;

            DAL.WorkFlow.FlowInstance.Save(inst);
        }

        /// <summary>
        /// 查找关卡实例
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public ActivityInstance FindActivityInstance(int activityId)
        {
            return this.ActivityInstances.FirstOrDefault(t => t.Activity.ID == activityId);
        }

        public ActivityInstance FindActivityInstance(string activityType)
        {
            return this.ActivityInstances.FirstOrDefault(t => t.Activity.Type == activityType);
        }

        /// <summary>
        /// 验证摘要
        /// </summary>
        /// <returns></returns>
        public bool VerifyDigest()
        {
            FlowAction<IFormDigest> flowAction = new FlowAction<IFormDigest>(this.FlowDefine.IsInner, this.FlowDefine.ID);

            if (flowAction.action != null)
            {
                string formDigest = flowAction.action.GetFormDigest(this.FlowNo);

                if (formDigest == this.FormDigest)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// 更新摘要
        /// </summary>
        /// <param name="digest"></param>
        public void UpdateDigest(string digest)
        {
            //更新状态
            F_INST_FLOW inst = DAL.WorkFlow.FlowInstance.Get(m_FlowInstanceId);

            inst.Digest = digest;

            DAL.WorkFlow.FlowInstance.Save(inst);
        }
    }
}

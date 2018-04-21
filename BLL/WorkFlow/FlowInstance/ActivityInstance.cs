using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.BLL.IBLL;
using Anchor.FA.Model;
//using Anchor.FA.BLL.Organize;
using Anchor.FA.DAL.WorkFlow;
using Anchor.FA.BLL.WorkFlow.Common;
using Anchor.FA.Utility;

namespace Anchor.FA.BLL.WorkFlow
{
    internal class ActivityInstance : Instance 
    {
        #region 属性
        private Activity m_Activity ;
        private List<TransationInstance> m_ListTransationInstance = new List<TransationInstance>();
        private List<WorkItemInstance> m_ListWorkItemInstance = new List<WorkItemInstance>();
        private FlowInstance m_FlowInstance;
        private int m_ActivityInstanceId;
        private string m_State = string.Empty;

        public int ID
        {
            get { return m_ActivityInstanceId; }
            set { m_ActivityInstanceId = value; }
        }

        public string State
        {
            get { return m_State; }
            set { m_State = value; }
        }

        public Activity Activity
        {
            get { return m_Activity; }
            set { m_Activity = value; }
        }

        public FlowInstance FlowInstance
        {
            get { return m_FlowInstance; }
            set { m_FlowInstance = value; }
        }

        public List<TransationInstance> TransationInstances
        {
            get
            {
                this.TransationInstances.Clear();


                List<F_INST_TRANSATION> listTransation = DAL.WorkFlow.TransationInstance.GetList(m_ActivityInstanceId);

                foreach (F_INST_TRANSATION entity in listTransation)
                {
                    this.m_ListTransationInstance.Add(new TransationInstance(entity.ID));
                }

                return m_ListTransationInstance;
            }
            set { m_ListTransationInstance = value; }
        }

        public List<WorkItemInstance> WorkItemInstances
        {
            get
            {
                this.m_ListWorkItemInstance.Clear();

                List<F_INST_WORKITEM> listWorkItem = DAL.WorkFlow.WorkItemInstance.GetList(m_ActivityInstanceId);

                foreach (F_INST_WORKITEM entity in listWorkItem)
                {
                    this.m_ListWorkItemInstance.Add(new WorkItemInstance(entity.ID));
                }

                return m_ListWorkItemInstance;
            }

            set { m_ListWorkItemInstance = value; }
        }
        #endregion

        #region 构造函数
        public ActivityInstance()
        {
        }

        public ActivityInstance(int id)
        {
            m_ActivityInstanceId = id;

            F_INST_ACTIVITY activityInst = DAL.WorkFlow.ActivityInstance.Get(this.ID);

            m_FlowInstance = new FlowInstance(activityInst.FlowInstID);

            m_Activity = new Activity(activityInst.ActivityID);

            m_State = activityInst.State;
        }
        #endregion



        /// <summary>
        /// 创建关卡实例
        /// </summary>
        /// <param name="instFlow"></param>
        /// <param name="activity"></param>
        /// <returns></returns>
        public void Create(int flowInstId, int activityId)
        {
            this.FlowInstance = new FlowInstance(flowInstId);
            this.Activity = new Activity(activityId);
            this.State = ActivityInstanceStatus.InActive;

            //新增关卡实例
            //IPrimaryKeyCreater prikey = ctx["PrimaryKeyCreater"] as IPrimaryKeyCreater;
            m_ActivityInstanceId = PrimaryKeyCreater.getIntPrimaryKey("F_INST_ACTIVITY");

            F_INST_ACTIVITY instActivity = new F_INST_ACTIVITY();
            instActivity.ID = m_ActivityInstanceId;
            instActivity.ActivityID = activityId;
            instActivity.BeginDate = DateTime.Now;
            instActivity.EndDate = null;
            instActivity.FlowInstID = flowInstId;
            instActivity.State = ActivityInstanceStatus.InActive;

            DAL.WorkFlow.ActivityInstance.Insert(instActivity);
        }

        /// <summary>
        /// 启动关卡实例
        /// </summary>
        /// <returns></returns>
        public void Active()
        {
            //更新状态
            F_INST_ACTIVITY inst = DAL.WorkFlow.ActivityInstance.Get(m_ActivityInstanceId);

            inst.BeginDate = DateTime.Now;
            inst.State = ActivityInstanceStatus.Active;

            DAL.WorkFlow.ActivityInstance.Save(inst);         

        }

        /// <summary>
        /// 搁置关卡
        /// </summary>
        /// <returns></returns>
        public void Pending()
        {
            //更新状态
            F_INST_ACTIVITY activityInst = DAL.WorkFlow.ActivityInstance.Get(m_ActivityInstanceId);

            activityInst.BeginDate = DateTime.Now;
            activityInst.State = ActivityInstanceStatus.Pending;

            DAL.WorkFlow.ActivityInstance.Save(activityInst);

            this.State = ActivityInstanceStatus.Pending;
        }


        /// <summary>
        /// 分配工作(关卡参与人)
        /// </summary>
        /// <param name="listWorker"></param>
        /// <returns></returns>
        public void AssignWork()
        {
            //解析参与人
            IList<int> listWorkerId = this.Activity.ParseParticipant(this.FlowInstance.FlowDefine.ID,
                this.FlowInstance.FlowNo);

            if (listWorkerId.Count > 0)
            {
                //分配工作
                foreach (int workerId in listWorkerId)
                {
                    WorkItemInstance item = new WorkItemInstance();
                    item.Create(m_ActivityInstanceId, workerId);
                    m_ListWorkItemInstance.Add(item);
                }
                
                //发送提醒短信
                if(this.Activity.IsSms == "Y" )
                {
                    List<string> listMobile = new List<string>();

                    foreach (int workerId in listWorkerId)
                    {
                        IWorker worker = ctx["Worker"] as IWorker;

                        string mobile = worker.GetWorkerById(workerId).Mobile;

                        if (!string.IsNullOrEmpty(mobile))
                        {
                            listMobile.Add(mobile);
                        }
                    }

                    if (listMobile.Count>0)
                    {
                        ISMS sms = ctx["SMS"] as ISMS;

                        string content = string.Format("您有一笔{0}需要审核", FlowInstance.FlowDefine.Name);

                        Log4Net.LogInfo(content);

                        sms.SendSMG(listMobile, content, "99999");
                    }
                }
            }
            else
            {
                throw new Exception(string.Format("关卡({0})无签核人",this.Activity.Name));
            }
        }


        /// <summary>
        /// 获取出去的Transation
        /// </summary>
        /// <returns></returns>
        public List<TransationInstance> GetSplitTransationInstance()
        {
            List<Transation> listTransation = this.Activity.SplitTransations;

            List<TransationInstance> listTransationInstance = new List<TransationInstance>();

            foreach(Transation trans in listTransation)
            {
                TransationInstance instTrans = new TransationInstance();

                instTrans.Create(this.ID, trans.ID);

                instTrans.Parse();

                listTransationInstance.Add(instTrans);
            }

            return listTransationInstance;
        }

        /// <summary>
        /// 检查全部工作是否完成
        /// </summary>
        /// <returns></returns>
        public bool CheckWorkComplete()
        {
            foreach (WorkItemInstance item in WorkItemInstances)
            {
                if (item.State != WorkItemState.Completed)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 检查前置关卡状态
        /// </summary>
        /// <returns></returns>
        public bool CheckFrontActivityState()
        {
            List<Transation> listTransation=this.Activity.JoinTransations;

            if(this.Activity.JoinType == JoinType.AND_JOIN) //与汇聚
            {
                foreach (Transation trans in listTransation)
                {
                    ActivityInstance activityInst=this.FlowInstance.FindActivityInstance(trans.FromActivity.ID);

                    //如果有关卡没有实例化，前置条件不满足
                    if (activityInst == null)
                    {
                        return false;
                    }

                    //如果有关卡没有完成，前置条件不满足
                    if (activityInst.State != ActivityInstanceStatus.Completed)
                    {
                        return false;
                    }
                }

                return true;
            }
            else //或汇聚
            {
                foreach (Transation trans in listTransation)
                {
                    ActivityInstance activityInst = this.FlowInstance.FindActivityInstance(trans.FromActivity.ID);

                    if (activityInst == null) continue;//关卡没有实例化，跳过

                    if (activityInst.State == ActivityInstanceStatus.Completed)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// 结束关卡实例
        /// </summary>
        /// <returns></returns>
        public void Close()
        {
            //更新状态
            F_INST_ACTIVITY inst = DAL.WorkFlow.ActivityInstance.Get(m_ActivityInstanceId);

            inst.EndDate = DateTime.Now;
            inst.State = ActivityInstanceStatus.Completed;
            this.State = ActivityInstanceStatus.Completed;

            DAL.WorkFlow.ActivityInstance.Save(inst);

            //关闭未完成任务
            foreach (WorkItemInstance instWorkItem in WorkItemInstances)
            {
                if (instWorkItem.State == WorkItemState.Waiting)
                {
                    instWorkItem.Pass();
                }
            }

        }
    }
}

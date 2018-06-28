using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Spring.Context;
using Spring.Context.Support;
using System.Transactions;

using Anchor.FA.Utility;
using Anchor.FA.BLL.IBLL;
using Anchor.FA.Model;
using Anchor.FA.BLL.WorkFlow.Common;

namespace Anchor.FA.BLL.WorkFlow
{
    internal class Engine : IEngine
    {
        private static object lockObj = new object();

        public int CreateFlowNo(int flowId)
        {
            //IApplicationContext ctx = ContextRegistry.GetContext();
            //IPrimaryKeyCreater prikey = ctx["PrimaryKeyCreater"] as IPrimaryKeyCreater;

            //m_FlowNo = prikey.getIntPrimaryKey(string.Format("TB_{0}", flowId));
            return PrimaryKeyCreater.getIntPrimaryKey("FlowNo");
        }
        /// <summary>
        /// 外部模块申请表单
        /// </summary>
        /// <param name="flowId">流程ID</param>
        /// <param name="flowId">流程号</param>
        /// <param name="applerId">申请人ID</param>
        /// <param name="formDigest">数字摘要</param>
        /// <returns>流程号(如果返回-1,代表失败)</returns>
        public int ApplyFlow(int flowId, int flowNo, int applyerId, string formDigest)
        {
            lock (lockObj)
            {
                if (flowNo < 0 || flowId <= 0 || applyerId <= 0)
                {
                    return -1;
                }

                //防止重复点击
                //if (Convert.ToString(HttpContext.Current.Session[applyerId.ToString()])==string.Format("flowId{0},flowNo{1}", flowId, flowNo))
                //{
                //    return -1;
                //}

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    try
                    {
                        Dictionary<string, string> param;

                        ApplyInnerFlow(flowId, ref flowNo, applyerId, formDigest, out param);

                        int activityInstId = int.Parse(param["ActivityInstId"]);

                        List<int> listActivityId = GetNextActivitys(activityInstId).Select(t => t.ID).ToList();

                        //转交下一步
                        if (TransferBack(activityInstId, listActivityId))
                        {
                            scope.Complete();

                            //HttpContext.CurrentSession[applyerId.ToString()] = string.Format("flowId{0},flowNo{1}", flowId, flowNo);

                            return flowNo;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        Log4Net.LogError("Workflow_Engine", string.Format("flowId:{0},flowNo:{1},Message:{2}", flowId, flowNo, ex.ToString()));

                        return -1;
                    }
                }
            }
        }



        /// <summary>
        /// 撤回流程
        /// </summary>
        /// <param name="flowInstId">流程实例Id</param>
        /// <returns></returns>
        public bool RecallFlow(int flowId, int flowNo, string remark)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    FlowInstance instFlow = new FlowInstance(flowId, flowNo);

                    if (instFlow.Status != FlowInstanceState.Active)
                    {
                        return false;
                    }

                    instFlow.Recall(remark);

                    scope.Complete();

                    //HttpContext.Current.Session.Abandon();
                }
                catch (Exception ex)
                {
                    Log4Net.LogError("Workflow_Engine", string.Format("flowId:{0},flowNo:{1},Message:{2}", flowId, flowNo, ex.ToString()));

                    return false; ;
                }

                return true;
            }
        }

        /// <summary>
        /// 申请工作流内部表单
        /// </summary>
        /// <param name="flowId">流程id</param>
        /// <param name="applyerId">申请人id</param>
        /// <returns>流程实例id,开始关卡实例id，流程号</returns>
        public void ApplyInnerFlow(int flowId, ref int flowNo, int applyerId, string formDigest, out Dictionary<string, string> param)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                param = new Dictionary<string, string>();

                if (flowNo == 0)
                {
                    flowNo = CreateFlowNo(flowId);
                }

                //创建流程实例
                FlowInstance flowInst = new FlowInstance();
                ActivityInstance activityInst = new ActivityInstance();

                if (FlowInstance.Exists(flowId, flowNo))
                {
                    flowInst = new FlowInstance(flowId, flowNo);
                    flowInst.UpdateDigest(formDigest);
                    Activity startActvity = flowInst.FlowDefine.GetStartActivity();
                    activityInst = flowInst.FindActivityInstance(ActivityType.START);

                    WorkItemInstance item = new WorkItemInstance();
                    item.CreateResend(activityInst.ID, flowInst.ApplerID, "");
                }
                else
                {
                    flowInst.Create(flowId, flowNo, applyerId, formDigest);
                    activityInst.Create(flowInst.ID, flowInst.FlowDefine.GetStartActivity().ID);
                    activityInst.Active();
                }

                //返回流程实例id,开始关卡实例id，流程号
                param.Add("FlowInstId", flowInst.ID.ToString());
                param.Add("ActivityInstId", activityInst.ID.ToString());
                param.Add("SplitType", flowInst.FlowDefine.GetStartActivity().SplitType);

                scope.Complete();

            }
        }


        public bool DeleteFlowStance(int flowInstId)
        {
            try
            {
                F_INST_FLOW flowInst = DAL.WorkFlow.FlowInstance.Get(flowInstId);

                if (flowInst == null) return false;

                if (flowInst.State == FlowInstanceState.Recall 
                    || flowInst.State == FlowInstanceState.InActive
                    || flowInst.State == FlowInstanceState.Reject)
                {
                    DAL.WorkFlow.FlowInstance.Delete(flowInstId);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log4Net.LogError("Workflow_Engine", string.Format("FlowInstId:{0},Message:{1}", flowInstId, ex.ToString()));

                return false;
            }

            return true;
        }

        public bool DeleteFlowStance(int flowId,int flowNo)
        {
            try
            {
                F_INST_FLOW flowInst = DAL.WorkFlow.FlowInstance.Get(flowId,flowNo);

                if (flowInst == null) return false;

                if (flowInst.State == FlowInstanceState.Recall
                    || flowInst.State == FlowInstanceState.InActive
                    || flowInst.State == FlowInstanceState.Reject)
                {
                    DAL.WorkFlow.FlowInstance.Delete(flowInst.ID);
                }
                else
                {
                    return false; ;
                }
            }
            catch (Exception ex)
            {
                Log4Net.LogError("Workflow_Engine", string.Format("FlowId:{0},FlowId:{1},Message:{2}", flowId,flowNo,ex.ToString()));

                return false;
            }

            return true;
        }

        /// <summary>
        /// 签核
        /// </summary>
        /// <param name="workItemId">任务ID</param>
        /// <param name="approveValue">签核值：Y同意;N:否决</param>
        /// <param name="approveRemark">签核意见</param>
        /// <param name="transferNext">是否判断转到哪个下一关卡</param>
        /// <returns>成功与否</returns>
        public bool Approve(int workItemId, int approverId, string approveValue,
            string approveRemark, string nextActivityId)
        {
            //防止重复点击
            //if (Convert.ToString(HttpContext.Current.Session[approverId.ToString()]) == string.Format("WorkItemId:{0}", workItemId))
            //{
            //    return false;
            //}
            //else
            //{
            //    HttpContext.Current.Session[approverId.ToString()] = string.Format("WorkItemId:{0}", workItemId);
            //}


            lock (lockObj)
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    try
                    {
                        WorkItemInstance instWorkItem = new WorkItemInstance(workItemId);
                        FlowInstance instFlow = instWorkItem.ActivityInstance.FlowInstance;
                        ActivityInstance instActivity = instWorkItem.ActivityInstance;

                        //验证数字摘要
                        if (!instFlow.VerifyDigest())
                        {
                            Log4Net.LogError("Workflow_Engine", string.Format("WorkItemID:{0},Message:表单数据变化，无法签核", workItemId));

                            return false;
                        }

                        //数字签名
                        if (instActivity.Activity.IsSign.ToUpper() == "Y")
                        {
                            instWorkItem.Sign();
                        }

                        //更新工作实例状态
                        instWorkItem.Actorid = approverId;
                        instWorkItem.AppValue = approveValue;
                        instWorkItem.AppRemark = approveRemark;
                        instWorkItem.Approve();

                        //否决关闭所有对象
                        if (approveValue == WorkItemAppValue.Reject)
                        {
                            instFlow.ApproveReject += new FlowInstance.ApproveEventHandler(Mail.SendMail);
                            instFlow.ApproveReject += new FlowInstance.ApproveEventHandler(CallBack.Update);

                            Activity endActivity = instFlow.FlowDefine.Activitys.SingleOrDefault(t => t.Type == "end");

                            if (endActivity != null && endActivity.IsSms.ToUpper() == "Y")
                            {
                                instFlow.ApproveReject += new FlowInstance.ApproveEventHandler(Sms.SendSms);
                            }

                            instActivity.Close();
                            instFlow.Reject();

                            //关闭平行关卡
                            List<ActivityInstance> listParallelActivity =
                                instFlow.ActivityInstances.Where(
                                t => t.State == ActivityInstanceStatus.Active).ToList();

                            foreach (ActivityInstance p in listParallelActivity)
                            {
                                p.Close();
                            }
                        }
                        else if (approveValue == WorkItemAppValue.Return)
                        {
                            //关闭当间关卡实例
                            instActivity.Close();

                            //创建退回关卡实例
                            ActivityInstance instReturnActivity = new ActivityInstance();

                            instReturnActivity.Create(instFlow.ID, int.Parse(nextActivityId));

                            instReturnActivity.Active();

                            instReturnActivity.AssignWork();
                        }
                        else if (approveValue == WorkItemAppValue.Agree)
                        {
                            if (!string.IsNullOrEmpty(nextActivityId))
                            {
                                string[] arrActivityId = nextActivityId.TrimEnd(',').Split(',');

                                List<int> listActivityId = new List<int>();

                                foreach (string id in arrActivityId)
                                {
                                    listActivityId.Add(int.Parse(id));
                                }

                                if (!TransferBack(instWorkItem.ActivityInstance.ID, listActivityId))
                                {
                                    return false;
                                }
                            }
                        }
                        else if (approveValue == WorkItemAppValue.Reserve)
                        {
                            WorkItemInstance item = new WorkItemInstance();
                            item.Create(instActivity.ID, approverId);
                        }

                        scope.Complete();

                        return true;

                    }
                    catch (Exception ex)
                    {
                        Log4Net.LogError("Workflow_Engine", string.Format("WorkItemID:{0},Message:{1}", workItemId, ex.ToString()));

                        return false;
                    }

                }
            }
        }

        /// <summary>
        /// 判断任务是否是关卡最后未完成任务
        /// </summary>
        /// <param name="workItemInstId"></param>
        /// <returns></returns>
        public bool CheckIsFinalTask(int workItemInstId)
        {
            WorkItemInstance item = new WorkItemInstance(workItemInstId);

            ActivityInstance instActivity = item.ActivityInstance;

            //只要任意一个人完成
            if (instActivity.Activity.CompleteType == ActivityCompleteType.ONE)
            {
                return true;
            }
            else//需要全部人完成
            {
                int count = instActivity.WorkItemInstances.
                    Count(t => t.ID != workItemInstId && t.State == WorkItemState.Waiting);

                //无其他人或其他人已完成
                if (count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 得到下一关卡集合
        /// </summary>
        /// <param name="activityInstanceId">关卡实例id</param>
        /// <returns>关卡集合</returns>
        public List<F_ACTIVITY> GetNextActivitys(int activityInstId)
        {
            List<F_ACTIVITY> listActivity = new List<F_ACTIVITY>();

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                //获得关卡的发散Transation
                ActivityInstance activityInst = new ActivityInstance(activityInstId);

                List<Transation> listTransation = activityInst.Activity.SplitTransations;

                List<TransationInstance> listTransationInstance = new List<TransationInstance>();

                //实例化Transation并解析
                foreach (Transation trans in listTransation)
                {
                    //解析Transation条件
                    TransationInstance transInst = new TransationInstance();
                    transInst.Create(activityInst.ID, trans.ID);
                    transInst.Parse();

                    if (transInst.Value)
                    {
                        //关卡集合
                        F_ACTIVITY activity = new F_ACTIVITY();

                        activity.ID = transInst.Transation.ToActivity.ID;
                        activity.Name = transInst.Transation.ToActivity.Name;

                        listActivity.Add(activity);
                    }
                }

                scope.Complete();

                return listActivity;
            }
        }


        /// <summary>
        /// 转交一下步
        /// </summary>
        /// <param name="flowInstId">流程实例id</param>
        /// <param name="listToActivityId">下一步关卡list</param>
        /// <returns>是否成功</returns>
        public bool TransferBack(int activityInstId, List<int> listToActivityId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    //初始化变量
                    ActivityInstance instActivity = new ActivityInstance(activityInstId);

                    FlowInstance instFlow = instActivity.FlowInstance;

                    instActivity.Close();


                    //实例化后面的关卡
                    foreach (int activityId in listToActivityId)
                    {
                        //检查关卡是否被实例化过
                        ActivityInstance instToActivity = instFlow.FindActivityInstance(activityId);

                        if (instToActivity == null || instToActivity.State == ActivityInstanceStatus.Completed)
                        {
                            instToActivity = new ActivityInstance();

                            instToActivity.Create(instFlow.ID, activityId);
                        }

                        //检查前置关卡完成情况
                        if (instToActivity.CheckFrontActivityState())
                        {
                            //如果是结束关卡，关闭关卡实例
                            if (instToActivity.Activity.Type == ActivityType.END)
                            {
                                instFlow.ApprovePass += new FlowInstance.ApproveEventHandler(Mail.SendMail);

                                instFlow.ApprovePass += new FlowInstance.ApproveEventHandler(CallBack.Update);

                                instToActivity.Close();

                                instFlow.Pass();
                            }

                            //如果不是结束关卡，激活关卡并分配工作                    
                            else
                            {
                                instToActivity.Active();

                                instToActivity.AssignWork();
                            }

                            //关闭与前置关卡平行的关卡
                            List<ActivityInstance> listParallelActivity = 
                                instFlow.ActivityInstances.Where(
                                t => t.State == ActivityInstanceStatus.Active
                                && t.Activity.ID != instToActivity.Activity.ID).ToList();

                            foreach(ActivityInstance p in listParallelActivity)
                            {
                                p.Close();
                            }

                        }
                        else
                        {
                            instToActivity.Pending(); //前置关卡未完成，搁置状态
                        }
                    }


                    if (instActivity.Activity.Type == ActivityType.START)
                    {
                        instFlow.Active();

                        //调用表单申请Action
                        FlowAction<IApply> flowAction = new FlowAction<IApply>(instFlow.FlowDefine.IsInner, instFlow.FlowDefine.ID);

                        if (flowAction.action != null)
                        {
                            if (!flowAction.action.ApplyAction(instFlow.FlowDefine.ID, instFlow.FlowNo, instFlow.ApplerID))
                            {
                                throw new Exception(string.Format("ApplyAction失败,flowId:{0},flowNo:{1},ApplerID:{2}", instFlow.FlowDefine.ID, instFlow.FlowNo, instFlow.ApplerID));
                            }
                        }

                        //知会
                        List<F_FLOW_CONFIG> listConfig = DAL.WorkFlow.Flow.GetFlowConfig(instFlow.FlowDefine.ID, "Notice");
                        
                        List<int> listWorkerId = new List<int>();
                        
                        foreach (F_FLOW_CONFIG config in listConfig)
                        {
                            listWorkerId.Add(int.Parse(config.ItemValue));
                        }

                        new Flow().Notify(instFlow.FlowDefine.ID, instFlow.FlowNo, listWorkerId);

                    }

                    scope.Complete();

                    return true;
                }
                catch (Exception ex)
                {
                    Log4Net.LogError("Workflow_Engine", string.Format("ActivityInstId:{0},Message:{1}", activityInstId, ex.ToString()));

                    return false;
                }
                
            }
        }


        public List<F_ACTIVITY> GetReturnActivitys(int currentActivityInstId)
        {
            ActivityInstance activityInstance = new ActivityInstance(currentActivityInstId);

            if (activityInstance.Activity.ReturnType == ActivityReturnType.All)
            {
                return DAL.WorkFlow.ActivityInstance.GetPrivousAllReturnActivitys(activityInstance.FlowInstance.ID, activityInstance.Activity.ID);
            }
            else if (activityInstance.Activity.ReturnType == ActivityReturnType.One)
            {
                return DAL.WorkFlow.ActivityInstance.GetPrivousOneReturnActivitys(activityInstance.FlowInstance.ID, activityInstance.Activity.ID);
            }
            else if (activityInstance.Activity.ReturnType == ActivityReturnType.Custom)
            {
                return DAL.WorkFlow.ActivityInstance.GetCustomReturnActivitys(activityInstance.FlowInstance.ID, activityInstance.Activity.ID);
            }
            else
            {
                return null;
            }

        }
    }
}

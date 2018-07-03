using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.BLL.IBLL;
using Anchor.FA.Model;
using Anchor.FA.BLL.WorkFlow.Common;

namespace Anchor.FA.BLL.WorkFlow
{
    internal class Flow : IFlow
    {
        /// <summary>
        /// 签核列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="flowType"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="flowNo"></param>
        /// <param name="approverId"></param>
        /// <returns></returns>
        public object ApproveList(int page, int rows, string order, string sort,
            string flowId, DateTime startTime, DateTime endTime, string flowNo, int approverId)
        {
            return DAL.WorkFlow.WorkItemInstance.ApproveList(page, rows, order, sort, flowId, startTime, endTime, flowNo, approverId);
        }

        public int ApproveListCount(int approverId)
        {
            return DAL.WorkFlow.WorkItemInstance.ApproveListCount(approverId);
        }

        public IList<F_FLOW> GetAllFlow(string type)
        {
            return DAL.WorkFlow.Flow.GetAll(type);
        }

        public F_FLOW GetFlow(int flowId)
        {
            return DAL.WorkFlow.Flow.Get(flowId);
        }


        /// <summary>
        /// 追寻申请
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="flowType"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="flowNo"></param>
        /// <param name="applyerId"></param>
        /// <returns></returns>
        public object TraceApply(int page, int rows, string order, string sort,
            string flowType, DateTime startTime, DateTime endTime, string flowNo, int applyerId)
        {
            return DAL.WorkFlow.WorkItemInstance.TraceApply(page, rows, order, sort, flowType, startTime, endTime, flowNo, applyerId);
        }

        /// <summary>
        /// 追寻签核
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="flowType"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="flowNo"></param>
        /// <param name="approverId"></param>
        /// <returns></returns>
        public object TraceApprove(int page, int rows, string order, string sort,
    string flowType, DateTime startTime, DateTime endTime, string flowNo, int approverId)
        {
            return DAL.WorkFlow.WorkItemInstance.TraceApprove(page, rows, order, sort, flowType, startTime, endTime, flowNo, approverId);
        }

        public object TraceNotify(int page, int rows, string order, string sort,
string flowType, DateTime startTime, DateTime endTime, string flowNo, int approverId)
        {
            return DAL.WorkFlow.WorkItemInstance.TraceNotify(page, rows, order, sort, flowType, startTime, endTime, flowNo, approverId);
        }

        public int NotifyListCount(int workerId)
        {
            return DAL.WorkFlow.WorkItemInstance.NotifyListCount(workerId);
        }

        public object Monitor(int page, int rows, string order, string sort,
    string flowId, DateTime startTime, DateTime endTime, string flowNo)
        {
            return DAL.WorkFlow.WorkItemInstance.Monitor(page, rows, order, sort, flowId, startTime, endTime, flowNo);
        }

        /// <summary>
        /// 任务列表
        /// </summary>
        /// <param name="flowInstId"></param>
        /// <returns></returns>
        public List<C_TASK_LIST> TaskList(int flowInstId)
        {
            return DAL.WorkFlow.WorkItemInstance.TaskList(flowInstId);
        }

        public List<C_TASK_LIST> TaskList(int flowId,int flowNo)
        {
            return DAL.WorkFlow.WorkItemInstance.TaskList(flowId, flowNo);
        }

        public string TransaferAppValue(string state)
        {
            string result = string.Empty;

            if (state == WorkItemAppValue.Agree)
            {
                return "同意"; 
            }
            else if(state == WorkItemAppValue.Reject)
            {
                return "否决";
            }
            else if (state == WorkItemAppValue.Return)
            {
                return "退回";
            }
            else if (state == WorkItemAppValue.Reserve)
            {
                return "保留";
            }

            return result;
        }

        public string TransaferFlowState(string state)
        {
            string result = string.Empty;

            if (state == FlowInstanceState.InActive) {
                return "未完成";
            }
            else if (state == FlowInstanceState.Active) {
                return "正在签核";
            }
            else if (state == FlowInstanceState.Pass) {
                return "签核通过";
            }
            else if (state == FlowInstanceState.Reject) {
                return "否决";
            }
            else if (state == FlowInstanceState.Recall) {
                return "已撤回";
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 是否可以重新申请
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool IsApply(string state)
        {
            string result = string.Empty;

            if (state == FlowInstanceState.Recall
                || state == FlowInstanceState.InActive
                || state == FlowInstanceState.Reject)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string TaskListScript(int flowInstId)
        {
            List<C_TASK_LIST> listTask = DAL.WorkFlow.WorkItemInstance.TaskList(flowInstId);


            StringBuilder sb = new StringBuilder();

            foreach (C_TASK_LIST task in listTask)
            {
                sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>",                                task.ApproverName, 
                    TransaferAppValue(task.AppValue), 
                    task.AppRemark, 
                    task.ApproveDate == null? string.Empty : Convert.ToDateTime(task.ApproveDate).ToString("yyyy-MM-dd HH:mm:ss"), 
                    task.Role);
            }

            StringBuilder content = new StringBuilder();
            content.Append("<div style='text-align: center;line-height: 20px;width: 100%; height: 200px;'>");
            content.Append("<table width='80%' border='1' bordercolor='#99BBE8' cellpadding='0' cellspacing='0' style='border-collapse:collapse;'>");
            content.Append("<tr style='background-color: #eee'><td  width='20%'>签核人</td><td  width='10%'>签核值</td><td width='30%'>签核意见</td><td  width='20%'>签核时间</td><td  width='20%'>步骤</td></tr>");
            content.Append(sb.ToString());
            content.Append("</table></div>");

            return content.ToString();
        }

        public bool SaveAttacment(int flowId,int flowNo,System.Web.HttpPostedFileBase file, string path)
        {
            return DAL.WorkFlow.Attachment.Save(flowId, flowNo, file, path);
        }


        public F_INST_ATTACHMENT DownLoadAttacment(int fileId)
        {
            return DAL.WorkFlow.Attachment.DownLoad(fileId);
        }

        public bool DeleteAttacment(int fileId, string path)
        {
            return DAL.WorkFlow.Attachment.Delete(fileId, path);
        }

        public List<F_INST_ATTACHMENT> GetAttacment(int flowId, int flowNo)
        {
            return DAL.WorkFlow.Attachment.GetAttacment(flowId, flowNo);
        }


        public List<F_FLOW_CONFIG> GetFlowConfig(int flowId, string itemName)
        {
            return DAL.WorkFlow.Flow.GetFlowConfig(flowId,itemName);
        }

        public bool Notify(int flowId, int flowNo, List<int> listWorkerId)
        {
            return DAL.WorkFlow.Flow.Notify(flowId, flowNo, listWorkerId);
        }


        public bool InsertFlowConfig(string value)
        {
            return DAL.WorkFlow.Flow.InsertFlowConfig(value);
        }

        public object LoadAllFlowByPage(int page, int rows, string order, string sort, int? catalogID)
        {
            return DAL.WorkFlow.Flow.LoadAllFlowByPage(page, rows, order, sort, catalogID);
        }

        /// <summary>
        /// 新增的保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(F_FLOW entity)
        {
            DAL.WorkFlow.Flow.Insert(entity);
            return true;
        }

        /// <summary>
        /// 修改的保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Save(F_FLOW entity)
        {
            if (entity.ID == 0)
            {
                entity.ID = DAL.WorkFlow.Flow.Insert(entity);
            }
            return DAL.WorkFlow.Flow.Save(entity);
        }

        /// <summary>
        /// 获取表单类型列表
        /// </summary>
        /// <returns></returns>
        public List<F_CATALOG> GetCatalog()
        {
            return DAL.WorkFlow.Flow.GetCatalog();
        }

        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public bool Delete(IList<int> idList)
        {
            return DAL.WorkFlow.Flow.Delete(idList);
        }

        public object LoadAllCatalogByPage(int page, int rows, string order, string sort)
        {
            return DAL.WorkFlow.Flow.LoadAllCatalogByPage(page,rows,order,sort);
        }

        public bool SaveCatalog(F_CATALOG entity)
        {
            return DAL.WorkFlow.Flow.SaveCatalog(entity);
        }

        public bool DeleteCatalog(int id)
        {
            return DAL.WorkFlow.Flow.DeleteCatalog(id);
        }

        public bool NoticeReadFlag(int flowId, int flowNo, int workerID)
        {
            return DAL.WorkFlow.WorkItemInstance.NoticeReadFlag(flowId, flowNo, workerID);
        }
    }
}

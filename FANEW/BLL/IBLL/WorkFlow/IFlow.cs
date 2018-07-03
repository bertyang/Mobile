using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;

namespace Anchor.FA.BLL.IBLL
{
    /// <summary>
    /// 流程相关接口
    /// </summary>
    public interface IFlow
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flowInstId"></param>
        /// <returns></returns>

        List<C_TASK_LIST> TaskList(int flowInstId);

        List<C_TASK_LIST> TaskList(int flowId, int flowNo);

        string TaskListScript(int flowInstId);


        F_FLOW GetFlow(int flowId);

        List<F_FLOW_CONFIG> GetFlowConfig(int flowId, string itemName);

        bool Notify(int flowId, int flowNo, List<int> listWorkerId);

        bool InsertFlowConfig(string value);
    }
}

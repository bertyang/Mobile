using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;

namespace Anchor.FA.BLL.IBLL
{
    /// <summary>
    /// 工作流引擎接口
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// 创建表单号
        /// </summary>
        /// <param name="flowId"></param>
        /// <param name="applyerId"></param>
        /// <returns></returns>
        int CreateFlowNo(int flowId);

        /// <summary>
        /// 外部模块申请表单
        /// </summary>
        /// <param name="flowId">流程ID</param>
        /// <param name="flowId">流程号</param>
        /// <param name="applerId">申请人ID</param>
        /// <param name="formDigest">数字摘要</param>
        /// <returns>流程号(如果返回-1,代表失败)</returns>
        int ApplyFlow(int flowId, int flowNo, int applyerId, string formDigest);


        /// <summary>
        /// 删除流程实例
        /// </summary>
        /// <param name="flowInstId"></param>
        /// <returns></returns>
        bool DeleteFlowStance(int flowInstId);

        /// <summary>
        /// 删除流程实例
        /// </summary>
        /// <param name="flowId"></param>
        /// <param name="flowNo"></param>
        /// <returns></returns>
        bool DeleteFlowStance(int flowId,int flowNo);


        /// <summary>
        /// 撤回表单
        /// </summary>
        /// <param name="flowId">流程Id</param>
        /// <param name="flowNo">流程No</param>
        /// <param name="recallerId">撤回人Id</param>
        /// <returns></returns>
        bool RecallFlow(int flowId, int flowNo, string remark);

    }
}

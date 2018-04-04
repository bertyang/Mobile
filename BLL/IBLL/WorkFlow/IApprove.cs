using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.BLL.IBLL
{
    /// <summary>
    /// 签核时刻Action
    /// </summary>
    public interface IApprove
    {
        bool ApproveAction(int flowId, int flowNo, int activityId, int approverId, string appValue, string appRemark);
    }
}

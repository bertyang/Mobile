using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.BLL.IBLL
{
    /// <summary>
    /// 申请时刻Action
    /// </summary>
    public interface IApply
    {
        bool ApplyAction(int flowId, int flowNo, int applyerId);
    }
}

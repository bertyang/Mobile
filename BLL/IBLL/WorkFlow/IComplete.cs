using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.BLL.IBLL
{
    /// <summary>
    /// 完成时刻Action
    /// </summary>
    public interface IComplete
    {
        bool Complete(int flowNo, string flowStatus);
    }
}

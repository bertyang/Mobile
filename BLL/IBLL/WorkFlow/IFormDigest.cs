using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.BLL.IBLL
{
    /// <summary>
    /// 表单数字摘要
    /// </summary>
    public interface IFormDigest
    {
        string GetFormDigest(int flowNo);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.BLL.IBLL
{
    public interface ISMS
    {
        bool SendSMG(List<string> TelList, string Content, string OperatorCode);
    }
}

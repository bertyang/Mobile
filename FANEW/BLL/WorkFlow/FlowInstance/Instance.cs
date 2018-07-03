using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Spring.Context;
using Spring.Context.Support;

namespace Anchor.FA.BLL.WorkFlow
{
    internal class Instance
    {
        protected static IApplicationContext ctx = ContextRegistry.GetContext();
    }
}

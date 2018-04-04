using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    public struct C_ORGANIZATION_ROLE
    {
        public  int OrgID;
        public int RoleID;

        public C_ORGANIZATION_ROLE(int orgId, int roleId)
        {
            OrgID = orgId;
            RoleID = roleId;
        }
    }
}

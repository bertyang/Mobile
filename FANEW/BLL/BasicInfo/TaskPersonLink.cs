using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;

namespace Anchor.FA.BLL.BasicInfo
{
    public class TaskPersonLink
    {
        public static List<TTaskPersonLink> GetTaskPersons(string TaskCode)
        {
            return Anchor.FA.DAL.BasicInfo.TaskPersonLink.GetTaskPersons(TaskCode);
        }
    }
}

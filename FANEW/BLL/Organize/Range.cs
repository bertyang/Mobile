using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;

namespace Anchor.FA.BLL.Organize
{
    public class Range
    {
        public static List<B_Range> GetRangeFromRole(int RoleID) {
            return Anchor.FA.DAL.Organize.Range.GetRangeFromRole(RoleID);
        }
        public static List<B_Range> GetRangeFromWorkerID(int WorkerID)
        {
            return Anchor.FA.DAL.Organize.Range.GetRangeFromWorkerID(WorkerID);
        }
        public static List<B_Range> GetRangeFromWorkerID(int WorkerID, int ActionID)
        {
            return Anchor.FA.DAL.Organize.Range.GetRangeFromWorkerID(WorkerID, ActionID);
        }

    }
}

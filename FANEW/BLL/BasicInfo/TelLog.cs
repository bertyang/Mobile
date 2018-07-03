using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;

namespace Anchor.FA.BLL.BasicInfo
{
    public class TelLog
    {
        //public object LoadAllTelLogByPage(DateTime begin, DateTime end, int page, int rows, string order, string sort)
        //{
        //    return DAL.BasicInfo.TelLog.LoadAllTelLogByPage(begin,end,page, rows, order, sort);
        //}
        public object TelLogSearch(DateTime begin, DateTime end, string tel, string rec, string op, string res, string des,
            int page, int rows, string order, string sort, Anchor.FA.Utility.ButtonPower p, C_WorkerDetail userDetail)
        {
            return DAL.BasicInfo.TelLog.Search(begin, end, tel, rec, op, res, des, page, rows, order, sort, p, userDetail);
        }

        public List<TZTelLogRecordType> GetAllRecordTypes()
        {
            return DAL.BasicInfo.TelLog.GetAllRecordTypes();
        }
        public List<TZTelLogOperator> GetAllOperatorTypes()
        {
            return DAL.BasicInfo.TelLog.GetAllOperatorTypes();
        }
        public List<TZTelLogResult> GetAllResult()
        {
            return DAL.BasicInfo.TelLog.GetAllResult();
        }
        public List<TDesk> GetAllDesks()
        {
            return DAL.BasicInfo.TelLog.GetAllDesks();
        }
    }
}

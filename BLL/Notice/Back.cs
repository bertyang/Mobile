using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.BLL.IBLL;
using Anchor.FA.Model;

namespace Anchor.FA.BLL.Notice
{
    internal class Back
    {
        public object GetTelBackCallsV7(int page, int rows, string order, string sort, string start, string end,string type)
        {
            return DAL.Notice.Back.GetTelBackCallsV7(page, rows, order, sort, start, end, type);
        }

        public TBackCallSM GetBackSM(int code)
        {
            return DAL.Notice.Back.GetBackSM(code);
        }

        public TBackCall GetBackCall(string taskCode)
        {
            return DAL.Notice.Back.GetBackCall(taskCode);
        }

        public void SaveBackSM(TBackCallSM entity)
        {
            DAL.Notice.Back.SaveBackSM(entity);
        }

        public void SaveBackCall(TBackCall entity)
        {
            DAL.Notice.Back.SaveBackCall(entity);
        }

        public string GetFirstDispatcher(string taskCode)
        {
            return DAL.Notice.Back.GetFirstDispatcher(taskCode);
        }
    }
}

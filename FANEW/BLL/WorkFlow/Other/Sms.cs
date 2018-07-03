using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.BLL.IBLL;
using Anchor.FA.Model;
using Anchor.FA.BLL.WorkFlow.Common;

namespace Anchor.FA.BLL.WorkFlow
{
    internal class Sms : Instance
    {
        public static void SendSms(object sender, ApproveEventArgs args)
        {
            ISMS sms = ctx["SMS"] as ISMS;
            IWorker worker = ctx["Worker"] as IWorker;

            //string title = string.Format("{0}(表单号:{1})被否决", args.FlowName, args.FlowNo);

            string content = string.Format("您于{0}申请的{1}(表单号:{2})已经被否决。",
                                            args.BeginDate.ToString("yyyy/MM/dd HH:mm"),
                                            args.FlowName,
                                            args.FlowNo);

            B_WORKER w= worker.GetWorkerById(args.ApplyerId);

            if (w != null && !string.IsNullOrEmpty(w.Mobile))
            {
                sms.SendSMG(new List<string> { w.Mobile }, content, "99999");

                //if (!innerCommBLL.SendSMG(new List<string> { w.Mobile }, content, "99999"))
                //{
                //    throw new Exception(string.Format("短信发送失败,flowId{0},flowNo{1},AppValue{2}", args.FlowId, args.FlowNo, args.AppValue));
                //}
            }
        }
    }
}

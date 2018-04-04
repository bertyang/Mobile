using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.BLL.IBLL;
using Anchor.FA.BLL.WorkFlow.Common;

namespace Anchor.FA.BLL.WorkFlow
{
    internal class Mail : Instance
    {
        public static void SendMail(object sender,ApproveEventArgs args)
        {
            IEmail email = ctx["Email"] as IEmail;
            IFlow flow = ctx["Flow"] as IFlow;
              
            string title;
            StringBuilder content=new StringBuilder();
            string link = string.Format(@"<a href='#' style='cursor:hand' onclick=Open('/WorkFlow/TraceView/?flowNo={0}&flowId={1}&flowInstId={2}&url={3}&isInner={4}')><u>表单号:{5}</u></a>", args.FlowNo,
            args.FlowId,
            args.FlowInstId,
            args.Url,
            args.IsInner,
            args.FlowNo);


            if (args.AppValue == "Y")
            {
                title = string.Format("{0}(表单号:{1})已经审核通过", args.FlowName,
                       args.FlowNo);


                content.Append("<html><meta http-equiv='content-type' content='text/html; charset=gb2312' /><body>");
                content.AppendFormat("您于{0}申请的{1}({2})已经审核通过。",
                    args.BeginDate.ToString("yyyy/MM/dd HH:mm"),
                    args.FlowName,
                    link);

            }
            else
            {
                title = string.Format("{0}(表单号:{1})被否决", args.FlowName, args.FlowNo);

                content.AppendFormat("您于{0}申请的{1}({2})已经被否决。",
                    args.BeginDate.ToString("yyyy/MM/dd HH:mm"),
                    args.FlowName,
                    link);

            }

            //content.Append("<BR><table cellspace='1' style='width:350px;' id='tablehelp'>");
            //content.Append("<tr><td><b>签核记录：</b></td><td>");
            content.AppendLine("<br>");
            content.AppendLine(flow.TaskListScript(args.FlowInstId));
            //content.Append("</td></tr></table></body><html>");

            email.SendMail(title, content.ToString(), args.ApplyerId.ToString(), false);

            //if (!email.SendMail(title, content.ToString(), args.ApplyerId.ToString(), false))
            //{
            //    throw new Exception(string.Format("邮件发送失败:flowId{0},flowNo{1},AppValue{2}", args.FlowId, args.FlowNo, args.AppValue));
            //}
        }
    }
}

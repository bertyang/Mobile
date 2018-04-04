using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Anchor.FA.Model;
using Anchor.FA.BLL.IBLL;
using Anchor.FA.BLL.WorkFlow.Common;

namespace Anchor.FA.BLL.WorkFlow
{
    internal class CallBack
    {
        public static void Update(object sender, ApproveEventArgs args)
        {
            FlowAction<IComplete> flowAction = new FlowAction<IComplete>(args.IsInner, args.FlowId);

            if (flowAction.action != null)
            {
                if (!flowAction.action.Complete(args.FlowNo, args.AppValue))
                {
                    throw new Exception(string.Format("回写失败,flowId{0},flowNo{1},AppValue{2}", args.FlowId, args.FlowNo, args.AppValue));
                }

            }
            //else
            //{
            //    throw new Exception(string.Format("没有配置回写,flowId{0},flowNo{1},AppValue{2}", args.FlowId, args.FlowNo, args.AppValue));
            //}
           
        }
    }
}

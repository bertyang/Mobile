using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Anchor.FA.BLL.IBLL;
using Anchor.FA.Model;
using Anchor.FA.Utility;

namespace Anchor.FA.BLL.WorkFlow
{
    /// <summary>
    /// 实例化流程Action实现类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class FlowAction<T>
    {
        public T action;

        public FlowAction(bool isInner, int flowId)
        {
            try
            {
                F_FLOW_CONFIG actionDllName = DAL.WorkFlow.Flow.
                    GetFlowConfig(flowId, "ActionDllName").SingleOrDefault();
                F_FLOW_CONFIG actionClassName = DAL.WorkFlow.Flow.
                    GetFlowConfig(flowId, "ActionClassName").SingleOrDefault();

                if (actionDllName != null && actionClassName != null
                            && !string.IsNullOrEmpty(actionDllName.ItemValue)
                            && !string.IsNullOrEmpty(actionClassName.ItemValue))
                {
                    action = (T)Assembly.
                        Load(actionDllName.ItemValue).
                        CreateInstance(actionClassName.ItemValue);

                }
                else
                {
                    string className = string.Format("FLOW{0}.Action", flowId);
                    string dllName = string.Format("FLOW{0}", flowId);

                    action = (T)Assembly.Load(dllName).CreateInstance(className);
                }
  
            }
            catch
            {
                //Log4Net.LogError("实例化流程Action实现类", ex.ToString());
            }
        }
    }
}

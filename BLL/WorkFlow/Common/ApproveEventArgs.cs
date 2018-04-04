using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.BLL.WorkFlow.Common
{
    internal class ApproveEventArgs : EventArgs
    {
        private int m_FlowId;
        private string m_FlowName;
        private int m_FlowInstId;
        private int m_FlowNo;
        private string m_Url;
        private bool m_IsInner;
        private string m_appValue;
        private int m_ApplyerId;
        private DateTime m_BeginDate;

        public int FlowId
        {
            get { return m_FlowId; }
            set { m_FlowId = value; }
        }

        public string FlowName
        {
            get { return m_FlowName; }
            set { m_FlowName = value; }
        }

        public int FlowInstId
        {
            get { return m_FlowInstId; }
            set { m_FlowInstId = value; }
        }

        public int FlowNo
        {
            get { return m_FlowNo; }
            set { m_FlowNo = value; }
        }

        public string Url
        {
            get { return m_Url; }
            set { m_Url = value; }
        }

        public bool IsInner
        {
            get { return m_IsInner; }
            set { m_IsInner = value; }
        }

        public string AppValue
        {
            get { return m_appValue; }
            set { m_appValue = value; }
        }

        public DateTime BeginDate
        {
            get { return m_BeginDate; }
            set { m_BeginDate = value; }
        }

        public int ApplyerId
        {
            get { return m_ApplyerId; }
            set { m_ApplyerId = value; }
        }

    }
}

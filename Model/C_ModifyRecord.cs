using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    public class C_ModifyRecord
    {
        private TModifyRecord m_tmr;//修改记录
        /// <summary>
        /// 修改记录
        /// </summary>
        public TModifyRecord tmr { get { return m_tmr; } set { m_tmr = value; } }
        private string m_修改类型;
        public string 修改类型
        {
            get { return m_修改类型; }
            set { m_修改类型 = value; }
        }
        private string m_操作员;
        public string 操作员
        {
            get { return m_操作员; }
            set { m_操作员 = value; }
        }
    }
}

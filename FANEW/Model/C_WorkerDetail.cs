using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{    
    /// <summary>
    /// 人员 等
    /// </summary>
    public class C_WorkerDetail
    {

        private B_WORKER m_W;//人员
        public B_WORKER W { get { return m_W; } set { m_W = value; } }//人员
        private List<B_ORGANIZATION> m_WOrg = new List<B_ORGANIZATION>();//人员 机构        
        /// <summary>
        /// 人员 机构
        /// </summary>
        public List<B_ORGANIZATION> WOrg { get { return m_WOrg; } set { m_WOrg = value; } }

        private B_ORGANIZATION m_WOrgM;//人员所属主要机构
        /// <summary>
        /// 人员所属主要机构 Type == 0
        /// </summary>
        public B_ORGANIZATION WOrgM { get { return m_WOrgM; } set { m_WOrgM = value; } }//人员所属主要机构

        private List<B_ROLE> m_WRol = new List<B_ROLE>();//人员 角色        
        /// <summary>
        /// 人员 角色
        /// </summary>
        public List<B_ROLE> WRol { get { return m_WRol; } set { m_WRol = value; } }
        /// <summary>
        /// 角色列表
        /// </summary>
        public int[] Rol;//角色列表
        /// <summary>
        /// 机构列表
        /// </summary>
        public int[] Org;//机构列表
        /// <summary>
        /// 老板FA分站列表
        /// </summary>
        public string[] Sta;//分站列表
        /// <summary>
        /// 老板FA工号列表
        /// </summary>
        public string[] EmpNo;//老板FA工号列表
        /// <summary>
        /// 老板FA编码列表
        /// </summary>
        public string[] PersonCode;//老板FA编码列表

        private List<TPerson> m_WPer = new List<TPerson>();//老板FA人员 列表        
        /// <summary>
        /// 老板FA人员 列表
        /// </summary>
        public List<TPerson> WPer { get { return m_WPer; } set { m_WPer = value; } }


        private List<TStation> m_WSta = new List<TStation>();//老板FA分站 列表        
        /// <summary>
        /// 老板FA分站 列表
        /// </summary>
        public List<TStation> WSta { get { return m_WSta; } set { m_WSta = value; } }

        private List<B_ACTION> m_WAct = new List<B_ACTION>();//人员 页面   
        /// <summary>
        /// 人员 页面
        /// </summary>
        public List<B_ACTION> WAct { get { return m_WAct; } set { m_WAct = value; } }


        private List<B_Range> m_WRan = new List<B_Range>();//人员 控制按钮
        /// <summary>
        /// 人员 控制按钮
        /// </summary>
        public List<B_Range> WRan { get { return m_WRan; } set { m_WRan = value; } }

        /// <summary>
        /// 获得某个页面的按钮权限
        /// </summary>
        public List<B_Range> GetRange(int ActionID)
        {
            List<B_Range> list = (from r in WRan
                                 where r.ActionId == ActionID
                                 select r).ToList();
                return list;
        }

        /// <summary>
        /// 所属分中心编码
        /// </summary>
        public int CenterCode;

        /// <summary>
        /// 所属分中心ID
        /// </summary>
        public int CenterID;
    }
}

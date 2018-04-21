using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;

namespace Anchor.FA.BLL.Organize
{
    public class Organize : IBLL.IOrganization
    {

        #region 获取组织机构树
        public List<C_ORGANIZE_TREE> GetTree(int? orgId)
        {
            if (orgId==null)
            {
                return GetTree("");
            }
            else
            {
                return GetTree(orgId.ToString());
            }
        }
        /// <summary>
        /// 获取组织机构树
        /// </summary>
        /// <returns>List</returns>
        public List<C_ORGANIZE_TREE> GetTree(string orgId)
        {
            List<B_ORGANIZATION> list = GetAllUnit();

            List<C_ORGANIZE_TREE> mtm = new List<C_ORGANIZE_TREE>();

            foreach (B_ORGANIZATION r in list)
            {
                string cls = "";
                string url = "";
                switch (r.Type)
                {
                    case (int)OrgType.Root: cls = "icon tu1605"; url = "/Organize/Center/"; break; //根节点
                    case (int)OrgType.Center: cls = "icon tu0820"; url = "/Organize/Station/?centerId="; break; //分中心                    
                    case (int)OrgType.Station: cls = "icon tu1911"; url = "/Organize/Branch/?stationId="; break; //分站
                    case (int)OrgType.Branch: cls = "icon tu0522"; break; //科室
                }

                mtm.Add(new C_ORGANIZE_TREE
                {
                    id = r.ID.ToString(),
                    text = r.Name,
                    ParentID = r.ParentID.ToString(),
                    iconCls = cls,
                    Type = "Org",
                    attributes = url
                });
            }
            if (list.Count() == 0)
            {
                return null;
            }
            else
            {
                List<C_ORGANIZE_TREE> listResult = new List<C_ORGANIZE_TREE>();

                if (string.IsNullOrEmpty(orgId))
                {
                    listResult.Add(GetUnitTree(mtm, "1"));
                }
                else
                { 
                    foreach(string s in orgId.Split(','))
                    {
                        listResult.Add(GetUnitTree(mtm, s));
                    }
                }

                return listResult;
            }
        }

        private C_ORGANIZE_TREE GetUnitTree(List<C_ORGANIZE_TREE> mtmList, string Pid)
        {
            //根节点
            C_ORGANIZE_TREE root = mtmList.Single(item => item.id == Pid);
            root.children = new List<C_ORGANIZE_TREE>();

            //子节点
            List<C_ORGANIZE_TREE> listParent = mtmList.Where(item => item.ParentID == Pid).ToList();

            foreach (C_ORGANIZE_TREE t in listParent)
            {
                //C_ORGANIZE_TREE tm = new C_ORGANIZE_TREE();

                //tm.id = t.id;
                //tm.text = t.text;
                //tm.ParentID = t.ParentID;
                //tm.Type = "Org";
                //tm.iconCls = t.iconCls;

                if (mtmList.Where(item => item.ParentID == t.id).Any())
                {
                    root.children.Add(GetUnitTree(mtmList, t.id));
                }
                else
                {
                    root.children.Add(t);
                }
               
            }

            return root;
        }

        /// <summary>
        /// 遍历父节点添加子节点
        /// </summary>
        /// <param name="listParentNode">父节点集合/param>
        /// <param name="parentNodeType">父节点类型</param>
        /// <param name="listChildNode">子节点集合</param>
        /// <param name="childNode">子节点样式</param>
        public void AddNode(List<C_ORGANIZE_TREE> listParentNode, string parentNodeType, List<C_ORGANIZE_TREE> listChildNode, string childNodeStyle)
        {
            if (listParentNode == null || listParentNode.Count == 0) return;

            foreach (C_ORGANIZE_TREE u in listParentNode)
            {
                List<C_ORGANIZE_TREE> listChildren = new List<C_ORGANIZE_TREE>();

                foreach (C_ORGANIZE_TREE w in listChildNode.Where(t => t.ParentID == u.id && u.Type == parentNodeType).ToList())
                {
                    C_ORGANIZE_TREE tc = new C_ORGANIZE_TREE();

                    tc.id = w.id;
                    tc.text = w.text;
                    tc.ParentID = w.ParentID;
                    tc.Type = w.Type;
                    tc.iconCls = childNodeStyle;
                    tc.children = w.children;

                    listChildren.Add(tc);
                }

                if (listChildren.Count > 0)
                {
                    if (u.children == null)
                    {
                        u.children = listChildren;
                    }
                    else
                    {
                        u.children.AddRange(listChildren);
                    }
                }

                AddNode(u.children, parentNodeType, listChildNode, childNodeStyle);

            }
        }

        #endregion

        #region 获取组织机构

        /// <summary>
        /// 获取组织信息
        /// </summary>
        /// <param name="id">机构编码</param>
        /// <returns>List</returns>
        public B_ORGANIZATION GetUnit(int id)
        {
            return DAL.Organize.Organize.GetUnit(id);
        }

        public void GetUnitList(List<int> listUnit, int departId)
        {
            listUnit.Add(departId);

            int ParentID = GetUnit(departId).ParentID;

            if (ParentID != 0)
            {
                GetUnitList(listUnit, ParentID);
            }
        }

        /// <summary>
        /// 获取所有组织机构
        /// </summary>
        /// <param name="id">机构编码</param>
        /// <returns>List</returns>
        public List<B_ORGANIZATION> GetAllUnit()
        {
            return DAL.Organize.Organize.GetAllUnit();
        }

        /// <summary>
        /// 获取指定机构下所有组织机构
        /// </summary>
        /// <param name="id">指定机构</param>
        /// <returns>List</returns>
        //public List<B_ORGANIZATION> GetAllUnit(string orgId)
        //{
        //    return DAL.Organize.Organize.GetAllUnit(orgId);
        //}

        #endregion

        public List<B_ORGANIZATION> GetUnitByLoginer(int CurrentUserID)
        {
            return DAL.Organize.Organize.GetUnitByLoginer(CurrentUserID);
        }
    }
}

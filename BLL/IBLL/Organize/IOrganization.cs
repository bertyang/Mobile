using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;

namespace Anchor.FA.BLL.IBLL
{
    public interface IOrganization
    {
        #region 部门管理
        //bool SaveUnit(B_ORGANIZATION entity);
        //bool UnitDelete(string id);

        //List<B_POST> GetPostByOrgID(int id);
        //List<S_STORE_HOUSE> GetHouseByOrgID(int id);  
        #endregion;

        #region 获取组织机构
        /// <summary>
        /// 获取组织机构
        /// </summary>
        /// <param name="id">机构编码</param>
        /// <returns>List</returns>
        B_ORGANIZATION GetUnit(int id);
        #endregion

        #region 获取组织机构树
        /// <summary>
        /// 获取组织机构树
        /// </summary>
        /// <param name="id">机构编码</param>
        /// <returns>List</returns>
        List<C_ORGANIZE_TREE> GetTree(int? exceptUnitId);

        /// <summary>
        /// 遍历父节点添加子节点
        /// </summary>
        /// <param name="listParentNode">父节点集合/param>
        /// <param name="parentNodeType">父节点类型</param>
        /// <param name="listChildNode">子节点集合</param>
        /// <param name="childNode">子节点样式</param>
        void AddNode(List<C_ORGANIZE_TREE> listParentNode, string parentNodeType, List<C_ORGANIZE_TREE> listChildNode, string childNodeStyle);

        #endregion

        #region 获取所有组织
        /// <summary>
        /// 获取组织机构
        /// </summary>
        /// <param name="id">机构编码</param>
        /// <returns>List</returns>
        List<B_ORGANIZATION> GetAllUnit();

        void GetUnitList(List<int> listUnit, int departId);
        #endregion

        /// <summary>
        /// 获取当前登陆者所属的机构
        /// </summary>
        /// <param name="CurrentUserID"></param>
        /// <returns></returns>
        List<B_ORGANIZATION> GetUnitByLoginer(int CurrentUserID);
   
    }
}


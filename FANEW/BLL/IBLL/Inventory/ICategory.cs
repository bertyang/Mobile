using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.BLL.IBLL
{
    public interface ICategory
    {
        /// <summary>
        /// 获取物品类别树
        /// </summary>
        /// <param name="id">物品编码</param>
        /// <returns>List</returns>
        object GetTree(string categoryID);

        ///// <summary>
        ///// 获取物品类别
        ///// </summary>
        //S_CATEGORY GetCategory(int id);

        //List<S_CATEGORY> GetCategoryByParentID(int id);

        ///// <summary>
        ///// 获取所有物品类别
        ///// </summary>
        //List<S_CATEGORY> GetAllCategory();

        ///// <summary>
        ///// 根据物品类别得到物品
        ///// </summary>
        ///// <returns></returns>
        //List<S_GOODS> GetGoodsByCategoryID(int categoryID);

        //bool SaveCategory(S_CATEGORY entity);
        //bool CategoryDelete(string id);
    }
}

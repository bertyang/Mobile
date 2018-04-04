using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.BLL.IBLL
{
    public interface IStorehouse
    {
        //object LoadAllStorehouseByPage(int page, int rows, string order, string sort, int? id, int? CurrentUserID, string houseId);

        //List<C_ORGANIZE_TREE> GetTree(string storeHouseId);

        //List<C_ORGANIZE_TREE> GetHouseTree(int CurrentUserID, int? transferIn);

        //S_STORE_HOUSE GetStorehouse(int? id);
        /// <summary>
        /// 根据组织获取下面库房
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns>JSON序列化字符串</returns>
        string GetStoreHouseJSONStringByOrg(int orgID);

        //List<B_ORGANIZATION> GetUnit(int CurrentUserID);

        //bool SaveStorehouse(S_STORE_HOUSE entity);
        //bool DeleteStorehouse(IList<int> idList);


        //object LoadAllStorageLocationByPage(int page, int rows, string order, string sort, int id);
        //S_STORE_HOUSE_LOCATION GetStorageLocation(int? id);
        //List<S_STORE_HOUSE_LOCATION> GetStorageLocationByHouse(int houseID);
        //bool SaveStorageLocation(S_STORE_HOUSE_LOCATION entity);
        //bool DeleteStorageLocation(IList<int> idList);
        /// <summary>
        /// 根据负责人获取对应库房
        /// </summary>
        /// <param name="managerID"></param>
        /// <returns>JSON序列化字符串</returns>
        string GetStoreHouseJSONStringByManager(int managerID);

        //List<S_STORE_HOUSE> GetAllStoreHouse();
    }
}

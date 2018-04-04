using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;
using Anchor.FA.Utility;

namespace Anchor.FA.BLL.BasicInfo
{
    public class CommonData
    {
        public IList<G_DATA> GetDataByType(string type)
        {
            return DAL.BasicInfo.CommonData.GetDataByType(type);
        }
        public object SearchLoadAll(string type)
        {
            return DAL.BasicInfo.CommonData.SearchLoadAll(type);
        }
        public object LoadAllDataByPage(int page, int rows, string order, string sort, string type)
        {
            return DAL.BasicInfo.CommonData.LoadAllDataByPage(page, rows, order, sort, type);
        }
        public object Edit(int? id)
        {
            return DAL.BasicInfo.CommonData.Edit(id);
        }
        public bool Save(G_DATA entity)
        {
            return DAL.BasicInfo.CommonData.Save(entity);
        }
        public bool Delete(IList<int> idList)
        {
            return DAL.BasicInfo.CommonData.Delete(idList);
        }
        public object DataType()
        {
            return DAL.BasicInfo.CommonData.DataType();
        }

        /// <summary>
        /// 加载护士列表
        /// </summary>
        /// <returns></returns>
        public object LoadNurse()
        {
            return DAL.BasicInfo.CommonData.LoadNurse();
        }
        public G_DATA GetData(string type, string value)
        {
            return DAL.BasicInfo.CommonData.GetData(type, value);
        }
    }
}

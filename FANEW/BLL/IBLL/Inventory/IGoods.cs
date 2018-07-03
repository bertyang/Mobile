using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Anchor.FA.BLL.IBLL
{
    public interface IGoods
    {
        object LoadAllGoodsByPage(int page, int rows, string order, string sort, string id, string name, int? num,string text);

        //S_GOODS GetGoodsById(int? goodsId);
        //List<S_GOODS> GetGoodsByCategory(int category);  

        //bool Save(S_GOODS entity);
        //bool Delete(IList<int> idList);

        //bool SaveBatch(S_GOODS_BATCH entity);

        //bool DeleteBatch(int goodsId, string batchNo);

        //object LoadAllGoodsBatchByPage(int? page, int? rows, string order, string sort, int goodsId);

        //object LoadAllGoodsBatchAmountByPage(int? page, int? rows, string order, string sort, int goodsId,int houseId);

        //S_GOODS_BATCH GetGoodsBatchByID(int goodsId, string batchNo);

        //string GoodsPicUpload(HttpPostedFileBase upfile, string path);
    }
}

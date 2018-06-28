using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.BLL.IBLL
{
    /// <summary>
    /// 表单数据结构
    /// 吴鹏飞
    /// </summary>
    public class DataStructure
    {
        public string TableName { get; set; }//表单名称
        /// <summary>
        /// 表单数据结构
        /// 二维表的内容分别是主表各列的标签及列名，以及子表的标签及表结构（仍为DataStructure类型）
        /// </summary>
        public Dictionary<string, object> TableStructure { get; set; }//
    }

    public interface IDataStructure
    {
        DataStructure GetDataStructure();
    }
}

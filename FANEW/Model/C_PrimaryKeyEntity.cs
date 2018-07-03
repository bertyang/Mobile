using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Anchor.FA.Model
{
    /// <summary>
    /// 主键实体
    /// 主键取值不能包含‘,‘
    /// 吴鹏飞
    /// 2012-11-14
    /// </summary>
    public class PrimaryKeyEntity
    {
        private Dictionary<string, object> map = new Dictionary<string,object>();//保存解析后的主键
        public string TableName { get; set; }
        public PrimaryKeyEntity(string tableName)
        {
            TableName = tableName.ToLower();
        }

        public void AddKeyValue(string key, object value)
        {
            map.Add(key.ToLower(), value);
        }
        /// <summary>
        /// 获取字符形式的键值
        /// </summary>
        /// <param name="key">主键构成列的列名</param>
        /// <returns></returns>
        public object GetKeyValue(string key)
        {
            return map[key.ToLower()];
        }
    }
}

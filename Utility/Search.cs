using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

using Anchor.FA.Model;

namespace Anchor.FA.Utility
{
    public class Search
    {
        #region 贾明

        #region 获取医疗管理字典表中的相关数据

        /// <summary>
        /// 获取医疗管理字典表中的相关数据
        /// </summary>
        /// <param name="TypeCode">字典类型编码-对应TDictionaryType表</param>
        /// <returns>object</returns>
        public static object GetManagerDictioninary(string TypeCode)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = (from td in dbContext.TDictionary
                            where td.类型编码 == TypeCode && td.是否有效==true
                            orderby td.顺序号 ascending
                            select new
                            {
                                ID = td.编码,
                                Name = td.名称
                            }).ToList();
                return list;
            }
        }

        #endregion

        #region 获取现场地点类型字典表中的相关数据

        /// <summary>
        /// 获取现场地点类型字典表中的相关数据
        /// </summary>
        /// <param name="TypeCode">字典表表名</param>
        /// <returns>object</returns>
        public static object GetMainDictioninary(string TypeCode)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = (from t in TypeCode
                            select t).ToList();
                return list;
            }
        }

        #endregion

        #endregion
    }
}

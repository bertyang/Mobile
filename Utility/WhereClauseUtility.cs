using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Utility
{
    /// <summary>
    /// sql语句拼 where语句的 公用方法
    /// </summary>
    public class WhereClauseUtility
    {
        /// <summary>
        /// 增加 字符串类型等于的 where 语句
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="valueStr"></param>
        /// <param name="sbWhereClause"></param>
        public static void AddStringEqual(string columnName, string valueStr, StringBuilder sbWhereClause)
        {
            if ((valueStr != null) && (valueStr != "") && (valueStr != "-1") && valueStr != "--请选择--")
            {
                if (sbWhereClause.Length > 0)
                    sbWhereClause.Append(" AND ").Append(columnName).Append(" = '").Append(valueStr).Append("' ");
                else
                    sbWhereClause.Append(" WHERE ").Append(columnName).Append(" = '").Append(valueStr).Append("' ");
            }
        }
        //2011-10-28 贾明
        /// <summary>
        /// 增加 字符串类型等于的 where 语句(多选临时使用)
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="valueStr"></param>
        /// <param name="sbWhereClause"></param>
        public static void AddStringEqualTemp(string columnName, string valueStr, StringBuilder sbWhereClause)
        {
            if ((valueStr != null) && (valueStr != "") && (valueStr != "-1") && valueStr != "--请选择--" && valueStr != "()")
            {
                if (sbWhereClause.Length > 0)
                    sbWhereClause.Append(" AND (").Append(columnName).Append(" = ").Append(valueStr).Append(") ");
                else
                    sbWhereClause.Append(" WHERE ").Append(columnName).Append(" = ").Append(valueStr);
            }
        }

        //2011-10-28 贾明
        /// <summary>
        /// 增加 字符串类型年龄段查询 语句
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="valueStr"></param>
        /// <param name="sbWhereClause"></param>
        public static void AddStringEqualAge(string columnName, string valueStr, StringBuilder sbWhereClause)
        {
            if ((valueStr != null) && (valueStr != "") && (valueStr != "-1") && valueStr != "--请选择--" && valueStr != "()")
            {
                if (valueStr == "0" && sbWhereClause.Length > 0)
                {
                    sbWhereClause.Append(" AND  convert (int,tpr.岁数) BETWEEN 1 AND 365 ").Append(" and tpr.年龄 in ('个月','小时' ,'天') ");
                }
                else if (valueStr == "1" && sbWhereClause.Length > 0)
                {
                    sbWhereClause.Append(" and convert (int,tpr.岁数) BETWEEN 1 AND 10").Append(" and tpr.年龄='岁' ");
                }
                else if (valueStr == "2" && sbWhereClause.Length > 0)
                {
                    sbWhereClause.Append(" and convert (int,tpr.岁数) BETWEEN 11 AND 20").Append(" and tpr.年龄='岁' ");
                }
                else if (valueStr == "3" && sbWhereClause.Length > 0)
                {
                    sbWhereClause.Append(" and convert (int,tpr.岁数) BETWEEN 21 AND 30").Append(" and tpr.年龄='岁' ");
                }
                else if (valueStr == "4" && sbWhereClause.Length > 0)
                {
                    sbWhereClause.Append(" and convert (int,tpr.岁数) BETWEEN 31 AND 40").Append(" and tpr.年龄='岁' ");
                }
                else if (valueStr == "5" && sbWhereClause.Length > 0)
                {
                    sbWhereClause.Append(" and convert (int,tpr.岁数) BETWEEN 41 AND 50").Append(" and tpr.年龄='岁' ");
                }
                else if (valueStr == "6" && sbWhereClause.Length > 0)
                {
                    sbWhereClause.Append(" and convert (int,tpr.岁数) BETWEEN 51 AND 60").Append(" and tpr.年龄='岁' ");
                }
                else if (valueStr == "7" && sbWhereClause.Length > 0)
                {
                    sbWhereClause.Append(" and convert (int,tpr.岁数) BETWEEN 61 AND 70").Append(" and tpr.年龄='岁' ");
                }
                else if (valueStr == "8" && sbWhereClause.Length > 0)
                {
                    sbWhereClause.Append(" and convert (int,tpr.岁数) BETWEEN 71 AND 80").Append(" and tpr.年龄='岁' ");
                }
                else if (valueStr == "9" && sbWhereClause.Length > 0)
                {
                    sbWhereClause.Append(" and convert (int,tpr.岁数) BETWEEN 81 AND 90").Append(" and tpr.年龄='岁' ");
                }
                else if (valueStr == "10" && sbWhereClause.Length > 0)
                {
                    sbWhereClause.Append(" and convert (int,tpr.岁数) BETWEEN 91 AND 100").Append(" and tpr.年龄='岁' ");
                }
                else if (valueStr == "11" && sbWhereClause.Length > 0)
                {
                    sbWhereClause.Append(" and convert (int,tpr.岁数) BETWEEN 101 AND 110").Append(" and tpr.年龄='岁' ");
                }
                else if (valueStr == "12" && sbWhereClause.Length > 0)
                {
                    sbWhereClause.Append(" and tpr.年龄='老年' ");
                }
                else if (valueStr == "13" && sbWhereClause.Length > 0)
                {
                    sbWhereClause.Append(" and tpr.年龄='中年' ");
                }
                else if (valueStr == "14" && sbWhereClause.Length > 0)
                {
                    sbWhereClause.Append(" and tpr.年龄='青年' ");
                }
                else if (valueStr == "15" && sbWhereClause.Length > 0)
                {
                    sbWhereClause.Append(" and tpr.年龄='少儿' ");
                }
                else if (valueStr == "16" && sbWhereClause.Length > 0)
                {
                    sbWhereClause.Append(" and tpr.年龄='婴儿' ");
                }
                else if (valueStr == "17" && sbWhereClause.Length > 0)
                {
                    sbWhereClause.Append(" and tpr.年龄='不详' ");
                }
            }
        }
        //2011-10-28 贾明
        /// <summary>
        /// 增加 字符串类型in的 where 语句(多选临时使用)
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="valueStr"></param>
        /// <param name="sbWhereClause"></param>
        public static void AddStringEqualIn(string columnName, string valueStr, StringBuilder sbWhereClause)
        {
            if ((valueStr != null) && (valueStr != "") && (valueStr != "-1") && valueStr != "--请选择--" && valueStr != "()")
            {

                sbWhereClause.Append(" WHERE ").Append(columnName).Append(valueStr);
            }
        }
        /// <summary>
        /// 增加 字符串类型 相似 的 where 语句， 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="valueStr"></param>
        /// <param name="sbWhereClause"></param>
        public static void AddStringLike(string columnName, string valueStr, StringBuilder sbWhereClause)
        {
            if ((valueStr != null) && (valueStr != "") && (valueStr != "-1") && valueStr != "--请选择--")
            {
                if (sbWhereClause.Length > 0)
                    sbWhereClause.Append(" AND ").Append(columnName).Append(" LIKE '%").Append(valueStr).Append("%' ");
                else
                    sbWhereClause.Append(" WHERE ").Append(columnName).Append(" LIKE '%").Append(valueStr).Append("%' ");
            }
        }
        /// <summary>
        /// bool相等
        /// </summary>
        /// <param name="colunmName"></param>
        /// <param name="valueBool"></param>
        /// <param name="sbWhereClause"></param>
        public static void AddBoolEqual(string colunmName, bool valueBool, StringBuilder sbWhereClause)
        {
            if (sbWhereClause.Length > 0)
                sbWhereClause.Append(" AND ").Append(colunmName).Append(" = '").Append(valueBool).Append("' ");
            else
                sbWhereClause.Append(" WHERE ").Append(colunmName).Append(" = '").Append(valueBool).Append("' ");
        }

        /// <summary>
        /// 增加 字符串类型 相似 的 where or 语句 <--add tanhuan 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="valueStr"></param>
        /// <param name="sbWhereClause"></param>
        public static void AddStringLikeOr(string columnName1, string valueStr1, string columName2, string valueStr2, StringBuilder sbWhereClause)
        {
            if ((valueStr1 != null) && (valueStr1 != "") && !string.IsNullOrEmpty(valueStr2))
            {
                if (sbWhereClause.Length > 0)
                    sbWhereClause.Append(" AND ( ").Append(columnName1).Append(" LIKE '%").Append(valueStr1).Append("%' ").Append(" OR ").Append(columName2).Append(" LIKE '%").Append(valueStr2).Append("%' ").Append(") ");
                else
                    sbWhereClause.Append(" WHERE ( ").Append(columnName1).Append(" LIKE '%").Append(valueStr1).Append("%' ").Append(" OR ").Append(columName2).Append(" LIKE '%").Append(valueStr2).Append("%' ").Append(") ");
            }
        }

        /// <summary>
        /// 增加 时间类型大于等于的 where 语句
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="valueStr"></param>
        /// <param name="sbWhereClause"></param>
        public static void AddDateTimeGreaterThan(string columnName, DateTime valueStr, StringBuilder sbWhereClause)
        {
            if (sbWhereClause.Length > 0)
                sbWhereClause.Append(" AND ").Append(columnName).Append(" >= '").Append(valueStr.ToString()).Append("' ");
            else
                sbWhereClause.Append(" WHERE ").Append(columnName).Append(" >= '").Append(valueStr.ToString()).Append("' ");
        }

        /// <summary>
        /// 增加 时间类型小于等于的 where 语句
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="valueStr"></param>
        /// <param name="sbWhereClause"></param>
        public static void AddDateTimeLessThan(string columnName, DateTime valueStr, StringBuilder sbWhereClause)
        {
            if (sbWhereClause.Length > 0)
                sbWhereClause.Append(" AND ").Append(columnName).Append(" < '").Append(valueStr.ToString()).Append("' ");
            else
                sbWhereClause.Append(" WHERE ").Append(columnName).Append(" < '").Append(valueStr.ToString()).Append("' ");
        }

        /// <summary>
        /// 整型等于
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="valueStr"></param>
        /// <param name="sbWhereClause"></param>
        public static void AddIntEqual(string columnName, int valueStr, StringBuilder sbWhereClause)
        {
            if (valueStr >= 0)
            {
                if (sbWhereClause.Length > 0)
                    sbWhereClause.Append(" AND ").Append(columnName).Append(" = ").Append(valueStr.ToString()).Append(" ");
                else
                    sbWhereClause.Append(" WHERE ").Append(columnName).Append(" = ").Append(valueStr.ToString()).Append(" ");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="selectQuery"></param>
        /// <param name="sbWhereClause"></param>
        public static void AddNotInSelectQuery(string columnName, string selectQuery, StringBuilder sbWhereClause)
        {
            if (sbWhereClause.Length > 0)
                sbWhereClause.Append(" AND ").Append(columnName).Append(" NOT IN (").Append(selectQuery).Append(") ");
            else
                sbWhereClause.Append(" WHERE ").Append(columnName).Append(" NOT IN (").Append(selectQuery).Append(") ");
        }
        public static void AddInSelectQuery(string columnName, string selectQuery, StringBuilder sbWhereClause)
        {
            if ((selectQuery != null) && (selectQuery != ""))
            {
                if (sbWhereClause.Length > 0)
                    sbWhereClause.Append(" AND ").Append(columnName).Append(" IN (").Append(selectQuery).Append(") ");
                else
                    sbWhereClause.Append(" WHERE ").Append(columnName).Append(" IN (").Append(selectQuery).Append(") ");
            }
        }
    }
}

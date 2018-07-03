using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Anchor.FA.Utility
{
    public class PrimaryKeyUtility
    {
        public static string GetCoding(string tableName)
        {
          
            DataSet ds = SQLHelper.ExecuteDataSet(AppConfig.ConnectionStringDispatch, CommandType.Text, string.Format(" select 编码 from {0}", tableName), null);

            object result = SQLHelper.ExecuteScalar(AppConfig.ConnectionStringDispatch, CommandType.Text, string.Format("select isnull(min(编码),1) from {0}", tableName));

            int i = Convert.ToInt32(result);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (i == Convert.ToInt32(dr["编码"]))
                { i++; }
                else
                { break; }
            }

            return i.ToString("00000");
        }

        public static bool IsExistCode(string tableName, string code)
        {
            DataSet ds = SQLHelper.ExecuteDataSet(AppConfig.ConnectionStringDispatch,
                CommandType.Text,
                string.Format(" select * from {0} where 编码='{1}'", tableName, code), null);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static int GetID(string tableName)
        {
            DataSet ds = SQLHelper.ExecuteDataSet(AppConfig.ConnectionString, CommandType.Text, string.Format(" select ID from {0}", tableName), null);

            object result = SQLHelper.ExecuteScalar(AppConfig.ConnectionString, CommandType.Text, string.Format("select isnull(min(ID),1) from {0}", tableName));

            int i = Convert.ToInt32(result);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (i == Convert.ToInt32(dr["ID"]))
                { i++; }
                else
                { break; }
            }

            return i;
        }

        public static bool IsExistID(string tableName, int id)
        {
            DataSet ds = SQLHelper.ExecuteDataSet(AppConfig.ConnectionStringDispatch,
                CommandType.Text,
                string.Format(" select * from {0} where ID={1}", tableName, id), null);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

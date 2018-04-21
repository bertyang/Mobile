using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using Anchor.FA.Model;

namespace Anchor.FA.Utility
{
    public class PrimaryKeyCreater
    {
         private static object lockObj = new object();

        public PrimaryKeyCreater()
        {

        }


        /// <summary>
        /// 获取整形主键
        /// </summary>
        /// <param name="name">表名</param>
        /// <returns>int</returns>
        public static int getIntPrimaryKey(string name)
        {
            lock (lockObj)
            {
                int keyValue = getKeyValue(name);

                return keyValue;
            }
        }

        public static long getLongPrimaryKey(string name)
        {
            return getIntPrimaryKey(name);
        }

        /// <summary>
        /// 获取字符型主键
        /// yyyyMMddHHmmss+6位序列号
        /// </summary>
        /// <param name="name">表名</param>
        /// <returns>string</returns>
        public static string getStringPrimaryKey(string name)
        {
            DateTime now = DateTime.Now;
            string ntime = now.ToString("yyyyMMddHHmmss");


            lock (lockObj)
            {

                string s = getKeyValue(name).ToString();

                s = ntime + "000000".Substring(s.Length) + s;
                return s;
            }
        }

        private static int getKeyValue(string keyName)
        {
            List<SqlParameter> paramArray = new List<SqlParameter>();
            paramArray.Add(new SqlParameter("@keyName", keyName));
            paramArray.Add(new SqlParameter("@KeyAdditional", DateTime.Now.ToString("yyyyMMdd")));

            SqlParameter param = new SqlParameter("@KeyValue", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            paramArray.Add(param);

            using (MainDataContext dbContext = new MainDataContext())
            {
                SQLHelper.ExecuteNonQuery(AppConfig.ConnectionString, CommandType.StoredProcedure, "usp_KeyValue_GetMaxID", paramArray.ToArray());

                return (int)paramArray[2].Value;
            }
        }

    }
}

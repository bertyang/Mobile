using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;
using System.Data.Linq;
using System.Linq.Expressions;
using Anchor.FA.Utility;
using Anchor.FA.BLL.IBLL;
using Spring.Context;
using Spring.Context.Support;
using System.Data.SqlClient;
using System.Data;

namespace Anchor.FA.DAL.BasicInfo
{
    public class Export
    {
        /// <summary>
        /// 得到视图和函数的列表
        /// </summary>
        public static DataTable LoadViewAndFunction()
        {
            DataTable dt = SQLHelper.ExecuteDataSet(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringReport"].ConnectionString
                , CommandType.Text,
@"SELECT id,right(a.name,len(a.name)-7) as name,value 
,t=case when a.xtype in('V') then '视图'
else '函数' end
FROM sysobjects as a 
left join  sys.extended_properties as b on a.id=b.major_id AND b.minor_id = 0 AND b.name = 'MS_Description' 
WHERE  a.xtype in('IF','TF','V') and a.status >= 0 
--and convert(varchar,a.name) like @value 
--and left(a.name,1)='@'
and left(a.name,7)='REPORT_'"
                ).Tables[0];
            return dt;
        }

        public static DataSet GetDataSet(string sql,params SqlParameter[] sqlPar)
        {
            DataSet ds = SQLHelper.ExecuteDataSet(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringReport"].ConnectionString,
                CommandType.Text, sql, sqlPar);
            return ds;
        }

    }
}

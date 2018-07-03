using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Anchor.FA.Model;

namespace Anchor.FA.DAL.WorkFlow
{
    public class Column
    {
        public static object GetColomnValue(string sql, int formNo,string type)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {

                sql = string.Format(sql,formNo);

                   
                if (type.ToUpper() == "STRING")
                {
                    return dbContext.ExecuteQuery<string>(sql).SingleOrDefault();
                }
                else if (type.ToUpper() == "INT")
                {
                    return dbContext.ExecuteQuery<int>(sql).SingleOrDefault();
                }
                else if (type.ToUpper() == "DECIMAL")
                {
                    return dbContext.ExecuteQuery<Decimal>(sql).SingleOrDefault();
                }
                else if (type.ToUpper() == "DOUBLE")
                {
                    return dbContext.ExecuteQuery<Double>(sql).SingleOrDefault();
                }

                return ""; 
            }
        }
        
        //public static DataTable GetColumns(string tableName)
        //{
        //    using (MainDataContext dbContext = new MainDataContext())
        //    {
        //        return dbContext.ExecuteQuery<DataTable>("select name,name from simpleflowdata.dbo.syscolumns where id in (select id from simpleflowdata.dbo.sysobjects where name='{0}')", tableName);

        //    }

        //}


        //public static string GetColumnType(string tableName, string columnName)
        //{
        //    string sql = "select a.name from simpleflowdata.dbo.systypes a  "
        //    + "inner join simpleflowdata.dbo.syscolumns b on a.xtype=b.xtype and a.xusertype=b.xtype and b.name='{0}'  "
        //    + "inner join simpleflowdata.dbo.sysobjects c on c.id=b.id and c.name='{1}' ";

        //    sql = string.Format(sql, columnName, tableName);

        //    Object obj = SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, sql);

        //    string columnType = string.Empty;

        //    if (obj != null)
        //    {
        //        columnType = (string)obj;
        //    }

        //    return columnType;
        //}
    }
}

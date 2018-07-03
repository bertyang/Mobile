using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Anchor.FA.Utility
{
    public class SqlParameterTool
    {
        /// <summary>
        /// sql语句或存储过程
        /// </summary>
        public System.Text.StringBuilder commandText;
        /// <summary>
        /// sql参数
        /// </summary>
        public List<SqlParameter> commandParameters;
        /// <summary>
        /// sql参数 为了和EntityFramework兼容
        /// 其实大部分的参数都是String 
        /// </summary>
        public List<object> commandObj;
        /// <summary>
        /// sql参数 进行到第几个参数
        /// </summary>
        public int i;
        private int beginInt;
        /// <summary>
        /// sql参数 最开始的参数
        /// </summary>
        public int BeginInt
        {
            get
            {
                return beginInt;
            }
            set
            {
                i = value;
                beginInt = value;
            }
        }
        public SqlParameterTool()
        {
            commandText = new System.Text.StringBuilder();
            commandParameters = new List<SqlParameter>();
            commandObj=new List<object>();
            BeginInt = 0;
        }
        public void AddObject(string parameterName, object value)
        {
            commandParameters.Add(new System.Data.SqlClient.SqlParameter(parameterName, value));
        }
        /// <summary>
        /// 增加 相等查询条件
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="value"></param>
        public bool AddObjectEqual(string cmdText, object value)
        {
            string parameterName = "@p" + i.ToString();
            bool result = AddObjectEqual(cmdText, parameterName, value);
            if (result)
                i++;
            return result;
        }

        /// <summary>
        /// 增加 相等查询条件
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        public bool AddObjectEqual(string cmdText, string parameterName, object value)
        {
            if (value == null)
                return false;
            if (value.GetType() == typeof(string))
            {
                string strValue = value.ToString();
                if (string.IsNullOrEmpty(strValue) || strValue == "--请选择--")
                    return false;
            }
            commandText.Append(" and ");
            commandText.Append(cmdText);
            commandText.Append(" = ");
            commandText.Append(parameterName);
            commandParameters.Add(new System.Data.SqlClient.SqlParameter(parameterName, value));
            commandObj.Add(value);
            return true;
        }

        /// <summary>
        /// 增加 like查询条件
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="value"></param>
        public bool AddObjectLike(string cmdText, object value)
        {
            string parameterName = "@p" + i.ToString();
            bool result = AddObjectLike(cmdText, parameterName, value);
            if (result)
                i++;
            return result;
        }

        /// <summary>
        /// 增加 like查询条件
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        public bool AddObjectLike(string cmdText, string parameterName, object value)
        {
            if (value==null)
                return false;
            if (value.GetType() == typeof(string))
            {
                string strValue = value.ToString();
                if (string.IsNullOrEmpty(strValue) || strValue == "--请选择--")
                    return false;
            }
            commandText.Append(" and ");
            commandText.Append(cmdText);
            commandText.Append(" like ");
            commandText.Append(parameterName);
            commandParameters.Add(new System.Data.SqlClient.SqlParameter(parameterName, "%" + value.ToString() + "%"));
            commandObj.Add("%" + value.ToString() + "%");
            return true;
        }
    }
}

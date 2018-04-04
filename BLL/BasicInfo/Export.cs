using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Anchor.FA.Utility;
using System.Xml;
using Anchor.FA.Model;

namespace Anchor.FA.BLL.BasicInfo
{
    public class Export
    {
        public static DataTable LoadViewAndFunction()
        {
            return DAL.BasicInfo.Export.LoadViewAndFunction();
        }


        public static DataSet GetDataSet(string sql, params SqlParameter[] sqlPar)
        {
            return DAL.BasicInfo.Export.GetDataSet(sql, sqlPar);
        }

        public static void GetExportSql(string selectColumnsXml, string tableName, string functionCaseXml, string inputCaseXml, out SqlParameterTool show, out List<int> ColumnsNumber, out List<string> ColumnsName)
        {
            show = new SqlParameterTool();
            //------------------------------------------------select...
            show.commandText.Append(@"select");

            XmlDocument xmldocColumns = new XmlDocument();
            XmlNode xmltablebodyColumns = xmldocColumns.CreateElement("tablebody");
            xmltablebodyColumns.InnerXml = selectColumnsXml;
            xmldocColumns.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><root></root>");
            xmldocColumns.DocumentElement.AppendChild(xmltablebodyColumns);
            ColumnsNumber = new List<int>();//用来存储数字类型列 数字依次增加
            ColumnsName = new List<string>();//用来存储选择列名
            int i = 0;
            foreach (XmlNode xn in xmltablebodyColumns.ChildNodes)
            {
                show.commandText.AppendFormat(@"
[{0}],", xn.Attributes["Name"].Value);
                ColumnsName.Add(xn.Attributes["Name"].Value);
                switch (xn.Attributes["TypeName"].Value)
                {
                    case "decimal":
                    case "numeric":
                    case "int":
                    case "float":
                    case "money":
                        ColumnsNumber.Add(i);
                        break;
                }
                i++;
            }
            show.commandText.Remove(show.commandText.Length - 1, 1);
            //-------------------------------------------------------------from...
            show.commandText.AppendFormat(@"
from [REPORT_{0}]", tableName);
            if (!string.IsNullOrEmpty(functionCaseXml))
            {
                show.commandText.Append(@"(");
                XmlDocument xmldocfunctionCase = new XmlDocument();
                XmlNode xmltablebodyfunctionCase = xmldocfunctionCase.CreateElement("tablebody");
                xmltablebodyfunctionCase.InnerXml = functionCaseXml;
                xmldocfunctionCase.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><root></root>");
                xmldocfunctionCase.DocumentElement.AppendChild(xmltablebodyfunctionCase);

                foreach (XmlNode xn in xmltablebodyfunctionCase.ChildNodes)
                {
                    string parameterName = "@p" + show.BeginInt;
                    show.commandText.Append(parameterName).Append(",");
                    show.AddObject(parameterName, xn.Attributes["value"].Value);
                    show.BeginInt++;
                }
                show.commandText.Remove(show.commandText.Length - 1, 1);
                show.commandText.Append(@")");
            }

            //-------------------------------------------将此表名称转化为 a 方便 特殊条件下可能的关联
            show.commandText.Append(@" as a");
            //-------------------------------------------where...
            if (!string.IsNullOrEmpty(inputCaseXml))
            {
                show.commandText.Append(@"
where");
                XmlDocument xmldocinputCase = new XmlDocument();
                XmlNode xmltablebodyinputCase = xmldocinputCase.CreateElement("tablebody");
                xmltablebodyinputCase.InnerXml = inputCaseXml;
                xmldocinputCase.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><root></root>");
                xmldocinputCase.DocumentElement.AppendChild(xmltablebodyinputCase);
                SqlParameterTool sql1_p = tosql_Parameters(xmltablebodyinputCase, show.BeginInt);
                show.commandText.Append(sql1_p.commandText);
                show.commandParameters.AddRange(sql1_p.commandParameters);
            }
        }

        private static SqlParameterTool tosql_Parameters(XmlNode xmlNode, int i)
        {
            SqlParameterTool sql1_p = new SqlParameterTool();
            sql1_p.BeginInt = i;
            //int begini=i;
            if (xmlNode.ChildNodes.Count > 0)
            {
                //sql1_p.commandText.Append("(");
                foreach (XmlNode xn in xmlNode.ChildNodes)
                {

                    //if (xn.Attributes["radio2"].Value == "*")//sql中没有*，这里*代表特殊条件，在此条件下不能再加查询了
                    //{ 


                    //}
                    //else
                    {
                        string radio1 = xn.Attributes["radio1"].Value;
                        string columnName = xn.Attributes["columnName"].Value;
                        string radio2 = xn.Attributes["radio2"].Value;
                        string caseText = xn.Attributes["caseText"].Value;
                        if (radio2=="like")
                            caseText="%"+caseText+"%";

                        sql1_p.commandText.AppendFormat(" {0} ", radio1);
                        //if (begini == i)
                        //{
                        //    sql1_p.commandText.Append("(");//在第一个循环体的 and 或 or 后添加括号
                        //}
                        if (xn.HasChildNodes)
                        {
                            sql1_p.commandText.Append("(");//含有孩子结点添加括号
                        }
                        sql1_p.commandText.AppendFormat("[{0}]{1} @p{2}", columnName, radio2, i);
                        sql1_p.commandParameters.Add(new System.Data.SqlClient.SqlParameter("@p" + i.ToString(), caseText));
                        i++; sql1_p.BeginInt++;

                        if (xn.HasChildNodes)
                        {
                            SqlParameterTool sql2_p = tosql_Parameters(xn, i);

                            sql1_p.commandText.Append(sql2_p.commandText);
                            sql1_p.commandParameters.AddRange(sql2_p.commandParameters);
                            i = sql2_p.BeginInt; sql1_p.BeginInt = i;
                            sql1_p.commandText.Append(")");
                        }
                    }
                }
                //sql1_p.commandText.Append(")");
            }
            return sql1_p;
        }

        /// <summary>
        /// 从List树结构 转化为树结构
        /// </summary>
        public static List<C_ExportTree> ExportUrlListToTree(List<C_ExportUrlStringTree> inputCaseV)
        {
            if (inputCaseV == null)
                return null;
            var qureyRoots = inputCaseV.Where(t => t.ParentID == "-1");
            if (!qureyRoots.Any())
            {
                return null;
            }
            List<C_ExportUrlStringTree> Roots = qureyRoots.ToList();
            List<C_ExportTree> result = new List<C_ExportTree>();
            foreach(C_ExportUrlStringTree r in Roots)
            {
                result.Add(getTree(r, inputCaseV));

            }
            return result;
        }
        private static C_ExportTree getTree(C_ExportUrlStringTree me, List<C_ExportUrlStringTree> inputCaseV)
        {
            C_ExportTree re = new C_ExportTree();
            re.radio1 = me.radio1;
            re.columnname = me.columnname;
            re.radio2 = me.radio2;
            re.casetext = me.casetext;
            var qureyChildren = inputCaseV.Where(t => t.ParentID == me.ID);
            if (qureyChildren.Any())
            {
                List<C_ExportUrlStringTree> children = qureyChildren.ToList();
                re.children = new List<C_ExportTree>();
                foreach (C_ExportUrlStringTree c in children)
                {
                    re.children.Add(getTree(c,inputCaseV));
                }
            }
            return re;
        }
    }
}

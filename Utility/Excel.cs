using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Infragistics.Excel;
using System.Web;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace Anchor.FA.Utility
{
    public class Excel
    {

        /// <summary>
        /// 去掉输入字符串的两边空格和单引号
        /// </summary>
        public static string FormatString(string inputString)
        {
            if (inputString != null && inputString.Length > 0)
            {
                inputString = inputString.Trim().Replace("'", "");
                inputString = inputString.Replace("（", "(");
                inputString = inputString.Replace("）", ")");
            }

            return inputString;
        }

        /// <summary>
        /// 只能输入数字
        /// </summary>
        public static bool CheckNumber(string value)
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 检验是正整数
        /// </summary>
        public static bool CheckIsNumber(string value)
        {
            Regex regex = new Regex("^[0-9]*$");
            Match m = regex.Match(value);
            if (m.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckIsExceed(string value)
        {
            try
            {
                Convert.ToInt32(value);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 下载Excel文件
        /// </summary>
        public static void DownLoadExcel(HttpResponse response, string fileFullPath, string fileName)
        {
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            response.ContentType = "application/excel";
            response.WriteFile(fileFullPath);
            response.Flush();
            response.Close();
        }

        /// <summary>
        /// 将Excel转化为DataSet
        /// </summary>
        public static DataSet ExcelToDataSet(string fileFullPath)
        {
            DataSet dsExcel = new DataSet();
            DataTable dt = new DataTable();

            Workbook workBook = Workbook.Load(fileFullPath);
            Worksheet workSheet = workBook.Worksheets[0];

            int maxRowCount = Workbook.MaxExcelRowCount;
            int maxColumnCount = Workbook.MaxExcelColumnCount;

            if (workSheet != null)
            {
                //根据第一行创建DataTable Column
                WorksheetRow firstRow = workSheet.Rows[0];
                int columnCount = 0;
                for (int i = 0; i < maxColumnCount; i++)
                {
                    if (firstRow.Cells[i].Value != null)
                    {
                        dt.Columns.Add(Convert.ToString(firstRow.Cells[i].Value));
                        columnCount = columnCount + 1;
                    }
                    else
                    {
                        break;
                    }
                }

                //加载数据
                for (int j = 1; j < maxRowCount; j++)
                {
                    WorksheetRow dataRow = workSheet.Rows[j];
                    if (dataRow.Cells[0].Value == null && dataRow.Cells[columnCount - 1].Value == null)
                    {
                        break;
                    }

                    DataRow dr = dt.NewRow();
                    for (int k = 0; k < columnCount; k++)
                    {

                        dr[k] = dataRow.Cells[k].Value;
                    }

                    dt.Rows.Add(dr);
                }
            }

            dsExcel.Tables.Add(dt);
            return dsExcel;
        }

        /// <summary>
        /// 将数据导入到Excel
        /// </summary>
        public static void ExportExcel(string excelPath, IList<Hashtable> list, string sheetName, string[] strValue)
        {
            Workbook wb = new Workbook();
            wb.Worksheets.Add(sheetName);

            WindowOptions options = wb.WindowOptions;
            Worksheet ws = options.SelectedWorksheet;

            //设置标题的格式

            IWorksheetCellFormat cellFormatHead = wb.CreateNewWorksheetCellFormat();
            cellFormatHead.Alignment = HorizontalCellAlignment.Center;
            cellFormatHead.BottomBorderStyle = CellBorderLineStyle.Thin;
            cellFormatHead.LeftBorderStyle = CellBorderLineStyle.Thin;
            cellFormatHead.RightBorderStyle = CellBorderLineStyle.Thin;
            cellFormatHead.TopBorderStyle = CellBorderLineStyle.Thin;
            cellFormatHead.Font.Bold = ExcelDefaultableBoolean.True;
            cellFormatHead.Font.Name = "Calibri";
            cellFormatHead.Font.Height = 220;
            ws.Rows[0].CellFormat.SetFormatting(cellFormatHead);

            //设置数据行的格式
            IWorksheetCellFormat cellFormatContent = wb.CreateNewWorksheetCellFormat();
            cellFormatContent.Alignment = HorizontalCellAlignment.Left;
            cellFormatContent.BottomBorderStyle = CellBorderLineStyle.Thin;
            cellFormatContent.LeftBorderStyle = CellBorderLineStyle.Thin;
            cellFormatContent.RightBorderStyle = CellBorderLineStyle.Thin;
            cellFormatContent.TopBorderStyle = CellBorderLineStyle.Thin;
            cellFormatContent.Font.Name = "Calibri";
            cellFormatContent.Font.Height = 220;

            //添加标题行

            //Hashtable headHashTable = list[0];
            int i = 0;
            foreach (string headValue in strValue)
            {
                ws.Rows[0].Cells[i].Value = headValue;
                ws.Columns[i].Width = 5000;
                i++;
            }

            //添加数据行

            for (int k = 0; k < list.Count; k++)
            {
                Hashtable dataHashTable = list[k];
                int j = 0;
                foreach (string dataValue in strValue)
                {
                    ws.Rows[k + 1].Cells[j].Value = dataHashTable[dataValue];
                    ws.Rows[k + 1].Cells[j].CellFormat.SetFormatting(cellFormatContent);
                    j++;
                }
            }

            wb.Save(excelPath);
        }


        public static bool ExportToExcel(DataTable dtblSource, string fileName)
        {
            int intColumnCount = 0;
            string connectionForExcel = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=Excel 8.0;Persist Security Info=False";
            OleDbConnection connection = new OleDbConnection(connectionForExcel);
            connection.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = connection;
            string strsql = "";
            bool returnValue = false;

            ArrayList excelTableColumnName = new ArrayList();
            string datetype = string.Empty;

            if (dtblSource != null && dtblSource.Rows.Count >= 0)
            {
                strsql = "Create Table [Sheet1] (";
                intColumnCount = dtblSource.Columns.Count - 1;
                for (int i = 0; i < intColumnCount; i++)
                {
                    DataColumn dc = new DataColumn();
                    dc = dtblSource.Columns[i];
                    excelTableColumnName.Add(dc.ColumnName);
                    switch (dc.DataType.FullName)
                    {
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                            datetype = "Int";
                            break;
                        case "System.String":
                            datetype = "text";
                            break;
                        case "System.Double":
                            datetype = "double";
                            break;
                        case "System.Decimal":
                            datetype = "decimal";
                            break;
                        default:
                            datetype = "text";
                            break;
                    }

                    strsql += "[" + dc.ColumnName + "]  " + datetype + ",";
                }
                datetype = dtblSource.Columns[intColumnCount].DataType.FullName;
                switch (datetype)
                {
                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                        datetype = "int";
                        break;
                    case "System.String":
                        datetype = "text";
                        break;
                    case "System.Double":
                        datetype = "double";
                        break;
                    case "System.Decimal":
                        datetype = "decimal";
                        break;
                    default:
                        datetype = "text";
                        break;
                }
                excelTableColumnName.Add(dtblSource.Columns[intColumnCount].ColumnName);
                strsql += "[" + dtblSource.Columns[intColumnCount].ColumnName + "]  " + datetype + " )\n";
                try
                {
                    cmd.CommandText = strsql;
                    cmd.ExecuteNonQuery();
                }
                catch (System.Data.OleDb.OleDbException err)
                {
                    System.Diagnostics.Trace.WriteLine(err.Message + err.ErrorCode.ToString());
                    if (err.ErrorCode == -2147217900)
                    {
                        cmd.CommandText = "Drop Table ExcelTable";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = strsql;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            if (strsql.Length != 0)
            {
                foreach (DataRow row in dtblSource.Rows)
                {
                    strsql = "INSERT INTO [Sheet1](";
                    for (int i = 0; i < excelTableColumnName.Count - 1; i++)
                    {
                        strsql += "[" + excelTableColumnName[i].ToString() + "],";
                    }
                    strsql += "[" + excelTableColumnName[excelTableColumnName.Count - 1].ToString() + "] )\n";
                    strsql += "Values(\n";
                    for (int i = 0; i < excelTableColumnName.Count - 1; i++)
                    {
                        datetype = row[i].GetType().ToString();
                        switch (datetype)
                        {
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                                strsql += int.Parse(row[i].ToString()) + ",";
                                break;
                            case "System.String":
                                strsql += "'" + row[i].ToString().Replace("'", "''") + "',";
                                break;
                            case "System.Double":
                                strsql += double.Parse(row[i].ToString()) + ",";
                                break;
                            case "System.Decimal":
                                strsql += Decimal.Parse(row[i].ToString()) + ",";
                                break;
                            case "System.DBNull":
                                strsql += " null, ";
                                break;
                            case "System.DateTime":
                                strsql += "'" + ((DateTime)row[i]).ToString("yyyy/MM/dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "',";
                                break;
                            default:
                                strsql += "'" + row[i].ToString().Replace("'", "''") + "',";
                                break;
                        }
                    }
                    datetype = row[excelTableColumnName.Count - 1].GetType().ToString();
                    switch (datetype)
                    {
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                            strsql += int.Parse(row[excelTableColumnName.Count - 1].ToString()) + ") \n ";
                            break;
                        case "System.String":
                            strsql += "'" + row[excelTableColumnName.Count - 1].ToString().Replace("'", "''") + "') \n ";
                            break;
                        case "System.Double":
                            strsql += double.Parse(row[excelTableColumnName.Count - 1].ToString()) + ") \n ";
                            break;
                        case "System.Decimal":
                            strsql += Decimal.Parse(row[excelTableColumnName.Count - 1].ToString()) + ") \n ";
                            break;
                        case "System.DateTime":
                            strsql += "'" + ((DateTime)row[excelTableColumnName.Count - 1]).ToString("yyyy/MM/dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "')";
                            break;
                        case "System.DBNull":
                            strsql += " null) \n ";
                            break;
                        default:
                            strsql += "'" + row[excelTableColumnName.Count - 1].ToString().Replace("'", "''") + "') \n ";
                            break;
                    }

                    try
                    {
                        cmd.CommandText = strsql;
                        cmd.ExecuteNonQuery();
                        strsql = "";
                        returnValue = true;
                    }
                    catch (System.Exception err)
                    {
                        throw err;
                    }
                }
            }
            connection.Close();
            connection = null;
            cmd = null;
            return returnValue;
        }


        public static bool ExportToExcel(DataSet dsSource, string fileName)
        {
            int intColumnCount = 0;
            string connectionForExcel = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=Excel 8.0;Persist Security Info=False";
            OleDbConnection connection = new OleDbConnection(connectionForExcel);
            connection.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = connection;
            bool returnValue = false;

            foreach (DataTable dtblSource in dsSource.Tables)
            {
                ArrayList excelTableColumnName = new ArrayList();
                string datetype = string.Empty;
                string strsql = "";

                if (dtblSource != null && dtblSource.Rows.Count >= 0)
                {
                    strsql = string.Format("Create Table [{0}] (", dtblSource.TableName);
                    intColumnCount = dtblSource.Columns.Count - 1;
                    for (int i = 0; i < intColumnCount; i++)
                    {
                        DataColumn dc = new DataColumn();
                        dc = dtblSource.Columns[i];
                        excelTableColumnName.Add(dc.ColumnName);
                        switch (dc.DataType.FullName)
                        {
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                                datetype = "Int";
                                break;
                            case "System.String":
                                datetype = "text";
                                break;
                            case "System.Double":
                                datetype = "double";
                                break;
                            case "System.Decimal":
                                datetype = "decimal";
                                break;
                            default:
                                datetype = "text";
                                break;
                        }

                        strsql += "[" + dc.ColumnName + "]  " + datetype + ",";
                    }
                    datetype = dtblSource.Columns[intColumnCount].DataType.FullName;
                    switch (datetype)
                    {
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                            datetype = "int";
                            break;
                        case "System.String":
                            datetype = "text";
                            break;
                        case "System.Double":
                            datetype = "double";
                            break;
                        case "System.Decimal":
                            datetype = "decimal";
                            break;
                        default:
                            datetype = "text";
                            break;
                    }
                    excelTableColumnName.Add(dtblSource.Columns[intColumnCount].ColumnName);
                    strsql += "[" + dtblSource.Columns[intColumnCount].ColumnName + "]  " + datetype + " )\n";
                    try
                    {
                        cmd.CommandText = strsql;
                        cmd.ExecuteNonQuery();
                    }
                    catch (System.Data.OleDb.OleDbException err)
                    {
                        System.Diagnostics.Trace.WriteLine(err.Message + err.ErrorCode.ToString());
                        if (err.ErrorCode == -2147217900)
                        {
                            cmd.CommandText = "Drop Table ExcelTable";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                if (strsql.Length != 0)
                {
                    foreach (DataRow row in dtblSource.Rows)
                    {
                        strsql = string.Format("INSERT INTO [{0}](", dtblSource.TableName);
                        for (int i = 0; i < excelTableColumnName.Count - 1; i++)
                        {
                            strsql += "[" + excelTableColumnName[i].ToString() + "],";
                        }
                        strsql += "[" + excelTableColumnName[excelTableColumnName.Count - 1].ToString() + "] )\n";
                        strsql += "Values(\n";
                        for (int i = 0; i < excelTableColumnName.Count - 1; i++)
                        {
                            datetype = row[i].GetType().ToString();
                            switch (datetype)
                            {
                                case "System.Int16":
                                case "System.Int32":
                                case "System.Int64":
                                    strsql += int.Parse(row[i].ToString()) + ",";
                                    break;
                                case "System.String":
                                    strsql += "'" + row[i].ToString().Replace("'", "''") + "',";
                                    break;
                                case "System.Double":
                                    strsql += double.Parse(row[i].ToString()) + ",";
                                    break;
                                case "System.Decimal":
                                    strsql += Decimal.Parse(row[i].ToString()) + ",";
                                    break;
                                case "System.DBNull":
                                    strsql += " null, ";
                                    break;
                                case "System.DateTime":
                                    strsql += "'" + ((DateTime)row[i]).ToString("yyyy/MM/dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "',";
                                    break;
                                default:
                                    strsql += "'" + row[i].ToString().Replace("'", "''") + "',";
                                    break;
                            }
                        }
                        datetype = row[excelTableColumnName.Count - 1].GetType().ToString();
                        switch (datetype)
                        {
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                                strsql += int.Parse(row[excelTableColumnName.Count - 1].ToString()) + ") \n ";
                                break;
                            case "System.String":
                                strsql += "'" + row[excelTableColumnName.Count - 1].ToString().Replace("'", "''") + "') \n ";
                                break;
                            case "System.Double":
                                strsql += double.Parse(row[excelTableColumnName.Count - 1].ToString()) + ") \n ";
                                break;
                            case "System.Decimal":
                                strsql += Decimal.Parse(row[excelTableColumnName.Count - 1].ToString()) + ") \n ";
                                break;
                            case "System.DateTime":
                                strsql += "'" + ((DateTime)row[excelTableColumnName.Count - 1]).ToString("yyyy/MM/dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "')";
                                break;
                            case "System.DBNull":
                                strsql += " null) \n ";
                                break;
                            default:
                                strsql += "'" + row[excelTableColumnName.Count - 1].ToString().Replace("'", "''") + "') \n ";
                                break;
                        }

                        try
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                            strsql = "";
                            returnValue = true;
                        }
                        catch (System.Exception err)
                        {
                            throw err;
                        }
                    }
                }
            }

            connection.Close();
            connection = null;
            cmd = null;
            return returnValue;
        }

    }
}

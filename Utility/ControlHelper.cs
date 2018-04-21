﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;

namespace Anchor.FA.Utility
{
    /// <summary>
    /// 绑定编码，名称，(自定义string不是每个都用)
    /// </summary>
    public class DropDownListItemObject
    {
        public string Code { get; set; }//编码
        public string Name { get; set; }//名称
        public string Remark { get; set; }//自定义string

        public DropDownListItemObject(string code, string name, string remark)
        {
            Code = code;
            Name = name;
            Remark = remark;
        }
    }
    public class StationListItem
    {
        public int Code { get; set; }//编码
        public string Name { get; set; }//名称
       

        public StationListItem(int code, string name)
        {
            Code = code;
            Name = name;
        }
    }
    /// <summary>
    /// 选择数量对象
    /// </summary>
    public class SelectedCountObject
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

        public SelectedCountObject(string code, string name, int count)
        {
            Code = code;
            Name = name;
            Count = count;
        }
    }
    /// <summary>
    /// 用药规格名称剂量用法对象，add-谭欢-2010-11-30
    /// </summary>
    public class MedicaWayCount
    {
        public string m_Code { get; set; }
        public string m_Name { get; set; }
        public int m_Count { get; set; }
        public string m_Way { get; set; }
        public string m_Specifications { get; set; }
        public MedicaWayCount(string code, string name, int count, string way, string specifications)
        {
            m_Code = code;
            m_Name = name;
            m_Count = count;
            m_Way = way;
            m_Specifications = specifications;
        }
    }
    [Serializable]
    public class DrugUsage
    {
        public string m_Name { get; set; }
        public string m_Usage { get; set; }
        public DrugUsage(string name, string usage)
        {
            m_Name = name;
            m_Usage = usage;
        }
    }
    public class ControlHelper : IComparer
    {
        private const string CPleaseSelectText = "--请选择--";

        /// <summary>
        /// 设置行色
        /// </summary>
        /// <param name="e"></param>
        public static void SetRowBGColor(GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#DDEEFF'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");
        }

        /// <summary>
        /// 按钮样式
        /// </summary>
        /// <param name="e"></param>
        /// <param name="OverCssName"></param>
        /// <param name="OutCssName"></param>
        public static void SetButtonBG(Button e, string OverCssName, string OutCssName)
        {
            e.CssClass = OutCssName;
            e.Attributes.Add("onmouseover", "this.className = '" + OverCssName + "'");
            e.Attributes.Add("onmouseout", "this.className = '" + OutCssName + "'");
        }
        #region DropDownList
        /// <summary>
        ///  设置DropDownList的选项 绑定显示“名称”选择“编码”
        /// </summary>
        /// <param name="ddl">DropDownList</param>
        /// <param name="dt">绑定数据集</param>
        /// <param name="indexNum">value选项int类型</param>
        public static void SetDropDownList(DropDownList ddl, DataTable dt, int indexNum)
        {
            DataTable temp = new DataTable();
            temp = dt.Clone();
            temp.Rows.Add(new object[] { indexNum, CPleaseSelectText });
            foreach (DataRow dr in dt.Rows)
            {
                temp.Rows.Add(dr.ItemArray);
            }
            ddl.DataSource = temp;
            ddl.DataTextField = "名称";
            ddl.DataValueField = "编码";
            ddl.DataBind();
        }

        /// <summary>
        ///  设置DropDownList的选项 绑定显示“名称”选择“名称”  tanhuan 2010-11-18
        /// </summary>
        /// <param name="ddl">DropDownList</param>
        /// <param name="dt">绑定数据集</param>
        /// <param name="indexNum">value选项int类型</param>
        public static void SetDropDownListName(DropDownList ddl, DataTable dt, int indexNum)
        {
            DataTable temp = new DataTable();
            temp = dt.Clone();
            temp.Rows.Add(new object[] { indexNum, CPleaseSelectText });
            foreach (DataRow dr in dt.Rows)
            {
                temp.Rows.Add(dr.ItemArray);
            }
            ddl.DataSource = temp;
            ddl.DataTextField = "名称";
            ddl.DataValueField = "名称";
            ddl.DataBind();
        }

        /// <summary>
        ///  设置不带请选择的DropDownList的选项 绑定显示“名称”选择“名称”  tanhuan 2010-12-29
        /// </summary>
        /// <param name="ddl">DropDownList</param>
        /// <param name="dt">绑定数据集</param>
        /// <param name="indexNum">value选项int类型</param>
        public static void SetNotALLDropDownListName(DropDownList ddl, DataTable dt)
        {
            ddl.DataSource = dt.DefaultView;
            ddl.DataTextField = "名称";
            ddl.DataValueField = "名称";
            ddl.DataBind();
        }

        /// <summary>
        ///  设置DropDownList的选项  绑定显示“名称”选择“编码”
        /// </summary>
        /// <param name="ddl">DropDownList</param>
        /// <param name="dt">绑定数据集</param>
        /// <param name="indexNum">value选项string类型</param>
        public static void SetDropDownList(DropDownList ddl, DataTable dt, string indexStr)
        {
            DataTable temp = new DataTable();
            temp = dt.Clone();
            temp.Rows.Add(new object[] { indexStr, CPleaseSelectText });
            foreach (DataRow dr in dt.Rows)
            {
                temp.Rows.Add(dr.ItemArray);
            }
            ddl.DataSource = temp;
            ddl.DataTextField = "名称";
            ddl.DataValueField = "编码";
            ddl.DataBind();
        }

        /// <summary>
        /// 设置DropDownList的选项  绑定显示Name选择Code
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="items"></param>
        /// <param name="indexStr"></param>
        public static void SetDropDownList(DropDownList ddl, List<DropDownListItemObject> items, string indexStr)
        {
            List<DropDownListItemObject> newitems = new List<DropDownListItemObject>();
            newitems.Insert(0, new DropDownListItemObject(indexStr, CPleaseSelectText, ""));
            for (int i = 1; i <= items.Count; i++)
            {
                newitems.Insert(i, items[i - 1]);
            }
            ddl.DataSource = newitems;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Code";
            ddl.DataBind();
        }
        /// <summary>
        /// 设置DropDownList的选项  绑定显示Name选择Code
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="items"></param>
        /// <param name="indexStr"></param>
        public static void SetStationList(DropDownList ddl, List<StationListItem> items, int indexStr)
        {
            List<StationListItem> newitems = new List<StationListItem>();
            newitems.Insert(0, new StationListItem(indexStr, CPleaseSelectText));
            for (int i = 1; i <= items.Count; i++)
            {
                newitems.Insert(i, items[i - 1]);
            }
            ddl.DataSource = newitems;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Code";
            ddl.DataBind();
        }

        public static void SetDropDownListText(DropDownList ddl, List<DropDownListItemObject> items, string indexStr)
        {
            List<DropDownListItemObject> newitems = new List<DropDownListItemObject>();
            newitems.Insert(0, new DropDownListItemObject(indexStr, CPleaseSelectText, ""));
            for (int i = 1; i <= items.Count; i++)
            {
                newitems.Insert(i, items[i - 1]);
            }
            ddl.DataSource = newitems;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Name";
            ddl.DataBind();
        }

        public static void SetDropDownListWithText(DropDownList ddl, DataTable dt, string defaultText)
        {
            DataTable temp = new DataTable();
            temp = dt.Clone();
            temp.Rows.Add(new object[] { defaultText, CPleaseSelectText });
            foreach (DataRow dr in dt.Rows)
            {
                temp.Rows.Add(dr.ItemArray);
            }
            ddl.DataSource = temp;
            ddl.DataTextField = "名称";
            ddl.DataValueField = "名称";
            ddl.DataBind();
        }
        public static void SetDropDownListWithText(HtmlSelect ddl, DataTable dt, string defaultText)
        {
            DataTable temp = new DataTable();
            temp = dt.Clone();
            temp.Rows.Add(new object[] { defaultText, CPleaseSelectText });
            foreach (DataRow dr in dt.Rows)
            {
                temp.Rows.Add(dr.ItemArray);
            }
            ddl.DataSource = temp;
            ddl.DataTextField = "名称";
            ddl.DataValueField = "名称";
            ddl.DataBind();
        }

        /// <summary>
        /// 得到DropDownList选择文本，如果是请选择则赋值空
        /// </summary>
        /// <param name="ddl"></param>
        /// <returns></returns>
        public static string GetDropDownListSelectedText(DropDownList ddl)
        {
            string ret = "";
            if (ddl.SelectedIndex != -1)
            {
                if (ddl.SelectedItem.Text != CPleaseSelectText)
                    ret = ddl.SelectedItem.Text;
            }
            return ret;
        }
        /// <summary>
        /// 设置分站权限
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="code"></param>
        public static void SetPermissions(DropDownList ddl, string code)
        {
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(code));
            ddl.Enabled = false;
        }
        /// <summary>
        /// 编码验证，图片的显示
        /// </summary>
        /// <param name="exist">是否存在,参数是True，返回×，参数是False，返回√</param>
        /// <param name="img">图片控件</param>
        public static void ValidateImg(bool exist, HtmlImage img)
        {
            if (exist) img.Src = "../images/X.gif";//已经存在，显示不通过
            else img.Src = "../images/Y.gif";//不存在，显示通过
        }
        #endregion

        #region CheckBoxList
        public static void FillCheckBoxList(CheckBoxList cbl, DataTable dt)
        {
            cbl.RepeatColumns = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                cbl.Items.Add(dr["名称"].ToString());
            }
        }
        public static string GetCheckListSelectedText(CheckBoxList cbl)
        {
            List<string> retList = new List<string>();
            foreach (ListItem li in cbl.Items)
            {
                if (li.Selected)
                    retList.Add(li.Text);
            }

            string ret = "";
            if (retList.Count > 0)
                ret = retList.Aggregate((commaText, next) => commaText + "," + next);
            return ret;
        }

        public static void SetCheckBoxList(CheckBoxList cbl, string text)
        {
            if (text == null)
                return;

            string[] itemArray = text.Split(',');
            foreach (ListItem li in cbl.Items)
            {
                if (itemArray.Contains(li.Text))
                    li.Selected = true;
            }
        }

        #endregion

        #region ComboBox
        public static void FillComboBox(AjaxControlToolkit.ComboBox cbb, List<DropDownListItemObject> items)
        {
            cbb.DataSource = items;
            cbb.DataTextField = "Name";
            cbb.DataValueField = "Name";
            cbb.DataBind();
        }

        public static void FillComboBox(AjaxControlToolkit.ComboBox cbb, DataTable dt)
        {
            cbb.DataSource = dt;
            cbb.DataTextField = "名称";
            cbb.DataValueField = "名称";
            cbb.DataBind();
        }
        //public static void FillComboBox(AjaxControlToolkit.ComboBox cbb, DataTable dt)
        //{
        //    DataTable temp = new DataTable();
        //    temp = dt.Clone();
        //    temp.Rows.Add(new object[] { "", "" });
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        temp.Rows.Add(dr.ItemArray);
        //    }
        //    cbb.DataSource = temp;
        //    cbb.DataTextField = "名称";
        //    cbb.DataValueField = "名称";
        //    cbb.DataBind();
        //}
        public static void SetComboBoxText(AjaxControlToolkit.ComboBox cbb, string text)
        {
            ListItem li = cbb.Items.FindByText(text);
            if (li == null)
            {
                li = new ListItem(text, text);
                cbb.Items.Add(li);
            }
            cbb.Text = text;
        }

        #endregion

        #region RadioList
        public static string GetRadioListSelectedText(RadioButtonList rbl)
        {
            string ret = "";
            if (rbl.SelectedItem != null)
                ret = rbl.SelectedItem.Text;
            return ret;
        }

        public static void SetRadioList(RadioButtonList rbl, string text)
        {
            if (text == null)
                return;
            rbl.SelectedValue = text;
        }

        public static void FillRadioButtonList(RadioButtonList rbl, DataTable dt)
        {
            rbl.RepeatColumns = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                rbl.Items.Add(dr["名称"].ToString());
            }
        }
        #endregion


        #region IComparer 成员

        public int Compare(object x, object y)
        {
            throw new NotImplementedException();
            return 1;
        }

        #endregion

        #region 下载文件
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="Response"></param>
        /// <param name="filepath"></param>
        /// <param name="filename"></param>
        public static void DownloadFile(HttpResponse Response, string filepath, string filename)
        {
            System.IO.Stream iStream = null;

            // Buffer to read 10K bytes in chunk:
            byte[] buffer = new Byte[10000];

            // Length of the file:
            int length;

            // Total bytes to read:
            long dataToRead;

            // Identify the file to download including its path.
            //string filepath = "strFilePath";

            // Identify the file name.
            //string filename = System.IO.Path.GetFileName(filepath);
            //filename = HttpUtility.UrlEncode(filename);//加密
            try
            {

                // Open the file.
                iStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open,
                System.IO.FileAccess.Read, System.IO.FileShare.Read);


                // Total bytes to read:
                dataToRead = iStream.Length;
                //Response.Charset = "utf-8";
                Response.Charset = "utf-8";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.ContentType = "application/octet-stream";
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8));
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度
                Response.AddHeader("Content-Length", dataToRead.ToString());
                // Read the bytes.
                while (dataToRead > 0)
                {
                    // Verify that the client is connected.
                    if (Response.IsClientConnected)
                    {
                        // Read the data in buffer.
                        length = iStream.Read(buffer, 0, 10000);

                        // Write the data to the current output stream.
                        Response.OutputStream.Write(buffer, 0, length);

                        // Flush the data to the HTML output.
                        Response.Flush();

                        buffer = new Byte[10000];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        //prevent infinite loop if user disconnects
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                // Trap the error, if any.
                Response.Write("系统消息:服务器中的附件（" + filename + "）已被删除!");

            }
            finally
            {
                if (iStream != null)
                {
                    //Close the file.
                    iStream.Close();
                }
            }

        }
        #endregion

        #region 绑定DropDownList
        /// <summary>
        /// 数据集是DataTable，绑定名称、编码
        /// </summary>
        /// <param name="ddl">DropDownList控件</param>
        /// <param name="dt">DataTable数据集</param>
        public static void FillDropDownList(DropDownList ddl, DataTable dt)
        {
            DataTable temp = new DataTable();
            temp = dt.Clone();
            temp.Rows.Add(new object[] { -1, CPleaseSelectText });
            foreach (DataRow dr in dt.Rows)
            {
                temp.Rows.Add(dr.ItemArray);
            }
            ddl.DataSource = temp;
            ddl.DataTextField = "名称";
            ddl.DataValueField = "编码";
            ddl.DataBind();
        }
        /// <summary>
        /// 数据集是DataTable，绑定名称、名称
        /// </summary>
        /// <param name="ddl">DropDownList控件</param>
        /// <param name="dt">DataTable数据集</param>
        public static void FillDropDownListWithText(DropDownList ddl, DataTable dt)
        {
            DataTable temp = new DataTable();
            temp = dt.Clone();
            temp.Rows.Add(new object[] { -1, CPleaseSelectText });
            foreach (DataRow dr in dt.Rows)
            {
                temp.Rows.Add(dr.ItemArray);
            }
            ddl.DataSource = temp;
            ddl.DataTextField = "名称";
            ddl.DataValueField = "名称";
            ddl.DataBind();
        }
        /// <summary>
        /// 数据集是List，绑定Name、Code
        /// </summary>
        /// <param name="ddl">DropDownList控件</param>
        /// <param name="items">List数据集</param>
        public static void FillDropDownList(DropDownList ddl, List<DropDownListItemObject> items)
        {
            List<DropDownListItemObject> newitems = new List<DropDownListItemObject>();
            newitems.Insert(0, new DropDownListItemObject("-1", CPleaseSelectText, ""));
            for (int i = 1; i <= items.Count; i++)
            {
                newitems.Insert(i, items[i - 1]);
            }
            ddl.DataSource = newitems;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Code";
            ddl.DataBind();
        }
        /// <summary>
        /// 数据集是List，绑定Name、Name
        /// </summary>
        /// <param name="ddl">DropDownList控件</param>
        /// <param name="items">List数据集</param>
        public static void FillDropDownListWithText(DropDownList ddl, List<DropDownListItemObject> items)
        {
            List<DropDownListItemObject> newitems = new List<DropDownListItemObject>();
            newitems.Insert(0, new DropDownListItemObject("-1", CPleaseSelectText, ""));
            for (int i = 1; i <= items.Count; i++)
            {
                newitems.Insert(i, items[i - 1]);
            }
            ddl.DataSource = newitems;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Name";
            ddl.DataBind();
        }
        #endregion

        #region 设置RadioButtonList
        public static void SetRadioButtonListWithText(RadioButtonList rbl, string text)
        {
            if (string.IsNullOrEmpty(text))
                return;
            for (int i = 0; i < rbl.Items.Count; i++)
            {
                if (rbl.Items[i].Text == text)
                { rbl.Items[i].Selected = true; break; }
            }
        }
        public static void SetRadioButtonList(RadioButtonList rbl, string code)
        {
            if (string.IsNullOrEmpty(code))
                return;
            rbl.SelectedItem.Value = code;
        }
        #endregion

        /// <summary>
        /// 从下拉列表中选择值
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="defaultText"></param>
        public static void SelectDropDownListWithText(DropDownList ddl, string defaultText)
        {
            if (defaultText != "" && defaultText != "-1" && defaultText != CPleaseSelectText)
            {
                foreach (ListItem a in ddl.Items)
                {
                    if (a.Text == defaultText)
                    {
                        ddl.SelectedValue = defaultText;
                        break;
                    }
                }
            }
        }


        /// <summary>
        /// 得到DropDownList选择文本，如果是请选择则赋值空
        /// </summary>
        /// <param name="ddl"></param>
        /// <returns></returns>
        public static string GetDropDownListSelectedCode(DropDownList ddl)
        {
            string ret = "-1";
            if (ddl.SelectedIndex != -1)
            {
                if (ddl.SelectedItem.Text != CPleaseSelectText)
                    ret = ddl.SelectedItem.Value;
            }
            return ret;
        }

        /// <summary>
        /// 根据名称选择下拉选项
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="text"></param>
        public static void SetDropDownListWithText(DropDownList ddl, string text)
        {
            if (string.IsNullOrEmpty(text))
                return;
            foreach (ListItem li in ddl.Items)
            {
                if (text == li.Text)
                { li.Selected = true; break; }
            }
        }

        public static void SetDropDownList(DropDownList ddl, string code)
        {
            if (string.IsNullOrEmpty(code))
                return;
            foreach (ListItem li in ddl.Items)
            {
                if (code == li.Value)
                { li.Selected = true; break; }
            }
        }


    }
}

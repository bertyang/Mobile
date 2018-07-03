using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Anchor.FA.Model;
using Anchor.FA.BLL.BasicInfo;
using Microsoft.Reporting.WebForms;
using Anchor.FA.Utility;

namespace Anchor.FA.Mobile.Report
{
    public partial class NoCase_Mode : System.Web.UI.Page
    {
        /// <summary>
        /// 需显示的报表名称
        /// </summary>
        public string ReportName
        {
            get
            {
                object o = ViewState["ReportName"];
                return (o == null) ? String.Empty : (string)o;
            }
            set
            {
                ViewState["ReportName"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Request.QueryString["ReportName"] != null)
                //{
                //    ReportName = Request.QueryString["ReportName"];
                //    //lblTitle.Text = Request.QueryString["PopedomName"];
                //}

                ReportName = Request.QueryString["code"];
                InitPage();
            }
        }


        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            BindReportDataSource();
        }


        #region 报表绑定数据集
        /// <summary>
        /// 报表绑定数据集
        /// </summary>
        /// <param name="accCode"></param>
        protected void BindReportDataSource()
        {
            //int WorkerId = int.Parse(User.Identity.Name.Split('|')[0]);

            this.MyReportViewer.LocalReport.DataSources.Clear();
            this.MyReportViewer.LocalReport.ReportPath = @"Report\RDLC\" + ReportName;
            switch (ReportName)
            {
                case "PrintAttemper.rdlc"://
                    string AlarmCode = Request.QueryString["AlarmCode"];
                    string name0 = Request.QueryString["name0"];
                    string name1 = Request.QueryString["name1"];
                    string name2 = Request.QueryString["name2"];
                    string name3 = Request.QueryString["name3"];
                    string name4 = Request.QueryString["name4"];
                    string name5 = Request.QueryString["name5"];
                    string name6 = Request.QueryString["name6"];
                    string name7 = Request.QueryString["name7"];
                    Microsoft.Reporting.WebForms.ReportParameter Name0 = new Microsoft.Reporting.WebForms.ReportParameter("name0", name0);
                    Microsoft.Reporting.WebForms.ReportParameter Name1 = new Microsoft.Reporting.WebForms.ReportParameter("name1", name1);
                    Microsoft.Reporting.WebForms.ReportParameter Name2 = new Microsoft.Reporting.WebForms.ReportParameter("name2", name2);
                    Microsoft.Reporting.WebForms.ReportParameter Name3 = new Microsoft.Reporting.WebForms.ReportParameter("name3", name3);
                    Microsoft.Reporting.WebForms.ReportParameter Name4 = new Microsoft.Reporting.WebForms.ReportParameter("name4", name4);
                    Microsoft.Reporting.WebForms.ReportParameter Name5 = new Microsoft.Reporting.WebForms.ReportParameter("name5", name5);
                    Microsoft.Reporting.WebForms.ReportParameter Name6 = new Microsoft.Reporting.WebForms.ReportParameter("name6", name6);
                    Microsoft.Reporting.WebForms.ReportParameter Name7 = new Microsoft.Reporting.WebForms.ReportParameter("name7", name7);
                    Microsoft.Reporting.WebForms.ReportParameter Header = new Microsoft.Reporting.WebForms.ReportParameter("Header", AppConfig.GetStringConfigValue("PrtAttHeaderName"));
                    Microsoft.Reporting.WebForms.ReportParameter Unit = new Microsoft.Reporting.WebForms.ReportParameter("Unit", AppConfig.GetStringConfigValue("PrtAttUnitName"));
                    Microsoft.Reporting.WebForms.ReportParameter Footer = new Microsoft.Reporting.WebForms.ReportParameter("Footer", AppConfig.GetStringConfigValue("PrtAttFooterName"));
                    this.MyReportViewer.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { Name0, Name1, Name2, Name3, Name4, Name5, Name6, Name7, Header, Unit, Footer });
                    List<AttemperAlarmInfo> list1 = Anchor.FA.BLL.BasicInfo.StatisticsBLL.GetAlarmInfo(AlarmCode);
                    this.MyReportViewer.LocalReport.DataSources.Add(new ReportDataSource("AnchorV7_FirstAidManager_SE_StatisticsInfo_AttemperAlarmInfo", list1));
                    List<AttemperAcceptInfo> list2 = StatisticsBLL.GetAcceptInfo(AlarmCode);
                    this.MyReportViewer.LocalReport.DataSources.Add(new ReportDataSource("AnchorV7_FirstAidManager_SE_StatisticsInfo_AttemperAcceptInfo", list2));
                    List<AttemperTaskInfo> list3 = StatisticsBLL.GetTaskInfo(AlarmCode);
                    this.MyReportViewer.LocalReport.DataSources.Add(new ReportDataSource("AnchorV7_FirstAidManager_SE_StatisticsInfo_AttemperTaskInfo", list3));
                    List<AttemperTelInfo> list4 = StatisticsBLL.GetTelInfo(AlarmCode);
                    this.MyReportViewer.LocalReport.DataSources.Add(new ReportDataSource("AnchorV7_FirstAidManager_SE_StatisticsInfo_AttemperTelInfo", list4));
                    break;
                case "PrintCommand.rdlc"://
                    string TaskCode = Request.QueryString["TaskCode"];
                    List<StationCommandInfo> list = new List<StationCommandInfo>();
                    StationCommandInfo info = StatisticsBLL.PrintCommand(TaskCode);
                    list.Add(info);
                    this.MyReportViewer.LocalReport.DataSources.Add(new ReportDataSource("AnchorV7_FirstAidManager_SE_StatisticsInfo_StationCommandInfo", list));
                    break;
                default:
                    break;
            }
            this.MyReportViewer.LocalReport.Refresh();
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
namespace Anchor.FA.Web.API
{
    /// <summary>
    /// Service 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {

        [WebMethod(Description = "传递车辆任务信息")]
        public string GetAmbulance(string CNumber)
        {

            Anchor.FA.BLL.BasicInfo.API bll = new Anchor.FA.BLL.BasicInfo.API();

            DataSet ds = bll.GetModifyRecord(CNumber);
            string Almbulance = bll.ConvertDataSetToXML(ds);
            return Almbulance;
        }

        //[WebMethod(Description = "保存病例及救治措施记录")]
        //public bool SavePatientRecord(List<Anchor.FA.DAL.Medical.API.PatientRecordInfo> PatientRecordInfo, List<Anchor.FA.DAL.Medical.API.MeasureInfo> MeasureInfo)
        //{
        //    Anchor.FA.DAL.Medical.API dal = new Anchor.FA.DAL.Medical.API();

        //    return dal.SavePatientRecord(PatientRecordInfo, MeasureInfo);

        //}

        [WebMethod(Description = "更新密码")]
        public bool UpdatePassword(string empNo, string newPassword)
        {
            Anchor.FA.BLL.Organize.Worker worker = new Anchor.FA.BLL.Organize.Worker();

            return worker.UpdatePassword(empNo,newPassword);
        }
    }
}

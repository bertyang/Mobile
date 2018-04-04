using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;
using System.IO;
using System.Web;
using Anchor.FA.Utility;

namespace Anchor.FA.BLL.Office
{
    internal class Office
    {
        public object GetOfficeList(int page, int rows, string order, string sort, string type, DateTime startTime, DateTime endTime, string title, string writer, int loginID)
        {
            return DAL.Office.Office.GetOfficeList(page, rows, order, sort, type, startTime, endTime, title, writer, loginID);
        }
        public IList<TDictionary> GetAllInfoType()    
        {
            return DAL.Office.Office.GetAllInfoType();
        }
        public TOffice GetOffice(int? id)
        {
            return DAL.Office.Office.GetOffice(id);
        }

        public List<TOffice> GetOfficeByTypeAndLogin(string type,int loginID) 
        {
            return DAL.Office.Office.GetOfficeByTypeAndLogin(type, loginID);
        }

        public List<TOffice> GetOfficeByType(string type, int orgId)
        {
            return DAL.Office.Office.GetOfficeByType(type, orgId);
        }

        public List<TOfficeAttachment> GetFileByType(string type)
        {
            return DAL.Office.Office.GetFileByType(type); 
        }
        public bool Save(TOffice entity)
        {
            return DAL.Office.Office.Save(entity);
        }

        public static string UpdateRec(int BGCode, string userCode)
        {
            return Anchor.FA.DAL.Office.Office.UpdateRec(BGCode, userCode);
        }

        public static object GetRecs(int BGCode, string isRead)
        {
            return Anchor.FA.DAL.Office.Office.GetRecs(BGCode, isRead);
        }


        public bool Delete(IList<int> idList)
        {
            return DAL.Office.Office.Delete(idList);
        }
        public object GetFile(int officeId)
        {
            return DAL.Office.Office.GetFile(officeId);
        }
        public object GetReceiveInfo(int officeId)
        {
            return DAL.Office.Office.GetReceiveInfo(officeId);
        }
        public bool SaveFile(HttpPostedFileBase file, string path, int officeId)
        {
            return DAL.Office.Office.SaveFile(file, path,officeId);
        }
        public bool DeleteFile(int fileId, string path)
        {
            return DAL.Office.Office.DeleteFile(fileId, path);
        }
        public TOfficeAttachment DownLoadFile(int fileId)
        {
            return DAL.Office.Office.GetFileById(fileId);    
        }

    }
}

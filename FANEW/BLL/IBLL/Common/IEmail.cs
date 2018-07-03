using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;
using System.Web;

namespace Anchor.FA.BLL.IBLL
{
    public interface IEmail
    {
        //bool SaveFile(HttpPostedFileBase file, string path, int mailID);
        //E_Mail_Attachment DownLoadFile(int fileId);
        //object GetFile(int mailID);
        //bool ReadFlag(int mailID, int workerID);
        //bool DeleteFile(int fileId, string path);
        //bool SendMail(E_Mail entity, string fromID, string toID, string ccID, string isSend, bool remind);
        bool SendMail(string title, string content, string toID, bool remind);

        //object GetEmailList(int page, int rows, string order, string sort,int workerID, int folderID);
        //E_Mail GetMailInfo(int mailID);
        //List<E_Mail_Worker> GetMailWorker(int mailID, int type);
        //bool Delete(IList<int> idList, int workerID, int folderID);
        //bool DeleteMail(IList<int> idList, string path);

        //int GetEmailCount(int loginID, int folderID);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Spring.Context;
using Spring.Context.Support;
using System.Text.RegularExpressions;


using Anchor.FA.Model;
using Anchor.FA.Utility;
using Anchor.FA.BLL.IBLL;

namespace Anchor.FA.BLL.Email
{
    internal class Email : IEmail
    {
        public bool SaveFile(HttpPostedFileBase file, string path, int mailID)
        {
            return DAL.Email.Email.SaveFile(file,path,mailID);
        }

        public E_Mail_Attachment DownLoadFile(int fileId)
        {
            return DAL.Email.Email.DownLoadFile(fileId);
        }

        public object GetFile(int mailID)
        {
            return DAL.Email.Email.GetFile(mailID);
        }

        public bool DeleteFile(int fileId, string path)
        {
            return DAL.Email.Email.DeleteFile(fileId,path);
        }

        public bool SendMail(E_Mail entity, string fromID, string toID, string ccID, string isSend, bool remind)
        {
            //邮件相关人
            List<E_Mail_Worker> mailWorker = GetMailWorker(entity,fromID,toID,ccID,isSend);

            //发送短信
            if (isSend == "1" && remind == true){
                bool send;

                List<string> TelList = DAL.Email.Email.GetTelList(mailWorker);
                string Content = GetStringNoHtml(entity.Body);

                send = SendMsg(TelList, Content);

                if (send)
                {
                    return DAL.Email.Email.SendMail(entity, mailWorker);
                }
                else
                {
                    return false;
                }
            }
            
            return DAL.Email.Email.SendMail(entity,mailWorker);
        }

        public bool SendMail(string title, string content, string toID, bool remind)
        {
            E_Mail mail = DAL.Email.Email.CreateMail(title, content, toID);

            //List<string> toStr = new List<string>();
            //foreach (string r in toID.Split(',').ToList())
            //{
            //    toStr.Add("Worker-" + r);
            //}

            //string toID_ = String.Join(",", toStr);

            //邮件相关人
            List<E_Mail_Worker> mailWorker = GetMailWorker(mail, "0", toID, "", "1");

            //发送短信
            if (remind == true)
            {
                List<string> TelList = DAL.Email.Email.GetTelList(mailWorker);
                string Content = content;

                SendMsg(TelList, content);
            }

            return DAL.Email.Email.SendMail(mail, mailWorker);
        }

        /// <summary>
        /// 得到邮件相关人 
        /// </summary>
        /// <returns></returns>
        public List<E_Mail_Worker> GetMailWorker(E_Mail entity, string fromID, string toID, string ccID,string isSend) 
        {      

            List<E_Mail_Worker> mailWorker = new List<E_Mail_Worker>();

            E_Mail_Worker mailS = new E_Mail_Worker();

            //发件人(类型为0)
            mailS.ID = PrimaryKeyCreater.getIntPrimaryKey("E_Mail_Worker");
            mailS.MailId = entity.ID;
            mailS.WorkerId = int.Parse(fromID);
            mailS.Type = 0;
            mailS.ReadFlag = "1";  //默认已读
            if (isSend == "1")
            {
                mailS.FolderID = 3;   //已发送 
            }
            else
            {
                mailS.FolderID = 2;   //草稿箱
            }

            mailWorker.Add(mailS);

            //收件人(类型为1)
            foreach (string r in toID.Split(',').ToList())
            {
                E_Mail_Worker mailR = new E_Mail_Worker();
                mailR.ID = PrimaryKeyCreater.getIntPrimaryKey("E_Mail_Worker");
                mailR.MailId = entity.ID;
                mailR.WorkerId = int.Parse(r);
                mailR.Type = 1;
                mailR.ReadFlag = "0";

                if (isSend == "1")
                {
                    mailR.FolderID = 1;   //收件箱
                }
                else
                {
                    mailR.FolderID = 5;   //临时文件夹
                }
                mailWorker.Add(mailR);
            }

            //抄送人(类型为2)
            if (!string.IsNullOrEmpty(ccID))
            {
                foreach (string r in ccID.Split(',').ToList())
                {
                    E_Mail_Worker mailCC = new E_Mail_Worker();
                    mailCC.ID = PrimaryKeyCreater.getIntPrimaryKey("E_Mail_Worker");
                    mailCC.MailId = entity.ID;
                    mailCC.WorkerId = int.Parse(r);
                    mailCC.Type = 2;
                    mailCC.ReadFlag = "0";
                    if (isSend == "1")
                    {
                        mailCC.FolderID = 1;   //收件箱
                    }
                    else
                    {
                        mailCC.FolderID = 5;   //临时文件夹
                    }
                    mailWorker.Add(mailCC);
                }
            }
            return mailWorker;
        }

        public List<E_Mail_Worker> GetMailWorker(int mailID, int type)
        {
            return DAL.Email.Email.GetMailWorker( mailID, type);
        }

        public object GetEmailList(int page, int rows, string order, string sort,int loginID, int folderID)
        {
            return DAL.Email.Email.GetEmailList(page,rows,order,sort,loginID,folderID);
        }

        public int GetEmailCount(int loginID, int folderID)
        {
            return DAL.Email.Email.GetEmailCount(loginID,folderID);
        }

        public E_Mail GetMailInfo(int mailID)
        {
            return DAL.Email.Email.GetMailInfo(mailID);
        }

        public bool ReadFlag(int mailID, int workerID)
        {
            return DAL.Email.Email.ReadFlag(mailID, workerID);
        }

        public bool Delete(IList<int> idList, int workerID, int folderID)
        {
            return DAL.Email.Email.Delete(idList, workerID, folderID);
        }

        public bool DeleteMail(IList<int> idList, string path)
        {
            return DAL.Email.Email.DeleteMail(idList,path);
        }

        public bool SendMsg(List<string> TelList, string Content)
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            ISMS sms = ctx["SMS"] as ISMS;

            sms.SendSMG(TelList, Content, "99999");
            return true;
        }

        /// <summary>
        /// 将Html标签转化为空 
        /// </summary>
        /// <param name="strHtml">待转化的字符串</param>
        /// <returns>经过转化的字符串</returns>
        public string GetStringNoHtml(string strHtml)
        {
            if (String.IsNullOrEmpty(strHtml))
            {
                return strHtml;
            }
            else
            {
                string[] aryReg ={ 
                @"<script[^>]*?>.*?</script>", 
                @"<!--.*\n(-->)?", 
                @"<(\/\s*)?(.|\n)*?(\/\s*)?>", 
                @"<(\w|\s|""|'| |=|\\|\.|\/|#)*", 
                @"([\r\n|\s])*", 
                @"&(quot|#34);", 
                @"&(amp|#38);", 
                @"&(lt|#60);", 
                @"&(gt|#62);", 
                @"&(nbsp|#160);", 
                @"&(iexcl|#161);", 
                @"&(cent|#162);", 
                @"&(pound|#163);", 
                @"&(copy|#169);", 
                @"&#(\d+);"};

                string newReg = aryReg[0];
                string strOutput = strHtml.Replace("&nbsp;", " ");
                for (int i = 0; i < aryReg.Length; i++)
                {
                    Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                    strOutput = regex.Replace(strOutput, "");
                }
                strOutput.Replace("<", "&gt;");
                strOutput.Replace(">", "&lt;");
                return strOutput.Replace(" ", "&nbsp;");
            }
        }
    }
}

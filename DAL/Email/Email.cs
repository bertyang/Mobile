using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.IO;

using Spring.Context;
using Spring.Context.Support;
using Anchor.FA.Model;
using Anchor.FA.BLL.IBLL;
using Anchor.FA.Utility;


namespace Anchor.FA.DAL.Email
{
    public class Email
    {
        public static IApplicationContext ctx = ContextRegistry.GetContext();

        /// <summary>
        /// 附件上传
        /// </summary>
        /// <returns></returns>
        public static bool SaveFile(HttpPostedFileBase file, string path, int mailID) 
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                E_Mail_Attachment entity = new E_Mail_Attachment();

                var list = dbContext.E_Mail_Attachment.Select(t => t.ID);
                if (list.LongCount() > 0)
                {
                    entity.ID = list.Max() + 1;
                }
                else
                {
                    entity.ID = 1;
                }

                int lastIndex = file.FileName.LastIndexOf("\\");
                entity.OriginalName = file.FileName.Substring(lastIndex + 1, file.FileName.Length - lastIndex - 1); //原文件名
                entity.CodingName = System.Guid.NewGuid().ToString() + entity.OriginalName;   //编码附件名
                entity.Size = (float)(file.ContentLength * 1.0 / 1024);  //文件大小
                entity.MailID = mailID;

                string _path = path + entity.CodingName;

                file.SaveAs(_path);    //上传至服务器

                dbContext.E_Mail_Attachment.InsertOnSubmit(entity);
                dbContext.SubmitChanges();
                return true;
            }
        }

        /// <summary>
        /// 单个附件
        /// </summary>
        /// <param name="fileId">附件编号</param>
        /// <returns></returns>
        public static E_Mail_Attachment DownLoadFile(int fileId) 
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.E_Mail_Attachment.FirstOrDefault(m => m.ID == fileId);
            }
        }
        /// <summary>
        /// 获取附件
        /// </summary>
        /// <param name="mailID">邮件编号</param>
        /// <returns></returns>
        public static object GetFile(int mailID) 
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = dbContext.E_Mail_Attachment.Where(m => m.MailID == mailID);

                var list2 = list.ToList().Select(t => new
                {
                    编码 = t.ID,
                    原附件名 = t.OriginalName,
                    文件大小 = Convert.ToDouble(t.Size).ToString("0.00") + "kb",
                });

                return list2.ToList();

            }
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public static bool DeleteFile(int fileId, string path)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var entity = dbContext.E_Mail_Attachment.FirstOrDefault(t => t.ID == fileId);
                dbContext.E_Mail_Attachment.DeleteOnSubmit(entity);
                dbContext.SubmitChanges();

                File.Delete(path + entity.CodingName);

                return true;
            }
        }

        /// <summary>
        ///  发送&&存草稿
        /// </summary>
        /// <param name="entity">邮件实体</param>
        /// <param name="mailWorker">邮件相关人</param>
        /// <returns></returns>
        public static bool SendMail(E_Mail entity, List<E_Mail_Worker> mailWorker) 
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                try
                {
                    var email = dbContext.E_Mail.FirstOrDefault(m => m.ID == entity.ID);

                    if (email == null)   //新建信件
                    {
                        //邮件主表
                        dbContext.E_Mail.InsertOnSubmit(entity);
                    }
                    else   //从草稿箱进入
                    {
                        //邮件主表
                        email.Title = entity.Title;
                        email.SendDate = entity.SendDate;
                        email.From = entity.From;
                        email.Body = entity.Body;
                        email.CC = entity.CC;
                        email.CreateTime = entity.CreateTime;
                        email.SC = entity.SC;
                        email.To = entity.To;

                        var list = dbContext.E_Mail_Worker.Where(m => m.MailId == entity.ID).ToList();
                        foreach (E_Mail_Worker m in list)
                        {
                            dbContext.E_Mail_Worker.DeleteOnSubmit(m);
                        }
                    }


                    //存入数据库

                    foreach (E_Mail_Worker w in mailWorker)
                    {
                        dbContext.E_Mail_Worker.InsertOnSubmit(w);
                    }

                    dbContext.SubmitChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    Log4Net.LogError("SendMail", ex.ToString());
                    return false;
                }
            }
        }

        public static E_Mail CreateMail(string title, string content, string toID) 
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                E_Mail mail = new E_Mail(); 

                //邮件主表
                mail.ID = PrimaryKeyCreater.getIntPrimaryKey("E_Mail");
                mail.From = "系统";
                mail.Title = title;
                mail.Body = content;
                mail.CreateTime = DateTime.Now;
                mail.SendDate = DateTime.Now;

                List<string> toStr = new List<string>();
                foreach (string r in toID.Split(',').ToList())
                {
                    int id = int.Parse(r);  //人员ID
                    string name = dbContext.B_WORKER.FirstOrDefault(b => b.ID == id).Name;
                    toStr.Add(name);
                }
                mail.To = String.Join(",", toStr);

                return mail;
            } 
        }

        /// <summary>
        /// 获取邮件列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="loginID">当前登录人</param>
        /// <param name="folderID">邮件所在的目录文件编号</param>
        /// <returns></returns>
        public static object GetEmailList(int page, int rows, string order, string sort,int loginID,int folderID) 
        {
             using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from m in dbContext.E_Mail
                           join w in dbContext.E_Mail_Worker on m.ID equals w.MailId
                           where w.WorkerId == loginID && w.FolderID == folderID
                           select new
                           {
                               ID = m.ID,
                               sender = m.From,
                               receiver = m.To,
                               CC = m.CC,
                               SC = m.SC,
                               Content = m.Body, 
                               title = m.Title,
                               sendDate = m.SendDate,
                               readFlag = w.ReadFlag, 
                           };

                long total = list.LongCount();
                list = list.OrderByDescending(t => t.sendDate);
                list = list.Skip((page - 1) * rows).Take(rows);

                var listEmail = list.ToList().Select(o => new
                {
                    ID = o.ID,
                    sender = o.sender,
                    title = o.title,
                    receiver = o.receiver,
                    CC = o.CC,
                    SC = o.SC,
                    Content = o.Content,
                    sendDate = o.sendDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    readFlag = o.readFlag,
                });
                var result = new { total = total, rows = listEmail.ToList() };
                return result;
            }
        }

        /// <summary>
        /// 得到登录人员未读邮件数
        /// </summary>
        /// <param name="loginID"></param>
        /// <param name="folderID"></param>
        /// <returns></returns>
        public static int GetEmailCount(int loginID, int folderID)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = dbContext.E_Mail_Worker.Where(m => m.WorkerId == loginID && m.FolderID == folderID && m.ReadFlag == "0");

                int total = list.Count();

                return total;
            }
        }

        public static E_Mail GetMailInfo(int mailID)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.E_Mail.FirstOrDefault(m => m.ID == mailID);
            }      
        }

        public static List<E_Mail_Worker> GetMailWorker(int mailID,int type)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.E_Mail_Worker.Where(m => m.MailId == mailID && m.Type == type).ToList();
            } 
        }

        /// <summary>
        /// (收件箱)查看过的邮件改为已读
        /// </summary>
        /// <param name="mailID"></param>
        /// <returns></returns>
        public static bool ReadFlag(int mailID, int workerID)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                E_Mail_Worker w = dbContext.E_Mail_Worker.FirstOrDefault(m => m.MailId == mailID && m.WorkerId == workerID && m.FolderID == 1);
                w.ReadFlag = "1";
                dbContext.SubmitChanges();
                return true;
            } 
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="folderID"></param>
        /// <returns></returns>
        public static bool Delete(IList<int> idList, int workerID, int folderID)  
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                foreach (int g in idList)
                {
                    //将邮件移动到“已删除”

                    var mail_worker = dbContext.E_Mail_Worker.Where(w => w.MailId == g && w.WorkerId == workerID && w.FolderID == folderID);
                    foreach (E_Mail_Worker w in mail_worker)
                    {
                        w.FolderID = 4;
                    }
                }
                dbContext.SubmitChanges();
                return true;
            }

        }

        /// <summary>
        /// 彻底删除
        /// </summary>
        /// <returns></returns>
        public static bool DeleteMail(IList<int> idList, string path) 
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                foreach (int g in idList)
                {
                    //删除邮件
                    var mail = dbContext.E_Mail.Single(m => m.ID == g);
                    dbContext.E_Mail.DeleteOnSubmit(mail); 

                    //删除邮件相关人
                    var mail_worker = dbContext.E_Mail_Worker.Where(w => w.MailId == g);
                    foreach (E_Mail_Worker w in mail_worker)
                    {
                        dbContext.E_Mail_Worker.DeleteOnSubmit(w);
                    }

                    //删除附件
                    var att = dbContext.E_Mail_Attachment.Where(a => a.MailID == g);
                    foreach (E_Mail_Attachment a in att) 
                    {
                        dbContext.E_Mail_Attachment.DeleteOnSubmit(a);
                        File.Delete(path + a.CodingName);
                    }
                }
                dbContext.SubmitChanges();
                return true;
            }

        }

        /// <summary>
        /// 得到电话列表
        /// </summary>
        /// <param name="mailWorker"></param>
        /// <returns></returns>
        public static List<string> GetTelList(List<E_Mail_Worker> mailWorker)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {      
                List<string> TelList = new List<string>();

                mailWorker.Remove(mailWorker.Find(m => m.Type == 0)); //排除发信人
                foreach (E_Mail_Worker worker in mailWorker)
                {
                    string Mobile = dbContext.B_WORKER.FirstOrDefault(w => w.ID == worker.WorkerId).Mobile;
                    TelList.Add(Mobile);
                }
                return TelList;            
            } 
        }

    }
}

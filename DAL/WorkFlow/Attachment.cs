using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


using Anchor.FA.Model;
using Anchor.FA.Utility;
using System.IO;

namespace Anchor.FA.DAL.WorkFlow
{
    public class Attachment
    {
        public static bool Save(int flowId,int flowNo,HttpPostedFileBase file, string path)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                try
                {
                    F_INST_ATTACHMENT entity = new F_INST_ATTACHMENT();

                    var list = dbContext.F_INST_ATTACHMENT.Select(t => t.ID);
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
                    entity.FlowID=flowId;
                    entity.FlowNo = flowNo;

                    string _path = path + entity.CodingName;

                    file.SaveAs(_path);    //上传至服务器


                    dbContext.F_INST_ATTACHMENT.InsertOnSubmit(entity);
                    dbContext.SubmitChanges();

                    return true;

                }
                catch(Exception ex)
                {
                    Log4Net.LogError("Workflow Upload",ex.ToString());

                    return false;
                }
            }
        }


        //public static bool Update(List<int> listFileId,int flowInstId)
        //{
        //    using (MainDataContext dbContext = new MainDataContext())
        //    {
        //        try
        //        {
        //            foreach (int fileId in listFileId)
        //            {
        //                F_INST_ATTACHMENT file = dbContext.F_INST_ATTACHMENT.First(t => t.ID == fileId);

        //                file.FlowInstID = flowInstId;

        //             }

        //            dbContext.SubmitChanges();

        //            return true;
        //        }
        //        catch(Exception ex)
        //        {
        //            return false;
        //        }
        //    }
        //}

        /// <summary>
        /// 单个附件
        /// </summary>
        /// <param name="fileId">附件编号</param>
        /// <returns></returns>
        public static F_INST_ATTACHMENT DownLoad(int fileId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_INST_ATTACHMENT.FirstOrDefault(m => m.ID == fileId);
            }
        }


        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public static bool Delete(int fileId,string path)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var entity = dbContext.F_INST_ATTACHMENT.FirstOrDefault(t => t.ID == fileId);
                dbContext.F_INST_ATTACHMENT.DeleteOnSubmit(entity);
                dbContext.SubmitChanges();

                File.Delete(path + entity.CodingName);

                return true;
            }
        }


        public static List<F_INST_ATTACHMENT> GetAttacment(int flowId,int flowNo)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_INST_ATTACHMENT.Where(t => t.FlowID == flowId && t.FlowNo == flowNo).ToList();
            }
        }

    }
}

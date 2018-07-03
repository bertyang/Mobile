using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;

using System.IO;
using System.Configuration;

using Anchor.FA.Utility;
using System.Web;
using Spring.Context;
using Spring.Context.Support;
using Anchor.FA.BLL.IBLL;

namespace Anchor.FA.DAL.Office
{
    public class Office
    {
        public static IApplicationContext ctx = ContextRegistry.GetContext();

        public static object GetOfficeList(int page, int rows, string order, string sort, string type, DateTime startTime, DateTime endTime, string title, string writer, int loginID)
        {
            endTime=endTime.AddDays(1);

            SqlParameterTool sql = new SqlParameterTool();
            sql.commandText.Append(@"
select 
OfficeType=td.名称
,ID=tof.编码
,Title=tof.标题
,Writer=tof.作者
,CreateTime=tof.创建时间
,SendType=case tof.发送类型编码 when 1 then '全体人员' when 2 then '发送到部门' when 3 then '发送到个人' else '其它' end
,AnnexCount=(select count(*) from TOfficeAttachment tofa where tofa.办公编码=tof.编码)
,ReadCount=case when tof.发送类型编码=3 then (select count(*) from TOfficeReceive tofr where tofr.办公编码=tof.编码 and tofr.是否已阅=1) else null end
,IsSelf=case when tof.发送人编码=@p{0} then convert(bit,1) else convert(bit,0) end
from TOffice tof
left join TDictionary td on td.编码=tof.办公类型编码
");

            sql.commandText.Append("where tof.创建时间>=").Append("@p" + sql.i++);
            sql.commandText.Append(" and tof.创建时间<").Append("@p" + sql.i++);

            sql.commandObj.Add(startTime);
            sql.commandObj.Add(endTime);
            //if (type != "")
            if (!string.IsNullOrEmpty(type))
            {
                sql.AddObjectEqual("tof.办公类型编码", type);
            }

            sql.AddObjectLike("tof.作者", writer);
            sql.AddObjectLike("tof.标题", title);


            sql.commandText.Append(@"
and (tof.发送类型编码=1 
    or (tof.发送人编码=@p{0})
    or (tof.发送类型编码=2 and exists(select * from f_B_ORGANIZATION_cid(
        (select OrgID from B_WORKER_ORGANIZATION where WorkerID=@p{0})
        ) as b where ','+tof.接收部门编码+',' like '%,'+convert(varchar(10),b.id)+',%'))
    or (tof.发送类型编码=3 and ','+tof.接收人编码+',' like ('%,'+convert(varchar(10),@p{0})+',%'))
)
ORDER BY tof.创建时间 desc");

            string str = string.Format(sql.commandText.ToString(), sql.i++);
            sql.commandObj.Add(loginID);

            using (MainDataContext dbContext = new MainDataContext())
            {

                var templist1 = dbContext.ExecuteQuery<C_OfficeList>(
                    str
                    , sql.commandObj.ToArray());

                var templist2 = dbContext.ExecuteQuery<C_OfficeList>(
                    str
                    , sql.commandObj.ToArray());

                long total = templist1.LongCount();

                var result = new { total = total, rows = templist2.Skip((page - 1) * rows).Take(rows).ToList() };
                return result;

            }

            //using (MainDataContext dbContext = new MainDataContext())
            //{
            //    var list = from t in dbContext.TOffice
            //               join d in dbContext.TDictionary on t.办公类型编码 equals d.编码 into left_d
            //               from d in left_d.DefaultIfEmpty()
            //               where t.创建时间 >= startTime && t.创建时间 <= endTime
            //               select new {
            //                    办公类型编码 = t.办公类型编码,
            //                    办公类型 = d.名称,
            //                    编码 = t.编码,
            //                    标题 = t.标题,
            //                    作者 = t.作者,
            //                    创建时间 = t.创建时间,
            //                    发送类型 = t.发送类型编码 == 1 ? "全体人员" : t.发送类型编码 == 2 ? "发送到部门" : t.发送类型编码 == 3 ? "发送到个人" : "发送到个人",
            //                    接收人 = t.发送类型编码 == 1 ? t.接收人:t.接收部门, 
            //                    附件个数 = (from a in dbContext.TOfficeAttachment where t.编码 == a.办公编码 select a).LongCount(),
            //                    阅读人数 = (from b in dbContext.TOfficeReceive where b.是否已阅 == true select b).LongCount(),
            //               };
                //if (!string.IsNullOrEmpty(type) && type != "--请选择--")
                //{
                //    list = list.Where(t => t.办公类型编码 == type);
                //}
                //if (!string.IsNullOrEmpty(writer))
                //{
                //    list = list.Where(t => t.作者.Contains(writer));
                //}
                //if (!string.IsNullOrEmpty(title))
                //{
                //    list = list.Where(t => t.标题.Contains(title));
                //}

                //long total = list.LongCount();

                //list = list.OrderByDescending(t => t.创建时间);
                //list = list.Skip((page - 1) * rows).Take(rows);

                //var listOffice = list.ToList().Select(o => new   
                //{
                //    ID = o.编码,
                //    OfficeType = o.办公类型,
                //    Recipient = o.接收人,
                //    Title = o.标题,
                //    Writer = o.作者,
                //    CreateTime = o.创建时间.ToString("yyyy-MM-dd HH:mm:ss"),
                //    SendType = o.发送类型,
                //    AttachmentNum = o.附件个数, 
                //    ReadNum = o.阅读人数,
                //});
                //var result = new { total = total, rows = listOffice.ToList() };
                //return result;
            //}
        }

        public static IList<TDictionary> GetAllInfoType()
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.TDictionary.Where(t => t.类型编码 == "Office").ToList();
            }
        }

        public static TOffice GetOffice(int? officeId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                if (officeId != null) // 修改
                {
                    return dbContext.TOffice.FirstOrDefault(t => t.编码 == officeId); 
                }
                else   //新增
                {
                    var list = dbContext.TOffice.Select(t => t.编码);
                    TOffice entity = new TOffice();
                    entity.编码 = PrimaryKeyCreater.getIntPrimaryKey("TOffice");
                    entity.接收人 = string.Empty;
                    entity.办公类型编码 = string.Empty;
                    entity.标题 = string.Empty;
                    entity.作者 = string.Empty;
                    entity.内容 = string.Empty;
                    entity.创建时间 = DateTime.Now;

                    return entity;
                }   
            }
        }

        /// <summary>
        /// 办公列表
        /// </summary>
        /// <param name="type">办公类型</param>
        /// <returns></returns>
        public static List<TOffice> GetOfficeByTypeAndLogin(string type, int loginID) 
        {
            using (MainDataContext dbContext = new MainDataContext())
            {

                List<TOffice> list = new List<TOffice>(); 

                List<TOffice> office1= dbContext.TOffice.Where(t => t.办公类型编码 == type && t.发送类型编码 == 1).ToList();
                list.AddRange(office1);  //添加发送给全体人员的办公新闻


                IOrganization storehouse = ctx["Organize"] as IOrganization;
                List<B_ORGANIZATION> listUnit = storehouse.GetUnitByLoginer(loginID);

                //List<TOffice> office2 = dbContext.TOffice.Where(t => t.办公类型编码 == type && t.发送类型编码 == 2).ToList();
                var VarOffice2Li = dbContext.TOffice.Where(t => t.办公类型编码 == type && t.发送类型编码 == 2);

                if (listUnit != null)
                {
                    foreach (var cot in listUnit)
                    {
                        //string ScotID = cot.ID.ToString();
                        //var VarOffice2 = VarOffice2Li.Where(t => ("," + t.接收部门编码 + ",").Contains("," + ScotID + ","));
                        //list.Union(VarOffice2);
                        foreach (TOffice office in VarOffice2Li)
                        {
                            string[] s = office.接收部门编码.Split(',');

                            if (s.Contains(cot.ID.ToString()))
                            {
                                list.Add(office); //添加所属机构的办公新闻
                            }
                        }
                    }
                }
                string SloginID = loginID.ToString();
                List<TOffice> office3 = dbContext.TOffice.Where(t => t.办公类型编码 == type && t.发送类型编码 == 3 && ("," + t.接收人编码 + ",").Contains("," + SloginID + ",")).ToList();
                list.AddRange(office3);

                return list.OrderByDescending(t => t.创建时间).Take(10).ToList(); ;
   
            }
        }

        //看新闻当然看接收人了，怎么搞发送人了
        public static List<TOffice> GetOfficeByType(string type, int orgId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                string sql = string.Format(@"select TOP 10 * from TOffice a
                        join B_WORKER b on a.发送人编码 = b.ID
                        join B_WORKER_ORGANIZATION c on b.ID = c.WorkerID
                        where a.办公类型编码 = '{0}' and c.OrgID = {1}
                        order by 创建时间 ", type, orgId);

                return dbContext.ExecuteQuery<TOffice>(sql).ToList<TOffice>();                
            }
        }

        /// <summary>
        /// 附件列表
        /// </summary>
        /// <param name="type">办公类型</param>
        /// <returns></returns>
        public static List<TOfficeAttachment> GetFileByType(string type) 
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                List<TOfficeAttachment> att = new List<TOfficeAttachment>();

                List<TOffice> office = dbContext.TOffice.Where(t => t.办公类型编码 == type).OrderByDescending(t => t.创建时间).Take(4).ToList();
                foreach (TOffice t in office)
                {
                    List<TOfficeAttachment> list = dbContext.TOfficeAttachment.Where(o => o.办公编码 == t.编码).ToList();
                    foreach (TOfficeAttachment oa in list)
                    {
                        att.Add(oa);
                    }    
                }
                return att;
            }
        }

        public static bool Save(TOffice entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                try
                {
                    var model = dbContext.TOffice.FirstOrDefault(t => t.编码 == entity.编码);
                    if (model == null)  //添加
                    {
                        //添加到数据库
                        InsertReceiver(entity, dbContext);
                        dbContext.TOffice.InsertOnSubmit(entity);
                    }
                    else  //修改
                    {
                        
                        //再插入人员接收反馈表

                        #region 发送目的地信息
                        ////先删除发送目的地信息
                        //var lisS = from b in dbContext.TOfficeSend
                        //           where b.办公编码 == entity.编码
                        //           select b;
                        //foreach (var l in lisS)
                        //{
                        //    dbContext.TOfficeSend.Remove(l);
                        //}

                        ////再插入发送目的地表
                        //var squery = from am in dbContext.TOfficeSend
                        //    select am.编码;
                        //int scode = 0;
                        //if (squery.Any())
                        //    scode = squery.Max() + 1;
                        //else
                        //    scode = 1;
                        ////int scode = 0;//主键
                        //int j = 0;
                        //TOfficeSend s;
                        //switch (entity.发送类型编码)
                        //{
                        //    case 2:
                        //        string[] branchIDs = entity.接收部门编码.Split(',');
                        //        foreach (string branchID in branchIDs)
                        //        {
                        //            s = new TOfficeSend();
                        //            s.编码 = scode + j;
                        //            s.办公编码 = entity.编码;
                        //            s.发送类型编码 = 2;
                        //            s.目的地编码 = branchID;
                        //            dbContext.TOfficeSend.Add(s);
                        //            j++;
                        //        }
                        //        break;
                        //    case 3:
                        //        foreach (string personID in personIDs)
                        //        {
                        //            s = new TOfficeSend();
                        //            s.编码 = scode + j;
                        //            s.办公编码 = entity.编码;
                        //            s.发送类型编码 = 3;
                        //            s.目的地编码 = personID;
                        //            dbContext.TOfficeSend.Add(s);
                        //            j++;
                        //        }
                        //        break;
                        //    default:
                        //        break;
                        //} 
                        #endregion

                        #region 接收反馈信息
                        //先删除人员接收反馈信息
                        var lisR = from b in dbContext.TOfficeReceive
                                   where b.办公编码 == entity.编码
                                   select b;
                        foreach (var l in lisR)
                        {
                            dbContext.TOfficeReceive.DeleteOnSubmit(l);
                        }

                        InsertReceiver(entity, dbContext);
                        #endregion

                        ReflectionUtility.CopyObjectProperty<TOffice>(entity, model);

                    }

                    dbContext.SubmitChanges();

                    return true;
                }
                catch(Exception ex)
                {
                    Log4Net.LogError("Office Save", ex.ToString());

                    return false;
                }
            }      
        }

        private static void InsertReceiver(TOffice entity, MainDataContext dbContext)
        {
            //再插入人员接收反馈表
            if (entity.发送类型编码 == 3)
            {

                IEnumerable<string> personIDs = entity.接收人编码.Split(',').Distinct();
                IEnumerable<string> personNames = entity.接收人.Split(',').Distinct();
                //IEnumerable<string> personIDs = entity.接收人编码.Split(',');
                //IEnumerable<string> personNames = entity.接收人.Split(',');

                entity.接收人编码 = string.Join(",", personIDs);
                entity.接收人 = string.Join(",", personNames);


                var query1 = from am in dbContext.TOfficeReceive
                             select am.编码;
                int code = 0;
                if (query1.Any())
                    code = query1.Max() + 1;
                else
                    code = 1;

                TOfficeReceive or;
                List<string> pid = personIDs.ToList<string>();
                for (int i = 0; i < pid.Count(); i++)
                {
                    or = new TOfficeReceive();
                    or.编码 = code + i;
                    or.办公编码 = entity.编码;
                    or.查阅时间 = null;
                    or.接收人编码 = pid[i];
                    or.是否已阅 = false;
                    dbContext.TOfficeReceive.InsertOnSubmit(or);
                }
            }
        }


        /// <summary>
        /// 更新反馈
        /// </summary>
        public static string UpdateRec(int BGCode, string userCode)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                string result = null;
                try
                {
                    var oldQuery = from t in dbContext.TOfficeReceive
                                   where t.办公编码 == BGCode && t.接收人编码 == userCode
                                   select t;
                    if (oldQuery.Any())
                    {
                        var old = oldQuery.First();
                        if (!old.是否已阅)
                        {
                            old.是否已阅 = true;
                            old.查阅时间 = DateTime.Now;
                            dbContext.SubmitChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log4Net.LogError("Office UpdateRec", ex.ToString());
                    result = ex.ToString();
                }
                return result;

            }
        }


        /// <summary>
        /// 查询接收人反馈信息
        /// </summary>
        public static object GetRecs(int BGCode, string isRead)
        {

            SqlParameterTool sql = new SqlParameterTool();
            sql.commandText.Append(string.Format(@"
select t.编码
,t.是否已阅
,t.查阅时间
,接收人=bw.Name
from TOfficeReceive t
left join B_WORKER bw on bw.ID=t.接收人编码
where t.办公编码=@p{0}", sql.i++));
            sql.commandObj.Add(BGCode);

            sql.AddObjectEqual("t.是否已阅", isRead);
            sql.commandText.Append(@"
ORDER BY t.查阅时间");
            using (MainDataContext dbContext = new MainDataContext())
            {

                var templist1 = dbContext.ExecuteQuery<C_OfficeReceive>(
                    sql.commandText.ToString()
                    , sql.commandObj.ToArray());
                var templist2 = dbContext.ExecuteQuery<C_OfficeReceive>(
                    sql.commandText.ToString()
                    , sql.commandObj.ToArray());

                long total = templist1.LongCount();
                var result = new { total = total, rows = templist2.ToList() };
                return result;
            
            }
        }

        public static bool Delete(IList<int> idList)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                foreach (int g in idList)
                {
                    //删除办公信息
                    var office = dbContext.TOffice.Single(t => t.编码 == g);
                    dbContext.TOffice.DeleteOnSubmit(office);

                    //删除附件
                    var att = dbContext.TOfficeAttachment.Where(t => t.办公编码 == g);
                    foreach (TOfficeAttachment t in att)
                    {
                        dbContext.TOfficeAttachment.DeleteOnSubmit(t);
                        File.Delete(t.附件路径);
                    }

                    //删除接收反馈信息
                    var rec = dbContext.TOfficeReceive.Where(t => t.办公编码 == g);
                    foreach (var l in rec)
                    {
                        dbContext.TOfficeReceive.DeleteOnSubmit(l);
                    }

                    ////删除发送目的地信息
                    //var send = dbContext.TOfficeSend.Where(t => t.办公编码 == g);
                    //dbContext.TOfficeSend.Remove(send);
                }
                dbContext.SubmitChanges();
                return true;
            }

        }

        public static object GetFile(int officeId) 
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = dbContext.TOfficeAttachment.Where(o => o.办公编码 == officeId);

                var list2 = list.ToList().Select(t => new
                            {
                                编码 = t.编码,
                                原附件名 = t.原附件名,
                                文件大小 = Convert.ToDouble(t.文件大小).ToString("0.00") + "kb",
                            });

                return list2.ToList();
                
            }
        }

        public static object GetReceiveInfo(int officeId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from r in dbContext.TOfficeReceive
                           join o in dbContext.TOffice on r.接收人编码 equals o.接收人编码
                           where r.办公编码 == officeId
                           select new
                           {
                               接收人 = o.接收人,
                               查阅时间 = r.查阅时间,
                           };
                var listReceive = list.ToList().Select(t => new
                {
                    接收人 = t.接收人,
                    查阅时间 = t.查阅时间.ToString(),
                });

                var result = new { rows = listReceive.ToList() };

                return result;
            }
        }

        public static bool SaveFile(HttpPostedFileBase file, string path, int officeId) 
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                TOfficeAttachment entity = new TOfficeAttachment();
                var list = dbContext.TOfficeAttachment.Select(t => t.编码);
                if (list.LongCount() > 0)
                {
                    entity.编码 = list.Max() + 1;
                } 
                else
                {
                    entity.编码 = 1;
                }

                int lastIndex = file.FileName.LastIndexOf("\\");
                entity.原附件名 = file.FileName.Substring(lastIndex + 1, file.FileName.Length - lastIndex - 1); //文件名
                entity.编码附件名 = System.Guid.NewGuid().ToString() + entity.原附件名;   //编码附件名
                entity.文件大小 = (float)(file.ContentLength * 1.0 / 1024);  //文件大小
                entity.附件路径 = path + entity.编码附件名;
                entity.办公编码 = officeId;

                file.SaveAs(entity.附件路径);    //上传至服务器


                dbContext.TOfficeAttachment.InsertOnSubmit(entity);
                dbContext.SubmitChanges();

                return true;
            }
        }

        public static bool DeleteFile(int fileId, string path)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var entity = dbContext.TOfficeAttachment.FirstOrDefault(t => t.编码 == fileId);
                //dbContext.TOfficeAttachment.Load();
                dbContext.TOfficeAttachment.DeleteOnSubmit(entity);
                dbContext.SubmitChanges();

                File.Delete(path + entity.编码附件名);
                return true;
            }
        }

        public static TOfficeAttachment GetFileById(int fileId) 
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.TOfficeAttachment.FirstOrDefault(a => a.编码 == fileId);
            }
        }

    }
}

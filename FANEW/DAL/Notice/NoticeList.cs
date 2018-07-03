using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data;


using Anchor.FA.Model;
using Anchor.FA.Utility;

namespace Anchor.FA.DAL.Notice
{
   public class NoticeList
    {
       public static List<B_ACTION> GetAllUnit(int? exceptUnitId)
       {
           using (MainDataContext dbContext = new MainDataContext())
           {
               return dbContext.B_ACTION.Where(t => t.ParentID == exceptUnitId).ToList();
           }
       }
       public static IList<TZNoticeType> getNoticeType()
       {
           using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
           {
               return dbContext.TZNoticeType.Where(t => t.编码 != 3).ToList();

           }
       }
       //public static IList<TStation> GetStation()
       //{
       //    using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
       //    {
       //        return dbContext.TStation.ToList();

       //    }
       //}

       public static object LoadRemindByPage(int page, int rows, string order,string sort)
       {
           using (MainDataContext dbContext = new MainDataContext())
           {
               var list = from s in dbContext.B_Remind
                          join w in dbContext.B_WORKER_ROLE on s.操作员编码 equals w.EmpNo
                          join b in dbContext.B_WORKER on w.WorkerID equals b.ID
                          select new
                          {
                            ID = s.编码,
                            content = s.内容,
                            time  = s.提醒时间,
                            telList = s.发送对象,
                            isSend = s.是否发送,
                            operatorName = b.Name,
                           };
               long total = list.LongCount();
               list = list.OrderBy(s => s.ID);
               list = list.Skip((page - 1) * rows).Take(rows);

               var list2 = list.ToList().Select(s => new
               {
                   ID = s.ID,        
                   content = s.content,
                   time = s.time.ToString("yyyy-MM-dd HH:mm:ss"),
                   telList = s.telList,
                   isSend = s.isSend,
                   operatorName = s.operatorName,
               });

               return new { total = total, rows = list2.ToList() };
           }
       }
       public static bool Save(B_Remind entity)
       {
           using (MainDataContext dbContext = new MainDataContext())
           {
               try
               {
                   if (entity.编码 == 0)  //添加
                   {
                       var list = from p in dbContext.B_Remind select p.编码;
                       long total = list.LongCount();
                       if (total == 0)
                       {
                           entity.编码 = 1;
                       }
                       else
                       {
                           entity.编码 = dbContext.B_Remind.Max(t => t.编码) + 1;
                       }

                       dbContext.B_Remind.InsertOnSubmit(entity);
                       dbContext.SubmitChanges();
                       return true;

                   }
                   else  //修改
                   {
                       var model = dbContext.B_Remind.FirstOrDefault(t => t.编码 == entity.编码);
                       model.编码 = entity.编码;
                       model.发送对象 = entity.发送对象;
                       model.内容 = entity.内容;
                       model.提醒时间 = entity.提醒时间;
                       dbContext.SubmitChanges();
                       return true;
                   }
               }
               catch(Exception ex)
               {
                   Log4Net.LogError("定时短信", ex.ToString());

                   return false;
               }
           }
       }
       public static object Edit(int? id)
       {
           using (MainDataContext dbContext = new MainDataContext())
           {
               B_Remind entity = null;
               if (id != null)
               {
                   entity = dbContext.B_Remind.FirstOrDefault(t => t.编码 == id);
               }
               entity = entity ?? new B_Remind
               {
                   编码 = 0,
                   内容 = string.Empty,
                   发送对象 = string.Empty,
                   提醒时间 = DateTime.Now,
                   操作员编码 = string.Empty,
               };
               return entity;
           }
       }
       public static bool Delete(IList<int> idList)
       {
           using (MainDataContext dbContext = new MainDataContext())
           {
               try
               {
                   foreach (int g in idList)
                   {
                       B_Remind model = dbContext.B_Remind.SingleOrDefault(t => t.编码 == g);
                       if (model != null)
                       {
                           dbContext.B_Remind.DeleteOnSubmit(model);
                       }
                   }
                   dbContext.SubmitChanges();
                   return true;
               }
               catch (Exception ex)
               {
                   Log4Net.LogError("定时短信删除", ex.ToString());

                   return false;
               }
           }

       }
       public static IList<B_WORKER> GetAllTel()
       {
           using (MainDataContext dbContext = new MainDataContext())
           {
               return dbContext.B_WORKER.ToList();
           }
       }
       public static string GetTelByWorkerID(int id) 
       {
           using (MainDataContext dbContext = new MainDataContext())
           {
               B_WORKER entity = dbContext.B_WORKER.FirstOrDefault(t => t.ID == id);

               string tel = entity.Mobile;
               return tel;
           }
       }

       public static string GetEmpNoByWorkerID(int id) 
       {
           using (MainDataContext dbContext = new MainDataContext())
           {
               B_WORKER_ROLE entity = dbContext.B_WORKER_ROLE.FirstOrDefault(t => t.WorkerID == id && t.EmpNo!="");

               if (entity == null)
               { 
                    return string.Empty;
               }
               else
               {
                    return entity.EmpNo;
               }
           }
       }

       public static List<TAmbulance> GetAmbulanceByStationID(string id,Anchor.FA.Utility.ButtonPower bp, C_WorkerDetail UserInfo)  
       {
           using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
           {
               if (string.IsNullOrEmpty(id) || id == "--请选择--")
               {
                   switch (bp.GetGroupRangePower("searchBound"))
                   {
                       case "SearchAll":
                           return dbContext.TAmbulance.Where(t => t.是否有效 == true).ToList();
                       case "SearchCenter"://查找所属分中心
                           return (from p in dbContext.TAmbulance
                                   join s in dbContext.TStation on p.分站编码 equals s.编码
                                   where p.是否有效 == true && s.中心编码 == UserInfo.CenterCode
                                   select p).ToList();
                       case "SearchOrganization"://查找分站
                           return dbContext.TAmbulance.Where(t => t.是否有效 == true && UserInfo.Sta.Contains(t.分站编码)).ToList();
                       default://没有设置查询权限
                           return null;
                       //break;
                   }


               }
               return dbContext.TAmbulance.Where(t => t.分站编码 == id && t.是否有效 == true).ToList();
           }
       }

       public static List<TDesk> GetAllDesk(Anchor.FA.Utility.ButtonPower bp, C_WorkerDetail UserInfo)
       {
           using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
           {
               switch (bp.GetGroupRangePower("searchBound"))
               {
                   case "SearchAll":
                       return dbContext.TDesk.Where(t => t.受理台类型编码 == 4 || t.受理台类型编码 == 17).ToList();
                   case "SearchCenter"://查找所属分中心
                       return dbContext.TDesk.Where(t => (t.受理台类型编码 == 4 || t.受理台类型编码 == 17) && t.中心编码 == UserInfo.CenterCode).ToList();
                   default://没有设置查询权限
                       return null;
                   //break;
               }
           }
       }

       public static object GetNoticeList(int page, int rows, string order, string sort, DateTime startTime, DateTime endTime,
           int sendType, string station, string vehicle, Anchor.FA.Utility.ButtonPower bp, C_WorkerDetail UserInfo)
       {
           using (MainDataContext dbContext = new MainDataContext())
           {
               StringBuilder strSQL = new StringBuilder();
               strSQL.Append("SELECT distinct ID=A.编码,Num=A.编码,content=A.内容,sendTime=A.发送时刻,A.操作员编码,worker=P.姓名,sendList=dbo.GetStrRecAmb(A.编码),typeName=tznt.名称,typeCode=A.类型编码 ");
               strSQL.Append(@",backCount=(select COUNT(*) from MobileGet 
                    where Mobile in (select 目的地编码 from TNoticeLog where 通知单编码=A.编码)
                    and [GetDate] > (select 发送时刻 from TNotice where 编码=A.编码)
                    and [GetDate] < (select dateadd(n,30,发送时刻) from TNotice where 编码=A.编码)) ");
               strSQL.Append("from TNotice A  ");
               //strSQL.Append("left join B_WORKER_ROLE B on rtrim(A.操作员编码)=rtrim(B.TPerson编码) and rtrim(B.TPerson编码) !='' ");
               //strSQL.Append("left join B_WORKER W on B.WorkerID=W.ID ");
               strSQL.Append("inner join TPerson P on A.操作员编码=P.编码 ");
               strSQL.Append("inner join TStation S on P.分站编码=S.编码 ");
               strSQL.Append("left join TZNoticeType tznt on A.类型编码=tznt.编码 ");

               StringBuilder sbWhereClause = new StringBuilder();
               //车辆通知0、台通知1、短信通知2、分中心通知3、分站通知4、收到短信5
               switch (sendType)
               {
                   case 0://车辆通知
                       if (vehicle != "-1")//选择了车：只查询车辆通知
                       {
                           strSQL.Append(" inner join TNoticeLog L on A.编码=L.通知单编码 and L.目的地编码 = '" + vehicle + "' and A.类型编码=0 ");
                       }
                       else if (station.Trim() != "-1")//没选车，选了分站
                       {
                           strSQL.Append(" inner join TNoticeLog L on A.编码=L.通知单编码 and (L.目的地编码 in( select 车辆编码 from TAmbulance where 分站编码='" + station + "') and A.类型编码=0) ");//车辆通知
                       }
                       WhereClauseUtility.AddIntEqual("A.类型编码", sendType, sbWhereClause);
                       break;
                   case 1://台通知
                       WhereClauseUtility.AddIntEqual("A.类型编码", sendType, sbWhereClause);
                       break;
                   case 2://短信通知
                       if (station.Trim() != "-1")//选了分站
                       {
                           strSQL.Append(" inner join TNoticeLog L on A.编码=L.通知单编码 and ( L.目的地编码 in(select EmpNo from B_WORKER_ROLE");

                           strSQL.Append(" where WorkerID in (select wo.WorkerID from B_WORKER_ORGANIZATION wo left join B_ORGANIZATION b on wo.OrgID = b.ID where b.编码='" + station + "')) and A.类型编码=2) ");//短信通知2 
                       }
                       WhereClauseUtility.AddIntEqual("A.类型编码", sendType, sbWhereClause);
                       break;
                   case 4://分站通知
                       if (station.Trim() != "-1")//选了分站
                       {
                           strSQL.Append(" inner join TNoticeLog L on A.编码=L.通知单编码 and ( L.目的地编码 ='" + station + "' and A.类型编码=4)");//分站通知4
                       }
                       WhereClauseUtility.AddIntEqual("A.类型编码", sendType, sbWhereClause);
                       break;
                   default://没有选择发送类型
                       if (vehicle != "-1")//选择了车：只查询车辆通知
                       {
                           strSQL.Append(" inner join TNoticeLog L on A.编码=L.通知单编码 and L.目的地编码 = '" + vehicle + "' and A.类型编码=0 ");
                           sbWhereClause.Append(" where A.类型编码=0 ");
                       }
                       else if (station.Trim() != "-1")//没选车，选了分站
                       {
                           strSQL.Append(" inner join TNoticeLog L on A.编码=L.通知单编码 ");
                           strSQL.Append(" and (L.目的地编码 in( select 车辆编码 from TAmbulance where 分站编码='" + station + "') and A.类型编码=0 ");//车辆通知
                           strSQL.Append(" or L.目的地编码 in(select EmpNo from B_WORKER_ROLE");//短信通知2
                           strSQL.Append(" where WorkerID in (select wo.WorkerID from B_WORKER_ORGANIZATION wo left join B_ORGANIZATION b on wo.OrgID = b.ID where b.编码='" + station + "')) and A.类型编码=2 ");//短信通知2 
                           strSQL.Append(" or L.目的地编码 ='" + station + "' and A.类型编码=4)");//分站通知4
                           sbWhereClause.Append(" where A.类型编码 in(0,2,4) ");
                       }
                       break;
               }
               WhereClauseUtility.AddDateTimeGreaterThan("A.发送时刻", startTime, sbWhereClause);
               WhereClauseUtility.AddDateTimeLessThan("A.发送时刻", endTime, sbWhereClause);

               switch (bp.GetGroupRangePower("searchBound"))
               {
                   case "SearchAll":
                       break;
                   case "SearchCenter"://查找所属分中心
                       WhereClauseUtility.AddIntEqual("S.中心编码", UserInfo.CenterCode, sbWhereClause);
                       break;
                   default://没有设置查询权限
                       return null;
                   //break;
               }

               strSQL.Append(sbWhereClause);

               strSQL.Append(" order by A.发送时刻 desc");
               string sql = strSQL.ToString();
               var list1 = dbContext.ExecuteQuery<C_Notice>(sql);
               var list2 = dbContext.ExecuteQuery<C_Notice>(sql);

               long total = list1.LongCount();
               list2 = list2.OrderByDescending(t => t.sendTime);
               list2 = list2.Skip((page - 1) * rows).Take(rows);

               return new { total = total, rows = list2.ToList() };
           }
       }

       public static object GetBack(int code)
       {
           string sql = string.Format(@"create table #Tmp
                    (
                        sendList  nvarchar(30),
                        content nvarchar(1000),   
                        sendTime datetime
                    );

                    DECLARE MyCursor CURSOR    
                        FOR 
                        select Mobile,min([GETDATE]) from MobileGet 
                    where Mobile in (select 目的地编码 from TNoticeLog where 通知单编码={0})
                    and [GetDate] > (select 发送时刻 from TNotice where 编码={0})
                    and [GetDate] < (select dateadd(n,30,发送时刻) from TNotice where 编码={0})
                    GROUP BY Mobile

                    --打开一个游标    
                    OPEN MyCursor

                    --循环一个游标
                    DECLARE @mobile nvarchar(30) ,@date datetime,@content NVARCHAR(1000)
                    FETCH NEXT FROM  MyCursor INTO @mobile, @date
                    WHILE @@FETCH_STATUS =0
                        BEGIN                            
		                    set @content=''
		                    select @content=@content+content from MobileGet 
		                    where Mobile in (select 目的地编码 from TNoticeLog where 通知单编码={0})
		                    and [GetDate] > (select 发送时刻 from TNotice where 编码={0})
		                    and [GetDate] < (select dateadd(n,30,发送时刻) from TNotice where 编码={0})
		                    and Mobile=@mobile
		
		                    insert #Tmp(sendList,content,sendTime) values(@mobile,@content,@date)
		
                            FETCH NEXT FROM  MyCursor INTO @mobile, @date
                        END    

                        --关闭游标
                        CLOSE MyCursor
                        --释放资源
                        DEALLOCATE MyCursor

                        select * from #Tmp
                        drop table #Tmp ", code);

           using (MainDataContext dbContext = new MainDataContext())
            {
                var result = dbContext.ExecuteQuery<C_Notice>(sql);;

                return result.ToList();
            }
            
       }
    }
}

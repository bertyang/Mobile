using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Anchor.FA.Model;
using System.Data.Linq;
using System.Linq.Expressions;
using Anchor.FA.Utility;

namespace Anchor.FA.DAL.BasicInfo
{
    public class TelLog
    {
        //public static object LoadAllTelLogByPage(DateTime begin, DateTime end, int page, int rows, string order, string sort)
        //{
        //    using (MainDataContext dbContext = new MainDataContext())
        //    {
        //        var list = (from p in dbContext.TTelLog
        //                    join o in dbContext.TZTelLogRecordType on p.记录类型编码 equals o.编码
        //                    join o1 in dbContext.TDesk on p.台号 equals o1.台号
        //                    join o2 in dbContext.TPerson  on p.调度员工号 equals o2.编码
        //                    join o3 in dbContext.TZTelLogResult on p.结果编码 equals o3.编码
        //                    where p.产生时刻 > begin && p.产生时刻 < end
        //                    select new
        //                    {
        //                        ID = p.编码,
        //                        RecordStyle=o.名称,
        //                        Tel=p.对方电话,
        //                        RecordTime = p.产生时刻,
        //                        CallTime = p.通话时刻,
        //                        Desk=o1.显示名称,
        //                        Dispatcher=o2.姓名,
        //                        RecordCode=p.录音号,
        //                        Result=o3.名称,
        //                    }).Take(100);
        //        long total = list.LongCount();
        //        list = list.OrderByDescending(p => p.RecordTime);
        //        list = list.Skip((page - 1) * rows).Take(rows);
        //        var list2 = list.ToList().Select(o => new
        //        {
        //            ID = o.ID,
        //            RecordStyle = o.RecordStyle,
        //            Tel = o.Tel,
        //            RecordTime = o.RecordTime.ToString(),
        //            Desk = o.Desk,
        //            Dispatcher = o.Dispatcher,
        //            RecordCode = o.RecordCode,
        //            Result = o.Result,
        //            CallTime = o.CallTime.ToString()              
        //        });
        //        var result = new { total = total, rows = list2.ToList() };

        //        return result;
        //    }

        //}
        public static object Search(DateTime begin, DateTime end, string tel, string rec, string op, string res, string des,
            int page, int rows, string order, string sort,Anchor.FA.Utility.ButtonPower b,C_WorkerDetail userDetail)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = (from p in dbContext.TTelLog
                            join o in dbContext.TZTelLogRecordType on p.记录类型编码 equals o.编码 into temp
                            from o in temp.DefaultIfEmpty()
                            join o1 in dbContext.TDesk on p.台号 equals o1.台号 into temp1
                            from o1 in temp1.DefaultIfEmpty()
                            join o2 in dbContext.TPerson on p.调度员工号 equals o2.编码 into temp2
                            from o2 in temp2.DefaultIfEmpty()
                            join o3 in dbContext.TZTelLogResult on p.结果编码 equals o3.编码 into temp3
                            from o3 in temp3.DefaultIfEmpty()
                            join o4 in dbContext.TZTelLogOperator on p.操作说明编码 equals o4.编码 into temp4
                            from o4 in temp4.DefaultIfEmpty()
                           
                            where p.产生时刻 > begin && p.产生时刻 < end
                            orderby p.产生时刻 descending
                            where p.产生时刻 > begin && p.产生时刻 < end
                            orderby p.产生时刻 descending
                            select new
                            {
                                ID = p.编码,
                                RecordStyle = o.名称,
                                Tel = p.对方电话,
                                RecordTime = p.产生时刻,
                                InhaleTime = p.呼入时刻,
                                FellInTime = p.排队时刻,
                                ShakeBellTime = p.震铃时刻,
                                CallTime = p.通话时刻,
                                MiddleHandleTime = p.中间操作时刻,
                                FinishTime = p.结束时刻,
                                Desk = o1.显示名称,
                                Dispatcher = o2.姓名,
                                RecordCode = p.录音号,
                                Result = o3.名称,
                                OP = o4.名称,
                                CenterCode = p.中心编码,
                            });

                if (!string.IsNullOrEmpty(tel))
                {
                    list = list.Where(o => o.Tel.Contains(tel));
                }
                if (!string.IsNullOrEmpty(rec) && rec != "请选择")
                {
                    list = list.Where(o => o.RecordStyle == rec);
                }
                if (!string.IsNullOrEmpty(op) && op != "请选择")
                {
                    list = list.Where(o => o.OP == op);
                }
                if (!string.IsNullOrEmpty(res) && res != "请选择")
                {
                    list = list.Where(o => o.Result == res);
                }
                if (!string.IsNullOrEmpty(des) && des != "请选择")
                {
                    list = list.Where(o => o.Desk == des);
                }

                switch (b.GetGroupRangePower("searchBound"))
                {
                    case "SearchAll"://查找所属分中心
                        break;
                    case "SearchCenter"://查找所属分中心
                        list = list.Where(t => t.CenterCode == userDetail.CenterCode);
                        break;
                    default://没有设置查询权限
                        return null;
                }

                long total = list.LongCount();
                //list = list.OrderBy(p => p.ID);
                list = list.Skip((page - 1) * rows).Take(rows);
                var list2 = list.ToList().Select(o => new
                {
                    ID = o.ID,
                    Num = o.ID,
                    RecordStyle = o.RecordStyle,//记录类型
                    Tel = o.Tel,//对方编码
                    RecordTime = o.RecordTime.ToString(),//产生时刻  
                    InhaleTime = o.InhaleTime.ToString(),//呼入时刻
                    FellInTime = o.FellInTime.ToString(),//排队时刻
                    ShakeBellTime = o.ShakeBellTime.ToString(),//振铃时刻
                    CallTime = o.CallTime.ToString(),//通话时刻
                    MiddleHandleTime = o.MiddleHandleTime.ToString(),//中间操作时刻
                    FinishTime = o.FinishTime.ToString(),//完成时刻
                    Desk = o.Desk,//台号
                    Dispatcher = o.Dispatcher,//调度员
                    RecordCode = o.RecordCode,//录音号
                    Result = o.Result,//结果
                    OP = o.OP //操作说明
                });
                //var   list2  =(from p in list
                //               join o5 in dbContext.TAlarmCall on p.RecordCode equals o5.录音号 into temp5
                //                from o5 in temp5.DefaultIfEmpty()
                //               select new
                //                {
                //                    ID = p.ID,
                //                    RecordStyle = p.RecordStyle,
                //                    Tel = p.Tel,
                //                    RecordTime = p.RecordTime.ToString(),
                //                    Desk = p.Desk,
                //                    Dispatcher = p.Dispatcher,
                //                    RecordCode = p.RecordCode,
                //                    Result = p.Result,
                //                    CallTime = p.CallTime.ToString(),
                //                    AlarmEventCode = o5.事件编码,
                //                });
                var result = new { total = total, rows = list2.ToList()};

                return result;
            }
        }
        public static List<TZTelLogRecordType> GetAllRecordTypes()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZTelLogRecordType.ToList();
            }
        }
        public static List<TZTelLogOperator> GetAllOperatorTypes()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZTelLogOperator.ToList();
            }
        }
        public static List<TZTelLogResult> GetAllResult()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZTelLogResult.ToList();
            }
        }
        public static List<TDesk> GetAllDesks()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TDesk.Where(p => p.受理台类型编码 == 4 || p.受理台类型编码 == 17).ToList();
            }
        }
    }
}

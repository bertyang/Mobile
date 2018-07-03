using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Anchor.FA.Model;

namespace Anchor.FA.DAL.WorkFlow
{
    public class WorkItemInstance
    {
        public static object ApproveList(int page, int rows, string order, string sort,
            string flowId, DateTime startTime, DateTime endTime, string flowNo, int approverId)
        {
            endTime = endTime.AddDays(1);

            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from a in dbContext.F_INST_WORKITEM
                           join b in dbContext.F_INST_ACTIVITY on a.ActivityInstID equals b.ID
                           join c in dbContext.F_INST_FLOW on b.FlowInstID equals c.ID
                           join d in dbContext.F_FLOW on c.FlowID equals d.ID
                           join e in dbContext.B_WORKER on c.ApplyerID equals e.ID
                           join f in dbContext.F_ACTIVITY on b.ActivityID equals f.ID
                           where a.State == "W"
                                && a.Assigneeid == approverId
                                && c.BeginDate >= startTime
                                && c.BeginDate < endTime

                           select new
                           {
                               WorkItemInstID = a.ID,
                               ActivityInstID = b.ID,
                               FlowInstID = c.ID,
                               FlowName = d.Name,
                               FlowID = d.ID,
                               FlowNo = c.FlowNo,
                               ActivityID = f.ID,
                               SplitType = f.SplitType,                                                                                         ReturnType= f.ReturnType,
                               Url = string.IsNullOrEmpty(f.Url)? d.Url : f.Url,
                               ApplyerName = e.Name,
                               BeginDate = c.BeginDate,

                           };

                if (!string.IsNullOrEmpty(flowId))
                {
                    int intFlowId = int.Parse(flowId);

                    list = list.Where(t => t.FlowID == intFlowId);
                }

                if (!string.IsNullOrEmpty(flowNo))
                {
                    int intFlowNo = int.Parse(flowNo);

                    list = list.Where(t => t.FlowNo == intFlowNo);
                }

                long total = list.LongCount();

                list = list.OrderByDescending(t => t.BeginDate);
                list = list.Skip((page - 1) * rows).Take(rows);

                //var listResult = list.ToList().Select(o => new
                //{
                //    WorkItemInstID = o.WorkItemInstID,
                //    ActivityInstID = o.ActivityInstID,
                //    FlowInstID = o.FlowInstID,
                //    FlowName = o.FlowName,
                //    FlowID = o.FlowID,
                //    ActivityID = o.ActivityID,
                //    SplitType = o.SplitType,
                //    ReturnType = o.ReturnType,
                //    FlowNo = o.FlowNo,
                //    Url = o.Url == null ? string.Empty : o.Url,
                //    ApplyerName = o.ApplyerName,
                //    BeginDate = o.BeginDate.ToString("yyyy-MM-dd HH:mm:ss"),
                //});

                var result = new { total = total, rows = list.ToList() };

                return result;
            }
        }

        public static int ApproveListCount(int approverId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                int count = (from a in dbContext.F_INST_WORKITEM
                           where a.State == "W" && a.Assigneeid == approverId
                           select a).Count();

                return count;
            }
        }

        public static object TraceApply(int page, int rows, string order, string sort,
            string flowId, DateTime startTime, DateTime endTime, string flowNo, int applyerId)
        {
            endTime = endTime.AddDays(1);

            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from a in dbContext.F_INST_FLOW
                           join b in dbContext.F_FLOW on a.FlowID equals b.ID
                           join c in dbContext.B_WORKER on a.ApplyerID equals c.ID
                           where a.ApplyerID == applyerId && a.BeginDate >= startTime
                                && a.BeginDate < endTime

                           select new
                           {
                               FlowName = b.Name,
                               FlowID = b.ID,
                               FlowNo = a.FlowNo,
                               FlowInstId = a.ID,
                               Url = b.Url,
                               ApplyerName = c.Name,
                               BeginDate = a.BeginDate,
                               State = a.State,
                               IsInner = b.IsInner
                           };

                if (!string.IsNullOrEmpty(flowId))
                {
                    int intFlowId = int.Parse(flowId);

                    list = list.Where(t => t.FlowID == intFlowId);
                }

                if (!string.IsNullOrEmpty(flowNo))
                {
                    int intFlowNo = int.Parse(flowNo);

                    list = list.Where(t => t.FlowNo == intFlowNo);
                }

                long total = list.LongCount();

                list = list.OrderByDescending(t => t.BeginDate);
                list = list.Skip((page - 1) * rows).Take(rows);

                //var listResult = list.ToList().Select(o => new
                //{
                //    FlowName = o.FlowName,
                //    FlowID = o.FlowID,
                //    FlowNo = o.FlowNo,
                //    FlowInstId = o.FlowInstId,
                //    Url = o.Url == null ? string.Empty : o.Url,
                //    ApplyerName = o.ApplyerName,
                //    BeginDate = o.BeginDate.ToString("yyyy-MM-dd HH:mm:ss"),
                //    StateCn = TransaferFlowState(o.State),
                //    StateEn = o.State,
                //    IsInner = o.IsInner
                //});


                var result = new { total = total, rows = list.ToList() };
                return result;
            }
        }


        public static object TraceApprove(int page, int rows, string order, string sort,
          string flowId, DateTime startTime, DateTime endTime, string flowNo, int approverId)
        {
            endTime = endTime.AddDays(1);

            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from a in dbContext.F_INST_WORKITEM
                           join b in dbContext.F_INST_ACTIVITY on a.ActivityInstID equals b.ID
                           join c in dbContext.F_INST_FLOW on b.FlowInstID equals c.ID
                           join d in dbContext.F_FLOW on c.FlowID equals d.ID
                           join e in dbContext.B_WORKER on c.ApplyerID equals e.ID
                           join f in dbContext.F_ACTIVITY on b.ActivityID equals f.ID
                           where (a.Actorid == approverId && a.State == "C" 
                           || a.Assigneeid == approverId && a.State == "P")
                                    && a.EndDate >= startTime
                                    && a.EndDate < endTime

                           select new
                           {
                               FlowName = d.Name,
                               FlowID = d.ID,
                               FlowNo = c.FlowNo,
                               FlowInstId = c.ID,
                               Url = d.Url,
                               ApplyerName = e.Name,
                               ApproveDate = a.EndDate,
                               State = c.State,
                               IsInner = d.IsInner
                           };


                //取签核时间最大的
                list = from l in list
                            group l by new 
                            { l.FlowName, 
                              l.FlowID,
                              l.FlowNo,
                              l.FlowInstId,
                              l.Url,
                              l.ApplyerName,
                              l.State,
                              l.IsInner
                            } into g  
                    select new  
                    {
                        FlowName = g.Key.FlowName,
                        FlowID = g.Key.FlowID,
                        FlowNo = g.Key.FlowNo,
                        FlowInstId = g.Key.FlowInstId,
                        Url = g.Key.Url,
                        ApplyerName = g.Key.ApplyerName,
                        ApproveDate = g.Max(l=>l.ApproveDate),
                        State = g.Key.State,
                        IsInner = g.Key.IsInner
                    };

                if (!string.IsNullOrEmpty(flowId))
                {
                    int intFlowId = int.Parse(flowId);

                    list = list.Where(t => t.FlowID == intFlowId);
                }

                if (!string.IsNullOrEmpty(flowNo))
                {
                    int intFlowNo = int.Parse(flowNo);

                    list = list.Where(t => t.FlowNo == intFlowNo);
                }

                long total = list.LongCount();

                list = list.OrderByDescending(t => t.ApproveDate);
                list = list.Skip((page - 1) * rows).Take(rows);

                //var listResult = list.ToList().Select(o => new
                //{
                //    FlowName = o.FlowName,
                //    FlowID = o.FlowID,
                //    FlowNo = o.FlowNo,
                //    FlowInstId = o.FlowInstId,
                //    Url = o.Url == null ? string.Empty : o.Url,
                //    ApplyerName = o.ApplyerName,
                //    ApproveDate = Convert.ToDateTime(o.ApproveDate).ToString("yyyy-MM-dd HH:mm:ss"),
                //    StateCn = TransaferFlowState(o.State),
                //    StateEn = o.State,
                //    IsInner = o.IsInner
                //});

                var result = new { total = total, rows = list.ToList() };

                return result;
            }
        }

        public static object TraceNotify(int page, int rows, string order, string sort,
           string flowId, DateTime startTime, DateTime endTime, string flowNo, int notifyWorkerId)
        {
            endTime = endTime.AddDays(1);

            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from a in dbContext.F_INST_FLOW
                           join b in dbContext.F_FLOW on a.FlowID equals b.ID
                           join c in dbContext.B_WORKER on a.ApplyerID equals c.ID
                           join d in dbContext.F_INST_NOTICE on a.FlowID equals d.FlowID
                           where a.FlowNo == d.FlowNo 
                               && d.WorkerID == notifyWorkerId 
                               && a.BeginDate >= startTime 
                               && a.BeginDate < endTime

                           select new
                           {
                               FlowName = b.Name,
                               FlowID = b.ID,
                               FlowNo = a.FlowNo,
                               FlowInstId = a.ID,
                               Url = b.Url,
                               ApplyerName = c.Name,
                               BeginDate = a.BeginDate,
                               State = a.State,
                               IsInner = b.IsInner,
                               ReadFlag = d.ReadFlag
                           };

                if (!string.IsNullOrEmpty(flowId))
                {
                    int intFlowId = int.Parse(flowId);

                    list = list.Where(t => t.FlowID == intFlowId);
                }

                if (!string.IsNullOrEmpty(flowNo))
                {
                    int intFlowNo = int.Parse(flowNo);

                    list = list.Where(t => t.FlowNo == intFlowNo);
                }

                long total = list.LongCount();

                list = list.OrderByDescending(t => t.BeginDate);
                list = list.Skip((page - 1) * rows).Take(rows);

                var result = new { total = total, rows = list.ToList() };
                return result;
            }
        }

        public static int NotifyListCount(int workerID)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                int count = (from a in dbContext.F_INST_NOTICE
                             where a.WorkerID == workerID && !a.ReadFlag 
                             select a).Count();

                return count;
            }
        }

        /// <summary>
        /// (收件箱)查看过的邮件改为已读
        /// </summary>
        /// <param name="mailID"></param>
        /// <returns></returns>
        public static bool NoticeReadFlag(int flowId,int flowNo, int workerID)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                F_INST_NOTICE w = dbContext.F_INST_NOTICE.FirstOrDefault(m => m.FlowID == flowId && m.FlowNo == flowNo && m.WorkerID == workerID);
                w.ReadFlag = true;
                dbContext.SubmitChanges();
                return true;
            }
        }

        public static object Monitor(int page, int rows, string order, string sort,
  string flowId, DateTime startTime, DateTime endTime, string flowNo)
        {
            endTime = endTime.AddDays(1);

            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from a in dbContext.F_INST_FLOW
                           join b in dbContext.F_FLOW on a.FlowID equals b.ID
                           join c in dbContext.B_WORKER on a.ApplyerID equals c.ID
                           where a.BeginDate >= startTime && a.BeginDate < endTime

                           select new
                           {
                               FlowName = b.Name,
                               FlowID = b.ID,
                               FlowNo = a.FlowNo,
                               FlowInstId = a.ID,
                               Url = b.Url,
                               ApplyerName = c.Name,
                               BeginDate = a.BeginDate,
                               State = a.State,
                               IsInner = b.IsInner
                           };

                if (!string.IsNullOrEmpty(flowId))
                {
                    int intFlowId = int.Parse(flowId);

                    list = list.Where(t => t.FlowID == intFlowId);
                }

                if (!string.IsNullOrEmpty(flowNo))
                {
                    int intFlowNo = int.Parse(flowNo);

                    list = list.Where(t => t.FlowNo == intFlowNo);
                }

                long total = list.LongCount();

                list = list.OrderByDescending(t => t.BeginDate);
                list = list.Skip((page - 1) * rows).Take(rows);

                var result = new { total = total, rows = list.ToList() };
                return result;
            }
        }

        public static void DeleteNoCompleteTask(int flowInstId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                string sql = string.Format(@"delete F_INST_WORKITEM where ID in (
                                                select a.id from F_INST_WORKITEM a
                                                join F_INST_ACTIVITY b on a.ActivityInstID = b.ID
                                                join F_INST_FLOW c on b.FlowInstID = c.ID                           
                                                where c.ID={0} and a.State='W')", flowInstId);

                dbContext.ExecuteCommand(sql);

            }
        }

        public static List<C_TASK_LIST> TaskList(int flowInstId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from a in dbContext.F_INST_WORKITEM
                           join b in dbContext.F_INST_ACTIVITY on a.ActivityInstID equals b.ID
                           join c in dbContext.F_INST_FLOW on b.FlowInstID equals c.ID
                           //join d in dbContext.F_FLOW on c.FlowID equals d.ID
                           join e in dbContext.B_WORKER on a.Assigneeid equals e.ID
                           join d in dbContext.B_WORKER on a.Actorid equals d.ID into d_join //left join
                           from d in d_join.DefaultIfEmpty()
                           join f in dbContext.F_ACTIVITY on b.ActivityID equals f.ID
                           where c.ID == flowInstId && a.State != "P"

                           select new C_TASK_LIST()
                           {
                               ApproveDate = a.EndDate,
                               ApproverName = d.Name == null? e.Name : d.Name,
                               AppValue = a.AppValue,
                               AppRemark = a.AppRemark,
                               Role = a.Role
                           };


                //var listResult = list.ToList().Select(o => new C_TASK_LIST()
                //{
                //    ApproveDate = o.ApproveDate,
                //    ApproverName = o.ApproverName,
                //    AppValue = TransaferAppValue(o.AppValue),
                //    AppRemark = o.AppRemark,
                //    Role = o.Role
                //});

                return list.ToList<C_TASK_LIST>();
            }
        }

        public static List<C_TASK_LIST> TaskList(int flowId,int flowNo)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from a in dbContext.F_INST_WORKITEM
                           join b in dbContext.F_INST_ACTIVITY on a.ActivityInstID equals b.ID
                           join c in dbContext.F_INST_FLOW on b.FlowInstID equals c.ID
                           //join d in dbContext.F_FLOW on c.FlowID equals d.ID
                           join e in dbContext.B_WORKER on a.Assigneeid equals e.ID
                           join d in dbContext.B_WORKER on a.Actorid equals d.ID into d_join //left join
                           from d in d_join.DefaultIfEmpty()
                           join f in dbContext.F_ACTIVITY on b.ActivityID equals f.ID
                           where c.FlowID == flowId && c.FlowNo == flowNo && a.State != "P"

                           select new C_TASK_LIST()
                           {
                               ApproveDate = a.EndDate,
                               ApproverName = d.Name == null ? e.Name : d.Name,
                               AppValue = a.AppValue,
                               AppRemark = a.AppRemark,
                               Role = a.Role
                           };


                //var listResult = list.ToList().Select(o => new C_TASK_LIST()
                //{
                //    ApproveDate = o.ApproveDate,
                //    ApproverName = o.ApproverName,
                //    AppValue = TransaferAppValue(o.AppValue),
                //    AppRemark = o.AppRemark,
                //    Role = o.Role
                //});

                return list.ToList<C_TASK_LIST>();
            }
        }
        public static F_INST_WORKITEM Get(int id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_INST_WORKITEM.FirstOrDefault(t => t.ID == id);
            }
        }

        public static List<F_INST_WORKITEM> GetList(int activityInstanceid)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_INST_WORKITEM.Where(t => t.ActivityInstID == activityInstanceid).ToList();
            }
        }


        public static void Insert(F_INST_WORKITEM entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                //var list = from a in dbContext.F_INST_WORKITEM select a.ID;

                //long total = list.LongCount();

                //if (total == 0)
                //{
                //    entity.ID = 1;
                //}
                //else
                //{
                //    entity.ID = dbContext.F_INST_WORKITEM.Max(a => a.ID) + 1;
                //}


                dbContext.F_INST_WORKITEM.InsertOnSubmit(entity);

                dbContext.SubmitChanges();

                //return entity.ID;
            }
        }

        public static bool Save(F_INST_WORKITEM entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var model = dbContext.F_INST_WORKITEM.FirstOrDefault(t => t.ID == entity.ID);

                model.ActivityInstID = entity.ActivityInstID;
                model.Actorid = entity.Actorid;
                model.Assigneeid = entity.Assigneeid;
                model.Assignerid = entity.Assignerid;
                model.BeginDate = entity.BeginDate;
                model.EndDate = entity.EndDate;
                model.State = entity.State;
                model.AppType = entity.AppType;
                model.AssignType = entity.AssignType;
                model.AppValue = entity.AppValue;
                model.AppRemark = entity.AppRemark;

                dbContext.SubmitChanges();
            }

            return true;
        }

        public static bool Delete(decimal id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var model = dbContext.B_WORKER.SingleOrDefault(t => t.ID == id);

                if (model != null)
                {
                    //dbContext.B_WORKER.Load();
                    dbContext.B_WORKER.DeleteOnSubmit(model);

                    dbContext.SubmitChanges();

                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
    }
}

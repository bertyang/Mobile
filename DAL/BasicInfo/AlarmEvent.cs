using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Anchor.FA.Model;
using System.Linq.Expressions;
using Anchor.FA.Utility;
using System.Reflection;

using Anchor.FA.BLL.IBLL;
using Spring.Context;
using Spring.Context.Support;

namespace Anchor.FA.DAL.BasicInfo
{
    public class AlarmEvent
    {

        
        /// <summary>
        /// 获取所有电话信息
        /// </summary>
        public static object GetStationMsgs(int PageSize, int PageIndex, string alarmEventCode)
        {

            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = (from tae in dbContext.TAlarmEvent

                            join t in dbContext.TStationMsg on tae.事件编码 equals t.事件编码 into tsm_join
                            from tsm in tsm_join.DefaultIfEmpty()

                            join t in dbContext.TStation on tsm.分站编码 equals t.编码 into ts_join
                            from ts in ts_join.DefaultIfEmpty()

                            join t in dbContext.TZStationMsgType on tsm.类型 equals t.编码 into tzm_join
                            from tzm in tzm_join.DefaultIfEmpty()

                            where tae.事件编码 == alarmEventCode

                            orderby tsm.时间 descending
                            select new
                            {
                                tae.事件名称
                                ,tsm.时间
                                ,分站=ts.名称
                                ,回复类型=tzm.名称
                            });
                long total = list.LongCount();
                list = list.Skip((PageIndex - 1) * PageSize).Take(PageSize);
                var result = new { total = total, rows = list.ToList() };
                return result;
            }
        }



        public static List<C_ModifyRecord> GetModifyRecord(string eventCode, out long total)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = (from tmr in dbContext.TModifyRecord
                            join t in dbContext.TPerson on tmr.操作员编码 equals t.编码 into TP_join
                            from TP in TP_join.DefaultIfEmpty()
                            join t in dbContext.TZModifyRecordType on tmr.修改类型编码 equals t.编码 into tzmt_join
                            from tzmt in tzmt_join.DefaultIfEmpty()

                            where tmr.事件编码 == eventCode
                            orderby tmr.产生时刻 descending
                            select new C_ModifyRecord
                            {
                                tmr=tmr,
                                修改类型 = tzmt!=null?tzmt.名称:null,
                                操作员 = TP!=null?TP.姓名:null
                            });
                total = list.LongCount();
                return list.ToList() ;
            }
        }
        #region 修改

        private static Dictionary<string, int> m_ModifyEventTypeDic = null;
        public string Update(TAlarmEvent entity)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                try
                {
                    TAlarmEvent model = dbContext.TAlarmEvent.FirstOrDefault(t => t.事件编码 == entity.事件编码);

                    if (entity.事故类型编码 != -1)
                    {
                        if (entity.所属事故编码 == null || entity.所属事故编码.Trim() == "")
                        {
                            entity.所属事故编码 = entity.事件编码;
                        }
                    }

                    if (m_ModifyEventTypeDic == null)
                    {
                        m_ModifyEventTypeDic = new Dictionary<string, int>();
                        m_ModifyEventTypeDic.Add("事件来源编码", 0);
                        m_ModifyEventTypeDic.Add("事件类型编码", 1);
                        m_ModifyEventTypeDic.Add("事故类型编码", 2);
                        m_ModifyEventTypeDic.Add("事故等级编码", 3);

                        m_ModifyEventTypeDic.Add("事件名称", 50);//往数据库插入 50 事件名称 ture
                        m_ModifyEventTypeDic.Add("区域", 28);
                        m_ModifyEventTypeDic.Add("所属事故编码", 29);
                        //m_ModifyEventTypeDic.Add("EventSourceCode", 0);
                        //m_ModifyEventTypeDic.Add("EventTypeCode", 1);
                        //m_ModifyEventTypeDic.Add("AccidentTypeCode", 2);
                        //m_ModifyEventTypeDic.Add("AccidentLevelCode", 3);
                        //m_ModifyEventTypeDic.Add("IsTest", 4);
                        //m_ModifyEventTypeDic.Add("Area", 28);
                        //m_ModifyEventTypeDic.Add("AccidentCode", 29);
                    }
                    //List<TModifyRecord> mriList = new List<TModifyRecord>();
                    //获得所有property的信息
                    PropertyInfo[] properties = model.GetType().GetProperties();
                    //string OperatePersonCode = Task.LoadWorkerRole(Convert.ToInt32(entity.首次调度员编码)).EmpNo;//使用 首次调度员编码 来存储操作员编码

                    C_WorkerDetail CWD = Anchor.FA.DAL.Organize.Worker.GetWorkerDetailById(Convert.ToInt32(entity.首次调度员编码));//使用 首次调度员编码 来存储操作员编码
                    string OperatePersonCode = CWD.PersonCode.FirstOrDefault();
                    //string OperatePersonCode = entity.首次调度员编码;
                    
                    foreach (PropertyInfo p in properties)
                    {
                        int typeId = -1;
                        if (m_ModifyEventTypeDic.TryGetValue(p.Name, out typeId)) //如果需要修改
                        {
                            object oldObj = p.GetValue(model, null);
                            object newObj = p.GetValue(entity, null);
                            if (!object.Equals(oldObj, newObj))
                            {
                                p.SetValue(model, newObj, null);
                                TModifyRecord mri = new TModifyRecord();
                                mri.编码 = Guid.NewGuid();
                                mri.修改后内容 = newObj == null ? "" : newObj.ToString();//
                                mri.修改前内容 = oldObj == null ? "" : oldObj.ToString();//
                                mri.产生时刻 = DateTime.Now;
                                mri.操作员编码 = OperatePersonCode;
                                mri.修改类型编码 = typeId;
                                mri.事件编码 = entity.事件编码;
                                //ModifyRecordInfo mri = new ModifyRecordInfo();
                                //mri.Code = Guid.NewGuid();
                                //mri.NewContent = newObj == null ? "" : newObj.ToString();//2011-02-14 如果是null就费了。
                                //mri.OriginContent = oldObj == null ? "" : oldObj.ToString();//2011-02-14 如果是null就费了。
                                //mri.OperateTime = DateTime.Now;
                                //mri.OperatorCode = operatorCode;
                                //mri.TypeId = typeId;
                                //mri.EventCode = originInfo.EventCode;
                                //mriList.Add(mri);

                                dbContext.TModifyRecord.InsertOnSubmit(mri);
                            }
                        }
                    }
                    

                    //model.事件名称 = entity.事件名称;
                    //model.区域 = entity.区域;
                    //model.事件来源编码 = entity.事件来源编码;
                    //model.事故等级编码 = entity.事故等级编码;
                    //model.事件类型编码 = entity.事件类型编码;
                    //model.事故类型编码 = entity.事故类型编码;
                    //ReflectionUtility.CopyObjectProperty<TAcceptEvent>(entity, model);
                    dbContext.SubmitChanges();
                    return "";
                }
                catch (Exception ex)
                {
                    //Log4Net.LogError("", ex.ToString());
                    return ex.ToString();
                }
            }
        }

        #endregion
//        public static object AlarmEventLoad(DateTime begin, DateTime end, int page, int rows, string order, string sort)
//        {
//            //主查询
//            SqlParameterTool sqllist = new SqlParameterTool();
//            sqllist.commandText.Append(@"
//--declare @BeginTime datetime,@EndTime datetime;
//declare @EventCodeB char(16),@EventCodeE char(16);
//SELECT @EventCodeB = convert(char(8),@p" + sqllist.BeginInt + @",112)+'00000000' 
//,@EventCodeE = convert(char(8),dateadd(day,1,@p" + (sqllist.BeginInt + 1) + @"),112)+'00000000';
//
//select id=事件编码
//,tel=首次呼救电话
//,ori=tzaeo.名称
//,accnum=受理次数
//,alarmName=事件名称
//,[type]=tzaet.名称	--事件类型
//,[time]=首次受理时刻
//,diaoduyuan=TP.姓名
//,c_time=首次派车时刻
//,chuche =TAE.执行任务总数
//,正常完成=(select count(*) from dbo.TTask tt where tt.事件编码=tae.事件编码 and tt.是否正常结束=1)
//from  TAlarmEvent TAE
//left join TPerson TP on TAE.首次调度员编码 = TP.编码
//left join TZAlarmEventType tzaet on TAE.事件类型编码 = tzaet.编码
//left join TZAlarmEventOrigin tzaeo on TAE.事件来源编码 = tzaeo.编码
//where 是否测试 = 0 and TAE.事件编码 between @EventCodeB and @EventCodeE");
//            sqllist.commandText.Append(" and 首次受理时刻>=").Append("@p" + sqllist.i++);
//            sqllist.commandText.Append(" and 首次受理时刻<").Append("@p" + sqllist.i++);
//            sqllist.commandText.Append(" order by 首次受理时刻 desc");
//            //--------------------------
//            sqllist.commandObj.Add(begin);
//            sqllist.commandObj.Add(end);

//            using (MainDataContext dbContext = new MainDataContext())
//            {
//                var templist = dbContext.ExecuteQuery<C_AlarmEventSearch>(
//                    sqllist.commandText.ToString()
//                    , sqllist.commandObj.ToArray());

//                long total = templist.LongCount();
//                templist = templist.Skip((page - 1) * rows).Take(rows);
//                var result = new { total = total, rows = templist.ToList() };
//                return result;
//            }
//        }

        /// <summary>
        /// 区域下拉填充
        /// </summary>
        /// <returns></returns>
        public static List<TZArea> LoadAreas()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZArea.Where(p => p.是否有效 == true).OrderBy(p => p.顺序号).ToList();
            }
        }
        /// <summary>
        /// 事故等级下拉填充
        /// </summary>
        /// <returns></returns>
        public static List<TZAccidentLevel> LoadAccidentLevels()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZAccidentLevel.Where(p => p.是否有效 == true).OrderBy(p => p.顺序号).ToList();
            }
        }

        /// <summary>
        /// 调度员下拉填充
        /// </summary>
        /// <returns></returns>
        public static List<TPerson> LoadDis(Anchor.FA.Utility.ButtonPower bp, C_WorkerDetail userDetail)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                switch (bp.GetGroupRangePower("searchBound"))
                {
                    case "SearchAll"://查找所属分中心

                        return dbContext.TPerson.Where(t => t.类型编码 == 2 && t.是否有效 == true).OrderBy(t => t.顺序号).ToList();

                    case "SearchCenter"://查找所属分中心

                        return (from p in dbContext.TPerson
                                join s in dbContext.TStation on p.分站编码 equals s.编码
                                where p.类型编码 == 2 && p.是否有效 == true && s.中心编码 == userDetail.CenterCode
                                select p).OrderBy(t => t.顺序号).ToList();
                    default://没有设置查询权限
                        return null;
                }
            }
        }
        /// <summary>
        /// 分站下拉填充
        /// </summary>
        /// <returns></returns>
        //public static List<TStation> LoadStations()
        //{
        //    using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
        //    {
        //        return dbContext.TStation.Where(s => s.是否有效 == true).OrderBy(s => s.顺序号).ToList();
        //    }
        //}
        /// <summary>
        /// 车辆下拉填充
        /// </summary>
        /// <returns></returns>
        //public static List<TAmbulance> LoadAlums()
        //{
        //    using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
        //    {
        //        return dbContext.TAmbulance.Where(a => a.是否有效 == true).OrderBy(a => a.顺序号).ToList();
        //    }
        //}
        /// <summary>
        /// 事件类型填充
        /// </summary>
        /// <returns></returns>
        public static List<TZAlarmEventType> LoadAlarmTypes()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZAlarmEventType.Where(z => z.是否有效 == true).OrderBy(z => z.顺序号).ToList();
            }
        }
        /// <summary>
        /// 事件来源填充
        /// </summary>
        /// <returns></returns>
        public static List<TZAlarmEventOrigin> LoadAlarmOriTypes()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZAlarmEventOrigin.Where(z => z.是否有效 == true).OrderBy(z => z.顺序号).ToList();
            }
        }
        /// <summary>
        /// 获取所有事故类型
        /// </summary>
        /// <returns></returns>
        public static List<TZAccidentType> LoadAccidentType()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZAccidentType.Where(t => t.是否有效 == true).OrderBy(t => t.顺序号).ToList();
            }
        }
        /// <summary>
        /// 获取调度台受理界面 按钮相关信息
        /// </summary>
        /// <returns></returns>
        public static List<TParameterAcceptInfo> LoadParameterAcceptInfo()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TParameterAcceptInfo.ToList();
            }
        }
        /// <summary>
        /// 获取调度台受理界面 车辆状态名称相关信息
        /// TaskInfoEdit.aspx页面特用
        /// </summary>
        /// <returns></returns>
        public static List<TZAmbulanceState> LoadAmbulanceStateInfo()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                List<TZAmbulanceState>  l=dbContext.TZAmbulanceState.ToList();
                if (l.Count < 10)
                {
                    l=new List<TZAmbulanceState>();
                    l.Add(new TZAmbulanceState { 编码 = 0, 名称 = "生成任务", 顺序号 = 0, 是否有效 = true });
                    l.Add(new TZAmbulanceState { 编码 = 1, 名称 = "接收命令", 顺序号 = 1, 是否有效 = true });
                    l.Add(new TZAmbulanceState { 编码 = 2, 名称 = "出车", 顺序号 = 2, 是否有效 = true });
                    l.Add(new TZAmbulanceState { 编码 = 3, 名称 = "到达现场", 顺序号 = 3, 是否有效 = true });
                    l.Add(new TZAmbulanceState { 编码 = 4, 名称 = "离开现场", 顺序号 = 4, 是否有效 = true });
                    l.Add(new TZAmbulanceState { 编码 = 5, 名称 = "到达医院", 顺序号 = 5, 是否有效 = true });
                    l.Add(new TZAmbulanceState { 编码 = 6, 名称 = "完成任务", 顺序号 = 6, 是否有效 = true });
                    l.Add(new TZAmbulanceState { 编码 = 7, 名称 = "返回站中", 顺序号 = 7, 是否有效 = true });//只用到这里
                    l.Add(new TZAmbulanceState { 编码 = 8, 名称 = "不能调用", 顺序号 = 8, 是否有效 = true });
                    l.Add(new TZAmbulanceState { 编码 = 9, 名称 = "暂停调用", 顺序号 = 9, 是否有效 = true });
                }
                return l;
            }
        }        

        /// <summary>
        /// 查询
        /// </summary>
        public static object AlarmEventSearch(DateTime begin, DateTime end, string c_begin, string c_end, string tel, string Addr, string Dri,
            string Doc, string Nur, string Dis, string sta, string Alum, string type, string ori
            , string SuffererName, string ZhuSu, string SendAddress, string IllState, string AlarmEventCode
            , string IsTest, string judge
            , int page, int rows, string order, string sort
            ,Anchor.FA.Utility.ButtonPower p,int WorkerID)
        {
            string Search = p.GetGroupRangePower("searchBound");
            //相关TAcceptEvent表存在查询
            SqlParameterTool sqllistAc = new SqlParameterTool();
            sqllistAc.commandText.Append(@"
and exists(select * from TAcceptEvent tac where tae.事件编码=tac.事件编码");
            sqllistAc.AddObjectLike("tac.主诉", ZhuSu);
            sqllistAc.AddObjectLike("tac.现场地址", Addr);
            sqllistAc.AddObjectLike("tac.患者姓名", SuffererName);
            sqllistAc.AddObjectLike("tac.送往地点", SendAddress);
            sqllistAc.AddObjectEqual("tac.病情编码", IllState);//
            sqllistAc.AddObjectLike("tac.病种判断", judge);
            if (Search == "SearchControlMe")//查找受理中的本人
            {
                sqllistAc.commandText.Append(string.Format(@"
and exists(select * from B_WORKER_ROLE bwr where bwr.EmpNo=tac.责任受理人编码 and bwr.WorkerID={0})
", "@p" + sqllistAc.i++));
                sqllistAc.commandObj.Add(WorkerID);
            
            }
            sqllistAc.commandText.Append(")");

            if (sqllistAc.BeginInt == sqllistAc.i)
            {
                sqllistAc.commandText.Clear();
            }

            //相关TTask表存在查询
            SqlParameterTool sqllistT = new SqlParameterTool();
            sqllistT.BeginInt = sqllistAc.i;
            sqllistT.commandText.Append(@"
and exists(select * from TTask tt where tae.事件编码=tt.事件编码");
            sqllistT.AddObjectLike("tt.司机", Dri);
            sqllistT.AddObjectLike("tt.医生", Doc);
            sqllistT.AddObjectLike("tt.护士", Nur);
            sqllistT.AddObjectLike("tt.车辆编码", Alum);
            sqllistT.AddObjectEqual("tt.分站编码", sta);
            switch (p.GetGroupRangePower("searchBound"))
            {
                case "SearchAll":
                    break;
                case "SearchCenter"://查找分中心                   
                    break;
                case "SearchOrganization"://查找分站
                    sqllistT.commandText.Append(string.Format(@"
and exists(select * from B_ORGANIZATION bo where bo.编码 is not null and bo.type=3 and bo.编码=tt.分站编码 
    and exists(select * from B_WORKER_ORGANIZATION bwo where bwo.WorkerID={0} and bwo.OrgID=bo.ID)
)", "@p" + sqllistT.i++));
                    sqllistT.commandObj.Add(WorkerID);
                    break;
                case "SearchTaskMe"://查找任务中的本人
                    sqllistT.commandText.Append(string.Format(@"
and exists(select * from TTaskPersonLink ttpl where ttpl.任务编码=tt.任务编码 
    and exists(select * from B_WORKER_ROLE bwr where bwr.TPerson编码=ttpl.人员编码 and bwr.WorkerID={0})
)", "@p" + sqllistT.i++));
                    sqllistT.commandObj.Add(WorkerID);
                    break;
                case "SearchControlMe"://查找受理中的本人
                    break;
                default://没有设置查询权限
                    return null;
                    //break;
            
            }
            sqllistT.commandText.Append(")");

            if (sqllistT.BeginInt == sqllistT.i)
            {
                sqllistT.commandText.Clear();
            }

            //主查询
            SqlParameterTool sqllist = new SqlParameterTool();
            sqllist.BeginInt = sqllistT.i;
            sqllist.commandText.Append(@"
--declare @BeginTime datetime,@EndTime datetime;
declare @EventCodeB char(16),@EventCodeE char(16);
SELECT @EventCodeB = convert(char(8),@p" + sqllist.BeginInt + @",112)+'00000000' 
,@EventCodeE = convert(char(8),dateadd(day,1,@p" + (sqllist.BeginInt + 1) + @"),112)+'00000000';

select id=事件编码
,tel=首次呼救电话
,ori=tzaeo.名称
,accnum=受理次数
,alarmName=事件名称
,[type]=tzaet.名称	--事件类型
,[time]=首次受理时刻
,diaoduyuan=TP.姓名
,c_time=首次派车时刻
,chuche =TAE.执行任务总数
,swdd=(select 送往地点 from dbo.tacceptevent tac where tac.事件编码=tae.事件编码 and tac.受理序号=1)
,正常完成=(select count(*) from dbo.TTask tt where tt.事件编码=tae.事件编码 and tt.是否正常结束=1)
from  TAlarmEvent TAE
left join TPerson TP on TAE.首次调度员编码 = TP.编码
left join TZAlarmEventType tzaet on TAE.事件类型编码 = tzaet.编码
left join TZAlarmEventOrigin tzaeo on TAE.事件来源编码 = tzaeo.编码
where TAE.事件编码 between @EventCodeB and @EventCodeE");
            int strCount=sqllist.commandText.Length;
            string TAcceptEventCount = @"
declare @EventCodeB char(16),@EventCodeE char(16);
SELECT @EventCodeB = convert(char(8),@p" + sqllist.BeginInt + @",112)+'00000000' 
,@EventCodeE = convert(char(8),dateadd(day,1,@p" + (sqllist.BeginInt + 1) + @"),112)+'00000000';
select N=sum(受理次数) from TAlarmEvent TAE where TAE.事件编码 between @EventCodeB and @EventCodeE";
            string TTaskCount = @"
declare @EventCodeB char(16),@EventCodeE char(16);
SELECT @EventCodeB = convert(char(8),@p" + sqllist.BeginInt + @",112)+'00000000' 
,@EventCodeE = convert(char(8),dateadd(day,1,@p" + (sqllist.BeginInt + 1) + @"),112)+'00000000';
select N=sum(执行任务总数) from TAlarmEvent TAE where TAE.事件编码 between @EventCodeB and @EventCodeE";
            string TTaskNormalCount = @"
declare @EventCodeB char(16),@EventCodeE char(16);
SELECT @EventCodeB = convert(char(8),@p" + sqllist.BeginInt + @",112)+'00000000' 
,@EventCodeE = convert(char(8),dateadd(day,1,@p" + (sqllist.BeginInt + 1) + @"),112)+'00000000';
select N=count(*) from TAlarmEvent TAE
inner join TTask ttc on ttc.事件编码=tae.事件编码 and ttc.是否正常结束=1 where TAE.事件编码 between @EventCodeB and @EventCodeE";




            sqllist.commandText.Append(" and TAE.首次受理时刻>=").Append("@p" + sqllist.i++);
            sqllist.commandText.Append(" and TAE.首次受理时刻<").Append("@p" + sqllist.i++);

            //--------------------------
            sqllist.commandObj.Add(begin);
            sqllist.commandObj.Add(end);

            try
            {
                if (!string.IsNullOrEmpty(c_begin.Replace(" ", "")))
                {
                    sqllist.commandText.Append(" and TAE.首次派车时刻>=").Append("@p" + sqllist.i++);
                    sqllist.commandObj.Add(Convert.ToDateTime(c_begin));
                }
                if (!string.IsNullOrEmpty(c_end.Replace(" ", "")))
                {
                    sqllist.commandText.Append(" and TAE.首次派车时刻<").Append("@p" + sqllist.i++);
                    sqllist.commandObj.Add(Convert.ToDateTime(c_end));
                }
            }
            catch (Exception e)
            {
                
                return null;
            }
            bool? it;
            if (string.IsNullOrEmpty(IsTest))
            {
                it = null;
            }
            else {
                it = Convert.ToBoolean(Convert.ToInt32(IsTest));
            }
            sqllist.AddObjectEqual("TAE.是否测试", it);

            sqllist.AddObjectLike("TAE.首次呼救电话", tel);
            sqllist.AddObjectLike("TAE.首次调度员编码", Dis);
            sqllist.AddObjectLike("TAE.事件类型编码", type);
            //sqllist.AddObjectLike("事故类型编码",);
            sqllist.AddObjectEqual("TAE.事件来源编码", ori);
            sqllist.AddObjectLike("TAE.事件编码", AlarmEventCode);//

            if (Search == "SearchCenter")//查找受理中的本人
            {
                IApplicationContext ctx = ContextRegistry.GetContext();
                IWorker worker = ctx["Worker"] as IWorker;

                sqllist.AddObjectEqual("TAE.中心编码", worker.GetWorkerDetailById(WorkerID).CenterCode);
            }

            IEnumerable<object> lo = sqllistAc.commandObj.Concat(sqllistT.commandObj).Concat(sqllist.commandObj);
            //List<object> lo = //sqllistAc.commandObj.Concat(sqllistT.commandObj).Concat(sqllist.commandObj);

            using (MainDataContext dbContext = new MainDataContext())
            {
                sqllist.commandText.Append(sqllistAc.commandText).Append(sqllistT.commandText);
                string Csql = sqllist.commandText.ToString().Substring(strCount);
                string sql = sqllist.commandText.Append(" order by TAE.首次受理时刻 desc").ToString();

                var templist1 = dbContext.ExecuteQuery<C_AlarmEventSearch>(sql, lo.ToArray());
                var templist2 = dbContext.ExecuteQuery<C_AlarmEventSearch>(sql, lo.ToArray());


                Num TAcceptEventCountL = dbContext.ExecuteQuery<Num>(TAcceptEventCount + Csql, lo.ToArray()).First();
                Num TTaskCountL = dbContext.ExecuteQuery<Num>(TTaskCount + Csql, lo.ToArray()).First();
                Num TTaskNormalCountL = dbContext.ExecuteQuery<Num>(TTaskNormalCount + Csql, lo.ToArray()).First();
                long total = templist1.LongCount();

                templist2 = templist2.Skip((page - 1) * rows).Take(rows);
                var result = new { total = total, rows = templist2.ToList(),
                                   tacc = TAcceptEventCountL.N == null ? 0 : TAcceptEventCountL.N.Value,
                                   ttc = TTaskCountL.N == null ? 0 : TTaskCountL.N.Value,
                                   ttnc = TTaskNormalCountL.N.Value
                };
                return result;
            }
        }
        public class Num
        {
            public int? N;
        }
        /// <summary>
        /// 获取 基本数据库对象
        /// </summary>
        public static string AccLoad(string id, out TAlarmEvent tae, out List<TAcceptEvent> tacLs, out List<TTask> ttLs, out List<TAlarmCall> acLs)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                try
                {
                    tae = dbContext.TAlarmEvent.FirstOrDefault(t => t.事件编码 == id);
                    tacLs = dbContext.TAcceptEvent.Where(t => t.事件编码 == id).ToList();
                    ttLs = dbContext.TTask.Where(t => t.事件编码 == id).ToList();
                    acLs = dbContext.TAlarmCall.Where(t => t.事件编码 == id).ToList();
                    return "";
                }
                catch (Exception ex)
                {
                    //Log4Net.LogError("", ex.ToString());
                    tae = null; tacLs = null; ttLs = null; acLs = null;
                    return ex.ToString();
                }
            }
        }

        /// <summary>
        /// 获取 显示的数据库对象
        /// </summary>
        public static string getAlarmAllShow(string id, out C_AlarmEventInfo tae, out List<C_AccInfo> tacLs, out List<C_TaskInfoDetail> ttLs, out List<C_AmbulanceStateTimeInfo> tastLs, out List<C_AlarmCallInfo> acLs)
        {
            //查询事件信息
            SqlParameterTool sqltae = new SqlParameterTool();
            sqltae.commandText.Append(@"SELECT EventCode=事件编码,FirstAlarmCall=首次呼救电话,EvetnName=事件名称,FirstAcceptTime=首次受理时刻,FirstDisptcher=首次调度员编码,EventTypeCode=事件类型编码,EventSourceCode=事件来源编码,AccidentTypeCode=事故类型编码,AccidentLevelCode=事故等级编码
,AcceptCount=受理次数,FirstSendAmbTime=首次派车时刻,Area=区域,TransactTaskCount=执行任务总数,NonceTransactTaskCount=当前执行任务数,CancelAcceptCount=撤消受理数,BreakTaskCount=中止任务数
,HangUpTime=挂起时刻,IsHangUp=是否挂起,BespeakTime=预约时刻,EventType=TZET.名称,EventSource=TZEO.名称
,AccidentType=TZAT.名称,AccidentLevel=TZAL.名称,FirstDisptcherName=TP.姓名
 FROM TAlarmEvent TAE
 left join TZAlarmEventType TZET on TZET.编码 = TAE.事件类型编码
 left join TZAlarmEventOrigin TZEO on TZEO.编码 =TAE.事件来源编码
 left join TZAccidentType TZAT on TZAT.编码 = TAE.事故类型编码
 left join TZAccidentLevel TZAL on TZAL.编码 =TAE.事故等级编码
 left join TPerson TP on TP.编码=TAE.首次调度员编码
 WHERE TAE.事件编码=@p0");
            sqltae.commandObj.Add(id);
            //查询受理信息
            SqlParameterTool sqltac = new SqlParameterTool();
            sqltac.commandText.Append(@"SELECT EventCode=事件编码,AcceptOrder=受理序号,TypeId=受理类型编码,DetailReasonId=受理类型子编码,AcceptPersonCode=责任受理人编码,AlarmTel=呼救电话
,HangUpTime=挂起时刻,RingTime=电话振铃时刻,AcceptBeginTime=开始受理时刻,AcceptEndTime=结束受理时刻,CommandTime=发送指令时刻,LocalAddr=现场地址,WaitAddr=等车地址
,SendAddr=送往地点,LocalAddrTypeId=往救地点类型编码,SendAddrTypeId=送往地点类型编码,LinkMan=联系人,LinkTel=联系电话,Extension=分机,PatientName=患者姓名,Sex=性别
,Age=年龄,Folk=民族,[National]=国籍,AlarmReason=主诉,Judge=病种判断,IllStateId=病情编码,IsNeedLitter=是否需要担架,PatientCount=患者人数,SpecialNeed=特殊要求,IsLabeled=是否标注
,X=X坐标,Y=Y坐标,AmbulanceList=派车列表,Remark=备注,AcceptType=TZET.名称,Dispatcher=TP.姓名
,Reason=isnull(tzhur.名称,'')+isnull(tzrr.名称,'')+isnull(tzdr.名称,'')+isnull(tzrr1.名称,'')
,IllState=tzis.名称
,LocalAddrType=tzlat.名称
,SendAddrType=tzsat.名称
FROM TAcceptEvent tac
left join TZAcceptEventType TZET on TZET.编码 = tac.受理类型编码
left join TPerson TP on TP.编码 = tac.责任受理人编码
left join TZLocalAddrType tzlat on tzlat.编码=tac.往救地点类型编码
left join TZSendAddrType tzsat on tzsat.编码=tac.送往地点类型编码
left join TZIllState tzis on tzis.编码=tac.病情编码
left join TZHangUpReason tzhur on tac.受理类型子编码=tzhur.编码 and tac.受理类型编码 in(1,3)
left join TZRejectReason tzrr on tac.受理类型子编码=tzrr.编码 and tac.受理类型编码=4
left join TZDropReason tzdr on tac.受理类型子编码=tzdr.编码 and tac.受理类型编码=6
left join TZReassignmentReason tzrr1 on tac.受理类型子编码=tzrr1.编码 and tac.受理类型编码=7

 where 事件编码 = @p0");
            sqltac.commandObj.Add(id);

            //TypeId=受理类型编码
            //    DetailReasonId=受理类型子编码




            //查询任务信息
            SqlParameterTool sqltt = new SqlParameterTool();
            sqltt.commandText.Append(@"SELECT Code=TT.任务编码,EventCode=事件编码,AcceptOrder=受理序号,TaskOrder=TT.任务流水号,UserOrder=TT.用户流水号,OperatorCode=责任调度人编码
,AmbulanceCode=TT.车辆编码,IsPerforming=是否执行中,CreateTaskTime=生成任务时刻,ReceiveCmdTime=接收命令时刻,AmbulanceLeaveTime=出车时刻,ArriveSceneTime=到达现场时刻
,LeaveSceneTime=离开现场时刻,ArriveHospitalTime=到达医院时刻,FinishTime=完成时刻,ReturnTime=返回站中时刻,HelpDistance=急救公里数,行驶公里数=行驶公里数
,IsNormalFinish=是否正常结束,AbnormalReasonId=异常结束原因编码,CureAmount=实际救治人数,IsFromStation=是否站内出动,Driver=TT.司机
,Doctor=TT.医生,Nurse=TT.护士,Litter=TT.担架工,Salver=TT.抢救员,ReassignTaskCode=改派前任务编码,RealSendAddr=实际送往地点,Remark=备注,CenterId=TT.中心编码
,TS.名称 as StationName,TA.车牌号码 as TradeMark,TA.实际标识 as RealSign
,TA.随车电话 as FollowTel,TZA.名称 as AmbulanceTypeName,TZT.名称 as AbnormalReasonName
 FROM TTask TT
 left join TStation TS on TS.编码=TT.分站编码
 left join TAmbulance TA on TA.车辆编码 = TT.车辆编码
 left join TZAmbulanceType TZA on TZA.编码 = TA.车辆类型编码
 left join TZTaskAbendReason TZT on TZT.编码 = TT.异常结束原因编码
 where TT.事件编码=@p0");
            sqltt.commandObj.Add(id);

            //查询对应任务车辆状态变化信息
            SqlParameterTool sqltast = new SqlParameterTool();
            sqltast.commandText.Append(@"select Code=A.编码,TaskCode=A.任务编码,AmbuCode=A.车辆编码,RealSign=B.实际标识,WorkStateId=车辆状态编码,C.名称 as WorkStateName
,KeyPressTime=时刻值,SaveTime=记录时刻,SourceCode=操作来源编码,D.名称 as SourceName,OperationCode=操作员编码,E.姓名 as OperationName,JobCode=E.工号,IsOnline=车辆是否在线
from dbo.TAmbulanceStateTime A
left join dbo.TAmbulance B on A.车辆编码=B.车辆编码
left join dbo.TZAmbulanceState C on A.车辆状态编码=C.编码
left join dbo.TZOperationOrigin D on A.操作来源编码=D.编码
left join dbo.TPerson E on A.操作员编码=E.编码
where exists(select * from dbo.TTask tt where a.任务编码=tt.任务编码 and tt.事件编码=@p0)
order by C.顺序号");
            sqltast.commandObj.Add(id);

            //查询对应电话录音信息
            SqlParameterTool sqlac = new SqlParameterTool();
            sqlac.commandText.Append(@"SELECT EventCode=事件编码,CallTime=通话时刻,FinishTime=结束时刻,TD.显示名称 as DeskName,
DispatcherCode=A.调度员编码,B.姓名 as DispatcherName,TelNumber=主叫号码,RecordCode=录音号, 
C.名称 as CallTypeName,IsOut=是否呼出,Remark=备注,CenterId=A.中心编码
,DeskCode=A.台号
--,D.名称 as CenterName
from TAlarmCall A left join TPerson B on 调度员编码=B.编码 
left join TZAlarmCallType C on 通话类型编码=C.编码
--left join TCenter D on A.中心编码=D.编码
left join TDesk TD on A.台号 = TD.台号
where 事件编码=@p0");
            sqlac.commandObj.Add(id);

            tae = null; tacLs = null; ttLs = null; tastLs = null; acLs = null;
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {

                try
                {
                    tae = dbContext.ExecuteQuery<C_AlarmEventInfo>(
                    sqltae.commandText.ToString()
                    , sqltae.commandObj.ToArray()).FirstOrDefault();

                    tacLs = dbContext.ExecuteQuery<C_AccInfo>(
                    sqltac.commandText.ToString()
                    , sqltac.commandObj.ToArray()).ToList();

                    //MPDS备注
                    string sql="select name from syscolumns where id=object_id('TAcceptEvent') and name='MPDS备注'";

                    var result = dbContext.ExecuteQuery<string>(sql);

                    if (result.Count() > 0)
                    {
                        tacLs = dbContext.ExecuteQuery<C_AccInfo>(
                        sqltac.commandText.ToString().Replace("SELECT", "SELECT MPDSRemark=MPDS备注,")
                        , sqltac.commandObj.ToArray()).ToList();
                    }
                    else
                    {
                        tacLs = dbContext.ExecuteQuery<C_AccInfo>(
                        sqltac.commandText.ToString()
                        , sqltac.commandObj.ToArray()).ToList();

                    }

                    ttLs = dbContext.ExecuteQuery<C_TaskInfoDetail>(
                    sqltt.commandText.ToString()
                    , sqltt.commandObj.ToArray()).ToList();

                    tastLs = dbContext.ExecuteQuery<C_AmbulanceStateTimeInfo>(
                    sqltast.commandText.ToString()
                    , sqltast.commandObj.ToArray()).ToList();

                    acLs = dbContext.ExecuteQuery<C_AlarmCallInfo>(
                    sqlac.commandText.ToString()
                    , sqlac.commandObj.ToArray()).ToList();
                    return "";
                }
                catch (Exception ex)
                {
                    //Log4Net.LogError("", ex.ToString());
                    return ex.ToString();
                }
            }
        }


        /// <summary>
        /// 获取事件对象
        /// </summary>
        public static C_AlarmEventInfo GetAlarmInfo(string eventCode)
        {
            SqlParameterTool sqltae = new SqlParameterTool();
            sqltae.commandText.Append(@"SELECT EventCode=事件编码,FirstAlarmCall=首次呼救电话,EvetnName=事件名称,FirstAcceptTime=首次受理时刻,FirstDisptcher=首次调度员编码,EventTypeCode=事件类型编码,EventSourceCode=事件来源编码,AccidentTypeCode=事故类型编码,AccidentLevelCode=事故等级编码
,AcceptCount=受理次数,FirstSendAmbTime=首次派车时刻,Area=区域,TransactTaskCount=执行任务总数,NonceTransactTaskCount=当前执行任务数,CancelAcceptCount=撤消受理数,BreakTaskCount=中止任务数
,HangUpTime=挂起时刻,IsHangUp=是否挂起,BespeakTime=预约时刻,EventType=TZET.名称,EventSource=TZEO.名称
,AccidentType=TZAT.名称,AccidentLevel=TZAL.名称,FirstDisptcherName=TP.姓名
 FROM TAlarmEvent TAE
 left join TZAlarmEventType TZET on TZET.编码 = TAE.事件类型编码
 left join TZAlarmEventOrigin TZEO on TZEO.编码 =TAE.事件来源编码
 left join TZAccidentType TZAT on TZAT.编码 = TAE.事故类型编码
 left join TZAccidentLevel TZAL on TZAL.编码 =TAE.事故等级编码
 left join TPerson TP on TP.编码=TAE.首次调度员编码
 WHERE TAE.事件编码=@p0");
            sqltae.commandObj.Add(eventCode);


            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {

                C_AlarmEventInfo tae = dbContext.ExecuteQuery<C_AlarmEventInfo>(
sqltae.commandText.ToString()
, sqltae.commandObj.ToArray()).FirstOrDefault();

                //var list = (from p in dbContext.TAlarmEvent
                //            join o2 in dbContext.TPerson on p.首次调度员编码 equals o2.编码
                //            join o3 in dbContext.TZAlarmEventOrigin on p.事件来源编码 equals o3.编码
                //            join o4 in dbContext.TZAlarmEventType on p.事件类型编码 equals o4.编码
                //            join o5 in dbContext.TZAccidentType on p.事故类型编码 equals o5.编码
                //            where p.事件编码 == eventCode
                //            select new
                //            {
                //                Name = p.事件名称,
                //                FirstAccTime = p.首次受理时刻,
                //                FirstDis = o2.姓名,
                //                FirstTel = p.首次呼救电话,
                //                AccTimes = p.受理次数,
                //                FirstSendTime = p.首次派车时刻,
                //                Area = p.区域,
                //                Ori = o3.名称,
                //                Type = o4.名称,
                //                AccidentType = o5.名称,
                //            }).First();
                //C_AlarmEventInfo result = new C_AlarmEventInfo();
                //result.Name = list.Name;
                //result.FirstAccTime = list.FirstAccTime.ToString();
                //result.FirstDis = list.FirstDis;
                //result.FirstTel = list.FirstTel;
                //result.AccTimes = list.AccTimes.ToString();
                //result.FirstSendTime = list.FirstSendTime.ToString();
                //result.Area = list.Area;
                //result.Ori = list.Ori;
                //result.Type = list.Type;
                //result.AccidentType = list.AccidentType;
                return tae;
            }
        }
        public static C_AccInfo GetAccInfo(string eventCode, int order)
        {
            SqlParameterTool sqltac = new SqlParameterTool();
            sqltac.commandText.Append(@"SELECT EventCode=事件编码,AcceptOrder=受理序号,TypeId=受理类型编码,DetailReasonId=受理类型子编码,AcceptPersonCode=责任受理人编码,AlarmTel=呼救电话
,HangUpTime=挂起时刻,RingTime=电话振铃时刻,AcceptBeginTime=开始受理时刻,AcceptEndTime=结束受理时刻,CommandTime=发送指令时刻,LocalAddr=现场地址,WaitAddr=等车地址
,SendAddr=送往地点,LocalAddrTypeId=往救地点类型编码,SendAddrTypeId=送往地点类型编码,LinkMan=联系人,LinkTel=联系电话,Extension=分机,PatientName=患者姓名,Sex=性别
,Age=年龄,Folk=民族,[National]=国籍,AlarmReason=主诉,Judge=病种判断,IllStateId=病情编码,IsNeedLitter=是否需要担架,PatientCount=患者人数,SpecialNeed=特殊要求,IsLabeled=是否标注
,X=X坐标,Y=Y坐标,AmbulanceList=派车列表,Remark=备注,AcceptType=TZET.名称,Dispatcher=TP.姓名
 FROM TAcceptEvent TAE
 inner join TZAcceptEventType TZET on TZET.编码 = TAE.受理类型编码
 inner join TPerson TP on TP.编码 = TAE.责任受理人编码
 where 事件编码 = @p0 and 受理序号 = @p1");
            sqltac.commandObj.Add(eventCode);

            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                C_AccInfo tacLs = dbContext.ExecuteQuery<C_AccInfo>(
                    sqltac.commandText.ToString()
                    , sqltac.commandObj.ToArray()).FirstOrDefault();
                //var list = (from p in dbContext.TAcceptEvent
                //            join o2 in dbContext.TPerson on p.责任受理人编码 equals o2.编码
                //            join o3 in dbContext.TZAcceptEventType on p.受理类型编码 equals o3.编码
                //            where p.事件编码 == eventCode && p.受理序号 == order
                //            select new
                //            {
                //                Tel = p.呼救电话,
                //                ZhuSu = p.主诉,
                //                Name = p.患者姓名,
                //                Sex = p.性别,
                //                Age = p.年龄,
                //                Nationnality = p.国籍,
                //                Nation = p.民族,
                //                Connector = p.联系人,
                //                ConnectTel = p.联系电话,
                //                LocalAddr = p.现场地址,
                //                TotalAddr = p.送往地点,
                //                Dispatcher = o2.姓名,
                //                Type = o3.名称,
                //                RingTime = p.电话振铃时刻,
                //                BeginTime = p.开始受理时刻,
                //                EndTime = p.结束受理时刻,
                //                SendTime = p.发送指令时刻,
                //                CarList = p.派车列表,
                //                Remark = p.备注,
                //            }).First();
                //C_AccInfo result = new C_AccInfo();
                //result.Tel = list.Tel;
                //result.ZhuSu = list.ZhuSu;
                //result.Name = list.Name;
                //result.Sex = list.Sex;
                //result.Age = list.Age;
                //result.Nationnality = list.Nationnality;
                //result.Nation = list.Nation;
                //result.Connector = list.Connector;
                //result.ConnectTel = list.ConnectTel;
                //result.LocalAddr = list.LocalAddr;
                //result.TotalAddr = list.TotalAddr;
                //result.Dispatcher = list.Dispatcher;
                //result.Type = list.Type;
                //result.RingTime = list.RingTime.ToString();
                //result.BeginTime = list.BeginTime.ToString();
                //result.EndTime = list.EndTime.ToString();
                //result.SendTime = list.SendTime.ToString();
                //result.CarList = list.CarList;
                //result.Remark = list.Remark;
                return tacLs;
            }
        }
        public static object GetAccTel(string code)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = (from p in dbContext.TAlarmCall
                            join o1 in dbContext.TDesk on p.台号 equals o1.台号
                            join o2 in dbContext.TPerson on p.调度员编码 equals o2.编码
                            join o3 in dbContext.TZAlarmCallType on p.通话类型编码 equals o3.编码
                            where p.事件编码 == code
                            select new
                            {
                                tel = p.主叫号码,
                                begintime = p.通话时刻,
                                endtime = p.结束时刻,
                                desk = o1.显示名称,
                                dis = o2.姓名,
                                recordcode = p.录音号,
                                result = o3.名称,
                                test = "<img src='../../Content/images/play.gif' width='20px' onClick='javascript:play(\"" + p.录音号 + "\");'>",
                            });
                long total = list.LongCount();
                list = list.OrderByDescending(p => p.begintime);
                var list2 = list.ToList().Select(o => new
                {
                    tel = o.tel,
                    begintime = o.begintime.ToString(),
                    endtime = o.endtime.ToString(),
                    desk = o.desk,
                    dis = o.dis,
                    recordcode = o.recordcode,
                    result = o.result,
                    Test = o.recordcode == null ? "" : o.test,
                });
                var result = new { total = total, rows = list2.ToList() };

                return result;
            }
        }
        public static object AumLoad(string code, int order)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = (from p in dbContext.TTask
                            join o1 in dbContext.TStation on p.分站编码 equals o1.编码
                            join o2 in dbContext.TAmbulance on p.车辆编码 equals o2.车辆编码
                            where p.事件编码 == code && p.受理序号 == order
                            select new
                            {
                                code = p.任务编码,
                                aum = o2.实际标识,
                                sta = o1.名称,
                                mark = o2.车牌号码,
                                end = p.是否正常结束 ? "是" : "否",
                                test = "<img src='../../Content/images/tip.png' width='20px' onClick='javascript:detail(\"" + p.任务编码 + "\");'>",
                            });
                long total = list.LongCount();
                list = list.OrderByDescending(p => p.code);
                var result = new { total = total, rows = list.ToList() };
                return result;
            }
        }
        public static C_TaskInfo GetTaskInfo(string Code)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = (from p in dbContext.TTask
                            join o2 in dbContext.TAmbulance on p.车辆编码 equals o2.车辆编码
                            join o3 in dbContext.TStation on p.分站编码 equals o3.编码
                            join o4 in dbContext.TZTaskAbendReason on p.异常结束原因编码 equals o4.编码 into temp
                            from q in temp.DefaultIfEmpty()
                            where p.任务编码 == Code
                            select new
                            {
                                TaskCode = p.任务编码,
                                Aum = o2.实际标识,
                                AumMark = o2.车牌号码,
                                Tel = o2.随车电话,
                                Sta = o3.名称,
                                ReceiveTime = p.接收命令时刻,
                                StartTime = p.出车时刻,
                                ArriveTime = p.到达现场时刻,
                                TakeTime = p.离开现场时刻,
                                HosTime = p.到达医院时刻,
                                EndTime = p.完成时刻,
                                IsEnd = p.是否正常结束 ? "是" : "否",
                                Reason = q.名称,
                                Dri = p.司机,
                                Doc = p.医生,
                                Nur = p.护士,
                            }).First();
                C_TaskInfo result = new C_TaskInfo();
                result.TaskCode = list.TaskCode;
                result.Aum = list.Aum;
                result.AumMark = list.AumMark;
                result.Tel = list.Tel;
                result.Sta = list.Sta;
                result.ReceiveTime = list.ReceiveTime.ToString();
                result.StartTime = list.StartTime.ToString();
                result.ArriveTime = list.ArriveTime.ToString();
                result.TakeTime = list.TakeTime.ToString();
                result.HosTime = list.HosTime.ToString();
                result.EndTime = list.EndTime.ToString();
                result.IsEnd = list.IsEnd;
                result.Reason = list.Reason == null ? "" : list.Reason;
                result.Dri = list.Dri;
                result.Doc = list.Doc;
                result.Nur = list.Nur;
                return result;
            }
        }

        #region 修改录音相关

        /// <summary>
        /// 关联到事件相关 通话类型
        /// </summary>
        /// <returns></returns>
        public static List<TZAlarmCallType> LoadAlarmCallType()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZAlarmCallType.Where(t => t.是否有效 == true).OrderBy(t => t.顺序号).ToList();
            }
        }
        /// <summary>
        /// 取消关联到事件 通话类型
        /// </summary>
        /// <returns></returns>
        public static List<TZCallType> LoadCallType()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZCallType.Where(t => t.是否有效 == true).OrderBy(t => t.顺序号).ToList();
            }
        }
        /// <summary>
        /// 取消录音与事件的关联
        /// </summary>
        public static string UnLinkCalls(DateTime tonghuashike, string taihao, string callTypeCode)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                try
                {
                    TAlarmCall originInfo = (from a in dbContext.TAlarmCall
                                             where a.通话时刻.Year == tonghuashike.Year && a.通话时刻.Month == tonghuashike.Month && a.通话时刻.Day == tonghuashike.Day
                                                    && a.通话时刻.Hour == tonghuashike.Hour && a.通话时刻.Minute == tonghuashike.Minute && a.通话时刻.Second == tonghuashike.Second
                                                    && a.台号 == taihao
                                             select a).FirstOrDefault();
                    TAlarmCallOther acoInfo = new TAlarmCallOther();

                    acoInfo.通话时刻 = originInfo.通话时刻;
                    acoInfo.振铃时刻 = originInfo.振铃时刻;
                    acoInfo.结束时刻 = originInfo.结束时刻;
                    acoInfo.台号 = originInfo.台号;
                    acoInfo.调度员编码 = originInfo.调度员编码;
                    acoInfo.主叫号码 = originInfo.主叫号码;
                    acoInfo.录音号 = originInfo.录音号;
                    acoInfo.是否呼出 = originInfo.是否呼出;
                    acoInfo.备注 = originInfo.备注;
                    acoInfo.中心编码 = originInfo.中心编码;
                    //ReflectionUtility.CopyObjectProperty<TAcceptEvent>(entity, model);

                    acoInfo.通话类型编码 = Convert.ToInt32(callTypeCode);//通话类型编码
                    //先插入到TAlarmCallOther
                    dbContext.TAlarmCallOther.InsertOnSubmit(acoInfo);
                    //然后删除TAlarmCall
                    dbContext.TAlarmCall.DeleteOnSubmit(originInfo);
                    dbContext.SubmitChanges();
                    return "";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

        }

        /// <summary>
        /// 关联录音与事件
        /// </summary>
        public static string LinkCalls(DateTime tonghuashike, string taihao, string eventCode, string callTypeCode)
        {

            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                try
                {
                    TAlarmCallOther originInfo = (from a in dbContext.TAlarmCallOther
                                                  where a.通话时刻.Year == tonghuashike.Year && a.通话时刻.Month == tonghuashike.Month && a.通话时刻.Day == tonghuashike.Day
                                                     && a.通话时刻.Hour == tonghuashike.Hour && a.通话时刻.Minute == tonghuashike.Minute && a.通话时刻.Second == tonghuashike.Second
                                                     && a.台号 == taihao
                                                  select a).FirstOrDefault();
                    TAlarmCall acInfo = new TAlarmCall();
                    acInfo.通话时刻 = originInfo.通话时刻;
                    acInfo.振铃时刻 = originInfo.振铃时刻;
                    acInfo.结束时刻 = originInfo.结束时刻;
                    acInfo.台号 = originInfo.台号;
                    acInfo.调度员编码 = originInfo.调度员编码;
                    acInfo.主叫号码 = originInfo.主叫号码;
                    acInfo.录音号 = originInfo.录音号;
                    acInfo.是否呼出 = originInfo.是否呼出;
                    acInfo.备注 = originInfo.备注;
                    acInfo.中心编码 = originInfo.中心编码;
                    //ReflectionUtility.CopyObjectProperty<TAcceptEvent>(entity, model);
                    acInfo.通话类型编码 = Convert.ToInt32(callTypeCode);//通话类型编码
                    acInfo.事件编码 = eventCode;//事件编码
                    //先插入到TAlarmCall
                    dbContext.TAlarmCall.InsertOnSubmit(acInfo);
                    //然后删除TAlarmCallOther
                    dbContext.TAlarmCallOther.DeleteOnSubmit(originInfo);
                    dbContext.SubmitChanges();
                    return "";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }


        /// <summary>
        /// 查询事件无关的电话
        /// </summary>
        public static object GetAlarmCallOthers(int pageIndex, int pageSize, DateTime m_BeginTime, DateTime m_EndTime,
            string deskNumber, string attemperCode, string callTypeCode, string callNumber, string recordNumber, string isCallOut
            , string remark,Anchor.FA.Utility.ButtonPower bp, C_WorkerDetail userDetail)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var Templist = (from a in dbContext.TAlarmCallOther
                                  join t in dbContext.TPerson on a.调度员编码 equals t.编码 into Leftjoin1
                                  from p in Leftjoin1.DefaultIfEmpty()
                                  join t in dbContext.TZCallType on a.通话类型编码 equals t.编码 into Leftjoin2
                                  from zc in Leftjoin2.DefaultIfEmpty()
                                  join t in dbContext.TCenter on a.中心编码 equals t.编码 into Leftjoin3
                                  from c in Leftjoin3.DefaultIfEmpty()
                                  join t in dbContext.TDesk on a.台号 equals t.台号 into Leftjoin4
                                  from d in Leftjoin4.DefaultIfEmpty()
                                  where a.通话时刻 >= m_BeginTime && a.通话时刻 < m_EndTime
                                    orderby a.通话时刻 descending
                                  select new {
                                      a.台号,
                                      a.调度员编码,
                                      a.通话类型编码,
                                      a.主叫号码,
                                      a.录音号,
                                      a.是否呼出,
                                      a.备注,
                                      a.通话时刻,
                                      a.结束时刻,
                                      a.中心编码,
                                      //显示名称 = d == null ? null : d.显示名称,
                                      //调度员 = p == null ? null : p.姓名,
                                      //通话类型 = zc == null ? null : zc.名称,
                                      //中心名称 = c == null ? null : c.名称,
                                      显示名称 = d.显示名称,
                                      调度员 = p.姓名,
                                      通话类型 = zc.名称,
                                      中心名称 = c.名称,
                                  });

                //动态Where
                if (!string.IsNullOrEmpty(deskNumber))
                {
                    Templist = Templist.Where(p => p.台号 == deskNumber);
                }
                if (!string.IsNullOrEmpty(attemperCode))
                {
                    Templist = Templist.Where(p => p.调度员编码 == attemperCode);
                }
                if (!string.IsNullOrEmpty(callTypeCode))
                {
                    Templist = Templist.Where(p => p.通话类型编码 == Convert.ToInt32(callTypeCode));
                }
                if (!string.IsNullOrEmpty(callNumber))
                {
                    Templist = Templist.Where(p => p.主叫号码.Contains(callNumber));
                }
                if (!string.IsNullOrEmpty(recordNumber))
                {
                    Templist = Templist.Where(p => p.录音号.Contains(recordNumber));
                }
                if (!string.IsNullOrEmpty(isCallOut))
                {
                    Templist = Templist.Where(p => p.是否呼出 == Convert.ToBoolean(isCallOut));
                }
                if (!string.IsNullOrEmpty(remark))
                {
                    Templist = Templist.Where(p => p.备注.Contains(remark));
                }

                switch (bp.GetGroupRangePower("searchBound"))
                {
                    case "SearchAll"://查找所属分中心
                        break;
                    case "SearchCenter"://查找所属分中心
                        Templist = Templist.Where(t => t.中心编码 == userDetail.CenterCode);
                        break;
                    default://没有设置查询权限
                        return null;
                }

                long total = Templist.LongCount();
                Templist = Templist.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                var result = new { total = total, rows = Templist.ToList() };
                return result;
            }
        } 
        #endregion

        public static DataSet GetModifyRecord(string CNumber)
        {
            string Sql = @"select 车牌号 = ta.车牌号码,任务编码 = ta.任务编码, 生成任务时刻 = tt.生成任务时刻,接受命令时刻=tt.接收命令时刻,
                                呼救电话 = tae.呼救电话, 现场地址= tae.现场地址, 等车地址 = tae.等车地址, 联系人=tae.联系人, 联系电话 = tae.联系电话,
                                分机=tae.分机, 患者姓名 = tae.患者姓名, 性别 = tae.性别, 年龄 = tae.年龄, 民族 = tae.民族, 国籍 = tae.国籍, 主诉 = tae.主诉,
                                病情判断 = tae.病种判断, 送往地点=tae.送往地点,病情编码 = tae.病情编码, 是否需要担架 = tae.是否需要担架, 患者人数 = tae.患者人数, 特殊要求 = tae.特殊要求,
                                派车列表 = tae.派车列表, 医生= tpl.人员编码,医生姓名=tt.医生,司机姓名=tt.司机, tac.X坐标,tac.Y坐标,tac.是否标注, 
                                 出车时刻= tt.出车时刻 ,到达现场时刻 = tt.到达现场时刻 ,到达患者时刻 =tt.到达现场时刻,离开现场时刻 =tt.离开现场时刻 ,到达目的地时刻 =tt.到达医院时刻 

                                from dbo.TTask tt
                                left join dbo.TAmbulance ta on tt.车辆编码=ta.车辆编码
                                left join dbo.TAlarmEvent tac on tac.事件编码 = tt.事件编码
                                left join dbo.TAcceptEvent tae on tae.事件编码 = tt.事件编码 and tae.受理序号=tt.受理序号
                                left join dbo.TTaskPersonLink tpl on tpl.任务编码 = tt.任务编码 and tpl.人员类型编码  = 4
                                where ta.车牌号码 = '" + CNumber + "' and tt.是否执行中=1 ";

            DataSet ds = SQLHelper.ExecuteDataSet(AppConfig.ConnectionStringDispatch, CommandType.Text, Sql);
            return ds;
        }
    }
}

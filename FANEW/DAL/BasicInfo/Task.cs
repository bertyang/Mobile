using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Anchor.FA.Model;
using System.Data.Linq;
using System.Linq.Expressions;
using Anchor.FA.Utility;
using Anchor.FA.BLL.IBLL;
using Spring.Context;
using Spring.Context.Support;
using System.Reflection;


namespace Anchor.FA.DAL.BasicInfo
{
    public class Task
    {

        /// <summary>
        /// 异常结束原因下拉菜单(树)填充
        /// </summary>
        /// <returns></returns>
        public static List<TZTaskAbendReason> LoadTaskAbendReasons()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZTaskAbendReason.Where(p => p.是否有效 == true).OrderBy(p => p.顺序号).ToList();
            }
        }


        #region 修改

        public static string Update(TTask newEntity, string Driver, string Doctor, string Nurse, string Litter, string Salver, string AmbulanceStateTime1, string AmbulanceStateTime2, string AmbulanceStateTime3, string AmbulanceStateTime4, string AmbulanceStateTime5, string AmbulanceStateTime6, string AmbulanceStateTime7
            ,ButtonPower p,C_WorkerDetail UserInfo)
        {
            int workID = UserInfo.W.ID;
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                try
                {
                    TTask entity = dbContext.TTask.FirstOrDefault(t => t.任务编码 == newEntity.任务编码);
                    if (entity == null)
                    {
                        return "找不到该任务，无法修改！";
                    }
                    List<TTaskPersonLink> LtPersonLink = new List<TTaskPersonLink>();
                    TAmbulance ambInfo = Ambulance.GetAmbulanceInfo(newEntity.车辆编码);
                    newEntity.分站编码 = ambInfo.分站编码;

                    #region 所有对象处理 和List<TAmbulanceStateTime>对象新增
                    C_WorkerDetail CWD = Anchor.FA.DAL.Organize.Worker.GetWorkerDetailById(Convert.ToInt32(workID));
                    string PersonCode = CWD.PersonCode.FirstOrDefault();
                    List<TModifyRecord> mriList = new List<TModifyRecord>();

                    //如果 （车辆可编辑）
                    if (true)
                    {
                        Task.UpdateTaskAmb(entity, newEntity, PersonCode, mriList);
                    }
                    //如果 （人员可编辑）
                    if (true)
                    {
                        List<string> LtDriver;
                        if (Driver.Length == 0)
                            LtDriver = new List<string>();
                        else
                            LtDriver = Driver.Split(',').ToList();
                        List<string> LtDovtor;
                        if (Doctor.Length == 0)
                            LtDovtor = new List<string>();
                        else
                            LtDovtor = Doctor.Split(',').ToList();
                        List<string> LtNures;
                        if (Nurse.Length == 0)
                            LtNures = new List<string>();
                        else
                            LtNures = Nurse.Split(',').ToList();
                        List<string> LtLitter;
                        if (Litter.Length == 0)
                            LtLitter = new List<string>();
                        else
                            LtLitter = Litter.Split(',').ToList();
                        List<string> LtSalver;
                        if (Salver.Length == 0)
                            LtSalver = new List<string>();
                        else
                            LtSalver = Salver.Split(',').ToList();

                        Task.UpdateTaskPerson(entity,newEntity,PersonCode, mriList, LtPersonLink
                            , LtDriver, LtDovtor, LtNures, LtLitter, LtSalver);

                        var lsPL = dbContext.TTaskPersonLink.Where(t => t.任务编码 == newEntity.任务编码);
                        if (lsPL.Any())
                        {
                            //Entity FrameWork 居然没有批量删除与新增 汗
                            //foreach (TTaskPersonLink pl in lsPL)
                            //{
                            //    dbContext.TTaskPersonLink.DeleteOnSubmit(pl);
                            //}
                            dbContext.TTaskPersonLink.DeleteAllOnSubmit(lsPL);
                        }
                        dbContext.TTaskPersonLink.InsertAllOnSubmit(LtPersonLink);
                        //foreach (TTaskPersonLink pl in LtPersonLink)
                        //{
                        //    dbContext.TTaskPersonLink.InsertOnSubmit(pl);
                        //}
                    }
                    //如果 EditTimeNode （各时间字段可编辑）
                    bool EditTimeNode = p.IsHaveRangePower("EditTimeNode");
                    List<TAmbulanceStateTime> LtAmbStateTime = new List<TAmbulanceStateTime>();
                    if (EditTimeNode)
                    {
                        int MaxTAmbulanceStateTimeID;
                        var query = from am in dbContext.TAmbulanceStateTime
                                    select am.编码;
                        if (query.Any())
                            MaxTAmbulanceStateTimeID = query.Max() + 1;
                        else
                            MaxTAmbulanceStateTimeID = 1;

                        //string OperatePersonCode = LoadWorkerRole(Convert.ToInt32(workID)).EmpNo;

                        Task.UpdateTaskStateTime(entity, newEntity, mriList, LtAmbStateTime, MaxTAmbulanceStateTimeID, ambInfo
                            , AmbulanceStateTime1
                            , AmbulanceStateTime2
                            , AmbulanceStateTime3
                            , AmbulanceStateTime4
                            , AmbulanceStateTime5
                            , AmbulanceStateTime6
                            , AmbulanceStateTime7
                            , PersonCode
                            );

                        //TAmbulanceStateTime 不删除只新增保留历史数据
                        //foreach (TAmbulanceStateTime AmbST in LtAmbStateTime)
                        //{
                        //    dbContext.TAmbulanceStateTime.InsertOnSubmit(AmbST);
                        //}
                        dbContext.TAmbulanceStateTime.InsertAllOnSubmit(LtAmbStateTime);
                    }
                    //如果 IsTaskAbdReasonEdit （是否正常结束,异常结束原因编码,备注可编辑）
                    if (true)
                    {
                        Task.UpdateTaskAbdReason(entity, newEntity, PersonCode, mriList);
                    }
                    #endregion
                    #region 数据修改

                    dbContext.TModifyRecord.InsertAllOnSubmit(mriList);
                    dbContext.SubmitChanges();
                    #endregion
                    return "";
                }
                catch (Exception ex)
                {
                    //Log4Net.LogError("", ex.ToString());
                    return ex.ToString();
                }
            }
        }


        ///// <summary>
        ///// 只好使用 workerId 得到对应的第一个工号
        ///// </summary>
        //public static B_WORKER_ROLE LoadWorkerRole(int workerId)
        //{
        //    using (MainDataContext dbContext = new MainDataContext())
        //    {
        //        return dbContext.B_WORKER_ROLE.Where(t => t.WorkerID == workerId).FirstOrDefault();
        //    }
        //}
        #region 所有保存前对象处理函数
        /// <summary>
        /// 暂不进行数据库操作(只修改对象) 分块修改对象 更灵活 提高效率
        /// 修改 TTask和 List<TTaskPersonLink> 对象 修改人员
        /// </summary>
        public static void UpdateTaskPerson(TTask entity, TTask newEntity,string operatorCode, List<TModifyRecord> mriList, List<TTaskPersonLink> LtPersonLink
            , List<string> LtDriver, List<string> LtDovtor, List<string> LtNures, List<string> LtLitter, List<string> LtSalver)
        {
            //任务编码 是不可修改的所以entity.任务编码==newEntity.任务编码
            foreach (string item in LtDriver)
            {
                LtPersonLink.Add(TaskPersonLink.InsertTaskPerson(entity.任务编码, item));
            }
            foreach (string item in LtDovtor)
            {
                LtPersonLink.Add(TaskPersonLink.InsertTaskPerson(entity.任务编码, item));
            }
            foreach (string item in LtNures)
            {
                LtPersonLink.Add(TaskPersonLink.InsertTaskPerson(entity.任务编码, item));
            }
            foreach (string item in LtLitter)
            {
                LtPersonLink.Add(TaskPersonLink.InsertTaskPerson(entity.任务编码, item));
            }
            foreach (string item in LtSalver)
            {
                LtPersonLink.Add(TaskPersonLink.InsertTaskPerson(entity.任务编码, item));
            }
            ModifyTask2(entity, newEntity, operatorCode, mriList);
            //entity.司机 = Driver;
            //entity.医生 = Doctor;
            //entity.护士 = Nures;
            //entity.担架工 = Litter;
        }

        ///// <summary>
        ///// 暂不进行数据库操作(只修改对象) 分块修改对象 更灵活 提高效率
        ///// 修改 TTask 对象 车辆
        ///// </summary>
        //public static void UpdateTaskAmb(TTask entity, TAmbulance ambInfo)
        //{
        //    //TAmbulance ambInfo = Ambulance.GetAmbulanceInfo(ambCode);
        //    entity.分站编码 = ambInfo.分站编码;
        //    entity.车辆编码 = ambInfo.车辆编码;
        //}
        /// <summary>
        /// 修改 TTask 对象 车辆
        /// </summary>
        public static void UpdateTaskAmb(TTask entity, TTask newEntity, string operatorCode, List<TModifyRecord> mriList)
        {
            ModifyTask2(entity, newEntity, operatorCode, mriList);
        }

        /// <summary>
        /// 暂不进行数据库操作(只修改对象) 分块修改对象 更灵活 提高效率
        /// 修改 TTask 对象 是否正常结束
        /// </summary>
        public static void UpdateTaskAbdReason(TTask OldEntity, TTask entity, string PersonCode, List<TModifyRecord> mriList)
        {
            //entity.是否正常结束 = Convert.ToBoolean(Convert.ToInt32(taskResult));//是否正常结束 
            if (entity.是否正常结束)
            {
                entity.异常结束原因编码 = -1;
                entity.备注 = "";
            }
            //else
            //{
            //    entity.异常结束原因编码 = Convert.ToInt32(taskAbdReasonId);
            //    entity.备注 = remark;
            //}
            ModifyTask1(OldEntity, entity, PersonCode, mriList);
        }

        /// <summary>
        /// 暂不进行数据库操作(只修改对象) 分块修改对象 更灵活 提高效率
        /// 修改 TTask 对象 各时间字段修改 添加  TAmbulanceStateTime list 对象
        /// </summary>
        public static void UpdateTaskStateTime(TTask Oldtaskinfo,TTask taskinfo,List<TModifyRecord> mriList, List<TAmbulanceStateTime> LtAmbStateTime, int MaxTAmbulanceStateTimeID, TAmbulance ambInfo, string Time1
            , string Time2, string Time3, string Time4, string Time5
            , string Time6, string Time7, string OperatePersonCode)
        {


            //var query = from am in db.AmbulanceStateTimeInfos
            //            select am.Code;
            //if (query.Count() > 0)
            //    ambulanceStateTimeInfo.Code = query.Max() + 1;
            //else
            //    ambulanceStateTimeInfo.Code = 1;

            //taskinfo 是新的model 里面并没有存储 接收命令时刻 。。。。
            TaskStateTimeNeed receiveCmdTime = compare(Oldtaskinfo.接收命令时刻, Time1);
            if (receiveCmdTime.NeedUpdate)
            {
                //taskinfo.接收命令时刻 = receiveCmdTime.TaskTime;
                taskinfo.接收命令时刻 = receiveCmdTime.TaskStateTime;
                LtAmbStateTime.Add(getAmbStateTimeInfo(MaxTAmbulanceStateTimeID, taskinfo.任务编码, ambInfo, 1, receiveCmdTime.TaskStateTime, OperatePersonCode));
                MaxTAmbulanceStateTimeID++;
            }
            TaskStateTimeNeed ambulanceLeaveTime = compare(Oldtaskinfo.出车时刻, Time2);
            if (ambulanceLeaveTime.NeedUpdate)
            {
                taskinfo.出车时刻 = ambulanceLeaveTime.TaskStateTime;
                LtAmbStateTime.Add(getAmbStateTimeInfo(MaxTAmbulanceStateTimeID, taskinfo.任务编码, ambInfo, 2, ambulanceLeaveTime.TaskStateTime, OperatePersonCode));
                MaxTAmbulanceStateTimeID++;
            }

            TaskStateTimeNeed arriveSceneTime = compare(Oldtaskinfo.到达现场时刻, Time3);
            if (arriveSceneTime.NeedUpdate)
            {
                taskinfo.到达现场时刻 = arriveSceneTime.TaskStateTime;
                LtAmbStateTime.Add(getAmbStateTimeInfo(MaxTAmbulanceStateTimeID, taskinfo.任务编码, ambInfo, 3, arriveSceneTime.TaskStateTime, OperatePersonCode));
                MaxTAmbulanceStateTimeID++;
            }
            TaskStateTimeNeed leaveSceneTime = compare(Oldtaskinfo.离开现场时刻, Time4);
            if (leaveSceneTime.NeedUpdate)
            {
                taskinfo.离开现场时刻 = leaveSceneTime.TaskStateTime;
                LtAmbStateTime.Add(getAmbStateTimeInfo(MaxTAmbulanceStateTimeID, taskinfo.任务编码, ambInfo, 4, leaveSceneTime.TaskStateTime, OperatePersonCode));
                MaxTAmbulanceStateTimeID++;
            }
            TaskStateTimeNeed arriveHospitalTime = compare(Oldtaskinfo.到达医院时刻, Time5);
            if (arriveHospitalTime.NeedUpdate)
            {
                taskinfo.到达医院时刻 = arriveHospitalTime.TaskStateTime;
                LtAmbStateTime.Add(getAmbStateTimeInfo(MaxTAmbulanceStateTimeID, taskinfo.任务编码, ambInfo, 5, arriveHospitalTime.TaskStateTime, OperatePersonCode));
                MaxTAmbulanceStateTimeID++;
            }
            TaskStateTimeNeed finishTime = compare(Oldtaskinfo.完成时刻, Time6);
            if (finishTime.NeedUpdate)
            {
                taskinfo.完成时刻 = finishTime.TaskStateTime;
                LtAmbStateTime.Add(getAmbStateTimeInfo(MaxTAmbulanceStateTimeID, taskinfo.任务编码, ambInfo, 6, finishTime.TaskStateTime, OperatePersonCode));
                MaxTAmbulanceStateTimeID++;
            }
            TaskStateTimeNeed returnTime = compare(Oldtaskinfo.返回站中时刻, Time7);
            if (returnTime.NeedUpdate)
            {
                taskinfo.返回站中时刻 = returnTime.TaskStateTime;
                LtAmbStateTime.Add(getAmbStateTimeInfo(MaxTAmbulanceStateTimeID, taskinfo.任务编码, ambInfo, 7, returnTime.TaskStateTime, OperatePersonCode));
                MaxTAmbulanceStateTimeID++;
            }
            ModifyTask3(Oldtaskinfo, taskinfo, OperatePersonCode, mriList);
        }

        /// <summary>
        /// 比较录入时间和原Task表时间，确定是否更改及改为什么
        /// </summary>
        /// <param name="TaskDateTime">Task中的时间</param>
        /// <param name="EnterStringTime">用户录入的字符串时间</param>
        private static TaskStateTimeNeed compare(DateTime? TaskDateTime, string EnterStringTime)
        {
            if (EnterStringTime == "")
            {

                if(TaskDateTime == null)//数据库Task时间为null（没改过）而用户没录入 则不修改
                {
                    return new TaskStateTimeNeed{
                    NeedUpdate=false
                    };
                }
                else
                {
                    return new TaskStateTimeNeed{
                    NeedUpdate=true,
                    TaskTime=null,
                    TaskStateTime=Convert.ToDateTime("1900-01-01")
                    //1900-01-01是特殊时间。在TAmbulanceStateTime用来表示，用户删除过此数据。（TAmbulanceStateTime表时间不为null）
                    };
                }
            }
            DateTime EnterTime = Convert.ToDateTime(EnterStringTime);
            if (String.Format("{0:yyyy-MM-dd HH:mm:ss}", TaskDateTime) == EnterTime.ToString("yyyy-MM-dd HH:mm:ss"))
                //因为时间有毫秒 所以和前台录入时间比较需要用同样格式的String来比较
                return new TaskStateTimeNeed
                {
                    NeedUpdate = false
                };
            else
                return new TaskStateTimeNeed
                {
                    NeedUpdate = true,
                    TaskTime=EnterTime,
                    TaskStateTime = EnterTime
                };
        }
        /// <summary>
        /// 暂不进行数据库操作(只修改对象) 分块修改对象 更灵活 提高效率
        /// 获取AmbulanceStateTimeInfo实体
        /// </summary>
        private static TAmbulanceStateTime getAmbStateTimeInfo(int MaxTAmbulanceStateTimeID, string TaskCode, TAmbulance ambInfo, int StateID, DateTime KeyPressTime, string OperatePersonCode)
        {
            //TAmbulance ambInfo = Ambulance.GetAmbulanceInfo(AmbCode);
            TAmbulanceStateTime asInfo = new TAmbulanceStateTime();

            asInfo.编码 = MaxTAmbulanceStateTimeID;
            asInfo.车辆编码 = ambInfo.车辆编码;
            asInfo.任务编码 = TaskCode;
            asInfo.车辆状态编码 = StateID;
            asInfo.时刻值 = KeyPressTime;
            asInfo.记录时刻 = DateTime.Now;
            asInfo.操作来源编码 = 5;
            asInfo.操作员编码 = OperatePersonCode;
            asInfo.车辆是否在线 = ambInfo.是否在线;
            return asInfo;
        }
        #endregion
        #region 更新并插入修改记录
        private static Dictionary<string, int> m_ModifyTaskTypeDic1 = null;
        private static Dictionary<string, int> m_ModifyTaskTypeDic2 = null;
        private static Dictionary<string, int> m_ModifyTaskTypeDic3 = null;
        private static void ModifyTask1(TTask originInfo, TTask newInfo, string operatorCode,
   List<TModifyRecord> mriList)
        {
            if (m_ModifyTaskTypeDic1 == null)
            {
                m_ModifyTaskTypeDic1 = new Dictionary<string, int>();

                m_ModifyTaskTypeDic1.Add("是否正常结束", 113);//任务是否正常结束
                m_ModifyTaskTypeDic1.Add("异常结束原因编码", 114);//异常结束原因
                m_ModifyTaskTypeDic1.Add("备注", 115);//任务备注
                m_ModifyTaskTypeDic1.Add("行驶公里数", 116);//行驶公里数
                m_ModifyTaskTypeDic1.Add("急救公里数", 117);//急救公里数
                m_ModifyTaskTypeDic1.Add("实际救治人数", 118);//实际救治人数

                //m_ModifyTaskTypeDic1.Add("IsNormalFinish", 113);//任务是否正常结束
                //m_ModifyTaskTypeDic1.Add("AbnormalReasonId", 114);//异常结束原因
                //m_ModifyTaskTypeDic1.Add("Remark", 115);//任务备注
                //m_ModifyTaskTypeDic1.Add("TravelDistance", 116);//行驶公里数
                //m_ModifyTaskTypeDic1.Add("HelpDistance", 117);//急救公里数
                //m_ModifyTaskTypeDic1.Add("CureAmount", 118);//实际救治人数
            }
            //获得所有property的信息
            PropertyInfo[] properties = originInfo.GetType().GetProperties();
            foreach (PropertyInfo p in properties)
            {
                int typeId = -1;
                if (m_ModifyTaskTypeDic1.TryGetValue(p.Name, out typeId)) //如果需要修改
                {
                    object oldObj = p.GetValue(originInfo, null);
                    object newObj = p.GetValue(newInfo, null);
                    if (!object.Equals(oldObj, newObj))
                    {
                        p.SetValue(originInfo, newObj, null);
                        TModifyRecord mri = new TModifyRecord();
                        mri.编码 = Guid.NewGuid();
                        mri.修改后内容 = newObj == null ? "" : newObj.ToString();//2011-02-14 如果是null就费了。
                        mri.修改前内容 = oldObj == null ? "" : oldObj.ToString();//2011-02-14 如果是null就费了。
                        mri.产生时刻 = DateTime.Now;
                        mri.操作员编码 = operatorCode;
                        mri.修改类型编码 = typeId;
                        mri.事件编码 = originInfo.事件编码;
                        mri.受理序号 = originInfo.受理序号;
                        mri.任务编码 = originInfo.任务编码;//任务编码
                        mriList.Add(mri);
                    }
                }
            }
        }

        private static void ModifyTask2(TTask originInfo, TTask newInfo, string operatorCode,
          List<TModifyRecord> mriList)
        {
            if (newInfo.车辆编码 == null)
                newInfo.车辆编码 = originInfo.车辆编码;
            if (newInfo.司机 == null)
                newInfo.司机 = originInfo.司机;
            if (newInfo.医生 == null)
                newInfo.医生 = originInfo.医生;
            if (newInfo.护士 == null)
                newInfo.护士 = originInfo.护士;
            if (newInfo.担架工 == null)
                newInfo.担架工 = originInfo.担架工;
            if (newInfo.抢救员 == null)
                newInfo.抢救员 = originInfo.抢救员;
            if (m_ModifyTaskTypeDic2 == null)
            {
                m_ModifyTaskTypeDic2 = new Dictionary<string, int>();

                m_ModifyTaskTypeDic2.Add("车辆编码", 101);//车辆编码
                m_ModifyTaskTypeDic2.Add("司机", 102);//司机
                m_ModifyTaskTypeDic2.Add("医生", 103);//医生
                m_ModifyTaskTypeDic2.Add("护士", 104);//护士
                m_ModifyTaskTypeDic2.Add("担架工", 105);//担架员
                m_ModifyTaskTypeDic2.Add("抢救员", 130);//抢救员
                //m_ModifyTaskTypeDic2.Add("AmbulanceCode", 101);//车辆编码
                //m_ModifyTaskTypeDic2.Add("Driver", 102);//司机
                //m_ModifyTaskTypeDic2.Add("Doctor", 103);//医生
                //m_ModifyTaskTypeDic2.Add("Nurse", 104);//护士
                //m_ModifyTaskTypeDic2.Add("Litter", 105);//担架员
            }
            //获得所有property的信息
            PropertyInfo[] properties = originInfo.GetType().GetProperties();
            foreach (PropertyInfo p in properties)
            {
                int typeId = -1;
                if (m_ModifyTaskTypeDic2.TryGetValue(p.Name, out typeId)) //如果需要修改
                {
                    object oldObj = p.GetValue(originInfo, null);
                    object newObj = p.GetValue(newInfo, null);
                    if (!object.Equals(oldObj, newObj))
                    {
                        p.SetValue(originInfo, newObj, null);
                        TModifyRecord mri = new TModifyRecord();
                        mri.编码 = Guid.NewGuid();
                        mri.修改后内容 = newObj == null ? "" : newObj.ToString();//2011-02-14 如果是null就费了。
                        mri.修改前内容 = oldObj == null ? "" : oldObj.ToString();//2011-02-14 如果是null就费了。
                        mri.产生时刻 = DateTime.Now;
                        mri.操作员编码 = operatorCode;
                        mri.修改类型编码 = typeId;
                        mri.事件编码 = originInfo.事件编码;
                        mri.受理序号 = originInfo.受理序号;
                        mri.任务编码 = originInfo.任务编码;//任务编码
                        mriList.Add(mri);
                    }
                }
            }
        }
        private static void ModifyTask3(TTask originInfo, TTask newInfo, string operatorCode,
          List<TModifyRecord> mriList)
        {
            if (newInfo.接收命令时刻 == null)//null 代表不变  1900-01-01 代表删除
                newInfo.接收命令时刻 = originInfo.接收命令时刻;
            if (newInfo.接收命令时刻 == Convert.ToDateTime("1900-01-01"))
                newInfo.接收命令时刻 = null;
            if (newInfo.出车时刻 == null)
                newInfo.出车时刻 = originInfo.出车时刻;
            if (newInfo.出车时刻 == Convert.ToDateTime("1900-01-01"))
                newInfo.出车时刻 = null;
            if (newInfo.到达现场时刻 == null)
                newInfo.到达现场时刻 = originInfo.到达现场时刻;
            if (newInfo.到达现场时刻 == Convert.ToDateTime("1900-01-01"))
                newInfo.到达现场时刻 = null;
            if (newInfo.离开现场时刻 == null)
                newInfo.离开现场时刻 = originInfo.离开现场时刻;
            if (newInfo.离开现场时刻 == Convert.ToDateTime("1900-01-01"))
                newInfo.离开现场时刻 = null;
            if (newInfo.到达医院时刻 == null)
                newInfo.到达医院时刻 = originInfo.到达医院时刻;
            if (newInfo.到达医院时刻 == Convert.ToDateTime("1900-01-01"))
                newInfo.到达医院时刻 = null;
            if (newInfo.完成时刻 == null)
                newInfo.完成时刻 = originInfo.完成时刻;
            if (newInfo.完成时刻 == Convert.ToDateTime("1900-01-01"))
                newInfo.完成时刻 = null;
            if (newInfo.返回站中时刻 == null)
                newInfo.返回站中时刻 = originInfo.返回站中时刻;
            if (newInfo.返回站中时刻 == Convert.ToDateTime("1900-01-01"))
                newInfo.返回站中时刻 = null;
            if (m_ModifyTaskTypeDic3 == null)
            {
                m_ModifyTaskTypeDic3 = new Dictionary<string, int>();

                m_ModifyTaskTypeDic3.Add("接收命令时刻", 106);//收到指令
                m_ModifyTaskTypeDic3.Add("出车时刻", 107);//驶向现场
                m_ModifyTaskTypeDic3.Add("到达现场时刻", 108);//抢救转送
                m_ModifyTaskTypeDic3.Add("离开现场时刻", 109);//病人上车
                m_ModifyTaskTypeDic3.Add("到达医院时刻", 110);//到达医院
                m_ModifyTaskTypeDic3.Add("完成时刻", 111);//途中待命
                m_ModifyTaskTypeDic3.Add("返回站中时刻", 112);//站内待命
            //    m_ModifyTaskTypeDic3.Add("ReceiveCmdTime", 106);//收到指令
            //    m_ModifyTaskTypeDic3.Add("AmbulanceLeaveTime", 107);//驶向现场
            //    m_ModifyTaskTypeDic3.Add("ArriveSceneTime", 108);//抢救转送
            //    m_ModifyTaskTypeDic3.Add("LeaveSceneTime", 109);//病人上车
            //    m_ModifyTaskTypeDic3.Add("ArriveHospitalTime", 110);//到达医院
            //    m_ModifyTaskTypeDic3.Add("FinishTime", 111);//途中待命
            //    m_ModifyTaskTypeDic3.Add("ReturnTime", 112);//站内待命
            }

            //获得所有property的信息
            PropertyInfo[] properties = originInfo.GetType().GetProperties();
            foreach (PropertyInfo p in properties)
            {
                int typeId = -1;
                if (m_ModifyTaskTypeDic3.TryGetValue(p.Name, out typeId)) //如果需要修改
                {
                    object oldObj = p.GetValue(originInfo, null);
                    object newObj = p.GetValue(newInfo, null);
                    if (!object.Equals(oldObj, newObj))
                    {
                        p.SetValue(originInfo, newObj, null);
                        TModifyRecord mri = new TModifyRecord();
                        mri.编码 = Guid.NewGuid();
                        mri.修改后内容 = newObj == null ? "" : newObj.ToString();//2011-02-14 如果是null就费了。
                        mri.修改前内容 = oldObj == null ? "" : oldObj.ToString();//2011-02-14 如果是null就费了。
                        mri.产生时刻 = DateTime.Now;
                        mri.操作员编码 = operatorCode;
                        mri.修改类型编码 = typeId;
                        mri.事件编码 = originInfo.事件编码;
                        mri.受理序号 = originInfo.受理序号;
                        mri.任务编码 = originInfo.任务编码;//任务编码
                        mriList.Add(mri);
                    }
                }
            }
        }
        #endregion

        #endregion

        /// <summary>
        /// 获取任务对象
        /// </summary>
        public static C_TaskInfoDetail GetC_TaskInfoDetail(string Code)
        {
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
 where TT.任务编码=@p0");
            sqltt.commandObj.Add(Code);


            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                C_TaskInfoDetail tt = dbContext.ExecuteQuery<C_TaskInfoDetail>(
sqltt.commandText.ToString()
, sqltt.commandObj.ToArray()).FirstOrDefault();
                return tt;
            }
        }


        /// <summary>
        /// 获取任务对象
        /// </summary>
        public static TTask GetTaskInfo(string Code)
        {
            //查询任务信息
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                TTask tt = (from t in dbContext.TTask
                           where t.任务编码 == Code
                           select t).First();
                return tt;
            }
        }



        /// <summary>
        /// 获取任务车辆对象
        /// </summary>
        public static List<C_AmbulanceStateTimeInfo> GetAmbulanceStateTimeLs(string Code)
        {
            //查询对应任务车辆状态变化信息
            SqlParameterTool sqltast = new SqlParameterTool();
            sqltast.commandText.Append(@"select Code=A.编码,TaskCode=A.任务编码,AmbuCode=A.车辆编码,RealSign=B.实际标识,WorkStateId=车辆状态编码,C.名称 as WorkStateName
,KeyPressTime=时刻值,SaveTime=记录时刻,SourceCode=操作来源编码,D.名称 as SourceName,OperationCode=操作员编码,E.姓名 as OperationName,JobCode=E.工号,IsOnline=车辆是否在线
from dbo.TAmbulanceStateTime A
left join dbo.TAmbulance B on A.车辆编码=B.车辆编码
left join dbo.TZAmbulanceState C on A.车辆状态编码=C.编码
left join dbo.TZOperationOrigin D on A.操作来源编码=D.编码
left join dbo.TPerson E on A.操作员编码=E.编码
where a.任务编码=@p0");
            sqltast.commandObj.Add(Code);

            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                List<C_AmbulanceStateTimeInfo> tastLs = dbContext.ExecuteQuery<C_AmbulanceStateTimeInfo>(
sqltast.commandText.ToString()
, sqltast.commandObj.ToArray()).ToList();
                return tastLs;
            }
        }

    }
    /// <summary>
    /// 想写为private类型的怎么不行？（这个类只有本页面使用）
    /// </summary>
    public class TaskStateTimeNeed
    {
        /// <summary>
        /// 表示需更改 数据库值
        /// </summary>
        public bool NeedUpdate { get; set; }
        /// <summary>
        /// Task中的时间
        /// </summary>
        public DateTime? TaskTime { get; set; }
        /// <summary>
        /// TaskStateTime中的时间
        /// </summary>
        public DateTime TaskStateTime { get; set; }
    }
}

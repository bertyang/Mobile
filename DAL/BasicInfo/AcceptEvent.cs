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
    public class AcceptEvent
    {
        private static Dictionary<string, int> m_ModifyAcceptTypeDic = null;
        /// <summary>
        /// 民族可录入下拉菜单填充
        /// </summary>
        /// <returns></returns>
        public static List<TZFolk> LoadFolks()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZFolk.Where(p => p.是否有效 == true).OrderBy(p => p.顺序号).ToList();
            }
        }
        /// <summary>
        /// 国籍可录入下拉菜单填充
        /// </summary>
        /// <returns></returns>
        public static List<TZNational> LoadNationals()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZNational.Where(p => p.是否有效 == true).OrderBy(p => p.顺序号).ToList();
            }
        }
        /// <summary>
        /// 年龄可录入下拉菜单填充
        /// </summary>
        /// <returns></returns>
        public static List<TZAge> LoadAges()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZAge.Where(p => p.是否有效 == true).OrderBy(p => p.顺序号).ToList();
            }
        }

        /// <summary>
        /// 病情判断
        /// </summary>
        /// <returns></returns>
        public static List<TZIllState> LoadIllStates()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZIllState.Where(p => p.是否有效 == true).OrderBy(p => p.顺序号).ToList();
            }
        }
        /// <summary>
        /// 往救地点
        /// </summary>
        public static List<TZLocalAddrType> LoadLocalAddrTypes()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZLocalAddrType.Where(p => p.是否有效 == true).OrderBy(p => p.顺序号).ToList();
            }
        }
        /// <summary>
        /// 送往地点
        /// </summary>
        public static List<TZSendAddrType> LoadSendAddrTypes()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZSendAddrType.Where(p => p.是否有效 == true).OrderBy(p => p.顺序号).ToList();
            }
        }
        /// <summary>
        /// 特殊要求
        /// </summary>
        public static List<TZSpecialRequest> LoadSpecialRequests()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZSpecialRequest.Where(p => p.是否有效 == true).OrderBy(p => p.顺序号).ToList();
            }
        }
        /// <summary>
        /// 保留字段1
        /// </summary>
        public static List<TZBackupOne> LoadBackupOnes()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZBackupOne.Where(p => p.是否有效 == true).OrderBy(p => p.顺序号).ToList();
            }
        }
        /// <summary>
        /// 保留字段2
        /// </summary>
        public static List<TZBackupTwo> LoadBackupTwos()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZBackupTwo.Where(p => p.是否有效 == true).OrderBy(p => p.顺序号).ToList();
            }
        }

        /// <summary>
        /// 主诉下拉菜单(树)填充
        /// </summary>
        /// <returns></returns>
        public static List<TZAlarmReason> LoadAlarmReasons()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZAlarmReason.Where(p => p.是否有效 == true).OrderBy(p => p.顺序号).ToList();
            }
        }

        #region 修改

        private static bool IfAllowUpdate(string ID, List<Anchor.FA.Model.TParameterAcceptInfo> tpaLs)
        {
            var q=from t in tpaLs
                  where t.控件名称==ID //&& t.是否可见==true
                  select t;
            return (q.Any());
        }

        public static string Update(TAcceptEvent entity)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                try
                {
                    TAcceptEvent model = dbContext.TAcceptEvent.FirstOrDefault(t => t.事件编码 == entity.事件编码 && t.受理序号 == entity.受理序号);


                    if (m_ModifyAcceptTypeDic == null)
                    {
                        m_ModifyAcceptTypeDic = new Dictionary<string, int>();

                        //后台根据设置是否可以更改

                        List<Anchor.FA.Model.TParameterAcceptInfo> tpaLs =AlarmEvent.LoadParameterAcceptInfo().Where(t=>t.是否可见==true).ToList();

                        //对应表 TModifyRecord 要在TModifyRecord有 MPDS备注

                        if (IfAllowUpdate("label_AlarmReason", tpaLs))
                            m_ModifyAcceptTypeDic.Add("主诉", 19);//主诉（呼救原因）
                        if (IfAllowUpdate("label_PatientName", tpaLs))
                            m_ModifyAcceptTypeDic.Add("患者姓名", 14);//患者姓名
                        if (IfAllowUpdate("label_Sex", tpaLs))
                            m_ModifyAcceptTypeDic.Add("性别", 15);//性别
                        if (IfAllowUpdate("label_Age", tpaLs))
                            m_ModifyAcceptTypeDic.Add("年龄", 16);//年龄
                        if (IfAllowUpdate("label_National", tpaLs))
                            m_ModifyAcceptTypeDic.Add("国籍", 18);//国籍
                        if (IfAllowUpdate("label_Folk", tpaLs))
                            m_ModifyAcceptTypeDic.Add("民族", 17);//民族
                        if (IfAllowUpdate("label_Judge", tpaLs))
                            m_ModifyAcceptTypeDic.Add("病种判断", 20);//病种判断
                        if (IfAllowUpdate("label_LinkMan", tpaLs))
                            m_ModifyAcceptTypeDic.Add("联系人", 11);//联系人
                        if (IfAllowUpdate("label_LinkTel", tpaLs))
                            m_ModifyAcceptTypeDic.Add("联系电话", 12);//联系电话
                        if (IfAllowUpdate("label_Extension", tpaLs))
                            m_ModifyAcceptTypeDic.Add("分机", 13);//分机
                        if (IfAllowUpdate("label_LocalAddr", tpaLs))
                            m_ModifyAcceptTypeDic.Add("现场地址", 6);//现场地址
                        if (IfAllowUpdate("label_WaitAddr", tpaLs))
                            m_ModifyAcceptTypeDic.Add("等车地址", 7);//等车地址
                        if (IfAllowUpdate("label_SendAddr", tpaLs))
                            m_ModifyAcceptTypeDic.Add("送往地点", 8);//送往地点
                        if (IfAllowUpdate("label_Remark", tpaLs))
                            m_ModifyAcceptTypeDic.Add("备注", 27);//备注

                        if (IfAllowUpdate("label_IllState", tpaLs))
                            m_ModifyAcceptTypeDic.Add("病情编码", 21);//
                        if (IfAllowUpdate("label_LocalAddrType", tpaLs))
                            m_ModifyAcceptTypeDic.Add("往救地点类型编码", 9);//
                        if (IfAllowUpdate("label_SendAddrType", tpaLs))
                            m_ModifyAcceptTypeDic.Add("送往地点类型编码", 10);//
                        if (IfAllowUpdate("label_PatientCount", tpaLs))
                            m_ModifyAcceptTypeDic.Add("患者人数", 23);//
                        if (IfAllowUpdate("label_SpecialNeed", tpaLs))
                            m_ModifyAcceptTypeDic.Add("特殊要求", 24);//
                        if (IfAllowUpdate("label_Reserve1", tpaLs))
                            m_ModifyAcceptTypeDic.Add("保留字段1", 25);//
                        if (IfAllowUpdate("label_Reserve2", tpaLs))
                            m_ModifyAcceptTypeDic.Add("保留字段2", 26);//
                        if (IfAllowUpdate("label_Mpds", tpaLs))
                            m_ModifyAcceptTypeDic.Add("MPDS备注", 30);//MPDS备注
                        m_ModifyAcceptTypeDic.Add("是否需要担架", 22);//是否需要担架


                        //m_ModifyAcceptTypeDic.Add("LocalAddr", 6);//现场地址
                        //m_ModifyAcceptTypeDic.Add("WaitAddr", 7);//等车地址
                        //m_ModifyAcceptTypeDic.Add("SendAddr", 8);//送往地点
                        //m_ModifyAcceptTypeDic.Add("LocalAddrTypeId", 9);//
                        //m_ModifyAcceptTypeDic.Add("SendAddrTypeId", 10);//
                        //m_ModifyAcceptTypeDic.Add("LinkMan", 11);//联系人
                        //m_ModifyAcceptTypeDic.Add("LinkTel", 12);//联系电话
                        //m_ModifyAcceptTypeDic.Add("Extension", 13);//分机
                        //m_ModifyAcceptTypeDic.Add("PatientName", 14);//患者姓名
                        //m_ModifyAcceptTypeDic.Add("Sex", 15);//性别
                        //m_ModifyAcceptTypeDic.Add("Age", 16);//年龄
                        //m_ModifyAcceptTypeDic.Add("Folk", 17);//民族
                        //m_ModifyAcceptTypeDic.Add("National", 18);//国籍
                        //m_ModifyAcceptTypeDic.Add("AlarmReason", 19);//主诉（呼救原因）
                        //m_ModifyAcceptTypeDic.Add("Judge", 20);//病种判断
                        //m_ModifyAcceptTypeDic.Add("IllStateId", 21);//
                        //m_ModifyAcceptTypeDic.Add("IsNeedLitter", 22);//是否需要担架
                        //m_ModifyAcceptTypeDic.Add("PatientCount", 23);//
                        //m_ModifyAcceptTypeDic.Add("SpecialNeed", 24);//
                        //m_ModifyAcceptTypeDic.Add("BackUpOne", 25);//
                        //m_ModifyAcceptTypeDic.Add("BackUpTwo", 26);//
                        //m_ModifyAcceptTypeDic.Add("Remark", 27);//备注
                    }
                    //List<TModifyRecord> mriList = new List<TModifyRecord>();
                    //获得所有property的信息
                    PropertyInfo[] properties = model.GetType().GetProperties();
                    C_WorkerDetail CWD = Anchor.FA.DAL.Organize.Worker.GetWorkerDetailById(Convert.ToInt32(entity.责任受理人编码));//使用 责任受理人编码 来存储操作员编码
                    string OperatePersonCode = CWD.PersonCode.FirstOrDefault();
                    //string OperatePersonCode = Task.LoadWorkerRole(Convert.ToInt32(entity.责任受理人编码)).EmpNo;
                    //string OperatePersonCode = entity.责任受理人编码;
                    
                    
                    
                    foreach (PropertyInfo p in properties)
                    {
                        int typeId = -1;
                        if (m_ModifyAcceptTypeDic.TryGetValue(p.Name, out typeId)) //如果需要修改
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
                                mri.受理序号 = entity.受理序号;
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



                    //model.主诉 = entity.主诉;
                    //model.患者姓名    =  entity.患者姓名    ;
                    //model.性别        =  entity.性别        ;
                    //model.年龄        =  entity.年龄        ;
                    //model.国籍        =  entity.国籍        ;
                    //model.民族        =  entity.民族        ;
                    //model.病种判断    =  entity.病种判断    ;
                    //model.联系人      =  entity.联系人      ;
                    //model.联系电话    =  entity.联系电话    ;
                    //model.分机        =  entity.分机        ;
                    //model.现场地址    =  entity.现场地址    ;
                    //model.等车地址    =  entity.等车地址    ;
                    //model.送往地点    =  entity.送往地点    ;
                    //model.是否需要担架=  entity.是否需要担架;
                    //model.备注        =  entity.备注        ;
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

        /// <summary>
        /// 获取受理对象
        /// </summary>
        public static C_AccInfo GetC_AccInfo(string eventCode, int orderNumber)
        {
            //查询受理信息
            SqlParameterTool sqltac = new SqlParameterTool();
            sqltac.commandText.Append(@"SELECT EventCode=事件编码,AcceptOrder=受理序号,TypeId=受理类型编码,DetailReasonId=受理类型子编码,AcceptPersonCode=责任受理人编码,AlarmTel=呼救电话
,HangUpTime=挂起时刻,RingTime=电话振铃时刻,AcceptBeginTime=开始受理时刻,AcceptEndTime=结束受理时刻,CommandTime=发送指令时刻,LocalAddr=现场地址,WaitAddr=等车地址
,SendAddr=送往地点,LocalAddrTypeId=往救地点类型编码,SendAddrTypeId=送往地点类型编码,LinkMan=联系人,LinkTel=联系电话,Extension=分机,PatientName=患者姓名,Sex=性别
,Age=年龄,Folk=民族,[National]=国籍,AlarmReason=主诉,Judge=病种判断,IllStateId=病情编码,IsNeedLitter=是否需要担架,PatientCount=患者人数,SpecialNeed=特殊要求,IsLabeled=是否标注
,X=X坐标,Y=Y坐标,AmbulanceList=派车列表,Remark=备注,AcceptType=TZET.名称,Dispatcher=TP.姓名
 FROM TAcceptEvent TAE
 left join TZAcceptEventType TZET on TZET.编码 = TAE.受理类型编码
 left join TPerson TP on TP.编码 = TAE.责任受理人编码
 where 事件编码 = @p0 and 受理序号=@p1");
            sqltac.commandObj.Add(eventCode);
            sqltac.commandObj.Add(orderNumber);


            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {

                C_AccInfo tac = dbContext.ExecuteQuery<C_AccInfo>(
sqltac.commandText.ToString()
, sqltac.commandObj.ToArray()).FirstOrDefault();
                return tac;
            }
        }
    }
}

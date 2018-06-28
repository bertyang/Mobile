using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using Anchor.FA.Model;
using Anchor.FA.Utility;

namespace Anchor.FA.DAL.BasicInfo
{
    public class StatisticsDAL
    {
        #region 打印事件详情
        /// <summary>
        /// 事件信息
        /// </summary>
        /// <param name="almCode"></param>
        /// <returns></returns>
        public static List<AttemperAlarmInfo> GetAlarmInfo(string almCode)
        {
            AttemperAlarmInfo info;
            List<AttemperAlarmInfo> list = new List<AttemperAlarmInfo>();
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
declare @eventCode varchar(16)
set @eventCode='" + almCode + "' ");
            sb.Append(@"
SELECT tae.事件编码,tae.首次呼救电话,tac.患者姓名,tac.主诉,tac.联系人,
                tac.性别,病情=tzis.名称,tac.联系电话,tac.年龄,担架=case when 是否需要担架=1 then '需要' else '不需要' end,
                tac.分机,tac.民族,tac.特殊要求,tac.现场地址,事件类型=tzat.名称,tac.送往地点,tac.等车地址
                from TAcceptEvent tac 
                left join TAlarmEvent tae on tac.事件编码=tae.事件编码
                left join TZIllState tzis on tac.病情编码=tzis.编码 
                left join TZAlarmEventType tzat on tae.事件类型编码=tzat.编码 
                where tac.事件编码=@eventCode 
                and tac.受理序号=(select max(受理序号) from TAcceptEvent where 事件编码=@eventCode)");
            using (DataSet ds = SQLHelper.ExecuteDataSet(AppConfig.ConnectionStringReport, CommandType.Text, sb.ToString(), null))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    info = new AttemperAlarmInfo();
                    info.AlarmCode = DBConvert.ConvertStringToString(dr["事件编码"]);
                    info.AlarmCall = DBConvert.ConvertStringToString(dr["首次呼救电话"]);
                    info.Name = DBConvert.ConvertStringToString(dr["患者姓名"]);
                    info.Judge = DBConvert.ConvertStringToString(dr["主诉"]);
                    info.LinkPerson = DBConvert.ConvertStringToString(dr["联系人"]);
                    info.Sex = DBConvert.ConvertStringToString(dr["性别"]);
                    info.IllState = DBConvert.ConvertStringToString(dr["病情"]);
                    info.LinkPhone = DBConvert.ConvertStringToString(dr["联系电话"]);
                    info.Age = DBConvert.ConvertStringToString(dr["年龄"]);
                    info.IsNeedLitter = DBConvert.ConvertStringToString(dr["担架"]);
                    info.ExtensionPhone = DBConvert.ConvertStringToString(dr["分机"]);
                    info.MinZu = DBConvert.ConvertStringToString(dr["民族"]);
                    info.Request = DBConvert.ConvertStringToString(dr["特殊要求"]);
                    info.LocalAddr = DBConvert.ConvertStringToString(dr["现场地址"]);
                    info.AlarmType = DBConvert.ConvertStringToString(dr["事件类型"]);
                    info.SendAddr = DBConvert.ConvertStringToString(dr["送往地点"]);
                    info.WaitAddr = DBConvert.ConvertStringToString(dr["等车地址"]);
                    list.Add(info);
                }
                return list;
            }
        }
        /// <summary>
        /// 受理信息
        /// </summary>
        /// <param name="almCode"></param>
        /// <returns></returns>
        public static List<AttemperAcceptInfo> GetAcceptInfo(string almCode)
        {
            AttemperAcceptInfo info;
            List<AttemperAcceptInfo> list = new List<AttemperAcceptInfo>();
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
declare @eventCode varchar(16)
set @eventCode='" + almCode + "' ");
            sb.Append(@"
                SELECT tac.受理序号,受理类型=tzac.名称,调度员=tp.姓名,
                tac.电话振铃时刻,tac.开始受理时刻,tac.发送指令时刻,tac.结束受理时刻,
                tac.备注,tac.MPDS备注 
                from TAcceptEvent tac 
                left join TPerson tp on tp.编码=tac.责任受理人编码 
                left join TZAcceptEventType tzac on tac.受理类型编码=tzac.编码 
                where tac.事件编码=@eventCode ");
            using (DataSet ds = SQLHelper.ExecuteDataSet(AppConfig.ConnectionStringReport, CommandType.Text, sb.ToString(), null))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    info = new AttemperAcceptInfo();
                    info.Order = DBConvert.ConvertStringToString(dr["受理序号"]);
                    info.AcceptType = DBConvert.ConvertStringToString(dr["受理类型"]);
                    info.AttemperPerson = DBConvert.ConvertStringToString(dr["调度员"]);
                    info.ShakeTime = DBConvert.ConvertStringToString(dr["电话振铃时刻"]);
                    info.StartTime = DBConvert.ConvertStringToString(dr["开始受理时刻"]);
                    info.DispatchTime = DBConvert.ConvertStringToString(dr["发送指令时刻"]);
                    info.EndTime = DBConvert.ConvertStringToString(dr["结束受理时刻"]);
                    info.Remark = DBConvert.ConvertStringToString(dr["备注"]);
                    info.MPDSRemark = DBConvert.ConvertStringToString(dr["MPDS备注"]);
                    list.Add(info);
                }
                return list;
            }
        }
        /// <summary>
        /// 出车信息
        /// </summary>
        /// <param name="almCode"></param>
        /// <returns></returns>
        public static List<AttemperTaskInfo> GetTaskInfo(string almCode)
        {
            AttemperTaskInfo info;
            List<AttemperTaskInfo> list = new List<AttemperTaskInfo>();
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
declare @eventCode varchar(16)
set @eventCode='" + almCode + "' ");
            sb.Append(@"
            select tt.任务编码,tt.事件编码,tt.受理序号,tam.实际标识,
            接收命令时刻,出车时刻,到达现场时刻,离开现场时刻,到达医院时刻,完成时刻,返回站中时刻,
            出车结果=case when 是否正常结束=1 then '正常完成' else '异常结束' end,
            异常结束原因=tztar.名称,tt.司机,tt.医生,tt.护士,tt.担架工,tt.抢救员,备注 
            ,分站=ts.名称,tam.车牌号码,责任调度人=tp.姓名 ");
            //刘爱青 2012.12.18 增加按键时刻操作来源
            if (AppConfig.GetBoolConfigValue("IsShowOrigin"))
            {
                sb.Append(@",接收命令时刻来源=(select top 1 来源=case when tast.操作来源编码=1 then tp.姓名 else '车载' end
                                            from TAmbulanceStateTime tast 
                                            left join TPerson tp on tast.操作员编码=tp.编码 
                                            where tast.任务编码=tt.任务编码 and tast.车辆状态编码=1 and tast.时刻值=tt.接收命令时刻) 
                        ,出车时刻来源=(select top 1 来源=case when tast.操作来源编码=1 then tp.姓名 else '车载' end
                                        from TAmbulanceStateTime tast 
                                        left join TPerson tp on tast.操作员编码=tp.编码 
                                        where tast.任务编码=tt.任务编码 and tast.车辆状态编码=2 and tast.时刻值=tt.出车时刻)
                     ,到达现场时刻来源=(select top 1 来源=case when tast.操作来源编码=1 then tp.姓名 else '车载' end
                                        from TAmbulanceStateTime tast 
                                        left join TPerson tp on tast.操作员编码=tp.编码 
                                        where tast.任务编码=tt.任务编码 and tast.车辆状态编码=3 and tast.时刻值=tt.到达现场时刻) 
                    ,离开现场时刻来源=(select top 1 来源=case when tast.操作来源编码=1 then tp.姓名 else '车载' end
                                        from TAmbulanceStateTime tast 
                                        left join TPerson tp on tast.操作员编码=tp.编码 
                                        where tast.任务编码=tt.任务编码 and tast.车辆状态编码=4 and tast.时刻值=tt.离开现场时刻) 
                    ,到达医院时刻来源=(select top 1 来源=case when tast.操作来源编码=1 then tp.姓名 else '车载' end
                                        from TAmbulanceStateTime tast 
                                        left join TPerson tp on tast.操作员编码=tp.编码 
                                        where tast.任务编码=tt.任务编码 and tast.车辆状态编码=5 and tast.时刻值=tt.到达医院时刻)
                        ,完成时刻来源=(select top 1 来源=case when tast.操作来源编码=1 then tp.姓名 else '车载' end
                                        from TAmbulanceStateTime tast 
                                        left join TPerson tp on tast.操作员编码=tp.编码 
                                        where tast.任务编码=tt.任务编码 and tast.车辆状态编码=6 and tast.时刻值=tt.完成时刻) 
                    ,返回站中时刻来源=(select top 1 来源=case when tast.操作来源编码=1 then tp.姓名 else '车载' end
                                        from TAmbulanceStateTime tast 
                                        left join TPerson tp on tast.操作员编码=tp.编码 
                                        where tast.任务编码=tt.任务编码 and tast.车辆状态编码=7 and tast.时刻值=tt.返回站中时刻) ");
            }
            sb.Append(@"from dbo.TTask tt 
            left join dbo.TAmbulance tam on tam.车辆编码=tt.车辆编码 
            left join dbo.TZTaskAbendReason tztar on tztar.编码=tt.异常结束原因编码 
            left join dbo.TStation ts on ts.编码=tt.分站编码 
            left join TPerson tp on tp.编码=tt.责任调度人编码 
            where 事件编码=@eventCode ");
            using (DataSet ds = SQLHelper.ExecuteDataSet(AppConfig.ConnectionStringReport, CommandType.Text, sb.ToString(), null))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    info = new AttemperTaskInfo();
                    info.TaskCode = DBConvert.ConvertStringToString(dr["任务编码"]);
                    info.AlarmEventCode = DBConvert.ConvertStringToString(dr["事件编码"]);
                    info.Order = DBConvert.ConvertStringToString(dr["受理序号"]);
                    info.AmbMark = DBConvert.ConvertStringToString(dr["实际标识"]);
                    if (AppConfig.GetBoolConfigValue("IsShowOrigin"))
                    {
                        info.ReceiveTime = dr["接收命令时刻"] == DBNull.Value ? "" : dr["接收命令时刻"].ToString() + "\r\n(" + dr["接收命令时刻来源"].ToString() + ")";
                        info.OutTime = dr["出车时刻"] == DBNull.Value ? "" : dr["出车时刻"].ToString() + "\r\n(" + dr["出车时刻来源"].ToString() + ")";
                        info.ArriveTime = dr["到达现场时刻"] == DBNull.Value ? "" : dr["到达现场时刻"].ToString() + "\r\n(" + dr["到达现场时刻来源"].ToString() + ")";
                        info.LeaveTime = dr["离开现场时刻"] == DBNull.Value ? "" : dr["离开现场时刻"].ToString() + "\r\n(" + dr["离开现场时刻来源"].ToString() + ")";
                        info.ArriHsplTime = dr["到达医院时刻"] == DBNull.Value ? "" : dr["到达医院时刻"].ToString() + "\r\n(" + dr["到达医院时刻来源"].ToString() + ")";
                        info.FinishTime = dr["完成时刻"] == DBNull.Value ? "" : dr["完成时刻"].ToString() + "\r\n(" + dr["完成时刻来源"].ToString() + ")";
                        info.ReturnTime = dr["返回站中时刻"] == DBNull.Value ? "" : dr["返回站中时刻"].ToString() + "\r\n(" + dr["返回站中时刻来源"].ToString() + ")";
                    }
                    else
                    {
                        info.ReceiveTime = DBConvert.ConvertStringToString(dr["接收命令时刻"]);
                        info.OutTime = DBConvert.ConvertStringToString(dr["出车时刻"]);
                        info.ArriveTime = DBConvert.ConvertStringToString(dr["到达现场时刻"]);
                        info.LeaveTime = DBConvert.ConvertStringToString(dr["离开现场时刻"]);
                        info.ArriHsplTime = DBConvert.ConvertStringToString(dr["到达医院时刻"]);
                        info.FinishTime = DBConvert.ConvertStringToString(dr["完成时刻"]);
                        info.ReturnTime = DBConvert.ConvertStringToString(dr["返回站中时刻"]);
                    }
                    info.TaskResult = DBConvert.ConvertStringToString(dr["出车结果"]);
                    info.TaskAbdReason = DBConvert.ConvertStringToString(dr["异常结束原因"]);
                    info.Driver = DBConvert.ConvertStringToString(dr["司机"]);
                    info.Doctor = DBConvert.ConvertStringToString(dr["医生"]);
                    info.Nurse = DBConvert.ConvertStringToString(dr["护士"]);
                    info.Litter = DBConvert.ConvertStringToString(dr["担架工"]);
                    info.JiJiuYuan = DBConvert.ConvertStringToString(dr["抢救员"]);
                    info.Remark = DBConvert.ConvertStringToString(dr["备注"]);
                    info.Station = DBConvert.ConvertStringToString(dr["分站"]);
                    info.AmbPlate = DBConvert.ConvertStringToString(dr["车牌号码"]);
                    info.AttemperPerson = DBConvert.ConvertStringToString(dr["责任调度人"]);
                    list.Add(info);
                }
                return list;
            }
        }
        /// <summary>
        /// 电话信息
        /// </summary>
        /// <param name="almCode"></param>
        /// <returns></returns>
        public static List<AttemperTelInfo> GetTelInfo(string almCode)
        {
            AttemperTelInfo info;
            List<AttemperTelInfo> list = new List<AttemperTelInfo>();
            StringBuilder sb = new StringBuilder();
            sb.Append("select 主叫号码,通话时刻,结束时刻,台号,");
            sb.Append("调度员=tp.姓名,通话类型=tzact.名称 ");
            sb.Append("from dbo.TAlarmCall tac ");
            sb.Append("left join dbo.TZAlarmCallType tzact on tzact.编码=tac.通话类型编码 ");
            sb.Append("left join dbo.TPerson tp on tp.编码=tac.调度员编码 ");
            sb.Append("where 事件编码='" + almCode + "' ");
            using (DataSet ds = SQLHelper.ExecuteDataSet(AppConfig.ConnectionStringReport, CommandType.Text, sb.ToString(), null))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    info = new AttemperTelInfo();
                    info.TelNumber = DBConvert.ConvertStringToString(dr["主叫号码"]);
                    info.CallTime = DBConvert.ConvertStringToString(dr["通话时刻"]);
                    info.EndTime = DBConvert.ConvertStringToString(dr["结束时刻"]);
                    info.DaskNumber = DBConvert.ConvertStringToString(dr["台号"]);
                    info.Attemper = DBConvert.ConvertStringToString(dr["调度员"]);
                    info.CallType = DBConvert.ConvertStringToString(dr["通话类型"]);
                    list.Add(info);
                }
                return list;
            }
        }
        #endregion

        #region 打印命令单
        /// <summary>
        /// 打印命令单
        /// 2012.1.6 刘爱青
        /// </summary>
        /// <param name="TaskCode"></param>
        /// <returns></returns>
        public static StationCommandInfo PrintCommand(string TaskCode)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" select 事件编码=tt.事件编码,收单时刻=tace.发送指令时刻,调度员=tp.工号,台号=right(tt.事件编码,2),现场地址=tace.现场地址,等车地址=tace.等车地址,送往地址=tace.送往地点,");
                sb.Append("呼救电话=tace.呼救电话,联系电话=tace.联系电话+tace.分机,联系人=tace.联系人,车辆='['+ta.实际标识+']'+'['+ta.车牌号码+']',");
                sb.Append("随车人员=tt.司机+' '+tt.医生+' '+tt.护士+' '+tt.担架工+' '+tt.抢救员,特殊要求=tace.特殊要求,");
                sb.Append("患者姓名=tace.患者姓名,性别=tace.性别,年龄=tace.年龄,民族=tace.民族,国籍=tace.国籍,患者人数=tace.患者人数,");
                sb.Append("初步判断=tace.主诉,备注=tace.备注,");
                sb.Append(" 分站=ts.名称 ");
                sb.Append("from TTask tt ");
                sb.Append(" left join TAcceptEvent tace on tace.事件编码=tt.事件编码 and tt.受理序号=tace.受理序号");
                sb.Append(" left join TAlarmEvent tae on tae.事件编码=tt.事件编码");
                sb.Append(" left join TAmbulance ta on ta.车辆编码=tt.车辆编码 ");
                sb.Append(" left join TStation ts on ts.编码=tt.分站编码 ");
                sb.Append(" left join TPerson tp on tp.编码=tt.责任调度人编码 ");
                sb.Append(" where tt.任务编码='").Append(TaskCode).Append("'");
                using (DataSet ds = SQLHelper.ExecuteDataSet(AppConfig.ConnectionStringReport, CommandType.Text, sb.ToString(), null))
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    StationCommandInfo info = new StationCommandInfo();
                    info.EventCode = DBConvert.ConvertStringToString(dr["事件编码"]);
                    info.ReceiveRecordCommandTime = DBConvert.ConvertStringToString(dr["收单时刻"]);
                    info.DispatcherWorkid = DBConvert.ConvertStringToString(dr["调度员"]);
                    info.DeskID = DBConvert.ConvertStringToString(dr["台号"]);
                    info.LocalAddr = DBConvert.ConvertStringToString(dr["现场地址"]);
                    info.WaitAddr = DBConvert.ConvertStringToString(dr["等车地址"]);
                    info.SendAddr = DBConvert.ConvertStringToString(dr["送往地址"]);
                    info.AlarmTel = DBConvert.ConvertStringToString(dr["呼救电话"]);
                    info.LinkTel = DBConvert.ConvertStringToString(dr["联系电话"]);
                    info.LinkPerson = DBConvert.ConvertStringToString(dr["联系人"]);
                    info.RealSignbunch = DBConvert.ConvertStringToString(dr["车辆"]);
                    info.AmbPersons = DBConvert.ConvertStringToString(dr["随车人员"]);
                    info.SpecialNeed = DBConvert.ConvertStringToString(dr["特殊要求"]);

                    info.PatientName = DBConvert.ConvertStringToString(dr["患者姓名"]);
                    info.Sex = DBConvert.ConvertStringToString(dr["性别"]);
                    info.Age = DBConvert.ConvertStringToString(dr["年龄"]);
                    info.Nationality = DBConvert.ConvertStringToString(dr["国籍"]);
                    info.Folk = DBConvert.ConvertStringToString(dr["民族"]);
                    info.PatientCount = DBConvert.ConvertStringToString(dr["患者人数"]);
                    info.Judge = DBConvert.ConvertStringToString(dr["初步判断"]);
                    info.Remark = DBConvert.ConvertStringToString(dr["备注"]);

                    info.StationName = DBConvert.ConvertStringToString(dr["分站"]);
                    return info;
                }
            }
            catch (Exception ex)
            {
                Log4Net.LogError("StatisticsDAL/PrintCommand()", ex.ToString());
                return null;
            }
        }
        #endregion
    }
}

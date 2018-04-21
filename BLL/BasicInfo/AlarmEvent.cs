using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;

namespace Anchor.FA.BLL.BasicInfo
{
    public class AlarmEvent
    {
        private static DAL.BasicInfo.AlarmEvent alarm1 = new DAL.BasicInfo.AlarmEvent();
        public string Update(TAlarmEvent entity)
        {
            return alarm1.Update(entity);
        }
        //public object AlarmEventLoad(DateTime begin, DateTime end, int page, int rows, string order, string sort)
        //{
        //    return DAL.BasicInfo.AlarmEvent.AlarmEventLoad(begin, end, page, rows, order, sort);
        //}
        public List<TZArea> LoadAreas()
        {
            return DAL.BasicInfo.AlarmEvent.LoadAreas();
        }
        public List<TZAccidentLevel> LoadAccidentLevels()
        {
            return DAL.BasicInfo.AlarmEvent.LoadAccidentLevels();
        }
        public List<TPerson> LoadDis(Anchor.FA.Utility.ButtonPower p, C_WorkerDetail userDetail)
        {
            return DAL.BasicInfo.AlarmEvent.LoadDis(p, userDetail);
        }
        //public List<TStation> LoadStations()
        //{
        //    return DAL.BasicInfo.AlarmEvent.LoadStations();
        //}
        //public List<TAmbulance> LoadAlums()
        //{
        //    return DAL.BasicInfo.AlarmEvent.LoadAlums();
        //}
        public List<TZAlarmEventType> LoadAlarmTypes()
        {
            return DAL.BasicInfo.AlarmEvent.LoadAlarmTypes();
        }
        public List<TZAlarmEventOrigin> LoadAlarmOriTypes()
        {
            return DAL.BasicInfo.AlarmEvent.LoadAlarmOriTypes();
        }
        public List<TZAccidentType> LoadAccidentType()
        {
            return DAL.BasicInfo.AlarmEvent.LoadAccidentType();
        }
        public List<TParameterAcceptInfo> LoadParameterAcceptInfo()
        {
            return DAL.BasicInfo.AlarmEvent.LoadParameterAcceptInfo();
        }
        public List<TZAmbulanceState> LoadAmbulanceStateInfo()
        {
            return DAL.BasicInfo.AlarmEvent.LoadAmbulanceStateInfo();
        }
        public object AlarmEventSearch(DateTime begin, DateTime end, string c_begin, string c_end, string tel, string Addr, string Dri,
            string Doc, string Nur, string Dis, string sta, string Alum, string type, string ori
            , string SuffererName, string ZhuSu, string SendAddress, string IllState, string AlarmEventCode
            ,string IsTest, string judge
            , int page, int rows, string order, string sort, Anchor.FA.Utility.ButtonPower p, int WorkerID)
        {
            return DAL.BasicInfo.AlarmEvent.AlarmEventSearch(begin, end,c_begin,c_end, tel, Addr, Dri, Doc, Nur,
                Dis, sta, Alum, type, ori, SuffererName, ZhuSu, SendAddress, IllState, AlarmEventCode, IsTest, judge, page, rows, order, sort, p, WorkerID);
        }
        public string AccLoad(string id, out TAlarmEvent tae, out List<TAcceptEvent> tacLs, out List<TTask> ttLs, out List<TAlarmCall> acLs)
        {
            return DAL.BasicInfo.AlarmEvent.AccLoad(id, out tae, out tacLs, out ttLs, out acLs);
        }
        public string getAlarmAllShow(string id, out C_AlarmEventInfo tae, out List<C_AccInfo> tacLs, out List<C_TaskInfoDetail> ttLs, out List<C_AmbulanceStateTimeInfo> tastLs, out List<C_AlarmCallInfo> acLs)
        {
            return DAL.BasicInfo.AlarmEvent.getAlarmAllShow(id, out tae, out tacLs, out ttLs, out tastLs, out acLs);
        }
        /// <summary>
        /// 获取调度个性名头
        /// 在view里不知道怎么写函数
        /// </summary>
        /// <returns></returns>
        public static string GetText(string ID, List<Anchor.FA.Model.TParameterAcceptInfo> tpaLs)
        {
            Anchor.FA.Model.TParameterAcceptInfo tpa = tpaLs.FirstOrDefault(t => t.控件名称 == ID);
            if (tpa.是否可见)
            {
                return tpa.默认值;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 获取调度个性 是否必填
        /// </summary>
        public static bool GetIsBiTian(string ID, List<Anchor.FA.Model.TParameterAcceptInfo> tpaLs)
        {
            Anchor.FA.Model.TParameterAcceptInfo tpa = tpaLs.FirstOrDefault(t => t.控件名称 == ID);
            if (tpa.是否可见 && tpa.是否必填)
            {
                
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 获取调度个性 是否必填 Html
        /// </summary>
        public static string GetIsBiTianHtml(string ID, List<Anchor.FA.Model.TParameterAcceptInfo> tpaLs)
        {
            Anchor.FA.Model.TParameterAcceptInfo tpa = tpaLs.FirstOrDefault(t => t.控件名称 == ID);
            if (tpa.是否可见)
            {
                if (tpa.是否必填)
                {
                    return "<span style=\"color:Red;\">" + tpa.默认值+ "</span>";
                }
                else
                {
                    return tpa.默认值;
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 获取调度 车辆状态名称
        /// </summary>
        /// <returns></returns>
        public static string GetAmbulanceStateName(TZAmbulanceState TZAmS)
        {
            if (TZAmS.是否有效)
            {
                return TZAmS.名称 + "时刻：";
            }
            else
            {
                return "";
            }
        }


        public static List<C_ModifyRecord> GetModifyRecord(string eventCode, out long total)
        {
            return DAL.BasicInfo.AlarmEvent.GetModifyRecord(eventCode, out total);
        }
        public static object GetStationMsgs(int PageSize, int PageIndex, string alarmEventCode)
        {
            return DAL.BasicInfo.AlarmEvent.GetStationMsgs(PageSize, PageIndex, alarmEventCode);
        }

        public C_AlarmEventInfo GetAlarmInfo(string eventCode)
        {
            return DAL.BasicInfo.AlarmEvent.GetAlarmInfo(eventCode);
        }
        public C_AccInfo GetAccInfo(string eventCode, int order)
        {
            return DAL.BasicInfo.AlarmEvent.GetAccInfo(eventCode, order);
        }
        public object GetAccTel(string code)
        {
            return DAL.BasicInfo.AlarmEvent.GetAccTel(code);
        }
        public object AumLoad(string code, int order)
        {
            return DAL.BasicInfo.AlarmEvent.AumLoad(code, order);
        }
        public C_TaskInfo GetTaskInfo(string Code)
        {
            return DAL.BasicInfo.AlarmEvent.GetTaskInfo(Code);
        }

        public List<C_ORGANIZE_TREE> GetAccidentTypeTree(string Code)
        {
            List<C_ORGANIZE_TREE> mtmList = (from p in DAL.BasicInfo.AlarmEvent.LoadAccidentType()
                                             //where p.上级编码==0
                                             select new C_ORGANIZE_TREE
                                             {
                                                 id = p.编码.ToString(),
                                                 text = p.名称,
                                                 ParentID = p.上级编码.ToString()
                                             }).ToList();
            Tree tr = new Tree();
            return tr.GetUnitTree(mtmList, Code);
        }

        #region 修改录音相关

        /// <summary>
        /// 关联到事件相关 通话类型
        /// </summary>
        public List<TZAlarmCallType> LoadAlarmCallType()//为什么不用static类型
        {
            return DAL.BasicInfo.AlarmEvent.LoadAlarmCallType();
        }
        /// <summary>
        /// 取消关联到事件 通话类型
        /// </summary>
        public List<TZCallType> LoadCallType()
        {
            return DAL.BasicInfo.AlarmEvent.LoadCallType();
        }


        /// <summary>
        /// 取消录音与事件的关联
        /// </summary>
        public static string UnLinkCalls(DateTime tonghuashike, string taihao, string callTypeCode)
        {
            return DAL.BasicInfo.AlarmEvent.UnLinkCalls(tonghuashike, taihao, callTypeCode);
        }
        /// <summary>
        /// 关联录音与事件
        /// </summary>
        public static string LinkCalls(DateTime tonghuashike, string taihao, string eventCode, string callTypeCode)
        {
            return DAL.BasicInfo.AlarmEvent.LinkCalls(tonghuashike, taihao, eventCode, callTypeCode);
        }
        /// <summary>
        /// 查询事件无关的电话
        /// </summary>
        public object GetAlarmCallOthers(int pageIndex, int pageSize, DateTime m_BeginTime, DateTime m_EndTime,
            string deskNumber, string attemperCode, string callTypeCode, string callNumber, string recordNumber, string isCallOut
            , string remark,Anchor.FA.Utility.ButtonPower p, C_WorkerDetail userDetail)
        {
            return DAL.BasicInfo.AlarmEvent.GetAlarmCallOthers(pageIndex, pageSize, m_BeginTime, m_EndTime,
             deskNumber, attemperCode, callTypeCode, callNumber, recordNumber, isCallOut
            , remark, p, userDetail);
        }

        #endregion

    }
}

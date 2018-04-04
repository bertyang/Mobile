using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    public class C_AlarmEventInfo
    //{
    //    public string Name { get; set; }
    //    public string FirstAccTime { get; set; }
    //    public string FirstDis { get; set; }
    //    public string FirstTel { get; set; }
    //    public string AccTimes { get; set; }
    //    public string FirstSendTime { get; set; }
    //    public string Area { get; set; }
    //    public string Ori { get; set; }
    //    public string Type { get; set; }
    //    public string AccidentType { get; set; }
    //}
    {
        private string m_EventCode;//事件编码
        /// <summary>
        /// 事件编码--
        /// </summary>
        public string EventCode
        {
            get { return m_EventCode; }
            set { m_EventCode = value; }
        }
        private string m_FirstAlarmCall;//首次呼救电话
        /// <summary>
        /// 首次呼救电话--
        /// </summary>
        public string FirstAlarmCall
        {
            get { return m_FirstAlarmCall; }
            set { m_FirstAlarmCall = value; }
        }
        private string m_EvetnName;//事件名称
        /// <summary>
        /// 事件名称--
        /// </summary>
        public string EvetnName
        {
            get { return m_EvetnName; }
            set { m_EvetnName = value; }
        }
        private int m_EventSourceCode;//事件来源编码
        /// <summary>
        /// 事件来源编码--
        /// </summary>
        public int EventSourceCode
        {
            get { return m_EventSourceCode; }
            set { m_EventSourceCode = value; }
        }
        private int m_EventTypeCode;//事件类型编码
        /// <summary>
        /// 事件类型编码--
        /// </summary>
        public int EventTypeCode
        {
            get { return m_EventTypeCode; }
            set { m_EventTypeCode = value; }
        }
        private int m_AccidentTypeCode;//事故类型编码
        /// <summary>
        /// 事故类型编码--
        /// </summary>
        public int AccidentTypeCode
        {
            get { return m_AccidentTypeCode; }
            set { m_AccidentTypeCode = value; }
        }
        private int m_AccidentLevelCode;//事故等级编码
        /// <summary>
        /// 事故等级编码--
        /// </summary>
        public int AccidentLevelCode
        {
            get { return m_AccidentLevelCode; }
            set { m_AccidentLevelCode = value; }
        }
        private bool m_IsTest;//是否测试
        /// <summary>
        /// 是否测试
        /// </summary>
        public bool IsTest
        {
            get { return m_IsTest; }
            set { m_IsTest = value; }
        }
        private int m_AcceptCount;//受理次数
        /// <summary>
        /// 受理次数--
        /// </summary>
        public int AcceptCount
        {
            get { return m_AcceptCount; }
            set { m_AcceptCount = value; }
        }
        private int m_CancelAcceptCount;//撤消受理数
        /// <summary>
        /// 撤消受理数--
        /// </summary>
        public int CancelAcceptCount
        {
            get { return m_CancelAcceptCount; }
            set { m_CancelAcceptCount = value; }
        }
        private int m_TransactTaskCount;//执行任务总数
        /// <summary>
        /// 执行任务总数--
        /// </summary>
        public int TransactTaskCount
        {
            get { return m_TransactTaskCount; }
            set { m_TransactTaskCount = value; }
        }
        private int m_NonceTransactTaskCount;//当前执行任务数
        /// <summary>
        /// 当前执行任务数--
        /// </summary>
        public int NonceTransactTaskCount
        {
            get { return m_NonceTransactTaskCount; }
            set { m_NonceTransactTaskCount = value; }
        }
        private int m_BreakTaskCount;//中止任务数
        /// <summary>
        /// 中止任务数--
        /// </summary>
        public int BreakTaskCount
        {
            get { return m_BreakTaskCount; }
            set { m_BreakTaskCount = value; }
        }
        private string m_FirstDisptcher;//首次调度员编码
        /// <summary>
        /// 首次调度员编码--
        /// </summary>
        public string FirstDisptcher
        {
            get { return m_FirstDisptcher; }
            set { m_FirstDisptcher = value; }
        }
        private Nullable<DateTime> m_FirstAcceptTime = null;//首次受理时刻
        /// <summary>
        /// 首次受理时刻--
        /// </summary>
        public Nullable<DateTime> FirstAcceptTime
        {
            get { return m_FirstAcceptTime; }
            set { m_FirstAcceptTime = value; }
        }
        private Nullable<DateTime> m_HangUpTime;//挂起时刻
        /// <summary>
        /// 挂起时刻--
        /// </summary>
        public Nullable<DateTime> HangUpTime
        {
            get { return m_HangUpTime; }
            set { m_HangUpTime = value; }
        }
        private Nullable<DateTime> m_BespeakTime;//预约时刻
        /// <summary>
        /// 预约时刻--
        /// </summary>
        public Nullable<DateTime> BespeakTime
        {
            get { return m_BespeakTime; }
            set { m_BespeakTime = value; }
        }
        private Nullable<DateTime> m_FirstSendAmbTime;//首次派车时刻
        /// <summary>
        /// 首次派车时刻--
        /// </summary>
        public Nullable<DateTime> FirstSendAmbTime
        {
            get { return m_FirstSendAmbTime; }
            set { m_FirstSendAmbTime = value; }
        }
        private bool m_IsHangUp;//是否挂起
        /// <summary>
        /// 是否挂起--
        /// </summary>
        public bool IsHangUp
        {
            get { return m_IsHangUp; }
            set { m_IsHangUp = value; }
        }
        private bool m_IsLabel;//是否标注
        /// <summary>
        /// 是否标注
        /// </summary>
        public bool IsLabel
        {
            get { return m_IsLabel; }
            set { m_IsLabel = value; }
        }

        private string m_AccidentCode;
        /// <summary>
        /// 所属事故编码
        /// </summary>
        public string AccidentCode
        {
            get { return m_AccidentCode; }
            set { m_AccidentCode = value; }
        }
        private double m_X;//X坐标
        /// <summary>
        /// X坐标
        /// </summary>
        public double X
        {
            get { return m_X; }
            set { m_X = value; }
        }
        private double m_Y;//Y坐标
        /// <summary>
        /// Y坐标
        /// </summary>
        public double Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }
        private string m_Area;//区域
        /// <summary>
        /// 区域--
        /// </summary>
        public string Area
        {
            get { return m_Area; }
            set { m_Area = value; }
        }
        private string m_TieUpDeskCode;//占用台号
        /// <summary>
        /// 占用台号
        /// </summary>
        public string TieUpDeskCode
        {
            get { return m_TieUpDeskCode; }
            set { m_TieUpDeskCode = value; }
        }
        private int m_CenterCode;//中心编码
        /// <summary>
        /// 中心编码
        /// </summary>
        public int CenterCode
        {
            get { return m_CenterCode; }
            set { m_CenterCode = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private bool m_IsFinish;//表单完成标志
        /// <summary>
        /// 表单完成标志
        /// </summary>
        public bool IsFinish
        {
            get { return m_IsFinish; }
            set { m_IsFinish = value; }
        }
        //-----------------------------以上为实际表格TAlarmEvent中的 字段名
        private string m_EventSource;
        /// <summary>
        /// 事件来源--
        /// </summary>
        public string EventSource
        {
            get { return m_EventSource; }
            set { m_EventSource = value; }
        }
        private string m_EventType;
        /// <summary>
        /// 事件类型--
        /// </summary>
        public string EventType
        {
            get { return m_EventType; }
            set { m_EventType = value; }
        }
        private string m_AccidentType;
        /// <summary>
        /// 事故类型--
        /// </summary>
        public string AccidentType
        {
            get { return m_AccidentType; }
            set { m_AccidentType = value; }
        }
        private string m_AccidentLevel;
        /// <summary>
        /// 事故等级--
        /// </summary>
        public string AccidentLevel
        {
            get { return m_AccidentLevel; }
            set { m_AccidentLevel = value; }
        }
        private string m_FirstDisptcherName;
        /// <summary>
        /// 首次受理调度名称--
        /// </summary>
        public string FirstDisptcherName
        {
            get { return m_FirstDisptcherName; }
            set { m_FirstDisptcherName = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    public class StationCommandInfo
    {
        private string m_StationName;
        /// <summary>
        /// 所属分站
        /// </summary>
        public string StationName
        {
            get { return m_StationName; }
            set { m_StationName = value; }
        }
        private string m_EventCode;
        /// <summary>
        /// 事件编码(呼救号)
        /// </summary>
        public string EventCode
        {
            get { return m_EventCode; }
            set { m_EventCode = value; }
        }
        private string m_ReceiveRecordCommandTime;
        ///<summary>
        /// 收单时刻 
        ///</summary>
        public string ReceiveRecordCommandTime
        {
            get { return m_ReceiveRecordCommandTime; }
            set { m_ReceiveRecordCommandTime = value; }
        }
        private string m_DispatcherWorkid;
        /// <summary>
        /// 调度员工号
        /// </summary>
        public string DispatcherWorkid
        {
            get { return m_DispatcherWorkid; }
            set { m_DispatcherWorkid = value; }
        }
        private string m_DeskID;
        /// <summary>
        /// 台号
        /// </summary>
        public string DeskID
        {
            get { return m_DeskID; }
            set { m_DeskID = value; }
        }
        private string m_LocalAddr;
        /// <summary>
        /// 现场地址
        /// </summary>
        public string LocalAddr
        {
            get { return m_LocalAddr; }
            set { m_LocalAddr = value; }
        }
        private string m_WaitAddr;
        /// <summary>
        /// 等车地址
        /// </summary>
        public string WaitAddr
        {
            get { return m_WaitAddr; }
            set { m_WaitAddr = value; }
        }
        private string m_SendAddr;
        /// <summary>
        /// 送往地点
        /// </summary>
        public string SendAddr
        {
            get { return m_SendAddr; }
            set { m_SendAddr = value; }
        }
        private string m_AlarmTel;
        /// <summary>
        /// 呼救电话
        /// </summary>
        public string AlarmTel
        {
            get { return m_AlarmTel; }
            set { m_AlarmTel = value; }
        }
        private string m_LinkTel;
        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkTel
        {
            get { return m_LinkTel; }
            set { m_LinkTel = value; }
        }
        private string m_LinkPerson;
        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkPerson
        {
            get { return m_LinkPerson; }
            set { m_LinkPerson = value; }
        }
        private string m_RealSignbunch;
        /// <summary>
        /// 出动车辆
        /// </summary>
        public string RealSignbunch
        {
            get { return m_RealSignbunch; }
            set { m_RealSignbunch = value; }
        }
        private string m_AmbPersons;
        /// <summary>
        /// 随车人员
        /// </summary>
        public string AmbPersons
        {
            get { return m_AmbPersons; }
            set { m_AmbPersons = value; }
        }
        private string m_SpecialNeed;
        /// <summary>
        /// 特殊要求
        /// </summary>
        public string SpecialNeed
        {
            get { return m_SpecialNeed; }
            set { m_SpecialNeed = value; }
        }
        private string m_PatientName;
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName
        {
            get { return m_PatientName; }
            set { m_PatientName = value; }
        }
        private string m_Sex;
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get { return m_Sex; }
            set { m_Sex = value; }
        }
        private string m_Age;
        /// <summary>
        /// 年龄
        /// </summary>
        public string Age
        {
            get { return m_Age; }
            set { m_Age = value; }
        }
        private string m_Folk;
        /// <summary>
        /// 民族
        /// </summary>
        public string Folk
        {
            get { return m_Folk; }
            set { m_Folk = value; }
        }
        private string m_Nationality;
        /// <summary>
        /// 国籍
        /// </summary>
        public string Nationality
        {
            get { return m_Nationality; }
            set { m_Nationality = value; }
        }
        private string m_PatientCount;
        /// <summary>
        /// 患者人数
        /// </summary>
        public string PatientCount
        {
            get { return m_PatientCount; }
            set { m_PatientCount = value; }
        }

        private string m_Judge;
        /// <summary>
        /// 主诉判断
        /// </summary>
        public string Judge
        {
            get { return m_Judge; }
            set { m_Judge = value; }
        }

        private string m_Remark;
        /// <summary>
        /// 备注
        /// </summary>   
        public string Remark
        {
            get { return m_Remark; }
            set { m_Remark = value; }
        }


    }
}

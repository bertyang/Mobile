using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    /// <summary>
    /// 受理单打印-事件信息
    /// 刘爱青
    /// 2011.4.7
    /// </summary>
    public class AttemperAlarmInfo
    {
        private string m_AlarmCode;
        /// <summary>
        /// 事件编码
        /// </summary>
        public string AlarmCode
        {
            get { return m_AlarmCode; }
            set { m_AlarmCode = value; }
        }
        private string m_AlarmCall;
        /// <summary>
        /// 呼救电话
        /// </summary>
        public string AlarmCall
        {
            get { return m_AlarmCall; }
            set { m_AlarmCall = value; }
        }
        private string m_Name;
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        private string m_Judge;
        /// <summary>
        /// 初步判断
        /// </summary>
        public string Judge
        {
            get { return m_Judge; }
            set { m_Judge = value; }
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
        private string m_Sex;
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get { return m_Sex; }
            set { m_Sex = value; }
        }
        private string m_IllState;
        /// <summary>
        /// 病情
        /// </summary>
        public string IllState
        {
            get { return m_IllState; }
            set { m_IllState = value; }
        }
        private string m_LinkPhone;
        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkPhone
        {
            get { return m_LinkPhone; }
            set { m_LinkPhone = value; }
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
        private string m_IsNeedLitter;
        /// <summary>
        /// 是否需要担架
        /// </summary>
        public string IsNeedLitter
        {
            get { return m_IsNeedLitter; }
            set { m_IsNeedLitter = value; }
        }
        private string m_ExtensionPhone;
        /// <summary>
        /// 分机
        /// </summary>
        public string ExtensionPhone
        {
            get { return m_ExtensionPhone; }
            set { m_ExtensionPhone = value; }
        }
        private string m_MinZu;
        /// <summary>
        /// 民族
        /// </summary>
        public string MinZu
        {
            get { return m_MinZu; }
            set { m_MinZu = value; }
        }
        private string m_Request;
        /// <summary>
        /// 特殊要求
        /// </summary>
        public string Request
        {
            get { return m_Request; }
            set { m_Request = value; }
        }
        private string m_LocalAddr;
        /// <summary>
        /// 现场地点
        /// </summary>
        public string LocalAddr
        {
            get { return m_LocalAddr; }
            set { m_LocalAddr = value; }
        }
        private string m_AlarmType;
        /// <summary>
        /// 事件类型
        /// </summary>
        public string AlarmType
        {
            get { return m_AlarmType; }
            set { m_AlarmType = value; }
        }
        private string m_SendAddr;
        /// <summary>
        /// 接收医院
        /// </summary>
        public string SendAddr
        {
            get { return m_SendAddr; }
            set { m_SendAddr = value; }
        }
        private string m_WaitAddr;
        /// <summary>
        /// 等车地点
        /// </summary>
        public string WaitAddr
        {
            get { return m_WaitAddr; }
            set { m_WaitAddr = value; }
        }
    }
}

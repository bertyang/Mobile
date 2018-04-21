using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    /// <summary>
    /// 受理单打印-出车信息
    /// 刘爱青
    /// 2011.4.7
    /// </summary>
    public class AttemperTaskInfo
    {
        private string m_TaskCode;
        /// <summary>
        /// 任务编码
        /// </summary>
        public string TaskCode
        {
            get { return m_TaskCode; }
            set { m_TaskCode = value; }
        }
        private string m_AlarmEventCode;
        /// <summary>
        /// 事件编码
        /// </summary>
        public string AlarmEventCode
        {
            get { return m_AlarmEventCode; }
            set { m_AlarmEventCode = value; }
        }
        private string m_Order;
        /// <summary>
        /// 受理序号
        /// </summary>
        public string Order
        {
            get { return m_Order; }
            set { m_Order = value; }
        }
        private string m_AttemperPerson;
        /// <summary>
        /// 责任调度人
        /// </summary>
        public string AttemperPerson
        {
            get { return m_AttemperPerson; }
            set { m_AttemperPerson = value; }
        }
        private string m_AmbMark;
        /// <summary>
        /// 车辆实际标识
        /// </summary>
        public string AmbMark
        {
            get { return m_AmbMark; }
            set { m_AmbMark = value; }
        }
        private string m_ReceiveTime;
        /// <summary>
        /// 接收命令时刻
        /// </summary>
        public string ReceiveTime
        {
            get { return m_ReceiveTime; }
            set { m_ReceiveTime = value; }
        }
        private string m_OutTime;
        /// <summary>
        /// 出车时刻
        /// </summary>
        public string OutTime
        {
            get { return m_OutTime; }
            set { m_OutTime = value; }
        }
        private string m_ArriveTime;
        /// <summary>
        /// 到达现场时刻
        /// </summary>
        public string ArriveTime
        {
            get { return m_ArriveTime; }
            set { m_ArriveTime = value; }
        }
        private string m_LeaveTime;
        /// <summary>
        /// 离开现场时刻
        /// </summary>
        public string LeaveTime
        {
            get { return m_LeaveTime; }
            set { m_LeaveTime = value; }
        }
        private string m_ArriHsplTime;
        /// <summary>
        /// 到达医院时刻
        /// </summary>
        public string ArriHsplTime
        {
            get { return m_ArriHsplTime; }
            set { m_ArriHsplTime = value; }
        }
        private string m_FinishTime;
        /// <summary>
        /// 任务完成时刻
        /// </summary>
        public string FinishTime
        {
            get { return m_FinishTime; }
            set { m_FinishTime = value; }
        }
        private string m_ReturnTime;
        /// <summary>
        /// 返回站中时刻
        /// </summary>
        public string ReturnTime
        {
            get { return m_ReturnTime; }
            set { m_ReturnTime = value; }
        }
        private string m_TaskResult;
        /// <summary>
        /// 出车结果
        /// </summary>
        public string TaskResult
        {
            get { return m_TaskResult; }
            set { m_TaskResult = value; }
        }
        private string m_TaskAbdReason;
        /// <summary>
        /// 异常结束原因
        /// </summary>
        public string TaskAbdReason
        {
            get { return m_TaskAbdReason; }
            set { m_TaskAbdReason = value; }
        }
        private string m_Driver;
        /// <summary>
        /// 司机
        /// </summary>
        public string Driver
        {
            get { return m_Driver; }
            set { m_Driver = value; }
        }
        private string m_Doctor;
        /// <summary>
        /// 医生
        /// </summary>
        public string Doctor
        {
            get { return m_Doctor; }
            set { m_Doctor = value; }
        }
        private string m_Nurse;
        /// <summary>
        /// 护士
        /// </summary>
        public string Nurse
        {
            get { return m_Nurse; }
            set { m_Nurse = value; }
        }
        private string m_Litter;
        /// <summary>
        /// 担架工
        /// </summary>
        public string Litter
        {
            get { return m_Litter; }
            set { m_Litter = value; }
        }
        private string m_JiJiuYuan;
        /// <summary>
        /// 急救员
        /// </summary>
        public string JiJiuYuan
        {
            get { return m_JiJiuYuan; }
            set { m_JiJiuYuan = value; }
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
        private string m_Station;
        /// <summary>
        /// 分站
        /// </summary>
        public string Station
        {
            get { return m_Station; }
            set { m_Station = value; }
        }
        private string m_AmbPlate;
        /// <summary>
        /// 车牌号码
        /// </summary>
        public string AmbPlate
        {
            get { return m_AmbPlate; }
            set { m_AmbPlate = value; }
        }
    }
}

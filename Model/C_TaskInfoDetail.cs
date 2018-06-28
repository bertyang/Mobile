using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    public class C_TaskInfoDetail
    {
        private string m_Code;//任务编码
        /// <summary>
        /// 任务编码--
        /// </summary>
        public string Code
        {
            get { return m_Code; }
            set { m_Code = value; }
        }

        private string m_EventCode;//事件编码
        /// <summary>
        /// 事件编码--
        /// </summary>
        public string EventCode
        {
            get { return m_EventCode; }
            set { m_EventCode = value; }
        }

        private int m_AcceptOrder;//受理序号
        /// <summary>
        /// 受理序号--
        /// </summary>
        public int AcceptOrder
        {
            get { return m_AcceptOrder; }
            set { m_AcceptOrder = value; }
        }

        private int m_TaskOrder;//任务流水号
        /// <summary>
        /// 任务流水号--
        /// </summary>
        public int TaskOrder
        {
            get { return m_TaskOrder; }
            set { m_TaskOrder = value; }
        }

        private string m_UserOrder;//用户流水号 给用户看的
        /// <summary>
        /// 用户流水号 给用户看的--
        /// </summary>
        public string UserOrder
        {
            get { return m_UserOrder; }
            set { m_UserOrder = value; }
        }

        private string m_OperatorCode;//责任调度人编码
        /// <summary>
        /// 责任调度人编码--
        /// </summary>
        public string OperatorCode
        {
            get { return m_OperatorCode; }
            set { m_OperatorCode = value; }
        }

        private string m_StationCode;//分站编码
        /// <summary>
        /// 分站编码
        /// </summary>
        public string StationCode
        {
            get { return m_StationCode; }
            set { m_StationCode = value; }
        }

        private string m_AmbulanceCode;//车辆编码
        /// <summary>
        /// 车辆编码--
        /// </summary>
        public string AmbulanceCode
        {
            get { return m_AmbulanceCode; }
            set { m_AmbulanceCode = value; }
        }

        private bool m_IsPerforming;//是否执行中
        /// <summary>
        /// 是否执行中--
        /// </summary>
        public bool IsPerforming
        {
            get { return m_IsPerforming; }
            set { m_IsPerforming = value; }
        }
        private DateTime m_CreateTaskTime;//生成任务时刻
        /// <summary>
        /// 生成任务时刻--
        /// </summary>
        public DateTime CreateTaskTime
        {
            get { return m_CreateTaskTime; }
            set { m_CreateTaskTime = value; }
        }
        private Nullable<DateTime> m_ReceiveCmdTime;//接收命令时刻 接收任务时刻
        /// <summary>
        /// 接收命令时刻-- 接收任务时刻
        /// </summary>
        public Nullable<DateTime> ReceiveCmdTime
        {
            get { return m_ReceiveCmdTime; }
            set { m_ReceiveCmdTime = value; }
        }

        private Nullable<DateTime> m_AmbulanceLeaveTime;//出车时刻
        /// <summary>
        /// 出车时刻--
        /// </summary>
        public Nullable<DateTime> AmbulanceLeaveTime
        {
            get { return m_AmbulanceLeaveTime; }
            set { m_AmbulanceLeaveTime = value; }
        }

        private Nullable<DateTime> m_ArriveSceneTime;//到达现场时刻
        /// <summary>
        /// 到达现场时刻--
        /// </summary>
        public Nullable<DateTime> ArriveSceneTime
        {
            get { return m_ArriveSceneTime; }
            set { m_ArriveSceneTime = value; }
        }

        private Nullable<DateTime> m_LeaveSceneTime;//离开现场时刻
        /// <summary>
        /// 离开现场时刻--
        /// </summary>
        public Nullable<DateTime> LeaveSceneTime
        {
            get { return m_LeaveSceneTime; }
            set { m_LeaveSceneTime = value; }
        }

        private Nullable<DateTime> m_ArriveHospitalTime;//到达医院时刻
        /// <summary>
        /// 到达医院时刻--
        /// </summary>
        public Nullable<DateTime> ArriveHospitalTime
        {
            get { return m_ArriveHospitalTime; }
            set { m_ArriveHospitalTime = value; }
        }

        private Nullable<DateTime> m_FinishTime;//完成时刻
        /// <summary>
        /// 完成时刻--
        /// </summary>
        public Nullable<DateTime> FinishTime
        {
            get { return m_FinishTime; }
            set { m_FinishTime = value; }
        }

        private Nullable<DateTime> m_ReturnTime;//返回站中时刻
        /// <summary>
        /// 返回站中时刻--
        /// </summary>
        public Nullable<DateTime> ReturnTime
        {
            get { return m_ReturnTime; }
            set { m_ReturnTime = value; }
        }

        private Nullable<double> m_HelpDistance;//急救公里数
        /// <summary>
        /// 急救公里数--
        /// </summary>
        public Nullable<double> HelpDistance
        {
            get { return m_HelpDistance; }
            set { m_HelpDistance = value; }
        }

        private double m_TravelDistance;//行驶公里数
        /// <summary>
        /// 行驶公里数--
        /// </summary>
        public double TravelDistance
        {
            get { return m_TravelDistance; }
            set { m_TravelDistance = value; }
        }

        private bool m_IsNormalFinish;//是否正常结束
        /// <summary>
        /// 是否正常结束--
        /// </summary>
        public bool IsNormalFinish
        {
            get { return m_IsNormalFinish; }
            set { m_IsNormalFinish = value; }
        }

        private int m_AbnormalReasonId;//异常结束原因编码
        /// <summary>
        /// 异常结束原因编码--
        /// </summary>
        public int AbnormalReasonId
        {
            get { return m_AbnormalReasonId; }
            set { m_AbnormalReasonId = value; }
        }

        private int m_CureAmount;//实际救治人数
        /// <summary>
        /// 实际救治人数--
        /// </summary>
        public int CureAmount
        {
            get { return m_CureAmount; }
            set { m_CureAmount = value; }
        }

        private bool m_IsFromStation;//是否站内出动
        /// <summary>
        /// 是否站内出动--
        /// </summary>
        public bool IsFromStation
        {
            get { return m_IsFromStation; }
            set { m_IsFromStation = value; }
        }

        private string m_Driver;//司机
        /// <summary>
        /// 司机--
        /// </summary>
        public string Driver
        {
            get { return m_Driver; }
            set { m_Driver = value; }
        }

        private string m_Doctor;//医生
        /// <summary>
        /// 医生--
        /// </summary>
        public string Doctor
        {
            get { return m_Doctor; }
            set { m_Doctor = value; }
        }

        private string m_Nurse;//护士
        /// <summary>
        /// 护士--
        /// </summary>
        public string Nurse
        {
            get { return m_Nurse; }
            set { m_Nurse = value; }
        }

        private string m_Litter;//担架工
        /// <summary>
        /// 担架工--
        /// </summary>
        public string Litter
        {
            get { return m_Litter; }
            set { m_Litter = value; }
        }

        private string m_Salver;//抢救员
        /// <summary>
        /// 抢救员--
        /// </summary>
        public string Salver
        {
            get { return m_Salver; }
            set { m_Salver = value; }
        }

        private string m_ReassignTaskCode;//改派前任务编码
        /// <summary>
        /// 改派前任务编码--
        /// </summary>
        public string ReassignTaskCode
        {
            get { return m_ReassignTaskCode; }
            set { m_ReassignTaskCode = value; }
        }

        private string m_realSendAddr;//实际送往地点
        /// <summary>
        /// 实际送往地点--
        /// </summary>
        public string RealSendAddr
        {
            get { return m_realSendAddr; }
            set { m_realSendAddr = value; }
        }

        private string m_Remark;//备注
        /// <summary>
        /// 备注--
        /// </summary>
        public string Remark
        {
            get { return m_Remark; }
            set { m_Remark = value; }
        }

        private int m_CenterId;//中心编码
        /// <summary>
        /// 中心编码--
        /// </summary>
        public int CenterId
        {
            get { return m_CenterId; }
            set { m_CenterId = value; }
        }
        //-----------------------------以上为实际表格TTask中的 字段名
        /// <summary>
        /// 分站名称--
        /// </summary>
        public string StationName { get; set; }
        /// <summary>
        /// 车牌号码--
        /// </summary>
        public string TradeMark { get; set; }
        /// <summary>
        /// 实际标识--
        /// </summary>
        public string RealSign { get; set; }
        /// <summary>
        /// 随车电话--
        /// </summary>
        public string FollowTel { get; set; }
        /// <summary>
        /// 车辆类型--
        /// </summary>
        public string AmbulanceTypeName { get; set; }
        /// <summary>
        /// 异常结束原因--
        /// </summary>
        public string AbnormalReasonName { get; set; }
    }
}
